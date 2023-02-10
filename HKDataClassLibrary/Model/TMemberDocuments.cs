using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TMemberDocuments
    {
        public Guid Id { get; set; }
        public Guid? MemberId { get; set; }
        public string FileAttachment { get; set; }

        public virtual TMembers Member { get; set; }
    }
}
