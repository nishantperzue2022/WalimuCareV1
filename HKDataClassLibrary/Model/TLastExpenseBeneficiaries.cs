using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TLastExpenseBeneficiaries
    {
        public Guid Id { get; set; }
        public Guid? InsuranceCoverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public virtual TInsuranceCovers InsuranceCover { get; set; }
    }
}
