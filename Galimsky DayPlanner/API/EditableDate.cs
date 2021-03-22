using System;
using System.ComponentModel;

namespace Galimsky_DayPlanner
{
    public class EditableDate : IDataErrorInfo
    {
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
            Hour = dt.Hour;
            Minute = dt.Minute;
        }

        public static DateTime GetFullDateTime(DateTime time, EditableDate ed)
        {
            return new DateTime(time.Year, time.Month, time.Day, ed.Hour, ed.Minute, 0);
        }

    }
}
