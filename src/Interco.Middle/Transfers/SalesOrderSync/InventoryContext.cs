using System.Collections.Generic;
using Push.Acumatica.Api.Distribution;

namespace Interco.Middle.Transfers.SalesOrderSync
{
    public class InventoryContext
    {
        public Dictionary<string, StockItem> Items { get; set; }

        public InventoryContext()
        {
            Items = new Dictionary<string, StockItem>();
        }

        public void Set(StockItem item)
        {
            Items[item.InventoryID.value] = item;
        }
    }
}

