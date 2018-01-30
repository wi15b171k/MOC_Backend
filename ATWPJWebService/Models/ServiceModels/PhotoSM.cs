using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models.ServiceModels
{
    public class PhotoSM
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public Coordinate Coordinate { get; set; }

        //public binary file
        //...

    }
}