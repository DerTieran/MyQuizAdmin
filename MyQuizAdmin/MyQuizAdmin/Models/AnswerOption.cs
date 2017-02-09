using System.ComponentModel;

namespace MyQuizAdmin.Models
{
    public class AnswerOption:INotifyPropertyChanged
    {
        public long Id { get; set; }
        private string notifyText { get; set; }
        public string Result { get; set; }
        //public bool IsCorrect { get; set; }

        public string Text
        {
            set { notifyText = value; OnPropertyChanged("Text"); }
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