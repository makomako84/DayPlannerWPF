using System;
using System.Linq;
using System.Windows;
namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for PhoneEditWindow.xaml
    /// </summary>
    public partial class PhoneEditWindow : Window
    {
        EditorMode _editorMode;

        private PhoneData _selection;
        public PhoneData Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;

            }
        }

        public PhoneEditWindow()
        {
            InitializeComponent();
        }

        public void Configure(EditorMode editorMode)
        {
            _editorMode = editorMode;
            InitializeComponent();
            DataContext = this;

            if (_editorMode == EditorMode.Edit)
            {
                Selection = PhonesRepo.Instance.SelectedPhone;
            }
            else if (_editorMode == EditorMode.New)
            {
                Selection = PhoneData.Create();
            }
        }

        private void SaveTask()
        {
            if (_editorMode == EditorMode.Edit)
            {
                PhoneData foundPhone = PhonesRepo.Instance.Phones.Where(x => x.ID == Selection.ID).ToList()[0];
                foundPhone.Name = Selection.Name;
                foundPhone.Number = Selection.Number;
            }
            else if (_editorMode == EditorMode.New)
            {
                PhonesRepo.Instance.Phones.Add(PhoneData.Create(Selection.Number, Selection.Name));
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PhonesRepo.Instance.ItemsView.Refresh();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTask();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
