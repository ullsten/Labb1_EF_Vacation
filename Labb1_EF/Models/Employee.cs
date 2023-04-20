using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Labb1_EF.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Full Name")]
        public string FullName => FirstName + " " + LastName;

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfHire { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Personal Number")]
        public string PersonalNumber { get; set; }

        [Required]
        [StringLength(maximumLength: 6, MinimumLength = 1)]
        public string Gender { get; set; }

        public virtual ICollection<LeaveApplicationList>? LeaveApplications { get; set;}
        public ICollection<PersonnelOffice> personnelOffice { get; set; }
    }
}
