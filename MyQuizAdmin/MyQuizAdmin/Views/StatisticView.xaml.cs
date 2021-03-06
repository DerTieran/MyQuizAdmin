﻿using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using MyQuizAdmin.Models;
using System.Collections.Generic;
using MyQuizAdmin;

namespace MyQuizAdmin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatisticView : Page
    {       
        bool LayoutUpdateFlag = true;
        Request request = new Request();
        List<GivenAnswer> data = new List<GivenAnswer>();

        public StatisticView()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<Group> groupResponList = new List<Group>();
            groupResponList = await request.GetGroups();             
            cbx_groups.ItemsSource = groupResponList;          
        }

        private class QuestionResult
        {
            public long id { get; set; }
            public string text { get; set; }
            public List<Answer> answers { get; set; }

            public class Answer
            {
                public long id { get; set; }
                public string text { get; set; }
                public int count { get; set; } = 1;
            }
        }

        private List<QuestionResult> aggregateQuestionResults(List<GivenAnswer> givenAnswers)
        {
            return givenAnswers.Aggregate(new List<QuestionResult>(), (questionResults, givenAnswer) => {
                if (givenAnswer.Question != null && givenAnswer.AnswerOption != null)
                {
                    var foundQuestion = questionResults.Find(question => question.id == givenAnswer.Question.Id);
                    if (foundQuestion != null)
                    {
                        var foundAnswer = foundQuestion.answers.Find(answer => answer.id == givenAnswer.AnswerOption.Id);
                        if (foundAnswer != null)
                        {
                            foundAnswer.count = foundAnswer.count + 1;
                        }
                        else
                        {
                            foundQuestion.answers.Add(new QuestionResult.Answer
                            {
                                id = givenAnswer.AnswerOption.Id,
                                text = givenAnswer.AnswerOption.Text
                            });
                        }
                    }
                    else
                    {
                        questionResults.Add(new QuestionResult
                        {
                            id = givenAnswer.Question.Id,
                            text = givenAnswer.Question.Text,
                            answers = new List<QuestionResult.Answer> { new QuestionResult.Answer
                        {
                            id = givenAnswer.AnswerOption.Id,
                            text = givenAnswer.AnswerOption.Text
                        } }
                        });
                    }
                }
                return questionResults;
            });
        }

        private async void cbx_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txb_error.Text = "";
            lv_static.ItemsSource = null;
            prb_statistics.Visibility = Windows.UI.Xaml.Visibility.Visible;
            var SelectedGroup = cbx_groups.SelectedItem as Group;
            if (SelectedGroup != null)
            {
                lbx_people.ItemsSource = SelectedGroup.SingleTopics;
                data = await request.getGivenAnswersForGroup(SelectedGroup);
                var aggregatedResults = aggregateQuestionResults(data);
                if (aggregatedResults.Count > 0)
                {
                    lv_static.ItemsSource = aggregatedResults;
                }
            }
            if (lv_static.ItemsSource == null)
            {
                txb_error.Text = "Es wurden keine Daten gefunden";
            }
            prb_statistics.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private async void txb_searchpeople_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var topicList = await request.getTopicsForGroup(cbx_groups.SelectedItem as Group);
            if (lbx_people.ItemsSource != null)
            {
                this.lbx_people.ItemsSource = topicList.Where(w => w.Name.ToUpper().Contains(txb_searchpeople.Text.ToUpper()) || w.Name.ToUpper().Contains(txb_searchpeople.Text.ToUpper()));
                LayoutUpdateFlag = true;
            }
        }

        private async void lbx_people_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txb_error.Text = "";
            lv_static.ItemsSource = null;
            prb_statistics.Visibility = Windows.UI.Xaml.Visibility.Visible;
            var SelectedGroup = cbx_groups.SelectedItem as Group;
            var SelectedTopic = lbx_people.SelectedItem as SingleTopic;
            if (SelectedGroup != null && SelectedTopic != null)
            {
                data = await request.getGivenAnswersForTopicInGroup(SelectedTopic, SelectedGroup);
                var aggregatedResults = aggregateQuestionResults(data);
                if (aggregatedResults.Count > 0)
                {
                    lv_static.ItemsSource = aggregatedResults;
                }

            }
            if (lv_static.ItemsSource == null)
            {
                txb_error.Text = "Es wurden keine Daten gefunden";
            }
            prb_statistics.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            
        }
    }
}

