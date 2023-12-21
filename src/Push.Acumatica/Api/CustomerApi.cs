using System;
using Push.Acumatica.Http;
using Push.Acumatica.Utility;


namespace Push.Acumatica.Api
{
    public class CustomerApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;
        
        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }

        public string RetrieveCustomers(DateTime? lastModified = null)
        {
            var queryString = "$expand=MainContact";

            if (lastModified.HasValue)
            {
                var restDate = lastModified.Value.ToAcumaticaRestDate();
                queryString += $"&$filter=LastModifiedDateTime gt datetimeoffset'{restDate}'";
            }

            var response = _httpContext.Get($"Customer?{queryString}");
            return response.Body;
        }

        public string RetrieveCustomer(string customerId)
        {
            var path = $"Customer/{customerId}?$expand=MainContact";
            var response = _httpContext.Get(path);
            return response.Body;
        }
        
        public string WriteCustomer(string content)
        {
            var response = _httpContext.Put("Customer", content);
            return response.Body;
        }

    }
}

