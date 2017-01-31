using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuizAdmin.Views
{
   public class Question :INotifyPropertyChanged
    {
        private string nQuestion { get; set; }

        public int id { get; set; }
        public string question
        {
            set { nQuestion = value; OnPropertyChanged("question");}
            get { return nQuestion; }
        }

        public ObservableCollection<string> awnsers = new ObservableCollection<string>();

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
