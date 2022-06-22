using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
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

namespace RestaurantPOS.Reports.SalesReports
{
    public partial class RptAllSales : Form
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
        bool promised = false;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public RptAllSales()
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

        private void cmbSalesStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            Reports.ReportParams SalesSummary = new Reports.ReportParams();
            SalesSummary.PrtOpt = 1;
            SalesSummary.Rec.Add(TbRep);
            SalesSummary.ReportName = "AllSales.rpt";
            SalesSummary.RptTitle = "All Sales";
            SalesSummary.Params = "compname";
            SalesSummary.PVals = CommonClass.CompName.Trim();
            CommonClass.ShowReport(SalesSummary);
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


        private void RptAllSales_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                dgReport.DataSource = TbGrid;
                DataRow rw = TbGrid.NewRow();
                rw[1] = "TOTAL";
                rw[4] = GrandTotal();
                rw[5] = TotalDue();
                TbGrid.Rows.Add(rw);
                //  dgReport.Columns["PartNumber"].Visible = false;
                FormatGrid();
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                FillSortCombo();

            }
            else
            {
                MessageBox.Show("Contains No Data.", "Report Information");
            }
            Cursor.Current = Cursors.Default;
        }
        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Transaction Date";
            this.dgReport.Columns[1].HeaderText = "Sales Number";
            this.dgReport.Columns[2].HeaderText = "Customer PO #";
            this.dgReport.Columns[3].HeaderText = "Customer";
            this.dgReport.Columns[4].HeaderText = "Total Amount";
            this.dgReport.Columns[5].HeaderText = "Balance Due";
            this.dgReport.Columns[6].HeaderText = "Status";
            this.dgReport.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";

            this.dgReport.Columns[4].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[5].DefaultCellStyle.Format = "C2";
         //   this.dgReport.Columns[6].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
        }
        public decimal GrandTotal()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["GrandTotal"].ToString());
            }
            return x;
        }
        public decimal TotalDue()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["TotalDue"].ToString());
            }
            return x;
        }
        private void FillSortCombo()
        {
            if (this.cmbSort.Items.Count == 0)
            {
                for (int i = 0; i < dgReport.ColumnCount; i++)
                {
                    this.cmbSort.Items.Add(dgReport.Columns[i].HeaderText);
                }
                this.cmbSort.Enabled = true;
                this.btnSortGrid.Enabled = true;
                this.cmbSort.SelectedIndex = 0;
            }
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? " asc" : " desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "All Sales Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
           dgPrinter.ColumnWidths.Add("TransactionDate", 90);
            dgPrinter.ColumnWidths.Add("SalesNumber", 80);
            dgPrinter.ColumnWidths.Add("CustomerPONumber",80);
            dgPrinter.ColumnWidths.Add("Name", 150);
            dgPrinter.ColumnWidths.Add("GrandTotal", 120);
           dgPrinter.ColumnWidths.Add("TotalDue", 120);
            dgPrinter.ColumnWidths.Add("InvoiceStatus", 80);
            //dgPrinter.ColumnWidths.Add("OnOrder", 80);
            //dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
           // dgPrinter.printDocument.DefaultPageSettings.Landscape = false;
            dgPrinter.PrintPreviewDataGridView(dgReport);
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
                    ws.Cells[4, 1] = "Transaction Date";
                    ws.Cells[4, 2] = "Sales Number";
                    ws.Cells[4, 3] = "Customer PO #";
                    ws.Cells[4, 4] = "Customer";
                    ws.Cells[4, 5] = "Total Amount";
                    ws.Cells[4, 6] = "Balance Due";
                    ws.Cells[4, 7] = "Invoice Status";
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {

                        if (item.Cells[0].Value != null && item.Cells[0].Value.ToString() != "")
                        {
                            ws.Cells[i, 1] = Convert.ToDateTime(item.Cells[0].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[1].Value != null)
                        {
                            ws.Cells[i, 2] = item.Cells[1].Value.ToString();
                        }
                        if (item.Cells[2].Value != null)
                        {
                            ws.Cells[i, 3] = item.Cells[2].Value.ToString();
                        }
                        if (item.Cells[3].Value != null)
                        {
                            ws.Cells[i, 4] = item.Cells[3].Value.ToString();
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = Math.Round(float.Parse(item.Cells[4].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 6] = Math.Round(float.Parse(item.Cells[5].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "G3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Sales Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "G4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
