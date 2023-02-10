using System;
using System.Collections.Generic;

namespace HKDataClassLibrary.Model
{
    public partial class TOrderPayments
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public byte PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionReference { get; set; }

        public virtual TOrders Order { get; set; }
    }
}
