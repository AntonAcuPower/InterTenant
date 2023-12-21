using System;
using Push.Acumatica.Api.Purchasing;
using Push.Acumatica.Api.SalesOrder;

namespace Interco.Middle.Transfers.SalesOrderSync
{
    public class SOOrderSyncContext : IWorkerContext
    {
        // input parameters
        public string DestSalesOrderCustId { get; set; }
        public string PurchaseOrderNbr { get; set; }
        public string PurchaseOrderType { get; set; }
        public bool MustCreatePayment { get; set; }

        
        // output from PullPurchaseOrder()
        public PurchaseOrder PurchaseOrder { get; set; }

        // output from PullFulfillingInventory()
        public InventoryContext InventoryContext { get; set; }

        // output from PushSalesOrder
        public SalesOrder SalesOrder { get; set; }

        // computed from output
        public bool CreatedSalesOrder => SalesOrder != null;


        // Payment Context
        public SOPaymentSyncContext PaymentSyncContext { get; set; }


        // Exception Handling
        public bool ExceptionThrown => Exception != null;
        public Exception Exception { get; set; }
    }
}
