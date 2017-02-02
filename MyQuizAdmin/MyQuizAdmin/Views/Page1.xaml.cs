using System;
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
        bool LayoutUpdateFlag = true;
        Request request = new Request();

        public Page1()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<GroupResponse> groupResponList = await request.GetGroups();             
            cbx_groups.ItemsSource = groupResponList;          
        }

        private async void cbx_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedGroup = cbx_groups.SelectedItem as GroupResponse;
            List<Topic> topicResponList = await request.getTopicsForGroup(SelectedGroup);
            lbx_people.ItemsSource = topicResponList;
            if (lv_static.ItemsSource != null )
            {
                lv_static.ItemsSource = null;
            }

            if (lv_static.ItemsSource == null)
            {
                lv_static.ItemsSource = await request.getResultForGroup(SelectedGroup);
            }
        }

        private async void txb_searchpeople_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var topicList = await request.getTopicsForGroup(cbx_groups.SelectedItem as GroupResponse);
            if (lbx_people.ItemsSource != null)
            {
                this.lbx_people.ItemsSource = topicList.Where(w => w.String.ToUpper().Contains(txb_searchpeople.Text.ToUpper()) || w.String.ToUpper().Contains(txb_searchpeople.Text.ToUpper()));
                LayoutUpdateFlag = true;
            }
        }

        private async void lbx_people_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedGroup = cbx_groups.SelectedItem as GroupResponse;
            var SelectedTopic = lbx_people.SelectedItem as Topic;
            lv_static.ItemsSource = await request.getResultForTopicInGroup(SelectedTopic, SelectedGroup);
        }
    }
}
