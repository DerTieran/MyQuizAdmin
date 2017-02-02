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
        public List<Group> Groups { get; private set; }

        public GroupPage()
        {
            Groups = new List<Group>
            {
                new Group { Id = 0, Title = "g0" },
                new Group { Id = 1, Title = "g1" },
                new Group { Id = 2, Title = "g2" },
                new Group { Id = 3, Title = "g3" },
                new Group { Id = 4, Title = "g4" }
            };
            
            this.InitializeComponent();
        }

    }
}
