﻿using MyQuizAdmin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MyQuizAdmin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupPage : Page
    {
        public ObservableCollection<Group> Groups { get; set; }

        public GroupPage()
        {
            this.InitializeComponent();

            Groups = new ObservableCollection<Group> { };

            this.getData();
        }

        public async void getData()
        {
            var request = new Request();
            var freshGroups = await request.GetGroups();
            Groups.Clear();

            foreach (var item in freshGroups)
            {
                var stReq = new Request();
                var freshList = await request.getTopicsForGroup(item);
                item.SingleTopics.Clear();

                foreach (var stItem in freshList)
                {
                    item.SingleTopics.Add(stItem);
                }

                Groups.Add(item);
            }
        }
        
        private void addGroup_Click(object sender, RoutedEventArgs e)
        {
            var newGroup = new Group();
            Groups.Add(newGroup);
            masterDetailsView.SelectedItem = newGroup;
        }

        private async void delGroup_Click(object sender, RoutedEventArgs e)
        {
            var selectedGroup = (Group) masterDetailsView.SelectedItem;
            Groups.Remove( (Group) masterDetailsView.SelectedItem );
            var request = new Request();
            var success = await request.deleteGroup(selectedGroup);

            if ( success == HttpStatusCode.Accepted && Groups.Count > 0 )
            {
                masterDetailsView.SelectedItem = Groups[Groups.Count - 1];
            }
        }
    }
}
