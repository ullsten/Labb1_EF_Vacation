using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb1_EF.Data;
using Labb1_EF.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Labb1_EF.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Labb1_EF.Controllers
{
    [Authorize(Roles = "Employee")]
    public class LeaveApplicationListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LeaveApplicationListsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            var emp = (await _userManager
              .GetUsersInRoleAsync("Employee"))
              .ToArray();

            var model = new AdminViewModel
            {
                Employees = emp,
            };

            var applicationDbContext = _context.LeaveApplications
                   .Include(la => la.Employees)
                   .Include(la => la.LeaveTypes);
 
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> filterApplications([Required] DateTime? searchStart, [Required] DateTime? searchEnd)
        {

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please choose a start and end date.";
                return RedirectToAction("Index"); // Redirect to the index action
            }

            ViewData["CurrentStartFilter"] = searchStart;
            ViewData["CurrentEndFilter"] = searchEnd;

            var applicationDbContext = _context.LeaveApplications
                .Include(la => la.Employees)
                .Include(la => la.LeaveTypes)
                .Where(la => la.CreatedAt >= searchStart && la.CreatedAt <= searchEnd);

            return View(await applicationDbContext.ToListAsync());

        }

        // GET: LeaveApplicationLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaveApplications == null)
            {
                return NotFound();
            }

            var leaveApplicationList = await _context.LeaveApplications
                .Include(l => l.Employees)
                .Include(l => l.LeaveTypes)
                .FirstOrDefaultAsync(m => m.LeaveApplicationListId == id);
            if (leaveApplicationList == null)
            {
                return NotFound();
            }

            return View(leaveApplicationList);
        }

        // GET: LeaveApplicationLists/Create
        public IActionResult Create()
        {
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FullName");
            ViewData["FK_LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeName");
            return View();
        }

        // POST: LeaveApplicationLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveApplicationListId,StartDate,EndDate,FK_EmployeeId,FK_LeaveTypeId")] LeaveApplicationList leaveApplicationList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveApplicationList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", leaveApplicationList.FK_EmployeeId);
            ViewData["FK_LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeId", leaveApplicationList.FK_LeaveTypeId);
            return View(leaveApplicationList);
        }

        // GET: LeaveApplicationLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveApplications == null)
            {
                return NotFound();
            }

            var leaveApplicationList = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplicationList == null)
            {
                return NotFound();
            }
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FullName", leaveApplicationList.FK_EmployeeId);
            ViewData["FK_LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeName", leaveApplicationList.FK_LeaveTypeId);
            return View(leaveApplicationList);
        }

        // POST: LeaveApplicationLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveApplicationListId,StartDate,EndDate,FK_EmployeeId,FK_LeaveTypeId")] LeaveApplicationList leaveApplicationList)
        {
            if (id != leaveApplicationList.LeaveApplicationListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveApplicationList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationListExists(leaveApplicationList.LeaveApplicationListId))
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
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", leaveApplicationList.FK_EmployeeId);
            ViewData["FK_LeaveTypeId"] = new SelectList(_context.LeaveTypes, "LeaveTypeId", "LeaveTypeId", leaveApplicationList.FK_LeaveTypeId);
            return View(leaveApplicationList);
        }

        // GET: LeaveApplicationLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveApplications == null)
            {
                return NotFound();
            }

            var leaveApplicationList = await _context.LeaveApplications
                .Include(l => l.Employees)
                .Include(l => l.LeaveTypes)
                .FirstOrDefaultAsync(m => m.LeaveApplicationListId == id);
            if (leaveApplicationList == null)
            {
                return NotFound();
            }

            return View(leaveApplicationList);
        }

        // POST: LeaveApplicationLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveApplications == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LeaveApplications'  is null.");
            }
            var leaveApplicationList = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplicationList != null)
            {
                _context.LeaveApplications.Remove(leaveApplicationList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationListExists(int id)
        {
          return (_context.LeaveApplications?.Any(e => e.LeaveApplicationListId == id)).GetValueOrDefault();
        }
    }
}
