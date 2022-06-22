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
using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;

namespace AbleRetailPOS.Reports.PurchaseReports
{
    public partial class AllPurchaseReport : Form
    {
        System.Data.DataTable TbRep;
        System.Data.DataTable TbGrid;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;
        public AllPurchaseReport()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
            }
        }

        private void LoadReportAllPurchases()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT 
                    p.TransactionDate,
                    p.PurchaseNumber,
                    p.SupplierINVNumber,
                    pro.Name,
                    p.GrandTotal,
                    p.POStatus,
                    p.TaxTotal,
                    p.TotalDue, 
                    p.PromiseDate,
                    p.PurchaseType,
                    pl.ReceiveQty 
                    FROM Purchases p INNER JOIN Profile pro ON p.SupplierID = pro.ID 
                    LEFT JOIN PurchaseLines pl ON pl.PurchaseID = p.PurchaseID 
                    Where p.TransactionDate BETWEEN @sdate and @edate ";

                if (PurchaseStatuscb.Text != "")
                {
                    if (PurchaseStatuscb.Text == "New")
                    {
                        sql += " AND p.POStatus  = 'New' ";
                    }
                    else if (PurchaseStatuscb.Text == "Active")
                    {
                        sql += " AND p.POStatus  = 'Active' ";
                    }
                    else if (PurchaseStatuscb.Text == "Backordered")
                    {
                        sql += " AND p.POStatus  = 'Backordered' ";
                    }
                    else if (PurchaseStatuscb.Text == "Completed")
                    {
                        sql += " AND p.POStatus = 'Completed' ";
                    }
                   
                }
                if (txtShipVia.Text != "")
                {
                    sql += " AND p.ShippingMethodID = "+ ShippingID;
                }
                if(toNum.Value != 0)
                {
                    sql += " AND p.GrandTotal BETWEEN '" + fromNum.Value+ "' AND ' " + toNum.Value + "'";
                }
                if(promised== true)
                {
                    sql += " AND p.PromiseDate BETWEEN @psdate AND @pedate ";
                }
                if (txtEmployee.Text != "")
                {
                    sql += " AND UserID = " + EmployeeID;
                }
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                DateTime sdate = Convert.ToDateTime(sdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                DateTime psdate = PSdateTimePicker.Value;
                DateTime pedate = PEdateTimePicker.Value;
                pedate = new DateTime(pedate.Year, pedate.Month, pedate.Day, 00, 00, 00);
                psdate = new DateTime(psdate.Year, psdate.Month, psdate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@psdate", psdate);
                cmd.Parameters.AddWithValue("@pedate", pedate);

                SqlDataAdapter da = new SqlDataAdapter();
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReportAllPurchases();
            ReportParams AllPurchases = new ReportParams();
            AllPurchases.PrtOpt = 1;
            AllPurchases.Rec.Add(TbRep);
            AllPurchases.ReportName = "AllPurchases.rpt";
            AllPurchases.RptTitle = "Purchase Register [All Purchases]";

            AllPurchases.Params = "compname|filter";
            AllPurchases.PVals = CommonClass.CompName.Trim() + "|P.O.Status:" + PurchaseStatuscb.Text;

            CommonClass.ShowReport(AllPurchases);
            Cursor.Current = Cursors.Default;
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

        private void AllPurchaseReport_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
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
            if(DateRangesStart.SelectedIndex == 0)
            {
                promised = false;
            }
        }

        private void DateRangesEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DateRangesEnd.SelectedIndex  != 0)
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

        private void toNum_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReportAllPurchases();
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
               // rw[5] = TotalDue();
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
        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Transaction Date";
            this.dgReport.Columns[1].HeaderText = "Purchase Number";
            this.dgReport.Columns[2].HeaderText = "Supplier Inv #";
            this.dgReport.Columns[3].HeaderText = "Supplier Name";
            this.dgReport.Columns[4].HeaderText = "Total Amount";
            this.dgReport.Columns[5].HeaderText = "PO Status";
            this.dgReport.Columns[6].Visible = false;
            this.dgReport.Columns[7].Visible = false;
            this.dgReport.Columns[8].Visible = false;
            this.dgReport.Columns[9].Visible = false;
            this.dgReport.Columns[10].Visible = false;


            this.dgReport.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgReport.Columns[4].DefaultCellStyle.Format = "C2";
           // this.dgReport.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";
            //   this.dgReport.Columns[6].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? " asc" : " desc");
            index = cmbSort.SelectedIndex;
            button1.PerformClick();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Purchase Register [All Purchase]";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("TransactionDate", 100);
            dgPrinter.ColumnWidths.Add("PurchaseNumber", 80);
            dgPrinter.ColumnWidths.Add("SupplierINVNumber", 100);
           // dgPrinter.ColumnWidths.Add("Name", 100);
            // dgPrinter.ColumnWidths.Add("ClosedDate", 100);
          //  dgPrinter.ColumnWidths.Add("TotalDue", 100);
            dgPrinter.ColumnWidths.Add("GrandTotal", 100);
            dgPrinter.ColumnWidths.Add("POStatus", 100);
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
                    ws.Cells[4, 2] = "Purchase Number";
                    ws.Cells[4, 3] = "Supplier Inv #";
                    ws.Cells[4, 4] = "Supplier Name";
                    ws.Cells[4, 5] = "Total Amount";
                    ws.Cells[4, 6] = "PO Status";
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
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "F3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Purchases Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "F4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Purchases Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
