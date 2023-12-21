using System.Collections.Generic;
using Push.Acumatica.Api.Common;
using Push.Acumatica.Api.SalesOrder;


namespace Interco.Middle.Transfers.SalesOrderSync
{
    public class SalesOrderBuilder
    {
        private readonly SalesOrder _output;

        public SalesOrderBuilder()
        {
            _output = new SalesOrder();
            _output.Details = new List<SalesOrderDetail>();
            _output.OrderType = SalesOrderType.SO.ToValue();
            _output.Status = Status.Open.ToValue();
            _output.Hold = false.ToValue();
            
            // Shipping Settings
            _output.ShippingSettings = new ShippingSettings
            {
                ShipSeparately = true.ToValue(),
                ShippingRule = "Back Order Allowed".ToValue(),
            };
            
            // Shipping Address & Contact
            _output.ShipToAddressOverride = false.ToValue();
            _output.ShipToContactOverride = false.ToValue();
        }

        public SalesOrderBuilder InjectSyncContext(SOOrderSyncContext syncContext)
        {
            // Header info
            _output.CustomerID = syncContext.DestSalesOrderCustId.ToValue();            
            _output.ExternalRef = $"{syncContext.PurchaseOrderNbr}".ToValue();
            _output.Description = $"Inventory Transfer for PO #{syncContext.PurchaseOrderNbr}".ToValue();
            
            //_output.PaymentMethod = syncContext.SalesOrderPaymentMethod.ToValue();
            //_output.CashAccount = syncContext.SalesOrderCashAccount.ToValue();

            //_output.ShipToAddressOverride = true.ToValue();
            //_output.ShipToAddress = new Address
            //{
            //    AddressLine1 = 
            //};


            _output.Details = new List<SalesOrderDetail>();

            foreach (var lineItem in syncContext.PurchaseOrder.Details)
            {
                var detail = new SalesOrderDetail();
                detail.InventoryID = lineItem.InventoryID.Copy();
                detail.OrderQty = lineItem.OrderQty.Copy();

                //detail.TaxCategory = preferences.AcumaticaTaxCategory.ToValue();

                var fulfillingItem 
                    = syncContext.InventoryContext.Items[lineItem.InventoryID.value];
                detail.ExtendedPrice = fulfillingItem.CurrentStdCost.Copy();

                _output.Details.Add(detail);
            }

            return this;
        }
        
       


        public SalesOrderBuilder AddTaxes()
        {
            //_output.FinancialSettings = new FinancialSettings()
            //{
            //    OverrideTaxZone = true.ToValue(),
            //    CustomerTaxZone = preferences.AcumaticaTaxZone.ToValue(),
            //};

            // Create Tax Details payload
            //var taxDetails = new TaxDetails();
            //taxDetails.TaxID = preferences.AcumaticaTaxId.ToValue();
            ////taxDetails.TaxRate
            ////    = ((double)(shopifyOrder.total_tax / shopifyOrder.TaxableAmountTotalAfterRefundCancels)).ToValue();
            //taxDetails.TaxableAmount = ((double)shopifyOrder.TaxableAmountTotalAfterRefundCancels).ToValue();
            //taxDetails.TaxAmount = ((double)shopifyOrder.TaxTotalAfterRefundCancels).ToValue();

            //_output.TaxDetails = new List<TaxDetails> { taxDetails };
            //_output.TaxTotal = ((double)shopifyOrder.total_tax).ToValue();
            //_output.IsTaxValid = true.ToValue();

            return this;
        }
        
        public SalesOrderBuilder OverrideShippingAddress(dynamic shopifyOrder)
        {
            // TODO - replace the Shopify object
            var shippingAddress = new Address();
            if (shopifyOrder.shipping_address != null)
            {
                shippingAddress.AddressLine1 = shopifyOrder.shipping_address.address1.ToValue();
                shippingAddress.AddressLine2 = shopifyOrder.shipping_address.address2.ToValue();
                shippingAddress.City = shopifyOrder.shipping_address.city.ToValue();
                shippingAddress.State = shopifyOrder.shipping_address.province.ToValue();
                shippingAddress.PostalCode = shopifyOrder.shipping_address.province_code.ToValue();
            }
            return this;
        }

        public SalesOrderBuilder OverrideShippingContact(dynamic shopifyOrder)
        {
            // TODO - replace the Shopify object
            var shippingContact = new ContactOverride();
            shippingContact.Email = shopifyOrder.contact_email.ToValue();

            if (shopifyOrder.shipping_address != null)
            {
                shippingContact.Attention = shopifyOrder.shipping_address.FullName.ToValue();
                shippingContact.BusinessName = shopifyOrder.shipping_address.company.ToValue();
                shippingContact.Phone1 = shopifyOrder.shipping_address.phone.ToValue();
            }

            _output.ShipToContact = shippingContact;
            return this;
        }
        
        // TODO - 
        public SalesOrder Result()
        {
            return _output;
        }
    }
}

