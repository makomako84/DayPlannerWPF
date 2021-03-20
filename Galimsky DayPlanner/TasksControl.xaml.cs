using System.Windows;
using System.Windows.Controls;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for TasksControl.xaml
    /// </summary>
    public partial class TasksControl : UserControl
    {

        #region components def
        private DaysRepo _repo;

        #endregion

        public TasksControl()
        {
            _repo = DaysRepo.Instance;
            InitializeComponent();
            DataContext = _repo;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            _repo.SelectedDate = mainCalendar.SelectedDate.GetValueOrDefault();
        }

        private void DayTasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count <= 0)
            //    _repo.DayTaskListSelection = null;
            if (e.AddedItems.Count > 0)
                _repo.DayTaskListSelection = e.AddedItems[0] as TaskData;
        }
        private void Tasks_EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (_repo.DayTaskListSelection != null)
            {
                OpenTaskEditWindow(TaskEditorMode.Edit);
            }
        }
        private void Tasks_AddItem_Click(object sender, RoutedEventArgs e)
        {
            OpenTaskEditWindow(TaskEditorMode.New);
        }

        private void DayTasksList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenTaskEditWindow(TaskEditorMode.Edit);
        }

        private void OpenTaskEditWindow(TaskEditorMode mode)
        {
            TaskEditWindow taskEditorWindow = new TaskEditWindow();
            taskEditorWindow.Owner = Application.Current.MainWindow;
            taskEditorWindow.Configure(mode);
            taskEditorWindow.ShowDialog();
        }
    }
}
