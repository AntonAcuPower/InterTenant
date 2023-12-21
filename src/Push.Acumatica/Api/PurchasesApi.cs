using Push.Acumatica.Api.Purchasing;
using Push.Acumatica.Http;
using Push.Foundation.Utilities.Json;
using Push.Foundation.Utilities.Logging;

namespace Push.Acumatica.Api
{
    public class PurchasesApi : IAcumaticaApi
    {
        private readonly IPushLogger _logger;
        private AcumaticaHttpContext _httpContext;

        public PurchasesApi(IPushLogger logger)
        {
            _logger = logger;
        }
        
        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }
        
        public PurchaseOrder RetrievePurchaseOrder(string orderNbr, string orderType)
        {
            var path = $"PurchaseOrder/{orderType}/{orderNbr}?$expand=Details,ShippingInstructions," 
                + "ShippingInstructions/ShipToAddress,ShippingInstructions/ShipToContact"; 
            var response = _httpContext.Get(path);
            return response.Body.DeserializeFromJson<PurchaseOrder>();
        }
        
        public string WritePurchaseOrder(string json)
        {
            var response = _httpContext.Put("PurchaseOrder", json);
            return response.Body;
        }

        public PurchaseReceipt WritePurchaseReceipt(PurchaseReceipt purchaseReceipt)
        {
            var response = _httpContext.Put("PurchaseReceipt", purchaseReceipt.SerializeToJson());
            return response.Body.DeserializeFromJson<PurchaseReceipt>();
        }
    }
}

