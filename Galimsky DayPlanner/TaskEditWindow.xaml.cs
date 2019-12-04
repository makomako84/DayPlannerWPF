using System;
using System.Collections.Generic;
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
    

    /// <summary>
    /// Interaction logic for TaskEditWindow.xaml
    /// </summary>
    public partial class TaskEditWindow : Window
    {

        public TaskEditWindow()
        {
            InitializeComponent();
            DataContext = DaysRepo.Instance;
            DaysRepo.Instance.EditedDateProp = new EditedDate(DaysRepo.Instance.DayTaskListSelection.Time);
        }

        private void SaveTask()
        {
            TaskData selection = DaysRepo.Instance.DayTaskListSelection;
            TaskData foundTask = DaysRepo.Instance.Tasks.Where(x => x.Id == selection.Id).ToList()[0];
            foundTask.Time = DaysRepo.Instance.EditedDateProp.GetDateTime();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveTask();
        }
    }
}
