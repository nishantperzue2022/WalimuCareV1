using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TClinic
    {
        public TClinic()
        {
            TClinicEmail = new HashSet<TClinicEmail>();
            TClinicPhone = new HashSet<TClinicPhone>();
            TClinicService = new HashSet<TClinicService>();
            TClinicVisuals = new HashSet<TClinicVisuals>();
        }

        public long Pkid { get; set; }
        public string Name { get; set; }
        public long Region { get; set; }
        public long County { get; set; }
        public long SubCounty { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string WebsiteUrl { get; set; }
        public string OpeningHours { get; set; }
        public string Category { get; set; }

        public virtual TCounty CountyNavigation { get; set; }
        public virtual TRegion RegionNavigation { get; set; }
        public virtual TSubCounty SubCountyNavigation { get; set; }
        public virtual ICollection<TClinicEmail> TClinicEmail { get; set; }
        public virtual ICollection<TClinicPhone> TClinicPhone { get; set; }
        public virtual ICollection<TClinicService> TClinicService { get; set; }
        public virtual ICollection<TClinicVisuals> TClinicVisuals { get; set; }
    }
}
