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
    public sealed partial class Page1 : Page
    {
        StaticTestData staticTestData;
        Group group;
        People people;
        bool LayoutUpdateFlag = true;
        Request request = new Request();

        public Page1()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //staticTestData = new StaticTestData();
            //staticTestData.id = 0;
            //staticTestData.group.Add(new Group { id = 0, titel = "Angewandte Informatik" });
            //staticTestData.group[0].people.Add(new People { idPeop = 0, name = "Ruthe" });
            //staticTestData.group[0].people[0].questionPeople.Add(new QuestionPeople { id = 0, textPeop = "Wie war gut hat er Präsentiert" });
            //staticTestData.group[0].people[0].questionPeople[0].listAnswerId.Add(new ListAnswersID { answer = "sehr gut", amount = 12 });
            //staticTestData.group[0].people[0].questionPeople[0].listAnswerId.Add(new ListAnswersID { answer = "gut", amount = 2 });
            //staticTestData.group[0].people[0].questionPeople[0].listAnswerId.Add(new ListAnswersID { answer = "schlecht", amount = 6 });
            //staticTestData.group[0].people[0].questionPeople[0].listAnswerId.Add(new ListAnswersID { answer = "sehr schlecht", amount = 3 });
            //staticTestData.group[0].people[0].questionPeople.Add(new QuestionPeople { id = 1, textPeop = "Wie hat er sich Präsentiert" });
            //staticTestData.group[0].people[0].questionPeople[1].listAnswerId.Add(new ListAnswersID { answer = "sehr gut", amount = 3 });
            //staticTestData.group[0].people[0].questionPeople[1].listAnswerId.Add(new ListAnswersID { answer = "gut", amount = 2 });
            //staticTestData.group[0].people[0].questionPeople[1].listAnswerId.Add(new ListAnswersID { answer = "schlecht", amount = 15 });
            //staticTestData.group[0].people[0].questionPeople[1].listAnswerId.Add(new ListAnswersID { answer = "sehr schlecht", amount = 15 });
            //staticTestData.group[0].people.Add(new People { idPeop = 1, name = "Johnny" });
            //staticTestData.group[0].question.Add(new Question { id = 0, text = "Wie war die Vorlesung" });
            //staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "sehr gut", amount = 4 });
            //staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "gut", amount = 5 });
            //staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "schlecht", amount = 2 });
            //staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "sehr schlecht", amount = 1 });
            //staticTestData.group[0].question.Add(new Question { id = 1, text = "Fandet ihr die Präsentation gut" });
            //staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "sehr gut", amount = 2 });
            //staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "gut", amount = 1 });
            //staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "schlecht", amount = 8 });
            //staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "sehr schlecht", amount = 11 });

            List<GroupResponse> groupResponList = await request.GetGroups();             
            cbx_groups.ItemsSource = groupResponList;


            
        }

        private async void cbx_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedGroup = cbx_groups.SelectedItem as GroupResponse;
            //lbx_people.ItemsSource = group.people;
            List<Topic> topicResponList = await request.getTopicsForGroup(SelectedGroup);
            lbx_people.ItemsSource = topicResponList;
        }

        private async void txb_searchpeople_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var item = await request.getTopicsForGroup(cbx_groups.SelectedItem as GroupResponse);
            if (lbx_people.ItemsSource != null)
            {
                this.lbx_people.ItemsSource = item.Where(w => w.Name.ToUpper().Contains(txb_searchpeople.Text.ToUpper()) || w.Name.ToUpper().Contains(txb_searchpeople.Text.ToUpper()));
                LayoutUpdateFlag = true;
            }
        }

        private async void lbx_people_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var PeSelect = lbx_people.SelectedItem as People;
            lv_static.Visibility = Windows.UI.Xaml.Visibility.Visible;
            lv_static.ItemsSource = await request.getResultForTopicInGroup(new Topic(), new Group());



        }
    }
}
