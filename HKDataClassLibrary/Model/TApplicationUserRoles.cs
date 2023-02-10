using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TApplicationUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual TApplicationRoles Role { get; set; }
        public virtual TApplicationUsers User { get; set; }
    }
}
