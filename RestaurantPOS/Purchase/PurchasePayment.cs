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
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AbleRetailPOS
{
    public partial class PurchasePayments : Form
    {
        bool IsLoading = true;
        private int mPaymentID = 0;
        private string TranNo;
        private string ChequeNo = "";
        private string CurSeries = "";
        private string CurBillSeries = "";
        private string gPaymentNo = "";
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string IssuingAccountID = "";
        private DataTable dtopenpurchases = null;
        private decimal TotalDiscount;


        private CommonClass.InvocationSource InvokeSrc;
        private string pTranNo;
        private string paymentMethod;
        private string glAccountCode;

        public PurchasePayments(CommonClass.InvocationSource pInvokeSrc, string pTranNo = "")
        {
            InitializeComponent();
            TranNo = pTranNo;
            InvokeSrc = pInvokeSrc;
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
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

        public PurchasePayments(string pTranNo)
        {
            this.pTranNo = pTranNo;
        }

        private void PurchasePayments_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dgridPayBills.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (dgridPayBills.Rows.Count == 0)
            {
                btnRecord.Enabled = false;
                btnDelete.Enabled = false;
            }

            this.dgridPayBills.CellFormatting += new DataGridViewCellFormattingEventHandler(dgridPayBills_CellFormatting);
            IssuingAccountID = CommonClass.DRowPref["PurchasePaymentGLCode"].ToString();

            //setLastBankUsed();           

            if (TranNo != "")
            {
                LoadFeededPayment();
            }

            //if (TranNo == "")
            //{
            //    this.dtpdate.MinDate = CommonClass.LockedPeriod.AddDays(1);
            //    btnDelete.Enabled = false;
            //}
           
            IsLoading = false;
        }

        //private void setLastBankUsed()
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(CommonClass.ConStr);
        //        connection.Open();
        //        string sql = @"SELECT TOP 1  
        //                         a.*
        //                       FROM Payment pmt
        //                       INNER JOIN Accounts a ON a.AccountID = pmt.AccountID 
        //                       WHERE pmt.PaymentFor = 'Purchase'
        //                       ORDER BY a.AccountID DESC";

        //        SqlCommand cmd_ = new SqlCommand(sql, connection);
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        DataTable dt = new DataTable();

        //        da.SelectCommand = cmd_;
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            if (dt.Rows[0]["AccountID"].ToString() == CommonClass.DRowLA["ElectronicPaymentsID"].ToString())
        //            {
        //                EPNumber.Text = dt.Rows[0]["AccountNumber"].ToString();
        //                EPAccountName.Text = dt.Rows[0]["AccountName"].ToString();
        //                IssuingAccount.Text = "";
        //                lblIssuingAccountID.Text = "";
        //                rdoEPayments.Checked = true;
        //            }
        //            else
        //            {
        //                IssuingAccount.Text = dt.Rows[0]["AccountNumber"].ToString();
        //                lblIssuingAccountID.Text = dt.Rows[0]["AccountID"].ToString();
        //                EPNumber.Text = "";
        //                EPAccountName.Text = "";
        //                rdoAccount.Checked = true;
        //            }

        //            IssuingAccountID = dt.Rows[0]["AccountID"].ToString();
        //            string strcuracctbal = dt.Rows[0]["CurrentAccountBalance"].ToString();
        //            decimal lbal = strcuracctbal != "" ? Convert.ToDecimal(strcuracctbal) : 0;
        //            CurrentAccountBalance.Text = (Math.Round(lbal, 2).ToString("C"));
        //            GenerateChequeNumber(dt.Rows[0]["LastChequeNumber"].ToString());
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //            connection.Close();
        //    }
        //}     

        //private void setEPBank()
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(CommonClass.ConStr);
        //        connection.Open();
        //        string sql = "SELECT TOP 1 * FROM Accounts WHERE AccountID = " + CommonClass.DRowLA["ElectronicPaymentsID"];
        //        SqlCommand cmd_ = new SqlCommand(sql, connection);
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        DataTable dt = new DataTable();

        //        da.SelectCommand = cmd_;
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            EPNumber.Text = dt.Rows[0]["AccountNumber"].ToString();
        //            EPAccountName.Text = dt.Rows[0]["AccountName"].ToString();
        //            string strcuracctbal = dt.Rows[0]["CurrentAccountBalance"].ToString();
        //            decimal lbal = strcuracctbal != "" ? Convert.ToDecimal(strcuracctbal) : 0;
        //            CurrentAccountBalance.Text = (Math.Round(lbal, 2).ToString("C"));
        //            GenerateChequeNumber(dt.Rows[0]["LastChequeNumber"].ToString());
        //            IssuingAccountID = dt.Rows[0]["AccountID"].ToString();
        //            IssuingAccount.Text = "";
        //            lblIssuingAccountID.Text = "";
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //            connection.Close();
        //    }
        //}

        //private void GenerateChequeNumber(string pLastNo)
        //{
        //    string lSeries = "";
        //    int lCnt = 0;
        //    int lNewSeries = 0;
        //    lSeries = (pLastNo);
        //    lCnt = lSeries.Length;
        //    lSeries = lSeries.TrimStart('0');
        //    lSeries = (lSeries == "" ? "0" : lSeries);
        //    int n;
        //    if (int.TryParse(lSeries, out n))
        //    {
        //        lNewSeries = n + 1;
        //    }
        //    else
        //    {
        //        lNewSeries = 1;
        //    }
        //    ChequeNo = lNewSeries.ToString().PadLeft(lCnt, '0');
        //    ChequeNumber.Text = ChequeNo;
        //}

        private void dgridPayBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow currentselectedrow = dgridPayBills.CurrentRow;
            int currentpurchaseid = Convert.ToInt32(currentselectedrow.Cells["PurchaseID"].Value);

            switch (e.ColumnIndex)
            {
                case 2: //Purchase Number
                    ShowPurchaseInfo(currentpurchaseid);
                    break;
                default:
                    break;
            }

            DataGridViewRow index = dgridPayBills.CurrentRow;
            if (IssuingAccountID == "" || InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
            {
                index.Cells["Discount"].ReadOnly = true;
                index.Cells["AmountApplied"].ReadOnly = true;
            }
            else
            {
                try
                {
                    if (e.RowIndex == -1)
                        return;

                    index.Cells["Discount"].ReadOnly = false;
                    index.Cells["AmountApplied"].ReadOnly = false;

                    switch (e.ColumnIndex)
                    {            
                        case 6: //Discount
                        case 8: //AmountApplied
                            dgridPayBills.CurrentCell = dgridPayBills.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            dgridPayBills.BeginEdit(true);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void dgridPayBills_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // ToDo: insert your own column index magic number 
            //if (this.dgridMoneyOut.Rows[e.RowIndex].IsNewRow && e.ColumnIndex == 3)
            //{
            //    e.Value = Properties.Resources.Assign_OneToMany;
            //}
            if ((e.ColumnIndex == 5    //Amount
                || e.ColumnIndex == 6  //Discount
                || e.ColumnIndex == 7  //TotalOwe
                || e.ColumnIndex == 8  //AmountApplied
                /*|| e.ColumnIndex == 9*/)
                && e.RowIndex != dgridPayBills.NewRowIndex)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }     
        
        private void dgridPayBills_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            
        }
 
        private void rdoAccount_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAccount.Checked == true)
            {
                if(lblIssuingAccountID.Text == "")
                {
                    //ShowPayfromAccountLookup();
                }
            }
        }

        private void rdoEPayments_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEPayments.Checked)
            {
                //setEPBank();
            }
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                lblProfileID.Text = lProfile[0];
                lblProfileName.Text = lProfile[2];
                PayeeInfo.Text = lProfile[2];
                LoadContacts(Convert.ToInt16(lProfile[0]), 0);
                Memo.Text = "Payment; " + lProfile[2];
                paymentMethod = lProfile[13];

                LoadPaymentMethod();
                IssuingAccount.Text = glAccountCode;
                dgridPayBills.Rows.Clear();
                LoadTransaction(Int32.Parse(lProfile[0]));
                CalcOutOfBalance();
            }    
        }
        void LoadPaymentMethod()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM PaymentMethods WHERE PaymentMethod = '" + paymentMethod + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    //paymentMethod.Text = dr["PaymentMethod"].ToString();
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
            if (dt.Rows.Count > 0)
            {
                            
                PayeeInfo.AppendText(Environment.NewLine + dt.Rows[0]["Street"].ToString() + " " + dt.Rows[0]["City"].ToString());
                PayeeInfo.AppendText(Environment.NewLine  + " " + dt.Rows[0]["State"].ToString() + " " + dt.Rows[0]["Country"].ToString() + " " + dt.Rows[0]["PostCode"].ToString());
               
            }
        }
        private void txtTotalPaid_TextChanged(object sender, EventArgs e)
        {
            CalcOutOfBalance();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (dgridPayBills.Rows.Count > 0)
            {
                int paymentid = RecordPayment();
            }
        }

        void CalcOutOfBalance()
        {
            this.TotalApplied.Value = 0;
            this.TotalOwe.Value = 0;
            decimal outnum = 0;
            decimal owednum = 0;
            decimal appliednum = 0;

            for (int i = 0; i < this.dgridPayBills.Rows.Count; i++)
            {
                if (this.dgridPayBills.Rows[i].Cells["TotalOwed"].Value != null
                    && this.dgridPayBills.Rows[i].Cells["TotalOwed"].Value.ToString() != "")
                {
                    Decimal.TryParse(this.dgridPayBills.Rows[i].Cells["TotalOwed"].Value.ToString(), out owednum);
                    TotalOwe.Value += owednum;
                    //outnum = 0;
                    if (dgridPayBills.Rows[i].Cells["AmountApplied"].Value != null)
                    {
                        Decimal.TryParse(dgridPayBills.Rows[i].Cells["AmountApplied"].Value.ToString(), out appliednum);
                        TotalApplied.Value += appliednum;
                    }
                       
                }
            }

            this.TotalPaid.Value = this.TotalSpentAmount.Value;
            OutOfBalance.Value = this.TotalSpentAmount.Value - TotalApplied.Value;
        }    

        private void Recalcline(int pColIndex, int pRowIndex)
        {
            if (pRowIndex < 0)
                return;

            if (!IsLoading)
            {
                DataGridViewRow dgvRows = dgridPayBills.Rows[pRowIndex];

                decimal lTotalOwe = 0;
                decimal lDiscount = 0;
                decimal lAmount = 0;
                decimal lTotalPaid = 0;

                if (pColIndex == 6) //Discount
                {
                    if (dgvRows.Cells["Amt"].Value != null)
                    {
                        if (dgvRows.Cells["Discount"].Value != null
                            && dgvRows.Cells["Discount"].Value.ToString() != "")
                            lDiscount = decimal.Parse(dgvRows.Cells["Discount"].Value.ToString(), NumberStyles.Currency);
                        if (dgvRows.Cells["Amt"].Value.ToString() != "")
                            lAmount = decimal.Parse(dgvRows.Cells["Amt"].Value.ToString(), NumberStyles.Currency);
                        if (dgvRows.Cells["TotalSettled"].Value.ToString() != "")
                            lTotalPaid = decimal.Parse(dgvRows.Cells["TotalSettled"].Value.ToString(), NumberStyles.Currency);

                        lTotalOwe = lAmount - lTotalPaid - lDiscount;
                        dgvRows.Cells["TotalOwed"].Value = lTotalOwe;
                    }
                }
            }
        }

        private void ShowPurchaseInfo(int pPurchaseID)
        {
            if (CommonClass.EnterPurchasefrm == null
                || CommonClass.EnterPurchasefrm.IsDisposed)
            {
                CommonClass.EnterPurchasefrm = new Purchase.EnterPurchase(CommonClass.InvocationSource.REGISTER, pPurchaseID.ToString());
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
            CommonClass.EnterPurchasefrm.Show();
            CommonClass.EnterPurchasefrm.Focus();
            if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EnterPurchasefrm.Close();
            }

            this.Cursor = Cursors.Default;
        }

        private void TotalSpentAmount_ValueChanged(object sender, EventArgs e)
        {
            CalcOutOfBalance();
        } 

        private void dgridPayBills_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6      //Discount
                || e.ColumnIndex == 8   //AmountApplied
                /*|| e.ColumnIndex == 9*/)
            {
                Recalcline(e.ColumnIndex, e.RowIndex);
                CalcOutOfBalance();
            }
        }

        private int RecordPayment()
        {
            int lPaymentID = 0;
            double d = (double)this.OutOfBalance.Value;
            if (d != 0)
            {
                MessageBox.Show("Transaction is Out of Balance.", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                PaymentCommon.GeneratePaymentNumber(ref CurSeries, ref gPaymentNo, "BP");

                if (gPaymentNo != "")
                {
                    lPaymentID = CreatePaymentRecord(gPaymentNo);
                    PaymentCommon.UpdatePaymentNumber(ref CurSeries, "BP");
                    if (PaymentCommon.CreateJournalEntriesBP(lPaymentID, Memo.Text))
                    {
                        //TransactionClass.CreateCurrentEarningsTran(gPaymentNo);
                        TransactionClass.UpdateProfileBalances(lblProfileID.Text, TotalApplied.Value * -1);
                        //if (TransactionClass.UpdateAccountBalances(gPaymentNo))
                        //{
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Payment Transaction No. " + gPaymentNo, lPaymentID.ToString());
                            MessageBox.Show("Payment Record created successfully", "Payment entry Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        //}
                    }
                }
                else if (gPaymentNo == "")
                {
                    MessageBox.Show("Invalid Payment Number");
                }
            }
            return lPaymentID;
        }

        private int CreatePaymentRecord(string pPaymentNo)
        {
            SqlConnection con = null;
            try
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = dtpdate.Value.ToUniversalTime();
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
                                            PaymentChequeNumber
                                            )
                                        VALUES ( 
                                            @ProfileID, 
                                            @TotalAmount, 
                                            @Memo, 
                                            'Purchase',
                                            @AccountID,
                                            @UserID,
                                            (SELECT id FROM PaymentMethods WHERE PaymentMethod = 'Cheque'),
                                            @TransactionDate,
                                            @PaymentNumber,
                                            @ChequeNo
                                        ); SELECT SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(strpaymentsql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProfileID", lblProfileID.Text);
                cmd.Parameters.AddWithValue("@PaymentNumber", pPaymentNo);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalPaid.Value);
                cmd.Parameters.AddWithValue("@Memo", Memo.Text);
                cmd.Parameters.AddWithValue("@AccountID", glAccountCode);
                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@TransactionDate", trandate);
                cmd.Parameters.AddWithValue("@ChequeNo", ChequeNumber.Text);

                con.Open();
                int paymentid = Convert.ToInt32(cmd.ExecuteScalar());
                TotalDiscount = 0;

                if (paymentid != 0)
                {
                    for (int i = 0; i < dgridPayBills.Rows.Count; i++)
                    {
                        if (dgridPayBills.Rows[i].Cells["Amt"].Value != null
                            && dgridPayBills.Rows[i].Cells["Amt"].Value.ToString() != "")
                        {
                            string Amount = (dgridPayBills.Rows[i].Cells["AmountApplied"].Value != null ? dgridPayBills.Rows[i].Cells["AmountApplied"].Value.ToString() : "0");
                            decimal deciamt = decimal.Parse(Amount, NumberStyles.Currency);
                            int PurchaseID = dgridPayBills.Rows[i].Cells["PurchaseID"].Value != null ? Convert.ToInt16(dgridPayBills.Rows[i].Cells["PurchaseID"].Value) : 0;
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
                            pmtlinecmd.Parameters.AddWithValue("@EntityID", PurchaseID);
                            pmtlinecmd.Parameters.AddWithValue("@Amount", deciamt);
                            pmtlinecmd.Parameters.AddWithValue("@EntryDate", trandate);

                            pmtlinecmd.ExecuteNonQuery();

                            string strDiscount = dgridPayBills.Rows[i].Cells["Discount"].Value != null ? dgridPayBills.Rows[i].Cells["Discount"].Value.ToString() : "0";
                            decimal lDiscount = strDiscount != "" ? Decimal.Parse(strDiscount, NumberStyles.Currency) : 0;

                            if (lDiscount > 0)
                            {
                                TotalDiscount += lDiscount;
                                PaymentCommon.DiscountFields discountparams = new PaymentCommon.DiscountFields();
                                discountparams.TermsReferenceID = dgridPayBills.Rows[i].Cells["PurchaseTermsReferenceID"].Value != null ? Convert.ToInt32(dgridPayBills.Rows[i].Cells["TermsReferenceID"].Value) : 0;
                                discountparams.ShippingContactID = dgridPayBills.Rows[i].Cells["PurchaseShippingContactID"].Value != null ? Convert.ToInt32(dgridPayBills.Rows[i].Cells["ShippingContactID"].Value) : 0;
                                discountparams.ShippingMethodID = dgridPayBills.Rows[i].Cells["PurchaseShippingMethodID"].Value != null ? Convert.ToInt32(dgridPayBills.Rows[i].Cells["ShippingMethodID"].Value) : 0;
                                discountparams.PurchaseReference = dgridPayBills.Rows[i].Cells["PurchaseReference"].Value != null ? dgridPayBills.Rows[i].Cells["PurchaseReference"].Value.ToString() : "";
                                discountparams.Memo = dgridPayBills.Rows[i].Cells["PurchaseMemo"].Value != null ? dgridPayBills.Rows[i].Cells["Memo"].Value.ToString() : "";
                                discountparams.SupplierInvNumber = dgridPayBills.Rows[i].Cells["PurchaseSupplierInvNumber"].Value != null ? dgridPayBills.Rows[i].Cells["SupplierInvNumber"].Value.ToString() : "";
                                discountparams.Discount = dgridPayBills.Rows[i].Cells["Discount"].Value != null ? Convert.ToDecimal(dgridPayBills.Rows[i].Cells["Discount"].Value.ToString()) : 0;

                                CreateDiscountPurchaseBill(discountparams);
                            }
                            string lPurchaseNo = dgridPayBills.Rows[i].Cells["PurchaseNo"].Value != null ? dgridPayBills.Rows[i].Cells["PurchaseNo"].Value.ToString() : "0";
                            PaymentCommon.UpdatePurchaseRecord(trandate, lPurchaseNo, deciamt, lDiscount);
                        }
                    }
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

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
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

        private void dgridPayBills_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= TextboxNumeric_KeyPress;
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 5)
            {
                e.Control.KeyPress += TextboxNumeric_KeyPress;
            }
        }

        private void LoadTransaction(int pSupplierID = 0)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 
                string sql = @"SELECT prc.*, 
                                  p.ProfileIDNumber,
                                  p.CurrentBalance, 
                                  p.Name ";
                if (glAccountCode != "")
                    sql += ", " + glAccountCode + " AS AccountID ";
                sql += @"FROM Purchases prc
                              INNER JOIN Profile p ON prc.SupplierID = p.ID
                              WHERE prc.PurchaseType IN ('BILL', 'SBILL')
                              AND prc.POStatus = 'Open'
                              AND prc.SupplierID = " + pSupplierID;

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dtopenpurchases = new DataTable();
                da.Fill(dtopenpurchases);

                bool isTherePaymentNotYetFull = false;

                if (dtopenpurchases.Rows.Count > 0)
                {
                    int purchaseid = (int) dtopenpurchases.Rows[0]["PurchaseID"];
                    //Memo.Text = dtopenpurchases.Rows[0]["Memo"].ToString();
                    //Load the current date as that is the default 
                    dtpdate.Value = DateTime.Now;  //dtpdate.Value = Convert.ToDateTime(dtopenpurchases.Rows[0]["TransactionDate"]).ToLocalTime();
                    //decimal profilecurrentbalance = (dtopenpurchases.Rows[0]["CurrentBalance"] != null) ? Convert.ToDecimal(dtopenpurchases.Rows[0]["CurrentBalance"]) : 0;

                    foreach (DataRow dtRow in dtopenpurchases.Rows)
                    {
                        int newrowindex = dgridPayBills.Rows.Add();
                        dgridPayBills["PurchaseID", newrowindex].Value = dtRow["PurchaseID"];
                        dgridPayBills["AccountID", newrowindex].Value = glAccountCode;
                        dgridPayBills["PurchaseNo", newrowindex].Value = dtRow["PurchaseNumber"];
                        dgridPayBills["Status", newrowindex].Value = dtRow["POStatus"];
                        dgridPayBills["Date", newrowindex].Value = dtRow["EntryDate"].ToString();
                        dgridPayBills["Amt", newrowindex].Value = dtRow["GrandTotal"];
                        dgridPayBills["TotalSettled", newrowindex].Value = dtRow["TotalPaid"];
                        //the ff. fields are hidden only used to pull back the data when creating a negative invoice for discounts
                        dgridPayBills["PurchaseTermsReferenceID", newrowindex].Value = dtRow["TermsReferenceID"];
                        dgridPayBills["PurchaseShippingContactID", newrowindex].Value = dtRow["ShippingContactID"];
                        dgridPayBills["PurchaseShippingMethodID", newrowindex].Value = dtRow["ShippingMethodID"];
                        dgridPayBills["PurchaseMemo", newrowindex].Value = dtRow["Memo"];
                        dgridPayBills["PurchaseSupplierInvNumber", newrowindex].Value = dtRow["SupplierInvNumber"];
                        dgridPayBills["PurchaseReference", newrowindex].Value = dtRow["PurchaseReference"];
                        decimal discount = 0;

                        if (dgridPayBills["Discount", newrowindex].Value != null
                            && dgridPayBills["Discount", newrowindex].Value.ToString() != "")
                        {
                            discount = Int32.Parse(dgridPayBills["Discount", newrowindex].Value.ToString());
                        }
                        decimal totalpaid = (dtRow["TotalPaid"] != null) ? Convert.ToDecimal(dtRow["TotalPaid"]) : 0;
                        decimal grandtotal = Convert.ToDecimal(dtRow["GrandTotal"]);
                        decimal totalowed = grandtotal - totalpaid;

                        dgridPayBills["TotalOwed", newrowindex].Value = totalowed - discount;


                        if (totalowed != 0)
                            isTherePaymentNotYetFull = true;
                    }
                }
                SqlCommand cmdchk = new SqlCommand("SELECT PaymentID FROM Payment WHERE ProfileID = " + pSupplierID + " ORDER BY PaymentID DESC", con);
                con.Open();
                object ret = cmdchk.ExecuteScalar();
                int paymentid = ret != null ? Convert.ToInt32(ret) : 0;

                if (mPaymentID != 0)
                {
                    if (isTherePaymentNotYetFull)
                        btnRecord.Enabled = true;
                    else
                        btnRecord.Enabled = false;

                    btnDelete.Enabled = false;
                }
                else
                {
                    btnRecord.Enabled = true;
                    btnDelete.Enabled = false;
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

        private void LoadFeededPayment()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 

                string sql = @"SELECT prc.*, 
                                  prcl.TaxCode,
                                  p.ProfileIDNumber,
                                  p.CurrentBalance, 
                                  p.Name,
                                  c.Phone,
                                  c.Email ";
                if (glAccountCode != "")
                    sql += ", " + glAccountCode + " AS AccountID ";
                if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                    sql += @", pts.Memo AS PaymentMemo, 
                               pts.TransactionDate AS PaymentDate,
                               pts.PaymentNumber, 
                               pl.Amount AS AmountApplied ";

                sql += @" FROM Purchases prc
                              INNER JOIN PurchaseLines prcl ON prc.PurchaseID = prcl.PurchaseID
                              INNER JOIN Profile p ON prc.SupplierID = p.ID
                              LEFT JOIN Contacts c ON prc.ShippingContactID = c.ContactID
                              ";
                if (InvokeSrc == CommonClass.InvocationSource.REGISTER)
                {
                    sql += " WHERE prc.PurchaseID = " + Convert.ToInt64(TranNo);
                }
                else if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                {
                    sql += @" INNER JOIN PaymentLines pl ON pl.EntityID = prc.PurchaseID 
                            INNER JOIN Payment pts ON pts.PaymentID = pl.PaymentID
                            WHERE pl.PaymentID = " + Convert.ToInt64(TranNo);

                    btnRecord.Enabled = false;
                    TotalSpentAmount.Enabled = false;
                }
               
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dtopenpurchases = new DataTable();
                da.Fill(dtopenpurchases);

                bool isTherePaymentNotYetFull = false;

                if (dtopenpurchases.Rows.Count > 0)
                {
                    if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                    {
                        btnPrint.Visible = true;
                        btnDelete.Enabled = CanDelete;
                    }

                    int purchaseid = (int)dtopenpurchases.Rows[0]["PurchaseID"];
                    if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                        Memo.Text = dtopenpurchases.Rows[0]["PaymentMemo"].ToString();
                    else
                        Memo.Text = dtopenpurchases.Rows[0]["Memo"].ToString();

                    lblProfileName.Text = dtopenpurchases.Rows[0]["Name"].ToString();
                    lblProfileID.Text = dtopenpurchases.Rows[0]["SupplierID"].ToString();
                    PayeeInfo.Text = dtopenpurchases.Rows[0]["Name"].ToString();
                    PayeeInfo.AppendText(Environment.NewLine + dtopenpurchases.Rows[0]["TaxCode"].ToString());
                    PayeeInfo.AppendText(Environment.NewLine + dtopenpurchases.Rows[0]["ShippingContactID"].ToString() 
                                                            + " " + dtopenpurchases.Rows[0]["ShippingMethodID"].ToString() 
                                                            + " " + dtopenpurchases.Rows[0]["Phone"].ToString());
                    PayeeInfo.AppendText(Environment.NewLine + dtopenpurchases.Rows[0]["Email"].ToString());

                    decimal profilecurrentbalance = (dtopenpurchases.Rows[0]["CurrentBalance"] != null) ? Convert.ToDecimal(dtopenpurchases.Rows[0]["CurrentBalance"]) : 0;

                    if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                    {
                        dtpdate.Value = (DateTime)dtopenpurchases.Rows[0]["PaymentDate"];
                        lblPaymentNo.Text = dtopenpurchases.Rows[0]["PaymentNumber"].ToString();
                        lblPaymentNo.Visible = true;
                        label5.Visible = true;
                    }
                    else
                    {
                        dtpdate.Value = DateTime.Now;
                    }

                    foreach (DataRow dtRow in dtopenpurchases.Rows)
                    {
                        int newrowindex = dgridPayBills.Rows.Add();

                        //the ff. fields are hidden only used to pull back the data when creating a negative invoice for discounts
                        dgridPayBills["PurchaseTermsReferenceID", newrowindex].Value = dtRow["TermsReferenceID"];
                        dgridPayBills["PurchaseShippingContactID", newrowindex].Value = dtRow["ShippingContactID"];
                        dgridPayBills["PurchaseShippingMethodID", newrowindex].Value = dtRow["ShippingMethodID"];
                        dgridPayBills["PurchaseMemo", newrowindex].Value = dtRow["Memo"];
                        dgridPayBills["PurchaseSupplierInvNumber", newrowindex].Value = dtRow["SupplierInvNumber"];
                        dgridPayBills["PurchaseReference", newrowindex].Value = dtRow["PurchaseReference"];

                        dgridPayBills["PurchaseID", newrowindex].Value = dtRow["PurchaseID"];
                        dgridPayBills["AccountID", newrowindex].Value = glAccountCode;
                        dgridPayBills["PurchaseNo", newrowindex].Value = dtRow["PurchaseNumber"];
                        dgridPayBills["Status", newrowindex].Value = dtRow["POStatus"];
                        dgridPayBills["Date", newrowindex].Value = dtRow["TransactionDate"].ToString();
                        decimal totalpaid = (dtRow["TotalPaid"] != null) ? Convert.ToDecimal(dtRow["TotalPaid"]) : 0;

                        if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                        {
                            decimal lAmountApplied = (dtRow["AmountApplied"] != null) ? Convert.ToDecimal(dtRow["AmountApplied"]) : 0;
                            dgridPayBills["AmountApplied", newrowindex].Value = lAmountApplied;
                        }

                        //if (totalpaid > 0 && InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                        //{
                        //    dgridPayBills["AmountApplied", newrowindex].Value = totalpaid;
                        //}

                        decimal grandtotal = Convert.ToDecimal(dtRow["GrandTotal"]);
                        decimal totalowed = grandtotal - totalpaid;
                        dgridPayBills["TotalSettled", newrowindex].Value = totalpaid;

                        if (InvokeSrc == CommonClass.InvocationSource.REGISTER
                            || InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                        {
                            dgridPayBills["Amt", newrowindex].Value = grandtotal;
                            dgridPayBills["TotalOwed", newrowindex].Value = totalowed;
                        }

                        if (totalowed != 0)
                            isTherePaymentNotYetFull = true;
                    }
                }
                int paymentid = 0;
                if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER)
                {
                    paymentid = Convert.ToInt32(TranNo);
                }

                else if (InvokeSrc == CommonClass.InvocationSource.REGISTER)
                {
                    string pmtidsql = "SELECT PaymentID FROM PaymentLines WHERE EntityID = " + Convert.ToInt64(TranNo);
                    SqlCommand cmdchk = new SqlCommand(pmtidsql, con);
                    con.Open();
                    object ret = cmdchk.ExecuteScalar();
                    paymentid = ret != null ? Convert.ToInt32(ret) : 0;
                }

                if (InvokeSrc == CommonClass.InvocationSource.SUPPLIER
                    || InvokeSrc == CommonClass.InvocationSource.REGISTER)
                {

                    if (mPaymentID != 0)
                    {
                        //if (isTherePaymentNotYetFull)
                        //    btnRecord.Enabled = true;
                        //else
                            btnRecord.Enabled = false;
                            btnDelete.Text = "Delete";
                            this.btnDelete.Enabled = CanDelete;
                            this.dgridPayBills.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
                    }
                    else
                    {
                        btnRecord.Enabled = true;
                        btnDelete.Enabled = false;
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

        void DeletePaymentRecord(string pOldPaymentNo, int pOldPaymentID = 0)
        {
            if (pOldPaymentNo == "" && pOldPaymentID == 0)
            {
                MessageBox.Show("Transaction cannot be deleted, Invalid Payment Number");
            }
            else
            {
                DateTime time = DateTime.Now; 
                DateTime dtpfromutc = dtpdate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                //DELETE PAYMENT RECORD
                PaymentCommon.PaymentReturn pr = PaymentCommon.RemovePaymentRecord(trandate, pOldPaymentID, "BP");
                if ( pr.mPaymentID > 0)
                {
                    //DELETE JOURNAL ENTRIES OF OLD
                    if (TransactionClass.DeleteJournalEntries(pOldPaymentNo) > 0)
                    {
                        //UPDATE ACCOUNT BALANCES
                        //TransactionClass.CreateCurrentEarningsTran(pOldPaymentNo);
                        TransactionClass.UpdateProfileBalances(lblProfileID.Text, pr.mTotalAmount);
                        //if (TransactionClass.UpdateAccountBalances(pOldPaymentNo))
                        //{
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Deleted Payment Transaction Number " + pOldPaymentNo, pOldPaymentNo);
                            MessageBox.Show("Deleted Payment Transaction successfully.", "Payment entry Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        //}
                    }
                }
            }
        }

        //private void IssuingAccount_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        CommonClass.getAccount(IssuingAccount.Text);
        //        List<string> list = new List<string>();
        //        list =CommonClass.GetAcc;
        //        if (list.Count == 0)
        //        {
        //            MessageBox.Show("Account Number does not exist");
        //        }
        //        else
        //        {
        //            string strcuracctbal = list[2].ToString();
        //            decimal lbal = strcuracctbal != "" ? Convert.ToDecimal(list[2]) : 0;
        //            CurrentAccountBalance.Text = (Math.Round(lbal, 2).ToString("C"));
        //            GenerateChequeNumber(list[3]);
        //        }
        //    }           
        //}

        private void dgridPayBills_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgridPayBills_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand("SELECT PaymentNumber FROM Payment WHERE PaymentID = " + mPaymentID, con);
                con.Open();
                string paymentno = cmd.ExecuteScalar().ToString();

                if (btnDelete.Text == "Delete")
                {
                    DeletePaymentRecord(paymentno, mPaymentID);
                }
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

        private string GenerateDiscountBillNum()
        {
            string invoicenum = "";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string sql = "SELECT PurchaseBillSeries, PurchaseBillPrefix FROM TransactionSeries";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                string lSeries = "";
                int lCnt = 0;
                int lNewSeries = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lSeries = (reader["PurchaseBillSeries"].ToString());
                            lCnt = lSeries.Length;
                            lSeries = lSeries.TrimStart('0');
                            lSeries = (lSeries == "" ? "0" : lSeries);
                            lNewSeries = Convert.ToInt16(lSeries) + 1;
                            CurBillSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                            invoicenum = (reader["PurchaseBillPrefix"].ToString()).Trim(' ') + CurBillSeries;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transaction Series Numbers not setup properly.");
                        this.BeginInvoke(new MethodInvoker(Close));
                    }
                }
                return invoicenum;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return invoicenum;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private bool CreateDiscountJournalEntries(int pID, string pBillNum, decimal pDiscountApplied)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                DataTable ltb = Purchase.EnterPurchase.GetPurchaseLine(pID.ToString());
                if (ltb.Rows.Count > 0)
                {
                    decimal lGrandTotal = Convert.ToDecimal(ltb.Rows[0]["GrandTotal"].ToString());
                    string lIssuingID = glAccountCode;
                    string lPurchaseNum = pBillNum;
                    string lMemo = ltb.Rows[0]["Memo"].ToString();
                    string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");

                    //INSERT JOURNAL FOR Total Amount Received
                    if (pDiscountApplied < 0)
                    {
                        //NEGATIVE SO CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type) " +
                              "VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lIssuingID + ", " +
                              (pDiscountApplied * -1).ToString() + ",'" + lPurchaseNum + "', 'PB')";
                    }
                    else
                    {
                        //DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type) " +
                              "VALUES('" + lTranDate + "','" + lMemo + "','" + lMemo + "'," + lIssuingID + ", " +
                              pDiscountApplied + ",'" + lPurchaseNum + "','PB')";
                    }
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        decimal lAccountID = Convert.ToDecimal(ltb.Rows[i]["EntityID"].ToString());
                        decimal lTaxEx = pDiscountApplied;
                        decimal lTaxInc = pDiscountApplied;
                        decimal lTaxAmt = lTaxInc - lTaxEx;
                        string lTaxPaidAccountID = ltb.Rows[i]["TaxPaidAccountID"].ToString();
                        string lJobID = "0";
                        if (ltb.Rows[i]["JobID"] != null)
                            lJobID = (ltb.Rows[i]["JobID"].ToString() == "" ? "0" : ltb.Rows[i]["JobID"].ToString());

                        if (lTaxEx < 0) // NEGATIVE SO DEBIT AMOUNT 
                        {
                            // NEGATIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID) VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lAccountID + ", " + (lTaxEx * -1) + ", '" + lPurchaseNum + "', 'PB', " + lJobID + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0)
                            {
                                lTaxAmt = lTaxEx - lTaxInc;
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID)";
                                sql += " VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lTaxPaidAccountID + ", " + lTaxAmt.ToString() + ", '" + lPurchaseNum + "', 'PB', " + lJobID + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else //POSITIVE SO CREDIT AMOUNT 
                        {

                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID) VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lAccountID + ", " + lTaxEx + ", '" + lPurchaseNum + "', 'PB', " + lJobID + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0 && lTaxPaidAccountID != "")
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID) VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lTaxPaidAccountID + ", " + lTaxAmt.ToString() + ", '" + lPurchaseNum + "', 'PB', " + lJobID + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction. No Purchase Lines found.");
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

        void CreateDiscountPurchaseBill(PaymentCommon.DiscountFields pDiscountParams)
        {
            SqlConnection con_ = null;
            try
            {
                string billno = GenerateDiscountBillNum();
                con_ = new SqlConnection(CommonClass.ConStr);

                string savesql = @"INSERT INTO Purchases (
                                        PurchaseType,
                                        SupplierID,
                                        UserID,
                                        PurchaseNumber,
                                        TransactionDate,
                                        PromiseDate,
                                        SubTotal,
                                        TaxTotal,
                                        Memo,
                                        LayoutType,
                                        GrandTotal,
                                        POStatus,
                                        Comments, 
                                        IsTaxInclusive, 
                                        TotalPaid,
                                        TotalDue,
                                        TermsReferenceID,
                                        ShippingContactID,
                                        ShippingMethodID,
                                        ClosedDate) 
                                   VALUES
                                       ( 'BILL',
                                        @SupplierID,
                                        @UserID,
                                        @PurchaseNumber,
                                        @TransactionDate,
                                        @PromiseDate,
                                        @SubTotal,
                                        @TaxTotal,
                                        @Memo, 
                                        'Service',
                                        @GrandTotal,
                                        'Closed',
                                        'Negative Bill for discount',
                                        'Y',
                                        @TotalPaid,
                                        @TotalDue,
                                        @TermsReferenceID,
                                        @ShippingContactID,
                                        @ShippingMethodID,
                                        getutcdate() ); 
                                    SELECT SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(savesql, con_);
                cmd.CommandType = CommandType.Text;
                //Sales Data
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = dtpdate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                cmd.Parameters.AddWithValue("@SupplierID", lblProfileID.Text);
                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@PurchaseNumber", billno);
                cmd.Parameters.AddWithValue("@TransactionDate", trandate);
                cmd.Parameters.AddWithValue("@PromiseDate", trandate);
                cmd.Parameters.AddWithValue("@SubTotal", pDiscountParams.Discount * -1);
                cmd.Parameters.AddWithValue("@TaxTotal", 0);
                cmd.Parameters.AddWithValue("@TotalPaid", pDiscountParams.Discount * -1);
                cmd.Parameters.AddWithValue("@TotalDue", 0);
                cmd.Parameters.AddWithValue("@Memo", "Discount - " + pDiscountParams.Memo);
                cmd.Parameters.AddWithValue("@GrandTotal", pDiscountParams.Discount * -1);
                cmd.Parameters.AddWithValue("@TermsReferenceID", pDiscountParams.TermsReferenceID);
                cmd.Parameters.AddWithValue("@ShippingContactID", pDiscountParams.ShippingContactID);
                cmd.Parameters.AddWithValue("@ShippingMethodID", pDiscountParams.ShippingMethodID);

                con_.Open();
                int NewPurchaseID = Convert.ToInt32(cmd.ExecuteScalar());

                //SalesLines
                string salesLinesql = @"INSERT INTO PurchaseLines (
                                                PurchaseID,
                                                Description,
                                                TotalAmount,
                                                TransactionDate,
                                                TaxCode,
                                                UnitPrice, 
                                                ActualUnitPrice, 
                                                OrderQty,
                                                ReceiveQty,
                                                EntityID, 
                                                SubTotal,
                                                TaxAmount,
                                                TaxPaidAccountID,
                                                JobID ) 
                                         VALUES ( @PurchaseID,
                                                  @Description,
                                                  @TotalAmount,
                                                  @TransactionDate,
                                                  @TaxCode,
                                                  @UnitPrice,
                                                  @ActualUnitPrice,
                                                  @OrderQty,
                                                  @ReceiveQty,
                                                  @EntityID,
                                                  @SubTotal,
                                                  @TaxAmount,
                                                  @TaxPaidAccountID,
                                                  @JobID )";

                cmd.CommandText = salesLinesql;
                cmd.Parameters.AddWithValue("@PurchaseID", NewPurchaseID);
                cmd.Parameters.AddWithValue("@Description", "Discount - " + Memo.Text);
                cmd.Parameters.AddWithValue("@TotalAmount", pDiscountParams.Discount * -1);
                cmd.Parameters.AddWithValue("@TaxCode", "N-T");
                cmd.Parameters.AddWithValue("@UnitPrice", pDiscountParams.Discount * -1);
                cmd.Parameters.AddWithValue("@ActualUnitPrice", pDiscountParams.Discount * -1);
                cmd.Parameters.AddWithValue("@OrderQty", 1);
                cmd.Parameters.AddWithValue("@ReceiveQty", 1);
                cmd.Parameters.AddWithValue("@EntityID", glAccountCode);
                cmd.Parameters.AddWithValue("@TaxAmount", 0);
                cmd.Parameters.AddWithValue("@TaxPaidAccountID", 0);
                cmd.Parameters.AddWithValue("@JobID", 0);

                int rowcount = cmd.ExecuteNonQuery();

                if (rowcount > 0)
                {
                    cmd.CommandText = "UPDATE TransactionSeries SET PurchaseBillSeries = '" + CurBillSeries + "'";
                    cmd.ExecuteNonQuery();

                    CreateDiscountJournalEntries(NewPurchaseID, billno, pDiscountParams.Discount * -1);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 
                string sql = @"SELECT CONCAT(c1.Street, ', ', c1.City, ', ', c1.State) AS ShippingAddress,
                                pay.TransactionDate, pay.Memo, pay.PaymentNumber, pay.TotalAmount, pay.PaymentFor,
                                p.SupplierINVNumber, p.PurchaseNumber, p.TransactionDate, p.GrandTotal,
                                l.DiscountPercent
                                FROM Purchases p 
                                INNER JOIN Profile pl ON p.SupplierID = pl.ID
                                INNER JOIN PurchaseLines l ON p.PurchaseID = l.PurchaseID 
                                INNER JOIN PaymentLines payline ON payline.EntityID = p.PurchaseID
                                INNER JOIN Payment pay ON pay.PaymentID = payline.PaymentID
                                LEFT JOIN Contacts c1 ON c1.ContactID = p.ShippingContactID
                                WHERE p.PurchaseID = (SELECT EntityID FROM PaymentLines WHERE PaymentID = " + TranNo + @")
                                ORDER BY pay.TransactionDate ASC";
                                
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dtremittance = new DataTable();
                da.Fill(dtremittance);
                DataTable fdtremittance = dtremittance.Clone();
                fdtremittance.Columns.Add("PreviousAmt");
                fdtremittance.Columns.Add("CurrentAmt");
                fdtremittance.Columns.Add("PaymentAmt");
                fdtremittance.Columns["PreviousAmt"].DataType = typeof(decimal);
                fdtremittance.Columns["CurrentAmt"].DataType = typeof(decimal);
                fdtremittance.Columns["PaymentAmt"].DataType = typeof(decimal);
                fdtremittance.ImportRow(dtremittance.Rows[0]);
                decimal lPmtAmt = 0;
                foreach(DataRow dr in dtremittance.Rows)
                {
                    lPmtAmt += Convert.ToDecimal(dr["TotalAmount"]);
                }
                fdtremittance.Rows[0]["PaymentAmt"] = lPmtAmt;
                int lastindex = dtremittance.Rows.Count - 1;
                fdtremittance.Rows[0]["CurrentAmt"] = dtremittance.Rows[lastindex]["TotalAmount"];
                if (lastindex > 0)
                    fdtremittance.Rows[0]["PreviousAmt"] = dtremittance.Rows[lastindex - 1]["TotalAmount"];
                else
                    fdtremittance.Rows[0]["PreviousAmt"] = 0;

                Reports.ReportParams remittanceadvice = new Reports.ReportParams();
                remittanceadvice.PrtOpt = 1;
                remittanceadvice.Rec.Add(fdtremittance);
                remittanceadvice.ReportName = "RemitanceAdvicePayBills.rpt";
                remittanceadvice.RptTitle = "Remitance Advice Pay Bills";

                remittanceadvice.Params = "compname|CompAddress";
                remittanceadvice.PVals = CommonClass.CompName.Trim()+"|"+CommonClass.CompAddress.Trim();

                CommonClass.ShowReport(remittanceadvice);
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
    } //END
}
