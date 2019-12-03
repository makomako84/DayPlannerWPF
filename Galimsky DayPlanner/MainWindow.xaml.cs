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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private ObservableCollection<TaskData> _tasks;
        public ObservableCollection<TaskData> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
            }
        }
        #region ItemsView imp.
        public ICollectionView ItemsView
        {
            get
            {
                return CollectionViewSource.GetDefaultView(Tasks);
            }
        }

        public bool Filter(DateTime dt)
        {
            return CompareDate(dt, SelectedDate);
        }

        private void ItemsViewInit()
        {
            ItemsView.Filter = new Predicate<object>(t => Filter((t as TaskData).Time));
            ItemsView.SortDescriptions.Add(new SortDescription("Time", ListSortDirection.Ascending));
        }
        #endregion

        private bool CompareDate(DateTime dt1, DateTime dt2)
        {
            return dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day;
        }

        public MainWindow()
        {
            XmlLoad();
            
            InitializeComponent();
            DataContext = this;
            SelectedDate = DateTime.Now;
            ItemsViewInit();
            //XMLSave();
            
        }

        #region XML read imp
        XElement _root;
        public void XmlLoad()
        {
            _root = XElement.Load(@"data.xml");
            Tasks = new ObservableCollection<TaskData>(GetData());

        }
        public void XMLSave()
        {
            _root = OutData();
            using (StringWriter sw = new StringWriter())
            {
                _root.Save("data.xml");
            }
        }
        public XElement OutData()
        {
            XElement root = new XElement("data");
            foreach(TaskData elem in Tasks)
            {
                root.Add(elem.ToXml());
            }
            return root;
        }
        public List<TaskData> GetData()
        {
            List<TaskData> res = new List<TaskData>();
            foreach (XElement item in _root.Elements())
            {
                res.Add(TaskData.GetFromXml(item));
            }
            return res;
        }
        #endregion

        private void Test()
        {
            TaskData task1 = new TaskData("Make new videogame", new DateTime(2019, 12, 3, 12, 51, 0));
            task1.Text = $"To Hold some beer for my friend, and wait when Trotsky will shoot in his leg {Environment.NewLine}Thats what I call wild west!";

            TaskData task2 = new TaskData("Get out of REFERAT", new DateTime(2019, 12, 4, 9, 8, 0));

            TaskData task3 = new TaskData("Hold a beer for a Trotsky", new DateTime(2019, 12, 4, 3, 4, 3));
            task3.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            TaskData task4 = new TaskData("Some text", new DateTime(2019, 12, 4, 5, 25, 3));
            task4.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            TaskData task5 = new TaskData("Aaaadadada new videogame", new DateTime(2019, 12, 3, 4, 59, 0));
            TaskData task6 = new TaskData("bdsfgbdsfbaadadada new videogame", new DateTime(2019, 12, 3, 21, 59, 0));

            TaskData task7 = new TaskData("adsfasdfasdgsdagh sdfhd sfh new videogame", new DateTime(2019, 12, 5, 4, 5, 0));
            TaskData task8 = new TaskData("32523dfgjh123124 new videogame", new DateTime(2019, 12, 5, 17, 51, 0));
            task8.Text = "fgsdifgdshgkjdfshgsadf87wertywej sdfgjo sdtgu098wea rt";
            TaskData task9 = new TaskData("gdfsgsd23532456gdf new videogame", new DateTime(2019, 12, 5, 12, 23, 0));
            TaskData task10 = new TaskData("1243fasdfa new videogame", new DateTime(2019, 12, 5, 5, 12, 0));

            Tasks = new ObservableCollection<TaskData>()
            {
                task1,
                task2,
                task3,
                task4,
                task5,
                task6,
                task7,
                task8,
                task9,
                task10
            };
            
        }


        #region selected date imp.
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
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDate = mainCalendar.SelectedDate.GetValueOrDefault();
        }
        #endregion

        #region INotifyPropertyChanged imp.
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        private void DayTasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;
            MessageBox.Show("sdgfsdgs");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XMLSave();
        }
    }



}
