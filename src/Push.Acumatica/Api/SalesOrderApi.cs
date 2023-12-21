using System;
using Push.Acumatica.Http;
using Push.Acumatica.Utility;
using Push.Foundation.Utilities.Json;

namespace Push.Acumatica.Api
{
    public class SalesOrderApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;

        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }

        public string RetrieveSalesOrders(
                DateTime? lastModified = null, int page = 1, int? pageSize = null)
        {
            var queryString = "$expand=Details,ShippingSettings";

            if (lastModified.HasValue)
            {
                var restDate = lastModified.Value.ToAcumaticaRestDate();
                queryString += $"&$filter=LastModified gt datetimeoffset'{restDate}'";
            }

            if (pageSize.HasValue)
            {
                queryString += "&" + Paging.QueryStringParams(page, pageSize.Value);
            }

            var response = _httpContext.Get($"SalesOrder?{queryString}");
            return response.Body;
        }
        
        public string RetrieveSalesOrderShipments(string salesOrderId)
        {
            var url = $"SalesOrder/SO/{salesOrderId}?$expand=Shipments";
            var response = _httpContext.Get(url);
            return response.Body;
        }

        public string RetrieveSalesOrderDetails(string salesOrderId)
        {
            var url = $"SalesOrder/SO/{salesOrderId}?$expand=Details,ShippingSettings";
            var response = _httpContext.Get(url);
            return response.Body;
        }

        public SalesOrder.SalesOrder RetrieveSalesOrder(string orderNbr, string orderType)
        {
            var path = $"SalesOrder/{orderType}/{orderNbr}?$expand=Details,ShippingSettings";
            var response = _httpContext.Get(path);
            return response.Body.DeserializeFromJson<SalesOrder.SalesOrder>();
        }

        public SalesOrder.SalesOrder WriteSalesOrder(string json)
        {
            var response = _httpContext.Put("SalesOrder", json);
            return response.Body.DeserializeFromJson<SalesOrder.SalesOrder>();
        }

        public string PrepareSalesInvoice(string json)
        {
            var response = 
                _httpContext.Post("SalesOrder/PrepareSalesInvoice", json);

            return response.Body;
        }
        
        public string ReleaseSalesInvoice(string invoiceType, string json)
        {
            var response =
                _httpContext.Post(
                    $"SalesInvoice/ReleaseSalesInvoice", json);

            return response.Body;
        }
        
        public string RetrieveSalesOrderInvoice(string invoiceRefNbr, string invoiceType)
        {
            var url = $"SalesInvoice/{invoiceType}/{invoiceRefNbr}?$expand=Details";
            var response = _httpContext.Get(url);
            return response.Body;
        }
        
        public string RetrieveInvoices()
        {
            var url = $"Invoice";
            var response = _httpContext.Get(url);
            return response.Body;
        }
    }
}

