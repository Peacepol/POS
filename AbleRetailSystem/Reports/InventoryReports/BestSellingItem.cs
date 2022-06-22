using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Reports.InventoryReports
{
    public partial class BestSellingItem : Form
    {
        private System.Data.DataTable TbGrid;
        string categories = "";
        System.Data.DataTable TbRep;
        string header = "";
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;
        private bool LastCostBool = true;
        public BestSellingItem()
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
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue("txtLastCost", out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        LastCostBool = valstr;
                    }
                    else
                    {
                        LastCostBool = valstr;
                    }
                }
            }
        }

        private void BestSellingItem_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadCategory();
            cmbArrange.SelectedIndex = 0;
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
        private void LoadReport()
        {
            string sqlcat = "";
            header = "";
            if (categories.Length > 0)
            {
                sqlcat = " AND i.CategoryID in (" + categories + ")";
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sql = @"SELECT TOP "+ txtdispCount.Value +" SUM(sl.ShipQty) as SoldQty," +
                        @"PartNumber,
                        ItemNumber,
                        ItemName,";
            if (LastCostBool)
            {
                sql += "SUM(s.GrandTotal) - SUM(s.TaxTotal) as TotalSaleExc," +
                    "SUM(sl.TotalCost) as TotalCost , (SUM(s.GrandTotal) - SUM(s.TaxTotal)) - SUM(sl.TotalCost) as Profit,";
            }
            else
            {
                sql += "SUM(0) - SUM(0) as TotalSaleExc," +
                    "SUM(0) as TotalCost , (SUM(0) - SUM(0)) - SUM(0) as Profit,";
            }
                 sql += @" c.CategoryCode, c1.CategoryCode as MainCat 
                        FROM Sales s  
                        INNER JOIN SalesLines sl ON s.SalesID = sl.SalesID 
                        INNER JOIN Items i ON i.ID = sl.EntityID 
                        LEFT JOIN Category c ON c.CategoryID = i.CategoryID 
                        INNER JOIN Category c1 ON c.MainCategoryID= c1.CategoryID 
                        WHERE s.TransactionDate BETWEEN @sdate AND @eDate " + sqlcat +
                        " GROUP BY PartNumber,ItemNumber,ItemName ,c.CategoryCode, c1.CategoryCode ";


            if(cmbArrange.Text ==  "Total Quantity Sold")
            {
                sql += " ORDER BY SoldQty ";
            }
            else if (cmbArrange.Text == "Total Sales Amount")
            {
                sql += " ORDER BY TotalSaleExc ";
            }
            else if (cmbArrange.Text == "Total Profit")
            {
                sql += " ORDER BY Profit ";
            }
            if (rdBest.Checked)
            {
                sql += "DESC";
                header = "Top " + txtdispCount.Value + " Best Selling Item by " + cmbArrange.Text;
            }
            else
            {
                sql += "ASC";
                header = txtdispCount.Value + " Least Selling Item by " + cmbArrange.Text;
            }
           
            DateTime sdate = sDate.Value;
            DateTime edate = eDate.Value;
          
           
            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
            param.Add("@sDate", sdate);
            param.Add("@eDate", edate);
            param.Add("@TopSelling", txtdispCount.Value);

            TbRep = new System.Data.DataTable();
            CommonClass.runSql(ref TbRep, sql, param);
           
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            filterCategories();
            LoadReport();
            Reports.ReportParams bestSelling = new Reports.ReportParams();
            bestSelling.PrtOpt = 1;
            bestSelling.Rec.Add(TbRep);

            bestSelling.ReportName = "BestSellingItem.rpt";
            bestSelling.RptTitle = "Best Selling Item";

            bestSelling.Params = "compname|Header";
            bestSelling.PVals = CommonClass.CompName.Trim() + "|" + header;

            CommonClass.ShowReport(bestSelling);
            Cursor.Current = Cursors.Default;
        }
        public string filterCategories()
        {
            categories = "";
            foreach (TreeNode mainNodes in treeCategory.Nodes)//Category
            {
              
                foreach (TreeNode subnodes in mainNodes.Nodes)
                {
                    if (subnodes.Checked)
                    {
                        categories += subnodes.Tag.ToString() + ",";

                    }
                }
            }
            if(categories.Length > 0)
            {
                categories = categories.Remove(categories.Length - 1);
            }
            return categories;
        }

        private void treeCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            treeCategory.BeginUpdate();
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
            treeCategory.EndUpdate();
        }
        private void LoadCategory()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sqlLoadCategories = @"SELECT * FROM Category";
            CommonClass.runSql(ref dt, sqlLoadCategories);

            string ItemID;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MainCategoryID"].ToString() == "0")
                {
                    ItemID = dt.Rows[i]["CategoryID"].ToString();
                    TreeNode node = new TreeNode(dt.Rows[i]["CategoryCode"].ToString());
                    node.Tag = dt.Rows[i]["CategoryID"].ToString();
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (ItemID == dt.Rows[x]["MainCategoryID"].ToString())
                        {
                            node.Nodes.Add(dt.Rows[x]["CategoryCode"].ToString()).Tag = dt.Rows[x]["CategoryID"].ToString();
                        }
                    }
                    treeCategory.Nodes.Add(node);
                }
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            filterCategories();
            LoadReport();
            FormatGrid();
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
                {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbRep.Rows[i];
                    dgReport.Rows.Add();
                    dgReport.Rows[i].Cells["PartNumber"].Value = dr["PartNumber"];
                    dgReport.Rows[i].Cells["ItemNo"].Value = dr["ItemNumber"];
                    dgReport.Rows[i].Cells["MainCat"].Value = dr["MainCat"];
                    dgReport.Rows[i].Cells["SubCat"].Value = dr["CategoryCode"];
                    dgReport.Rows[i].Cells["QtySold"].Value = dr["SoldQty"];
                    dgReport.Rows[i].Cells["TotalCost"].Value = dr["TotalCost"];
                    dgReport.Rows[i].Cells["TotalSales"].Value = dr["TotalSaleExc"];
                    dgReport.Rows[i].Cells["TotalProfit"].Value = dr["Profit"];
                    dgReport.Rows[i].Cells["TotalProfitMargin"].Value =(float.Parse(dr["Profit"].ToString())/ float.Parse(dr["TotalSaleExc"].ToString()));

                }
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
            ApplyItemFieldAccess(CommonClass.UserID);
        }
        public void FormatGrid()
        {
            this.dgReport.Columns["QtySold"].DefaultCellStyle.Format = "F";
            this.dgReport.Columns["TotalCost"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["TotalSales"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["TotalProfit"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["TotalProfitMargin"].DefaultCellStyle.Format = "P";
            this.dgReport.Columns["QtySold"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns["TotalCost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns["TotalSales"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns["TotalProfit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns["TotalProfitMargin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }
        public void ApplyItemFieldAccess(String FieldID)
        {
            CommonClass.GetAccess(FieldID);
            foreach (DataGridViewRow dgvr in dgReport.Rows)
            {
                if (dgvr.Cells[5].Value.ToString() != "")
                {
                    LastCostFieldRights();
                }
            }
        }

        private void LastCostFieldRights()
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue("txtLastCost", out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        this.dgReport.Columns[5].Visible = valstr;
                        this.dgReport.Columns[6].Visible = valstr;
                        this.dgReport.Columns[7].Visible = valstr;
                        this.dgReport.Columns[8].Visible = valstr;
                    }
                    else
                    {
                        this.dgReport.Columns[5].Visible = valstr;
                        this.dgReport.Columns[6].Visible = valstr;
                        this.dgReport.Columns[7].Visible = valstr;
                        this.dgReport.Columns[8].Visible = valstr;
                    }
                }
            }
        }

        private void dgReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6
             || e.ColumnIndex == 7
          && e.RowIndex != this.dgReport.NewRowIndex)
            {
                if (e.Value != null)
                {
                  //  double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    //e.Value = d.ToString("C2");
                }
            }
            if (e.ColumnIndex == 8
        && e.RowIndex != this.dgReport.NewRowIndex)
            {
                if (e.Value != null)
                {
                  //  double d = double.Parse(e.Value.ToString(), NumberStyles.pr);
                  //  e.Value = d.ToString("P");
                }
            }
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? "asc" : "desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = header;
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("PartNumber", 80);
            dgPrinter.ColumnWidths.Add("ItemNo", 80);
            dgPrinter.ColumnWidths.Add("MainCat", 80);
            dgPrinter.ColumnWidths.Add("SubCat", 80);
            dgPrinter.ColumnWidths.Add("QtySold", 60);
            dgPrinter.ColumnWidths.Add("TotalCost", 100);
            dgPrinter.ColumnWidths.Add("TotalSales", 100);
            dgPrinter.ColumnWidths.Add("TotalProfit", 80);
            dgPrinter.ColumnWidths.Add("TotalProfitMargin", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            // dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
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

                    ws.Cells[4, 1] = "Part Number";
                    ws.Cells[4, 2] = "Item Number";
                    ws.Cells[4, 3] = "Main Category";
                    ws.Cells[4, 4] = "Sub Category";
                    ws.Cells[4, 5] = "Qty Sold";
                    ws.Cells[4, 6] = "Total Cost";
                    ws.Cells[4, 7] = "Total Sales";
                    ws.Cells[4, 8] = "Total Profit";
                    ws.Cells[4, 9] = "Total Profit Margin";

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
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            double TotalCost = Double.Parse(item.Cells[5].Value.ToString());
                            ws.Cells[i, 6] = TotalCost.ToString("C");
                        }
                        if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                        {
                            double TotalSales = Double.Parse(item.Cells[6].Value.ToString());
                            ws.Cells[i, 7] = TotalSales.ToString("C");
                        }
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            double TotalProfit = Double.Parse(item.Cells[7].Value.ToString());
                            ws.Cells[i, 8] = TotalProfit.ToString("C");
                        }
                        if (item.Cells[7].Value != null && item.Cells[8].Value.ToString() != "")
                        {
                            double TotalProfitMargin = Double.Parse(item.Cells[8].Value.ToString());
                            ws.Cells[i, 9] = TotalProfitMargin.ToString("C");
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
                    ws.Cells[1, 1] = header;

                    //Style Table
                    cellRange = ws.get_Range("A4", "I4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    ws.get_Range("F4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.get_Range("G4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.get_Range("H4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.get_Range("I4").EntireColumn.NumberFormat = "##0,00%";


                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Best Selling Item Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
