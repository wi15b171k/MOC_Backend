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
    [Authorize]
    public class UserController : ApiController
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/user - get data of active user

        public PersonSM Get()
        {
            //Get Id of current User
            var requestUserId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from u in db.Users
                        where u.Id == requestUserId
                        select u;

            var result = query.FirstOrDefault();

            PersonSM user = null;

            if (result != null)
            {
                //Bind TrimSM Object and return
                user = new PersonSM()
                {
                    PersonId = result.Id,
                    FirstName = result.FirstName,
                    LastName = result.LastName
                };

                
            }

            return user;
        }
    }
}
