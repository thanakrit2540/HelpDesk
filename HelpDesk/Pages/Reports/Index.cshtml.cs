using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpDesk.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public IndexModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }

        public IList<Asset> Asset { get; set; }
        public IList<Report> Report { get;set; }

        public IList<string> Report_Firstname { get; set; }

        public IList<string> Report_Lastname { get; set; }

        public IList<Employee> Employee { get; set; }

        public Employee Employee1 { get; set; }

        public IList<Employee> Employees { get; set; }
        public async Task OnGetAsync()
        {
           

            Asset = await _context.Asset.ToListAsync();

            Employee = await _context.Employee.ToListAsync();

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employees = await _context.Employee
             .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();

            if (Employee1.Employee_PositionID == 1)
            {
                Report = await _context.Report
               .Include(r => r.Report_AssetID)
              
               .ToListAsync();
            }
            else
            {
                Report = await _context.Report
             .Include(r => r.Report_AssetID)
             .Where(r => r.Report_EmployeeID == Employee1.EmployeeID)
             .ToListAsync();
            }

            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            ViewData["ReportID"] = new SelectList(_context.Report, "ReportID", "ReportID");
            ViewData["AssetID"] = new SelectList(_context.Asset, "AssetID", "AssetID");
         

        }
    }
}
