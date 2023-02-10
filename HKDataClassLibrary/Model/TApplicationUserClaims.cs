using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TApplicationUserClaims
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual TApplicationUsers User { get; set; }
    }
}
