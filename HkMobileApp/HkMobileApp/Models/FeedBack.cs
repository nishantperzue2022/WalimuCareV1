using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class FeedBack
    {
        public Guid MemberId { get; set; }
        public int FeedBackType { get; set; }
        public int clinicId { get; set; }
        public string Remarks { get; set; }
    }
}
