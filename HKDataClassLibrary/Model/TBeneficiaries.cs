using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TBeneficiaries
    {
        public TBeneficiaries()
        {
            TBeneficiaryInsuranceCoverTypes = new HashSet<TBeneficiaryInsuranceCoverTypes>();
            TInsuranceCovers = new HashSet<TInsuranceCovers>();
            TMedicalCoverProductLimits = new HashSet<TMedicalCoverProductLimits>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? MedicalCoverProductId { get; set; }
        public decimal? Amount { get; set; }
        public int ClusteredId { get; set; }
        public string CreatedBy { get; set; }
        public byte RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual TMedicalCoverProducts MedicalCoverProduct { get; set; }
        public virtual ICollection<TBeneficiaryInsuranceCoverTypes> TBeneficiaryInsuranceCoverTypes { get; set; }
        public virtual ICollection<TInsuranceCovers> TInsuranceCovers { get; set; }
        public virtual ICollection<TMedicalCoverProductLimits> TMedicalCoverProductLimits { get; set; }
    }
}
