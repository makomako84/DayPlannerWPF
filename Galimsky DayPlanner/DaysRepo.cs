using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galimsky_DayPlanner
{
    public class DaysRepo 
    {
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


        public DaysRepo()
        {

        }
        //public ObservableCollection<TaskData> GetTasks()
        //{
        //    return _selectedDayTasks;
        //}

        //public override string ToString()
        //{
        //    return String.Join(",", _selectedDayTasks.Select(x => x.Header));
        //}
    }
}