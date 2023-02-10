using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TContactUs
    {
        public long Pkid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long InquiryType { get; set; }
        public long ContactType { get; set; }
        public string Message { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Remark { get; set; }
        public long? Status { get; set; }

        public virtual TContactType ContactTypeNavigation { get; set; }
        public virtual TInquiry InquiryTypeNavigation { get; set; }
        public virtual TStatus StatusNavigation { get; set; }
    }
}
