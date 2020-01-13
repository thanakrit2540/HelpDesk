using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Login
    {
        public int LoginID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        [ForeignKey("Employee")]
        public int Login_EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
