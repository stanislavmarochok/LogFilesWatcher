using LogFilesWatcher.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace LogFilesWatcher.Controllers
{
    internal class HistoryItemsController
    {
        private IList<HistoryItem> _HistoryItemsList;

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

        private ICommand mUpdater;
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
