using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TClinicPhone
    {
        public long Pkid { get; set; }
        public long ClinicId { get; set; }
        public string Phone { get; set; }

        public virtual TClinic Clinic { get; set; }
    }
}
