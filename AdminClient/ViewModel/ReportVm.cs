using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.ViewModel
{
    public class ReportVm
    {
        public int Id { get; set; }
        public string ReportedBy { get; set; }
        public string PictureOwner { get; set; }
        public int PictureId { get; set; }
        public string Comment { get; set; }
    }
}
