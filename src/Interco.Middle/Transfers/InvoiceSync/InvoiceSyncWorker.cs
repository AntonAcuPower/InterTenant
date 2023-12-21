using Push.Acumatica.Api;
using Push.Foundation.Utilities.Logging;

namespace Interco.Middle.Transfers.InvoiceSync
{
    public class InvoiceSyncWorker
    {
        private readonly ApiDepot _apiDepot;
        private readonly IPushLogger _logger;

        public InvoiceSyncWorker(ApiDepot apiDepot, IPushLogger logger)
        {
            _apiDepot = apiDepot;
            _logger = logger;
        }
        
        public void PullInvoice(InvoiceContext syncContext)
        {
            _logger.Debug(Constants.LogMsg_PullInvoice);

            _apiDepot.SourceApiFactory.RunSession(factory =>
            {
                var receivablesApi = factory.Make<ReceivablesApi>();
                
                syncContext.Invoice
                    = receivablesApi
                        .RetrieveInvoice(syncContext.InvoiceReferenceNbr, syncContext.InvoiceDocType);
            });
        }

        public void PushBill(InvoiceContext syncContext)
        {
            _logger.Debug(Constants.LogMsg_PushBill);

            _apiDepot.DestinationApiFactory.RunSession(factory =>
            {
                var payablesApi = factory.Make<PayablesApi>();
                var bill
                    = new APBillBuilder()
                        .InjectContext(syncContext)
                        .Flush();

                syncContext.Bill = payablesApi.WritePayable(bill);

                _logger.Info(
                    string.Format(Constants.LogMsg_CreatedBill, syncContext.Bill.ReferenceNbr));

            });

        }
    }
}

