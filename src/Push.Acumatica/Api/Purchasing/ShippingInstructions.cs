using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Purchasing
{
    public class ShippingInstructions
    {
        public string id { get; set; }
        public int rowNumber { get; set; }
        public StringValue note { get; set; }
        public StringValue ShippingDestinationType { get; set; }
        public StringValue ShippingLocation { get; set; }
        public StringValue ShipTo { get; set; }
        public BoolValue ShipToAddressOverride { get; set; }
        public BoolValue ShipToContactOverride { get; set; }
        public StringValue Warehouse { get; set; }
        public StringValue custom { get; set; }

        public Address ShipToAddress { get; set; }
        public ContactOverride ShipToContact { get; set; }

        //public List<object> files { get; set; }
    }

}
