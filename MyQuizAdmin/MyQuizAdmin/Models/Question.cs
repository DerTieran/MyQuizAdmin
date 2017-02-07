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
        public string category { get; set; }//Abstimmung oder Quizfrage
        public string type { get; set; }//Singlechoie oder Multilechoice
        public string text
        {
            set { notifyText = value; OnPropertyChanged("text");}
            get { return notifyText; }
        }

        public ObservableCollection<AnswerOption> answerOptions { get; set; } = new ObservableCollection<AnswerOption>();

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
