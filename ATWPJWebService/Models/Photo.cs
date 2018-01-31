using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        //FK zu AspNetUsers
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        //Nav Properties
        public virtual ICollection<Report> Reports { get; set; }
    }
}