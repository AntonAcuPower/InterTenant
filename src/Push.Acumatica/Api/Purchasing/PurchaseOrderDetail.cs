using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Purchasing
{
    public class PurchaseOrderDetail
    {
        public string id { get; set; }
        public int rowNumber { get; set; }
        public string note { get; set; }
        public StringValue Account { get; set; }
        public StringValue AlternateID { get; set; }
        public StringValue BranchID { get; set; }
        public BoolValue Completed { get; set; }
        public DoubleValue CompleteOn { get; set; }
        public StringValue Description { get; set; }
        public DoubleValue ExtendedCost { get; set; }
        public StringValue InventoryID { get; set; }
        public StringValue LineDescription { get; set; }
        public IntegerValue LineNbr { get; set; }
        public StringValue LineType { get; set; }
        public DoubleValue MaxReceiptPercent { get; set; }
        public DoubleValue MinReceiptPercent { get; set; }
        public StringValue OrderNbr { get; set; }
        public DoubleValue OrderQty { get; set; }
        public StringValue OrderType { get; set; }
        public StringValue OrigPONbr { get; set; }
        public StringValue OrigPOType { get; set; }
        public DateValue Promised { get; set; }
        public DoubleValue QtyOnReceipts { get; set; }
        public StringValue ReceiptAction { get; set; }
        public DoubleValue ReceivedAmount { get; set; }
        public DateValue Requested { get; set; }
        public StringValue Subaccount { get; set; }
        public StringValue TaxCategory { get; set; }
        public DoubleValue UnitCost { get; set; }
        public StringValue UOM { get; set; }
        public StringValue WarehouseID { get; set; }
        public StringValue custom { get; set; }
        public List<object> files { get; set; }
    }
}
