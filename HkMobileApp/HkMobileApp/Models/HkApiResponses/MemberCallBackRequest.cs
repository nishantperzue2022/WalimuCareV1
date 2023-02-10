using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses
{
    public class MemberCallBackRequest
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public DateTime RequestDate { get; set; }
        public byte RequestStatus { get; set; }
        public string PhoneNumber { get; set; }
    }
}
