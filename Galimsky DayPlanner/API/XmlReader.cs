using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Galimsky_DayPlanner
{
    public class XmlReader
    {
        #region singleton def
        private static XmlReader _instance;
        public static XmlReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new XmlReader();
                return _instance;
            }
        }
        #endregion

        XElement _root;
        DaysRepo _repo;

        public XmlReader()
        {
            _repo = DaysRepo.Instance;
        }

        public void XmlLoad(string datafileName)
        {
            _root = XElement.Load($"{datafileName}");
            _repo.Tasks = new ObservableCollection<TaskData>(GetData());

        }
        private XElement OutData()
        {
            XElement root = new XElement("data");
            foreach (TaskData elem in _repo.Tasks)
            {
                root.Add(elem.ToXml());
            }
            return root;
        }


        public void XMLSave()
        {
            _root = OutData();
            using (StringWriter sw = new StringWriter())
            {
                _root.Save("data.xml");
            }
        }
        private List<TaskData> GetData()
        {
            List<TaskData> res = new List<TaskData>();
            foreach (XElement item in _root.Elements())
            {
                res.Add(TaskData.GetFromXml(item));
            }
            return res;
        }

    }
}
