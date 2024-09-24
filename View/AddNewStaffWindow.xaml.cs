using System.Windows;
using ManageStaffDbApp.ViewModel;

namespace ManageStaffDbApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewStaffWindow.xaml
    /// </summary>
    public partial class AddNewStaffWindow : Window
    {
        public AddNewStaffWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
