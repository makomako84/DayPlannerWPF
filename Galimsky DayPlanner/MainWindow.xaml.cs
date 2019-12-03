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

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DaysRepo _dayDataRepo;
        private ObservableCollection<TaskData> _days;

        private ObservableCollection<TaskData> _tasks;
        public ObservableCollection<TaskData> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
            }
        }
        public ICollectionView ItemsView
        {
            get { return CollectionViewSource.GetDefaultView(Tasks); }
        }

        public bool Filter(DateTime dt)
        {
            return dt.Year == SelectedDate.Year && dt.Month == SelectedDate.Month && dt.Day == SelectedDate.Day;
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged("SelectedDate");
                ItemsView.Refresh();
            }
        }





        public MainWindow()
        {
            _days = new ObservableCollection<TaskData>();
            _dayDataRepo = DaysRepo.Instance;
            Test();
            InitializeComponent();
            DataContext = this;
            ItemsView.Filter = new Predicate<object>(t => Filter((t as TaskData).Time));
        }

        private void Test()
        {
            TaskData task1 = new TaskData("Make new videogame", new DateTime(2019, 11, 3, 12, 59, 0));
            task1.Text = $"To Hold some beer for my friend, and wait when Trotsky will shoot in his leg {Environment.NewLine}Thats what I call wild west!";

            TaskData task2 = new TaskData("Get out of REFERAT", new DateTime(2019, 12, 4, 9, 8, 0));

            TaskData task3 = new TaskData("Hold a beer for a Trotsky", new DateTime(2019, 12, 4, 15, 4, 3));
            task3.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            TaskData task4 = new TaskData("Some text", new DateTime(2019, 12, 5, 15, 25, 3));
            task4.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            Tasks = new ObservableCollection<TaskData>()
            {
                task1,
                task2,
                task3,
                task4,
            };
            SelectedDate = DateTime.Now;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDate = mainCalendar.SelectedDate.GetValueOrDefault();
        }

        #region INotifyPropertyChanged imp.
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion


    }



}
