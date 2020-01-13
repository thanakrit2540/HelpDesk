﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;

namespace HelpDesk.Pages.Reports
{
    public class EditModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        
       
        public EditModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Asset Asset { get; set; }
        [BindProperty]
        public Report Report { get; set; }

        public Employee Employee1 { get; set; }

        public IList<Employee> Employees { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = await _context.Report
                .Include(r => r.Report_AssetID).FirstOrDefaultAsync(m => m.ReportID == id);

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employees = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();

            if (Report == null)
            {
                return NotFound();
            }
           ViewData["AssetID"] = new SelectList(_context.Asset, "AssetID", "AssetName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(Report.ReportID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.ReportID == id);
        }
    }
}
