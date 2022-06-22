using AbleRetailPOS.Inventory;
using AbleRetailPOS.References;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Purchase
{
    public partial class EnterPurchase : Form
    {
        private string[] Tax;
        private string XpurchaseID = "";
        private string XShippingID = "";
        private string layoutType = "";
        private decimal OldBalanceDue = 0;
        private string AP_AccountID;
        private string AP_PaymentAccountID;
        private string AP_InventoryAccountID;
        private string AP_FreightAccountID;
        private string AP_DepositsAccountID;
        public string purchasenumber = "";
        public string receiveitemnumber = "";
        public string SupplierID;
        public string TermsOfPaymentID;
        private DataRow SupplierRow;
        private DataRow ShipID;
        private string baldate;
        private string discountdate;
        bool IsLoading = false;
        private string ShippingID;
        private string ShipVia;
        private string[] Jobs;
        private string ActualDueDate;
        private string ActualDiscountDate;
        private string BalanceDueDays;
        private string DiscountDays;
        private string VolumeDiscount;
        private string EarlyPaymenDiscount;
        private string LatePaymenDiscount;
        private string ProfileTax = "";
        private float lTaxEx = 0;
        private float lTaxInc = 0;
        private float lTaxRate = 0;
        private float lAmount = 0;
        private string CurSeries = "";
        private SqlCommand cmd = new SqlCommand();
        private string istaxInclusive;
        private string TermRefID;
        private string TermReferenceID = "0";
        private bool createterm;
        private int shipAddressID;
        private string defPurchaseNum;
        private string POStatus;
        private string ToPurchaseType;
        private string FromPurchaseType = "";
        bool enter = false;
        private string lTranType;

        //FOR FREIGHT
        private string FreightTaxCode = "";
        private string FreightTaxAccountID = "0";
        private float FreightTaxRate = 0;
        private float FreightTax = 0;
        private float FreightAmountEx = 0;
        private float FreightAmountInc = 0;

        CommonClass.InvocationSource SrcOfInvoke;
        private DataTable TbRepPurchase;
        private DataTable TbRepPurchaseLines;

        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;


        //For Payment Info
        private DataTable PaymentInfoTb = null;
        private string gPaymentNo = "";
        private int NewPurchaseID = 0;
        private int NewReceiveItemID = 0;
        private Decimal PrevPaid = 0;

        private DataRow PurchaseTermsRow;
        //Import Item
        DataTable OrderItemDataTable = new DataTable();
        DataTable ReceiveItemDataTable = new DataTable();

        public CommonClass.InvocationSource SourceOfInvoke
        {
            get { return SrcOfInvoke; }
            set { SrcOfInvoke = value; }
        }

        public EnterPurchase(CommonClass.InvocationSource pSrcInvoke, string pID = "", string ShippingID = "", string pPurchaseType = "")
        {
            SrcOfInvoke = pSrcInvoke;
            ToPurchaseType = pPurchaseType;
            XpurchaseID = pID;
            XShippingID = ShippingID;
            InitializeComponent();


            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                outx = false;
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
        private void LoadDefaultTerms(string pProfileID)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM  Profile  WHERE ID = " + pProfileID;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SupplierRow = dt.Rows[0];

                    TermsOfPaymentID = SupplierRow["TermsOfPayment"].ToString();
                    PurchaseTermsRow["TermsOfPaymentID"] = TermsOfPaymentID;
                    VolumeDiscount = SupplierRow["VolumeDiscount"].ToString();
                    PurchaseTermsRow["VolumeDiscount"] = VolumeDiscount;
                    EarlyPaymenDiscount = SupplierRow["EarlyPaymentDiscountPercent"].ToString();
                    PurchaseTermsRow["EarlyPaymentDiscountPercent"] = EarlyPaymenDiscount;
                    LatePaymenDiscount = SupplierRow["LatePaymentChargePercent"].ToString();
                    PurchaseTermsRow["LatePaymentChargePercent"] = LatePaymenDiscount;


                    switch (TermsOfPaymentID)
                    {

                        case "SD": //Specific Days
                            BalanceDueDays = SupplierRow["BalanceDueDays"].ToString();
                            PurchaseTermsRow["BalanceDueDays"] = BalanceDueDays;
                            DiscountDays = SupplierRow["DiscountDays"].ToString();
                            PurchaseTermsRow["DiscountDays"] = DiscountDays;
                            lblTerms.Text = "Specific Days";
                            break;

                        default: //CASH
                            lblTerms.Text = TermsOfPaymentID;
                            BalanceDueDays = "0";
                            PurchaseTermsRow["BalanceDueDays"] = BalanceDueDays;
                            DiscountDays = "0";
                            PurchaseTermsRow["DiscountDays"] = DiscountDays;
                            break;
                    }
                    this.lblTerms.Text = (TermsOfPaymentID != "CASH" ? BalanceDueDays.ToString() + " Days" : TermsOfPaymentID);
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

        private void LoadBillTerms()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM Terms where TermsID = " + TermReferenceID;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);

                BalanceDueDays = "0";
                if (dt.Rows.Count > 0)
                {
                    PurchaseTermsRow = dt.Rows[0];
                    TermsOfPaymentID = PurchaseTermsRow["TermsOfPaymentID"].ToString();
                    VolumeDiscount = PurchaseTermsRow["VolumeDiscount"].ToString();
                    EarlyPaymenDiscount = PurchaseTermsRow["EarlyPaymentDiscountPercent"].ToString();
                    LatePaymenDiscount = PurchaseTermsRow["LatePaymentChargePercent"].ToString();


                    switch (TermsOfPaymentID)
                    {
                        //case "DM"://Day of the Month
                        //    baldate = PurchaseTermsRow["BalanceDueDate"].ToString();
                        //    discountdate = PurchaseTermsRow["DiscountDate"].ToString();

                        //    break;
                        //case "DMEOM": //Day of the Month after EOM
                        //    baldate = PurchaseTermsRow["BalanceDueDate"].ToString();
                        //    discountdate = PurchaseTermsRow["DiscountDate"].ToString();
                        //    break;
                        //case "SDEOM"://Specifc Day after EOM
                        //    BalanceDueDays = PurchaseTermsRow["BalanceDueDays"].ToString();
                        //    DiscountDays = PurchaseTermsRow["DiscountDays"].ToString();
                        //    break;
                        case "SD": //Specific Days
                            BalanceDueDays = PurchaseTermsRow["BalanceDueDays"].ToString();
                            DiscountDays = PurchaseTermsRow["DiscountDays"].ToString();
                            break;

                        default: //CASH
                            lblTerms.Text = TermsOfPaymentID;
                            break;
                    }
                    this.lblTerms.Text = BalanceDueDays.ToString() + " Days";
                }
                else
                {
                    PurchaseTermsRow = dt.NewRow();
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
                ShippingID = "0";
                if (dt.Rows.Count > 0)
                {
                    ShipID = dt.Rows[0];
                    ShippingID = ShipID["ShippingID"].ToString();
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
                ShippingID = "0";
                if (dt.Rows.Count > 0)
                {
                    ShipID = dt.Rows[0];
                    ShipVia = ShipID["ShippingMethod"].ToString();
                    ShippingID = ShipID["ShippingID"].ToString();
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
        public void ApplyItemFieldAccess(String FieldID)
        {
            CommonClass.GetAccess(FieldID);

            foreach (Control c in this.Controls)
            {
                //BUTTON IN TABACCOUNTDETAILS
                if (c is Button)
                {
                    Button btn = (Button)c;
                    CheckButtonRights(btn);
                }
            }
        }
        private void CheckButtonRights(Button item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
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
        private void EnterPurchase_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            foreach (DataGridViewColumn column in dgPurchase.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Only enable the print button if the record is existing, not a new record to be dtDatePurchase
            dtDatePurchase.Value = DateTime.Now.ToUniversalTime();
            if (SrcOfInvoke != CommonClass.InvocationSource.REGISTER)
                btnPrint.Visible = false;

            ReloadPurchase();

            SqlConnection con_ua = null;
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);

                string selectSql_ua = "SELECT TOP(1) TradeCreditorGLCode,PurchaseFreightGLCode FROM Preference";
                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AP_AccountID = (reader["TradeCreditorGLCode"].ToString());
                            AP_FreightAccountID = (reader["PurchaseFreightGLCode"].ToString());


                            if (AP_AccountID == "0" || AP_AccountID == "")
                            {
                                string titles = "Information";
                                DialogResult noCustomerReceipts = MessageBox.Show("Setup Purchase Linked Account first.", titles, MessageBoxButtons.OK);
                                if (noCustomerReceipts == DialogResult.OK)
                                {
                                    this.BeginInvoke(new MethodInvoker(Close));
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Setup Purchase Linked Account First");
                    }
                }

                if (SrcOfInvoke != CommonClass.InvocationSource.USERECURRING && SrcOfInvoke != CommonClass.InvocationSource.REGISTER && SrcOfInvoke != CommonClass.InvocationSource.REMINDER)
                {
                    SqlCommand cmdrecur = new SqlCommand("SELECT EntityID FROM Recurring WHERE TranType IN ('PO')", con_ua);
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
        }

        private void ReloadPurchase()
        {
            ApplyItemFieldAccess(CommonClass.UserID);
            if (XpurchaseID != "")
            {
                if (SrcOfInvoke == CommonClass.InvocationSource.TODOLIST)
                {
                    DataTable lItemTb = GetItem(XpurchaseID);//ITEM ID
                    DataRow dr = lItemTb.Rows[0];

                    string Supplier = dr["SupplierID"].ToString();
                    LoadBillTerms();
                    if (Supplier != "0")
                    {
                        txtSupplier.Text = dr["Name"].ToString();
                        LoadDefaultTerms(Supplier);
                        txtMemo.Text = "Purchase; " + dr["Name"].ToString();
                        LoadContacts(Convert.ToInt32(Supplier), this.cmb_shippingcontact.SelectedIndex + 1);
                    }
                    else
                    {
                        MessageBox.Show("The item does not have a default supplier, Please select a supplier", "Information");
                        ShowSupplierAccounts();
                    }


                    DataGridViewRow dgvRows = dgPurchase.CurrentRow;
                    dgvRows.Cells["OrderedQty"].Value = XShippingID;//ORder Quantity
                    dgvRows.Cells["PartNumber"].Value = dr["PartNumber"].ToString();
                    dgvRows.Cells["Description"].Value = dr["ItemName"].ToString();
                    dgvRows.Cells["TaxCode"].Value = dr["PurchaseTaxCode"].ToString();
                    float lastcost = float.Parse(dr["StandardCostEx"].ToString() == "" ? "0" : dr["StandardCostEx"].ToString()); ;

                    dgvRows.Cells["ItemID"].Value = dr["ID"].ToString();
                    bool isCounted = (bool)dr["IsCounted"];
                    if (isCounted)
                    {
                        dgvRows.Cells["InventoryAccountID"].Value = dr["AssetAccountID"].ToString();
                    }
                    else
                    {
                        dgvRows.Cells["InventoryAccountID"].Value = dr["COSAccountID"].ToString();
                    }

                    float ltaxrate = 0;
                    DataRow rTx = CommonClass.getTaxDetails(dr["PurchaseTaxCode"].ToString());
                    if (rTx.ItemArray.Length > 0)
                    {

                        ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                        string lTaxPaidAccountID = "";
                        lTaxPaidAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                        dgvRows.Cells["TaxPaidAccountID"].Value = lTaxPaidAccountID;
                        dgvRows.Cells["TaxRate"].Value = ltaxrate;
                    }
                    float lastcostInc = lastcost * (1 + (ltaxrate / 100));
                    if (chk_TaxInclusive.Checked)
                    {
                        dgvRows.Cells["Price"].Value = lastcostInc;
                    }
                    else
                    {
                        dgvRows.Cells["Price"].Value = lastcost;
                    }
                    this.dgPurchase.Refresh();

                    btnSaveRecurring.Enabled = true;

                    CheckForm();
                    RecalclineItem(0, 0);
                    CalcOutOfBalance();
                }
                else
                {
                    CheckForm();
                    LoadTransaction(XpurchaseID);
                    CalcOutOfBalance();
                    this.lblID.Text = XpurchaseID;
                }




                if (SrcOfInvoke != CommonClass.InvocationSource.CHANGETO
                       && SrcOfInvoke != CommonClass.InvocationSource.USERECURRING
                       && SrcOfInvoke != CommonClass.InvocationSource.REMINDER
                       && SrcOfInvoke != CommonClass.InvocationSource.TODOLIST)
                {
                    if (POStatus == "New") //New Orders only
                    {
                        btnRecord.Text = "Update";
                        btnRecord.Enabled = CanEdit;
                        btnRemove.Enabled = CanEdit;
                        if (!CommonClass.IsEditOK)
                        {
                            btnRecord.Enabled = false;
                        }
                        //btnApprove.Enabled = true; Rights already applied in ApplyFieldAccess()
                        btnReceive.Enabled = false;
                        btnDeletePO.Enabled = CanDelete;
                    }
                    else if (POStatus == "Active" || POStatus == "Backordered")
                    {
                        btnRecord.Text = "Update";
                        btnRecord.Enabled = false;
                        btnRemove.Enabled = false;
                        btnApprove.Enabled = false;
                        //btnReceive.Enabled = true;
                        if (POStatus == "Active")
                        {
                            btnUndoApprove.Enabled = true;
                        }
                    }
                    else
                    {
                        btnRecord.Text = "Update";
                        btnRecord.Enabled = false;
                        btnRemove.Enabled = false;
                        btnApprove.Enabled = false;
                        btnReceive.Enabled = false;
                        btnDeletePO.Enabled = false;
                        btnUndoApprove.Enabled = false;
                    }

                }
                else //If invocation source is to change it, btnRecord should show Record.
                {
                    btnRecord.Text = "Record"; //must apply rights to receive items
                    btnRecord.Enabled = btnReceive.Enabled;//must apply rights to receive items
                    btnRemove.Enabled = btnReceive.Enabled;
                    btnApprove.Enabled = false;
                    btnReceive.Enabled = false;
                    btnDeletePO.Enabled = false;
                    btnUndoApprove.Enabled = false;
                    for (int i = 0; i < this.dgPurchase.Rows.Count; i++)
                    {
                        if (dgPurchase.Rows[i].Cells["PartNumber"].Value != null)
                        {
                            if (dgPurchase.Rows[i].Cells["PartNumber"].Value.ToString() != "")
                            {
                                RecalclineItem(0, i);
                            }
                        }
                    }
                    CalcOutOfBalance();

                }


            }
            else
            {
                CheckForm();
                LoadBillTerms();
                NewPurchase();
                btnRecord.Enabled = CanAdd;

                this.lblID.Visible = false;
                this.lblPurchaseID.Visible = false;
                this.grpReceive.Visible = false;
            }
        }
        private void ReloadReceiveItem(string pID)
        {

            if (XpurchaseID != "")
            {
                if (SrcOfInvoke == CommonClass.InvocationSource.TODOLIST)
                {
                    DataTable lItemTb = GetItem(XpurchaseID);//ITEM ID
                    DataRow dr = lItemTb.Rows[0];

                    string Supplier = dr["SupplierID"].ToString();
                    LoadBillTerms();
                    if (Supplier != "0")
                    {
                        txtSupplier.Text = dr["Name"].ToString();
                        LoadDefaultTerms(Supplier);
                        txtMemo.Text = "Purchase; " + dr["Name"].ToString();
                        LoadContacts(Convert.ToInt32(Supplier), this.cmb_shippingcontact.SelectedIndex + 1);
                    }
                    else
                    {
                        MessageBox.Show("The item does not have a default supplier, Please select a supplier", "Information");
                        ShowSupplierAccounts();
                    }


                    DataGridViewRow dgvRows = dgPurchase.CurrentRow;
                    dgvRows.Cells["OrderedQty"].Value = XShippingID;//ORder Quantity
                    dgvRows.Cells["PartNumber"].Value = dr["PartNumber"].ToString();
                    dgvRows.Cells["Description"].Value = dr["ItemName"].ToString();
                    dgvRows.Cells["TaxCode"].Value = dr["PurchaseTaxCode"].ToString();
                    float lastcost = float.Parse(dr["StandardCostEx"].ToString() == "" ? "0" : dr["StandardCostEx"].ToString()); ;

                    dgvRows.Cells["ItemID"].Value = dr["ID"].ToString();
                    bool isCounted = (bool)dr["IsCounted"];
                    if (isCounted)
                    {
                        dgvRows.Cells["InventoryAccountID"].Value = dr["AssetAccountID"].ToString();
                    }
                    else
                    {
                        dgvRows.Cells["InventoryAccountID"].Value = dr["COSAccountID"].ToString();
                    }

                    float ltaxrate = 0;
                    DataRow rTx = CommonClass.getTaxDetails(dr["PurchaseTaxCode"].ToString());
                    if (rTx.ItemArray.Length > 0)
                    {

                        ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                        string lTaxPaidAccountID = "";
                        lTaxPaidAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                        dgvRows.Cells["TaxPaidAccountID"].Value = lTaxPaidAccountID;
                        dgvRows.Cells["TaxRate"].Value = ltaxrate;
                    }
                    float lastcostInc = lastcost * (1 + (ltaxrate / 100));
                    if (chk_TaxInclusive.Checked)
                    {
                        dgvRows.Cells["Price"].Value = lastcostInc;
                    }
                    else
                    {
                        dgvRows.Cells["Price"].Value = lastcost;
                    }
                    this.dgPurchase.Refresh();

                    btnSaveRecurring.Enabled = true;

                    CheckForm();
                    RecalclineItem(0, 0);
                    CalcOutOfBalance();
                }
                else
                {
                    LoadTransaction(XpurchaseID);
                    CalcOutOfBalance();
                    this.lblID.Text = XpurchaseID;
                }



            }

        }
        private void SetGrid(string pType)
        {
            layoutType = "Item";
            dgPurchase.Rows.Clear();

            lblMemo.Text = "Memo:";

            SetItemGrid(pType);
            PopulateDataGridView();
        }

        private void CheckForm()
        {
            this.btnImportOrderItem.Enabled = false;
            this.btnImportReceiveItem.Enabled = false;
            if (txtSupplier.Text != "")
            {
                //Item Layout For Order purchase type
                if (this.lblType.Text == "ORDER")
                {
                    this.btnImportOrderItem.Enabled = true;
                    this.btnImportReceiveItem.Enabled = false;
                    lblMemo.Text = " Journal Memo:";
                    this.BackColor = System.Drawing.Color.DarkSeaGreen;
                    dgPurchase.Columns[0].Visible = false;
                    dgPurchase.Columns[1].Visible = false;
                    dgPurchase.Columns[2].Visible = true; //OrderQty
                    dgPurchase.Columns[3].Width = 100;
                    dgPurchase.Columns[4].Visible = false;
                    dgPurchase.Columns[5].Visible = false; // Backourder
                    dgPurchase.Columns[6].Visible = true;
                    dgPurchase.Columns[6].Width = 175;
                    dgPurchase.Columns[7].Width = 300;

                    dgPurchase.Columns[8].Visible = true;
                    dgPurchase.Columns[9].Visible = true;
                    dgPurchase.Columns[10].Visible = false;
                    dgPurchase.Columns[11].Visible = true;

                    dgPurchase.Columns[11].Width = 150;

                    dgPurchase.Columns[12].Visible = true;

                    dgPurchase.Columns[12].Width = 60;

                    dgPurchase.Columns[13].Visible = true;

                    dgPurchase.Columns[13].Width = 60;
                    dgPurchase.Columns[14].Visible = false;
                }

                //Item Layout for Recieve Items Purchase Type
                if (this.lblType.Text == "RECEIVE ITEMS")
                {
                    this.btnImportOrderItem.Enabled = false;
                    this.btnImportReceiveItem.Enabled = true;
                    //Validate if item is already receive to create bill

                    lblMemo.Text = " Journal Memo:";
                    this.BackColor = System.Drawing.Color.Orange;
                    dgPurchase.Columns[0].Visible = false;
                    dgPurchase.Columns[1].Visible = false;
                    dgPurchase.Columns[2].Visible = true;
                    dgPurchase.Columns[3].Visible = true;
                    dgPurchase.Columns[3].Width = 60;
                    dgPurchase.Columns[4].Width = 60;
                    dgPurchase.Columns[4].Visible = true;
                    dgPurchase.Columns[5].Visible = true;
                    dgPurchase.Columns[5].Width = 60;
                    dgPurchase.Columns[6].Width = 180;
                    dgPurchase.Columns[7].Width = 300;

                    dgPurchase.Columns[8].Visible = true;
                    dgPurchase.Columns[9].Visible = true;
                    dgPurchase.Columns[10].Visible = false;
                    dgPurchase.Columns[11].Visible = true;

                    dgPurchase.Columns[11].Width = 150;

                    dgPurchase.Columns[12].Visible = true;

                    dgPurchase.Columns[12].Width = 60;

                    dgPurchase.Columns[13].Visible = true;

                    dgPurchase.Columns[13].Width = 60;
                    dgPurchase.Columns[14].Visible = false;
                }
            }
        }



        private void LoadContacts(int cID, int index)
        {
            SqlConnection con = new SqlConnection(CommonClass.ConStr);
            //GET THE HEADER 
            string sql = @"SELECT * FROM Contacts WHERE ProfileID = " + cID + " and Location = " + index;

            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();

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
                shipAddressID = index;
            }
        }

        public void ShowSupplierAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                SupplierID = lProfile[0];
                this.txtSupplier.Text = lProfile[2];

                TermRefID = lProfile[5];
                ProfileTax = lProfile[6];
                txtMemo.Text = "Purchase; " + lProfile[2];
                txtShipVia.Text = lProfile[4];
                ShippingID = (lProfile[11] == "" ? "0" : lProfile[11]);
                cmb_shippingcontact.SelectedIndex = Convert.ToInt16(lProfile[14]) - 1;
                LoadContacts(Convert.ToInt32(SupplierID), this.cmb_shippingcontact.SelectedIndex + 1);
                LoadDefaultTerms(SupplierID);
                LoadFreightTax(ProfileTax);

                txtTotalAmount.Visible = true;
                txtMemo.Visible = true;
                // txtSupInvNum.Visible = true;
                dtDatePurchase.Visible = true;
                dtPromisedDate.Visible = true;
                txtShipVia.Visible = true;
                Shippingmethod_btn.Visible = true;
                txtComment.Visible = true;
                txtFreight.Visible = true;
                txtFreight.ReadOnly = false;
                txtSubTotal.Visible = true;
                txtFreight.Visible = true;
                txtTax.Visible = true;

            }
        }

        public void ShowTermLookUp()
        {
            if (SupplierID == null)
            {
                MessageBox.Show("Pls Select a Supplier.");
            }
            else
            {
                TermsLookup TermsDlg = new TermsLookup(PurchaseTermsRow);
                if (TermsDlg.ShowDialog() == DialogResult.OK)
                {
                    PurchaseTermsRow = TermsDlg.GetTerms;

                    //lblTerms.Text = lTerms[0];
                    //BalanceDueDays = lTerms[9];
                    //DiscountDays = lTerms[10];
                    //VolumeDiscount = lTerms[3];
                    //EarlyPaymenDiscount = lTerms[4];
                    //LatePaymenDiscount = lTerms[5];
                    //baldate = lTerms[6];
                    //discountdate = lTerms[7];
                    TermRefID = PurchaseTermsRow["TermsOfPaymentID"].ToString();
                    if (TermRefID == "CASH")
                    {
                        lblTerms.Text = "Cash";
                    }
                    else
                    {
                        lblTerms.Text = PurchaseTermsRow["BalanceDueDays"].ToString() + " Days";
                    }
                    createterm = true;
                }
            }
        }

        private int NewTerm()
        {
            SqlConnection con = null;
            try
            {
                //Calculate Actual Due Date of the Transaction
                DateTime lTranDate = this.dtDatePurchase.Value.ToUniversalTime();
                DateTime lDueDate = lTranDate;
                DateTime lDiscountDate = lTranDate;
                switch (PurchaseTermsRow["TermsOfPaymentID"].ToString().Trim())
                {

                    //case "DM"://Day of the Month
                    //    baldate = PurchaseTermsRow["BalanceDueDate"].ToString();
                    //    discountdate = PurchaseTermsRow["DiscountDate"].ToString();

                    //    break;
                    //case "DMEOM": //Day of the Month after EOM
                    //    baldate = PurchaseTermsRow["BalanceDueDate"].ToString();
                    //    discountdate = PurchaseTermsRow["DiscountDate"].ToString();
                    //    break;

                    //case "SDEOM"://Specifc Day after EOM
                    //    BalanceDueDays = PurchaseTermsRow["BalanceDueDays"].ToString();
                    //    DiscountDays = PurchaseTermsRow["DiscountDays"].ToString();
                    //    break;
                    case "SD": //Specific Days
                        BalanceDueDays = PurchaseTermsRow["BalanceDueDays"].ToString();
                        DiscountDays = PurchaseTermsRow["DiscountDays"].ToString();
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

                cmd.Parameters.AddWithValue("@TermsOfPaymentID", TermsOfPaymentID);
                cmd.Parameters.AddWithValue("@DiscountDays", DiscountDays);
                cmd.Parameters.AddWithValue("@BalanceDueDays", BalanceDueDays);
                cmd.Parameters.AddWithValue("@VolumeDiscount", VolumeDiscount);
                cmd.Parameters.AddWithValue("@ActualDueDate", ActualDueDate);
                cmd.Parameters.AddWithValue("@ActualDiscountDate", ActualDiscountDate);
                cmd.Parameters.AddWithValue("@EarlyPaymentDiscountPercent", PurchaseTermsRow["EarlyPaymentDiscountPercent"].ToString());
                cmd.Parameters.AddWithValue("@LatePaymentChargePercent", PurchaseTermsRow["LatePaymentChargePercent"].ToString());
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
        private int UpdateTerm(string pTermsID)
        {
            SqlConnection con = null;
            try
            {
                //Calculate Actual Due Date of the Transaction
                DateTime lTranDate = this.dtDatePurchase.Value.ToUniversalTime();
                DateTime lDueDate = lTranDate;
                DateTime lDiscountDate = lTranDate;
                switch (PurchaseTermsRow["TermsOfPaymentID"].ToString().Trim())
                {
                    case "SD": //Specific Days
                        BalanceDueDays = PurchaseTermsRow["BalanceDueDays"].ToString();
                        DiscountDays = PurchaseTermsRow["DiscountDays"].ToString();
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
                string termsql = @"UPDATE Terms set 
                                TermsOfPaymentID = @TermsOfPaymentID, 
                                DiscountDays = @DiscountDays, 
                                BalanceDueDays = @BalanceDueDays, 
                                VolumeDiscount = @VolumeDiscount, 
                                ActualDueDate = @ActualDueDate,
                                ActualDiscountDate = @ActualDiscountDate,
                                EarlyPaymentDiscountPercent = @EarlyPaymentDiscountPercent,
                                LatePaymentChargePercent = @LatePaymentChargePercent where TermsID = " + pTermsID;
                SqlCommand cmd = new SqlCommand(termsql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@TermsOfPaymentID", PurchaseTermsRow["TermsOfPaymentID"].ToString());
                cmd.Parameters.AddWithValue("@DiscountDays", PurchaseTermsRow["DiscountDays"].ToString());
                cmd.Parameters.AddWithValue("@BalanceDueDays", PurchaseTermsRow["BalanceDueDays"].ToString());
                cmd.Parameters.AddWithValue("@VolumeDiscount", PurchaseTermsRow["VolumeDiscount"].ToString());
                cmd.Parameters.AddWithValue("@ActualDueDate", ActualDueDate);
                cmd.Parameters.AddWithValue("@ActualDiscountDate", ActualDiscountDate);
                cmd.Parameters.AddWithValue("@EarlyPaymentDiscountPercent", PurchaseTermsRow["EarlyPaymentDiscountPercent"].ToString());
                cmd.Parameters.AddWithValue("@LatePaymentChargePercent", PurchaseTermsRow["LatePaymentChargePercent"].ToString());
                con.Open();
                int res = Convert.ToInt32(cmd.ExecuteNonQuery());
                return res;

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
        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowSupplierAccounts();
            CheckForm();
            dgEnable();
        }

        private void GenerateBillNum(string pType = "")
        {
            SqlConnection con_ua = null;
            if (pType == "")
            {
                pType = lblType.Text;
            }
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);
                string selectSql_ua = "Select PurchaseOrderSeries, PurchaseOrderPrefix, PurchaseQuoteSeries, PurchaseQuotePrefix, PurchaseBillSeries, PurchaseBillPrefix, ReceivedItemsSeries, ReceivedItemsPrefix from TransactionSeries";
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
                            if (pType == "ORDER")
                            {
                                lSeries = (reader["PurchaseOrderSeries"].ToString());
                                lCnt = lSeries.Length;
                                lSeries = lSeries.TrimStart('0');
                                lSeries = (lSeries == "" ? "0" : lSeries);
                                lNewSeries = Convert.ToInt16(lSeries) + 1;
                                CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                                this.lblPurchaseNum.Text = purchasenumber = (reader["PurchaseOrderPrefix"].ToString()).Trim(' ') + CurSeries;
                            }
                            else if (pType == "RECEIVE ITEMS")
                            {
                                lSeries = (reader["ReceivedItemsSeries"].ToString());
                                lCnt = lSeries.Length;
                                lSeries = lSeries.TrimStart('0');
                                lSeries = (lSeries == "" ? "0" : lSeries);
                                lNewSeries = Convert.ToInt16(lSeries) + 1;
                                CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                                receiveitemnumber = (reader["ReceivedItemsPrefix"].ToString()).Trim(' ') + CurSeries;
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

        private void Shippingmethod_btn_Click(object sender, EventArgs e)
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;
                ShipVia = ShipList[0];
                txtShipVia.Text = ShipList[0];
                ShippingID = ShipList[1];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgPurchase_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= TextboxNumeric_KeyPress;
            int ColIndex = (int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex);

            e.Control.KeyPress -= Numeric_KeyPress;

            if (ColIndex == 2 || ColIndex == 4 || ColIndex == 5 || ColIndex == 8 || ColIndex == 9 || ColIndex == 11)
            {
                e.Control.KeyPress += TextboxNumeric_KeyPress;
            }
            else
            {
                e.Control.KeyPress -= TextboxNumeric_KeyPress;
            }
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

        private void txtFreight_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btn_Record_Click(object sender, EventArgs e)
        {
            if (btnRecord.Text == "Update")
            {
                UpdateItemPurchase(defPurchaseNum);
            }
            else
            {
                if (SrcOfInvoke == CommonClass.InvocationSource.CHANGETO)
                {
                    ReceivePO(defPurchaseNum);
                }
                else
                {
                    CreateNewItemPurchase();

                }
            }
        }



        void UpdateItemPurchase(string pOldPurchaseNo)
        {

            //DELETE PURCHASE LINES
            if (DeletePurchaseLines(XpurchaseID) > 0)
            {
                //EDIT PurchaseLine Records
                if (EditItemPurchaseRecord(XpurchaseID))
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, "Purchases " + lblType.Text, "Edited " + lblType.Text + " Number " + pOldPurchaseNo + " with Purchase ID " + XpurchaseID, XpurchaseID);
                    DialogResult PrintInvoice = MessageBox.Show("Would you like to print " + this.lblType.Text + "?", "Information", MessageBoxButtons.YesNo);
                    if (PrintInvoice == DialogResult.Yes)
                    {
                        LoadItemLayoutReport(XpurchaseID.ToString());
                    }
                    MessageBox.Show("Edited " + lblType.Text + " Transaction successfully.", "Purchase Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
            }
        }


        private void ReceivePO(string pOldPurchaseNo)
        {

            ReceiveDialog RDlg = new ReceiveDialog();
            RDlg.ShowDialog();
            if (RDlg.DialogResult == DialogResult.OK)
            {
                string[] ReceiveInfo;
                ReceiveInfo = RDlg.GetReceiveInfo;
                DateTime lReceivedDate = Convert.ToDateTime(ReceiveInfo[0]);

                int lReceiveID = CreateNewReceiveItems(lReceivedDate, ReceiveInfo[1], ReceiveInfo[2], this.lblID.Text);
                if (lReceiveID > 0)
                {
                    SrcOfInvoke = CommonClass.InvocationSource.SELF;
                    ToPurchaseType = "";
                    ReloadPurchase();

                }
                else
                {
                    MessageBox.Show("Error occured while receiving items");
                }
            }


        }




        private int DeletePurchaseLines(string pPurchaseID)
        {
            SqlConnection con = null;
            try
            {
                //Reverse Journal Entry Record
                con = new SqlConnection(CommonClass.ConStr);

                string sql = "DELETE FROM PurchaseLines WHERE PurchaseID = " + pPurchaseID;
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                int rec = cmd.ExecuteNonQuery();

                return rec;
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



        private bool EditItemPurchaseRecord(string pPurchaseID)
        {
            SqlConnection con_ = new SqlConnection(CommonClass.ConStr);
            try
            {
                LoadShippingID(this.txtShipVia.Text);
                DateTime EntryDate = DateTime.Now;
                string layout = "";
                string purchasetype = lblType.Text;
                string savesql = @"UPDATE Purchases SET  
                                        SupplierID = @SupplierID, 
                                        UserID = @SupplierID, 
                                        ShippingContactID = @ShippingContactID, 
                                        BillingContactID = @BillingContactID, 
                                        TermsReferenceID = @TermsReferenceID, 
                                        PurchaseType = @PurchaseType, 
                                        LayoutType = @LayoutType, 
                                        PurchaseNumber = @PurchaseNumber,
                                        TransactionDate = @TransactionDate, 
                                        SubTotal = @SubTotal, 
                                        FreightSubTotal = @FreightSubTotal,
                                        FreightTax = @FreightTax, 
                                        GrandTotal = @GrandTotal, 
                                        PurchaseReference = @PurchaseReference, 
                                        ShippingMethodID = @ShippingMethodID, 
                                        PromiseDate = @PromiseDate, 
                                        Memo = @Memo, 
                                        Comments = @Comments, 
                                        POStatus = @POStatus, 
                                        TaxTotal = @TaxTotal, 
                                        IsTaxInclusive = @IsTaxInclusive, 
                                        ClosedDate = @ClosedDate,
                                        FreightTaxCode = @FreightTaxCode,
                                        FreightTaxRate = @FreightTaxRate WHERE PurchaseID = " + pPurchaseID;

                cmd = new SqlCommand(savesql, con_);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                cmd.Parameters.AddWithValue("@PurchaseNumber", lblPurchaseNum.Text);
                cmd.Parameters.AddWithValue("@PromiseDate", dtPromisedDate.Value.ToUniversalTime());
                //cmd.Parameters.AddWithValue("@SupplierINVNumber", txtSupInvNum.Text);
                cmd.Parameters.AddWithValue("@TransactionDate", dtDatePurchase.Value.ToUniversalTime());

                layout = "Item";

                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@ShippingContactID", 0);
                cmd.Parameters.AddWithValue("@BillingContactID", 0);
                cmd.Parameters.AddWithValue("@TermsReferenceID", TermReferenceID);
                cmd.Parameters.AddWithValue("@LayoutType", "Item");
                cmd.Parameters.AddWithValue("@SubTotal", txtSubTotal.Value);
                cmd.Parameters.AddWithValue("@FreightSubTotal", FreightAmountEx);
                cmd.Parameters.AddWithValue("@FreightTax", FreightTax);
                cmd.Parameters.AddWithValue("@GrandTotal", txtTotalAmount.Value);

                cmd.Parameters.AddWithValue("@PurchaseReference", "N/A");
                cmd.Parameters.AddWithValue("@ShippingMethodID", ShippingID);
                cmd.Parameters.AddWithValue("@Memo", txtMemo.Text);
                cmd.Parameters.AddWithValue("@Comments", txtComment.Text);
                cmd.Parameters.AddWithValue("@TaxTotal", txtTax.Value);

                cmd.Parameters.AddWithValue("@FreightTaxCode", txtFTaxCode.Text);
                cmd.Parameters.AddWithValue("@FreightTaxRate", FreightTaxRate);

                if (chk_TaxInclusive.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "N");
                }

                if (lblType.Text == "ORDER")
                {
                    purchasetype = "ORDER";

                    cmd.Parameters.AddWithValue("@POStatus", "New");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                }

                cmd.Parameters.AddWithValue("@PurchaseType", purchasetype);
                con_.Open();

                int count = cmd.ExecuteNonQuery();

                //PurchaseLines
                //PurchaseLines
                string Descript;
                string Amount = "";
                string Job = "";
                string Tax = "";

                string lTaxPaidAccountID = "";
                float lTaxInc = 0;
                float lTaxEx = 0;
                float lTaxRate = 0;
                float lPriceEx = 0;
                string lOrderedQty = "0";
                string lReceivedQty = "0";
                string lToDateQty = "0";
                string lDiscountRate = "0";

                int entity = 0;
                for (int x = 0; x < this.dgPurchase.Rows.Count; x++)
                {
                    if (this.dgPurchase.Rows[x].Cells["PartNumber"].Value != null)
                    {
                        if (this.dgPurchase.Rows[x].Cells["PartNumber"].Value.ToString() != "")
                        {
                            Descript = String.Format("{0}", dgPurchase.Rows[x].Cells["Description"].Value.ToString());
                            Amount = dgPurchase.Rows[x].Cells["Amount"].Value.ToString();
                            double dAmount = double.Parse(Amount, NumberStyles.Currency);
                            lTaxInc = float.Parse(dgPurchase.Rows[x].Cells["TaxInclusiveAmount"].Value.ToString());
                            lTaxEx = float.Parse(dgPurchase.Rows[x].Cells["TaxExclusiveAmount"].Value.ToString());
                            lTaxRate = float.Parse(dgPurchase.Rows[x].Cells["TaxRate"].Value.ToString());
                            lPriceEx = float.Parse(dgPurchase.Rows[x].Cells["Price"].Value.ToString());
                            if (this.chk_TaxInclusive.Checked)
                            {
                                lPriceEx = lPriceEx / (1 + (lTaxRate / 100));
                            }
                            entity = Convert.ToInt32(dgPurchase.Rows[x].Cells["ItemID"].Value);
                            Job = dgPurchase.Rows[x].Cells["JobID"].Value == null ? "0" : dgPurchase.Rows[x].Cells["JobID"].Value.ToString();
                            Tax = dgPurchase.Rows[x].Cells["TaxCode"].Value.ToString();
                            lTaxPaidAccountID = dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value == null ? "0" : dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value.ToString();

                            lReceivedQty = dgPurchase.Rows[x].Cells["ReceivedQty"].Value == null ? "0" : dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString();
                            lOrderedQty = dgPurchase.Rows[x].Cells["OrderedQty"].Value == null ? "0" : dgPurchase.Rows[x].Cells["OrderedQty"].Value.ToString();
                            lDiscountRate = dgPurchase.Rows[x].Cells["DiscountRate"].Value == null ? "0" : dgPurchase.Rows[x].Cells["DiscountRate"].Value.ToString();

                            DateTime sDate = dtDatePurchase.Value.ToUniversalTime();
                            string purchaseLinesql = @"INSERT INTO PurchaseLines(
                                            PurchaseID, JobID, EntityID, TransactionDate, OrderQty, ReceiveQty, UnitPrice, ActualUnitPrice, 
                                            DiscountPercent, SubTotal, TotalAmount, Description, TaxCode, TaxAmount, TaxPaidAccountID, TaxRate)
                                            VALUES(
                                            " + pPurchaseID + ", " + Job + "," + entity.ToString() + ", @TransactionDate, " + lOrderedQty + ", " + lReceivedQty + ", " + lPriceEx.ToString() + ", " + lPriceEx.ToString() + " , " +
                                lDiscountRate + ", " + lTaxEx.ToString() + ", " + lTaxInc.ToString() + ", '" + Descript + "','" + Tax + "', " + (lTaxInc - lTaxEx).ToString() + ", '" + lTaxPaidAccountID + "'," + lTaxRate.ToString() + ")";

                            cmd.CommandText = purchaseLinesql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                UpdateTerm(TermReferenceID);
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

        private bool ChangeItemPurchaseType(string pPurchaseID, string pNewPurchaseNo, string pOldPurchaseNo)
        {
            SqlConnection con_ = new SqlConnection(CommonClass.ConStr);
            try
            {
                LoadShippingID(this.txtShipVia.Text);
                DateTime EntryDate = DateTime.Now;
                string layout = "";
                string purchasetype = lblType.Text;
                string savesql = @"UPDATE Purchases SET                                          
                                        SupplierID = @SupplierID, 
                                        UserID = @SupplierID, 
                                        ShippingContactID = @ShippingContactID, 
                                        BillingContactID = @BillingContactID, 
                                        TermsReferenceID = @TermsReferenceID, 
                                        PurchaseType = @PurchaseType, 
                                        LayoutType = @LayoutType, 
                                        PurchaseNumber = @PurchaseNumber,
                                        TransactionDate = @TransactionDate, 
                                        SubTotal = @SubTotal, 
                                        FreightSubTotal = @FreightSubTotal,
                                        FreightTax = @FreightTax, 
                                        GrandTotal = @GrandTotal, 
                                        TotalPaid = @TotalPaid, 
                                        TotalDue = @TotalDue, 
                                        PurchaseReference = @PurchaseReference, 
                                        ShippingMethodID = @ShippingMethodID, 
                                        PromiseDate = @PromiseDate, 
                                        Memo = @Memo, 
                                        Comments = @Comments, 
                                        POStatus = @POStatus, 
                                        TaxTotal = @TaxTotal, 
                                        IsTaxInclusive = @IsTaxInclusive, 
                                        ClosedDate = @ClosedDate,
                                        FreightTaxCode = @FreightTaxCode,
                                        FreightTaxRate = @FreightTaxRate WHERE PurchaseID = " + pPurchaseID;

                cmd = new SqlCommand(savesql, con_);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                cmd.Parameters.AddWithValue("@PurchaseNumber", pNewPurchaseNo);
                cmd.Parameters.AddWithValue("@PromiseDate", dtPromisedDate.Value.ToUniversalTime());
                //cmd.Parameters.AddWithValue("@SupplierINVNumber", txtSupInvNum.Text);
                cmd.Parameters.AddWithValue("@TransactionDate", dtDatePurchase.Value.ToUniversalTime());

                layout = "Item";

                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@ShippingContactID", 0);
                cmd.Parameters.AddWithValue("@BillingContactID", 0);
                cmd.Parameters.AddWithValue("@TermsReferenceID", TermReferenceID);
                cmd.Parameters.AddWithValue("@LayoutType", layout);
                cmd.Parameters.AddWithValue("@SubTotal", txtSubTotal.Value);
                cmd.Parameters.AddWithValue("@FreightSubTotal", FreightAmountEx);
                cmd.Parameters.AddWithValue("@FreightTax", FreightTax);
                cmd.Parameters.AddWithValue("@GrandTotal", txtTotalAmount.Value);

                cmd.Parameters.AddWithValue("@PurchaseReference", "N/A");
                cmd.Parameters.AddWithValue("@ShippingMethodID", ShippingID);
                cmd.Parameters.AddWithValue("@Memo", txtMemo.Text);
                cmd.Parameters.AddWithValue("@Comments", txtComment.Text);
                cmd.Parameters.AddWithValue("@TaxTotal", txtTax.Value);

                cmd.Parameters.AddWithValue("@FreightTaxCode", txtFTaxCode.Text);
                cmd.Parameters.AddWithValue("@FreightTaxRate", FreightTaxRate);

                if (chk_TaxInclusive.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "N");
                }
                if (lblType.Text == "BILL")
                {
                    cmd.Parameters.AddWithValue("@POStatus", "Open");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                }
                if (lblType.Text == "ORDER")
                {
                    purchasetype = "ORDER";
                    cmd.Parameters.AddWithValue("@POStatus", "Order");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                }
                if (lblType.Text == "RECEIVE ITEMS")
                {
                    purchasetype = "ORDER";
                    cmd.Parameters.AddWithValue("@POStatus", "Received");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                }
                if (lblType.Text == "QUOTE")
                {
                    cmd.Parameters.AddWithValue("@POStatus", "Quote");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                }
                cmd.Parameters.AddWithValue("@PurchaseType", purchasetype);



                con_.Open();

                int count = cmd.ExecuteNonQuery();


                //PurchaseLines
                //PurchaseLines
                string Descript;
                string Amount = "";
                string Job = "";
                string Tax = "";

                string lTaxPaidAccountID = "";
                float lTaxInc = 0;
                float lTaxEx = 0;
                float lTaxRate = 0;
                float lPriceEx = 0;
                float lPriceInc = 0;

                string lOrderedQty = "0";
                string lReceivedQty = "0";


                string lDiscountRate = "0";
                string lToDateQty = "0";
                float lTotalReceived = 0;
                float lTSubTotal = 0;
                float lTGrandTotal = 0;
                float lTempLineSub = 0;
                float lTemLineTotal = 0;

                int entity = 0;
                for (int x = 0; x < this.dgPurchase.Rows.Count; x++)
                {
                    if (this.dgPurchase.Rows[x].Cells["Description"].Value != null)
                    {
                        if (this.dgPurchase.Rows[x].Cells["Description"].Value.ToString() != "")
                        {
                            Descript = String.Format("{0}", dgPurchase.Rows[x].Cells["Description"].Value.ToString());
                            Amount = dgPurchase.Rows[x].Cells["Amount"].Value.ToString();
                            double dAmount = double.Parse(Amount, NumberStyles.Currency);
                            lTaxInc = float.Parse(dgPurchase.Rows[x].Cells["TaxInclusiveAmount"].Value.ToString());
                            lTaxEx = float.Parse(dgPurchase.Rows[x].Cells["TaxExclusiveAmount"].Value.ToString());
                            lTaxRate = float.Parse(dgPurchase.Rows[x].Cells["TaxRate"].Value.ToString());
                            lPriceEx = float.Parse(dgPurchase.Rows[x].Cells["Price"].Value.ToString());
                            if (this.chk_TaxInclusive.Checked)
                            {
                                lPriceInc = lPriceEx;
                                lPriceEx = lPriceEx / (1 + (lTaxRate / 100));

                            }
                            else
                            {
                                lPriceInc = lPriceEx * (1 + (lTaxRate / 100));
                            }
                            entity = Convert.ToInt32(dgPurchase.Rows[x].Cells["ItemID"].Value);
                            Job = dgPurchase.Rows[x].Cells["JobID"].Value == null ? "0" : dgPurchase.Rows[x].Cells["JobID"].Value.ToString();
                            Tax = dgPurchase.Rows[x].Cells["TaxCode"].Value.ToString();
                            lTaxPaidAccountID = dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value == null ? "0" : dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value.ToString();

                            lReceivedQty = dgPurchase.Rows[x].Cells["ReceivedQty"].Value == null ? "0" : dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString();
                            lOrderedQty = dgPurchase.Rows[x].Cells["OrderedQty"].Value == null ? "0" : dgPurchase.Rows[x].Cells["OrderedQty"].Value.ToString();
                            lDiscountRate = dgPurchase.Rows[x].Cells["DiscountRate"].Value == null ? "0" : dgPurchase.Rows[x].Cells["DiscountRate"].Value.ToString();
                            lToDateQty = dgPurchase.Rows[x].Cells["ToDateQty"].Value == null ? "0" : dgPurchase.Rows[x].Cells["ToDateQty"].Value.ToString();
                            lTotalReceived = float.Parse(lToDateQty) + float.Parse(lReceivedQty);
                            float lSubTotal = 0;
                            float lGrandTotal = 0;
                            if (lblType.Text == "ORDER" || lblType.Text == "QUOTE")
                            {

                                lSubTotal = float.Parse(lOrderedQty) * lPriceEx;
                                lGrandTotal = float.Parse(lOrderedQty) * lPriceInc;


                            }
                            if (lblType.Text == "BILL")
                            {
                                lSubTotal = float.Parse(lReceivedQty) * lPriceEx;
                                lGrandTotal = float.Parse(lReceivedQty) * lPriceInc;

                            }
                            if (lblType.Text == "RECEIVE ITEMS")
                            {
                                lSubTotal = lTotalReceived * lPriceEx;
                                lGrandTotal = lTotalReceived * lPriceInc;

                            }
                            lTSubTotal += lSubTotal;
                            lTGrandTotal += lGrandTotal;
                            DateTime sDate = dtDatePurchase.Value.ToUniversalTime();
                            string purchaseLinesql;
                            purchaseLinesql = @"INSERT INTO PurchaseLines(
                                            PurchaseID, JobID, EntityID, TransactionDate, OrderQty, ReceiveQty, UnitPrice, ActualUnitPrice, 
                                            DiscountPercent, SubTotal, TotalAmount, Description, TaxCode, TaxAmount, TaxPaidAccountID, TaxRate)
                                            VALUES(
                                            " + pPurchaseID + ", " + Job + "," + entity.ToString() + ", @TransactionDate, " + lOrderedQty + ", " + lTotalReceived.ToString() + ", " + lPriceEx.ToString() + ", " + lPriceEx.ToString() + " , " +
                           lDiscountRate + ", " + lSubTotal.ToString() + ", " + lGrandTotal.ToString() + ", '" + Descript + "','" + Tax + "', " + (lGrandTotal - lSubTotal).ToString() + ", " + lTaxPaidAccountID + "," + lTaxRate.ToString() + ")";

                            cmd.CommandText = purchaseLinesql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                if (txtSubTotal.Value != (decimal)lTGrandTotal)
                {
                    cmd.CommandText = "UPDATE Purchases set SubTotal = " + lTSubTotal.ToString() + ", GrandTotal = " + lTGrandTotal.ToString() + " + " + FreightAmountEx.ToString() + " + " + FreightTax.ToString() + " where PurchaseID = " + pPurchaseID;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "UPDATE Purchases set TotalDue = GrandTotal - TotalPaid where PurchaseID = " + pPurchaseID;
                    cmd.ExecuteNonQuery();

                }
                if (pOldPurchaseNo != pNewPurchaseNo)
                {
                    UpdateTransSeries(ref cmd);
                }
                UpdateTerm(TermReferenceID);
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

        void UpdateTransSeries(ref SqlCommand pCmd, string pType = "")
        {
            if (pType == "")
            {
                pType = lblType.Text;
            }
            string sql = "";
            if (pType == "ORDER")
            {
                sql = "UPDATE TransactionSeries SET PurchaseOrderSeries = '" + CurSeries + "'";
            }
            else
            {
                sql = "UPDATE TransactionSeries SET ReceivedItemsSeries = '" + CurSeries + "'";
            }

            pCmd.CommandText = sql;
            int res2 = pCmd.ExecuteNonQuery();
        }

        public int SavePurchase(bool pIsRecurring = false)
        {
            CreateNewItemPurchase(pIsRecurring);
            return NewPurchaseID;
        }



        private void CreateNewItemPurchase(bool isRecurring = false)
        {
            SqlConnection con = null;
            try
            {
                if (txtSupplier.Text == "")
                {
                    MessageBox.Show("Pls Select a Supplier");
                }
                else
                {
                    if (!isRecurring)
                        GenerateBillNum();
                    else
                    {
                        lblPurchaseNum.Text = purchasenumber = "RECURRING";
                    }

                    if (purchasenumber != "")
                    {

                        int NewTermID = 0;
                        NewTermID = NewTerm();

                        string layout = "";
                        string purchasetype = lblType.Text;
                        string sqli = @"INSERT INTO Purchases (
                                        SupplierID, 
                                        UserID, 
                                        ShippingContactID, 
                                        BillingContactID, 
                                        TermsReferenceID, 
                                        PurchaseType, 
                                        LayoutType, 
                                        PurchaseNumber,
                                        TransactionDate, 
                                        SubTotal, 
                                        FreightSubTotal,
                                        FreightTax, 
                                        GrandTotal,                                         
                                      
                                        PurchaseReference, 
                                        ShippingMethodID, 
                                        PromiseDate, 
                                        Memo, 
                                        Comments, 
                                        POStatus, 
                                        TaxTotal, 
                                        IsTaxInclusive, 
                                        ClosedDate,
                                        FreightTaxCode,
                                        FreightTaxRate) 
                                    VALUES (
                                        @SupplierID, 
                                        @UserID, 
                                        @ShippingContactID, 
                                        @BillingContactID, 
                                        @TermsReferenceID, 
                                        @PurchaseType, 
                                        @LayoutType, 
                                        @PurchaseNumber, 
                                        @TransactionDate, 
                                        @SubTotal, 
                                        @FreightSubTotal, 
                                        @FreightTax,
                                        @GrandTotal,                                       
                                      
                                        @PurchaseReference, 
                                        @ShippingMethodID, 
                                        @PromiseDate, 
                                        @Memo, 
                                        @Comments, 
                                        @POStatus, 
                                        @TaxTotal, 
                                        @IsTaxInclusive, 
                                        @ClosedDate,
                                        @FreightTaxCode,
                                        @FreightTaxRate); SELECT SCOPE_IDENTITY()";

                        con = new SqlConnection(CommonClass.ConStr);
                        SqlCommand cmd = new SqlCommand(sqli, con);

                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                        cmd.Parameters.AddWithValue("@PurchaseNumber", lblPurchaseNum.Text);
                        cmd.Parameters.AddWithValue("@PromiseDate", dtPromisedDate.Value.ToUniversalTime());
                        // cmd.Parameters.AddWithValue("@SupplierINVNumber", txtSupInvNum.Text);
                        cmd.Parameters.AddWithValue("@TransactionDate", dtDatePurchase.Value.ToUniversalTime());
                        string lTranDate = dtDatePurchase.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        layout = "Item";

                        cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                        int scontactid = cmb_shippingcontact.SelectedIndex + 1;
                        cmd.Parameters.AddWithValue("@ShippingContactID", scontactid/*shipAddressID*/);
                        cmd.Parameters.AddWithValue("@BillingContactID", scontactid);
                        cmd.Parameters.AddWithValue("@TermsReferenceID", NewTermID);
                        cmd.Parameters.AddWithValue("@LayoutType", layout);
                        cmd.Parameters.AddWithValue("@SubTotal", txtSubTotal.Value);
                        cmd.Parameters.AddWithValue("@FreightSubTotal", FreightAmountEx);
                        cmd.Parameters.AddWithValue("@FreightTax", FreightTax);
                        cmd.Parameters.AddWithValue("@GrandTotal", txtTotalAmount.Value);

                        cmd.Parameters.AddWithValue("@PurchaseReference", txtReference.Text);
                        cmd.Parameters.AddWithValue("@ShippingMethodID", ShippingID);
                        cmd.Parameters.AddWithValue("@Memo", txtMemo.Text);
                        cmd.Parameters.AddWithValue("@Comments", txtComment.Text);
                        cmd.Parameters.AddWithValue("@TaxTotal", txtTax.Value);

                        cmd.Parameters.AddWithValue("@FreightTaxCode", txtFTaxCode.Text);
                        cmd.Parameters.AddWithValue("@FreightTaxRate", FreightTaxRate);

                        if (chk_TaxInclusive.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@isTaxInclusive", "N");
                        }


                        if (lblType.Text == "ORDER")
                        {
                            purchasetype = "ORDER";
                            cmd.Parameters.AddWithValue("@POStatus", "New");
                            cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                        }


                        cmd.Parameters.AddWithValue("@PurchaseType", purchasetype);

                        con.Open();
                        int PurchaseID = Convert.ToInt32(cmd.ExecuteScalar());
                        NewPurchaseID = PurchaseID;

                        if (PurchaseID != 0)
                        {
                            //PurchaseLines
                            string Descript;
                            string Amount = "0";
                            string Job = "0";
                            string Tax = "0";

                            string lTaxPaidAccountID = "0";
                            float lTaxInc = 0;
                            float lTaxEx = 0;
                            float lTaxRate = 0;
                            float lPriceEx = 0;
                            string lOrderedQty = "0";
                            string lReceivedQty = "0";
                            string lToDateQty = "0";
                            string lDiscountRate = "0";

                            int entity = 0;
                            for (int x = 0; x < this.dgPurchase.Rows.Count; x++)
                            {
                                Dictionary<string, object> paramPurchaseLine = new Dictionary<string, object>();
                                if (this.dgPurchase.Rows[x].Cells["Description"].Value != null)
                                {
                                    if (this.dgPurchase.Rows[x].Cells["Description"].Value.ToString() != "")
                                    {
                                        Descript = String.Format("{0}", dgPurchase.Rows[x].Cells["Description"].Value.ToString());
                                        Amount = dgPurchase.Rows[x].Cells["Amount"].Value.ToString();
                                        double dAmount = double.Parse(Amount, NumberStyles.Currency);
                                        lTaxInc = float.Parse(dgPurchase.Rows[x].Cells["TaxInclusiveAmount"].Value.ToString());
                                        lTaxEx = float.Parse(dgPurchase.Rows[x].Cells["TaxExclusiveAmount"].Value.ToString());
                                        lTaxRate = float.Parse(dgPurchase.Rows[x].Cells["TaxRate"].Value.ToString());
                                        lPriceEx = float.Parse(dgPurchase.Rows[x].Cells["Price"].Value.ToString(), NumberStyles.Currency);
                                        if (this.chk_TaxInclusive.Checked)
                                        {
                                            lPriceEx = lPriceEx / (1 + (lTaxRate / 100));
                                        }
                                        entity = Convert.ToInt32(dgPurchase.Rows[x].Cells["ItemID"].Value);
                                        if (dgPurchase.Rows[x].Cells["JobID"].Value != null
                                            && dgPurchase.Rows[x].Cells["JobID"].Value.ToString() != "")
                                            Job = dgPurchase.Rows[x].Cells["JobID"].Value.ToString();

                                        Tax = dgPurchase.Rows[x].Cells["TaxCode"].Value.ToString();

                                        if (dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value != null
                                            && dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value.ToString() != "")
                                            lTaxPaidAccountID = dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value.ToString();

                                        if (dgPurchase.Rows[x].Cells["ReceivedQty"].Value != null
                                            && dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString() != "")
                                            lReceivedQty = dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString();

                                        if (dgPurchase.Rows[x].Cells["OrderedQty"].Value != null
                                            && dgPurchase.Rows[x].Cells["OrderedQty"].Value.ToString() != "")
                                            lOrderedQty = dgPurchase.Rows[x].Cells["OrderedQty"].Value.ToString();

                                        if (dgPurchase.Rows[x].Cells["DiscountRate"].Value != null
                                            && dgPurchase.Rows[x].Cells["DiscountRate"].Value.ToString() != "")
                                            lDiscountRate = dgPurchase.Rows[x].Cells["DiscountRate"].Value.ToString();


                                        DateTime sDate = dtDatePurchase.Value.ToUniversalTime();
                                        string purchaseLinesql = @"INSERT INTO PurchaseLines(
                                            PurchaseID, JobID, EntityID, TransactionDate, OrderQty, ReceiveQty, UnitPrice, ActualUnitPrice, 
                                            DiscountPercent, SubTotal, TotalAmount, Description, TaxCode, TaxAmount, TaxPaidAccountID, TaxRate)
                                            VALUES( @PurchaseID, @JobID, @EntityID, @TransactionDate, @OrderQty, @ReceiveQty, @UnitPrice, @ActualUnitPrice, 
                                            @DiscountPercent, @SubTotal, @TotalAmount, @Description, @TaxCode, @TaxAmount, @TaxPaidAccountID, @TaxRate)";

                                        paramPurchaseLine.Add("@PurchaseID", PurchaseID);
                                        paramPurchaseLine.Add("@JobID", Job);
                                        paramPurchaseLine.Add("@EntityID", entity);
                                        paramPurchaseLine.Add("@OrderQty", lOrderedQty);
                                        paramPurchaseLine.Add("@ReceiveQty", lReceivedQty);
                                        paramPurchaseLine.Add("@UnitPrice", lPriceEx);
                                        paramPurchaseLine.Add("@ActualUnitPrice", lPriceEx);
                                        paramPurchaseLine.Add("@DiscountPercent", lDiscountRate);
                                        paramPurchaseLine.Add("@SubTotal", lTaxEx);
                                        paramPurchaseLine.Add("@TotalAmount", lTaxInc);
                                        paramPurchaseLine.Add("@Description", Descript);
                                        paramPurchaseLine.Add("@TaxCode", Tax);
                                        paramPurchaseLine.Add("@TaxAmount", (lTaxInc - lTaxEx));
                                        paramPurchaseLine.Add("@TaxPaidAccountID", lTaxPaidAccountID);
                                        paramPurchaseLine.Add("@TaxRate", lTaxRate);
                                        paramPurchaseLine.Add("@TransactionDate", dtDatePurchase.Value.ToUniversalTime());

                                        CommonClass.runSql(purchaseLinesql, CommonClass.RunSqlInsertMode.QUERY, paramPurchaseLine);
                                    }
                                }
                            }
                        }

                        UpdateTransSeries(ref cmd);

                        CommonClass.SaveSystemLogs(CommonClass.UserID, "Purchases " + lblType.Text, "Created " + lblType.Text + " Number " + purchasenumber + " with Purchase ID " + PurchaseID.ToString(), PurchaseID.ToString());
                        DialogResult PrintInvoice = MessageBox.Show("Would you like to print " + this.lblType.Text + "?", "Information", MessageBoxButtons.YesNo);
                        if (PrintInvoice == DialogResult.Yes)
                        {
                            LoadItemLayoutReport(NewPurchaseID.ToString());
                        }
                        if (PurchaseID != 0 && SrcOfInvoke != CommonClass.InvocationSource.SAVERECURRING)
                        {
                            SrcOfInvoke = CommonClass.InvocationSource.SELF;
                            ToPurchaseType = "";
                            XpurchaseID = PurchaseID.ToString();
                            ReloadPurchase();


                        }
                    }
                }//if statement

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private int CreateNewReceiveItems(DateTime pRDate, string pSupInv, string pRef, string pPurchaseID, bool isRecurring = false)
        {
            SqlConnection con = null;
            try
            {
                GenerateBillNum();
                if (receiveitemnumber != "")
                {

                    string layout = "";
                    string purchasetype = lblType.Text;
                    string sqli = @"INSERT INTO ReceiveItems (
                                        SupplierID, 
                                        UserID, 
                                        TermsReferenceID, 
                                        ShippingContactID, 
                                        BillingContactID,                                         
                                        PurchaseID,                                       
                                        ReceiveItemNumber,
                                        TransactionDate, 
                                        SubTotal, 
                                        FreightSubTotal,
                                        FreightTax, 
                                        GrandTotal, 
                                        SupplierINVNumber,
                                        ReceiveItemReference, 
                                        ShippingMethodID,                                        
                                        Memo, 
                                        Comments,                                         
                                        TaxTotal, 
                                        IsTaxInclusive,                                       
                                        FreightTaxCode,
                                        FreightTaxRate) 
                                    VALUES (
                                        @SupplierID, 
                                        @UserID, 
                                        @TermsReferenceID, 
                                        @ShippingContactID, 
                                        @BillingContactID,                                         
                                        @PurchaseID,                                       
                                        @ReceiveItemNumber,
                                        @TransactionDate, 
                                        @SubTotal, 
                                        @FreightSubTotal,
                                        @FreightTax, 
                                        @GrandTotal, 
                                        @SupplierINVNumber,
                                        @ReceiveItemReference, 
                                        @ShippingMethodID,                                       
                                        @Memo, 
                                        @Comments,                                         
                                        @TaxTotal, 
                                        @IsTaxInclusive,                                       
                                        @FreightTaxCode,
                                        @FreightTaxRate); SELECT SCOPE_IDENTITY()";

                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(sqli, con);

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                    cmd.Parameters.AddWithValue("@TermsReferenceID", TermReferenceID);
                    int scontactid = cmb_shippingcontact.SelectedIndex + 1;
                    cmd.Parameters.AddWithValue("@ShippingContactID", scontactid/*shipAddressID*/);
                    cmd.Parameters.AddWithValue("@BillingContactID", scontactid);
                    cmd.Parameters.AddWithValue("@PurchaseID", pPurchaseID);
                    cmd.Parameters.AddWithValue("@ReceiveItemNumber", receiveitemnumber);
                    cmd.Parameters.AddWithValue("@TransactionDate", pRDate.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@SubTotal", txtSubTotal.Value);
                    cmd.Parameters.AddWithValue("@FreightSubTotal", FreightAmountEx);
                    cmd.Parameters.AddWithValue("@GrandTotal", txtTotalAmount.Value);
                    cmd.Parameters.AddWithValue("@SupplierINVNumber", pSupInv);
                    cmd.Parameters.AddWithValue("@ReceiveItemReference", pRef);
                    cmd.Parameters.AddWithValue("@ShippingMethodID", ShippingID);
                    cmd.Parameters.AddWithValue("@Memo", txtMemo.Text);
                    cmd.Parameters.AddWithValue("@Comments", txtComment.Text);
                    cmd.Parameters.AddWithValue("@TaxTotal", txtTax.Value);
                    cmd.Parameters.AddWithValue("@FreightTax", FreightTax);
                    if (chk_TaxInclusive.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isTaxInclusive", "N");
                    }
                    cmd.Parameters.AddWithValue("@FreightTaxCode", txtFTaxCode.Text);
                    cmd.Parameters.AddWithValue("@FreightTaxRate", FreightTaxRate);
                    string lTranDate = pRDate.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    con.Open();
                    int ReceiveItemID = Convert.ToInt32(cmd.ExecuteScalar());
                    NewPurchaseID = ReceiveItemID;

                    if (ReceiveItemID != 0)
                    {
                        //PurchaseLines
                        string Descript;
                        string Amount = "0";
                        string Job = "0";
                        string Tax = "0";

                        string lTaxPaidAccountID = "0";
                        float lTaxInc = 0;
                        float lTaxEx = 0;
                        float lTaxRate = 0;
                        float lPriceEx = 0;
                        string lOrderedQty = "0";
                        string lReceivedQty = "0";
                        string lToDateQty = "0";
                        string lDiscountRate = "0";

                        int entity = 0;
                        for (int x = 0; x < this.dgPurchase.Rows.Count; x++)
                        {
                            if (this.dgPurchase.Rows[x].Cells["ReceivedQty"].Value != null)
                            {
                                Dictionary<string, object> paramline = new Dictionary<string, object>();

                                string lRQ = this.dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString();
                                if (lRQ != "" && lRQ != "0")
                                {
                                    Descript = String.Format("{0}", dgPurchase.Rows[x].Cells["Description"].Value.ToString());
                                    Amount = dgPurchase.Rows[x].Cells["Amount"].Value.ToString();
                                    double dAmount = double.Parse(Amount, NumberStyles.Currency);
                                    lTaxInc = float.Parse(dgPurchase.Rows[x].Cells["TaxInclusiveAmount"].Value.ToString());
                                    lTaxEx = float.Parse(dgPurchase.Rows[x].Cells["TaxExclusiveAmount"].Value.ToString());
                                    lTaxRate = float.Parse(dgPurchase.Rows[x].Cells["TaxRate"].Value.ToString());
                                    lPriceEx = float.Parse(dgPurchase.Rows[x].Cells["Price"].Value.ToString());
                                    if (this.chk_TaxInclusive.Checked)
                                    {
                                        lPriceEx = lPriceEx / (1 + (lTaxRate / 100));
                                    }
                                    entity = Convert.ToInt32(dgPurchase.Rows[x].Cells["ItemID"].Value);
                                    if (dgPurchase.Rows[x].Cells["JobID"].Value != null
                                        && dgPurchase.Rows[x].Cells["JobID"].Value.ToString() != "")
                                        Job = dgPurchase.Rows[x].Cells["JobID"].Value.ToString();

                                    Tax = dgPurchase.Rows[x].Cells["TaxCode"].Value.ToString();

                                    if (dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value != null
                                        && dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value.ToString() != "")
                                        lTaxPaidAccountID = dgPurchase.Rows[x].Cells["TaxPaidAccountID"].Value.ToString();

                                    if (dgPurchase.Rows[x].Cells["ReceivedQty"].Value != null
                                        && dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString() != "")
                                        lReceivedQty = dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString();

                                    if (dgPurchase.Rows[x].Cells["OrderedQty"].Value != null
                                        && dgPurchase.Rows[x].Cells["OrderedQty"].Value.ToString() != "")
                                        lOrderedQty = dgPurchase.Rows[x].Cells["OrderedQty"].Value.ToString();

                                    if (dgPurchase.Rows[x].Cells["DiscountRate"].Value != null
                                        && dgPurchase.Rows[x].Cells["DiscountRate"].Value.ToString() != "")
                                        lDiscountRate = dgPurchase.Rows[x].Cells["DiscountRate"].Value.ToString();


                                    DateTime sDate = dtDatePurchase.Value.ToUniversalTime();
                                    string purchaseLinesql = @"INSERT INTO ReceiveItemsLines(
                                            ReceiveItemID, JobID, EntityID, TransactionDate, OrderQty, ReceiveQty, UnitPrice, ActualUnitPrice, 
                                            DiscountPercent, SubTotal, TotalAmount, Description, TaxCode, TaxAmount, TaxPaidAccountID, TaxRate)
                                            VALUES(
                                            " + ReceiveItemID + ", " + Job + "," + entity.ToString() + ", @TransactionDate, " + lOrderedQty + ", " + lReceivedQty + ", " + lPriceEx.ToString() + ", " + lPriceEx.ToString() + " , " +
                                        lDiscountRate + ", " + lTaxEx.ToString() + ", " + lTaxInc.ToString() + ", @Descript,'" + Tax + "', " + (lTaxInc - lTaxEx).ToString() + ", '" + lTaxPaidAccountID + "'," + lTaxRate.ToString() + ")";
                                    paramline.Add("@Descript", Descript);
                                    paramline.Add("@TransactionDate", pRDate.ToUniversalTime());
                                    CommonClass.runSql(purchaseLinesql, CommonClass.RunSqlInsertMode.SCALAR, paramline);


                                    //UPDATE ReceiveQty of PurchaseLines
                                    string lLineID = this.dgPurchase.Rows[x].Cells["LineID"].Value.ToString();
                                    purchaseLinesql = "UPDATE PurchaseLines set ReceiveQty = ReceiveQty + " + lReceivedQty + " where PurchaseLineID = " + lLineID;
                                    cmd.CommandText = purchaseLinesql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    UpdateTransSeries(ref cmd);

                    string lptrantype = "RI";
                    CreateItemJournalEntries(ReceiveItemID.ToString(), lptrantype, AP_AccountID, receiveitemnumber);
                    //UPDATE PO Status of Purchase

                    if (IsPOCompleted(pPurchaseID))
                    {
                        string lClosedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        cmd.CommandText = "Update Purchases set POStatus = 'Completed', ClosedDate = '" + lClosedDate + "' where PurchaseID = " + pPurchaseID;
                    }
                    else
                    {
                        cmd.CommandText = "Update Purchases set POStatus = 'Backordered' where PurchaseID = " + pPurchaseID;
                    }
                    cmd.ExecuteNonQuery();
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Items Received for  Purchase Order " + this.lblPurchaseNum.Text + " on ReceiveItemNumber " + purchasenumber, this.lblID.Text);
                    DialogResult PrintInvoice = MessageBox.Show("Would you like to print " + this.lblType.Text + "?", "Information", MessageBoxButtons.YesNo);
                    if (PrintInvoice == DialogResult.Yes)
                    {
                        LoadItemLayoutReport(NewPurchaseID.ToString());
                    }
                    //if (ReceiveItemID != 0 && SrcOfInvoke != CommonClass.InvocationSource.SAVERECURRING)
                    //{
                    //    string titles = "Information";
                    //    DialogResult createNew = MessageBox.Show("Purchase " + lblType.Text + " has been created. Would you like to enter a new " + lblType.Text + "?", titles, MessageBoxButtons.YesNo);

                    //    if (createNew == DialogResult.Yes)
                    //    {   //clear for new datas
                    //        dgPurchase.Rows.Clear();
                    //        dgPurchase.Refresh();
                    //        txtSupplier.Clear();
                    //        PayeeInfo.Clear();
                    //        lblPurchaseNum.Visible = false;
                    //        txtTotalAmount.Value = 0;
                    //        dtPromisedDate.Value = DateTime.Now.ToUniversalTime();
                    //        dtDatePurchase.Value = DateTime.Now.ToUniversalTime();
                    //        txtShipVia.Clear();
                    //        NewPurchase(lblType.Text);
                    //    }
                    //    else if (createNew == DialogResult.No)
                    //    {
                    //        CommonClass.EnterPurchasefrm.Close();
                    //    }
                    //}
                }
                return NewPurchaseID;
            }
            catch (SqlException ex)
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

        private bool IsPOCompleted(string pPurchaseID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            bool RetVal = false;
            try
            {
                string sql = @"SELECT * from PurchaseLines where OrderQty <> ReceiveQty and PurchaseID = " + pPurchaseID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                if (RTb.Rows.Count == 0)
                {
                    RetVal = true;
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
            return RetVal;
        } //END
        private void label5_Click(object sender, EventArgs e)
        {
            ShowTermLookUp();
        }

        private void TaxCodeLookup_btn_Click()
        {
            TaxCodeLookup TaxCodeL = new TaxCodeLookup("");
            if (TaxCodeL.ShowDialog() == DialogResult.OK)
            {
                Tax = TaxCodeL.GetTax;
                DataGridViewRow dgvRows = dgPurchase.CurrentRow;
                dgvRows.Cells["TaxCode"].Value = Tax[0];
                dgvRows.Cells["TaxPaidAccountID"].Value = Tax[1];
                DataRow rTx = CommonClass.getTaxDetails(dgvRows.Cells["TaxCode"].Value.ToString());
                if (rTx.ItemArray.Length > 0)
                {
                    //  string lrate = (rTx["TaxPercentageRate"].ToString() == "" ? 0 : rTx["TaxPercentageRate"].ToString()).toString();
                    float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                    string lTaxPaidAccountID = "";
                    lTaxPaidAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                    dgvRows.Cells["TaxPaidAccountID"].Value = lTaxPaidAccountID;
                    dgvRows.Cells["TaxRate"].Value = ltaxrate;
                }
            }
        }

        private void dgPurchase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            switch (e.ColumnIndex)
            {
                case 6: // PART NO
                    if (txtSupplier.Text != "")
                    {
                        ShowItemLookup("", "", 0, 0);
                        RecalclineItem(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                    }
                    dgEnable();
                    break;

                case 11: //AMOUNT
                    this.dgPurchase.CurrentCell = this.dgPurchase.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgPurchase.BeginEdit(true);
                    break;
                case 12://Job
                    if (txtSupplier.Text != "")
                    {
                        ShowJobLookup();
                    }
                    break;
                case 13://Tax
                    if (txtSupplier.Text != "")
                    {
                        TaxCodeLookup_btn_Click();
                        RecalclineItem(e.ColumnIndex, e.RowIndex);
                        CalcOutOfBalance();
                    }
                    break;
                default:
                    break;
            }
        }

        public void ShowJobLookup(string jobSearch = "") //Jobs
        {
            SelectJobs DlgJob = new SelectJobs("D", jobSearch);

            if (DlgJob.ShowDialog() == DialogResult.OK)
            {
                Jobs = DlgJob.GetJob;
                DataGridViewRow dgvRows = dgPurchase.CurrentRow;
                dgvRows.Cells["JobID"].Value = Jobs[0].ToString();
                dgvRows.Cells["Job"].Value = Jobs[2].ToString();
            }
        }

        public void ShowTaxLookup(string TaxSearch = "")
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup(TaxSearch);
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                DataGridViewRow dgvRows = dgPurchase.CurrentRow;
                dgvRows.Cells["TaxCode"].Value = Tax[0];
                dgvRows.Cells["TaxRate"].Value = Tax[2];
                dgvRows.Cells["TaxPaidAccountID"].Value = Tax[4];
            }
        }



        void CalcOutOfBalance()
        {
            this.txtTotalAmount.Value = 0;
            this.txtTax.Value = 0;
            decimal TotalTaxEx = 0;
            decimal TotalTaxInc = 0;
            decimal outnum;
            decimal TaxEx = 0;
            decimal TaxInc = 0;
            decimal CurAmt = 0;

            for (int i = 0; i < this.dgPurchase.Rows.Count; i++)
            {
                if (this.dgPurchase.Rows[i].Cells["Amount"].Value != null)
                {
                    if (this.dgPurchase.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        Decimal.TryParse(this.dgPurchase.Rows[i].Cells["Amount"].Value.ToString(), out outnum);
                        this.txtTotalAmount.Value += outnum;
                        CurAmt = outnum;
                        if (CurAmt != 0)
                        {
                            Decimal.TryParse(dgPurchase.Rows[i].Cells["TaxExclusiveAmount"].Value.ToString(), out outnum);
                            TaxEx = outnum;
                            TotalTaxEx += TaxEx;
                            Decimal.TryParse(this.dgPurchase.Rows[i].Cells["TaxInclusiveAmount"].Value.ToString(), out outnum);
                            TaxInc = outnum;
                            TotalTaxInc += TaxInc;

                        }
                    }
                }
            }

            this.txtTax.Value = (TotalTaxInc - TotalTaxEx) + (decimal)FreightTax;
            txtSubTotal.Value = TotalTaxEx;
            txtTotalAmount.Value = TotalTaxInc + (decimal)FreightAmountInc;

        }

        private void dgPurchase_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 11
                || e.ColumnIndex == 8
                && e.RowIndex != this.dgPurchase.NewRowIndex)
            {
                if (e.Value != null)
                {
                    decimal d = decimal.Parse(e.Value.ToString(), NumberStyles.Any);
                    e.Value = d.ToString("C2");
                }
            }
            if (e.ColumnIndex == 9 && e.RowIndex != this.dgPurchase.NewRowIndex)
            {
                if (e.Value != null)
                {
                    string p = e.Value.ToString().Replace("%", "");
                    float d = float.Parse(p);
                    e.Value = Math.Round(d, 2).ToString() + "%";
                }
            }
        }

        public static DataTable GetPurchaseLine(string pPurchaseID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = "SELECT l.*, p.PurchaseNumber, p.GrandTotal, p.Memo, p.FreightSubTotal, p.FreightTax, p.FreightTaxCode, p.FreightTaxRate, i.AssetAccountID, i.IsCounted, i.COSAccountID, i.IsBought FROM ( PurchaseLines l INNER JOIN Purchases p ON l.PurchaseID = p.PurchaseID ) left join Items as i on l.EntityID =i.ID WHERE l.PurchaseID = " + pPurchaseID;
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



        private bool CreateItemJournalEntries(string pID, string pTransactionType, string pAP_Account, string pReceiveItemNo, bool pRecalcAveCost = true)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                int lReceiveItemID = Convert.ToInt16(pID);
                DataTable ltb = GetReceiveItemLines(lReceiveItemID);
                if (ltb.Rows.Count > 0)
                {

                    float lGrandTotal = float.Parse(ltb.Rows[0]["GrandTotal"].ToString());
                    string lReceiveItemNumber = pReceiveItemNo;
                    string lMemo = String.Format("{0}", ltb.Rows[0]["Memo"]);
                    string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    string lAccountID = "0";
                    float lFreightEx = float.Parse(ltb.Rows[0]["FreightSubTotal"].ToString());
                    float lFreightTax = float.Parse(ltb.Rows[0]["FreightTax"].ToString());
                    float lTotalItemCost = 0;
                    int recordinserted = 0;
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@LineMemo", lMemo);
                    param.Add("@Memo", lMemo);

                    //INSERT JOURNAL FOR Total Amount Received
                    if (lGrandTotal < 0)
                    {
                        //NEGATIVE SO DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + pAP_Account + "', " +
                              (lGrandTotal * -1).ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "'," + pID + ")";
                    }
                    else
                    {
                        //CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                             " VALUES('" + lTranDate + "',  @Memo, @LineMemo, '" + pAP_Account + "', " +
                            lGrandTotal.ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "'," + pID + ")";
                    }
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

                    //FOR PURCHASE LINES

                    //INSERT JOURNAL FOR FREIGHT 
                    if (lFreightEx != 0)
                    {
                        if (lFreightEx < 0) // NEGATIVE SO CREDIT AMOUNT 
                        {
                            // NEGATIVE SO CREDIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate + "',  @Memo, @LineMemo, '" + AP_FreightAccountID + "', " +
                                  (lFreightEx * -1).ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "',0,'" + pID + "')";
                            recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                            //THIS IS FOR THE TAX COMPONENT
                            if (lFreightTax != 0 && FreightTaxAccountID != "")
                            {

                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "',  @Memo, @LineMemo, '" + FreightTaxAccountID + "', " +
                                      (lFreightTax * -1).ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "',0,'" + pID + "')";
                                
                                recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                            }
                        }
                        else //POSITIVE SO DEBIT AMOUNT 
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate + "',  @Memo, @LineMemo, '" + AP_FreightAccountID + "', " +
                                       lFreightEx.ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "',0," + pID + ")";
                            recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                            //THIS IS FOR THE TAX COMPONENT
                            if (lFreightTax != 0 && FreightTaxAccountID != "")
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "',  @Memo, @LineMemo, '" + FreightTaxAccountID + "', " +
                                         lFreightTax.ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "',0," + pID + ")";
                                recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                            }
                        }
                    }
                    string Descript;
                    string Amount = "";
                    string lJobID = "";
                    string Tax = "";
                    string lTaxPaidAccountID = "";
                    float lTaxInc = 0;
                    float lTaxEx = 0;
                    float lTaxRate = 0;
                    float lPriceEx = 0;
                    float lPriceInc = 0;
                    float lTaxAmt = 0;
                    string lOrderedQty = "0";
                    string lReceivedQty = "0";
                    string lDiscountRate = "0";
                    int entity = 0;
                    string lEntityID = "";
                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        lEntityID = ltb.Rows[i]["EntityID"].ToString();
                        Descript = String.Format("{0}", ltb.Rows[i]["Description"].ToString());
                        //Amount = ltb.Rows[i]["Amount"].ToString();
                        //double dAmount = double.Parse(Amount, NumberStyles.Currency);
                        lTaxInc = float.Parse(ltb.Rows[i]["TotalAmount"].ToString());
                        lTaxEx = float.Parse(ltb.Rows[i]["SubTotal"].ToString());
                        lTaxAmt = lTaxInc - lTaxEx;

                        entity = Convert.ToInt32(ltb.Rows[i]["EntityID"].ToString());
                        lJobID = ltb.Rows[i]["JobID"] == null ? "0" : ltb.Rows[i]["JobID"].ToString();
                        Tax = ltb.Rows[i]["TaxCode"].ToString();
                        lTaxPaidAccountID = ltb.Rows[i]["TaxPaidAccountID"] == null ? "0" : ltb.Rows[i]["TaxPaidAccountID"].ToString();

                        lReceivedQty = ltb.Rows[i]["ReceiveQty"] == null ? "0" : ltb.Rows[i]["ReceiveQty"].ToString();
                        lOrderedQty = ltb.Rows[i]["OrderQty"] == null ? "0" : ltb.Rows[i]["OrderQty"].ToString();
                        lDiscountRate = ltb.Rows[i]["DiscountPercent"] == null ? "0" : ltb.Rows[i]["DiscountPercent"].ToString();

                        DataTable lItemTb = TransactionClass.GetItem(entity.ToString());
                        bool isCounted = (bool)lItemTb.Rows[0]["IsCounted"];

                        if (isCounted)
                        {
                            lAccountID = (lItemTb.Rows[0]["AssetAccountID"] != null ? lItemTb.Rows[0]["AssetAccountID"].ToString() : "0");
                        }
                        else
                        {
                            lAccountID = (lItemTb.Rows[0]["COSAccountID"] != null ? lItemTb.Rows[0]["COSAccountID"].ToString() : "0");
                        }

                        if (lTaxEx < 0) // NEGATIVE SO CREDIT AMOUNT 
                        {

                            // NEGATIVE SO CREDIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                       VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                  (lTaxEx * -1).ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "'," + lJobID + "," + entity + ")";
                            recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                            // }
                            //THIS IS FOR THE TAX COMPONENT                        
                            if (lTaxAmt != 0 && lTaxPaidAccountID != "")
                            {
                                lTaxAmt = lTaxEx - lTaxInc;
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lTaxPaidAccountID + "', " +
                                      lTaxAmt.ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "'," + lJobID + "," + entity + ")";
                                recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                            }
                        }
                        else //POSITIVE SO DEBIT AMOUNT 
                        {

                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                         VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                              lTaxEx.ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "'," + lJobID + "," + entity + ")";
                            recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);


                            //THIS IS FOR THE TAX COMPONENT                          
                            if (lTaxAmt != 0 && lTaxPaidAccountID != "")
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lTaxPaidAccountID + "', " +
                                      lTaxAmt.ToString() + ", '" + lReceiveItemNumber + "', '" + pTransactionType + "'," + lJobID + "," + entity + ")";
                                recordinserted = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                            }
                        }
                        //UPDATE ITEMS QTY & COST
                        float lQty = float.Parse(lReceivedQty);
                        float lTotalCost = lTaxEx;
                        float lUnitCost = (lQty == 0 ? 0 : lTotalCost / lQty);
                        string lTranID = pID;
                        float lTranQty = 0;
                        if (lQty != 0)
                        {
                            lTotalItemCost += lTotalCost;

                            if (lItemTb.Rows.Count > 0)
                            {
                                bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                                if (lIsCounted)
                                {

                                    float QtyPerBUOM = float.Parse(lItemTb.Rows[0]["QtyPerBuyingUOM"].ToString());
                                    lTranQty = lQty * QtyPerBUOM;

                                    float lOldQty = float.Parse(lItemTb.Rows[0]["OnHandQty"].ToString());
                                    float lOldCost = float.Parse(lItemTb.Rows[0]["AverageCostEx"].ToString());
                                    float lNewAveCost = ((lTranQty + lOldQty) == 0 ? 0 : ((lOldQty * lOldCost) + lTotalCost) / (lTranQty + lOldQty));
                                    float lLastCost = (lQty == 0 ? 0 : lTotalCost / lQty);

                                    //UPDATE onHAnd QTY

                                    sql = "UPDATE ItemsQty set OnHandQty = " + (lTranQty + lOldQty).ToString() + " where ItemID = " + entity;
                                    cmd.CommandText = sql;
                                    recordinserted = cmd.ExecuteNonQuery();
                                    if (pRecalcAveCost && lNewAveCost > 0)
                                    {
                                        //UPDATE AVECOST
                                        sql = "UPDATE ItemsCostPrice set PrevAverageCostEx = " + lOldCost + ", AverageCostEx = " + lNewAveCost + ", LastCostEx = " + lLastCost.ToString() + " , StandardCostEx = " + lLastCost.ToString() + " where ItemID = " + entity;
                                        cmd.CommandText = sql;
                                        recordinserted = cmd.ExecuteNonQuery();
                                    }
                                    lUnitCost = lTotalCost / lTranQty;
                                    // INSERT RECEIVED ITEMS IN ITEM TRANSACTION
                                    sql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                        VALUES('" + lTranDate + "'," + entity + "," + lTranQty.ToString() + "," + lTranQty.ToString() + "," + lUnitCost + "," + lTotalCost.ToString() + ",'" + pTransactionType + "'," + lTranID.ToString() + "," + CommonClass.UserID + ")";
                                    cmd.CommandText = sql;
                                    recordinserted = cmd.ExecuteNonQuery();

                                }
                            }


                        }

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
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }



        private void LoadTransaction(string pID, string pType = "ORDER")
        {
            IsLoading = true;
            int purchaseid = Convert.ToInt32(pID);
            string sID = "";
            float lTotalReceived = 0;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 
                string sql = "";
                if (pType == "ORDER")
                {
                    sql = @"SELECT p.*, c.* 
                             FROM Purchases p 
                             INNER JOIN Profile c ON p.SupplierID = c.ID
                             WHERE p.PurchaseID = " + purchaseid;
                }
                else
                {
                    sql = @"SELECT p.*, c.* 
                             FROM ReceiveItems p 
                             INNER JOIN Profile c ON p.SupplierID = c.ID
                             WHERE p.ReceiveItemID = " + purchaseid;
                }


                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRepPurchase = new DataTable();
                da.Fill(TbRepPurchase);

                if (TbRepPurchase.Rows.Count > 0)
                {
                    string dte = TbRepPurchase.Rows[0]["TransactionDate"].ToString();
                    dtDatePurchase.Value = DateTime.Parse(dte).ToLocalTime();
                    TermReferenceID = TbRepPurchase.Rows[0]["TermsReferenceID"].ToString();
                    sID = TbRepPurchase.Rows[0]["SupplierID"].ToString();

                    if (TermReferenceID == "0")
                    {
                        LoadDefaultTerms(sID);
                    }
                    else
                    {
                        LoadBillTerms();
                    }






                    SupplierID = TbRepPurchase.Rows[0]["SupplierID"].ToString();
                    txtComment.Text = TbRepPurchase.Rows[0]["Comments"].ToString();
                    LoadShippingMethod(TbRepPurchase.Rows[0]["ShippingMethodID"].ToString());
                    txtShipVia.Text = ShipVia;
                    txtSubTotal.Text = TbRepPurchase.Rows[0]["SubTotal"].ToString();
                    //txtSupInvNum.Text = TbRepPurchase.Rows[0]["SupplierINVNumber"].ToString();
                    this.cmb_shippingcontact.SelectedIndex = Convert.ToInt32(TbRepPurchase.Rows[0]["ShippingContactID"].ToString()) - 1;
                    LoadContacts(Convert.ToInt32(SupplierID), Convert.ToInt32(TbRepPurchase.Rows[0]["ShippingContactID"].ToString()));

                    txtSupplier.Text = TbRepPurchase.Rows[0]["Name"].ToString();
                    txtMemo.Text = TbRepPurchase.Rows[0]["Memo"].ToString();

                    if (pType == "ORDER")
                    {
                        lblPurchaseNum.Text = TbRepPurchase.Rows[0]["PurchaseNumber"].ToString();
                        defPurchaseNum = TbRepPurchase.Rows[0]["PurchaseNumber"].ToString();
                        FromPurchaseType = TbRepPurchase.Rows[0]["PurchaseType"].ToString();
                        lblType.Text = pType;
                        POStatus = TbRepPurchase.Rows[0]["POStatus"].ToString();
                        this.lblReceiveNo.Text = "";
                    }
                    else
                    {
                        this.lblReceiveNo.Text = TbRepPurchase.Rows[0]["ReceiveItemNumber"].ToString();
                    }
                    this.lblPOStatus.Text = POStatus;

                    FreightTax = float.Parse(TbRepPurchase.Rows[0]["FreightTax"].ToString());
                    FreightAmountEx = float.Parse(TbRepPurchase.Rows[0]["FreightSubTotal"].ToString());
                    FreightAmountInc = FreightTax + FreightAmountEx;
                    FreightTaxRate = float.Parse(TbRepPurchase.Rows[0]["FreightTaxRate"].ToString());
                    this.txtFTaxCode.Text = TbRepPurchase.Rows[0]["FreightTaxCode"].ToString();
                    //GET FreightTaxAccountID
                    DataRow rTx = CommonClass.getTaxDetails(this.txtFTaxCode.Text);
                    if (rTx.ItemArray.Length > 0)
                    {
                        FreightTaxAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                    }

                    istaxInclusive = TbRepPurchase.Rows[0]["IsTaxInclusive"].ToString();

                    if (istaxInclusive == "Y")
                    {
                        chk_TaxInclusive.Checked = true;
                        txtFreight.Value = (decimal)FreightAmountInc;
                    }
                    else
                    {
                        chk_TaxInclusive.Checked = false;
                        txtFreight.Value = (decimal)FreightAmountEx;
                    }

                    txtSubTotal.Value = Convert.ToDecimal(TbRepPurchase.Rows[0]["SubTotal"].ToString());
                    string strtotamt = TbRepPurchase.Rows[0]["GrandTotal"].ToString();
                    txtTotalAmount.Value = strtotamt != "" ? Convert.ToDecimal(strtotamt) : 0;
                    string strtottax = TbRepPurchase.Rows[0]["TaxTotal"].ToString();
                    txtTax.Value = strtottax != "" ? Convert.ToDecimal(strtottax) : 0;

                    string gridtype = pType;
                    if (SrcOfInvoke == CommonClass.InvocationSource.CHANGETO)
                    {
                        this.lblType.Text = "RECEIVE ITEMS";
                        gridtype = this.lblType.Text;
                    }

                    SetGrid(gridtype);
                    //GET PURCHASE LINES
                    //dt.Clear();

                    TbRepPurchaseLines = new DataTable();
                    DataGridViewRow DRow;

                    if (pType == "ORDER")
                    {
                        sql = @"SELECT  l.*, j.*, i.PartNumber 
                                FROM Purchases p 
                                INNER JOIN PurchaseLines l ON p.PurchaseID = l.PurchaseID 
                                LEFT JOIN Jobs j ON l.JobID = j.JobID 
                                INNER JOIN Items i ON i.ID = l.EntityID 
                                WHERE p.PurchaseID = " + purchaseid;
                        if (SrcOfInvoke == CommonClass.InvocationSource.CHANGETO)
                        {
                            sql += @" and OrderQty <> ReceiveQty ";
                        }

                    }
                    else
                    {
                        sql = @"SELECT  l.*, j.*, i.PartNumber 
                                FROM ReceiveItems p 
                                INNER JOIN ReceiveItemsLines l ON p.ReceiveItemID = l.ReceiveItemID 
                                LEFT JOIN Jobs j ON l.JobID = j.JobID 
                                INNER JOIN Items i ON i.ID = l.EntityID 
                                WHERE p.ReceiveItemID = " + purchaseid;
                    }

                    da = new SqlDataAdapter();
                    cmd.CommandText = sql;
                    da.SelectCommand = cmd;

                    da.Fill(TbRepPurchaseLines);

                    if (TbRepPurchaseLines.Rows.Count > 0)
                    {
                        dgPurchase.Rows.Clear();
                    }

                    for (int i = 0; i < TbRepPurchaseLines.Rows.Count; i++)
                    {
                        dgPurchase.Rows.Add();
                        DRow = dgPurchase.Rows[i];
                        // DRow.Cells[0].Value = dl.Rows[i]["MoneyInLineID"].ToString();

                        if (pType == "ORDER" || (this.lblType.Text == "RECEIVE ITEMS" && SrcOfInvoke == CommonClass.InvocationSource.CHANGETO))
                        {
                            DRow.Cells["LineID"].Value = TbRepPurchaseLines.Rows[i]["PurchaseLineID"].ToString();
                        }
                        else
                        {
                            DRow.Cells["LineID"].Value = TbRepPurchaseLines.Rows[i]["ReceiveItemID"].ToString();
                        }
                        DRow.Cells["ItemID"].Value = TbRepPurchaseLines.Rows[i]["EntityID"].ToString();
                        DRow.Cells["PartNumber"].Value = TbRepPurchaseLines.Rows[i]["PartNumber"].ToString();

                        float lTAmt = 0;
                        float lDiscountRate = float.Parse(TbRepPurchaseLines.Rows[i]["DiscountPercent"].ToString());

                        string strRQty = "0";
                        float lTaxRate = float.Parse(TbRepPurchaseLines.Rows[i]["TaxRate"].ToString());
                        float lUPrice = float.Parse(TbRepPurchaseLines.Rows[i]["UnitPrice"].ToString());


                        strRQty = TbRepPurchaseLines.Rows[i]["ReceiveQty"].ToString();
                        string strOQty = TbRepPurchaseLines.Rows[i]["OrderQty"].ToString();
                        lTotalReceived += float.Parse(strRQty);

                        if (pType == "ORDER")
                        {
                            DRow.Cells["ToDateQty"].Value = (strRQty == "0" ? null : strRQty);
                            DRow.Cells["ReceivedQty"].Value = null;
                            if (SrcOfInvoke == CommonClass.InvocationSource.CHANGETO)
                            {
                                float toReceive = float.Parse(strOQty) - float.Parse(strRQty);
                                strOQty = toReceive.ToString();
                                DRow.Cells["ReceivedQty"].Value = null;
                            }
                        }
                        else
                        {

                            DRow.Cells["ReceivedQty"].Value = (strRQty == "0" ? null : strRQty);
                        }
                        DRow.Cells["OrderedQty"].Value = strOQty;


                        if (istaxInclusive == "Y")
                        {
                            lTAmt = float.Parse(TbRepPurchaseLines.Rows[i]["TotalAmount"].ToString());
                            lUPrice = lUPrice * (1 + (lTaxRate / 100));
                        }
                        else
                        {
                            lTAmt = float.Parse(TbRepPurchaseLines.Rows[i]["SubTotal"].ToString());

                        }
                        DRow.Cells["Price"].Value = lUPrice;
                        DRow.Cells["Amount"].Value = lTAmt;
                        DRow.Cells["DiscountRate"].Value = lDiscountRate;
                        DRow.Cells["Discount"].Value = lDiscountRate.ToString() + "%";



                        DRow.Cells["Description"].Value = TbRepPurchaseLines.Rows[i]["Description"].ToString();
                        DRow.Cells["Job"].Value = TbRepPurchaseLines.Rows[i]["JobName"].ToString();
                        DRow.Cells["JobID"].Value = TbRepPurchaseLines.Rows[i]["JobID"].ToString();
                        DRow.Cells["TaxCode"].Value = TbRepPurchaseLines.Rows[i]["TaxCode"].ToString();//Compute taxEx/Taxin 
                        DRow.Cells["TaxExclusiveAmount"].Value = float.Parse(TbRepPurchaseLines.Rows[i]["SubTotal"].ToString());
                        DRow.Cells["TaxInclusiveAmount"].Value = float.Parse(TbRepPurchaseLines.Rows[i]["TotalAmount"].ToString());
                        DRow.Cells["TaxPaidAccountID"].Value = TbRepPurchaseLines.Rows[i]["TaxPaidAccountID"].ToString();
                        DRow.Cells["TaxRate"].Value = float.Parse(TbRepPurchaseLines.Rows[i]["TaxRate"].ToString());

                    }
                    for (int j = dgPurchase.Rows.Count; j < 12; j++)
                    {
                        dgPurchase.Rows.Add();
                    }
                    PopulateDataGridView();
                    // Fill Received Dates
                    this.cboReceived.Enabled = false;
                    if (lblType.Text == "ORDER")
                    {
                        FillReceiveDates(XpurchaseID);
                    }

                    if (POStatus == "New" || POStatus == "Active")
                    {
                        cboReceived.Enabled = false;
                        this.grpReceive.Visible = false;
                    }
                    else
                    {
                        this.cboReceived.Enabled = true;
                        this.grpReceive.Visible = true;
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
            IsLoading = false;
            dgEnable();


        }


        void NewPurchase()
        {
            SetGrid("ORDER");
            dgEnable();
            purchasenumber = lblPurchaseNum.Text;
            this.btnUndoApprove.Enabled = false;
            this.btnApprove.Enabled = false;
            this.btnReceive.Enabled = false;
            this.btnDeletePO.Enabled = false;
            this.cboReceived.Enabled = false;

        }

        void dgEnable()
        {
            if (txtSupplier.Text == "")
            {
                dgPurchase.Columns["ReceivedQty"].ReadOnly = true;
                dgPurchase.Columns["OrderedQty"].ReadOnly = true;
                dgPurchase.Columns["PartNumber"].ReadOnly = true;
                dgPurchase.Columns["Price"].ReadOnly = true;
                dgPurchase.Columns["Discount"].ReadOnly = true;

                dgPurchase.Columns["Description"].ReadOnly = true;
                dgPurchase.Columns["AccountNumber"].ReadOnly = true;
                dgPurchase.Columns["Amount"].ReadOnly = true;
                dgPurchase.Columns["Job"].ReadOnly = true;
                dgPurchase.Columns["TaxCode"].ReadOnly = true;
            }
            else
            {
                if (lblType.Text == "ORDER")
                {
                    dgPurchase.Columns["ReceivedQty"].ReadOnly = true;
                }
                else
                {
                    dgPurchase.Columns["ReceivedQty"].ReadOnly = false;
                }

                dgPurchase.Columns["OrderedQty"].ReadOnly = false;
                dgPurchase.Columns["PartNumber"].ReadOnly = false;
                dgPurchase.Columns["Price"].ReadOnly = false;
                dgPurchase.Columns["Discount"].ReadOnly = false;
                dgPurchase.Columns["Description"].ReadOnly = false;
                dgPurchase.Columns["Amount"].ReadOnly = false;
                dgPurchase.Columns["AccountNumber"].ReadOnly = false;
                dgPurchase.Columns["Job"].ReadOnly = false;
                dgPurchase.Columns["TaxCode"].ReadOnly = false;
                dgPurchase.ReadOnly = false;
            }
        }

        void PopulateDataGridView()
        {
            IsLoading = true;
            for (int i = 0; i < 1; i++)
            {
                dgPurchase.Rows.Add();
            }
            IsLoading = false;
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            dgEnable();
        }

        private void chk_TaxInclusive_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgPurchase.Rows.Count; i++)
            {
                if (this.dgPurchase.Rows[i].Cells["Amount"].Value != null)
                {
                    RecalclineItem(0, i);

                }
            }
            CalcOutOfBalance();
        }

        void Recalc()
        {
            DataRow rTx = CommonClass.getTaxDetails(ProfileTax);
            if (rTx.ItemArray.Length > 0)
            {
                //  string lrate = (rTx["TaxPercentageRate"].ToString() == "" ? 0 : rTx["TaxPercentageRate"].ToString()).toString();
                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                string lTaxPaidAccountID = "";
                lTaxPaidAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                lTaxRate = ltaxrate;

                if (!IsLoading)
                {
                    if (this.chk_TaxInclusive.Checked == true)
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            lAmount = float.Parse(txtTotalAmount.Value.ToString(), NumberStyles.Currency);
                            lTaxEx = lAmount / (1 + (lTaxRate / 100));
                            lTaxInc = lAmount;
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                    else
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            lTaxInc = lAmount * (1 + (lTaxRate / 100));
                            lTaxEx = lAmount;
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
                else
                {
                    if (this.chk_TaxInclusive.Checked == true)
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxInc);
                        }
                    }
                    else
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
            }
        }


        private void SetItemGrid(string pType)
        {
            dgPurchase.Columns[0].Visible = false; //InventoryAccountID
            dgPurchase.Columns[1].Visible = false; //NotUsed
            dgPurchase.Columns[2].Visible = true;//Ordered Qty
            dgPurchase.Columns[2].Width = 80;
            dgPurchase.Columns[3].Visible = false;//ToDate
            dgPurchase.Columns[3].Width = 80;
            dgPurchase.Columns[4].Visible = false;//Received Qty
            dgPurchase.Columns[4].Width = 80;
            dgPurchase.Columns[5].Visible = false;//Backordered
            dgPurchase.Columns[5].Width = 80;
            dgPurchase.Columns[6].Visible = true;//PartNumber
            dgPurchase.Columns[6].Width = 120;
            dgPurchase.Columns[7].Visible = true;//Description
            dgPurchase.Columns[7].Width = 240;
            dgPurchase.Columns[8].Visible = true; //Price
            dgPurchase.Columns[8].Width = 100;
            dgPurchase.Columns[9].Visible = true;
            dgPurchase.Columns[10].Visible = false;
            dgPurchase.Columns[11].Visible = true; // Amount
            dgPurchase.Columns[11].Width = 150;
            dgPurchase.Columns[12].Visible = true;//Job
            dgPurchase.Columns[12].Width = 70;
            dgPurchase.Columns[13].Visible = true;//Tax Code
            dgPurchase.Columns[13].Width = 70;
            dgPurchase.Columns[14].Visible = false;
            lblMemo.Text = " Journal Memo:";

            switch (pType)
            {


                case "ORDER":
                    this.BackColor = System.Drawing.Color.DarkSeaGreen;
                    dgPurchase.Columns[2].Visible = true;//Ordered Qty
                    dgPurchase.Columns[2].Width = 60;
                    dgPurchase.Columns[3].Visible = true;//ToDate
                    dgPurchase.Columns[3].Width = 60;
                    dgPurchase.Columns[3].ReadOnly = true;//ToDate
                    dgPurchase.Columns[4].Visible = false;//Received Qty
                    dgPurchase.Columns[4].Width = 60;
                    dgPurchase.Columns[4].ReadOnly = true;//Received Qty
                    dgPurchase.Columns[5].Visible = false;//Backordered
                    dgPurchase.Columns[6].Width = 110;//PartNumber                  
                    dgPurchase.Columns[7].Width = 270;//Description
                    dgPurchase.Columns[9].Width = 60;//Discount
                    break;



                case "RECEIVE ITEMS":
                    //ONLY THE Receive Qty Column is Editable others should be readonly
                    this.BackColor = System.Drawing.Color.Orange;
                    dgPurchase.Columns[2].Visible = true;//Ordered Qty
                    dgPurchase.Columns[2].Width = 60;
                    dgPurchase.Columns[2].ReadOnly = true;
                    dgPurchase.Columns[3].Visible = false;//ToDate
                    dgPurchase.Columns[3].Width = 60;
                    dgPurchase.Columns[3].ReadOnly = true;
                    dgPurchase.Columns[4].Visible = true;//Received Qty
                    dgPurchase.Columns[4].Width = 60;
                    dgPurchase.Columns[4].ReadOnly = false;
                    dgPurchase.Columns[5].Visible = false;//Backordered
                    dgPurchase.Columns[5].Width = 60;
                    dgPurchase.Columns[6].Width = 95;//PartNumber
                    dgPurchase.Columns[6].ReadOnly = true;
                    dgPurchase.Columns[7].Width = 225;//Description
                    dgPurchase.Columns[7].ReadOnly = true;
                    dgPurchase.Columns[9].Width = 60;//Discount
                    dgPurchase.Columns[9].ReadOnly = true;
                    break;
            }
        }



        private void txtPaidToday_ValueChanged(object sender, EventArgs e)
        {
            CalcOutOfBalance();
        }

        private void ClearRowItems(int pRowIndex)
        {
            //dgPurchase.Rows[pRowIndex].Cells["Description"].Value = null;
            dgPurchase.Rows[pRowIndex].Cells["Price"].Value = null;
            dgPurchase.Rows[pRowIndex].Cells["Discount"].Value = null;
            dgPurchase.Rows[pRowIndex].Cells["Amount"].Value = null;
            dgPurchase.Rows[pRowIndex].Cells["Job"].Value = null;
            dgPurchase.Rows[pRowIndex].Cells["TaxCode"].Value = null;
        }

        private void dgPurchase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 11)
            {
                if (e.ColumnIndex == 9)
                {
                    if (dgPurchase.Rows[e.RowIndex].Cells["Discount"].Value != null)
                    {
                        string lDiscRate = dgPurchase.Rows[e.RowIndex].Cells["Discount"].Value.ToString().Replace("%", "");
                        float lDRate = float.Parse(lDiscRate);
                        dgPurchase.Rows[e.RowIndex].Cells["DiscountRate"].Value = lDRate;
                    }
                }
                RecalclineItem(e.ColumnIndex, e.RowIndex);
                CalcOutOfBalance();
                //this.dgPurchase.CurrentCell = this.dgPurchase[e.ColumnIndex + 1, e.RowIndex];
            }

            if (e.RowIndex == (this.dgPurchase.Rows.Count - 1))
            {
                this.dgPurchase.Rows.Add();
            }

            if (e.ColumnIndex == 6) //PArtNumber
            {
                ClearRowItems(e.RowIndex);
                if (dgPurchase.CurrentCell.Value != null)
                {
                    ShowItemLookup(dgPurchase.CurrentCell.Value.ToString(), "", 0, 0);
                    RecalclineItem(e.ColumnIndex, e.RowIndex);
                }
                else
                {
                    ShowItemLookup("", "", 0, 0);
                    RecalclineItem(e.ColumnIndex, e.RowIndex);
                }
                btnSaveRecurring.Enabled = true;
                CalcOutOfBalance();
            }


            if (e.ColumnIndex == 12)//Job
            {
                if (dgPurchase.CurrentCell.Value != null)
                {
                    ShowJobLookup(dgPurchase.CurrentCell.Value.ToString());
                }
                else
                {
                    ShowJobLookup("");
                }
            }
            if (e.ColumnIndex == 13)//Tax
            {
                if (dgPurchase.CurrentCell.Value != null)
                {
                    ShowTaxLookup(dgPurchase.CurrentCell.Value.ToString());

                }
                else
                {
                    ShowTaxLookup("");
                }
                RecalclineItem(e.ColumnIndex, e.RowIndex);
                CalcOutOfBalance();
            }
        }

        public bool CheckExistItemInGridReceiveItem(string pNum)
        {
            int shipCount = 0;
            if (dgPurchase.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgPurchase.Rows)
                {
                    if (dgvr.Cells["PartNumber"].Value != null)
                    {
                        if (dgvr.Cells["PartNumber"].Value.ToString() == pNum)
                        {
                            shipCount += 1;
                            if (dgvr.Cells["ReceivedQty"].Value != null)
                            {
                                dgvr.Cells["ReceivedQty"].Value = int.Parse(dgvr.Cells["ReceivedQty"].Value.ToString()) + 1;

                            }
                            else
                            {
                                dgvr.Cells["ReceivedQty"].Value = 1;
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool CheckExistItemInGrid(string pNum, int pIndex)
        {
            int shipCount = 0;
            if (dgPurchase.Rows.Count > 0)
            {
                for(int i = 0; i < dgPurchase.Rows.Count; i++ )
                {
                        DataGridViewRow dgvr = dgPurchase.Rows[i];
                    if (dgvr.Cells["PartNumber"].Value != null)
                    {
                        if (dgvr.Cells["PartNumber"].Value.ToString() == pNum && pIndex != i)
                        {
                            shipCount += 1;
                            if (dgvr.Cells["OrderedQty"].Value != null)
                            {
                                dgvr.Cells["OrderedQty"].Value = int.Parse(dgvr.Cells["OrderedQty"].Value.ToString()) + 1;

                            }
                            else
                            {
                                dgvr.Cells["OrderedQty"].Value = 1;
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void ShowItemLookup(string itemNum, string pLastIndex, float pQty, float pPrice)
        {
            int pIndex = 0;
            ItemLookup ItemDlg = new ItemLookup(ItemLookupSource.ENTERPURCHASE, itemNum);
            DataGridViewRow dr;
            if (pLastIndex != "")
            {
                dgPurchase.Rows.Add();
                pIndex = int.Parse(pLastIndex);
                DataGridViewRow QtyCell = dgPurchase.Rows[pIndex];

                if (lblType.Text == "ORDER")
                {
                    if (pQty != 0)
                    {
                        //pQty -= 1;
                        QtyCell.Cells["OrderedQty"].Value = pQty;
                    }
                    else
                    {
                        QtyCell.Cells["OrderedQty"].Value = 1;
                    }
                }
                else
                {
                    if (pQty == 0)
                    {
                        QtyCell.Cells["ReceivedQty"].Value = QtyCell.Cells["OrderedQty"].Value;
                        QtyCell.Cells["OrderedQty"].Value = "";
                    }
                    else
                    {
                        QtyCell.Cells["ReceivedQty"].Value = pQty;

                    }
                }
                //pIndex = dgPurchase.Rows.Count - 1;
                //dgPurchase.Rows[pIndex].Selected = true;

            }
            else
            {
                dgPurchase.Rows.Add();
                pIndex = dgPurchase.CurrentRow.Index;
            }
            DataGridViewRow dgvRows = dgPurchase.Rows[pIndex];

            if (ItemDlg.ShowDialog() == DialogResult.OK)
            {
                dr = ItemDlg.GetSelectedItem;
                if (lblType.Text == "ORDER")
                {
                    if (!CheckExistItemInGrid(dr.Cells["PartNo"].Value.ToString(), pIndex))
                    {
                        dgvRows.Cells["PartNumber"].Value = dr.Cells["PartNo"].Value.ToString();
                        //Console.WriteLine(dr.Cells["PartNumber"].Value.ToString());
                        dgvRows.Cells["Description"].Value = dr.Cells["ItemName"].Value.ToString();
                        dgvRows.Cells["TaxCode"].Value = dr.Cells["PurchaseTaxCode"].Value.ToString();
                        float lastcost = float.Parse(dr.Cells["StandardCost"].Value.ToString() == "" ? "0" : dr.Cells["StandardCost"].Value.ToString()); ;

                        dgvRows.Cells["ItemID"].Value = dr.Cells["ItemID"].Value.ToString();
                        bool isCounted = (bool)dr.Cells["IsCounted"].Value;
                        if (isCounted)
                        {
                            dgvRows.Cells["InventoryAccountID"].Value = dr.Cells["AssetAccountID"].Value.ToString();
                        }
                        else
                        {
                            dgvRows.Cells["InventoryAccountID"].Value = dr.Cells["COSAccountID"].Value.ToString();
                        }

                        float ltaxrate = 0;
                        DataRow rTx = CommonClass.getTaxDetails(dr.Cells["PurchaseTaxCode"].Value.ToString());
                        if (rTx.ItemArray.Length > 0)
                        {

                            ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                            string lTaxPaidAccountID = "";
                            lTaxPaidAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                            dgvRows.Cells["TaxPaidAccountID"].Value = lTaxPaidAccountID;
                            dgvRows.Cells["TaxRate"].Value = ltaxrate;
                        }
                        float lastcostInc = lastcost * (1 + (ltaxrate / 100));

                        if (pPrice != 0)
                        {
                            dgvRows.Cells["Price"].Value = pPrice;
                        }
                        else
                        {
                            if (chk_TaxInclusive.Checked)
                            {
                                dgvRows.Cells["Price"].Value = lastcostInc;
                            }
                            else
                            {
                                dgvRows.Cells["Price"].Value = lastcost;
                            }
                        }
                        // this.dgvRows.CurrentCell = this.dgPurchase[dgPurchase.CurrentCell.ColumnIndex + 1, dgPurchase.CurrentCell.RowIndex];
                        this.dgPurchase.Refresh();

                        btnSaveRecurring.Enabled = true;
                    }
                }
                else
                {
                    if (!CheckExistItemInGridReceiveItem(dr.Cells["PartNo"].Value.ToString()))
                    {
                        dgvRows.Cells["PartNumber"].Value = dr.Cells["PartNo"].Value.ToString();
                        //Console.WriteLine(dr.Cells["PartNumber"].Value.ToString());
                        dgvRows.Cells["Description"].Value = dr.Cells["ItemName"].Value.ToString();
                        dgvRows.Cells["TaxCode"].Value = dr.Cells["PurchaseTaxCode"].Value.ToString();
                        float lastcost = float.Parse(dr.Cells["StandardCost"].Value.ToString() == "" ? "0" : dr.Cells["StandardCost"].Value.ToString()); ;

                        dgvRows.Cells["ItemID"].Value = dr.Cells["ItemID"].Value.ToString();
                        bool isCounted = (bool)dr.Cells["IsCounted"].Value;
                        if (isCounted)
                        {
                            dgvRows.Cells["InventoryAccountID"].Value = dr.Cells["AssetAccountID"].Value.ToString();
                        }
                        else
                        {
                            dgvRows.Cells["InventoryAccountID"].Value = dr.Cells["COSAccountID"].Value.ToString();
                        }

                        float ltaxrate = 0;
                        DataRow rTx = CommonClass.getTaxDetails(dr.Cells["PurchaseTaxCode"].Value.ToString());
                        if (rTx.ItemArray.Length > 0)
                        {

                            ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                            string lTaxPaidAccountID = "";
                            lTaxPaidAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                            dgvRows.Cells["TaxPaidAccountID"].Value = lTaxPaidAccountID;
                            dgvRows.Cells["TaxRate"].Value = ltaxrate;
                        }
                        float lastcostInc = lastcost * (1 + (ltaxrate / 100));

                        if (pPrice != 0)
                        {
                            dgvRows.Cells["Price"].Value = pPrice;
                        }
                        else
                        {
                            if (chk_TaxInclusive.Checked)
                            {
                                dgvRows.Cells["Price"].Value = lastcostInc;
                            }
                            else
                            {
                                dgvRows.Cells["Price"].Value = lastcost;
                            }
                        }
                        // this.dgvRows.CurrentCell = this.dgPurchase[dgPurchase.CurrentCell.ColumnIndex + 1, dgPurchase.CurrentCell.RowIndex];
                        this.dgPurchase.Refresh();

                        btnSaveRecurring.Enabled = true;
                    }
                }


            }
        }

        private void RecalclineItem(int pColIndex, int pRowIndex)
        {
            if (pRowIndex < 0)
                return;

            if (!IsLoading)
            {
                DataGridViewRow dgvRows = dgPurchase.Rows[pRowIndex];

                float lTaxEx = 0;
                float lTaxInc = 0;
                float lTaxRate = 0;
                float lAmount = 0;
                float lDiscount = 0;
                float lDiscountRate = 0;
                float lPrice = 0;
                float lQty = 0;
                float lOrderedQty = 0;
                float lReceivedQty = 0;
                float lToDateQty = 0;
                float lBackOrderQty = 0;

                if (dgvRows.Cells["PartNumber"].Value != null)
                {
                    lOrderedQty = (dgvRows.Cells["OrderedQty"].Value != null ? (dgvRows.Cells["OrderedQty"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["OrderedQty"].Value.ToString())) : 0);
                    lReceivedQty = (dgvRows.Cells["ReceivedQty"].Value != null ? (dgvRows.Cells["ReceivedQty"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["ReceivedQty"].Value.ToString())) : 0);
                    lToDateQty = (dgvRows.Cells["ToDateQty"].Value != null ? (dgvRows.Cells["ToDateQty"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["ToDateQty"].Value.ToString())) : 0);
                    lBackOrderQty = (dgvRows.Cells["Backorder"].Value != null ? (dgvRows.Cells["Backorder"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["Backorder"].Value.ToString())) : 0);

                    switch (lblType.Text)
                    {

                        case "ORDER":
                            lQty = lOrderedQty;
                            break;
                        case "RECEIVE ITEMS":
                            lQty = lReceivedQty;
                            break;
                    }
                    lTaxRate = (dgvRows.Cells["TaxRate"].Value != null ? float.Parse(dgvRows.Cells["TaxRate"].Value.ToString()) : 0);
                    lDiscountRate = (dgvRows.Cells["DiscountRate"].Value != null ? (dgvRows.Cells["DiscountRate"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["DiscountRate"].Value.ToString())) : 0);
                    lPrice = (dgvRows.Cells["Price"].Value != null ? float.Parse(dgvRows.Cells["Price"].Value.ToString(), NumberStyles.Currency) : 0);

                    if (lDiscountRate != 0)
                    {
                        lDiscount = (lQty * lPrice) * (lDiscountRate / 100);
                    }

                    lAmount = (lQty * lPrice) - lDiscount;
                    if (this.chk_TaxInclusive.Checked == true)
                    {
                        lTaxEx = lAmount / (1 + (lTaxRate / 100));
                        lTaxInc = lAmount;
                    }
                    else
                    {
                        lTaxInc = lAmount * (1 + (lTaxRate / 100));
                        lTaxEx = lAmount;
                    }
                    dgvRows.Cells["TaxExclusiveAmount"].Value = lTaxEx;
                    dgvRows.Cells["TaxInclusiveAmount"].Value = lTaxInc;
                    dgvRows.Cells["Amount"].Value = lAmount;

                }


            }
        }



        void RecalcItem()
        {
            DataRow rTx = CommonClass.getTaxDetails(ProfileTax);
            if (rTx.ItemArray.Length > 0)
            {
                //  string lrate = (rTx["TaxPercentageRate"].ToString() == "" ? 0 : rTx["TaxPercentageRate"].ToString()).toString();
                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                string lTaxPaidAccountID = "";
                lTaxPaidAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                lTaxRate = ltaxrate;

                if (!IsLoading)
                {
                    if (this.chk_TaxInclusive.Checked == true)
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            lAmount = float.Parse(txtTotalAmount.Value.ToString(), NumberStyles.Currency);
                            lTaxEx = lAmount / (1 + (lTaxRate / 100));
                            lTaxInc = lAmount;
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                    else
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            lTaxInc = lAmount * (1 + (lTaxRate / 100));
                            lTaxEx = lAmount;
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
                else
                {
                    if (this.chk_TaxInclusive.Checked == true)
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxInc);
                        }
                    }
                    else
                    {
                        if (txtTotalAmount.Value.ToString() != null)
                        {
                            txtTotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
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
                FreightTaxAccountID = (rTx["TaxPaidAccountID"] == null ? "" : rTx["TaxPaidAccountID"].ToString());
                this.txtFTaxCode.Text = pTaxCode;
                CalcFreight();
            }

        }

        private void txtFreight_ValueChanged(object sender, EventArgs e)
        {
            if (AP_FreightAccountID == "0")
            {
                MessageBox.Show("Account for Freight Expenses in Purchases is not setup.");
                this.txtFreight.Value = 0;
                return;
            }
            else
            {
                CalcFreight();
            }

        }

        private void pbFTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                this.txtFTaxCode.Text = Tax[0];
                FreightTaxRate = float.Parse(Tax[2]);
                FreightTaxAccountID = Tax[4];
                CalcFreight();
            }
        }

        private void CalcFreight()
        {
            if (this.chk_TaxInclusive.Checked)
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

        private void cmb_shippingcontact_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadContacts(Convert.ToInt16(SupplierID), this.cmb_shippingcontact.SelectedIndex + 1);
        }



        private void LoadItemLayoutReport(string pPurchaseID)
        {
            Reports.ReportParams purchaselayoutparams = new Reports.ReportParams();
            purchaselayoutparams.PrtOpt = 1;
            string printPurchase = "";

            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();

            if (this.lblType.Text == "RECEIVE ITEMS")
            {
                printPurchase = @"SELECT pl.Name, 'RECEIVE ITEM' as PurchaseType, p.ReceiveItemNumber, p.EntryDate, p.SubTotal, p.GrandTotal,p.FreightSubTotal,
                                    p.FreightTax, p.TaxTotal,p.TotalPaid, p.Comments, pl.CurrentBalance, tc.TaxPercentageRate, p.TransactionDate ,
                                    s.ShippingMethod, l.Description, l.TaxCode,
                                    c.Street, c.City, c.State, c.Country, c.Postcode, tc.TaxPercentageRate ,  p.IsTaxInclusive, p.SupplierINVNumber, p.PromiseDate,tp.Description as Term ,
                                    tc.TaxPercentageRate,
                                    l.OrderQty,l.ReceiveQty, l.UnitPrice, l.DiscountPercent, i.PartNumber, l.SubTotal as lSubTotal
                                    , p.TotalDue  
                                    FROM ReceiveItems p
                                    LEFT JOIN Profile pl ON p.SupplierID = pl.ID
                                    LEFT JOIN Contacts c ON c.ContactID = pl.ID
                                    LEFT JOIN ShippingMethods s ON s.ShippingID = p.ShippingMethodID
                                    LEFT JOIN PurchaseLines l ON p.PurchaseID = l.PurchaseID
                                    LEFT JOIN TaxCodes tc ON tc.TaxCode = l.TaxCode
                                    LEFT JOIN Items i ON i.ID = l.EntityID
                                    LEFT JOIN Terms t ON t.TermsID = p.TermsReferenceID
                                    LEFT JOIN TermsOfPayment tp ON tp.TermsOfPaymentID = t.TermsOfPaymentID
                                    WHERE p.ReceiveItemID =  @PurchaseID";
                purchaselayoutparams.ReportName = "PurchaseItemReceive.rpt";
                purchaselayoutparams.RptTitle = "Receive Items";

            }
            else
            {
                printPurchase = @"SELECT pl.Name, p.PurchaseType, p.PurchaseNumber, p.EntryDate,
                                CONVERT(datetime, 
                                    SWITCHOFFSET(CONVERT(datetimeoffset, 
                                    p.TransactionDate), 
                                    DATENAME(TzOffset, SYSDATETIMEOFFSET()))) 
                                As TransactionDate,p.SubTotal, p.GrandTotal,p.FreightSubTotal,
                                p.FreightTax, p.TaxTotal,p.TotalPaid, p.Comments, pl.CurrentBalance, tc.TaxPercentageRate,p.TransactionDate, 
                                s.ShippingMethod, l.Description, l.TotalAmount, l.TaxCode,
                                c.Street, c.City, c.State, c.Country, c.Postcode, tc.TaxPercentageRate ,  p.IsTaxInclusive, p.SupplierINVNumber, p.PromiseDate,tp.Description as Term ,
                                tc.TaxPercentageRate,
                                l.OrderQty, l.ReceiveQty, l.UnitPrice, l.DiscountPercent,l.SubTotal as lSubTotal, i.PartNumber  , p.TotalDue                              , p.TotalDue  
                                FROM Purchases p
                                INNER JOIN Profile pl ON p.SupplierID = pl.ID
                                LEFT JOIN Contacts c ON c.ProfileID = pl.ID and c.Location = p.BillingContactID
                                LEFT JOIN ShippingMethods s ON s.ShippingID = p.ShippingMethodID
                                INNER JOIN PurchaseLines l ON p.PurchaseID = l.PurchaseID
                                LEFT JOIN TaxCodes tc ON tc.TaxCode = l.TaxCode
                                INNER JOIN Items i ON i.ID = l.EntityID
                                LEFT JOIN Terms t ON t.TermsID = p.TermsReferenceID
                                LEFT JOIN TermsOfPayment tp ON tp.TermsOfPaymentID = t.TermsOfPaymentID 
                                WHERE p.PurchaseID = @PurchaseID";

                purchaselayoutparams.ReportName = "PurchaseItemLayout.rpt";
                purchaselayoutparams.RptTitle = "Purchase Item Layout";
            }

            param.Add("@PurchaseID", pPurchaseID);
            CommonClass.runSql(ref dt, printPurchase, param);
            purchaselayoutparams.Rec.Add(dt);
            purchaselayoutparams.Params = "compname|CompAddress|TIN";
            purchaselayoutparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim();

            CommonClass.ShowReport(purchaselayoutparams);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadItemLayoutReport(this.lblID.Text);
        }

        private bool CheckIfReceived()
        {
            float lTotalReceived = 0;
            float lQty = 0;
            for (int x = 0; x < this.dgPurchase.Rows.Count; x++)
            {
                if (this.dgPurchase.Rows[x].Cells["ReceivedQty"].Value != null)
                {
                    lQty = (this.dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString() == "" ? 0 : float.Parse(this.dgPurchase.Rows[x].Cells["ReceivedQty"].Value.ToString()));
                    lTotalReceived += lQty;
                }
            }
            if (lTotalReceived != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void ReverseItemsQty(string pPurchaseID)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                string lTranDate = dtDatePurchase.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                DataTable lTbLines = GetPurchaseLine(pPurchaseID);
                for (int i = 0; i < lTbLines.Rows.Count; i++)
                {
                    //UPDATE ITEMS QTY & COST
                    float lQty = lTbLines.Rows[i]["ReceiveQty"].ToString() == "" ? 0 : float.Parse(lTbLines.Rows[i]["ReceiveQty"].ToString());
                    float lUPrice = float.Parse(lTbLines.Rows[i]["UnitPrice"].ToString());
                    float lSubTotal = float.Parse(lTbLines.Rows[i]["SubTotal"].ToString());
                    float lUnitCost = lSubTotal / lQty;
                    string lItemID = (lTbLines.Rows[i]["EntityID"] != null ? lTbLines.Rows[i]["EntityID"].ToString() : "0");
                    string lTranID = lTbLines.Rows[i]["PurchaseID"].ToString();
                    float lTranQty = 0;
                    if (lQty != 0)
                    {

                        DataTable lItemTb = TransactionClass.GetItem(lItemID);
                        if (lItemTb.Rows.Count > 0)
                        {

                            bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                            if (lIsCounted)
                            {
                                float QtyPerBUOM = float.Parse(lItemTb.Rows[0]["QtyPerBuyingUOM"].ToString());
                                lTranQty = lQty * QtyPerBUOM;

                                float lOldQty = lItemTb.Rows[0]["OnHandQty"].ToString() == "" ? 0 : float.Parse(lItemTb.Rows[0]["OnHandQty"].ToString());
                                float lNewQty = lOldQty - lTranQty;
                                //UPDATE onHAnd QTY
                                sql = "UPDATE ItemsQty set OnHandQty = " + lNewQty.ToString() + " where ItemID = " + lItemID;
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();


                                float lOldCost = float.Parse(lItemTb.Rows[0]["AverageCostEx"].ToString());
                                float lNewAveCost = ((lOldQty - lTranQty) == 0 ? 0 : ((lOldQty * lOldCost) - lSubTotal) / (lOldQty - lTranQty));
                                if (lNewAveCost > 0)
                                {
                                    //UPDATE AVECOST
                                    sql = "UPDATE ItemsCostPrice set PrevAverageCostEx = " + lOldCost + ", AverageCostEx = " + lNewAveCost + " where ItemID = " + lItemID;
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }

                        }
                        string lPtype = "";
                        if (lblType.Text == "BILL")
                        {
                            lPtype = "PB";
                        }
                        else
                        {
                            lPtype = "RI";
                        }
                        sql = @"DELETE FROM ItemTransaction where TranType = '" + lPtype + "' and SourceTranID = " + pPurchaseID;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dgPurchase.SelectedCells)
            {
                dgPurchase.Rows.RemoveAt(dgPurchase.CurrentRow.Index);
            }
            CalcOutOfBalance();
            dgPurchase.Refresh();
        }

        private void dgPurchase_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }






        private void dgPurchase_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                if (e.ColumnIndex.ToString() != "")
                {
                    enter = true;
                }
            }
        }

        private void btnSaveRecurring_Click(object sender, EventArgs e)
        {
            switch (lblType.Text)
            {
                case "QUOTE":
                    lTranType = "PQ";
                    break;
                case "ORDER":
                    lTranType = "PO";
                    break;
                case "BILL":
                    lTranType = "PB";
                    break;
                case "RECEIVE ITEMS":
                    lTranType = "RI";
                    break;
                default:
                    lTranType = "";
                    break;
            }
            RecurringSetup lRecurringForm = new RecurringSetup(this, txtSupplier.Text, lTranType);
            lRecurringForm.MdiParent = this.MdiParent;
            lRecurringForm.Show();
        }

        private void btnUseRecurring_Click(object sender, EventArgs e)
        {
            UseRecurring lUseRecurringForm = new UseRecurring(CommonClass.InvocationSource.PURCHASE);
            lUseRecurringForm.MdiParent = this.MdiParent;
            lUseRecurringForm.Show();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            TransactionsLookup TLookup = new TransactionsLookup("Bill", XpurchaseID);
            TLookup.ShowDialog();
        }
        private void UpdateNotify()
        {
            switch (lblType.Text)
            {
                case "QUOTE":
                    lTranType = "PQ";
                    break;
                case "ORDER":
                    lTranType = "PO";
                    break;
                case "BILL":
                    lTranType = "PB";
                    break;
                case "RECEIVE ITEMS":
                    lTranType = "RI";
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

                string selectSql_ua = "SELECT * FROM Recurring WHERE EntityID = " + XpurchaseID + " AND TranType = '" + lTranType + "'";
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


                        string sql = @"UPDATE Recurring set NotifyDate = @newNotifyDate ,LastPosted = @newLastPosted WHERE EntityID = " + XpurchaseID + " AND TranType = '" + lTranType + "'";

                        con = new SqlConnection(CommonClass.ConStr);
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@newNotifyDate", sdate);
                        cmd.Parameters.AddWithValue("@newLastPosted", DateTime.Today); ;
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

        public static DataTable GetReceiveItemLines(int pReceiveItemID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT l.*, p.ReceiveItemNumber, p.GrandTotal, p.Memo, p.FreightSubTotal, p.FreightTax, p.FreightTaxCode, p.FreightTaxRate, i.AssetAccountID, 
                    i.IsCounted, i.IncomeAccountID, i.IsSold, i.COSAccountID, i.IsBought FROM ( ReceiveItemsLines l INNER JOIN ReceiveItems p ON l.ReceiveItemID = p.ReceiveItemID ) left join Items as i on l.EntityID =i.ID WHERE l.ReceiveItemID = " + pReceiveItemID;

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
        }
        public static DataTable GetItem(string pItemID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT i.*, q.OnHandQty,q.CommitedQty, c.AverageCostEx, p.Name, c.StandardCostEx FROM ( Items i INNER JOIN ItemsQty q ON i.ID = q.ItemID )
                                INNER JOIN ItemsCostPrice c ON i.ID = c.ItemID 
                                LEFT JOIN Profile p ON p.ID = i.SupplierID 
                                WHERE i.ID = " + pItemID;
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


        private void FillReceiveDates(string pPurchaseID)
        {
            IsLoading = true;
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = "SELECT 0 as ReceiveItemID, TransactionDate, '--Main Order--' as SupplierINVNumber, '' as RDetail from Purchases where PurchaseID = " + pPurchaseID + " UNION SELECT ReceiveItemID, TransactionDate, SupplierINVNumber, '' as RDetail  From ReceiveItems where PurchaseID = " + pPurchaseID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                for (int i = 0; i < RTb.Rows.Count; i++)
                {
                    DateTime ltrandate = (DateTime)RTb.Rows[i]["TransactionDate"];
                    RTb.Rows[i]["TransactionDate"] = ltrandate.ToLocalTime();
                    RTb.Rows[i]["RDetail"] = ltrandate.ToLocalTime().ToShortDateString() + "-" + RTb.Rows[i]["SupplierINVNumber"];

                }
                cboReceived.DataSource = RTb;
                cboReceived.ValueMember = "ReceiveItemID";
                cboReceived.DisplayMember = "RDetail";
                cboReceived.Enabled = true;

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
            IsLoading = false;
        }
        private void cboReceived_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            int lID = Convert.ToInt16(this.cboReceived.SelectedValue.ToString());
            string ltype = "ORDER";
            int SelVal = 0;
            if (lID > 0)
            {
                ltype = "RECEIVE ITEMS";
                SelVal = lID;
            }
            else
            {
                lID = Convert.ToInt16(XpurchaseID);
            }
            LoadTransaction(lID.ToString(), ltype);
            IsLoading = true;
            cboReceived.SelectedValue = SelVal;
            this.lblType.Text = ltype;
            IsLoading = false;

        }

        private bool ApprovePO(string pPurchaseID)
        {
            SqlConnection con = null;
            bool retval = false;
            try
            {
                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                string lSendDate = DateTime.UtcNow.ToString("yyyy -MM-dd HH:mm:ss");
                sql = @"Update Purchases set POStatus = 'Active', ApprovedBy = '" + CommonClass.UserID + "', ApprovedDate = '" + lSendDate + "' where PurchaseID = " + pPurchaseID;
                cmd.CommandText = sql;
                int affectedrecords = cmd.ExecuteNonQuery();
                if (affectedrecords == 1)
                {
                    retval = true;

                }
                else
                {
                    retval = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                retval = false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return retval;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (ApprovePO(this.lblID.Text))
            {

                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Purchase Order  " + this.lblPurchaseNum.Text + " Approved", this.lblID.Text);
                DialogResult PrintInvoice = MessageBox.Show("Would you like to print Purchase Order?", "Information", MessageBoxButtons.YesNo);
                if (PrintInvoice == DialogResult.Yes)
                {
                    LoadItemLayoutReport(this.lblID.Text);
                }
                this.btnApprove.Enabled = false;

                lblPOStatus.Text = "Active";
                //this.btnRecord.Enabled = false; 
                //this.btnRemove.Enabled = false;
                ReloadPurchase();

            }
            else
            {
                MessageBox.Show("Error Occured while sending PO.");
            }


        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            SrcOfInvoke = CommonClass.InvocationSource.CHANGETO;
            ReloadPurchase();
            dtDatePurchase.Value.ToUniversalTime();
            btnImportReceiveItem.Enabled = true;
            btnImportOrderItem.Enabled = false;
        }

        private void btnUndoApprove_Click(object sender, EventArgs e)
        {
            if (lblPOStatus.Text == "Active")
            {
                if (MessageBox.Show("Do you really want to undo Approval of the PO?", "Undo PO Approval", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = "Update Purchases set POStatus = 'New' where PurchaseID = " + this.lblID.Text;
                    int cnt = CommonClass.runSql(sql);
                    if (cnt > 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Purchase Order  " + this.lblPurchaseNum.Text + " Undo Approval", this.lblID.Text);
                        this.lblPOStatus.Text = "New";
                        ReloadPurchase();

                    }
                }
            }
        }

        private void btnDeletePO_Click(object sender, EventArgs e)
        {
            if (lblPOStatus.Text == "New")
            {
                if (MessageBox.Show("Do you really want to delete PO?", "Delete PO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = "Delete Purchases where PurchaseID = " + this.lblID.Text;
                    int cnt = CommonClass.runSql(sql);
                    if (cnt > 0)
                    {
                        sql = "Delete PurchaseLines where PurchaseID = " + this.lblID.Text;
                        int cnt2 = CommonClass.runSql(sql);
                        if (cnt2 > 0)
                        {
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Purchase Order  " + this.lblPurchaseNum.Text + " Deleted", this.lblID.Text);
                            MessageBox.Show("Purchase Order Deleted.");
                            this.Close();
                        }

                    }
                }
            }
        }
        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { ",", "\t" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            return csvData;
        }
        private void btnImportOrderItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            string SafeFileName = "";
            string FileName = "";
            int ExistingItems = 0;
            string[] pNum = new string[0];
            string[] pQty = new string[0];
            string partnum = "";
            float qty;
            float price;
            //int lastFilledRow = 0;
            int lLastIndex = 0;
            if (dlg.FileName != "")
            {
                if (dlg.FileName.EndsWith(".csv"))
                {
                    OrderItemDataTable = new DataTable();

                    OrderItemDataTable = GetDataTabletFromCSVFile(dlg.FileName);
                    SafeFileName = dlg.SafeFileName;
                    FileName = dlg.FileName;

                    if (OrderItemDataTable.Rows != null)
                    {
                        for (int x = 0; OrderItemDataTable.Rows.Count > x; x++)
                        {
                            price = 0;
                            qty = 0;

                            DataRow dr = OrderItemDataTable.Rows[x];
                            DataColumnCollection columns = OrderItemDataTable.Columns;

                            partnum = dr["PartNumber"].ToString();
                            qty = float.Parse(dr["Quantity"].ToString());
                            if (columns.Contains("Price"))
                            {
                                if (dr["Price"].ToString() != "")
                                {
                                    price = float.Parse(dr["Price"].ToString());
                                }
                            }
                            
                            int lSameIndex = GetExistingItemIndex(partnum);
                           
                            if (lSameIndex != -1)
                            {
                                //UPDATE EXISTING
                               
                                float lNewQty = (dgPurchase.Rows[lSameIndex].Cells["OrderedQty"].Value != null ? (dgPurchase.Rows[lSameIndex].Cells["OrderedQty"].Value.ToString() == "" ? 0 : float.Parse(dgPurchase.Rows[lSameIndex].Cells["OrderedQty"].Value.ToString())) : 0);
                                dgPurchase.Rows[lSameIndex].Cells["OrderedQty"].Value = lNewQty + qty;
                                if (columns.Contains("Price"))
                                {
                                    if (dr["Price"].ToString() != "")
                                    {
                                        price = float.Parse(dr["Price"].ToString());
                                        dgPurchase.Rows[lSameIndex].Cells["Price"].Value = price;
                                    }

                                }
                                RecalclineItem(2, lSameIndex);
                                CalcOutOfBalance();
                            }
                            else
                            {
                                lLastIndex = GetLastIndex();
                                ShowItemLookup(partnum, lLastIndex.ToString(), qty, price);
                                RecalclineItem(2, lLastIndex);
                                CalcOutOfBalance();
                            }
                           
                        }
                        dgPurchase.RefreshEdit();
                        if (lLastIndex < dgPurchase.Rows.Count)
                        {
                            dgPurchase.FirstDisplayedScrollingRowIndex = lLastIndex;
                        }
                    }
                }
            }
        }

        private int GetLastIndex()
        {
            int lLastIndex = dgPurchase.Rows.Count - 1;
            int lNextIndex = 0;
            for(int i = lLastIndex; i > -1; i--)
            {
                if (dgPurchase.Rows[i].Cells["PartNumber"].Value == null)
                {
                    if (i != 0)
                    {
                        if (dgPurchase.Rows[i - 1].Cells["PartNumber"].Value != null)
                        {
                            lNextIndex = i;
                            break;
                        }
                    }
                }

            }
            return lNextIndex;
        }

        private int GetExistingItemIndex(string pPartNumber)
        {
           
            int lSameIndex =-1;
            for (int i = 0; i < dgPurchase.Rows.Count; i++)
            {
                if (dgPurchase.Rows[i].Cells["PartNumber"].Value != null)
                {
                    if (dgPurchase.Rows[i].Cells["PartNumber"].Value.ToString() == pPartNumber)
                    {
                        lSameIndex = i;
                        break;
                    }
                }

            }
            return lSameIndex;
        }

        private void btnImportReceiveItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            string SafeFileName = "";
            string FileName = "";
            int ExistingItems = 0;
            string[] pNum = new string[0];
            string[] pQty = new string[0];
            string partnum = "";
            float qty;
            float price;
            //int lastFilledRow = 0;
            int lLastIndex = 0;
            if (dlg.FileName != "")
            {
                if (dlg.FileName.EndsWith(".csv"))
                {
                    ReceiveItemDataTable = new DataTable();

                    ReceiveItemDataTable = GetDataTabletFromCSVFile(dlg.FileName);
                    SafeFileName = dlg.SafeFileName;
                    FileName = dlg.FileName;

                    if (ReceiveItemDataTable.Rows != null)
                    {
                        for (int x = 0; ReceiveItemDataTable.Rows.Count > x; x++)
                        {
                            qty = 0;
                            price = 0;
                          
                            DataRow dr = ReceiveItemDataTable.Rows[x];
                            DataColumnCollection columns = ReceiveItemDataTable.Columns;

                            partnum = dr["PartNumber"].ToString();
                            if (dr["Quantity"].ToString() != "")
                            {
                                qty = float.Parse(dr["Quantity"].ToString());
                            }
                            if (columns.Contains("Price"))
                            {
                                if (dr["Price"].ToString() != "")
                                {
                                    price = float.Parse(dr["Price"].ToString());
                                }
                            }
                          
                            int lSameIndex = GetExistingItemIndex(partnum);

                            if (lSameIndex != -1)
                            {
                                dgPurchase.Rows[lSameIndex].Cells["ReceivedQty"].Value =  qty;
                                if (columns.Contains("Price"))
                                {
                                    if (dr["Price"].ToString() != "")
                                    {
                                        price = float.Parse(dr["Price"].ToString());
                                        dgPurchase.Rows[lSameIndex].Cells["Price"].Value = price;
                                    }

                                }
                                RecalclineItem(2, lSameIndex);
                                CalcOutOfBalance();
                            }
                           
                          
                        }
                        dgPurchase.RefreshEdit();
                        if (lLastIndex < dgPurchase.Rows.Count)
                        {
                            dgPurchase.FirstDisplayedScrollingRowIndex = lLastIndex;
                        }
                    }
                }
            }
        }
    }
}