using Push.Acumatica.Api;
using Push.Foundation.Utilities.Logging;

namespace Interco.Middle.Transfers.PurchaseReceiptSync
{
    public class PurchaseReceiptSyncWorker
    {
        private readonly ApiDepot _clientDepot;
        private readonly IPushLogger _logger;


        public PurchaseReceiptSyncWorker(ApiDepot clientDepot, IPushLogger logger)
        {
            _clientDepot = clientDepot;
            _logger = logger;
        }
        
        // This call should be isolated to the ProvideCo
        public void PullShipmentAndSalesOrder(PurchaseReceiptSyncContext context)
        {
            _clientDepot.SourceApiFactory.RunSession(factory =>
            {
                var shipmentApi = factory.Make<ShipmentApi>();
                var salesOrderApi = factory.Make<SalesOrderApi>();

                context.Shipment = shipmentApi.RetrieveShipment(context.FulfillingShipmentNbr);
                context.SalesOrder
                    = salesOrderApi.RetrieveSalesOrder(
                            context.ShipmentSOOrderNbr, context.ShipmentSOOrderType);
            });
        }
        
        // This call should be isolated to the ObtainCo
        public void PullPurchaseOrder(PurchaseReceiptSyncContext context)
        {
            _clientDepot.DestinationApiFactory.RunSession(factory =>
            {
                var _purchasesClient = factory.Make<PurchasesApi>();
                context.PurchaseOrder = 
                    _purchasesClient.RetrievePurchaseOrder(
                        context.SOPurchaseOrderNbr, context.SOPurchaseOrderType);
            });
        }

        // This call should be isolated to the ObtainCo
        public void PushPurchaseReceipt(PurchaseReceiptSyncContext context)
        {
            _clientDepot.DestinationApiFactory.RunSession(factory =>
            {
                var purchaseReceipt = new PurchaseReceiptBuilder()
                    .InjectContext(context)
                    .Result();

                var _purchasesClient = factory.Make<PurchasesApi>();
                context.PurchaseReceipt = _purchasesClient.WritePurchaseReceipt(purchaseReceipt);

                _logger.Info($"Created Purchase Receipt {context.PurchaseReceipt.ReceiptNbr.value}");
            });
        }
    }
}

