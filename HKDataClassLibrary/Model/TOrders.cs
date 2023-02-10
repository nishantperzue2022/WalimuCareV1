using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TOrders
    {
        public TOrders()
        {
            TOrderPayments = new HashSet<TOrderPayments>();
        }

        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid InsuranceCoverId { get; set; }
        public byte Status { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public Guid? AgentId { get; set; }

        public virtual TAgents Agent { get; set; }
        public virtual TInsuranceCovers InsuranceCover { get; set; }
        public virtual ICollection<TOrderPayments> TOrderPayments { get; set; }
    }
}
