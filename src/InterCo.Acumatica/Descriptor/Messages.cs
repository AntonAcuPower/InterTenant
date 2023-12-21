using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCInterCo
{
    public static class Messages
    {
        #region DAC Names
        public const string ICSite = "Inter Company Site";
        public const string ICTran = "Inter Company Transaction";
        public const string ICTranView = "Inter Company Transaction View";

        public const string ICSiteBAccount = "IC Account Settings";
        public const string ICSiteBAccountSettings = "IC Site Account Settings";

        #endregion

        #region Methods
        public const string POOrderToSOOrder = "POOrderToSOOrder";
        public const string POOrderToARPayment = "POOrderToARPayment";
        public const string SOShipmentToPOReceipt = "SOShipmentToPOReceipt";

        public const string SOShipmentToAPPayment = "SOShipmentToAPPayment";
        public const string ARInvoiceToAPBill = "ARInvoiceToAPBill";
        
        #endregion

    }
}
