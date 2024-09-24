using ManageStaffDbApp.Model;
using ManageStaffDbApp.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageStaffDbApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewPositionWindow.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position positionToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedPosition = positionToEdit;
            DataManageVM.PositionName = positionToEdit.Name;
            DataManageVM.PositionMaxCount = positionToEdit.MaxCount;
            DataManageVM.PositionSalary = positionToEdit.Salary;
        }
    }
}
