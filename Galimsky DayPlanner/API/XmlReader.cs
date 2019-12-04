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
        BaseRepo<BaseData> _repo;

        public XmlReader(BaseRepo<BaseData> repo)
        {
            _repo = repo;
        }

        public void SetFile(XElement file, string rootName)
        {
            _root = file.Element(rootName);
            _repo.Items = new ObservableCollection<BaseData>(GetData());

        }
        private XElement OutData()
        {
            XElement root = new XElement("data");
            foreach (TaskData elem in _repo.Items)
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
                BaseData baseData = BaseData.C
                res.Add(BaseData.GetFromXml(item));
            }
            return res;
        }

    }
}
