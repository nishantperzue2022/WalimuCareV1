using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TBlog
    {
        public long Pkid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string SubTitle { get; set; }
        public string Tag { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsActive { get; set; }
        public string Uri { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
    }
}
