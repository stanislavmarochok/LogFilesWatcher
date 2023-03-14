using LogFilesWatcher.Controllers;
using System;
using System.Windows;
using Forms = System.Windows.Forms;

namespace LogFilesWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            using (Forms.FolderBrowserDialog dialog = new Forms.FolderBrowserDialog())
            {
                Forms.DialogResult result = dialog.ShowDialog();
                if (result != Forms.DialogResult.OK)
                {
                    return;
                }

                string selectedPath = dialog.SelectedPath;
                try
                {
                    ((HistoryItemsController)DataContext).SetSelectedPath(selectedPath);
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show("Invalid cast error occured when trying to update the selected path");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Some unhandled error occured when trying to update the selected path.\nError:\n{ex.Message}");
                }
            }
        }

        private void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            ((HistoryItemsController)DataContext).UpdateDirectoryContent();
        }

        private void ClearBoardButton_Click(object sender, RoutedEventArgs e)
        {
            ((HistoryItemsController)DataContext).ClearBoard();
        }

        private void ClearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ((HistoryItemsController)DataContext).ClearHistoryCaches();
        }
    }
}
