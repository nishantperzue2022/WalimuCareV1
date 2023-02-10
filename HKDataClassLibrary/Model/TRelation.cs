using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TRelation
    {
        public long Pkid { get; set; }
        public string Relation { get; set; }
        public bool IsActive { get; set; }
    }
}
