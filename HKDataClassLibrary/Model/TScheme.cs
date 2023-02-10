using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TScheme
    {
        public TScheme()
        {
            TInsuranceCovers = new HashSet<TInsuranceCovers>();
        }

        public long Pkid { get; set; }
        public long? SchemeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? MaxSpouse { get; set; }
        public long? MaxChildren { get; set; }
        public long? MaxDisabledChildren { get; set; }

        public virtual ICollection<TInsuranceCovers> TInsuranceCovers { get; set; }
    }
}
