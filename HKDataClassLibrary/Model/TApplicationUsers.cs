using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TApplicationUsers
    {
        public TApplicationUsers()
        {
            TApplicationUserClaims = new HashSet<TApplicationUserClaims>();
            TApplicationUserLogins = new HashSet<TApplicationUserLogins>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? PasswordChangeDate { get; set; }

        public virtual ICollection<TApplicationUserClaims> TApplicationUserClaims { get; set; }
        public virtual ICollection<TApplicationUserLogins> TApplicationUserLogins { get; set; }
    }
}
