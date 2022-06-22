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


namespace AbleRetailPOS
{
    public partial class RptJobProfitAndLoss : Form
    {
        private DataTable TbRep;
        private bool CanView = false;
        public RptJobProfitAndLoss()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
            }
        }

        private void RptJobProfitAndLoss_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            DateTime lMinDate = DateTime.Now.AddMonths(-12);
            DateTime lMaxDate = DateTime.Now.AddMonths(24).AddDays(-1);

            this.dtpStartDate.MinDate = lMinDate;
            this.dtpStartDate.MaxDate = lMaxDate;
            this.dtpEndDate.MinDate = lMinDate;
            this.dtpEndDate.MaxDate = lMaxDate;
            this.lblNote.Text = "*NOTE: Allowed Date Range is from " + lMinDate.ToString("d MMM, yyyy");
            this.lblNote2.Text = " to " + lMaxDate.ToString("d MMM, yyyy");

            LoadJobs();


        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {        
            LoadReport();           
        }
        private void LoadJobs()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string sql = "SELECT CAST('true' AS bit) AS Include,JobCode,JobName,JobID from Jobs order by JobCode";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                this.dgJobs.DataSource = dt;
                this.dgJobs.Columns[3].Visible = false;
                this.dgJobs.Columns[1].Frozen = true;
                this.dgJobs.Columns[1].HeaderText = "Job Code";
                this.dgJobs.Columns[2].Frozen = true;
                this.dgJobs.Columns[2].HeaderText = "Name";
                this.dgJobs.Columns[3].Frozen = true;
                this.dgJobs.Columns[3].HeaderText = "ID";

                con.Close();
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
        private string GetIncludedJobs()
        {
            string retJobs = "";
            foreach (DataGridViewRow dvr in this.dgJobs.Rows)
            {
                if (dvr.Cells["JobID"].Value.ToString() != "")
                {
                    if ((bool)dvr.Cells["Include"].Value)
                    {
                        retJobs += (retJobs != "" ? "," + dvr.Cells["JobID"].Value.ToString() :  dvr.Cells["JobID"].Value.ToString() );
                    }
                }
            }
            return retJobs;
        }
        private void LoadReport()
        {           
            try
            {
               

                DateTime dtpfromutc = Convert.ToDateTime(dtpStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime dtptoutc = Convert.ToDateTime(dtpEndDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();


                DateTime lSDate = dtpfromutc;
                //DateTime lEDate = dtptoutc.AddDays(1);
                
                int lFY = CommonClass.CurFY;
                 
                string lStartDateStr = lSDate.ToString("yyyy-MM-dd HH:mm:ss");
                string lEndDateStr = dtptoutc.ToString("yyyy-MM-dd HH:mm:ss");
                string lJobs = GetIncludedJobs();
                if (lJobs != "")
                {
                    TbRep = GetJobs(lJobs);
                    DataTable lTbIncome = GetIncome(lStartDateStr, lEndDateStr, lJobs);
                    DataTable lTbOtherIncome = GetOtherIncome(lStartDateStr, lEndDateStr, lJobs);
                    DataTable lTbCOS = GetCOS(lStartDateStr, lEndDateStr, lJobs);
                    DataTable lTbExpenses = GetExpenses(lStartDateStr, lEndDateStr, lJobs);
                    DataTable lTbOtherExpenses = GetOtherExpenses(lStartDateStr, lEndDateStr, lJobs);

                    Reports.ReportParams acctlistparams = new Reports.ReportParams();
                    acctlistparams.PrtOpt = 1;
                    acctlistparams.ReportName = "JobProfitLoss.rpt";
                    acctlistparams.Rec.Add(TbRep);
                    acctlistparams.RptTitle = "Job Profit And Loss Summary";
                    acctlistparams.Params = "compname|StartDate|EndDate|CurSymbol";
                    acctlistparams.PVals = CommonClass.CompName.Trim() + "|" + this.dtpStartDate.Value + "|" + this.dtpEndDate.Value + "|" + System.Globalization.RegionInfo.CurrentRegion.CurrencySymbol;
                    acctlistparams.children.Add("subIncome", lTbIncome);
                    acctlistparams.children.Add("subCOS", lTbCOS);
                    acctlistparams.children.Add("subExpenses", lTbExpenses);
                    acctlistparams.children.Add("subOtherIncome", lTbOtherIncome);
                    acctlistparams.children.Add("subOtherExpenses", lTbOtherExpenses);

                    CommonClass.ShowReport(acctlistparams);
                }
                else
                {
                    MessageBox.Show("Report contains no data");
                }          
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }
        private static DataTable GetJobs(string pJobs)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string sql = "SELECT *, CONVERT(varchar(10), JobID) as Job from Jobs where JobID in (" + pJobs + ")";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private static DataTable GetIncome( string pStartDate, string pEndDate, string pJobs)
        {
            SqlConnection con = null;
            DataTable lTb;
            try
            {
                string sql = "";
            
                //sql = @"SELECT a.AccountID, a.ParentAccountID, a.AccountNumber, a.AccountName, a.AccountClassificationID,
                //    a.AccountLevel,  j.JobID, ISNULL(j.NetActivity, 0) AS CurrentBalance, a.IsHeader
                //    FROM Accounts a INNER JOIN 
                //    (SELECT AccountID, JobID, ISNULL(SUM(CreditAmount), 0) - ISNULL(SUM(DebitAmount), 0) AS NetActivity FROM Journal 
                //    WHERE JobID in (" + pJobs + ") and TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY AccountID, JobID) AS j ON a.AccountID = j.AccountID WHERE a.AccountClassificationID = 'I' ORDER BY a.AccountNumber";

                sql = @"SELECT j.AccountID, j.JobID, ISNULL(SUM(CreditAmount), 0) - ISNULL(SUM(DebitAmount), 0) AS CurrentBalance,
                    a.AccountNumber, a.AccountName, a.AccountClassificationID, a.IsHeader, CONVERT(varchar(10), j.JobID) as Job FROM Journal as j
                    INNER JOIN Accounts as a on j.AccountID = a.AccountID WHERE j.JobID in (" + pJobs + ")  and j.TransactionDate BETWEEN'" + pStartDate + "' AND '" + pEndDate + "'" +
                    " AND a.AccountClassificationID = 'I' GROUP BY j.AccountID, j.JobID, a.AccountNumber, a.AccountName, a.AccountClassificationID, a.IsHeader ORDER BY a.AccountNumber";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                lTb = new DataTable();
                da.Fill(lTb);            
             
                Decimal lCurBal = 0;
                foreach(DataRow rw in lTb.Rows)
                {
                    if(rw["IsHeader"].ToString() == "D")
                    {
                        lCurBal = Convert.ToDecimal(rw["CurrentBalance"].ToString());

                        rw["CurrentBalance"] = lCurBal;                        
                    }
                    else
                    {
                        rw["CurrentBalance"] = 0;
                    }
                }
                return lTb;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private static DataTable GetOtherIncome(string pStartDate, string pEndDate, string pJobs)
        {
            SqlConnection con = null;
            DataTable lTb;
            try
            {
                string sql = "";        
              
                sql = @"SELECT a.AccountID, a.ParentAccountID, a.AccountNumber, a.AccountName, a.AccountClassificationID,
                    a.AccountLevel, j.JobID, ISNULL(j.NetActivity, 0) AS CurrentBalance, a.IsHeader, CONVERT(varchar(10), j.JobID) as Job
                    FROM Accounts a LEFT JOIN 
                    (SELECT AccountID,  JobID, ISNULL(SUM(CreditAmount), 0) - ISNULL(SUM(DebitAmount), 0) AS NetActivity FROM Journal 
                    WHERE TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY AccountID,JobID) AS j ON a.AccountID = j.AccountID WHERE a.AccountClassificationID = 'OI' ORDER BY a.AccountNumber";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                lTb = new DataTable();
                da.Fill(lTb);

                Decimal lCurBal = 0;
                foreach (DataRow rw in lTb.Rows)
                {
                    if (rw["IsHeader"].ToString() == "D")
                    {
                        lCurBal = Convert.ToDecimal(rw["CurrentBalance"].ToString());
                        rw["CurrentBalance"] = lCurBal;
                    }
                    else
                    {
                        rw["CurrentBalance"] = 0;
                    }
                }
                return lTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private static DataTable GetCOS(string pStartDate, string pEndDate, string pJobs)
        {
            SqlConnection con = null;
            DataTable lTb;
            try
            {
                string sql = "";
              
                sql = @"SELECT a.AccountID, a.ParentAccountID, a.AccountNumber, a.AccountName, a.AccountClassificationID,
                    a.AccountLevel, j.JobID, ISNULL(j.NetActivity, 0) AS CurrentBalance, a.IsHeader, CONVERT(varchar(10), j.JobID) as Job
                    FROM Accounts a LEFT JOIN 
                    (SELECT AccountID, JobID, ISNULL(SUM(DebitAmount), 0) - ISNULL(SUM(CreditAmount), 0) AS NetActivity FROM Journal 
                    WHERE TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY AccountID ,JobID) AS j ON a.AccountID = j.AccountID WHERE a.AccountClassificationID = 'COS'  ORDER BY a.AccountNumber";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                lTb = new DataTable();
                da.Fill(lTb);
                Decimal lCurBal = 0;
                foreach (DataRow rw in lTb.Rows)
                {
                    if (rw["IsHeader"].ToString() == "D")
                    {
                        lCurBal = Convert.ToDecimal(rw["CurrentBalance"].ToString());
                        rw["CurrentBalance"] = lCurBal;
                    }
                    else
                    {
                        rw["CurrentBalance"] = 0;
                    }
                }
                return lTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private static DataTable GetExpenses(string pStartDate, string pEndDate, string pJobs)
        {
            SqlConnection con = null;
            DataTable lTb;
            try
            {
                string sql = "";
               
                sql = @"SELECT a.AccountID, a.ParentAccountID, a.AccountNumber, a.AccountName, a.AccountClassificationID,
                    a.AccountLevel,  j.JobID, ISNULL(j.NetActivity, 0) AS CurrentBalance, a.IsHeader, CONVERT(varchar(10), j.JobID) as Job
                    FROM Accounts a LEFT JOIN 
                    (SELECT AccountID,  JobID,ISNULL(SUM(DebitAmount), 0) - ISNULL(SUM(CreditAmount), 0) AS NetActivity FROM Journal 
                    WHERE TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY AccountID, JobID) AS j ON a.AccountID = j.AccountID WHERE a.AccountClassificationID = 'EXP' ORDER BY a.AccountNumber";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                lTb = new DataTable();
                da.Fill(lTb);
               
                Decimal lCurBal = 0;
                foreach (DataRow rw in lTb.Rows)
                {
                    if (rw["IsHeader"].ToString() == "D")
                    {
                        lCurBal = Convert.ToDecimal(rw["CurrentBalance"].ToString());
                        rw["CurrentBalance"] = lCurBal;
                    }
                    else
                    {
                        rw["CurrentBalance"] = 0;
                    }
                }
                return lTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private static DataTable GetOtherExpenses(string pStartDate, string pEndDate, string pJobs)
        {
            SqlConnection con = null;
            DataTable lTb;
            try
            {
                string sql = "";
                
                sql = @"SELECT a.AccountID, a.ParentAccountID, a.AccountNumber, a.AccountName, a.AccountClassificationID,
                    a.AccountLevel,  j.JobID,ISNULL(j.NetActivity, 0) AS CurrentBalance, a.IsHeader, CONVERT(varchar(10), j.JobID) as Job
                    FROM Accounts a LEFT JOIN 
                    (SELECT AccountID,  JobID,ISNULL(SUM(DebitAmount), 0) - ISNULL(SUM(CreditAmount), 0) AS NetActivity FROM Journal 
                    WHERE TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY AccountID,JobID) AS j ON a.AccountID = j.AccountID WHERE a.AccountClassificationID = 'OE' ORDER BY a.AccountNumber";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                lTb = new DataTable();
                da.Fill(lTb);

                Decimal lCurBal = 0;
                foreach (DataRow rw in lTb.Rows)
                {
                    if (rw["IsHeader"].ToString() == "D")
                    {
                        lCurBal = Convert.ToDecimal(rw["CurrentBalance"].ToString());
                        rw["CurrentBalance"] = lCurBal;
                    }
                    else
                    {
                        rw["CurrentBalance"] = 0;
                    }
                }
                return lTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private static decimal getHBalL0_PL(string pType, string pID, string pStartDate, string pEndDate,string pJobs )
        {
            decimal lParentBal = 0;
            string sql5 = @"SELECT a.AccountID, j.JobID, a.IsHeader,  ISNULL(j.Debit, 0) AS Debit, ISNULL(j.Credit, 0) AS Credit FROM Accounts a INNER JOIN (
                SELECT j.AccountID, j.JobID, ISNULL(SUM(DebitAmount), 0) AS Debit, ISNULL(SUM(CreditAmount), 0) AS Credit
                  FROM Journal j INNER JOIN Accounts a ON j.AccountID = a.AccountID
                WHERE j.JobID in (" + pJobs + ") and a.AccountClassificationID = '" + pType + "' AND TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY j.AccountID, j.JobID,) AS j ON a.AccountID = j.AccountID WHERE a.AccountLevel = 1 AND a.ParentAccountID = " + pID + " GROUP BY a.AccountID, j.JobID";

            SqlConnection connection5 = null;
            try
            {
                connection5 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd5 = new SqlCommand(sql5, connection5);
                connection5.Open();
                SqlDataAdapter da1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();

                da1.SelectCommand = cmd5;
                da1.Fill(dt1);

                decimal lDebit = 0;
                decimal lCredit = 0;
                decimal lCurBal = 0;

                foreach (DataRow rw in dt1.Rows)
                {
                    if (rw["IsHeader"].ToString() == "H")
                    {
                        lCurBal = getHBalL1_PL( pType, rw["AccountID"].ToString(), pStartDate, pEndDate, pJobs);
                    }
                    else
                    {
                        lDebit = Convert.ToDecimal(rw["Debit"].ToString());
                        lCredit = Convert.ToDecimal(rw["Credit"].ToString());
                        lCurBal = getNet(pType, lDebit, lCredit);
                    }
                    
                    lParentBal += lCurBal;
                }

                return lParentBal;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return lParentBal;
            }
            finally
            {
                if (connection5 != null)
                    connection5.Close();
            }
        }

        private static decimal getHBalL1_PL( string pType, string pID, string pStartDate, string pEndDate, string pJobs)
        {
            decimal lParentBal = 0;
            string sql5 = @"SELECT a.AccountID, j.JobID, a.IsHeader, ISNULL(j.Debit, 0) AS Debit, ISNULL(j.Credit, 0) AS Credit FROM  Accounts a INNER JOIN (
                SELECT j.AccountID, ISNULL(SUM(DebitAmount), 0) AS Debit, ISNULL(SUM(CreditAmount), 0) AS Credit
                FROM Journal j INNER JOIN Accounts a ON j.AccountID = a.AccountID
                WHERE j.JobID in (" + pJobs + ") and a.AccountClassificationID = '" + pType + "' AND TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY j.AccountID ) AS j ON a.AccountID = j.AccountID WHERE a.AccountLevel = 2 AND a.ParentAccountID = " + pID + " GROUP BY  a.AccountID, j.JobID";
            SqlConnection connection5 = null;
            try
            {
                connection5 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd5 = new SqlCommand(sql5, connection5);
                connection5.Open();
                SqlDataAdapter da1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();

                da1.SelectCommand = cmd5;
                da1.Fill(dt1);
                decimal lDebit = 0;
                decimal lCredit = 0;
                decimal lCurBal = 0;

                foreach (DataRow rw in dt1.Rows)
                {
                    if (rw["IsHeader"].ToString() == "H")
                    {
                        lCurBal = getHBalL2_PL( pType, rw["AccountID"].ToString(), pStartDate, pEndDate,pJobs);
                    }
                    else
                    {
                        lDebit = Convert.ToDecimal(dt1.Rows[0]["Debit"].ToString());
                        lCredit = Convert.ToDecimal(dt1.Rows[0]["Credit"].ToString());
                        lCurBal = getNet(pType, lDebit, lCredit);
                    }
                  
                    lParentBal += lCurBal;
                }
                return lParentBal;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return lParentBal;
            }
            finally
            {
                if (connection5 != null)
                    connection5.Close();
            }
        }

        private static decimal getHBalL2_PL(string pType,string pID, string pStartDate, string pEndDate, string pJobs)
        {
            decimal retval = 0;               
            string sql5 = @"SELECT JobID, ISNULL(SUM(Debit), 0) AS Debit, ISNULL(SUM(Credit), 0) AS Credit FROM (SELECT a.AccountID, j.JobID, ISNULL(SUM(DebitAmount), 0) AS Debit, ISNULL(SUM(CreditAmount), 0) AS Credit  
                  FROM Accounts a INNER JOIN Journal j ON a.AccountID = j.AccountID  
                  WHERE j.JobID in (" + pJobs + ") and AccountLevel = 3 AND a.IsHeader = 'D' AND a.ParentAccountID = " + pID + " AND j.TransactionDate BETWEEN '" + pStartDate + "' AND '" + pEndDate + "' GROUP BY a.AccountID, j.JobID ) AS t  group by JobID";
            SqlConnection connection5 = null;
            try
            {
                connection5 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd5 = new SqlCommand(sql5, connection5);
                connection5.Open();

                SqlDataAdapter da1;
                DataTable dt1;
                da1 = new SqlDataAdapter();
                da1.SelectCommand = cmd5;
                dt1 = new DataTable();
                da1.Fill(dt1);
                decimal lDebit = Convert.ToDecimal(dt1.Rows[0]["Debit"].ToString());
                decimal lCredit = Convert.ToDecimal(dt1.Rows[0]["Credit"].ToString());
                retval = getNet(pType, lDebit, lCredit);
              
                return retval;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return retval;
            }
            finally
            {
                if (connection5 != null)
                    connection5.Close();
            }
        }

        private static decimal getNet(string pType,decimal Debit, decimal Credit)
        {
            decimal lNet = 0;
            switch (pType)
            {
                case "A":
                    lNet = Debit - Credit;
                    break;
                case "L":
                    lNet = Credit - Debit;
                    break;
                case "COS":                  
                    lNet = Debit - Credit;
                    break;
                case "I":
                    lNet = Credit - Debit;
                    break;
                case "OI":
                    lNet = Credit - Debit;
                    break;
                case "EXP":                    
                    lNet = Debit - Credit;
                    break;
                case "OE":                    
                    lNet = Debit - Credit;
                    break;
                case "EQ":
                    lNet = Credit - Debit;
                    break;
            }
            return lNet;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
