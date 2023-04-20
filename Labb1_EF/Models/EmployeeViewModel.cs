using System.ComponentModel;

namespace Labb1_EF.Models
{
    public class EmployeeViewModel
    {
        //from Employee
        [DisplayName("Employee Id")]
        public int EmployeeId { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
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

        
        //From department
        [DisplayName("Department")]
        public string DepartMentName { get; set; }

        //From Leave applicationlist
        [DisplayName("Application ID")]
        public int LeaveApplicationListId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }

        [DisplayName("Days off")]
        public int NumberOfDays { get; set; }

        //Leave type
        [DisplayName("Leave type Id")]
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }

        //Address
        public int AddressId { get; set; }
        [DisplayName("Street address")]
        public string StreetAddress { get; set; }
        [DisplayName("Postal code")]
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //public IEnumerable<Employee> Employees { get; set; }
        //public ICollection<LeaveApplicationList> LeaveApplications { get; set; }
        //public IEnumerable<PersonnelOffice> PersonnelOffice { get; set; }
    }
}
