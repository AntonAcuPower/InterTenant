

namespace ASCInterCo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PX.Data;
    using PX.Objects;

    public class ASCICTranProcess : PXGraph<ASCICTranProcess>
    {
        public PXCancel<ASCICTranView> Cancel;
        public PXAction<ASCICTranView> viewDocument;

        #region Dataviews

        public PXSelect<ASCICTranView, Where<ASCICTranView.complete, Equal<False>>> Transactions;
        public PXProcessing<ASCICTranView, Where<ASCICTranView.complete, Equal<False>>> PagePrimaryView;
        #endregion

        #region Process Receipts List

        private static void ProcessICTranList(IList<ASCICTranView> records)
        {

            var globalError = false;
            int iCTranID = 0;
            var graph = CreateInstance<ICTranViewEntry>();

            foreach (var record in records)

            {
                var lineError = false;
                try
                {
                    graph.Clear();
                    iCTranID = (int)record.ICtranid;
                    PXTrace.WriteInformation(String.Format("(ProcessICTranList) iCTranID:{0}", iCTranID));
                    graph.Document.Current = record;
                    graph.ProcessTran(record);
                    graph.Save.Press();

                }
                catch (PXException e)
                {
                    lineError = true; //LINE
                    globalError = true; //OVERALL
                    var message = "Error Processing IC Transaction: " + iCTranID + " - " + e.Message;
                    PXProcessing.SetError<ASCICTranView>(records.IndexOf(record), message);

                }

                var lineMessage = "Transaction Processed";
                if (!lineError) PXProcessing.SetInfo<ASCICTranView>(records.IndexOf(record), lineMessage);

            }
            if (globalError)
                throw new PXException($"At Least One Inter Company Transaction Was Not Processed.");

        }

        #endregion

        #region Constructor
        public ASCICTranProcess()
        {
            PXTrace.WriteInformation(String.Format("{0}", "Constructor"));
            PagePrimaryView.SetProcessDelegate(ProcessICTranList);
            PagePrimaryView.SetSelected<ASCICTranView.selected>();
        }

        #endregion

        #region Overridden Properties
        public override bool IsDirty => false;
        #endregion

    }
}
