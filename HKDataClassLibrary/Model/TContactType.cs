using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TContactType
    {
        public TContactType()
        {
            TContactUs = new HashSet<TContactUs>();
        }

        public long Pkid { get; set; }
        public string ContactType { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TContactUs> TContactUs { get; set; }
    }
}
