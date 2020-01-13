using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HelpDesk.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public IndexModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }

        public Login Login { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
           
            string Username = Request.Form["Username"];
            string Password = Request.Form["Password"];
            Debug.WriteLine(Username);
            if (Username == string.Empty && Password == string.Empty)
            {
                return Page();

            }


            Login = await _context.Login.Include(e => e.Employee)
                .FirstOrDefaultAsync(u => u.Username.Equals(Username));

            if (Login == null)
            {
                return Page();
            }
            if (!Login.Password.Equals(Password))
            {
                return Page();
            }
            //ViewData["Login_EmployeeID"] = new SelectList(_context.Set<Employee>(), "EmployeeID", "EmployeeID");
            Debug.WriteLine(Login.Login_EmployeeID);




            HttpContext.Session.SetLogin(Login.Employee);
            return RedirectToPage("./Includes/ShowJob");

        }
    }
}
