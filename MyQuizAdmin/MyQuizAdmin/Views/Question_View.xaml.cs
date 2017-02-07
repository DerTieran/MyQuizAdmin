using System;
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
        public ObservableCollection<QuestionBlock> questionlist = new ObservableCollection<QuestionBlock>();
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
            var selectetQuestionnaire = cbx_questionListEdit.SelectedItem as QuestionBlock;

        }

        private void lbx_question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var question = lbx_question.SelectedItem as Question;
            if (lbx_question.SelectedItem != null)
                lbx_answer.ItemsSource = question.AnswerOption;
        }
        /*******************************
                  Click-Events
         *******************************/
        private void bt_questionlistDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var toRemove = cbx_questionListEdit.SelectedItem as QuestionBlock;
            questionlist.Remove(toRemove);
        }

        private void bt_questionlistAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (tbx_questionlist.Text!="")
            {
                QuestionBlock newQuestionnaire = new QuestionBlock();
                newQuestionnaire.Title = tbx_questionlist.Text;
                questionlist.Add(newQuestionnaire);
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
            newQuestion.Text = "neue Frage";
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
            selectetQuestion.AnswerOption.Remove(toRemove);
        }

        private void bt_answerAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectetQuestion = lbx_question.SelectedItem as Question;
            if (lbx_question.SelectedItem!=null && tbx_answer.Text!= "")
            {
                AnswerOption newAnswer = new AnswerOption();
                newAnswer.Text = tbx_answer.Text;
                if (selectetQuestion.AnswerOption!=null)
                    selectetQuestion.AnswerOption.Add(newAnswer);
            }
        }

        private void ts_typ_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ts_typ.IsOn)
                tbt_result.IsEnabled = false;
            else
                tbt_result.IsEnabled = true;
        }
    }
}
