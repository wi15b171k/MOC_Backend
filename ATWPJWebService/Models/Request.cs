using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models
{
    public class Request
    {
        public int Id { get; set; }
        
        public bool IsNew { get; set; }
        public bool IsAccepted { get; set; }

        public DateTime CreationDate { get; set; }

        public string RequestFromUserId { get; set; }
        [ForeignKey("RequestFromUserId")]
        [InverseProperty("Requests")]
        public virtual ApplicationUser RequestFromUser { get; set; }

        public string RequestToUserId { get; set; }
        [ForeignKey("RequestToUserId")]
        public virtual ApplicationUser RequestToUser { get; set; }

    }
}