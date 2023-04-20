using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1_EF.Models
{
    public partial class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [StringLength(100)]
        public string? StreetAddress { get; set; }

        [Required]
        [StringLength(5)]
        [DisplayName("Postal Code")]
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        //one to many relation Address and Employee
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
