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

namespace RestaurantPOS.Reports
{
    public partial class RptJobTransactionsOptions : Form
    {
        private bool CanView = false;
        public RptJobTransactionsOptions()
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (dtmTxFrom.Text == ""
                || dtmTxTo.Text == "")
            {
                MessageBox.Show("The From and To date must be set");
                return;
            }

            LoadReport();
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {

                DateTime dtpfromutc = Convert.ToDateTime(dtmTxFrom.Value.ToString("yyyy-MM-dd 00:00:00")).ToUniversalTime();
                DateTime dtptoutc = Convert.ToDateTime(dtmTxTo.Value.ToString("yyyy-MM-dd 23:59:59")).ToUniversalTime();

                string dtfromstr = dtpfromutc.ToString("yyyy-MM-dd HH:mm:ss");
                string dttostr = dtptoutc.ToString("yyyy-MM-dd HH:mm:ss");

                string sql = @"SELECT 
                                AccountID,
                                CreditAmount, 
                               DebitAmount,
                                Memo,
                                Type,
                                TransactionNumber,
                                TransactionDate,
                                0 AS BegBalance,
                                0 AS NetActivity,
                                0 AS EndBalance
                             FROM Journal 
                             WHERE TransactionDate BETWEEN '" + dtfromstr
                             + "' AND '" + dttostr + "'";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);

                CalculateActivity(ref TbRep, dtfromstr);

                Reports.ReportParams jobtxparams = new Reports.ReportParams();
                jobtxparams.PrtOpt = 1;
                jobtxparams.Rec.Add(TbRep);
                jobtxparams.ReportName = "JobTransaction.rpt";
                jobtxparams.RptTitle = "Job Transactions";

                jobtxparams.Params = "compname";
                jobtxparams.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(jobtxparams);
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

        private void CalculateActivity(ref DataTable pTbRep, string pSDate)
        {
            Decimal lOpening = 0;
            Decimal lNet = 0;
            Decimal lDebit = 0;
            Decimal lCredit = 0;
            Decimal lEnding = 0;
            foreach (DataRow dr in pTbRep.Rows)
            {
                string ID = dr["AccountID"].ToString();
                if (ID == "")
                    break;

                string debitstr = (dr["DebitAmount"].ToString() == "") ? "0" : dr["DebitAmount"].ToString();
                string creditstr = (dr["CreditAmount"].ToString() == "") ? "0" : dr["CreditAmount"].ToString();
           //     string disyropenbalance = (dr["ThisYearOpeningBalance"].ToString() == "") ? "0" : dr["ThisYearOpeningBalance"].ToString();
                lDebit = Convert.ToDecimal(debitstr);
                lCredit = Convert.ToDecimal(creditstr);
                lOpening = 0;//TransactionClass.CalculateOpeningBalance(Convert.ToDecimal(disyropenbalance), ID, pSDate);
                dr["BegBalance"] = lOpening;

                lNet = lDebit - lCredit;

                lEnding = lOpening + lNet;
                dr["NetActivity"] = lNet;
                dr["EndBalance"] = lEnding;
            }
        }

        private void RptJobTransactionsOptions_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
