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
namespace AbleRetailPOS.Reports
{
    public partial class SessionReportsCustomizer : Form
    {
        public DataTable dt = new DataTable();
        public string ID = "";
        private bool CanView = false;

        public SessionReportsCustomizer()
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
        private void LoadSummaryReport()
        {
            Reports.ReportParams SessionSummary = new Reports.ReportParams();
            SessionSummary.PrtOpt = 1;
            string cashAR = "";
            Dictionary<string, object> param = new Dictionary<string, object>();



            string printSessionSummaryReport = @"SELECT s.TotalperTender as Amount, c.TotalCount, (s.TotalperTender - c.TotalCount) as Discrepancy, s.PaymentMethodID, s.PaymentMethod, ss.* from (SELECT SUM(pt.Amount) as TotalperTender, pt.PaymentMethodID, pm.PaymentMethod, p.SessionID  from PaymentTender pt 
                    inner join(SELECT PaymentID, SessionID from Payment where SessionID in (" + ID + ") ) p on pt.PaymentID = p.PaymentID ";
            printSessionSummaryReport += @" inner join PaymentMethods pm on pt.PaymentMethodID = pm.id group by pt.PaymentMethodID, pm.PaymentMethod, p.SessionID ) s
                    inner join CountPerSession c on s.SessionID = c.SessionID and s.PaymentMethodID = c.PaymentMethodID
                    inner join Sessions ss on s.SessionID = ss.SessionID";
            DataTable dtSummaryReport = new DataTable();
            CommonClass.runSql(ref dtSummaryReport, printSessionSummaryReport, param);

            string printSalesBreakdown = @"SELECT InvoiceType, SessionID, SUM(GrandTotal) as GrandTotal  FROM Sales where SalesType = 'INVOICE' and SessionID in ( " + ID + ") group by InvoiceType,SessionID";




            SqlConnection con2 = new SqlConnection(CommonClass.ConStr);
            SqlCommand cmd2 = new SqlCommand(printSalesBreakdown, con2);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dtSalesBreakdown = new DataTable();
            da2.Fill(dtSalesBreakdown);
            //DataTable dtSalesBreakdown = new DataTable();
            //CommonClass.runSql(ref dtSalesBreakdown, printSalesBreakdown);

            if (dtSummaryReport.Rows.Count > 0)
            {
                SessionSummary.Rec.Add(dtSummaryReport);
                if (dtSalesBreakdown.Rows.Count > 0)
                {
                    SessionSummary.SubRpt = "InvoiceType";
                    SessionSummary.tblSubRpt = dtSalesBreakdown;
                }

                SessionSummary.Params = "compname|StartDate|EndDate";
                SessionSummary.PVals = CommonClass.CompName.Trim() + "|" + dtmTxFrom.Value.ToShortDateString() + " |" + dtmTxTo.Value.ToShortDateString();

                SessionSummary.ReportName = "SessionReport.rpt";
                SessionSummary.RptTitle = "Session Summary Report";

                CommonClass.ShowReport(SessionSummary);

            }
        }
        public bool GenerateSummaryReport()
        {
            int i = 0;
            foreach (DataGridViewRow session in dgSession.Rows)
            {
                if (bool.Parse(session.Cells[0].Value.ToString()))
                {
                    ID += session.Cells[1].Value.ToString() + ",";
                    i++;
                }
            }
            if (i > 0)
            {
                ID = ID.Remove(ID.Length - 1);
                return true;
            }
            else
            {
                MessageBox.Show("Must check atleast 1", "Information");
                return false;
            }
        }
        private void LoadDetailReport()
        {
            string cashAR = "";
            Reports.ReportParams SessionDetail = new Reports.ReportParams();
            SessionDetail.PrtOpt = 1;
          
            string DetailSummaryReport = @"SELECT pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod, pt.Amount, pl.EntityID, s.SalesNumber as TransactionNumber, 'Sales' as TranType, pf.Name, s.GrandTotal as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Sales s on pl.EntityID = s.SalesID
                inner join Profile pf on s.CustomerID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = '' and p.SessionID in (" + ID + ")  and ss.SessionID in (" + ID + ")";
            DetailSummaryReport += @"UNION SELECT pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod, pt.Amount, pl.EntityID, p.PaymentNumber as TransactionNumber, 'AR Payment' as TranType, pf.Name, p.TotalAmount as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Profile pf on p.ProfileID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = 'P' and p.SessionID in (" + ID + ")  and ss.SessionID in (" + ID + ")";
            DataTable dtDetailReport = new DataTable();

            CommonClass.runSql(ref dtDetailReport, DetailSummaryReport);
            SessionDetail.Rec.Add(dtDetailReport);

            SessionDetail.ReportName = "SessionReportDetails.rpt";
            SessionDetail.RptTitle = "POS Session - List of Transactions By Payment Method";
            SessionDetail.Params = "compname|StartDate|EndDate";
            SessionDetail.PVals = CommonClass.CompName.Trim() + "|" + dtmTxFrom.Value.ToShortDateString() + " |" + dtmTxTo.Value.ToShortDateString();
            CommonClass.ShowReport(SessionDetail);
        }
        private void LoadDetailReportByEntryDate()
        {
            string cashAR = "";
            Reports.ReportParams SessionDetail = new Reports.ReportParams();
            SessionDetail.PrtOpt = 1;
            string DetailSummaryReport = @"SELECT pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod, pt.Amount, pl.EntityID, s.SalesNumber as TransactionNumber, 'Sales' as TranType, pf.Name, s.GrandTotal as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Sales s on pl.EntityID = s.SalesID
                inner join Profile pf on s.CustomerID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = '' and p.SessionID in (" + ID + ")  and ss.SessionID in (" + ID + ")";
            DetailSummaryReport += @"UNION SELECT pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod, pt.Amount, pl.EntityID, p.PaymentNumber as TransactionNumber, 'AR Payment' as TranType, pf.Name, p.TotalAmount as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Profile pf on p.ProfileID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = 'P' and p.SessionID in (" + ID + ")  and ss.SessionID in (" + ID + ")";

            DataTable dtDetailReport = new DataTable();

            CommonClass.runSql(ref dtDetailReport, DetailSummaryReport);
            SessionDetail.Rec.Add(dtDetailReport);

            SessionDetail.ReportName = "SessionReportDetailsByTime.rpt";
            SessionDetail.RptTitle = "POS Session - List of Transactions By Entry Date";
            SessionDetail.Params = "compname";
            SessionDetail.PVals = CommonClass.CompName.Trim();
            CommonClass.ShowReport(SessionDetail);
        }
        private void LoadDetailReportARPayments()
        {
            string cashAR = "";
            Reports.ReportParams SessionDetail = new Reports.ReportParams();
            SessionDetail.PrtOpt = 1;

            string DetailSummaryReport = @"SELECT p.PaymentID,p.TransactionDate, pl.EntryDate, p.TotalAmount, p.PaymentNumber, pl.EntityID, s.SalesNumber, pf.Name, s.GrandTotal, pl.Amount as AmountPaid,  u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd from Payment p
                inner join PaymentLines pl on p.PaymentID = pl.PaymentID
                inner join Sales s on pl.EntityID = s.SalesID
                inner join Profile pf on p.ProfileID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = 'P' and p.SessionID in (" + ID + ")  and ss.SessionID in (" + ID + ")";
            DataTable dtDetailReport = new DataTable();

            CommonClass.runSql(ref dtDetailReport, DetailSummaryReport);
            SessionDetail.Rec.Add(dtDetailReport);

            SessionDetail.ReportName = "SessionReportDetailsARPayments.rpt";
            SessionDetail.RptTitle = "POS Session - List of A/R Payments";
            SessionDetail.Params = "compname";
            SessionDetail.PVals = CommonClass.CompName.Trim();
            CommonClass.ShowReport(SessionDetail);
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            ID = "";
           if( GenerateSummaryReport())
            {
                switch (cmbSessionType.SelectedIndex)
                {
                    case 0:
                    default:
                        LoadSummaryReport();
                        break;
                    case 1:
                        LoadDetailReport();
                        break;
                    case 2:
                        LoadDetailReportByEntryDate();
                        break;
                    case 3:
                        LoadDetailReportARPayments();
                        break;
                }
            }
        }

