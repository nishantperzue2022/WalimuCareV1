using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TService
    {
        public TService()
        {
            TClinicService = new HashSet<TClinicService>();
        }

        public long Pkid { get; set; }
        public string Service { get; set; }

        public virtual ICollection<TClinicService> TClinicService { get; set; }
    }
}
