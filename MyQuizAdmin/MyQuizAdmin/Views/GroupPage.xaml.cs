using MyQuizAdmin.Models;
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
                new Group { id = 0, title = "g0" },
                new Group { id = 1, title = "g1" },
                new Group { id = 2, title = "g2" },
                new Group { id = 3, title = "g3" },
                new Group { id = 4, title = "g4" }
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
