using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizapp
{
   public class Question :INotifyPropertyChanged
    {
        private string nQuestion { get; set; }

        public string question
        {
            set { nQuestion = value; OnPropertyChanged("question");}
            get { return nQuestion; }
        }

        public ObservableCollection<string> awnsers { get; set; }

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
