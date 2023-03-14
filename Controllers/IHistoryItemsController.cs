namespace LogFilesWatcher.Controllers
{
    internal interface IHistoryItemsController
    {
        string SelectedPath { get; set; }
        void SetSelectedPath(string selectedPath);
        void UpdateDirectoryContent();
        void ClearBoard();
        void ClearHistoryCaches();
    }
}
