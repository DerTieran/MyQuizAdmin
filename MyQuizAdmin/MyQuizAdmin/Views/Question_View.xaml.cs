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
        public ObservableCollection<QuestionBlock> questionBlockList = new ObservableCollection<QuestionBlock>();
        public List<string> typeList = new List<string>() { "Singlechoice", "Multiplechoice" };
        public List<string> categoryList = new List<string>() {"Umfrage","Quiz" };
        public Request request = new Request();

        public Question_View()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            cbx_questionCategory.ItemsSource = categoryList;
            cbx_questionType.ItemsSource = typeList;
            questionBlockList = await request.questionnaireRequest();

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
                lbx_answer.ItemsSource = selectedQuestion.answerOptions;

                var categoryIndex = categoryList.IndexOf(selectedQuestion.Category);
                cbx_questionCategory.SelectedIndex = categoryIndex;
                var typeIndex = typeList.IndexOf(selectedQuestion.Type);
                if (cbx_questionCategory.SelectedItem as string != "Umfrage")
                    cbx_questionType.SelectedIndex = typeIndex;
            }
        }

        private void cbx_questionCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbx_question.SelectedItem!=null)
            {
                var question = lbx_question.SelectedItem as Question;
                question.Category = cbx_questionCategory.SelectedItem as string;
                if (question.Category == "Umfrage")
                {
                    cbx_questionType.IsEnabled = false;
                }
                else
                {
                    cbx_questionType.IsEnabled = true;
                }
            }
        }

        private void asd()
        {
            throw new NotImplementedException();
        }

        private void cbx_questionTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbx_question.SelectedItem != null)
            {
                var selectedQuestion = lbx_question.SelectedItem as Question;

                if (cbx_questionType.SelectedItem as string == "Singlechoice")
                {
                    int rightAnswerCount = 0;
                    foreach (AnswerOption answer in selectedQuestion.answerOptions)
                    {
                        if (answer.Result == "true")
                            rightAnswerCount++;
                    }
                    if (rightAnswerCount > 1)
                        cbx_questionType.SelectedIndex = 1;
                }
            }
        }


        /*******************************
                  Click-Events
         *******************************/
        private void bt_questionlistDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (cbx_questionListEdit.SelectedItem != null)
            {
                var toRemove = cbx_questionListEdit.SelectedItem as QuestionBlock;
                questionBlockList.Remove(toRemove);
            }

        }

        private void bt_questionlistAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (tbx_questionlist.Text!="")
            {
                QuestionBlock newQuestionnaire = new QuestionBlock();
                newQuestionnaire.Title = tbx_questionlist.Text;
                tbx_questionlist.Text = "";
                questionBlockList.Add(newQuestionnaire);
                cbx_questionListEdit.SelectedItem = newQuestionnaire;
            }

        }
        private void bt_questionDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (lbx_question.SelectedItem!=null)
            {
                var questionaire = cbx_questionListEdit.SelectedItem as QuestionBlock;
                var toRemove = lbx_question.SelectedItem as Question;
                questionaire.questions.Remove(toRemove);
            }

        }

        private void bt_questionAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var questionaire = cbx_questionListEdit.SelectedItem as QuestionBlock;
            if (questionaire != null)
            {
                Question newQuestion = new Question();
                newQuestion.Text = "neue Frage";
                questionaire.questions.Add(newQuestion);
            }

        }

        private void bt_answerDel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (lbx_answer.SelectedItem!=null)
            {
                var selectetQuestion = lbx_question.SelectedItem as Question;
                var toRemove = lbx_answer.SelectedItem as AnswerOption;
                selectetQuestion.answerOptions.Remove(toRemove);
            }

        }

        private void bt_answerAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectetQuestion = lbx_question.SelectedItem as Question;
            if (lbx_question.SelectedItem!=null && tbx_answer.Text!= "")
            {
                AnswerOption newAnswer = new AnswerOption();
                newAnswer.Text = tbx_answer.Text;
                tbx_answer.Text = "";
                newAnswer.Result = "false";
                if (selectetQuestion.answerOptions!=null)
                    selectetQuestion.answerOptions.Add(newAnswer);
            }
        }


        private void bt_questionSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (cbx_questionListEdit.SelectedItem != null)
                request.questionairePost(cbx_questionListEdit.SelectedItem as QuestionBlock);
        }

        private void bt_questionlistChange_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectedQuestionBlock = cbx_questionListEdit.SelectedItem as QuestionBlock;
            if (selectedQuestionBlock != null && tbx_questionlist.Text != "")
            {
                selectedQuestionBlock.Title = tbx_questionlist.Text;
                tbx_questionlist.Text = "";
                cbx_questionListEdit.ItemsSource = null;
                cbx_questionListEdit.ItemsSource = questionBlockList;
                cbx_questionListEdit.SelectedItem = selectedQuestionBlock;
            }
        }

        private void bt_answerEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectedAnswer = lbx_answer.SelectedItem as AnswerOption;
            if (selectedAnswer!=null && tbx_answer.Text!="")
            {
                selectedAnswer.Text = tbx_answer.Text;
                tbx_answer.Text = "";
            }
        }

        private void CheckBox_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var selectedQuestion = lbx_question.SelectedItem as Question;

            if (cbx_questionType.SelectedItem as string == "Singlechoice")
            {
                int rightAnswerCount = 0;
                int rightAnswerIndex = 0;
                foreach (AnswerOption answer in selectedQuestion.answerOptions)
                {
                    if (answer.Result == "true")
                    {
                        rightAnswerCount++;
                        rightAnswerIndex = selectedQuestion.answerOptions.IndexOf(answer);
                    }

                }
                if (rightAnswerCount > 1)
                    selectedQuestion.answerOptions[rightAnswerIndex].Result = "false";
            }
        }
    }
}   