using System.Windows;
using System.Windows.Controls;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for PhonesControl.xaml
    /// </summary>
    public partial class PhonesControl : UserControl
    {
        public PhonesControl()
        {
            InitializeComponent();
            DataContext = PhonesRepo.Instance;
        }

        private void PhonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                PhonesRepo.Instance.SelectedPhone = e.AddedItems[0] as PhoneData;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            PhonesRepo.Instance.Phones.Add(PhoneData.Create());
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            PhonesRepo.Instance.ItemsView.Refresh();
        }
    }
}
