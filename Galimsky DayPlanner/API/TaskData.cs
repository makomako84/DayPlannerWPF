using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Linq;

namespace Galimsky_DayPlanner
{
    public class TaskData : INotifyPropertyChanged
    {
        public int Id { get; private set; }
        private static int _idCounter = -1;

        private string _header;
        private string _text;
        private DateTime _time;

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
            SetId();
        }
        public TaskData(string header, string text, DateTime time)
        {
            Header = header;
            Text = text;
            Time = time;
            SetId();
        }

        private void SetId()
        {
            _idCounter++;
            Id = _idCounter;
        }

        public static TaskData GetFromXml(XElement elem)
        {
            DateTime dateTime = DateTime.Parse(elem.Attribute("DateTime").Value);
            string header = elem.Element("header").Value;
            string text = elem.Element("text").Value;
            return new TaskData(header, text, dateTime);
        }
        
        public XElement ToXml()
        {
            XElement root = new XElement("task");
            root.Add(new XAttribute("DateTime", String.Format("{0:s}",Time)));
            root.Add(new XElement("header", this.Header));
            root.Add(new XElement("text", this.Text));
            return root;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            if(DaysRepo.Instance.ItemsView!=null)
                DaysRepo.Instance.ItemsView.Refresh();
        }
        #endregion
    }
}
