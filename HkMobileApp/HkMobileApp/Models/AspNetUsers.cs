using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class AspNetUsers
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? dateCreated { get; set; }
        public DateTime? dateUpdated { get; set; }
        public object lastLoginDate { get; set; }
        public DateTime? passwordChangeDate { get; set; }
        public string email { get; set; }
        public bool emailConfirmed { get; set; }
        public string passwordHash { get; set; }
        public string pinHash { get; set; }
        public string securityStamp { get; set; }
        public string phoneNumber { get; set; }
        public bool phoneNumberConfirmed { get; set; }
        public bool twoFactorEnabled { get; set; }
        public object lockoutEndDateUtc { get; set; }
        public bool lockoutEnabled { get; set; }
        public int accessFailedCount { get; set; }
        public string userName { get; set; }
        public object supervisorId { get; set; }
        public object agentId { get; set; }
        public string memberId { get; set; }
        public object agent { get; set; }
        public string schemeId { get; set; }
    }
}
