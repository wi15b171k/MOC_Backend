
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
        private List<Photo> groupedPhotos = null;
        private PhotoDistance referencePhoto = null;
        private List<PhotoDistance> photosInRadius = null;

        private class PhotoDistance
        {
            public int Id { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public double Distance { get; set; }
            public GeoCoordinate Coordinate { get; set; }
            public string FileName { get; set; }
            public int TripId { get; set; }

        }

        public List<Photo> GroupPhotosByCoordinates(List<Photo> photosOrig)
        {
            photosInRadius = new List<PhotoDistance>();
            groupedPhotos = new List<Photo>();

            //Clone List
            List<Photo> photos = new List<Photo>(photosOrig);
            while(photos != null)
            {
                //Fotos innerhalb des Radius finden
                foreach (var item in photos)
                {
                    //create objet
                    PhotoDistance activePhoto = new PhotoDistance();
                    activePhoto.Id = item.Id;
                    activePhoto.FileName = item.FileName;
                    activePhoto.TripId = item.TripId;
                    activePhoto.Latitude = item.Latitude;
                    activePhoto.Longitude = item.Longitude;
                    activePhoto.Coordinate = new GeoCoordinate()
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    };

                    if (referencePhoto == null)
                    {
                        referencePhoto = activePhoto;
                    }
                    else
                    {
                        //calculate distance
                        activePhoto.Distance = referencePhoto.Coordinate.GetDistanceTo(activePhoto.Coordinate);
                        if (activePhoto.Distance <= ApplicationSettings.PhotoGroupingRadius)
                        {
                            photosInRadius.Add(activePhoto);
                        }
                    }
                }

                //Liste sortieren nach Distanz
                List<PhotoDistance> photosInRadiusOrdered = photosInRadius.OrderBy(o => o.Distance).ToList();

                //Nähesten Punkt wählen der nicht Null ist
                PhotoDistance closestPhoto = null;
                foreach (var item in photosInRadiusOrdered)
                {
                    if (item.Distance != 0)
                    {
                        closestPhoto = item;
                    }
                }

                //Nur wenn Foto innerhalb Radius mit ungeleichen Koordinaten
                if (closestPhoto != null)
                {
                    //Neuen Koordinaten Berechnen
                    double latNew = (referencePhoto.Latitude + closestPhoto.Latitude) / 2;
                    double longNew = (referencePhoto.Longitude + closestPhoto.Longitude) / 2;

                    //Fotos in neue Liste speichern und aus alter Liste entfernen
                    photos.Find(item => item.Id == referencePhoto.Id).Latitude = latNew;
                    photos.Find(item => item.Id == closestPhoto.Id).Latitude = latNew;
                    photos.Find(item => item.Id == referencePhoto.Id).Longitude = longNew;
                    photos.Find(item => item.Id == closestPhoto.Id).Longitude = longNew;
                }
                else
                {
                    //foreach (var item in photos)
                    for (int i = photos.Count - 1; i >= 0; i--)
                    {
                        if (photos[i].Latitude == referencePhoto.Latitude && photos[i].Longitude == referencePhoto.Longitude)
                        {
                            //Update orgi List
                            photosOrig.Single(orig => orig.Id == photos[i].Id).Latitude = referencePhoto.Latitude;
                            photosOrig.Single(orig => orig.Id == photos[i].Id).Longitude = referencePhoto.Longitude;

                            //Revomve form copy
                            photos.RemoveAt(i);
                        }
                    }
                }
            }
                
            
            return photosOrig;
        }   
    }

    
}