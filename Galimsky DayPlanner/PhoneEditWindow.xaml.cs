using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        
        private PhoneEditWindowViewModel _viewModel;

        public PhoneEditWindow()
        {
            InitializeComponent();
        }

        public void Configure(EditorMode editorMode)
        {
            _editorMode = editorMode;
            InitializeComponent();
            _viewModel = new PhoneEditWindowViewModel();
            this.DataContext = _viewModel;

            if (_editorMode == EditorMode.Edit)
            {
                Selection = PhonesRepo.Instance.SelectedPhone;
            }
            else if (_editorMode == EditorMode.New)
            {
                Selection = PhoneData.Create();
            }
            _viewModel.PhoneNumber = Selection.Number;
            _viewModel.Name = Selection.Name;
            _viewModel.CountryCode = Selection.CountryCode;
        }

        private void SaveTask()
        {
            if (_editorMode == EditorMode.Edit)
            {
                PhoneData foundPhone = PhonesRepo.Instance.Phones.Where(x => x.ID == Selection.ID).ToList()[0];
                foundPhone.Name = _viewModel.Name;
                foundPhone.Number = _viewModel.PhoneNumber;
                foundPhone.CountryCode = _viewModel.CountryCode;
            }
            else if (_editorMode == EditorMode.New)
            {
                PhonesRepo.Instance.Phones.Add(PhoneData.Create(_viewModel.PhoneNumber, _viewModel.Name, _viewModel.CountryCode));
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

    public class PhoneEditWindowViewModel: DependencyObject
    {
        public string PhoneNumber
        {
            get { return (string)GetValue(PhoneNumberProperty); }
            set { SetValue(PhoneNumberProperty, value); }
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string CountryCode
        {
            get { return (string)GetValue(CountryCodeProperty); }
            set { SetValue(CountryCodeProperty, value); }
        }

        public static DependencyProperty PhoneNumberProperty =
            DependencyProperty.Register("PhoneNumber", typeof(string), typeof(PhoneEditWindowViewModel));

        public static DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(PhoneEditWindowViewModel));

        public static DependencyProperty CountryCodeProperty =
            DependencyProperty.Register("CountryCode", typeof(string), typeof(PhoneEditWindowViewModel));
    }

    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult;
            string val = (string)value;

            //if field is null / empty or contains some special characters, fail validation            
            if (!string.IsNullOrEmpty(val) &&
                System.Text.RegularExpressions.Regex.IsMatch(val, @"^[2-9]\d{2}-\d{3}-\d{4}$"))
                validationResult = new ValidationResult(true, null);
            else                
                validationResult = new ValidationResult(false, "The value contains invalid characters");

            return validationResult;
        }
    }

    public class CountryCodeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult;
            string val = (string)value;

            //if field is null / empty or contains some special characters, fail validation            
            if (!string.IsNullOrEmpty(val) &&
                System.Text.RegularExpressions.Regex.IsMatch(val, @"^(\+?\d{1,3}|\d{1,4})$"))
                validationResult = new ValidationResult(true, null);
            else
                validationResult = new ValidationResult(false, "The value contains invalid characters");

            return validationResult;
        }
    }
}
