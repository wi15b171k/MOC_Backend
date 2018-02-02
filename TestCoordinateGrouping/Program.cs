using ATWPJWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoordinateGrouping
{
    class Program
    {
        static void Main(string[] args)
        {
            Photo photo1 = new Photo()
            {
                Id = 1,
                TripId = 2,
                Latitude = 48.111111,
                Longitude = 18.111111
            };

            Photo photo2 = new Photo()
            {
                Id = 2,
                TripId = 2,
                Latitude = 48.111112,
                Longitude = 18.111112
            };

            Photo photo3 = new Photo()
            {
                Id = 3,
                TripId = 2,
                Latitude = 48.111113,
                Longitude = 18.111113
            };

            Photo photo4 = new Photo()
            {
                Id = 4,
                TripId = 2,
                Latitude = 48.111114,
                Longitude = 18.111114
            };

            Photo photo5 = new Photo()
            {
                Id = 1,
                TripId = 2,
                Latitude = 48.111115,
                Longitude = 18.111115
            };

            Photo photo6 = new Photo()
            {
                Id = 6,
                TripId = 2,
                Latitude = 48.111116,
                Longitude = 18.111116
            };

            Photo photo7 = new Photo()
            {
                Id = 7,
                TripId = 2,
                Latitude = 48.111117,
                Longitude = 18.111117
            };

            Photo photo8 = new Photo()
            {
                Id = 8,
                TripId = 2,
                Latitude = 48.111118,
                Longitude = 18.111118
            };

        }
    }
}
