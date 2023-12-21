using System;
using System.Linq;
using Push.Acumatica.Api.Common.Monster.Middle.Processes.Acumatica.Persist;
using Push.Acumatica.Api.Purchasing;
using Push.Acumatica.Api.SalesOrder;
using Push.Acumatica.Api.Shipment;

namespace Interco.Middle.Transfers.PurchaseReceiptSync
{
    public class PurchaseReceiptSyncContext : IWorkerContext
    {
        public string FulfillingShipmentNbr { get; set; }

        // output from PullShipmentAndSalesOrder()
        public Shipment Shipment { get; set; }
        public SalesOrder SalesOrder { get; set; }

        // computed
        public string ShipmentSOOrderNbr => Shipment.Details.First().OrderNbr.value;
        public string ShipmentSOOrderType => Shipment.Details.First().OrderType.value;

        public string SOPurchaseOrderNbr => SalesOrder.ExternalRef.value;
        
        // TODO - Remove this hard-coding
        public string SOPurchaseOrderType => PurchaseOrderType.Normal;


        // output from PullPurchaseOrder()
        public PurchaseOrder PurchaseOrder { get; set; }

        // output from PushPurchaseReceipt()
        public PurchaseReceipt PurchaseReceipt { get; set; }

        // computed from output
        public bool CreatedPurchaseReceipt => PurchaseReceipt != null;


        // exception handling
        public bool ExceptionThrown => Exception != null;
        public Exception Exception { get; set; }
    }
}

