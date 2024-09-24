using ManageStaffDbApp.Model;
using ManageStaffDbApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManageStaffDbApp.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        //all departments
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        }

        //all positions
        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get
            {
                return allPositions;
            }
            private set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }
        //all staffs
        private List<Staff> allStaffs = DataWorker.GetAllStaffs();
        public List<Staff> AllStaffs
        {
            get
            {
                return allStaffs;
            }
            private set
            {
                allStaffs = value;
                NotifyPropertyChanged("AllStaffs");
            }
        }

        //properties for department
        public static string DepartmentName { get; set; }
        //properties for position
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxCount { get; set; }
        public static Department PositionDepartment { get; set; }

        //properties for staff
        public static string StaffName { get; set; }
        public static string StaffSurName { get; set; }
        public static string StaffPhone { get; set; }
        public static Position StaffPosition { get; set; }

        //selection properties 
        public TabItem SelectedTabItem { get; set; }
        public static Staff SelectedStaff { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }


        #region COMMANDS TO ADD
        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (PositionSalary == 0) 
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    if (PositionMaxCount == 0)
                    {
                        SetRedBlockControll(wnd, "MaxNumberBlock");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите отдел");
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxCount, PositionDepartment);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand addNewStaff;
        public RelayCommand AddNewStaff
        {
            get
            {
                return addNewStaff ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (StaffName == null || StaffName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (StaffSurName == null || StaffSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "SurNameBlock");
                    }
                    if (StaffPhone == null || StaffPhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PhoneBlock");
                    }
                    if (StaffPosition == null)
                    {
                        MessageBox.Show("Укажите должность");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateStaff(StaffName, StaffSurName, StaffPhone, StaffPosition);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        #endregion

        #region EDIT COMMANDS
        private RelayCommand editStaff;
        public RelayCommand EditStaff
        {
            get
            {
                return editStaff ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noPositionStr = "Не выбрана новая должность";
                    if (SelectedStaff != null)
                    {
                        if (StaffPosition != null)
                        {
                            resultStr = DataWorker.EditStaff(SelectedStaff, StaffName, StaffSurName, StaffPhone, StaffPosition);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPositionStr);
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        private RelayCommand editPosition;
        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана должность";
                    string noDepartmentStr = "Не выбран новый отдел";
                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionMaxCount, PositionSalary, PositionDepartment);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noDepartmentStr);
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";
                    if (SelectedDepartment != null)
                    {
                        resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        #endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand openAddNewDepartmentWnd;
        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openAddNewPositionWnd;
        public RelayCommand OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewStaffWnd;
        public RelayCommand OpenAddNewStaffWnd
        {
            get
            {
                return openAddNewStaffWnd ?? new RelayCommand(obj =>
                {
                    OpenAddStaffWindowMethod();
                }
                );
            }
        }
        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //staff
                    if (SelectedTabItem.Name == "StaffsTab" && SelectedStaff != null)
                    {
                        OpenEditStaffWindowMethod(SelectedStaff);
                    }
                    //position
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMethod(SelectedPosition);
                    }
                    //department
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                }
                    );
            }
        }
        #endregion

        #region METHODS TO OPEN WINDOW
        //open windows methods
        private void OpenAddDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }
        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newPositionWindow);
        }
        private void OpenAddStaffWindowMethod()
        {
            AddNewStaffWindow newStaffWindow = new AddNewStaffWindow();
            SetCenterPositionAndOpen(newStaffWindow);
        }
        //edit windows
        private void OpenEditDepartmentWindowMethod(Department department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }
        private void OpenEditPositionWindowMethod(Position position)
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow(position);
            SetCenterPositionAndOpen(editPositionWindow);
        }
        private void OpenEditStaffWindowMethod(Staff staff)
        {
            EditStaffWindow editStaffWindow = new EditStaffWindow(staff);
            SetCenterPositionAndOpen(editStaffWindow);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //for staff
                    if (SelectedTabItem.Name == "StaffsTab" && SelectedStaff != null)
                    {
                        resultStr = DataWorker.DeleteStaff(SelectedStaff);
                        UpdateAllDataView();
                    }
                    //for position
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }
                    //for department
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }
                    //refresh
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                    );
            }
        }

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region UPDATE VIEWS
        private void SetNullValuesToProperties()
        {
            //for staff
            StaffName = null;
            StaffSurName = null;
            StaffPhone = null;
            StaffPosition = null;
            //for position
            PositionName = null;
            PositionSalary = 0;
            PositionMaxCount = 0;
            PositionDepartment = null;
            //for department
            DepartmentName = null;
        }
        private void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllStaffsView();
        }

        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }
        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }
        private void UpdateAllStaffsView()
        {
            AllStaffs = DataWorker.GetAllStaffs();
            MainWindow.AllStaffsView.ItemsSource = null;
            MainWindow.AllStaffsView.Items.Clear();
            MainWindow.AllStaffsView.ItemsSource = AllStaffs;
            MainWindow.AllStaffsView.Items.Refresh();
        }
        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
