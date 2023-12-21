using System.Collections.Generic;
using System.Linq;
using Push.Acumatica.Api.Common;
using Push.Acumatica.Api.Common.Monster.Middle.Processes.Acumatica.Persist;
using Push.Acumatica.Api.Purchasing;


namespace Interco.Middle.Transfers.PurchaseReceiptSync
{
    public class PurchaseReceiptBuilder
    {
        private readonly PurchaseReceipt _purchaseReceipt;

        public PurchaseReceiptBuilder()
        {
            _purchaseReceipt = new PurchaseReceipt();
            _purchaseReceipt.Hold = true.ToValue();
            _purchaseReceipt.Type = PurchaseReceiptType.Receipt.ToValue();
        }
        
        public PurchaseReceiptBuilder InjectContext(PurchaseReceiptSyncContext context)
        {
            // Heuristic to locate the Sales Order number
            var salesOrderNbr = context.Shipment.Details.First().OrderNbr.value;
            var trackingNbr = context.Shipment.Packages.First().TrackingNbr.value;

            _purchaseReceipt.VendorRef =
                    $"Sales Order #{salesOrderNbr} - {trackingNbr}".ToValue();

            _purchaseReceipt.VendorID = context.PurchaseOrder.VendorID.Copy();
            _purchaseReceipt.Details = new List<PurchaseReceiptDetail>();

            var lineNumber = 1;

            foreach (var line in context.Shipment.Details)
            {
                var detail = new PurchaseReceiptDetail();
                detail.InventoryID = line.InventoryID.Copy();
                detail.POOrderNbr = context.PurchaseOrder.OrderNbr.Copy();
                detail.POOrderType = context.PurchaseOrder.Type.Copy();
                detail.ReceiptQty = line.ShippedQty.Copy();

                // From the StackOverflow example. Really....?
                detail.POLineNbr = lineNumber.ToValue();

                _purchaseReceipt.Details.Add(detail);
            }

            return this;
        }
        
        public PurchaseReceipt Result()
        {
            return _purchaseReceipt;
        }

    }
}
