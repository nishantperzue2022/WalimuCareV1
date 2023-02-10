using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TStatus
    {
        public TStatus()
        {
            TContactUs = new HashSet<TContactUs>();
        }

        public long Pkid { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<TContactUs> TContactUs { get; set; }
    }
}
