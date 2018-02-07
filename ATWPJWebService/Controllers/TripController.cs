using ATWPJWebService.Models;
using Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ATWPJWebService.Helpers;

namespace ATWPJWebService.Controllers
{
    [Authorize]
    public class TripController : ApiController
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/trip - Get latest Trip of active user
        public TripSM Get()
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from t in db.Trips
                        where t.UserId == userId
                        orderby t.CreationDate descending
                        select t;

            var result = query.FirstOrDefault<Trip>();
            TripSM lastTrip = null;

            if (result != null)
            {
                //Bind TrimSM Object and return
                lastTrip = new TripSM();
                lastTrip.Title = result.Title;
                lastTrip.TripId = result.Id;
                lastTrip.UserId = result.UserId;
                lastTrip.isPrivate = result.IsPrivate;

                //Group Coordinates
                List<Photo> groupPhotos = new List<Photo>(result.Photos);
                CoordinateHelper cHelper = new CoordinateHelper();
                result.Photos = cHelper.GroupPhotosByCoordinates(groupPhotos);

                foreach (var item in result.Photos)
                {
                    lastTrip.Coordinates.Add(new Coordinate
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    });
                }
            }
            
            return lastTrip;
        }

        // GET api/trip/5 - get trip by tripId
        public TripSM Get(int id)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from t in db.Trips
                        where t.Id == id
                        select t;

            var result = query.FirstOrDefault<Trip>();

            TripSM trip = null;

            if (result != null)
            {
                //Check User Credentials
                IdentityHelper helper = new IdentityHelper();

                //check if private
                if (result.IsPrivate == true && helper.isOwner(userId, result.UserId) == false)
                {
                    return null;
                }

                var isAuthorized = helper.isFriendOrOwner(userId, result.UserId);
                if(isAuthorized == false)
                {
                    return null;
                }

                //Bind TrimSM Object and return
                trip = new TripSM();
                trip.Title = result.Title;
                trip.TripId = result.Id;
                trip.UserId = result.UserId;
                trip.isPrivate = result.IsPrivate;

                //Group Coordinates
                List<Photo> groupPhotos = new List<Photo>(result.Photos);
                CoordinateHelper cHelper = new CoordinateHelper();
                result.Photos = cHelper.GroupPhotosByCoordinates(groupPhotos);

                foreach (var item in result.Photos)
                {
                    trip.Coordinates.Add(new Coordinate
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    });
                }
            }

            return trip;
        }

        // POST api/trip - add new trip
        public HttpResponseMessage Post([FromBody]TripAddSM value)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            db.Trips.Add(new Trip()
            {
                Title = value.Title,
                CreationDate = DateTime.Now,
                UserId = userId,
                IsPrivate = value.isPrivate
            });

            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
