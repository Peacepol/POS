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

namespace AbleRetailPOS.Reports.SalesReports
{
    public partial class RptReceivableJournal : Form
    {
        DateTime sdate;
        DateTime edate;
        private bool CanView = false;
        public RptReceivableJournal()
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
            dtmTxFrom.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            dtmTxTo.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
        private void LoadReportCustomerLedger()
        {
            SqlConnection con = null;
            try
            {
                string selectSql = @"SELECT j.*,  p.SalesDepositGLCode, jb.JobCode 
                                      FROM Journal j 
                                    LEFT JOIN Preference p ON j.AccountID = p.SalesDepositGLCode 
                                    LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                     WHERE j.Type IN ('SI', 'SP')  AND TransactionNumber NOT LIKE 'SYS-%' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);

                sdate = dtmTxFrom.Value;
                edate = dtmTxTo.Value;
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);

                Reports.ReportParams CustomerLedger = new Reports.ReportParams();
                CustomerLedger.PrtOpt = 1;
                CustomerLedger.Rec.Add(TbRep);
                CustomerLedger.ReportName = "SalesReceivableJournal.rpt";
                CustomerLedger.RptTitle = "Sales Receivable Journal";
                CustomerLedger.Params = "compname|StartDate|EndDate";
                CustomerLedger.PVals = CommonClass.CompName.Trim() +"|"+ dtmTxFrom.Value.ToShortDateString() + "|"+ dtmTxTo.Value.ToShortDateString();

                CommonClass.ShowReport(CustomerLedger);
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReportCustomerLedger();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RptReceivableJournal_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
