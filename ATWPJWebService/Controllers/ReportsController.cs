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
    public class ReportsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST api/report - report a picture
        public HttpResponseMessage Post([FromBody]ReportAddSM report)
        {
            //Check User credentials
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var queryPhoto = from p in db.Photos
                             where p.Id == report.PicId
                             select p;

            var resultPhoto = queryPhoto.FirstOrDefault<Photo>();
            if (resultPhoto == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            //get trip owner and private stat
            var queryTrip = from t in db.Trips
                            where t.Id == resultPhoto.TripId
                            select t;

            var resultTrip = queryTrip.FirstOrDefault<Trip>();
            if (resultTrip == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            //Check User Credentials
            IdentityHelper helper = new IdentityHelper();

            //check if private
            if (resultTrip.IsPrivate == true && helper.isOwner(userId, resultTrip.UserId) == false)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            if (helper.isFriendOrOwner(userId, resultTrip.UserId) == false)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            

            db.Reports.Add(new Report()
            {
                PhotoId = report.PicId,
                UserId = userId,
                IsDone = false,
                Comment = report.Comment
            });

            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
