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
using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;

namespace RestaurantPOS.Reports.SalesReports
{
    public partial class rptSalesReportDetail : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
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
        private bool CanView = false;
        public rptSalesReportDetail()
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
            pdateFrom.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            pdateTo.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void SalesReportDetail_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
        private void GetReportData()
        {
            selectSql = @"SELECT  SalesType,Name,SalesNumber, 
                                         SalesLines.TransactionDate,
                                         SalesLines.ShipQty,
                                         PartNumber,
                                         Description,
                                         GrandTotal,
                                         TaxTotal,
                                         TotalDue,
                                         SalesLines.TaxCode,
                                         InvoiceStatus,
                                         PromiseDate
                                FROM Sales 
                                INNER JOIN Profile ON Sales.CustomerId = Profile.ID
                                INNER JOIN SalesLines ON Sales.SalesID = SalesLines.SalesID
                                LEFT JOIN Items ON Items.ID = SalesLines.EntityID
                                WHERE SalesLines.TransactionDate BETWEEN @sdate AND @edate 
                                ";

            if (cmbSalesStatus.Text == "All Invoices")
            {
                selectSql += " AND SalesType in ('INVOICE','SINVOICE')";
            }
            else if (cmbSalesStatus.Text == "Open Sales")
            {
                selectSql += " AND SalesType in ('INVOICE','SINVOICE') AND InvoiceStatus = 'OPEN'";
            }
            else if (cmbSalesStatus.Text == "Orders")
            {
                selectSql += " AND SalesType = 'ORDER'";
            }
            else if (cmbSalesStatus.Text == "Quotes")
            {
                selectSql += " AND SalesType = 'QUOTE'";
            }
            else if (cmbSalesStatus.Text == "Lay-By")
            {
                selectSql += " AND SalesType = 'LAY-BY'";
            }
            else
            {
                //ALL
            }
            if (txtAmountFrom.Text != "")
            {
                selectSql += "AND GrandTotal BETWEEN " + txtAmountFrom.Text + " AND " + txtAmountTo.Text + "";
            }
            else
            {
                //All Amounts
            }
            if (txtShipVia.Text != "")
            {
                selectSql += "AND Sales.ShippingMethodID = " + shippingID + "";
            }
            else
            {
                //All Shipping Method
            }
            if (txtEmployee.Text != "")
            {
                selectSql += "AND SalesPersonID = " + salespersonID + "";
            }
            else
            {
                //ALL Sales Person
            }
            if (DateRange1.Text != "DATE RANGE" && DateRange2.Text != "DATE RANGE")
            {
                selectSql += "AND PromiseDate BETWEEN @pdateStart AND @pdateEnd";
            }
            else
            {
                //Default
            }

            try
            {
                
                con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);
                con.Open();
                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                DateTime psdate = Convert.ToDateTime(pdateFrom.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime pedate = Convert.ToDateTime(pdateTo.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();;


                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@pdateStart", psdate);
                cmd.Parameters.AddWithValue("@pdateEnd", pedate);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new System.Data.DataTable();
                da.Fill(TbRep);
               
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
        void LoadReport(int pViewType = 0)
        {
            if (pViewType == 0)
            {
                reportName = "SalesReportDetail.rpt";
                reportTitle = "Sales Report Details";
                Reports.ReportParams SalesSummary = new Reports.ReportParams();
                SalesSummary.PrtOpt = 1;
                SalesSummary.Rec.Add(TbRep);
                SalesSummary.ReportName = reportName;
                SalesSummary.RptTitle = reportTitle;
                SalesSummary.Params = "compname";
                SalesSummary.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(SalesSummary);

            }
            else
            {

                CalculateTotal();
                this.dgReport.DataSource = TbGrid;
                FormatGrid();
                FillSortCombo();


            }
        }
        private void CalculateTotal(int pSortIndex = 1, string pSortMode = "asc")
        {
            TbGrid = TbRep.Copy();
            float TotalAmount = 0;
            float TotalTax = 0;
            float TotalBalance = 0;
            for (int i = 0; i < TbGrid.Rows.Count; i++)
            {
                DateTime lTranDate = Convert.ToDateTime(TbGrid.Rows[i]["TransactionDate"].ToString()).ToLocalTime();
                DateTime lPromiseDate = Convert.ToDateTime(TbGrid.Rows[i]["PromiseDate"].ToString()).ToLocalTime();
                TbGrid.Rows[i]["TransactionDate"] = lTranDate.ToShortDateString();
                TbGrid.Rows[i]["PromiseDate"] = lPromiseDate.ToShortDateString();
                TotalAmount += float.Parse(TbGrid.Rows[i]["GrandTotal"].ToString());
                TotalTax += float.Parse(TbGrid.Rows[i]["TaxTotal"].ToString());
                TotalBalance += float.Parse(TbGrid.Rows[i]["TotalDue"].ToString());
            }

            DataView dv = TbGrid.DefaultView;
            dv.Sort = TbGrid.Columns[pSortIndex].ColumnName + " " + pSortMode;
            TbGrid = dv.ToTable();
            DataRow rw = TbGrid.NewRow();
            rw[0] = "TOTAL";
            rw[7] = TotalAmount;
            rw[8] = TotalTax;
            rw[9] = TotalBalance;
            TbGrid.Rows.Add(rw);
        }

        private void FormatGrid()
        {            
            this.dgReport.Columns[0].HeaderText = "Sales Type";
            this.dgReport.Columns[1].HeaderText = "Customer Name";
            this.dgReport.Columns[2].HeaderText = "Sales Number";
            this.dgReport.Columns[3].HeaderText = "Date";
            this.dgReport.Columns[4].HeaderText = "Qty";
            this.dgReport.Columns[5].HeaderText = "Part Number";            
            this.dgReport.Columns[6].HeaderText = "Description";
            this.dgReport.Columns[7].HeaderText = "Total Amount";
            this.dgReport.Columns[8].HeaderText = "Total Tax";
            this.dgReport.Columns[9].HeaderText = "Balance";
            this.dgReport.Columns[10].HeaderText = "Tax";
            this.dgReport.Columns[11].HeaderText = "Status";
            this.dgReport.Columns[12].HeaderText = "Promise Date";           
            this.dgReport.Columns[7].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[8].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[9].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
           
        }
        private void FillSortCombo()
        {
            if(this.cbSort.Items.Count == 0)
            {                
                for (int i = 0; i < dgReport.ColumnCount; i++)
                {
                    this.cbSort.Items.Add(dgReport.Columns[i].HeaderText);
                }
                this.cbSort.Enabled = true;
                this.btnSortGrid.Enabled = true;
                this.cbSort.SelectedIndex = 0;
            }
          

        }

        private void cmbSalesStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalesStatus.Text == "All Invoices")
            {
                SalesType = "INVOICE";

            }
            else if (cmbSalesStatus.Text == "Open Sales")
            {
                InvoiceStatus = "OPEN";
                SalesType = "INVOICE";
            }
            else if (cmbSalesStatus.Text == "Order")
            {
                InvoiceStatus = "Order";
                SalesType = "ORDER";
            }
            else if (cmbSalesStatus.Text == "Quote")
            {
                InvoiceStatus = "Quote";
                SalesType = "QUOTE";
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

        private void DateRange1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRange1.Text == "July")
            {
                DateRange2.Text = "July";
                int monthvalue = 7;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "August")
            {
                DateRange2.Text = "August";
                int monthvalue = 8;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "September")
            {
                DateRange2.Text = "September";
                int monthvalue = 9;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "October")
            {
                DateRange2.Text = "October";
                int monthvalue = 10;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "November")
            {
                DateRange2.Text = "November";
                int monthvalue = 11;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "December")
            {
                DateRange2.Text = "December";
                int monthvalue = 12;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "January")
            {
                DateRange2.Text = "January";
                int monthvalue = 1;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "February")
            {
                DateRange2.Text = "February";
                int monthvalue = 2;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "March")
            {
                DateRange2.Text = "March";
                int monthvalue = 3;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "April")
            {
                DateRange2.Text = "April";
                int monthvalue = 4;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "May")
            {
                DateRange2.Text = "May";
                int monthvalue = 5;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange1.Text == "June")
            {
                DateRange2.Text = "June";
                int monthvalue = 6;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else
            {
                DateRange2.Text = "DATE RANGE";
                DateRange1.Text = "DATE RANGE";
            }
        }

        private void DateRange2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRange2.Text == "July")
            {
                DateRange1.Text = "July";
                int monthvalue = 7;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "August")
            {
                DateRange1.Text = "August";
                int monthvalue = 8;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "September")
            {
                DateRange1.Text = "September";
                int monthvalue = 9;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "October")
            {
                DateRange1.Text = "October";
                int monthvalue = 10;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "November")
            {
                DateRange1.Text = "November";
                int monthvalue = 11;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "December")
            {
                DateRange1.Text = "December";
                int monthvalue = 12;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "January")
            {
                DateRange1.Text = "January";
                int monthvalue = 1;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "February")
            {
                DateRange1.Text = "February";
                int monthvalue = 2;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "March")
            {
                DateRange1.Text = "March";
                int monthvalue = 3;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "April")
            {
                DateRange1.Text = "April";
                int monthvalue = 4;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "May")
            {
                DateRange1.Text = "May";
                int monthvalue = 5;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "June")
            {
                DateRange1.Text = "June";
                int monthvalue = 6;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else
            {
                DateRange1.Text = "DATE RANGE";
                DateRange2.Text = "DATE RANGE";
            }
        }

        private void DateRange2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (DateRange2.Text == "July")
            {
                DateRange1.Text = "July";
                int monthvalue = 7;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "August")
            {
                DateRange1.Text = "August";
                int monthvalue = 8;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "September")
            {
                DateRange1.Text = "September";
                int monthvalue = 9;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "October")
            {
                DateRange1.Text = "October";
                int monthvalue = 10;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "November")
            {
                DateRange1.Text = "November";
                int monthvalue = 11;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "December")
            {
                DateRange1.Text = "December";
                int monthvalue = 12;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "January")
            {
                DateRange1.Text = "January";
                int monthvalue = 1;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "February")
            {
                DateRange1.Text = "February";
                int monthvalue = 2;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "March")
            {
                DateRange1.Text = "March";
                int monthvalue = 3;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "April")
            {
                DateRange1.Text = "April";
                int monthvalue = 4;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "May")
            {
                DateRange1.Text = "May";
                int monthvalue = 5;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else if (DateRange2.Text == "June")
            {
                DateRange1.Text = "June";
                int monthvalue = 6;
                pdateFrom.Value = new DateTime(pdateFrom.Value.Year, monthvalue, 1, 00, 00, 00);
                pdateTo.Value = new DateTime(pdateFrom.Value.Year, monthvalue, (DateTime.DaysInMonth(pdateFrom.Value.Year, monthvalue)), 23, 59, 59);
            }
            else
            {
                DateRange1.Text = "DATE RANGE";
                DateRange2.Text = "DATE RANGE";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(0);
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(1);
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            string lSortMode = (rdoAsc.Checked == true ? "asc" : "desc");
            CalculateTotal(this.cbSort.SelectedIndex, lSortMode);
            this.dgReport.DataSource = TbGrid;
            FormatGrid();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Sales Detail Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("SalesType", 80);
            dgPrinter.ColumnWidths.Add("Name", 80);
            dgPrinter.ColumnWidths.Add("SalesNumber", 80);
            dgPrinter.ColumnWidths.Add("TransactionDate", 70);
            dgPrinter.ColumnWidths.Add("ShipQty", 70);
            dgPrinter.ColumnWidths.Add("PartNumber", 80);
            dgPrinter.ColumnWidths.Add("Description", 80);
            dgPrinter.ColumnWidths.Add("GrandTotal", 80);
            dgPrinter.ColumnWidths.Add("TaxTotal", 80);
            dgPrinter.ColumnWidths.Add("TotalDue", 80);
            dgPrinter.ColumnWidths.Add("TaxCode", 80);
            dgPrinter.ColumnWidths.Add("InvoiceStatus", 80);
            dgPrinter.ColumnWidths.Add("PromiseDate", 70);
            
            dgPrinter.PageSettings.Landscape = true;
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.PrintPreviewDataGridView(dgReport);

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    // app.Visible = false;
                    ws.Cells[4, 1] = "Sales Type";
                    ws.Cells[4, 2] = "Customer Name";
                    ws.Cells[4, 3] = "Sales Number";
                    ws.Cells[4, 4] = "Date";
                    ws.Cells[4, 5] = "Qty";
                    ws.Cells[4, 6] = "Part Number";
                    ws.Cells[4, 7] = "Description";
                    ws.Cells[4, 8] = "Total Amount";
                    ws.Cells[4, 9] = "Total Tax";
                    ws.Cells[4, 10] = "Balance";
                    ws.Cells[4, 11] = "Tax";
                    ws.Cells[4, 12] = "Status";
                    ws.Cells[4, 13] = "Promise Date";
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {
                        ws.Cells[i, 1] = item.Cells[0].Value.ToString();
                        ws.Cells[i, 2] = item.Cells[1].Value.ToString();
                        ws.Cells[i, 3] = item.Cells[2].Value.ToString();

                        if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                        {
                            ws.Cells[i, 4] = Convert.ToDateTime(item.Cells[3].Value.ToString()).ToShortDateString();
                        }

                        ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                        ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        ws.Cells[i, 8] = Math.Round(float.Parse(item.Cells[7].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        ws.Cells[i, 9] = Math.Round(float.Parse(item.Cells[8].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        ws.Cells[i, 10] = Math.Round(float.Parse(item.Cells[9].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        ws.Cells[i, 11] = item.Cells[10].Value.ToString();
                        ws.Cells[i, 12] = item.Cells[11].Value.ToString();
                        if (item.Cells[12].Value != null && item.Cells[12].Value.ToString() != "")
                        {
                            ws.Cells[i, 13] = Convert.ToDateTime(item.Cells[12].Value.ToString()).ToShortDateString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "M3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Sales Details Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "M4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales Details Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
