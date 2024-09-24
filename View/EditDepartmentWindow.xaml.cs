using ManageStaffDbApp.Model;
using ManageStaffDbApp.ViewModel;
using System.Windows;



namespace ManageStaffDbApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow(Department departmentToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedDepartment = departmentToEdit;
            DataManageVM.DepartmentName = departmentToEdit.Name;
        }
    }
}
