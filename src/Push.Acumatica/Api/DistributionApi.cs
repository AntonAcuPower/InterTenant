using System;
using Push.Acumatica.Api.Distribution;
using Push.Acumatica.Http;
using Push.Acumatica.Utility;
using Push.Foundation.Utilities.Http;
using Push.Foundation.Utilities.Json;

namespace Push.Acumatica.Api
{
    public class DistributionApi : IAcumaticaApi
    {
        private AcumaticaHttpContext _httpContext;

        public void InjectHttpContext(AcumaticaHttpContext context)
        {
            _httpContext = context;
        }


        public string RetrieveItemClass()
        {
            var response = _httpContext.Get("ItemClass");
            return response.Body;
        }
        
        public string RetrievePostingClasses()
        {
            var response = _httpContext.Get("PostingClass");
            return response.Body;
        }

        public string RetrieveWarehouses()
        {
            var queryString =
                    new QueryStringBuilder()
                        .Add("$expand", "Locations")
                        .ToString();

            var response = _httpContext.Get($"Warehouse?{queryString}");
            return response.Body;
        }

        public string AddNewWarehouse(string content)
        {
            var response = _httpContext.Put("Warehouse", content);
            return response.Body;
        }


        public string RetrieveStockItems(
                DateTime? lastModified = null, int page = 1, int? pageSize = null)
        {
            var queryString = "$expand=WarehouseDetails&$filter=ItemStatus eq 'Active'";
            if (lastModified.HasValue)
            {
                var restDate = lastModified.Value.ToAcumaticaRestDate();
                queryString += $" and LastModified gt datetimeoffset'{restDate}'";
            }

            if (pageSize.HasValue)
            {
                queryString += "&" + Paging.QueryStringParams(page, pageSize.Value);
            }

            var response = _httpContext.Get($"StockItem?{queryString}");
            return response.Body;
        }

        public StockItem RetrieveStockItem(string itemCD)
        {
            var path = $"StockItem/{itemCD}?$expand=WarehouseDetails";
            var response = _httpContext.Get(path);
            return response.Body.DeserializeFromJson<StockItem>();
        }

        public string AddNewStockItem(string content)
        {
            var response = _httpContext.Put("StockItem", content);
            return response.Body;
        }

        public string RetreiveInventoryReceipts()
        {
            var response = _httpContext.Get("InventoryReceipt?$expand=Details");
            return response.Body;
        }

        public string AddInventoryReceipt(string content)
        {
            var response = _httpContext.Put("InventoryReceipt", content);
            return response.Body;
        }

        public string ReleaseInventoryReceipt(string content)
        {
            var response 
                = _httpContext.Post(
                    "InventoryReceipt/ReleaseInventoryReceipt", content);
            return response.Body;
        }

    }
}
