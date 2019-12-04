using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Linq;

namespace Galimsky_DayPlanner
{
    public class TaskData : BaseData, INotifyPropertyChanged
    {

        private string _header;
        private string _text;
        private DateTime _time;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged("Header");
            }
        }
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged("Time");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }
<<<<<<< HEAD

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                RaisePropertyChanged("IsDone");
            }
        }
        private TaskData()
=======
        private TaskData(DateTime time)
>>>>>>> parent of 406f4d9... phone init, xmlreader prepare for poly
        {
            
        }
        private TaskData(string header, DateTime time, int id)
        {
            Header = header;
            Time = time;
<<<<<<< HEAD
            ID = id;
            IsDone = isDone;
=======
            Id = id;
>>>>>>> parent of 406f4d9... phone init, xmlreader prepare for poly
        }
        private TaskData(string header, string text, DateTime time, int id)
        {
            Header = header;
            Text = text;
            Time = time;
<<<<<<< HEAD
            ID = id;
            IsDone = isDone;
=======
            Id = id;
>>>>>>> parent of 406f4d9... phone init, xmlreader prepare for poly
        }

        public static TaskData Create(string header, DateTime time)
        {
            return new TaskData(header, time, GetNewId());
        }
        public static TaskData Create(string header, string text, DateTime time)
        {
            return new TaskData(header, text, time, GetNewId());
        }
        public static TaskData Create()
        {
            return new TaskData() { ID = GetNewId() };
        }
        public static TaskData CreateTempTask(DateTime time)
        {
            return new TaskData() { Time = time };
        }

        public override BaseData InitFromXml(XElement elem)
        {
<<<<<<< HEAD
            Time = DateTime.Parse(elem.Attribute("DateTime").Value);
            Header = elem.Element("header").Value;
            Text = elem.Element("text").Value;
            IsDone = elem.Element("IsDone") != null ? true : false;
            return this;
=======
            DateTime dateTime = DateTime.Parse(elem.Attribute("DateTime").Value);
            string header = elem.Element("header").Value;
            string text = elem.Element("text").Value;
            return TaskData.Create(header, text, dateTime);
>>>>>>> parent of 406f4d9... phone init, xmlreader prepare for poly
        }

        //public static TaskData GetFromXml(XElement elem)
        //{
        //    DateTime dateTime = DateTime.Parse(elem.Attribute("DateTime").Value);
        //    string header = elem.Element("header").Value;
        //    string text = elem.Element("text").Value;
        //    bool isDone = elem.Element("IsDone") !=null ? true : false;
        //    return TaskData.Create(header, text, dateTime, isDone);
        //}
        
        public override XElement ToXml()
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
            //if(DaysRepo.Instance.ItemsView!=null)
            //    DaysRepo.Instance.ItemsView.Refresh();
        }
        #endregion
    }
}
