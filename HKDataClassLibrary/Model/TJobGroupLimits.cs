using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TJobGroupLimits
    {
        public long Pkid { get; set; }
        public long JobGroupId { get; set; }
        public string Inpatient { get; set; }
        public string Outpatient { get; set; }
        public string Dental { get; set; }
        public string Optical { get; set; }
        public string Maternity { get; set; }
    }
}
