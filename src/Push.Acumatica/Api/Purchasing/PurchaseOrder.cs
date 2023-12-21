using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Purchasing
{    
    public class PurchaseOrder
    {
        public string id { get; set; }
        public int rowNumber { get; set; }
        public string note { get; set; }
        public DoubleValue DoubleValue { get; set; }
        public StringValue CurrencyID { get; set; }
        public DateValue Date { get; set; }
        public StringValue Description { get; set; }
        public List<PurchaseOrderDetail> Details { get; set; }
        public BoolValue Hold { get; set; }
        public DoubleValue LineTotal { get; set; }
        public StringValue Location { get; set; }
        public StringValue OrderNbr { get; set; }
        public DoubleValue OrderTotal { get; set; }
        public StringValue Owner { get; set; }
        public DateValue PromisedOn { get; set; }
        public ShippingInstructions ShippingInstructions { get; set; }
        public StringValue Status { get; set; }
        public DoubleValue TaxTotal { get; set; }
        public StringValue Type { get; set; }
        public StringValue VendorID { get; set; }
        public StringValue VendorRef { get; set; }
        public StringValue custom { get; set; }
    }
}
