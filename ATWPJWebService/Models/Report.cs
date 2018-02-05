using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models
{
    public class Report
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public bool IsForbidden { get; set; }

        public string Comment { get; set; }

        //FK Photos
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

        //FK zu AspNetUsers --> Reporting User
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}