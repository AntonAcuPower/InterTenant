//using PX.Data;
//using PX.Objects.SO;

//namespace PX.Objects.PO
//{
//    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
//    public class ASCICCOPOOrderEntry_Extension : PXGraphExtension<POOrderEntry>
//    {
//        #region Event Handlers
//        protected virtual void _(Events.FieldUpdated<POOrder.sOOrderNbr> e)
//        {
//            if (e.Row == null)
//            {
//                return;
//            }
//            //POOrder row = (POOrder)e.Row;

//            if (row.OrderType == POOrderType.DropShip &&
//                !string.IsNullOrEmpty(row.SOOrderNbr) &&
//                !string.IsNullOrEmpty(row.SOOrderType))
//            {
//                SOOrder soOrder = PXSelect<SOOrder,
//                                    Where<SOOrder.orderNbr, Equal<Required<SOOrder.orderNbr>>,
//                                        And<SOOrder.orderType, Equal<Required<SOOrder.orderType>>>>>
//                                .Select(Base, row.SOOrderNbr, row.SOOrderType);

//                if (soOrder != null)
//                {
//                    ASCICCOPOOrderExt orderRowExt = row.GetExtension<ASCICCOPOOrderExt>();
//                    if (orderRowExt != null)
//                    {
//                        orderRowExt.UsrASCCustomerOrderNbr = soOrder.CustomerOrderNbr;
//                    }
//                }
//            }
//        }
//        #endregion
//    }
//}