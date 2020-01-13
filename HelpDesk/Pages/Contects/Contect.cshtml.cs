using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.Pages.Contects
{
    public class ContectModel : PageModel
    {
        public string Message { get; set; }

        [BindProperty]
        public Email mails { get; set; }
        public void OnGet()
        {
            Message = "Your contact page.";
        }
        public async Task onPost()
        {
            using (var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = @"c:\Users\Master\Desktop\TBKK-master";
                var msg = new MailMessage
                {
                    Body = mails.Body,
                    Subject = mails.Subject,
                    From = new MailAddress(mails.From)
               };
                msg.To.Add(mails.To);
                await smtp.SendMailAsync(msg);

            }
        }
    }
}