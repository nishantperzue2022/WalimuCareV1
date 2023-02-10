using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TBenefits
    {
        public TBenefits()
        {
            TInsuranceCoverTypeBenefits = new HashSet<TInsuranceCoverTypeBenefits>();
            TMedicalCoverProductLimits = new HashSet<TMedicalCoverProductLimits>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClusteredId { get; set; }
        public string CreatedBy { get; set; }
        public byte RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TInsuranceCoverTypeBenefits> TInsuranceCoverTypeBenefits { get; set; }
        public virtual ICollection<TMedicalCoverProductLimits> TMedicalCoverProductLimits { get; set; }
    }
}
