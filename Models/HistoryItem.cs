using System;
using System.ComponentModel;

namespace LogFilesWatcher.Models
{
    public class HistoryItem : INotifyPropertyChanged
    {
        private string _status;
        private string _title;
        private DateTime _timestamp;
        private int _version;

        public string Status 
        { 
            get => _status; 
            set 
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        
        public string FileName
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public DateTime LastModifiedTime
        {
            get => _timestamp;
            set
            {
                _timestamp = value;
                OnPropertyChanged(nameof(LastModifiedTime));
                OnPropertyChanged(nameof(LastModifiedTimeFormatted));
            }
        }

        public string LastModifiedTimeFormatted
        {
            get => LastModifiedTime.ToString("yyyy-mm-dd HH:mm:ss");
        }

        public int FileVersion
        {
            get => _version;
            set
            {
                _version = value;
                OnPropertyChanged(nameof(FileVersion));
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
