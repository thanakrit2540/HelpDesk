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
    public class DeleteModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

     

        
        public DeleteModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]
        public Report Report { get; set; }

        [BindProperty]
        public Repair Repair { get; set; }

        public Repair Repairs { get; set; }

        public Employee Employee1 { get; set; }

        public IList<Employee> Employees { get; set; }


        //Get การแสดงหน้า
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employees = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();


            if (id == null)
            {
                return NotFound();
            }
            

            Repair = await _context.Repair
                .Include(r => r.Repair_ReportID).FirstOrDefaultAsync(m => m.RepairID == id);
            Report = await _context.Report
               .Include(r => r.Report_AssetID).FirstOrDefaultAsync(m => m.ReportID == Repair.ReportID);

            Employee = await _context.Employee.FirstOrDefaultAsync(r => r.EmployeeID == Repair.Repair_EmployeeID)
               ;

            




            if (Repair == null)
            {
                return NotFound();
            }
            ViewData["ReportID"] = new SelectList(_context.Report, "ReportID", "ReportID");
            ViewData["AssetID"] = new SelectList(_context.Asset, "AssetID", "AssetID");
            return Page();

        }
        
       


        //POST การกดปุ่มเปลี่ยนหรือใช้งาน
        public async Task<IActionResult> OnPostAsync()
        {

            DateTime date = DateTime.Now;
            Repair.Date_Finish = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

            Report.Date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

            _context.Attach(Report).State = EntityState.Modified;

            _context.Attach(Repair).State = EntityState.Modified;





            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");


        }
           

           

           
        
    }
}
