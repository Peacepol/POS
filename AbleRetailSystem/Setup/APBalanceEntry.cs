using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace RestaurantPOS
{
    public partial class APBalanceEntry : Form
    {
        private string TranID = "";
        private string ProfileID = "";
        private static decimal TaxRate;
        private static decimal CurBalance;
        private static decimal CreditLimit;
        private static decimal TaxEx;
        private static string BillNumber;
        private static string APLinkedAccount;
        private static bool IsLoading = false;
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        private DataRow SupplierRow;
        private string ActualDueDate;
        private string ActualDiscountDate;
        private string BalanceDueDays;
        private string DiscountDays;
        private string VolumeDiscount;
        private string EarlyPaymenDiscount;
        private string LatePaymenDiscount;
        public string TermsOfPaymentID;
        private string baldate;
        private string discountdate;
        private string TermRefID;

        public APBalanceEntry(string pFormCode, string pProfileID = "", string pTranID = "")
        {
            InitializeComponent();
            ProfileID = pProfileID;
            TranID = pTranID;
            thisFormCode = pFormCode;

            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(thisFormCode, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Add", out outx);
                if (outx == true)
                {
                    CanAdd = true;
                }
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                if (outx == true)
                {
                    CanDelete = true;
                }
                outx = false;
                FormRights.TryGetValue("View", out outx);
                if (outx == true)
                {
                    CanView = true;
                }
            }
            string outy = "";

            GetAPLinkedAccount();
        }

        private void APBalanceEntry_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }

            if (APLinkedAccount == "" || APLinkedAccount == "0")
            {
                MessageBox.Show("GL Account of Trade Creditors is not setup. Please complete the setup in the Preferences.");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            if (ProfileID != "")
            {
                grpTran.Visible = false;
                LoadSupplier(ProfileID);
                this.btnRecord.Enabled = CanAdd;
            }
            else if (TranID != "")
            {
                LoadTransaction();
                btnRecord.Enabled = false;
                this.pbProfile.Enabled = false;
                this.txtSupplierINV.ReadOnly = true;
                this.txtProfileName.ReadOnly = true;
                this.dtpDate.Enabled = false;
                this.txtMemo.ReadOnly = true;
                this.txtAmount.ReadOnly = true;
                this.txtAmount.InterceptArrowKeys = false;
                this.txtTaxCode.ReadOnly = true;
                this.txtJobCode.ReadOnly = true;
                this.pbTaxCode.Enabled = false;
            }
            else
            {
                grpTran.Visible = true;
            }
               
           
        }
        private void GetAPLinkedAccount()
        {

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = @"SELECT Top 1 TradeCreditorGLCode from Preference";
                SqlCommand cmd_ = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow rw = dt.Rows[0];
                    APLinkedAccount = rw["TradeCreditorGLCode"].ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }


        }

        private void LoadSupplier(string pID)
        {
            SqlConnection connection = null;
            try
            {
                IsLoading = true;
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = @"SELECT p.*,isnull(t.TaxCollectedAccountID,0) as TaxCollectedAccountID, isnull(t.TaxPercentageRate,0) as TaxPercentageRate                     
                           FROM Profile as p left join TaxCodes as t on p.TaxCode = t.TaxCode where Type = 'Supplier' and p.id = " + pID;

                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow rw = dt.Rows[0];
                  
                    if (rw["TermsOfPayment"].ToString() != "CASH")
                    {
                        ProfileID = rw["ID"].ToString();
                        this.txtProfileName.Text = rw["Name"].ToString();
                        this.txtProfileID.Text = rw["ID"].ToString();
                        this.txtTaxCode.Text = rw["TaxCode"].ToString();
                        TaxRate = Convert.ToDecimal(rw["TaxPercentageRate"].ToString());

                        CurBalance = Convert.ToDecimal(rw["CurrentBalance"].ToString());
                        CreditLimit = Convert.ToDecimal(rw["CreditLimit"].ToString());
                        this.lblCurrentBalance.Text = (Math.Round(CurBalance, 2).ToString("C"));
                        this.lblTerms.Text = rw["BalanceDueDays"].ToString() + " Days";
                        LoadDefaultTerms(ProfileID);



                    }
                    else
                    {
                        MessageBox.Show("Supplier selected is a cash only supplier and not allowed for AP sale.");
                        this.txtProfileName.Text = "";
                        this.txtProfileID.Text = "";
                        this.txtTaxCode.Text = "";
                        this.lblTaxEx.Text = "";
                        TaxRate = 0;

                        CurBalance = 0;

                    }
                    RecalcTaxEx();

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
                IsLoading = false;
            }

        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup();
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                LoadSupplier(lProfile[0]);
            }
        }

        private void txtAmount_ValueChanged(object sender, EventArgs e)
        {
            RecalcTaxEx();
        }

        private void RecalcTaxEx()
        {
            TaxEx = Math.Round(this.txtAmount.Value / (1 + TaxRate / 100), 2);
            this.lblTaxEx.Text = TaxEx.ToString();
            this.lblTaxAmt.Text = (this.txtAmount.Value - TaxEx).ToString();

        }

        private void pbTaxCode_Click(object sender, EventArgs e)
        {
            IsLoading = true;
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;

                this.txtTaxCode.Text = Tax[0];
                TaxRate = decimal.Parse(Tax[2] == "" ? "0" : Tax[2]);

                RecalcTaxEx();


            }
            IsLoading = false;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (TranID == "")
            {
                if ((CurBalance + txtAmount.Value) > CreditLimit)
                {
                    MessageBox.Show("Credit Limit Exceeded");

                }
                else
                {
                    SaveAPBalance();
                }

            }
            else
            {

            }

        }

        private void SaveAPBalance()
        {
            SqlConnection con_ = new SqlConnection(CommonClass.ConStr);
            try
            {
                GenerateHistoricalBillNo();
                if (BillNumber != "")
                {
                    string purchasetype = "SBILL";
                    string layouttype = "Service";
                    DateTime lTranDate = this.dtpDate.Value.ToUniversalTime();


                    int NewTermID = 0;
                    NewTermID = NewTerm();
                    
                    string sql = @"INSERT INTO Purchases (
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
                                        TotalPaid,
                                        TotalDue, 
                                        SupplierINVNumber, 
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
                                        @TotalPaid,                                       
                                        @TotalDue, 
                                        @SupplierINVNumber, 
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

                    con_ = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(sql, con_);

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PurchaseType", purchasetype);
                    cmd.Parameters.AddWithValue("@SupplierID", ProfileID);
                    cmd.Parameters.AddWithValue("@PurchaseNumber", BillNumber);
                    cmd.Parameters.AddWithValue("@PromiseDate", lTranDate);
                    cmd.Parameters.AddWithValue("@SupplierINVNumber", txtSupplierINV.Text);
                    cmd.Parameters.AddWithValue("@TransactionDate", lTranDate);                
                    cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);                 
                    cmd.Parameters.AddWithValue("@ShippingContactID", 1);
                    cmd.Parameters.AddWithValue("@BillingContactID", 0);
                    cmd.Parameters.AddWithValue("@TermsReferenceID", NewTermID);
                    cmd.Parameters.AddWithValue("@LayoutType", layouttype);
                    cmd.Parameters.AddWithValue("@SubTotal", TaxEx.ToString());
                    cmd.Parameters.AddWithValue("@FreightSubTotal", 0);
                    cmd.Parameters.AddWithValue("@FreightTax", 0);
                    cmd.Parameters.AddWithValue("@GrandTotal", this.txtAmount.Value.ToString());
                    cmd.Parameters.AddWithValue("@TotalPaid", 0);
                    cmd.Parameters.AddWithValue("@TotalDue", this.txtAmount.Value.ToString());
                    cmd.Parameters.AddWithValue("@PurchaseReference", "N/A");
                    cmd.Parameters.AddWithValue("@ShippingMethodID", 0);
                    cmd.Parameters.AddWithValue("@Memo", this.txtMemo.Text);
                    cmd.Parameters.AddWithValue("@Comments", "");
                    cmd.Parameters.AddWithValue("@TaxTotal", (this.txtAmount.Value - TaxEx).ToString());

                    cmd.Parameters.AddWithValue("@FreightTaxCode", "");
                    cmd.Parameters.AddWithValue("@FreightTaxRate",0);
                    cmd.Parameters.AddWithValue("@POStatus", "Open");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                    

                    con_.Open();
                    int PurchaseID = Convert.ToInt32(cmd.ExecuteScalar());
                    if (PurchaseID > 0)
                    {

                        //INSERT SALES LINES

                        string lJobID = (this.lblJobID.Text == "" ? "0" : this.lblJobID.Text);


                        sql = @"INSERT INTO PurchaseLines(PurchaseID,EntityID,JobID,TransactionDate,
                                TaxCode,Description,OrderQty,ReceiveQty,UnitPrice,ActualUnitPrice,
                                DiscountPercent,SubTotal,TaxAmount,TotalAmount) " +
                            " VALUES(" + PurchaseID + ",2," + lJobID + ",'" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + this.txtTaxCode.Text + "','Starting AP Balance',1,1," + TaxEx.ToString() + "," + TaxEx.ToString() + "," +
                             " 0," + TaxEx.ToString() + "," + (this.txtAmount.Value - TaxEx).ToString() + "," + this.txtAmount.Value.ToString() + ")";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();



                        //CREATE JOURNAL ENTRIES


                        //INSERT JOURNAL FOR Total SBILL HEADER
                        if (this.txtAmount.Value < 0)
                        {
                            //NEGATIVE SO DEBIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber,  Type, EntityID)  " +
                                  " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + APLinkedAccount + "', " +
                                  (this.txtAmount.Value * -1).ToString() + ",'" + BillNumber + "', 'HP',0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //CREDIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                                   " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + APLinkedAccount + "', " +
                                   this.txtAmount.Value.ToString() + ",'" + BillNumber + "', 'HP',0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }

                        //INSERT JOURNAL FOR Total SBILL Detail
                        decimal lTaxAmt = this.txtAmount.Value - TaxEx;
                        if (TaxEx < 0)
                        {
                            //NEGATIVE SO CREDIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID)  " +
                                   " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + APLinkedAccount + "', " +
                                   (TaxEx * -1).ToString() + ",'" + BillNumber + "', 'HP', " + lJobID + ",0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0)
                            {
                                lTaxAmt = TaxEx - this.txtAmount.Value;
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + APLinkedAccount + "', " +
                                  lTaxAmt.ToString() + ",'" + BillNumber + "', 'HP'," + lJobID + ",0)";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }

                        }
                        else
                        {

                            // POSITIVE SO DEBIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)  " +
                                  " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + APLinkedAccount + "', " +
                                  TaxEx.ToString() + ",'" + BillNumber + "', 'HP', " + lJobID + ",0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0)
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID,                                                     
                                                           DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + APLinkedAccount + "', " +
                                  lTaxAmt.ToString() + ",'" + BillNumber + "', 'HP'," + lJobID + ",0)";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        sql = "UPDATE Profile set CurrentBalance = CurrentBalance + " + this.txtAmount.Value.ToString() + " where ID = " + ProfileID;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added AP Starting Balance with Purchase ID " + PurchaseID.ToString(), PurchaseID.ToString());
                        MessageBox.Show("Historical Purchase Recorded successfully", "AP Starting Balances Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }

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

        private void GenerateHistoricalBillNo()
        {

            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = @"SELECT Top 1 PurchaseNumber from Purchases where PurchaseType = 'SBILL' order by PurchaseNumber desc";

                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow rw = dt.Rows[0];
                    string[] lNo = rw["PurchaseNumber"].ToString().Split('-');
                    lNo[1] = lNo[1].TrimStart('0');
                    if (lNo[1] != "")
                    {
                        int i = Convert.ToInt16(lNo[1]);
                        BillNumber = "SB-" + (i + 1).ToString();
                    }
                    else
                    {
                        BillNumber = "SB-1";

                    }




                }
                else
                {
                    BillNumber = "SB-1";
                }






            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void pbJobs_Click(object sender, EventArgs e)
        {
            IsLoading = true;
            SelectJobs DlgJob = new SelectJobs();

            if (DlgJob.ShowDialog() == DialogResult.OK)
            {
                string[] Job = DlgJob.GetJob;
                this.lblJobID.Text = Job[0];
                this.txtJobCode.Text = Job[1];


            }
            IsLoading = false;
        }

        private void txtJobCode_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            this.lblJobID.Text = "";
        }

        private void txtTaxCode_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            TaxRate = 0;
            RecalcTaxEx();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
                    VolumeDiscount = SupplierRow["VolumeDiscount"].ToString();
                    EarlyPaymenDiscount = SupplierRow["EarlyPaymentDiscountPercent"].ToString();
                    LatePaymenDiscount = SupplierRow["LatePaymentChargePercent"].ToString();


                    switch (TermsOfPaymentID)
                    {
                        case "DM"://Day of the Month
                            baldate = SupplierRow["BalanceDueDate"].ToString();
                            discountdate = SupplierRow["DiscountDate"].ToString();
                            lblTerms.Text = "Day of the Month";
                            break;
                        case "DMEOM": //Day of the Month after EOM
                            baldate = SupplierRow["BalanceDueDate"].ToString();
                            discountdate = SupplierRow["DiscountDate"].ToString();
                            lblTerms.Text = "Day of the Month after EOM";
                            break;
                        case "SD": //Specific Days
                            BalanceDueDays = SupplierRow["BalanceDueDays"].ToString();
                            DiscountDays = SupplierRow["DiscountDays"].ToString();
                            lblTerms.Text = "Specific Days";
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            BalanceDueDays = SupplierRow["BalanceDueDays"].ToString();
                            DiscountDays = SupplierRow["DiscountDays"].ToString();
                            lblTerms.Text = "Specific Days after EOM";
                            break;
                        default: //CASH
                            lblTerms.Text = TermsOfPaymentID;
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

        private int NewTerm()
        {
            SqlConnection con = null;
            try
            {
                //Calculate Actual Due Date of the Transaction
                DateTime lTranDate = this.dtpDate.Value.ToUniversalTime();
                DateTime lDueDate = lTranDate;
                DateTime lDiscountDate = lTranDate;
                switch (TermsOfPaymentID)
                {
                    case "SD": //Specific Days
                        BalanceDueDays = SupplierRow["BalanceDueDays"].ToString();
                        DiscountDays = SupplierRow["DiscountDays"].ToString();
                        lDueDate = lTranDate.AddDays(Convert.ToInt16(BalanceDueDays));
                        lDiscountDate = lTranDate.AddDays(Convert.ToInt16(DiscountDays));
                        break;

                    case "DM"://Day of the Month
                        baldate = SupplierRow["BalanceDueDate"].ToString();
                        discountdate = SupplierRow["DiscountDate"].ToString();

                        break;
                    case "DMEOM": //Day of the Month after EOM
                        baldate = SupplierRow["BalanceDueDate"].ToString();
                        discountdate = SupplierRow["DiscountDate"].ToString();
                        break;

                    case "SDEOM"://Specifc Day after EOM
                        BalanceDueDays = SupplierRow["BalanceDueDays"].ToString();
                        DiscountDays = SupplierRow["DiscountDays"].ToString();
                        break;

                    default: //CASH
                        BalanceDueDays = "0";
                        DiscountDays = "0";
                        break;
                }
                ActualDueDate = lTranDate.ToString("yyyy-MM-dd");
                ActualDiscountDate = lDiscountDate.ToString("yyyy-MM-dd");
                //Create Terms
                con = new SqlConnection(CommonClass.ConStr);
                string termsql = @"INSERT INTO Terms (TermsOfPaymentID, DiscountDays, BalanceDueDays, VolumeDiscount, ActualDueDate, ActualDiscountDate) 
                                   VALUES (@TermsOfPaymentID, @DiscountDays, @BalanceDueDays, @VolumeDiscount, @ActualDueDate,@ActualDiscountDate); 
                                   SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(termsql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@TermsOfPaymentID", TermsOfPaymentID);
                cmd.Parameters.AddWithValue("@DiscountDays", DiscountDays);
                cmd.Parameters.AddWithValue("@BalanceDueDays", BalanceDueDays);
                cmd.Parameters.AddWithValue("@VolumeDiscount", VolumeDiscount);
                cmd.Parameters.AddWithValue("@ActualDueDate", ActualDueDate);
                cmd.Parameters.AddWithValue("@ActualDiscountDate", ActualDiscountDate);
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
            }
        }

        private void LoadTransaction()
        {
            int purchaseid = Convert.ToInt32(TranID);
            string sID = "";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 
                string sql = @"SELECT p.*, c.* 
                             FROM Purchases p 
                             INNER JOIN Profile c ON p.SupplierID = c.ID
                             WHERE p.PurchaseID = " + purchaseid;

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRepPurchase;
                TbRepPurchase = new DataTable();
                da.Fill(TbRepPurchase);

                if (TbRepPurchase.Rows.Count > 0)
                {
                    string dte = TbRepPurchase.Rows[0]["TransactionDate"].ToString();
                    dtpDate.Value = DateTime.Parse(dte).ToLocalTime();
                    TermRefID = TbRepPurchase.Rows[0]["TermsReferenceID"].ToString();
                    sID = TbRepPurchase.Rows[0]["SupplierID"].ToString();

                    if (TermRefID == "0")
                    {
                        LoadDefaultTerms(sID);
                    }
                    else
                    {
                        LoadBillTerms();
                    }
                    this.lblTranNo.Text = TbRepPurchase.Rows[0]["PurchaseNumber"].ToString();                   
                    this.lblID.Text = TbRepPurchase.Rows[0]["PurchaseID"].ToString();
                    BillNumber = TbRepPurchase.Rows[0]["PurchaseNumber"].ToString();
                    //billstatus = TbRepPurchase.Rows[0]["POStatus"].ToString();
                    ProfileID = TbRepPurchase.Rows[0]["SupplierID"].ToString();
                    this.txtMemo.Text = TbRepPurchase.Rows[0]["Comments"].ToString();
                    
                    
                    this.txtSupplierINV.Text = TbRepPurchase.Rows[0]["SupplierINVNumber"].ToString();                   
                    this.txtProfileName.Text = TbRepPurchase.Rows[0]["Name"].ToString();
                    txtMemo.Text = TbRepPurchase.Rows[0]["Memo"].ToString();
                  
                    string strtotamt = TbRepPurchase.Rows[0]["GrandTotal"].ToString();
                  
                    string strtottax = TbRepPurchase.Rows[0]["TaxTotal"].ToString();
              

                    //GET PURCHASE LINES
                    //dt.Clear();
                    DataTable TbRepPurchaseLines;

                    TbRepPurchaseLines = new DataTable();
                
                    sql = @"SELECT l.*, j.*, a.AccountNumber 
                                FROM Purchases p 
                                INNER JOIN PurchaseLines l ON p.PurchaseID = l.PurchaseID 
                                LEFT JOIN Jobs j ON l.JobID = j.JobID 
                                INNER JOIN Accounts a ON a.AccountID = l.EntityID 
                                WHERE p.PurchaseID = " + purchaseid;

                    da = new SqlDataAdapter();
                    cmd.CommandText = sql;
                    da.SelectCommand = cmd;

                    da.Fill(TbRepPurchaseLines);
                   
                    for (int i = 0; i < TbRepPurchaseLines.Rows.Count; i++)
                    {
                       
                        string stramt = "";
                        stramt = TbRepPurchaseLines.Rows[i]["TotalAmount"].ToString();
                        txtAmount.Value = Convert.ToDecimal(stramt);
                        this.txtMemo.Text = TbRepPurchaseLines.Rows[i]["Description"].ToString();
                        TaxEx = Convert.ToDecimal(TbRepPurchaseLines.Rows[0]["SubTotal"].ToString());
                        txtJobCode.Text = TbRepPurchaseLines.Rows[i]["JobName"].ToString();
                        txtTaxCode.Text = TbRepPurchaseLines.Rows[i]["TaxCode"].ToString();//Compute taxEx/Taxin                         
                        this.lblTaxEx.Text = Math.Round(TaxEx, 2).ToString("C");
                        decimal ltaxamt = Convert.ToDecimal(TbRepPurchaseLines.Rows[i]["TaxAmount"].ToString());
                        this.lblTaxAmt.Text = Math.Round(ltaxamt, 2).ToString("C");


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

        private void LoadBillTerms()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM Terms where TermsID = " + TermRefID;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow lTermsRow;
                BalanceDueDays = "0";
                if (dt.Rows.Count > 0)
                {
                    lTermsRow = dt.Rows[0];
                    TermsOfPaymentID = lTermsRow["TermsOfPaymentID"].ToString();
                    VolumeDiscount = lTermsRow["VolumeDiscount"].ToString();
                    EarlyPaymenDiscount = lTermsRow["EarlyPaymentDiscountPercent"].ToString();
                    LatePaymenDiscount = lTermsRow["LatePaymentChargePercent"].ToString();


                    switch (TermsOfPaymentID)
                    {
                        case "DM"://Day of the Month
                            baldate = lTermsRow["BalanceDueDate"].ToString();
                            discountdate = lTermsRow["DiscountDate"].ToString();

                            break;
                        case "DMEOM": //Day of the Month after EOM
                            baldate = lTermsRow["BalanceDueDate"].ToString();
                            discountdate = lTermsRow["DiscountDate"].ToString();
                            break;
                        case "SD": //Specific Days
                            BalanceDueDays = lTermsRow["BalanceDueDays"].ToString();
                            DiscountDays = lTermsRow["DiscountDays"].ToString();
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            BalanceDueDays = lTermsRow["BalanceDueDays"].ToString();
                            DiscountDays = lTermsRow["DiscountDays"].ToString();
                            break;
                        default: //CASH
                            lblTerms.Text = TermsOfPaymentID;
                            break;
                    }
                }


                this.lblTerms.Text = BalanceDueDays.ToString() + " Days";

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
    }
}
