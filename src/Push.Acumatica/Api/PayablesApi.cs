using Push.Acumatica.Api.Payables;
using Push.Acumatica.Api.Receivables;
using Push.Acumatica.Http;
using Push.Foundation.Utilities.Json;

namespace Push.Acumatica.Api
{
    public class PayablesApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;

        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }
        
        public Bill RetrievePayable(string referenceNbr, string type)
        {
            var path = $"Bill/{type}/{referenceNbr}?$expand=Details,TaxDetails";
            var response = _httpContext.Get(path);
            return response.Body.DeserializeFromJson<Bill>();
        }

        public Bill WritePayable(Bill bill)
        {
            var json = bill.SerializeToJson();
            var response = _httpContext.Put("Bill", json);
            return response.Body.DeserializeFromJson<Bill>();
        }
    }
}

