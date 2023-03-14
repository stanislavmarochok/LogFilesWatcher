using System.Windows;

namespace LogFilesWatcher.Helpers
{
    internal class UIHelper
    {
        public static void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }
    }
}
