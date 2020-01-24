using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;

namespace HelpDesk
{
    public class SummaryJobModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public SummaryJobModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }
        public Employee Employee1 { get; set; }
        public IList<Repair> Repair { get;set; }

        public IList<Employee> Employees { get; set; }
        public async Task OnGetAsync()
        {
            Repair = await _context.Repair
                .Include(r => r.Repair_ReportID).ToListAsync();

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employees = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();
        }
    }
}
