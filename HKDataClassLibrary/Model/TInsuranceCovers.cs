using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TInsuranceCovers
    {
        public TInsuranceCovers()
        {
            TAgentCommissions = new HashSet<TAgentCommissions>();
            TAspNetUserSchemes = new HashSet<TAspNetUserSchemes>();
            TGroupLifeBeneficiaries = new HashSet<TGroupLifeBeneficiaries>();
            TInsuranceCoverApprovals = new HashSet<TInsuranceCoverApprovals>();
            TLastExpenseBeneficiaries = new HashSet<TLastExpenseBeneficiaries>();
            TMemberInsuranceCovers = new HashSet<TMemberInsuranceCovers>();
            TNextOfKins = new HashSet<TNextOfKins>();
            TOrders = new HashSet<TOrders>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public Guid InsuranceId { get; set; }
        public Guid? InsuranceCoverTypeId { get; set; }
        public Guid BeneficiaryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte Status { get; set; }
        public decimal? Amount { get; set; }
        public long? SchemeId { get; set; }
        public string InsurancePolicyDocument { get; set; }
        public byte ApprovalStep { get; set; }
        public string Authorizer { get; set; }
        public string PolicyNumber { get; set; }
        public string InsuranceCompany { get; set; }
        public Guid? AgentId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual TAgents Agent { get; set; }
        public virtual TBeneficiaries Beneficiary { get; set; }
        public virtual TInsurances Insurance { get; set; }
        public virtual TInsuranceCoverTypes InsuranceCoverType { get; set; }
        public virtual TScheme Scheme { get; set; }
        public virtual ICollection<TAgentCommissions> TAgentCommissions { get; set; }
        public virtual ICollection<TAspNetUserSchemes> TAspNetUserSchemes { get; set; }
        public virtual ICollection<TGroupLifeBeneficiaries> TGroupLifeBeneficiaries { get; set; }
        public virtual ICollection<TInsuranceCoverApprovals> TInsuranceCoverApprovals { get; set; }
        public virtual ICollection<TLastExpenseBeneficiaries> TLastExpenseBeneficiaries { get; set; }
        public virtual ICollection<TMemberInsuranceCovers> TMemberInsuranceCovers { get; set; }
        public virtual ICollection<TNextOfKins> TNextOfKins { get; set; }
        public virtual ICollection<TOrders> TOrders { get; set; }
    }
}
