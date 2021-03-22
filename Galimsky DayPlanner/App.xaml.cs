using System;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public XElement Data { private set; get; }
        public App()
        {
            Data = XElement.Load("data.xml");
            DaysRepo.Instance.InitFromXml();
            DaysRepo.Instance.ItemsViewInit();
            DaysRepo.Instance.SelectedDate = DateTime.Now;

            PhonesRepo.Instance.InitFromXml();
            PhonesRepo.Instance.ItemsViewInit();
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveData();
        }

        public void SaveData()
        {
            XElement root = new XElement("data");
            root.Add(new XElement(DaysRepo.Instance.GetXMLData()));
            root.Add(new XElement(PhonesRepo.Instance.GetXMLData()));
            using (StringWriter sw = new StringWriter())
            {
                root.Save("data.xml");
            }
        }
    }
}
