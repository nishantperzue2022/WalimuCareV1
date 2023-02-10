using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TNominee
    {
        public long Pkid { get; set; }
        public long RegistrationId { get; set; }
        public string NomineeName { get; set; }
        public DateTime Dob { get; set; }
        public long RelationshipId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string NationalId { get; set; }
    }
}
