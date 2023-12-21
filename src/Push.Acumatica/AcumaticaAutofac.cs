using Autofac;
using Push.Acumatica.Api;
using Push.Acumatica.Config;
using Push.Acumatica.Http;

namespace Push.Acumatica
{
    public class AcumaticaHttpAutofac
    {
        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterType<AcumaticaCredentialsConfig>();
            builder.Register(x => AcumaticaHttpConfig.Settings);

            builder.RegisterType<AcumaticaHttpContext>().InstancePerDependency();
            builder.RegisterType<ApiFactory>().InstancePerDependency();
            builder.RegisterType<CustomerApi>().InstancePerDependency();
            builder.RegisterType<DistributionApi>().InstancePerDependency();
            builder.RegisterType<SalesOrderApi>().InstancePerDependency();
            builder.RegisterType<ShipmentApi>().InstancePerDependency();
            builder.RegisterType<PaymentApi>().InstancePerDependency();
            builder.RegisterType<ReferenceApi>().InstancePerDependency();
            builder.RegisterType<PurchasesApi>().InstancePerDependency();
            builder.RegisterType<ReceivablesApi>().InstancePerDependency();
            builder.RegisterType<PayablesApi>().InstancePerDependency();
        }
    }
}

