using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Repair
    {
        public int RepairID { get; set; }

        [Required]
        public string RepairName { get; set; }

        [Required]
        public DateTime Date_Start { get; set; }

        public DateTime Date_Finish { get; set; }



        public string Note { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual Report Repair_ReportID { get; set; }

        public int ReportID { get; set; }


        public int Repair_EmployeeID { get; set; }

      
    }
}
