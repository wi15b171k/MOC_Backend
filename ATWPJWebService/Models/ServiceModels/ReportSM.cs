using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Models.ServiceModels
{
    public class ReportSM
    {
        public int ReportId { get; set; }
        public int ReportingUserId { get; set; }
        public int PicId { get; set; }
        public string Comment { get; set; }


    }

    public class AddReportSM
    {
        public int PicId { get; set; }
        public string Comment { get; set; }
    }



}