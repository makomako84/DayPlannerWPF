using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Galimsky_DayPlanner
{
    public enum EditorMode { New, Edit }
    /// <summary>
    /// Interaction logic for TaskEditControl.xaml
    /// </summary>
    public partial class TaskEditControl : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged imp.
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        private EditorMode _taskEditorMode;

        private TaskData _selection;
        public TaskData Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;

            }
        }

        private EditableDate _editedDate;
        public EditableDate EditedDateProp
        {
            get { return _editedDate; }
            set
            {
                _editedDate = value;
                RaisePropertyChanged("EditedDateProp");
            }
        }

        public TaskEditControl()
        {
            InitializeComponent();
        }

        public void Configure(EditorMode taskEditorMode)
        {
            _taskEditorMode = taskEditorMode;

            InitializeComponent();
            DataContext = this;

            if (_taskEditorMode == EditorMode.Edit)
            {
                Selection = DaysRepo.Instance.DayTaskListSelection;
                EditedDateProp = new EditableDate(Selection.Time);
            }
            else if (_taskEditorMode == EditorMode.New)
            {
                Selection = TaskData.CreateTempTask(DaysRepo.Instance.SelectedDate);
                EditedDateProp = new EditableDate(Selection.Time);
            }
        }

        private void SaveTask()
        {
            if (_taskEditorMode == EditorMode.Edit)
            {
                TaskData foundTask = DaysRepo.Instance.Tasks.Where(x => x.Id == Selection.Id).ToList()[0];
                foundTask.Header = Selection.Header;
                foundTask.Text = Selection.Text;
                foundTask.Time = EditableDate.GetFullDateTime(Selection.Time, EditedDateProp);
            }
            else if (_taskEditorMode == EditorMode.New)
            {
                DaysRepo.Instance.Tasks.Add(TaskData.Create(Selection.Header, Selection.Text, EditableDate.GetFullDateTime(Selection.Time, EditedDateProp), Selection.IsDone));
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DatePicker.Visibility == Visibility.Hidden)
                DatePicker.Visibility = Visibility.Visible;
            else if (DatePicker.Visibility == Visibility.Visible)
                DatePicker.Visibility = Visibility.Hidden;
        }

        private void DatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Selection.Time = DatePicker.SelectedDate.GetValueOrDefault();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseParentWindow();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveTask();
            CloseParentWindow();
        }

        private void CloseParentWindow()
        {
            Window.GetWindow(this).Close();            
        }
    }
}
