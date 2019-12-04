using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml.Linq;
using System.IO;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region components def
        private DaysRepo _repo;

        #endregion

        public MainWindow()
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
                TaskEditWindow taskEditorWindow = TaskEditWindow.Inst;
                taskEditorWindow.ConfigureWindow(TaskEditorMode.Edit);
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
            TaskEditWindow taskEditorWindow = TaskEditWindow.Inst;
            taskEditorWindow.ConfigureWindow(TaskEditorMode.New);
            taskEditorWindow.Show();            
        }


        private void PhonesBook_Click(object sender, RoutedEventArgs e)
        {
            PhonesWindow phonesWindow = PhonesWindow.Inst;
            phonesWindow.Show();
        }
    }



}
