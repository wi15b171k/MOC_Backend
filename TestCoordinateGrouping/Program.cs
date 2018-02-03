using ATWPJWebService.Helpers;
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
                Latitude = 48.11111,
                Longitude = 18.11111
            };

            Photo photo2 = new Photo()
            {
                Id = 2,
                TripId = 2,
                Latitude = 48.11120,
                Longitude = 18.11112
            };

            Photo photo3 = new Photo()
            {
                Id = 3,
                TripId = 2,
                Latitude = 47.11130,
                Longitude = 18.11113
            };

            Photo photo4 = new Photo()
            {
                Id = 4,
                TripId = 2,
                Latitude = 47.11140,
                Longitude = 18.11114
            };

            Photo photo5 = new Photo()
            {
                Id = 5,
                TripId = 2,
                Latitude = 46.11151,
                Longitude = 18.11115
            };

            Photo photo6 = new Photo()
            {
                Id = 6,
                TripId = 2,
                Latitude = 48.11161,
                Longitude = 18.11116
            };

            Photo photo7 = new Photo()
            {
                Id = 7,
                TripId = 2,
                Latitude = 48.12170,
                Longitude = 18.11117
            };

            Photo photo8 = new Photo()
            {
                Id = 8,
                TripId = 2,
                Latitude = 48.11280,
                Longitude = 18.111118
            };

            List<Photo> orig = new List<Photo>();
            orig.Add(photo1);
            orig.Add(photo2);
            orig.Add(photo3);
            orig.Add(photo4);
            orig.Add(photo5);
            orig.Add(photo6);
            orig.Add(photo7);
            orig.Add(photo8);

            Console.WriteLine("Orignalwerte: ");
            foreach (var item in orig)
            {
                Console.WriteLine("ID: " + item.Id + ", Lat: " + item.Latitude + ", Long: " + item.Longitude);
            }

            var helper = new CoordinateHelper();
            var changedCoordinate = helper.GroupPhotosByCoordinates(orig);



            Console.WriteLine("\nGruppiete Werte: ");
            foreach (var item in changedCoordinate)
            {
                Console.WriteLine("ID: " + item.Id + ", Lat: " + item.Latitude + ", Long: " + item.Longitude);
            }

            Console.ReadLine();
        }
    }
}
