using System.Collections.Generic;
using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Payment
{
    public class PaymentOrdersRef
    {
        public StringValue OrderNbr { get; set; }
        public StringValue OrderType { get; set; }

        public static List<PaymentOrdersRef> ForOrder(string orderNbr, string orderType)
        {
            return new List<PaymentOrdersRef>()
            {
                new PaymentOrdersRef()
                {
                    OrderNbr = orderNbr.ToValue(),
                    OrderType = orderType.ToValue()
                }
            };
        }
    }
}
