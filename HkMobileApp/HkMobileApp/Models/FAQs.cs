using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class FAQs
    {
        public bool success { get; set; }
        public List<qa> data { get; set; }
        public string message { get; set; }
    }
}


public class qa
{
    public string question { get; set; }
    public string answer { get; set; }
}
