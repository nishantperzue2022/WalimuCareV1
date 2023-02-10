using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TPromotion
    {
        public long Pkid { get; set; }
        public string Id { get; set; }
        public string MemberId { get; set; }
        public string UserId { get; set; }
        public string PromotionCode { get; set; }
        public string ClinicId { get; set; }
        public DateTime? Date { get; set; }
        public string MemberNo { get; set; }
        public bool? IsOptedHomeDeliver { get; set; }
        public string MpesaPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string EmailAddress { get; set; }
        public string InvoiceNo { get; set; }
    }
}
