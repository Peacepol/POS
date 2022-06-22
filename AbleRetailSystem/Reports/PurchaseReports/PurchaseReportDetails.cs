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
    public partial class PurchaseReportDetails : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public PurchaseReportDetails()
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
        private void LoadReportDetails()
        {
            SqlConnection con = null;
            try
            {
                string sql = @"SELECT j.Name, p.TransactionDate,
                                p.PurchaseNumber,
                                p.GrandTotal,
                                p.TotalDue,
                                p.SupplierINVNumber,
                                p.POStatus,
                                p.TaxTotal,
                                p.LayoutType,
                                i.PartNumber,
                                i.ItemNumber,
                                l.Description,
                                l.OrderQty,
                                l.ReceiveQty,
                                l.SubTotal as ItemSubTotal,
                                l.TaxCode 
			                   FROM (Purchases p INNER JOIN PurchaseLines l on p.PurchaseID = l.PurchaseID 
                             	    LEFT JOIN Profile j on p.SupplierID = j.ID 
				                    LEFT JOIN Items i on i.ID = l.EntityID)
			                   WHERE p.TransactionDate BETWEEN @sdate AND @edate ";
               
                if(PurchaseStatuscb.Text != "")
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

        private void PurchaseStatuscb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReportDetails();
            Reports.ReportParams PurchaseDetails = new Reports.ReportParams();
            PurchaseDetails.PrtOpt = 1;
            PurchaseDetails.Rec.Add(TbRep);
            PurchaseDetails.ReportName = "PurchaseReportDetail.rpt";
            PurchaseDetails.RptTitle = "Purchase Report Details";

            PurchaseDetails.Params = "compname";
            PurchaseDetails.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(PurchaseDetails);
            Cursor.Current = Cursors.Default;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            LoadReportDetails();
            TbGrid = TbRep.Copy();
            string lPrevItem = "";
            DataRow rw;
            string[] RowArray;
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                int rIndex = 0;
                // dgReport.DataSource = TbGrid;
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    if (lPrevItem != dr["Name"].ToString() && lPrevItem != "")
                    {
                        RowArray = new string[9];
                        RowArray[6] = "TOTAL :";
                        RowArray[7] = GrandTotal(lPrevItem).ToString("C2");

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
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray[0] = dr["PurchaseNumber"].ToString();
                        RowArray[1] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                        RowArray[2] = decimal.Parse(dr["OrderQty"].ToString()).ToString("F");
                        RowArray[3] = decimal.Parse(dr["ReceiveQty"].ToString()).ToString("F");
                        RowArray[4] = dr["ItemNumber"].ToString();
                        RowArray[5] = dr["PartNumber"].ToString();
                        RowArray[6] = dr["Description"].ToString();
                        RowArray[7] = decimal.Parse(dr["ItemSubTotal"].ToString()).ToString("C2");
                        RowArray[8] = dr["POStatus"].ToString();
                        dgReport.Rows.Add(RowArray);
                    }
                    else
                    {
                        RowArray = new string[9];
                        RowArray[0] = dr["PurchaseNumber"].ToString();
                        RowArray[1] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                        RowArray[2] = decimal.Parse(dr["OrderQty"].ToString()).ToString("F");
                        RowArray[3] = decimal.Parse(dr["ReceiveQty"].ToString()).ToString("F");
                        RowArray[4] = dr["ItemNumber"].ToString();
                        RowArray[5] = dr["PartNumber"].ToString();
                        RowArray[6] = dr["Description"].ToString();
                        RowArray[7] = decimal.Parse(dr["ItemSubTotal"].ToString()).ToString("C2");
                        RowArray[8] = dr["POStatus"].ToString();
                        dgReport.Rows.Add(RowArray);

                    }
                    lPrevItem = dr["Name"].ToString();
                    if (TbRep.Rows.Count - 1 == i)
                    {
                        RowArray = new string[9];
                        RowArray[6] = "TOTAL :";
                        RowArray[7] = GrandTotal(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C2") : GrandTotal(lPrevItem).ToString("C2");
                      
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        //GRANDTOTAL
                        RowArray[6] = "GRAND TOTAL :";
                        RowArray[7] = GrandTotal().ToString("C2");
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
                FillSortCombo();

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
            this.dgReport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        public decimal GrandTotal(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (dr["Name"].ToString() == ItemName)
                {
                    x += decimal.Parse(dr["ItemSubTotal"].ToString());
                }
            }
            return x;
        }
        public decimal GrandTotal()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["ItemSubTotal"].ToString());
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

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Purchase Report Detail";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("TransDate", 70);
            dgPrinter.ColumnWidths.Add("Amount", 120);
            dgPrinter.ColumnWidths.Add("PurchaseNum", 100);
            dgPrinter.ColumnWidths.Add("Ordered", 100);
            dgPrinter.ColumnWidths.Add("Received", 100);
            dgPrinter.ColumnWidths.Add("TotalDue", 100);
            dgPrinter.ColumnWidths.Add("ItemNumber", 100);
            dgPrinter.ColumnWidths.Add("PartNumber", 100);
            dgPrinter.ColumnWidths.Add("PDate", 100);
            dgPrinter.ColumnWidths.Add("Status", 100);
           
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? " asc" : " desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
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
                    ws.Cells[4, 3] = "Ordered";
                    ws.Cells[4, 4] = "Received";
                    ws.Cells[4, 5] = "Item Number";
                    ws.Cells[4, 6] = "Part Number";
                    ws.Cells[4, 7] = "Description";
                    ws.Cells[4, 8] = "Grand Total";
                    ws.Cells[4, 9] = "Status";
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
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[5].Value != null)
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
                        if (item.Cells[8].Value != null)
                        {
                            ws.Cells[i, 9] = item.Cells[8].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "I3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Purchase Detail Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "I4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Purchase Detail Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void PurchaseReportDetails_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }

}
