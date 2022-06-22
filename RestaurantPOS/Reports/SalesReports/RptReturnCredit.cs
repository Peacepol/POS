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

namespace AbleRetailPOS.Reports.SalesReports
{
    public partial class RptReturnCredit : Form
    {
        private DataTable TbRep;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection con;
        String SalesType = "INVOICE";
        String InvoiceStatus = "";
        string selectSql;
        string reportName;
        string reportTitle;
        string shippingID;
        string salespersonID;
        DateTime TimeNow = DateTime.Now;
        bool promised = false;
        private bool CanView = false;
        public RptReturnCredit()
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
            sdatePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edatePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        void LoadReport()
        {
            if (cmbSalesStatus.Text == "All Sales")
            {
                selectSql = @"SELECT TransactionDate, 
                                    SalesNumber,
                                    CustomerPONumber,
                                    Name,
                                    GrandTotal,
                                    TotalDue,
                                    InvoiceStatus 
                                FROM Sales s 
                                INNER JOIN Profile p ON s.CustomerID = p.ID 
                                WHERE TransactionDate BETWEEN @sdate AND @edate";

                if (txtAmountFrom.Text != "")
                {
                    selectSql += " AND GrandTotal BETWEEN " + txtAmountFrom.Text + " AND " + txtAmountTo.Text;
                }
                if (txtShipVia.Text != "")
                {
                    selectSql += " AND Sales.ShippingMethodID = " + shippingID;
                }
                if (txtEmployee.Text != "")
                {
                    selectSql += " AND SalesPersonID = " + salespersonID;
                }
            }
            else if (cmbSalesStatus.Text != "")
            {
                selectSql = @"SELECT TransactionDate, 
                                         SalesNumber,
                                         CustomerPONumber,
                                         Name,
                                         GrandTotal,
                                         TotalDue,
                                         InvoiceStatus 
                                FROM Sales s 
                                INNER JOIN Profile p ON s.CustomerID = p.ID 
                                WHERE TransactionDate BETWEEN @sdate AND @edate";
                if (cmbSalesStatus.Text == "All Invoices")
                {
                    selectSql += " AND SalesType = '" + SalesType + "'";
                }
                else if (cmbSalesStatus.Text == "Open")
                {
                    selectSql += " AND SalesType = 'INVOICE' AND InvoiceStatus = 'OPEN'";
                }
                else if (cmbSalesStatus.Text == "Credit")
                {
                    selectSql += " AND SalesType = 'INVOICE'";
                }
                else if (cmbSalesStatus.Text == "Closed")
                {
                    selectSql += " AND SalesType = 'INVOICE' AND InvoiceStatus = 'Closed'";
                }
                else if (cmbSalesStatus.Text == "Closed")
                {
                    selectSql += " AND SalesType = 'INVOICE' AND InvoiceStatus = 'Closed'";
                }
                else if (cmbSalesStatus.Text == "Orders")
                {
                    selectSql += " AND SalesType = 'ORDER'";
                }
                else if (cmbSalesStatus.Text == "Quotes")
                {
                    selectSql += " AND SalesType = 'QUOTE'";
                }

                if (txtAmountFrom.Text != "")
                {
                    selectSql += " AND GrandTotal BETWEEN " + txtAmountFrom.Text + " AND " + txtAmountTo.Text;
                }
                if (txtShipVia.Text != "")
                {
                    selectSql += " AND Sales.ShippingMethodID = " + shippingID;
                }

                if (txtEmployee.Text != "")
                {
                    selectSql += " AND SalesPersonID = " + salespersonID;
                }
            }
            else
            {
                MessageBox.Show("Select Sales Type");
            }

            if (promised == true)
            {
                selectSql += " AND p.PromiseDate BETWEEN @psdate AND @pedate";
            }

            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);
                //con.Open();
                DateTime sdate = sdatePicker.Value;
                DateTime edate = edatePicker.Value;
                DateTime psdate = PSdateTimePicker.Value;
                DateTime pedate = PEdateTimePicker.Value;

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@psdate", psdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@pedate", pedate.ToUniversalTime());
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);
                Reports.ReportParams SalesSummary = new Reports.ReportParams();
                SalesSummary.PrtOpt = 1;
                SalesSummary.Rec.Add(TbRep);
                SalesSummary.ReportName = "AllSales.rpt";
                SalesSummary.RptTitle = "All Sales";
                SalesSummary.Params = "compname";
                SalesSummary.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(SalesSummary);
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

        private void btn_SalespersonLookup_Click(object sender, EventArgs e)
        {
            SalespersonLookup SalespersonDlg = new SalespersonLookup();
            if (SalespersonDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lSales = SalespersonDlg.GetSalesperson;
                salespersonID = lSales[0];
                txtEmployee.Text = lSales[1];
            }
        }

        private void btn_ShippingmethodLookup_Click(object sender, EventArgs e)
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;
                txtShipVia.Text = ShipList[0];
                shippingID = ShipList[1];
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
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
            else
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

        private void RptReturnCredit_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
