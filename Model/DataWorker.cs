using System.Collections.Generic;
using System.Linq;

namespace ManageStaffDbApp.Model
{
    public static class DataWorker
    {
        //create department
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                bool checkIsExist = db.Departments.Any(x => x.Name == name);
                if (!checkIsExist)
                {
                   Department newDepartment = new Department { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Добавлен!";
                }
                return result;
            }    
        }

        //remove department
        public static string DeleteDepartment(Department department)
        {
            string result = "Такого отдела не существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = $"Отдел {department.Name} удален";
            }
            return result;
        }


        //edit department
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Такого отдела не существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                db.SaveChanges();
                result = $"Отдел {department.Name} изменен";
            }
            return result;
        }

        //return all departments
        public static List<Department> GetAllDepartments()
        {
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        //create position 
        public static string CreatePosition(string name, decimal salary, int maxCount, Department department)
        {
            string result = "Уже существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                bool checkIsExist = db.Positions.Any(p => p.Name == name && p.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPosition = new Position 
                    { 
                        Name = name, 
                        Salary = salary,
                        MaxCount = maxCount,
                        DepartmentId = department.Id

                    };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Добавлен!";
                }                
            }
            return result;
        }

        //remove position 
        public static string DeletePosition(Position position)
        {
            string result = "Такой должности не существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = $"Должность {position.Name} удалена";
            }
            return result;
        }

        //edit position 
        public static string EditPosition(Position oldPosition, string newName, int newMaxCount, decimal newSalary, Department newDepartment)
        {
            string result = "Такой должности не существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxCount = newMaxCount;
                position.DepartmentId = newDepartment.Id;
                db.SaveChanges();
                result = $"Должность {position.Name} изменена";
            }
            return result;
        }

        //return all positions
        public static List<Position> GetAllPositions()
        {
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }

        //create employee
        public static string CreateStaff(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                bool checkIsExist = db.Staffs.Any(x => x.Name == name && x.SurName == surName && x.Position == position);
                if (!checkIsExist)
                {
                    Staff newStaff = new Staff
                    {
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Staffs.Add(newStaff);
                    db.SaveChanges();
                    result = "Добавлен!";
                }               
            }
            return result;
        }

        //remove employee
        public static string DeleteStaff(Staff staff)
        {
            string result = "Такого сотрудника не существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                db.Staffs.Remove(staff);
                db.SaveChanges();
                result = $"Сотрудник {staff.Name} {staff.SurName} уволен";
            }
            return result;
        }

        //edit employee
        public static string EditStaff(Staff oldStaff, string newName, string newSurName, string newPhone, Position newPosition)
        {
            string result = "Такого сотрудника не существует";
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                Staff staff = db.Staffs.FirstOrDefault(s => s.Id == oldStaff.Id);
                staff.Name = newName;
                staff.SurName = newSurName;
                staff.Phone = newPhone;
                staff.PositionId = newPosition.Id;
                db.SaveChanges();
                result = $"Сотрудник {staff.Name} {staff.SurName} изменен";
            }
            return result;
        }

        //return all employees
        public static List<Staff> GetAllStaffs()
        {
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                var result = db.Staffs.ToList();
                return result;
            }
        }

        //get position by Id
        public static Position GetPositionById(int id)
        {
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        //get department by Id
        public static Department GetDepartmentById(int id)
        {
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                Department dep = db.Departments.FirstOrDefault(d => d.Id == id);
                return dep;
            }
        }

        //get all employees by position Id
        public static List<Staff> GetAllStaffsByPositionId(int id)
        {
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                List<Staff> staffs = (from staff in GetAllStaffs() where staff.PositionId == id select staff).ToList();
                return staffs;
            }
        }
        //get all positions by department Id
        public static List<Position> GetAllPositionsByDepartmentId(int id)
        {
            using (Data.ApplicationContext db = new Data.ApplicationContext())
            {
                List<Position> positions = (from position in GetAllPositions() where position.DepartmentId == id select position).ToList();
                return positions;
            }
        }
    }
}
