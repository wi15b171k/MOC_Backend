
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
        private List<PhotoCalculation> photosInRadius = null;
        private GeoCoordinate referenceCoordinate = null; 

        private class PhotoCalculation
        {
            public int Id { get; set; }
            public double Distance { get; set; }
            public GeoCoordinate CoordinateOriginal { get; set; }
            public GeoCoordinate CoordinateGrouping { get; set; }
        }

        public List<Photo> GroupPhotosByCoordinates(List<Photo> photos)
        {
            //Clone List
            List<PhotoCalculation> photosGrouping = new List<PhotoCalculation> ();
            foreach (var item in photos)
            {
                photosGrouping.Add(new PhotoCalculation()
                {
                    Id = item.Id,
                    CoordinateOriginal = new GeoCoordinate()
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    },
                    CoordinateGrouping = new GeoCoordinate()
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    }
                });
            }

            while (photosGrouping.Count > 0)
            {
                referenceCoordinate = null;
                photosInRadius = new List<PhotoCalculation>();

                //Fotos innerhalb des Radius finden
                foreach (var item in photosGrouping)
                {
                    //Create Reference Coordinate
                    if (referenceCoordinate == null)
                    {
                        referenceCoordinate = new GeoCoordinate()
                        {
                            Latitude = item.CoordinateGrouping.Latitude,
                            Longitude = item.CoordinateGrouping.Longitude
                        };
                    }

                    //Create objet
                    PhotoCalculation activePhoto = new PhotoCalculation()
                    {
                        Id = item.Id,
                        CoordinateOriginal = new GeoCoordinate()
                        {
                            Latitude = item.CoordinateOriginal.Latitude,
                            Longitude = item.CoordinateOriginal.Longitude
                        },
                        CoordinateGrouping = new GeoCoordinate()
                        {
                            Latitude = item.CoordinateGrouping.Latitude,
                            Longitude = item.CoordinateGrouping.Longitude
                        }
                    };
                    
                    //Calculate distance
                    activePhoto.Distance = referenceCoordinate.GetDistanceTo(activePhoto.CoordinateGrouping);
                    if (activePhoto.Distance <= ApplicationSettings.PhotoGroupingRadius)
                    {
                        photosInRadius.Add(activePhoto);
                    }                    
                }

                //prüfen ob noch ein photo mit Distanz größer Null existierst
                bool isGrouped = true;
                foreach (var item in photosInRadius)
                {
                    if(item.Distance != 0)
                    {
                        isGrouped = false;
                    }
                }

                if(isGrouped == false)
                {
                    //Neue berechnen über ganze Liste
                    double latNew = 0;
                    double longNew = 0;

                    foreach (var item in photosInRadius)
                    {
                        latNew = latNew + item.CoordinateOriginal.Latitude;
                        longNew = longNew + item.CoordinateOriginal.Longitude;
                    }

                    latNew = latNew / photosInRadius.Count;
                    longNew = longNew / photosInRadius.Count;

                    //Coordinaten von photos änndern
                    foreach (var item in photosInRadius)
                    {
                        var photo = photosGrouping.Single(p => p.Id == item.Id);
                        photo.CoordinateGrouping.Latitude = latNew;
                        photo.CoordinateGrouping.Longitude = longNew;
                    }
                }
                else
                {
                    //Wenn alle gruppiert, Fotos aus Liste löschen.
                    foreach (var item in photosInRadius)
                    {
                        //Copie new coordinate to return list
                        var photoOrig = photos.Single(po => po.Id == item.Id);
                        photoOrig.Latitude = item.CoordinateGrouping.Latitude;
                        photoOrig.Longitude = item.CoordinateGrouping.Longitude;

                        //delete photo from photos
                        var photo = photosGrouping.Single(p => p.Id == item.Id);
                        photosGrouping.Remove(photo);
                    }
                }
            }               
            
            return photos;
        }   
    }

    
}