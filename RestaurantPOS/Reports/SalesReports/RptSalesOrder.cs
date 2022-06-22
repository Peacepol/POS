using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Reports.SalesReports
{
    public partial class RptSalesOrder : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        string selectSql = "";
        string reportName;
        string reportTitle;
        string shippingID;
        string salespersonID;
        bool promised = false;
        private int index = 1;
        private bool CanView = false;
        private string sort = " asc";

        public RptSalesOrder()
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
        void LoadReport()
        {
            selectSql = @"SELECT TransactionDate, 
                                SalesNumber, 
                                 CustomerPONumber,
                                    Name, 
                                GrandTotal, 
                                TotalDue, 
                                PromiseDate 
                            FROM Sales s 
                            INNER JOIN Profile p ON s.CustomerID = p.ID 
                            WHERE TransactionDate BETWEEN @sdate AND @edate ";
                selectSql += " AND SalesType = 'ORDER'AND InvoiceStatus = 'Order' ";

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

            if (promised == true)
            {
                selectSql += " AND p.PromiseDate BETWEEN @psdate AND @pedate";
            }
            reportName = "OpenSales.rpt";
                reportTitle = "Sales Register [Orders]";
               
                DateTime sdate = sdatePicker.Value;
                DateTime edate = edatePicker.Value;
                DateTime psdate = PSdateTimePicker.Value;
                DateTime pedate = PEdateTimePicker.Value;

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
            Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@sdate", sdate);
                param.Add("@edate", edate);
                param.Add("@psdate", psdate.ToUniversalTime());
                param.Add("@pedate", pedate.ToUniversalTime());
                TbRep = new System.Data.DataTable();
             CommonClass.runSql(ref TbRep, selectSql, param);
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            Reports.ReportParams openinvoiceorders = new Reports.ReportParams();
            openinvoiceorders.PrtOpt = 1;
            openinvoiceorders.Rec.Add(TbRep);
            openinvoiceorders.ReportName = reportName;
            openinvoiceorders.RptTitle = reportTitle;
            openinvoiceorders.Params = "compname|SubTitle";
            openinvoiceorders.PVals = CommonClass.CompName.Trim()+"|" + reportTitle;

            CommonClass.ShowReport(openinvoiceorders);
            Cursor.Current = Cursors.Default;
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
                //   rw[5] = TotalDue();
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
            this.dgReport.Columns[6].HeaderText = "Promised Date";
            this.dgReport.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgReport.Columns[4].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[5].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            dgPrinter.SubTitle = "Closed Invoice Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("TransactionDate", 100);
            dgPrinter.ColumnWidths.Add("SalesNumber", 80);
            dgPrinter.ColumnWidths.Add("CustomerPONumber", 100);
            dgPrinter.ColumnWidths.Add("Name", 100);
            // dgPrinter.ColumnWidths.Add("ClosedDate", 100);
            dgPrinter.ColumnWidths.Add("TotalDue", 100);
            dgPrinter.ColumnWidths.Add("GrandTotal", 100);
            dgPrinter.ColumnWidths.Add("PromiseDate", 100);
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
                    ws.Cells[4, 7] = "Promised Date";
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
                        if (item.Cells[4].Value != null && item.Cells[4].Value.ToString() != "")
                        {
                            ws.Cells[i, 5] = Math.Round(float.Parse(item.Cells[4].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            ws.Cells[i, 6] = Math.Round(float.Parse(item.Cells[5].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                        {
                            ws.Cells[i, 7] = Convert.ToDateTime(item.Cells[6].Value.ToString()).ToShortDateString();
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
                    ws.Cells[1, 1] = "Sales Order Report";

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
                    MessageBox.Show("Sales Order Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptSalesOrder_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
