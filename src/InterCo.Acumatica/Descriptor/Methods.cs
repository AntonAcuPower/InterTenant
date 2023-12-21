using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCInterCo
{
    public static class Methods
    {
        #region DAC Names
        public const string ProcessTran = "ProcessTran";
        public const string POOrderToSOOrder = "POOrderToSOOrder";
        public const string POOrderToARPayment = "POOrderToARPayment";
        public const string SOShipmentToPOReceipt = "SOShipmentToPOReceipt";
        public const string SOShipmentToAPPayment = "SOShipmentToAPPayment";
        public const string ARInvoiceToAPBill = "ARInvoiceToAPBill";
        public const string BuildTransferMgr = "BuildTransferMgr";
        public const string CreateTran = "CreateTran";
        public const string GetSiteCredentials = "GetSiteCredentials";
        public const string GetSiteConfig = "GetSiteConfig";


        #endregion

    }
}
