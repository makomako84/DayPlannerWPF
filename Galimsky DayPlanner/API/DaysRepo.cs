using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace Galimsky_DayPlanner
{
    public class DaysRepo : INotifyPropertyChanged
    {
        #region singleton def
        private static DaysRepo _instance;
        public static DaysRepo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DaysRepo();
                return _instance;
            }
        }
        #endregion

        #region INotifyPropertyChanged imp.
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        #region Data def.
        private ObservableCollection<TaskData> _tasks;
        public ObservableCollection<TaskData> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
            }
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

        private TaskData _dayTaskListSelection;
        public TaskData DayTaskListSelection
        {
            get { return _dayTaskListSelection; }
            set
            {
                _dayTaskListSelection = value;
                RaisePropertyChanged("DayTaskListSelection");
            }
        }


        #endregion

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
            return Tools.CompareDate(dt, SelectedDate);
        }

        public void ItemsViewInit()
        {
            ItemsView.Filter = new Predicate<object>(t => Filter((t as TaskData).Time));
            ItemsView.SortDescriptions.Add(new SortDescription("Time", ListSortDirection.Ascending));
        }
        #endregion

        #region constrtuctors
        public DaysRepo()
        {
            //Test();
        }
        #endregion



        private void Test()
        {
            TaskData task1 = TaskData.Create("Make new videogame", new DateTime(2019, 12, 3, 12, 51, 0), false);
            task1.Text = $"To Hold some beer for my friend, and wait when Trotsky will shoot in his leg {Environment.NewLine}Thats what I call wild west!";

            TaskData task2 = TaskData.Create("Get out of REFERAT", new DateTime(2019, 12, 4, 9, 8, 0), false);

            TaskData task3 = TaskData.Create("Hold a beer for a Trotsky", new DateTime(2019, 12, 4, 3, 4, 3), false);
            task3.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            TaskData task4 = TaskData.Create("Some text", new DateTime(2019, 12, 4, 5, 25, 3), false);
            task4.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            TaskData task5 = TaskData.Create("Aaaadadada new videogame", new DateTime(2019, 12, 3, 4, 59, 0), false);
            TaskData task6 = TaskData.Create("bdsfgbdsfbaadadada new videogame", new DateTime(2019, 12, 3, 21, 59, 0), false);

            TaskData task7 = TaskData.Create("adsfasdfasdgsdagh sdfhd sfh new videogame", new DateTime(2019, 12, 5, 4, 5, 0), false);
            TaskData task8 = TaskData.Create("32523dfgjh123124 new videogame", new DateTime(2019, 12, 5, 17, 51, 0), false);
            task8.Text = "fgsdifgdshgkjdfshgsadf87wertywej sdfgjo sdtgu098wea rt";
            TaskData task9 = TaskData.Create("gdfsgsd23532456gdf new videogame", new DateTime(2019, 12, 5, 12, 23, 0), false);
            TaskData task10 = TaskData.Create("1243fasdfa new videogame", new DateTime(2019, 12, 5, 5, 12, 0), false);

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

        XElement _root;

        public void InitFromXml()
        {
            App app = Application.Current as App;
            _root = app.Data.Element("tasks");
            Tasks = new ObservableCollection<TaskData>();
            foreach (XElement item in _root.Elements())
            {
                Tasks.Add(TaskData.GetFromXml(item));
            }
        }
        public XElement GetXMLData()
        {
            _root = new XElement("tasks");
            foreach (TaskData elem in Tasks)
            {
                _root.Add(elem.ToXml());
            }
            return _root;
        }
    }


}