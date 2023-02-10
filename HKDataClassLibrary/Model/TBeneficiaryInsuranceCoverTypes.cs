using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TBeneficiaryInsuranceCoverTypes
    {
        public Guid Id { get; set; }
        public Guid BeneficiaryId { get; set; }
        public decimal Amount { get; set; }
        public int ClusteredId { get; set; }
        public string CreatedBy { get; set; }
        public byte RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid InsuranceCoverTypeId { get; set; }

        public virtual TBeneficiaries Beneficiary { get; set; }
        public virtual TInsuranceCoverTypes InsuranceCoverType { get; set; }
    }
}
