using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using System.Collections;
using System.Text;

namespace HelpDesk.Pages.Includes
{
    public class ShowJobModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public ShowJobModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Report Reports { get; set; }


        [BindProperty]
        public Repair Repairs { get; set; }
        public IList<Report> Report { get;set; }

        public IList<Repair> Repair { get; set; }

        public IList<Repair> Repair1 { get; set; }
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
                .Where(r => r.Status != "S3: Succeed")
                .ToListAsync();

            Repair = await _context.Repair
               .Include(r => r.Repair_ReportID)
                .Where(r => r.Status != "S2: Solve")
               .ToListAsync();

           

           


            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employee = await _context.Employee.Where(r => r.EmployeeID == Employee1.EmployeeID)
                 .Include(r => r.Position)
                .ToListAsync();






        }
        public async Task<IActionResult> OnPostAsync()
        {



            DateTime date = DateTime.Now;
            Repairs.Date_Start = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);





            Reports.Date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);


            Repair1 = await _context.Repair.ToListAsync();

            StringBuilder a = new StringBuilder("RPR");
            a.Append(Repair1.Count().ToString());
            string b = a.ToString();


            Repairs.RepairName = b;

            _context.Repair.Add(Repairs);

            _context.Attach(Reports).State = EntityState.Modified;



            await _context.SaveChangesAsync();

            return RedirectToPage("./../Repairs/Index");
        }
    }
}
