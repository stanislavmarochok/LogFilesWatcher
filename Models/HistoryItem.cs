using System;
using System.ComponentModel;

namespace LogFilesWatcher.Models
{
    public class HistoryItem : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private DateTime _timestamp;
        private int _version;

        public int Id 
        { 
            get => _id; 
            set 
            { 
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public DateTime Timestamp
        {
            get => _timestamp;
            set
            {
                _timestamp = value;
                OnPropertyChanged(nameof(Timestamp));
            }
        }

        public int Version
        {
            get => _version;
            set
            {
                _version = value;
                OnPropertyChanged(nameof(Version));
            }
        }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
