using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace MyQuizAdmin.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        StaticTestData staticTestData;
        Group group;
        Question question;
        bool LayoutUpdateFlag = true;

        public Page1()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            staticTestData = new StaticTestData();
            staticTestData.id = 0;
            staticTestData.group.Add(new Group { id = 0, titel = "Angewandte Informatik" });

            staticTestData.group[0].question.Add(new Question { id = 0, text = "Wie war die Vorlesung" });
            staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "sehr gut", amount = 4 });
            staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "gut", amount = 5 });
            staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "schlecht", amount = 2 });
            staticTestData.group[0].question[0].listAnswerId.Add(new ListAnswersID { answer = "sehr schlecht", amount = 1 });
            staticTestData.group[0].question.Add(new Question { id = 1, text = "Fandet ihr die Präsentation gut" });
            staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "sehr gut", amount = 2 });
            staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "gut", amount = 1 });
            staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "schlecht", amount = 8 });
            staticTestData.group[0].question[1].listAnswerId.Add(new ListAnswersID { answer = "sehr schlecht", amount = 11 });
            g_staticPag.DataContext = staticTestData.group[staticTestData.id];
            cbx_groups.ItemsSource = staticTestData.group;
        }

        private void lbx_question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var AnSelected = lbx_question.SelectedItem as Question;
            if (AnSelected.listAnswerId != null)
            {
                (ColumnChart.Series[0] as ColumnSeries).ItemsSource = AnSelected.listAnswerId;
            }
        }

        private void cbx_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbx_groups.SelectedItem as Group;
            group = item;
            lbx_question.ItemsSource = group.question;
        }

        private void txb_search_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.lbx_question.ItemsSource = group.question.Where(w => w.text.ToUpper().Contains(txb_search.Text.ToUpper()) || w.text.ToUpper().Contains(txb_search.Text.ToUpper()));
            LayoutUpdateFlag = true;
        }

 
    }
}
