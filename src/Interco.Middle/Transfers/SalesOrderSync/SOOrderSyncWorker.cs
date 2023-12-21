using Push.Acumatica.Api;
using Push.Acumatica.Api.Common;
using Push.Acumatica.Api.Purchasing;
using Push.Foundation.Utilities.Json;
using Push.Foundation.Utilities.Logging;

namespace Interco.Middle.Transfers.SalesOrderSync
{
    public class SOOrderSyncWorker
    {
        private readonly ApiDepot _clientDepot;
        private readonly IPushLogger _logger;

        public SOOrderSyncWorker(ApiDepot clientDepot, IPushLogger logger)
        {
            _clientDepot = clientDepot;
            _logger = logger;
        }


        public void PullPurchaseOrder(SOOrderSyncContext syncContext)
        {
            _clientDepot.SourceApiFactory.RunSession(factory =>
            {
                var purchasesClient = factory.Make<PurchasesApi>();
                var purchaseOrderNbr = syncContext.PurchaseOrderNbr;
                var purchaseOrderType = syncContext.PurchaseOrderType;

                syncContext.PurchaseOrder
                    = purchasesClient.RetrievePurchaseOrder(purchaseOrderNbr, purchaseOrderType);
            });
        }

        public void PullFulfillingInventory(SOOrderSyncContext syncContext)
        {
            _clientDepot.DestinationApiFactory.RunSession(factory =>
            {
                syncContext.InventoryContext = new InventoryContext();
                var distributionApi = factory.Make<DistributionApi>();

                foreach (var line in syncContext.PurchaseOrder.Details)
                {
                    var item = distributionApi.RetrieveStockItem(line.InventoryID.value);
                    syncContext.InventoryContext.Set(item);
                }
            });
        }

        public void PushSalesOrder(SOOrderSyncContext syncContext)
        {
            _clientDepot.DestinationApiFactory.RunSession(factory =>
            {
                var salesOrderApi = factory.Make<SalesOrderApi>();

                var salesOrder = new SalesOrderBuilder()
                    .InjectSyncContext(syncContext)
                    .Result();

                syncContext.SalesOrder = salesOrderApi.WriteSalesOrder(JsonExtensions.SerializeToJson(salesOrder));
                _logger.Info(
                    $"Created Sales Order {syncContext.SalesOrder.OrderNbr} from " +
                    $"Purchase Order {syncContext.PurchaseOrder.OrderNbr}");
            });
        }

        public void UpdatePOVendorRef(SOOrderSyncContext syncContext)
        {
            _clientDepot.SourceApiFactory.RunSession(factory =>
            {
                var purchasingApi = factory.Make<PurchasesApi>();
                var update = new POVenderRefUpdate
                {
                    OrderNbr = syncContext.PurchaseOrderNbr.ToValue(),
                    VendorRef = syncContext.SalesOrder.OrderNbr.Copy(),
                };

                purchasingApi.WritePurchaseOrder(update.SerializeToJson());
                _logger.Info(
                    $"Updated Purchase Order {syncContext.PurchaseOrder.OrderNbr} " + 
                    $"with Sales Order {syncContext.SalesOrder.OrderNbr} as Vendor Ref");

            });
        }

        // TODO - Push Order Taxes - do we need this..?
    }
}

