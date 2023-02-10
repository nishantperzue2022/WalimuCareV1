using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class ByMedicalCoverCarousel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Description { get; set; }

    }
}
