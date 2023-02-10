using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TSurvey
    {
        public TSurvey()
        {
            TSurveyDetail = new HashSet<TSurveyDetail>();
        }

        public long Pkid { get; set; }
        public string Result { get; set; }
        public DateTime CreateDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TSurveyDetail> TSurveyDetail { get; set; }
    }
}
