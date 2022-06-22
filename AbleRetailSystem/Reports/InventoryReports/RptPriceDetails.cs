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

namespace RestaurantPOS.Reports.InventoryReports
{
    public partial class RptPriceDetails : Form
    {
        System.Data.DataTable TbRep;
        System.Data.DataTable TbGrid;
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        string categories = "";
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;
        public RptPriceDetails()
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
            Cursor.Current = Cursors.WaitCursor;
            filterCategories();
            LoadReport();
            GenerateReport();
            Cursor.Current = Cursors.Default;
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            string level = @"isp.Level0,
                                    isp.Level1,
                                    isp.Level2,
                                    isp.Level3,
                                    isp.Level4,
                                    isp.Level5,
                                    isp.Level6,
                                    isp.Level7,
                                    isp.Level8,
                                    isp.Level9,
                                    isp.Level10,
                                    isp.Level11,
                                    isp.Level12";
            if (cmbPriceLevel.Text != "All")
            {
                level = cmbPriceLevel.Text;
            }
                try
            {
                string sql = @"SELECT i.ItemName, 
                                    i.ItemNumber,
                                    i.QtyPerSellingUnit, "+ level+ @" FROM Items i
                                  INNER JOIN ItemsSellingPrice isp ON i.ID = isp.ItemID";
                
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
        public void GenerateReport()
        {
            Reports.ReportParams priceparams = new Reports.ReportParams();
            priceparams.PrtOpt = 1;
            priceparams.Rec.Add(TbRep);

            priceparams.ReportName = "PriceListDetails.rpt";
            priceparams.RptTitle = "Price List Details";

            priceparams.Params = "compname";
            priceparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(priceparams);
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

        private void RptPriceDetails_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadCategory();
            cmbPriceLevel.SelectedIndex = 0;
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

        private void treeCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            treeCategory.BeginUpdate();
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
            treeCategory.EndUpdate();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            dgReport.Rows.Clear();
            FormatGrid();
            filterCategories();
            LoadReport();

            float total = 0;
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                int x = 0;
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    dgReport.Rows.Add();
                    if (cmbPriceLevel.Text != "All")
                    {
                        dgReport.Rows[i].Cells["ItemName"].Value = dr["ItemName"];
                        dgReport.Rows[i].Cells["ItemNum"].Value = dr["ItemNumber"];
                        dgReport.Rows[i].Cells["QtyPerSellingUnit"].Value = dr["QtyPerSellingUnit"];
                        dgReport.Rows[i].Cells[cmbPriceLevel.Text].Value = dr[cmbPriceLevel.Text];
                    }
                  
                    else
                    {
                        dgReport.Rows[i].Cells["ItemName"].Value = dr["ItemName"];
                        dgReport.Rows[i].Cells["ItemNum"].Value = dr["ItemNumber"];
                        dgReport.Rows[i].Cells["QtyPerSellingUnit"].Value = dr["QtyPerSellingUnit"];
                        dgReport.Rows[i].Cells["level0"].Value = dr["Level0"];
                        dgReport.Rows[i].Cells["level1"].Value = dr["Level1"];
                        dgReport.Rows[i].Cells["level2"].Value = dr["Level2"];
                        dgReport.Rows[i].Cells["level3"].Value = dr["Level3"];
                        dgReport.Rows[i].Cells["level4"].Value = dr["Level4"];
                        dgReport.Rows[i].Cells["level5"].Value = dr["Level5"];
                        dgReport.Rows[i].Cells["level6"].Value = dr["Level6"];
                        dgReport.Rows[i].Cells["level7"].Value = dr["Level7"];
                        dgReport.Rows[i].Cells["level8"].Value = dr["Level8"];
                        dgReport.Rows[i].Cells["level9"].Value = dr["Level9"];
                        dgReport.Rows[i].Cells["level10"].Value = dr["Level10"];
                        dgReport.Rows[i].Cells["level11"].Value = dr["Level11"];
                        dgReport.Rows[i].Cells["level12"].Value = dr["Level12"];
                    }
                    
                    x++;
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
        }
        public void FormatGrid()
        {
            this.dgReport.Columns["level0"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level1"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level2"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level3"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level4"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level5"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level6"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level7"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level8"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level9"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level10"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level11"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["level12"].DefaultCellStyle.Format = "C2";
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
            sort = (rdoAsc.Checked == true ? "asc" : "desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Price Detail Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ItemName", 80);
            dgPrinter.ColumnWidths.Add("ItemNum", 60);
            dgPrinter.ColumnWidths.Add("QtyPerSellingUnit", 60);
            dgPrinter.ColumnWidths.Add("Level0", 60);
            dgPrinter.ColumnWidths.Add("Level1", 60);
            dgPrinter.ColumnWidths.Add("Level2", 60);
            dgPrinter.ColumnWidths.Add("Level3", 60);
            dgPrinter.ColumnWidths.Add("Level4", 60);
            dgPrinter.ColumnWidths.Add("Level5", 60);
            dgPrinter.ColumnWidths.Add("Level6", 60);
            dgPrinter.ColumnWidths.Add("Level7", 60);
            dgPrinter.ColumnWidths.Add("Level8", 60);
            dgPrinter.ColumnWidths.Add("Level9", 60);
            dgPrinter.ColumnWidths.Add("Level10", 60);
            dgPrinter.ColumnWidths.Add("Level11", 60);
            dgPrinter.ColumnWidths.Add("Level12", 60);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
             dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
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
                    ws.Cells[4, 1] = "Item Name";
                    ws.Cells[4, 2] = "Item Number";
                    ws.Cells[4, 3] = "Qty Per Selling Unit";
                    ws.Cells[4, 4] = "level 0";
                    ws.Cells[4, 5] = "level 1";
                    ws.Cells[4, 6] = "level 2";
                    ws.Cells[4, 7] = "level 3";
                    ws.Cells[4, 8] = "level 4";
                    ws.Cells[4, 9] = "level 5";
                    ws.Cells[4, 10] = "level 6";
                    ws.Cells[4, 11] = "level 7";
                    ws.Cells[4, 12] = "level 8";
                    ws.Cells[4, 13] = "level 9";
                    ws.Cells[4, 14] = "level 10";
                    ws.Cells[4, 15] = "level 11";
                    ws.Cells[4, 16] = "level 12";


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
                        if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                        {
                            double Level0 = Double.Parse(item.Cells[3].Value.ToString());
                            ws.Cells[i, 4] = Level0.ToString("C");
                        }
                        if (item.Cells[4].Value != null && item.Cells[4].Value.ToString() != "")
                        {
                            double Level1 = Double.Parse(item.Cells[4].Value.ToString());
                            ws.Cells[i, 5] = Level1.ToString("C");
                        }
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            double Level2 = Double.Parse(item.Cells[5].Value.ToString());
                            ws.Cells[i, 6] = Level2.ToString("C");
                        }
                        if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                        {
                            double Level3 = Double.Parse(item.Cells[6].Value.ToString());
                            ws.Cells[i, 7] = Level3.ToString("C");
                        }
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            double Level4 = Double.Parse(item.Cells[7].Value.ToString());
                            ws.Cells[i, 8] = Level4.ToString("C");
                        }
                        if (item.Cells[8].Value != null && item.Cells[8].Value.ToString() != "")
                        {
                            double Level5 = Double.Parse(item.Cells[8].Value.ToString());
                            ws.Cells[i, 9] = Level5.ToString("C");
                        }
                        if (item.Cells[9].Value != null && item.Cells[9].Value.ToString() != "")
                        {
                            double Level6 = Double.Parse(item.Cells[9].Value.ToString());
                            ws.Cells[i, 10] = Level6.ToString("C");
                        }
                        if (item.Cells[10].Value != null && item.Cells[10].Value.ToString() != "")
                        {
                            double Level7 = Double.Parse(item.Cells[10].Value.ToString());
                            ws.Cells[i, 11] = Level7.ToString("C");
                        }
                        if (item.Cells[11].Value != null && item.Cells[11].Value.ToString() != "")
                        {
                            double Level8 = Double.Parse(item.Cells[11].Value.ToString());
                            ws.Cells[i, 12] = Level8.ToString("C");
                        }
                        if (item.Cells[12].Value != null && item.Cells[12].Value.ToString() != "")
                        {
                            double Level9 = Double.Parse(item.Cells[12].Value.ToString());
                            ws.Cells[i, 13] = Level9.ToString("C");
                        }
                        if (item.Cells[13].Value != null && item.Cells[13].Value.ToString() != "")
                        {
                            double Level10 = Double.Parse(item.Cells[13].Value.ToString());
                            ws.Cells[i, 14] = Level10.ToString("C");
                        }
                        if (item.Cells[14].Value != null && item.Cells[14].Value.ToString() != "")
                        {
                            double Level11 = Double.Parse(item.Cells[14].Value.ToString());
                            ws.Cells[i, 15] = Level11.ToString("C");
                        }
                        if (item.Cells[15].Value != null && item.Cells[15].Value.ToString() != "")
                        {
                            double Level12 = Double.Parse(item.Cells[15].Value.ToString());
                            ws.Cells[i, 16] = Level12.ToString("C");
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "P3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Price List Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "P4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Price List Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
