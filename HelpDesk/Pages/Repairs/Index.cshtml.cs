using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpDesk.Pages.Repairs
{
    public class IndexModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public IndexModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }
        public IList<Employee> Employee { get; set; }
        public IList<Repair> Repair { get;set; }

        public IList<Repair> Repair1 { get; set; }
        public IList<Report> Report { get; set; }

        public Employee Employee1 { get; set; }

        public IList<Employee> Employees { get; set; }

        public async Task OnGetAsync()
        {

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);


            Employees = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();

            Report = await _context.Report
               .Include(r => r.Report_AssetID)
               .ToListAsync();

            Repair = await _context.Repair
                .Include(r => r.Repair_ReportID)
                .Where(r => r.Repair_EmployeeID == Employee1.EmployeeID)
                .ToListAsync();

          

            Repair1 = await _context.Repair.ToListAsync();

            Employee = await _context.Employee.ToListAsync();



            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
        }
    }
}
