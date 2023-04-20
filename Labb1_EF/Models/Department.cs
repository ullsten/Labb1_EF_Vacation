using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Labb1_EF.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Department")]
        public string DepartmentName { get; set; }

        //one to many relation Department and employee
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
