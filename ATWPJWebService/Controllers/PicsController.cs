using ATWPJWebService.Models;
using ATWPJWebService.Models.ServiceModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATWPJWebService.Controllers
{
    [Authorize]
    public class PicsController : ApiController
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/pics/3 - get pics by tripId
        public IEnumerable<PhotoSM> Get(int Id)
        {
            //Get Id of current User
            var requestUserId = User.Identity.GetUserId();

            //Check User Credentials
            var queryTrip = from t in db.Trips
                            where t.Id == Id
                            select t;

            IdentityHelper helper = new IdentityHelper();
            var isAuthorized = helper.isFriendOrOwner(requestUserId, queryTrip.FirstOrDefault<Trip>().UserId);
            if (isAuthorized == false)
            {
                return null;
            }


            //Get Data From DB
            var queryPhotos = from ph in db.Photos
                              where ph.TripId == Id
                              select ph;

            var result = queryPhotos.ToList<Photo>();

            List<PhotoSM> photos = null;

            if (result != null)
            {
                //Bind PhotoSM Object and return
                photos = new List<PhotoSM>();
                foreach (var item in result)
                {
                    PhotoSM photo = new PhotoSM();
                    photo.PhotoId = item.Id;
                    photo.TripId = item.TripId;
                    photo.Coordinate = new Coordinate()
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    };

                    photos.Add(photo);
                }
            }

            return photos;
        }

        // GET /api/pics/{tripId}?latitude={latitude}&longitude={longitude}/ - get pics by tripId and long and lat
        //ACHTUNG: Abschließender slash bei URL ist wichtig!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        [Route("api/Pics/{Id}/{latitude}/{longitude}")]
        public IEnumerable<PhotoSM> Get(int Id, string latitude, string longitude)
        {
            //Get Id of current User
            var requestUserId = User.Identity.GetUserId();

            //Check User Credentials
            var queryTrip = from t in db.Trips
                            where t.Id == Id
                            select t;

            IdentityHelper helper = new IdentityHelper();
            var isAuthorized = helper.isFriendOrOwner(requestUserId, queryTrip.FirstOrDefault<Trip>().UserId);
            if (isAuthorized == false)
            {
                return null;
            }

            //Parse Longitude and Latitude
            double dLongitude = double.Parse(longitude.Replace(',', '.'), CultureInfo.InvariantCulture);
            double dLatitude = double.Parse(latitude.Replace(',', '.'), CultureInfo.InvariantCulture);

            //Get Data From DB
            var queryPhotos = from ph in db.Photos
                              where ph.TripId == Id && ph.Latitude == dLatitude && ph.Longitude == dLongitude
                              select ph;

            var result = queryPhotos.ToList<Photo>();

            List<PhotoSM> photos = null;

            if (result != null)
            {
                //Bind PhotoSM Object and return
                photos = new List<PhotoSM>();
                foreach (var item in result)
                {
                    PhotoSM photo = new PhotoSM();
                    photo.PhotoId = item.Id;
                    photo.TripId = item.TripId;
                    photo.Coordinate = new Coordinate()
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    };

                    photos.Add(photo);
                }
            }

            return photos;
        }
    }
}
