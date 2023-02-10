using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TMedicalCoverProducts
    {
        public TMedicalCoverProducts()
        {
            TBeneficiaries = new HashSet<TBeneficiaries>();
            TMedicalCoverProductLimits = new HashSet<TMedicalCoverProductLimits>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TBeneficiaries> TBeneficiaries { get; set; }
        public virtual ICollection<TMedicalCoverProductLimits> TMedicalCoverProductLimits { get; set; }
    }
}
