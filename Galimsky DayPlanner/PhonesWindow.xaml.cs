using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for PhonesWindow.xaml
    /// </summary>
    public partial class PhonesWindow : Window
    {
        #region singleton def
        private static PhonesWindow _inst;
        public static PhonesWindow Inst
        {
            get
            {
                if (_inst == null)
                    _inst = new PhonesWindow();
                return _inst;
            }
        }
        #endregion

        private PhonesWindow()
        {
            InitializeComponent();
            DataContext = PhonesRepo.Instance;
        }
    }
}
