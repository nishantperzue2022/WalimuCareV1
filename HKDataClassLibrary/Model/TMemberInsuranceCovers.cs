using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TMemberInsuranceCovers
    {
        public Guid Id { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? InsuranceCoverId { get; set; }

        public virtual TInsuranceCovers InsuranceCover { get; set; }
        public virtual TMembers Member { get; set; }
    }
}
