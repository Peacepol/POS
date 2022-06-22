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
    public partial class RptSupplierItemSummary : Form
    {
        System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public RptSupplierItemSummary()
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

     
        private void LoadReportItemSummary()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.EntryDate, 
                        PromiseDate,
                        f.Name,
                        i.PartNumber,
                        i.ItemNumber,
                        i.ItemName,                        
                        l.OrderQty, 
                        l.ReceiveQty,
                        l.TotalAmount,
                        p.POStatus
                        FROM Purchases p INNER JOIN Profile f ON p.SupplierID = f.ID
                        INNER JOIN PurchaseLines l ON l.PurchaseID = p.PurchaseID
                        INNER JOIN Items i ON i.SupplierID = p.SupplierID
                        WHERE p.LayoutType = 'Item' AND p.TransactionDate BETWEEN @sdate AND @edate";
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
                if (promised == true)
                {
                    sql += " AND p.PromiseDate BETWEEN @psdate AND @pedate ";
                }
                if (toNum.Value != 0)
                {
                    sql += " AND p.GrandTotal BETWEEN '" + fromNum.Value + "' AND ' " + toNum.Value + "'";
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
                psdate = new DateTime(psdate.Year, psdate.Month, psdate.Day, 00, 00, 00);
                pedate = new DateTime(pedate.Year, pedate.Month, pedate.Day, 23, 59, 59);
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

        private void pbShipVia_Click(object sender, EventArgs e)
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            LoadReportItemSummary();
            TbGrid = TbRep.Copy();
            string lPrevItem = "";
            string lPrevItemName = "";
            DataRow rw;
            string[] RowArray;
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort ="ItemNumber asc";
                TbGrid = dv.ToTable();
                int rIndex = 0;
                // dgReport.DataSource = TbGrid;
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    if (lPrevItem != dr["ItemNumber"].ToString() && lPrevItem != "")
                    {
                        RowArray = new string[9];
                        RowArray[0] = lPrevItemName + " TOTAL :";
                        RowArray[4] = TotalOrdered(lPrevItem).ToString("F");
                        RowArray[5] = TotalReceived(lPrevItem).ToString("F");
                        RowArray[6] = GrandTotal(lPrevItem).ToString("C");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                    }
                    if (lPrevItem != dr["ItemNumber"].ToString())
                    {
                        RowArray = new string[9];
                        RowArray[0] = dr["ItemNumber"].ToString();
                        RowArray[2] = dr["ItemName"].ToString();
                        RowArray[3] = dr["PartNumber"].ToString();
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray = new string[9];
                        RowArray[1] = dr["Name"].ToString();
                        RowArray[4] = decimal.Parse(dr["OrderQty"].ToString()).ToString("F");
                        RowArray[5] = decimal.Parse(dr["ReceiveQty"].ToString()).ToString("F");
                        RowArray[6] = decimal.Parse(dr["TotalAmount"].ToString()).ToString("C");
                        dgReport.Rows.Add(RowArray);

                    }
                    else
                    {
                        RowArray = new string[9];
                        RowArray[1] = dr["Name"].ToString();
                        RowArray[4] = decimal.Parse(dr["OrderQty"].ToString()).ToString("F");
                        RowArray[5] = decimal.Parse(dr["ReceiveQty"].ToString()).ToString("F");
                        RowArray[6] = decimal.Parse(dr["TotalAmount"].ToString()).ToString("C");
                        dgReport.Rows.Add(RowArray);
                    }
                    if (TbRep.Rows.Count - 1 == i)
                    {//last purchase total
                        RowArray = new string[9];
                        RowArray[0] = lPrevItemName + " TOTAL :";
                        RowArray[4] = TotalOrdered(lPrevItem).ToString("F");
                        RowArray[5] = TotalReceived(lPrevItem).ToString("F");
                        RowArray[6] = GrandTotal(lPrevItem).ToString("C");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                        //Grandtotal
                        RowArray = new string[9];
                        RowArray[0] = "GRAND TOTAL:";
                        RowArray[4] = TotalOrdered().ToString("F");
                        RowArray[5] = TotalReceived().ToString("F");
                        RowArray[6] = GrandTotal().ToString("C");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    }
                    lPrevItemName = dr["ItemName"].ToString();
                    lPrevItem = dr["ItemNumber"].ToString();
                }
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
        public decimal GrandTotal(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if(ItemName == dr["ItemNumber"].ToString())
                {
                    x += decimal.Parse(dr["TotalAmount"].ToString());
                }
              
            }
            return x;
        }
        public decimal TotalReceived(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["ItemNumber"].ToString())
                {
                    x += decimal.Parse(dr["ReceiveQty"].ToString());
                }
            }
            return x;
        }
        public decimal TotalOrdered(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["ItemNumber"].ToString())
                {
                    x += decimal.Parse(dr["OrderQty"].ToString());
                }

            }
            return x;
        }
        private void FillSortCombo()
        {
            //if (this.cmbSort.Items.Count == 0)
            //{
            //    for (int i = 0; i < dgReport.ColumnCount; i++)
            //    {
            //        this.cmbSort.Items.Add(dgReport.Columns[i].HeaderText);
            //    }
            //    this.cmbSort.Enabled = true;
            //    this.btnSortGrid.Enabled = true;
            //    this.cmbSort.SelectedIndex = 0;
            //}
        }
        public decimal TotalOrdered()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["OrderQty"].ToString());
            }
            return x;
        }
        public decimal TotalReceived()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["ReceiveQty"].ToString());
            }
            return x;
        }
        public decimal GrandTotal()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["TotalAmount"].ToString());
            }
            return x;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReportItemSummary();
            Reports.ReportParams ItemSummary = new Reports.ReportParams();
            ItemSummary.PrtOpt = 1;
            ItemSummary.Rec.Add(TbRep);
            ItemSummary.ReportName = "PurchaseItemSummary.rpt";
            ItemSummary.RptTitle = "Supplier [Item Summary]";

            ItemSummary.Params = "compname";
            ItemSummary.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(ItemSummary);
            Cursor.Current = Cursors.Default;
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Purchase[Item Summary]";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ItemNum", 150);
            dgPrinter.ColumnWidths.Add("Supplier",100);
            dgPrinter.ColumnWidths.Add("ItemName", 100);
            dgPrinter.ColumnWidths.Add("PartNumber", 100);
            dgPrinter.ColumnWidths.Add("OrderQty", 80);
             dgPrinter.ColumnWidths.Add("ReceiveQty", 80);
            dgPrinter.ColumnWidths.Add("TotalAmount", 100);
            //  dgPrinter.ColumnWidths.Add("", 100);
            //dgPrinter.ColumnWidths.Add("POStatus", 100);
            ////dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = false;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            //sort = (rdoAsc.Checked == true ? " asc" : " desc");
            //index = cmbSort.SelectedIndex;
            //btnDisplay.PerformClick();
        }
        private void FormatGrid()
        {
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          //  this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
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

                    ws.Cells[4, 1] = "Item Number";
                    ws.Cells[4, 2] = "Supplier";
                    ws.Cells[4, 3] = "Item Name";
                    ws.Cells[4, 4] = "Part Number";
                    ws.Cells[4, 5] = "Order Qty";
                    ws.Cells[4, 6] = "Received Qty";
                    ws.Cells[4, 7] = "Total Amount";

                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {

                        if (item.Cells[0].Value != null)
                        {
                            ws.Cells[i, 1] = item.Cells[0].Value.ToString();
                        }
                        if (item.Cells[1].Value != null && item.Cells[1].Value.ToString() != "")
                        {
                            ws.Cells[i, 2] = item.Cells[1].Value.ToString();
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
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "G3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Supplier Item Summary Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "G4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Supplier Item Summary Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptSupplierItemSummary_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
