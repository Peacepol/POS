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

namespace AbleRetailPOS.Reports.InventoryReports
{
    public partial class ItemCountCustomizer : Form
    {
        private System.Data.DataTable TbGrid;
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        string categories = "";
        System.Data.DataTable TbRep;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;
        public ItemCountCustomizer()
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

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            if (txtSupplier.Text == "")
            {
                supplierID = "";
            }
        }

        private void txtList3_TextChanged(object sender, EventArgs e)
        {
            if (txtList3.Text == "")
            {
                list3 = "";
            }
        }

        private void txtList2_TextChanged(object sender, EventArgs e)
        {
            if (txtList2.Text == "")
            {
                list2 = "";
            }
        }

        private void txtList1_TextChanged(object sender, EventArgs e)
        {
            if (txtList1.Text == "")
            {
                list1 = "";
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            filterCategories();
            LoadReport();

            Reports.ReportParams itemparams = new Reports.ReportParams();
            itemparams.PrtOpt = 1;
            itemparams.Rec.Add(TbRep);
            itemparams.ReportName = "InventoryCountSheet.rpt";
            itemparams.RptTitle = "Inventory Count Sheet";
            itemparams.Params = "compname";
            itemparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(itemparams);
        }

        private void treeCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {

            treeCategory.BeginUpdate();
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
            treeCategory.EndUpdate();
        }

        private void ItemCountCustomizer_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }//cmbSort.SelectedIndex = 1;
            LoadCategory();
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
        private void LoadReport()
        {

            string sql = @"SELECT i.ItemNumber, 
                                    i.ItemName, i.PartNumber ,
                                    iq.OnHandQty, 
                                    i.BuyingUOM
                                 FROM Items i 
                                 INNER JOIN ItemsQty iq ON i.ID = iq.ItemID";
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
            if (cmbSort.Text == "Item Number")
            {
                sql += " ORDER BY ItemNumber DESC";
            }
            else if (cmbSort.Text == "Part Number")
            {
                sql += " ORDER BY PartNumber DESC";
            }

            TbRep = new System.Data.DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@supplier", supplierID);
            param.Add("@CList1", list1);
            param.Add("@CList2", list2);
            param.Add("@CList3", list3);
            CommonClass.runSql(ref TbRep, sql, param);
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
           
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
                    dgReport.Rows[i].Cells["QtyOnHand"].Value = dr["OnHandQty"];
                    dgReport.Rows[i].Cells["Units"].Value = dr["BuyingUOM"];
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
            dgPrinter.SubTitle = "Item Count Sheet";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ItemNo", 80);
            dgPrinter.ColumnWidths.Add("ItemName", 150);
            dgPrinter.ColumnWidths.Add("QtyOnHand", 100);
            dgPrinter.ColumnWidths.Add("Units", 80);
            dgPrinter.ColumnWidths.Add("Count1", 150);
            dgPrinter.ColumnWidths.Add("Count2", 150);
            //dgPrinter.ColumnWidths.Add("Committed", 80);
            //dgPrinter.ColumnWidths.Add("OnOrder", 80);
            //dgPrinter.ColumnWidths.Add("Available", 70);
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
                    ws.Cells[4, 1] = "Item Number";
                    ws.Cells[4, 2] = "Item Name";
                    ws.Cells[4, 3] = "Qty On Hand";
                    ws.Cells[4, 4] = "Units";

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

                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "D3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Item Count Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "D4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Item Count Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