        private void SessionReportsCustomizer_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            this.cmbSessionType.SelectedIndex = 0;
        }
        public void LoadSession()
        {
            dt.Rows.Clear();
            dgSession.Rows.Clear();
            string selectSql = @"SELECT DISTINCT s.SessionID, SessionKey, SessionStart,SessionEnd FROM Sessions s 
                                 INNER JOIN Sales sl ON s.SessionID = sl.SessionID
                                 WHERE sl.TransactionDate BETWEEN @sdate AND @edate"; // OR SessionEnd BETWEEN @sdate AND @edate";
            Dictionary<string, object> param = new Dictionary<string, object>();

            DateTime sdate = dtmTxFrom.Value;
            DateTime edate = dtmTxTo.Value;
            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
            sdate = sdate.ToUniversalTime();
            edate = edate.ToUniversalTime();
            param.Add("@sdate", sdate);
            param.Add("@edate", edate);

            CommonClass.runSql(ref dt, selectSql, param);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DateTime startDate = Convert.ToDateTime(dr["SessionStart"].ToString());
                    DateTime endDate = Convert.ToDateTime(dr["SessionEnd"].ToString());

                    dgSession.Rows.Add();
                    dgSession.Rows[i].Cells["chkSession"].Value = "false";
                    dgSession.Rows[i].Cells["SessionID"].Value = dr["SessionID"].ToString();
                    dgSession.Rows[i].Cells["Terminal"].Value = dr["SessionKey"].ToString();
                    dgSession.Rows[i].Cells["SessionStart"].Value = startDate.ToShortDateString();
                    dgSession.Rows[i].Cells["SessionEnd"].Value = endDate.ToShortDateString();

                }
            }
        }

        private void dgSession_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbSessionType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLoadSession_Click(object sender, EventArgs e)
        {
            LoadSession();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
