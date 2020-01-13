﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Report
    {
        public int ReportID { get; set; }

        [Required]
        public string ReportName { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public string Note { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public string Status { get; set; }

        public virtual Asset Report_AssetID { get; set; }

        public int AssetID { get; set; }

        public int Report_EmployeeID { get; set; }

       

        

       


    }
}
