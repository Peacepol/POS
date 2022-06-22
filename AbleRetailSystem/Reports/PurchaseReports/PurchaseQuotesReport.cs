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
    public partial class PurchaseQuotesReport : Form
    {
        DataTable TbRep;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        public PurchaseQuotesReport()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void pbShipping_Click(object sender, EventArgs e)
        {
            ShowShippingmethod();
        }


        private void LoadReportQuote()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.PurchaseNumber, p.TransactionDate, p.GrandTotal, p.TaxTotal, p.TotalDue, p.POStatus, p.PromiseDate, pro.Name, p.PurchaseType, p.SupplierINVNumber, pl.ReceiveQty
                        FROM Purchases p INNER JOIN Profile pro ON p.SupplierID = pro.ID LEFT JOIN PurchaseLines pl ON pl.PurchaseID = p.PurchaseID 
                        WHERE p.TransactionDate BETWEEN @sdate AND @edate ";
                if(PurchaseStatuscb.Text != "")
                {
                    if (PurchaseStatuscb.Text == "All Bills")
                    {
                        sql += " AND p.PurchaseType = 'BILL' ";
                    }
                    else if (PurchaseStatuscb.Text == "Open")
                    {
                        sql += " AND p.POStatus  = 'Open' ";
                    }
                    else if (PurchaseStatuscb.Text == "Debit")
                    {
                        sql += " AND p.POStatus  = 'Debit' ";
                    }
                    else if (PurchaseStatuscb.Text == "Closed")
                    {
                        sql += " AND p.POStatus  = 'Closed' ";
                    }
                    else if (PurchaseStatuscb.Text == "Orders")
                    {
                        sql += " AND p.POStatus = 'Order' ";
                    }
                    else if (PurchaseStatuscb.Text == "Quotes")
                    {
                        sql += " AND p.POStatus = 'Quote' ";
                    }
                    else
                    {
                        //All Purchase Display ALL
                    }
                }
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

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@sdate", sdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@psdate", psdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@pedate", pedate.ToUniversalTime());

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);
                Reports.ReportParams Qoutes = new Reports.ReportParams();
                Qoutes.PrtOpt = 1;
                Qoutes.Rec.Add(TbRep);
                Qoutes.ReportName = "ReportQuote.rpt";
                Qoutes.RptTitle = "Purchase Register [Qoutes]";

                Qoutes.Params = "compname";
                Qoutes.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(Qoutes);
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReportQuote();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
