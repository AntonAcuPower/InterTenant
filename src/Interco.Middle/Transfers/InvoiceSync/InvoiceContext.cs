using System;
using Push.Acumatica.Api.Payables;
using Push.Acumatica.Api.Receivables;

namespace Interco.Middle.Transfers.InvoiceSync
{
    public class InvoiceContext : IWorkerContext
    {
        // input
        public string InvoiceReferenceNbr { get; set; }
        public string InvoiceDocType { get; set; }
        public string DestVendorCustId { get; set; }
        
        public string PayableReferenceNbr { get; set; }
        public string PayableDocType { get; set; }
        


        // output
        public Invoice Invoice { get; set; }
        

        // output
        public Bill Bill { get; set; }


        // exception handling
        public bool ExceptionThrown => Exception != null;
        public Exception Exception { get; set; }
    }
}
