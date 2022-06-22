using AbleRetailPOS.Inventory;
using AbleRetailPOS.References;
using AbleRetailPOS.Setup;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AbleRetailPOS.Sales
{
    public partial class QuickSales : Form
    {

        private string salestype = "INVOICE";
        private string invoiceStatus = "";
        public string invoicenum = "";
        private DataRow CustomerRow;
        private string ChangePaymentMethodID = "0";
        bool IsLoading = true;
        private string[] Tax;
        private string CustomerID;
        private string ShippingMethodID;
        private string[] Jobs;
        private string XsalesID = "";
        private int xMethodID = 0;
        private string ProfileTax = "";
        private string MethodPaymentID = "";
        private float lTaxEx = 0;
        private float lTaxInc = 0;
        private float lTaxRate = 0;
        private float lAmount = 0;
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
        private string memberID = "";
        private string SalesPersonID = CommonClass.UserID;
        bool enter = false;
        DataTable table = new DataTable();
        public DataTable dtv = new DataTable();
        int rowin;
        DataTable FreeTable = null;


        //Shipping Address
        private DataTable ContactInfoTb = null;
        private string LocationID = "";


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
        bool ismember = false;
        string member = "Not a Member";

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

        private string thisFormCode = "";
        private bool CanView = false;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool changeqty = false;

        //For Customer's Current Balance & Credit Limit
        private float CustomerBalance = 0;
        private float CustomerCreditLimit = 0;

        //for Convertion os Sales
        private string FromSalesType = "";
        private string ToSalesType = "";

        private DataRow ShipID;
        private string ShipVia;

        //For Payment Info
        private DataTable PaymentInfoTb = null;
        private string gPaymentNo = "";
        private Decimal PrevPaid = 0;
        private int NewSalesID = 0;

        //Payment Method
        int countTxt = 1;
        int countBtn = 1;
        int countPayment = 0;
        decimal totalAmountApplied = new decimal();
        System.Windows.Forms.Button txtButton = new System.Windows.Forms.Button();
        //IsIngredient
        private DataTable IngredientTb = null;

        private float FreeProductAmt;
        private bool isFreeProduct;

        //For Void
        string supervisor = "Not a Salesperson";
        string prevAmt;
        string password = "";
        string username = "";
        string formcode = "";

        //for GC payment row
        int GCRowID = -1;
        

        public CommonClass.InvocationSource SourceOfInvoke
        {
            get { return SrcOfInvoke; }
            set { SrcOfInvoke = value; }
        }
        public DataGridView GetSalesLinesGridView
        {
            get { return dgEnterSales; }
        }
        public QuickSales(CommonClass.InvocationSource pSrcInvoke, string pSalesID = "", string pSalesType = "")
        {
            SrcOfInvoke = pSrcInvoke;
            XsalesID = pSalesID;
            InitializeComponent();
            salestype = pSalesType;
            ToSalesType = pSalesType;
            this.Text = "QuickSales";
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
            InvoiceNumTxt.Visible = false;
            if (customerText.Text != "")
            {
                PaidToday_txt.Visible = true;
                BalanceDue_txt.Visible = true;
                InvoiceNumTxt.Visible = true;
            }
            if (CommonClass.isSalesperson == true)
            {
                btnremove.Text = "Void Line";
            }
            record_btn.Enabled = CanAdd;

            GetFormCode("Invoice");
            if (formcode == "Invoice")
            {
                record_btn.Text = "Save " + formcode;
                ApplyFieldAccess(CommonClass.UserID);
            }
            dgEnable();
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

        private void EnterSalesTouchScreenLayout_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadPaymentMethods();
            PopulateDataGridView();
            foreach (DataGridViewColumn column in dgEnterSales.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Only enable the print button if the record is existing, not a new record to be save
            if (SrcOfInvoke != CommonClass.InvocationSource.REGISTER)
                btnPrint.Visible = false;

            InitPaymentInfoTb();
            InitIngredientInfoTb();
            InitContactInfoTb();
            InitVoidTable();
            InitFreeTable();

            FormCheck();

            PopulateDataGridView();
            record_btn.Enabled = CanAdd;
            lblSalesID.Visible = false;
            LoadDefaultCustomer();
            SalesPersonID = CommonClass.UserID;
            txtSalesperson.Text = CommonClass.UserName;

            InvoiceNumTxt.Visible = false;
            if (customerText.Text != "")
            {
                InvoiceNumTxt.Visible = true;
            }

            DataTable dt = new DataTable();
            string selectSql = "SELECT * FROM Preference";
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AR_AccountID = (dt.Rows[i]["TradeDebtorGLCode"].ToString());
                    //AR_ChequeID = (dt.Rows[i]["SalesDepositGLCode"].ToString());
                    AR_FreightAccountID = (dt.Rows[i]["SalesFreightGLCode"].ToString());
                    AR_CustomerDepositsID = (dt.Rows[i]["SalesDepositGLCode"].ToString());


                    if (XsalesID == "")
                    {
                        if (salestype == "INVOICE")
                        {
                            PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_AccountID;
                        }
                        else
                        {
                            PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_CustomerDepositsID;
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
                MessageBox.Show("Setup GL code in Preference first.");
            }
            if (SrcOfInvoke != CommonClass.InvocationSource.USERECURRING && SrcOfInvoke != CommonClass.InvocationSource.REGISTER && SrcOfInvoke != CommonClass.InvocationSource.REMINDER)
            {
                string selectRecur = @"SELECT EntityID FROM Recurring WHERE TranType IN ('SQ', 'SO', 'SI')";
                DataTable dtrecur = new DataTable();
                int noofrecords = CommonClass.runSql(ref dtrecur, selectRecur);
            }
            txtPartNum.Focus();
            IsLoading = false;
        }

        void PopulateDataGridView()
        {
            IsLoading = true;
            for (int i = 0; i <= 1; i++)
            {
                //REMOVED dgEnterSales.Rows.Add();
            }
            IsLoading = false;
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

        private void GenerateInvoiceNum()
        {
            DataTable dt = new DataTable();
            string selectSql = @"SELECT SalesOrderSeries, 
                                               SalesOrderPrefix, 
                                               SalesQuoteSeries, 
                                               SalesQuotePrefix,
                                               SalesInvoiceSeries,
                                               SalesInvoicePrefix 
                                        FROM TransactionSeries";

            CommonClass.runSql(ref dt, selectSql);
            string lSeries = "";
            int lCnt = 0;
            int lNewSeries = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (salestype == "ORDER")
                    {
                        lSeries = (dt.Rows[i]["SalesOrderSeries"].ToString());
                        lCnt = lSeries.Length;
                        lSeries = lSeries.TrimStart('0');
                        lSeries = (lSeries == "" ? "0" : lSeries);
                        lNewSeries = Convert.ToInt16(lSeries) + 1;
                        CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                        this.InvoiceNumTxt.Text = invoicenum = (dt.Rows[i]["SalesOrderPrefix"].ToString()).Trim(' ') + CurSeries;
                    }
                    else if (salestype == "QUOTE")
                    {
                        lSeries = (dt.Rows[i]["SalesQuoteSeries"].ToString());
                        lCnt = lSeries.Length;
                        lSeries = lSeries.TrimStart('0');
                        lSeries = (lSeries == "" ? "0" : lSeries);
                        lNewSeries = Convert.ToInt16(lSeries) + 1;
                        CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                        this.InvoiceNumTxt.Text = invoicenum = (dt.Rows[i]["SalesQuotePrefix"].ToString()).Trim(' ') + CurSeries;
                    }
                    else
                    {
                        lSeries = (dt.Rows[i]["SalesInvoiceSeries"].ToString());
                        lCnt = lSeries.Length;
                        lSeries = lSeries.TrimStart('0');
                        lSeries = (lSeries == "" ? "0" : lSeries);
                        lNewSeries = Convert.ToInt16(lSeries) + 1;
                        CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                        this.InvoiceNumTxt.Text = invoicenum = (dt.Rows[i]["SalesInvoicePrefix"].ToString()).Trim(' ') + CurSeries;
                    }
                }
            }
            else
            {
                MessageBox.Show("Transaction Series Numbers not setup properly.");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salestype_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormCheck();
            if (XsalesID == "")
            {
                if (salestype == "INVOICE")
                {
                    PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_AccountID;
                }
                else
                {
                    PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_CustomerDepositsID;
                }
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

                ShippingmethodText.Text = lProfile[4];
                ShippingMethodID = (lProfile[11] == "" ? "0" : lProfile[11]);
                InvoiceNumTxt.Visible = true;
                cmb_shippingcontact.SelectedIndex = Convert.ToInt16(lProfile[14]) - 1;
                LoadContacts(Convert.ToInt32(CustomerID), this.cmb_shippingcontact.SelectedIndex + 1);
                LoadFreightTax(ProfileTax);
                //LoadPaymentMethod();
                LoadPoints();
                txtSalesperson.Text = CommonClass.UserName;
                FormCheck();
            }
        }
        private void InitVoidTable()
        {
            dtv.Columns.Add("UserName", typeof(string));
            dtv.Columns.Add("AuditAction", typeof(string));
        }
        public decimal qtyvalidation(decimal itemqty)
        
        {
            if(itemqty < 0)
            {
               if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                {
                    VoidValidation vv = new VoidValidation("Return Item Override");
                    if (vv.ShowDialog() == DialogResult.OK)
                    {
                        DataRow rw = dtv.NewRow();

                        rw["UserName"] = vv.GetUsername;
                        rw["AuditAction"] = "Quantity over ride to " + itemqty + " by " + vv.GetUsername;
                        dtv.Rows.Add(rw);
                    }
                    else
                    {
                        itemqty = 1;
                    }
                }
                
            }
            return itemqty;
        }
        public void saveValidationLog(int salesID)
        {
            foreach(DataRow drv in dtv.Rows)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, drv["AuditAction"].ToString(), salesID.ToString());
            }
        }
        public void ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum, CustomerID, whereCon);
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)            {
                ItemRows = Items.GetSelectedItem;
                // MessageBox.Show(txtQTY.Text);
                if (!CheckExistItemInGrid(ItemRows.Cells[1].Value.ToString()))
                {
                    dgEnterSales.Rows.Add();
                    int lLastIndex = dgEnterSales.Rows.Count - 1;
                    dgEnterSales.Rows[lLastIndex].Selected = true;
                    DataGridViewRow dgvRows = dgEnterSales.Rows[lLastIndex];
                  
                    dgvRows.Cells["Ship"].Value = qtyvalidation(decimal.Parse(txtQTY.Text));
                    dgvRows.Cells["ItemID"].Value = ItemRows.Cells[0].Value.ToString();
                    dgvRows.Cells["PartNumber"].Value = ItemRows.Cells[1].Value;
                    dgvRows.Cells["Description"].Value = ItemRows.Cells[3].Value.ToString();
                    dgvRows.Cells["Price"].Value = ItemRows.Cells[5].Value.ToString();
                    ItemOnHand = Convert.ToInt32(ItemRows.Cells[4].Value.ToString());
                    dgvRows.Cells["TaxCode"].Value = ItemRows.Cells["SalesTaxCode"].Value.ToString();
                    dgvRows.Cells["CostPrice"].Value = ItemRows.Cells["AverageCostEx"].Value.ToString();

                    dgvRows.Cells["Description"].ReadOnly = false;
                    dgvRows.Cells["Supplier"].Value = int.Parse(ItemRows.Cells["SupplierID"].Value.ToString());
                    dgvRows.Cells["CategoryID"].Value = int.Parse(ItemRows.Cells["CategoryID"].Value.ToString());
                    dgvRows.Cells["Brand"].Value = ItemRows.Cells["BrandName"].Value.ToString();
                    dgvRows.Cells["AutoBuild"].Value = ItemRows.Cells["IsAutoBuild"].Value.ToString();
                    dgvRows.Cells["BundleType"].Value = ItemRows.Cells["BundleType"].Value.ToString();
                    float ltaxrate = 0;
                    DataRow rTx = CommonClass.getTaxDetails(ItemRows.Cells["SalesTaxCode"].Value.ToString());
                    if (rTx.ItemArray.Length > 0)
                    {
                        ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                        string lTaxCollectedAccountID = "";
                        lTaxCollectedAccountID = (rTx["TaxCollectedAccountID"] == null ? "" : rTx["TaxCollectedAccountID"].ToString());
                        dgvRows.Cells["TaxCollectedAccountID"].Value = lTaxCollectedAccountID;
                        dgvRows.Cells["TaxRate"].Value = ltaxrate;
                    }
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
                        dgvRows.Cells["Price"].Value = lSellingPriceInc;
                        dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceInc;
                        
                    }
                    else
                    {
                        dgvRows.Cells["Price"].Value = lSellingPriceEx;
                        dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceEx;
                        
                    }
                    ContractPriceAmount(CustomerID, ItemRows.Cells[0].Value.ToString(), lLastIndex);
                    float lAmount = float.Parse(dgvRows.Cells["Price"].Value.ToString()) * float.Parse(dgvRows.Cells["Ship"].Value.ToString());
                    dgvRows.Cells["Amount"].Value = lAmount;

                    if (dgvRows.Cells["AutoBuild"].Value != null && bool.Parse(dgvRows.Cells["AutoBuild"].Value.ToString()))
                    {
                        int lTempIndex = dgEnterSales.CurrentRow.Index;
                        if (dgvRows.Cells["BundleType"].Value.ToString() == "Dynamic")
                            IsAutoBuild(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), lLastIndex);
                        
                    }
                    else
                    {
                        VerifyItemQty(lLastIndex, (XsalesID == "" ? "0" : XsalesID));
                    }

                }
            }
        }
        private bool ContractPriceAmount(string cusID, string itemid, int pRowIndex)
        {
            bool lRet = false;
            DateTime dtime = DateTime.Now;
            //DateTime dtpfromutc = datenow.Value.ToUniversalTime();
            DateTime timeutc = dtime.ToUniversalTime();
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sqlSelect = @"SELECT ContractPrice, IsExpiry, ExpiryDate FROM ContractPricing 
            WHERE CustomerID = " + cusID + "AND ExpiryDate > @DateNow and ItemID = " + itemid;
            param.Add("@DateNow", dtime);
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sqlSelect, param);
            DataGridViewRow dgvRows = dgEnterSales.Rows[pRowIndex];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvRows.Cells["Price"].Value = dr["ContractPrice"].ToString();
                }
                lRet = true;
            }
            return lRet;
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
            dgEnterSales.Rows[pRowIndex].Cells["Price"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["Discount"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["Amount"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["Job"].Value = null;
            dgEnterSales.Rows[pRowIndex].Cells["TaxCode"].Value = null;
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
                        }
                    }
                    else
                    {
                        if (dgvRows.Cells["TaxExclusiveAmount"].Value != null)
                        {
                            dgvRows.Cells["Amount"].Value = dgvRows.Cells["TaxExclusiveAmount"].Value;
                        }
                    }
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
                            if (dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value != null)
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
            }

            this.TaxAmount.Value = (TotalTaxInc - TotalTaxEx) + (decimal)FreightTax;
            this.subtotalAmountText.Value = TotalTaxEx;
            TotalAmount.Value = TotalTaxInc + (decimal)FreightAmountInc;
            this.BalanceDue_txt.Value = TotalAmount.Value - PaidToday_txt.Value;
        }

        private void chckInc_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
            {
                if (this.dgEnterSales.Rows[i].Cells["Amount"] != null)
                {
                    Recalcline(0, i);
                }
            }


            Recalc();
            CalcOutOfBalance();
        }
        void LoadPayment()
        {
            for (int x = 0; x < PaymentInfoTb.Rows.Count; x++)
            {
                DataRow dr = PaymentInfoTb.Rows[x];

                for (int i = 0; i < dgPaymentMethod.Rows.Count; i++)
                {
                    DataGridViewRow dgRow = dgPaymentMethod.Rows[i];
                    if (dgRow.Cells[0].Value.ToString() == dr["PaymentMethodID"].ToString())
                    {
                        decimal lamt = Convert.ToDecimal(dr["AmountPaid"].ToString());
                        if (lamt > 0)
                        {
                            dgRow.Cells[3].Value = dr["AmountPaid"].ToString();
                            dgRow.Cells[4].Value = dr["PaymentAuthorisationNumber"].ToString();
                            dgRow.Cells[5].Value = dr["PaymentCardNumber"].ToString();
                            dgRow.Cells[6].Value = dr["PaymentNameOnCard"].ToString();
                            dgRow.Cells[7].Value = dr["PaymentExpirationDate"].ToString();
                            dgRow.Cells[8].Value = dr["PaymentCardNotes"].ToString();
                            dgRow.Cells[9].Value = dr["PaymentBSB"].ToString();
                            dgRow.Cells[10].Value = dr["PaymentBankAccountNumber"].ToString();
                            dgRow.Cells[11].Value = dr["PaymentBankAccountName"].ToString();
                            dgRow.Cells[12].Value = dr["PaymentChequeNumber"].ToString();
                            dgRow.Cells[13].Value = dr["PaymentBankNotes"].ToString();
                            dgRow.Cells[14].Value = dr["PaymentNotes"].ToString();
                            dgRow.Cells[15].Value = dr["PaymentGCNo"].ToString();
                            dgRow.Cells[16].Value = dr["PaymentGCNotes"].ToString();
                        }
                        else
                        {
                            if (dgRow.Cells[0].Value.ToString() == ChangePaymentMethodID)
                            {
                                AmountChange.Value = lamt;
                            }
                            else
                            {
                                dgRow.Cells[3].Value = dr["AmountPaid"].ToString();
                                dgRow.Cells[4].Value = dr["PaymentAuthorisationNumber"].ToString();
                                dgRow.Cells[5].Value = dr["PaymentCardNumber"].ToString();
                                dgRow.Cells[6].Value = dr["PaymentNameOnCard"].ToString();
                                dgRow.Cells[7].Value = dr["PaymentExpirationDate"].ToString();
                                dgRow.Cells[8].Value = dr["PaymentCardNotes"].ToString();
                                dgRow.Cells[9].Value = dr["PaymentBSB"].ToString();
                                dgRow.Cells[10].Value = dr["PaymentBankAccountNumber"].ToString();
                                dgRow.Cells[11].Value = dr["PaymentBankAccountName"].ToString();
                                dgRow.Cells[12].Value = dr["PaymentChequeNumber"].ToString();
                                dgRow.Cells[13].Value = dr["PaymentBankNotes"].ToString();
                                dgRow.Cells[14].Value = dr["PaymentNotes"].ToString();
                                dgRow.Cells[15].Value = dr["PaymentGCNo"].ToString();
                                dgRow.Cells[16].Value = dr["PaymentGCNotes"].ToString();
                            }

                        }

                    }
                }
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
                    lTaxPaidAccountID = (rTx["TaxCollectedAccountID"] == null ? "" : rTx["TaxCollectedAccountID"].ToString());
                    dgvRows.Cells["TaxCollectedAccountID"].Value = lTaxPaidAccountID;
                    dgvRows.Cells["TaxRate"].Value = ltaxrate;
                }
            }
            else
            {
                dgvRows.Cells["TaxCode"].Value = "";
            }
        }


        public int SaveQuickSale(bool pIsRecurring = false)
        {
            if (salestype == "INVOICE")
            {
                DataTable lTbError = CheckOnHandQtyNewSale();
                if (lTbError.Rows.Count > 0)
                {
                    ItemErrorInfo ItemError = new ItemErrorInfo(lTbError);
                    ItemError.ShowDialog();
                    return 0;
                }
            }
            int count = 0;
            int NewTermID = 0;
            //NewTermID = CreateTerm();
            GenerateInvoiceNum();
            //string salestype = "INVOICE";
            string layout = "";
            Dictionary<string, object> param = new Dictionary<string, object>();
            string savesql = @"INSERT INTO Sales ( SalesType,
                                                       CustomerID,
                                                       UserID,
                                                       SalesNumber,
                                                       TransactionDate,
                                                       ShippingMethodID,
                                                       SubTotal,
                                                       FreightSubTotal,
                                                       FreightTax, 
                                                       TaxTotal,
                                                       LayoutType,
                                                       GrandTotal,
                                                       InvoiceStatus,
                                                       IsTaxInclusive,
                                                       ShippingContactID,
                                                       ClosedDate,
                                                       TotalPaid,
                                                       TotalDue,
                                                       FreightTaxCode,
                                                       FreightTaxRate,
                                                       SalesPersonID , 
                                                        SessionID,
                                                       InvoiceType,
                                                        PromiseDate,
                                                        Memo) 
                                              VALUES ( @SalesType,
                                                       @CustomerID,
                                                       @UserID,
                                                       @SalesNum,
                                                       @TransDate,
                                                       @ShippingMethodID,
                                                       @SubTotal,
                                                       @FreightSubTotal,
                                                       @FreightTax, 
                                                       @TaxTotal,
                                                       @Layout,
                                                       @GrandTotal,
                                                       @InvoiceStatus,
                                                       @isTaxInclusive,
                                                       @ShippingID,
                                                       @ClosedDate,
                                                       @TotalPaid,
                                                       @TotalDue,
                                                       @FreightTaxCode,
                                                       @FreightTaxRate,
                                                       @SalesPersonID ,
                                                       @SessionID,
                                                       @InvoiceType,
                                                       @PromiseDate,
                                                       @Memo);  
                                            SELECT SCOPE_IDENTITY()";
            param.Add("@SalesType", salestype);
            param.Add("@CustomerID", CustomerID);
            param.Add("@UserID", CommonClass.UserID);
            param.Add("@SalesNum", InvoiceNumTxt.Text);
            param.Add("@TransDate", salesDate.Value.ToUniversalTime());
            param.Add("@ShippingID", shipAddressID);
            param.Add("@SessionID", CommonClass.SessionID);
            param.Add("@InvoiceType", "Cash");
            param.Add("@PromiseDate", DateTime.Now.ToUniversalTime());
            param.Add("@Memo", "Sale;" + InvoiceNumTxt.Text);

            layout = "Item";

            param.Add("@InvoiceStatus", "Closed");
            param.Add("@ClosedDate", DateTime.Now.ToUniversalTime());

            param.Add("@Layout", layout);
            param.Add("@ShippingMethodID", ShippingMethodID == null || ShippingMethodID == "" ? "0" : ShippingMethodID);
            param.Add("@SubTotal", subtotalAmountText.Value);
            param.Add("@FreightSubTotal", FreightAmountEx);
            param.Add("@FreightTax", FreightTax);
            param.Add("@TaxTotal", TaxAmount.Value);
            param.Add("@TotalPaid", 0);
            param.Add("@TotalDue", BalanceDue_txt.Value);
            param.Add("@GrandTotal", TotalAmount.Value);

            param.Add("@TaxID", TaxID);
            param.Add("@FreightTaxCode", txtFTaxCode.Text);
            param.Add("@FreightTaxRate", FreightTaxRate);
            param.Add("@SalesPersonID", SalesPersonID);

            if (CommonClass.IsTaxcInclusiveEnterSales)
            {
                param.Add("@isTaxInclusive", "Y");
            }
            else
            {
                param.Add("@isTaxInclusive", "N");
            }
            NewSalesID = CommonClass.runSql(savesql, CommonClass.RunSqlInsertMode.SCALAR, param);

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
            string CostPrice = "";
            string TotalCost = "";
            string payment = "";
            int entity = 0;
            string promoid = "";
            Dictionary<string, object> paramItemQty = new Dictionary<string, object>();
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

                            
                        string salesLinesql = "";
                      
                        ShipQty = dgEnterSales.Rows[i].Cells["Ship"].Value.ToString();
                        UnitPrice = dgEnterSales.Rows[i].Cells["Price"].Value.ToString();
                        ActualPrice = dgEnterSales.Rows[i].Cells["ActualUnitPrice"].Value.ToString();
                        Discount = dgEnterSales.Rows[i].Cells["DiscountRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["DiscountRate"].Value.ToString();
                        ShipQ = Convert.ToInt32(ShipQty);
                        CostPrice = dgEnterSales.Rows[i].Cells["CostPrice"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["CostPrice"].Value.ToString();
                        TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();
                        promoid = dgEnterSales.Rows[i].Cells["PromoID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString();
                        string lTranDate = salesDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        if (salestype == "INVOICE")
                        {
                         
                            DataTable lItemTb = GetItem(entity.ToString());
                            if (lItemTb.Rows.Count > 0)
                            {
                                if ((bool)lItemTb.Rows[0]["IsAutoBuild"] && lItemTb.Rows[0]["BundleType"].ToString() == "Ingredient")
                                {
                                    IsIngredientBuild(entity, i);
                                }
                                if (float.Parse(CostPrice) == 0)
                                {
                                    //CostPrice = AverageCostEx
                                    CostPrice = lItemTb.Rows[0]["AverageCostEx"].ToString();
                                    CostPrice = (CostPrice == "" ? "0" : CostPrice);
                                    TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();
                                    TotalCost = (TotalCost == "" ? "0" : CostPrice);
                                   
                                }
                                
                                bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                                float lQty = float.Parse(ShipQty);
                                float lQtySUOM = float.Parse(lItemTb.Rows[0]["QtyPerSellingUnit"].ToString());
                                float lTranQty = lQty * lQtySUOM;
                                if (lIsCounted)
                                {
                                                                      
                                    salesLinesql = "UPDATE ItemsQty SET OnHandQty = OnHandQty - " + lTranQty.ToString() + " WHERE ItemID = " + entity;
                                    count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY);
                                    // INSERT SOLD ITEMS IN ITEM TRANSACTION
                                    salesLinesql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                                    VALUES('" + lTranDate + "'," + entity + "," + lTranQty + "," + lTranQty + " *(-1)," + CostPrice + "," + TotalCost + ",'SI'," + NewSalesID + "," + CommonClass.UserID + ")";
                                    //paramItemQty.Add("@TransactionDate", salesDate.Value.ToUniversalTime());
                                    count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY, paramItemQty);
                                }
                            }
                        }
                        promoid = dgEnterSales.Rows[i].Cells["PromoID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString();
                        promoid = promoid == "" ? "0" : promoid;


                        salesLinesql = "INSERT INTO SalesLines (SalesID, Description, TotalAmount, TransactionDate, TaxCode, UnitPrice, ActualUnitPrice, DiscountPercent, OrderQty, ShipQty, EntityID, JobID, SubTotal, TaxAmount, TaxCollectedAccountID,TaxRate, CostPrice, TotalCost, PromoID)" +
                                       " VALUES (@SalesID, @Description, @TotalAmount,@TransactionDate,@TaxCode,@UnitPrice,@ActualUnitPrice,@DiscountPercent,@OrderQty,@ShipQty,@EntityID,@JobID,@SubTotal,@TaxAmount,@TaxCollectedAccountID,@TaxRate,@CostPrice,@TotalCost,@PromoID)";
                        
                        Dictionary<string, object> paramSalesLine = new Dictionary<string, object>();
                        paramSalesLine.Add("@SalesID", NewSalesID);
                        paramSalesLine.Add("@Description", Descript);
                        paramSalesLine.Add("@TotalAmount", dAmount);
                        paramSalesLine.Add("@TransactionDate", lTranDate);
                        paramSalesLine.Add("@TaxCode", Tax);
                        paramSalesLine.Add("@UnitPrice", UnitPrice);
                        paramSalesLine.Add("@ActualUnitPrice", ActualPrice);
                        paramSalesLine.Add("@DiscountPercent", Discount);
                        paramSalesLine.Add("@OrderQty", OrderQty);
                        paramSalesLine.Add("@ShipQty", ShipQty);
                        paramSalesLine.Add("@EntityID", entity);
                        paramSalesLine.Add("@JobID", Job);
                        paramSalesLine.Add("@SubTotal", taxEx);
                        paramSalesLine.Add("@TaxAmount", TaxAm);
                        paramSalesLine.Add("@TaxCollectedAccountID", TaxID);
                        paramSalesLine.Add("@TaxRate", TaxRate);
                        paramSalesLine.Add("@CostPrice", CostPrice);
                        paramSalesLine.Add("@TotalCost", TotalCost);
                        paramSalesLine.Add("@PromoID", promoid);

                        count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.SCALAR, paramSalesLine);

                        double lPoints = dgEnterSales.Rows[i].Cells["Points"].Value != null ? Math.Round(float.Parse(dgEnterSales.Rows[i].Cells["Points"].Value.ToString()), 2) : 0;

                        if (lPoints != 0)
                        {
                            Dictionary<string, object> parampts = new Dictionary<string, object>();
                            string lPromoID = "0";
                            if (dgEnterSales.Rows[i].Cells["PromoID"].Value != null)
                            {
                                lPromoID = (dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString() == "" ? "0" : dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString());
                            }
                            parampts.Add("@PromoID", lPromoID);
                            parampts.Add("@Points", lPoints);
                            parampts.Add("@CustomerID", memberID);
                            parampts.Add("@TransDate", lTranDate);
                            parampts.Add("@SalesLineID", count);
                            parampts.Add("@ItemID", entity);

                            string promosql = "";

                            if (lPoints < 0)
                            {
                                parampts.Add("@RedemptionType", dgEnterSales.Rows[i].Cells["RedemptionType"].Value);
                                parampts.Add("@RedeemID", dgEnterSales.Rows[i].Cells["RedeemID"].Value.ToString());
                                promosql = @"INSERT INTO AccumulatedPoints (
                                                            PromoID, 
                                                            TransactionDate, 
                                                            PointsAccumulated, 
                                                            CustomerID, 
                                                            SalesLineID,
                                                            ItemID,
                                                            RedemptionType,
                                                            RedeemID) 
                                                VALUES (
                                                        @PromoID, 
                                                        @TransDate, 
                                                        @Points, 
                                                        @CustomerID, 
                                                        @SalesLineID,
                                                        @ItemID,
                                                        @RedemptionType,
                                                        @RedeemID)";
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

                            CommonClass.runSql(promosql, CommonClass.RunSqlInsertMode.QUERY, parampts);
                        }
                    }
                }
            }
            //UpdateItemQty();
            SaveIngredientData();
            UpdateTransSeries(ref cmd);
            if (SrcOfInvoke == CommonClass.InvocationSource.USERECURRING ||
                SrcOfInvoke == CommonClass.InvocationSource.REMINDER)
            {
                UpdateNotify();
            }

            if (salestype == "INVOICE")
            {
                CreateJournalEntries(NewSalesID);
                if (PaidToday_txt.Value != 0)
                {
                    RecordPayment(invoicenum, PaidToday_txt.Value - AmountChange.Value, 0);
                }
            }

            if (cmb_shippingcontact.SelectedIndex != 0)
            {
                string typeofContact = "";
                if (cmb_shippingcontact.SelectedIndex == 1)
                {
                    typeofContact = "Shipping";
                }

                if (cmb_shippingcontact.SelectedIndex == 2)
                {
                    typeofContact = "Main";
                }

                if (cmb_shippingcontact.SelectedIndex == 3)
                {
                    typeofContact = "Other";
                }
                string locID = "((SELECT MAX( Location ) FROM Contacts WHERE ProfileID = " + CustomerID + " ) +1 )";

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
                paramContact.Add("@TypeOfContact", typeofContact);
                CommonClass.runSql(strContacts, CommonClass.RunSqlInsertMode.QUERY, paramContact);
            }
            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Sales  " + salestype + " No. " + InvoiceNumTxt.Text);
            saveValidationLog(NewSalesID);
            if (SrcOfInvoke == CommonClass.InvocationSource.REMINDER || SrcOfInvoke == CommonClass.InvocationSource.SAVERECURRING)
            {
                MessageBox.Show("Sales Record has been created");
                return NewSalesID;
            }
            if (count != 0 && SrcOfInvoke != CommonClass.InvocationSource.SAVERECURRING)
            {
                ChangeAmount DlgChange = new ChangeAmount(AmountChange.Value.ToString());
                DlgChange.ShowDialog();

                string titles = "Information";
                DialogPrint printsale = new DialogPrint("Would you like to print the new  " + salestype + " created?");
                 if (printsale.ShowDialog() == DialogResult.OK)
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

                DialogPrint createNew = new DialogPrint("Sales Record has been created. Would you like to enter a new " + salestype + "?");
                if(createNew.ShowDialog() == DialogResult.OK)
                {   //clear for new datas
                    dgEnterSales.Rows.Clear();
                    dgEnterSales.Refresh();
                    customerText.Clear();
                    PayeeInfo.Clear();
                    PopulateDataGridView();
                    InvoiceNumTxt.Text = "";
                    InvoiceNumTxt.Visible = false;
                    TotalAmount.Value = 0;
                    BalanceDue_txt.Value = 0;
                    PaidToday_txt.Value = 0;
                    AmountChange.Value = 0;
                    memberID = "";
                    txtCustPoints.Visible = false;
                    btnRedeem.Visible = false;
                    lblPoints.Visible = false;
                    salesDate.Value = DateTime.Now;
                    ShippingmethodText.Clear();
                    InitContactInfoTb();
                    InitIngredientInfoTb();
                    InitFreeTable();
                    InitPaymentInfoTb();

                    InitVoidTable();
                    LoadPaymentMethods();
                    LoadDefaultCustomer();
                    QuickSalesTabControl.SelectedIndex = 0;
                    dtv.Rows.Clear();
                    rowin = 0;
                }
                else
                {
                    CommonClass.QuickSalesfrm.Close();
                }
            }
            return NewSalesID;
        }
        private void SaveIngredientData()
        {
            string lTranDate = salesDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            if (IngredientTb.Rows.Count > 0)
            {
                foreach (DataRow dr in IngredientTb.Rows)
                {
                    if (dr["ItemID"] != null)
                    {
                        if (dr["ItemID"].ToString() != "")
                        {
                            Dictionary<string, object> paramIngredient = new Dictionary<string, object>();
                            string sql = @"INSERT INTO Ingredient (ItemID, Qty, PartNumber, Amount, TaxRate, TaxCode, TaxCollectedAccountID, Cost, TotalCost, Description, SalesID) 
                            VALUES (@ItemID,@Qty,@PartNumber, @Amount, @TaxRate, @TaxCode , @TaxCollectedAccountID, @Cost, @TotalCost, @Description, @SalesID)";
                            paramIngredient.Add("@ItemID", dr["ItemID"].ToString());
                            if (dr["Ship"].ToString() != "")
                            {
                                paramIngredient.Add("@Qty", float.Parse(dr["Ship"].ToString()));
                            }
                            else
                            {
                                paramIngredient.Add("@Qty", 0);
                            }
                            paramIngredient.Add("@PartNumber", dr["PartNumber"].ToString());
                            paramIngredient.Add("@Amount", dr["Amount"].ToString());
                            paramIngredient.Add("@TaxRate", dr["TaxRate"].ToString());
                            paramIngredient.Add("@TaxCode", dr["TaxCode"].ToString());
                            paramIngredient.Add("@TaxCollectedAccountID", dr["TaxCollectedAccountID"].ToString());
                            paramIngredient.Add("@Cost", dr["Cost"].ToString());
                            paramIngredient.Add("@TotalCost", dr["TotalCost"].ToString());
                            paramIngredient.Add("@Description", dr["Description"].ToString());
                            paramIngredient.Add("@SalesID", NewSalesID);
                            if (dr["ItemID"].ToString() != "" && dr["ItemID"].ToString() != "0")
                            {
                                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramIngredient);
                            }
                            if (salestype == "INVOICE")
                            {
                                string CostPrice = "";
                                string TotalCost = "";
                                DataTable lItemTb = GetItem(dr["ItemID"].ToString());
                                if (lItemTb.Rows.Count > 0)
                                {
                                    if (float.Parse(dr["Cost"].ToString()) == 0)
                                    {
                                        //CostPrice = AverageCostEx
                                        CostPrice = lItemTb.Rows[0]["AverageCostEx"].ToString();
                                        CostPrice = (CostPrice == "" ? "0" : CostPrice);
                                        TotalCost = (float.Parse(dr["Ship"].ToString()) * float.Parse(CostPrice)).ToString();
                                        TotalCost = (TotalCost == "" ? "0" : TotalCost);
                                    }
                                    else
                                    {
                                        CostPrice = dr["Cost"].ToString();
                                        CostPrice = (CostPrice == "" ? "0" : CostPrice);
                                        TotalCost = (float.Parse(dr["Ship"].ToString()) * float.Parse(CostPrice)).ToString();
                                        TotalCost = (TotalCost == "" ? "0" : TotalCost);
                                    }
                                    bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                                    float lQty = float.Parse(dr["Ship"].ToString());
                                    float lQtySUOM = float.Parse(lItemTb.Rows[0]["QtyPerSellingUnit"].ToString());
                                    float lTranQty = lQty * lQtySUOM;

                                  
                                    if (lIsCounted)
                                    {
                                        string salesLinesql = "UPDATE ItemsQty SET OnHandQty = OnHandQty - " + lTranQty.ToString() + " WHERE ItemID = " + dr["ItemID"].ToString();
                                        int count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY);
                                        // INSERT SOLD ITEMS IN ITEM TRANSACTION
                                        salesLinesql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                                    VALUES('" + lTranDate + "'," + dr["ItemID"].ToString() + "," + lTranQty + "," + lTranQty + " *(-1)," + CostPrice + "," + TotalCost + ",'SI'," + NewSalesID + "," + CommonClass.UserID + ")";
                                        //paramItemQty.Add("@TransactionDate", salesDate.Value.ToUniversalTime());
                                        count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY);
                                    }
                                }
                            }
                        }
                    }
                }
            }
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
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (NewOnHand < 0)
            {
                MessageBox.Show("Need to purchase Item" + NewOnHand * -1, "Information");
            }
            else
            {
                string termsql = @"UPDATE ItemsQty SET OnHandQty = @NewOnHandQty, CommitedQty = @NewCommitedQty WHERE ItemID = " + ItemID;
                param.Add("@NewOnHandQty", NewOnHand);
                param.Add("@NewCommitedQty", NewOnHand);
                CommonClass.runSql(termsql, CommonClass.RunSqlInsertMode.QUERY, param);
                SqlConnection con = null;
            }
        }


        void UpdateTransSeries(ref SqlCommand pCmd)
        {
            string sql = "";
            if (salestype == "ORDER")
            {
                sql = "UPDATE TransactionSeries SET SalesOrderSeries = '" + CurSeries + "'";
            }
            else if (salestype == "QUOTE")
            {
                sql = "UPDATE TransactionSeries SET SalesQuoteSeries = '" + CurSeries + "'";
            }
            else
            {
                sql = "UPDATE TransactionSeries SET SalesInvoiceSeries = '" + CurSeries + "'";
            }

            int res2 = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY);
        }

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
                dgEnterSales.Columns["Amount"].ReadOnly = true;
                dgEnterSales.Columns["Job"].ReadOnly = true;
                dgEnterSales.Columns["TaxCode"].ReadOnly = true;
            }
            else
            {
                //    dgEnterSales.Columns["Ship"].ReadOnly = false;
                //    dgEnterSales.Columns["PartNumber"].ReadOnly = false;
                //    dgEnterSales.Columns["Price"].ReadOnly = false;
                //    dgEnterSales.Columns["Discount"].ReadOnly = false;
                //    dgEnterSales.Columns["Description"].ReadOnly = false;
                //    dgEnterSales.Columns["Amount"].ReadOnly = false;
                //    dgEnterSales.Columns["Job"].ReadOnly = false;
                //    dgEnterSales.Columns["TaxCode"].ReadOnly = false;
                if (salestype != "QUOTE")
                {

                    PaidToday_txt.Visible = true;
                    //   flpPaymentsMethods.Enabled = true;
                    BalanceDueAmountTxt.Visible = true;
                }
                if (PaidToday_txt.Value != 0)
                {
                    PaidToday_txt.Enabled = false;

                }
            }
        }

        private void customerText_TextChanged(object sender, EventArgs e)
        {
            dgEnable();
        }

        void itemcalc(int RoWindex)
        {
            if (RoWindex < 0)
            {
                return;
            }
            double shipVal = 0;
            double priceVal = 0;
            double disc = 0;
            double discValue = 0;
            double woDisc = 0;
            
            DataGridViewRow dgvRows = dgEnterSales.Rows[RoWindex];
            shipVal = Convert.ToDouble(dgvRows.Cells["Ship"].Value == null ? "1" : dgvRows.Cells["Ship"].Value);
            dgvRows.Cells[1].Value = shipVal.ToString();
            if (dgvRows.Cells["Price"].Value != null)
            {
                //MessageBox.Show("Price:" + dgvRows.Cells["Price"].Value.ToString());
                //APPLY NON DEFAULT PROMOS
                isItemPromo(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()),float.Parse(dgvRows.Cells["Price"].Value.ToString()), RoWindex);
                //APPLY DEFAULT PROMOS for Loyalty Members

                if (memberID != "")
                {
                    // MessageBox.Show("Price:" + dgvRows.Cells["Price"].Value.ToString());
                    dgvRows.Cells["Points"].Value = isItemPromo(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), float.Parse(dgvRows.Cells["Price"].Value.ToString()), RoWindex, "Default");
                }
                priceVal = double.Parse(dgvRows.Cells["Price"].Value.ToString(), NumberStyles.Currency);
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

            if (shipVal > 0)
            {
                dgvRows.Cells["Amount"].Value = woDisc - discValue;
            }
            else
            {
                dgvRows.Cells["Amount"].Value = woDisc - discValue;
            }
          
        }

        private int VerifyItemQty(int pRIndex, string pSalesID = "0", bool isautobuild = false)
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
                            return 0;
                        }
                    }

                }
            }
            return 1;

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
            DataTable RTb = new DataTable();

            string sql = @"SELECT l.*, s.SalesNumber, s.GrandTotal, s.Memo, s.FreightSubTotal, s.FreightTax, s.FreightTaxCode, s.FreightTaxRate, i.AssetAccountID, 
                    i.IsCounted, i.IncomeAccountID, i.IsSold, i.COSAccountID, i.IsBought FROM ( SalesLines l INNER JOIN Sales s ON l.SalesID = s.SalesID ) left join Items as i on l.EntityID =i.ID WHERE l.SalesID = " + pSalesID;
            CommonClass.runSql(ref RTb, sql);
            return RTb;
        } //END
        public static DataTable GetUsedIngredients(string pSalesID)
        {
            DataTable RTb = new DataTable();

            string sql = @"SELECT ing.*, i.AssetAccountID, i.COSAccountID, i.IsCounted, i.IsBought
  FROM Ingredient ing inner join Items i on ing.ItemID = i.ID where ing.SalesID = " + pSalesID;
            CommonClass.runSql(ref RTb, sql);
            return RTb;
        } //END
        private bool CreateJournalEntries(int pID)
        {
            string sql = "";
            DataTable ltb = GetSalesLines(pID);
            if (ltb.Rows.Count > 0)
            {
                decimal lGrandTotal = Convert.ToDecimal(ltb.Rows[0]["GrandTotal"].ToString());
                string lRecipientID = /*Convert.ToDecimal(*/AR_AccountID/*)*/;
                string lSalesNum = InvoiceNumTxt.Text;
                string lMemo = String.Format("{0}", ltb.Rows[0]["Memo"].ToString());
                string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                decimal lFreightEx = Convert.ToDecimal(ltb.Rows[0]["FreightSubTotal"].ToString());
                decimal lFreightTax = Convert.ToDecimal(ltb.Rows[0]["FreightTax"].ToString());
                string lSalesID = ltb.Rows[0]["SalesID"].ToString();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@lMemo", lMemo);
                //INSERT JOURNAL FOR Total Amount Received
                if (TotalAmount.Value < 0)
                {
                    //NEGATIVE SO CREDIT AMOUNT
                    sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                            " VALUES('" + lTranDate + "',  @lMemo, @lMemo , '" + lRecipientID + "', " +
                            (TotalAmount.Value * -1).ToString() + ",'" + lSalesNum + "', 'SI', " + lSalesID + ")";
                }
                else
                {
                    //DEBIT AMOUNT
                    sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID) " +
                            " VALUES('" + lTranDate + "', @lMemo, @lMemo ,'" + lRecipientID + "', " +
                            TotalAmount.Value + ",'" + lSalesNum + "','SI', " + lSalesID + ")";
                }
                new DataTable();

                //INSERT JOURNAL FOR FREIGHT 
                if (lFreightEx != 0)
                {
                    if (lFreightEx < 0) // NEGATIVE SO DEBIT AMOUNT 
                    {
                        // NEGATIVE SO DEBIT AMOUNT 
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                VALUES('" + lTranDate + "',  @lMemo, @lMemo , '" + AR_FreightAccountID + "', " +
                                (lFreightEx * -1).ToString() + ", '" + lSalesNum + "', 'SI',0, " + lSalesID + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY,param);

                        //THIS IS FOR THE TAX COMPONENT
                        if (lFreightTax != 0 && FreightTaxAccountID != "")
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                        VALUES('" + lTranDate + "',  @lMemo, @lMemo , '" + FreightTaxAccountID + "', " +
                                        (lFreightTax * -1).ToString() + ", '" + lSalesNum + "', 'SI',0, " + lSalesID + ")"; ;
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                        }
                    }
                    else //POSITIVE SO CREDIT AMOUNT 
                    {
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                VALUES('" + lTranDate + "',  @lMemo, @lMemo , '" + AR_FreightAccountID + "', " +
                                lFreightEx.ToString() + ", '" + lSalesNum + "', 'SI',0, " + lSalesID + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                        //THIS IS FOR THE TAX COMPONENT
                        if (lFreightTax != 0 && FreightTaxAccountID != "")
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                    VALUES('" + lTranDate + "',  @lMemo, @lMemo , '" + FreightTaxAccountID + "', " +
                                    lFreightTax.ToString() + ", '" + lSalesNum + "','SI',0, " + lSalesID + ")";
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                        }
                    }
                }

                for (int i = 0; i < ltb.Rows.Count; i++)
                {
                    string lAccountID = ltb.Rows[i]["EntityID"].ToString();
                    decimal lTaxEx = Convert.ToDecimal(ltb.Rows[i]["SubTotal"].ToString());
                    decimal lTaxInc = Convert.ToDecimal(ltb.Rows[i]["TotalAmount"].ToString());
                    decimal lTaxAmt = lTaxInc - lTaxEx;
                    string lTaxCollectedAccountID = (ltb.Rows[i]["TaxCollectedAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["TaxCollectedAccountID"].ToString());
                    string lJobID = (ltb.Rows[i]["JobID"].ToString() == "" ? "0" : ltb.Rows[i]["JobID"].ToString());
                    string lIncomeAccountID = ltb.Rows[i]["IncomeAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["IncomeAccountID"].ToString();
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
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,DebitAmount, TransactionNumber, Type, JobID, EntityID)
VALUES('" + lTranDate + "', @lMemo, @lMemo,'" + lAccountID + "'," + (lTaxEx * -1) + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                        //THIS IS FOR THE TAX COMPONENT
                        if (lTaxAmt != 0)
                        {
                            lTaxAmt = lTaxEx - lTaxInc;
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID,
                                                DebitAmount, TransactionNumber, Type, JobID, EntityID)";
                            sql += " VALUES('" + lTranDate + "',  @lMemo, @lMemo,'" + lTaxCollectedAccountID + "', " + lTaxAmt.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lSalesID + ")";
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                        }
                        if (lIsCounted)
                        {
                            decimal lTotalCost = (ltb.Rows[i]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(ltb.Rows[i]["TotalCost"].ToString()));
                            //FOR INVENTORY
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                    VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lAssetAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                            //FOR COST OF SALES
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lCOSAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                        }
                    }
                    else //POSITIVE SO CREDIT AMOUNT 
                    {
                        //FOR INCOME
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,CreditAmount, TransactionNumber, Type, JobID, EntityID)
VALUES ('" + lTranDate + "',  @lMemo, @lMemo, '" + lAccountID + "', " + lTaxEx + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                        //cmd.CommandText = sql;
                        //cmd.ExecuteNonQuery();
                        //THIS IS FOR THE TAX COMPONENT
                        if (lTaxAmt != 0 && lTaxCollectedAccountID != "")
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID) VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lTaxCollectedAccountID + "'," + lTaxAmt.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lTaxCollectedAccountID + ")";
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                        }
                        if (lIsCounted)
                        {
                            decimal lTotalCost = (ltb.Rows[i]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(ltb.Rows[i]["TotalCost"].ToString()));
                            //FOR INVENTORY
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lAssetAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                            //FOR COST OF SALES
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', @lMemo, @lMemo,'" + lCOSAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                        }
                    }

                }
                //GET USED INGREDIENTS

                DataTable lTbIng = GetUsedIngredients(pID.ToString());
                for (int j = 0; j < lTbIng.Rows.Count; j++)
                {
                    decimal lTotalCost = (lTbIng.Rows[j]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(lTbIng.Rows[j]["TotalCost"].ToString()));
                    string lAssetAccountID = (lTbIng.Rows[j]["AssetAccountID"].ToString() == "" ? "0" : lTbIng.Rows[j]["AssetAccountID"].ToString());
                    string lCOSAccountID = (lTbIng.Rows[j]["COSAccountID"].ToString() == "" ? "0" : lTbIng.Rows[j]["COSAccountID"].ToString());
                    if (lTotalCost < 0)
                    {
                        //FOR INGREDIENT INVENTORY
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lAssetAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                        //FOR INGREDIENT COST OF SALES
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lCOSAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                    }
                    else
                    {
                        //FOR INVENTORY
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lAssetAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                        //FOR COST OF SALES
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lCOSAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                       CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY,param);

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



        private void LoadContacts(int cID, int index)
        {
            DataTable dt = new DataTable();
            //GET THE HEADER 
            string sql = @"SELECT * FROM Contacts WHERE ProfileID = " + cID + " and Location = " + index;
            CommonClass.runSql(ref dt, sql);
            if (dt.Rows.Count == 0)
            {
                shipAddressID = 0;
                return;
            }
            else
            {
                PayeeInfo.Text = dt.Rows[0]["Street"].ToString() + Environment.NewLine + dt.Rows[0]["City"].ToString() + " " + dt.Rows[0]["State"].ToString() + Environment.NewLine + dt.Rows[0]["Country"].ToString() + " " + dt.Rows[0]["PostCode"].ToString();
                shipAddressID = index;
            }
        }

        void CreateSalesReversal(string pOldSalesID)
        {
            GenerateInvoiceNum();
            if (invoicenum != "")
            {
                int res = ReverseSalesRecord(invoicenum, pOldSalesID);
                int lSalesID = res;
                if (CreateJournalEntries(lSalesID))
                {
                    decimal lbal = this.BalanceDue_txt.Value * (-1);
                    TransactionClass.UpdateProfileBalances(CustomerID, lbal);

                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Reversal for Sales Number " + pOldSalesID + " with New Journal Number " + invoicenum, lSalesID.ToString());
                    DialogResult PrintInvoice = MessageBox.Show("Would you like to print the sales reversal  " + salestype + " created?", "Information"
                        , MessageBoxButtons.YesNo);
                    if (PrintInvoice == DialogResult.Yes)
                    {
                        LoadItemLayoutReport(lSalesID.ToString());
                    }

                    MessageBox.Show("Sales Reversal Recorded successfully", "Sales Entry Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Transaction Series Numbers not setup properly.");
            }
        }

        private int ReverseSalesRecord(string pNewSalesNo, string pOldSalesNo)
        {
            //Reverse Sales Record
            string lTranDate = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");

            string sql = @"INSERT INTO Sales ( SalesNumber, TransactionDate,SalesType, CustomerID, UserID, ShippingMethodID, 
                        SubTotal, FreightSubTotal,FreightTax, TaxTotal, GrandTotal, Memo, TotalPaid, TotalDue,
                        LayoutType, InvoiceStatus, SalesReference, CustomerPONumber, Comments,
                        IsTaxInclusive,TermsReferenceID, ShippingContactID,ClosedDate, FreightTaxCode, FreightTaxRate, SalesPersonID )";
            sql += "SELECT '" + pNewSalesNo + "' AS SalesNumber, '" + lTranDate + "' As TransactionDate,SalesType, CustomerID, UserID, ShippingMethodID, " +
                        " SubTotal * (1) AS SubTotal, FreightSubTotal * (1) as FreightSubTotal, FreightTax * (-1) as FreightTax, TaxTotal * (-1) as TaxTotal, GrandTotal * (1) as GrandTotal, 'Reversal of " + pOldSalesNo + "; " + customerText.Text + "' as Memo, TotalPaid, TotalDue * (-1) as TotalDue, " +
                        " LayoutType, InvoiceStatus, SalesReference, CustomerPONumber, Comments," +
                        " IsTaxInclusive,TermsReferenceID, ShippingContactID,ClosedDate, FreightTaxCode, FreightTaxRate, SalesPersonID FROM Sales WHERE SalesNumber = '" + pOldSalesNo + "'; SELECT SCOPE_IDENTITY()";

            int NewsalesID = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR);
            if (NewsalesID != 0)
            {
                //Update series #                              
                sql = "UPDATE TransactionSeries SET SalesInvoiceSeries = '" + CurSeries + "'";
                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY);

                //REVERSE SalesLines  

                sql = "INSERT INTO SalesLines (SalesID, Description,  TransactionDate, JobID, TaxCode, EntityID, ShipQty, UnitPrice, ActualUnitPrice, SubTotal, TaxAmount, TotalAmount,TaxCollectedAccountID,TaxRate, CostPrice, TotalCost)" +
                                                " SELECT " + NewsalesID + " AS SalesID, Description, '" + lTranDate + "' As  TransactionDate, JobID, TaxCode, EntityID, ShipQty * (-1) as ShipQty, UnitPrice, ActualUnitPrice, SubTotal * (-1) as SubTotal, " +
                                                " TaxAmount * (-1) as TaxAmount, TotalAmount * (-1) as TotalAmount, TaxCollectedAccountID,TaxRate, CostPrice, TotalCost * (-1) as TotalCost  FROM SalesLines WHERE SalesID = " + XsalesID;
                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY);
            }
            return NewsalesID;
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

                }
            }
        }

        private void LoadTerms()
        {
            DataTable dt = new DataTable();
            string selectSql = "SELECT t.Description, p.* FROM TermsOfPayment t INNER JOIN Profile p ON t.TermsOfPaymentID = p.TermsOfPayment WHERE TermsOfPaymentID =  '" + TermRefID + "'";
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                CustomerRow = dt.Rows[0];
            }
            BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
            DiscountDays = CustomerRow["DiscountDays"].ToString();
            VolumeDiscount = CustomerRow["VolumeDiscount"].ToString();
            EarlyPaymenDiscount = CustomerRow["EarlyPaymentDiscountPercent"].ToString();
            LatePaymenDiscount = CustomerRow["LatePaymentChargePercent"].ToString();
            baldate = CustomerRow["BalanceDueDate"].ToString();
            discountdate = CustomerRow["DiscountDate"].ToString();
        }

        private void LoadShippingID(string pshippingMethod)
        {
            DataTable dt = new DataTable();
            string selectSql = "SELECT * FROM ShippingMethods WHERE ShippingMethod = '" + pshippingMethod + "'";
            CommonClass.runSql(ref dt, selectSql);
            ShippingMethodID = "0";
            if (dt.Rows.Count > 0)
            {
                ShipID = dt.Rows[0];
                ShippingMethodID = ShipID["ShippingID"].ToString();
                ShipVia = ShipID["ShippingMethod"].ToString();
            }
        }

        private void LoadShippingMethod(string pShipID)
        {
            DataTable dt = new DataTable();
            string selectSql = "SELECT * FROM ShippingMethods WHERE ShippingID = '" + pShipID + "'";
            CommonClass.runSql(ref dt, selectSql);
            ShipVia = "";
            ShippingMethodID = "0";
            if (dt.Rows.Count > 0)
            {
                ShipID = dt.Rows[0];
                ShipVia = ShipID["ShippingMethod"].ToString();
                ShippingMethodID = ShipID["ShippingID"].ToString();
            }
        }

        private int UpdateInvoiceTerm(string pTermID)
        {
            //Calculate Actual Due Date of the Transaction
            DateTime lTranDate = this.salesDate.Value.ToUniversalTime();
            DateTime lDueDate = lTranDate;
            DateTime lDiscountDate = lTranDate;
            string lTermsOfPaymentID = SalesTermsRow["TermsOfPaymentID"].ToString().Trim();
            Dictionary<string, object> param = new Dictionary<string, object>();
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
            string termsql = @"UPDATE Terms set TermsOfPaymentID = @TermsOfPaymentID, 
                                DiscountDays = @DiscountDays, 
                                BalanceDueDays = @BalanceDueDays, 
                                VolumeDiscount = @VolumeDiscount, 
                                ActualDueDate = @ActualDueDate, 
                                ActualDiscountDate = @ActualDiscountDate,
                                EarlyPaymentDiscountPercent = @EarlyPaymentDiscountPercent,
                                LatePaymentChargePercent = @LatePaymentChargePercent
                                Where TermsID = " + pTermID;

            param.Add("@TermsOfPaymentID", SalesTermsRow["TermsOfPaymentID"]);
            param.Add("@DiscountDays", SalesTermsRow["DiscountDays"]);
            param.Add("@BalanceDueDays", SalesTermsRow["BalanceDueDays"]);
            param.Add("@VolumeDiscount", SalesTermsRow["VolumeDiscount"]);
            param.Add("@ActualDueDate", ActualDueDate);
            param.Add("@ActualDiscountDate", ActualDiscountDate);
            param.Add("@EarlyPaymentDiscountPercent", SalesTermsRow["EarlyPaymentDiscountPercent"].ToString());
            param.Add("@LatePaymentChargePercent", SalesTermsRow["LatePaymentChargePercent"].ToString());
            int NewTermID = CommonClass.runSql(termsql, CommonClass.RunSqlInsertMode.SCALAR, param);
            return NewTermID;
        }

        private void PaidToday_txt_ValueChanged(object sender, EventArgs e)
        {
            CalcOutOfBalance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dgEnterSales.SelectedCells)
            {
                if (oneCell.RowIndex >= 0 && oneCell.RowIndex < (dgEnterSales.Rows.Count - 1))
                    dgEnterSales.Rows.RemoveAt(oneCell.RowIndex);
            }
            CalcOutOfBalance();
            dgEnterSales.Refresh();
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

        private void LoadLayoutReport(string pSalesID)
        {
            Reports.ReportParams saleslayoutparams = new Reports.ReportParams();
            saleslayoutparams.PrtOpt = 1;
            //if (TbRepSalesLines.Rows.Count == 0
            //    && TbRepSales.Rows.Count > 0)
            //{
            //    saleslayoutparams.Rec.Add(TbRepSales);
            //}
            //else
            //{
            //    saleslayoutparams.Rec.Add(TbRepSalesLines);
            //}
            string printsale = @"SELECT SalesType, SalesNumber , Sales.TransactionDate  , Sales.Comments, Sales.SubTotal ,p.Name as ProfileName,
                        c.Street as ShipStreet, c.City as ShipCity, c.State as ShipState,
                        c2.Street as BillStreet, c2.City as BillCity, c2.State as BillState, 
                        sl.Description ,sl.TotalAmount, sl.TaxCode,Sales.TaxTotal,FreightTax,TotalPaid, GrandTotal,TotalDue ,
                        ShippingMethod, u.user_fullname FROM Sales                       
                        Left JOIN ShippingMethods  sm On sm.ShippingID = Sales.ShippingMethodID                       
                        LEFT JOIN Contacts c ON c.Location = Sales.ShippingContactID and c.ProfileID = Sales.CustomerID  
                        LEFT JOIN Contacts c2 ON c2.Location = Sales.BillingContactID and c2.ProfileID = Sales.CustomerID 
                        INNER JOIN SalesLines sl ON sl.SalesID = Sales.SalesID
                        INNER JOIN Users u ON u.user_id = Sales.SalesPersonID
                        INNER JOIN Profile p ON p.ID = Sales.CustomerID WHERE Sales.SalesID = '" + pSalesID + "'";
            SqlConnection con = new SqlConnection(CommonClass.ConStr);
            SqlCommand cmd = new SqlCommand(printsale, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            saleslayoutparams.Rec.Add(dt);

            saleslayoutparams.ReportName = "ServiceLayout.rpt";
            saleslayoutparams.RptTitle = "Service Layout";

            saleslayoutparams.Params = "compname|CompAddress|TIN";
            saleslayoutparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim();

            CommonClass.ShowReport(saleslayoutparams);
        }
        public void PrintGC()
        {
            DataTable TbRep = new DataTable();
            TbRep.Columns.Add("GCNumber", typeof(string));
            TbRep.Columns.Add("GCAmount", typeof(float));
            TbRep.Columns.Add("ItemNumber", typeof(string));
            TbRep.Columns.Add("EndDate", typeof(string));
            TbRep.Columns.Add("ProfileName", typeof(string));

            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
            {
                if (dgvr.Cells["RedeemID"].Value != null)
                {
                   string sql = @"SELECT DISTINCT g.GCNumber, g.GCAmount, i.ItemNumber, g.EndDate,
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
                        rw["GCNumber"] = "*" + dr[0] + "*";
                        rw["GCAmount"] = dr[1];
                        rw["ItemNumber"] = dr[2].ToString();
                        rw["EndDate"] = dr[3];
                        rw["ProfileName"] = dr[4].ToString();
                        TbRep.Rows.Add(rw);
                    }

                }
            }
            Reports.ReportParams GCReceipt = new Reports.ReportParams();
            GCReceipt.PrtOpt = 0;
            GCReceipt.Rec.Add(TbRep);
            GCReceipt.ReportName = "GCReceipt.rpt";
            GCReceipt.RptTitle = "Gift Certificate";
            GCReceipt.Params = "compname|CompAddress|TIN";
            GCReceipt.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim();

            CommonClass.ShowReport(GCReceipt);
        }

        private void LoadItemLayoutReport(string pSalesID)
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

            salesItemlayoutparams.ReportName = "Receipt76logo.rpt";
            salesItemlayoutparams.RptTitle = "Item Layout 76mm";
            salesItemlayoutparams.PrtOpt = 0;
            salesItemlayoutparams.Params = "compname|CompAddress|TIN|LogoPath|tenders";
            salesItemlayoutparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim() + "|" + CommonClass.CompLogoPath + "|" + lTenders;

            CommonClass.ShowReport(salesItemlayoutparams);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadItemLayoutReport(this.lblID.Text);
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
                FreightTaxAccountID = (rTx["TaxCollectedAccountID"] == null ? "" : rTx["TaxCollectedAccountID"].ToString());
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
                    string Amount = (PaidToday_txt.Value - AmountChange.Value).ToString();
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
                                                          @PaymentMethodID ) ; SELECT SCOPE_IDENTITY()";
                        SqlCommand cmdTender = new SqlCommand(sqlTender, con);
                        cmdTender.Parameters.AddWithValue("@PaymentID", paymentid);
                        cmdTender.Parameters.AddWithValue("@Amount", PaymentInfoTb.Rows[i]["AmountPaid"].ToString());
                        cmdTender.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"]);
                        //cmdTender.ExecuteNonQuery();
                        int ptid = Convert.ToInt32(cmdTender.ExecuteScalar());
                        //Update GC
                        string lMethodId = PaymentInfoTb.Rows[i]["PaymentMethodID"].ToString().Trim();
                        //MessageBox.Show("PaymentMethodID:" + lMethodId);
                        if (lMethodId == "16") ;
                        {
                            //MessageBox.Show("GC:" + PaymentInfoTb.Rows[i]["PaymentMethodID"].ToString());
                            string gcsql = @"Update GiftCertificate set IsUsed = '1',usedSalesID = '" + NewSalesID + "' where GCNumber = '" + PaymentInfoTb.Rows[i]["PaymentGCNo"].ToString() + "'";
                            CommonClass.runSql(gcsql);
                        }
                        //Details
                        string sqlDetails = @"SET IDENTITY_INSERT PaymentDetails ON;
                                                INSERT INTO PaymentDetails (
                                                            PaymentDetailsID,
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
                                                        @PaymentDetailsID,
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
                        cmdDetails.Parameters.AddWithValue("@PaymentDetailsID", ptid);
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
            Dictionary<string, object> param = new Dictionary<string, object>();
            DateTime time = DateTime.Now;
            DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
            DateTime timeutc = time.ToUniversalTime();

            string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");
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
                                        PaymentAuthorisationNumber, 
                                        PaymentCardNumber, 
                                        PaymentNameOnCard,
                                        PaymentExpirationDate, 
                                        PaymentCardNotes, 
                                        PaymentBSB, 
                                        PaymentBankAccountNumber, 
                                        PaymentBankAccountName,
                                        PaymentChequeNumber, 
                                        PaymentBankNotes, 
                                        PaymentNotes
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
                                        @PaymentNotes
                                    ); SELECT SCOPE_IDENTITY()";

            param.Add("@ProfileID", CustomerID);
            param.Add("@PaymentNumber", pPaymentNo);
            param.Add("@TotalAmount", PaidToday_txt.Value);
            param.Add("@Memo", "Payment " + customerText.Text);
            param.Add("@AccountID", AR_CustomerDepositsID);
            param.Add("@UserID", CommonClass.UserID);
            param.Add("@TransactionDate", trandate);
            param.Add("@PaymentMethodID", PaymentInfoTb.Rows[0]["PaymentMethodID"].ToString());
            param.Add("@PaymentAuthorisationNumber", PaymentInfoTb.Rows[0]["PaymentAuthorisationNumber"].ToString());
            param.Add("@PaymentCardNumber", PaymentInfoTb.Rows[0]["PaymentCardNumber"].ToString());
            param.Add("@PaymentNameOnCard", PaymentInfoTb.Rows[0]["PaymentNameOnCard"].ToString()); ;
            param.Add("@PaymentExpirationDate", PaymentInfoTb.Rows[0]["PaymentExpirationDate"].ToString());
            param.Add("@PaymentCardNotes", PaymentInfoTb.Rows[0]["PaymentCardNotes"].ToString());
            param.Add("@PaymentBSB", PaymentInfoTb.Rows[0]["PaymentBSB"].ToString());
            param.Add("@PaymentBankAccountNumber", PaymentInfoTb.Rows[0]["PaymentBankAccountNumber"].ToString());
            param.Add("@PaymentBankAccountName", PaymentInfoTb.Rows[0]["PaymentBankAccountName"].ToString());
            param.Add("@PaymentChequeNumber", PaymentInfoTb.Rows[0]["PaymentChequeNumber"].ToString());
            param.Add("@PaymentBankNotes", PaymentInfoTb.Rows[0]["PaymentBankNotes"].ToString());
            param.Add("@PaymentNotes", PaymentInfoTb.Rows[0]["PaymentNotes"].ToString());

            int paymentid = CommonClass.runSql(strpaymentsql, CommonClass.RunSqlInsertMode.SCALAR, param);

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
                param.Add("@PaymentID", paymentid);
                param.Add("@EntityID", NewSalesID);
                param.Add("@Amount", deciamt);
                param.Add("@EntryDate", trandate);

                CommonClass.runSql(strpaymentlnesql, CommonClass.RunSqlInsertMode.QUERY, param);

                //PaymentCommon.UpdateSalesRecord(trandate, invoicenum, deciamt);
            }

            return paymentid;
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

                if (PaymentCommon.CreateJournalEntriesCD(lPaymentID, "Payment " + gPaymentNo + customerText.Text))
                {
                    TransactionClass.UpdateProfileBalances(CustomerID, PaidToday_txt.Value * -1);
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Payment Transaction No. " + gPaymentNo, lPaymentID.ToString());
                }
            }
            return lPaymentID;
        }

        private int TransferCustomerDeposit(string pSalesNo, decimal pAmountApplied, decimal pDiscount)
        {
            int lPaymentID = 0;
            PaymentCommon.GeneratePaymentNumber(ref CurSeries, ref gPaymentNo, "SP");

            if (gPaymentNo != "")
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                lPaymentID = CreatePaymentRecordFromCD(gPaymentNo);
                PaymentCommon.UpdatePaymentNumber(ref CurSeries, "SP");

                if (PaymentCommon.CreateJournalEntriesSP(lPaymentID, "Transfer from Customer Deposit ;" + customerText.Text))
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Payment Transaction No. " + gPaymentNo, lPaymentID.ToString());
                }
            }
            return lPaymentID;
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            TransactionsLookup TLookup = new TransactionsLookup("Invoice", XsalesID);
            TLookup.ShowDialog();
        }

        private void btnUseRecurring_Click(object sender, EventArgs e)
        {
            UseRecurring lUseRecurringForm = new UseRecurring(CommonClass.InvocationSource.SALES);
            lUseRecurringForm.MdiParent = this.MdiParent;
            lUseRecurringForm.Show();
        }

        private void btnSaveRecurring_Click_1(object sender, EventArgs e)
        {
            string lTranType;
            switch (salestype)
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
                        if (dgEnterSales.Rows[i].Cells["Ship"].Value != null && dgEnterSales.Rows[i].Cells["Ship"].Value.ToString() != "")
                        {
                            lShipQty = 0;//Convert.ToDecimal(dgEnterSales.Rows[i].Cells["Ship"].Value.ToString());
                        }
                        lItemID = dgEnterSales.Rows[i].Cells["ItemID"].Value.ToString();
                        lPartNumber = dgEnterSales.Rows[i].Cells["PartNumber"].Value.ToString();

                        lResTb = GetNewEndingQty(lItemID);
                        if (lResTb.Rows.Count > 0)
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
            switch (salestype)
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
            Dictionary<string, object> param = new Dictionary<string, object>();
            DataTable dtRecur = new DataTable();
            string selectSql_ua = "SELECT * FROM Recurring WHERE EntityID = " + XsalesID + " AND TranType = '" + lTranType + "'";
            CommonClass.runSql(ref dtRecur, selectSql_ua);
            if (dtRecur.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecur.Rows.Count; i++)
                {
                    frequency = (dtRecur.Rows[i]["Frequency"].ToString());
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

                string sql = @"UPDATE Recurring SET NotifyDate = @newNotifyDate WHERE EntityID = " + XsalesID + " AND TranType = '" + lTranType + "'";
                param.Add("@newNotifyDate", sdate);
                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
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
            DataTable RTb = null;
            string sql = @"SELECT * FROM SalesLines WHERE SalesID = " + pSalesID + " AND EntityID = " + pItemID;
            RTb = new DataTable();
            CommonClass.runSql(ref RTb, sql);
            decimal lShipQty = 0;
            if (RTb.Rows.Count > 0)
            {
                lShipQty = Convert.ToDecimal(RTb.Rows[0]["ShipQty"].ToString());
            }
            return lShipQty;
        } //END

        private void PaymentMethodLookup()
        {
            PaymentMethodLookup PMLup = new PaymentMethodLookup();
            DataGridViewRow dgvRows = dgEnterSales.CurrentRow;
            if (PMLup.ShowDialog() == DialogResult.OK)
            {
                string[] lPMLup = PMLup.GetPaymentMethod;
                dgvRows.Cells["Payment"].Value = lPMLup[1];
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
                        ShowItemLookup("", "");
                        itemcalc(e.RowIndex);
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                    }
                    dgEnable();
                    break;
                case 4:
                    this.dgEnterSales.CurrentCell = this.dgEnterSales.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgEnterSales.BeginEdit(true);
                    break;
                case 7: //GL ACCOUNT
                    //if (customerText.Text != "")
                    //{
                    //    ShowAccountLookup("");
                    //}
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
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                    }
                    break;
                case 20://Payment
                    if (customerText.Text != "")
                    {
                        PaymentMethodLookup();
                    }
                    break;
                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }

        private void dgEnterSales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
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
                    }
                }
            }

            if (e.ColumnIndex == 4
                || e.ColumnIndex == 2
                || e.ColumnIndex == 5
                || e.ColumnIndex == 6)
            {
             //   itemcalc(e.RowIndex);
                Recalcline(e.ColumnIndex, e.RowIndex);
                Recalc();
                CalcOutOfBalance();
            }

            if (e.ColumnIndex == 3) //PartNumber
            {
                ClearRowItems(e.RowIndex);
                if (dgEnterSales.CurrentCell.Value != null)
                {
                    ShowItemLookup(dgEnterSales.CurrentCell.Value.ToString(), "");
                    itemcalc(e.RowIndex);
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                }
                else
                {
                    ShowItemLookup("", "");
                    itemcalc(e.RowIndex);
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                }
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

                        Recalc();
                        CalcOutOfBalance();
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

                            Recalc();
                            CalcOutOfBalance();
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
                        Recalc();
                        CalcOutOfBalance();

                    }
                }
                else
                {
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    Recalc();
                    CalcOutOfBalance();

                }

            }

            if (e.RowIndex == (this.dgEnterSales.Rows.Count - 1))
            {
                //REMOVED this.dgEnterSales.Rows.Add();
            }

            subtotalAmountText.Visible = true;
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
                    if (this.record_btn.Text != "REVERSE")
                    {
                        this.dgEnterSales.CurrentCell.ReadOnly = false;
                    }
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

        private void dgEnterSales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 //Price
             || e.ColumnIndex == 7 //Amount
             && e.RowIndex != this.dgEnterSales.NewRowIndex)
            {
                if (e.Value != null && e.Value.ToString() != "0")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
                else
                {
                    e.Value = "";
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
            txtPartNum.Select();
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
            if (e.ColumnIndex == 4)
            {
                if (txtQTY.Text != "")
                {
                    itemcalc(e.RowIndex);
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    Recalc();
                    CalcOutOfBalance();

                }
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            else
            {
                if (e.ColumnIndex == 6)
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
                        }
                    }
                    itemcalc(e.RowIndex);
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    Recalc();
                    CalcOutOfBalance();

                }
                //if (e.ColumnIndex == 5)
                //{


                //}
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNextTab_Click(object sender, EventArgs e)
        {
            //DataGridViewRow dgcurr = dgEnterSales.CurrentRow;
            //if (dgcurr.Cells[1].Value != null)
            //{
            //    dgcurr.Cells[1].Value = txtQTY.Text;

            //}
            //else
            //{
            //    MessageBox.Show("No Quantity Added for this item!");
            //}           
        }

        private void pbAccount_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            SetPartName(txtPartNum.Text);
        }


        private void btnremove_Click(object sender, EventArgs e)
        {
            if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
            {
                VoidValidation DlgVoid = new VoidValidation("Remove Line Override");
                if (DlgVoid.ShowDialog() == DialogResult.OK)
                {
                    password = DlgVoid.GetPassword;
                    username = DlgVoid.GetUsername;
                    foreach (DataGridViewCell oneCell in dgEnterSales.SelectedCells)
                    {
                        if (oneCell.RowIndex >= 0 && oneCell.RowIndex < (dgEnterSales.Rows.Count))
                            dgEnterSales.Rows.RemoveAt(oneCell.RowIndex);
                    }
                    CalcOutOfBalance();
                    dgEnterSales.Refresh();
                    //dgEnterSales.Rows[rowin].Selected = true;
                }
            }
            else
            {
                foreach (DataGridViewCell oneCell in dgEnterSales.SelectedCells)
                {
                    if (oneCell.RowIndex >= 0 && oneCell.RowIndex < (dgEnterSales.Rows.Count))
                        dgEnterSales.Rows.RemoveAt(oneCell.RowIndex);
                }
                CalcOutOfBalance();
                dgEnterSales.Refresh();
                //dgEnterSales.Rows[rowin].Selected = true;
            }
        }
        public bool checksale()
        {
            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
            {
                if (dgEnterSales.Rows.Count > 0)
                {
                    if (dgvr.Cells["AccountID"].Value != null && dgvr.Cells["AccountID"].Value.ToString() != "")
                        return true;
                }

            }
            return false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (QuickSalesTabControl.SelectedIndex == 2)
            {
                btnNext.Visible = false;
            }
            QuickSalesTabControl.SelectedIndex += 1;
        }

        private void BalanceDue_txt_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadPaymentMethods()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand("SELECT PaymentMethod, ID FROM PaymentMethods ", con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd_;
                DataTable ldt = new DataTable();
                da.Fill(ldt);
                dgPaymentMethod.Rows.Clear();
                int lctr = 0;
                for (int i = 0; i < ldt.Rows.Count; i++)
                {
                    DataRow dr = ldt.Rows[i];
                    dgPaymentMethod.Rows.Add();
                    dgPaymentMethod.Rows[i].Cells[0].Value = dr["ID"].ToString();
                    dgPaymentMethod.Rows[i].Cells[2].Value = dr["PaymentMethod"].ToString();
                    //    dgPaymentMethod.Rows[i].Cells[3].Value = "";
                    if (dr["PaymentMethod"].ToString().ToUpper() == "CASH")
                    {
                        ChangePaymentMethodID = dr["ID"].ToString();
                    }
                    LoadPaymentLogo(dr["PaymentMethod"].ToString(), i);
                    if (dr["PaymentMethod"].ToString().ToUpper() == "GC")
                    {
                        GCRowID = lctr;
                    }
                    lctr++;


                }
                if (ldt.Rows.Count > 0)
                    dgPaymentMethod.Rows[0].Selected = true;

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
        void LoadPaymentLogo(string PayM, int index)
        {
            int width = 50;
            int height = 47;
            Image img = null;
            Image ResizeImg = null;
            switch (PayM)
            {
                case "Cash"://cash
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.Cash);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;

                case "Cheque": //cheque
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.Cheque);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
                case "Complimentary": //cheque
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.business_and_finance);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;

                case "Credit Card": //credit
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.credit_card);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
                case "Debit Card": //Debit Card
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.pay);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
                case "Direct Deposit":  //
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.DirectDeposit);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
                case "Eftpos":   //
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.eftpos);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
                case "GC":   //GC
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.gift_card);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
                case "Other":    //
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.Others);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;

                case "Paypal":   //paypal
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.paypal);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;

                case "Salary Sacrifice":    //
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.SalarySacrifice);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;

                case "Staff Deduction":   //
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.staffDeduction);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
                case "Voucher":   //
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.coupon);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;

                default:
                    img = new Bitmap(global::AbleRetailPOS.Properties.Resources.Others);
                    ResizeImg = new Bitmap(img, width, height);
                    dgPaymentMethod.Rows[index].Cells[1].Value = ResizeImg;
                    break;
            }
        }


        private void QuickSalesTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (QuickSalesTabControl.SelectedIndex == 1)
            {
                btnNext.Visible = false;
            }
            else
            {
                btnNext.Visible = true;
            }
            if (QuickSalesTabControl.SelectedIndex != 0)
            {
                TotalAmount.Visible = true;
                AmountChange.Visible = true;
                label10.Visible = true;
                label9.Visible = true;
            }
        }
        public bool CheckExistItemInGrid(string pNum)
        {
            int shipCount = 0;
            if (dgEnterSales.Rows.Count > 0)
            {
                for (int i = 0; i < dgEnterSales.Rows.Count; i++)
                {
                    DataGridViewRow dgvr = dgEnterSales.Rows[i];
                    if (dgvr.Cells["PartNumber"].Value != null)
                    {
                        if (dgvr.Cells["PartNumber"].Value.ToString() == pNum)
                        {
                            if (dgvr.Cells["Price"].Value != null && dgvr.Cells["Price"].Value.ToString() != "0")//free Table validation
                            {
                                shipCount += 1;
                                changeqty = true;
                                dgvr.Cells["Ship"].Value = int.Parse(dgvr.Cells["Ship"].Value.ToString()) + 1;

                                if (dgvr.Cells["AutoBuild"].Value != null && bool.Parse(dgvr.Cells["AutoBuild"].Value.ToString()))
                                {
                                    int lTempIndex = dgEnterSales.CurrentRow.Index;
                                    if (dgvr.Cells["BundleType"].Value.ToString() == "Dynamic")
                                        IsAutoBuild(int.Parse(dgvr.Cells["ItemID"].Value.ToString()), i);
                                }
                                else
                                {
                                    VerifyItemQty(i, (XsalesID == "" ? "0" : XsalesID));
                                }
                                itemcalc(i);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            string itemName = txtItemName.Text;
            string WhereCon = "ItemName";
            if(customerText.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    PopulateDataGridView();
                    DataGridViewRow dgcur = dgEnterSales.Rows[rowin];
                    txtQTY.Visible = true;
                    lbItemQty.Visible = true;
                    // if (txtQTY.Text != "")
                    //{
                    int cur = dgcur.Index;

                    if (customerText.Text != "")
                    {
                        ShowItemLookup(itemName, WhereCon);
                        dgcur.Cells["Ship"].Value = txtQTY.Text;
                        if (float.Parse(txtQTY.Text) < 0)
                        {

                        }
                        itemcalc(cur);
                        for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                        {
                            if (this.dgEnterSales.Rows[i].Cells["Amount"] != null)
                            {
                                Recalcline(1, i);
                            }
                        }
                    }
                    dgEnterSales.ClearSelection();
                    if (rowin < dgEnterSales.RowCount)
                    {
                        dgEnterSales.Rows[rowin].Cells["Ship"].Selected = false;
                        dgEnterSales.Rows[++rowin].Cells["Ship"].Selected = true;
                    }
                    txtPartNum.Text = "";
                    //txtQTY.Text = "1";
                  
                    txtPartNum.Select();
                    CalcOutOfBalance();
                }
            }
           
        }
        private void txtPartNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (customerText.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string lqty = txtQTY.Text == "" ? "1" : txtQTY.Text;
                    txtQTY.Text = lqty;
                    SetPartName(txtPartNum.Text);
                }
            }

        }

        public void SetPartName(string pPartNum)
        {
            string ptnum = pPartNum;
            string WhereCon = "PartNumber";
          
            txtQTY.Visible = true;
            lbItemQty.Visible = true;

            
            int lastFilledRow = 0;
            if (customerText.Text != "")
            {
                ShowItemLookup(ptnum, WhereCon);
                int cur = dgEnterSales.Rows.Count - 1;
                dgEnterSales.Rows[cur].Selected = true;
                itemcalc(cur);
                changeqty = false;
                for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                {
                    if (this.dgEnterSales.Rows[i].Cells["PartNumber"] != null)
                    {
                        Recalcline(1, i);
                        lastFilledRow = i;
                        //rowin = lastFilledRow -1;
                    }
                }
                //REMOVED dgEnterSales.Rows.Add();
            }
            txtPartNum.Text = "";
            txtQTY.Text = "1";
           
            
            txtPartNum.Select();
            CalcOutOfBalance();
            dgEnterSales.RefreshEdit();
            if (lastFilledRow  < dgEnterSales.Rows.Count)
            {
                dgEnterSales.FirstDisplayedScrollingRowIndex = lastFilledRow;
            }


            
        }
        
        private void txtQTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (customerText.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgEnterSales.Rows[rowin].Cells["Ship"].Value = txtQTY.Text;
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int lrowin = dgEnterSales.CurrentRow.Index;
            dgEnterSales.Rows[lrowin].Selected = false;
            lrowin--;
            if (lrowin >= 0 && lrowin < dgEnterSales.Rows.Count)
            {
                
                dgEnterSales.Rows[lrowin].Selected = true;
            }

            dgEnterSales.Refresh();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int lrowin = dgEnterSales.CurrentRow.Index;
            dgEnterSales.Rows[lrowin].Selected = false;
            lrowin++;
            if (lrowin >= 0 && lrowin < dgEnterSales.Rows.Count - 1)            {
               
                dgEnterSales.Rows[lrowin].Selected = true;
            }
            dgEnterSales.Refresh();
        }
        void LoadDefaultCustomer()
        {
            if (!CommonClass.MandatoryCustomer)
            {
                DataTable dt = new DataTable();
                string sqldefcust = @"Select p.*, isnull(s.ShippingID,0) as ShippingID FROM Profile p INNER JOIN Contacts c ON p.ID = c.ProfileID 
                                LEFT JOIN ShippingMethods s on p.ShippingMethodID = s.ShippingMethod WHERE ID = " + CommonClass.DefaultCustomerID;
                
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
                    ShippingMethodID = (c["ShippingID"].ToString() == "" ? "0" : c["ShippingID"].ToString());
                    InvoiceNumTxt.Visible = true;
                    cmb_shippingcontact.SelectedIndex = Convert.ToInt16(c["LocationID"].ToString()) - 1;
                    LoadContacts(Convert.ToInt32(CustomerID), this.cmb_shippingcontact.SelectedIndex + 1);
                    LoadFreightTax(ProfileTax);
                    LoadPoints();
                    FormCheck();
                }
            }
        }


        private float ComputePromo(string accumulation, float points, float pointsvalue, float PurchasePrice, int promoID, int CRowIndex)
        {
            float ltaxexprice = 0;
            if(CRowIndex != dgEnterSales.CurrentRow.Index)
            {
                dgEnterSales.Rows[CRowIndex].Selected = true;
            }
            
            DataGridViewRow dgvRows = dgEnterSales.Rows[CRowIndex];
            //IF DEFAULT PROMOTYPE, GET THE FINAL PRICE OF THE ITEM
            if (CommonClass.IsTaxcInclusiveEnterSales)
            {
                if (dgvRows.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx / (1 + (lTaxRate / 100));
                    ltaxexprice = PurchasePrice / (1 + (lTaxRate / 100));
                }
            }
            else
            {
                if (dgEnterSales.CurrentRow.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx;
                    ltaxexprice = PurchasePrice;
                }
            }

            if (accumulation == "Points (X)")
            {
                points += pointsvalue * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());
            }
            else if (accumulation == "Points (X) percentage")
            {
                points = ((pointsvalue / 100) * ltaxexprice) * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());
            }

            else if (accumulation == "Points (X) percentage profit")
            {
                //Get the total cost (costprice)
                float costprice = float.Parse(dgvRows.Cells["CostPrice"].Value == null ? "0" : dgvRows.Cells["CostPrice"].Value.ToString());
                //get the profit
                float profit = ltaxexprice - costprice;
                points = ((pointsvalue / 100) * profit) * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());
            }

            float lOrigUnitPrice = 0;
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
                if (CommonClass.IsTaxcInclusiveEnterSales)
                {
                    float lTaxRate = float.Parse(dgEnterSales.CurrentRow.Cells["TaxRate"].Value.ToString());
                    difPrice = (ltaxexprice * (1 + (lTaxRate / 100))) - pointsvalue;
                }

                if (PurchasePrice > difPrice)
                {
                    //Change only if difprice will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = difPrice;
                }

                points = 0;
            }
            else if (accumulation == "Percentage Discount")
            {
                float discountedprice = ltaxexprice - (ltaxexprice * (pointsvalue / 100));
                if (CommonClass.IsTaxcInclusiveEnterSales)
                {
                    float lTaxRate = float.Parse(dgEnterSales.CurrentRow.Cells["TaxRate"].Value.ToString());
                    discountedprice = discountedprice * (1 + (lTaxRate / 100));
                }
                if (PurchasePrice > discountedprice)
                {
                    //Change only if pointsvalue will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = discountedprice;
                }

                points = 0;
            }
            else if (accumulation == "Price (X)")
            {
                if (PurchasePrice > pointsvalue)
                {
                    //Change only if pointsvalue will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = pointsvalue;
                }

                points = 0;
            }
            else if (accumulation == "Free Product")
            {
                int lNewRowindex;
                if (dgvRows.Cells["PartNumber"].ToString() != "")
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
                                dgEnterSales.Rows.Add();
                                lNewRowindex = dgEnterSales.Rows.Count - 1;
                                
                                dgEnterSales.Rows[lNewRowindex].Cells["ItemID"].Value = dgRow["ItemID"].ToString();
                               
                                dgEnterSales.Rows[lNewRowindex].Cells["PartNumber"].Value = dgRow["PartNum"].ToString();
                                dgEnterSales.Rows[lNewRowindex].Cells["Description"].Value = "Free Product " + dgRow["ItemName"].ToString();
                                dgEnterSales.Rows[lNewRowindex].Cells["Price"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["ActualUnitPrice"].Value = float.Parse(dgRow["Price"].ToString());
                                dgEnterSales.Rows[lNewRowindex].Cells["Amount"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxExclusiveAmount"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxInclusiveAmount"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["Ship"].Value = 1;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxCode"].Value = dgRow["SalesTaxCode"].ToString() == "" ? "N-T" : dgRow["SalesTaxCode"].ToString();
                                DataRow rTx = CommonClass.getTaxDetails(dgEnterSales.Rows[lNewRowindex].Cells["TaxCode"].Value.ToString());
                                //if (rTx.ItemArray.Length > 0)
                                //{
                                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                                string lTaxPaidAccountID = "";
                                lTaxPaidAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxCollectedAccountID"].Value = lTaxPaidAccountID;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxRate"].Value = ltaxrate;
                                dgEnterSales.Rows[lNewRowindex].Cells["PromoID"].Value = promoID;
                                dgEnterSales.Rows[lNewRowindex].Cells["CostPrice"].Value = float.Parse(dgRow["CostPrice"].ToString());
                                Recalcline(8, lNewRowindex);

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
                float listprice = GetItemListPrice(dgvRows.Cells["ItemID"].Value.ToString());
                float percentDiscount = ((pointsvalue / 100) * listprice) * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());

                float discounted = listprice - percentDiscount;
                if (PurchasePrice > discounted)
                {
                    //Change only if discounted will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = discounted;
                }
                points = 0;
            }

            dgvRows.Cells["PromoID"].Value = promoID;
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

        private float isItemPromo(int ItemID, float PurchasePrice, int CRowIndex,string pType = "")
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
                                    return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                }
                            }
                            else if (promo.CriteriaName == "Supplier")
                            {
                                if (dgEnterSales.Rows[CRowIndex].Cells["Supplier"].Value != null)
                                {
                                    if (dgEnterSales.Rows[CRowIndex].Cells["Supplier"].Value.ToString() == promo.CriteriaValue)
                                    {
                                        return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                    }
                                }
                            }
                            else if (promo.CriteriaName == "Category")
                            {
                                if (dgEnterSales.Rows[CRowIndex].Cells["CategoryID"].Value != null)
                                {
                                    if (dgEnterSales.Rows[CRowIndex].Cells["CategoryID"].Value.ToString() == promo.CriteriaValue)
                                    {
                                        return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                    }
                                }
                            }                           
                            else if (promo.CriteriaName == "Brand")
                            {
                                if (dgEnterSales.Rows[CRowIndex].Cells["Brand"].Value != null) {
                                        if (dgEnterSales.Rows[CRowIndex].Cells["Brand"].Value.ToString() == promo.CriteriaValue)
                                        {
                                            return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                        }
                                    }
                            }
                        }
                    }
                }
            }

            return points;
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
            else
            {
                txtCustPoints.Text = "0";
            }
        }

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
                CommonClass.PointRedemption = new PointsRedemption(CommonClass.RedemptionType.GIFTCERTIFICATE, null, null, this, CustomerID, customerText.Text);
                
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

        private void btnAddShipAddress_Click(object sender, EventArgs e)
        {
            ContactsFillUpForm ContactsDlg = new ContactsFillUpForm(ContactInfoTb, CustomerID);//PaymentInfoTb, this.TotalAmount.Value, CanEdit);
            if (ContactsDlg.ShowDialog() == DialogResult.OK)
            {
                ContactInfoTb = ContactsDlg.GetContactInfo;
                PayeeInfo.Text = ContactsDlg.GetPayeeInfo;
                cmb_shippingcontact.SelectedIndex = 1;
                shipAddressID = cmb_shippingcontact.SelectedIndex;
            }
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

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnDefaultSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
        void TenderCalc()
        {
            decimal amt = 0;
            for (int i = 0; i < dgPaymentMethod.Rows.Count; i++)
            {
                if (dgPaymentMethod.Rows[i].Cells["TenderAmount"].Value != null)
                {
                    if (this.dgPaymentMethod.Rows[i].Cells["TenderAmount"].Value.ToString() != "")
                    {
                        amt += decimal.Parse(dgPaymentMethod.Rows[i].Cells["TenderAmount"].Value.ToString());
                    }
                }
            }
            PaidToday_txt.Value = amt;
            AmountChange.Value = 0;
            if (PaidToday_txt.Value > TotalAmount.Value)
            {
                AmountChange.Value = PaidToday_txt.Value - TotalAmount.Value;
            }
            BalanceDue_txt.Value = TotalAmount.Value - (PaidToday_txt.Value - AmountChange.Value);
        }

        private void record_btn_Click(object sender, EventArgs e)
        {
            if (customerText.Text != "" || checksale())
            {
                TendertoTable();
                salestype = "INVOICE";
                float TotalEndingBalance = 0;
                if (XsalesID == "" || SrcOfInvoke == CommonClass.InvocationSource.USERECURRING || SrcOfInvoke == CommonClass.InvocationSource.REMINDER)
                {
                    if (salestype == "INVOICE")
                    {
                        string tAmount = String.Format("{0:0.##}", TotalAmount.Value);
                        string pAmount = String.Format("{0:0.##}", PaidToday_txt.Value - AmountChange.Value);
                        if (pAmount == tAmount)
                        {
                            SaveQuickSale();
                        }
                        else
                        {
                            MessageBox.Show("Invoice must be paid in full.");
                        }

                        //TotalEndingBalance = CustomerBalance + (float)this.BalanceDue_txt.Value;
                        //if (TotalEndingBalance <= CustomerCreditLimit) //Check first if the invoice will not exceed the Credit Limit
                        //{
                        //    if (BalanceDue_txt.Value < 0)
                        //    {
                        //        decimal CustChaneg = PaidToday_txt.Value - TotalAmount.Value;
                        //        MessageBox.Show("Customer Change is " + CustChaneg.ToString());
                        //    }
                        //    SaveQuickSale();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("This invoice will exceed the Customer's Credit Limit.");
                        //    string selectSupervisor = "SELECT * FROM Users";
                        //    DataTable dtSupervisors = new DataTable();

                        //    string titles = "Information";
                        //    DialogResult OverrideCreditLimit = MessageBox.Show("Would you like to override the Credit Limit?", titles, MessageBoxButtons.YesNo);
                        //    if (OverrideCreditLimit == DialogResult.Yes)
                        //    {
                        //        VoidValidation DlgVoid = new VoidValidation();
                        //        if (DlgVoid.ShowDialog() == DialogResult.OK)
                        //        {
                        //            password = DlgVoid.GetPassword;
                        //            username = DlgVoid.GetUsername;
                        //        }
                        //        selectSupervisor += " WHERE user_name = '" + username + "' AND user_pwd = '" + CommonClass.SHA512(password) + "'";
                        //        CommonClass.runSql(ref dtSupervisors, selectSupervisor);

                        //        if (dtSupervisors.Rows.Count > 0 && dtSupervisors.Rows[0]["user_role"].ToString() == "Supervisor")
                        //        {
                        //            SaveQuickSale();
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (BalanceDue_txt.Value < 0)
                        {
                            decimal CustChaneg = PaidToday_txt.Value - TotalAmount.Value;
                            MessageBox.Show("Customer Change is " + CustChaneg.ToString());
                        }
                        SaveQuickSale();
                    }
                }

            }
        }
        private void record_btn_ClickOLD(object sender, EventArgs e)
        {
            if (customerText.Text != "" || checksale())
            {
                TendertoTable();
                salestype = "INVOICE";
                float TotalEndingBalance = 0;
                if (XsalesID == "" || SrcOfInvoke == CommonClass.InvocationSource.USERECURRING || SrcOfInvoke == CommonClass.InvocationSource.REMINDER)
                {
                    if (salestype == "INVOICE")
                    {
                        TotalEndingBalance = CustomerBalance + (float)this.BalanceDue_txt.Value;
                        if (TotalEndingBalance <= CustomerCreditLimit) //Check first if the invoice will not exceed the Credit Limit
                        {
                            if (BalanceDue_txt.Value < 0)
                            {
                                decimal CustChaneg = PaidToday_txt.Value - TotalAmount.Value;
                                MessageBox.Show("Customer Change is " + CustChaneg.ToString());
                            }
                            SaveQuickSale();
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
                                    SaveQuickSale();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (BalanceDue_txt.Value < 0)
                        {
                            decimal CustChaneg = PaidToday_txt.Value - TotalAmount.Value;
                            MessageBox.Show("Customer Change is " + CustChaneg.ToString());
                        }
                        SaveQuickSale();
                    }
                }

            }
        }

        private void dgPaymentMethod_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                TenderCalc();
            }
        }

        private void dgPaymentMethod_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }
        void TendertoTable()
        {
            int x = 0;
            foreach (DataGridViewRow item in dgPaymentMethod.Rows)
            {
                if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                {
                    if (x != 0)
                    {
                        PaymentInfoTb.Rows.Add();
                    }
                    PaymentInfoTb.Rows[x]["RecipientAccountID"] = AR_AccountID;
                    PaymentInfoTb.Rows[x]["AmountPaid"] = item.Cells[3].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentMethodID"] = item.Cells[0].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentMethod"] = item.Cells[2].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentAuthorisationNumber"] = item.Cells[4].Value == null ? "" : item.Cells[4].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentCardNumber"] = item.Cells[5].Value == null ? "" : item.Cells[5].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentNameOnCard"] = item.Cells[6].Value == null ? "" : item.Cells[6].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentExpirationDate"] = item.Cells[7].Value == null ? "" : item.Cells[7].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentCardNotes"] = item.Cells[8].Value == null ? "" : item.Cells[8].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBSB"] = item.Cells[9].Value == null ? "" : item.Cells[9].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBankAccountNumber"] = item.Cells[10].Value == null ? "" : item.Cells[10].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBankAccountName"] = item.Cells[11].Value == null ? "" : item.Cells[11].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentChequeNumber"] = item.Cells[12].Value == null ? "" : item.Cells[12].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBankNotes"] = item.Cells[13].Value == null ? "" : item.Cells[13].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentNotes"] = item.Cells[14].Value == null ? "" : item.Cells[14].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentGCNo"] = item.Cells[15].Value == null ? "" : item.Cells[15].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentGCNotes"] = item.Cells[16].Value == null ? "" : item.Cells[16].Value.ToString();
                    x++;
                }
            }
            if (AmountChange.Value != 0)
            {
                PaymentInfoTb.Rows.Add();
                PaymentInfoTb.Rows[x]["RecipientAccountID"] = AR_AccountID;
                PaymentInfoTb.Rows[x]["AmountPaid"] = AmountChange.Value * (-1);
                PaymentInfoTb.Rows[x]["PaymentMethodID"] = ChangePaymentMethodID;
                PaymentInfoTb.Rows[x]["PaymentMethod"] = "";
                PaymentInfoTb.Rows[x]["PaymentAuthorisationNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentCardNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentNameOnCard"] = "";
                PaymentInfoTb.Rows[x]["PaymentExpirationDate"] = "";
                PaymentInfoTb.Rows[x]["PaymentCardNotes"] = "";
                PaymentInfoTb.Rows[x]["PaymentBSB"] = "";
                PaymentInfoTb.Rows[x]["PaymentBankAccountNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentBankAccountName"] = "";
                PaymentInfoTb.Rows[x]["PaymentChequeNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentBankNotes"] = "";
                PaymentInfoTb.Rows[x]["PaymentNotes"] = "";
                PaymentInfoTb.Rows[x]["PaymentGCNo"] = "";
                PaymentInfoTb.Rows[x]["PaymentGCNotes"] = "";

            }
        }
        void MethodValues()
        {
            if (PaymentInfoTb != null)
            {
                //   txtAuthorization.Text = PaymentInfoTb.Rows[0]["PaymentAuthorisationNumber"].ToString();
                txtCardNo.Text = PaymentInfoTb.Rows[0]["PaymentCardNumber"].ToString();
                txtCardName.Text = PaymentInfoTb.Rows[0]["PaymentNameOnCard"].ToString();
                txtCardExpiry.Text = PaymentInfoTb.Rows[0]["PaymentExpirationDate"].ToString();
                txtCardNotes.Text = PaymentInfoTb.Rows[0]["PaymentCardNotes"].ToString();
                txtBankBSB.Text = PaymentInfoTb.Rows[0]["PaymentBSB"].ToString();
                txtBankAccountName.Text = PaymentInfoTb.Rows[0]["PaymentBankAccountNumber"].ToString();
                txtBankAccountNo.Text = PaymentInfoTb.Rows[0]["PaymentBankAccountName"].ToString();
                txtChequeNo.Text = PaymentInfoTb.Rows[0]["PaymentChequeNumber"].ToString();
                txtBankNotes.Text = PaymentInfoTb.Rows[0]["PaymentBankNotes"].ToString();
                txtPaymentNotes.Text = PaymentInfoTb.Rows[0]["PaymentNotes"].ToString();
                //AR_AccountID = PaymentInfoTb.Rows[0]["RecipientAccountID"].ToString();
                txtGCNo.Text = PaymentInfoTb.Rows[0]["PaymentGCNo"].ToString();
                txtGCNotes.Text = PaymentInfoTb.Rows[0]["PaymentGCNotes"].ToString();
                //   AmountChange.Value = ChangeAmount;
            }
        }

        private void btnLoyalty_Click(object sender, EventArgs e)
        {

        }
        void LoadMember(int ProfileID)
        {
            
            string membersql = @"Select p.*, isnull(s.ShippingID,0) as ShippingID FROM Profile p INNER JOIN Contacts c ON p.ID = c.ProfileID 
                                LEFT JOIN ShippingMethods s on p.ShippingMethodID = s.ShippingMethod WHERE ID = " + ProfileID;
            DataTable memberdt = new DataTable();
            CommonClass.runSql(ref memberdt, membersql);
            if (memberdt.Rows.Count > 0)
            {
                DataRow c = memberdt.Rows[0];
                CustomerID = c["ID"].ToString();
                this.customerText.Text = c["Name"].ToString();
                ShippingmethodText.Text = c["ShippingMethodID"].ToString();
                TermRefID = c["TermsOfPayment"].ToString();
                ProfileTax = c["TaxCode"].ToString();
                MethodPaymentID = c["MethodOfPaymentID"].ToString();
                CustomerBalance = (c["CurrentBalance"].ToString() == "" ? 0 : float.Parse(c["CurrentBalance"].ToString(), NumberStyles.Currency));
                CustomerCreditLimit = (c["CreditLimit"].ToString() == "" ? 0 : float.Parse(c["CreditLimit"].ToString(), NumberStyles.Currency));
                //TermsText.Visible = true;
               // MemoText.Text = "Sale; " + c["Name"].ToString();
                ShippingMethodID = (c["ShippingID"].ToString() == "" ? "0" : c["ShippingID"].ToString());
                InvoiceNumTxt.Visible = true;
                //cmb_shippingcontact.SelectedIndex = Convert.ToInt16(c["LocationID"].ToString()) - 1;

                string sql = "SELECT ContactID, TypeOfContact FROM Contacts WHERE ProfileID = @ProfileID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProfileID", CustomerID);
                DataTable dt2 = new DataTable();
                CommonClass.runSql(ref dt2, sql, param);
                List<KeyValuePair<string, string>> mylist = new List<KeyValuePair<string, string>>();
                if (memberdt.Rows.Count > 0)
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
               // LoadDefaultTerms(CustomerID);
                LoadFreightTax(ProfileTax);
               // LoadPaymentMethod();
                LoadPoints();
                FormCheck();
              //  ApplyVoidAccess();
            }

        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            string loyaltysql = "SELECT * FROM LoyaltyMember";
            DataTable loyaldt = new DataTable();
            CommonClass.runSql(ref loyaldt, loyaltysql);
            memberID = CustomerID;
            member = Interaction.InputBox("Please Enter Member Loyalty Number", "Member Loyalty", "");
            if (loyaldt.Rows.Count > 0)
            {
                for (int i = 0; i < loyaldt.Rows.Count; i++)
                {
                    DataRow dr = loyaldt.Rows[i];

                    if (member == dr["Number"].ToString())
                    { 
                        ismember = true;
                        memberID = dr["ID"].ToString();
                        LoadMember(int.Parse(dr["ProfileID"].ToString()));
                        btnRedeem.Visible = true;
                    }
                }
            }
            if (ismember)
            {
                txtCustPoints.Visible = true;
                btnRedeem.Visible = true;
                lblPoints.Visible = true;
            }
            else
            {
                txtCustPoints.Visible = false;
                btnRedeem.Visible = false;
                lblPoints.Visible = false;
            }
        }



        private void dgPaymentMethod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                case 2:
                    xMethodID = int.Parse(dgPaymentMethod.Rows[e.RowIndex].Cells[0].Value.ToString());
                    switch (xMethodID)
                    {
                        case 5:
                        case 6:
                        case 10://Card
                            TabPayDetails.SelectedIndex = 0;
                            ((Control)this.tabCardNo).Enabled = true;
                            ((Control)this.tabBankNo).Enabled = false;
                            ((Control)this.tabNotes).Enabled = false;
                            ((Control)tabGC).Enabled = false;
                            break;
                        case 7:
                        case 3:
                        case 12:
                        case 14:
                        case 15:
                        case 13://Note
                            TabPayDetails.SelectedIndex = 3;
                            ((Control)this.tabCardNo).Enabled = false;
                            ((Control)this.tabBankNo).Enabled = false;
                            ((Control)this.tabNotes).Enabled = true;
                            ((Control)tabGC).Enabled = false;
                            break;
                        case 4:
                        case 9://Bank
                            TabPayDetails.SelectedIndex = 1;
                            ((Control)this.tabCardNo).Enabled = false;
                            ((Control)this.tabBankNo).Enabled = true;
                            ((Control)this.tabNotes).Enabled = false;
                            ((Control)tabGC).Enabled = false;
                            break;
                        case 16://GC
                            TabPayDetails.SelectedIndex = 2;
                            ((Control)this.tabCardNo).Enabled = false;
                            ((Control)this.tabBankNo).Enabled = false;
                            ((Control)this.tabNotes).Enabled = false;
                            ((Control)tabGC).Enabled = true;
                            break;
                        default:
                            ((Control)this.tabCardNo).Enabled = true;
                            ((Control)this.tabBankNo).Enabled = true;
                            ((Control)this.tabNotes).Enabled = true;
                            ((Control)tabGC).Enabled = true;
                            break;
                    }
                    break;
                case 3:
                    this.dgPaymentMethod.CurrentCell = this.dgPaymentMethod.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgPaymentMethod.BeginEdit(true);
                    break;
            }
        }

        private void AmountChange_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {

            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn3.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn6.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn9.Text;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btnDot.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value += btn0.Text;
        }

        private void BackSpace_Click(object sender, EventArgs e)
        {
            if (dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value.ToString().Length > 0 || dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value.ToString() != "")
            {
                dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value = dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value.ToString().Remove(dgPaymentMethod.CurrentRow.Cells["TenderAmount"].Value.ToString().Length - 1, 1);
            }
        }

        private void btnEndEdit_Click(object sender, EventArgs e)
        {

            this.dgPaymentMethod.CurrentCell = this.dgPaymentMethod.CurrentRow.Cells["TenderAmount"];
            this.dgPaymentMethod.BeginEdit(true);
            //this.dgPaymentMethod.BeginEdit(false);
            this.dgPaymentMethod.EndEdit();

        }

        private void txtGCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string sql = "SELECT * FROM GiftCertificate WHERE GCNumber=@GCNumber";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@GCNumber", txtGCNo.Text);
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, sql, param);

                if (dt.Rows.Count >= 1)
                {
                    bool isUsed = (bool)dt.Rows[0]["IsUsed"];
                    if (isUsed)
                    {
                        MessageBox.Show("Gift Certificate is already used");
                        return;
                    }

                    DateTime gcsdate = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToLocalTime();
                    DateTime gcedate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToLocalTime();
                    DateTime today = DateTime.Now.ToLocalTime();

                    if (today >= gcsdate || today <= gcedate)
                    {
                        double gcamount = Convert.ToDouble(dt.Rows[0]["GCAmount"]);
                        if (gcamount > 0)
                        {
                            dgPaymentMethod.Rows[GCRowID].Cells["TenderAmount"].Value = gcamount;
                            TenderCalc();
                            txtGCNotes.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Gift Certificate amount is 0");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gift Certificate is already expired");
                        return;
                    }
                }
            }
        }

        private void ItemTabPage_Click(object sender, EventArgs e)
        {

        }
        public bool iSelectedItem()
        {
            bool selecteditem = dgEnterSales.CurrentRow.Selected;
            return selecteditem;
        }
        private void btnPriceOverride_Click(object sender, EventArgs e)
        {
            int lRowIndex = dgEnterSales.CurrentCell.RowIndex;
            rowin = lRowIndex;
            if (dgEnterSales.Rows.Count > 0)
            {
                 
                if (dgEnterSales.Rows[rowin].Cells["PartNumber"].Value != null)
                {
                    if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                    {
                        VoidValidation DlgVoid = new VoidValidation("Price Override");
                        if (DlgVoid.ShowDialog() == DialogResult.OK)
                        {
                            password = DlgVoid.GetPassword;
                            username = DlgVoid.GetUsername;
                            KeyboardOnScreen kbos = new KeyboardOnScreen("Price Value Override", "Enter Price Value : ");
                            if (kbos.ShowDialog() == DialogResult.OK)
                            {
                                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                                string strValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                                if (strValue == "-")
                                {
                                    pValue = strValue + pValue;
                                }
                                dgEnterSales.Rows[rowin].Cells["Price"].Value = pValue;
                            }
                                DataRow rw = dtv.NewRow();
                                rw["UserName"] = DlgVoid.GetUsername;
                                rw["AuditAction"] = "Price override to " + dgEnterSales.Rows[rowin].Cells["Price"].Value + " by " + DlgVoid.GetUsername;
                                dtv.Rows.Add(rw);
                           
                        }
                    }
                    else
                    {
                        KeyboardOnScreen kbos = new KeyboardOnScreen("Price Value Override", "Enter Price Value : ");
                        if (kbos.ShowDialog() == DialogResult.OK)
                        {
                            string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                            if (kbos.GetValue == "-" + pValue)
                            {
                                pValue = "-" + pValue;
                            }
                            dgEnterSales.Rows[rowin].Cells["Price"].Value = pValue;
                        }
                        DataRow rw = dtv.NewRow();
                        rw["AuditAction"] = "Price override to " + dgEnterSales.Rows[rowin].Cells["Price"].Value + " by " + CommonClass.UserName;
                        dtv.Rows.Add(rw);
                    }
                    itemcalc(rowin);
                    Recalcline(5, rowin);
                    Recalc();
                    CalcOutOfBalance();

                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }

        }

     
        private void btnTaxCodeOverride_Click(object sender, EventArgs e)
        {
            int lRowIndex = dgEnterSales.CurrentCell.RowIndex;
            if (dgEnterSales.Rows.Count > 0)
            {
                if (dgEnterSales.Rows[lRowIndex].Cells["PartNumber"].Value != null)
                {
                    if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                    {
                        VoidValidation DlgVoid = new VoidValidation("Tax Code Override");
                        if (DlgVoid.ShowDialog() == DialogResult.OK)
                        {
                            password = DlgVoid.GetPassword;
                            username = DlgVoid.GetUsername;
                            DataGridViewRow dgvRows = dgEnterSales.CurrentRow;
                            TaxCodeLookup TaxCodeL = new TaxCodeLookup("");
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
                    }
                    else
                    {
                        DataGridViewRow dgvRows = dgEnterSales.CurrentRow;
                        TaxCodeLookup TaxCodeL = new TaxCodeLookup("");
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
                    itemcalc(rowin);
                    Recalcline(9, rowin);
                    Recalc();
                    CalcOutOfBalance();

                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            else
            {
                MessageBox.Show("Please select an item");
            }

        }

        private void btnQtyOverride_Click(object sender, EventArgs e)
        {
            int lRowIndex = dgEnterSales.CurrentCell.RowIndex;
            if (dgEnterSales.Rows.Count > 0)
            {
                if (dgEnterSales.Rows[lRowIndex].Cells["PartNumber"].Value != null)
                {
                    if (iSelectedItem())
                    {
                        rowin = dgEnterSales.CurrentCell.RowIndex;
                    }
                    if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                    {
                        VoidValidation DlgVoid = new VoidValidation("Quantity Override");
                        if (DlgVoid.ShowDialog() == DialogResult.OK)
                        {
                            password = DlgVoid.GetPassword;
                            username = DlgVoid.GetUsername;
                            KeyboardOnScreen kbos = new KeyboardOnScreen("Change Quantity Value", "Enter Qty Value : ");
                            if (kbos.ShowDialog() == DialogResult.OK)
                            {
                                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                                string strValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                                if (strValue == "-")
                                {
                                    pValue = strValue + pValue;
                                }
                                
                                dgEnterSales.Rows[rowin].Cells["Ship"].Value =  qtyvalidation(decimal.Parse(pValue));
                            }
                        }
                    }
                    else
                    {
                        KeyboardOnScreen kbos = new KeyboardOnScreen("Change Quantity Value", "Enter Qty Value : ");
                        if (kbos.ShowDialog() == DialogResult.OK)
                        {
                            string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                            if (kbos.GetValue == "-" + pValue)
                            {
                                pValue = "-" + pValue;
                            }
                            changeqty = true;
                            dgEnterSales.Rows[rowin].Cells["Ship"].Value = qtyvalidation(decimal.Parse(pValue));
                        }
                    }
                    if (dgEnterSales.Rows[lRowIndex].Cells["AutoBuild"].Value != null && bool.Parse(dgEnterSales.Rows[lRowIndex].Cells["AutoBuild"].Value.ToString()))
                    {
                        int lTempIndex = dgEnterSales.CurrentRow.Index;
                        if (dgEnterSales.Rows[lRowIndex].Cells["BundleType"].Value.ToString() == "Dynamic")
                            IsAutoBuild(int.Parse(dgEnterSales.Rows[lRowIndex].Cells["ItemID"].Value.ToString()), lRowIndex);
                        
                    }
                    else
                    {
                        VerifyItemQty(lRowIndex, (XsalesID == "" ? "0" : XsalesID));
                    }
                    itemcalc(rowin);
                    Recalcline(4, rowin);

                    Recalc();
                    CalcOutOfBalance();
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }

        }

        private void txtItemName_MouseClick(object sender, MouseEventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Item Name", "Enter Item Name : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                txtItemName.Text = pValue;
                string itemName = txtItemName.Text;
                string WhereCon = "ItemName";

                PopulateDataGridView();
                DataGridViewRow dgcur = dgEnterSales.Rows[rowin];
                txtQTY.Visible = true;
                lbItemQty.Visible = true;
                // if (txtQTY.Text != "")
                //{
                dgcur.Cells["Ship"].Value = txtQTY.Text;
                int cur = dgcur.Index;

                if (customerText.Text != "")
                {
                    ShowItemLookup(itemName, WhereCon);
                    itemcalc(cur);
                    for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                    {
                        if (this.dgEnterSales.Rows[i].Cells["Amount"] != null)
                        {
                            Recalcline(1, i);
                        }
                    }
                }
                dgEnterSales.ClearSelection();
                if (rowin < dgEnterSales.RowCount)
                {
                    dgEnterSales.Rows[rowin].Cells["Ship"].Selected = false;
                    dgEnterSales.Rows[++rowin].Cells["Ship"].Selected = true;
                }
                txtQTY.Text = "";
                txtPartNum.Text = "";
                txtPartNum.Select();
                CalcOutOfBalance();
            }
            txtQTY.Text = "1";
        }

        private void txtBarCode_MouseClick(object sender, MouseEventArgs e)
        {
            //KeyboardOnScreen kbos = new KeyboardOnScreen("Barcode", "Enter Barcode : ");
            //if (kbos.ShowDialog() == DialogResult.OK)
            //{
            //    string pValue = kbos.GetValue.ToString();
            //    txtBarCode.Text = pValue;
            //    string barsql = "SELECT * from Barcodes b Inner join Items i ON i.ID = b.ItemID";
            //    DataTable dt = new DataTable();
            //    CommonClass.runSql(ref dt, barsql);

            //    for (int x = 0; x < dt.Rows.Count; x++)
            //    {
            //        DataRow dr = dt.Rows[x];

            //        if (txtBarCode.Text == CommonClass.base64Decode(dr["BarcodeData"].ToString()))
            //        {
            //            txtPartNum.Text = dr["PartNumber"].ToString();
            //            //txtPartNum.KeyDown += txtItemName_KeyDown;
            //            SetPartName();
            //        }
            //    }
            //    txtQTY.Text = "1";
            //}
        }

        private void txtPartNum_MouseClick(object sender, MouseEventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Item Part Number", "Enter Part Number : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                txtPartNum.Text = pValue;
                SetPartName(txtPartNum.Text);
                txtQTY.Text = "1";
            }
        }

        private void txtPartNum_Click(object sender, EventArgs e)
        {
            if (customerText.Text != "")
            {
                KeyboardOnScreen kbos = new KeyboardOnScreen("Item Part Number", "Enter Part Number : ");
                if (kbos.ShowDialog() == DialogResult.OK)
                {
                    string pValue = kbos.GetValue;
                    txtPartNum.Text = pValue;
                    SetPartName(txtPartNum.Text);
                    txtQTY.Text = "1";
                }
                txtPartNum.Text = "";
            }
        }

        private void txtItemName_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Item Name", "Enter Item Name : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                txtItemName.Text = pValue;
                string itemName = txtItemName.Text;
                string WhereCon = "ItemName";

                PopulateDataGridView();
                if (iSelectedItem())
                {
                    rowin = dgEnterSales.CurrentCell.RowIndex;
                }
                DataGridViewRow dgcur = dgEnterSales.Rows[rowin];
                txtQTY.Visible = true;
                lbItemQty.Visible = true;
                // if (txtQTY.Text != "")
                //{
                dgcur.Cells["Ship"].Value = txtQTY.Text;
                int cur = dgcur.Index;

                if (customerText.Text != "")
                {
                    ShowItemLookup(itemName, WhereCon);
                    itemcalc(cur);
                    for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                    {
                        if (this.dgEnterSales.Rows[i].Cells["Amount"] != null)
                        {
                            Recalcline(1, i);
                        }
                    }
                }
                dgEnterSales.ClearSelection();
                if (rowin < dgEnterSales.RowCount)
                {
                    dgEnterSales.Rows[rowin].Selected = false;
                    dgEnterSales.Rows[++rowin].Selected = true;
                }
                txtQTY.Text = "";
                txtPartNum.Text = "";
                txtPartNum.Select();
                CalcOutOfBalance();
            }
            txtQTY.Text = "1";
        }

        //private void txtBarCode_Click(object sender, EventArgs e)
        //{
        //    KeyboardOnScreen kbos = new KeyboardOnScreen("Barcode", "Enter Barcode : ");
        //    if (kbos.ShowDialog() == DialogResult.OK)
        //    {
        //        string pValue = kbos.GetValue.ToString();
        //        txtBarCode.Text = pValue;
        //        string barsql = "SELECT * from Barcodes b Inner join Items i ON i.ID = b.ItemID";
        //        DataTable dt = new DataTable();
        //        CommonClass.runSql(ref dt, barsql);

        //        for (int x = 0; x < dt.Rows.Count; x++)
        //        {
        //            DataRow dr = dt.Rows[x];

        //            if (txtBarCode.Text == CommonClass.base64Decode(dr["BarcodeData"].ToString()))
        //            {
        //                txtPartNum.Text = dr["PartNumber"].ToString();
        //                //txtPartNum.KeyDown += txtItemName_KeyDown;
        //                SetPartName(txtBarCode.Text);
        //            }
        //        }
        //        txtQTY.Text = "1";
        //    }
        //}

        private void txtQTY_Click(object sender, EventArgs e)
        {
            //if (dgEnterSales.Rows.Count > 0)
            //{
            //    if (dgEnterSales.Rows[dgEnterSales.CurrentCell.RowIndex].Cells["PartNumber"].Value != null)
            //    {
            //        if (iSelectedItem())
            //        {
            //            rowin = dgEnterSales.CurrentCell.RowIndex;
            //        }
            //        if (CommonClass.isSalesperson == true)
            //        {
            //            VoidValidation DlgVoid = new VoidValidation();
            //            if (DlgVoid.ShowDialog() == DialogResult.OK)
            //            {
            //                password = DlgVoid.GetPassword;
            //                username = DlgVoid.GetUsername;
            //                KeyboardOnScreen kbos = new KeyboardOnScreen("Change Quantity Value", "Enter Qty Value : ");
            //                if (kbos.ShowDialog() == DialogResult.OK)
            //                {
            //                    string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
            //                    string strValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
            //                    if (strValue == "-")
            //                    {
            //                        pValue = strValue + pValue;
            //                    }
            //                    dgEnterSales.Rows[rowin].Cells["Ship"].Value = pValue;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            KeyboardOnScreen kbos = new KeyboardOnScreen("Change Quantity Value", "Enter Qty Value : ");
            //            if (kbos.ShowDialog() == DialogResult.OK)
            //            {
            //                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
            //                if (kbos.GetValue == "-" + pValue)
            //                {
            //                    pValue = "-" + pValue;
            //                }
            //                dgEnterSales.Rows[rowin].Cells["Ship"].Value = pValue;
            //            }
            //        }
            //        itemcalc(rowin);
            //        Recalcline(4, rowin);
            //        Recalc();
            //        CalcOutOfBalance();

            //    }
            //    else
            //    {
            //        MessageBox.Show("No Item Found!");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No Item Found!");
            //}
        }



        private void txtGCNo_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Gift Certificate Number", "Enter GC Number : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtGCNo.Text = pValue;
                string sql = "SELECT * FROM GiftCertificate WHERE GCNumber=@GCNumber";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@GCNumber", txtGCNo.Text);
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, sql, param);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count >= 1)
                    {
                        bool isUsed = (bool)dt.Rows[0]["IsUsed"];
                        if (isUsed)
                        {
                            MessageBox.Show("Gift Certificate is already used");
                            return;
                        }

                        DateTime gcsdate = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToLocalTime();
                        DateTime gcedate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToLocalTime();
                        DateTime today = DateTime.Now.ToLocalTime();

                        if (today >= gcsdate || today <= gcedate)
                        {
                            double gcamount = Convert.ToDouble(dt.Rows[0]["GCAmount"]);
                            if (gcamount > 0)
                            {
                                dgPaymentMethod.Rows[GCRowID].Cells["TenderAmount"].Value = gcamount;
                                TenderCalc();
                                txtGCNotes.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Gift Certificate amount is 0");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gift Certificate is already expired");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Gift Certificate Number Does Not Exist! ");
                    return;
                }

            }
        }

        private void txtGCNotes_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Gift Certificate Notes", "Enter GC Notes : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtGCNotes.Text = pValue;
            }
        }

        private void txtPaymentNotes_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Payment Notes", "Enter Payment Notes : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtPaymentNotes.Text = pValue;
            }
        }

        private void txtBankAccountName_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Bank Account Name", "Enter Bank Account Name : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                txtBankAccountName.Text = pValue;
            }
        }

        private void txtBankAccountNo_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Bank Account Number", "Enter Bank Account Number : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                txtBankAccountNo.Text = pValue;
            }
        }

        private void txtChequeNo_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Cheque Number", "Enter Cheque Number : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtChequeNo.Text = pValue;
            }
        }

        private void txtBankNotes_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Bank Notes", "Enter Bank Notes : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtBankNotes.Text = pValue;
            }
        }

        private void txtBankBSB_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Bank BSB", "Enter Six Digit BSB : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                txtBankBSB.Text = pValue;
            }
        }
        private void txtCardNo_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Card Number", "Enter Card Number : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                txtCardNo.Text = pValue;
            }
        }

        private void txtCardExpiry_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Card Expiry", "Enter Card Expiry : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                if (pValue.Length == 4)
                {
                    txtCardExpiry.Text = pValue;
                }
                else
                {
                    MessageBox.Show("Invalid Expiry Date");
                }
            }
        }

        private void txtCardName_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Card Name", "Enter Card Name : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                txtCardName.Text = pValue;
            }
        }

        private void txtCardNotes_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Card Note", "Enter Card Note : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtCardNotes.Text = pValue;
            }
        }

        private void dgEnterSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEnterSales.CurrentCell == null || dgEnterSales.CurrentCell.RowIndex < 0)
                return;

           // dgEnterSales.Rows[dgEnterSales.CurrentCell.RowIndex].Selected = true;
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
                int lPartIndex = rowindex;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dgRow = dt.Rows[i];
                    lPartIndex++;
                    if (!changeqty)
                    {
                        dgEnterSales.Rows.Add();
                    }
                  
                    dgEnterSales.Rows[lPartIndex].Cells["ItemID"].Value = dgRow["PartItemID"].ToString();
                    dgEnterSales.Rows[lPartIndex].Cells["Ship"].Value = float.Parse(dgRow["PartItemQty"].ToString()) * float.Parse(dgEnterSales.Rows[rowindex].Cells["Ship"].Value.ToString());
                    dgEnterSales.Rows[lPartIndex].Cells["PartNumber"].Value = dgRow["PartNumber"].ToString();
                    dgEnterSales.Rows[lPartIndex].Cells["Description"].Value = dgRow["ItemName"].ToString();
                    dgEnterSales.Rows[lPartIndex].Cells["Amount"].Value = 0;

                    string taxcode = dgRow["SalesTaxCode"].ToString();
                    if (taxcode != "")
                    {
                        dgEnterSales.Rows[lPartIndex].Cells["TaxRate"].Value = dgRow["RateTaxSales"];
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCode"].Value = dgRow["SalesTaxCode"];
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCollectedAccountID"].Value = dgRow["TaxCollectedAccountID"];
                    }
                    else
                    {
                        dgEnterSales.Rows[lPartIndex].Cells["TaxRate"].Value = 0;
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCode"].Value = "N-T";
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCollectedAccountID"].Value = 0;
                    }

                    dgEnterSales.Rows[lPartIndex].Cells["Price"].Value = 0;
                    dgEnterSales.Rows[lPartIndex].Cells["ActualUnitPrice"].Value = 0;
                    dgEnterSales.Rows[lPartIndex].Cells["Amount"].Value = 0;

                    Recalcline(8, lPartIndex);
                    int c = VerifyItemQty(lPartIndex);
                    if(c == 0)
                    {
                        changeqty = true;
                        dgEnterSales.Rows[rowindex].Cells["Ship"].Value = 0;
                        dgEnterSales.Rows[lPartIndex].Cells["Ship"].Value = 0;
                        itemcalc(rowindex);
                    }
                }
            }
        }
        private void IsIngredientBuild(int itemID, int rowindex)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ia.PartItemID,
                                    ia.PartItemQty, 
                                    i.PartNumber,
                                    i.ItemName,
                                    i.SalesTaxCode,
                                    t.TaxCollectedAccountID, 
                                    t.TaxPercentageRate AS RateTaxSales,
                                    ict.AverageCostEx
                            FROM ItemsAutoBuild ia 
                            INNER JOIN Items i ON i.ID = ia.PartItemID 
                            LEFT JOIN TaxCodes t ON i.SalesTaxCode = t.taxcode
                            LEFT JOIN ItemsCostPrice ict ON ict.ItemID = i.ID
                            WHERE ia.ItemID = " + itemID;
            CommonClass.runSql(ref dt, sql);
            float lCost = 0;
            float lTCost = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dgRow = dt.Rows[i];
                    DataRow rw = IngredientTb.NewRow();
                    rw["ItemID"] = dgRow["PartItemID"].ToString();
                    rw["Ship"] = float.Parse(dgRow["PartItemQty"].ToString()) * float.Parse(dgEnterSales.Rows[rowindex].Cells["Ship"].Value.ToString());
                    rw["PartNumber"] = dgRow["PartNumber"].ToString();
                    rw["Description"] = dgRow["ItemName"].ToString();
                    rw["Amount"] = 0;
                    string taxcode = dgRow["SalesTaxCode"].ToString();
                    if (taxcode != "")
                    {
                        rw["TaxRate"] = dgRow["RateTaxSales"];
                        rw["TaxCode"] = dgRow["SalesTaxCode"];
                        rw["TaxCollectedAccountID"] = dgRow["TaxCollectedAccountID"];
                    }
                    else
                    {
                        rw["TaxRate"] = 0;
                        rw["TaxCode"] = "N-T";
                        rw["TaxCollectedAccountID"] = 0;
                    }
                    rw["Cost"] = dgRow["AverageCostEx"].ToString();
                    rw["TotalCost"] = float.Parse(dgRow["PartItemQty"].ToString()) * float.Parse(dgEnterSales.Rows[rowindex].Cells["Ship"].Value.ToString()) * float.Parse(dgRow["AverageCostEx"].ToString());
                    IngredientTb.Rows.Add(rw);
                    lCost += float.Parse(rw["Cost"].ToString());
                    lTCost += float.Parse(rw["TotalCost"].ToString());
                }
            }
            dgEnterSales.Rows[rowindex].Cells["CostPrice"].Value = lCost;
        }
        private void InitIngredientInfoTb()
        {
            IngredientTb = new DataTable();
            IngredientTb.Columns.Add("ItemID", typeof(string));
            IngredientTb.Columns.Add("Ship", typeof(decimal));
            IngredientTb.Columns.Add("PartNumber", typeof(string));
            IngredientTb.Columns.Add("Description", typeof(string));
            IngredientTb.Columns.Add("Amount", typeof(string));
            IngredientTb.Columns.Add("TaxRate", typeof(string));
            IngredientTb.Columns.Add("TaxCode", typeof(string));
            IngredientTb.Columns.Add("TaxCollectedAccountID", typeof(string));
            IngredientTb.Columns.Add("Cost", typeof(string));
            IngredientTb.Columns.Add("TotalCost", typeof(string));

        }
        public void UpdateItemQty()
        {
            if (IngredientTb.Rows.Count > 0)
            {
                foreach (DataRow dr in IngredientTb.Rows)
                {
                    Dictionary<string, object> paramItemTransaction = new Dictionary<string, object>();

                    // INSERT ADJUSTED ITEMS IN ITEM TRANSACTION

                    string sqlItemTransaction = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                            VALUES(@TransDate , @lItemID ,@lQty, @lQtyAdjustment , @CostPrice ,@TotalCost ,'SII'," + NewSalesID + "," + CommonClass.UserID + ")";
                    paramItemTransaction.Add("@TransDate", this.salesDate.Value.ToUniversalTime());
                    paramItemTransaction.Add("@lItemID", dr["ItemID"].ToString());
                    paramItemTransaction.Add("@lQty", dr["Ship"].ToString());
                    if (dr["Ship"].ToString() != "")
                    {
                        paramItemTransaction.Add("@lQtyAdjustment", float.Parse(dr["Ship"].ToString()) * (-1));
                    }
                    else
                    {
                        paramItemTransaction.Add("@lQtyAdjustment", 0);
                    }
                    paramItemTransaction.Add("@TotalCost", dr["TotalCost"].ToString());
                    paramItemTransaction.Add("@CostPrice", dr["Cost"].ToString());

                    // UPDATE ITEM QTY ON HAND
                    CommonClass.runSql(sqlItemTransaction, CommonClass.RunSqlInsertMode.QUERY, paramItemTransaction);
                    string sql = @"UPDATE ItemsQty SET OnHandQty = ((SELECT OnHandQty FROM ItemsQty WHERE ItemID=@ItemID) - " + dr["Ship"].ToString() + ")"
                                 + " WHERE ItemID=@ItemID";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@ItemID", dr["ItemID"].ToString());
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                }
            }
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {

        }

        private void txtCustPoints_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            int lRowIndex = dgEnterSales.CurrentCell.RowIndex; ;
            if (dgEnterSales.Rows.Count > 0)
            {
                if (iSelectedItem())
                {
                    rowin = dgEnterSales.CurrentCell.RowIndex;
                }
                if (dgEnterSales.Rows[lRowIndex].Cells["PartNumber"].Value != null)
                {
                    if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                    {
                        VoidValidation DlgVoid = new VoidValidation("Discount Override");
                        if (DlgVoid.ShowDialog() == DialogResult.OK)
                        {
                            password = DlgVoid.GetPassword;
                            username = DlgVoid.GetUsername;
                            KeyboardOnScreen kbos = new KeyboardOnScreen("Discount Override", "Enter Discount Percentage : ");
                            if (kbos.ShowDialog() == DialogResult.OK)
                            {
                                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                                string strValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                                if (strValue == "-")
                                {
                                    pValue = strValue + pValue;
                                }
                                dgEnterSales.Rows[lRowIndex].Cells["Discount"].Value = pValue;
                            }
                            DataRow rw = dtv.NewRow();
                            rw["UserName"] = DlgVoid.GetUsername;
                            rw["AuditAction"] = "Discount override to " + dgEnterSales.Rows[rowin].Cells["Discount"].Value + " by " + DlgVoid.GetUsername;
                            dtv.Rows.Add(rw);

                        }
                    }
                    else
                    {
                        KeyboardOnScreen kbos = new KeyboardOnScreen("Discount Override", "Enter Discount Percentage : ");
                        if (kbos.ShowDialog() == DialogResult.OK)
                        {
                            string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                            if (kbos.GetValue == "-" + pValue)
                            {
                                pValue = "-" + pValue;
                            }
                            dgEnterSales.Rows[lRowIndex].Cells["Discount"].Value = pValue;
                        }
                        DataRow rw = dtv.NewRow();
                        rw["AuditAction"] = "Discount override to " + dgEnterSales.Rows[rowin].Cells["Discount"].Value + " by " + CommonClass.UserName;
                        dtv.Rows.Add(rw);
                    }
                    itemcalc(lRowIndex);
                    Recalcline(5, lRowIndex);
                    Recalc();
                    CalcOutOfBalance();

                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }
        }

        private void txtGCNo_TextChanged(object sender, EventArgs e)
        {

        }
    }//End
}
