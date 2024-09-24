using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageStaffDbApp.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MaxCount { get; set; }
        public List<Staff> Staffs { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [NotMapped]
        public Department PositionDepartment
        {
            get
            {
                return DataWorker.GetDepartmentById(DepartmentId);
            }
        }

        [NotMapped]
        public List<Staff> PositionStaffs
        {
            get
            {
                return DataWorker.GetAllStaffsByPositionId(Id);
            }
        }
    }
}
