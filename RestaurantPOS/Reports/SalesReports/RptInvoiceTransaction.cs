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
using AbleRetailPOS.Reports;

namespace AbleRetailPOS.Reports.SalesReports
{
    public partial class RptInvoiceTransaction : Form
    {
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        private bool CanView = false;
        public RptInvoiceTransaction()
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

        private void LoadReportAllPurchases()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT SalesNumber, LayoutType
                    ,SalesLines.TransactionDate , Sales.SubTotal
                    ,Sales.GrandTotal ,pl.Amount , pl.EntryDate
                    ,Sales.TaxTotal,Sales.TotalDue
                    , InvoiceStatus, Name, p.Memo as PaymentMemo
                    , p.PaymentNumber, p.TransactionDate as PaymentTransDate
                    , ProfileIDNumber , Sales.Memo FROM
                     Sales INNER JOIN Profile ON Sales.CustomerId = Profile.ID
                     INNER JOIN SalesLines on Sales.SalesID = SalesLines.SalesID
                    LEFT JOIN PaymentLines pl ON pl.EntityID = Sales.SalesID
                    Left Join Payment p ON p.PaymentId = pl.PaymentID
                    WHERE SalesType = 'INVOICE' AND SalesLines.TransactionDate  BETWEEN @sdate and @edate ";

                if (txtShipVia.Text != "")
                {
                    sql += " AND p.ShippingMethodID = " + ShippingID;
                }
                if (toNum.Value != 0)
                {
                    sql += " AND p.GrandTotal  BETWEEN '" + fromNum.Value + "' AND ' " + toNum.Value + "'";
                }
                if (promised == true)
                {
                    sql += " AND p.PromiseDate  BETWEEN @psdate AND @pedate ";
                }
                if (txtEmployee.Text != "")
                {
                    sql += " AND UserID = " + EmployeeID;
                }
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                DateTime sdate = sdateTimePicker.Value;
                DateTime edate = edateTimePicker.Value;
                DateTime psdate = PSdateTimePicker.Value;
                DateTime pedate = PEdateTimePicker.Value;

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@psdate", psdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@pedate", pedate.ToUniversalTime());

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);

                ReportParams AllPurchases = new ReportParams();
                AllPurchases.PrtOpt = 1;
                AllPurchases.Rec.Add(TbRep);
                AllPurchases.ReportName = "InvoiceTransactions.rpt";
                AllPurchases.RptTitle = "Invoice Transaction";

                AllPurchases.Params = "compname";
                AllPurchases.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(AllPurchases);
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
            LoadReportAllPurchases();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowShippingmethod();
        }
        void ShowShippingmethod()
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;

                txtShipVia.Text = ShipList[0];
                ShippingID = ShipList[1];
            }
        }

        private void DateRangesStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRangesStart.SelectedIndex != 0)
            {
                DateRangesEnd.SelectedIndex = DateRangesStart.SelectedIndex;
                PSdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex, 1, 00, 00, 00);
                PEdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesEnd.SelectedIndex, (DateTime.DaysInMonth(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex)), 23, 59, 59);
                promised = true;
            }
            if (DateRangesStart.SelectedIndex == 0)
            {
                promised = false;
            }
        }

        private void DateRangesEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRangesEnd.SelectedIndex != 0)
            {
                PEdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesEnd.SelectedIndex, (DateTime.DaysInMonth(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex)), 23, 59, 59);
            }
            else
            {
                promised = false;
            }
        }

        private void pbEmployee_Click(object sender, EventArgs e)
        {
            SalespersonLookup SalespersonDlg = new SalespersonLookup();
            if (SalespersonDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lSales = SalespersonDlg.GetSalesperson;
                EmployeeID = lSales[0];
                txtEmployee.Text = lSales[1];
            }
        }

        private void PurchaseStatuscb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RptInvoiceTransaction_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
