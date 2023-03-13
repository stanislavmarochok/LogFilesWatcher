using LogFilesWatcher.UserControls;
using System.Collections.ObjectModel;
using System.Windows;

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
            this.DataContext = this;
            HistoryItems.Add(new HistoryItemControl());
        }


        private ObservableCollection<HistoryItemControl> historyItems;
        public ObservableCollection<HistoryItemControl> HistoryItems
        {
            get
            {
                if (historyItems == null)
                    historyItems = new ObservableCollection<HistoryItemControl>();
                return historyItems;
            }
        }
    }
}
