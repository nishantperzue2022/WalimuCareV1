using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TAspNetUserSchemes
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid SchemeId { get; set; }

        public virtual TInsuranceCovers Scheme { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
