using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Purchasing
{
    public class PurchaseReceiptDetail
    {
        public StringValue Account { get; set; }
        public StringValue AccrualAccount { get; set; }
        public StringValue AccrualSubaccount { get; set; }
        public StringValue Branch { get; set; }
        public DateValue ExpirationDate { get; set; }
        public DoubleValue ExtendedCost { get; set; }
        public StringValue InventoryID { get; set; }
        public IntegerValue LineNbr { get; set; }
        public StringValue LineType { get; set; }
        public StringValue Location { get; set; }
        public StringValue LotSerialNbr { get; set; }
        public DoubleValue OpenQty { get; set; }
        public DoubleValue OrderedQty { get; set; }
        public IntegerValue POLineNbr { get; set; }
        public StringValue POOrderNbr { get; set; }
        public StringValue POOrderType { get; set; }
        public StringValue Project { get; set; }
        public StringValue ProjectTask { get; set; }
        public DoubleValue ReceiptQty { get; set; }
        public StringValue Subaccount { get; set; }
        public StringValue Subitem { get; set; }
        public StringValue TransactionDescription { get; set; }
        public DoubleValue UnitCost { get; set; }
        public StringValue UOM { get; set; }
        public StringValue Warehouse { get; set; }
    }
}
