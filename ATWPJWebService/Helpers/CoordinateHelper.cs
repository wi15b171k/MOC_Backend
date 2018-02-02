
using ATWPJWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;
using ATWPJWebService.Settings;

namespace ATWPJWebService.Helpers
{
    public class CoordinateHelper
    {
        private class PhotoDistance
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public double Distance { get; set; }
            public GeoCoordinate Coordinate { get; set; }

        }


        public List<Photo> GroupPhotosByCoordinates(List<Photo> photos)
        {
            PhotoDistance referencePhoto = null;
            List<PhotoDistance> photosInRadius = new List<PhotoDistance>();

            //Fotos innerhalb des Radius finden
            foreach (var item in photos)
            {
                //create objet
                PhotoDistance activePhoto = new PhotoDistance();
                activePhoto.Id = item.Id;
                activePhoto.Coordinate = new GeoCoordinate()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude
                };

                if (referencePhoto == null)
                {
                    referencePhoto = activePhoto;
                }else
                {
                    //calculate distance
                    activePhoto.Distance = referencePhoto.Coordinate.GetDistanceTo(activePhoto.Coordinate);
                    if(activePhoto.Distance <= ApplicationSettings.PhotoGroupingRadius)
                    {
                        photosInRadius.Add(activePhoto);
                    }
                }          
            }

            //Liste sortieren nach Distanz
            

            //Nähesten Punkt wählen und Koordinaten überschreiben


            return null;
        }   
    }

    
}