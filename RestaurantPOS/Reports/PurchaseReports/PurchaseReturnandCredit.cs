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
    public partial class PurchaseReturnandCredit : Form
    {
        DataTable TbRep;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        public PurchaseReturnandCredit()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void LoadReportReturnAndDebits()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT l.Description, l.TransactionDate, l.ReceiveQty, l.TaxCode, p.PurchaseNumber, p.GrandTotal, p.TotalDue, p.LayoutType, p.SupplierINVNumber, p.POStatus, p.TaxTotal, j.Name, i.DebitAmount, p.PromiseDate 
                        FROM (Purchases p INNER JOIN PurchaseLines l ON p.PurchaseID = l.PurchaseID 
                        INNER JOIN Profile j ON p.SupplierID = j.ID 
                        INNER JOIN Journal i ON i.JournalNumber = p.PurchaseNumber
                        INNER JOIN Preference pf ON pf.TradeCreditorGLCode = i.AccountID) 
                        WHERE p.TransactionDate BETWEEN @sdate AND @edate 
                        ";
                if (cbPurchaseStatus.Text != "")
                {
                    if (cbPurchaseStatus.Text == "All Bills")
                    {
                        sql += " AND p.PurchaseType = 'BILL' ";
                    }
                    else if (cbPurchaseStatus.Text == "Open")
                    {
                        sql += " AND p.POStatus  = 'Open' ";
                    }
                    else if (cbPurchaseStatus.Text == "Debit")
                    {
                        sql += " AND p.POStatus  = 'Debit' ";
                    }
                    else if (cbPurchaseStatus.Text == "Closed")
                    {
                        sql += " AND p.POStatus  = 'Closed' ";
                    }
                    else if (cbPurchaseStatus.Text == "Orders")
                    {
                        sql += " AND p.POStatus = 'Order' ";
                    }
                    else if (cbPurchaseStatus.Text == "Quotes")
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
                    sql += " AND p.GrandTotal BETWEEN '" + fromNum.Value + "' AND '" + toNum.Value + "'";
                }
                if (promised == true)
                {
                    sql += " AND p.PromiseDate BETWEEN @psdate AND @pedate ";
                }
                if (txtEmployee.Text != "")
                {
                    sql += " AND UserID = " + EmployeeID;
                }
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
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
                Reports.ReportParams ReturnsAndDebits = new Reports.ReportParams();
                ReturnsAndDebits.PrtOpt = 1;
                ReturnsAndDebits.Rec.Add(TbRep);
                ReturnsAndDebits.ReportName = "ReturnsAndDebits.rpt";
                ReturnsAndDebits.RptTitle = "Purchase Register [Returns And Debits]";

                ReturnsAndDebits.Params = "compname";
                ReturnsAndDebits.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(ReturnsAndDebits);
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

        private void pbShipping_Click(object sender, EventArgs e)
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReportReturnAndDebits();
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
