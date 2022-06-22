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
    public partial class RptCategoryList : Form
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
        public RptCategoryList()
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
        private void LoadReport()
        {

            string sql = @"SELECT * FROM Category";
            //if (list1 != "")
            //{
            //    sql += " AND i.CList1 = @CList1";
            //}
            //if (list2 != "")
            //{
            //    sql += " AND i.CList2 = @CList2";
            //}
            //if (list3 != "")
            //{
            //    sql += " AND i.CList3 = @CList3";
            //}
            //if (supplierID != "")
            //{
            //    sql += " AND i.SupplierID = @supplier";
            //}
            //if (categories.Length > 0)
            //{
            //    sql += " AND CategoryID in (" + categories + ")";
            //}
            //if (cmbSort.Text == "Item Number")
            //{
            //    sql += " ORDER BY ItemNumber DESC";
            //}
            //else if (cmbSort.Text == "Part Number")
            //{
            //    sql += " ORDER BY PartNumber DESC";
            //}

            TbRep = new System.Data.DataTable();
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("@supplier", supplierID);
            //param.Add("@CList1", list1);
            //param.Add("@CList2", list2);
            //param.Add("@CList3", list3);
            CommonClass.runSql(ref TbRep, sql);//, param);
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            dgReport.Rows.Clear();
            //filterCategories();
            LoadReport();
            TbGrid = TbRep.Copy();
            int rIndex = 0;
            int rSubCat = 0;
            string ItemID = "";
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                //dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];

                    if (TbRep.Rows[i]["MainCategoryID"].ToString() == "0") {
                        dgReport.Rows.Add();
                        rIndex = dgReport.Rows.Count - 1;
                        ItemID = TbRep.Rows[i]["CategoryID"].ToString();
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        dgReport.Rows[rIndex].Cells["Description"].Value = TbRep.Rows[i]["CategoryCode"];
                        dgReport.Rows[rIndex].Cells["CatID"].Value = TbRep.Rows[i]["CategoryID"];
                        for (int x = 0; x < TbRep.Rows.Count; x++)
                        {
                            string curID = TbRep.Rows[x]["MainCategoryID"].ToString();
                            rSubCat = dgReport.Rows.Count - 1;
                            if (ItemID == curID)
                            {
                                dgReport.Rows.Add();
                                dgReport.Rows[rSubCat+1].Cells["Description"].Value = TbRep.Rows[x]["Description"];
                                dgReport.Rows[rSubCat+1].Cells["CatID"].Value = TbRep.Rows[x]["CategoryID"];
                                dgReport.Rows[rSubCat+1].Cells["IncomeGLCode"].Value = TbRep.Rows[x]["IncomeGLCode"];
                                dgReport.Rows[rSubCat+1].Cells["COSGLCode"].Value = TbRep.Rows[x]["COSGLCode"];
                                dgReport.Rows[rSubCat+1].Cells["InventoryGLCode"].Value = TbRep.Rows[x]["InventoryGLCode"];
                                dgReport.Rows[rSubCat+1].Cells["ItemType"].Value = TbRep.Rows[x]["ItemType"];
                            }
                        }
                    }
                }
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //FillSortCombo();
            }
            else
            {
                MessageBox.Show("Contains No Data.", "Report Information");
            }
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Category List";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            //dgPrinter.ColumnWidths.Add("Main Category", 80);
            //dgPrinter.ColumnWidths.Add("Sub Category", 80);
            //dgPrinter.ColumnWidths.Add("PartNum", 70);
            //dgPrinter.ColumnWidths.Add("ItemNum", 80);
            //dgPrinter.ColumnWidths.Add("ItemDesc", 80);
            //dgPrinter.ColumnWidths.Add("OnHand", 80);
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //filterCategories();
            LoadReport();
            Reports.ReportParams CategoryList = new Reports.ReportParams();
            CategoryList.PrtOpt = 1;
            CategoryList.Rec.Add(TbRep);
            CategoryList.ReportName = "CategoryList.rpt";
            CategoryList.RptTitle = "Category List";
            CategoryList.Params = "compname";
            CategoryList.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(CategoryList);
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
                    ws.Cells[4, 1] = "Category ID";
                    ws.Cells[4, 2] = "Description";
                    ws.Cells[4, 3] = "Income GL Code";
                    ws.Cells[4, 4] = "COS GL Code";
                    ws.Cells[4, 5] = "Inventory GL Code";
                    ws.Cells[4, 6] = "Item Type";

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
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "F3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Category List";

                    //Style Table
                    cellRange = ws.get_Range("A4", "F4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Category List has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptCategoryList_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
