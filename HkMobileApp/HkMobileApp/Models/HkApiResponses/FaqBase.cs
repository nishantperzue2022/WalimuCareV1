using HkMobileApp.Models.HkApiBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses
{
    public class FaqBase
    {
        public string id { get; set; }
        public string category { get; set; }
        public List<FAQ> faqs { get; set; }
    }
}
