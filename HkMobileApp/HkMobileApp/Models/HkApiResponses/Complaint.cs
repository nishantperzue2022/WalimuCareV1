using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses
{
    public class Complaint
    {
        public string id { get; set; }
        public string topic { get; set; }

        public override string ToString()
        {
            return topic;
        }
    }

}
