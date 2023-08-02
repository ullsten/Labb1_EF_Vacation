using Bogus;
using Labb1_EF.Data;
using Labb1_EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Labb1_EF.Models.ViewModels;

namespace Labb1_EF.Controllers
{
    [Authorize(Roles = "Employee, Administrator")]
    public class EmployeeInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeeInfoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public async Task<ActionResult> Index1()
        //{
            

        //    return _context.Employees != null ?
        //        View(await _context.Employees.ToListAsync()) :
        //        Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
        //}

        public async Task<IActionResult> Index(int? id)
        {
            var emp = (await _userManager
               .GetUsersInRoleAsync("Employee"))
               .ToArray();

            var model = new AdminViewModel
            {
                Employees = emp,
            };
            var viewModel = await _context.Employees.Include(e => e.LeaveApplications)
                .Join(_context.LeaveApplications, emp => emp.EmployeeId, la => la.FK_EmployeeId, (emp, la) => new { emp, la })
                .Join(_context.Departments, x => x.la.FK_EmployeeId, d => d.DepartmentId, (x, d) => new { x, d })
                .Join(_context.LeaveTypes, lt => lt.x.la.FK_LeaveTypeId, l => l.LeaveTypeId, (lt, l) => new { lt, l })
                .Join(_context.Addresses, a => a.lt.x.la.FK_EmployeeId, ad => ad.AddressId, (a, ad) => new EmployeeViewModel
                {
                    DepartMentName = a.lt.d.DepartmentName,
                    LeaveApplicationListId = a.lt.x.la.LeaveApplicationListId,
                    StartDate = a.lt.x.la.StartDate,
                    EndDate = a.lt.x.la.EndDate,
                    CreatedAt = a.lt.x.la.CreatedAt,
                    NumberOfDays = a.lt.x.la.NumberOfDays,
                    EmployeeId = a.lt.x.emp.EmployeeId,
                    FirstName = a.lt.x.emp.FirstName,
                    LastName = a.lt.x.emp.LastName,
                    Email = a.lt.x.emp.Email,
                    PersonalNumber = a.lt.x.emp.PersonalNumber,
                    Gender = a.lt.x.emp.Gender,
                    DateOfHire = a.lt.x.emp.DateOfHire,
                    Salary = a.lt.x.emp.Salary,
                    LeaveTypeId = a.l.LeaveTypeId,
                    LeaveTypeName = a.l.LeaveTypeName,
                    AddressId = ad.AddressId,
                    StreetAddress = ad.StreetAddress,
                    PostalCode = ad.PostalCode,
                    City = ad.City,
                    Country = ad.Country,
                })
                .ToListAsync();

            if (id != null)
            {
                ViewData["EmployeeId"] = id.Value;
                Employee employee = _context.Employees.Where(
                    e => e.EmployeeId == id.Value).Single();

                if (employee.LeaveApplications == null || employee.LeaveApplications.Any())
                {
                    ViewData["Message"] = "No leave applications found for this employee";
                }
            }

            return View(viewModel);
        }

    }
}
