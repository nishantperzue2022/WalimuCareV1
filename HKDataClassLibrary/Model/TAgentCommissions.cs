using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TAgentCommissions
    {
        public Guid Id { get; set; }
        public Guid AgentId { get; set; }
        public Guid InsuranceCoverId { get; set; }
        public decimal AgentCommission { get; set; }
        public decimal ParentCommission { get; set; }
        public DateTime CommissionDate { get; set; }
        public byte ComissionStatus { get; set; }

        public virtual TAgents Agent { get; set; }
        public virtual TInsuranceCovers InsuranceCover { get; set; }
    }
}
