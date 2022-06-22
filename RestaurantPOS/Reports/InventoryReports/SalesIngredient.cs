using AbleRetailPOS.Inventory;
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
    public partial class SalesIngredient : Form
    {
        private System.Data.DataTable TbRep = new System.Data.DataTable();
        private DateTime sdate = new DateTime();
        private DateTime edate = new DateTime();
        private string SelItemID = "";

        string mSupplierID;
        string mUserID;
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        string categories = "";
        string ItemID = "";
        private bool CanView = false;
        public SalesIngredient()
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
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
            sdate = sdateTimePicker.Value;
            edate = edateTimePicker.Value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            filterCategories();
            LoadItemTransactions();
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
        private void LoadItemTransactions()
        {
            string sql = "";
            string sqlfilter = "";
            string itemcon = "";
            itemcon = (cmb_searby.SelectedIndex == 1 ? " and i.ID = " + SelItemID : "");
            string itemcon1 = "";
            itemcon1 = (cmb_searby.SelectedIndex == 1 ? " and ItemID = " + SelItemID : "");
            if (toNum.Value != 0 || fromNum.Value != 0)
            {
                sqlfilter += " AND t.TotalCostEx BETWEEN " + fromNum.Value + " AND " + toNum.Value;
            }
            if (txtUser.Text != "")
            {
                sqlfilter += " AND t.UserID = " + mUserID;
            }
            if (categories.Length > 0)
            {
                sqlfilter += " AND i.CategoryID in (" + categories + ") ";
            }
            if (supplierID != "")
            {
                sqlfilter += " AND i.SupplierID = @supplier ";
            }
            sql += @"SELECT i.*, CAST(0 as float) as [On Hand], ISNULL(b.BegQty,0) as BegQty from ( SELECT t.TransactionDate as [Transaction Date], t.TranType,p.SalesNumber as  [Transaction Number],  
                         i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'S' as formtype, t.ItemID
                            FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join Sales as p on t.SourceTranID = p.SalesID where TranType IN ('SII') and t.TransactionDate Between @sdate and @edate ";
            if (sqlfilter != "")
            {
                sql += sqlfilter;
            }

            sql += " ) as i LEFT JOIN ( SELECT ItemID, Sum(QtyAdjustment) as BegQty from ItemTransaction where TransactionDate < @sdate " + itemcon1 + " group by ItemID ) as b on i.ItemID = b.ItemID order by i.[Transaction Date]";


            sdate = new DateTime(sdateTimePicker.Value.Year, sdateTimePicker.Value.Month, sdateTimePicker.Value.Day, 00, 00, 00).ToUniversalTime();
            edate = new DateTime(edateTimePicker.Value.Year, edateTimePicker.Value.Month, edateTimePicker.Value.Day, 23, 59, 59).ToUniversalTime();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@supplier", supplierID);
            param.Add("@sdate", sdate);
            param.Add("@edate", edate);
            param.Add("@ItemID", ItemID);
            CommonClass.runSql(ref TbRep, sql, param);
            if (TbRep.Rows.Count > 0)
            {
                for(int i = 0; i< TbRep.Rows.Count; i++)
                {
                    DateTime lLocal = (DateTime)TbRep.Rows[i]["Transaction Date"];
                    TbRep.Rows[i]["Transaction Date"] = lLocal.ToLocalTime();
                }
                this.dgReport.DataSource = TbRep;
                FormatGrid();
            }
        }
        public void LoadReport()
        {
            Reports.ReportParams ingUsed = new Reports.ReportParams();
            ingUsed.PrtOpt = 1;
            ingUsed.Rec.Add(TbRep);
            ingUsed.ReportName = "UsedIngredient.rpt";
            ingUsed.RptTitle = "Used Ingredient";
            ingUsed.Params = "compname|Startdate|Enddate";
            ingUsed.PVals = CommonClass.CompName.Trim() + "|" + sdateTimePicker.Value.ToString("yyyy-MM-dd") + "|" + edateTimePicker.Value.ToString("yyyy-MM-dd");

            CommonClass.ShowReport(ingUsed);
        }

        private void pbUser_Click(object sender, EventArgs e)
        {
            SalespersonLookup SalespersonDlg = new SalespersonLookup();
            if (SalespersonDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lSales = SalespersonDlg.GetSalesperson;
                mUserID = lSales[0];
                txtUser.Text = lSales[1];
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
        private void treeCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            treeCategory.BeginUpdate();
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
            treeCategory.EndUpdate();
        }

        private void txtProfile_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            if (txtSupplier.Text == "")
            {
                supplierID = "";
            }
        }

        private void SalesIngredient_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filterCategories();
            LoadItemTransactions();
            LoadReport();
        }

        private void cmb_searby_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Transaction Date";
            this.dgReport.Columns[1].HeaderText = "Transaction Type";
            this.dgReport.Columns[2].HeaderText = "Transaction #";
            this.dgReport.Columns[3].HeaderText = "Part #";
            this.dgReport.Columns[4].HeaderText = "Item Name";
            this.dgReport.Columns[5].HeaderText = "Qty Adjustment";
            this.dgReport.Columns[6].HeaderText = "Total Cost";
            this.dgReport.Columns[7].Visible = false; //source of transaction ID
            this.dgReport.Columns[8].HeaderText = "Form Type";
            this.dgReport.Columns[9].Visible = false; //Item ID
            this.dgReport.Columns[10].HeaderText = "On Hand Qty";
            this.dgReport.Columns[11].HeaderText = "Beginning Qty";


            this.dgReport.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgReport.Columns[6].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[11].DefaultCellStyle.Format = "F";
            this.dgReport.Columns[10].DefaultCellStyle.Format = "F";

            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                    ws.Cells[4, 1] = "Transaction Date";
                    ws.Cells[4, 2] = "Transaction Type";
                    ws.Cells[4, 3] = "Transaction #";
                    ws.Cells[4, 4] = "Part #";
                    ws.Cells[4, 5] = "Item Name";
                    ws.Cells[4, 6] = "Qty Adjustment";
                    ws.Cells[4, 7] = "Total Cost";
                    ws.Cells[4, 8] = "Form Type";
                    ws.Cells[4, 9] = "On Hand Qty";
                    ws.Cells[4, 10] = "Beginning Qty";

                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {
                        if (item.Cells[0].Value != null && item.Cells[0].Value.ToString() != "")
                        {
                            ws.Cells[i, 1] = Convert.ToDateTime(item.Cells[0].Value.ToString()).ToShortDateString();
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
                        if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                        {
                            ws.Cells[i, 7] = Math.Round(float.Parse(item.Cells[6].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[8].Value != null && item.Cells[8].Value.ToString() != "")
                        {
                            ws.Cells[i, 8] = item.Cells[8].Value.ToString();
                        }
                        if (item.Cells[10].Value != null && item.Cells[10].Value.ToString() != "")
                        {
                            ws.Cells[i, 9] = item.Cells[10].Value.ToString();
                        }
                        if (item.Cells[11].Value != null && item.Cells[11].Value.ToString() != "")
                        {
                            ws.Cells[i, 10] = item.Cells[11].Value.ToString();
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
                    ws.Cells[1, 1] = "Sales Ingredients Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "J4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales Ingredients Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Sales Ingredients Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("TransactionDate", 90);
            dgPrinter.ColumnWidths.Add("Transaction Type", 80);
            dgPrinter.ColumnWidths.Add("Transaction #", 80);
            dgPrinter.ColumnWidths.Add("Part #", 100);
            dgPrinter.ColumnWidths.Add("Item Name", 100);
            dgPrinter.ColumnWidths.Add("Qty Adjustment", 100);
            dgPrinter.ColumnWidths.Add("Total Cost", 80);
            dgPrinter.ColumnWidths.Add("Form Type", 80);
            dgPrinter.ColumnWidths.Add("On Hand Qty", 80);
            dgPrinter.ColumnWidths.Add("Beginning Qty", 80);

            //dgPrinter.ColumnWidths.Add("OnOrder", 80);
            //dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }
    }
}
