using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class THospital
    {
        public long Pkid { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? Logistics { get; set; }
        public long? SortOrder { get; set; }
    }
}
