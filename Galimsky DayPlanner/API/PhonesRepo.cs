using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Data;

namespace Galimsky_DayPlanner
{
    public class PhonesRepo : INotifyPropertyChanged
    {
        #region singleton def
        private static PhonesRepo _instance;
        public static PhonesRepo Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PhonesRepo();
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

        #region DataDef
        private ObservableCollection<PhoneData> _phones;
        public ObservableCollection<PhoneData> Phones
        {
            get { return _phones; }
            set
            {
                _phones = value;
                RaisePropertyChanged("Phones");
            }
        }

        private PhoneData _selectedPhone;
        public PhoneData SelectedPhone
        {
            get { return _selectedPhone; }
            set
            {
                _selectedPhone = value;
                RaisePropertyChanged("SelectedPhone");
            }
        }
        #endregion

        #region ctor
        public PhonesRepo()
        {

        }
        #endregion

        XElement _root;

        public void InitFromXml()
        {
            App app = Application.Current as App;
            _root = app.Data.Element("phones");
            Phones = new ObservableCollection<PhoneData>();
            foreach (XElement item in _root.Elements())
            {
                Phones.Add(PhoneData.GetFromXml(item));
            }
        }
        public XElement GetXMLData()
        {
            _root = new XElement("phones");
            foreach (PhoneData elem in Phones)
            {
                _root.Add(elem.ToXml());
            }
            return _root;
        }

        #region ItemsView imp.
        public ICollectionView ItemsView
        {
            get
            {
                return CollectionViewSource.GetDefaultView(Phones);
            }
        }

        public void ItemsViewInit()
        {
            ItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }
        #endregion


    }
}
