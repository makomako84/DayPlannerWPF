using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            XmlReader.Instance.XmlLoad();
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            XmlReader.Instance.XMLSave();
        }
    }
}
