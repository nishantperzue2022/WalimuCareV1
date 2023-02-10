using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TSubCounty
    {
        public TSubCounty()
        {
            TClinic = new HashSet<TClinic>();
        }

        public long Pkid { get; set; }
        public long CountyId { get; set; }
        public string SubCounty { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TClinic> TClinic { get; set; }
    }
}
