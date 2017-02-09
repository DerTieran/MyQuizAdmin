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

        public long Id { get; set; }
        public string Category { get; set; }//Abstimmung oder Quizfrage
        public string Type { get; set; }//Singlechoie oder Multilechoice
        public ObservableCollection<AnswerOption> answerOptions { get; set; } = new ObservableCollection<AnswerOption>();

        public string Text
        {
            set { notifyText = value; OnPropertyChanged("Text");}
            get { return notifyText; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
