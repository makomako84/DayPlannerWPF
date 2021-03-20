using System.Windows;

namespace Galimsky_DayPlanner
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

        private void SaveData()
        {
            (Application.Current as App).SaveData();
        }

        private void menuItem_save_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void menuItem_exit_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
            Application.Current.Shutdown();
        }
    }
}
