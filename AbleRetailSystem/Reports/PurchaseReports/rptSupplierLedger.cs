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
    public partial class rptSupplierLedger : Form
    {
        public rptSupplierLedger()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
        private void LoadReportSupplierLedger()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT prc.TransactionDate, 
                            prc.PurchaseID, 
                            prc.Memo, 
                            prc.PurchaseNumber, 
                            prc.GrandTotal, 
                            prc.TotalDue, 
                            p.Name, 
                            pmt.PaymentNumber, 
                            pmt.TotalAmount , 
                            pmt.PaymentID
                        FROM Purchases prc
                        INNER JOIN PaymentLines pmtl ON prc.PurchaseID = pmtl.EntityID 
                        INNER JOIN Profile p ON p.ID = prc.SupplierID 
                        INNER JOIN Payment pmt ON pmt.PaymentID = pmtl.PaymentID
                        WHERE prc.PurchaseType = 'BILL'
                        AND prc.TransactionDate BETWEEN @sdate AND @edate";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                DateTime sdate = sdateTimePicker.Value;
                DateTime edate = edateTimePicker.Value;
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@sdate", sdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);

                SqlCommand cmd2 = new SqlCommand(@"SELECT PurchaseBillPrefix, 
                                                        BillsPaymentPrefix 
                                                    FROM TransactionSeries", con);
                da.SelectCommand = cmd2;
                DataTable TbRep2 = new DataTable();
                da.Fill(TbRep2);

                Reports.ReportParams SupplierLedger = new Reports.ReportParams();
                SupplierLedger.PrtOpt = 1;
                SupplierLedger.Rec.Add(TbRep);
                SupplierLedger.Rec.Add(TbRep2);
                SupplierLedger.ReportName = "SupplierLedger.rpt";
                SupplierLedger.RptTitle = "Supplier Ledger";

                SupplierLedger.Params = "compname";
                SupplierLedger.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(SupplierLedger);
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadReportSupplierLedger();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
