using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TInsuranceCoverApprovals
    {
        public Guid Id { get; set; }
        public int Step { get; set; }
        public Guid SchemeManagerId { get; set; }
        public Guid AdminId { get; set; }
        public string Remarks { get; set; }
        public byte Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public Guid? InsuranceCoverId { get; set; }

        public virtual TInsuranceCovers InsuranceCover { get; set; }
    }
}
