using Push.Acumatica.Api;

namespace Interco.Middle.Transfers
{
    public class ApiDepot
    {
        public ApiFactory SourceApiFactory { get; set; }
        public ApiFactory DestinationApiFactory { get; set; }

        public ApiDepot(ApiFactory sourceApiFactory, ApiFactory destinationApiFactory)
        {
            SourceApiFactory = sourceApiFactory;
            DestinationApiFactory = destinationApiFactory;
        }
    }
}
