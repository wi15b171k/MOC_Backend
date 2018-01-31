using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models.ServiceModels
{
    public class TripSM
    {
        public TripSM()
        {
            Coordinates = new List<Coordinate>();
        }

        public int TripId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public bool isPrivate { get; set; }

    }


    public class TripAddSM
    {
        public string Title { get; set; }
        public bool isPrivate { get; set; }
    }

}