using LogFilesWatcher.Controllers;
using System.Windows;

namespace LogFilesWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            HistoryItemsController historyItemsController = new HistoryItemsController();
            window.DataContext = historyItemsController;
            window.Show();
        }
    }
}
