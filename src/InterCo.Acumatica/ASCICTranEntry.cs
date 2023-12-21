
namespace ASCInterCo
{
    using System;
    using PX.Data;
    using PX.Objects.PO;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Collections;
    using System.Threading.Tasks;

    using Interco.Middle.Transfers;
    using Interco.Middle.Transfers.InvoiceSync;
    using Interco.Middle.Transfers.PurchaseReceiptSync;
    using Interco.Middle.Transfers.SalesOrderSync;

    using Push.Acumatica.Api.Common.Monster.Middle.Processes.Acumatica.Persist;
    using Push.Acumatica.Config;
    using Push.Acumatica.Http;
    using Push.Foundation.Utilities.Helpers;


    public class ASCICTranEntry : PXGraph<ASCICTranEntry, ASCICTran>
    {
        #region Views
        [PXViewName(Messages.ICTran)]

        public PXSelect<ASCICTran, Where<ASCICTran.iCtranid, Equal<Current<ASCICTran.iCtranid>>>> Document;
        public PXSelect<ASCICSite, Where<ASCICSite.iCsiteID, Equal<Required<ASCICTran.sourceSiteID>>>> SourceSite;
        public PXSelect<ASCICSite, Where<ASCICSite.iCsiteID, Equal<Required<ASCICTran.destinationSiteID>>>> DestinationSite;

        #endregion
    }
}
