using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TApplicationUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }

        public virtual TApplicationUsers User { get; set; }
    }
}
