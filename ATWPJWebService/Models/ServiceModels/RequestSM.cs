using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models.ServiceModels
{
    public class RequestSM
    {
        public int RequestId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }

    }
}