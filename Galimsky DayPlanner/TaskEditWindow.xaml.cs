﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Galimsky_DayPlanner
{
    
    public enum TaskEditorMode { New, Edit}
    /// <summary>
    /// Interaction logic for TaskEditWindow.xaml
    /// </summary>
    public partial class TaskEditWindow : Window, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged imp.
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        private TaskEditorMode _taskEditorMode;

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

        public TaskEditWindow(TaskEditorMode taskEditorMode)
        {
            _taskEditorMode = taskEditorMode;

            InitializeComponent();
            DataContext = this;

            if (_taskEditorMode == TaskEditorMode.Edit)
            {
                Selection = DaysRepo.Instance.DayTaskListSelection;
                EditedDateProp = new EditableDate(Selection.Time);
            }
            else if(_taskEditorMode == TaskEditorMode.New)
            {
                Selection = TaskData.CreateTempTask(DaysRepo.Instance.SelectedDate);
                EditedDateProp = new EditableDate(Selection.Time);
            }
        }

        private void SaveTask()
        {
            if (_taskEditorMode == TaskEditorMode.Edit)
            {
                TaskData foundTask = DaysRepo.Instance.Tasks.Where(x => x.Id == Selection.Id).ToList()[0];
                foundTask.Header = Selection.Header;
                foundTask.Text = Selection.Text;
                foundTask.Time = EditableDate.GetFullDateTime(Selection.Time,EditedDateProp);
            }
            else if(_taskEditorMode == TaskEditorMode.New)
            {
                DaysRepo.Instance.Tasks.Add(TaskData.Create(Selection.Header, Selection.Text, EditableDate.GetFullDateTime(Selection.Time, EditedDateProp)));
            }
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            DaysRepo.Instance.ItemsView.Refresh();
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
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveTask();
            this.Close();
        }
    }
}
