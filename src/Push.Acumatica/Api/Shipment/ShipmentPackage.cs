using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Shipment
{

    public class ShipmentPackage
    {
        public StringValue BoxID { get; set; }
        public BoolValue Confirmed { get; set; }
        public DoubleValue DeclaredValue { get; set; }
        public StringValue Description { get; set; }
        public StringValue TrackingNbr { get; set; }
        public StringValue UOM { get; set; }
        public DoubleValue Weight { get; set; }
    }
}
