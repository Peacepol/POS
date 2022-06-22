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

namespace RestaurantPOS.Sales
{
    public partial class PrintSalesReceipts : Form
    {
        string paymentIDs;
        string WholeWord;
        string DeciWord;
        private bool CanView = false;
        public PrintSalesReceipts()
        {
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
            }
            dtmTxFrom.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            dtmTxTo.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
        void PopulateGrid()
        {
                string sql = @"SELECT TransactionDate , Memo ,TotalAmount , p.Name , PaymentID" +
                               " FROM Payment " +
                               " INNER JOIN Profile p ON p.ID = Payment.ProfileID " +
                               " INNER JOIN Preference pf ON pf.SalesDepositGLCode = Payment.AccountID " +
                               " WHERE PaymentFor = 'Sales' " +
                               " AND TransactionDate BETWEEN @sdate and @edate ";

                DateTime sdate = dtmTxFrom.Value.ToUniversalTime();
                DateTime edate = dtmTxTo.Value.ToUniversalTime();
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                DataTable TbRep = new DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@sdate", sdate);
                param.Add("@edate", edate);
                CommonClass.runSql(ref TbRep, sql, param);

                for (int x = 0; x < TbRep.Rows.Count; x++)
                {
                    DataRow dr = TbRep.Rows[x];
                    dgSalesReceipts.Rows.Add();
                    dgSalesReceipts.Rows[x].Cells[0].Value = "false";
                    dgSalesReceipts.Rows[x].Cells[1].Value = dr["Name"].ToString();
                    dgSalesReceipts.Rows[x].Cells[2].Value = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToLocalTime().ToShortDateString();
                    dgSalesReceipts.Rows[x].Cells[3].Value = dr["TotalAmount"].ToString();
                    dgSalesReceipts.Rows[x].Cells[4].Value = dr["PaymentID"].ToString();
                }
           
        }

        private void PrintSalesReceipts_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            PopulateGrid();
        }
        private void LoadReceipt()
        {
                DataTable dt = new DataTable();
                string selectSql = "SELECT WholeNumberWord, DecimalWord FROM Currency";
                CommonClass.runSql(ref dt, selectSql);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    WholeWord = dr["WholeNumberWord"].ToString();
                   DeciWord = dr["DecimalWord"].ToString();
               
                }
                string sql = @"SELECT Payment.TransactionDate , Payment.Memo ,TotalAmount , p.Name 
                             ,Payment.PaymentID,PaymentNumber, pl.Amount, Address = (Select Concat(Street,', ',City,',
                             ',State,', ', Postcode,', ', Country) FROM Contacts WHERE ProfileID = p.ID AND Location = p.LocationID ), s.SalesNumber
                             FROM Payment  
                            INNER JOIN Profile p ON p.ID = Payment.ProfileID
                            INNER JOIN Preference pf ON pf.SalesDepositGLCode = Payment.AccountID 
                            INNER JOIN PaymentLines pl ON pl.PaymentID = Payment.PaymentID
                            INNER JOIN Sales s ON s.SalesID = pl.EntityID " +
                    " WHERE PaymentFor = 'Sales' AND Payment.PaymentID in ("+ paymentIDs+")";
                DateTime sdate = dtmTxFrom.Value.ToUniversalTime();
                DateTime edate = dtmTxTo.Value.ToUniversalTime();
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);

                DataTable TbRep = new DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@sdate", sdate);
                param.Add("@edate", edate);
                CommonClass.runSql(ref TbRep, sql, param);
                         
                Reports.ReportParams salesparams = new Reports.ReportParams();
                salesparams.PrtOpt = 1;
                salesparams.Rec.Add(TbRep);

                salesparams.ReportName = "PrintSalesReceipt.rpt";
                salesparams.RptTitle = "Receipt";

                salesparams.Params = "compname|Address|WholeNumWord|DecimalWord";
                salesparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() +"|" + WholeWord+"|" +DeciWord;

                CommonClass.ShowReport(salesparams);
 
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int i= 0;
            foreach(DataGridViewRow item in dgSalesReceipts.Rows)
            {
                if(bool.Parse(item.Cells[0].Value.ToString()))
                {
                    paymentIDs += item.Cells[4].Value.ToString() + ",";
                    i++;
                }
            }
            if (i > 0)
            {
                paymentIDs = paymentIDs.Remove(paymentIDs.Length - 1);
                LoadReceipt();
                paymentIDs = "";
            }
            else
            {
                MessageBox.Show("Must check atleast 1", "Information");
            }
           
        }

        private void dgSalesReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
