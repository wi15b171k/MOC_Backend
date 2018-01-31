using ATWPJWebService.Models;
using ATWPJWebService.Models.ServiceModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATWPJWebService.Controllers
{
    [Authorize]
    public class TripsController : ApiController
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/trips/abcd-dfdf-dfdf-dfd - get trips by userId
        public IEnumerable<TripSM> Get(string id)
        {
            //Get Id of current User
            var requestUserId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from t in db.Trips
                        where t.UserId == id
                        select t;

            var result = query.ToList<Trip>();

            List<TripSM> trips = null;

            if (result != null)
            {
                //Check User Credentials
                IdentityHelper helper = new IdentityHelper();
                var isAuthorized = helper.isFriendOrOwner(requestUserId, id);
                if (isAuthorized == false)
                {
                    return null;
                }

                //Bind TrimSM Object and return
                trips = new List<TripSM>();
                foreach (var item in result)
                {
                    TripSM trip = new TripSM();
                    trip.Title = item.Title;
                    trip.TripId = item.Id;
                    trip.UserId = item.UserId;

                    foreach (var photo in item.Photos)
                    {
                        trip.Coordinates.Add(new Coordinate
                        {
                            Latitude = photo.Latitude,
                            Longitude = photo.Longitude
                        });
                    }
                   
                    trips.Add(trip);
                }
            }

            return trips;
        }
    }
}
