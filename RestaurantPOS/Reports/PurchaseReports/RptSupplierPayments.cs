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

namespace AbleRetailPOS.Reports.PurchaseReports
{
    public partial class RptSupplierPayments : Form
    {
        DataTable TbRep;
        public RptSupplierPayments()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void LoadReportSupplierReport()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT j.Name, pl.EntryDate, pl.Amount,
                        pay.TransactionDate, pay.PaymentNumber, pay.TotalAmount,
                        c.Street, c.City, c.State, c.Country, c.Postcode,  l.TaxCode 
                        FROM (Purchases p 
                        Inner join PurchaseLines l on p.PurchaseID = l.PurchaseID 
                        Inner Join PaymentLines pl on pl.EntityID = p.PurchaseID 
                        Inner join Payment pay on pay.PaymentID = pl.PaymentID
                        Inner join Profile j on p.SupplierID = j.ID 
                        Inner Join Contacts c on c.ProfileID = pay.ProfileID)
                        WHERE c.Location = pay.LocationID AND pay.PaymentFor = 'Purchase' AND 
                         pay.TransactionDate BETWEEN @sdate AND @edate AND c.Location = pay.LocationID ";
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
                TbRep = new DataTable();
                da.Fill(TbRep);
                Reports.ReportParams SupplierPayment = new Reports.ReportParams();
                SupplierPayment.PrtOpt = 1;
                SupplierPayment.Rec.Add(TbRep);
                SupplierPayment.ReportName = "SupplierPayment.rpt";
                SupplierPayment.RptTitle = "Supplier Payment";

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

        private void RptSupplierPayments_Load(object sender, EventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReportSupplierReport();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
