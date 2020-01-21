using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HelpDesk.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Pages.Assets
{
    public class CreateModel : PageModel
    {
        private readonly HelpDesk.Models.HelpDeskContext _context;

        public CreateModel(HelpDesk.Models.HelpDeskContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Asset> Assets { get; set; }
        public async Task OnGetAsync()
        {
           

            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
        ViewData["ModelID"] = new SelectList(_context.Models, "ModelID", "ModelID");
        ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID");
           
        }

        [BindProperty]
        public Asset Asset { get; set; }

      

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Assets = await _context.Asset
          .ToListAsync();


            if (!ModelState.IsValid)
            {
                return Page();
            }
            StringBuilder a = new StringBuilder("ASS");
            a.Append(Assets.Count().ToString());
            string b = a.ToString();
            Asset.AssetName = b;

            _context.Asset.Add(Asset);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
