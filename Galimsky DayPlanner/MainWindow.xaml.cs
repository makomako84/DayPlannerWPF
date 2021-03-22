using System.Windows;
using System.Linq;
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

        private void Window_Closed(object sender, System.EventArgs e)
        {
            SaveData();
        }

        private void MenuItem_DayStats_Click(object sender, RoutedEventArgs e)
        {
            DayStatsWindow dayStats = new DayStatsWindow();
            dayStats.Date = DaysRepo.Instance.SelectedDate;
            dayStats.StatMessage = DaysRepo.Instance.Tasks.Count(task => task.Time.Date == DaysRepo.Instance.SelectedDate && task.IsDone).ToString() 
                + " / " 
                + DaysRepo.Instance.Tasks.Count(task => task.Time.Date == DaysRepo.Instance.SelectedDate).ToString();
            dayStats.ShowDialog();
            
        }
    }
}
