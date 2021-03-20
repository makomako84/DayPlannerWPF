using System.Windows;

namespace Galimsky_DayPlanner
{
    /// <summary>
    /// Interaction logic for TaskEditWindow.xaml
    /// </summary>
    public partial class TaskEditWindow : Window
    {
        public TaskEditWindow()
        {
            InitializeComponent();
        }

        public void Configure(TaskEditorMode mode)
        {
            taskEditControl.Configure(mode);
        }

    }
}
