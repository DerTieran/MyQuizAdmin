﻿using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using MyQuizAdmin.Models;
using System.Collections.Generic;

namespace MyQuizAdmin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Question_View : Page
    {
        public ObservableCollection<Questionnaire> questionlist = new ObservableCollection<Questionnaire>();
        public Request request = new Request();

        public Question_View()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            questionlist = await request.questionnaireRequest();
            cbx_questionListEdit.ItemsSource = questionlist;
            //lbx_question.ItemsSource = await request.questionRequest();
        }

        private async void cbx_questionListEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectetQuestionnaire = cbx_questionListEdit.SelectedItem as Questionnaire;

        }

        private void lbx_question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var question = lbx_question.SelectedItem as Question;
            if (lbx_question.SelectedItem!=null)
                lbx_answer.ItemsSource = question.awnsers;
        }
        /*******************************
                  Click-Events
         *******************************/
        private void bt_questionlistDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var toRemove = cbx_questionListEdit.SelectedItem as Questionnaire;
            questionlist.Remove(toRemove);
        }

        private void bt_questionlistAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Questionnaire newQuestionnaire = new Questionnaire();
            newQuestionnaire.name = tbx_questionlist.Text;
            questionlist.Add(newQuestionnaire);
            cbx_questionListEdit.SelectedItem = newQuestionnaire;
        }
        private void bt_questionDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var questionaire = cbx_questionListEdit.SelectedItem as Questionnaire;
            var toRemove = lbx_question.SelectedItem as Question;
            questionaire.questions.Remove(toRemove);
        }

        private void bt_questionAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            var questionaire = cbx_questionListEdit.SelectedItem as Questionnaire;
            Question newQuestion = new Question();
            newQuestion.text = "neue Frage";
            if (questionaire != null)
                questionaire.questions.Add(newQuestion);

            lbx_question.ItemsSource = questionaire.questions;
        }

        private void bt_answerDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectetQuestion = lbx_question.SelectedItem as Question;
            var toRemove = lbx_answer.SelectedItem as string;
            selectetQuestion.awnsers.Remove(toRemove);
        }

        private void bt_answerAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectetQuestion = lbx_question.SelectedItem as Question;
            selectetQuestion.awnsers.Add(tbx_answer.Text);
        }
    }
}
