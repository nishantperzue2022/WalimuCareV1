using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TLog
    {
        public long Pkid { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string RawUrl { get; set; }
        public string Time { get; set; }
        public string Browser { get; set; }
        public string Crawler { get; set; }
        public string BrowserType { get; set; }
        public string Version { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
    }
}
