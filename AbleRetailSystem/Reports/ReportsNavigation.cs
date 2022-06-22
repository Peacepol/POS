using RestaurantPOS.Reports;
using RestaurantPOS.Reports.PurchaseReports;
using RestaurantPOS.Reports.SalesReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS
{
    public partial class ReportsNavigation : Form
    {
        public ReportsNavigation()
        {
            InitializeComponent();
        }

        private void ReportsNavigation_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Sales Reports
            if (listSalesReports.SelectedIndex == 1)//Sales Summary
            {
                if (CommonClass.SalesReportSummaryCustomizer == null || CommonClass.SalesReportSummaryCustomizer.IsDisposed)
                {
                    CommonClass.SalesReportSummaryCustomizer = new rptCusomizerSalesSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SalesReportSummaryCustomizer.MdiParent = this.MdiParent;
                CommonClass.SalesReportSummaryCustomizer.Show();
                CommonClass.SalesReportSummaryCustomizer.Focus();
                if (CommonClass.SalesReportSummaryCustomizer.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SalesReportSummaryCustomizer.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 2)//Sales Detail
            {
                if (CommonClass.SalesReportDetail == null || CommonClass.SalesReportDetail.IsDisposed)
                {
                    CommonClass.SalesReportDetail = new rptSalesReportDetail();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SalesReportDetail.MdiParent = this.MdiParent;
                CommonClass.SalesReportDetail.Show();
                CommonClass.SalesReportDetail.Focus();
                if (CommonClass.SalesReportDetail.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SalesReportDetail.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 3)
            {
                if (CommonClass.SalesItemSummary == null || CommonClass.SalesItemSummary.IsDisposed)
                {
                    CommonClass.SalesItemSummary = new RptSalesItemSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SalesItemSummary.MdiParent = this.MdiParent;
                CommonClass.SalesItemSummary.Show();
                CommonClass.SalesItemSummary.Focus();
                if (CommonClass.SalesItemSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SalesItemSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 4)
            {
                if (CommonClass.SalesItemDetails == null || CommonClass.SalesItemDetails.IsDisposed)
                {
                    CommonClass.SalesItemDetails = new RptSalesItemDetails();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SalesItemDetails.MdiParent = this.MdiParent;
                CommonClass.SalesItemDetails.Show();
                CommonClass.SalesItemDetails.Focus();
                if (CommonClass.SalesItemDetails.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SalesItemDetails.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 5)//analyse Sale
            {
                if (CommonClass.SalesReportAnalyse == null || CommonClass.SalesReportAnalyse.IsDisposed)
                {
                    CommonClass.SalesReportAnalyse = new Reports.RptAnalyseSales();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SalesReportAnalyse.MdiParent = this.MdiParent;
                CommonClass.SalesReportAnalyse.Show();
                CommonClass.SalesReportAnalyse.Focus();
                if (CommonClass.SalesReportAnalyse.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SalesReportAnalyse.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 6)
            {
                if (CommonClass.AnalyseSalesFY == null || CommonClass.AnalyseSalesFY.IsDisposed)
                {
                    CommonClass.AnalyseSalesFY = new RptAnalyseSalesFY();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AnalyseSalesFY.MdiParent = this.MdiParent;
                CommonClass.AnalyseSalesFY.Show();
                CommonClass.AnalyseSalesFY.Focus();
                if (CommonClass.AnalyseSalesFY.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AnalyseSalesFY.Close();
                }
            }
            if (listSalesReports.SelectedIndex == 7)
            {
                if (CommonClass.CustomerPayment == null || CommonClass.CustomerPayment.IsDisposed)
                {
                    CommonClass.CustomerPayment = new RptCustomerPayments();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.CustomerPayment.MdiParent = this.MdiParent;
                CommonClass.CustomerPayment.Show();
                CommonClass.CustomerPayment.Focus();
                if (CommonClass.CustomerPayment.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.CustomerPayment.Close();

                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 8)
            {
                if (CommonClass.CustomerLedger == null || CommonClass.CustomerLedger.IsDisposed)
                {
                    CommonClass.CustomerLedger = new RptCustomerLedger();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.CustomerLedger.MdiParent = this.MdiParent;
                CommonClass.CustomerLedger.Show();
                CommonClass.CustomerLedger.Focus();
                if (CommonClass.CustomerLedger.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.CustomerLedger.Close();
                }
                this.Cursor = Cursors.Default;
            }

            if (listSalesReports.SelectedIndex == 11)
            {
                if (CommonClass.AllSales == null || CommonClass.AllSales.IsDisposed)
                {
                    CommonClass.AllSales = new RptAllSales();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AllSales.MdiParent = this.MdiParent;
                CommonClass.AllSales.Show();
                CommonClass.AllSales.Focus();
                if (CommonClass.AllSales.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AllSales.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 12)
            {
                if (CommonClass.ClosedInvoices == null || CommonClass.ClosedInvoices.IsDisposed)
                {
                    CommonClass.ClosedInvoices = new RptClosedInvoices();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ClosedInvoices.MdiParent = this.MdiParent;
                CommonClass.ClosedInvoices.Show();
                CommonClass.ClosedInvoices.Focus();
                if (CommonClass.ClosedInvoices.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ClosedInvoices.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 13)
            {
                if (CommonClass.OpenInvoiceOrder == null || CommonClass.OpenInvoiceOrder.IsDisposed)
                {
                    CommonClass.OpenInvoiceOrder = new RptOpenInvoiceOrder();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.OpenInvoiceOrder.MdiParent = this.MdiParent;
                CommonClass.OpenInvoiceOrder.Show();
                CommonClass.OpenInvoiceOrder.Focus();
                if (CommonClass.OpenInvoiceOrder.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.OpenInvoiceOrder.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 14)
            {
                if (CommonClass.RptSalesOrder == null || CommonClass.RptSalesOrder.IsDisposed)
                {
                    CommonClass.RptSalesOrder = new RptSalesOrder();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptSalesOrder.MdiParent = this.MdiParent;
                CommonClass.RptSalesOrder.Show();
                CommonClass.RptSalesOrder.Focus();
                if (CommonClass.RptSalesOrder.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptSalesOrder.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 15)
            {
                if (CommonClass.Quotes == null || CommonClass.Quotes.IsDisposed)
                {
                    CommonClass.Quotes = new RptQuotes();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.Quotes.MdiParent = this.MdiParent;
                CommonClass.Quotes.Show();
                CommonClass.Quotes.Focus();
                if (CommonClass.Quotes.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.Quotes.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 16)
            {
                if (CommonClass.LayByReportCustomizer == null || CommonClass.LayByReportCustomizer.IsDisposed)
                {
                    CommonClass.LayByReportCustomizer = new LayByReportCustomizer();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.LayByReportCustomizer.MdiParent = this.MdiParent;
                CommonClass.LayByReportCustomizer.Show();
                CommonClass.LayByReportCustomizer.Focus();
                if (CommonClass.LayByReportCustomizer.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.LayByReportCustomizer.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 19)
            {
                if (CommonClass.AgeReceivableSummary == null || CommonClass.AgeReceivableSummary.IsDisposed)
                {
                    CommonClass.AgeReceivableSummary = new RptAgeReceivableSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AgeReceivableSummary.MdiParent = this.MdiParent;
                CommonClass.AgeReceivableSummary.Show();
                CommonClass.AgeReceivableSummary.Focus();
                if (CommonClass.AgeReceivableSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AgeReceivableSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 20)
            {
                if (CommonClass.AgeReceivableDetail == null || CommonClass.AgeReceivableDetail.IsDisposed)
                {
                    CommonClass.AgeReceivableDetail = new RptAgeReceivableDetail();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AgeReceivableDetail.MdiParent = this.MdiParent;
                CommonClass.AgeReceivableDetail.Show();
                CommonClass.AgeReceivableDetail.Focus();
                if (CommonClass.AgeReceivableDetail.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AgeReceivableDetail.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 21)
            {
                if (CommonClass.SalesSummarywithTax == null || CommonClass.SalesSummarywithTax.IsDisposed)
                {
                    CommonClass.SalesSummarywithTax = new Reports.SalesReports.SalesSummarywithTax();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SalesSummarywithTax.MdiParent = this.MdiParent;
                CommonClass.SalesSummarywithTax.Show();
                CommonClass.SalesSummarywithTax.Focus();
                if (CommonClass.SalesSummarywithTax.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SalesSummarywithTax.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 24)
            {
                if (CommonClass.SalesStatement == null || CommonClass.SalesStatement.IsDisposed)
                {
                    CommonClass.SalesStatement = new SalesStatement();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SalesStatement.MdiParent = this.MdiParent;
                CommonClass.SalesStatement.Show();
                CommonClass.SalesStatement.Focus();
                if (CommonClass.SalesStatement.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SalesStatement.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 25)
            {
                if (CommonClass.RptInvoicesTransaction == null || CommonClass.RptInvoicesTransaction.IsDisposed)
                {
                    CommonClass.RptInvoicesTransaction = new RptInvoiceTransaction();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptInvoicesTransaction.MdiParent = this.MdiParent;
                CommonClass.RptInvoicesTransaction.Show();
                CommonClass.RptInvoicesTransaction.Focus();
                if (CommonClass.RptInvoicesTransaction.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptInvoicesTransaction.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 28)
            {
                if (CommonClass.RptReceivableJournal == null || CommonClass.RptReceivableJournal.IsDisposed)
                {
                    CommonClass.RptReceivableJournal = new Reports.SalesReports.RptReceivableJournal();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptReceivableJournal.MdiParent = this.MdiParent;
                CommonClass.RptReceivableJournal.Show();
                CommonClass.RptReceivableJournal.Focus();
                if (CommonClass.RptReceivableJournal.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptReceivableJournal.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSalesReports.SelectedIndex == 29)
            {
                if (CommonClass.PrintSalesReceipts == null || CommonClass.PrintSalesReceipts.IsDisposed)
                {
                    CommonClass.PrintSalesReceipts = new Sales.PrintSalesReceipts();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PrintSalesReceipts.MdiParent = this.MdiParent;
                CommonClass.PrintSalesReceipts.Show();
                CommonClass.PrintSalesReceipts.Focus();
                if (CommonClass.PrintSalesReceipts.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PrintSalesReceipts.Close();
                }
                this.Cursor = Cursors.Default;
            }



            //Inventory Reports
            if (listInventoryReports.SelectedIndex == 1)
            {
                if (CommonClass.ItemReportSummary == null
                || CommonClass.ItemReportSummary.IsDisposed)
                {
                    CommonClass.ItemReportSummary = new Reports.InventoryReports.RptItemSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ItemReportSummary.MdiParent = this.MdiParent;
                CommonClass.ItemReportSummary.Show();
                CommonClass.ItemReportSummary.Focus();
                if (CommonClass.ItemReportSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ItemReportSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 2)
            {
                if (CommonClass.ItemReportDetails == null
                            || CommonClass.ItemReportDetails.IsDisposed)
                {
                    CommonClass.ItemReportDetails = new Reports.InventoryReports.RptItemDetails();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ItemReportDetails.MdiParent = this.MdiParent;
                CommonClass.ItemReportDetails.Show();
                CommonClass.ItemReportDetails.Focus();
                if (CommonClass.ItemReportDetails.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ItemReportDetails.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 3)
            {
                if (CommonClass.AnalyseInventorySum == null
                           || CommonClass.AnalyseInventorySum.IsDisposed)
                {
                    CommonClass.AnalyseInventorySum = new Reports.InventoryReports.AnalyseInventorySum();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AnalyseInventorySum.MdiParent = this.MdiParent;
                CommonClass.AnalyseInventorySum.Show();
                CommonClass.AnalyseInventorySum.Focus();
                if (CommonClass.AnalyseInventorySum.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AnalyseInventorySum.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 4)
            {
                if (CommonClass.AnalyseInventoryDetail == null
                            || CommonClass.AnalyseInventoryDetail.IsDisposed)
                {
                    CommonClass.AnalyseInventoryDetail = new Reports.InventoryReports.AnalyseInventoryDetail();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AnalyseInventoryDetail.MdiParent = this.MdiParent;
                CommonClass.AnalyseInventoryDetail.Show();
                CommonClass.AnalyseInventoryDetail.Focus();
                if (CommonClass.AnalyseInventoryDetail.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AnalyseInventoryDetail.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 5)
            {
                if (CommonClass.AutoBuildReport == null || CommonClass.AutoBuildReport.IsDisposed)
                {
                    CommonClass.AutoBuildReport = new Reports.InventoryReports.AutoBuildReport();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AutoBuildReport.MdiParent = this.MdiParent;
                CommonClass.AutoBuildReport.Show();
                CommonClass.AutoBuildReport.Focus();
                if (CommonClass.AutoBuildReport.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AutoBuildReport.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 6)
            {
                if (CommonClass.ItemCountCustomizer == null || CommonClass.ItemCountCustomizer.IsDisposed)
                {
                    CommonClass.ItemCountCustomizer = new Reports.InventoryReports.ItemCountCustomizer();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ItemCountCustomizer.MdiParent = this.MdiParent;
                CommonClass.ItemCountCustomizer.Show();
                CommonClass.ItemCountCustomizer.Focus();
                if (CommonClass.ItemCountCustomizer.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ItemCountCustomizer.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 7)
            {
                if (CommonClass.ItemReportRegisterSummary == null || CommonClass.ItemReportRegisterSummary.IsDisposed)
                {
                    CommonClass.ItemReportRegisterSummary = new Reports.InventoryReports.RptItemRegisterSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ItemReportRegisterSummary.MdiParent = this.MdiParent;
                CommonClass.ItemReportRegisterSummary.Show();
                CommonClass.ItemReportRegisterSummary.Focus();
                if (CommonClass.ItemReportRegisterSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ItemReportRegisterSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 8)
            {
                if (CommonClass.ItemReportTransactions == null || CommonClass.ItemReportTransactions.IsDisposed)
                {
                    CommonClass.ItemReportTransactions = new Reports.InventoryReports.RptItemTransactions();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ItemReportTransactions.MdiParent = this.MdiParent;
                CommonClass.ItemReportTransactions.Show();
                CommonClass.ItemReportTransactions.Focus();
                if (CommonClass.ItemReportTransactions.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ItemReportTransactions.Close();
                }
                this.Cursor = Cursors.Default;
            }

            if (listInventoryReports.SelectedIndex == 9)
            {
                if (CommonClass.BestSellingItem == null || CommonClass.BestSellingItem.IsDisposed)
                {
                    CommonClass.BestSellingItem = new Reports.InventoryReports.BestSellingItem();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.BestSellingItem.MdiParent = this.MdiParent;
                CommonClass.BestSellingItem.Show();
                CommonClass.BestSellingItem.Focus();
                if (CommonClass.BestSellingItem.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.BestSellingItem.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 10)
            {
                if (CommonClass.CategoryList == null || CommonClass.CategoryList.IsDisposed)
                {
                    CommonClass.CategoryList = new Reports.InventoryReports.RptCategoryList();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.CategoryList.MdiParent = this.MdiParent;
                CommonClass.CategoryList.Show();
                CommonClass.CategoryList.Focus();
                if (CommonClass.CategoryList.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.CategoryList.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 13)
            {
                if (CommonClass.PriceReportDetails == null
           || CommonClass.PriceReportDetails.IsDisposed)
                {
                    CommonClass.PriceReportDetails = new Reports.InventoryReports.RptPriceDetails();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PriceReportDetails.MdiParent = this.MdiParent;
                CommonClass.PriceReportDetails.Show();
                CommonClass.PriceReportDetails.Focus();
                if (CommonClass.PriceReportDetails.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PriceReportDetails.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listInventoryReports.SelectedIndex == 14)
            {
                if (CommonClass.PriceReportAnalysis == null
                           || CommonClass.PriceReportAnalysis.IsDisposed)
                {
                    CommonClass.PriceReportAnalysis = new Reports.InventoryReports.RptPriceAnalysis();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PriceReportAnalysis.MdiParent = this.MdiParent;
                CommonClass.PriceReportAnalysis.Show();
                CommonClass.PriceReportAnalysis.Focus();
                if (CommonClass.PriceReportAnalysis.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PriceReportAnalysis.Close();
                }
                this.Cursor = Cursors.Default;
            }

            //Purchases Reports
            if (listPurchasesReports.SelectedIndex == 1)
            {
                if (CommonClass.PurchaseReportSummary == null
                           || CommonClass.PurchaseReportSummary.IsDisposed)
                {
                    CommonClass.PurchaseReportSummary = new rptPurchaseReportSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PurchaseReportSummary.MdiParent = this.MdiParent;
                CommonClass.PurchaseReportSummary.Show();
                CommonClass.PurchaseReportSummary.Focus();
                if (CommonClass.PurchaseReportSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PurchaseReportSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPurchasesReports.SelectedIndex == 2)
            {
                if (CommonClass.PurchaseReportDetails == null
                       || CommonClass.PurchaseReportDetails.IsDisposed)
                {
                    CommonClass.PurchaseReportDetails = new PurchaseReportDetails();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PurchaseReportDetails.MdiParent = this.MdiParent;
                CommonClass.PurchaseReportDetails.Show();
                CommonClass.PurchaseReportDetails.Focus();
                if (CommonClass.PurchaseReportDetails.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PurchaseReportDetails.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPurchasesReports.SelectedIndex == 3)
            {
                if (CommonClass.AnalysePurchaseRptfrm == null
            || CommonClass.AnalysePurchaseRptfrm.IsDisposed)
                {
                    CommonClass.AnalysePurchaseRptfrm = new AnalysePurchaseRpt();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AnalysePurchaseRptfrm.MdiParent = this.MdiParent;
                CommonClass.AnalysePurchaseRptfrm.Show();
                CommonClass.AnalysePurchaseRptfrm.Focus();
                if (CommonClass.AnalysePurchaseRptfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AnalysePurchaseRptfrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPurchasesReports.SelectedIndex == 4)
            {
                if (CommonClass.AnalysePuchaseFYComparison == null
            || CommonClass.AnalysePuchaseFYComparison.IsDisposed)
                {
                    CommonClass.AnalysePuchaseFYComparison = new AnalysePuchaseFYComparison();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AnalysePuchaseFYComparison.MdiParent = this.MdiParent;
                CommonClass.AnalysePuchaseFYComparison.Show();
                CommonClass.AnalysePuchaseFYComparison.Focus();
                if (CommonClass.AnalysePuchaseFYComparison.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AnalysePuchaseFYComparison.Close();
                }
                this.Cursor = Cursors.Default;
            }

            if (listPurchasesReports.SelectedIndex == 7)
            {
                if (CommonClass.AllPurchaseReport == null
                || CommonClass.AllPurchaseReport.IsDisposed)
                {
                    CommonClass.AllPurchaseReport = new AllPurchaseReport();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.AllPurchaseReport.MdiParent = this.MdiParent;
                CommonClass.AllPurchaseReport.Show();
                CommonClass.AllPurchaseReport.Focus();
                if (CommonClass.AllPurchaseReport.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.AllPurchaseReport.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPurchasesReports.SelectedIndex == 8)
            {
                if (CommonClass.ReceivedOrdersSummary == null
                || CommonClass.ReceivedOrdersSummary.IsDisposed)
                {
                    CommonClass.ReceivedOrdersSummary = new ReceivedOrdersSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ReceivedOrdersSummary.MdiParent = this.MdiParent;
                CommonClass.ReceivedOrdersSummary.Show();
                CommonClass.ReceivedOrdersSummary.Focus();
                if (CommonClass.ReceivedOrdersSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ReceivedOrdersSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPurchasesReports.SelectedIndex == 9)
            {
                if (CommonClass.ReceivedOrdersDetail == null
                || CommonClass.ReceivedOrdersDetail.IsDisposed)
                {
                    CommonClass.ReceivedOrdersDetail = new ReceivedOrdersDetail();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ReceivedOrdersDetail.MdiParent = this.MdiParent;
                CommonClass.ReceivedOrdersDetail.Show();
                CommonClass.ReceivedOrdersDetail.Focus();
                if (CommonClass.ReceivedOrdersDetail.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ReceivedOrdersDetail.Close();
                }
                this.Cursor = Cursors.Default;
            }




            if (listPurchasesReports.SelectedIndex == 12)
            {
                if (CommonClass.rptSupplierItemSummary == null
      || CommonClass.rptSupplierItemSummary.IsDisposed)
                {
                    CommonClass.rptSupplierItemSummary = new RptSupplierItemSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.rptSupplierItemSummary.MdiParent = this.MdiParent;
                CommonClass.rptSupplierItemSummary.Show();
                CommonClass.rptSupplierItemSummary.Focus();
                if (CommonClass.rptSupplierItemSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.rptSupplierItemSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPurchasesReports.SelectedIndex == 13)
            {
                if (CommonClass.RptSupplierItemDetail == null
                || CommonClass.RptSupplierItemDetail.IsDisposed)
                {
                    CommonClass.RptSupplierItemDetail = new RptSupplierItemDetail();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptSupplierItemDetail.MdiParent = this.MdiParent;
                CommonClass.RptSupplierItemDetail.Show();
                CommonClass.RptSupplierItemDetail.Focus();
                if (CommonClass.RptSupplierItemDetail.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptSupplierItemDetail.Close();
                }
                this.Cursor = Cursors.Default;
            }



            //Tax Reports
            if (listTaxReport.SelectedIndex == 1)
            {
                if (CommonClass.RptTaxTransactionsSFrm == null
                                || CommonClass.RptTaxTransactionsSFrm.IsDisposed)
                {
                    CommonClass.RptTaxTransactionsSFrm = new RptTaxTransactionsS();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptTaxTransactionsSFrm.MdiParent = this.MdiParent;
                CommonClass.RptTaxTransactionsSFrm.Show();
                CommonClass.RptTaxTransactionsSFrm.Focus();
                if (CommonClass.RptTaxTransactionsSFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptTaxTransactionsSFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listTaxReport.SelectedIndex == 2)
            {
                if (CommonClass.RptTaxTransactionsDFrm == null
                    || CommonClass.RptTaxTransactionsDFrm.IsDisposed)
                {
                    CommonClass.RptTaxTransactionsDFrm = new RptTaxTransactionsD();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptTaxTransactionsDFrm.MdiParent = this.MdiParent;
                CommonClass.RptTaxTransactionsDFrm.Show();
                CommonClass.RptTaxTransactionsDFrm.Focus();
                if (CommonClass.RptTaxTransactionsDFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptTaxTransactionsDFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }

            //Job Reports
            if (listJobReports.SelectedIndex == 1)
            {
                if (CommonClass.RptJobListFrm == null
                || CommonClass.RptJobListFrm.IsDisposed)
                {
                    CommonClass.RptJobListFrm = new RptJobList();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptJobListFrm.MdiParent = this.MdiParent;
                CommonClass.RptJobListFrm.Show();
                CommonClass.RptJobListFrm.Focus();
                if (CommonClass.RptJobListFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptJobListFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listJobReports.SelectedIndex == 2)
            {
                if (CommonClass.RptActivityOptionsFrm == null
                   || CommonClass.RptActivityOptionsFrm.IsDisposed)
                {
                    CommonClass.RptActivityOptionsFrm = new Reports.RptActivityOptions();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptActivityOptionsFrm.MdiParent = this.MdiParent;
                CommonClass.RptActivityOptionsFrm.Show();
                CommonClass.RptActivityOptionsFrm.Focus();
                if (CommonClass.RptActivityOptionsFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptActivityOptionsFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listJobReports.SelectedIndex == 3)
            {
                if (CommonClass.RptJobTransactionsOptionsFrm == null
                  || CommonClass.RptJobTransactionsOptionsFrm.IsDisposed)
                {
                    CommonClass.RptJobTransactionsOptionsFrm = new Reports.RptJobTransactionsOptions();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptJobTransactionsOptionsFrm.MdiParent = this.MdiParent;
                CommonClass.RptJobTransactionsOptionsFrm.Show();
                CommonClass.RptJobTransactionsOptionsFrm.Focus();
                if (CommonClass.RptJobTransactionsOptionsFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptJobTransactionsOptionsFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listJobReports.SelectedIndex == 4)
            {
                if (CommonClass.RptJobProfitAndLossFrm == null
               || CommonClass.RptJobProfitAndLossFrm.IsDisposed)
                {
                    CommonClass.RptJobProfitAndLossFrm = new RptJobProfitAndLoss();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptJobProfitAndLossFrm.MdiParent = this.MdiParent;
                CommonClass.RptJobProfitAndLossFrm.Show();
                CommonClass.RptJobProfitAndLossFrm.Focus();
                if (CommonClass.RptJobProfitAndLossFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptJobProfitAndLossFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }

            //Customer & Supplier Reports
            if (listCustomerSupplierReports.SelectedIndex == 1)
            {
                if (CommonClass.RptProfileListOptionsFrm == null
                  || CommonClass.RptProfileListOptionsFrm.IsDisposed)
                {
                    CommonClass.RptProfileListOptionsFrm = new Reports.RptProfileListOptions();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptProfileListOptionsFrm.MdiParent = this.MdiParent;
                CommonClass.RptProfileListOptionsFrm.Show();
                CommonClass.RptProfileListOptionsFrm.Focus();
                if (CommonClass.RptProfileListOptionsFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptProfileListOptionsFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listCustomerSupplierReports.SelectedIndex == 2)
            {
                if (CommonClass.RptLoyaltyMembers == null
                  || CommonClass.RptLoyaltyMembers.IsDisposed)
                {
                    CommonClass.RptLoyaltyMembers = new Reports.RptLoyaltyMembers();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptLoyaltyMembers.MdiParent = this.MdiParent;
                CommonClass.RptLoyaltyMembers.Show();
                CommonClass.RptLoyaltyMembers.Focus();
                if (CommonClass.RptLoyaltyMembers.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptLoyaltyMembers.Close();
                }
                this.Cursor = Cursors.Default;
            }

            //POS Summary Report
            if (listSummaryReport.SelectedIndex == 1)
            {
                if (CommonClass.RptSessionCustomizer == null
                  || CommonClass.RptSessionCustomizer.IsDisposed)
                {
                    CommonClass.RptSessionCustomizer = new Reports.SessionReportsCustomizer();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptSessionCustomizer.MdiParent = this.MdiParent;
                CommonClass.RptSessionCustomizer.Show();
                CommonClass.RptSessionCustomizer.Focus();
                if (CommonClass.RptSessionCustomizer.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptSessionCustomizer.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSummaryReport.SelectedIndex == 3)
            {
                if (CommonClass.RptTenderDetails == null
                  || CommonClass.RptTenderDetails.IsDisposed)
                {
                    CommonClass.RptTenderDetails = new Reports.RptTenderDetails();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptTenderDetails.MdiParent = this.MdiParent;
                CommonClass.RptTenderDetails.Show();
                CommonClass.RptTenderDetails.Focus();
                if (CommonClass.RptTenderDetails.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptTenderDetails.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listSummaryReport.SelectedIndex == 2)
            {
                if (CommonClass.RptTenderSummary == null
                  || CommonClass.RptTenderSummary.IsDisposed)
                {
                    CommonClass.RptTenderSummary = new Reports.RptTenderSummary();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.RptTenderSummary.MdiParent = this.MdiParent;
                CommonClass.RptTenderSummary.Show();
                CommonClass.RptTenderSummary.Focus();
                if (CommonClass.RptTenderSummary.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.RptTenderSummary.Close();
                }
                this.Cursor = Cursors.Default;
            }
            // listCustomerSupplierReports.ClearSelected();

            //Promotion And Discount
            if (listPromotionAndDiscount.SelectedIndex == 1)
            {
                if (CommonClass.salesByPromos == null
                  || CommonClass.salesByPromos.IsDisposed)
                {
                    CommonClass.salesByPromos = new Reports.PromotionDiscountReports.SalesByPromos();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.salesByPromos.MdiParent = this.MdiParent;
                CommonClass.salesByPromos.Show();
                CommonClass.salesByPromos.Focus();
                if (CommonClass.salesByPromos.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.salesByPromos.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPromotionAndDiscount.SelectedIndex == 2)
            {
                if (CommonClass.discountPromos == null
                  || CommonClass.discountPromos.IsDisposed)
                {
                    CommonClass.discountPromos = new Reports.PromotionDiscountReports.DiscountPromos();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.discountPromos.MdiParent = this.MdiParent;
                CommonClass.discountPromos.Show();
                CommonClass.discountPromos.Focus();
                if (CommonClass.discountPromos.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.discountPromos.Close();
                }
                this.Cursor = Cursors.Default;
            }
            if (listPromotionAndDiscount.SelectedIndex == 3)
            {
                if (CommonClass.freeProductsReport == null
                  || CommonClass.freeProductsReport.IsDisposed)
                {
                    CommonClass.freeProductsReport = new Reports.PromotionDiscountReports.FreeProductsReport();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.freeProductsReport.MdiParent = this.MdiParent;
                CommonClass.freeProductsReport.Show();
                CommonClass.freeProductsReport.Focus();
                if (CommonClass.freeProductsReport.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.freeProductsReport.Close();
                }
                this.Cursor = Cursors.Default;
            }

        }

        private void listPurchasesReports_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void listSalesReports_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void ReportsContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSalesReports.ClearSelected();
            listPurchasesReports.ClearSelected();
            listInventoryReports.ClearSelected();
            listTaxReport.ClearSelected();
            listJobReports.ClearSelected();
            listCustomerSupplierReports.ClearSelected();
            listPromotionAndDiscount.ClearSelected();
            listSummaryReport.ClearSelected();
        }

        private void listSalesReports_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void listInventoryReports_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void listPurchasesReports_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void listTaxReport_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void listJobReports_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void listCustomerSupplierReports_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void listSummaryReport_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void listPromotionAndDiscount_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }
    }
}

