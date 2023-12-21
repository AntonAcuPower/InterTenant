using Autofac;
using Interco.Middle.Transfers;
using Interco.Middle.Transfers.InvoiceSync;
using Interco.Middle.Transfers.Misc;
using Interco.Middle.Transfers.PurchaseReceiptSync;
using Interco.Middle.Transfers.SalesOrderSync;
using Interco.Middle.Utility;
using Push.Acumatica;
using Push.Foundation.Utilities.Logging;


namespace Interco.Middle
{
    public class MiddleAutofac
    {
        public static string LoggerName = "Interco.System";

        public static ContainerBuilder Build(ContainerBuilder builder)
        {
            // Register external dependencies
            AcumaticaHttpAutofac.Build(builder);            
            
            // Logging registrations
            builder.RegisterType<DefaultFormatter>()
                .As<ILogFormatter>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConsoleAndDebugLogger>()
                .As<IPushLogger>()
                .InstancePerLifetimeScope();

            // Process Registrations
            RegisterInventoryTransfer(builder);     

            // Misc
            builder.RegisterType<TimeZoneTranslator>().InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder RegisterNLogger(ContainerBuilder builder)
        {
            // By default NLogger is used
            builder.Register(x => new NLogger(LoggerName, x.Resolve<ILogFormatter>()))
                .As<IPushLogger>()
                .InstancePerLifetimeScope();

            return builder;
        }

        private static void RegisterInventoryTransfer(ContainerBuilder builder)
        {
            // Processes -> Transfer
            builder.RegisterType<CustomerSync>().InstancePerLifetimeScope();
            builder.RegisterType<SOOrderSyncWorker>().InstancePerLifetimeScope();
            builder.RegisterType<SOPaymentSyncWorker>().InstancePerLifetimeScope();
            builder.RegisterType<PurchaseReceiptSyncWorker>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceSyncWorker>().InstancePerLifetimeScope();

            builder.RegisterType<ApiDepot>().InstancePerLifetimeScope();

            // Utilities
            builder.RegisterType<AcumaticaTimeZoneService>().InstancePerLifetimeScope();
        }        
    }
}

