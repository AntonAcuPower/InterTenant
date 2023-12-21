using System;
using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Receivables
{
    public class InvoiceDetail
    {
        public string id { get; set; }
        public int rowNumber { get; set; }
        public string note { get; set; }
        public StringValue Account { get; set; }
        public DoubleValue Amount { get; set; }
        public StringValue Branch { get; set; }
        public DoubleValue DiscountAmount { get; set; }
        public DoubleValue ExtendedPrice { get; set; }
        public StringValue InventoryID { get; set; }
        public DateValue LastModifiedDateTime { get; set; }
        public IntegerValue LineNbr { get; set; }
        public object ProjectTask { get; set; }
        public DoubleValue Qty { get; set; }
        public StringValue Subaccount { get; set; }
        public StringValue TransactionDescription { get; set; }
        public DoubleValue UnitPrice { get; set; }
        public StringValue UOM { get; set; }
        public object custom { get; set; }
        public List<object> files { get; set; }
    }
    
    public class Invoice
    {
        public string id { get; set; }
        public int rowNumber { get; set; }
        public string note { get; set; }
        public DoubleValue Amount { get; set; }
        public DoubleValue Balance { get; set; }
        public BoolValue BillingPrinted { get; set; }
        public DateValue CreatedDateTime { get; set; }
        public StringValue Customer { get; set; }
        public StringValue CustomerOrder { get; set; }
        public DateValue Date { get; set; }
        public StringValue Description { get; set; }
        public List<InvoiceDetail> Details { get; set; }
        public DateValue DueDate { get; set; }
        public BoolValue Hold { get; set; }
        public DateValue LastModifiedDateTime { get; set; }
        public StringValue LinkARAccount { get; set; }
        public StringValue LinkBranch { get; set; }
        public StringValue LocationID { get; set; }
        public StringValue PostPeriod { get; set; }
        public StringValue Project { get; set; }
        public StringValue ReferenceNbr { get; set; }
        public StringValue Status { get; set; }
        public List<object> TaxDetails { get; set; }
        public DoubleValue TaxTotal { get; set; }
        public StringValue Terms { get; set; }
        public StringValue Type { get; set; }
        public object custom { get; set; }
        public List<object> files { get; set; }
    }
}
