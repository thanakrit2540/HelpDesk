using System;
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
        public string ReportName { get; set; }//ID แทน ReportID 

        [Required]
        public string Title { get; set; }// หัวข้อ
        [Required]
        public DateTime Date { get; set; }// วันที่ทำการ แจ้ง

        public string Note { get; set; }// รายละเอียด

       
        [Required]
        public string Status { get; set; }//สถานะการแจ้ง

        public virtual Asset Report_AssetID { get; set; }

        public int AssetID { get; set; }

        public int Report_EmployeeID { get; set; }//ID ของพนักงานที่ทำการแจ้ง

       

        

       


    }
}
