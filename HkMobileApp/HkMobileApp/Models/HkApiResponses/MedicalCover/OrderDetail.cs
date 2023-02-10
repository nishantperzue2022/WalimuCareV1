using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses.MedicalCover
{
    public class OrderDetail
    {
        public string id { get; set; }
        public string orderNumber { get; set; }
        public DateTime orderDate { get; set; }
        public string insuranceCoverId { get; set; }
        public string status { get; set; }
        public decimal amount { get; set; }
        public string statusDescription { get; set; }
        public string agentName { get; set; }
        public string agentId { get; set; }
        public string insuranceCoverName { get; set; }
        public string requestId { get; set; }
        public string receiptNumber { get; set; }
        public string medicalProductName { get; set; }
        public string userId { get; set; }
        public string principleDependants { get; set; }
    }

}
