using System;
using System.Collections;
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
        public IList<Asset> Asset { get; set; }
        public IList<Report> Report { get; set; }

        public IList<Report> Report_New { get; set; }


        public IList<Repair> Repair { get; set; }

        public IList<sad> Report_Asset_Count { get; set; }
        public IList<Repair> Repair_OnFix { get; set; }

        public IList<Repair> Repair_Close { get; set; }

        public IList<Repair> Repair_Succesful { get; set; }

        public Employee Employee1 { get; set; }

        public IList<Employee> Employees { get; set; }

        public IList<Report> Report_Asset_count { get; set; }
        public IList<Report> Reports { get; set; }
        public IList<int> count { get; set; }

        public IList<string> Report_AssetName { get; set; }
        public IList<string> MostAsset { get; set; } 
        public IList<Report> Report_Asset { get; set; }
        public async Task OnGetAsync()
        {

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employees = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();

            MostAsset = new List<string>();

            count = new List<int>();

            Report = await _context.Report
                .Include(r => r.Report_AssetID)  
                .ToListAsync();

            Asset = await _context.Asset
                .ToListAsync();

            Reports = await _context.Report
              .Include(r => r.Report_AssetID)
              .ToListAsync();

            var a = new List<sad>();

            Report_AssetName = new List<string>();

            Boolean flag = false;
            foreach (var item in Asset)
            {
                flag = false;
                foreach (var item2 in Report)
                { 
                    
                    if (Report_AssetName.Count == 0)
                    {
                        Report_AssetName.Add(item2.Report_AssetID.AssetName);
                        flag = true;
                    }
                    else
                    {
                       
                        if (item.AssetName == item2.Report_AssetID.AssetName)
                        {
                            for(int i = 0; i < Report_AssetName.Count; i++)
                            {
                                if (Report_AssetName[i] == item2.Report_AssetID.AssetName)
                                {
                                    flag = true;
                                }
                            }
                          
                        }
                    }
                 
                }
                if (flag == false)
                {
                    Report_AssetName.Add(item.AssetName);
                }
            }
          
               
                   
                
                    

               for(int i = 0; i < Report_AssetName.Count; i++)
                {
                var s = new sad();
                flag = false;
                s.Reports = Report_AssetName[i];
                foreach (var item in Report)
                {
                    if (item.Report_AssetID.AssetName.Equals(Report_AssetName[i]))
                    {
                        s.count += 1;
                    }
                            
                }
             
               
                a.Add(s);
                }
               
            
                
            
            a.OrderByDescending(r => r.count).ToList();
            Report_Asset_Count = a;


           



            




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


    public class sad
    {
        public int count { get; set; }

        public string Reports { get; set; }
        
    }

}