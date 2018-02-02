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
    public class UsersController : ApiController
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/users - get users by searchsting
        [Route("api/users/{searchString}")]
        public IEnumerable<PersonSM> Get(string searchString)
        {
            //Get Id of current User
            var requestUserId = User.Identity.GetUserId();

            //Get Data From DB
            var query = from u in db.Users
                        where u.FirstName.Contains(searchString) || u.LastName.Contains(searchString) || u.Email.Contains(searchString)
                        select u;

            var result = query.ToList<ApplicationUser>();

            List<PersonSM> users = null;

            if (result != null)
            {
                //Bind TrimSM Object and return
                users = new List<PersonSM>();

                foreach (var item in result)
                {
                    PersonSM user = new PersonSM();
                    user.FirstName = item.FirstName;
                    user.LastName = item.LastName;
                    user.PersonId = item.Id;

                    users.Add(user);
                }
            }

            return users;
        }
    }
}
