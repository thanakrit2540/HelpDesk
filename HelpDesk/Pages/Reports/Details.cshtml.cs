using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace HelpDesk.Pages.Reports
{
    public class DetailsModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;



      
        public DetailsModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public Asset Asset { get; set; }
        [BindProperty]
        public Report Report { get; set; }
        
       
        [BindProperty]
        public Repair Repair { get; set; }

        public IList<Repair> Repair1 { get; set; }



        public Employee Employee1 { get; set; }

        public IList<Employee> Employee2 { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
           
            Report = await _context.Report
                .Include(r => r.Report_AssetID).FirstOrDefaultAsync(m => m.ReportID == id);

            Employee = await _context.Employee.FirstOrDefaultAsync(r => r.EmployeeID == Report.Report_EmployeeID)
                ;

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employee2 = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();
            if (Report == null)
            {
                return NotFound();
            }

          

            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "FirstName");
            ViewData["ReportID"] = new SelectList(_context.Report, "AssetName", "AssetName");
            ViewData["AssetID"] = new SelectList(_context.Asset, "AssetID", "AssetName");
            return Page();
        }



        
       

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {



            DateTime date = DateTime.Now;
            Repair.Date_Start = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

            

           

            Report.Date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);


            Repair1 = await _context.Repair.ToListAsync();

            StringBuilder a = new StringBuilder("RPR");
            a.Append(Repair1.Count().ToString());
            string b = a.ToString();


           Repair.RepairName = b;

            _context.Repair.Add(Repair);

            _context.Attach(Report).State = EntityState.Modified;

           
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./../Repairs/Index");
        }
    }
}
