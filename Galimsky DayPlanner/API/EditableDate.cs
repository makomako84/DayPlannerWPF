using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galimsky_DayPlanner
{
    public class EditableDate : IDataErrorInfo
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        #region IDataErrorInfo
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Month":
                        if (Month < 1 || Month > 12)
                            error = "Month must be in range from 1 to 12";
                        break;
                    case "Year":
                        if (Year < 1900)
                            error = "Year must be more then 1900";
                        break;
     
                    case "Day":
                        if (Day < 1 || Day > 31)
                            error = "Day must be in range from 1 to 31";
                        break;
                    case "Hour":
                        if (Hour < 0 || Hour > 59)
                            error = "Hour must be in range from 0 to 59";
                        break;
                    case "Minute":
                        if (Minute < 0 || Minute > 59)
                            error = "Month must be in range from 0 to 59";
                        break;
                }
                return error;
            }
        }

        #endregion

        public EditableDate(DateTime dt)
        {
            Year = dt.Year;
            Month = dt.Month;
            Day = dt.Day;
            Hour = dt.Hour;
            Minute = dt.Minute;
        }

        public DateTime GetDateTime()
        {            
            return new DateTime(Year, Month, Day, Hour, Minute, 0);
        }

    }
}
