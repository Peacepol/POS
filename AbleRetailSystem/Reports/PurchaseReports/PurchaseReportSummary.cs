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

namespace RestaurantPOS.Reports.PurchaseReports
{
    public partial class rptPurchaseReportSummary : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public rptPurchaseReportSummary()
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

        private void LoadReport()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT Name, PurchaseNumber,TransactionDate, SubTotal, FreightSubTotal, TaxTotal, GrandTotal, POStatus, PromiseDate
                FROM Purchases p INNER JOIN Profile ON p.SupplierID = Profile.ID WHERE TransactionDate BETWEEN @sdate AND @edate order by Name";
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
                    sql += " AND p.GrandTotal BETWEEN '" + fromNum.Value + "' AND ' " + toNum.Value + "'";
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
                            
                DateTime sdate = Convert.ToDateTime(sdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime edate = Convert.ToDateTime(edateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                DateTime psdate = Convert.ToDateTime(PSdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime pedate = Convert.ToDateTime(PEdateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                cmd.Parameters.AddWithValue("@sdate", sdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@psdate", psdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@pedate", pedate.ToUniversalTime());

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbShip_Click(object sender, EventArgs e)
        {
            ShowShippingmethod();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            Reports.ReportParams PurchaseSummary = new Reports.ReportParams();
            PurchaseSummary.PrtOpt = 1;
            PurchaseSummary.Rec.Add(TbRep);
            PurchaseSummary.ReportName = "PurchaseReport.rpt";
            PurchaseSummary.RptTitle = "Purchase Report Summary";

            PurchaseSummary.Params = "compname";
            PurchaseSummary.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(PurchaseSummary);
            Cursor.Current = Cursors.Default;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            LoadReport();
            TbGrid = TbRep.Copy();
            string lPrevItem = "";
            DataRow rw;
            string[] RowArray;
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = "Name " + sort;
                TbGrid = dv.ToTable();
                int rIndex = 0;
                // dgReport.DataSource = TbGrid;
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    if (lPrevItem != dr["Name"].ToString() && lPrevItem != "")
                    {
                        RowArray = new string[9];
                        RowArray[0] = "TOTAL :";
                        RowArray[2] = GrandTotal(lPrevItem).ToString("C2");
                        RowArray[3] = TotalTax(lPrevItem).ToString("C2");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                    }
                    if (lPrevItem != dr["Name"].ToString())
                    {
                        RowArray = new string[9];
                        RowArray[0] = dr["Name"].ToString();
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray[0] = dr["PurchaseNumber"].ToString();
                        RowArray[1] = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd-MM-yyyy");
                        RowArray[2] = decimal.Parse(dr["GrandTotal"].ToString()).ToString("C2");
                        RowArray[3] = decimal.Parse(dr["TaxTotal"].ToString()).ToString("C2");
                        RowArray[4] = decimal.Parse(dr["SubTotal"].ToString()).ToString("C2");
                        RowArray[5] = decimal.Parse(dr["FreightSubTotal"].ToString()).ToString("C2");
                        RowArray[6] = dr["POStatus"].ToString();
                        RowArray[7] = DateTime.Parse(dr["PromiseDate"].ToString()).ToString("dd-MM-yyyy");
                        dgReport.Rows.Add(RowArray);
                        lPrevItem = dr["Name"].ToString();
                    }
                    else
                    {
                        RowArray = new string[9];
                        RowArray[0] = dr["PurchaseNumber"].ToString();
                        RowArray[1] = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd-MM-yyyy");
                        RowArray[2] = decimal.Parse(dr["GrandTotal"].ToString()).ToString("C2");
                        RowArray[3] = decimal.Parse(dr["TaxTotal"].ToString()).ToString("C2");
                        RowArray[4] = decimal.Parse(dr["SubTotal"].ToString()).ToString("C2");
                        RowArray[5] = decimal.Parse(dr["FreightSubTotal"].ToString()).ToString("C2");
                        RowArray[6] = dr["POStatus"].ToString();
                        RowArray[7] = DateTime.Parse(dr["PromiseDate"].ToString()).ToString("dd-MM-yyyy");
                        dgReport.Rows.Add(RowArray);
                    }
                   
                    if (TbRep.Rows.Count - 1 == i)
                    {//last purchase total
                        RowArray = new string[9];
                        RowArray[0] = "TOTAL :";
                        RowArray[2] = GrandTotal(lPrevItem).ToString("C2");
                        RowArray[3] = TotalTax(lPrevItem).ToString("C2");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                        //Grandtotal
                        RowArray = new string[9];
                        RowArray[0] = "GRAND TOTAL:";
                        RowArray[2] = GrandTotal().ToString("C2");
                        RowArray[3] = TotalTax().ToString("C2");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    }
                       
                }
                FormatGrid();
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
              //  FillSortCombo();

            }
            else
            {
                MessageBox.Show("Contains No Data.", "Report Information");
            }
            Cursor.Current = Cursors.Default;
        }
        void FormatGrid()
        {
            this.dgReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        public decimal GrandTotal(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
              if (dr["Name"].ToString() == ItemName)
                {
                    x += decimal.Parse(dr["GrandTotal"].ToString());
                }
            }
            return x;
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
        public decimal TotalTax(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (dr["Name"].ToString() == ItemName)
                {
                    x += decimal.Parse(dr["TaxTotal"].ToString());
                }
            }
            return x;
        }
        public decimal TotalTax()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["TaxTotal"].ToString());
            }
            return x;
        }
        //private void FillSortCombo()
        //{
        //    if (this.cmbSort.Items.Count == 0)
        //    {
        //        for (int i = 0; i < dgReport.ColumnCount; i++)
        //        {
        //            this.cmbSort.Items.Add(dgReport.Columns[i].HeaderText);
        //        }
        //        this.cmbSort.Enabled = true;
        //        this.btnSortGrid.Enabled = true;
        //        this.cmbSort.SelectedIndex = 0;
        //    }
        //}
      

        private void rptPurchaseReportSummary_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Purchase Report Summary";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("TransDate", 70);
            dgPrinter.ColumnWidths.Add("PurchaseNum", 100);
            dgPrinter.ColumnWidths.Add("CustomerPONumber", 100);
            dgPrinter.ColumnWidths.Add("Amount", 100);
            dgPrinter.ColumnWidths.Add("Tax", 100);
            dgPrinter.ColumnWidths.Add("SubTotal", 100);
            dgPrinter.ColumnWidths.Add("FreightSubTotal", 100);
            dgPrinter.ColumnWidths.Add("PDate", 70);
            dgPrinter.ColumnWidths.Add("Status", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            // dgPrinter.printDocument.DefaultPageSettings.Landscape = false;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            //sort = (rdoAsc.Checked == true ? " asc" : " desc");
            //index = cmbSort.SelectedIndex;
            //btnDisplay.PerformClick();
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

                    ws.Cells[4, 1] = "Purchase Number";
                    ws.Cells[4, 2] = "Date";
                    ws.Cells[4, 3] = "Grand Total";
                    ws.Cells[4, 4] = "Total Tax";
                    ws.Cells[4, 5] = "Sub Total";
                    ws.Cells[4, 6] = "Freight Sub Total";
                    ws.Cells[4, 7] = "Status";
                    ws.Cells[4, 8] = "Promised Date";
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {

                        if (item.Cells[0].Value != null)
                        {
                            ws.Cells[i, 1] = item.Cells[0].Value.ToString();
                        }
                        if (item.Cells[1].Value != null && item.Cells[1].Value.ToString() != "")
                        {
                            ws.Cells[i, 2] = Convert.ToDateTime(item.Cells[1].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[2].Value != null && item.Cells[2].Value.ToString() != "")
                        {
                            ws.Cells[i, 3] = item.Cells[2].Value.ToString();
                        }
                        if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                        {
                            ws.Cells[i, 4] = item.Cells[3].Value.ToString();
                        }
                        if (item.Cells[4].Value != null && item.Cells[4].Value.ToString() != "")
                        {
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            ws.Cells[i, 8] = Convert.ToDateTime(item.Cells[1].Value.ToString()).ToShortDateString();
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
                    ws.Cells[1, 1] = "Purchase Summary Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "H4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Purchase Summary Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
