using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Reports.InventoryReports
{
    public partial class RptPriceAnalysis : Form
    {
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        string categories = "";
        System.Data.DataTable TbRep;
        private int index = 1;
        private string sort = " asc";
        private System.Data.DataTable TbGrid;
        private bool CanView = false;
        public RptPriceAnalysis()
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            filterCategories();
            LoadReport();

            Reports.ReportParams priceparams = new Reports.ReportParams();
            priceparams.PrtOpt = 1;
            priceparams.Rec.Add(TbRep);

            priceparams.ReportName = "PriceAnalysis.rpt";
            priceparams.RptTitle = "Price Analysis";

            priceparams.Params = "compname";
            priceparams.PVals = CommonClass.CompName.Trim();
            CommonClass.ShowReport(priceparams);
        }

        private void LoadReport()
        {
                string sql = @"SELECT i.ItemName, 
                                i.ItemNumber, 
                                isp.Level0,
                                icp.AverageCostEx
                            FROM Items i
                            INNER JOIN ItemsSellingPrice isp ON i.ID = isp.ItemID
                            INNER JOIN ItemsCostPrice icp ON i.ID = icp.ItemID";

                if (cmbCostBasis.Text == "Last Price")
                {
                    sql = @"SELECT i.ItemName, 
                                i.ItemNumber, 
                                isp.Level0,
                                icp.AverageCostEx
                            FROM Items i
                            INNER JOIN ItemsSellingPrice isp ON i.ID = isp.ItemID
                            INNER JOIN ItemsCostPrice icp ON i.ID = icp.ItemID";
                }

                if (list1 != "")
                {
                    sql += " AND i.CList1 = @CList1";
                }
                if (list2 != "")
                {
                    sql += " AND i.CList2 = @CList2";
                }
                if (list3 != "")
                {
                    sql += " AND i.CList3 = @CList3";
                }
                if (supplierID != "")
                {
                    sql += " AND i.SupplierID = @supplier";
                }

                if (categories.Length > 0)
                {
                    sql += " AND CategoryID in (" + categories + ")";
                }
                
                TbRep = new System.Data.DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@supplier", supplierID);
                param.Add("@CList1", list1);
                param.Add("@CList2", list2);
                param.Add("@CList3", list3);
                CommonClass.runSql(ref TbRep, sql, param);


        }

        private void RptPriceAnalysis_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadCategory();
         //   cmbSort.SelectedIndex = 1;
            cmbCostBasis.SelectedIndex = 0;
        }

        private void pbSupplier_Click(object sender, EventArgs e)
        {

            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                supplierID = lProfile[0];
                this.txtSupplier.Text = lProfile[2];
            }
        }

        private void pbList1_Click(object sender, EventArgs e)
        {
            List1Lookup L1Dlg = new List1Lookup("Items");
            string[] l1 = new string[2];

            if (L1Dlg.ShowDialog() == DialogResult.OK)
            {
                l1 = L1Dlg.GetList1;

                this.txtList1.Text = l1[0];
                list1 = l1[1];
            }
        }

        private void pbList2_Click(object sender, EventArgs e)
        {

            List2Lookup L2Dlg = new List2Lookup("Items");
            string[] l2 = new string[2];

            if (L2Dlg.ShowDialog() == DialogResult.OK)
            {
                l2 = L2Dlg.GetList2;

                this.txtList2.Text = l2[0];
                list2 = l2[1];
            }
        }

        private void pbList3_Click(object sender, EventArgs e)
        {
            List3Lookup L3Dlg = new List3Lookup("Items");
            string[] l3 = new string[2];

            if (L3Dlg.ShowDialog() == DialogResult.OK)
            {
                l3 = L3Dlg.GetList3;

                this.txtList2.Text = l3[0];
                list3 = l3[1];
            }
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
            if (categories.Length > 0)
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            filterCategories();
            LoadReport();
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    dgReport.Rows.Add();
                    dgReport.Rows[i].Cells["ItemNo"].Value = dr["ItemNumber"];
                    dgReport.Rows[i].Cells["ItemName"].Value = dr["ItemName"];
                    dgReport.Rows[i].Cells["CurrentPrice"].Value = float.Parse(dr["Level0"].ToString()).ToString("C2");
                    dgReport.Rows[i].Cells["CostBasis"].Value = float.Parse(dr["AverageCostEx"].ToString()).ToString("C2"); 
                    dgReport.Rows[i].Cells["GrossProfit"].Value = float.Parse(dr["Level0"].ToString()) - float.Parse(dr["AverageCostEx"].ToString());
                    dgReport.Rows[i].Cells["PercentMargin"].Value = ((float.Parse(dr["Level0"].ToString()) - float.Parse(dr["AverageCostEx"].ToString()))/ float.Parse(dr["Level0"].ToString())).ToString("P");
                    dgReport.Rows[i].Cells["PercentMarkup"].Value = ((float.Parse(dr["Level0"].ToString()) - float.Parse(dr["AverageCostEx"].ToString()))/ float.Parse(dr["AverageCostEx"].ToString())).ToString("P");
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

        private void dgReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
      //      if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 
      // && e.RowIndex != this.dgReport.NewRowIndex)
      //      {
      //          if (e.Value != null)
      //          {
      //              double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
      //              e.Value = d.ToString("C2");
      //          }
      //      }
      //      if (e.ColumnIndex == 5 || e.ColumnIndex == 6
      //&& e.RowIndex != this.dgReport.NewRowIndex)
      //      {
      //          if (e.Value != null)
      //          {
      //              double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
      //              e.Value = d.ToString("P");
      //          }
      //      }
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
            dgPrinter.SubTitle = "Price Analysis Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ItemNo", 100);
            dgPrinter.ColumnWidths.Add("ItemName",130);
            dgPrinter.ColumnWidths.Add("CurrentPrice", 100);
            dgPrinter.ColumnWidths.Add("CostBasis", 100);
            dgPrinter.ColumnWidths.Add("GrossProfit", 100);
            dgPrinter.ColumnWidths.Add("PercentMargin", 100);
            dgPrinter.ColumnWidths.Add("PercentMarkup",100);
            //dgPrinter.ColumnWidths.Add("Committed", 80);
            //dgPrinter.ColumnWidths.Add("OnOrder", 80);
            //dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            //dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
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
                    ws.Cells[4, 1] = "Item Number";
                    ws.Cells[4, 2] = "Item Name";
                    ws.Cells[4, 3] = "Current Price";
                    ws.Cells[4, 4] = "Cost Basis";
                    ws.Cells[4, 5] = "Gross Profit";
                    ws.Cells[4, 6] = "Percent Margin";
                    ws.Cells[4, 7] = "Percent Markup";
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
                        if (item.Cells[2].Value != null && item.Cells[2].Value.ToString() != "")
                        {
                            double CurrentPrice = Double.Parse(item.Cells[2].Value.ToString());
                            ws.Cells[i, 3] = CurrentPrice.ToString("C");
                        }
                        if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                        {
                            double GrossProfit = Double.Parse(item.Cells[3].Value.ToString());
                            ws.Cells[i, 4] = GrossProfit.ToString("C");
                        }
                        if (item.Cells[4].Value != null && item.Cells[4].Value.ToString() != "")
                        {
                            double CostBasis = Double.Parse(item.Cells[4].Value.ToString());
                            ws.Cells[i, 5] = CostBasis.ToString("C");
                        }
                        if (item.Cells[5].Value != null)
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
                    ws.Cells[1, 1] = "Price Analysis Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "G4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    ws.get_Range("C4").EntireColumn.NumberFormat = "$##,##0.00";
                    ws.get_Range("D4").EntireColumn.NumberFormat = "$##,##0.00";
                    ws.get_Range("E4").EntireColumn.NumberFormat = "$##,##0.00";
                    ws.get_Range("F4").EntireColumn.NumberFormat = "##0,00%";
                    ws.get_Range("G4").EntireColumn.NumberFormat = "##0,00%";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Price Analysis Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
