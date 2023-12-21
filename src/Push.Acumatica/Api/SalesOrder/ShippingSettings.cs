using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.SalesOrder
{
    public class ShippingSettings
    {
        public BoolValue ShipSeparately { get; set; }
        public StringValue ShippingRule { get; set; }
    }
}
