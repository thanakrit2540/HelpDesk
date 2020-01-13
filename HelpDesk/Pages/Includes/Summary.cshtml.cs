using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk
{
    public class SummaryModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public SummaryModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }

        public IList<Report> Report { get; set; }

        public IList<Report> Report_New { get; set; }


        public IList<Repair> Repair { get; set; }


        public IList<Repair> Repair_OnFix { get; set; }

        public IList<Repair> Repair_Close { get; set; }

        public IList<Repair> Repair_Succesful { get; set; }

        public IList<Report> Report_Asset { get; set; }
        public async Task OnGetAsync()
        {
            Report = await _context.Report
                .Include(r => r.Report_AssetID) 
                .ToListAsync();

            



            Report_New = await _context.Report
               .Include(r => r.Report_AssetID)
               .Where(r => r.Status == "S1: New")
               .ToListAsync();

            Repair_OnFix = await _context.Repair
               .Include(r => r.Repair_ReportID)
               .Where(r =>r.Status == "S1: On Fixing")
               .ToListAsync();

            Repair_Close = await _context.Repair
               .Include(r => r.Repair_ReportID)
               .Where(r => r.Status == "S3: Can't fix")
               .ToListAsync();

            Repair_Succesful = await _context.Repair
               .Include(r => r.Repair_ReportID)
               .Where(r => r.Status == "S2: Sucessful")
               .ToListAsync();

            Repair = await _context.Repair
               .Include(r => r.Repair_ReportID)
               .ToListAsync();




            






        }
    }
}