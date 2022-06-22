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

namespace AbleRetailPOS.Reports.SalesReports
{
    public partial class rptCusomizerSalesSummary : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private string GridSortMode = "ASC";
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection con;
        String SalesType = "INVOICE";
        String InvoiceStatus;
        string selectSql;
        string reportName;
        string reportTitle;
        string shippingID;
        string salespersonID;
        DateTime TimeNow = DateTime.Now;
        private bool CanView = false;

        public rptCusomizerSalesSummary()
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
        private void rptCusomizerSalesSummary_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
        private void GetReportData()
        {
            selectSql = @"SELECT Name, SalesNumber, 
                                         TransactionDate,
                                         GrandTotal,
                                         TaxTotal,
                                         TotalDue,
                                         InvoiceStatus,
                                         PromiseDate
                                FROM Sales 
                                INNER JOIN Profile ON Sales.CustomerId = Profile.ID 
                                WHERE TransactionDate BETWEEN @sdate AND @edate 
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
            //selectSql += "AND PromiseDate BETWEEN @pdateStart AND @pdateEnd";
            if (DateRange1.Text != "DATE RANGE" && DateRange2.Text != "DATE RANGE")
            {
                selectSql += "AND Sales.PromiseDate BETWEEN @psdate AND @pedate";
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
                DateTime pedate = Convert.ToDateTime(pdateTo.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();
              


                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@psdate", psdate);
                cmd.Parameters.AddWithValue("@pedate", pedate);

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
            
           if(pViewType == 0)
            {
                reportName = "SalesReport.rpt";
                reportTitle = "Sales Report Summary";               
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

                CalculateTotal(1);
                this.dgReport.DataSource = TbGrid;
                for (int i = 0; i < this.dgReport.Rows.Count; i++)
                {
                    if (this.dgReport.Rows[i].Cells["TransactionDate"].Value != null)
                    {
                        if (this.dgReport.Rows[i].Cells["TransactionDate"].Value.ToString() != "")
                        {
                            this.dgReport.Rows[i].Cells["TransactionDate"].Value = Convert.ToDateTime(this.dgReport.Rows[i].Cells["TransactionDate"].Value.ToString()).ToShortDateString();
                        }
                    }
                }
                for (int i = 0; i < this.dgReport.Rows.Count; i++)
                {
                    if (this.dgReport.Rows[i].Cells["PromiseDate"].Value != null)
                    {
                        if (this.dgReport.Rows[i].Cells["PromiseDate"].Value.ToString() != "")
                        {
                            this.dgReport.Rows[i].Cells["PromiseDate"].Value = Convert.ToDateTime(this.dgReport.Rows[i].Cells["PromiseDate"].Value.ToString()).ToShortDateString();
                        }
                    }
                }
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
            rw[3] = TotalAmount;
            rw[4] = TotalTax;
            rw[5] = TotalBalance;
            TbGrid.Rows.Add(rw);
        }

        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Customer Name";
            this.dgReport.Columns[1].HeaderText = "Sales Number";
            this.dgReport.Columns[2].HeaderText = "Date";
            this.dgReport.Columns[3].HeaderText = "Sale Amount";
            this.dgReport.Columns[4].HeaderText = "Tax";
            this.dgReport.Columns[5].HeaderText = "Balance";
            this.dgReport.Columns[6].HeaderText = "Status";
            this.dgReport.Columns[7].HeaderText = "Promise Date";
            this.dgReport.Columns[3].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[4].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[5].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
        }
        private void FillSortCombo()
        {
            if (this.cbSort.Items.Count == 0)
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
        private void btnDisplay_Click(object sender, EventArgs e)
        {

            GetReportData();
            LoadReport(1);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(0);
        }

       

        private void cmbSalesStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalesStatus.Text == "All Invoices")
            {
                SalesType = "INVOICE";
                
            } else if (cmbSalesStatus.Text == "Open Sales")
            {
                InvoiceStatus = "OPEN";
                SalesType = "INVOICE";
            }else if (cmbSalesStatus.Text == "Order")
            {
                InvoiceStatus = "Order";
                SalesType = "ORDER";
            }else if (cmbSalesStatus.Text == "Quote")
            {
                InvoiceStatus = "Quote";
                SalesType = "QUOTE";
            }
        }

        private void Shippingmethod_btn_Click(object sender, EventArgs e)
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;
                txtShipVia.Text = ShipList[0];
                shippingID = ShipList[1];
            }
        }

        private void pbSalesperson_Click(object sender, EventArgs e)
        {
            SalespersonLookup SalespersonDlg = new SalespersonLookup();
            if (SalespersonDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lSales = SalespersonDlg.GetSalesperson;
                salespersonID = lSales[0];
                txtEmployee.Text = lSales[1];

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
                pdateFrom.Refresh();
                pdateTo.Refresh();
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

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();
           
            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Sales Summary Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("Name", 150);
            dgPrinter.ColumnWidths.Add("SalesNumber", 80);
            dgPrinter.ColumnWidths.Add("TransactionDate", 70);
            dgPrinter.ColumnWidths.Add("GrandTotal", 80);
            dgPrinter.ColumnWidths.Add("TaxTotal", 80);
            dgPrinter.ColumnWidths.Add("TotalDue", 80);
            dgPrinter.ColumnWidths.Add("InvoiceStatus", 80);
            dgPrinter.ColumnWidths.Add("PromiseDate", 70);          
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;          
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;           
            dgPrinter.FooterSpacing = 15;            
            dgPrinter.PrintPreviewDataGridView(dgReport);

        }

       
       
        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            string lSortMode = (rdoAsc.Checked == true ? "asc" : "desc");
            CalculateTotal(this.cbSort.SelectedIndex, lSortMode);
            this.dgReport.DataSource = TbGrid;
            FormatGrid();
        }

        private void btnExportExcell_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    ws.Cells[4, 1] = "Customer Name";
                    ws.Cells[4, 2] = "Sales Number";
                    ws.Cells[4, 3] = "Date";
                    ws.Cells[4, 4] = "Sale Amount";
                    ws.Cells[4, 5] = "Tax";
                    ws.Cells[4, 6] = "Balance";
                    ws.Cells[4, 7] = "Invoice Status";
                    ws.Cells[4, 8] = "Promise Date";
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {
                        ws.Cells[i, 1] = item.Cells[0].Value.ToString();
                        ws.Cells[i, 2] = item.Cells[1].Value.ToString();
                        if (item.Cells[2].Value != null && item.Cells[2].Value.ToString() != "")
                        {
                            ws.Cells[i, 3] = Convert.ToDateTime(item.Cells[2].Value.ToString()).ToShortDateString();
                        }
                        
                        ws.Cells[i, 4] = Math.Round(float.Parse(item.Cells[3].Value.ToString()),2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        ws.Cells[i, 5] = Math.Round(float.Parse(item.Cells[4].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        ws.Cells[i, 6] = Math.Round(float.Parse(item.Cells[5].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            ws.Cells[i, 8] = Convert.ToDateTime(item.Cells[7].Value.ToString()).ToShortDateString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "H3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Sales Summary Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "H4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();
                   
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales Summary Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
