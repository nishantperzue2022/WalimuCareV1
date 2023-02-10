using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TRegion
    {
        public TRegion()
        {
            TClinic = new HashSet<TClinic>();
        }

        public long Pkid { get; set; }
        public string Region { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TClinic> TClinic { get; set; }
    }
}
