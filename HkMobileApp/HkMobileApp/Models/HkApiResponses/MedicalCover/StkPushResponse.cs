using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses.MedicalCover
{
 
    public class StkPushResponse
    {
        public string merchantRequestID { get; set; }
        public string checkoutRequestID { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public string customerMessage { get; set; }
    }

}
