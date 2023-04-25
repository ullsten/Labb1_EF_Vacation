using Microsoft.AspNetCore.Identity;

namespace Labb1_EF.Models.ViewModels
{
    public class AdminViewModel
    {
        public IdentityUser[]? Administrators { get; set; }
        public IdentityUser[]? Employees { get; set; }
    }
}
