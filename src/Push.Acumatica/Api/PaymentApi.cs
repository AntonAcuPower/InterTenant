using Push.Acumatica.Api.Payment;
using Push.Acumatica.Http;
using Push.Foundation.Utilities.Json;

namespace Push.Acumatica.Api
{
    public class PaymentApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;

        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }

        public Payment.Payment WritePayment(PaymentWrite paymentWrite)
        {
            var response = _httpContext.Put("Payment", paymentWrite.SerializeToJson());
            return response.Body.DeserializeFromJson<Payment.Payment>();
        }
        
        public Payment.Payment RetrievePayment(
                        string referenceNbr, string paymentType, string expand)
        {
            var path = $"Payment/{paymentType}/{referenceNbr}?$expand={expand}";
            var response = _httpContext.Get(path);
            return response.Body.DeserializeFromJson<Payment.Payment>();
        }
    }
}
