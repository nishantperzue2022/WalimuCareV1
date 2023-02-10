using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TRegistration
    {
        public long Pkid { get; set; }
        public string MemberNo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public long? County { get; set; }
        public string NationalId { get; set; }
        public string SchemeId { get; set; }

    }
}
