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

namespace AbleRetailPOS.Reports.PurchaseReports
{
    public partial class RptSupplierItemDetail : Form
    {
        System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public RptSupplierItemDetail()
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
        private void LoadReportItemDetails()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.TransactionDate, 
                        PromiseDate, 
                        f.Name, 
                        p.PurchaseNumber, 
                        p.POStatus,
                        i.PartNumber,
                        i.ItemNumber, 
                        i.ItemName, 
                        l.OrderQty, 
                        l.ReceiveQty,
                        l.TotalAmount
                        FROM Purchases p INNER JOIN Profile f ON p.SupplierID = f.ID
                        INNER JOIN PurchaseLines l ON l.PurchaseID = p.PurchaseID
                        INNER JOIN Items i ON i.SupplierID = p.SupplierID
                        WHERE p.TransactionDate BETWEEN @sdate AND @edate ";
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

        private void pbShipVia_Click(object sender, EventArgs e)
        {
            ShowShippingmethod();
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



        private void RptSupplierItemDetail_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReportItemDetails();
            Reports.ReportParams ItemDetails = new Reports.ReportParams();
            ItemDetails.PrtOpt = 1;
            ItemDetails.Rec.Add(TbRep);
            ItemDetails.ReportName = "PurchaseItemDetail.rpt";
            ItemDetails.RptTitle = "Supplier [Item Detail]";

            ItemDetails.Params = "compname";
            ItemDetails.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(ItemDetails);
            Cursor.Current = Cursors.Default;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            LoadReportItemDetails();
            TbGrid = TbRep.Copy();
            string lPrevItem = "";
            string lPrevItemName = "";
            DataRow rw;
            string[] RowArray;
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = "ItemNumber asc";
                TbGrid = dv.ToTable();
                int rIndex = 0;
                // dgReport.DataSource = TbGrid;
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    if (lPrevItem != dr["ItemNumber"].ToString() && lPrevItem != "")
                    {
                        RowArray = new string[10];
                        RowArray[0] = lPrevItemName + " TOTAL :";
                        RowArray[6] = TotalOrdered(lPrevItem).ToString("F");
                        RowArray[7] = TotalReceived(lPrevItem).ToString("F");
                        RowArray[8] = GrandTotal(lPrevItem).ToString("C2");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                    }
                    if (lPrevItem != dr["ItemNumber"].ToString())
                    {
                        RowArray = new string[10];
                        RowArray[0] = dr["ItemNumber"].ToString();
                        RowArray[4] = dr["ItemName"].ToString();
                        RowArray[5] = dr["PartNumber"].ToString();

                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray = new string[10];
                        RowArray[1] = dr["Name"].ToString();
                        RowArray[2] = dr["PurchaseNumber"].ToString();
                        RowArray[3] = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("MM-dd-yyyy");
                        RowArray[6] = decimal.Parse(dr["OrderQty"].ToString()).ToString("F");
                        RowArray[7] = decimal.Parse(dr["ReceiveQty"].ToString()).ToString("F");
                        RowArray[8] = decimal.Parse(dr["TotalAmount"].ToString()).ToString("C2");
                        RowArray[9] = DateTime.Parse(dr["PromiseDate"].ToString()).ToString("MM-dd-yyyy");
                        dgReport.Rows.Add(RowArray);

                    }
                    else
                    {
                        RowArray = new string[10];
                        RowArray[1] = dr["Name"].ToString();
                        RowArray[2] = dr["PurchaseNumber"].ToString();
                        RowArray[3] = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("MM-dd-yyyy");
                        RowArray[6] = decimal.Parse(dr["OrderQty"].ToString()).ToString("F");
                        RowArray[7] = decimal.Parse(dr["ReceiveQty"].ToString()).ToString("F");
                        RowArray[8] = decimal.Parse(dr["TotalAmount"].ToString()).ToString("C2");
                        RowArray[9] = DateTime.Parse(dr["PromiseDate"].ToString()).ToString("MM-dd-yyyy");
                        dgReport.Rows.Add(RowArray);
                    }
                    if (TbRep.Rows.Count - 1 == i)
                    {//last purchase total
                        RowArray = new string[10];
                        RowArray[0] = lPrevItemName + " TOTAL :";
                        RowArray[6] = TotalOrdered(lPrevItem).ToString("F");
                        RowArray[7] = TotalReceived(lPrevItem).ToString("F");
                        RowArray[8] = GrandTotal(lPrevItem).ToString("C2");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                    }
                    lPrevItemName = dr["ItemName"].ToString();
                    lPrevItem = dr["ItemNumber"].ToString();
                }
                //    FormatGrid();
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
            foreach (DataRow dr in TbGrid.Rows)
            {
                if (ItemName == dr["ItemNumber"].ToString())
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

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Purchase[Item Summary]";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ItemNum", 150);
            dgPrinter.ColumnWidths.Add("Supplier", 100);
            dgPrinter.ColumnWidths.Add("ItemName", 100);
            dgPrinter.ColumnWidths.Add("PartNumber", 100);
            dgPrinter.ColumnWidths.Add("PurchaseNumber", 100);
            dgPrinter.ColumnWidths.Add("OrderQty", 80);
            dgPrinter.ColumnWidths.Add("ReceiveQty", 80);
            dgPrinter.ColumnWidths.Add("TotalAmount", 100);
            dgPrinter.ColumnWidths.Add("Date", 80);
            dgPrinter.ColumnWidths.Add("PromisedDate", 80);
            //  dgPrinter.ColumnWidths.Add("", 100);
            //dgPrinter.ColumnWidths.Add("POStatus", 100);
            ////dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
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

                    ws.Cells[4, 1] = "Item Number";
                    ws.Cells[4, 2] = "Supplier";
                    ws.Cells[4, 3] = "Purchase Number";
                    ws.Cells[4, 4] = "Date";
                    ws.Cells[4, 5] = "Item Name";
                    ws.Cells[4, 6] = "Part Number";
                    ws.Cells[4, 7] = "Order Qty";
                    ws.Cells[4, 8] = "Received Qty";
                    ws.Cells[4, 9] = "Total Amount";
                    ws.Cells[4, 10] = "Promised Date";

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
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                        }
                        if (item.Cells[8].Value != null && item.Cells[8].Value.ToString() != "")
                        {
                            ws.Cells[i, 9] = item.Cells[8].Value.ToString();
                        }
                        if (item.Cells[9].Value != null && item.Cells[9].Value.ToString() != "")
                        {
                            ws.Cells[i, 10] = item.Cells[9].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "J3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Supplier Item Details Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "J4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Supplier Item Details Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
