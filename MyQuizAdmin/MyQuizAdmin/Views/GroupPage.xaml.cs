using MyQuizAdmin.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MyQuizAdmin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupPage : Page
    {
        public GroupPage()
        {
            
            Groups = new List<Group>
            {
                new Group { id = 0, name = "g1" }
            };
            
            this.InitializeComponent();
        }

        public List<Group> Groups { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            /*
            var propertyDesc = e.Parameter; as PropertyDescriptor;

            if (propertyDesc != null)
            {
                DataContext = propertyDesc.Expando;
            }
            */
        }
    }
}
