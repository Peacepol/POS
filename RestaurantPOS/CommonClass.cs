using CrystalDecisions;
using Microsoft.VisualBasic;
using mshtml;
using AbleRetailPOS;
using AbleRetailPOS.Inventory;
using AbleRetailPOS.Purchase;
using AbleRetailPOS.References;
using AbleRetailPOS.Reports;
using AbleRetailPOS.Sales;
using AbleRetailPOS.Setup;
using AbleRetailPOS.Utilities;
using AbleRetailPOS.Utilities;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS
{
    public static class CommonClass
    {
        public const int MAGICNO = 603777;

        //User Variables
        public static string UserName;
        public static string UserID;
        public static bool isSalesperson;
        public static bool isSupervisor;
        public static bool isAdministrator;
        public static bool isTechnician;
        public static string ShiftTimeSpan;
        public static int SessionID;
        public static float OpeningFund;
        public static float FloatFund;

        public static Dictionary<string, Dictionary<string, Boolean>> UserAccess;

        //FORMS DICTIONARY
        public static Dictionary<string, string> AppFormCode;
        //public static string taxcodeID;
        public static Dictionary<int, string> NumberMapping;
        public static Dictionary<int, string> MonthMapping;

        public static string taxcode;
        public static string accntnum;
        public static string profilenum;
        public static string Customernum;
        public static string Select_Profile_;
        public static string selectIncomeAccountList_;
        public static string accountNull; //moneyOut
        public static string JobNull; //moneyOut
        public static string TaxNull; //moneyOut
        public static string profile; //moneyOut
        public static string ConStr;
        public static string CompName;
        public static int CurFY;
        //public static int ThisYr;
        //public static int ConYr;
        //public static int FYLastMonth;
        //public static int FYBegMonth;
        //public static int FYConMonth;
        //public static DateTime FYEndDate;
        //public static DateTime FYBegDate;
        //public static DateTime FYConDate;
        public static bool IsPastFY = false;
        public static bool IsEditOK = false;
        public static bool AutoEnd = false;
        public static bool SessionRunning = false;
        public static string TerminalName;
        public static DateTime SessionDate;
        //public static string FYBegDateStr;
        public static int LCurrencyID;
        public static string LCurrency;
        public static string LCurSymbol;
        public static string DefaultCustomerID;
        //public static DataRow DRowLA;
        public static DataRow DRowPref;
        //public static DateTime LockedPeriod;
        //public static bool IsPeriodLocked = false;
        public static bool IsItemPriceInclusive = false;
        public static bool IsTaxcInclusiveEnterSales = false;
        public static bool MandatoryCustomer = true;
        public static bool KeepQuote = false;
        public static string CompAddress;
        public static string CompSalesTaxNo;
        public static IHTMLDocument2 doc;
        public static WebBrowser HTMLEditor = new WebBrowser();
        public static int MaxTerminalAllowed;
        public static string CompLogoPath = "";
        public enum InvocationSource
        {
            SELF = 1,
            REGISTER,
            CHANGETO,
            AUTOBUILD,
            STOCKTAKE,
            CUSTOMER,
            SUPPLIER,
            MONEYIN,
            MONEYOUT,
            SALES,
            PURCHASE,
            SAVERECURRING,
            USERECURRING,
            JOURNALENTRY,
            REMINDER,
            REMITADVICE,
            TODOLIST
        };

        public enum RedemptionType
        {
            GIFTCERTIFICATE = 0,
            ITEM,
            PRICEDISCOUNT
        };

        public enum RunSqlInsertMode
        {
            QUERY = 0,
            SCALAR
        }
        public enum FreeItemInvocation
        {
            POINTSACCUMULATION,
            ENTERSALES
        }

        //FORMS
        //REFERENCES
        public static ReferencesNP RefPanelFrm;
        public static CustomerList CustomerListFrm;
        public static SupplierList SupplierListFrm;
        public static Job JobFrm;
        public static CreateTaxCode TaxCodeFrm;
        public static CreatePaymentMethod PaymentMethodFrm;
        public static Currency CurrencyFrm;
        public static CreateShippingMethod ShippingMethodFrm;
        public static CreateTerms TermsFrm;
        public static CustomNames CustomNamesFrm;
        public static CustomList1 CustomList1Frm;
        public static CustomList2 CustomList2Frm;
        public static CustomList3 CustomList3Frm;
        public static GiftCertificateLookup GiftCertificateLookup;
        public static LoyaltyMemberList LoyaltyMemberLookup;

        //Accounts Command Centre
        public static TransactionJournal TranJournalFrm;
        public static TransactionsLookup TransactionsLookupFrm;

        //Bannking Command Centre
        public static SalesNP SalesPanelform;
        public static PurchaseNP PurchasePanelform;
        public static InventoryNP InventoryPanelfrm;

        //SALES
        public static SalesRegister SalesRegfrm;
        public static EnterSales EnterSalesfrm;
        public static SalesReceivePayment SRPaymentsfrm;
        public static EmailStatement EmailStatementfrm;
        public static PrintSalesReceipts PrintSalesReceipts;
        public static QuickSales QuickSalesfrm;
        public static PointsRedemption PointRedemption;
        public static ChangeAmount ChangeAmount;
        public static DialogPrint DialogPrint;
        public static RestoPOS RestoPOS;

        //SETUP
        public static SetupNP setupNP;
        //public static EmailAccount EmailAcctfrm;
        public static RedeemItem RedeemItemfrm;
        public static PointsAccumulation PointAccumulation;
        public static PointsExchangeRate PointExhangeRate;

        public static Reports.SalesReports.rptCusomizerSalesSummary SalesReportSummaryCustomizer;
        public static Reports.SalesReports.rptSalesReportDetail SalesReportDetail;
        public static Reports.RptAnalyseSales SalesReportAnalyse;
        public static Reports.SalesReports.RptAnalyseSalesFY AnalyseSalesFY;
        public static Reports.SalesReports.RptCustomerPayments CustomerPayment;
        public static Reports.SalesReports.RptCustomerLedger CustomerLedger;
        public static Reports.SalesReports.RptAllSales AllSales;
        public static Reports.SalesReports.RptClosedInvoices ClosedInvoices;
        public static Reports.SalesReports.RptOpenInvoiceOrder OpenInvoiceOrder;
        public static Reports.SalesReports.RptQuotes Quotes;
        public static Reports.SalesReports.RptSalesItemSummary SalesItemSummary;
        public static Reports.SalesReports.RptSalesItemDetails SalesItemDetails;
        public static Reports.SalesReports.RptAgeReceivableSummary AgeReceivableSummary;
        public static Reports.SalesReports.RptAgeReceivableDetail AgeReceivableDetail;
        public static Reports.SalesReports.RptReconciliationSummary RptReconciliationSummary;
        public static Reports.SalesReports.RptReconciliationDetail RptReconciliationDetail;
        public static Reports.SalesReports.SalesStatement SalesStatement;
        public static Reports.SalesReports.SalesSummarywithTax SalesSummarywithTax;
        public static Reports.SalesReports.RptReceivableJournal RptReceivableJournal;
        public static Reports.SalesReports.RptInvoiceTransaction RptInvoicesTransaction;
        public static Reports.SalesReports.LayByReportCustomizer LayByReportCustomizer;
        public static Reports.SalesReports.RptSalesOrder RptSalesOrder;

        //PURCHASE
        public static PurchaseRegister PurchaseRegfrm;
        public static EnterPurchase EnterPurchasefrm;
        public static PurchasePayments PRPaymentsfrm;
        public static Purchase.Replenishment Replenishmentfrm;

        public static Reports.PurchaseReports.rptPurchaseReportSummary PurchaseReportSummary;
        public static Reports.PurchaseReports.PurchaseReportDetails PurchaseReportDetails;
        public static Reports.PurchaseReports.AnalysePurchaseRpt AnalysePurchaseRptfrm;
        public static Reports.PurchaseReports.AnalysePuchaseFYComparison AnalysePuchaseFYComparison;
        public static Reports.PurchaseReports.rptSupplierLedger rptSupllierLedger;
        public static Reports.PurchaseReports.AllPurchaseReport AllPurchaseReport;
        public static Reports.PurchaseReports.ReceivedOrdersSummary ReceivedOrdersSummary;
        public static Reports.PurchaseReports.ReceivedOrdersDetail ReceivedOrdersDetail;
        public static Reports.PurchaseReports.PurchaseQuotesReport PurchaseQuotesReport;
        public static Reports.PurchaseReports.PurchaseReturnandCredit PurchaseReturnandCredit;
        public static Reports.PurchaseReports.RptAgeingDetail RptAgeingDetail;
        public static Reports.PurchaseReports.RptAgeingSummary RptAgeingSummary;
        public static Reports.PurchaseReports.RptOpenItemReciepts RptOpenItemReciepts;
        public static Reports.PurchaseReports.RptSupplierItemDetail RptSupplierItemDetail;
        public static Reports.PurchaseReports.RptSupplierItemSummary rptSupplierItemSummary;
        public static Reports.PurchaseReports.RptSupplierPaymentHistory RptSupplierPaymentHistory;
        public static Reports.PurchaseReports.RptSupplierPayments rptSupplierPayments;
        public static Reports.PurchaseReports.RptAgeingSummary rptAgeingSummary;
        public static Reports.PurchaseReports.RptAgeingDetail rptAgeingDetail;
        public static Reports.PurchaseReports.RptPurchaseReconciliationSummary rptPurchaseReconSummary;
        public static Reports.PurchaseReports.RptPurchaseReconciliationDetails rptPurchaseReconDetails;
        public static Reports.PurchaseReports.RptBillTransaction rptBillTransaction;
        public static Reports.PurchaseReports.RptPurchasePayablesJournal rptPurchasePayablesJournal;
        public static Reports.PurchaseReports.RptRemittanceAdvicePayBills rptRemittanceAdvicePayBills;

        //PromotionDiscount
        public static Reports.PromotionDiscountReports.SalesByPromos salesByPromos;
        public static Reports.PromotionDiscountReports.FreeProductsReport freeProductsReport;
        public static Reports.PromotionDiscountReports.DiscountPromos discountPromos;

        //INVENTORY
        public static Item ItemFrm;
        public static ItemList ItemListfrm;
        public static ItemRegister ItemRegisterFrm;
        public static StockAdjustments StockAdjustmentsFrm;
        public static BuildItems BuildItemsFrm;
        public static AutoBuildItems AutoBuildItemsFrm;
        public static Stocktake StocktakeFrm;
        public static ItemPriceUpdate ItemPriceUpdateFrm;
        public static Categories Categories;
        public static PriceTags PriceTags;
        public static PriceHistory PriceHistory;

        public static Reports.InventoryReports.RptItemSummary ItemReportSummary;
        public static Reports.InventoryReports.RptItemDetails ItemReportDetails;
        public static Reports.InventoryReports.RptPriceDetails PriceReportDetails;
        public static Reports.InventoryReports.RptPriceAnalysis PriceReportAnalysis;
        public static Reports.InventoryReports.RptItemRegisterSummary ItemReportRegisterSummary;
        public static Reports.InventoryReports.RptItemTransactions ItemReportTransactions;
        public static Reports.InventoryReports.AnalyseInventorySum AnalyseInventorySum;
        public static Reports.InventoryReports.AnalyseInventoryDetail AnalyseInventoryDetail;
        public static Reports.InventoryReports.ItemCountCustomizer ItemCountCustomizer;
        public static Reports.InventoryReports.AutoBuildReport AutoBuildReport;
        public static Reports.InventoryReports.RptCategoryList CategoryList;
        public static Reports.InventoryReports.SalesIngredient salesIngredient;
        public static Reports.InventoryReports.RptSalesAnalysisItem ItemSalesAnalysis;
        public static Reports.InventoryReports.RptSalesAnalysisCategory CategorySalesAnalysis;

        //SETUP
        public static JobBalances JobBalancesFrm;
        public static UserMaintenance UserMaintenanceFrm;
        public static DataInformation DataInfoFrm;
        public static Preferences PreferencesFrm;
        public static ARBalances ARBalancesFrm;
        public static ARBalanceEntry ARBalanceEntryFrm;
        public static APBalances APBalancesFrm;
        public static APBalanceEntry APBalanceEntryFrm;
        public static SessionTransactions SessionTransactions;
        public static OpeningFund SessionOpeningFund;

        //Reports
        public static ReportsNavigation RptPanelFrm;
        public static RptViewer RptViewerFrm;
        public static RptPNGGST RptPNGGSTFrm;

        public static Reports.RptProfileListOptions RptProfileListOptionsFrm;
        public static Reports.RptJobTransactionsOptions RptJobTransactionsOptionsFrm;
        public static Reports.RptActivityOptions RptActivityOptionsFrm;
        public static Reports.InventoryReports.BestSellingItem BestSellingItem;
        public static Reports.RptLoyaltyMembers RptLoyaltyMembers;

        //Tax Reports
        public static RptTaxTransactionsS RptTaxTransactionsSFrm;
        public static RptTaxTransactionsD RptTaxTransactionsDFrm;

        //Session Reports
        public static SessionReportsCustomizer RptSessionCustomizer;
        public static RptTenderDetails RptTenderDetails;
        public static RptTenderSummary RptTenderSummary;


        //Job Reports
        public static RptJobProfitAndLoss RptJobProfitAndLossFrm;
        public static RptJobList RptJobListFrm;

        //UTILITIES
        public static Reminder Reminder;
        public static UserLookup UserLookup;
        public static AuditTrail AuditTrail;
        public static FormLookup FormLookup;
        public static ToDoList ToDoList;
        public static TerminalSetup terminalSetup;
        public static PurchaseOrderSpecimen PurchaseOrderSpecimen;
        public static ItemImport itemImport;
        public static SessionManager SessionManager;

        //search
        public static List<string> accountnumbers = new List<string> { }; //Holds the account numbers of this Linked Accounts
        public static List<string> accountnames = new List<string> { }; //Holds the account names of this Linked Accounts
        public static string[] CurrentBal = new string[7];
        public static string[] lastChequenum = new string[7];
        public static List<string> partnumbers = new List<string> { };

        static CommonClass()
        {
            CommonClass.UserName = "";
            CommonClass.UserID = "";
            CommonClass.isSalesperson = false;
            CommonClass.isSupervisor = false;
            CommonClass.isAdministrator = false;
            CommonClass.isTechnician = false;
            CommonClass.SessionID = 0;
            CommonClass.ConStr = "";
            //CommonClass.IsPastFY = true;
            //CommonClass.IsEditOK = true;

            NumberMapping = new Dictionary<int, string>();
            MonthMapping = new Dictionary<int, string>();

            NumberMapping.Add(12, "Twelve");
            NumberMapping.Add(13, "Thirteen");

            MonthMapping.Add(1, "January");
            MonthMapping.Add(2, "February");
            MonthMapping.Add(3, "March");
            MonthMapping.Add(4, "April");
            MonthMapping.Add(5, "May");
            MonthMapping.Add(6, "June");
            MonthMapping.Add(7, "July");
            MonthMapping.Add(8, "August");
            MonthMapping.Add(9, "September");
            MonthMapping.Add(10, "October");
            MonthMapping.Add(11, "November");
            MonthMapping.Add(12, "December");
        }

        private static string s_LoggedInCompany;
        private static string s_LoggedInSerialNo;
        private static string s_LoggedInRegNo;
        private static string s_LoggedInDbName;
        private static string s_LoggedInServerName;
        private static string s_DbUser;
        private static string s_DbPass;
        private static bool s_IsLocked;


        public static string LoggedInCompany
        {
            get { return s_LoggedInCompany; }
            set { s_LoggedInCompany = value; }
        }

        public static string LoggedInSerialNo
        {
            get { return s_LoggedInSerialNo; }
            set { s_LoggedInSerialNo = value; }
        }

        public static string LoggedInRegNo
        {
            get { return s_LoggedInRegNo; }
            set { s_LoggedInRegNo = value; }
        }

        public static string LoggedInDbName
        {
            get { return s_LoggedInDbName; }
            set { s_LoggedInDbName = value; }
        }

        public static string LoggedInServerName
        {
            get { return s_LoggedInServerName; }
            set { s_LoggedInServerName = value; }
        }

        public static string DbUser
        {
            get { return s_DbUser; }
            set { s_DbUser = value; }
        }

        public static string DbPass
        {
            get { return s_DbPass; }
            set { s_DbPass = value; }
        }

        public static bool IsLocked
        {
            get { return s_IsLocked; }
            set { s_IsLocked = value; }
        }


        public static List<string> GetAcc
        {
            get { return accountnumbers; }
            set { accountnumbers = value; }
        }
        public static List<string> GetItem
        {
            get { return partnumbers; }
            set { partnumbers = value; }
        }
        public static List<string> GetAccName
        {
            get { return accountnames; }
            set { accountnames = value; }
        }

        public static string htmlToText(string html)
        {
            string htmltext = "";
            if (html != "")
            {
                HTMLEditor.DocumentText = "<html><body></body></html>";
                doc = HTMLEditor.Document.DomDocument as IHTMLDocument2;
                doc.designMode = "On";
                HTMLEditor.Document.OpenNew(true).Write(html);
                if (HTMLEditor.Document.Body == null)
                {
                    htmltext = "";
                }
                else
                {
                    htmltext = HTMLEditor.Document.Body.InnerText;
                }

                return htmltext;               
            }
            else
            {
                return "";
            }
        }

        public static string ToLiteral(string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }

        public static int runSql(ref DataTable dtOutput, string sql, Dictionary<string, object> valueParams = null)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                if (valueParams != null)
                {
                    foreach (KeyValuePair<string, object> param in valueParams)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dtOutput.Clear();
                da.Fill(dtOutput);
                return dtOutput.Rows.Count;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static int runSql(string sql, RunSqlInsertMode mode = RunSqlInsertMode.QUERY, Dictionary<string, object> valueParams = null)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConStr);
                con.Open();
                if (mode == RunSqlInsertMode.SCALAR)
                {
                    sql += "; SELECT SCOPE_IDENTITY()";
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                if (valueParams != null)
                {
                    foreach (KeyValuePair<string, object> param in valueParams)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                int returnvalue = 0;
                switch (mode)
                {
                    case RunSqlInsertMode.QUERY:
                        returnvalue = cmd.ExecuteNonQuery();
                        break;
                    case RunSqlInsertMode.SCALAR:
                        returnvalue = Convert.ToInt32(cmd.ExecuteScalar());
                        break;
                }
                return returnvalue;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        public static List<string> getItem(string pPartNo)
        {
            partnumbers.Clear();
            SqlConnection con = null;
            con = new SqlConnection(ConStr);
            string sql = @"SELECT i.ID, i.PartNumber, i.ItemName, c.LastCostEx,i.ID, isp.Level0,i.PurchaseTaxCode,i.SalesTaxCode ,i.ItemDescription
                                    FROM Items i Left JOIN ItemsSellingPrice isp on i.ID = isp.ItemID
                                    Left JOIN ItemsCostPrice c ON c.ItemID = i.ID Where i.PartNumber = '" + pPartNo + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                string innersql = @"SELECT i.PartNumber, i.ItemName, t.TaxCode, c.LastCostEx, t.TaxPaidAccountID, t.TaxPercentageRate, i.ID, i.IsCounted,i.AssetAccountID, i.COSAccountID
                                    FROM TaxCodes t INNER JOIN Items i on i.PurchaseTaxCode = t.TaxCode
                                    INNER JOIN ItemsCostPrice c ON c.ItemID = i.ID Where i.ID = " + (int)reader[0];
                SqlCommand innercmd = new SqlCommand(innersql, con);
                SqlDataReader datareader = innercmd.ExecuteReader();
                while (datareader.Read())
                {
                    partnumbers.Add(String.Format("{0}", datareader["PartNumber"])); //0
                    partnumbers.Add(String.Format("{0}", datareader["ItemName"]));//1
                    partnumbers.Add(String.Format("{0}", datareader["TaxCode"]));//2
                    partnumbers.Add(String.Format("{0}", datareader["LastCostEx"]));//3

                    partnumbers.Add(String.Format("{0}", datareader["TaxPaidAccountID"]));//4
                    partnumbers.Add(String.Format("{0}", datareader["TaxPercentageRate"]));//5
                    partnumbers.Add(String.Format("{0}", datareader["ID"]));//6
                    partnumbers.Add(String.Format("{0}", datareader["IsCounted"]).ToString());//7
                    partnumbers.Add(String.Format("{0}", datareader["AssetAccountID"]));//8
                    partnumbers.Add(String.Format("{0}", datareader["COSAccountID"]));//9

                    partnumbers.Add(String.Format("{0}", reader["PartNumber"]));//0
                    partnumbers.Add(String.Format("{0}", reader["ItemName"]));//1
                    partnumbers.Add(String.Format("{0}", reader["LastCostEx"]));//2 buying price
                    partnumbers.Add(String.Format("{0}", reader["ID"]));//3
                    partnumbers.Add(String.Format("{0}", reader["Level0"]));//4 selling price
                    partnumbers.Add(String.Format("{0}", reader["PurchaseTaxCode"]));//5
                    partnumbers.Add(String.Format("{0}", reader["SalesTaxCode"]));//6
                    partnumbers.Add(String.Format("{0}", reader["ItemDescription"]));//7 item description
                }
            }
            return partnumbers;
        }

        public static void FillAutoCompleteList(ref DataTable dt, string field)
        {
            SqlConnection con_ = null;
            try
            {

                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "";
                switch (field)
                {
                    //case "Account":
                    //    selectSql = "SELECT AccountNumber, AccountName, AccountID, (AccountNumber + ' ' + AccountName) AS FullAccount FROM Accounts WHERE IsHeader = 'D' AND AccountLevel > 0";
                    //    break;
                    case "Item":
                        selectSql = "SELECT PartNumber, ItemName, (PartNumber + ' ' + ItemName) AS FullItem, ID FROM Items";
                        break;
                    case "Job":
                        selectSql = "SELECT JobID, JobCode, JobName, (JobCode + ' ' + JobName) AS FullJob FROM Jobs";
                        break;
                    case "TaxCode":
                        selectSql = "SELECT TaxCode FROM TaxCodes";
                        break;
                }

                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                da.Fill(dt);

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        //public static void FillJobAutoList(ref DataTable dt)
        //{
        //    SqlConnection con_ = null;
        //    try
        //    {
        //        con_ = new SqlConnection(CommonClass.ConStr);
        //        string selectSql = "";

        //        selectSql = "SELECT * FROM Jobs";

        //        SqlCommand cmd_ = new SqlCommand(selectSql, con_);
        //        con_.Open();

        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = cmd_;
        //        da.Fill(dt);

        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        ////    }
        //    finally
        //    {
        //        if (con_ != null)
        //            con_.Close();
        //    }
        //}

        public static void InitCompanyFile()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConStr);
                connection.Open();
                string sql = "SELECT TOP 1 * FROM DataFileInformation";
                SqlCommand cmd_ = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    CompName = dt.Rows[0]["CompanyName"].ToString();
                    CurFY = DateTime.Now.Year;

                    CompAddress = dt.Rows[0]["Add1"].ToString();
                    CompAddress += (CompAddress != "" ? ", " : "") + dt.Rows[0]["Add2"].ToString();
                    CompAddress += (CompAddress != "" ? ", " : "") + dt.Rows[0]["Street"].ToString();
                    CompAddress += (CompAddress != "" ? ", " : "") + "\r\n" + dt.Rows[0]["City"].ToString();
                    CompAddress += (CompAddress != "" ? ", " : "") + dt.Rows[0]["State"].ToString();
                    CompAddress += (CompAddress != "" ? ", " : "") + dt.Rows[0]["Country"].ToString();
                    CompAddress += "\r\n" + dt.Rows[0]["POBox"].ToString();
                    CompAddress += "\r\nPhone: " + dt.Rows[0]["Phone"].ToString();

                    CompSalesTaxNo = dt.Rows[0]["SalesTaxNumber"].ToString();
                    MaxTerminalAllowed = int.Parse(dt.Rows[0]["MaxTerminal"].ToString());
                    CompLogoPath = Application.StartupPath + "\\" + (dt.Rows[0]["CompanyLogo"] != null ? dt.Rows[0]["CompanyLogo"].ToString() : "");
                }

                cmd_ = new SqlCommand("SELECT TOP 1 p.*, c.* FROM Preference p LEFT JOIN Currency c ON p.LocalCurrency = c.CurrencyCode ", connection);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    IsEditOK = (bool)dt.Rows[0]["IsTranEditable"];
                    IsItemPriceInclusive = (bool)dt.Rows[0]["IsItemPriceInclusive"];
                    IsTaxcInclusiveEnterSales = (bool)dt.Rows[0]["IsTaxInclusive"];
                    KeepQuote = (bool)dt.Rows[0]["KeepQuotations"];
                    LCurrencyID = (dt.Rows[0]["CurrencyID"].ToString() == "" ? 0 : (int)dt.Rows[0]["CurrencyID"]);
                    LCurrency = dt.Rows[0]["LocalCurrency"].ToString();
                    LCurSymbol = dt.Rows[0]["CurrencySymbol"].ToString();
                    MandatoryCustomer = (bool)dt.Rows[0]["CustomerMandatory"];
                    AutoEnd = (bool)dt.Rows[0]["AutoEndSession"];
                    ShiftTimeSpan = dt.Rows[0]["StandardShift"].ToString();
                    if (MandatoryCustomer == false)
                    {
                        DefaultCustomerID = dt.Rows[0]["DefaultCustomerID"].ToString();
                    }
                    DRowPref = dt.Rows[0];

                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static string Encrypt(string pStr)
        {
            if (pStr != null)
            {
                int shft = 5;
                string encrypted = pStr.Select(ch => ((int)ch) << shft).Aggregate("", (current, val) => current + (char)(val * 2));
                encrypted = Convert.ToBase64String(Encoding.UTF8.GetBytes(encrypted));

                return encrypted;
            }
            else
            {
                return "";
            }
        }

        public static string Decrypt(string pStr)
        {
            if (pStr != null)
            {
                int shft = 5;
                byte[] ub64str = Convert.FromBase64String(pStr);
                string decrypted = Encoding.UTF8.GetString(ub64str).Select(ch => ((int)ch) >> shft).Aggregate("", (current, val) => current + (char)(val / 2));

                return decrypted;
            }
            else
            {
                return "";
            }
        }

        //Hashing function for User password
        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                //Convert to text
                var hashedInputStringBuilder = new System.Text.StringBuilder(64);
                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }

        private static void Fisher_Yates(int[] array)
        {
            System.Random rnd = new System.Random();
            int arraysize = array.Length;
            int random;
            int temp;

            for (int i = 0; i < arraysize; i++)
            {
                random = i + (int)(rnd.NextDouble() * (arraysize - i));

                temp = array[random];
                array[random] = array[i];
                array[i] = temp;
            }
        }

        public static string StringMixer(int length)
        {
            string s = "1234567890abcdefghijklmnopqrstuvwxyz";
            string output = "";
            int arraysize = s.Length;
            int[] randomArray = new int[arraysize];

            for (int i = 0; i < arraysize; i++)
            {
                randomArray[i] = i;
            }

            Fisher_Yates(randomArray);

            for (int i = 0; i < arraysize; i++)
            {
                output += s[randomArray[i]];
            }

            return output.Substring(0, length).ToUpper();
        }

        public static string generateMemberNumber()
        {
            int lExistID = 0;
            string lNewRndNo = CommonClass.StringMixer(8);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Number", lNewRndNo);
            lExistID = CommonClass.runSql("SELECT ID FROM LoyaltyMember WHERE Number=@Number", CommonClass.RunSqlInsertMode.SCALAR, param);
            if (lExistID > 0)
            {
                return generateMemberNumber();
            }
            else
            {
                return lNewRndNo;
            }
        }

        public static int generateRandomNumber(string pCompName)
        {
            using (RNGCryptoServiceProvider randomgenerator = new RNGCryptoServiceProvider())
            {
                byte[] rawrandom = new byte[6]; //pCompName is irrelevant for now
                randomgenerator.GetBytes(rawrandom);
                return BitConverter.ToUInt16(rawrandom, 0);
            }
        }

        public static string base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string generateActivationCode(string pCompanyName, string pRegNo, string pSerialNo)
        {
            string lCompName = pCompanyName.ToUpper();
            string lRegNo = pRegNo.ToUpper();
            string lSerialNo = pSerialNo.ToUpper();

            char[] compnameasciicode = (lCompName.Length > 4) ? lCompName.Substring(0, 4).ToCharArray() : lCompName.ToCharArray();
            char[] regnoasciicode = (lRegNo.Length > 4) ? lRegNo.Substring(0, 4).ToCharArray() : lRegNo.ToCharArray();
            char[] serialnoasciicode = (lSerialNo.Length > 4) ? lSerialNo.Substring(0, 4).ToCharArray() : lSerialNo.ToCharArray();

            string compnamestr = "";
            string compregnostr = "";
            string compserialnostr = "";

            for (int iterate = 0; iterate < compnameasciicode.Length; ++iterate)
            {
                compnamestr += (int)compnameasciicode[iterate];
            }
            for (int iterate = 0; iterate < regnoasciicode.Length; ++iterate)
            {
                compregnostr += (int)regnoasciicode[iterate];
            }
            for (int iterate = 0; iterate < serialnoasciicode.Length; ++iterate)
            {
                compserialnostr += (int)serialnoasciicode[iterate];
            }

            ulong compnamecodeinteger = ulong.Parse(compnamestr);
            ulong compregnointeger = ulong.Parse(compregnostr);
            ulong compserialnointeger = ulong.Parse(compserialnostr);

            compnamecodeinteger = compnamecodeinteger ^ MAGICNO;
            compregnointeger = compregnointeger ^ MAGICNO;
            compserialnointeger = compserialnointeger ^ MAGICNO;

            string compnamecodehexa = compnamecodeinteger.ToString("X");
            string compregnohexa = compregnointeger.ToString("X");
            string compserialnohexa = compserialnointeger.ToString("X");

            string activationkey = base64Encode(compnamecodehexa + "," + compregnohexa + "," + compserialnohexa);

            return activationkey;
        }

        public static string generateActivationCode2(string pCompanyName, string pRegNo, string pSerialNo, string pMaxUser)
        {
            char[] compnameasciicode = pCompanyName.ToCharArray();
            char[] regnoasciicode = pRegNo.ToCharArray();
            char[] serialnoasciicode = pSerialNo.ToCharArray();
            char[] maxuserasciicode = pMaxUser.ToCharArray();

            string compnamestr = "";
            string regnostr = "";
            string serialnostr = "";
            string maxuserstr = "";

            for (int iterate = 0; iterate < compnameasciicode.Length; ++iterate)
            {
                compnamestr += ((int)compnameasciicode[iterate]).ToString("X");
            }
            for (int iterate = 0; iterate < regnoasciicode.Length; ++iterate)
            {
                regnostr += ((int)regnoasciicode[iterate]).ToString("X");
            }
            for (int iterate = 0; iterate < serialnoasciicode.Length; ++iterate)
            {
                serialnostr += ((int)serialnoasciicode[iterate]).ToString("X");
            }
            for (int iterate = 0; iterate < maxuserasciicode.Length; ++iterate)
            {
                maxuserstr += ((int)maxuserasciicode[iterate]).ToString("X");
            }

            string activationkey = base64Encode(compnamestr + "," + regnostr + "," + serialnostr + "," + maxuserstr);

            return activationkey;
        }

        public static string generateReActivationCode(string pCompanyName, string pInvoiceNo)
        {
            string lCompanyName = pCompanyName.ToUpper();
            string lInvoiceNo = pInvoiceNo.ToUpper();

            char[] compnameasciicode = (lCompanyName.Length > 4) ? lCompanyName.Substring(0, 4).ToCharArray() : lCompanyName.ToCharArray();
            char[] invoicenoasciicode = (lInvoiceNo.Length > 4) ? lInvoiceNo.Substring(0, 4).ToCharArray() : lInvoiceNo.ToCharArray();

            string compnamestr = "";
            string invoicenostr = "";

            for (int iterate = 0; iterate < compnameasciicode.Length; ++iterate)
            {
                compnamestr += (int)compnameasciicode[iterate];
            }
            for (int iterate = 0; iterate < invoicenoasciicode.Length; ++iterate)
            {
                invoicenostr += (int)invoicenoasciicode[iterate];
            }

            ulong compnameinteger = ulong.Parse(compnamestr);
            ulong invoicenointeger = ulong.Parse(invoicenostr);

            compnameinteger = compnameinteger ^ MAGICNO;
            invoicenointeger = invoicenointeger ^ MAGICNO;

            string compnamehexa = compnameinteger.ToString("X");
            string invoicenohexa = invoicenointeger.ToString("X");

            string activationkey = base64Encode(compnamehexa + "," + invoicenohexa);

            return activationkey;
        }

        public static string[] decodeActivationKey(string pActivationKey)
        {
            string[] activationelements = base64Decode(pActivationKey).Split(',');
            ulong compnameinteger = ulong.Parse(activationelements[0], System.Globalization.NumberStyles.HexNumber);
            ulong regnointeger = ulong.Parse(activationelements[1], System.Globalization.NumberStyles.HexNumber);
            ulong serialnointeger = ulong.Parse(activationelements[2], System.Globalization.NumberStyles.HexNumber);

            compnameinteger = compnameinteger ^ MAGICNO;
            regnointeger = regnointeger ^ MAGICNO;
            serialnointeger = serialnointeger ^ MAGICNO;

            string compnamestr = compnameinteger.ToString();
            string regnostr = regnointeger.ToString();
            string serialnostr = serialnointeger.ToString();

            string compnamedecodedstr = "";
            string regnodecodedstr = "";
            string serialnodecodedstr = "";

            for (short iterator = 0; iterator < compnamestr.Length; iterator += 2)
            {
                string asciicode = compnamestr.ElementAt(iterator).ToString() + compnamestr.ElementAt(iterator + 1).ToString();
                compnamedecodedstr += char.ConvertFromUtf32(Int32.Parse(asciicode));
            }

            for (short iterator = 0; iterator < regnostr.Length; iterator += 2)
            {
                string asciicode = regnostr.ElementAt(iterator).ToString() + regnostr.ElementAt(iterator + 1).ToString();
                regnodecodedstr += char.ConvertFromUtf32(Int32.Parse(asciicode));
            }

            for (short iterator = 0; iterator < serialnostr.Length; iterator += 2)
            {
                string asciicode = serialnostr.ElementAt(iterator).ToString() + serialnostr.ElementAt(iterator + 1).ToString();
                serialnodecodedstr += char.ConvertFromUtf32(Int32.Parse(asciicode));
            }

            string[] activationkeygroup = { compnamedecodedstr, regnodecodedstr, serialnodecodedstr };

            return activationkeygroup;
        }

        public static string[] decodeActivationKey2(string pActivationKey, bool activation = true)
        {
            string[] activationelements = base64Decode(pActivationKey).Split(',');
              if (activationelements.Count() != 4)
                {
                    string[] notvalid = { "Not Valid" };
                    return notvalid;
                }
              
            string compnamestr = activationelements[0];
            string regnostr = activationelements[1];
            string serialnostr = activationelements[2];
            string maxuserstr = activationelements[3];

            string compnamedecodedstr = "";
            string regnodecodedstr = "";
            string serialnodecodedstr = "";
            string maxuserdecodedstr = "";

            for (short iterator = 0; iterator < compnamestr.Length; iterator += 2)
            {
                string hexacode = compnamestr.ElementAt(iterator).ToString() + compnamestr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                compnamedecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            for (short iterator = 0; iterator < regnostr.Length; iterator += 2)
            {
                string hexacode = regnostr.ElementAt(iterator).ToString() + regnostr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                regnodecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            for (short iterator = 0; iterator < serialnostr.Length; iterator += 2)
            {
                string hexacode = serialnostr.ElementAt(iterator).ToString() + serialnostr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                serialnodecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            for (short iterator = 0; iterator < maxuserstr.Length; iterator += 2)
            {
                string hexacode = maxuserstr.ElementAt(iterator).ToString() + maxuserstr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                maxuserdecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            string[] activationkeygroup = { compnamedecodedstr, regnodecodedstr, serialnodecodedstr, maxuserdecodedstr };

            return activationkeygroup;
        }


        public static string[] decodeReActivationKey(string pReActivationKey)
        {
            string[] activationelements = base64Decode(pReActivationKey).Split(',');
            string[] notvalid = { "Not Valid" };
            if (activationelements.Count() != 3)
            {
                return notvalid;
            }
            ulong compnameinteger = ulong.Parse(activationelements[0], System.Globalization.NumberStyles.HexNumber);
            ulong invoicenointeger = ulong.Parse(activationelements[1], System.Globalization.NumberStyles.HexNumber);
            ulong maxuserinteger = ulong.Parse(activationelements[2], System.Globalization.NumberStyles.HexNumber);

            compnameinteger = compnameinteger ^ MAGICNO;
            invoicenointeger = invoicenointeger ^ MAGICNO;
            maxuserinteger = maxuserinteger ^ MAGICNO;

            string compnamestr = compnameinteger.ToString();
            string invoicenostr = invoicenointeger.ToString();
            string maxuserstr = maxuserinteger.ToString();

            string compnamedecodedstr = "";
            string invoicenodecodedstr = "";
            string maxuserdecodedstr = "";

            for (short iterator = 0; iterator < compnamestr.Length; iterator += 2)
            {
                string asciicode = compnamestr.ElementAt(iterator).ToString() + compnamestr.ElementAt(iterator + 1).ToString();
                compnamedecodedstr += char.ConvertFromUtf32(Int32.Parse(asciicode));
            }
           if(invoicenostr.Length % 2 == 0)
            {
                for (short iterator = 0; iterator < invoicenostr.Length; iterator += 2)
                {
                    string asciicode = invoicenostr.ElementAt(iterator).ToString() + invoicenostr.ElementAt(iterator + 1).ToString();
                    invoicenodecodedstr += char.ConvertFromUtf32(Int32.Parse(asciicode));
                }
            }
            else
            {
                return notvalid;
            }
          
            for (short iterator = 0; iterator < maxuserstr.Length; iterator += 2)
            {
                string asciicode = maxuserstr.ElementAt(iterator).ToString() + maxuserstr.ElementAt(iterator + 1).ToString();
                maxuserdecodedstr += char.ConvertFromUtf32(Int32.Parse(asciicode));
            }

            string[] activationkeygroup = { compnamedecodedstr, invoicenodecodedstr, maxuserdecodedstr };

            return activationkeygroup;
        }

        public static bool AppLogin(string pServerName,
                                    string pDbName,
                                    string pUname,
                                    string pPwd,
                                    string pDbUname,
                                    string pDbPwd)
        {
            SqlConnection con_ = null;
            try
            {
                string salesperon;
                string supervisor;
                string administrator;
                string technician;
                con_ = new SqlConnection(ConStr);
                String Sql = "SELECT * FROM Users WHERE user_name = '" + pUname + "' AND user_pwd = '" + SHA512(pPwd) + "'";

                SqlCommand cmd_ = new SqlCommand(Sql, con_);
                con_.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable ltb = new DataTable();
                da.Fill(ltb);
                if (ltb.Rows.Count == 1)
                {
                    UserName = ltb.Rows[0]["user_name"].ToString();
                    UserID = ltb.Rows[0]["user_id"].ToString();
                    salesperon = ltb.Rows[0]["IsSalesperson"].ToString();
                    supervisor = ltb.Rows[0]["IsSupervisor"].ToString();
                    administrator = ltb.Rows[0]["IsAdministrator"].ToString();
                    technician = ltb.Rows[0]["IsTechnician"].ToString();
                    isSalesperson = bool.Parse(salesperon);
                    isSupervisor = bool.Parse(supervisor);
                    isAdministrator = bool.Parse(administrator);
                    isTechnician = bool.Parse(technician);
                    return GetAccess(UserID);
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to connect to the selected server or database doest not exist");
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        public static bool GetAccess(String pUID)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(ConStr);
                String Sql = "SELECT u.*, f.form_name FROM User_Access u INNER JOIN Forms f ON (u.form_code = f.form_code) WHERE u.user_id = " + pUID;
                SqlCommand cmd_ = new SqlCommand(Sql, con_);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable ltb = new DataTable();
                da.Fill(ltb);
                string fname = "";
                if (ltb.Rows.Count > 0)
                {
                    UserAccess = new Dictionary<string, Dictionary<string, Boolean>>();
                    foreach (DataRow lrow in ltb.Rows)
                    {
                        if (lrow["form_name"].ToString() == "Save INVOICE")
                        {
                            //MessageBox.Show("INVOICE");
                        }
                        UserAccess.Add(lrow["form_name"].ToString(), new Dictionary<string, Boolean>
                        {
                            { "View", Convert.ToBoolean(lrow["u_view"].ToString())     },
                            { "Add", Convert.ToBoolean(lrow["u_add"].ToString())       },
                            { "Edit", Convert.ToBoolean(lrow["u_edit"].ToString())     },
                            { "Delete", Convert.ToBoolean(lrow["u_delete"].ToString()) }
                        });
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        public static void SetAppFormCodes()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(ConStr);
                String Sql = "SELECT * FROM Forms order by form_code";
                SqlCommand cmd_ = new SqlCommand(Sql, con_);
                con_.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable ltb = new DataTable();
                da.Fill(ltb);

                if (ltb.Rows.Count > 0)
                {
                    AppFormCode = new Dictionary<string, string>();
                    foreach (DataRow lrow in ltb.Rows)
                    {
                        AppFormCode.Add(lrow["form_name"].ToString(), lrow["form_code"].ToString());
                        Console.WriteLine(lrow["form_name"].ToString() + " - " + lrow["form_code"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        public static bool CreateDbBackup(string pBackupPath)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConStr);
                string backupsql = "BACKUP DATABASE " + s_LoggedInDbName + " TO DISK = '" + pBackupPath + "'";
                SqlCommand bckupcmd = new SqlCommand(backupsql, con);
                con.Open();
                bckupcmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static void SaveSystemLogs(String pUserID, String pFormCode, String pAction, String pRecord = "", String pOldData = "", String pNewData = "")
        {
            SqlConnection con = null;
            try
            {
                string sql = @"INSERT INTO SystemAuditTrail (
                                                UserID, 
                                                FormCode, 
                                                AuditAction, 
                                                AffectedRecordID, 
                                                OldData, 
                                                NewData ) 
                                        VALUES (
                                                @UserID, 
                                                @FormCode, 
                                                @AuditAction, 
                                                @AffectedRecord, 
                                                @OldData, 
                                                @NewData )";

                con = new SqlConnection(ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserID", pUserID);
                cmd.Parameters.AddWithValue("@FormCode", pFormCode);
                cmd.Parameters.AddWithValue("@AuditAction", pAction);
                cmd.Parameters.AddWithValue("@AffectedRecord", pRecord);
                cmd.Parameters.AddWithValue("@OldData", pOldData);
                cmd.Parameters.AddWithValue("@NewData", pNewData);
                con.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    Console.WriteLine("Record is successfully inserted in SystemAudiTrail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static DataRow getTaxDetails(string pTaxCode)
        {
            string[] TaxDet = new string[2];

            using (SqlConnection sqlCon = new SqlConnection(ConStr))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM taxcodes WHERE TaxCode = '" + pTaxCode + "'", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDA.Fill(dtbl);

                if (dtbl.Rows.Count > 0)
                {
                    return dtbl.Rows[0];
                }
                else
                {
                    return dtbl.NewRow();
                }
            }
        }

        public static void ShowReport(Reports.ReportParams pParams)
        {
            string rptPath = Application.StartupPath + "\\Reports\\" + pParams.ReportName;
            CrystalDecisions.CrystalReports.Engine.ReportDocument crReport = null;

            if (pParams.Rec != null)
            {
                for (int i = 0; i < pParams.Rec.Count; ++i)
                {
                    if (pParams.Rec[i] != null
                        && pParams.Rec[i].Rows.Count > 0)
                    {
                        if (i == 0)
                        {
                            crReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                            crReport.Load(rptPath, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);
                        }

                        if (crReport != null)
                            crReport.Database.Tables[i].SetDataSource(pParams.Rec[i]);
                    }
                    else
                    {
                        MessageBox.Show("Report Contains No Data.");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Report Contains No Data.");
                return;
            }

            if (pParams.tblSubRpt != null)
            {
                crReport.OpenSubreport(pParams.SubRpt).SetDataSource(pParams.tblSubRpt);
            }

            foreach (KeyValuePair<string, DataTable> child in pParams.children)
            {
                if (child.Value != null)
                {
                    crReport.OpenSubreport(child.Key).SetDataSource(child.Value);
                }
            }

            if (pParams.Params != "")
            {
                string[] a = pParams.Params.Split('|');
                string[] b = pParams.PVals.Split('|');
                for (int i = 0; i < a.Length; i++)
                {
                    crReport.SetParameterValue(a[i], b[i]);
                }
            }

            if (pParams.HideSec != "")
            {
                if (Information.IsNumeric(pParams.HideSec))
                {
                    int vs;
                    vs = Convert.ToInt32(pParams.HideSec);
                    crReport.ReportDefinition.Sections[vs].SectionFormat.EnableSuppress = true;
                }
                else
                {
                    crReport.ReportDefinition.Sections[pParams.HideSec].SectionFormat.EnableSuppress = true;
                }
            }

            if (pParams.PapSize != "")
            {
                PrintDocument doctoprint = new PrintDocument();
                for (int iterate = 0; iterate < doctoprint.PrinterSettings.PaperSizes.Count - 1; ++iterate)
                {
                    int rawKind;
                    if (doctoprint.PrinterSettings.PaperSizes[iterate].PaperName == pParams.PapSize)
                    {
                        rawKind = doctoprint.PrinterSettings.PaperSizes[iterate].RawKind;
                        crReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawKind;
                        crReport.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                        break;
                    }
                }
            }

            switch (pParams.PrtOpt)
            {
                case 0: //Print
                    System.Drawing.Printing.PrintDocument docs = new System.Drawing.Printing.PrintDocument();
                    PrintDialog printDialg = new PrintDialog();
                    printDialg.UseEXDialog = true;
                    if (printDialg.ShowDialog() == DialogResult.OK)
                    {
                        crReport.PrintOptions.PrinterName = printDialg.PrinterSettings.PrinterName;
                        crReport.PrintToPrinter(printDialg.PrinterSettings.Copies, false, printDialg.PrinterSettings.FromPage, printDialg.PrinterSettings.ToPage);
                    }
                    break;
                case 1: //Preview
                    RptViewerFrm = new RptViewer();
                    RptViewerFrm.crViewer.ReportSource = crReport;
                    RptViewerFrm.ShowDialog();
                    break;
                case 2: //Save to file
                    if (pParams.fname == "")
                    {
                        MessageBox.Show("Please specify the file to save to.");
                        return;
                    }
                    crReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pParams.fname);
                    break;
            }
        }
    }
}
