using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TSurveyDetail
    {
        public long Pkid { get; set; }
        public long SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }

        public virtual TSurvey Survey { get; set; }
    }
}
