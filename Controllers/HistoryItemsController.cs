using LogFilesWatcher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace LogFilesWatcher.Controllers
{
    internal class HistoryItemsController : IHistoryItemsController, INotifyPropertyChanged
    {
        private string lastSelectedPath = string.Empty;

        private IList<HistoryItem> _HistoryItemsList;
        private List<FileInfo> filesInSelectedPath = new List<FileInfo>();

        public HistoryItemsController()
        {
            _HistoryItemsList = new List<HistoryItem>
            {
                new HistoryItem 
                {
                    Id = 0,
                    Version = 0,
                    Timestamp = DateTime.Now,
                    Title = "Initial"
                },
            };
        }

        public IList<HistoryItem> HistoryItems
        {
            get { return _HistoryItemsList; }
            set { _HistoryItemsList = value; }
        }

        public string SelectedPath
        {
            get { return lastSelectedPath; }
            set 
            { 
                lastSelectedPath = value;
                OnPropertyChanged(nameof(SelectedPath));
            }
        }

        private ICommand mUpdater;

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

        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        public void SelectedPathUpdated(string newSelectedPath)
        {
            if (lastSelectedPath != newSelectedPath)
            {
                // directory was changed
                // clean all the caches
                // update SelectedPath
                filesInSelectedPath.Clear();
                SelectedPath = newSelectedPath;
            }

            UpdateDirectoryContent();
        }

        public void UpdateDirectoryContent()
        {
            // todo: update content here

            // step 1: if there are no caches - create them
            // step 2: if caches are newly-created - set all the files to version 1


        }

        private class Updater : ICommand
        {
            #region ICommand Members  

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }

            #endregion
        }
    }
}
