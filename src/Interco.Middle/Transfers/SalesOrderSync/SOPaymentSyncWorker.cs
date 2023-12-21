using Push.Acumatica.Api;
using Push.Foundation.Utilities.Logging;

namespace Interco.Middle.Transfers.SalesOrderSync
{
    public class SOPaymentSyncWorker
    {
        private readonly ApiDepot _clientDepot;
        private readonly IPushLogger _logger;
        
        public SOPaymentSyncWorker(ApiDepot clientDepot, IPushLogger logger)
        {
            _clientDepot = clientDepot;
            _logger = logger;
        }

        public void PullSalesOrder(SOPaymentSyncContext syncContext)
        {
            _clientDepot.DestinationApiFactory.RunSession(factory =>
            {
                var salesOrderClient = factory.Make<SalesOrderApi>();
                syncContext.SalesOrder =
                    salesOrderClient.RetrieveSalesOrder(
                            syncContext.SalesOrderNbr, syncContext.SalesOrderType);
            });
        }
        
        public void PushPayment(SOPaymentSyncContext syncContext)
        {
            _clientDepot.DestinationApiFactory.RunSession(factory =>
                {
                    var paymentClient = factory.Make<PaymentApi>();
                    var paymentWrite
                        = new SOPaymentBuilder()
                            .InjectContext(syncContext)
                            .Result();

                    syncContext.Payment = paymentClient.WritePayment(paymentWrite);
                    _logger.Info($"Created Payment for Sales Order {syncContext.SalesOrder.OrderNbr}");
                });
        }

        
        // TODO - Push Order Taxes - do we need this..?
    }
}

