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
    public class DaysRepo : BaseRepo<BaseData>, INotifyPropertyChanged
    {

        #region Data def.

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
                return CollectionViewSource.GetDefaultView(Items);
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
            TaskData task1 = TaskData.Create("Make new videogame", new DateTime(2019, 12, 3, 12, 51, 0));
            task1.Text = $"To Hold some beer for my friend, and wait when Trotsky will shoot in his leg {Environment.NewLine}Thats what I call wild west!";

            TaskData task2 = TaskData.Create("Get out of REFERAT", new DateTime(2019, 12, 4, 9, 8, 0));

            TaskData task3 = TaskData.Create("Hold a beer for a Trotsky", new DateTime(2019, 12, 4, 3, 4, 3));
            task3.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            TaskData task4 = TaskData.Create("Some text", new DateTime(2019, 12, 4, 5, 25, 3));
            task4.Text = "Yeeeeaeaeaeh, today wi well meat Stolen";

            TaskData task5 = TaskData.Create("Aaaadadada new videogame", new DateTime(2019, 12, 3, 4, 59, 0));
            TaskData task6 = TaskData.Create("bdsfgbdsfbaadadada new videogame", new DateTime(2019, 12, 3, 21, 59, 0));

            TaskData task7 = TaskData.Create("adsfasdfasdgsdagh sdfhd sfh new videogame", new DateTime(2019, 12, 5, 4, 5, 0));
            TaskData task8 = TaskData.Create("32523dfgjh123124 new videogame", new DateTime(2019, 12, 5, 17, 51, 0));
            task8.Text = "fgsdifgdshgkjdfshgsadf87wertywej sdfgjo sdtgu098wea rt";
            TaskData task9 = TaskData.Create("gdfsgsd23532456gdf new videogame", new DateTime(2019, 12, 5, 12, 23, 0));
            TaskData task10 = TaskData.Create("1243fasdfa new videogame", new DateTime(2019, 12, 5, 5, 12, 0));

            Items = new ObservableCollection<BaseData>()
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