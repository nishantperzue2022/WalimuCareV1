using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TCounty
    {
        public TCounty()
        {
            TClinic = new HashSet<TClinic>();
        }

        public long Pkid { get; set; }
        public long? Code { get; set; }
        public string County { get; set; }
        public string FormerProvince { get; set; }
        public long? RegionId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TClinic> TClinic { get; set; }
    }
}
