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
    
    public partial class RptItemDetails : Form
    {
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        private string categories = "";
        System.Data.DataTable TbRep;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;
        public RptItemDetails()
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
            Reports.ReportParams itemparams = new Reports.ReportParams();
            itemparams.PrtOpt = 1;
            itemparams.Rec.Add(TbRep);

            itemparams.ReportName = "ItemListDetails.rpt";
            itemparams.RptTitle = "Items List Details";

            itemparams.Params = "compname";
            itemparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(itemparams);
            Cursor.Current = Cursors.Default;
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            filterCategories();
            FormatGrid();
            LoadReport();
            dgReport.Rows.Clear();
            float total = 0;
            if (TbRep.Rows.Count > 0)
            {
                DataView dv = TbRep.DefaultView;
                dv.Sort = TbRep.Columns[index].ColumnName + " " + sort;
                TbRep = dv.ToTable();

                for (int i = 0; i < TbRep.Rows.Count; i++)
                {
                    DataRow dr = TbRep.Rows[i];
                    dgReport.Rows.Add();
                    dgReport.Rows[i].Cells["ItemNo"].Value = dr["ItemNumber"];
                    dgReport.Rows[i].Cells["ItemName"].Value = dr["ItemName"];
                    dgReport.Rows[i].Cells["Supplier"].Value = dr["Name"];
                    dgReport.Rows[i].Cells["UnitsHand"].Value = dr["OnHandQty"];
                    dgReport.Rows[i].Cells["BuyingPrice"].Value = dr["AverageCostEx"];
                    dgReport.Rows[i].Cells["SellingPrice"].Value = dr["Level0"];
                    dgReport.Rows[i].Cells["AssetID"].Value = dr["AssetAccountID"];
                    dgReport.Rows[i].Cells["IncomeID"].Value = dr["IncomeAccountID"];
                    dgReport.Rows[i].Cells["COSID"].Value = dr["COSAccountID"];
                    dgReport.Rows[i].Cells["SNumPer"].Value = dr["QtyPerSellingUnit"];
                    dgReport.Rows[i].Cells["SalesTax"].Value = dr["SalesTaxCode"].ToString() == ""? "N-T": dr["SalesTaxCode"];
                    dgReport.Rows[i].Cells["BUnit"].Value = dr["QtyPerBuyingUOM"];
                    dgReport.Rows[i].Cells["BTax"].Value = dr["PurchaseTaxCode"].ToString() == "" ? "N-T" : dr["PurchaseTaxCode"];


                    dgReport.Rows[i].Cells["TotalValue"].Value = float.Parse(dr["OnHandQty"].ToString()) * float.Parse(dr["AverageCostEx"].ToString());
                }
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                FormatGrid();
                FillSortCombo();
            }
            else
            {
                MessageBox.Show("Contains No Data.", "Report Information");
            }
            Cursor.Current = Cursors.Default;
        }
        public void FormatGrid()
        {
            this.dgReport.Columns["UnitsHand"].DefaultCellStyle.Format = "F";
            this.dgReport.Columns["SNumPer"].DefaultCellStyle.Format = "F";
            this.dgReport.Columns["SellingPrice"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["BuyingPrice"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["TotalValue"].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns["BUnit"].DefaultCellStyle.Format = "F";
            this.dgReport.Columns["BUnit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns["SNumPer"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns["SellingPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns["BuyingPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                string sql = @"SELECT ItemNumber, 
                                      ItemName, 
                                      Name, 
                                      OnHandQty,
                                      AverageCostEx,
                                      icp.LastCostEx, 
                                      Level0,
                                      SupplierItemNumber,
                                      i.COSAccountID, 
                                      i.IncomeAccountID,
                                      i.AssetAccountID, 
                                      i.QtyPerBuyingUOM, 
                                      i.QtyPerSellingUnit,
                                      SalesTaxCode, 
                                      PurchaseTaxCode 
                                FROM Items i 
                                INNER JOIN ItemsQty iq ON i.ID = iq.ItemID 
                                LEFT JOIN Profile p ON p.ID = i.SupplierID
                                INNER JOIN ItemsCostPrice icp ON icp.ItemID = i.ID
                                INNER JOIN ItemsSellingPrice isp ON isp.ItemID = i.ID ";

                if (cmbIncludeItems.Text == "Only Bought")
                {
                    sql += "WHERE i.IsBought = 1 AND i.IsSold = 0 AND i.IsCounted = 0";
                }
                else if (cmbIncludeItems.Text == "Only Sold")
                {
                    sql += "WHERE i.IsBought = 0 AND i.IsSold = 1 AND i.IsCounted = 0";
                }
                else if (cmbIncludeItems.Text == "Only Inventoried")
                {
                    sql += "WHERE i.IsBought = 0 AND i.IsSold = 0 AND i.IsCounted = 1";
                }
                else 
                {
                    sql += "WHERE i.IsBought = 1 AND i.IsSold = 1 AND i.IsCounted = 1";
                }
            if (list1 != "")
                {
                    sql += " ANDc i.CList1 = @CList1";
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


        private void RptItemDetails_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadCategory();
            cmbIncludeItems.SelectedIndex = 3;
          //  cmbSort.SelectedIndex = 1;
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

        private void txtSupplier_TextChanged_1(object sender, EventArgs e)
        {
            if (txtSupplier.Text == "")
            {
                supplierID = "";
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
            dgPrinter.SubTitle = "Item Details Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ItemNo", 80);
            dgPrinter.ColumnWidths.Add("ItemName", 100);
            dgPrinter.ColumnWidths.Add("Supplier", 100);
            dgPrinter.ColumnWidths.Add("UnitsHand", 70);
            dgPrinter.ColumnWidths.Add("TotalValue", 110);
            dgPrinter.ColumnWidths.Add("AssetID", 60);
            dgPrinter.ColumnWidths.Add("IncomeID", 60);
            dgPrinter.ColumnWidths.Add("COSID", 60);
            dgPrinter.ColumnWidths.Add("SellingPrice", 70); 
            dgPrinter.ColumnWidths.Add("SNumPer", 60);
            dgPrinter.ColumnWidths.Add("SalesTax",40);
            dgPrinter.ColumnWidths.Add("BuyingPrice", 70);
            dgPrinter.ColumnWidths.Add("BUnit",60);
            dgPrinter.ColumnWidths.Add("BTax",40); 
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
                    ws.Cells[4, 1] = "Item Number";
                    ws.Cells[4, 2] = "Item Name";
                    ws.Cells[4, 3] = "Supplier";
                    ws.Cells[4, 4] = "Units on Hand";
                    ws.Cells[4, 5] = "Total Value";
                    ws.Cells[4, 6] = "Asset Account";
                    ws.Cells[4, 7] = "Income Account";
                    ws.Cells[4, 8] = "Cost Of Sale Account";
                    ws.Cells[4, 9] = "Selling Price";
                    ws.Cells[4, 10] = "Qty Per Selling Unit";
                    ws.Cells[4, 11] = "Sales Tax Code";
                    ws.Cells[4, 12] = "Buying Price";
                    ws.Cells[4, 13] = "Qty Per Buying Unit";
                    ws.Cells[4, 14] = "Purchase Tax Code";
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
                        if (item.Cells[8].Value != null && item.Cells[8].Value.ToString() != "")
                        {
                            double SellingPrice = Double.Parse(item.Cells[8].Value.ToString());
                            ws.Cells[i, 9] = SellingPrice.ToString("C");
                        }
                        if (item.Cells[9].Value != null)
                        {
                            ws.Cells[i, 10] = item.Cells[9].Value.ToString();
                        }
                        if (item.Cells[10].Value != null)
                        {
                            ws.Cells[i, 11] = item.Cells[10].Value.ToString();
                        }
                        if (item.Cells[11].Value != null && item.Cells[11].Value.ToString() != "")
                        {
                            double BuyingPrice = Double.Parse(item.Cells[11].Value.ToString());
                            ws.Cells[i, 12] = BuyingPrice.ToString("C");
                        }
                        if (item.Cells[2].Value != null)
                        {
                            ws.Cells[i, 13] = item.Cells[12].Value.ToString();
                        }
                        if (item.Cells[13].Value != null)
                        {
                            ws.Cells[i, 14] = item.Cells[13].Value.ToString();
                        }
                       
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "N3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Item Details Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "N4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Item Details Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
