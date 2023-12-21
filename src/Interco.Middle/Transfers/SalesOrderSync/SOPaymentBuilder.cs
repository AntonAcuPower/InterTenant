using Push.Acumatica.Api.Common;
using Push.Acumatica.Api.Common.Monster.Middle.Processes.Acumatica.Persist;
using Push.Acumatica.Api.Payment;
using Push.Acumatica.Api.SalesOrder;

namespace Interco.Middle.Transfers.SalesOrderSync
{
    public class SOPaymentBuilder
    {
        private readonly PaymentWrite _paymentWrite;

        public SOPaymentBuilder()
        {
            _paymentWrite = new PaymentWrite();
        }

        public SOPaymentBuilder InjectContext(SOPaymentSyncContext context)
        {
            _paymentWrite.CustomerID = context.SalesOrder.CustomerID.Copy();
            _paymentWrite.Hold = false.ToValue();
            _paymentWrite.Type = PaymentType.Payment.ToValue();
            _paymentWrite.PaymentMethod = context.SalesOrder.PaymentMethod.Copy();
            
            _paymentWrite.CashAccount = context.SalesOrder.CashAccount.Copy();
            _paymentWrite.PaymentRef = context.SalesOrder.ExternalRef.Copy();
            _paymentWrite.PaymentAmount = context.SalesOrder.OrderTotal.Copy();
            _paymentWrite.Description = $"Payment for {context.SalesOrder.ExternalRef.value}".ToValue();
            _paymentWrite.OrdersToApply =
                PaymentOrdersRef.ForOrder(context.SalesOrder.OrderNbr.value, SalesOrderType.SO);

            return this;
        }

        public PaymentWrite Result()
        {
            return _paymentWrite;
        }
    }
}
