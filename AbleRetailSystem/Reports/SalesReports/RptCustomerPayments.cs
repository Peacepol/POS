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

namespace RestaurantPOS.Reports.SalesReports
{
    public partial class RptCustomerPayments : Form
    {
        private DataTable TbRep;
        private bool CanView = false;
        public RptCustomerPayments()
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
        private void LoadReportCustomerPayment()
        {
            SqlConnection con = null;
            try
            {
                string selectSql = @"SELECT SalesNumber, 
                                         TransactionDate,
                                         GrandTotal,
                                         TaxTotal,
                                         TotalDue,
                                         InvoiceStatus,
                                         PromiseDate, 
                                         Name,
                                         ProfileIDNumber,
                                         ClosedDate 
                                    FROM Sales 
                                    INNER JOIN Profile ON Sales.CustomerId = Profile.ID  
                                    WHERE Sales.TransactionDate BETWEEN  @sdate AND @edate 
                                    AND InvoiceStatus = 'Closed'";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);
                DateTime sdate = dtmTxFrom.Value;
                DateTime edate = dtmTxTo.Value;
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);

                Reports.ReportParams CustomerPayments = new Reports.ReportParams();
                CustomerPayments.PrtOpt = 1;
                CustomerPayments.Rec.Add(TbRep);
                CustomerPayments.ReportName = "CustomerPayments.rpt";
                CustomerPayments.RptTitle = "Customer Payments [Closed Invoice]";

                CustomerPayments.Params = "compname";
                CustomerPayments.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(CustomerPayments);
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
            LoadReportCustomerPayment();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RptCustomerPayments_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
