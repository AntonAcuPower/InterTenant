
namespace ASCInterCo
{
    using System;
    using PX.Data;
    //using PX.Objects.PO;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Collections;
    using System.Threading.Tasks;

    using Interco.Middle.Transfers;
    using Interco.Middle.Transfers.SalesOrderSync;
    using Interco.Middle.Transfers.PurchaseReceiptSync;
    using Interco.Middle.Transfers.InvoiceSync;
    using Interco.Middle.Utility;

    using Push.Acumatica.Api.Common.Monster.Middle.Processes.Acumatica.Persist;
    using Push.Acumatica.Config;
    using Push.Acumatica.Http;
    using Push.Foundation.Utilities.Helpers;
    //using PX.Objects.AR;
    //using PX.Objects.CM;
    using PX.Objects.Common.Logging;
    //using PX.Objects.CS;
    //using PX.Objects.CT;
    //using PX.Objects.GL;
    //using PX.Objects.GL.ConsolidationImport;
    //using PX.Reports;


    public partial class ICTranViewEntry : PXGraph<ICTranViewEntry, ASCICTranView>
    {

        #region Views
        [PXViewName(Messages.ICTran)]
        public PXSelect<ASCICTranView, Where<ASCICTranView.iCtranid, Equal<Current<ASCICTran.iCtranid>>>> Document;
        public PXSelect<ASCICSite, Where<ASCICSite.iCsiteID, Equal<Required<ASCICSite.iCsiteID>>>> Site;
        public PXSelect<ASCICTran, Where<ASCICTran.sourceRef, Equal<Required<ASCICTran.sourceRef>>, And<ASCICTran.sourceType, Equal<Required<ASCICTran.sourceType>>>>> POTran;

        public void ProcessTran(ASCICTranView ict)
        {
            ProcessTransaction(ict);
        }

        private void ProcessTransaction(ASCICTranView ict)
        {
            try
            {
                string method = Methods.ProcessTran;
                //PXTrace.WriteInformation($"({method}) STARTING");
                string srcObject = ict.SourceObject.ToString();
                //PXTrace.WriteInformation($"({method}) srcObject:{srcObject}");
                string dstObject = ict.DestinationObject.ToString();
                //PXTrace.WriteInformation($"({method}) dstObject:{dstObject}");
                string process = $"{srcObject}To{dstObject}";
                //PXTrace.WriteInformation($"({method}) process:{process}");
                ASCICSite srcSite = Site.Select(ict.SourceSiteID);
                //PXTrace.WriteInformation($"({method}) srcSite:{srcSite.ICsiteCD}");
                ASCICSite dstSite = Site.Select(ict.DestinationSiteID);
                //PXTrace.WriteInformation($"({method}) dstSite:{dstSite.ICsiteCD}");

                switch (process.Trim())
                {
                    case Messages.POOrderToSOOrder:
                        POOrderToSOOrder(ict);
                        break;
                    case Messages.POOrderToARPayment:
                        POOrderToARPayment(ict);
                        break;
                    case Messages.SOShipmentToPOReceipt:
                        SOShipmentToPOReceipt(ict);
                        break;
                    case Messages.SOShipmentToAPPayment:
                        SOShipmentToAPPayment(ict);
                        break;
                    case Messages.ARInvoiceToAPBill:
                        ARInvoiceToAPBill(ict);
                        break;
                    default:
                        throw new PXException($"({method}) {srcObject} To {dstObject} Inter Company Handler not found.");

                }

                PXTrace.WriteInformation($"({method}) EXITING");
            }
            catch (Exception E)
            {
                PXTrace.WriteError(E);
                ict.FailedAttempts += 1;
                throw E;
            }
        }

