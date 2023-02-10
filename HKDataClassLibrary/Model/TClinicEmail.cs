using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TClinicEmail
    {
        public long Pkid { get; set; }
        public long ClinicId { get; set; }
        public string EmailId { get; set; }

        public virtual TClinic Clinic { get; set; }
    }
}
