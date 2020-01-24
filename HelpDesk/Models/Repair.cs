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
        public string RepairName { get; set; }//ID ที่ใช้แทน RepairID

        [Required]
        public DateTime Date_Start { get; set; }// วันที่เริ่มซ่อม หรือ รับการซ่อม

        public DateTime Date_Finish { get; set; }//วันที่คิดว่าซ่อมเสร็จ

        public string Behavior { get; set; }//อาการ

        public string Cause { get; set; }//สาเหตุ

        public string Solving { get; set; }//การแก้ไข
     
        public string Note { get; set; }

        [Required]
        public string Type { get; set; }//ประเภทการซ่อม

        [Required]
        public string Status { get; set; }//สถานะการซ่อม

        public virtual Report Repair_ReportID { get; set; }

        public int ReportID { get; set; }


        public int Repair_EmployeeID { get; set; }//ID พนักงานที่ทำการซ่อม

      
    }
}
