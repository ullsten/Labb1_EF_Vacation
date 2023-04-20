using Bogus;
using Labb1_EF.Data;
using Labb1_EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Labb1_EF.Controllers
{
    public class EmployeeInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public ICollection<LeaveApplicationList>? EmployeeViewModel { get; private set; }

        //public async Task<IActionResult> Index(int? id)
        //{
        //    var viewModel = await _context.Employees
        //        .Include(e => e.Department)
        //        .Include(e => e.LeaveApplications)
        //        .Join(_context.LeaveApplications, emp => emp.EmployeeId, la => la.FK_EmployeeId, (emp, la) => new { emp, la })
        //        .Join(_context.Departments, x => x.la.FK_EmployeeId, d => d.DepartmentId, (x, d) => new { x, d })
        //        .Join(_context.LeaveTypes, lt => lt.x.la.FK_LeaveTypeId, l => l.LeaveTypeId, (lt, l) => new { lt, l })
        //        .Join(_context.Addresses, a => a.lt.x.la.FK_EmployeeId, ad => ad.AddressId, (a, ad) => new EmployeeViewModel
        //        {
        //            DepartMentName = a.lt.d.DepartmentName,
        //            LeaveApplicationListId = a.lt.x.la.LeaveApplicationListId,
        //            StartDate = a.lt.x.la.StartDate,
        //            EndDate = a.lt.x.la.EndDate,
        //            CreatedAt = a.lt.x.la.CreatedAt,
        //            NumberOfDays = a.lt.x.la.NumberOfDays,
        //            EmployeeId = a.lt.x.emp.EmployeeId,
        //            FirstName = a.lt.x.emp.FirstName,
        //            LastName = a.lt.x.emp.LastName,
        //            Email = a.lt.x.emp.Email,
        //            PersonalNumber = a.lt.x.emp.PersonalNumber,
        //            Gender = a.lt.x.emp.Gender,
        //            DateOfHire = a.lt.x.emp.DateOfHire,
        //            Salary = a.lt.x.emp.Salary,
        //            LeaveTypeId = a.l.LeaveTypeId,
        //            LeaveTypeName = a.l.LeaveTypeName,
        //            AddressId = ad.AddressId,
        //            StreetAddress = ad.StreetAddress,
        //            PostalCode = ad.PostalCode,
        //            City = ad.City,
        //            Country = ad.Country,
        //        })
        //        .ToListAsync();

        //    if (id != null)
        //    {
        //        ViewData["EmployeeId"] = id.Value;
        //        Employee employee = _context.Employees
        //            .Include(e => e.LeaveApplications)
        //            .Where(e => e.EmployeeId == id.Value)
        //            .SingleOrDefault();

        //        if (employee.LeaveApplications == null || !employee.LeaveApplications.Any())
        //        {
        //            ViewData["Message"] = "No leave applications found for this employee";
        //        }
        //        else
        //        {
        //            viewModel = employee.LeaveApplications
        //                .Select(la => new EmployeeViewModel
        //                {
        //                    DepartMentName = la.Employees.Department.DepartmentName,
        //                    LeaveApplicationListId = la.LeaveApplicationListId,
        //                    StartDate = la.StartDate,
        //                    EndDate = la.EndDate,
        //                    CreatedAt = la.CreatedAt,
        //                    NumberOfDays = la.NumberOfDays,
        //                    EmployeeId = la.Employees.EmployeeId,
        //                    FirstName = la.Employees.FirstName,
        //                    LastName = la.Employees.LastName,
        //                    Email = la.Employees.Email,
        //                    PersonalNumber = la.Employees.PersonalNumber,
        //                    Gender = la.Employees.Gender,
        //                    DateOfHire = la.Employees.DateOfHire,
        //                    Salary = la.Employees.Salary,
        //                    LeaveTypeId = la.LeaveTypes.LeaveTypeId,
        //                    LeaveTypeName = la.LeaveTypes.LeaveTypeName,
        //                    //    AddressId = la.Employee.Address.AddressId,
        //                    //    StreetAddress = la.Employees.Address.StreetAddress,
        //                    //    PostalCode = la.Employee.Address.PostalCode,
        //                    //    City = la.Employee.Address.City,
        //                    //    Country = la.Employee.Address.Country,
        //                })
        //                .ToList();

        //        }
        //    }
        //    return View(viewModel);
        //}


        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = await _context.Employees
                .Include(e => e.LeaveApplications)
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
                .Distinct()
                .ToListAsync();
            if (id != null)
            {
                ViewData["EmployeeId"] = id.Value;
                Employee employee = _context.Employees.Where(
                    e => e.EmployeeId == id.Value).Single();
                //viewModel = employee.LeaveApplications;

                if (employee.LeaveApplications == null || employee.LeaveApplications.Any())
                {
                    ViewData["Message"] = "No leave applications found for this employee";
                }
            }

            return View(viewModel);
        }
    }
}
