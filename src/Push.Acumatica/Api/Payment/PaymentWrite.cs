﻿using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Payment
{
    public class PaymentWrite
    {
        public StringValue ReferenceNbr { get; set; }
        public BoolValue Hold { get; set; }
        public StringValue Type { get; set; }
        public StringValue CustomerID { get; set; }
        public StringValue PaymentMethod { get; set; }
        public StringValue CashAccount { get; set; }
        public StringValue PaymentRef { get; set; }
        public StringValue Description { get; set; }
        public DoubleValue PaymentAmount { get; set; }
        public DoubleValue AppliedToDocuments { get; set; }

        public List<PaymentOrdersRef> OrdersToApply { get; set; }
        public List<PaymentDocumentsToApply> DocumentsToApply { get; set; }
    }
}

