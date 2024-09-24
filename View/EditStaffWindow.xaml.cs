using ManageStaffDbApp.Model;
using ManageStaffDbApp.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageStaffDbApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewStaffWindow.xaml
    /// </summary>
    public partial class EditStaffWindow : Window
    {
        public EditStaffWindow(Staff staffToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedStaff = staffToEdit;
            DataManageVM.StaffName = staffToEdit.Name;
            DataManageVM.StaffSurName = staffToEdit.SurName;
            DataManageVM.StaffPhone = staffToEdit.Phone;
        }
    }
}
