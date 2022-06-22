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
    public partial class RptPurchaseSummary : Form
    {
        private DataTable TbRep;
        public RptPurchaseSummary()
        {
            InitializeComponent();

        }

        private void RptPurchaseSummary_Load(object sender, EventArgs e)
        {

        }
        private void LoadReport()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT PurchaseNumber, TransactionDate, GrandTotal, TaxTotal, TotalDue, POStatus, PromiseDate, Name FROM Purchases INNER JOIN Profile ON Purchases.SupplierID = Profile.ID WHERE TransactionDate BETWEEN  @sdate AND @edate AND PurchaseType = 'BILL'";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
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

                DataTable tbRpt = new DataTable();
                da.Fill(tbRpt);
                Reports.ReportParams PurchaseSummary = new Reports.ReportParams();
                PurchaseSummary.PrtOpt = 1;
                PurchaseSummary.Rec.Add(TbRep);
                PurchaseSummary.ReportName = "PurchaseReport.rpt";
                PurchaseSummary.RptTitle = "Purchase Report Summary";

                PurchaseSummary.Params = "compname";
                PurchaseSummary.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(PurchaseSummary);
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
            LoadReport();
        }
    }
}
