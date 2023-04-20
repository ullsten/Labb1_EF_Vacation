using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb1_EF.Data;
using Labb1_EF.Models;
using Microsoft.Data.SqlClient;

namespace Labb1_EF.Controllers
{
    public class PersonnelOfficesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonnelOfficesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonnelOffices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PersonnelOffices
                .Include(p => p.Addresses)
                .Include(p => p.Departments)
                .Include(p => p.Employees)
                .Include(p => p.LeaveApplications);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PersonnelOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonnelOffices == null)
            {
                return NotFound();
            }

            var personnelOffice = await _context.PersonnelOffices
                .Include(p => p.Addresses)
                .Include(p => p.Departments)
                .Include(p => p.Employees)
                .Include(p => p.LeaveApplications)
                .FirstOrDefaultAsync(m => m.PersonnelOfficeId == id);
            if (personnelOffice == null)
            {
                return NotFound();
            }

            return View(personnelOffice);
        }

        // GET: PersonnelOffices/Create
        public IActionResult Create()
        {
            ViewData["FK_AddressId"] = new SelectList(_context.Addresses, "AddressId", "PostalCode");
            ViewData["FK_DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email");
            ViewData["FK_LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationListId", "LeaveApplicationListId");
            return View();
        }

        // POST: PersonnelOffices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonnelOfficeId,FK_EmployeeId,FK_AddressId,FK_DepartmentId,FK_LeaveApplicationId")] PersonnelOffice personnelOffice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personnelOffice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_AddressId"] = new SelectList(_context.Addresses, "AddressId", "PostalCode", personnelOffice.FK_AddressId);
            ViewData["FK_DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", personnelOffice.FK_DepartmentId);
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", personnelOffice.FK_EmployeeId);
            ViewData["FK_LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationListId", "LeaveApplicationListId", personnelOffice.FK_LeaveApplicationListId);
            return View(personnelOffice);
        }

        // GET: PersonnelOffices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonnelOffices == null)
            {
                return NotFound();
            }

            var personnelOffice = await _context.PersonnelOffices.FindAsync(id);
            if (personnelOffice == null)
            {
                return NotFound();
            }
            ViewData["FK_AddressId"] = new SelectList(_context.Addresses, "AddressId", "PostalCode", personnelOffice.FK_AddressId);
            ViewData["FK_DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", personnelOffice.FK_DepartmentId);
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", personnelOffice.FK_EmployeeId);
            ViewData["FK_LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationListId", "LeaveApplicationListId", personnelOffice.FK_LeaveApplicationListId);
            return View(personnelOffice);
        }

        // POST: PersonnelOffices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonnelOfficeId,FK_EmployeeId,FK_AddressId,FK_DepartmentId,FK_LeaveApplicationId")] PersonnelOffice personnelOffice)
        {
            if (id != personnelOffice.PersonnelOfficeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnelOffice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelOfficeExists(personnelOffice.PersonnelOfficeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_AddressId"] = new SelectList(_context.Addresses, "AddressId", "PostalCode", personnelOffice.FK_AddressId);
            ViewData["FK_DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", personnelOffice.FK_DepartmentId);
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", personnelOffice.FK_EmployeeId);
            ViewData["FK_LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationListId", "LeaveApplicationListId", personnelOffice.FK_LeaveApplicationListId);
            return View(personnelOffice);
        }

        // GET: PersonnelOffices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonnelOffices == null)
            {
                return NotFound();
            }

            var personnelOffice = await _context.PersonnelOffices
                .Include(p => p.Addresses)
                .Include(p => p.Departments)
                .Include(p => p.Employees)
                .Include(p => p.LeaveApplications)
                .FirstOrDefaultAsync(m => m.PersonnelOfficeId == id);
            if (personnelOffice == null)
            {
                return NotFound();
            }

            return View(personnelOffice);
        }

        // POST: PersonnelOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonnelOffices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PersonnelOffices'  is null.");
            }
            var personnelOffice = await _context.PersonnelOffices.FindAsync(id);
            if (personnelOffice != null)
            {
                _context.PersonnelOffices.Remove(personnelOffice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelOfficeExists(int id)
        {
          return (_context.PersonnelOffices?.Any(e => e.PersonnelOfficeId == id)).GetValueOrDefault();
        }
    }
}
