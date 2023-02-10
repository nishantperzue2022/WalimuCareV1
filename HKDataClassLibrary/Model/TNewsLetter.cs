using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TNewsLetter
    {
        public long Pkid { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
