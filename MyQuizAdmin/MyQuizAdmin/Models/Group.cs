using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyQuizAdmin.Models
{
    public class Group : INotifyPropertyChanged
    {
        public static ObservableCollection<Group> AllGroups { get; set; }
        
        public long Id { get; set; }

        private string _title { get; set; }
        public string Title {
            get { return _title; } set { _title = value; OnPropertyChanged("Title"); } }

        private string _enterGroupPin { get; set; }
        public string EnterGroupPin
        {
            get { return _enterGroupPin; }
            set { _enterGroupPin = value; OnPropertyChanged("EnterGroupPin"); }
        }
        
        public ObservableCollection<SingleTopic> SingleTopics { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if ( handler != null )
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Group()
        {
            SingleTopics = new ObservableCollection<SingleTopic>();
        }
    }
}