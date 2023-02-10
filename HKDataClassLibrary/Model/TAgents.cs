using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TAgents
    {
        public TAgents()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            TAgentCommissions = new HashSet<TAgentCommissions>();
            TInsuranceCovers = new HashSet<TInsuranceCovers>();
            TMembers = new HashSet<TMembers>();
            TOrders = new HashSet<TOrders>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AgentType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CertificateOfInCorporation { get; set; }
        public string Krapin { get; set; }
        public string MpesaAccountNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalCode { get; set; }
        public string NationalId { get; set; }
        public byte? Status { get; set; }
        public decimal? ComissionPercentage { get; set; }
        public string IdentificationCard { get; set; }
        public string KrapinCertificate { get; set; }
        public decimal? ParentAgentCommissionPercentage { get; set; }
        public Guid? ParentId { get; set; }
        public string Gender { get; set; }
        public string AgentNumber { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<TAgentCommissions> TAgentCommissions { get; set; }
        public virtual ICollection<TInsuranceCovers> TInsuranceCovers { get; set; }
        public virtual ICollection<TMembers> TMembers { get; set; }
        public virtual ICollection<TOrders> TOrders { get; set; }
    }
}
