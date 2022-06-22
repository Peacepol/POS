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
    public partial class RptPurchaseReconciliationSummary : Form
    {
        DataTable TbRep;
        public RptPurchaseReconciliationSummary()
        {
            InitializeComponent();
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
        private void LoadReportAgeingSummary()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.EntryDate, p.PromiseDate, f.Name, p.TotalDue, p.TransactionDate,
                        (SELECT CURRENT_TIMESTAMP) AS StarDate
                        FROM Purchases p 
                        INNER JOIN Profile f ON p.SupplierID = f.ID
                        INNER JOIN PurchaseLines l ON l.PurchaseID = p.PurchaseID
                        LEFT JOIN Terms t ON t.TermsID = p.TermsReferenceID
                        WHERE p.POStatus = 'Open' OR p.PurchaseType = 'ORDER'";
                if (cbAgeMethod.Text == "Number of Days since P.O. date")
                {
                    sql += " AND p.TransactionDate <= @edate ";
                }
                else
                {
                    sql += " AND ActualDueDate <= @edate ";
                }

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                DateTime edate = edateTimePicker.Value;
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);

                Reports.ReportParams AgeingSummary = new Reports.ReportParams();
                AgeingSummary.PrtOpt = 1;
                AgeingSummary.Rec.Add(TbRep);
                AgeingSummary.ReportName = "PayableReconciliationSummary.rpt";
                AgeingSummary.RptTitle = "Payables Reconciliation [Summary]";

                AgeingSummary.Params = "compname|asofDate";
                AgeingSummary.PVals = CommonClass.CompName.Trim()+"|"+ edateTimePicker.Text;

                CommonClass.ShowReport(AgeingSummary);
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
            LoadReportAgeingSummary();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
