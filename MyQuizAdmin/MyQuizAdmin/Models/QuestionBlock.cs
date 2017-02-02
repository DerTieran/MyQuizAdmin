using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizAdmin.Models
{
    public class QuestionBlock
    {
        public int id { get; set; }
        public string title { get; set; }

        public ObservableCollection<Question> questionList { get; set; }
    }
}
