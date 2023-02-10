using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TClinicService
    {
        public long Pkid { get; set; }
        public long ClinicId { get; set; }
        public long ServiceId { get; set; }

        public virtual TClinic Clinic { get; set; }
        public virtual TService Service { get; set; }
    }
}
