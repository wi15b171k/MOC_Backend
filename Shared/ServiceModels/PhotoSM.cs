using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ServiceModels
{
    public class PhotoSM
    {
        public int PhotoId { get; set; }
        public int TripId { get; set; }
        public Coordinate Coordinate { get; set; }
    }
}
