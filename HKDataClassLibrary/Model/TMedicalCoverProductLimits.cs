using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TMedicalCoverProductLimits
    {
        public Guid Id { get; set; }
        public Guid? MedicalCoverProductId { get; set; }
        public Guid? InsuranceCoverTypeId { get; set; }
        public Guid? BeneficiaryId { get; set; }
        public Guid? BenefitId { get; set; }
        public decimal Amount { get; set; }

        public virtual TBeneficiaries Beneficiary { get; set; }
        public virtual TBenefits Benefit { get; set; }
        public virtual TInsuranceCoverTypes InsuranceCoverType { get; set; }
        public virtual TMedicalCoverProducts MedicalCoverProduct { get; set; }
    }
}
