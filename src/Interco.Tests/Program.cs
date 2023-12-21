using System;
using Interco.Middle.Transfers;
using Interco.Middle.Transfers.InvoiceSync;
using Interco.Middle.Transfers.PurchaseReceiptSync;
using Interco.Middle.Transfers.SalesOrderSync;
using Push.Acumatica.Api.Purchasing;
using Push.Acumatica.Api.Receivables;
using Push.Acumatica.Config;
using Push.Acumatica.Http;
using Push.Foundation.Utilities.Helpers;


namespace Interco.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Interco Console App");
            Console.WriteLine($"++++++++++++++++++");
            Console.WriteLine($"Advanced Solutions Co. / AcuCode LLC");
            Console.WriteLine(Environment.NewLine + "Hit any key to begin");
            Console.ReadKey();

            // Execute each process separately
            //
            //RunPOOrderToSOOrder();
            //RunSOOrderToSOPayment();
            RunSOShipmentToPOReceipt();
            //RunExceptionHandlingTest();
            //RunARInvoiceToAPBill();

            Console.WriteLine(Environment.NewLine + "Hit any key to Close");
            Console.ReadKey();
        }


        private static AcumaticaCredentials CredentialsForUSCo()
        {
            //InterCo18112
            //Acu20201

            return new AcumaticaCredentials
            {
                CompanyName = "Company",
                Branch = "ProdRetail",
                InstanceUrl = "http://localhost/Acu20201",
                Username = "admin",
                Password = "1ascadmin!",
                VersionSegment = "/entity/ASCInterco/18.200.001/",
                Timeout = 300,
                MaxAttempts = 3
            };
        }

        private static AcumaticaCredentials CredentialsForCanadaCo()
        {
            return new AcumaticaCredentials
            {
                CompanyName = "Company2",
                Branch = "ProdRetail",
                InstanceUrl = "http://localhost/Acu20201",
                Username = "admin",
                Password = "1ascadmin!",
                VersionSegment = "/entity/ASCInterco/18.200.001/",
                Timeout = 300,
                MaxAttempts = 3

            };
        }

        private static TransferManager BuildTransferMgr(
                        AcumaticaCredentials source, AcumaticaCredentials destination)
        {
            var SourceSettings = new AcumaticaHttpConfig
            {
                VersionSegment = "/entity/ASCInterco/18.200.001/",
                MaxAttempts = 1,
                Timeout = 300
            };

            var DestinationSettings = new AcumaticaHttpConfig
            {
                VersionSegment = "/entity/ASCInterco/18.200.001/",
                MaxAttempts = 1,
                Timeout = 300
            };

            SourceSettings.VersionSegment = source.VersionSegment;
            SourceSettings.Timeout = source.Timeout;
            SourceSettings.MaxAttempts = source.MaxAttempts;

            DestinationSettings.VersionSegment = destination.VersionSegment;
            DestinationSettings.Timeout = destination.Timeout;
            DestinationSettings.MaxAttempts = destination.MaxAttempts;


            var manager = new TransferManager();
            manager.SourceCredentials = source;
            manager.SourceSettings = SourceSettings;
            manager.DestinationCredentials = destination;
            manager.DestinationSettings = DestinationSettings;

            // Uncomment this line to enable NLogger
            manager.EnableNLogger();
            manager.Initialize();
            return manager;
        }


        private static void RunPOOrderToSOOrder()
        {
            // Transfer US PO => Canada SO + Payment
            //
            var poNumber = "PO001333";
            var poType = PurchaseOrderType.DropShip;
            var destSalesOrderCustId = "ABARTENDE";    // TODO - this will be replaced by Web Service call aka WhoAmI?

            Console.WriteLine(Environment.NewLine + "*** Transfer POOrder to SOSalesOrder ***");
            Console.WriteLine("(NOTE: make sure the PO is On Hold, else the Vendor Ref won't get updated)");

            // Get the PO Number
            Console.WriteLine($"Enter the PO Number (default is {poNumber})");
            poNumber = Console.ReadLine().IsNullOrEmptyAlt(poNumber);

            // Get the PO Type
            Console.WriteLine($"Enter the PO Type (default is {poType})");
            poType = Console.ReadLine().IsNullOrEmptyAlt(poType);

            RunPOOrderToSOOrder(
                CredentialsForUSCo(), CredentialsForCanadaCo(),
                destSalesOrderCustId, poNumber, poType);
        }

        private static void RunSOOrderToSOPayment()
        {
            // Transfer US PO => Canada SO + Payment
            //
            var orderNumber = "SO005817";
            var orderType = "SO";

            Console.WriteLine(Environment.NewLine + "*** Create ARPayment for POOrder Created ***");
            Console.WriteLine("(NOTE: make sure the PO is On Hold, else the Vendor Ref won't get updated)");

            // Get the PO Number
            Console.WriteLine($"Enter the Sales Order (default is {orderNumber})");
            orderNumber = Console.ReadLine().IsNullOrEmptyAlt(orderNumber);

            // Get the PO Type
            Console.WriteLine($"Enter the Order Type (default is {orderType})");
            orderType = Console.ReadLine().IsNullOrEmptyAlt(orderType);

            RunSOOrderToSOPayment(
                CredentialsForUSCo(), CredentialsForCanadaCo(),
                orderNumber, orderType);
        }

        private static void RunPOOrderToSOOrder(
                AcumaticaCredentials source, AcumaticaCredentials destination,
                string destSalesOrderCustId, string poNumber, string poType)
        {
            var manager = BuildTransferMgr(source, destination);
            var salesOrderContext = new SOOrderSyncContext
            {
                PurchaseOrderNbr = poNumber,
                PurchaseOrderType = poType,
                DestSalesOrderCustId = destSalesOrderCustId,
            };
            manager.POOrderToSOOrder(salesOrderContext);

            Console.WriteLine($"Sales Order successfully created: {salesOrderContext.CreatedSalesOrder}");
            Console.WriteLine($"Sales Order Nbr: {salesOrderContext.SalesOrder.OrderNbr}");

            var paymentContext = new SOPaymentSyncContext()
            {
                SalesOrderNbr = salesOrderContext.SalesOrder.OrderNbr.value,
                SalesOrderType = salesOrderContext.SalesOrder.OrderType.value,
            };
            manager.SOOrderToSOPayment(paymentContext);

            Console.WriteLine($"Payment succesfully created: {paymentContext.CreatedPayment}");
            Console.WriteLine($"Payment Ref Nbr: {paymentContext.Payment.ReferenceNbr}");
        }


        private static void RunSOOrderToSOPayment(
                AcumaticaCredentials source, AcumaticaCredentials destination,
                string OrderNbr, string OrderType)
        {
            var manager = BuildTransferMgr(source, destination);

            var paymentContext = new SOPaymentSyncContext()
            {
                SalesOrderNbr = OrderNbr,
                SalesOrderType = OrderType,
            };
            manager.SOOrderToSOPayment(paymentContext);

            Console.WriteLine($"Payment succesfully created: {paymentContext.CreatedPayment}");
            Console.WriteLine($"Payment Ref Nbr: {paymentContext.Payment.ReferenceNbr}");
        }


        private static void RunSOShipmentToPOReceipt()
        {
            // Generate Purchase Receipt from the Shipment Tracking Information
            //
            var shipmentNbr = "003423";
            Console.WriteLine(Environment.NewLine + "*** Transfer Shipment to Purchase Receipt ***");
            Console.WriteLine("(NOTE: make sure the Purchase Order is Open and Approved)");
            Console.WriteLine($"Enter the Shipment Number (default is {shipmentNbr})");
            shipmentNbr = Console.ReadLine().IsNullOrEmptyAlt(shipmentNbr);

            RunSOShipmentToPOReceipt(CredentialsForCanadaCo(), CredentialsForUSCo(), shipmentNbr);
        }

        private static void RunSOShipmentToPOReceipt(
                AcumaticaCredentials source, AcumaticaCredentials destination, string shipmentNbr)
        {
            // *** Notice, this time the source is Canada
            //
            var manager = BuildTransferMgr(source, destination);
            var context = new PurchaseReceiptSyncContext
            {
                FulfillingShipmentNbr = shipmentNbr,
            };
            manager.SOShipmentToPOReceipt(context);

            Console.WriteLine($"Purchase Receipt Created: {context.CreatedPurchaseReceipt}");
        }


        private static void RunARInvoiceToAPBill()
        {
            // Generate Purchase Receipt from the Shipment Tracking Information
            //
            var referenceNbr = "AR005793";
            Console.WriteLine(Environment.NewLine + "*** Transfer AR Invoice to AP Bill ***");
            Console.WriteLine($"Enter the Invoice Reference Nbr (default is {referenceNbr})");
            referenceNbr = Console.ReadLine().IsNullOrEmptyAlt(referenceNbr);

            RunARInvoiceToAPBill(
                CredentialsForUSCo(), CredentialsForCanadaCo(), referenceNbr);
        }

        private static void RunARInvoiceToAPBill(
                AcumaticaCredentials source, AcumaticaCredentials destination, string referenceNbr)
        {
            var manager = BuildTransferMgr(source, destination);
            var context = new InvoiceContext
            {
                InvoiceReferenceNbr = referenceNbr,
                InvoiceDocType = ArInvoiceDocType.Invoice,
                DestVendorCustId = "ICUS",
            };

            manager.ARInvoiceToAPBill(context);
            Console.WriteLine($"TEST TEST TEST");
        }

        private static void RunExceptionHandlingTest()
        {
            Console.WriteLine(Environment.NewLine + "*** Demonstrating Exception Handling ***");

            var manager = BuildTransferMgr(CredentialsForUSCo(), CredentialsForCanadaCo());

            var context = new SOOrderSyncContext
            {
                PurchaseOrderNbr = "PLEASETHROWERROR",
                PurchaseOrderType = PurchaseOrderType.Normal,
                DestSalesOrderCustId = "ANOTHERERROR",
            };
            manager.POOrderToSOOrder(context);

            if (context.ExceptionThrown)
            {
                Console.WriteLine(
                    $"An exception was thrown and contained: {context.Exception.Message}");
            }
        }
    }
}

