using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Setup
{
    public partial class SessionTransactions : Form
    {
        private string thisFormCode = "";

        public SessionTransactions()
        {
            InitializeComponent();
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

        public decimal GetFloatFund
        {
            get { return txtFloat.Value; }
        }

        private void SessionTransactions_Load(object sender, EventArgs e)
        {
            txtOpeningFund.Value = decimal.Parse(CommonClass.OpeningFund.ToString());
            txtFloat.Value = decimal.Parse(CommonClass.OpeningFund.ToString());
            LoadSessionInfo();
            TotalSales();
            TotalTender();
        }
        void SessionCalc()
        {
            decimal amt = 0;
            for (int i = 0; i < this.dgSession.Rows.Count; i++)
            {
                if (this.dgSession.Rows[i].Cells["TotalAmount"].Value != null)
                {
                    if (this.dgSession.Rows[i].Cells["TotalAmount"].Value.ToString() != "")
                    {
                        amt += decimal.Parse(dgSession.Rows[i].Cells["TotalAmount"].Value.ToString());
                    }
                }
            }

            txtTotalSale.Value = amt;
        }

        private void btnSaveFloat_Click(object sender, EventArgs e)
        {
            bool allok = false;
            if (dgTender.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgTender.Rows)
                        {
                    if (dgvr.Cells["Amount"].Value.ToString() != "0")
                    {
                        if (dgvr.Cells["Count"].Value.ToString() != "0")
                        {
                            //check if all fields with amount is counted
                        }
                        else
                        {
                            MessageBox.Show("Total count cannot be zero if amount is no zero", "Session Warning!");
                            return;
                        }
                    }
                }
                saveCount();//Save count 
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }


            // string titles = "Information";
            //DialogResult PrintDetails = MessageBox.Show("Would you like to print the Details Summary Report", titles, MessageBoxButtons.YesNo);
            //if (PrintDetails == DialogResult.Yes)
            //{
            if (chkSummary.Checked)
            {
                LoadReport();
            }
            if (chkPayment.Checked)
            {
                SessionDetailReportLoad();
            }
            if (chkEntryDate.Checked)
            {
                LoadDetailReportByEntryDate();
            }
            if (chkAR.Checked)
            {
                LoadDetailReportARPayments();
            }
           
           // }
        }
        private void LoadReport()
        {
            Reports.ReportParams SessionSummaryReport = new Reports.ReportParams();
            SessionSummaryReport.PrtOpt = 1;
           
            SqlConnection con = new SqlConnection(CommonClass.ConStr); //SUMMARY
           
            string printSessionSummaryReport = @"SELECT s.TotalperTender as Amount, c.TotalCount, (s.TotalperTender - c.TotalCount) as Discrepancy, s.PaymentMethodID, s.PaymentMethod, ss.* from (SELECT SUM(pt.Amount) as TotalperTender, pt.PaymentMethodID, pm.PaymentMethod, p.SessionID  from PaymentTender pt 
                    inner join(SELECT PaymentID, SessionID from Payment where SessionID in (@SessionID) ) p on pt.PaymentID = p.PaymentID ";
            printSessionSummaryReport += @" inner join PaymentMethods pm on pt.PaymentMethodID = pm.id group by pt.PaymentMethodID, pm.PaymentMethod, p.SessionID ) s
                    inner join CountPerSession c on s.SessionID = c.SessionID and s.PaymentMethodID = c.PaymentMethodID
                    inner join Sessions ss on s.SessionID = ss.SessionID";


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionID", CommonClass.SessionID);
            DataTable dt = new DataTable();
            string invoice = @"SELECT InvoiceType, SessionID, SUM(GrandTotal) as GrandTotal  FROM Sales where SalesType = 'INVOICE' group by InvoiceType, SessionID";
            DataTable dti = new DataTable();

            CommonClass.runSql(ref dt, printSessionSummaryReport, param);
            CommonClass.runSql(ref dti, invoice);
            SessionSummaryReport.Rec.Add(dt);
            SessionSummaryReport.children.Add("InvoiceType", dti);
            SessionSummaryReport.ReportName = "SessionReport.rpt";
            SessionSummaryReport.RptTitle = "Session Summary Report";
            //SessionSummaryReport.Params = "compname";
            //SessionSummaryReport.PVals = CommonClass.CompName.Trim();

            string sqlSelect = @"SELECT * FROM Sessions WHERE SessionID = "+ CommonClass.SessionID;
            DataTable dtSelect = new DataTable();
            CommonClass.runSql(ref dtSelect, sqlSelect);
            string dateStart = "";
            if (dtSelect.Rows.Count > 0)
            {
                for (int i = 0; dtSelect.Rows.Count > i; i++)
                {
                    DataRow dr = dtSelect.Rows[i];
                    dateStart = dr["SessionStart"].ToString();
                }
            }
            DateTime sDate = DateTime.Parse(dateStart);
            DateTime eDate = DateTime.Now;

            SessionSummaryReport.Params = "compname|StartDate|EndDate";
            SessionSummaryReport.PVals = CommonClass.CompName.Trim() + "|" + sDate.ToShortDateString() + " |" + eDate.ToShortDateString();
            CommonClass.ShowReport(SessionSummaryReport);

        }
        void LoadSessionInfo()
        {
            string sql = @"SELECT s.*,t.TerminalName from Sessions s inner join Terminal t on s.SessionKey = t.TerminalID where SessionStatus = 'Open' and s.SessionID = @SessionID";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionID", CommonClass.SessionID);
            DataTable dt = new DataTable();
            txtOpeningFund.Value = decimal.Parse(CommonClass.OpeningFund.ToString());
            CommonClass.runSql(ref dt, sql, param);
            if (dt.Rows.Count > 0)
            {
                lblTerminalID.Text = dt.Rows[0]["SessionKey"].ToString();
                lblTerminalName.Text = dt.Rows[0]["TerminalName"].ToString();
                lblSessionStart.Text = Convert.ToDateTime(dt.Rows[0]["SessionStart"].ToString()).ToString("yyyy-MMM-dd HH:mm:ss");


            }
            
        }

        void TotalSales()
        {
            string sql = @"SELECT DISTINCT s.* FROM Sales s  WHERE SalesType = 'INVOICE' and s.SessionID = @SessionID";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionID", CommonClass.SessionID);
            DataTable dt = new DataTable();
            txtOpeningFund.Value = decimal.Parse(CommonClass.OpeningFund.ToString());
            CommonClass.runSql(ref dt, sql, param);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dgSession.Rows.Add();
                    dgSession.Rows[i].Cells["InvoiceNum"].Value = dr["SalesNumber"];
                    dgSession.Rows[i].Cells["TotalAmount"].Value = dr["GrandTotal"];
                    dgSession.Rows[i].Cells["TotalPaid"].Value = dr["TotalPaid"];
                    dgSession.Rows[i].Cells["Balance"].Value = dr["TotalDue"];
                }
            }
            SessionCalc();
        }
        void TotalTender()
        {
      //      string sql = @"SELECT  SUM(pt.Amount) as TotalperTender, pd.PaymentMethodID, pm.PaymentMethod FROM PaymentTender pt 
      //                  inner join PaymentDetails pd on pt.PaymentID = pd.PaymentID and pt.id = pd.PaymentDetailsID
      //                  inner join Payment p on pt.PaymentID = p.PaymentID
      //                  inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
						//left join PaymentMethods pm on pd.PaymentMethodID = pm.id
      //                  WHERE pt.Amount <> 0 and SessionID = @SessionID group by pd.PaymentMethodID, pm.PaymentMethod ";
            string sql = @"SELECT SUM(pt.Amount) as TotalperTender, pt.PaymentMethodID, pm.PaymentMethod from PaymentTender pt 
inner join(SELECT PaymentID, SessionID from Payment where SessionID = @SessionID) p on pt.PaymentID = p.PaymentID
inner join PaymentMethods pm on pt.PaymentMethodID = pm.id group by pt.PaymentMethodID, pm.PaymentMethod";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionID", CommonClass.SessionID);
            DataTable dt = new DataTable();
           
            CommonClass.runSql(ref dt, sql, param);
            if (dt.Rows.Count > 0)
            {
                decimal amt = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dgTender.Rows.Add();
                    dgTender.Rows[i].Cells["PaymentMethodID"].Value = dr["PaymentMethodID"];
                    dgTender.Rows[i].Cells["PaymentMeth"].Value = dr["PaymentMethod"];
                    dgTender.Rows[i].Cells["Amount"].Value = dr["TotalperTender"];
                    dgTender.Rows[i].Cells["Count"].Value = "0";
                    dgTender.Rows[i].Cells["Discrepancy"].Value ="0";
                    amt += decimal.Parse(dr["TotalperTender"].ToString());
                }
                txtTotalCollection.Value = amt;



            }
         
        }

        private void dgSession_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 
            || e.ColumnIndex == 2 
            || e.ColumnIndex == 3
            && e.RowIndex != this.dgSession.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgTotalTender_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        { 
           if(e.ColumnIndex == 1
            || e.ColumnIndex == 2
            || e.ColumnIndex == 3
            && e.RowIndex != this.dgTender.NewRowIndex)
            {
                if (e.Value != null)
                {
                    float x = 0;
                    if(float.TryParse(e.Value.ToString(), out x))
                    {
                        double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                        e.Value = d.ToString("C2");
                    }
                  
                }
            }
        }

        private void dgTotalTender_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            if(e.ColumnIndex == 2)
            {
               // this.dgTender.CurrentCell = this.dgTender.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.dgTender.BeginEdit(true);
            }
        }

        private void dgTotalTender_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                float x = 0;
                if(dgTender.Rows[e.RowIndex].Cells["Count"].Value != null)
                {
                    if (float.TryParse(dgTender.Rows[e.RowIndex].Cells["Count"].Value.ToString(), out x))
                    {
                        dgTender.Rows[e.RowIndex].Cells["Discrepancy"].Value = float.Parse(dgTender.Rows[e.RowIndex].Cells["Count"].Value.ToString()) - float.Parse(dgTender.Rows[e.RowIndex].Cells["Amount"].Value.ToString() == "" ? "0" : dgTender.Rows[e.RowIndex].Cells["Amount"].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("You've entered an invalid format. Please Try Again", "Session Warning");
                        dgTender.Rows[e.RowIndex].Cells["Count"].Value = "0";
                        dgTender.Rows[e.RowIndex].Cells["Discrepancy"].Value = "0";
                        this.dgTender.CurrentCell = this.dgTender.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        this.dgTender.BeginEdit(true);
                    }
                }
            }
        }

        private void dgTotalTender_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colindex = (int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex);
            e.Control.KeyPress -= Numeric_KeyPress;

            if (colindex == 2)
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
        void saveCount()
        {
            foreach (DataGridViewRow dgvr in dgTender.Rows)
            {
                if (dgvr.Cells["Amount"].Value.ToString() != "0")
                {
                    if (dgvr.Cells["Count"].Value.ToString() != "0")
                    {
                        string insertcount = @"INSERT INTO  CountPerSession (SessionID,PaymentMethodID,TotalCount)VALUES(@SessionID,@PaymentMethodID,@TotalCount)";
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@SessionID", CommonClass.SessionID);
                        param.Add("@PaymentMethodID", dgvr.Cells["PaymentMethodID"].Value.ToString());
                        param.Add("@TotalCount", float.Parse(dgvr.Cells["Count"].Value.ToString()));
                        int x = CommonClass.runSql(insertcount, CommonClass.RunSqlInsertMode.QUERY, param);
                        if (x > 0)
                        {
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added Session Transaction for Session ID " + CommonClass.SessionID.ToString(), CommonClass.SessionID.ToString());
                        }
                    }
                }
            }
        }
        void SessionDetailReportLoad()
        {
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
                where p.Source = '' and p.SessionID in (@SessionID)  and ss.SessionID in (@SessionID)";
            DetailSummaryReport += @"UNION SELECT pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod, pt.Amount, pl.EntityID, p.PaymentNumber as TransactionNumber, 'AR Payment' as TranType, pf.Name, p.TotalAmount as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Profile pf on p.ProfileID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = 'P' and p.SessionID in (@SessionID)  and ss.SessionID in (@SessionID)";
            DataTable dtDetailReport = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionID", CommonClass.SessionID); CommonClass.runSql(ref dtDetailReport, DetailSummaryReport, param);
            SessionDetail.Rec.Add(dtDetailReport);

            SessionDetail.ReportName = "SessionReportDetails.rpt";
            SessionDetail.RptTitle = "POS Session - List of Transactions By Payment Method";
            //SessionDetail.Params = "compname";
            //SessionDetail.PVals = CommonClass.CompName.Trim();
            string sqlSelect = @"SELECT * FROM Sessions WHERE SessionID = " + CommonClass.SessionID;
            DataTable dtSelect = new DataTable();
            CommonClass.runSql(ref dtSelect, sqlSelect);
            string dateStart = "";
            if (dtSelect.Rows.Count > 0)
            {
                for (int i = 0; dtSelect.Rows.Count > i; i++)
                {
                    DataRow dr = dtSelect.Rows[i];
                    dateStart = dr["SessionStart"].ToString();
                }
            }
            DateTime sDate = DateTime.Parse(dateStart);
            DateTime eDate = DateTime.Now;

            SessionDetail.Params = "compname|StartDate|EndDate";
            SessionDetail.PVals = CommonClass.CompName.Trim() + "|" + sDate.ToShortDateString() + " |" + eDate.ToShortDateString();
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
                where p.Source = '' and p.SessionID in (@SessionID)  and ss.SessionID in (@SessionID)";
            DetailSummaryReport += @"UNION SELECT pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod, pt.Amount, pl.EntityID, p.PaymentNumber as TransactionNumber, 'AR Payment' as TranType, pf.Name, p.TotalAmount as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Profile pf on p.ProfileID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = 'P' and p.SessionID in (@SessionID)  and ss.SessionID in (@SessionID)";

            DataTable dtDetailReport = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionID", CommonClass.SessionID);
            CommonClass.runSql(ref dtDetailReport, DetailSummaryReport,param);
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
                where p.Source = 'P' and p.SessionID in (@SessionID)  and ss.SessionID in (@SessionID)";
            DataTable dtDetailReport = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SessionID", CommonClass.SessionID);

            CommonClass.runSql(ref dtDetailReport, DetailSummaryReport, param);
            SessionDetail.Rec.Add(dtDetailReport);

            SessionDetail.ReportName = "SessionReportDetailsARPayments.rpt";
            SessionDetail.RptTitle = "POS Session - List of A/R Payments";
            SessionDetail.Params = "compname";
            SessionDetail.PVals = CommonClass.CompName.Trim();
            CommonClass.ShowReport(SessionDetail);
        }
    }
}
