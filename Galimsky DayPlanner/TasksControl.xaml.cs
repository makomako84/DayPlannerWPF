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
                TaskEditWindow taskEditorWindow = new TaskEditWindow();
                taskEditorWindow.Configure(TaskEditorMode.Edit);
                taskEditorWindow.Show();
            }
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;
            app.SaveData();
        }

        private void Tasks_AddItem_Click(object sender, RoutedEventArgs e)
        {
            TaskEditWindow taskEditorWindow = new TaskEditWindow();
            taskEditorWindow.Configure(TaskEditorMode.New);
            taskEditorWindow.Show();
        }
    }
}
