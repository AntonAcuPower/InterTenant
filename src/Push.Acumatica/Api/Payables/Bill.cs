using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Payables
{    
    public class BillDetail
    {
        public string id { get; set; }
        //public int rowNumber { get; set; }
        public string note { get; set; }
        public StringValue InventoryID { get; set; }
        public StringValue Account { get; set; }
        public DoubleValue Amount { get; set; }
        public StringValue Branch { get; set; }
        public StringValue Description { get; set; }
        public DoubleValue ExtendedCost { get; set; }
        public BoolValue NonBillable { get; set; }
        public StringValue POOrderNbr { get; set; }
        public StringValue POOrderType { get; set; }
        public StringValue Project { get; set; }
        public object ProjectTask { get; set; }
        public DoubleValue Qty { get; set; }
        public StringValue Subaccount { get; set; }
        public StringValue TaxCategory { get; set; }
        public StringValue TransactionDescription { get; set; }
        public DoubleValue UnitCost { get; set; }
        public StringValue UOM { get; set; }
        public object custom { get; set; }
        public List<object> files { get; set; }
    }
    
    public class Bill
    {
        public string id { get; set; }
        //public int rowNumber { get; set; }
        public string note { get; set; }
        public DoubleValue Amount { get; set; }
        public StringValue ApprovedForPayment { get; set; }
        public DoubleValue Balance { get; set; }
        public StringValue BranchID { get; set; }
        public StringValue CashAccount { get; set; }
        public StringValue CurrencyID { get; set; }
        public DateValue Date { get; set; }
        public StringValue Description { get; set; }
        public List<BillDetail> Details { get; set; }
        public DateValue DueDate { get; set; }
        public BoolValue Hold { get; set; }
        public StringValue LocationID { get; set; }
        public StringValue PostPeriod { get; set; }
        public StringValue ReferenceNbr { get; set; }
        public StringValue Status { get; set; }
        public List<object> TaxDetails { get; set; }
        public DoubleValue TaxTotal { get; set; }
        public StringValue Terms { get; set; }
        public StringValue Type { get; set; }
        public StringValue Vendor { get; set; }
        public StringValue VendorRef { get; set; }
        public object custom { get; set; }
        public List<object> files { get; set; }
    }
}
