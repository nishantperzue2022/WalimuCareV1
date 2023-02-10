using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses
{
    public class PolicyWriteUpResponse
    {
        public string BenefitName { get; set; }
        public string Title { get; set; }
        public List<string> Details { get; set; }
        public bool IsVisible { get; set; }
    }


    //public class Rootobject
    //{
    //    public Class1[] Property1 { get; set; }
    //}

    //public class Class1
    //{
    //    public string benefitName { get; set; }
    //    public string[] details { get; set; }
    //}

}
