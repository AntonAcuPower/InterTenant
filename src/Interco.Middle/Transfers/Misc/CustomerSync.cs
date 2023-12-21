using Push.Acumatica.Api;
using Push.Acumatica.Api.Common;
using Push.Acumatica.Api.Customer;
using Push.Foundation.Utilities.Logging;

namespace Interco.Middle.Transfers.Misc
{
    public class CustomerSync
    {
        private readonly CustomerApi _customerClient;
        private readonly IPushLogger _logger;

        public CustomerSync(
                CustomerApi customerClient, IPushLogger logger)
        {
            _customerClient = customerClient;
            _logger = logger;
        }
        

        public void PushCustomer()
        {
            dynamic shopifyCustomer = new object();            
            var customer = BuildCustomer(shopifyCustomer);


            // Push Customer to Acumatica API
            var resultJson = _customerClient.WriteCustomer(customer.SerializeToJson());
            var customerResult = resultJson.DeserializeFromJson<Customer>();

            var log = $"Wrote Customer {customerResult.CustomerID.value} to Acumatica";
            _logger.Info(log);            
        }


        // TODO - determine how to appropriately share Customer between Company-Instances
        private static Customer BuildCustomer(dynamic shopifyCustomer)
        {
            var name = shopifyCustomer.first_name + " " + shopifyCustomer.last_name;
            var shopifyAddress = shopifyCustomer.default_address;

            var customer = new Customer();
            customer.CustomerName = name.ToValue();

            var address = new Address();
            if (shopifyAddress != null)
            {
                address.AddressLine1 = shopifyAddress.address1.ToValue();
                address.AddressLine2 = shopifyAddress.address2.ToValue();
                address.City = shopifyAddress.city.ToValue();
                address.State = shopifyAddress.province.ToValue();
                address.PostalCode = shopifyAddress.zip.ToValue();
            }

            var mainContact = new Contact();
            mainContact.Address = address;
            mainContact.FirstName = shopifyCustomer.first_name.ToValue();
            mainContact.LastName = shopifyCustomer.last_name.ToValue();
            mainContact.Phone1 = shopifyCustomer.phone.ToValue();
            mainContact.Email = shopifyCustomer.email.ToValue();

            customer.MainContact = mainContact;
            customer.AccountRef = $"Shopify Customer #{shopifyCustomer.id}".ToValue();
            return customer;
        }
    }
}

