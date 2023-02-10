using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TClinicVisuals
    {
        public long Pkid { get; set; }
        public long ClinicId { get; set; }
        public string ImageUri { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        public virtual TClinic Clinic { get; set; }
    }
}
