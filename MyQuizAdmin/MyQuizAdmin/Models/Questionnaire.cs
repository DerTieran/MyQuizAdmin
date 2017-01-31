using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizAdmin.Views
{
    public class Questionnaire
    {
        public int id { get; set; }
        public string name { get; set; }

        public ObservableCollection<Question> questions = new ObservableCollection<Question>();
    }
}
