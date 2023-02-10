using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class CallBackrequests
    {
        public string MemberId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public byte? RequestStatus { get; set; }
        public string Reason { get; set; }
        public string cbrDate
        {
            get
            {
                return RequestDate.ToString("dd-MMM-yyyy");
            }
        }
    }
}
