using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1_EF.Models
{
    public class EmployeeIndexData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayName("Full Name")]
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; }

        [DisplayName("Personal number")]
        public string PersonalNumber { get; set; }
        public string Gender { get; set; }
        [DisplayName("Hire date")]
        public DateTime DateOfHire { get; set; }
        public decimal Salary { get; set; }
        [DisplayName("ID")]
        public int LeaveApplicationListId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [DisplayName("Created")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Leave type")]
        public string LeaveType { get; set; }
        [DisplayName("Employee")]
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<LeaveApplicationList> LeaveApplications { get; set; }
        public IEnumerable<PersonnelOffice> PersonnelOffice { get; set; }

        //public IEnumerable<EmployeeViewModel> Employees { get; set; }
        //public IEnumerable<LeaveApplicationViewModel> LeaveApplications { get; set; }
    }
}

