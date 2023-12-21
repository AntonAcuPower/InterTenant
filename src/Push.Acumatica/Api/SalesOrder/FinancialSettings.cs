using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.SalesOrder
{
    public class FinancialSettings
    {
        public BoolValue OverrideTaxZone { get; set; }
        public StringValue CustomerTaxZone { get; set; }
    }
}