        private void CreateTran(ASCICTranView ict, string DestinationRef, bool Complete = true, int FailedAttempts = 0)
        {
            string method = Methods.CreateTran;
            PXTrace.WriteInformation($"({method}) Start");
            var tran = new ASCICTran();
            


            tran.SourceSiteID = ict.SourceSiteID;
            tran.SourceObject = ict.SourceObject;
            tran.SourceType = ict.SourceType;
            tran.SourceRef = ict.SourceRef;
            tran.DestinationSiteID = ict.DestinationSiteID;
            tran.DestinationObject = ict.DestinationObject;
            tran.DestinationType = ict.DestinationType;
            tran.RemoteAcctCD = ict.RemoteAcctCD;
            tran.DestinationRef = DestinationRef;
            tran.Complete = Complete;
            tran.FailedAttempts = FailedAttempts;
            tran.CompletedDateTime = System.DateTime.Now;
            tran.CreatedByID = ict.CreatedByID;
            tran.CreatedByScreenID = ict.CreatedByScreenID;
            tran.CreatedDateTime = ict.CreatedDateTime;
            tran.LastModifiedByID = ict.LastModifiedByID;
            tran.LastModifiedByScreenID = ict.LastModifiedByScreenID;
            tran.LastModifiedDateTime = ict.LastModifiedDateTime;

            PXTrace.WriteInformation($"({method}) Saving");

            var graph = CreateInstance<ASCICTranEntry>();
            graph.Document.Insert(tran);
            graph.Save.Press();
            PXTrace.WriteInformation($"({method}) End");

        }

        private void POOrderToSOOrder(ASCICTranView ict)
        {
            string method = Methods.POOrderToSOOrder;
            PXTrace.WriteInformation($"({method}) Start");
            try
            {
                var manager = BuildTransferMgr(GetSiteCredentials(Site.Select(ict.SourceSiteID))
                                             , GetSiteCredentials(Site.Select(ict.DestinationSiteID)));

                PXTrace.WriteInformation($"({method}) salesOrderContext ({ict.SourceType},{ict.SourceRef},{ict.RemoteAcctCD})");
                var context = new SOOrderSyncContext
                {
                    PurchaseOrderNbr = ict.SourceRef,
                    PurchaseOrderType = ict.SourceType,
                    DestSalesOrderCustId = ict.RemoteAcctCD,
                };
                PXTrace.WriteInformation($"({method}) manager.POOrderToSOOrder");
                manager.POOrderToSOOrder(context);
                if (context.ExceptionThrown == true)
                {
                    PXTrace.WriteInformation($"({context.Exception.Message}) context.Exception.Message");
                    PXTrace.WriteInformation($"({context.Exception.StackTrace}) context.Exception.StackTrace");
                    throw new PXException(context.Exception.Message.ToString());
                }
                CreateTran(ict, context.SalesOrder.OrderNbr.value);

            }
            catch
            {
                throw;
            }
            PXTrace.WriteInformation(String.Format("({0}) End", method));

        }

        private void POOrderToARPayment(ASCICTranView ict)
        {
            string method = Methods.POOrderToARPayment;
            PXTrace.WriteInformation($"({method}) Start");
            try
            {
                var manager = BuildTransferMgr(GetSiteCredentials(Site.Select(ict.SourceSiteID))
                                             , GetSiteCredentials(Site.Select(ict.DestinationSiteID)));

                ASCICTran poTran = POTran.Select(ict.SourceRef, ict.SourceType);
                PXTrace.WriteInformation($"poTran.DestinationRef:{poTran.DestinationRef}");
                PXTrace.WriteInformation($"poTran.DestinationRef:{poTran.DestinationType}");
                PXTrace.WriteInformation($"({method}) SOPaymentSyncContext");

                var context = new SOPaymentSyncContext()
                {
                    SalesOrderNbr = poTran.DestinationRef,
                    SalesOrderType = poTran.DestinationType,
                };
                PXTrace.WriteInformation($"({method}) manager.SOOrderToSOPayment");
                manager.SOOrderToSOPayment(context);

                if (context.ExceptionThrown==true)
                {
                    throw new PXException(context.Exception.Message.ToString());
                }
                CreateTran(ict, context.Payment.PaymentRef.value);

            }
            catch (Exception E)
            {
                throw new PXException(E.Message);
            }
            PXTrace.WriteInformation($"({method}) End");


        }

