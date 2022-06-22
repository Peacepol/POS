using RestaurantPOS.Inventory;
using RestaurantPOS.References;
using RestaurantPOS.Setup;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RestaurantPOS.Sales
{
    public partial class EnterSales : Form
    {
        private string invoiceStatus = "";
        public string invoicenum = "";
        private DataRow CustomerRow;
        bool IsLoading = true;
        private string[] Tax;
        private string CustomerID;
        private string ShippingMethodID;
        private string[] Jobs;
        private string XsalesID = "";
        private string ProfileTax = "";
        private string MethodPaymentID = "";
        private float lTaxEx = 0;
        private float lTaxInc = 0;
        private float lTaxRate = 0;
        private float lAmount = 0;
        private float mPriceEx = 0;
        private decimal OldBalanceDue = 0;
        private string CurSeries = "";
        SqlCommand cmd;
        private string AR_AccountID;
        private string AR_FreightAccountID;
        private string AR_CustomerDepositsID;
        private string AR_ChequeID;
        private string TaxID = "";
        private int shipAddressID = 0;
        private string defSalesNum;
        private string SalesPersonID = CommonClass.UserID;
        bool enter = false;
        private string memberID = "";
        private string AmountChange = "";
        DataTable FreeTable = null;


        //Terms
        private string ActualDueDate;
        private string ActualDiscountDate;
        public string TermsOfPaymentID;
        private string BalanceDueDays;
        private string DiscountDays;
        private string VolumeDiscount;
        private string EarlyPaymenDiscount;
        private string LatePaymenDiscount;
        private string baldate;
        private string discountdate;
        private string TermRefID = "0";
        private Boolean createterm = false;
        private DataRow SalesTermsRow;

        //ITEMS
        int ShipQ = 0;
        int ItemOnHand = 0;
        private List<RuleCriteriaPoints> itemPromos;
        private List<RuleCriteria> isLoyal;
        private int SupplierID;
        string glAccountCode;
        string member = "Not a Member";

        bool ismember = false;
        bool isContractPrice = false;
        private float ContractPrice = 0;

        //FOR FREIGHT
        private string FreightTaxCode = "";
        private string FreightTaxAccountID = "0";
        private float FreightTaxRate = 0;
        private float FreightTax = 0;
        private float FreightAmountEx = 0;
        private float FreightAmountInc = 0;

        CommonClass.InvocationSource SrcOfInvoke;
        private DataTable TbRepSales;
        private DataTable TbRepSalesLines;
        public DataTable dtv;

        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;

        //For Customer's Current Balance & Credit Limit
        private float CustomerBalance = 0;
        private float CustomerCreditLimit = 0;

        //for Convertion os Sales
        private string FromSalesType = "";
        private string ToSalesType = "";

        private DataRow ShipID;
        private string ShipVia;
        public bool isFreeProduct;
        public float FreeProductAmt;
        //For Contact Info
        private DataTable ContactInfoTb = null;
        private string LocationID = "";

        //For Payment Info
        private DataTable PaymentInfoTb = null;
        private string gPaymentNo = "";
        private Decimal PrevPaid = 0;
        private int NewSalesID = 0;

        //For Void
        string supervisor = "Not a Salesperson";
        string prevAmt;
        string password = "";
        string username = "";

        string formcode = "";
        public CommonClass.InvocationSource SourceOfInvoke
        {
            get { return SrcOfInvoke; }
            set { SrcOfInvoke = value; }
        }

        public DataGridView GetSalesLinesGridView
        {
            get { return dgEnterSales; }
        }


        public EnterSales(CommonClass.InvocationSource pSrcInvoke, string pSalesID = "", string pSalesType = "")
        {

            SrcOfInvoke = pSrcInvoke;
            XsalesID = pSalesID;
            InitializeComponent();
            salestype_cb.Text = pSalesType;
            ToSalesType = pSalesType;
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                FormRights.TryGetValue("Add", out outx);
                CanAdd = outx;
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                CanEdit = outx;
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                CanDelete = outx;
            }
            string outy = "";
            CommonClass.AppFormCode.TryGetValue(this.Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
        }

        void FormCheck()
        {
            if (this.salestype_cb.Text == "QUOTE")
            {
                lblMemo.Text = "Memo:";
                this.BackColor = System.Drawing.SystemColors.ActiveCaption;
                lblBalanceDue.Visible = false;
                lblPaidToday.Visible = false;
                PaidToday_txt.Visible = false;
                //PaymentMethodTxt.Visible = false;
                BalanceDueAmountTxt.Visible = false;

                BalanceDue_txt.Visible = false;
                btnPaymentDetails.Visible = false;
                InvoiceNumTxt.Visible = false;
                rdbCash.Visible = false;
                rdbAR.Visible = false;
            }
            else if (this.salestype_cb.Text == "ORDER")
            {
                lblMemo.Text = " Journal Memo:";
                this.BackColor = System.Drawing.Color.DarkSeaGreen;
                lblBalanceDue.Visible = true;
                lblPaidToday.Visible = true;

                PaidToday_txt.Visible = false;
                //PaymentMethodTxt.Visible = false;
                BalanceDue_txt.Visible = false;
                btnPaymentDetails.Visible = false;
                InvoiceNumTxt.Visible = false;
                rdbCash.Visible = false;
                rdbAR.Visible = false;

                if (customerText.Text != "")
                {
                    PaidToday_txt.Visible = true;
                    BalanceDue_txt.Visible = true;
                    btnPaymentDetails.Visible = true;
                    InvoiceNumTxt.Visible = true;
                }
            }
            else if (this.salestype_cb.Text == "INVOICE")
            {
                InvoiceNumTxt.Visible = false;
                lblMemo.Text = " Journal Memo:";
                this.BackColor = System.Drawing.Color.Khaki;


                InvoiceNumTxt.Visible = false;
                rdbCash.Visible = true;
                rdbAR.Visible = true;
                rdbCash.Checked = true;

                btnPaymentDetails.Visible = false;
                if (customerText.Text != "")
                {
                    PaidToday_txt.Visible = true;
                    btnPaymentDetails.Visible = true;
                    this.BalanceDue_txt.Visible = true;
                    this.lblBalanceDue.Visible = true;
                    lblBalanceDue.Visible = true;
                    lblPaidToday.Visible = true;
                    if (rdbCash.Checked)
                    {
                        btnPaymentDetails.Visible = true;
                    }
                    else
                    {
                        btnPaymentDetails.Visible = false;
                    }
                    InvoiceNumTxt.Visible = true;

                }

            }
            else if (this.salestype_cb.Text == "LAY-BY")
            {
                InvoiceNumTxt.Visible = false;
                lblMemo.Text = " Journal Memo:";
                this.BackColor = System.Drawing.Color.IndianRed;
                lblBalanceDue.Visible = true;
                lblPaidToday.Visible = true;

                PaidToday_txt.Visible = false;
                BalanceDue_txt.Visible = true;
                btnPaymentDetails.Visible = false;
                InvoiceNumTxt.Visible = false;
                InvoiceNumTxt.Visible = false;
                rdbCash.Visible = false;
                rdbAR.Visible = false;
                if (customerText.Text != "")
                {
                    PaidToday_txt.Visible = true;
                    BalanceDue_txt.Visible = true;
                    btnPaymentDetails.Visible = true;
                    InvoiceNumTxt.Visible = true;
                }
            }
            if (CommonClass.isSalesperson == true)
            {
                btnremove.Text = "Remove Line";
            }
            if (CanAdd)
            {
                record_btn.Enabled = CanAdd;
                GetFormCode(salestype_cb.Text);
                if (formcode == salestype_cb.Text)
                {
                    CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;

                    record_btn.Text = "Save " + textInfo.ToTitleCase(formcode);
                    btnVoid.Text = "Void " + textInfo.ToTitleCase(formcode);
                    ApplyFieldAccess(CommonClass.UserID);
                }
            }
            else
            {
                record_btn.Enabled = CanAdd;
            }
            txtSalesperson.Text = CommonClass.UserName;
            SalesPersonID = CommonClass.UserID;
            dgEnable();
        }

        private void salestype_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormCheck();
            if (XsalesID == "")
            {
                if (salestype_cb.Text == "INVOICE")
                {
                    PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_ChequeID;
                }
                else
                {
                    PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_CustomerDepositsID;
                }
            }
            ApplyVoidAccess();
        }
        public void GetFormCode(string fCode)
        {
            string sqlFormCode = @"SELECT * FROM User_Access WHERE form_code = '" + fCode + "'";
            DataTable dtformcode = new DataTable();
            CommonClass.runSql(ref dtformcode, sqlFormCode);
            if (dtformcode.Rows.Count > 0)
            {
                for (int i = 0; i < dtformcode.Rows.Count; i++)
                {
                    DataRow dr = dtformcode.Rows[i];
                    formcode = dr["form_code"].ToString();
                }
            }
        }
        public void ApplyFieldAccess(String FieldID)
        {
            CommonClass.GetAccess(FieldID);
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.Button")
                    {
                        Button btn = (Button)c;
                        ButtonFieldsRights(btn);
                    }
                }
            }
        }
        private void ButtonFieldsRights(Button item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Text, out lDic))
            {
                if (item.Name == "btnVoid")
                {
                    Console.WriteLine("btnVOid");
                }
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        private void EnterSales_Load(object sender, EventArgs e)
        {
            txtCustPoints.Text = "0";
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            foreach (DataGridViewColumn column in dgEnterSales.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Only enable the print button if the record is existing, not a new record to be save
            if (SrcOfInvoke != CommonClass.InvocationSource.REGISTER)
                btnPrint.Visible = false;
            InitVoidTable();
            InitPaymentInfoTb();
            InitContactInfoTb();
            InitFreeTable();
            FormCheck();

            if (XsalesID != "")
            {
                LoadTransaction();
                LoadPaymentInfoTb(XsalesID);
                CalcOutOfBalance();
                this.BalanceDue_txt.Visible = true;
                this.lblID.Text = XsalesID;
                this.btnVoid.Visible = true;
                if (!CommonClass.IsEditOK)
                {
                    if (SrcOfInvoke != CommonClass.InvocationSource.CHANGETO
                        && SrcOfInvoke != CommonClass.InvocationSource.USERECURRING && SrcOfInvoke != CommonClass.InvocationSource.REMINDER)
                    {
                        record_btn.Text = "UPDATE";
                    }
                    else//If invocation source is to change it, btnRecord should show Record.
                    {
                        record_btn.Text = "RECORD";
                    }
                    record_btn.Enabled = CanEdit;
                }
                else //EDIT IS OK
                {
                    if (invoiceStatus != "Closed")
                    {
                        record_btn.Text = "UPDATE";
                        record_btn.Enabled = CanEdit;
                        if (SrcOfInvoke == CommonClass.InvocationSource.CHANGETO
                            || SrcOfInvoke == CommonClass.InvocationSource.USERECURRING
                            || SrcOfInvoke == CommonClass.InvocationSource.REMINDER) //If invocation source is to change it, btnRecord should show Record.
                        {
                            InvoiceNumTxt.Text = "";
                            invoicenum = "";
                            record_btn.Text = "RECORD";
                            btnUseRecurring.Enabled = false;
                        }
                        record_btn.Enabled = CanEdit;
                    }
                    else
                    {
                        record_btn.Enabled = false;
                    }
                }
                ApplyFieldAccess(CommonClass.UserID);
                if (PaidToday_txt.Value != 0
                   && SrcOfInvoke != CommonClass.InvocationSource.CHANGETO //When Converting to Invoice,  disable payment details if there is already payment on it
                   && SrcOfInvoke != CommonClass.InvocationSource.USERECURRING
                   && SrcOfInvoke != CommonClass.InvocationSource.REMINDER)
                {
                    record_btn.Enabled = false;
                    btnremove.Enabled = false;
                    btnPaymentDetails.Enabled = false;
                    btnViewPayments.Visible = true;
                    PaidToday_txt.Enabled = false;
                    BalanceDue_txt.Enabled = false;
                    lblPaidToday.Text = "Paid To Date:";

                }

            }
            else
            {

                LoadInvoiceTerms();
                PopulateDataGridView();
                lblSalesID.Visible = false;
                LoadDefaultCustomer();

            }
            InvoiceNumTxt.Visible = false;
            if (customerText.Text != "")
            {
                InvoiceNumTxt.Visible = true;
            }
            //FormCheck();
            ApplyVoidAccess();
            dgEnable();

            SqlConnection con_ua = null;
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);

                string selectSql_ua = "SELECT * FROM Preference";
                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AR_AccountID = reader["TradeDebtorGLCode"].ToString();//(reader["ReceivablesAccountID"].ToString());
                            AR_ChequeID = (reader["SalesDepositGLCode"].ToString());
                            AR_FreightAccountID = (reader["SalesFreightGLCode"].ToString());
                            AR_CustomerDepositsID = (reader["SalesDepositGLCode"].ToString());

                            if (XsalesID == "")
                            {
                                if (salestype_cb.Text == "INVOICE")
                                {
                                    PaymentInfoTb.Rows[0]["RecipientAccountID"] = "0";
                                }
                                else
                                {
                                    PaymentInfoTb.Rows[0]["RecipientAccountID"] = "0";
                                }
                            }

                            if (AR_AccountID == "0")
                            {
                                string titles = "Information";
                                DialogResult noCustomerReceipts = MessageBox.Show("Setup GL code in Preference first.", titles, MessageBoxButtons.OK);
                                if (noCustomerReceipts == DialogResult.OK)
                                {
                                    this.BeginInvoke(new MethodInvoker(Close));
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Setup GL code in Preference first");
                    }
                }
                if (SrcOfInvoke != CommonClass.InvocationSource.USERECURRING && SrcOfInvoke != CommonClass.InvocationSource.REGISTER && SrcOfInvoke != CommonClass.InvocationSource.REMINDER)
                {
                    SqlCommand cmdrecur = new SqlCommand("SELECT EntityID FROM Recurring WHERE TranType IN ('SQ', 'SO', 'SI')", con_ua);
                    int noofrecords = Convert.ToInt32(cmdrecur.ExecuteScalar());
                    if (noofrecords > 0)
                        btnUseRecurring.Enabled = true;
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ua != null)
                    con_ua.Close();
            }
            IsLoading = false;
        }

        void PopulateDataGridView()
        {
            IsLoading = true;
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            dgEnterSales.Rows.Add();
            IsLoading = false;
        }

        private void InitContactInfoTb()
        {
            ContactInfoTb = new DataTable();
            ContactInfoTb.Columns.Add("Street", typeof(string));
            ContactInfoTb.Columns.Add("City", typeof(string));
            ContactInfoTb.Columns.Add("State", typeof(string));
            ContactInfoTb.Columns.Add("Postcode", typeof(string));
            ContactInfoTb.Columns.Add("Country", typeof(string));
            ContactInfoTb.Columns.Add("Phone", typeof(string));
            ContactInfoTb.Columns.Add("Fax", typeof(string));
            ContactInfoTb.Columns.Add("Email", typeof(string));
            ContactInfoTb.Columns.Add("Website", typeof(string));
            ContactInfoTb.Columns.Add("ContactPerson", typeof(string));
            ContactInfoTb.Columns.Add("ProfileID", typeof(string));
            ContactInfoTb.Columns.Add("Comments", typeof(string));
            ContactInfoTb.Columns.Add("TypeOfContact", typeof(string));
        }

        private void InitPaymentInfoTb()
        {
            PaymentInfoTb = new DataTable();
            PaymentInfoTb.Columns.Add("RecipientAccountID", typeof(string));
            PaymentInfoTb.Columns.Add("AmountPaid", typeof(decimal));
            PaymentInfoTb.Columns.Add("PaymentMethodID", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentMethod", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentAuthorisationNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentCardNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentNameOnCard", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentExpirationDate", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentCardNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBSB", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankAccountNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankAccountName", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentChequeNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentGCNo", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentGCNotes", typeof(string));


            DataRow rw = PaymentInfoTb.NewRow();

            rw["RecipientAccountID"] = AR_AccountID;
            rw["AmountPaid"] = 0;
            rw["PaymentMethodID"] = "0";
            rw["PaymentMethod"] = "";
            rw["PaymentAuthorisationNumber"] = "";
            rw["PaymentCardNumber"] = "";
            rw["PaymentNameOnCard"] = "";
            rw["PaymentExpirationDate"] = "";
            rw["PaymentCardNotes"] = "";
            rw["PaymentBSB"] = "";
            rw["PaymentBankAccountNumber"] = "";
            rw["PaymentBankAccountName"] = "";
            rw["PaymentChequeNumber"] = "";
            rw["PaymentBankNotes"] = "";
            rw["PaymentNotes"] = "";
            rw["PaymentGCNo"] = "";
            rw["PaymentGCNotes"] = "";


            PaymentInfoTb.Rows.Add(rw);
        }
        private void InitVoidTable()
        {
            dtv = new DataTable();
            dtv.Columns.Add("UserName", typeof(string));
            dtv.Columns.Add("AuditAction", typeof(string));
        }
        public decimal qtyvalidation(decimal itemqty)
        {
            if (itemqty < 0)
            {
                VoidValidation vv = new VoidValidation();
                if (vv.ShowDialog() == DialogResult.OK)
                {
                    DataRow rw = dtv.NewRow();

                    rw["UserName"] = vv.GetUsername;
                    rw["AuditAction"] = "Quantity over ride to " + itemqty + " by " + vv.GetUsername;
                    dtv.Rows.Add(rw);
                }
            }
            return itemqty;
        }
        public void saveValidationLog(int salesID)
        {
            foreach (DataRow drv in dtv.Rows)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, drv["AuditAction"].ToString(), salesID.ToString());
            }
        }

        private void GenerateInvoiceNum()
        {
            SqlConnection con_ua = null;
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);
                string selectSql_ua = @"SELECT SalesOrderSeries, 
                                               SalesOrderPrefix, 
                                               SalesQuoteSeries, 
                                               SalesQuotePrefix,
                                               SalesInvoiceSeries,
                                               SalesInvoicePrefix,
                                               SaleLayBySeries,
                                               SaleLayByPrefix 
                                        FROM TransactionSeries";
                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                string lSeries = "";
                int lCnt = 0;
                int lNewSeries = 0;

                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (salestype_cb.Text == "ORDER")
                            {
                                lSeries = (reader["SalesOrderSeries"].ToString());
                                lCnt = lSeries.Length;
                                lSeries = lSeries.TrimStart('0');
                                lSeries = (lSeries == "" ? "0" : lSeries);
                                lNewSeries = Convert.ToInt16(lSeries) + 1;
                                CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                                this.InvoiceNumTxt.Text = invoicenum = (reader["SalesOrderPrefix"].ToString()).Trim(' ') + CurSeries;
                            }
                            else if (salestype_cb.Text == "QUOTE")
                            {
                                lSeries = (reader["SalesQuoteSeries"].ToString());
                                lCnt = lSeries.Length;
                                lSeries = lSeries.TrimStart('0');
                                lSeries = (lSeries == "" ? "0" : lSeries);
                                lNewSeries = Convert.ToInt16(lSeries) + 1;
                                CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                                this.InvoiceNumTxt.Text = invoicenum = (reader["SalesQuotePrefix"].ToString()).Trim(' ') + CurSeries;
                            }
                            else if (salestype_cb.Text == "LAY-BY")
                            {
                                lSeries = (reader["SaleLayBySeries"].ToString());
                                lCnt = lSeries.Length;
                                lSeries = lSeries.TrimStart('0');
                                lSeries = (lSeries == "" ? "0" : lSeries);
                                lNewSeries = Convert.ToInt16(lSeries) + 1;
                                CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                                this.InvoiceNumTxt.Text = invoicenum = (reader["SaleLayByPrefix"].ToString()).Trim(' ') + CurSeries;
                            }
                            else
                            {
                                lSeries = (reader["SalesInvoiceSeries"].ToString());
                                lCnt = lSeries.Length;
                                lSeries = lSeries.TrimStart('0');
                                lSeries = (lSeries == "" ? "0" : lSeries);
                                lNewSeries = Convert.ToInt16(lSeries) + 1;
                                CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                                this.InvoiceNumTxt.Text = invoicenum = (reader["SalesInvoicePrefix"].ToString()).Trim(' ') + CurSeries;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transaction Series Numbers not setup properly.");
                        this.BeginInvoke(new MethodInvoker(Close));
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ua != null)
                    con_ua.Close();
            }
        }

        private void pbAccount_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
            dgEnable();
        }

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                CustomerID = lProfile[0];
                this.customerText.Text = lProfile[2];
                ShippingmethodText.Text = lProfile[4];
                TermRefID = lProfile[5].ToString();
                ProfileTax = lProfile[6].ToString();
                MethodPaymentID = lProfile[8].ToString();
                CustomerBalance = (lProfile[3].ToString() == "" ? 0 : float.Parse(lProfile[3].ToString(), NumberStyles.Currency));
                CustomerCreditLimit = (lProfile[12].ToString() == "" ? 0 : float.Parse(lProfile[12].ToString(), NumberStyles.Currency));
                TermsText.Visible = true;
                MemoText.Text = "Sale; " + lProfile[2];
                ShippingmethodText.Text = lProfile[4];
                ShippingMethodID = (lProfile[11] == "" ? "0" : lProfile[11]);
                InvoiceNumTxt.Visible = true;
                LoadDefaultTerms(CustomerID);
                LoadFreightTax(ProfileTax);
                LoadPaymentMethod();
                LoadPoints();
                FormCheck();
                ApplyVoidAccess();
                btnAddShipAddress.Enabled = true;

                string sql = "SELECT ContactID, TypeOfContact FROM Contacts WHERE ProfileID = @ProfileID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProfileID", CustomerID);
                param.Add("@Location", lProfile[14]);
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, sql, param);
                List<KeyValuePair<string, string>> mylist = new List<KeyValuePair<string, string>>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        mylist.Add(new KeyValuePair<string, string>(dr["ContactID"].ToString(), dr["TypeOfContact"].ToString()));
                    }
                    cmb_shippingcontact.DataSource = new BindingSource(mylist, null);
                    cmb_shippingcontact.DisplayMember = "Value";
                    cmb_shippingcontact.ValueMember = "Key";
                }
                LoadContacts(Convert.ToInt32(CustomerID), Convert.ToInt32(lProfile[14]));
            }
        }

        private void dgEnterSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvrow = dgEnterSales.CurrentRow;
            if (e.RowIndex < 0)
                return;

            switch (e.ColumnIndex)
            {
                case 3: //PartNumber
                    if (customerText.Text != "")
                    {
                        if (dgEnterSales.Rows[e.RowIndex].Cells["PartNumber"].Value != null)
                        {
                            ShowItemLookup("", "PartNumber");
                            itemcalc(e.RowIndex);
                            Recalcline(e.ColumnIndex, e.RowIndex);
                            CalcOutOfBalance();

                        }
                    }


                    dgEnable();
                    break;
                case 4:
                    this.dgEnterSales.CurrentCell = this.dgEnterSales.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgEnterSales.BeginEdit(true);
                    break;
                case 7: //GL ACCOUNT
                    if (customerText.Text != "")
                    {
                        //ShowAccountLookup("");
                    }
                    dgvrow.Cells["Amount"].ReadOnly = false;
                    dgvrow.Cells["Job"].ReadOnly = false;
                    dgvrow.Cells["TaxCode"].ReadOnly = false;
                    dgEnable();
                    break;

                case 9://Job
                    if (customerText.Text != "")
                    {
                        ShowJobLookup();
                    }
                    break;
                case 10://Tax
                    if (customerText.Text != "")
                    {
                        TaxCodeLookup_btn_Click();
                        itemcalc(e.RowIndex);
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                    }
                    break;
                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }
        public void ContractPriceAmount(string cusID)
        {
            DateTime dtime = DateTime.Now;
            //DateTime dtpfromutc = datenow.Value.ToUniversalTime();
            DateTime timeutc = dtime.ToUniversalTime();
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sqlSelect = @"SELECT c.ContractPrice, c.IsExpiry, c.ExpiryDate FROM Items i 
            INNER JOIN ContractPricing c on i.ID = c.ItemID
            WHERE c.CustomerID = " + cusID + "AND c.ExpiryDate > @DateNow";
            param.Add("@DateNow", dtime);
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sqlSelect, param);
            DataGridViewRow dgvRows = dgEnterSales.CurrentRow;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvRows.Cells["Price"].Value = dr["ContractPrice"].ToString();
                }
                isContractPrice = true;
            }
        }
        public bool CheckExistItemInGrid(string pNum)
        {
            int shipCount = 0;
            if (dgEnterSales.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
                {
                    if (dgvr.Cells["PartNumber"].Value != null)
                    {
                        if (dgvr.Cells["PartNumber"].Value.ToString() == pNum)
                        {
                            shipCount += 1;
                            dgvr.Cells["Ship"].Value =  int.Parse(dgvr.Cells["Ship"].Value.ToString()) +1;
                            itemcalc(dgvr.Index);
                            Recalcline(8, dgvr.Index);
                            CalcOutOfBalance();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum, CustomerID, whereCon);

            DataGridViewRow dgvRows = dgEnterSales.CurrentRow;
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                if (!CheckExistItemInGrid(ItemRows.Cells[1].Value.ToString()))
                    {
                    dgvRows.Cells["ItemID"].Value = ItemRows.Cells[0].Value.ToString();
                    dgvRows.Cells["PartNumber"].Value = ItemRows.Cells[1].Value;
                    dgvRows.Cells["Description"].Value = ItemRows.Cells[3].Value.ToString();
                    ItemOnHand = Convert.ToInt32(ItemRows.Cells[4].Value.ToString());
                    dgvRows.Cells["TaxCode"].Value = ItemRows.Cells["SalesTaxCode"].Value.ToString();
                    dgvRows.Cells["CostPrice"].Value = ItemRows.Cells["AverageCostEx"].Value.ToString();
                    dgvRows.Cells["Price"].ReadOnly = false;
                    dgvRows.Cells["Description"].ReadOnly = false;
                    dgvRows.Cells["Amount"].ReadOnly = false;
                    dgvRows.Cells["Job"].ReadOnly = false;
                    dgvRows.Cells["TaxCode"].ReadOnly = false;
                    dgvRows.Cells["Supplier"].Value = int.Parse(ItemRows.Cells["SupplierID"].Value.ToString());
                    dgvRows.Cells["CategoryID"].Value = int.Parse(ItemRows.Cells["CategoryID"].Value.ToString());
                    dgvRows.Cells["Brand"].Value = ItemRows.Cells["BrandName"].Value.ToString();
                    dgvRows.Cells["AutoBuild"].Value = ItemRows.Cells["IsAutoBuild"].Value.ToString();
                    dgvRows.Cells["BundleType"].Value = ItemRows.Cells["BundleType"].Value.ToString();
                    float ltaxrate = 0;
                    DataRow rTx = CommonClass.getTaxDetails(ItemRows.Cells["SalesTaxCode"].Value.ToString());
                    //if (rTx.ItemArray.Length > 0)
                    //{
                    ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                    string lTaxCollectedAccountID = "";
                    lTaxCollectedAccountID = ((rTx["TaxCollectedAccountID"] == null || rTx["TaxCollectedAccountID"].ToString() == "") ? "0" : rTx["TaxCollectedAccountID"].ToString());
                    dgvRows.Cells["TaxCollectedAccountID"].Value = lTaxCollectedAccountID;
                    dgvRows.Cells["TaxRate"].Value = ltaxrate;
                    //}
                    //Assume that Price is already Inclusive
                    float lSellingPriceInc = float.Parse(ItemRows.Cells[8].Value.ToString(), NumberStyles.Currency);
                    float lSellingPriceEx = (ltaxrate != 0 ? lSellingPriceInc / (1 + (ltaxrate / 100)) : lSellingPriceInc);
                    if (!CommonClass.IsItemPriceInclusive)//Recalc if not inclusive
                    {
                        lSellingPriceEx = lSellingPriceInc;
                        lSellingPriceInc = (ltaxrate != 0 ? lSellingPriceEx * (1 + (ltaxrate / 100)) : lSellingPriceEx);
                    }
                    //Fill in the grid
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        ContractPriceAmount(CustomerID);
                        if (!isContractPrice)
                        {
                            dgvRows.Cells["Price"].Value = lSellingPriceInc;
                            dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceInc;
                        }
                    }
                    else
                    {
                        ContractPriceAmount(CustomerID);
                        if (!isContractPrice)
                        {
                            dgvRows.Cells["Price"].Value = lSellingPriceEx;
                            dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceEx;
                        }
                    }
                    btnSaveRecurring.Enabled = true;

                    mPriceEx = lSellingPriceEx;
                    //if ((bool)ItemRows.Cells["IsCounted"].Value && salestype_cb.Text == "INVOICE")
                    //{
                    //    float lToShipQty = float.Parse(dgvRows.Cells["Ship"].Value.ToString());
                    //    if(ItemOnHand < lToShipQty)
                    //    {
                    //        MessageBox.Show("Not Enought Quantity.");
                    //        dgvRows.Cells["Ship"].Value = ItemOnHand;
                    //    }
                    //}
                }
                else
                {
                    dgvRows.Cells["Ship"].Value = "";
                    dgvRows.Cells["PartNumber"].Value = "";
                    dgvRows.Cells["Amount"].Value = "";
                    return false;

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private float ComputePromo(string accumulation, float points, float pointsvalue, float PurchasePrice, int promoID)
        {
            float ltaxexprice = 0;

            //IF DEFAULT PROMOTYPE, GET THE FINAL PRICE OF THE ITEM
            if (CommonClass.IsTaxcInclusiveEnterSales)
            {
                if (dgEnterSales.CurrentRow.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgEnterSales.CurrentRow.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx / (1 + (lTaxRate / 100));
                    ltaxexprice = PurchasePrice / (1 + (lTaxRate / 100));
                }
            }
            else
            {
                if (dgEnterSales.CurrentRow.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgEnterSales.CurrentRow.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx;
                    ltaxexprice = PurchasePrice;
                }
            }

            if (accumulation == "Points (X)")
            {
                points += pointsvalue * float.Parse(dgEnterSales.CurrentRow.Cells[1].Value == null ? "0" : dgEnterSales.CurrentRow.Cells[1].Value.ToString());
            }
            else if (accumulation == "Points (X) percentage")
            {
                points = ((pointsvalue / 100) * ltaxexprice) * float.Parse(dgEnterSales.CurrentRow.Cells[1].Value == null ? "0" : dgEnterSales.CurrentRow.Cells[1].Value.ToString());
            }

            else if (accumulation == "Points (X) percentage profit")
            {
                //Get the total cost (costprice)
                float costprice = float.Parse(dgEnterSales.CurrentRow.Cells["CostPrice"].Value == null ? "0" : dgEnterSales.CurrentRow.Cells["CostPrice"].Value.ToString());
                //get the profit
                float profit = ltaxexprice - costprice;
                points = ((pointsvalue / 100) * profit) * float.Parse(dgEnterSales.CurrentRow.Cells[1].Value == null ? "0" : dgEnterSales.CurrentRow.Cells[1].Value.ToString());
            }

            float lOrigUnitPrice;
            //IF NON-DEFAULT PROMO TYPE, GET THE ACTUAL UNIT PRICE 
            if (dgEnterSales.CurrentRow.Cells["ActualUnitPrice"].Value == null || dgEnterSales.CurrentRow.Cells["ActualUnitPrice"].Value.ToString() == "")
            {
                lOrigUnitPrice = 0;
            }
            else
            {
                lOrigUnitPrice = float.Parse(dgEnterSales.CurrentRow.Cells["ActualUnitPrice"].Value.ToString());
            }
            if (CommonClass.IsTaxcInclusiveEnterSales)
            {
                if (dgEnterSales.CurrentRow.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgEnterSales.CurrentRow.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx / (1 + (lTaxRate / 100));
                    ltaxexprice = lOrigUnitPrice / (1 + (lTaxRate / 100));
                }
            }
            else
            {
                if (dgEnterSales.CurrentRow.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgEnterSales.CurrentRow.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx;
                    ltaxexprice = lOrigUnitPrice;
                }
            }
            if (accumulation == "Fixed (X) Discount")
            {
                float difPrice = ltaxexprice - pointsvalue;
                if (PurchasePrice > difPrice)
                {
                    //Change only if difprice will be lesser than the purchase price
                    dgEnterSales.CurrentRow.Cells["Price"].Value = difPrice;
                }

                points = 0;
            }
            else if (accumulation == "Percentage Discount")
            {
                float discountedprice = ltaxexprice - (ltaxexprice * (pointsvalue / 100));
                if (PurchasePrice > discountedprice)
                {
                    //Change only if pointsvalue will be lesser than the purchase price
                    dgEnterSales.CurrentRow.Cells["Price"].Value = discountedprice;
                }

                points = 0;
            }
            else if (accumulation == "Price (X)")
            {
                if (PurchasePrice > pointsvalue)
                {
                    //Change only if pointsvalue will be lesser than the purchase price
                    dgEnterSales.CurrentRow.Cells["Price"].Value = pointsvalue;
                }

                points = 0;
            }
            else if (accumulation == "Free Product")
            {
                int rowindex = dgEnterSales.CurrentRow.Index;
                if (dgEnterSales.Rows[rowindex].Cells["PartNumber"].ToString() != "")
                {
                    FreeProducts FreeLookup = new FreeProducts(CommonClass.FreeItemInvocation.ENTERSALES, FreeTable, promoID, (int)pointsvalue);
                    if (FreeLookup.ShowDialog() == DialogResult.OK)
                    {
                        FreeTable = FreeLookup.GetFreeProductTable;
                        if (FreeTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < FreeTable.Rows.Count; i++)
                            {
                                DataRow dgRow = FreeTable.Rows[i];
                                rowindex++;
                                dgEnterSales.Rows[rowindex].Cells["ItemID"].Value = dgRow["ItemID"].ToString();
                                dgEnterSales.Rows[rowindex].Cells["Ship"].Value = 1;//Convert.ToInt32(dgRow["Quantity"].ToString()) * Convert.ToInt32(dgEnterSales.Rows[rowindex].Cells["Ship"].Value.ToString());
                                dgEnterSales.Rows[rowindex].Cells["PartNumber"].Value = dgRow["PartNum"].ToString();
                                dgEnterSales.Rows[rowindex].Cells["Description"].Value = "Free Product " + dgRow["ItemName"].ToString();
                                dgEnterSales.Rows[rowindex].Cells["Price"].Value = 0;
                                dgEnterSales.Rows[rowindex].Cells["ActualUnitPrice"].Value = float.Parse(dgRow["Price"].ToString());
                                dgEnterSales.Rows[rowindex].Cells["Amount"].Value = 0;
                                dgEnterSales.Rows[rowindex].Cells["TaxCode"].Value = dgRow["SalesTaxCode"].ToString() == "" ? "N-T" : dgRow["SalesTaxCode"].ToString();
                                DataRow rTx = CommonClass.getTaxDetails(dgEnterSales.Rows[rowindex].Cells["TaxCode"].Value.ToString());
                                //if (rTx.ItemArray.Length > 0)
                                //{
                                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                                string lTaxPaidAccountID = "";
                                lTaxPaidAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                                dgEnterSales.Rows[rowindex].Cells["TaxCollectedAccountID"].Value = lTaxPaidAccountID;
                                dgEnterSales.Rows[rowindex].Cells["TaxRate"].Value = ltaxrate;
                                dgEnterSales.Rows[rowindex].Cells["PromoID"].Value = promoID;
                                dgEnterSales.Rows[rowindex].Cells["CostPrice"].Value = float.Parse(dgRow["CostPrice"].ToString());
                                Recalcline(8, rowindex);

                                //       }

                                //dgEnterSales.Rows[rowindex].Cells["TaxRate"].Value = 0;
                                //dgEnterSales.Rows[rowindex].Cells["TaxCode"].Value = "N-T";
                                //dgEnterSales.Rows[rowindex].Cells["TaxCollectedAccountID"].Value = 0;

                            }
                        }
                        //dgEnterSales.ClearSelection();
                        // dgEnterSales.Rows[rowindex].Cells["Ship"].Selected = true;
                    }
                    //FreeProductAmt = 0;
                    isFreeProduct = true;
                    FreeTable.Clear();
                }

            }
            else if (accumulation == "Percentage (X) Discount From List")
            {
                float listprice = GetItemListPrice(dgEnterSales.CurrentRow.Cells["ItemID"].Value.ToString());
                float percentDiscount = ((pointsvalue / 100) * listprice) * float.Parse(dgEnterSales.CurrentRow.Cells[1].Value == null ? "0" : dgEnterSales.CurrentRow.Cells[1].Value.ToString());

                float discounted = listprice - percentDiscount;
                if (PurchasePrice > discounted)
                {
                    //Change only if discounted will be lesser than the purchase price
                    dgEnterSales.CurrentRow.Cells["Price"].Value = discounted;
                }
                points = 0;
            }

            dgEnterSales.CurrentRow.Cells["PromoID"].Value = promoID;
            return points;
        }

        private float GetItemListPrice(string pItemID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            float listprice = 0;
            try
            {
                string sql = @"SELECT * from ItemsSellingPrice where ItemID = " + pItemID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                if (RTb.Rows.Count > 0)
                {
                    listprice = float.Parse(RTb.Rows[0]["Level0"].ToString());
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
            return listprice;
        } //END

        private void InitFreeTable()
        {
            FreeTable = new DataTable();
            FreeTable.Columns.Add("ItemID", typeof(int));
            FreeTable.Columns.Add("Quantity", typeof(string));
            FreeTable.Columns.Add("ItemName", typeof(string));
            FreeTable.Columns.Add("PartNum", typeof(string));
            FreeTable.Columns.Add("SalesTaxCode", typeof(string));
            FreeTable.Columns.Add("Price", typeof(float));
            FreeTable.Columns.Add("CostPrice", typeof(float));
            //FreeTable.Rows.Add();
        }

        private float isItemPromo(int ItemID, float PurchasePrice, string pType = "")
        {
            string itemPromoJson;
            float points = 0;
            float pointsvalue = 0;
            DataTable dtPromo = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            string lpromotype = (pType == "" ? " PromotionType <> 'Default'" : " PromotionType = 'Default'");
            CommonClass.runSql(ref dtPromo, "SELECT * FROM Promos WHERE isActive = 1 AND GETDATE() BETWEEN StartDate AND EndDate AND " + lpromotype);
            string accumulation = "";
            if (dtPromo.Rows.Count > 0)
            {
                foreach (DataRow x in dtPromo.Rows)
                {
                    string file = x["RuleCriteriaID"].ToString();
                    accumulation = x["PointAccumulationCriteria"].ToString();
                    pointsvalue = float.Parse(x["PointsValue"].ToString());
                    x["RuleCriteria"].ToString().Replace("\t", "");
                    itemPromoJson = file.Replace("\t", "");
                    itemPromos = JsonConvert.DeserializeObject<List<RuleCriteriaPoints>>(itemPromoJson);
                    isLoyal = JsonConvert.DeserializeObject<List<RuleCriteria>>(x["RuleCriteria"].ToString().Replace("\t", ""));
                    bool includepromo = false;
                    foreach (RuleCriteria criteria in isLoyal)
                    {
                        if (criteria.CriteriaName == "Loyalty"
                            && criteria.CriteriaValue == "False")
                        {
                            if (isLoyal.Count == 1)
                                includepromo = true;
                        }
                        else if (criteria.CriteriaName == "Loyalty"
                                && criteria.CriteriaValue == "True")
                        {
                            includepromo = ismember;
                        }

                        if (!includepromo)
                        {
                            if (criteria.CriteriaName == "Customer"
                                && criteria.CriteriaValue != "")
                            {
                                if (CustomerID == criteria.CriteriaValue)
                                {
                                    includepromo = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (includepromo)
                    {
                        foreach (RuleCriteriaPoints promo in itemPromos)
                        {
                            if (promo.CriteriaName == "Item")
                            {
                                if (promo.CriteriaValue == ItemID.ToString())
                                {
                                    return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()));
                                }
                            }
                            else if (promo.CriteriaName == "Supplier")
                            {
                                if (dgEnterSales.CurrentRow.Cells["Supplier"].Value.ToString() == promo.CriteriaValue)
                                {
                                    return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()));
                                }
                            }
                            else if (promo.CriteriaName == "Category")
                            {
                                if (dgEnterSales.CurrentRow.Cells["CategoryID"].Value.ToString() == promo.CriteriaValue)
                                {
                                    return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()));
                                }
                            }
                            else if (promo.CriteriaName == "Category")
                            {
                                if (dgEnterSales.CurrentRow.Cells["CategoryID"].Value.ToString() == promo.CriteriaValue)
                                {
                                    return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()));
                                }
                            }
                            else if (promo.CriteriaName == "Brand")
                            {
                                if (dgEnterSales.CurrentRow.Cells["Brand"].Value.ToString() == promo.CriteriaValue)
                                {
                                    return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()));
                                }
                            }
                        }
                    }
                }
            }

            return points;
        }

        private void Shippingmethod_btn_Click(object sender, EventArgs e)
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;
                ShippingmethodText.Text = ShipList[0];
                ShippingMethodID = ShipList[1];
                ShipVia = ShipList[0];
            }
        }

        private void ClearRowItems(int pRowIndex)
        {
            //dgEnterSales.Rows[pRowIndex].Cells["Description"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["Price"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["Discount"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["Amount"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["Job"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["TaxCode"].Value = null;
        }

        private void dgEnterSales_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                if (e.ColumnIndex.ToString() != "")
                {
                    enter = true;
                }
            }
        }
        private void VerifyItemQty(int pRIndex, string pSalesID = "0")
        {
            //CHECK ITEM ON HAND QTY
            if (this.salestype_cb.Text == "INVOICE")
            {
                if (dgEnterSales.Rows[pRIndex].Cells["ItemID"].Value != null)
                {
                    string lItemID = dgEnterSales.Rows[pRIndex].Cells["ItemID"].Value.ToString();
                    decimal lShipQty = Convert.ToDecimal(dgEnterSales.Rows[pRIndex].Cells["Ship"].Value.ToString());
                    if (lItemID != "" || lItemID != "0")
                    {
                        DataTable ltb = GetNewEndingQty(lItemID);
                        if ((bool)ltb.Rows[0]["IsCounted"])
                        {
                            decimal lOnHandQty = Convert.ToDecimal(ltb.Rows[0]["OnHandQty"].ToString());
                            decimal lPrevShipQty = GetPrevShipQty(pSalesID, lItemID);
                            decimal lNewQty = lOnHandQty + lPrevShipQty - lShipQty;

                            if (lNewQty < 0)
                            {
                                MessageBox.Show("Not Enough Quantity.");
                                dgEnterSales.Rows[pRIndex].Cells["Ship"].Value = lOnHandQty;
                                DataGridViewCellEventArgs le = new DataGridViewCellEventArgs(1, pRIndex);

                                dgEnterSales_CellEndEdit(null, le);
                            }
                        }

                    }


                }


            }
        }

        private void dgEnterSales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)//Qty
            {
                if (CommonClass.isSalesperson == true || CommonClass.isTechnician == true)
                {

                    if (dgEnterSales.Rows[e.RowIndex].Cells["Ship"].Value != null)
                    {
                        dgEnterSales.Rows[e.RowIndex].Cells["Ship"].Value = qtyvalidation(decimal.Parse(dgEnterSales.Rows[e.RowIndex].Cells["Ship"].Value.ToString()));
                    }

                }
            }
            if (e.ColumnIndex == 5)//Ship
            {
                if (CommonClass.isSalesperson == true || CommonClass.isTechnician == true)
                {
                    VoidValidation DlgVoid = new VoidValidation();
                    if (DlgVoid.ShowDialog() == DialogResult.OK)
                    {
                        password = DlgVoid.GetPassword;
                        username = DlgVoid.GetUsername;
                        if (dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value != null)
                        {
                            string lPrice = dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                            float Price = float.Parse(lPrice);
                            dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value = Price;
                        }
                        DataRow rw = dtv.NewRow();

                        rw["AuditAction"] = "Price override to " + dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value.ToString() + " by " + DlgVoid.GetUsername;
                        dtv.Rows.Add(rw);
                    }
                    else
                    {
                        dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value = dgEnterSales.Rows[e.RowIndex].Cells["ActualUnitPrice"].Value;
                    }
                }
                else//Not SalesPerson
                {
                    if (dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value != null)
                    {
                        string lPrice = dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                        float Price = float.Parse(lPrice);
                        dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value = Price;
                        DataRow rw = dtv.NewRow();

                        rw["AuditAction"] = "Price override to " + dgEnterSales.Rows[e.RowIndex].Cells["Price"].Value.ToString() + " by " + CommonClass.UserName;
                        dtv.Rows.Add(rw);
                    }
                }
            }
            if (e.ColumnIndex == 6)
            {
                if (CommonClass.isSalesperson == true || CommonClass.isTechnician == true)
                {
                    VoidValidation DlgVoid = new VoidValidation();
                    if (DlgVoid.ShowDialog() == DialogResult.OK)
                    {
                        password = DlgVoid.GetPassword;
                        username = DlgVoid.GetUsername;
                        if (dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value != null)
                        {
                            string lDiscRate = dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value.ToString().Replace("%", "");
                            float lDRate = float.Parse(lDiscRate);
                            if (lDRate > 100)
                            {
                                MessageBox.Show("Discount cannot exceed 100%", "Information");
                                dgEnterSales.Rows[e.RowIndex].Cells["DiscountRate"].Value = null;
                                dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value = null;
                            }
                            else
                            {
                                dgEnterSales.Rows[e.RowIndex].Cells["DiscountRate"].Value = lDRate;
                            }
                        }
                        DataRow rw = dtv.NewRow();

                        rw["AuditAction"] = "Discpunt override to " + dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value.ToString() + " by " + DlgVoid.GetUsername;
                        dtv.Rows.Add(rw);
                    }
                    else
                    {
                        dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value = null;
                        dgEnterSales.Rows[e.RowIndex].Cells["DiscountRate"].Value = null;

                    }
                }
                else
                {
                    if (dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value != null)
                    {
                        string lDiscRate = dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value.ToString().Replace("%", "");
                        float lDRate = float.Parse(lDiscRate);
                        if (lDRate > 100)
                        {
                            MessageBox.Show("Discount cannot exceed 100%", "Information");
                            dgEnterSales.Rows[e.RowIndex].Cells["DiscountRate"].Value = null;
                            dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value = null;
                        }
                        else
                        {
                            dgEnterSales.Rows[e.RowIndex].Cells["DiscountRate"].Value = lDRate;
                            DataRow rw = dtv.NewRow();
                            rw["AuditAction"] = "Discount override to " + dgEnterSales.Rows[e.RowIndex].Cells["Discount"].Value.ToString() + " by " + CommonClass.UserName;
                            dtv.Rows.Add(rw);
                        }

                    }
                }

            }

            if (e.ColumnIndex == 1
                || e.ColumnIndex == 2
                || e.ColumnIndex == 5
                || e.ColumnIndex == 6)
            {
                itemcalc(e.RowIndex);
                Recalcline(e.ColumnIndex, e.RowIndex);

                Recalc();
                CalcOutOfBalance();
            }
            if (e.ColumnIndex == 3) //PartNumber
            {
                ClearRowItems(e.RowIndex);
                if (dgEnterSales.CurrentCell.Value != null)
                {
                    if (ShowItemLookup(dgEnterSales.CurrentCell.Value.ToString(), "PartNumber"))
                    {
                        itemcalc(e.RowIndex);
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                    }

                }
                else
                {
                    if (ShowItemLookup("", "PartNumber"))
                    {
                        itemcalc(e.RowIndex);
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                    }

                }
                btnSaveRecurring.Enabled = true;
            }
            else if (e.ColumnIndex == 7)//Account Number
            {
                if (enter == true)
                {
                    ClearRowItems(e.RowIndex);
                    if (dgEnterSales.CurrentCell.Value != null)
                    {
                        //ShowAccountLookup(dgEnterSales.CurrentCell.Value.ToString());
                    }
                    else
                    {
                        //ShowAccountLookup("");
                    }
                    itemcalc(e.RowIndex);
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                    enter = false;

                }
            }
            else if (e.ColumnIndex == 9)//Job
            {
                if (dgEnterSales.CurrentCell.Value != null)
                {
                    ShowJobLookup(dgEnterSales.CurrentCell.Value.ToString());
                }
            }
            else if (e.ColumnIndex == 10)//Tax
            {
                if (dgEnterSales.CurrentCell.Value != null)
                {
                    TaxCodeLookup_btn_Click(dgEnterSales.CurrentCell.Value.ToString());
                    itemcalc(e.RowIndex);
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                }
            }

            if (e.ColumnIndex == 8)//Amount
            {
                if (CommonClass.isSalesperson == true)
                {
                    string loyaltysql = "SELECT * FROM Users";
                    DataTable dtSupervisors = new DataTable();

                    VoidValidation DlgVoid = new VoidValidation();
                    if (DlgVoid.ShowDialog() == DialogResult.OK)
                    {
                        password = DlgVoid.GetPassword; //Interaction.InputBox("Please Enter Supervisor Password", "Void Request", "");
                        username = DlgVoid.GetUsername;
                    }
                    else
                    {
                        dgEnterSales.CurrentRow.Cells[e.ColumnIndex].Value = prevAmt;
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                        Recalc();
                        btnSaveRecurring.Enabled = true;
                    }

                    loyaltysql += " WHERE user_name = '" + username + "' AND user_pwd = '" + CommonClass.SHA512(password) + "'";
                    CommonClass.runSql(ref dtSupervisors, loyaltysql);
                    string curAmt = "";
                    if (dgEnterSales.CurrentRow.Cells[e.ColumnIndex].Value != null)
                    {
                        curAmt = dgEnterSales.CurrentRow.Cells[e.ColumnIndex].Value.ToString();
                    }
                    else
                    {
                        curAmt = "0";
                        // dgEnterSales.CurrentRow.Cells[e.ColumnIndex].Value = prevAmt.ToString();
                    }

                    if (dtSupervisors.Rows.Count > 0 && dtSupervisors.Rows[0]["user_role"].ToString() == "Supervisor")
                    {
                        if (prevAmt != curAmt)
                        {
                            Recalcline(e.ColumnIndex, e.RowIndex);
                            CalcOutOfBalance();
                            Recalc();
                            btnSaveRecurring.Enabled = true;
                        }
                        else
                        {
                            dgEnterSales.CurrentRow.Cells[e.ColumnIndex].Value = prevAmt.ToString();
                        }
                    }
                    else
                    {
                        dgEnterSales.CurrentRow.Cells[e.ColumnIndex].Value = prevAmt;
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                        Recalc();
                        btnSaveRecurring.Enabled = true;
                    }
                }
                else
                {
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                    Recalc();
                    btnSaveRecurring.Enabled = true;
                }

            }


            TotalAmountText.Visible = true;
            subtotalAmountText.Visible = true;
        }

        public void Recalcline(int pColIndex, int pRowIndex)
        {
            if (pRowIndex < 0)
                return;

            if (!IsLoading)
            {
                DataGridViewRow dgvRows = dgEnterSales.Rows[pRowIndex];

                float lTaxEx = 0;
                float lTaxInc = 0;
                float lTaxRate = 0;
                float lAmount = 0;

                if (pColIndex > 0)
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (dgvRows.Cells["Amount"].Value != null
                            && dgvRows.Cells["TaxRate"].Value != null)
                        {
                            lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                            lAmount = float.Parse(dgvRows.Cells["Amount"].Value.ToString(), NumberStyles.Currency);
                            lTaxEx = lAmount / (1 + (lTaxRate / 100));
                            lTaxInc = lAmount;
                            dgvRows.Cells["TaxExclusiveAmount"].Value = lTaxEx;
                            dgvRows.Cells["TaxInclusiveAmount"].Value = lTaxInc;
                        }
                    }
                    else
                    {
                        if (dgvRows.Cells["Amount"].Value != null
                            && dgvRows.Cells["TaxRate"].Value != null)
                        {
                            lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                            lAmount = float.Parse(dgvRows.Cells["Amount"].Value.ToString(), NumberStyles.Currency);
                            lTaxInc = lAmount * (1 + (lTaxRate / 100));
                            lTaxEx = lAmount;
                            dgvRows.Cells["TaxExclusiveAmount"].Value = lTaxEx;
                            dgvRows.Cells["TaxInclusiveAmount"].Value = lTaxInc;
                        }
                    }
                }
                else
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (dgvRows.Cells["TaxInclusiveAmount"].Value != null)
                        {
                            dgvRows.Cells["Amount"].Value = dgvRows.Cells["TaxInclusiveAmount"].Value;
                            prevAmt = dgvRows.Cells["TaxInclusiveAmounts"].Value.ToString();
                        }
                    }
                    else
                    {
                        if (dgvRows.Cells["TaxExclusiveAmount"].Value != null)
                        {
                            dgvRows.Cells["Amount"].Value = dgvRows.Cells["TaxExclusiveAmount"].Value;
                            prevAmt = dgvRows.Cells["TaxExclusiveAmount"].Value.ToString();
                        }
                    }
                }
            }
        }

        private void dgEnterSales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 //Price
             || e.ColumnIndex == 8 //Amount
              || e.ColumnIndex == 18
             && e.RowIndex != this.dgEnterSales.NewRowIndex)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
            else if (e.ColumnIndex == 6 //Discount
             && e.RowIndex != this.dgEnterSales.NewRowIndex)
            {
                if (e.Value != null)
                {
                    string p = e.Value.ToString().Replace("%", "");
                    float d = float.Parse(p);
                    e.Value = Math.Round(d, 2).ToString() + "%";
                }
            }
        }

        public void CalcOutOfBalance()
        {
            this.TotalAmount.Value = 0;
            this.TaxAmount.Value = 0;
            BalanceDue_txt.Value = 0;
            decimal TotalTaxEx = 0;
            decimal TotalTaxInc = 0;
            decimal outnum;
            decimal TaxEx = 0;
            decimal TaxInc = 0;
            decimal CurAmt = 0;

            for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
            {
                if (this.dgEnterSales.Rows[i].Cells["Amount"].Value != null)
                {
                    if (this.dgEnterSales.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        Decimal.TryParse(this.dgEnterSales.Rows[i].Cells["Amount"].Value.ToString(), out outnum);
                        this.TotalAmount.Value += outnum;
                        CurAmt = outnum;
                        if (CurAmt != 0)
                        {
                            Decimal.TryParse(dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value.ToString(), out outnum);
                            TaxEx = outnum;
                            TotalTaxEx += TaxEx;
                            Decimal.TryParse(this.dgEnterSales.Rows[i].Cells["TaxInclusiveAmount"].Value.ToString(), out outnum);
                            TaxInc = outnum;
                            TotalTaxInc += TaxInc;
                        }
                    }
                }
            }

            this.TaxAmount.Value = (TotalTaxInc - TotalTaxEx) + (decimal)FreightTax;
            this.subtotalAmountText.Value = TotalTaxEx;
            TotalAmount.Value = TotalTaxInc + (decimal)FreightAmountInc;
            this.BalanceDue_txt.Value = TotalAmount.Value - PaidToday_txt.Value;
            //if (salestype_cb.Text == "INVOICE" && rdbCash.Checked)
            //{
            //    decimal amtpd = Convert.ToDecimal(PaymentInfoTb.Rows[0]["AmountPaid"].ToString());
            //    if (amtpd != 0)
            //    {

            //    } else
            //    {
            //        //this.BalanceDue_txt.Visible = false;
            //        //this.lblBalanceDue.Visible = false;
            //    }
            //} else
            //{
            //    this.BalanceDue_txt.Value = TotalAmount.Value - PaidToday_txt.Value;
            //}

        }

        //private void chckInc_CheckedChanged(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
        //    {
        //        if (this.dgEnterSales.Rows[i].Cells["Amount"] != null)
        //        {
        //            Recalcline(0, i);
        //        }
        //    }

        //    CalcOutOfBalance();
        //    Recalc();
        //}

        //private void PaymentMethodLookup_btn_Click(object sender, EventArgs e)
        //{
        //    PaymentMethodLookup PMLup = new PaymentMethodLookup();
        //    if (PMLup.ShowDialog() == DialogResult.OK)
        //    {
        //        string[] lPMLup = PMLup.GetPaymentMethod;
        //        PaymentMethodTxt.Text = lPMLup[1];
        //    }
        //}

        void LoadPaymentMethod()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM PaymentMethods WHERE id = '" + MethodPaymentID + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    // PaymentMethodTxt.Text = dr["PaymentMethod"].ToString();
                    glAccountCode = dr["GLAccountCode"].ToString();

                }
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

        private void TaxCodeLookup_btn_Click(string TaxSearch = "")
        {
            DataGridViewRow dgvRows = dgEnterSales.CurrentRow;
            TaxCodeLookup TaxCodeL = new TaxCodeLookup(TaxSearch);
            if (TaxCodeL.ShowDialog() == DialogResult.OK)
            {
                Tax = TaxCodeL.GetTax;
                dgvRows.Cells["TaxCode"].Value = Tax[0];
                dgvRows.Cells["TaxCollectedAccountID"].Value = Tax[1];
                DataRow rTx = CommonClass.getTaxDetails(dgvRows.Cells["TaxCode"].Value.ToString());
                if (rTx.ItemArray.Length > 0)
                {
                    float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                    string lTaxPaidAccountID = "";
                    lTaxPaidAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                    dgvRows.Cells["TaxCollectedAccountID"].Value = lTaxPaidAccountID;
                    dgvRows.Cells["TaxRate"].Value = ltaxrate;
                }
            }
            else
            {
                dgvRows.Cells["TaxCode"].Value = "";
            }
        }

        private void record_btn_Click(object sender, EventArgs e)
        {
            float TotalEndingBalance = 0;
            if (XsalesID == "" || SrcOfInvoke == CommonClass.InvocationSource.USERECURRING || SrcOfInvoke == CommonClass.InvocationSource.REMINDER)
            {
                if (salestype_cb.Text == "INVOICE")
                {
                    if (rdbCash.Checked)
                    {
                        string tAmount = String.Format("{0:0.##}", TotalAmount.Value);
                        string pAmount = String.Format("{0:0.##}", PaidToday_txt.Value);
                        if (pAmount == tAmount)
                        {
                            SaveSale();
                        }
                        else
                        {
                            MessageBox.Show("Payment is required for Cash Invoices.");
                        }
                    }
                    else
                    {
                        TotalEndingBalance = CustomerBalance + (float)this.BalanceDue_txt.Value;
                        if (TotalEndingBalance <= CustomerCreditLimit) //Check first if the invoice will not exceed the Credit Limit
                        {
                            SaveSale();
                        }
                        else
                        {
                            MessageBox.Show("This invoice will exceed the Customer's Credit Limit.");
                            string selectSupervisor = "SELECT * FROM Users";
                            DataTable dtSupervisors = new DataTable();

                            string titles = "Information";
                            DialogResult OverrideCreditLimit = MessageBox.Show("Would you like to override the Credit Limit?", titles, MessageBoxButtons.YesNo);
                            if (OverrideCreditLimit == DialogResult.Yes)
                            {
                                VoidValidation DlgVoid = new VoidValidation();
                                if (DlgVoid.ShowDialog() == DialogResult.OK)
                                {
                                    password = DlgVoid.GetPassword;
                                    username = DlgVoid.GetUsername;
                                }
                                selectSupervisor += " WHERE user_name = '" + username + "' AND user_pwd = '" + CommonClass.SHA512(password) + "'";
                                CommonClass.runSql(ref dtSupervisors, selectSupervisor);

                                if (dtSupervisors.Rows.Count > 0 && dtSupervisors.Rows[0]["user_role"].ToString() == "Supervisor")
                                {
                                    SaveSale();
                                }
                            }
                        }
                    }
                }
                else
                {
                    SaveSale();
                }
            }
            else
            {
                if (record_btn.Text == "UPDATE")
                {
                    decimal lnetBalance = BalanceDue_txt.Value - OldBalanceDue;
                    TotalEndingBalance = CustomerBalance + (float)lnetBalance;
                    if (salestype_cb.Text == "INVOICE")
                    {
                        if (TotalEndingBalance <= CustomerCreditLimit) //Check first if the invoice will not exceed the Credit Limit
                        {
                            UpdateSale(InvoiceNumTxt.Text);
                        }
                        else
                        {
                            MessageBox.Show("This invoice will exceed the Customer's Credit Limit.");
                        }
                    }
                    else
                    {
                        UpdateSale(InvoiceNumTxt.Text);
                    }
                }
                else //CONVERT SALES
                {
                    if (FromSalesType == "QUOTE" && CommonClass.KeepQuote)
                    {
                        SaveSale();
                    }
                    else
                    {
                        ConvertSale(defSalesNum);
                    }
                }
            }
        }
        private bool checkSale()
        {
            bool canSaveSale = false;
            int i = 0;
            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
            {
                if (dgEnterSales.Rows.Count > 0)
                {
                    if (dgEnterSales.Rows[i].Cells["PartNumber"].Value == null)
                    {
                        canSaveSale = false;
                    }
                    else
                    {
                        canSaveSale = true;
                    }
                }
            }
            i++;
            return canSaveSale;
        }
        public int SaveSale(bool pIsRecurring = false)
        {
            if (customerText.Text != "" && checkSale())
            {
                if (salestype_cb.Text == "INVOICE")
                {
                    DataTable lTbError = CheckOnHandQtyNewSale();
                    if (lTbError.Rows.Count > 0)
                    {
                        ItemErrorInfo ItemError = new ItemErrorInfo(lTbError);
                        ItemError.ShowDialog();
                        return 0;
                    }
                }
                SqlConnection con_ = null;
                try
                {
                    con_ = new SqlConnection(CommonClass.ConStr);
                    int count = 0;
                    int NewTermID = 0;
                    NewTermID = CreateTerm();
                    if (!pIsRecurring)
                        GenerateInvoiceNum();
                    else
                        InvoiceNumTxt.Text = invoicenum = "RECURRING";

                    string salestype = salestype_cb.Text;

                    if (ContactInfoTb.Rows.Count > 0)
                    {
                        string locID = "((SELECT MAX( Location ) FROM Contacts WHERE ProfileID = " + CustomerID + " ) + 1 )";

                        string strContacts = @"INSERT INTO Contacts (
                                            Location,
                                            Street,
                                            City,
                                            State,
                                            Postcode,
                                            Country,
                                            Phone,
                                            Fax,
                                            Email,
                                            Website,
                                            ContactPerson,
                                            ProfileID,
                                            Comments,
                                            TypeOfContact)
                                        VALUES  (" +
                                                        locID +
                                                    ",@Street," +
                                                    "@City," +
                                                    "@State," +
                                                    "@Postcode," +
                                                    "@Country," +
                                                    "@Phone," +
                                                    "@Fax," +
                                                    "@Email," +
                                                    "@Website," +
                                                    "@ContactPerson," +
                                                    "@ProfileID," +
                                                    "@Comments," +
                                                    "@TypeOfContact)";
                        Dictionary<string, object> paramContact = new Dictionary<string, object>();
                        paramContact.Add("@Street", ContactInfoTb.Rows[0]["Street"].ToString());
                        paramContact.Add("@City", ContactInfoTb.Rows[0]["City"].ToString());
                        paramContact.Add("@State", ContactInfoTb.Rows[0]["State"].ToString());
                        paramContact.Add("@Postcode", ContactInfoTb.Rows[0]["Postcode"].ToString());
                        paramContact.Add("@Country", ContactInfoTb.Rows[0]["Country"].ToString());
                        paramContact.Add("@Phone", ContactInfoTb.Rows[0]["Phone"].ToString());
                        paramContact.Add("@Fax", ContactInfoTb.Rows[0]["Fax"].ToString());
                        paramContact.Add("@Email", ContactInfoTb.Rows[0]["Email"].ToString());
                        paramContact.Add("@Website", ContactInfoTb.Rows[0]["Website"].ToString());
                        paramContact.Add("@ContactPerson", ContactInfoTb.Rows[0]["ContactPerson"].ToString());
                        paramContact.Add("@ProfileID", CustomerID);
                        paramContact.Add("@Comments", ContactInfoTb.Rows[0]["Comments"].ToString());
                        paramContact.Add("@TypeOfContact", ContactInfoTb.Rows[0]["TypeOfContact"].ToString());

                        shipAddressID = CommonClass.runSql(strContacts, CommonClass.RunSqlInsertMode.SCALAR, paramContact);
                    }

                    string layout = "";

                    string savesql = @"INSERT INTO Sales ( SalesType,
                                                       CustomerID,
                                                       UserID,
                                                       SalesNumber,
                                                       TransactionDate,
                                                       PromiseDate,
                                                       ShippingMethodID,
                                                       SubTotal,
                                                       FreightSubTotal,
                                                       FreightTax, 
                                                       TaxTotal,
                                                       Memo,
                                                       LayoutType,
                                                       GrandTotal,
                                                       InvoiceStatus,
                                                       SalesReference,
                                                       CustomerPONumber,
                                                       Comments,
                                                       IsTaxInclusive,
                                                       TermsReferenceID,
                                                       ShippingContactID,
                                                       LocationID,
                                                       ClosedDate,
                                                       TotalPaid,
                                                       TotalDue,
                                                       FreightTaxCode,
                                                       FreightTaxRate,
                                                       SalesPersonID,
                                                       SessionID,
                                                       InvoiceType) 
                                              VALUES ( @SalesType,
                                                       @CustomerID,
                                                       @UserID,
                                                       @SalesNum,
                                                       @TransDate,
                                                       @PromiseDate,
                                                       @ShippingMethodID,
                                                       @SubTotal,
                                                       @FreightSubTotal,
                                                       @FreightTax, 
                                                       @TaxTotal,
                                                       @Memo,
                                                       @Layout,
                                                       @GrandTotal,
                                                       @InvoiceStatus,
                                                       @SalesRef,
                                                       @custPo,
                                                       @Comment,
                                                       @isTaxInclusive,
                                                       @TermId,
                                                       @ShippingID,
                                                       @LocationID,
                                                       @ClosedDate,
                                                       @TotalPaid,
                                                       @TotalDue,
                                                       @FreightTaxCode,
                                                       @FreightTaxRate,
                                                       @SalesPersonID,
                                                       @SessionID,
                                                       @InvoiceType); 
                                            SELECT SCOPE_IDENTITY()";
                    cmd = new SqlCommand(savesql, con_);
                    cmd.CommandType = CommandType.Text;
                    //Sales Data
                    cmd.Parameters.AddWithValue("@SalesType", salestype);
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                    cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                    cmd.Parameters.AddWithValue("@SalesNum", InvoiceNumTxt.Text);
                    cmd.Parameters.AddWithValue("@TransDate", salesDate.Value.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@PromiseDate", PromiseDate.Value.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@ShippingID", shipAddressID);
                    cmd.Parameters.AddWithValue("@LocationID", cmb_shippingcontact.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@TermId", NewTermID);

                    layout = "Item";
                    if (salestype_cb.Text == "INVOICE")
                    {
                        if (rdbCash.Checked)
                        {
                            cmd.Parameters.AddWithValue("@InvoiceType", "Cash");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@InvoiceType", "AR");
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceType", "AR");
                    }

                    if (BalanceDue_txt.Value == 0 && salestype_cb.Text == "INVOICE")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Closed");
                        cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now.ToUniversalTime());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                        if (salestype_cb.Text == "INVOICE")
                        {
                            cmd.Parameters.AddWithValue("@InvoiceStatus", "Open");

                        }
                        else if (salestype_cb.Text == "ORDER")
                        {
                            cmd.Parameters.AddWithValue("@InvoiceStatus", "Order");
                        }
                        else if (salestype_cb.Text == "QUOTE")
                        {
                            cmd.Parameters.AddWithValue("@InvoiceStatus", "Quote");
                        }
                        else if (salestype_cb.Text == "LAY-BY")
                        {
                            cmd.Parameters.AddWithValue("@InvoiceStatus", "Lay-by");
                        }
                    }

                    cmd.Parameters.AddWithValue("@Layout", layout);
                    cmd.Parameters.AddWithValue("@ShippingMethodID", ShippingMethodID == null || ShippingMethodID == "" ? "" : ShippingMethodID);
                    cmd.Parameters.AddWithValue("@SubTotal", subtotalAmountText.Value);
                    cmd.Parameters.AddWithValue("@FreightSubTotal", FreightAmountEx);
                    cmd.Parameters.AddWithValue("@FreightTax", FreightTax);
                    cmd.Parameters.AddWithValue("@TaxTotal", TaxAmount.Value);
                    cmd.Parameters.AddWithValue("@TotalPaid", 0);
                    cmd.Parameters.AddWithValue("@TotalDue", BalanceDue_txt.Value);
                    cmd.Parameters.AddWithValue("@Memo", MemoText.Text);
                    cmd.Parameters.AddWithValue("@GrandTotal", TotalAmount.Value);
                    cmd.Parameters.AddWithValue("@SalesRef", SalesRefText.Text);
                    cmd.Parameters.AddWithValue("@custPo", custPOText.Text);
                    cmd.Parameters.AddWithValue("@Comment", CommentsText.Text);
                    cmd.Parameters.AddWithValue("@TaxID", TaxID);
                    cmd.Parameters.AddWithValue("@FreightTaxCode", txtFTaxCode.Text);
                    cmd.Parameters.AddWithValue("@FreightTaxRate", FreightTaxRate);
                    cmd.Parameters.AddWithValue("@SalesPersonID", SalesPersonID);
                    cmd.Parameters.AddWithValue("@SessionID", CommonClass.SessionID);

                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isTaxInclusive", "N");
                    }
                    con_.Open();
                    NewSalesID = Convert.ToInt32(cmd.ExecuteScalar());

                    //SalesLines
                    string Descript;
                    //string EscapedDescription = "";
                    string Amount = "";
                    string Job = "";
                    string Tax = "";
                    string TaxRate = "";
                    string OrderQty = "";
                    string BackOrderQty = "";
                    string ShipQty = "";
                    string UnitPrice = "";
                    string ActualPrice = "";
                    string Discount = "";
                    string CostPrice = "";
                    string TotalCost = "";
                    string promoid = "";
                    int entity = 0;
                    for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                    {
                        if (dgEnterSales.Rows[i].Cells["ItemID"].Value != null)
                        {
                            if (dgEnterSales.Rows[i].Cells["ItemID"].Value.ToString() != "")
                            {
                                Descript = String.Format("{0}", dgEnterSales.Rows[i].Cells["Description"].Value.ToString());
                                Amount = dgEnterSales.Rows[i].Cells["Amount"].Value.ToString();
                                double dAmount = double.Parse(Amount, NumberStyles.Currency);

                                entity = Convert.ToInt32(dgEnterSales.Rows[i].Cells["ItemID"].Value);

                                TaxID = dgEnterSales.Rows[i].Cells["TaxCollectedAccountID"].Value.ToString();
                                Job = dgEnterSales.Rows[i].Cells["JobID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["JobID"].Value.ToString();
                                Tax = dgEnterSales.Rows[i].Cells["TaxCode"].Value.ToString();
                                TaxRate = dgEnterSales.Rows[i].Cells["TaxRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["TaxRate"].Value.ToString();
                                double taxEx = Convert.ToDouble(dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value.ToString());
                                double TaxAm = dAmount - taxEx;

                                //if (salestype_cb.Text == "INVOICE")
                                //{
                                //    //OrderQty = dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() == null ? dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() : "0";
                                //    //BackOrderQty = dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() == null ? dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() : "0";
                                //    OrderQty = "0";
                                //    BackOrderQty = "0";
                                //}
                                //else
                                //{
                                //    OrderQty = "0";
                                //}

                                string salesLinesql = "";

                                ShipQty = dgEnterSales.Rows[i].Cells["Ship"].Value.ToString();
                                UnitPrice = dgEnterSales.Rows[i].Cells["Price"].Value.ToString();
                                if (dgEnterSales.Rows[i].Cells["ActualUnitPrice"].Value == null)
                                {
                                    ActualPrice = "0";
                                }
                                else
                                {
                                    ActualPrice = dgEnterSales.Rows[i].Cells["ActualUnitPrice"].Value.ToString();
                                }
                                Discount = dgEnterSales.Rows[i].Cells["DiscountRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["DiscountRate"].Value.ToString();
                                ShipQ = Convert.ToInt32(ShipQty);
                                CostPrice = dgEnterSales.Rows[i].Cells["CostPrice"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["CostPrice"].Value.ToString();
                                TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();

                                if (salestype_cb.Text == "INVOICE")
                                {
                                    DataTable lItemTb = GetItem(entity.ToString());
                                    if (lItemTb.Rows.Count > 0)
                                    {
                                        if (float.Parse(CostPrice) == 0)
                                        {
                                            //CostPrice = AverageCostEx
                                            CostPrice = lItemTb.Rows[0]["AverageCostEx"].ToString();
                                            TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();
                                        }
                                        bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                                        if (lIsCounted)
                                        {
                                            float lQty = float.Parse(ShipQty);
                                            salesLinesql = "UPDATE ItemsQty SET OnHandQty = OnHandQty - " + lQty.ToString() + " WHERE ItemID = " + entity;
                                            cmd.CommandText = salesLinesql;
                                            count = cmd.ExecuteNonQuery();
                                            // INSERT SOLD ITEMS IN ITEM TRANSACTION
                                            salesLinesql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                                        VALUES(@TransDate," + entity + "," + ShipQty + "," + ShipQty + " *(-1)," + CostPrice + "," + TotalCost + ",'SI'," + NewSalesID + "," + CommonClass.UserID + ")";
                                            cmd.CommandText = salesLinesql;
                                            count = cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                promoid = dgEnterSales.Rows[i].Cells["PromoID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString();
                                promoid = promoid == "" ? "0" : promoid;
                                salesLinesql = "INSERT INTO SalesLines (SalesID, Description, TotalAmount, TransactionDate, TaxCode, UnitPrice, ActualUnitPrice, DiscountPercent, OrderQty, ShipQty, EntityID, JobID, SubTotal, TaxAmount, TaxCollectedAccountID,TaxRate, CostPrice, TotalCost, PromoID)" +
                                                " VALUES (" + NewSalesID + ", '" + Descript + "', '" + dAmount + "', @TransDate, '" + Tax + "', '" + UnitPrice + "', '" + ActualPrice + "', '" + Discount + "', '" + OrderQty + "', '" + ShipQty + "', '" + entity + "', '" + Job + "', '" + taxEx + "', '" + TaxAm + "', '" + TaxID + "','" + TaxRate + "','" + CostPrice + "','" + TotalCost + "'," + promoid + ") ";

                                Dictionary<string, object> param = new Dictionary<string, object>();

                                param.Add("@TransDate", salesDate.Value.ToUniversalTime());

                                count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.SCALAR, param);

                                double lPoints = dgEnterSales.Rows[i].Cells["Points"].Value != null ? Math.Round(float.Parse(dgEnterSales.Rows[i].Cells["Points"].Value.ToString()), 2) : 0;

                                if (lPoints != 0)
                                {
                                    param.Add("@PromoID", dgEnterSales.Rows[i].Cells["PromoID"].Value);
                                    param.Add("@Points", lPoints);
                                    param.Add("@CustomerID", memberID);
                                    param.Add("@SalesLineID", count);
                                    param.Add("@ItemID", entity);

                                    string promosql = "";

                                    if (lPoints < 0)
                                    {
                                        param.Add("@RedemptionType", dgEnterSales.Rows[i].Cells["RedemptionType"].Value);
                                        param.Add("@RedeemID", dgEnterSales.Rows[i].Cells["RedeemID"].Value.ToString());
                                        promosql = @"INSERT INTO AccumulatedPoints (
                                                            PromoID, 
                                                            TransactionDate, 
                                                            PointsAccumulated, 
                                                            CustomerID, 
                                                            SalesLineID,
                                                            ItemID,
                                                            RedemptionType,
                                                            RedeemID ) 
                                                VALUES (
                                                        @PromoID, 
                                                        @TransDate, 
                                                        @Points, 
                                                        @CustomerID, 
                                                        @SalesLineID,
                                                        @ItemID,
                                                        @RedemptionType,@RedeemID)";
                                        if (dgEnterSales.Rows[i].Cells["RedemptionType"].Value.ToString() == "Gift Certificate")
                                        {
                                            string gcsql = @"Update GiftCertificate set issuedSalesID = " + NewSalesID + " where ID = " + dgEnterSales.Rows[i].Cells["RedeemID"].Value.ToString();
                                            CommonClass.runSql(gcsql);

                                        }
                                    }
                                    else
                                    {
                                        promosql = @"INSERT INTO AccumulatedPoints (
                                                            PromoID, 
                                                            TransactionDate, 
                                                            PointsAccumulated, 
                                                            CustomerID, 
                                                            SalesLineID,
                                                            ItemID ) 
                                                    VALUES (
                                                            @PromoID, 
                                                            @TransDate, 
                                                            @Points, 
                                                            @CustomerID, 
                                                            @SalesLineID,
                                                            @ItemID )";
                                    }
                                    CommonClass.runSql(promosql, CommonClass.RunSqlInsertMode.QUERY, param);
                                }
                            }
                        }
                    }

                    UpdateTransSeries(ref cmd);
                    if (SrcOfInvoke == CommonClass.InvocationSource.USERECURRING ||
                        SrcOfInvoke == CommonClass.InvocationSource.REMINDER)
                    {
                        UpdateNotify();
                    }

                    if (salestype_cb.Text == "INVOICE" && !pIsRecurring)
                    {
                        CreateJournalEntries(NewSalesID);
                        if (rdbAR.Checked) // Update profile Balances only if the invoice type is AR
                        {
                            TransactionClass.UpdateProfileBalances(CustomerID, BalanceDue_txt.Value);
                        }

                        if (PaidToday_txt.Value != 0)
                        {
                            RecordPayment(invoicenum, PaidToday_txt.Value, 0);
                        }
                    }
                    if (salestype_cb.Text == "ORDER" || salestype_cb.Text == "LAY-BY")
                    {
                        if (PaidToday_txt.Value != 0)
                        {
                            RecordCustomerDeposit(invoicenum, PaidToday_txt.Value, 0);
                        }
                    }
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Sales  " + this.salestype_cb.Text + " No. " + InvoiceNumTxt.Text);
                    saveValidationLog(NewSalesID);
                    if (SrcOfInvoke == CommonClass.InvocationSource.REMINDER || SrcOfInvoke == CommonClass.InvocationSource.SAVERECURRING)
                    {
                        MessageBox.Show("Sales Record has been created");

                        return NewSalesID;
                    }

                    if (count != 0 && SrcOfInvoke != CommonClass.InvocationSource.SAVERECURRING)
                    {


                        string titles = "Information";
                        if (rdbCash.Checked && this.salestype_cb.Text == "INVOICE")
                        {
                            ChangeAmount dispChange = new ChangeAmount(AmountChange);
                            dispChange.ShowDialog();
                        }

                        DialogResult PrintInvoice = MessageBox.Show("Would you like to print the new  " + this.salestype_cb.Text + " created?", titles, MessageBoxButtons.YesNo);
                        if (PrintInvoice == DialogResult.Yes)
                        {
                            LoadItemLayoutReport(NewSalesID.ToString());
                            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
                            {
                                if (dgvr.Cells["RedemptionType"].Value != null)
                                {
                                    if (dgvr.Cells["RedemptionType"].Value.ToString() == "Gift Certificate")
                                    {
                                        PrintGC();
                                    }
                                }

                            }
                        }

                        DialogResult createNew = MessageBox.Show("Sales Record has been created. Would you like to enter a new " + this.salestype_cb.Text + "?", titles, MessageBoxButtons.YesNo);
                        if (createNew == DialogResult.Yes)
                        {   //clear for new datas
                            dgEnterSales.Rows.Clear();
                            dgEnterSales.Refresh();
                            customerText.Clear();
                            PayeeInfo.Clear();
                            PopulateDataGridView();
                            InvoiceNumTxt.Visible = false;
                            TotalAmount.Value = 0;
                            BalanceDue_txt.Value = 0;
                            PaidToday_txt.Value = 0;
                            PromiseDate.Value = DateTime.Now.ToUniversalTime();
                            salesDate.Value = DateTime.Now.ToUniversalTime();
                            ShippingmethodText.Clear();
                            TermsText.Text = "";
                            PaymentInfoTb.Clear();
                            InitPaymentInfoTb();
                            ContactInfoTb.Clear();
                            InitContactInfoTb();
                            InitVoidTable();
                            FormCheck();
                            ApplyVoidAccess();
                        }
                        else if (createNew == DialogResult.No)
                        {
                            CommonClass.EnterSalesfrm.Close();

                        }
                    }
                    return NewSalesID;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
                finally
                {
                    if (con_ != null)
                        con_.Close();
                }
            }
            else
            {
                MessageBox.Show("Transaction must have a CUSTOMER and ITEM.");
                return 0;
            }
        }

        public void PrintGC()
        {
            DataTable TbRep = new DataTable();
            TbRep.Columns.Add("GCNumber", typeof(string));
            TbRep.Columns.Add("ItemNumber", typeof(string));
            TbRep.Columns.Add("EndDate", typeof(string));
            TbRep.Columns.Add("ProfileName", typeof(string));

            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
            {
                string sql = @"SELECT DISTINCT g.GCNumber, i.ItemNumber, g.EndDate,
					p.Name as ProfileName
                    FROM Sales
                    INNER JOIN Profile p on Sales.CustomerID = p.ID                    
                    INNER JOIN SalesLines sl ON sl.SalesID = Sales.SalesID
                    INNER JOIN Items i ON sl.EntityID = i.ID                   
					INNER JOIN GiftCertificate g ON g.ItemID = i.ID
                    WHERE g.ID = " + dgvr.Cells["RedeemID"].Value.ToString();

                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, sql);
                DataRow dr = dt.Rows[0];
                for (int x = 0; x < int.Parse(dgvr.Cells["Ship"].Value.ToString()); x++)
                {
                    DataRow rw = TbRep.NewRow();
                    rw["GCNumber"] = dr[0];
                    rw["ItemNumber"] = dr[1].ToString();
                    rw["EndDate"] = dr[2];
                    rw["ProfileName"] = dr[3].ToString();
                    TbRep.Rows.Add(rw);
                }
            }
            Reports.ReportParams GCReceipt = new Reports.ReportParams();
            GCReceipt.PrtOpt = 1;
            GCReceipt.Rec.Add(TbRep);
            GCReceipt.ReportName = "GCReceipt.rpt";
            GCReceipt.RptTitle = "Gift Certificate";
            GCReceipt.Params = "compname|CompAddress|TIN";
            GCReceipt.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim();

            CommonClass.ShowReport(GCReceipt);
        }
        public static DataTable GetItem(string pItemID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = "SELECT i.*, q.OnHandQty,q.CommitedQty, c.AverageCostEx from ( Items i inner join ItemsQty q on i.ID = q.ItemID ) inner join ItemsCostPrice c on i.ID = c.ItemID where i.ID = " + pItemID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        void CheckItemQty(int ItemID)
        {
            int NewOnHand = ItemOnHand - ShipQ;
            if (NewOnHand < 0)
            {
                MessageBox.Show("Need to purchase Item" + NewOnHand * -1, "Information");
            }
            else
            {
                SqlConnection con = null;
                try
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    string termsql = @"UPDATE ItemsQty SET OnHandQty = @NewOnHandQty, CommitedQty = @NewCommitedQty WHERE ItemID = " + ItemID;
                    SqlCommand cmd = new SqlCommand(termsql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@NewOnHandQty", NewOnHand);
                    cmd.Parameters.AddWithValue("@NewCommitedQty", NewOnHand);
                    con.Open();
                    cmd.ExecuteNonQuery();
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
        }

        void UpdateSale(string pOldSalesNo)
        {

            //TransactionClass.ReverseAccountBalances(pOldSalesNo);
            if (salestype_cb.Text == "INVOICE")
            {
                DataTable lTbError = CheckOnHandQtyUpdateSale(XsalesID);
                if (lTbError.Rows.Count > 0)
                {
                    ItemErrorInfo ItemError = new ItemErrorInfo(lTbError);
                    ItemError.ShowDialog();
                    return;
                }
                ReverseItemQty(XsalesID);
            }

            //DELETE Sales LINES
            if (DeleteSalesLines(XsalesID) > 0)
            {
                //EDIT Sales Records
                UpdateInvoiceTerm(TermRefID);
                if (EditSalesRecord(XsalesID, pOldSalesNo))
                {
                    if (salestype_cb.Text == "INVOICE")
                    {
                        // DELETE JOURNAL ENTRIES OF OLD
                        if (TransactionClass.DeleteJournalEntries(pOldSalesNo) > 0)
                        {
                            if (CreateJournalEntries(Convert.ToInt32(XsalesID)))
                            {
                                decimal lnetBalance = BalanceDue_txt.Value - OldBalanceDue;
                                TransactionClass.UpdateProfileBalances(CustomerID, lnetBalance);
                            }
                        }
                        if (PaidToday_txt.Value != 0 && PrevPaid == 0)
                        {
                            RecordPayment(invoicenum, PaidToday_txt.Value, 0);
                        }
                    }
                    if (salestype_cb.Text == "ORDER")
                    {
                        if (PaidToday_txt.Value != 0 && PrevPaid == 0)
                        {
                            RecordCustomerDeposit(invoicenum, PaidToday_txt.Value, 0);
                        }
                    }

                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Edited SalesLine Transaction Number " + pOldSalesNo, XsalesID);
                    DialogResult PrintInvoice = MessageBox.Show("Would you like to print the edited " + this.salestype_cb.Text + "?", "Information"
                           , MessageBoxButtons.YesNo);
                    if (PrintInvoice == DialogResult.Yes)
                    {
                        LoadItemLayoutReport(XsalesID);
                    }
                    MessageBox.Show("Edited Sales Transaction successfully.", "Sales Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        void ConvertSale(string pOldSalesNo)
        {
            if (salestype_cb.Text == "INVOICE")
            {
                DataTable lTbError = CheckOnHandQtyNewSale();
                if (lTbError.Rows.Count > 0)
                {
                    ItemErrorInfo ItemError = new ItemErrorInfo(lTbError);
                    ItemError.ShowDialog();
                    return;
                }
            }
            if (rdbCash.Checked)
            {
                if (PaidToday_txt.Value != TotalAmount.Value)
                {
                    MessageBox.Show("Payment is required for Cash Invoices.");
                    return;
                }

            }
            //DELETE Sales LINES
            if (DeleteSalesLines(XsalesID) > 0)
            {
                GenerateInvoiceNum();
                //EDIT Sales Records
                UpdateInvoiceTerm(TermRefID);
                if (EditSalesRecord(XsalesID, invoicenum))
                {
                    if (salestype_cb.Text == "INVOICE")
                    {
                        if (CreateJournalEntries(Convert.ToInt32(XsalesID)))
                        {
                            decimal lnetBalance = BalanceDue_txt.Value - OldBalanceDue;
                            if (rdbAR.Checked)
                            {
                                TransactionClass.UpdateProfileBalances(CustomerID, lnetBalance);
                            }
                        }
                        if (PaidToday_txt.Value != 0 && PrevPaid == 0)
                        {
                            RecordPayment(invoicenum, PaidToday_txt.Value, 0);
                        }
                        if (PaidToday_txt.Value == PrevPaid && PrevPaid != 0)
                        {
                            TransferCustomerDeposit(XsalesID, PaidToday_txt.Value, 0);
                        }
                    }
                    if (salestype_cb.Text == "ORDER")
                    {
                        if (PaidToday_txt.Value != 0 && PrevPaid == 0)
                        {
                            RecordCustomerDeposit(invoicenum, PaidToday_txt.Value, 0);
                        }
                    }

                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, FromSalesType + " " + pOldSalesNo + " Converted to " + ToSalesType + " with Sales Number " + invoicenum, XsalesID);
                    DialogResult PrintInvoice = MessageBox.Show("Would you like to print the new  " + this.salestype_cb.Text + " created?", "Information", MessageBoxButtons.YesNo);
                    if (PrintInvoice == DialogResult.Yes)
                    {
                        LoadItemLayoutReport(XsalesID);
                    }
                    MessageBox.Show("Converted Sales Transaction successfully.", "Sales Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private int DeleteSalesLines(string salesNoID)
        {
            //SqlConnection con = null;
            try
            {
                string sql = "";
                //Delete Accumulated Points
                for (int i = 0; i < TbRepSalesLines.Rows.Count; i++)
                {
                    sql = "DELETE From AccumulatedPoints where SalesLineID = " + TbRepSalesLines.Rows[i]["SalesLineID"].ToString();
                    CommonClass.runSql(sql);
                }
                //con = new SqlConnection(CommonClass.ConStr);

                sql = "DELETE FROM SalesLines WHERE SalesID = " + salesNoID;
                //SqlCommand cmd = new SqlCommand(sql, con);

                //cmd.CommandType = CommandType.Text;
                //con.Open();
                //int rec = cmd.ExecuteNonQuery();
                int rec = CommonClass.runSql(sql);

                return rec;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        public void ReverseItemQty(string pSalesID)
        {
            SqlConnection con = null;
            try
            {
                string sql = @"SELECT l.*, s.SalesNumber, s.GrandTotal, s.Memo, s.FreightSubTotal, s.FreightTax, s.FreightTaxCode, s.FreightTaxRate, i.AssetAccountID, 
                    i.IsCounted, i.IncomeAccountID, i.IsSold, i.COSAccountID, i.IsBought FROM ( SalesLines l INNER JOIN Sales s ON l.SalesID = s.SalesID ) left join Items as i on l.EntityID =i.ID WHERE l.SalesID = " + pSalesID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable lItemTb = new DataTable();
                da.Fill(lItemTb);
                int count = 0;
                for (int i = 0; i < lItemTb.Rows.Count; i++)
                {
                    bool lIsCounted = (bool)lItemTb.Rows[i]["IsCounted"];
                    if (lIsCounted)
                    {

                        float lQty = float.Parse(lItemTb.Rows[i]["ShipQty"].ToString()) * -1;
                        float CostPrice = (lItemTb.Rows[i]["CostPrice"] == null || lItemTb.Rows[i]["CostPrice"].ToString() == "" ? 0 : float.Parse(lItemTb.Rows[i]["CostPrice"].ToString()));
                        float TotalCost = lQty * CostPrice;
                        string entity = lItemTb.Rows[i]["EntityID"].ToString();
                        string lTranDate = salesDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");

                        sql = "UPDATE ItemsQty set OnHandQty = OnHandQty - " + lQty.ToString() + " where ItemID = " + entity;
                        cmd.CommandText = sql;
                        count = cmd.ExecuteNonQuery();
                        //DELETE ENTRY IN ITEM TRANSACTION
                        sql = "DELETE FROM ItemTransaction where TranType = 'SI' and SourceTranID = " + pSalesID;
                        cmd.CommandText = sql;
                        count = cmd.ExecuteNonQuery();
                        cmd.CommandText = sql;
                        count = cmd.ExecuteNonQuery();
                    }
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
        } //END

        public void UpdateItemQty(string pSalesID)
        {
            SqlConnection con = null;
            try
            {
                string sql = @"SELECT l.*, s.SalesNumber, s.GrandTotal, s.Memo, s.FreightSubTotal, s.FreightTax, s.FreightTaxCode, s.FreightTaxRate, i.AssetAccountID, 
                    i.IsCounted, i.IncomeAccountID, i.IsSold, i.COSAccountID, i.IsBought FROM ( SalesLines l INNER JOIN Sales s ON l.SalesID = s.SalesID ) left join Items as i on l.EntityID =i.ID WHERE l.SalesID = " + pSalesID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable lItemTb = new DataTable();
                da.Fill(lItemTb);
                int count = 0;
                for (int i = 0; i < lItemTb.Rows.Count; i++)
                {
                    bool lIsCounted = (bool)lItemTb.Rows[i]["IsCounted"];
                    if (lIsCounted)
                    {
                        float lQty = float.Parse(lItemTb.Rows[i]["ShipQty"].ToString());
                        float CostPrice = float.Parse(lItemTb.Rows[i]["CostPrice"].ToString());
                        float TotalCost = lQty * CostPrice;
                        string entity = lItemTb.Rows[i]["EntityID"].ToString();
                        string lTranDate = salesDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");

                        sql = "UPDATE ItemsQty set OnHandQty = OnHandQty - " + lQty.ToString() + " where ItemID = " + entity;
                        cmd.CommandText = sql;
                        count = cmd.ExecuteNonQuery();
                        // INSERT SOLD ITEMS IN ITEM TRANSACTION
                        sql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                            VALUES( '" + lTranDate + "'," + entity + "," + lQty + "," + lQty + " *(-1)," + CostPrice + "," + TotalCost + ",'SI'," + pSalesID + "," + CommonClass.UserID + ")";

                        cmd.CommandText = sql;
                        count = cmd.ExecuteNonQuery();
                    }
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
        } //END

        private bool EditSalesRecordOLD(string pSalesID, string pSalesNo)
        {
            SqlConnection con_ = null;
            try
            {
                LoadShippingID(ShippingmethodText.Text);
                con_ = new SqlConnection(CommonClass.ConStr);
                DateTime EntryDate = DateTime.Now;
                string salestype = salestype_cb.Text;
                string layout = "";

                string updatesql = @"UPDATE Sales SET SalesType = @SalesType, CustomerID = @CustomerID, UserID = @UserID, SalesNumber = @SalesNum,TransactionDate = @TransDate, PromiseDate = @PromiseDate,
                            ShippingMethodID = @ShippingMethodID,  Memo = @Memo,InvoiceStatus = @InvoiceStatus, ClosedDate = @ClosedDate, SalesReference = @SalesRef,CustomerPONumber = @CustomerPONumber,
                            LayoutType = @LayoutType, Comments = @Comments ,IsTaxInclusive = @IsTaxInclusive, TermsReferenceID = @TermsReferenceID, ShippingContactID = @ShippingContactID,
                            SubTotal = @SubTotal, FreightSubTotal = @FreightSubTotal, FreightTax = @FreightTax, TaxTotal = @TaxTotal,GrandTotal = @GrandTotal,                               
                            TotalPaid = @TotalPaid, TotalDue = @TotalDue, FreightTaxCode = @FreightTaxCode, FreightTaxRate = @FreightTaxRate, SalesPersonID = @SalesPersonID WHERE SalesID = " + pSalesID;

                cmd = new SqlCommand(updatesql, con_);
                cmd.CommandType = CommandType.Text;

                //Sales Data
                cmd.Parameters.AddWithValue("@SalesType", salestype);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@SalesNum", pSalesNo);
                cmd.Parameters.AddWithValue("@TransDate", salesDate.Value.ToUniversalTime());
                cmd.Parameters.AddWithValue("@PromiseDate", PromiseDate.Value.ToUniversalTime());
                cmd.Parameters.AddWithValue("@ShippingMethodID", ShippingMethodID == null || ShippingMethodID == "" ? "" : ShippingMethodID);
                cmd.Parameters.AddWithValue("@Memo", MemoText.Text);
                if (BalanceDue_txt.Value == 0)
                {
                    cmd.Parameters.AddWithValue("@InvoiceStatus", "Closed");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now.ToUniversalTime());
                }
                else
                {
                    if (salestype_cb.Text == "INVOICE")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Open");
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                    }
                    else if (salestype_cb.Text == "ORDER")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Order");
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                    }
                    else if (salestype_cb.Text == "QUOTE")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Quote");
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                    }
                }
                cmd.Parameters.AddWithValue("@SalesRef", SalesRefText.Text.ToString());
                cmd.Parameters.AddWithValue("@CustomerPONumber", custPOText.Text.ToString());

                layout = "Item";

                cmd.Parameters.AddWithValue("@LayoutType", layout);
                cmd.Parameters.AddWithValue("@Comments", CommentsText.Text.ToString());
                if (CommonClass.IsTaxcInclusiveEnterSales)
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "N");
                }

                cmd.Parameters.AddWithValue("@TermsReferenceID", TermRefID);
                cmd.Parameters.AddWithValue("@ShippingContactID", shipAddressID);
                cmd.Parameters.AddWithValue("@SubTotal", subtotalAmountText.Value);
                cmd.Parameters.AddWithValue("@FreightSubTotal", FreightAmountEx);
                cmd.Parameters.AddWithValue("@FreightTax", FreightTax);
                cmd.Parameters.AddWithValue("@TaxTotal", TaxAmount.Value.ToString());
                cmd.Parameters.AddWithValue("@GrandTotal", TotalAmount.Value.ToString());
                cmd.Parameters.AddWithValue("@TotalPaid", PaidToday_txt.Value);
                cmd.Parameters.AddWithValue("@TotalDue", BalanceDue_txt.Value);
                cmd.Parameters.AddWithValue("@FreightTaxCode", txtFTaxCode.Text);
                cmd.Parameters.AddWithValue("@FreightTaxRate", FreightTaxRate);
                cmd.Parameters.AddWithValue("@SalesPersonID", SalesPersonID);

                con_.Open();
                int count = cmd.ExecuteNonQuery();

                //SalesLines
                string Descript;
                string Amount = "";
                string Job = "";
                string Tax = "";
                string TaxRate = "";
                string OrderQty = "";
                string BackOrderQty = "";
                string ShipQty = "";
                string UnitPrice = "";
                string ActualPrice = "";
                string Discount = "";
                int entity = 0;
                string CostPrice = "";
                string TotalCost = "";
                for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                {
                    if (this.dgEnterSales.Rows[i].Cells["Description"].Value != null)
                    {
                        if (this.dgEnterSales.Rows[i].Cells["Description"].Value.ToString() != "")
                        {
                            Descript = String.Format("{0}", dgEnterSales.Rows[i].Cells["Description"].Value.ToString());
                            Amount = dgEnterSales.Rows[i].Cells["Amount"].Value.ToString();
                            double dAmount = double.Parse(Amount, NumberStyles.Currency);

                            entity = Convert.ToInt32(dgEnterSales.Rows[i].Cells["ItemID"].Value);

                            TaxID = dgEnterSales.Rows[i].Cells["TaxCollectedAccountID"].Value.ToString();
                            Job = dgEnterSales.Rows[i].Cells["JobID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["JobID"].Value.ToString();
                            Tax = dgEnterSales.Rows[i].Cells["TaxCode"].Value.ToString();
                            TaxRate = dgEnterSales.Rows[i].Cells["TaxRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["TaxRate"].Value.ToString();
                            double taxEx = Convert.ToDouble(dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value.ToString());
                            double TaxAm = dAmount - taxEx;
                            if (customerText.Text == "INVOICE")
                            {
                                OrderQty = dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() == null ? dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() : "0";
                                BackOrderQty = dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() == null ? dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() : "0";
                            }
                            else
                            {
                                OrderQty = "0";
                            }

                            string salesLinesql = "";

                            ShipQty = dgEnterSales.Rows[i].Cells["Ship"].Value.ToString();
                            UnitPrice = dgEnterSales.Rows[i].Cells["Price"].Value.ToString();
                            ActualPrice = dgEnterSales.Rows[i].Cells["ActualUnitPrice"].Value.ToString();
                            Discount = dgEnterSales.Rows[i].Cells["DiscountRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["DiscountRate"].Value.ToString();
                            ShipQ = Convert.ToInt32(ShipQty);
                            CostPrice = dgEnterSales.Rows[i].Cells["CostPrice"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["CostPrice"].Value.ToString();
                            TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();

                            if (salestype_cb.Text == "INVOICE")
                            {
                                //UPDATE QTY
                            }
                            salesLinesql = "INSERT INTO SalesLines (SalesID, Description, TotalAmount, TransactionDate, TaxCode, UnitPrice, ActualUnitPrice, DiscountPercent, OrderQty, ShipQty, EntityID, JobID, SubTotal, TaxAmount, TaxCollectedAccountID,TaxRate, CostPrice, TotalCost)" +
                                            " VALUES (" + XsalesID + ", '" + Descript + "', '" + dAmount + "', @TransDate, '" + Tax + "', '" + UnitPrice + "', '" + ActualPrice + "', '" + Discount + "', '" + OrderQty + "', '" + ShipQty + "', '" + entity + "', '" + Job + "', " + taxEx + ", " + TaxAm + ", '" + TaxID + "'," + TaxRate + "," + CostPrice + "," + TotalCost + ")";

                            cmd.CommandText = salesLinesql;
                            count = cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (defSalesNum != pSalesNo)
                {
                    UpdateTransSeries(ref cmd);
                }

                if (count != 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Update of Sales  " + InvoiceNumTxt.Text);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }
        private bool EditSalesRecord(string pSalesID, string pSalesNo)
        {
            SqlConnection con_ = null;
            try
            {
                LoadShippingID(ShippingmethodText.Text);
                con_ = new SqlConnection(CommonClass.ConStr);
                DateTime EntryDate = DateTime.Now;
                string salestype = salestype_cb.Text;
                string layout = "";

                string updatesql = @"UPDATE Sales SET SalesType = @SalesType, CustomerID = @CustomerID, UserID = @UserID, SalesNumber = @SalesNum,TransactionDate = @TransDate, PromiseDate = @PromiseDate,
                            ShippingMethodID = @ShippingMethodID,  Memo = @Memo,InvoiceStatus = @InvoiceStatus, ClosedDate = @ClosedDate, SalesReference = @SalesRef,CustomerPONumber = @CustomerPONumber,
                            LayoutType = @LayoutType, Comments = @Comments ,IsTaxInclusive = @IsTaxInclusive, TermsReferenceID = @TermsReferenceID, ShippingContactID = @ShippingContactID,
                            SubTotal = @SubTotal, FreightSubTotal = @FreightSubTotal, FreightTax = @FreightTax, TaxTotal = @TaxTotal,GrandTotal = @GrandTotal,                               
                            TotalPaid = @TotalPaid, TotalDue = @TotalDue, FreightTaxCode = @FreightTaxCode, FreightTaxRate = @FreightTaxRate, SalesPersonID = @SalesPersonID, InvoiceType = @InvoiceType WHERE SalesID = " + pSalesID;

                cmd = new SqlCommand(updatesql, con_);
                cmd.CommandType = CommandType.Text;

                //Sales Data
                cmd.Parameters.AddWithValue("@SalesType", salestype);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@SalesNum", pSalesNo);
                cmd.Parameters.AddWithValue("@TransDate", salesDate.Value.ToUniversalTime());
                cmd.Parameters.AddWithValue("@PromiseDate", PromiseDate.Value.ToUniversalTime());
                cmd.Parameters.AddWithValue("@ShippingMethodID", ShippingMethodID == null || ShippingMethodID == "" ? "" : ShippingMethodID);
                cmd.Parameters.AddWithValue("@Memo", MemoText.Text);

                if (salestype_cb.Text == "INVOICE")
                {
                    if (rdbCash.Checked)
                    {
                        cmd.Parameters.AddWithValue("@InvoiceType", "Cash");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceType", "AR");
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InvoiceType", "AR");
                }

                if (BalanceDue_txt.Value == 0 && salestype_cb.Text == "INVOICE")
                {
                    cmd.Parameters.AddWithValue("@InvoiceStatus", "Closed");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now.ToUniversalTime());
                }
                else
                {
                    if (salestype_cb.Text == "INVOICE")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Open");
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                    }
                    else if (salestype_cb.Text == "ORDER")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Order");
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                    }
                    else if (salestype_cb.Text == "LAY-BY")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Lay-By");
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                    }
                    else if (salestype_cb.Text == "QUOTE")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceStatus", "Quote");
                        cmd.Parameters.AddWithValue("@ClosedDate", System.DBNull.Value);
                    }
                }
                cmd.Parameters.AddWithValue("@SalesRef", SalesRefText.Text.ToString());
                cmd.Parameters.AddWithValue("@CustomerPONumber", custPOText.Text.ToString());
                layout = "Item";
                cmd.Parameters.AddWithValue("@LayoutType", layout);
                cmd.Parameters.AddWithValue("@Comments", CommentsText.Text.ToString());
                if (CommonClass.IsTaxcInclusiveEnterSales)
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "N");
                }

                cmd.Parameters.AddWithValue("@TermsReferenceID", TermRefID);
                cmd.Parameters.AddWithValue("@ShippingContactID", shipAddressID);
                cmd.Parameters.AddWithValue("@SubTotal", subtotalAmountText.Value);
                cmd.Parameters.AddWithValue("@FreightSubTotal", FreightAmountEx);
                cmd.Parameters.AddWithValue("@FreightTax", FreightTax);
                cmd.Parameters.AddWithValue("@TaxTotal", TaxAmount.Value.ToString());
                cmd.Parameters.AddWithValue("@GrandTotal", TotalAmount.Value.ToString());
                cmd.Parameters.AddWithValue("@TotalPaid", PaidToday_txt.Value);
                cmd.Parameters.AddWithValue("@TotalDue", BalanceDue_txt.Value);
                cmd.Parameters.AddWithValue("@FreightTaxCode", txtFTaxCode.Text);
                cmd.Parameters.AddWithValue("@FreightTaxRate", FreightTaxRate);
                cmd.Parameters.AddWithValue("@SalesPersonID", SalesPersonID);

                con_.Open();
                int count = cmd.ExecuteNonQuery();

                //SalesLines
                string Descript;
                string Amount = "";
                string Job = "";
                string Tax = "";
                string TaxRate = "";
                string OrderQty = "";
                string BackOrderQty = "";
                string ShipQty = "";
                string UnitPrice = "";
                string ActualPrice = "";
                string Discount = "";
                int entity = 0;
                string CostPrice = "";
                string TotalCost = "";
                string promoid = "";
                for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                {
                    if (this.dgEnterSales.Rows[i].Cells["ItemID"].Value != null)
                    {
                        if (this.dgEnterSales.Rows[i].Cells["ItemID"].Value.ToString() != "")
                        {
                            Descript = String.Format("{0}", dgEnterSales.Rows[i].Cells["Description"].Value.ToString());
                            Amount = dgEnterSales.Rows[i].Cells["Amount"].Value.ToString();
                            double dAmount = double.Parse(Amount, NumberStyles.Currency);
                            entity = Convert.ToInt32(dgEnterSales.Rows[i].Cells["ItemID"].Value);

                            TaxID = dgEnterSales.Rows[i].Cells["TaxCollectedAccountID"].Value.ToString();
                            Job = dgEnterSales.Rows[i].Cells["JobID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["JobID"].Value.ToString();
                            Tax = dgEnterSales.Rows[i].Cells["TaxCode"].Value.ToString();
                            TaxRate = dgEnterSales.Rows[i].Cells["TaxRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["TaxRate"].Value.ToString();
                            double taxEx = Convert.ToDouble(dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value.ToString());
                            double TaxAm = dAmount - taxEx;
                            //if (customerText.Text == "INVOICE")
                            //{
                            //    OrderQty = dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() == null ? dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() : "0";
                            //    BackOrderQty = dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() == null ? dgEnterSales.Rows[i].Cells["Backorder"].Value.ToString() : "0";
                            //}
                            //else
                            //{
                            //    OrderQty = "0";
                            //}

                            string salesLinesql = "";
                            ShipQty = dgEnterSales.Rows[i].Cells["Ship"].Value.ToString();
                            UnitPrice = dgEnterSales.Rows[i].Cells["Price"].Value.ToString();
                            ActualPrice = dgEnterSales.Rows[i].Cells["ActualUnitPrice"].Value.ToString();
                            Discount = dgEnterSales.Rows[i].Cells["DiscountRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["DiscountRate"].Value.ToString();
                            ShipQ = Convert.ToInt32(ShipQty);
                            CostPrice = dgEnterSales.Rows[i].Cells["CostPrice"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["CostPrice"].Value.ToString();
                            TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();

                            if (salestype_cb.Text == "INVOICE")
                            {
                                DataTable lItemTb = GetItem(entity.ToString());
                                if (lItemTb.Rows.Count > 0)
                                {
                                    if (float.Parse(CostPrice) == 0)
                                    {
                                        //CostPrice = AverageCostEx
                                        CostPrice = lItemTb.Rows[0]["AverageCostEx"].ToString();
                                        TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();
                                    }
                                    bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                                    if (lIsCounted)
                                    {
                                        float lQty = float.Parse(ShipQty);
                                        salesLinesql = "UPDATE ItemsQty set OnHandQty = OnHandQty - " + lQty.ToString() + " where ItemID = " + entity;
                                        cmd.CommandText = salesLinesql;
                                        count = cmd.ExecuteNonQuery();
                                        // INSERT SOLD ITEMS IN ITEM TRANSACTION
                                        salesLinesql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                            VALUES(@TransDate," + entity + "," + ShipQty + "," + ShipQty + " *(-1)," + CostPrice + "," + TotalCost + ",'SI'," + pSalesID + "," + CommonClass.UserID + ")";
                                        cmd.CommandText = salesLinesql;
                                        count = cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            promoid = dgEnterSales.Rows[i].Cells["PromoID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString();
                            promoid = promoid == "" ? "0" : promoid;

                            salesLinesql = "INSERT INTO SalesLines (SalesID, Description, TotalAmount, TransactionDate, TaxCode, UnitPrice, ActualUnitPrice, DiscountPercent, OrderQty, ShipQty, EntityID, JobID, SubTotal, TaxAmount, TaxCollectedAccountID,TaxRate, CostPrice, TotalCost, PromoID)" +
                                           " VALUES (" + XsalesID + ", '" + Descript + "', '" + dAmount + "', @TransDate, '" + Tax + "', '" + UnitPrice + "', '" + ActualPrice + "', '" + Discount + "', '" + OrderQty + "', '" + ShipQty + "', '" + entity + "', '" + Job + "', " + taxEx + ", " + TaxAm + ", '" + TaxID + "'," + TaxRate + "," + CostPrice + "," + TotalCost + "," + promoid + ")";


                            Dictionary<string, object> param = new Dictionary<string, object>();

                            param.Add("@TransDate", salesDate.Value.ToUniversalTime());

                            count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.SCALAR, param);
                            double lPoints = dgEnterSales.Rows[i].Cells["Points"].Value != null ? Math.Round(float.Parse(dgEnterSales.Rows[i].Cells["Points"].Value.ToString()), 2) : 0;

                            if (lPoints != 0)
                            {
                                param.Add("@PromoID", dgEnterSales.Rows[i].Cells["PromoID"].Value);
                                param.Add("@Points", lPoints);
                                param.Add("@CustomerID", memberID);
                                param.Add("@SalesLineID", count);
                                param.Add("@ItemID", entity);

                                string promosql = "";

                                if (lPoints < 0)
                                {
                                    param.Add("@RedemptionType", dgEnterSales.Rows[i].Cells["RedemptionType"].Value);
                                    promosql = @"INSERT INTO AccumulatedPoints (
                                                            PromoID, 
                                                            TransactionDate, 
                                                            PointsAccumulated, 
                                                            CustomerID, 
                                                            SalesLineID,
                                                            ItemID,
                                                            RedemptionType ) 
                                                VALUES (
                                                        @PromoID, 
                                                        @TransDate, 
                                                        @Points, 
                                                        @CustomerID, 
                                                        @SalesLineID,
                                                        @ItemID,
                                                        @RedemptionType )";
                                }
                                else
                                {
                                    promosql = @"INSERT INTO AccumulatedPoints (
                                                            PromoID, 
                                                            TransactionDate, 
                                                            PointsAccumulated, 
                                                            CustomerID, 
                                                            SalesLineID,
                                                            ItemID ) 
                                                    VALUES (
                                                            @PromoID, 
                                                            @TransDate, 
                                                            @Points, 
                                                            @CustomerID, 
                                                            @SalesLineID,
                                                            @ItemID )";
                                }

                                CommonClass.runSql(promosql, CommonClass.RunSqlInsertMode.QUERY, param);
                            }


                        }
                    }
                }
                if (defSalesNum != pSalesNo)
                {
                    UpdateTransSeries(ref cmd);
                }

                if (count != 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Update of Sales  " + InvoiceNumTxt.Text);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }
        void UpdateTransSeries(ref SqlCommand pCmd)
        {
            string sql = "";
            if (salestype_cb.Text == "ORDER")
            {
                sql = "UPDATE TransactionSeries SET SalesOrderSeries = '" + CurSeries + "'";
            }
            else if (salestype_cb.Text == "QUOTE")
            {
                sql = "UPDATE TransactionSeries SET SalesQuoteSeries = '" + CurSeries + "'";
            }
            else if (salestype_cb.Text == "INVOICE")
            {
                sql = "UPDATE TransactionSeries SET SalesInvoiceSeries = '" + CurSeries + "'";
            }
            else if (salestype_cb.Text == "LAY-BY")
            {
                sql = "UPDATE TransactionSeries SET SaleLayBySeries = '" + CurSeries + "'";
            }

            cmd.CommandText = sql;
            int res2 = cmd.ExecuteNonQuery();
        }

        //private void label2_Click(object sender, EventArgs e)
        //{

        //}

        public void ShowJobLookup(string jobSearch = "") //Jobs
        {
            SelectJobs DlgJob = new SelectJobs("D", jobSearch);
            DataGridViewRow dgvRows = dgEnterSales.CurrentRow;
            if (DlgJob.ShowDialog() == DialogResult.OK)
            {
                Jobs = DlgJob.GetJob;
                dgvRows.Cells["JobID"].Value = Jobs[0];
                dgvRows.Cells["Job"].Value = Jobs[2];
            }
            else
            {
                dgvRows.Cells["Job"].Value = "";
            }
        }

        private void item_CheckedChanged(object sender, EventArgs e)
        {
            dgEnterSales.Rows.Clear();
            dgEnterSales.Columns["Ship"].Visible = true;
            dgEnterSales.Columns["PartNumber"].Visible = true;
            dgEnterSales.Columns["Price"].Visible = true;
            dgEnterSales.Columns["Discount"].Visible = true;
            dgEnterSales.Columns["AccountNumber"].Visible = false;
            //if (salestype_cb.Text == "INVOICE") Hide Backorder No use for now
            //{
            //    dgEnterSales.Columns["Backorder"].Visible = true;
            //}

            PopulateDataGridView();
        }

        private void dgEnterSales_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colindex = (int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex);
            e.Control.KeyPress -= Numeric_KeyPress;

            if (colindex == 2 || colindex == 5 || colindex == 6 || colindex == 8)
            {

                e.Control.KeyPress += TextboxNumeric_KeyPress;
            }
            else
            {
                e.Control.KeyPress -= TextboxNumeric_KeyPress;
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
            }
        }

        private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
              && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // only allow one negative char before the number
            if (e.KeyChar == '-'
                && (sender as TextBox).Text.IndexOf('-') == 0)
            {
                e.Handled = true;
            }
        }

        void dgEnable()
        {
            if (customerText.Text == "")
            {
                dgEnterSales.Columns["Ship"].ReadOnly = true;
                dgEnterSales.Columns["PartNumber"].ReadOnly = true;
                dgEnterSales.Columns["Price"].ReadOnly = true;
                dgEnterSales.Columns["Discount"].ReadOnly = true;
                dgEnterSales.Columns["Description"].ReadOnly = true;
                dgEnterSales.Columns["AccountNumber"].ReadOnly = true;
                dgEnterSales.Columns["Amount"].ReadOnly = true;
                dgEnterSales.Columns["Job"].ReadOnly = true;
                dgEnterSales.Columns["TaxCode"].ReadOnly = true;
            }
            else
            {
                dgEnterSales.Columns["Ship"].ReadOnly = false;
                dgEnterSales.Columns["PartNumber"].ReadOnly = false;
                dgEnterSales.Columns["Price"].ReadOnly = false;
                dgEnterSales.Columns["Discount"].ReadOnly = false;
                dgEnterSales.Columns["Description"].ReadOnly = false;
                dgEnterSales.Columns["AccountNumber"].ReadOnly = false;
                dgEnterSales.Columns["Job"].ReadOnly = false;
                dgEnterSales.Columns["TaxCode"].ReadOnly = false;
                dgEnterSales.Columns["Amount"].ReadOnly = true;

                if (salestype_cb.Text != "QUOTE")
                {
                    lblBalanceDue.Visible = true;
                    lblPaidToday.Visible = true;

                    PaidToday_txt.Visible = true;
                    //PaymentMethodTxt.Visible = true;
                    BalanceDueAmountTxt.Visible = true;
                    btnPaymentDetails.Visible = true;
                    if (salestype_cb.Text == "INVOICE")
                    {
                        if (rdbAR.Checked)
                        {
                            btnPaymentDetails.Visible = false;
                        }
                    }
                }
                if (PaidToday_txt.Value != 0)
                {
                    PaidToday_txt.Enabled = false;
                    lblPaidToday.Text = "Paid To Date:";
                    btnPaymentDetails.Visible = false;

                    //PaymentMethodTxt.Visible = false;
                    btnViewPayments.Visible = true;
                }
            }
        }

        private void customerText_TextChanged(object sender, EventArgs e)
        {
            dgEnable();
        }

        void itemcalc(int RoWindex)
        {
            double shipVal = 0;
            double priceVal = 0;
            double disc = 0;
            double discValue = 0;
            double woDisc = 0;
            double amtVal = 0;
            DataGridViewRow dgvRows = dgEnterSales.Rows[RoWindex];
            if (dgvRows.Cells["Ship"].Value != null && dgvRows.Cells["Ship"].Value.ToString() != "")
            {
                shipVal = Convert.ToDouble(dgvRows.Cells["Ship"].Value.ToString());
                dgvRows.Cells["Ship"].Value = shipVal.ToString();
            }
            else
            {
                dgvRows.Cells["Ship"].Value = 1;
            }

            if (salestype_cb.Text == "INVOICE") //APPLY PROMO ON INVOICES ONLY
            {
                if (dgvRows.Cells["Price"].Value != null)
                {
                    //MessageBox.Show("Price:" + dgvRows.Cells["Price"].Value.ToString());
                    //APPLY NON DEFAULT PROMOS
                    isItemPromo(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), float.Parse(dgvRows.Cells["Price"].Value.ToString()));
                    //APPLY DEFAULT PROMOS for Loyalty Members

                    if (memberID != "")
                    {
                        // MessageBox.Show("Price:" + dgvRows.Cells["Price"].Value.ToString());
                        dgvRows.Cells["Points"].Value = isItemPromo(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), float.Parse(dgvRows.Cells["Price"].Value.ToString()), "Default");
                    }
                    priceVal = double.Parse(dgvRows.Cells["Price"].Value.ToString(), NumberStyles.Currency);
                }
            }
            else
            {
                if (dgvRows.Cells["Price"].Value != null)
                {
                    priceVal = double.Parse(dgvRows.Cells["Price"].Value.ToString(), NumberStyles.Currency);
                }
            }
            if (dgvRows.Cells["Discount"].Value == null || dgvRows.Cells["Discount"].Value.ToString() == "")
            {
                woDisc = shipVal * priceVal;
            }
            else
            {
                disc = (dgvRows.Cells["Discount"].Value.ToString() == "" ? 0 : Convert.ToDouble(dgvRows.Cells["Discount"].Value.ToString().Replace("%", "").Trim()));
                woDisc = shipVal * priceVal;
                discValue = shipVal * priceVal * (disc / 100);
            }
            amtVal = woDisc - discValue;

            //DataTable lTbError = CheckOnHandQtyNewSale();
            //if (lTbError.Rows.Count > 0)
            //{
            //    ItemErrorInfo ItemError = new ItemErrorInfo(lTbError);
            //    if (ItemError.ShowDialog() == DialogResult.OK)
            //    {
            //        DataGridViewRow dgvr = dgEnterSales.CurrentRow;
            //        dgvr.Cells["Ship"].Value = 0;

            //    }
            //    return;
            //}
            //if (shipVal > 0)
            //{

            //}
                dgvRows.Cells["Amount"].Value = woDisc - discValue;
                prevAmt = dgvRows.Cells["Amount"].Value.ToString();
            

            //dgvRows.Cells["Price"].Value = woDisc - discValue;

            if (dgvRows.Cells["AutoBuild"].Value != null && bool.Parse(dgvRows.Cells["AutoBuild"].Value.ToString()))
            {
                if (dgvRows.Cells["BundleType"].Value.ToString() == "Dynamic")
                    IsAutoBuild(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), dgEnterSales.CurrentRow.Index);
            }
            VerifyItemQty(RoWindex, (XsalesID == "" ? "0" : XsalesID));

        }

        private void dgEnterSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            switch (e.ColumnIndex)
            {
                case 4:
                    this.dgEnterSales.CurrentCell = this.dgEnterSales.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgEnterSales.BeginEdit(true);
                    break;
                case 6: //Price
                case 8: //Discount
                case 9: //Amount                               
                    this.dgEnterSales.CurrentCell = this.dgEnterSales.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgEnterSales.BeginEdit(true);
                    break;
                case 10://Tax
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                    break;
                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }

        private void Terms_Click(object sender, EventArgs e)
        {
            ShowTermLookUp();
        }

        void Recalc()
        {
            DataRow rTx = CommonClass.getTaxDetails(ProfileTax);
            if (rTx.ItemArray.Length > 0)
            {
                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                string lTaxCollectedAccountID = "";
                lTaxCollectedAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                lTaxRate = ltaxrate;

                if (!IsLoading)
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            lAmount = float.Parse(TotalAmount.Value.ToString(), NumberStyles.Currency);
                            lTaxEx = lAmount / (1 + (lTaxRate / 100));
                            lTaxInc = lAmount;
                            TotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                    else
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            lTaxInc = lAmount * (1 + (lTaxRate / 100));
                            lTaxEx = lAmount;
                            TotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
                else
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            TotalAmount.Value = Convert.ToDecimal(lTaxInc);
                        }
                    }
                    else
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            TotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
            }
        }

        public static DataTable GetSalesLines(int pSalesID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT l.*, s.SalesNumber, s.GrandTotal, s.Memo, s.FreightSubTotal, s.FreightTax, s.FreightTaxCode, s.FreightTaxRate, i.AssetAccountID, 
                    i.IsCounted, i.IncomeAccountID, i.IsSold, i.COSAccountID, i.IsBought FROM ( SalesLines l INNER JOIN Sales s ON l.SalesID = s.SalesID ) left join Items as i on l.EntityID =i.ID WHERE l.SalesID = " + pSalesID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        private bool CreateJournalEntries(int pID)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                DataTable ltb = GetSalesLines(pID);
                if (ltb.Rows.Count > 0)
                {
                    decimal lGrandTotal = Convert.ToDecimal(ltb.Rows[0]["GrandTotal"].ToString());
                    string lRecipientID = AR_AccountID;
                    string lSalesNum = InvoiceNumTxt.Text;
                    string lMemo = String.Format("{0}", ltb.Rows[0]["Memo"].ToString());
                    string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    decimal lFreightEx = Convert.ToDecimal(ltb.Rows[0]["FreightSubTotal"].ToString());
                    decimal lFreightTax = Convert.ToDecimal(ltb.Rows[0]["FreightTax"].ToString());
                    string lSalesID = ltb.Rows[0]["SalesID"].ToString();
                    //INSERT JOURNAL FOR Total Amount Received
                    if (TotalAmount.Value < 0)
                    {
                        //NEGATIVE SO CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', '" + lRecipientID + "', " +
                             (TotalAmount.Value * -1).ToString() + ",'" + lSalesNum + "', 'SI', " + lSalesID + ")";
                    }
                    else
                    {
                        //DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID) " +
                               " VALUES('" + lTranDate + "','" + lMemo + "','" + lMemo + "','" + lRecipientID + "', " +
                              TotalAmount.Value + ",'" + lSalesNum + "','SI', " + lSalesID + ")";
                    }
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    //INSERT JOURNAL FOR FREIGHT 
                    if (lFreightEx != 0)
                    {
                        if (lFreightEx < 0) // NEGATIVE SO DEBIT AMOUNT 
                        {
                            // NEGATIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', '" + AR_FreightAccountID + "', " +
                                  (lFreightEx * -1).ToString() + ", '" + lSalesNum + "', 'SI',0, '" + lSalesID + "')";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lFreightTax != 0 && FreightTaxAccountID != "")
                            {

                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', '" + FreightTaxAccountID + "', " +
                                      (lFreightTax * -1).ToString() + ", '" + lSalesNum + "', 'SI',0, '" + lSalesID + "')"; ;
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else //POSITIVE SO CREDIT AMOUNT 
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', '" + AR_FreightAccountID + "', " +
                                   lFreightEx.ToString() + ", '" + lSalesNum + "', 'SI',0, '" + lSalesID + "')";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lFreightTax != 0 && FreightTaxAccountID != "")
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', '" + FreightTaxAccountID + "', " +
                                     lFreightTax.ToString() + ", '" + lSalesNum + "','SI',0, '" + lSalesID + "')";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        string lAccountID = "";
                        decimal lTaxEx = Convert.ToDecimal(ltb.Rows[i]["SubTotal"].ToString());
                        decimal lTaxInc = Convert.ToDecimal(ltb.Rows[i]["TotalAmount"].ToString());
                        decimal lTaxAmt = lTaxInc - lTaxEx;
                        string lTaxCollectedAccountID = (ltb.Rows[i]["TaxCollectedAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["TaxCollectedAccountID"].ToString());
                        string lJobID = (ltb.Rows[i]["JobID"].ToString() == "" ? "0" : ltb.Rows[i]["JobID"].ToString());
                        string lIncomeAccountID = (ltb.Rows[i]["IncomeAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["IncomeAccountID"].ToString());
                        string lAssetAccountID = (ltb.Rows[i]["AssetAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["AssetAccountID"].ToString());
                        string lCOSAccountID = (ltb.Rows[i]["COSAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["COSAccountID"].ToString());
                        bool lIsCounted = (ltb.Rows[i]["IsCounted"].ToString() == "" ? false : (bool)ltb.Rows[i]["IsCounted"]);

                        //  string lLineMemo = ltb.Rows[i]["LineMemo"].ToString();
                        string lEntity = ltb.Rows[i]["EntityID"].ToString();
                        lAccountID = lIncomeAccountID;

                        if (lTaxEx < 0) // NEGATIVE SO DEBIT AMOUNT 
                        {
                            //FOR INCOME
                            // NEGATIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,DebitAmount, TransactionNumber, Type, JobID, EntityID) VALUES('" + lTranDate + "','" + lMemo + "','" + lMemo + "'," + lAccountID + "," + (lTaxEx * -1) + ",'" + lSalesNum + "', 'SI','" + lJobID + "','" + lEntity + "')";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0)
                            {
                                lTaxAmt = lTaxEx - lTaxInc;
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID,
                                                    DebitAmount, TransactionNumber, Type, JobID, EntityID)";
                                sql += " VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "','" + lTaxCollectedAccountID + "', " + lTaxAmt.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + ",'" + lSalesID + "')";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            if (lIsCounted)
                            {
                                decimal lTotalCost = (ltb.Rows[i]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(ltb.Rows[i]["TotalCost"].ToString()));
                                //FOR INVENTORY
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "','" + lAssetAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                //FOR COST OF SALES
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "','" + lCOSAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else //POSITIVE SO CREDIT AMOUNT 
                        {
                            //FOR INCOME
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,CreditAmount, TransactionNumber, Type, JobID, EntityID) VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', '" + lAccountID + "', " + lTaxEx + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0 && lTaxCollectedAccountID != "")
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID) VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "','" + lTaxCollectedAccountID + "'," + lTaxAmt.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + ",'" + lSalesID + "')";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            if (lIsCounted)
                            {
                                decimal lTotalCost = (ltb.Rows[i]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(ltb.Rows[i]["TotalCost"].ToString()));
                                //FOR INVENTORY
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "','" + lAssetAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                //FOR COST OF SALES
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "','" + lCOSAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction. No Sales Lines found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void LoadTransaction()
        {
            TermsText.Visible = true;
            btnSaveRecurring.Enabled = true;
            string cID;
            //string salestype = SalesRegister
            int salesid = Convert.ToInt32(XsalesID);
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 

                string sql = @"SELECT s.*, c.*, u.user_id, u.user_name
                             FROM Sales s 
                             INNER JOIN Profile c ON s.CustomerID = c.ID
                             LEFT JOIN Users u ON u.user_id = s.SalesPersonID
                             WHERE  s.SalesID = " + salesid;
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRepSales = new DataTable();

                da.Fill(TbRepSales);

                if (TbRepSales.Rows.Count > 0)
                {
                    string dte = TbRepSales.Rows[0]["TransactionDate"].ToString();
                    salesDate.Value = DateTime.Parse(dte).ToLocalTime();
                    cID = TbRepSales.Rows[0]["CustomerID"].ToString();
                    TermRefID = TbRepSales.Rows[0]["TermsReferenceID"].ToString();
                    FromSalesType = TbRepSales.Rows[0]["SalesType"].ToString();

                    if (SrcOfInvoke == CommonClass.InvocationSource.CHANGETO)
                    {
                        this.salestype_cb.Text = ToSalesType;
                        salesDate.Value = DateTime.Now.ToUniversalTime();
                    }
                    else
                    {
                        this.salestype_cb.Text = TbRepSales.Rows[0]["SalesType"].ToString();
                    }

                    LoadInvoiceTerms();

                    if (TermRefID == "0")
                    {
                        LoadDefaultTerms(cID);
                    }

                    InvoiceNumTxt.Text = TbRepSales.Rows[0]["SalesNumber"].ToString();
                    defSalesNum = TbRepSales.Rows[0]["SalesNumber"].ToString();
                    txtFTaxCode.Text = TbRepSales.Rows[0]["FreightTaxCode"].ToString();
                    invoiceStatus = TbRepSales.Rows[0]["InvoiceStatus"].ToString();
                    string strtotamtrecvd = TbRepSales.Rows[0]["GrandTotal"].ToString();
                    TotalAmount.Value = strtotamtrecvd != "" ? Convert.ToDecimal(strtotamtrecvd) : 0;
                    string subamountstr = TbRepSales.Rows[0]["SubTotal"].ToString();
                    subtotalAmountText.Value = subamountstr != "" ? Convert.ToDecimal(subamountstr) : 0;
                    string taxAmountStr = TbRepSales.Rows[0]["TaxTotal"].ToString();
                    TaxAmount.Value = taxAmountStr != "" ? Convert.ToDecimal(taxAmountStr) : 0;
                    customerText.Text = TbRepSales.Rows[0]["Name"].ToString();
                    MemoText.Text = TbRepSales.Rows[0]["Memo"].ToString();
                    shipAddressID = Convert.ToInt32(TbRepSales.Rows[0]["ShippingContactID"].ToString());
                    string istaxInclusive = TbRepSales.Rows[0]["IsTaxInclusive"].ToString();
                    string strtottax = TbRepSales.Rows[0]["TaxTotal"].ToString();
                    salestype_cb.Enabled = false;
                    CustomerID = TbRepSales.Rows[0]["ID"].ToString();
                    SalesPersonID = TbRepSales.Rows[0]["user_id"].ToString();
                    txtSalesperson.Text = TbRepSales.Rows[0]["user_name"].ToString();

                    string sqlconttype = "SELECT ContactID, TypeOfContact,Location FROM Contacts WHERE ProfileID = @ProfileID";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@ProfileID", CustomerID);
                    param.Add("@Location", TbRepSales.Rows[0]["LocationID"]);
                    DataTable dt = new DataTable();
                    CommonClass.runSql(ref dt, sqlconttype, param);
                    List<KeyValuePair<string, string>> mylist = new List<KeyValuePair<string, string>>();
                    string locID = "";
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            mylist.Add(new KeyValuePair<string, string>(dr["ContactID"].ToString(), dr["TypeOfContact"].ToString()));
                            locID = dr["Location"].ToString();
                        }
                        cmb_shippingcontact.DataSource = new BindingSource(mylist, null);
                        cmb_shippingcontact.DisplayMember = "Value";
                        cmb_shippingcontact.ValueMember = "Key";
                    }
                    if (locID != "")
                    {
                        LoadContacts(Convert.ToInt32(CustomerID), Convert.ToInt32(locID));
                    }

                    //Get CustomerBalance & Credit Limit
                    CustomerBalance = (CustomerID == "" ? 0 : float.Parse(TbRepSales.Rows[0]["CurrentBalance"].ToString().ToString(), NumberStyles.Currency));
                    CustomerCreditLimit = (CustomerID == "" ? 0 : float.Parse(TbRepSales.Rows[0]["CreditLimit"].ToString(), NumberStyles.Currency));

                    TaxAmount.Value = strtottax != "" ? Convert.ToDecimal(strtottax) : 0;
                    CommentsText.Text = TbRepSales.Rows[0]["Comments"].ToString();
                    SalesRefText.Text = TbRepSales.Rows[0]["SalesReference"].ToString();
                    custPOText.Text = TbRepSales.Rows[0]["CustomerPONumber"].ToString();
                    LoadShippingMethod(TbRepSales.Rows[0]["ShippingMethodID"].ToString());
                    ShippingmethodText.Text = ShipVia;
                    if (TbRepSales.Rows[0]["SalesType"].ToString() == "INVOICE")
                    {
                        OldBalanceDue = Convert.ToDecimal(TbRepSales.Rows[0]["TotalDue"].ToString());
                        if (TbRepSales.Rows[0]["InvoiceType"].ToString().ToUpper() == "CASH")
                        {
                            rdbCash.Checked = true;
                            rdbCash.Enabled = false;
                        }
                        else
                        {
                            rdbAR.Checked = true;

                        }
                        rdbCash.Enabled = false;
                        rdbAR.Enabled = false;
                    }
                    FreightTax = float.Parse(TbRepSales.Rows[0]["FreightTax"].ToString());
                    FreightAmountEx = float.Parse(TbRepSales.Rows[0]["FreightSubTotal"].ToString());
                    FreightAmountInc = FreightTax + FreightAmountEx;
                    FreightTaxRate = float.Parse(TbRepSales.Rows[0]["FreightTaxRate"].ToString());
                    this.txtFTaxCode.Text = TbRepSales.Rows[0]["FreightTaxCode"].ToString();
                    //GET FreightTaxAccountID
                    FreightTaxAccountID = "0";
                    DataRow rTx = CommonClass.getTaxDetails(this.txtFTaxCode.Text);
                    if (rTx.ItemArray.Length > 0)
                    {
                        FreightTaxAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                    }

                    if (istaxInclusive == "Y")
                    {
                        CommonClass.IsTaxcInclusiveEnterSales = true;
                        txtFreight.Value = (decimal)FreightAmountInc;
                    }
                    else
                    {
                        CommonClass.IsTaxcInclusiveEnterSales = true;
                        txtFreight.Value = (decimal)FreightAmountEx;
                    }

                    //CHECK IF INVOICE HAS TOTAL PAID
                    string PaidStr = TbRepSales.Rows[0]["TotalPaid"].ToString();
                    PaidToday_txt.Value = PaidStr != "" ? Convert.ToDecimal(PaidStr) : 0;
                    PrevPaid = PaidToday_txt.Value;

                    if (PaidToday_txt.Value != 0)
                    {
                        PaidToday_txt.Enabled = false;
                        lblPaidToday.Text = "Paid To Date:";
                        btnPaymentDetails.Visible = false;
                        //PaymentMethodTxt.Visible = false;
                        btnViewPayments.Visible = false;
                    }

                    TbRepSalesLines = new DataTable();

                    //For ITEM works for newly created sales
                    sql = @"SELECT l.*, j.*, i.PartNumber, i.ItemNumber, ISNULL(ap.PointsAccumulated,0) as PointsAccumulated, ISNULL(ap.CustomerID,0) as lMemberID, ISNULL(lm.Number,'') as lMemberNo
                            FROM Sales s 
                            INNER JOIN SalesLines l ON s.SalesID = l.SalesID
                            LEFT JOIN Jobs j ON l.JobID = j.JobID
                            INNER JOIN Items i ON i.ID = l.EntityID
                            LEFT JOIN AccumulatedPoints ap ON ap.SalesLineID = l.SalesLineID
                            LEFT JOIN LoyaltyMember lm ON ap.CustomerID = lm.ID
                            WHERE s.SalesID = " + salesid;

                    da = new SqlDataAdapter();
                    cmd.CommandText = sql;
                    da.SelectCommand = cmd;
                    da.Fill(TbRepSalesLines);
                    DataGridViewRow DRow;

                    for (int i = 0; i < TbRepSalesLines.Rows.Count; i++)
                    {
                        dgEnterSales.Rows.Add();
                        DRow = dgEnterSales.Rows[i];
                        string stramt = "";

                        DRow.Cells["ItemID"].Value = TbRepSalesLines.Rows[i]["EntityID"].ToString();
                        DRow.Cells["PartNumber"].Value = TbRepSalesLines.Rows[i]["PartNumber"].ToString();
                        DRow.Cells["Ship"].Value = TbRepSalesLines.Rows[i]["ShipQty"].ToString();
                        float lTAmt = 0;
                        float lQty = float.Parse(TbRepSalesLines.Rows[i]["ShipQty"].ToString());
                        float lDiscountRate = float.Parse(TbRepSalesLines.Rows[i]["DiscountPercent"].ToString());
                        float lTaxRate = float.Parse(TbRepSalesLines.Rows[i]["TaxRate"].ToString());
                        float lUPrice = float.Parse(TbRepSalesLines.Rows[i]["UnitPrice"].ToString());
                        float lActualUPrice = float.Parse(TbRepSalesLines.Rows[i]["ActualUnitPrice"].ToString());
                        float lUP = 0;
                        float lCostPrice = float.Parse(TbRepSalesLines.Rows[i]["CostPrice"].ToString());
                        if (istaxInclusive == "Y")
                        {
                            lTAmt = float.Parse(TbRepSalesLines.Rows[i]["TotalAmount"].ToString());
                            lUP = lTAmt / lQty;
                            if (lUP > 0 && lUP != lUPrice)
                            {
                                lUPrice = lUP;
                            }
                        }
                        else
                        {
                            lTAmt = float.Parse(TbRepSalesLines.Rows[i]["SubTotal"].ToString());
                            lTAmt = float.Parse(TbRepSalesLines.Rows[i]["TotalAmount"].ToString());
                            lUP = lTAmt / lQty;
                            if (lUP > 0 && lUP != lUPrice)
                            {
                                lUPrice = lUP;
                            }
                        }
                        DRow.Cells["Price"].Value = lUPrice;
                        DRow.Cells["Amount"].Value = lTAmt;
                        DRow.Cells["DiscountRate"].Value = lDiscountRate;
                        DRow.Cells["Discount"].Value = (lDiscountRate == 0 ? null : lDiscountRate.ToString() + "%");
                        DRow.Cells["ActualUnitPrice"].Value = lActualUPrice;
                        DRow.Cells["CostPrice"].Value = lCostPrice;
                        DRow.Cells["Points"].Value = TbRepSalesLines.Rows[i]["PointsAccumulated"];

                        DRow.Cells["Description"].Value = TbRepSalesLines.Rows[i]["Description"].ToString();
                        DRow.Cells["Job"].Value = TbRepSalesLines.Rows[i]["JobName"].ToString();
                        DRow.Cells["JobID"].Value = TbRepSalesLines.Rows[i]["JobID"].ToString();
                        DRow.Cells["TaxCode"].Value = TbRepSalesLines.Rows[i]["TaxCode"].ToString();//Compute taxEx/Taxin 
                        DRow.Cells["TaxExclusiveAmount"].Value = float.Parse(TbRepSalesLines.Rows[i]["SubTotal"].ToString());
                        DRow.Cells["TaxInclusiveAmount"].Value = float.Parse(TbRepSalesLines.Rows[i]["TotalAmount"].ToString());
                        DRow.Cells["TaxCollectedAccountID"].Value = TbRepSalesLines.Rows[i]["TaxCollectedAccountID"].ToString();
                        string lrate = TbRepSalesLines.Rows[i]["TaxRate"].ToString();
                        DRow.Cells["TaxRate"].Value = (lrate == "" ? 0 : float.Parse(lrate));
                    }

                    this.lblLoyalty.Text = TbRepSalesLines.Rows[0]["lMemberNo"].ToString();
                    memberID = TbRepSalesLines.Rows[0]["lMemberID"].ToString();
                    this.btnLoyaltyMem.Enabled = false;
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

        private void LoadContacts(int cID, int index)
        {
            SqlConnection con = new SqlConnection(CommonClass.ConStr);
            //GET THE HEADER 
            string sql = @"SELECT * FROM Contacts WHERE ProfileID = " + cID + " and Location = " + index;

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                shipAddressID = 0;
                return;
            }
            else
            {
                PayeeInfo.Text = dt.Rows[0]["Street"].ToString() + ", " + dt.Rows[0]["City"].ToString() + ", " + dt.Rows[0]["State"].ToString() + ", " + dt.Rows[0]["Country"].ToString() + ", " + dt.Rows[0]["PostCode"].ToString();
                LocationID = dt.Rows[0]["Location"].ToString();
                shipAddressID = Convert.ToInt32(dt.Rows[0]["ContactID"]);/*index - 1;*/
            }
        }

        public void ShowTermLookUp()
        {
            if (CustomerID == null)
            {
                MessageBox.Show("Please select a customer first", "Information");
            }
            else
            {
                TermsLookup ProfileDlg = new TermsLookup(SalesTermsRow);
                if (ProfileDlg.ShowDialog() == DialogResult.OK)
                {
                    SalesTermsRow = ProfileDlg.GetTerms;
                    //TermsText.Text = lProfile[0];
                    //BalanceDueDays = lProfile[9] == null ? "0" : lProfile[9];
                    //DiscountDays = lProfile[10] == null ? "0" : lProfile[10];
                    //VolumeDiscount = lProfile[3] == null ? "0" : lProfile[5]; ;
                    //EarlyPaymenDiscount = lProfile[4] == null ? "0" : lProfile[4];
                    //LatePaymenDiscount = lProfile[5] == null ? "0" : lProfile[5];
                    //baldate = lProfile[6] == null ? "0" : lProfile[6];
                    //discountdate = lProfile[7] == null ? "0" : lProfile[7];
                    TermRefID = SalesTermsRow["TermsOfPaymentID"].ToString();
                    if (TermRefID == "CASH")
                    {
                        TermsText.Text = "CASH";
                    }
                    else
                    {
                        TermsText.Text = SalesTermsRow["BalanceDueDays"].ToString() + " Days";
                    }
                    PromiseDate.Value = PromiseDate.Value.AddDays(Convert.ToDouble(BalanceDueDays));
                }
            }
        }

        private void LoadDefaultTerms(string pProfileID) // This function pulls out the default terms from the profile.
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM  Profile  WHERE ID = " + pProfileID;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CustomerRow = dt.Rows[0];
                    TermsOfPaymentID = CustomerRow["TermsOfPayment"].ToString();
                    SalesTermsRow["TermsOfPaymentID"] = CustomerRow["TermsOfPayment"];
                    VolumeDiscount = CustomerRow["VolumeDiscount"].ToString();
                    SalesTermsRow["VolumeDiscount"] = CustomerRow["VolumeDiscount"];
                    EarlyPaymenDiscount = CustomerRow["EarlyPaymentDiscountPercent"].ToString();
                    SalesTermsRow["EarlyPaymentDiscountPercent"] = CustomerRow["EarlyPaymentDiscountPercent"];
                    LatePaymenDiscount = CustomerRow["LatePaymentChargePercent"].ToString();
                    SalesTermsRow["LatePaymentChargePercent"] = CustomerRow["LatePaymentChargePercent"];

                    switch (TermsOfPaymentID)
                    {
                        case "DM"://Day of the Month
                            baldate = CustomerRow["BalanceDueDate"].ToString();
                            SalesTermsRow["BalanceDueDate"] = CustomerRow["BalanceDueDate"];
                            discountdate = CustomerRow["DiscountDate"].ToString();
                            SalesTermsRow["DiscountDate"] = CustomerRow["DiscountDate"];
                            TermsText.Text = "Day of the Month";
                            break;
                        case "DMEOM": //Day of the Month after EOM
                            baldate = CustomerRow["BalanceDueDate"].ToString();
                            SalesTermsRow["BalanceDueDate"] = CustomerRow["BalanceDueDate"];
                            discountdate = CustomerRow["DiscountDate"].ToString();
                            SalesTermsRow["DiscountDate"] = CustomerRow["DiscountDate"];
                            TermsText.Text = "Day of the Month after EOM";
                            break;
                        case "SD": //Specific Days
                            BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
                            SalesTermsRow["BalanceDueDays"] = CustomerRow["BalanceDueDays"];

                            DiscountDays = CustomerRow["DiscountDays"].ToString();
                            SalesTermsRow["DiscountDays"] = CustomerRow["DiscountDays"];
                            TermsText.Text = "Specific Days";
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
                            SalesTermsRow["BalanceDueDays"] = CustomerRow["BalanceDueDays"];
                            DiscountDays = CustomerRow["DiscountDays"].ToString();
                            SalesTermsRow["DiscountDays"] = CustomerRow["DiscountDays"];
                            TermsText.Text = "Specific Days after EOM";
                            break;
                        default: //CASH
                            TermsText.Text = TermsOfPaymentID;
                            SalesTermsRow["BalanceDueDate"] = 0;
                            SalesTermsRow["DiscountDate"] = 0;
                            SalesTermsRow["BalanceDueDays"] = 0;
                            SalesTermsRow["DiscountDays"] = 0;
                            break;
                    }
                    this.TermsText.Text = (TermsOfPaymentID != "CASH" ? BalanceDueDays.ToString() + " Days" : TermsOfPaymentID);
                }
                btnAddShipAddress.Enabled = true;
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

        private void LoadTerms()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT t.Description, p.* FROM TermsOfPayment t INNER JOIN Profile p ON t.TermsOfPaymentID = p.TermsOfPayment WHERE TermsOfPaymentID =  '" + TermRefID + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CustomerRow = dt.Rows[0];
                }
                TermsText.Text = CustomerRow["Description"].ToString();
                BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
                DiscountDays = CustomerRow["DiscountDays"].ToString();
                VolumeDiscount = CustomerRow["VolumeDiscount"].ToString();
                EarlyPaymenDiscount = CustomerRow["EarlyPaymentDiscountPercent"].ToString();
                LatePaymenDiscount = CustomerRow["LatePaymentChargePercent"].ToString();
                baldate = CustomerRow["BalanceDueDate"].ToString();
                discountdate = CustomerRow["DiscountDate"].ToString();
                PromiseDate.Value = PromiseDate.Value.AddDays(Convert.ToDouble(BalanceDueDays));
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

        private void LoadShippingID(string pshippingMethod)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM ShippingMethods WHERE ShippingMethod = '" + pshippingMethod + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                ShippingMethodID = "0";
                if (dt.Rows.Count > 0)
                {
                    ShipID = dt.Rows[0];
                    ShippingMethodID = ShipID["ShippingID"].ToString();
                    ShipVia = ShipID["ShippingMethod"].ToString();
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

        private void LoadShippingMethod(string pShipID)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM ShippingMethods WHERE ShippingID = '" + pShipID + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                ShipVia = "";
                ShippingMethodID = "0";
                if (dt.Rows.Count > 0)
                {
                    ShipID = dt.Rows[0];
                    ShipVia = ShipID["ShippingMethod"].ToString();
                    ShippingMethodID = ShipID["ShippingID"].ToString();
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

        private void LoadInvoiceTerms() //This functions pulls out the specific terms set for the specific invoice.
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                if (TermRefID == "")
                {
                    TermRefID = "0";
                }
                string selectSql = "SELECT * FROM Terms where TermsID = " + TermRefID;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);

                BalanceDueDays = "0";
                if (dt.Rows.Count > 0)
                {
                    SalesTermsRow = dt.Rows[0];
                    TermsOfPaymentID = SalesTermsRow["TermsOfPaymentID"].ToString();
                    VolumeDiscount = SalesTermsRow["VolumeDiscount"].ToString();
                    EarlyPaymenDiscount = SalesTermsRow["EarlyPaymentDiscountPercent"].ToString();
                    LatePaymenDiscount = SalesTermsRow["LatePaymentChargePercent"].ToString();

                    switch (TermsOfPaymentID)
                    {
                        case "DM"://Day of the Month
                            baldate = SalesTermsRow["BalanceDueDate"].ToString();
                            discountdate = SalesTermsRow["DiscountDate"].ToString();

                            break;
                        case "DMEOM": //Day of the Month after EOM
                            baldate = SalesTermsRow["BalanceDueDate"].ToString();
                            discountdate = SalesTermsRow["DiscountDate"].ToString();
                            break;
                        case "SD": //Specific Days
                            BalanceDueDays = SalesTermsRow["BalanceDueDays"].ToString();
                            DiscountDays = SalesTermsRow["DiscountDays"].ToString();
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            BalanceDueDays = SalesTermsRow["BalanceDueDays"].ToString();
                            DiscountDays = SalesTermsRow["DiscountDays"].ToString();
                            break;
                        default: //CASH
                            TermsText.Text = TermsOfPaymentID;
                            break;
                    }
                    this.TermsText.Text = (TermsOfPaymentID != "CASH" ? BalanceDueDays.ToString() + " Days" : TermsOfPaymentID);
                }
                else
                {
                    SalesTermsRow = dt.NewRow();
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

        private int CreateTerm()
        {
            SqlConnection con = null;
            try
            {
                //Calculate Actual Due Date of the Transaction
                DateTime lTranDate = this.salesDate.Value.ToUniversalTime();
                DateTime lDueDate = lTranDate;
                DateTime lDiscountDate = lTranDate;
                string lTermsOfPaymentID = SalesTermsRow["TermsOfPaymentID"].ToString().Trim();
                switch (lTermsOfPaymentID)
                {
                    //case "DM"://Day of the Month
                    //    baldate = SalesTermsRow["BalanceDueDate"].ToString();
                    //    discountdate = SalesTermsRow["DiscountDate"].ToString();

                    //    break;
                    //case "DMEOM": //Day of the Month after EOM
                    //    baldate = SalesTermsRow["BalanceDueDate"].ToString();
                    //    discountdate = SalesTermsRow["DiscountDate"].ToString();
                    //    break;

                    //case "SDEOM"://Specifc Day after EOM
                    //    BalanceDueDays = SalesTermsRow["BalanceDueDays"].ToString();
                    //    DiscountDays = SalesTermsRow["DiscountDays"].ToString();
                    //    break;
                    case "SD": //Specific Days
                        BalanceDueDays = SalesTermsRow["BalanceDueDays"].ToString();
                        DiscountDays = SalesTermsRow["DiscountDays"].ToString();
                        lDueDate = lTranDate.AddDays(Convert.ToInt16(BalanceDueDays));
                        lDiscountDate = lTranDate.AddDays(Convert.ToInt16(DiscountDays));
                        break;
                    default: //CASH
                        BalanceDueDays = "0";
                        DiscountDays = "0";
                        break;
                }
                ActualDueDate = lDueDate.ToString("yyyy-MM-dd");
                ActualDiscountDate = lDiscountDate.ToString("yyyy-MM-dd");
                //Create Terms
                con = new SqlConnection(CommonClass.ConStr);
                string termsql = @"INSERT INTO Terms (TermsOfPaymentID, DiscountDays, BalanceDueDays, VolumeDiscount, ActualDueDate, ActualDiscountDate,EarlyPaymentDiscountPercent,LatePaymentChargePercent) 
                                   VALUES (@TermsOfPaymentID, @DiscountDays, @BalanceDueDays, @VolumeDiscount, @ActualDueDate,@ActualDiscountDate,@EarlyPaymentDiscountPercent,@LatePaymentChargePercent); 
                                   SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(termsql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@TermsOfPaymentID", SalesTermsRow["TermsOfPaymentID"]);
                cmd.Parameters.AddWithValue("@DiscountDays", SalesTermsRow["DiscountDays"]);
                cmd.Parameters.AddWithValue("@BalanceDueDays", SalesTermsRow["BalanceDueDays"]);
                cmd.Parameters.AddWithValue("@VolumeDiscount", SalesTermsRow["VolumeDiscount"]);
                cmd.Parameters.AddWithValue("@ActualDueDate", ActualDueDate);
                cmd.Parameters.AddWithValue("@ActualDiscountDate", ActualDiscountDate);
                cmd.Parameters.AddWithValue("@EarlyPaymentDiscountPercent", SalesTermsRow["EarlyPaymentDiscountPercent"].ToString());
                cmd.Parameters.AddWithValue("@LatePaymentChargePercent", SalesTermsRow["LatePaymentChargePercent"].ToString());

                con.Open();
                int NewTermID = Convert.ToInt32(cmd.ExecuteScalar());
                return NewTermID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
                createterm = true;
            }
        }

        private int UpdateInvoiceTerm(string pTermID)
        {
            SqlConnection con = null;
            try
            {
                //Calculate Actual Due Date of the Transaction
                DateTime lTranDate = this.salesDate.Value.ToUniversalTime();
                DateTime lDueDate = lTranDate;
                DateTime lDiscountDate = lTranDate;
                string lTermsOfPaymentID = SalesTermsRow["TermsOfPaymentID"].ToString().Trim();
                switch (lTermsOfPaymentID)
                {
                    //case "DM"://Day of the Month
                    //     baldate = SalesTermsRow["BalanceDueDate"].ToString();
                    //     discountdate = CustomerRow["DiscountDate"].ToString();

                    //     break;
                    // case "DMEOM": //Day of the Month after EOM
                    //     baldate = SalesTermsRow["BalanceDueDate"].ToString();
                    //     discountdate = SalesTermsRow["DiscountDate"].ToString();
                    //     break;

                    // case "SDEOM"://Specifc Day after EOM
                    //     BalanceDueDays = SalesTermsRow["BalanceDueDays"].ToString();
                    //     DiscountDays = SalesTermsRow["DiscountDays"].ToString();
                    //     break;
                    case "SD": //Specific Days
                        BalanceDueDays = SalesTermsRow["BalanceDueDays"].ToString();
                        DiscountDays = SalesTermsRow["DiscountDays"].ToString();
                        lDueDate = lTranDate.AddDays(Convert.ToInt16(BalanceDueDays));
                        lDiscountDate = lTranDate.AddDays(Convert.ToInt16(DiscountDays));
                        break;
                    default: //CASH
                        BalanceDueDays = "0";
                        DiscountDays = "0";
                        break;
                }
                ActualDueDate = lDueDate.ToString("yyyy-MM-dd");
                ActualDiscountDate = lDiscountDate.ToString("yyyy-MM-dd");
                //Create Terms
                con = new SqlConnection(CommonClass.ConStr);
                string termsql = @"UPDATE Terms set TermsOfPaymentID = @TermsOfPaymentID, 
                                    DiscountDays = @DiscountDays, 
                                    BalanceDueDays = @BalanceDueDays, 
                                    VolumeDiscount = @VolumeDiscount, 
                                    ActualDueDate = @ActualDueDate, 
                                    ActualDiscountDate = @ActualDiscountDate,
                                    EarlyPaymentDiscountPercent = @EarlyPaymentDiscountPercent,
                                    LatePaymentChargePercent = @LatePaymentChargePercent
                                    Where TermsID = " + pTermID;
                SqlCommand cmd = new SqlCommand(termsql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@TermsOfPaymentID", SalesTermsRow["TermsOfPaymentID"]);
                cmd.Parameters.AddWithValue("@DiscountDays", SalesTermsRow["DiscountDays"]);
                cmd.Parameters.AddWithValue("@BalanceDueDays", SalesTermsRow["BalanceDueDays"]);
                cmd.Parameters.AddWithValue("@VolumeDiscount", SalesTermsRow["VolumeDiscount"]);
                cmd.Parameters.AddWithValue("@ActualDueDate", ActualDueDate);
                cmd.Parameters.AddWithValue("@ActualDiscountDate", ActualDiscountDate);
                cmd.Parameters.AddWithValue("@EarlyPaymentDiscountPercent", SalesTermsRow["EarlyPaymentDiscountPercent"].ToString());
                cmd.Parameters.AddWithValue("@LatePaymentChargePercent", SalesTermsRow["LatePaymentChargePercent"].ToString());

                con.Open();
                int NewTermID = Convert.ToInt32(cmd.ExecuteScalar());
                return NewTermID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
                createterm = true;
            }
        }

        private void BalanceDue_txt_ValueChanged(object sender, EventArgs e)
        {
        }

        private void PaidToday_txt_ValueChanged(object sender, EventArgs e)
        {
            CalcOutOfBalance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Users";
            string salesperon = "";
            string technician = "";
            DataTable dtAuthorization = new DataTable();

            if (btnremove.Text == "Remove Line")
            {
                VoidValidation DlgVoid = new VoidValidation();
                if (DlgVoid.ShowDialog() == DialogResult.OK)
                {
                    password = DlgVoid.GetPassword;
                    username = DlgVoid.GetUsername;
                }
            }
            sql += " WHERE user_name = '" + username + "' AND user_pwd = '" + CommonClass.SHA512(password) + "'";
            CommonClass.runSql(ref dtAuthorization, sql);
            foreach (DataRow dr in dtAuthorization.Rows)
            {
                salesperon = dr["IsSalesperson"].ToString();
                technician = dr["IsTechnician"].ToString();
            }
            if (dtAuthorization.Rows.Count > 0 && !bool.Parse(salesperon) && !bool.Parse(technician))
            {
                btnremove.Text = "Remove";
            }

            if (btnremove.Text == "Remove")
            {
                foreach (DataGridViewCell oneCell in dgEnterSales.SelectedCells)
                {
                    if (oneCell.RowIndex >= 0 && oneCell.RowIndex < (dgEnterSales.Rows.Count - 1))
                        dgEnterSales.Rows.RemoveAt(oneCell.RowIndex);
                }
                CalcOutOfBalance();
                dgEnterSales.Refresh();
                dgEnterSales.Rows.Add();
            }
            if (CommonClass.isSalesperson == true)
            {
                btnremove.Text = "Remove Line";
            }

        }

        private void pbFTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                this.txtFTaxCode.Text = Tax[0];
                lblFreightTaxID.Text = Tax[4];
                FreightTaxRate = float.Parse(Tax[2]);
                FreightTaxAccountID = Tax[4];
                CalcFreight();
            }
        }

        private void CalcFreight()
        {
            if (CommonClass.IsTaxcInclusiveEnterSales)
            {
                FreightAmountInc = (float)this.txtFreight.Value;
                FreightAmountEx = FreightAmountInc / (1 + (FreightTaxRate / 100));
            }
            else
            {
                FreightAmountEx = (float)this.txtFreight.Value;
                FreightAmountInc = FreightAmountEx * (1 + (FreightTaxRate / 100));
            }
            FreightTax = FreightAmountInc - FreightAmountEx;
            CalcOutOfBalance();
        }

        private void LoadItemLayoutReport(string pSalesID)
        {
            if (cmbPrintFormat.SelectedIndex == 0)
            {
                string rptName = "ItemLayout.rpt";

                Reports.ReportParams salesItemlayoutparams = new Reports.ReportParams();
                salesItemlayoutparams.PrtOpt = 1;

                SqlConnection con = new SqlConnection(CommonClass.ConStr);
                string printSale = @"SELECT SalesType, SalesNumber , Sales.TransactionDate  , Sales.Comments, Sales.SubTotal , p.Name as ProfileName,
                    c.Street as ShipStreet, c.City as ShipCity, c.State as ShipState,
                   c2.Street as BillStreet, c2.City as BillCity, c2.State as BillState, 
                    sl.Description ,sl.TotalAmount, sl.TaxCode,Sales.TaxTotal,FreightTax,TotalPaid, GrandTotal,TotalDue,
                    ShippingMethod, i.ItemNumber, sl.ShipQty, sl.OrderQty, sl.CostPrice, sl.UnitPrice, sl.DiscountPercent, u.user_fullname,i.ItemDescription
                    FROM Sales
                    INNER JOIN Profile p on Sales.CustomerID = p.ID
                    Left JOIN ShippingMethods  sm On sm.ShippingID = Sales.ShippingMethodID
                    LEFT JOIN Contacts c ON c.Location = Sales.ShippingContactID and c.ProfileID = Sales.CustomerID  
                    LEFT JOIN Contacts c2 ON c2.Location = Sales.BillingContactID and c2.ProfileID = Sales.CustomerID 
                    INNER JOIN SalesLines sl ON sl.SalesID = Sales.SalesID
                    INNER JOIN Items i ON sl.EntityID = i.ID
                    INNER JOIN Users u ON u.user_id = Sales.SalesPersonID 
					WHERE Sales.SalesID = '" + pSalesID + "'";
                SqlCommand cmd = new SqlCommand(printSale, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                salesItemlayoutparams.Rec.Add(dt);
               
                string tenders = @"SELECT DISTINCT pd.PaymentDetailsID, pd.PaymentMethodID,pt.Amount,pm.PaymentMethod,p.TransactionDate FROM PaymentTender pt 
                        inner join PaymentDetails pd on pt.PaymentID = pd.PaymentID and pt.id = pd.PaymentDetailsID
                        inner join Payment p on pt.PaymentID = p.PaymentID
                        inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                        left join PaymentMethods pm on pd.PaymentMethodID = pm.id
                        where pt.Amount <> 0 and pl.EntityID = " + pSalesID;
                SqlConnection con2 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd2 = new SqlCommand(tenders, con2);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                decimal lchange = 0;
                string lTenders = "";
                for (int x = 0; x < dt2.Rows.Count; x++)
                {
                    if (dt2.Rows[x]["PaymentMethod"].ToString().ToUpper() == "CASH")
                    {
                        if (Convert.ToDecimal(dt2.Rows[x]["Amount"].ToString()) < 0)
                        {
                            dt2.Rows[x]["PaymentMethod"] = "Change";
                        }
                    }
                    lTenders += dt2.Rows[x]["PaymentMethod"].ToString() + "  " + Convert.ToDecimal(dt2.Rows[x]["Amount"].ToString()).ToString("C2") + "/";

                }
                if (salestype_cb.Text == "INVOICE")
                {
                    rptName = "ItemLayoutARInvoice.rpt";
                    if (rdbCash.Checked)
                    {
                        rptName = "ItemLayoutCashInvoice.rpt";
                    }
                }
                salesItemlayoutparams.ReportName = rptName;
                salesItemlayoutparams.RptTitle = "Invoice";

                salesItemlayoutparams.Params = "compname|CompAddress|TIN|LogoPath|tenders";
                salesItemlayoutparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim() + "|" + CommonClass.CompLogoPath + "|" + lTenders;

                CommonClass.ShowReport(salesItemlayoutparams);
            }
            else
            {
                Reports.ReportParams salesItemlayoutparams = new Reports.ReportParams();
                salesItemlayoutparams.PrtOpt = 1;

                SqlConnection con = new SqlConnection(CommonClass.ConStr);
                string printSale = @"SELECT SalesType, SalesNumber , Sales.TransactionDate  , Sales.Comments, Sales.SubTotal , p.Name as ProfileName,
                    c.Street as ShipStreet, c.City as ShipCity, c.State as ShipState,
                   c2.Street as BillStreet, c2.City as BillCity, c2.State as BillState, 
                    sl.Description ,sl.TotalAmount, sl.TaxCode,Sales.TaxTotal,FreightTax,TotalPaid, GrandTotal,TotalDue,
                    ShippingMethod, i.ItemNumber, sl.ShipQty, sl.OrderQty, sl.CostPrice, sl.UnitPrice, sl.DiscountPercent, u.user_fullname
                    FROM Sales
                    INNER JOIN Profile p on Sales.CustomerID = p.ID
                    Left JOIN ShippingMethods  sm On sm.ShippingID = Sales.ShippingMethodID
                    LEFT JOIN Contacts c ON c.Location = Sales.ShippingContactID and c.ProfileID = Sales.CustomerID  
                    LEFT JOIN Contacts c2 ON c2.Location = Sales.BillingContactID and c2.ProfileID = Sales.CustomerID 
                    INNER JOIN SalesLines sl ON sl.SalesID = Sales.SalesID
                    INNER JOIN Items i ON sl.EntityID = i.ID
                    INNER JOIN Users u ON u.user_id = Sales.SalesPersonID 
					WHERE Sales.SalesID = '" + pSalesID + "'";
                SqlCommand cmd = new SqlCommand(printSale, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                salesItemlayoutparams.Rec.Add(dt);
                string tenders = @"SELECT DISTINCT pd.PaymentDetailsID, pd.PaymentMethodID,pt.Amount,pm.PaymentMethod,p.TransactionDate FROM PaymentTender pt 
                        inner join PaymentDetails pd on pt.PaymentID = pd.PaymentID and pt.id = pd.PaymentDetailsID
                        inner join Payment p on pt.PaymentID = p.PaymentID
                        inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                        left join PaymentMethods pm on pd.PaymentMethodID = pm.id
                        where pt.Amount <> 0 and pl.EntityID = " + pSalesID;
                SqlConnection con2 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd2 = new SqlCommand(tenders, con2);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                decimal lchange = 0;
                string lTenders = "";
                //string lcursymbol = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
                if (rdbCash.Checked)
                {
                    for (int x = 0; x < dt2.Rows.Count; x++)
                    {
                        if (dt2.Rows[x]["PaymentMethod"].ToString().ToUpper() == "CASH")
                        {
                            if (Convert.ToDecimal(dt2.Rows[x]["Amount"].ToString()) < 0)
                            {
                                dt2.Rows[x]["PaymentMethod"] = "Change";
                            }
                        }
                        lTenders += dt2.Rows[x]["PaymentMethod"].ToString() + "  " + Convert.ToDecimal(dt2.Rows[x]["Amount"].ToString()).ToString("C2") + "/";
                    }
                    // salesItemlayoutparams.SubRpt = "Tenders";
                    // salesItemlayoutparams.tblSubRpt = dt2;
                }
                salesItemlayoutparams.ReportName = "Receipt76logo.rpt";
                salesItemlayoutparams.RptTitle = "Item Layout 76mm";

                salesItemlayoutparams.Params = "compname|CompAddress|TIN|LogoPath|tenders";
                salesItemlayoutparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim() + "|" + CommonClass.CompLogoPath + "|" + lTenders;

                CommonClass.ShowReport(salesItemlayoutparams);
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadItemLayoutReport(this.lblID.Text);
            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
            {
                if (dgvr.Cells["RedemptionType"].Value != null)
                {
                    if (dgvr.Cells["RedemptionType"].Value.ToString() == "Gift Certificate")
                    {
                        PrintGC();
                    }
                }

            }
        }

        private void txtFreight_ValueChanged(object sender, EventArgs e)
        {
            if (AR_FreightAccountID == "0")
            {
                MessageBox.Show("Account for Freight Collected in Sales is not setup.");
                this.txtFreight.Value = 0;
                return;
            }
            else
            {
                CalcFreight();
            }
        }

        private void LoadFreightTax(string pTaxCode)
        {
            DataRow rTx = CommonClass.getTaxDetails(pTaxCode);
            if (rTx.ItemArray.Length > 0)
            {
                FreightTaxCode = pTaxCode;
                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString());
                FreightTaxRate = ltaxrate;
                FreightTaxAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                this.txtFTaxCode.Text = pTaxCode;
                CalcFreight();
            }
        }

        private void cmb_shippingcontact_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadContacts(Convert.ToInt16(CustomerID), this.cmb_shippingcontact.SelectedIndex + 1);
        }

        private void pbSalesperson_Click(object sender, EventArgs e)
        {
            if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
            {
                VoidValidation DlgVoid = new VoidValidation("Sales Person Override");
                if (DlgVoid.ShowDialog() == DialogResult.OK)
                {
                    SalespersonLookup SalespersonDlg = new SalespersonLookup();
                    if (SalespersonDlg.ShowDialog() == DialogResult.OK)
                    {
                        string[] lSales = SalespersonDlg.GetSalesperson;
                        SalesPersonID = lSales[0];
                        txtSalesperson.Text = lSales[1];
                    }
                }
            }
            else
            {
                SalespersonLookup SalespersonDlg = new SalespersonLookup();
                if (SalespersonDlg.ShowDialog() == DialogResult.OK)
                {
                    string[] lSales = SalespersonDlg.GetSalesperson;
                    SalesPersonID = lSales[0];
                    txtSalesperson.Text = lSales[1];
                }
            }
        }

        private void btnPaymentDetails_Click(object sender, EventArgs e)
        {

            decimal lTotalDue = this.TotalAmount.Value;
            if (salestype_cb.Text == "ORDER" || salestype_cb.Text == "LAY-BY")
            { //This is if getting customer deposit. Paid Today value is passed on as total due if user indicated an amount to pay otherwise pass the Total Amount.
                lTotalDue = (this.PaidToday_txt.Value == 0 ? lTotalDue : this.PaidToday_txt.Value);
            }
            TenderDetails TenderDlg = new TenderDetails(PaymentInfoTb, lTotalDue, CanAdd);
            if (TenderDlg.ShowDialog() == DialogResult.OK)
            {
                PaymentInfoTb = TenderDlg.GetPaymentInfo;
                this.PaidToday_txt.Value = TenderDlg.GetPayedAmount;
                AmountChange = TenderDlg.GetChangemount;
                // this.PaymentMethodTxt.Text = PaymentInfoTb.Rows[0]["PaymentMethod"].ToString();
                this.BalanceDue_txt.Visible = true;
                this.lblBalanceDue.Visible = true;
            }
        }


        //SAVE PAYMENT MADE AGAINTS THE INVOICE
        private int RecordPayment(string pSalesNo, decimal pAmountApplied, decimal pDiscount)
        {
            int lPaymentID = 0;
            PaymentCommon.GeneratePaymentNumber(ref CurSeries, ref gPaymentNo, "SP");

            if (gPaymentNo != "")
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                lPaymentID = CreatePaymentRecord(gPaymentNo);
                PaymentCommon.UpdatePaymentNumber(ref CurSeries, "SP");

                if (PaymentCommon.CreateJournalEntriesSP(lPaymentID, "Payment;" + customerText.Text))
                {
                    //TransactionClass.UpdateProfileBalances(CustomerID, PaidToday_txt.Value * -1);

                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Payment Transaction No. " + gPaymentNo, lPaymentID.ToString());
                }
            }
            return lPaymentID;
        }

        private int CreatePaymentRecord(string pPaymentNo, string pSource = "")
        {
            SqlConnection con = null;
            try
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                con = new SqlConnection(CommonClass.ConStr);

                string strpaymentsql = @"INSERT INTO Payment (
                                            ProfileID,
                                            TotalAmount,
                                            Memo,
                                            PaymentFor,
                                            AccountID,
                                            UserID,
                                            PaymentMethodID,
                                            TransactionDate,
                                            PaymentNumber,
                                            SessionID,
                                            Source
                                            )
                                        VALUES ( 
                                            @ProfileID, 
                                            @TotalAmount, 
                                            @Memo, 
                                            'Sales',
                                            @AccountID,
                                            @UserID,
                                            @PaymentMethodID,
                                            @TransactionDate,
                                            @PaymentNumber, 
                                            @SessionID,
                                            @Source
                                        ); SELECT SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(strpaymentsql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProfileID", CustomerID);
                cmd.Parameters.AddWithValue("@PaymentNumber", pPaymentNo);
                cmd.Parameters.AddWithValue("@TotalAmount", PaidToday_txt.Value);
                cmd.Parameters.AddWithValue("@Memo", "Payment " + customerText.Text);
                cmd.Parameters.AddWithValue("@AccountID", PaymentInfoTb.Rows[0]["RecipientAccountID"].ToString());
                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@TransactionDate", trandate);
                cmd.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[0]["PaymentMethodID"]);
                cmd.Parameters.AddWithValue("@SessionID", CommonClass.SessionID);
                cmd.Parameters.AddWithValue("@Source", pSource);


                con.Open();
                int paymentid = Convert.ToInt32(cmd.ExecuteScalar());

                if (paymentid != 0)
                {//FullPayment
                    Dictionary<string, object> paramPayLines = new Dictionary<string, object>();
                    string Amount = PaidToday_txt.Value.ToString();
                    decimal deciamt = decimal.Parse(Amount, NumberStyles.Currency);
                    string strpaymentlnesql = @"INSERT INTO PaymentLines (
                                                          PaymentID,
                                                          EntityID,
                                                          Amount,
                                                          EntryDate )
                                                      VALUES (
                                                          @PaymentID,
                                                          @EntityID,
                                                          @Amount,
                                                          @EntryDate )";
                    paramPayLines.Add("@PaymentID", paymentid);
                    paramPayLines.Add("@EntityID", NewSalesID);
                    paramPayLines.Add("@Amount", deciamt);
                    paramPayLines.Add("@EntryDate", trandate);
                    CommonClass.runSql(strpaymentlnesql, CommonClass.RunSqlInsertMode.QUERY, paramPayLines);
                    decimal change = TotalAmount.Value - PaidToday_txt.Value;


                    for (int i = 0; i < PaymentInfoTb.Rows.Count; i++)
                    {
                        //Tender details
                        string sqlTender = @"INSERT INTO PaymentTender (
                                                          PaymentID,
                                                          Amount,
                                                          PaymentMethodID)
                                                      VALUES (
                                                          @PaymentID,
                                                          @Amount,
                                                          @PaymentMethodID )";
                        SqlCommand cmdTender = new SqlCommand(sqlTender, con);
                        cmdTender.Parameters.AddWithValue("@PaymentID", paymentid);
                        cmdTender.Parameters.AddWithValue("@Amount", PaymentInfoTb.Rows[i]["AmountPaid"].ToString());
                        cmdTender.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"]);
                        cmdTender.ExecuteNonQuery();
                        //Update GC
                        if (PaymentInfoTb.Rows[i]["PaymentMethodID"].ToString() == "16") ;
                        {
                            string gcsql = @"Update GiftCertificate set IsUsed = '1',usedSalesID = '" + NewSalesID + "' where GCNumber = '" + PaymentInfoTb.Rows[i]["PaymentGCNo"].ToString() + "'";
                            CommonClass.runSql(gcsql);
                        }
                        //Details
                        string sqlDetails = @"INSERT INTO PaymentDetails (
                                                            PaymentID,
                                                            PaymentMethodID,
                                                            PaymentAuthorisationNumber, 
                                                            PaymentCardNumber, 
                                                            PaymentNameOnCard,
                                                            PaymentExpirationDate, 
                                                            CardNotes, 
                                                            PaymentBSB, 
                                                            PaymentBankAccountNumber, 
                                                            PaymentBankAccountName,
                                                            PaymentChequeNumber, 
                                                            BankNotes, 
                                                            PaymentNotes,
                                                            PaymentGCNo,
                                                            PaymentGCNotes)
                                                      VALUES (
                                                        @PaymentID,
                                                        @PaymentMethodID,
                                                        @PaymentAuthorisationNumber, 
                                                        @PaymentCardNumber, 
                                                        @PaymentNameOnCard,
                                                        @PaymentExpirationDate, 
                                                        @PaymentCardNotes, 
                                                        @PaymentBSB, 
                                                        @PaymentBankAccountNumber, 
                                                        @PaymentBankAccountName,
                                                        @PaymentChequeNumber, 
                                                        @PaymentBankNotes, 
                                                        @PaymentNotes,
                                                        @PaymentGCNo,
                                                        @PaymentGCNotes)";
                        SqlCommand cmdDetails = new SqlCommand(sqlDetails, con);
                        cmdDetails.Parameters.AddWithValue("@PaymentID", paymentid);
                        cmdDetails.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"]);
                        cmdDetails.Parameters.AddWithValue("@PaymentAuthorisationNumber", PaymentInfoTb.Rows[i]["PaymentAuthorisationNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentCardNumber", PaymentInfoTb.Rows[i]["PaymentCardNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentNameOnCard", PaymentInfoTb.Rows[i]["PaymentNameOnCard"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentExpirationDate", PaymentInfoTb.Rows[i]["PaymentExpirationDate"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentCardNotes", PaymentInfoTb.Rows[i]["PaymentCardNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBSB", PaymentInfoTb.Rows[i]["PaymentBSB"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankAccountNumber", PaymentInfoTb.Rows[i]["PaymentBankAccountNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankAccountName", PaymentInfoTb.Rows[i]["PaymentBankAccountName"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentChequeNumber", PaymentInfoTb.Rows[i]["PaymentChequeNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankNotes", PaymentInfoTb.Rows[i]["PaymentBankNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentNotes", PaymentInfoTb.Rows[i]["PaymentNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentGCNo", PaymentInfoTb.Rows[i]["PaymentGCNo"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentGCNotes", PaymentInfoTb.Rows[i]["PaymentGCNotes"].ToString());
                        cmdDetails.ExecuteNonQuery();

                    }


                    PaymentCommon.UpdateSalesRecord(trandate, invoicenum, deciamt);
                }
                return paymentid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private int CreatePaymentRecordFromCD(string pPaymentNo)
        {
            SqlConnection con = null;
            try
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                con = new SqlConnection(CommonClass.ConStr);

                string strpaymentsql = @"INSERT INTO Payment (
                                            ProfileID,
                                            TotalAmount,
                                            Memo,
                                            PaymentFor,
                                            AccountID,
                                            UserID,
                                            PaymentMethodID,
                                            TransactionDate,
                                            PaymentNumber
                                            )
                                        VALUES ( 
                                            @ProfileID, 
                                            @TotalAmount, 
                                            @Memo, 
                                            'Sales',
                                            @AccountID,
                                            @UserID,
                                            @PaymentMethodID,
                                            @TransactionDate,
                                            @PaymentNumber
                                        ); SELECT SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(strpaymentsql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProfileID", CustomerID);
                cmd.Parameters.AddWithValue("@PaymentNumber", pPaymentNo);
                cmd.Parameters.AddWithValue("@TotalAmount", PaidToday_txt.Value);
                cmd.Parameters.AddWithValue("@Memo", "Payment " + customerText.Text);
                cmd.Parameters.AddWithValue("@AccountID", AR_CustomerDepositsID);
                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@TransactionDate", trandate);
                cmd.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[0]["PaymentMethodID"].ToString());

                con.Open();
                int paymentid = Convert.ToInt32(cmd.ExecuteScalar());

                if (paymentid != 0)
                {
                    string Amount = PaidToday_txt.Value.ToString();
                    decimal deciamt = decimal.Parse(Amount, NumberStyles.Currency);
                    string strpaymentlnesql = @"INSERT INTO PaymentLines (
                                                          PaymentID,
                                                          EntityID,
                                                          Amount,
                                                          EntryDate )
                                                      VALUES (
                                                          @PaymentID,
                                                          @EntityID,
                                                          @Amount,
                                                          @EntryDate )";
                    SqlCommand pmtlinecmd = new SqlCommand(strpaymentlnesql, con);
                    pmtlinecmd.Parameters.AddWithValue("@PaymentID", paymentid);
                    pmtlinecmd.Parameters.AddWithValue("@EntityID", NewSalesID);
                    pmtlinecmd.Parameters.AddWithValue("@Amount", deciamt);
                    pmtlinecmd.Parameters.AddWithValue("@EntryDate", trandate);

                    pmtlinecmd.ExecuteNonQuery();

                    for (int i = 0; i < PaymentInfoTb.Rows.Count; i++)
                    {
                        //Tender details
                        string sqlTender = @"INSERT INTO PaymentTender (
                                                          PaymentID,
                                                          Amount,
                                                          PaymentMethodID)
                                                      VALUES (
                                                          @PaymentID,
                                                          @Amount,
                                                          @PaymentMethodID )";
                        SqlCommand cmdTender = new SqlCommand(sqlTender, con);
                        cmdTender.Parameters.AddWithValue("@PaymentID", paymentid);
                        cmdTender.Parameters.AddWithValue("@Amount", PaymentInfoTb.Rows[i]["AmountPaid"].ToString());
                        cmdTender.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"]);
                        cmdTender.ExecuteNonQuery();

                        //Details
                        string sqlDetails = @"INSERT INTO PaymentDetails (
                                                            PaymentID,
                                                            PaymentMethodID,
                                                            PaymentAuthorisationNumber, 
                                                            PaymentCardNumber, 
                                                            PaymentNameOnCard,
                                                            PaymentExpirationDate, 
                                                            CardNotes, 
                                                            PaymentBSB, 
                                                            PaymentBankAccountNumber, 
                                                            PaymentBankAccountName,
                                                            PaymentChequeNumber, 
                                                            BankNotes, 
                                                            PaymentNotes,
                                                            PaymentGCNo,
                                                            PaymentGCNotes)
                                                      VALUES (
                                                        @PaymentID,
                                                        @PaymentMethodID,
                                                        @PaymentAuthorisationNumber, 
                                                        @PaymentCardNumber, 
                                                        @PaymentNameOnCard,
                                                        @PaymentExpirationDate, 
                                                        @PaymentCardNotes, 
                                                        @PaymentBSB, 
                                                        @PaymentBankAccountNumber, 
                                                        @PaymentBankAccountName,
                                                        @PaymentChequeNumber, 
                                                        @PaymentBankNotes, 
                                                        @PaymentNotes,
                                                        @PaymentGCNo,
                                                        @PaymentGCNotes)";
                        SqlCommand cmdDetails = new SqlCommand(sqlDetails, con);
                        cmdDetails.Parameters.AddWithValue("@PaymentID", paymentid);
                        cmdDetails.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"]);
                        cmdDetails.Parameters.AddWithValue("@PaymentAuthorisationNumber", PaymentInfoTb.Rows[i]["PaymentAuthorisationNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentCardNumber", PaymentInfoTb.Rows[i]["PaymentCardNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentNameOnCard", PaymentInfoTb.Rows[i]["PaymentNameOnCard"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentExpirationDate", PaymentInfoTb.Rows[i]["PaymentExpirationDate"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentCardNotes", PaymentInfoTb.Rows[i]["PaymentCardNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBSB", PaymentInfoTb.Rows[i]["PaymentBSB"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankAccountNumber", PaymentInfoTb.Rows[i]["PaymentBankAccountNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankAccountName", PaymentInfoTb.Rows[i]["PaymentBankAccountName"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentChequeNumber", PaymentInfoTb.Rows[i]["PaymentChequeNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankNotes", PaymentInfoTb.Rows[i]["PaymentBankNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentNotes", PaymentInfoTb.Rows[i]["PaymentNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentGCNo", PaymentInfoTb.Rows[i]["PaymentGCNo"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentGCNotes", PaymentInfoTb.Rows[i]["PaymentGCNotes"].ToString());
                        cmdDetails.ExecuteNonQuery();
                    }
                    //PaymentCommon.UpdateSalesRecord(trandate, invoicenum, deciamt);
                }

                return paymentid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private int RecordCustomerDeposit(string pSalesNo, decimal pAmountApplied, decimal pDiscount)
        {
            int lPaymentID = 0;
            PaymentCommon.GeneratePaymentNumber(ref CurSeries, ref gPaymentNo, "SP");

            if (gPaymentNo != "")
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                lPaymentID = CreatePaymentRecord(gPaymentNo);
                PaymentCommon.UpdatePaymentNumber(ref CurSeries, "SP");
                if (PaymentCommon.CreateJournalEntriesSP(lPaymentID, "Payment " + gPaymentNo + customerText.Text, CommonClass.DRowPref["SalesDepositGLCode"].ToString()))
                {
                    TransactionClass.UpdateProfileBalances(CustomerID, PaidToday_txt.Value * -1);
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Payment Transaction No. " + gPaymentNo, lPaymentID.ToString());
                }
            }
            return lPaymentID;
        }

        private void TransferCustomerDeposit(string pSalesID, decimal pAmountApplied, decimal pDiscount)
        {

            DateTime time = DateTime.Now;
            DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
            DateTime timeutc = time.ToUniversalTime();

            string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");
            string lPaymentNo = PaymentCommon.GetCDPaymentNumber(pSalesID);
            if (PaymentCommon.TransferCDJournal(pSalesID, lPaymentNo, "Transfer from Customer Deposit ;" + customerText.Text, trandate, pAmountApplied))
            {
                TransactionClass.UpdateProfileBalances(CustomerID, pAmountApplied);
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Transferred Customer Deposit " + lPaymentNo, lPaymentNo);
            }

        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            if (rdbCash.Checked && salestype_cb.Text == "INVOICE")
            { //Show Tender Details dialog for Cash invoices.
                TenderDetails TenderDlg = new TenderDetails(PaymentInfoTb, this.TotalAmount.Value, false);
                TenderDlg.ShowDialog();
            }
            else
            {
                TransactionsLookup TLookup = new TransactionsLookup("Invoice", XsalesID);
                TLookup.ShowDialog();
            }

        }

        private void btnSaveRecurring_Click(object sender, EventArgs e)
        {
            string lTranType;
            switch (salestype_cb.Text)
            {
                case "QUOTE":
                    lTranType = "SQ";
                    break;
                case "ORDER":
                    lTranType = "SO";
                    break;
                case "INVOICE":
                    lTranType = "SI";
                    break;
                default:
                    lTranType = "";
                    break;
            }
            RecurringSetup lRecurringForm = new RecurringSetup(this, customerText.Text, lTranType);
            lRecurringForm.MdiParent = this.MdiParent;
            lRecurringForm.Show();
        }

        private void btnUseRecurring_Click(object sender, EventArgs e)
        {
            UseRecurring lUseRecurringForm = new UseRecurring(CommonClass.InvocationSource.SALES);
            lUseRecurringForm.MdiParent = this.MdiParent;
            lUseRecurringForm.Show();
        }

        public DataTable CheckOnHandQtyNewSale()
        {
            DataTable lTb = new DataTable();
            lTb.Columns.Add("Part Number", typeof(string));
            lTb.Columns.Add("Error", typeof(string));
            DataRow rw;
            DataTable lResTb;
            string lItemID = "";
            decimal lShipQty = 0;
            string lPartNumber = "";
            for (int i = 0; i < dgEnterSales.Rows.Count; i++)
            {
                if (dgEnterSales.Rows[i].Cells["PartNumber"].Value != null)
                {
                    if (dgEnterSales.Rows[i].Cells["PartNumber"].Value.ToString() != "")
                    {
                        lShipQty = Convert.ToDecimal(dgEnterSales.Rows[i].Cells["Ship"].Value.ToString());
                        lItemID = dgEnterSales.Rows[i].Cells["ItemID"].Value.ToString();
                        lPartNumber = dgEnterSales.Rows[i].Cells["PartNumber"].Value.ToString();

                        lResTb = GetNewEndingQty(lItemID);
                        if (lResTb.Rows.Count > 0)
                        {
                            if ((bool)lResTb.Rows[0]["IsCounted"])
                            {
                                decimal lOnHandQty = Convert.ToDecimal(lResTb.Rows[0]["OnHandQty"].ToString());
                                decimal lNewQty = lOnHandQty - lShipQty;
                                if (lNewQty < 0)
                                {
                                    rw = lTb.NewRow();
                                    rw["Part Number"] = lPartNumber;
                                    rw["Error"] = "Not Enough Quantity";
                                    lTb.Rows.Add(rw);
                                }
                            }

                        }
                        else
                        {
                            rw = lTb.NewRow();
                            rw["Part Number"] = lPartNumber;
                            rw["Error"] = "Missing Item";
                            lTb.Rows.Add(rw);
                        }
                    }
                }
            }
            return lTb;
        } //END

        private DataTable GetNewEndingQty(string pItemID, string pLocID = "1")
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT OnHandQty, i.IsCounted from Items i inner join ItemsQty q on i.ID = q.ItemID where LocationID = " + pLocID + " and q.ItemID = " + pItemID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        private void UpdateNotify()
        {
            string lTranType;
            switch (salestype_cb.Text)
            {
                case "QUOTE":
                    lTranType = "SQ";
                    break;
                case "ORDER":
                    lTranType = "SO";
                    break;
                case "INVOICE":
                    lTranType = "SI";
                    break;
                default:
                    lTranType = "";
                    break;
            }
            string frequency = "";
            DateTime sdate = DateTime.Now;
            DateTime edate = DateTime.MaxValue;
            SqlConnection con_ua = null;
            SqlConnection con = null;
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);

                string selectSql_ua = "SELECT * FROM Recurring WHERE EntityID = " + XsalesID + " AND TranType = '" + lTranType + "'";
                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            frequency = (reader["Frequency"].ToString());
                        }
                        switch (frequency)
                        {
                            case "Daily":
                                sdate = sdate.AddDays(1);
                                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                                break;
                            case "Weekly":
                                sdate = sdate.AddDays(7);
                                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                                break;
                            case "Monthly":
                                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                                break;
                            case "Quarterly":
                                sdate = sdate.AddMonths(3);
                                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                                break;
                            case "Every 6 Months":
                                sdate = sdate.AddMonths(6);
                                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                                break;
                        }

                        string sql = @"UPDATE Recurring set NotifyDate = @newNotifyDate WHERE EntityID = " + XsalesID + " AND TranType = '" + lTranType + "'";

                        con = new SqlConnection(CommonClass.ConStr);
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@newNotifyDate", sdate);
                        con.Open();
                        cmd.ExecuteScalar();
                    }
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
                con_ua.Close();
            }
        }

        public DataTable CheckOnHandQtyUpdateSale(string pSalesID)
        {
            DataTable lTb = new DataTable();
            lTb.Columns.Add("Part Number", typeof(string));
            lTb.Columns.Add("Error", typeof(string));
            DataRow rw;
            DataTable lResTb;
            string lItemID = "";
            decimal lShipQty = 0;
            string lPartNumber = "";
            for (int i = 0; i < dgEnterSales.Rows.Count; i++)
            {
                if (dgEnterSales.Rows[i].Cells["PartNumber"].Value != null)
                {
                    if (dgEnterSales.Rows[i].Cells["PartNumber"].Value.ToString() != "")
                    {
                        lShipQty = Convert.ToDecimal(dgEnterSales.Rows[i].Cells["Ship"].Value.ToString());
                        lItemID = dgEnterSales.Rows[i].Cells["ItemID"].Value.ToString();
                        lPartNumber = dgEnterSales.Rows[i].Cells["PartNumber"].Value.ToString();

                        lResTb = GetNewEndingQty(lItemID);
                        if (lResTb.Rows.Count > 0)
                        {
                            if ((bool)lResTb.Rows[0]["IsCounted"])
                            {
                                decimal lOnHandQty = Convert.ToDecimal(lResTb.Rows[0]["OnHandQty"].ToString());
                                decimal lPrevShipQty = GetPrevShipQty(pSalesID, lItemID);
                                decimal lNewQty = lOnHandQty + lPrevShipQty - lShipQty;
                                if (lNewQty < 0)
                                {
                                    rw = lTb.NewRow();
                                    rw["Part Number"] = lPartNumber;
                                    rw["Error"] = "Not Enough Quantity";
                                    lTb.Rows.Add(rw);
                                }
                            }
                        }
                        else
                        {
                            rw = lTb.NewRow();
                            rw["Part Number"] = lPartNumber;
                            rw["Error"] = "Missing Item";
                            lTb.Rows.Add(rw);
                        }
                    }
                }
            }
            return lTb;
        } //END

        public decimal GetPrevShipQty(string pSalesID, string pItemID, string pLocID = "1")
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT * from SalesLines where SalesID = " + pSalesID + " and EntityID = " + pItemID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                decimal lShipQty = 0;
                if (RTb.Rows.Count > 0)
                {
                    lShipQty = Convert.ToDecimal(RTb.Rows[0]["ShipQty"].ToString());
                }
                return lShipQty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        private void btnRedeem_Click(object sender, EventArgs e)
        {
            if (float.Parse(txtCustPoints.Text) <= 0)
            {
                MessageBox.Show("No customer points available");
                return;
            }

            if (CommonClass.PointRedemption == null
            || CommonClass.PointRedemption.IsDisposed)
            {
                CommonClass.PointRedemption = new PointsRedemption(CommonClass.RedemptionType.GIFTCERTIFICATE, this, null, CustomerID, customerText.Text);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PointRedemption.MdiParent = this.MdiParent;
            CommonClass.PointRedemption.Show();
            CommonClass.PointRedemption.Focus();
            if (CommonClass.PointRedemption.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PointRedemption.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void LoadPoints()
        {
            string pointssql = @"SELECT SUM(PointsAccumulated) AS TotalPoints 
                                 FROM AccumulatedPoints 
                                 WHERE CustomerID = @CustomerID";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CustomerID", CustomerID);
            CommonClass.runSql(ref dt, pointssql, param);
            if (dt.Rows.Count == 1 && dt.Rows[0]["TotalPoints"].ToString() != "")
            {
                txtCustPoints.Text = dt.Rows[0]["TotalPoints"].ToString();
            }
        }

        void LoadDefaultCustomer()
        {
            if (!CommonClass.MandatoryCustomer)
            {
                DataTable dt = new DataTable();
                string sqldefcust = "Select p.* FROM Profile p INNER JOIN Contacts c ON p.ID = c.ProfileID  WHERE ID = " + CommonClass.DefaultCustomerID;
                CommonClass.runSql(ref dt, sqldefcust);
                if (dt.Rows.Count > 0)
                {
                    DataRow c = dt.Rows[0];
                    CustomerID = c["ID"].ToString();
                    this.customerText.Text = c["Name"].ToString();
                    ShippingmethodText.Text = c["ShippingMethodID"].ToString();
                    TermRefID = c["TermsOfPayment"].ToString();
                    ProfileTax = c["TaxCode"].ToString();
                    MethodPaymentID = c["MethodOfPaymentID"].ToString();
                    CustomerBalance = (c["CurrentBalance"].ToString() == "" ? 0 : float.Parse(c["CurrentBalance"].ToString(), NumberStyles.Currency));
                    CustomerCreditLimit = (c["CreditLimit"].ToString() == "" ? 0 : float.Parse(c["CreditLimit"].ToString(), NumberStyles.Currency));
                    TermsText.Visible = true;
                    MemoText.Text = "Sale; " + c["Name"].ToString();
                    ShippingMethodID = (c["ShippingMethodID"].ToString() == "" ? "0" : c["ShippingMethodID"].ToString());
                    InvoiceNumTxt.Visible = true;
                    //cmb_shippingcontact.SelectedIndex = Convert.ToInt16(c["LocationID"].ToString()) - 1;

                    string sql = "SELECT ContactID, TypeOfContact FROM Contacts WHERE ProfileID = @ProfileID";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@ProfileID", CustomerID);
                    DataTable dt2 = new DataTable();
                    CommonClass.runSql(ref dt2, sql, param);
                    List<KeyValuePair<string, string>> mylist = new List<KeyValuePair<string, string>>();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt2.Rows)
                        {
                            mylist.Add(new KeyValuePair<string, string>(dr["ContactID"].ToString(), dr["TypeOfContact"].ToString()));
                        }
                        cmb_shippingcontact.DataSource = new BindingSource(mylist, null);
                        cmb_shippingcontact.DisplayMember = "Value";
                        cmb_shippingcontact.ValueMember = "Key";
                    }
                    LoadContacts(Convert.ToInt32(CustomerID), 1);

                    //LoadContacts(Convert.ToInt32(CustomerID), cmb_shippingcontact.SelectedIndex + 1);
                    LoadDefaultTerms(CustomerID);
                    LoadFreightTax(ProfileTax);
                    LoadPaymentMethod();
                    LoadPoints();
                    FormCheck();
                    ApplyVoidAccess();
                }
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            string barsql = "SELECT * from Barcodes b Inner join Items i ON i.ID = b.ItemID";
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, barsql);
            if (e.KeyCode == Keys.Enter)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];

                    if (txtBarcode.Text == CommonClass.base64Decode(dr["BarcodeData"].ToString()))
                    {
                        //txtPartNum.Text = dr["PartNumber"].ToString();
                        //txtPartNum.KeyDown += txtItemName_KeyDown;
                        SetPartName(dr["PartNumber"].ToString());
                    }
                }
            }
        }

        public void SetPartName(string ptnum)
        {
            string WhereCon = "PartNumber";
            PopulateDataGridView();
            DataGridViewRow dgcur = dgEnterSales.CurrentRow;
            //txtQTY.Visible = true;
            //lbItemQty.Visible = true;
            // if (txtQTY.Text != "")
            //{
            // dgcur.Cells[1].Value = txtQTY.Text;
            int cur = dgcur.Index;
            if (customerText.Text != "")
            {
                if (ShowItemLookup(ptnum, WhereCon))
                {
                    itemcalc(cur);
                }

            }
            dgEnterSales.ClearSelection();
            //if (rowin < dgEnterSales.RowCount)
            //{
            //    dgEnterSales.Rows[rowin].Cells["Ship"].Selected = false;
            //    dgEnterSales.Rows[++rowin].Cells["Ship"].Selected = true;
            //}
            //txtQTY.Text = "";
            //txtBarCode.Text = "";
            //txtBarCode.Select();
            // CalcOutOfBalance();
        }

        private void btnAddShipAddress_Click(object sender, EventArgs e)
        {
            //PaymentInfoTb.Rows[0]["AmountPaid"] = PaidToday_txt.Value;
            ContactsFillUpForm ContactsDlg = new ContactsFillUpForm(ContactInfoTb, CustomerID);//PaymentInfoTb, this.TotalAmount.Value, CanEdit);
            if (ContactsDlg.ShowDialog() == DialogResult.OK)
            {
                ContactInfoTb = ContactsDlg.GetContactInfo;
                string sql = "SELECT ContactID, TypeOfContact FROM Contacts WHERE ProfileID = @ProfileID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProfileID", CustomerID);
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, sql, param);
                List<KeyValuePair<string, string>> mylist = new List<KeyValuePair<string, string>>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        mylist.Add(new KeyValuePair<string, string>(dr["ContactID"].ToString(), dr["TypeOfContact"].ToString()));
                    }
                }
                mylist.Add(new KeyValuePair<string, string>("-1", ContactInfoTb.Rows[0]["TypeOfContact"].ToString()));
                cmb_shippingcontact.DataSource = new BindingSource(mylist, null);
                cmb_shippingcontact.DisplayMember = "Value";
                cmb_shippingcontact.ValueMember = "Key";

                cmb_shippingcontact.SelectedIndex = cmb_shippingcontact.Items.Count - 1;
                //shipAddressID = cmb_shippingcontact.ValueMember;
                PayeeInfo.Text = ContactsDlg.GetPayeeInfo;
            }
        }

        private void IsAutoBuild(int itemID, int rowindex)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ia.PartItemID,
                                    ia.PartItemQty, 
                                    i.PartNumber,
                                    i.ItemName,
                                    i.SalesTaxCode,
                                    t.TaxCollectedAccountID, 
                                    t.TaxPercentageRate AS RateTaxSales 
                            FROM ItemsAutoBuild ia 
                            INNER JOIN Items i ON i.ID = ia.PartItemID 
                            LEFT JOIN TaxCodes t ON i.SalesTaxCode = t.taxcode
                            WHERE ia.ItemID = " + itemID;
            CommonClass.runSql(ref dt, sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dgRow = dt.Rows[i];
                    rowindex++;
                    dgEnterSales.Rows[rowindex].Cells["ItemID"].Value = dgRow["PartItemID"].ToString();
                    dgEnterSales.Rows[rowindex].Cells["Ship"].Value = float.Parse(dgRow["PartItemQty"].ToString()) * float.Parse(dgEnterSales.CurrentRow.Cells["Ship"].Value.ToString());
                    dgEnterSales.Rows[rowindex].Cells["PartNumber"].Value = dgRow["PartNumber"].ToString();
                    dgEnterSales.Rows[rowindex].Cells["Description"].Value = dgRow["ItemName"].ToString();
                    dgEnterSales.Rows[rowindex].Cells["Amount"].Value = 0;

                    string taxcode = dgRow["SalesTaxCode"].ToString();
                    if (taxcode != "")
                    {
                        dgEnterSales.Rows[rowindex].Cells["TaxRate"].Value = dgRow["RateTaxSales"];
                        dgEnterSales.Rows[rowindex].Cells["TaxCode"].Value = dgRow["SalesTaxCode"];
                        dgEnterSales.Rows[rowindex].Cells["TaxCollectedAccountID"].Value = dgRow["TaxCollectedAccountID"];
                    }
                    else
                    {
                        dgEnterSales.Rows[rowindex].Cells["TaxRate"].Value = 0;
                        dgEnterSales.Rows[rowindex].Cells["TaxCode"].Value = "N-T";
                        dgEnterSales.Rows[rowindex].Cells["TaxCollectedAccountID"].Value = 0;
                    }

                    dgEnterSales.Rows[rowindex].Cells["Price"].Value = 0;
                    dgEnterSales.Rows[rowindex].Cells["ActualUnitPrice"].Value = 0;
                    dgEnterSales.Rows[rowindex].Cells["Amount"].Value = 0;

                    Recalcline(8, rowindex);

                    this.dgEnterSales.Rows.Add();
                }
            }
        }

        private void rdbCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCash.Checked)
            {
                btnPaymentDetails.Visible = true;
                if (SrcOfInvoke == CommonClass.InvocationSource.CHANGETO)
                {
                    if (PaidToday_txt.Value != 0)
                    {
                        btnPaymentDetails.Visible = false;
                    }
                }

            }
            else
            {
                btnPaymentDetails.Visible = false;
            }
        }

        private void LoadPaymentInfoTb(string pSalesID)
        {
            SqlConnection con = null;
            DataTable ltb = null;
            try
            {
                string sql = @"SELECT  pd.*,pt.Amount, p.AccountID FROM PaymentTender pt 
                        inner join PaymentDetails pd on pt.PaymentID = pd.PaymentID and pt.id = pd.PaymentDetailsID
                        inner join Payment p on pt.PaymentID = p.PaymentID
                        inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                        where pl.EntityID = " + pSalesID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ltb = new DataTable();
                da.Fill(ltb);
                if (ltb.Rows.Count > 0)
                {
                    PaymentInfoTb.Rows.Clear();
                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {

                        DataRow rw = PaymentInfoTb.NewRow();
                        rw["RecipientAccountID"] = ltb.Rows[i]["AccountID"].ToString();
                        rw["AmountPaid"] = ltb.Rows[i]["Amount"].ToString();
                        rw["PaymentMethodID"] = ltb.Rows[i]["PaymentMethodID"].ToString();
                        rw["PaymentMethod"] = ltb.Rows[i]["PaymentMethod"].ToString();
                        rw["PaymentAuthorisationNumber"] = ltb.Rows[i]["PaymentAuthorisationNumber"].ToString();
                        rw["PaymentCardNumber"] = ltb.Rows[i]["PaymentCardNumber"].ToString();
                        rw["PaymentNameOnCard"] = ltb.Rows[i]["PaymentNameOnCard"].ToString();
                        rw["PaymentExpirationDate"] = ltb.Rows[i]["PaymentExpirationDate"].ToString();
                        rw["PaymentCardNotes"] = ltb.Rows[i]["CardNotes"].ToString();
                        rw["PaymentBSB"] = ltb.Rows[i]["PaymentBSB"].ToString();
                        rw["PaymentBankAccountNumber"] = ltb.Rows[i]["PaymentBankAccountNumber"].ToString();
                        rw["PaymentBankAccountName"] = ltb.Rows[i]["PaymentBankAccountName"].ToString();
                        rw["PaymentChequeNumber"] = ltb.Rows[i]["PaymentChequeNumber"].ToString();
                        rw["PaymentBankNotes"] = ltb.Rows[i]["BankNotes"].ToString();
                        rw["PaymentNotes"] = ltb.Rows[i]["PaymentNotes"].ToString();
                        rw["PaymentGCNo"] = ltb.Rows[i]["PaymentGCNo"].ToString();
                        rw["PaymentGCNotes"] = ltb.Rows[i]["PaymentGCNotes"].ToString();
                        PaymentInfoTb.Rows.Add(rw);
                    }
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
        private void btnLoyaltyMem_Click(object sender, EventArgs e)
        {

            memberID = CustomerID;
            member = Interaction.InputBox("Please Enter Member Loyalty Number", "Member Loyalty", "");
            string loyaltysql = "SELECT * FROM LoyaltyMember where Number = '" + member + "'";
            DataTable loyaldt = new DataTable();
            CommonClass.runSql(ref loyaldt, loyaltysql);
            if (loyaldt.Rows.Count > 0)
            {

                ismember = true;
                memberID = loyaldt.Rows[0]["ID"].ToString();
                this.lblLoyalty.Text = member;
            }
            else
            {
                MessageBox.Show("Member Number does not exists.");
                this.lblLoyalty.Text = "";
            }
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voiding a " + salestype_cb.Text + " will delete the record. Are you sure to delete a sales record?", "VOIDING SALE", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                VoidSale(this.lblID.Text, this.InvoiceNumTxt.Text);
            }
        }

        private void VoidSale(string pSalesID, string pSalesNo)
        {
            string sql = "";
            //DELETE from  Sales table
            sql = "DELETE from Sales where SalesID = " + pSalesID;
            int a = CommonClass.runSql(sql);
            //Delete Sales Line
            int b = DeleteSalesLines(pSalesID);
            //Delete Journal
            sql = "DELETE  from Journal where TransactionNumber = '" + pSalesNo + "'";
            int c = CommonClass.runSql(sql);

            //Delete Item Transaction
            sql = "DELETE from ItemTransaction where TranType = 'SI' and SourceTranID = " + pSalesID;
            int d = CommonClass.runSql(sql);
            if (salestype_cb.Text == "INVOICE")
            {
                TransactionClass.UpdateProfileBalances(CustomerID, BalanceDue_txt.Value * -1);
            }
            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Voided Sales with Sales Number " + pSalesNo + " and Sales ID " + pSalesID, pSalesID);
            MessageBox.Show("Sales Record " + pSalesNo + " has been voided.");
            this.Close();

        }

        private void ApplyVoidAccess()
        {
            if (XsalesID != "")
            {
                if (lblPaidToday.Text == "Paid To Date:")
                {
                    this.btnVoid.Enabled = false;
                }
                else
                {
                    this.btnVoid.Enabled = true;
                }

            }
            else //New Transaction Void should not be visible
            {
                this.btnVoid.Enabled = false;

            }
        }
    }//END
}//end


