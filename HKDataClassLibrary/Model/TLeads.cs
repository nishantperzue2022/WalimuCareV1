using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TLeads
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public byte Status { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public string PostalAddress { get; set; }
        public string PostalCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? MedicalCoverProductId { get; set; }
        public Guid? InsuranceCoverTypeId { get; set; }
    }
}
