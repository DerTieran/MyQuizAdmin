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
        public ObservableCollection<QuestionBlock> questionBlockList = new ObservableCollection<QuestionBlock>();
        public Request request = new Request();

        public Question_View()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            questionBlockList = await request.questionnaireRequest();
            QuestionBlock testQuestionnaire = new QuestionBlock();
            testQuestionnaire.title = "Testliste";
            Question testQuestion = new Question();
            testQuestion.Category = "Abstimmung";
            testQuestion.MultipleChoice = "Singlechoice";
            testQuestion.text = "testfrage";
            testQuestionnaire.questionList.Add(testQuestion);
            questionBlockList.Add(testQuestionnaire);
            cbx_questionListEdit.ItemsSource = questionBlockList;
        }


        /*******************************
          selection Changed Methoden
         *******************************/

        private void lbx_question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedQuestion = lbx_question.SelectedItem as Question;
            if (lbx_question.SelectedItem != null)
            {
                lbx_answer.ItemsSource = selectedQuestion.answerOption;
                cbx_questionCategory.SelectedItem = selectedQuestion.Category;
                cbx_questionTyp.SelectedItem = selectedQuestion.MultipleChoice;
            }
        }

        private void cbx_questionCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbx_question.SelectedItem!=null)
            {
                var question = lbx_question.SelectedItem as Question;
                question.Category = cbx_questionCategory.SelectedItem as string;
            }
        }

        private void cbx_questionTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbx_question.SelectedItem != null)
            {
                var question = lbx_question.SelectedItem as Question;
                question.MultipleChoice = cbx_questionTyp.SelectedItem.ToString();
            }

        }

        private void lbx_answer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAnswer = lbx_answer.SelectedItem as AnswerOption;
            if (selectedAnswer.isCorrect == "isCorrect")
            {
                bt_result.Content = "Richtig";
            }
            else
            {
                bt_result.Content = "Falsch";
            }
        }

        /*******************************
                  Click-Events
         *******************************/
        private void bt_questionlistDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var toRemove = cbx_questionListEdit.SelectedItem as QuestionBlock;
            questionBlockList.Remove(toRemove);
        }

        private void bt_questionlistAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (tbx_questionlist.Text!="")
            {
                QuestionBlock newQuestionnaire = new QuestionBlock();
                newQuestionnaire.title = tbx_questionlist.Text;
                questionBlockList.Add(newQuestionnaire);
                cbx_questionListEdit.SelectedItem = newQuestionnaire;
            }

        }
        private void bt_questionDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var questionaire = cbx_questionListEdit.SelectedItem as QuestionBlock;
            var toRemove = lbx_question.SelectedItem as Question;
            questionaire.questionList.Remove(toRemove);
        }

        private void bt_questionAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            var questionaire = cbx_questionListEdit.SelectedItem as QuestionBlock;
            Question newQuestion = new Question();
            newQuestion.text = "neue Frage";
            if (questionaire != null)
            {
                questionaire.questionList.Add(newQuestion);
                lbx_question.ItemsSource = questionaire.questionList;
            }

        }

        private void bt_answerDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectetQuestion = lbx_question.SelectedItem as Question;
            var toRemove = lbx_answer.SelectedItem as AnswerOption;
            selectetQuestion.answerOption.Remove(toRemove);
        }

        private void bt_answerAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectetQuestion = lbx_question.SelectedItem as Question;
            if (lbx_question.SelectedItem!=null && tbx_answer.Text!= "")
            {
                AnswerOption newAnswer = new AnswerOption();
                newAnswer.text = tbx_answer.Text;
                newAnswer.isCorrect = "isWrong";
                if (selectetQuestion.answerOption!=null)
                    selectetQuestion.answerOption.Add(newAnswer);
            }
        }


        private void bt_questionSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            request.questionairePost(questionBlockList);
        }

        private void bt_result_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        { 
            var selectedAnswer = lbx_answer.SelectedItem as AnswerOption;
            if (selectedAnswer!=null)
            {
                if (selectedAnswer.isCorrect == "isCorrect")
                {
                    selectedAnswer.isCorrect = "isWrong";
                    bt_result.Content = "Falsch";
                }
                else
                {
                    selectedAnswer.isCorrect = "isCorrect";
                    bt_result.Content = "Richtig";
                }

            }
            
        }


    }
}
