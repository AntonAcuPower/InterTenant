using System;
using Push.Acumatica.Api.Payment;
using Push.Acumatica.Api.SalesOrder;

namespace Interco.Middle.Transfers.SalesOrderSync
{
    public class SOPaymentSyncContext : IWorkerContext
    {
        // input parameters
        public string SalesOrderNbr { get; set; }
        public string SalesOrderType { get; set; }

        // output from PullSalesOrder
        public SalesOrder SalesOrder { get; set; }

        // output from PushSalesOrder
        public Payment Payment { get; set; }

        // computed from output
        public bool CreatedPayment => Payment != null;

        // exception handling
        public bool ExceptionThrown => Exception != null;
        public Exception Exception { get; set; }
    }
}
