using System;
using Push.Acumatica.Http;
using Push.Acumatica.Utility;
using Push.Foundation.Utilities.Json;
using Push.Foundation.Utilities.Logging;

namespace Push.Acumatica.Api
{
    public class ShipmentApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;

        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }


        public string RetrieveShipments(
                DateTime? lastModified = null, string expand = "Details",
                int page = 1, int? pageSize = null)
        {
            var queryString = $"$expand={expand}";

            if (lastModified.HasValue)
            {
                var restDate = lastModified.Value.ToAcumaticaRestDate();
                queryString += $"&$filter=LastModifiedDateTime gt datetimeoffset'{restDate}'";
            }

            if (pageSize.HasValue)
            {
                queryString += "&" + Paging.QueryStringParams(page, pageSize.Value);
            }

            var response = _httpContext.Get($"Shipment?{queryString}");
            return response.Body;
        }
        
        public Shipment.Shipment RetrieveShipment(string shipmentNbr)
        {
            var queryString = "$expand=Details,Packages";
            var response = _httpContext.Get($"Shipment/{shipmentNbr}?{queryString}");
            return response.Body.DeserializeFromJson<Shipment.Shipment>();
        }

        public string WriteShipment(string json)
        {
            var response = _httpContext.Put("Shipment", json);
            return response.Body;
        }

        public string ConfirmShipment(string json)
        {
            var response = _httpContext.Post("Shipment/ConfirmShipment", json);
            return response.Body;
        }
    }
}

