using ATWPJWebService.Models;
using Shared.ServiceModels;
using ATWPJWebService.Settings;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATWPJWebService.Controllers
{
    
    public class ReportsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST api/report - report a picture
        [Authorize]
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

        // GET api/reports - get all undone reports with 
        [Authorize(Roles = "admin")]
        public IEnumerable<ReportSM> Get()
        {
            //Get Id of current User
            var requestUserId = User.Identity.GetUserId();
            var isAdmin = User.IsInRole("admin");
            var isAdmin2 = User.IsInRole("846004c4-c444-4ae5-a063-c0401e03bc4f");


            //Get Data From DB
            var query = from r in db.Reports
                        where r.IsDone == false
                        select r;

            var result = query.ToList<Report>();

            List<ReportSM> reports = null;

            if (result != null)
            {
               
                reports = new List<ReportSM>();

                foreach (var item in result)
                {
                    ReportSM report = new ReportSM();
                    report.ReportId = item.Id;
                    report.FirstNameReportingUser = item.User.FirstName;
                    report.LastNameReportingUser = item.User.LastName;
                    report.PicId = item.PhotoId;
                    report.FirstNameOwner = item.Photo.Trip.User.FirstName;
                    report.LastNameOwner = item.Photo.Trip.User.LastName;
                    report.Comment = item.Comment;

                    reports.Add(report);
                }
            }

            return reports;
        }

        // PUT api/requests/5
        [Authorize(Roles = "admin")]
        public HttpResponseMessage Put(int id, [FromBody]bool isForbidden)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from r in db.Reports
                        where r.Id == id
                        select r;



            var result = query.FirstOrDefault<Report>();
            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }


            if(isForbidden == true)
            {
                //delete picture
                //Get Data From DB
                var queryPhoto = from p in db.Photos
                                 where p.Id == result.PhotoId
                                 select p;

                var resultPhoto = queryPhoto.FirstOrDefault<Photo>();
                if (resultPhoto == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }

                try
                {
                    File.Delete(ApplicationSettings.FileRootDirectory + resultPhoto.FileName);
                }
                catch (Exception)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }

                result.IsForbidden = true;
                resultPhoto.isDeleted = true;
            }
            else
            {
                result.IsForbidden = false;
            }

            result.IsDone = true;
            db.SaveChanges();


            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }


}
