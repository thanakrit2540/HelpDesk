using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace HelpDesk.Pages.Reports
{
    public class CreateModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;


        

      

        public CreateModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Report Report { get; set; }

        public IList<Report> Report1 { get; set; }
        public IList<Employee> Employees { get; set; }

       

        public Employee Employee1 { get; set; }
        [BindProperty]
       
        public string error { get; set; }
        public int count { get; set; }
        public Asset Asset { get; set; }

        

        public async Task OnGetAsync()
        {
           

            ViewData["AssetID"] = new SelectList(_context.Asset, "AssetID", "AssetName");

            Employee1 = HttpContext.Session.GetLogin(_context.Employee);

            Employees = await _context.Employee
                .Where(r => r.EmployeeID == Employee1.EmployeeID).ToListAsync();
        }
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            public async Task<IActionResult> OnPostAsync(int id)
        {
            Employee1 = await _context.Employee.FindAsync(id);
            string sb = Employee1.Email;
            
            var myMail = new MailMessage();
            myMail.From = new MailAddress("Admin System<59160102@go.buu.ac.th>");

            myMail.Subject = "My Subject";
            myMail.To.Add(new MailAddress(sb.ToString()));
            myMail.IsBodyHtml = true;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.Body = "My Body & <b>Description</b>";


            var credential = new NetworkCredential("59160102@go.buu.ac.th", "thanakrit2540bb!"); // User & Password
            var smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            smtpClient.Host = "smtp.gmail.com"; // SMTP
            smtpClient.EnableSsl = true;

            Report1 = await _context.Report
               .ToListAsync();

            try
                {
                    smtpClient.Send(myMail);
                    
                    smtpClient.Dispose();
                    myMail.Dispose();
                  


                }
                catch (Exception)
                {
                    error = "Please check email security.";

                }
            
           
                    DateTime date = DateTime.Now;
                    Report.Date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);



            StringBuilder a = new StringBuilder("RPT");
            a.Append(Report1.Count().ToString());
            string b = a.ToString();


            Report.ReportName = b;

                    _context.Report.Add(Report);


                    await _context.SaveChangesAsync();




            return RedirectToPage("./../Includes/ShowJob");



        }
    }
}
