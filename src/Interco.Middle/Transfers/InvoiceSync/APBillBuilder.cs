using System.Collections.Generic;
using Push.Acumatica.Api.Common;
using Push.Acumatica.Api.Payables;

namespace Interco.Middle.Transfers.InvoiceSync
{
    public class APBillBuilder
    {
        private readonly Bill _output;

        public APBillBuilder()
        {
            _output = new Bill();
        }

        public APBillBuilder InjectContext(InvoiceContext context)
        {
            _output.Type = context.PayableDocType.ToValue();
            _output.Vendor = context.DestVendorCustId.ToValue();
            _output.VendorRef = context.Invoice.ReferenceNbr.Copy();
            _output.Description 
                = string.Format(
                    Constants.PushBill_TransDescription, context.Invoice.ReferenceNbr).ToValue();

            _output.DueDate = context.Invoice.DueDate.Copy();
            _output.Details = new List<BillDetail>();

            //var rowNumber = 1;

            foreach (var line in context.Invoice.Details)
            {
                var detail = new BillDetail();
                //detail.rowNumber = rowNumber++;
                detail.InventoryID = line.InventoryID.Copy();
                detail.TransactionDescription = line.TransactionDescription.Copy();
                detail.Qty = line.Qty.Copy();
                _output.Details.Add(detail);
            }
            _output.Hold = false.ToValue();

            return this;
        }

        public Bill Flush()
        {
            return this._output;
        }
    }
}
