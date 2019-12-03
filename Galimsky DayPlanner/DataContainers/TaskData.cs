using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Galimsky_DayPlanner
{
    public class TaskData : INotifyPropertyChanged
    {
        private string _header;
        private string _text;
        private DateTime _time;

        private int _hour;
        public int Hour
        {
            get { return _hour; }
            private set
            {
                _hour = value;
                RaisePropertyChanged("Hour");
            }
        }

        public string Header
        {
            get { return _header; }
            set
            {
                if (_header != value)
                {
                    _header = value;
                    RaisePropertyChanged("Header");
                }
            }
        }
        public DateTime Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged("Time");
                }
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    RaisePropertyChanged("Text");
                }
            }
        }
        public TaskData(string header, DateTime time)
        {
            Header = header;
            Time = time;
            Hour = Time.Hour;
        }
        public TaskData(string header, string text, DateTime time)
        {
            Header = header;
            Text = text;
            Time = time;
            Hour = Time.Hour;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
