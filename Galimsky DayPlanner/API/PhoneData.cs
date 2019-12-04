using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Galimsky_DayPlanner
{
    public class PhoneData : INotifyPropertyChanged
    {
        private static int _idCounter = -1;
        public int ID { private set; get; }

        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                RaisePropertyChanged("Number");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private PhoneData()
        {
        }
        public PhoneData Create(string number, string name)
        {
            return new PhoneData() { ID = GetNewId(), Number = number, Name = name };
        }

        private static int GetNewId()
        {
            return _idCounter++;
        }


        #region INotifyPropertyChanged imp.
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
