using System;
using Autofac;
using Interco.Middle.Transfers.InvoiceSync;
using Interco.Middle.Transfers.PurchaseReceiptSync;
using Interco.Middle.Transfers.SalesOrderSync;
using Push.Acumatica.Config;
using Push.Acumatica.Http;
using Push.Foundation.Utilities.Logging;


namespace Interco.Middle.Transfers
{
    public class TransferManager
    {
        private readonly ContainerBuilder _containerBuilder;
        private IContainer _container;

        public AcumaticaCredentials SourceCredentials { get; set; }
        public AcumaticaCredentials DestinationCredentials { get; set; }
        public AcumaticaHttpConfig SourceSettings { get; set; }
        public AcumaticaHttpConfig DestinationSettings { get; set; }

        public TransferManager()
        {
            _containerBuilder = new ContainerBuilder();
            MiddleAutofac.Build(_containerBuilder);
            SourceSettings = new AcumaticaHttpConfig();
            DestinationSettings = new AcumaticaHttpConfig();
        }

        public void Initialize()
        {
            _container = _containerBuilder.Build();
        }

        public void EnableNLogger()
        {
            if (_container != null)
            {
                throw new Exception("Can only call EnableNLogger before Initialize is invoked");
            }

            MiddleAutofac.RegisterNLogger(_containerBuilder);
        }

        public void InjectLogger<T>() where T : IPushLogger
        {
            _containerBuilder.RegisterType<T>().As<IPushLogger>();
        }


        public void POOrderToSOOrder(SOOrderSyncContext soContext)
        {
            RunWorker<SOOrderSyncWorker>(
                soContext, worker =>
                {
                    worker.PullPurchaseOrder(soContext);
                    worker.PullFulfillingInventory(soContext);
                    worker.PushSalesOrder(soContext);
                    worker.UpdatePOVendorRef(soContext);
                });

            if (soContext.MustCreatePayment == true)
            {
                soContext.PaymentSyncContext = new SOPaymentSyncContext()
                {
                    SalesOrderNbr = soContext.SalesOrder.OrderNbr.value,
                    SalesOrderType = soContext.SalesOrder.OrderType.value,
                };
                SOOrderToSOPayment(soContext.PaymentSyncContext);
            }
        }

        public void SOOrderToSOPayment(SOPaymentSyncContext context)
        {
            RunWorker<SOPaymentSyncWorker>(
                context, worker =>
                {
                    worker.PullSalesOrder(context);
                    worker.PushPayment(context);
                });
        }
        
        public void SOShipmentToPOReceipt(PurchaseReceiptSyncContext context)
        {
            RunWorker<PurchaseReceiptSyncWorker>(
                context, worker =>
                {
                    worker.PullShipmentAndSalesOrder(context);
                    worker.PullPurchaseOrder(context);
                    worker.PushPurchaseReceipt(context);
                });
        }

        public void ARInvoiceToAPBill(InvoiceContext context)
        {
            RunWorker<InvoiceSyncWorker>(
                context, worker =>
                {
                    worker.PullInvoice(context);
                    worker.PushBill(context);
                });
        }


        private void RunWorker<T>(IWorkerContext context, Action<T> action)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var apiDepot = scope.Resolve<ApiDepot>();
                apiDepot.SourceApiFactory.Initialize(SourceCredentials, SourceSettings);
                apiDepot.DestinationApiFactory.Initialize(DestinationCredentials, DestinationSettings);

                var logger = scope.Resolve<IPushLogger>();

                try
                {
                    var worker = scope.Resolve<T>();
                    action(worker);
                }
                catch (Exception ex)
                {
                    context.Exception = ex;
                    logger.Error(ex);
                }
            }
        }        
    }
}

