using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TMembers
    {
        public TMembers()
        {
            InverseParent = new HashSet<TMembers>();
            TMemberDocuments = new HashSet<TMemberDocuments>();
            TMemberInsuranceCovers = new HashSet<TMemberInsuranceCovers>();
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MemberNumber { get; set; }
        public string AddressPhysical { get; set; }
        public string AddressPostal { get; set; }
        public string AddressStreet { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCity { get; set; }
        public string AddressEmail { get; set; }
        public string AddressLandLine { get; set; }
        public int ClusteredId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string AddressMobileNumber { get; set; }
        public byte RecordStatus { get; set; }
        public bool IsPrincipalDependant { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte Status { get; set; }
        public string Remarks { get; set; }
        public string IdNumber { get; set; }
        public string Gender { get; set; }
        public bool IsLastExpenseBeneficiary { get; set; }
        public bool IsGroupLifeBeneficiary { get; set; }
        public string SuspensionReasons { get; set; }
        public Guid? AgentId { get; set; }
        public string Relationship { get; set; }

        public virtual TAgents Agent { get; set; }
        public virtual TMembers Parent { get; set; }
        public virtual ICollection<TMembers> InverseParent { get; set; }
        public virtual ICollection<TMemberDocuments> TMemberDocuments { get; set; }
        public virtual ICollection<TMemberInsuranceCovers> TMemberInsuranceCovers { get; set; }
    }
}
