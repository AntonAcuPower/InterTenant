﻿using System;
using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Payment
{
    
    public class OrdersToApply
    {
        public string id { get; set; }
        public int rowNumber { get; set; }
        public StringValue note { get; set; }
        public DoubleValue AppliedToOrder { get; set; }
        public StringValue OrderNbr { get; set; }
        public StringValue OrderType { get; set; }
        public StringValue custom { get; set; }
        public List<object> files { get; set; }
    }
    
    public class Payment
    { 
       public string id { get; set; }
        public int rowNumber { get; set; }
        public string note { get; set; }
        public DateValue ApplicationDate { get; set; }
        public DoubleValue AppliedToDocuments { get; set; }
        public StringValue CardAccountNbr { get; set; }
        public StringValue CashAccount { get; set; }
        public StringValue CurrencyID { get; set; }
        public StringValue CustomerID { get; set; }
        public StringValue Description { get; set; }
        public BoolValue Hold { get; set; }
        public List<OrdersToApply> OrdersToApply { get; set; }
        public DoubleValue PaymentAmount { get; set; }
        public StringValue PaymentMethod { get; set; }
        public StringValue PaymentRef { get; set; }
        public StringValue ReferenceNbr { get; set; }
        public StringValue Status { get; set; }
        public StringValue Type { get; set; }
        public StringValue custom { get; set; }
        public List<object> files { get; set; }
    }
}
