using System.Windows;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for TaskEditWindow.xaml
    /// </summary>
    public partial class TaskEditWindow : Window
    {
        public TaskEditWindow()
        {
            InitializeComponent();
        }

        public void Configure(EditorMode mode)
        {
            taskEditControl.Configure(mode);
        }

        private void taskEditWindow_Closed(object sender, System.EventArgs e)
        {
            DaysRepo.Instance.ItemsView.Refresh();
        }
    }
}
