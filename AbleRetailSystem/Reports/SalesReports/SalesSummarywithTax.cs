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
    public partial class SalesSummarywithTax : Form
    {
        DataTable TbRep;
        private bool CanView = false;
        public SalesSummarywithTax()
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
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
        private void LoadReportSummaryTax()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"Select TotalDue , (Select CURRENT_TIMESTAMP ) as StartDate, TransactionDate , Name, 
                        TaxTotal, GrandTotal ,TotalPaid 
                        FROM Sales 
                        INNER JOIN Profile ON Sales.CustomerID = Profile.ID 
                        WHERE InvoiceStatus = 'Order' OR InvoiceStatus = 'Open'
                        AND TransactionDate BETWEEN  @sdate AND @edate
                        ";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                DateTime sdate = sdateTimePicker.Value;
                DateTime edate = edateTimePicker.Value;

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);
                Reports.ReportParams SupplierPayment = new Reports.ReportParams();
                SupplierPayment.PrtOpt = 1;
                SupplierPayment.Rec.Add(TbRep);
                SupplierPayment.ReportName = "SalesSummarywithTax.rpt";
                SupplierPayment.RptTitle = "Summary with Tax";

                SupplierPayment.Params = "compname";
                SupplierPayment.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(SupplierPayment);
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
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReportSummaryTax();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SalesSummarywithTax_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
