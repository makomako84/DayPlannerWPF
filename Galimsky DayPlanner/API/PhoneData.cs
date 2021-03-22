using System.ComponentModel;
using System.Xml.Linq;

namespace Galimsky_DayPlanner
{
    public class PhoneData : INotifyPropertyChanged
    {
        private static int _idCounter = -1;
        public int ID { private set; get; }

        private string _countryCode;
        public string CountryCode
        {
            get { return _countryCode; }
            set
            {
                _countryCode = value;
                RaisePropertyChanged("CountryCode");
            }
        }

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
        public static PhoneData Create(string number, string name, string countryCode)
        {
            return new PhoneData() { ID = GetNewId(), Number = number, Name = name, CountryCode = countryCode };
        }
        public static PhoneData Create()
        {
            return new PhoneData();
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

        #region XElement methods
        public static PhoneData GetFromXml(XElement elem)
        {
            string phone = elem.Element("number").Value;
            string name = elem.Element("name").Value;
            string countryCode = elem.Element("code").Value;
            return PhoneData.Create(phone, name, countryCode);
        }

        public XElement ToXml()
        {
            XElement root = new XElement("phone");
            root.Add(new XElement("number", Number));
            root.Add(new XElement("name", Name));
            root.Add(new XElement("code", CountryCode));
            return root;
        }
        #endregion
    }
}
