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
                //EditedDateProp = new EditableDate(Selection.Time);
            }
            else if (_editorMode == EditorMode.New)
            {
                //Selection = TaskData.CreateTempTask(DaysRepo.Instance.SelectedDate);
                //EditedDateProp = new EditableDate(Selection.Time);
            }
        }
    }
}
