using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class Blog
    {
        public int pkid { get; set; }
        public string title { get; set; }

        public string TrimmedTitle { get; set; }
        public string description { get; set; }
        public string author { get; set; }
        public string subTitle { get; set; }
        public string tag { get; set; }
        public DateTime createDate { get; set; }
        public bool isActive { get; set; }
        public string imgUrl { get; set; }
        public string fileName { get; set; }

        public string ShareLink { get; set; }
    }

}
