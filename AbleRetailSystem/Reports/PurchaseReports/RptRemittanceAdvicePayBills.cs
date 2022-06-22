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

namespace RestaurantPOS.Reports.PurchaseReports
{
    public partial class RptRemittanceAdvicePayBills : Form
    {
        private DataTable TbBankTran;
        private static decimal CurFYOpening = 0;
        public RptRemittanceAdvicePayBills()
        {
            InitializeComponent();
        }

        private void LoadBankTransactions(string pAcctID = "0")
        {
            SqlConnection con = null;
            try
            {
                DateTime dtpfromutc = dtpfrom.Value;
                DateTime dtptoutc = dtpto.Value;

                string sdate = dtpfromutc.ToUniversalTime().ToString("yyyy-MM-dd") + " 00:00:00";
                string edate = dtptoutc.ToUniversalTime().ToString("yyyy-MM-dd") + " 23:59:59";
                string sql = @"SELECT pmt.PaymentID, 
                                      j.TransactionDate, 
                                      j.TransactionNumber, 
                                      Type, 
                                      '' AS Payee, 
                                      '' AS SplitAccount, 
                                      CreditAmount, 
                                      DebitAmount, 
                                      0 AS AccountBalance, 
                                      j.AccountID, 
                                      AllocationMemo 
                                FROM Journal j 
                                INNER JOIN Payment pmt ON pmt.PaymentNumber = j.TransactionNumber 
                                WHERE j.TransactionDate BETWEEN @sdate AND @edate 
                                AND pmt.PaymentFor = 'Purchase'";

                if (pAcctID != "0")
                    sql += " AND j.AccountID = @accountid ";

                sql += " ORDER BY j.TransactionDate";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@accountid", pAcctID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbBankTran = new DataTable();
                da.Fill(TbBankTran);

                //if (pAcctID != "0")
                //{
                //    decimal sbal = 0;
                //    sbal = CalculateOpeningBalance(pAcctID, sdate);
                //    CalculateLineBalance(pAcctID, sbal, sdate, edate);
                //}

                this.dgRemittanceAdvicePayBills.DataSource = TbBankTran;
                this.dgRemittanceAdvicePayBills.Columns[0].HeaderText = "Payment ID";
                this.dgRemittanceAdvicePayBills.Columns[1].HeaderText = "Transaction Date";
                this.dgRemittanceAdvicePayBills.Columns[2].HeaderText = "Transaction No";
                this.dgRemittanceAdvicePayBills.Columns[3].HeaderText = "Type";
                this.dgRemittanceAdvicePayBills.Columns[4].HeaderText = "Payee";
                this.dgRemittanceAdvicePayBills.Columns[5].HeaderText = "Account";
                this.dgRemittanceAdvicePayBills.Columns[6].HeaderText = "Withdrawal";
                this.dgRemittanceAdvicePayBills.Columns[7].HeaderText = "Deposit";
                this.dgRemittanceAdvicePayBills.Columns[8].HeaderText = "Ending Balance";
                this.dgRemittanceAdvicePayBills.Columns[6].DefaultCellStyle.Format = "C2";
                this.dgRemittanceAdvicePayBills.Columns[7].DefaultCellStyle.Format = "C2";
                this.dgRemittanceAdvicePayBills.Columns[8].DefaultCellStyle.Format = "C2";
                for (int i = 0; i < this.dgRemittanceAdvicePayBills.Rows.Count; i++)
                {
                    if (this.dgRemittanceAdvicePayBills.Rows[i].Cells["TransactionDate"].Value != null)
                    {
                        if (this.dgRemittanceAdvicePayBills.Rows[i].Cells["TransactionDate"].Value.ToString() != "")
                        {
                            this.dgRemittanceAdvicePayBills.Rows[i].Cells["TransactionDate"].Value = Convert.ToDateTime(this.dgRemittanceAdvicePayBills.Rows[i].Cells["TransactionDate"].Value.ToString()).ToShortDateString();
                        }
                    }
                }
                this.dgRemittanceAdvicePayBills.Columns[8].Visible = false;
                this.dgRemittanceAdvicePayBills.Columns[9].Visible = false;
                if (dgRemittanceAdvicePayBills.Rows.Count > 0)
                {
                    this.btnDisplay.Enabled = true;
                }
                else
                {
                    this.btnDisplay.Enabled = false;
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
        //private static decimal CalculateOpeningBalance(string pAccountID, string sdate)
        //{
        //    SqlConnection con = null;
        //    try
        //    {
        //        string sql = @"SELECT AccountID, 
        //                              SUM(DebitAmount) AS Debit, 
        //                              SUM(CreditAmount) AS Credit 
        //                        FROM Journal WHERE TransactionDate Between @sdate AND @edate
        //                        AND AccountId = @accountid
        //                        GROUP BY AccountID";
        //        con = new SqlConnection(CommonClass.ConStr);
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.Parameters.AddWithValue("@sdate", CommonClass.FYBegDateStr);
        //        cmd.Parameters.AddWithValue("@edate", sdate);
        //        cmd.Parameters.AddWithValue("@accountid", pAccountID);

        //        SqlDataAdapter da = new SqlDataAdapter();

        //        da.SelectCommand = cmd;
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        decimal lBal = CurFYOpening;
        //        decimal lDebit = 0;
        //        decimal lCredit = 0;

        //        if (dt.Rows.Count > 0)
        //        {
        //            lDebit = Convert.ToDecimal((dt.Rows[0]["Debit"].ToString() != "" ? dt.Rows[0]["Debit"].ToString() : "0"));
        //            lCredit = Convert.ToDecimal((dt.Rows[0]["Credit"].ToString() != "" ? dt.Rows[0]["Credit"].ToString() : "0"));
        //            lBal = lBal + lDebit - lCredit;
        //        }

        //        return lBal;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return 0;
        //    }
        //    finally
        //    {
        //        if (con != null)
        //            con.Close();
        //    }
        //}

        //private void CalculateLineBalance(string pAccountID, decimal pOpeningBal, string sdate, string edate)
        //{
        //    try
        //    {
        //        string lType = "";
        //        decimal lPrevBalance = pOpeningBal;
        //        decimal lTranAmt = 0;
        //        decimal lLineBalance = pOpeningBal;
        //        string lTranNo = "";
        //        string lTranType = "";
        //        string[] lRet;
        //        for (int i = 0; i < TbBankTran.Rows.Count; ++i)
        //        {
        //            lType = (TbBankTran.Rows[i]["CreditAmount"].ToString() != "" ? "W" : "D");
        //            lTranNo = TbBankTran.Rows[i]["TransactionNumber"].ToString();
        //            lTranType = TbBankTran.Rows[i]["Type"].ToString().Trim();
        //            switch (lTranType)
        //            {
        //                case "BP":
        //                    lRet = GetPaymentTran(pAccountID, lTranNo);
        //                    if (lRet != null)
        //                    {
        //                        TbBankTran.Rows[i]["SplitAccount"] = lRet[0];
        //                        TbBankTran.Rows[i]["Payee"] = lRet[1];
        //                    }
        //                    break;
        //                case "BD":
        //                    break;
        //            }

        //            if (lType == "W")
        //            {
        //                string strcredamt = TbBankTran.Rows[i]["CreditAmount"].ToString();
        //                lTranAmt = strcredamt != "" ? Convert.ToDecimal(strcredamt) : 0;
        //                lLineBalance -= lTranAmt;
        //            }
        //            else
        //            {
        //                string strdebamt = TbBankTran.Rows[i]["DebitAmount"].ToString();
        //                lTranAmt = strdebamt != "" ? Convert.ToDecimal(strdebamt) : 0;
        //                lLineBalance += lTranAmt;
        //            }
        //            TbBankTran.Rows[i]["AccountBalance"] = lLineBalance;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private static string[] GetPaymentTran(string pAccountID, string pTranNo)
        //{
        //    SqlConnection con = null;
        //    try
        //    {
        //        string[] ret = new string[2];
        //        con = new SqlConnection(CommonClass.ConStr);

        //        string sql = @"SELECT pmt.AccountID, 
        //                        a.AccountNumber, 
        //                        a.AccountName, 
        //                        p.Name 
        //                    FROM Payment pmt
        //                    INNER JOIN Accounts a ON pmt.AccountID = a.AccountID
        //                    INNER JOIN Profile p ON pmt.ProfileID = p.ID 
        //                    WHERE pmt.PaymentNumber = @tranno
        //                    AND pmt.AccountID <> " + pAccountID;

        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.Parameters.AddWithValue("@tranno", pTranNo);

        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = cmd;
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count == 1)
        //        {
        //            ret[0] = dt.Rows[0]["AccountNumber"].ToString();
        //            ret[1] = dt.Rows[0]["Name"].ToString();
        //        }
        //        else if (dt.Rows.Count > 0)
        //        {
        //            ret[0] = "Multiple Accounts";
        //            ret[1] = dt.Rows[0]["Name"].ToString();
        //        }
        //        else
        //        {
        //            ret[0] = "";
        //            ret[1] = "";
        //        }

        //        return ret;
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (con != null)
        //            con.Close();
        //    }
        //}

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadBankTransactions(this.lblID.Text.Trim());
        }

        private void dgRemittanceAdvicePayBills_DoubleClick(object sender, EventArgs e)
        {
            ShowTran();
        }

        private void ShowTran()
        {
            string ltrantype = "";
            string ltranNo = "";
            string Pid = "";
            Pid = dgRemittanceAdvicePayBills.CurrentRow.Cells[0].Value.ToString();
            ltrantype = dgRemittanceAdvicePayBills.CurrentRow.Cells[3].Value.ToString();
            switch (ltrantype.Trim())
            {
                case "BP":
                    LoadPayBills(Pid);
                    break;
                case "BD":
                    string val = this.dgRemittanceAdvicePayBills.CurrentRow.Cells[0].Value.ToString();
                    LoadPayBills(val);
                    break;
            }
        }

        private void LoadPayBills(string Pid)
        {
            try
            {
                CommonClass.PRPaymentsfrm = new PurchasePayments(CommonClass.InvocationSource.SUPPLIER, Pid);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PRPaymentsfrm.MdiParent = this.MdiParent;
                CommonClass.PRPaymentsfrm.Show();
                CommonClass.PRPaymentsfrm.Focus();
                if (CommonClass.PRPaymentsfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PRPaymentsfrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RptRemittanceAdvicePayBills_Load(object sender, EventArgs e)
        {
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbAccount_Click(object sender, EventArgs e)
        {

        }
    }
}
