using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPrivate { get; set; }


        //FK zu AspNetUsers
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}