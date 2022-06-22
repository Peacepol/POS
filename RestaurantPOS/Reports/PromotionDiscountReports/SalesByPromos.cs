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

namespace AbleRetailPOS.Reports.PromotionDiscountReports
{
    public partial class SalesByPromos : Form
    {
        private System.Data.DataTable Tbrep;
        private System.Data.DataTable TbGrid;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public SalesByPromos()
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
        }

        void LoadReport()
        {
            string sql = @"SELECT p.PromoCode, p.PromotionType, s.SalesNumber, pl.Name, s.TransactionDate, i.PartNumber, i.ItemNumber,i.ItemName,
                                sl.OrderQty, s.SubTotal, sl.TotalCost
                                FROM Sales s 
                                INNER JOIN SalesLines sl ON sl.SalesID = s.SalesID
                                INNER JOIN Items i ON i.ID = sl.EntityID
                                INNER JOIN Profile pl ON pl.ID = s.CustomerID
								INNER JOIN Promos p ON p.PromoID = sl.PromoID
                                WHERE s.TransactionDate BETWEEN @sdate AND @edate";
            if (txtPromoCode.Text != "")
            {
                 sql += " AND p.PromoCode LIKE '%" + txtPromoCode.Text + "%'";
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            DateTime sdate = dtmTxFrom.Value;
            DateTime edate = dtmTxTo.Value;
            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
            param.Add("@sdate", sdate);
            param.Add("@edate", edate);
            Tbrep = new System.Data.DataTable();
            CommonClass.runSql(ref Tbrep, sql, param);
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
           
                TbGrid = Tbrep.Copy();
                if (TbGrid.Rows.Count > 0)
                {
                    DataView dv = TbGrid.DefaultView;
                    dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                    TbGrid = dv.ToTable();
                    string lPrevItem = "";
                    string lPrevPayMethod = "";
                    string[] RowArray;
                    int rIndex;
                    dgReport.Rows.Clear();
                    for (int i = 0; i < TbGrid.Rows.Count; i++)
                    {
                        DataRow dr = TbGrid.Rows[i];       
                        RowArray = new string[13];
                        RowArray[0] = dr["PromoCode"].ToString();
                        RowArray[1] = dr["PromotionType"].ToString();
                        RowArray[2] = dr["SalesNumber"].ToString();
                        RowArray[3] = dr["Name"].ToString();
                        RowArray[4] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString(); ;
                        RowArray[5] = dr["PartNumber"].ToString();
                        RowArray[6] = dr["ItemNumber"].ToString();
                        RowArray[7] = dr["ItemName"].ToString();
                        RowArray[8] = dr["OrderQty"].ToString();
                        double subtotal = double.Parse(dr["SubTotal"].ToString()); 
                        RowArray[9] = subtotal.ToString("C");
                        double totalcost = double.Parse(dr["TotalCost"].ToString());
                        RowArray[10] = totalcost.ToString("C");
                        double profit = double.Parse(dr["TotalCost"].ToString()) - double.Parse(dr["SubTotal"].ToString());
                        RowArray[11] = profit.ToString("C");
                        double profitMargin = profit / double.Parse(dr["SubTotal"].ToString());
                        RowArray[12] = profitMargin.ToString("P");
                        dgReport.Rows.Add(RowArray);                                                                             
                    }
                }
                else
                {
                    MessageBox.Show("Contains No Data.", "Report Information");
                }
            FillSortCombo();
                    FormatGrid();
            Cursor.Current = Cursors.Default;
            
            
        }
        private void FormatGrid()
        {
            this.dgReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //this.dgReport.Columns[0].Visible = false;
            //this.dgReport.Columns[1].Visible = false;
            //this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Sales By Promos Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("PromoCode", 70);
            dgPrinter.ColumnWidths.Add("PromoType", 70);
            dgPrinter.ColumnWidths.Add("SalesNumber", 70);
            dgPrinter.ColumnWidths.Add("CustomerName", 70);
            dgPrinter.ColumnWidths.Add("TransactionDate", 80);
            dgPrinter.ColumnWidths.Add("PartNumber", 80);
            dgPrinter.ColumnWidths.Add("ItemNumber", 80);
            dgPrinter.ColumnWidths.Add("ItemName", 100);
            dgPrinter.ColumnWidths.Add("Qty", 50);
            dgPrinter.ColumnWidths.Add("TotalAmountEx", 80);
            dgPrinter.ColumnWidths.Add("TotalCost", 80);
            dgPrinter.ColumnWidths.Add("Profit", 80);
            dgPrinter.ColumnWidths.Add("ProfitMargin", 50);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }
        private void LoadSalesByReport()
        {
            Reports.ReportParams SalesByPromos = new Reports.ReportParams();
            SalesByPromos.PrtOpt = 1;
            SalesByPromos.Rec.Add(Tbrep);
            SalesByPromos.ReportName = "SalesByPromos.rpt";
            SalesByPromos.RptTitle = "Sales By Promos Report";
            SalesByPromos.Params = "compname|StartDate|EndDate";
            SalesByPromos.PVals = CommonClass.CompName.Trim() + "|" + dtmTxFrom.Value.ToShortDateString() + " |" + dtmTxTo.Value.ToShortDateString();
            CommonClass.ShowReport(SalesByPromos);
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            LoadSalesByReport();
            Cursor.Current = Cursors.Default;
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
                    // app.Visible = false;
                    ws.Cells[4, 1] = "Promo Code";
                    ws.Cells[4, 2] = "Promotion Type";
                    ws.Cells[4, 3] = "Sales Number";
                    ws.Cells[4, 4] = "Customer Name";
                    ws.Cells[4, 5] = "Transaction Date";
                    ws.Cells[4, 6] = "Part Number";
                    ws.Cells[4, 7] = "Item Number";
                    ws.Cells[4, 8] = "Item Name";
                    ws.Cells[4, 9] = "Order Qty";
                    ws.Cells[4, 10] = "Sub Total";
                    ws.Cells[4, 11] = "Total Cost";
                    ws.Cells[4, 12] = "Profit";
                    ws.Cells[4, 13] = "Profit Margin";

                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {
                        if (item.Cells[0].Value != null)
                        {
                            ws.Cells[i, 1] = item.Cells[0].Value.ToString();
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
                            ws.Cells[i, 5] = Convert.ToDateTime(item.Cells[4].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null)
                        {
                            ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                        }
                        if (item.Cells[8].Value != null)
                        {
                            ws.Cells[i, 9] = item.Cells[8].Value.ToString();
                        }
                        if (item.Cells[9].Value != null)
                        {
                            ws.Cells[i, 10] = item.Cells[9].Value.ToString();
                        }
                        if (item.Cells[10].Value != null)
                        {
                            ws.Cells[i, 11] = item.Cells[10].Value.ToString();
                        }
                        if (item.Cells[11].Value != null)
                        {
                            ws.Cells[i, 12] = item.Cells[11].Value.ToString();
                        }
                        if (item.Cells[12].Value != null)
                        {
                            ws.Cells[i, 13] = item.Cells[12].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "M3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 20;
                    ws.Cells[1, 1] = "Sales By Promos Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "M4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales By Promos Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
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

        private void SalesByPromos_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
