using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;

namespace HelpDesk.Pages.Repairs
{
    public class EditModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public EditModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Repair Repair { get; set; }

        public IList<Report> Report { get; set; }
        public Employee Employee1 { get; set; }

        public IList<Employee> Employees { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employees = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();

            Report = await _context.Report
               .Include(r => r.Report_AssetID)
               .ToListAsync();

            Repair = await _context.Repair
                .Include(r => r.Repair_ReportID).FirstOrDefaultAsync(m => m.RepairID == id);

            if (Repair == null)
            {
                return NotFound();
            }
           ViewData["ReportID"] = new SelectList(_context.Report, "ReportID", "ReportName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.Attach(Repair).State = EntityState.Modified;

          
                await _context.SaveChangesAsync();
           

            return RedirectToPage("./Index");
        }

        private bool RepairExists(int id)
        {
            return _context.Repair.Any(e => e.RepairID == id);
        }
    }
}
