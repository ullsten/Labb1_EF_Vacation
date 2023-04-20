using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1_EF.Models
{
    public class PersonnelOffice
    {
        //virtual = lazy loading = delay loading until needed
        [Key]
        public int PersonnelOfficeId { get; set; }

        [ForeignKey("Employees")]
        public int FK_EmployeeId { get; set; }
        public virtual Employee? Employees { get; set; }

        [ForeignKey("Addresses")]
        public int FK_AddressId { get; set; }
        public virtual Address? Addresses { get; set; }

        [ForeignKey("Departments")]
        public int FK_DepartmentId { get; set; }
        public virtual Department? Departments { get; set; }

        [ForeignKey("LeaveApplicationsList")]
        public int FK_LeaveApplicationListId { get; set; }
        public virtual LeaveApplicationList? LeaveApplications { get; set; }
    }
}