        private void SOShipmentToPOReceipt(ASCICTranView ict)
        {
            string method = Methods.SOShipmentToPOReceipt;
            PXTrace.WriteInformation($"({method}) Start");
            try
            {
                var manager = BuildTransferMgr(GetSiteCredentials(Site.Select(ict.SourceSiteID))
                                             , GetSiteCredentials(Site.Select(ict.DestinationSiteID)));

                PXTrace.WriteInformation($"({method}) PurchaseReceiptSyncContext");
                var context = new PurchaseReceiptSyncContext
                {
                    FulfillingShipmentNbr = ict.SourceRef,
                };
                manager.SOShipmentToPOReceipt(context);
                PXTrace.WriteInformation($"({method}) manager.SOShipmentToPOReceipt");
                if (context.ExceptionThrown == true)
                {
                    throw new PXException(context.Exception.Message);
                }
                CreateTran(ict, context.PurchaseReceipt.ReceiptNbr.value);

            }
            catch (Exception E)
            {
                throw new PXException(E.Message);
            }
            PXTrace.WriteInformation(String.Format("({0}) End", method));
        }

        private void ARInvoiceToAPBill(ASCICTranView ict)
        {

            string method = Methods.ARInvoiceToAPBill;
            PXTrace.WriteInformation($"({method}) Start");
            try
            {
                var manager = BuildTransferMgr(GetSiteCredentials(Site.Select(ict.SourceSiteID))
                                             , GetSiteCredentials(Site.Select(ict.DestinationSiteID)));

                PXTrace.WriteInformation($"({method}) ARInvoiceToAPBill");
                var context = new InvoiceContext
                {
                    InvoiceReferenceNbr = ict.SourceRef,
                    InvoiceDocType = ict.SourceType,
                    DestVendorCustId = ict.RemoteAcctCD,
                };
                manager.ARInvoiceToAPBill(context);
                PXTrace.WriteInformation($"({method}) manager.ARInvoiceToAPBill");
                if (context.ExceptionThrown == true)
                {
                    throw new PXException(context.Exception.Message);
                }
                CreateTran(ict, context.Bill.ReferenceNbr.value);

            }
            catch (Exception E)
            {
                throw new PXException(E.Message);
            }
            PXTrace.WriteInformation(String.Format("({0}) End", method));
        }

        private void SOShipmentToAPPayment(ASCICTranView ict)
        {
            string method = Methods.SOShipmentToAPPayment;
            PXTrace.WriteInformation(String.Format("({0}) Start", method));
            try
            {
                //var manager = BuildTransferMgr(GetSiteCredentials(SourceSite.Select(ict.SourceSiteID))
                //                             , GetSiteCredentials(DestinationSite.Select(ict.DestinationSiteID)));

                //var salesOrderContext = new SOOrderSyncContext
                //{
                //    PurchaseOrderNbr = ict.SourceRef,
                //    PurchaseOrderType = ict.SourceType,
                //    DestSalesOrderCustId = ict.RemoteAcctCD,
                //};
                //manager.POOrderToSOOrder(salesOrderContext);
            }
            catch (Exception E)
            {
                throw new PXException(E.Message);
            }
            PXTrace.WriteInformation(String.Format("({0}) End", method));
        }

        /********************HANDLER TEMPLATE***************************//*
        private void TemplateToTemplate(ASCICTran ict)
        {
            string method = Methods.TemplateToTemplate; //Remember to Add Method name to Method Class
            PXTrace.WriteInformation(String.Format("({0}) Start", method));
            try
            {
                var manager = BuildTransferMgr(GetSiteCredentials(SourceSite.Select(ict.SourceSiteID))
                                             , GetSiteCredentials(DestinationSite.Select(ict.DestinationSiteID)));

                var TemplateContext = new TemplateSyncContext
                {
                    PurchaseOrderNbr = ict.SourceRef,
                    PurchaseOrderType = ict.SourceType,
                    DestSalesOrderCustId = ict.RemoteAcctCD,
                };
                manager.TemplateToTemplate(salesOrderContext);
            }
            catch (Exception E)
            {
                throw new PXException(E.Message);
            }
        }

        *//***************************************************************/

