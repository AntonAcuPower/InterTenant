using Push.Acumatica.Http;

namespace Push.Acumatica.Api
{
    public class ReferenceApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;

        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }
        
        public string RetrieveItemClass()
        {
            var response = _httpContext.Get($"ItemClass");
            return response.Body;
        }
        
        public string RetrievePaymentMethod()
        {
            var response = _httpContext.Get($"PaymentMethod?$expand=AllowedCashAccounts");
            return response.Body;
        }

        public string RetrieveTaxZones()
        {
            var response = _httpContext.Get($"TaxZone");
            return response.Body;
        }

        public string RetrieveTaxCategories()
        {
            var response = _httpContext.Get($"TaxCategory");
            return response.Body;
        }

        public string RetrieveTaxes()
        {
            var response = _httpContext.Get($"Tax");
            return response.Body;
        }
    }
}
