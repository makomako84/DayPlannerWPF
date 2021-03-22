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

        private void OpenPhoneEdit(EditorMode mode)
        {
            PhoneEditWindow phoneEditWindow = new PhoneEditWindow();
            phoneEditWindow.Owner = Application.Current.MainWindow;
            phoneEditWindow.Configure(mode);
            phoneEditWindow.ShowDialog();
        }

        private void PhonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                PhonesRepo.Instance.SelectedPhone = e.AddedItems[0] as PhoneData;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenPhoneEdit(EditorMode.New);
        }

        private void PhonesList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenPhoneEdit(EditorMode.Edit);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenPhoneEdit(EditorMode.Edit);
        }
    }
}