        private static TransferManager BuildTransferMgr(
                AcumaticaCredentials source, AcumaticaCredentials destination)
        {
            string method = Methods.BuildTransferMgr;
            PXTrace.WriteInformation($"({method}) Start");

            //var SourceSettings = GetSiteConfig(source);
            //var DestinationSettings = GetSiteConfig(destination);

            var SourceSettings = new AcumaticaHttpConfig
            {
                VersionSegment = source.VersionSegment,
                MaxAttempts = source.MaxAttempts,
                Timeout = 30000,
            };

            var DestinationSettings = new AcumaticaHttpConfig
            {
                VersionSegment = destination.VersionSegment,
                MaxAttempts = destination.MaxAttempts,
                Timeout = 30000,
            };

            var director = new TransferManager();

            director.SourceCredentials = source;
            director.DestinationCredentials = destination;
            director.SourceSettings = SourceSettings;
            director.DestinationSettings = DestinationSettings;

            // Uncomment this line to enable NLogger
            //director.EnableNLogger();
            PXTrace.WriteInformation($"({method}) Initialize");
            director.Initialize();
            PXTrace.WriteInformation($"({method}) End");
            return director;
        }

        private AcumaticaCredentials GetSiteCredentials(ASCICSite site)
        {
            string method = Methods.GetSiteCredentials;
            PXTrace.WriteInformation($"({method}) Start");

            //Int32 MaxAttempts = 3;
            //Int32.TryParse(site.MaxAttempts.ToString(), out MaxAttempts);
            //Int32 Timeout = 120;
            //Int32.TryParse(site.Timeout.ToString(), out Timeout);

            //AcumaticaHttpConfig config = new AcumaticaHttpConfig
            //{
            //    VersionSegment = site.VersionSegment.ToString(),
            //    MaxAttempts = MaxAttempts,
            //    Timeout = Timeout
            //};

            PXTrace.WriteInformation($"({method}) Return");

            return new AcumaticaCredentials
            {

                CompanyName = site.CompanyKey.ToString(),
                Branch = site.BranchCD.ToString(),
                Username = site.Login.ToString(),
                Password = site.Password.ToString(),
                InstanceUrl = site.Url.ToString(),
                MaxAttempts = (int)site.MaxAttempts,
                VersionSegment = site.VersionSegment,

            };

        }
        private static AcumaticaHttpConfig GetSiteConfig(AcumaticaCredentials credentials)
        {
            string method = Methods.GetSiteCredentials;
            PXTrace.WriteInformation($"({method}) Start");

            string VersionSegment = credentials.VersionSegment.ToString() ?? "/entity/Default/18.200.001/";
            Int32 MaxAttempts = 3;
            Int32.TryParse(credentials.MaxAttempts.ToString(), out MaxAttempts);
            Int32 Timeout = 180000;
            Int32.TryParse(credentials.Timeout.ToString(), out Timeout);

            PXTrace.WriteInformation($"({method}) Return");

            return new AcumaticaHttpConfig
            {
                VersionSegment = VersionSegment,
                MaxAttempts = MaxAttempts,
                Timeout = Timeout
            };

        }

        private ASCICSite DecryptRemoteUserPassword(ASCICSite site)
        {
            ASCICSite setup = site;
            var cache = Caches[typeof(ASCICSite)];
            PXDBCryptStringAttribute.SetDecrypted<ASCICSite.password>(cache, true);
            setup.Password = cache.GetValueExt<ASCICSite.password>(setup).ToString();
            PXDBCryptStringAttribute.SetDecrypted<ASCICSite.password>(cache, false);
            return setup;
        }

        #endregion
    }
}
