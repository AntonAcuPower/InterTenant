using Push.Acumatica.Api.Common;

namespace Push.Acumatica.Api.Purchasing
{
    public class POVenderRefUpdate
    {
        public StringValue OrderNbr { get; set; }
        public StringValue Type { get; set; }
        public StringValue VendorRef { get; set; }
    }
}
