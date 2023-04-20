using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;

namespace Labb1_EF.Models
{
    public class LeaveApplicationList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int LeaveApplicationListId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DisplayName("Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [NotMapped] //saves in memory only
        [DisplayName("Days off")]
        public int NumberOfDays => (EndDate - StartDate).Days;

        [ForeignKey("Employees")]
        public int FK_EmployeeId { get; set; }
        
        public virtual Employee? Employees { get; set; }

        [ForeignKey("LeaveTypes")]
        public int? FK_LeaveTypeId { get; set; }

        public virtual LeaveType? LeaveTypes { get; set; }

    }
}



