using System.ComponentModel;

namespace Labb1_EF.Models
{
    public class LeaveType
    {
        public int LeaveTypeId { get; set; }

        [DisplayName("Leave type")]
        public string LeaveTypeName { get; set; }

        public virtual ICollection<LeaveApplicationList>? LeaveApplications { get; set; }
    }
}
