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
    public partial class ARBalanceEntry : Form
    {
        private string TranID = "";
        private string ProfileID = "";
        private static decimal TaxRate;
        private static decimal CurBalance;
        private static decimal CreditLimit;
        private static decimal TaxEx;
        private static string SalesNumber;
        private static string ARLinkedAccount;
        private static bool IsLoading = false;
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanView = false;
        private bool CanDelete = false;
        private DataRow CustomerRow;
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

        public ARBalanceEntry(string pFormCode,string pProfileID = "", string pTranID = "")
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
           
            GetARLinkedAccount();
        }

        private void ARBalanceEntry_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            else
            {
                if (ARLinkedAccount == "" || ARLinkedAccount == "0")
                {
                    MessageBox.Show("GL Account of Trade Debtors is not setup. Please complete the setup in the Preferences.");
                    this.BeginInvoke(new MethodInvoker(Close));
                }

                if (ProfileID != "")
                {
                    grpTran.Visible = false;
                    LoadCustomer(ProfileID);
                    this.btnRecord.Enabled = CanAdd;
                }
                else if (TranID != "")
                {
                    LoadTransaction();
                    btnRecord.Enabled = false;
                    this.pbProfile.Enabled = false;
                    this.txtCustomerPO.ReadOnly = true;
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
        }
        private void GetARLinkedAccount()
        {
            
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = @"SELECT Top 1 TradeDebtorGLCode from Preference";
                SqlCommand cmd_ = new SqlCommand(sql, connection);  
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow rw = dt.Rows[0];
                    ARLinkedAccount = rw["TradeDebtorGLCode"].ToString();
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
        private void LoadCustomer(string pID)
        {
            SqlConnection connection = null;
            try
            {
                IsLoading = true;
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = @"SELECT p.*,isnull(t.TaxCollectedAccountID,0) as TaxCollectedAccountID, isnull(t.TaxPercentageRate,0) as TaxPercentageRate                     
                           FROM Profile as p left join TaxCodes as t on p.TaxCode = t.TaxCode where Type = 'Customer' and p.id = " + pID;

                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
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
                        MessageBox.Show("Customer selected is a cash only customer and not allowed for AR sale.");
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
                LoadCustomer(lProfile[0]);
            }
        }

        private void txtAmount_ValueChanged(object sender, EventArgs e)
        {
            RecalcTaxEx();
        }

        private void RecalcTaxEx()
        {
            TaxEx = Math.Round(this.txtAmount.Value / (1 + TaxRate / 100),2);
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
                TaxRate = decimal.Parse(Tax[2] == "" ? "0": Tax[2]);             
               
                RecalcTaxEx();


            }
            IsLoading = false;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if(TranID == "")
            {
                if( (CurBalance + txtAmount.Value) > CreditLimit)
                {
                    MessageBox.Show("Credit Limit Exceeded");

                }else
                {
                    SaveARBalance();
                }
                
            }
            else
            {

            }
           
        }

        private void SaveARBalance()
        {
            SqlConnection con_ = new SqlConnection(CommonClass.ConStr);
            try
            {
                GenerateHistoricalSalesNo();
                if (SalesNumber != "")
                {
                    string salestype = "SINVOICE";
                    string layouttype = "Item";
                    DateTime lTranDate = this.dtpDate.Value.ToUniversalTime();
                    int NewTermID = 0;
                    NewTermID = NewTerm();

                  
                    string sql = @"INSERT INTO Sales ( SalesType,
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
                                                       ClosedDate,
                                                       TotalPaid,
                                                       TotalDue,
                                                       FreightTaxCode,
                                                       FreightTaxRate,
                                                       SalesPersonID ) 
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
                                                       @ClosedDate,
                                                       @TotalPaid,
                                                       @TotalDue,
                                                       @FreightTaxCode,
                                                       @FreightTaxRate,
                                                       @SalesPersonID ); 
                                            SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, con_);
                    cmd.CommandType = CommandType.Text;
                    //Sales Data
                    cmd.Parameters.AddWithValue("@SalesType", salestype);
                    cmd.Parameters.AddWithValue("@CustomerID", ProfileID);
                    cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                    cmd.Parameters.AddWithValue("@SalesNum", SalesNumber);
                    cmd.Parameters.AddWithValue("@TransDate", lTranDate);
                    cmd.Parameters.AddWithValue("@PromiseDate", lTranDate);
                    cmd.Parameters.AddWithValue("@ShippingID", 1);
                    cmd.Parameters.AddWithValue("@TermId", NewTermID);
                   
                    cmd.Parameters.AddWithValue("@InvoiceStatus", "Open");
                    cmd.Parameters.AddWithValue("@ClosedDate", DateTime.Now).Value = System.DBNull.Value;
                    cmd.Parameters.AddWithValue("@Layout", layouttype);
                    cmd.Parameters.AddWithValue("@ShippingMethodID",0);
                    cmd.Parameters.AddWithValue("@SubTotal", TaxEx);
                    cmd.Parameters.AddWithValue("@FreightSubTotal", 0);
                    cmd.Parameters.AddWithValue("@FreightTax", 0);
                    cmd.Parameters.AddWithValue("@TaxTotal", (this.txtAmount.Value - TaxEx).ToString());
                    cmd.Parameters.AddWithValue("@TotalPaid", 0);
                    cmd.Parameters.AddWithValue("@TotalDue", txtAmount.Value);
                    cmd.Parameters.AddWithValue("@Memo", this.txtMemo.Text);
                    cmd.Parameters.AddWithValue("@GrandTotal", txtAmount.Value);
                    cmd.Parameters.AddWithValue("@SalesRef","");
                    cmd.Parameters.AddWithValue("@custPo", txtCustomerPO.Text);
                    cmd.Parameters.AddWithValue("@Comment", "");                    
                    cmd.Parameters.AddWithValue("@FreightTaxCode", "");
                    cmd.Parameters.AddWithValue("@FreightTaxRate", 0);
                    cmd.Parameters.AddWithValue("@SalesPersonID", CommonClass.UserID);
                    cmd.Parameters.AddWithValue("@isTaxInclusive", "Y");
                    con_.Open();
                    int SalesID = Convert.ToInt32(cmd.ExecuteScalar());
                    if(SalesID > 0)
                    {

                        //INSERT SALES LINES

                        string lJobID = (this.lblJobID.Text == "" ? "0" : this.lblJobID.Text);
                       

                        sql = @"INSERT INTO SalesLines(SalesID,EntityID,JobID,TransactionDate,
                                TaxCode,Description,OrderQty,ShipQty,UnitPrice,ActualUnitPrice,
                                DiscountPercent,SubTotal,TaxAmount,TotalAmount) " +
                            " VALUES(" + SalesID + ",2," + lJobID + ",'" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," + 
                             "'" + this.txtTaxCode.Text + "','Starting AR Balance',1,1," + TaxEx.ToString() + "," + TaxEx.ToString() + "," + 
                             " 0," + TaxEx.ToString() + "," + (this.txtAmount.Value - TaxEx).ToString() + "," + this.txtAmount.Value.ToString()+ ")";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();



                        //CREATE JOURNAL ENTRIES


                        //INSERT JOURNAL FOR Total SInvoice HEADER
                        if (this.txtAmount.Value < 0)
                        {
                            //NEGATIVE SO CREDIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber,  Type, EntityID)  " +
                                  " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + ARLinkedAccount + "', " +
                                  (this.txtAmount.Value * -1).ToString() + ",'" + SalesNumber + "', 'HS',0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //DEBIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID)  " +
                                   " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + ARLinkedAccount + "', " +
                                   this.txtAmount.Value.ToString() + ",'" + SalesNumber + "', 'HS',0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }

                        //INSERT JOURNAL FOR Total SInvoice Detail
                        decimal lTaxAmt = this.txtAmount.Value - TaxEx;
                        if (TaxEx < 0)
                        {
                            //NEGATIVE SO DEBIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)  " +
                                   " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + ARLinkedAccount + "', " +
                                   (TaxEx * -1).ToString() + ",'" + SalesNumber + "', 'HS', " + lJobID + ",0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0)
                            {
                                lTaxAmt = TaxEx - this.txtAmount.Value;
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + ARLinkedAccount + "', " +
                                  lTaxAmt.ToString() + ",'" + SalesNumber + "', 'HS'," + lJobID + ",0)";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }

                        }
                        else
                        {                           

                            // POSITIVE SO CREDIT AMOUNT
                            sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID)  " +
                                  " VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + ARLinkedAccount + "', " +
                                  TaxEx.ToString() + ",'" + SalesNumber + "', 'HS', " + lJobID + ",0)";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0)
                            {                                
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID,                                                     
                                                           CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.txtMemo.Text + "', '" + this.txtMemo.Text + "', '" + ARLinkedAccount + "', " +
                                  lTaxAmt.ToString() + ",'" + SalesNumber + "', 'HS'," + lJobID + ",0)";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        sql = "UPDATE Profile set CurrentBalance = CurrentBalance + " + this.txtAmount.Value.ToString() + " where ID = " + ProfileID;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added AR Starting Balance with Sales ID " + SalesID.ToString(), SalesID.ToString());
                        MessageBox.Show("Historical Sale Recorded successfully", "AR Starting Balances Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void GenerateHistoricalSalesNo()
        {

            SqlConnection connection = null;
            try
            {
                
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = @"SELECT Top 1 SalesNumber from Sales where SalesType = 'SINVOICE' order by SalesNumber desc";

                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow rw = dt.Rows[0];
                    string[] lNo = rw["SalesNumber"].ToString().Split('-');
                    lNo[1] = lNo[1].TrimStart('0');
                    if(lNo[1] != "") {
                        int i = Convert.ToInt16(lNo[1]);
                        SalesNumber = "SI-" + (i + 1).ToString();
                    }else
                    {
                        SalesNumber = "SI-1";

                    }




                }else
                {
                    SalesNumber = "SI-1";
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
                    CustomerRow = dt.Rows[0];

                    TermsOfPaymentID = CustomerRow["TermsOfPayment"].ToString();
                    VolumeDiscount = CustomerRow["VolumeDiscount"].ToString();
                    EarlyPaymenDiscount = CustomerRow["EarlyPaymentDiscountPercent"].ToString();
                    LatePaymenDiscount = CustomerRow["LatePaymentChargePercent"].ToString();


                    switch (TermsOfPaymentID)
                    {
                        case "DM"://Day of the Month
                            baldate = CustomerRow["BalanceDueDate"].ToString();
                            discountdate = CustomerRow["DiscountDate"].ToString();
                            lblTerms.Text = "Day of the Month";
                            break;
                        case "DMEOM": //Day of the Month after EOM
                            baldate = CustomerRow["BalanceDueDate"].ToString();
                            discountdate = CustomerRow["DiscountDate"].ToString();
                            lblTerms.Text = "Day of the Month after EOM";
                            break;
                        case "SD": //Specific Days
                            BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
                            DiscountDays = CustomerRow["DiscountDays"].ToString();
                            lblTerms.Text = "Specific Days";
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
                            DiscountDays = CustomerRow["DiscountDays"].ToString();
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
                        BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
                        DiscountDays = CustomerRow["DiscountDays"].ToString();
                        lDueDate = lTranDate.AddDays(Convert.ToInt16(BalanceDueDays));
                        lDiscountDate = lTranDate.AddDays(Convert.ToInt16(DiscountDays));
                        break;

                    case "DM"://Day of the Month
                        baldate = CustomerRow["BalanceDueDate"].ToString();
                        discountdate = CustomerRow["DiscountDate"].ToString();

                        break;
                    case "DMEOM": //Day of the Month after EOM
                        baldate = CustomerRow["BalanceDueDate"].ToString();
                        discountdate = CustomerRow["DiscountDate"].ToString();
                        break;

                    case "SDEOM"://Specifc Day after EOM
                        BalanceDueDays = CustomerRow["BalanceDueDays"].ToString();
                        DiscountDays = CustomerRow["DiscountDays"].ToString();
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
            int salesid = Convert.ToInt32(TranID);
            string sID = "";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 
                string sql = @"SELECT s.*, c.* 
                             FROM Sales s
                             INNER JOIN Profile c ON s.CustomerID = c.ID
                             WHERE s.SalesID = " + salesid;

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
                    sID = TbRepPurchase.Rows[0]["CustomerID"].ToString();

                    if (TermRefID == "0")
                    {
                        LoadDefaultTerms(sID);
                    }
                    else
                    {
                        LoadBillTerms();
                    }
                    this.lblTranNo.Text = TbRepPurchase.Rows[0]["SalesNumber"].ToString();
                    this.lblID.Text = TbRepPurchase.Rows[0]["SalesID"].ToString();
                    SalesNumber = TbRepPurchase.Rows[0]["SalesNumber"].ToString();
                    //billstatus = TbRepPurchase.Rows[0]["POStatus"].ToString();
                    ProfileID = TbRepPurchase.Rows[0]["CustomerID"].ToString();
                    this.txtMemo.Text = TbRepPurchase.Rows[0]["Comments"].ToString();


                    this.txtCustomerPO.Text = TbRepPurchase.Rows[0]["CustomerPONumber"].ToString();
                    this.txtProfileName.Text = TbRepPurchase.Rows[0]["Name"].ToString();
                    txtMemo.Text = TbRepPurchase.Rows[0]["Memo"].ToString();

                    string strtotamt = TbRepPurchase.Rows[0]["GrandTotal"].ToString();

                    string strtottax = TbRepPurchase.Rows[0]["TaxTotal"].ToString();


                    //GET PURCHASE LINES
                    //dt.Clear();
                    DataTable TbRepSalesLines;

                    TbRepSalesLines = new DataTable();

                    sql = @"SELECT l.*, j.* 
                                FROM Sales s 
                                INNER JOIN SalesLines l ON s.SalesID = l.SalesID 
                                LEFT JOIN Jobs j ON l.JobID = j.JobID 
                                WHERE s.SalesID = " + salesid;

                    da = new SqlDataAdapter();
                    cmd.CommandText = sql;
                    da.SelectCommand = cmd;

                    da.Fill(TbRepSalesLines);

                    for (int i = 0; i < TbRepSalesLines.Rows.Count; i++)
                    {

                        string stramt = "";
                        stramt = TbRepSalesLines.Rows[i]["TotalAmount"].ToString();
                        txtAmount.Value = Convert.ToDecimal(stramt);
                        this.txtMemo.Text = TbRepSalesLines.Rows[i]["Description"].ToString();
                        TaxEx = Convert.ToDecimal(TbRepSalesLines.Rows[0]["SubTotal"].ToString());
                        txtJobCode.Text = TbRepSalesLines.Rows[i]["JobName"].ToString();
                        txtTaxCode.Text = TbRepSalesLines.Rows[i]["TaxCode"].ToString();//Compute taxEx/Taxin                         
                        this.lblTaxEx.Text = Math.Round(TaxEx, 2).ToString("C");
                        decimal ltaxamt = Convert.ToDecimal(TbRepSalesLines.Rows[i]["TaxAmount"].ToString());
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
                string selectSql = "SELECT * FROM Terms where TermsID = " + (TermRefID == "" ? "0" : TermRefID);
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
