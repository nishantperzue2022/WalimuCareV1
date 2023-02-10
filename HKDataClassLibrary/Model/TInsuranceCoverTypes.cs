using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TInsuranceCoverTypes
    {
        public TInsuranceCoverTypes()
        {
            TBeneficiaryInsuranceCoverTypes = new HashSet<TBeneficiaryInsuranceCoverTypes>();
            TInsuranceCoverTypeBenefits = new HashSet<TInsuranceCoverTypeBenefits>();
            TInsuranceCovers = new HashSet<TInsuranceCovers>();
            TMedicalCoverProductLimits = new HashSet<TMedicalCoverProductLimits>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }
        public int ClusteredId { get; set; }
        public string CreatedBy { get; set; }
        public byte RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TBeneficiaryInsuranceCoverTypes> TBeneficiaryInsuranceCoverTypes { get; set; }
        public virtual ICollection<TInsuranceCoverTypeBenefits> TInsuranceCoverTypeBenefits { get; set; }
        public virtual ICollection<TInsuranceCovers> TInsuranceCovers { get; set; }
        public virtual ICollection<TMedicalCoverProductLimits> TMedicalCoverProductLimits { get; set; }
    }
}
