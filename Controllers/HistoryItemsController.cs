using LogFilesWatcher.Helpers;
using LogFilesWatcher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LogFilesWatcher.Controllers
{
    internal class HistoryItemsController : IHistoryItemsController, INotifyPropertyChanged
    {
        private string lastSelectedPath = string.Empty;

        private ObservableCollection<HistoryItem> _historyItemsList;
        private Dictionary<string, List<FileItem>> directoriesContentLastVersionsLists = new Dictionary<string, List<FileItem>>();

        public HistoryItemsController()
        {
            _historyItemsList = new ObservableCollection<HistoryItem>();
        }

        public ObservableCollection<HistoryItem> HistoryItems
        {
            get { return _historyItemsList; }
            set { _historyItemsList = value; }
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

        public void SetSelectedPath(string selectedPath)
        {
            SelectedPath = selectedPath;
        }

        public void UpdateDirectoryContent()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedPath) || !Directory.Exists(SelectedPath))
                {
                    throw new DirectoryNotFoundException(SelectedPath);
                }

                List<FileItem> modifiedFiles = new List<FileItem>();
                List<FileItem> addedFiles = new List<FileItem>();
                List<FileItem> removedFiles = new List<FileItem>();

                // check for modified files or added files
                GetLastFiles:
                    if (!directoriesContentLastVersionsLists.TryGetValue(SelectedPath, out List<FileItem>? filesLastVersionInSelectedPath))
                    {
                        directoriesContentLastVersionsLists.Add(SelectedPath, new List<FileItem>());
                        goto GetLastFiles;
                    }

                foreach (FileItem oldFile in filesLastVersionInSelectedPath)
                {
                    // if the old file still exists in the filesystem
                    if (File.Exists(oldFile.FileFullPath))
                    {
                        FileInfo newFileInfo = new FileInfo(oldFile.FileFullPath);
                        if (newFileInfo.LastWriteTimeUtc != oldFile.LastModifiedTime)
                        {
                            modifiedFiles.Add(new FileItem(oldFile.FileFullPath, newFileInfo.LastWriteTimeUtc));
                        }
                    }
                    else
                    {
                        removedFiles.Add(new FileItem(oldFile.FileFullPath, null));
                    }
                }

                string[] modifiedOrRemovedFileNames = modifiedFiles.Concat(addedFiles).DistinctBy(x => x.FileFullPath).Select(x => x.FileFullPath).ToArray();
                string[] allFilesInSelectedPath = Directory.GetFiles(SelectedPath);
                addedFiles = allFilesInSelectedPath
                    .Where(file => !modifiedOrRemovedFileNames.Contains(file))
                    .Where(file => !filesLastVersionInSelectedPath.Select(x => x.FileFullPath).Contains(file))
                    .Select(file => new FileItem(file, new FileInfo(file).LastWriteTimeUtc)).ToList();

                foreach (FileItem addedFile in addedFiles)
                    filesLastVersionInSelectedPath.Add(addedFile);

                List<List<HistoryItem>> allNewHistoryItemsLists = new List<List<HistoryItem>>
                {
                    AddItemsToHistoryItems(addedFiles, "Added"),
                    AddItemsToHistoryItems(removedFiles, "Removed"),
                    AddItemsToHistoryItems(modifiedFiles, "Modified")
                };
                List<HistoryItem> allNewHistoryItems = allNewHistoryItemsLists.SelectMany(list => list).ToList();
                if (allNewHistoryItems.Any())
                {
                    foreach (var item in allNewHistoryItems)
                    {
                        HistoryItems.Add(item);
                    }
                } else
                {
                    HistoryItems.Add(CreateNoChangesHistoryItem());
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                UIHelper.ShowError("Check your PATH value!");
            }
            catch (Exception ex) 
            {
                UIHelper.ShowError($"Error occured when trying to get files from directory.\nError:\n{ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        public void ClearBoard()
        {
            HistoryItems.Clear();
        }

        public void ClearHistoryCaches()
        {
            foreach(KeyValuePair<string, List<FileItem>> directoryLastVersion in directoriesContentLastVersionsLists) 
            {
                directoryLastVersion.Value.Clear();
            }
        }

        private HistoryItem CreateNoChangesHistoryItem()
        {
            return new HistoryItem()
            {
                Status = "~",
                FileName = "no changes since last check",
                LastModifiedTime = DateTime.Now,
                FileVersion = 0
            };
        }

        private List<HistoryItem> AddItemsToHistoryItems(List<FileItem> addedFiles, string status)
        {
            List<HistoryItem> addedHistoryItems = new List<HistoryItem>();
            foreach (FileItem fileItem in addedFiles)
            {
                HistoryItem newHistoryItem = new HistoryItem
                {
                    Status = status,
                    FileName = fileItem.FileFullPath,
                    FileVersion = 1,
                    LastModifiedTime = fileItem.LastModifiedTime ?? DateTime.MinValue, // TODO: make a NORMAL solution here
                };
                addedHistoryItems.Add(newHistoryItem);
            }

            return addedHistoryItems;
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

        private class FileItem
        {
            public string FileFullPath { get; set; }
            public DateTime? LastModifiedTime { get; set; }

            public FileItem(string fileName, DateTime? lastModified)
            {
                FileFullPath = fileName;
                LastModifiedTime = lastModified;
            }
        }
    }
}
