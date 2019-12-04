using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
    }

    
}