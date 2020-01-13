using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using System.Collections;

namespace HelpDesk.Pages.Includes
{
    public class ShowJobModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public ShowJobModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }

        public IList<Report> Report { get;set; }

        public IList<Repair> Repair { get; set; }

        public List<string> RepairID { get; set; }

        public List<string> Repair_Status { get; set; }

        public List<string> Repair_Employee { get; set; }

        public IList<string> Report_Firstname { get; set; }

        public IList<string> Report_Lastname { get; set; }

        public IList<Employee> Employee { get; set; }

        public Employee Employee1 { get; set; }
        public List<string> Repair_Date { get; set; }
        public async Task OnGetAsync()
        {
            Report = await _context.Report
                .Include(r => r.Report_AssetID)
                .Where(r => r.Date.Date.Day==DateTime.Today.Day)
                .ToListAsync();

            Repair = await _context.Repair
               .Include(r => r.Repair_ReportID)
               .ToListAsync();

           

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employee = await _context.Employee.Where(r => r.EmployeeID == Employee1.EmployeeID)
                 .Include(r => r.Position)
                .ToListAsync();






        }
    }
}
