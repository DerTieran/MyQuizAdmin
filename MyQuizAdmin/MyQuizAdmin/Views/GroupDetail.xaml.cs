using MyQuizAdmin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyQuizAdmin.Views
{
    public sealed partial class GroupDetail : UserControl
    {
        public Group group
        {
            get {
                return (Group)GetValue(groupProperty);
            }
            set {
                SetValue(groupProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for group.  This enables animation, styling, binding, etc...
        private static DependencyProperty groupProperty =
            DependencyProperty.Register("group", typeof(Group), typeof(GroupDetail), null);
        
        public GroupDetail()
        {
            InitializeComponent();
        }

        private void memberList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            delMember.IsEnabled = memberList.SelectedItem != null;
        }

        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            var newTopic = new SingleTopic();
            group.SingleTopics.Add(newTopic);
            memberList.SelectedItem = newTopic;
        }

        private void delMember_Click(object sender, RoutedEventArgs e)
        {
            group.SingleTopics.RemoveAt(memberList.SelectedIndex);
            if( memberList.Items.Count > 0 )
            {
                memberList.SelectedItem = memberList.Items.Last();
            }
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var request = new Request();
            var updatedGroup = await request.updateOrCreateGroup(group);
        }
    }
}
