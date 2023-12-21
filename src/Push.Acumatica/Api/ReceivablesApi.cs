using Push.Acumatica.Api.Payment;
using Push.Acumatica.Api.Receivables;
using Push.Acumatica.Http;
using Push.Foundation.Utilities.Json;

namespace Push.Acumatica.Api
{
    public class ReceivablesApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;

        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }
        
        public Invoice RetrieveInvoice(string referenceNbr, string type)
        {
            var path = $"Invoice/{type}/{referenceNbr}?$expand=Details,TaxDetails";
            var response = _httpContext.Get(path);
            return response.Body.DeserializeFromJson<Invoice>();
        }
    }
} 
