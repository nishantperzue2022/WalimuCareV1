using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TInsurances
    {
        public TInsurances()
        {
            TInsuranceCovers = new HashSet<TInsuranceCovers>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TInsuranceCovers> TInsuranceCovers { get; set; }
    }
}
