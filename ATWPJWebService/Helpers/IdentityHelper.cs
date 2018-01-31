using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models
{
    public class IdentityHelper
    {
        //DB Context
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool isFriendOrOwner(string userId, string ownerId)
        {
            //Check if Friend
            var query = from r in db.Requests
                        where r.RequestFromUserId == userId && r.RequestToUserId == ownerId && r.IsAccepted == true
                        select r;

            var result = query.FirstOrDefault<Request>();

            if (userId == ownerId || result != null)
            {
                return true;
            }
            else{
                return false;
            }
        }

        public bool isOwner(string userId, string ownerId)
        {
            if (userId == ownerId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}