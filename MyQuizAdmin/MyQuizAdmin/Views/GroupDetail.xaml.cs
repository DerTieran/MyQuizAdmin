using System;
using System.Collections.Generic;
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
        public Models.Group group
        {
            get {
                return (Models.Group)GetValue(groupProperty);
            }
            set {
                SetValue(groupProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for group.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty groupProperty =
            DependencyProperty.Register("group", typeof(Models.Group), typeof(GroupDetail), null);


        public GroupDetail()
        {
            this.InitializeComponent();
        }
    }
}
