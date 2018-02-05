using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models.ServiceModels
{
    public class ReportSM
    {
        public int ReportId { get; set; }
        public string FirstNameReportingUser { get; set; }
        public string LastNameReportingUser { get; set; }
        public string FirstNameOwner { get; set; }
        public string LastNameOwner { get; set; }
        public int PicId { get; set; }
        public string Comment { get; set; }


    }

    public class ReportAddSM
    {
        public int PicId { get; set; }
        public string Comment { get; set; }
    }



}