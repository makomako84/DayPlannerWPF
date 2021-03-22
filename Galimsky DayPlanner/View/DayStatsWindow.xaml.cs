using System;
using System.Windows;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for DayStatsWindow.xaml
    /// </summary>
    public partial class DayStatsWindow : Window
    {
        public DateTime Date{ get; set; }
        public string StatMessage { get; set; }
        public DayStatsWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
