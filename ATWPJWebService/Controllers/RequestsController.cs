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
    public class RequestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/requests - Get active friend requests of users
        public IEnumerable<RequestSM> Get()
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from r in db.Requests
                        where r.RequestToUserId == userId && r.IsNew == true
                        select r;

            var result = query.ToList<Request>();
            List<RequestSM> activeRequests = null;

            if (result != null)
            {
                activeRequests = new List<RequestSM>();
                foreach (var item in result)
                {
                    //Bind TrimSM Object and return
                    RequestSM request = new RequestSM();
                    request.FirstName = item.RequestFromUser.FirstName;
                    request.LastName = item.RequestFromUser.LastName;
                    request.RequestId = item.Id;

                    activeRequests.Add(request);
                }
            }

            return activeRequests;
        }

        // POST api/request - add new request
        public HttpResponseMessage Post([FromBody]string value)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            db.Requests.Add(new Request()
            {
                RequestToUserId = value,
                RequestFromUserId = userId,
                CreationDate = DateTime.Now,
                IsAccepted = false,
                IsNew = true
            });

            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT api/requests/5
        public HttpResponseMessage Put(int id, [FromBody]bool value)
        {
            //Get Id of current User
            var userId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from r in db.Requests
                             where r.Id == id && r.RequestToUserId == userId
                             select r;

            

            var result = query.FirstOrDefault<Request>();
            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            result.IsAccepted = value;
            result.IsNew = false;
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
