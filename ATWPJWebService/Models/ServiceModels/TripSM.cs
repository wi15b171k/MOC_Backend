using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models.ServiceModels
{
    public class TripSM
    {
        public int TripId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public List<Coordinate> Coordinates { get; set; }

    }

}