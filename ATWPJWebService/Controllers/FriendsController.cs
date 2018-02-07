using ATWPJWebService.Models;
using Microsoft.AspNet.Identity;
using Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATWPJWebService.Controllers
{
    public class FriendsController : ApiController
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/friends - get friends of active user
        public IEnumerable<PersonSM> Get()
        {
            //Get Id of current User
            var requestUserId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from r in db.Requests
                        where r.RequestFromUserId == requestUserId && r.IsNew == false && r.IsAccepted == true
                        select r;

            var result = query.ToList<Request>();

            List<PersonSM> friends = null;

            if (result != null)
            {
                //Bind TrimSM Object and return
                friends = new List<PersonSM>();

                foreach (var item in result)
                {
                    PersonSM friend = new PersonSM();
                    friend.FirstName = item.RequestToUser.FirstName;
                    friend.LastName = item.RequestToUser.LastName;
                    friend.PersonId = item.RequestToUser.Id;
                    
                    friends.Add(friend);
                }
            }

            return friends;
        }
    }
}
