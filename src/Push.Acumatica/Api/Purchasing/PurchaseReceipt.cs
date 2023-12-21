using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Purchasing
{
    public class PurchaseReceipt
    {
        public DoubleValue ControlAmount { get; set; }
        public DoubleValue ControlQty { get; set; }
        public BoolValue CreateBill { get; set; }
        public StringValue CurrencyID { get; set; }
        public DateValue Date { get; set; }
        public BoolValue Hold { get; set; }
        public StringValue PostPeriod { get; set; }
        public StringValue ReceiptNbr { get; set; }
        public StringValue Status { get; set; }
        public DoubleValue TotalAmount { get; set; }
        public DoubleValue TotalQty { get; set; }
        public StringValue Type { get; set; }
        public StringValue VendorID { get; set; }
        public StringValue VendorRef { get; set; }
        public List<PurchaseReceiptDetail> Details { get; set; }
    }
}
