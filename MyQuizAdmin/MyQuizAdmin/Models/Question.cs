using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizAdmin.Models
{
   public class Question :INotifyPropertyChanged
    {
        private string notifyText { get; set; }

        public int id { get; set; }
        public string Category { get; set; }
        public string MultipleChoice { get; set; }
        public string text
        {
            set { notifyText = value; OnPropertyChanged("text");}
            get { return notifyText; }
        }

        public ObservableCollection<AnswerOption> answerOption { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler !=null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
