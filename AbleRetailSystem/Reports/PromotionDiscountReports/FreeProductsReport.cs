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

namespace RestaurantPOS.Reports.PromotionDiscountReports
{
    public partial class FreeProductsReport : Form
    {
        private System.Data.DataTable Tbrep;
        private System.Data.DataTable TbGrid;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public FreeProductsReport()
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
            string sql = @"SELECT pl.Name, s.SalesNumber,  s.TransactionDate, i.PartNumber, i.ItemNumber, i.ItemName, u.user_fullname ,p.PromoCode 
								FROM Sales s
								INNER JOIN Users u ON u.user_id = s.SalesPersonID
                                INNER JOIN SalesLines sl ON sl.SalesID = s.SalesID
                                INNER JOIN Items i ON i.ID = sl.EntityID
                                INNER JOIN Profile pl ON pl.ID = s.CustomerID
								LEFT JOIN Promos p ON p.PromoID = sl.PromoID
								WHERE sl.UnitPrice = '0' AND s.TransactionDate BETWEEN @sdate AND @edate";
            if (customerText.Text != "")
            {
                sql += " AND  pl.Name LIKE '%" + customerText.Text + "%'";
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
        private void LoadFreeProducts()
        {
            Reports.ReportParams FreeProduct = new Reports.ReportParams();
            FreeProduct.PrtOpt = 1;
            FreeProduct.Rec.Add(Tbrep);
            FreeProduct.ReportName = "FreeProduct.rpt";
            FreeProduct.RptTitle = "Free Product Promos Report";
            FreeProduct.Params = "compname|StartDate|EndDate";
            FreeProduct.PVals = CommonClass.CompName.Trim() + "|" + dtmTxFrom.Value.ToShortDateString() + " |" + dtmTxTo.Value.ToShortDateString();
            CommonClass.ShowReport(FreeProduct);
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
                        RowArray = new string[15];
                        RowArray[0] = dr["Name"].ToString();
                        RowArray[1] = dr["SalesNumber"].ToString();
                        if (dr["TransactionDate"].ToString() != "")
                        {
                            RowArray[2] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                        }
                        RowArray[3] = dr["PartNumber"].ToString();
                        RowArray[4] = dr["ItemNumber"].ToString();
                        RowArray[5] = dr["ItemName"].ToString();
                        RowArray[6] = dr["user_fullname"].ToString();
                        RowArray[7] = dr["PromoCode"].ToString();
                        dgReport.Rows.Add(RowArray);
                    }
                }
                else
                {
                    MessageBox.Show("Contains No Data.", "Report Information");
                }
                Cursor.Current = Cursors.Default;
           
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Free Product Promos Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("CustomerName", 120);
            dgPrinter.ColumnWidths.Add("SalesNumber", 80);
            dgPrinter.ColumnWidths.Add("TransactionDate", 70);
            dgPrinter.ColumnWidths.Add("PartNumber", 80);
            dgPrinter.ColumnWidths.Add("ItemNumber", 80);
            dgPrinter.ColumnWidths.Add("ItemName", 120);
            dgPrinter.ColumnWidths.Add("User", 100);
            dgPrinter.ColumnWidths.Add("PromoCode", 80);
           
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
           // dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }
        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                this.customerText.Text = lProfile[2];
            }
        }
        private void pbAccount_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
            LoadFreeProducts();
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
                    ws.Cells[4, 1] = "Customer Name";
                    ws.Cells[4, 2] = "Sales Number";
                    ws.Cells[4, 3] = "Transaction Date";
                    ws.Cells[4, 4] = "Part Number";
                    ws.Cells[4, 5] = "Item Number";
                    ws.Cells[4, 6] = "Item Name";
                    ws.Cells[4, 7] = "User Name";
                    ws.Cells[4, 8] = "Promo Code";

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
                            ws.Cells[i, 3] = Convert.ToDateTime(item.Cells[2].Value.ToString()).ToShortDateString();
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
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "H3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 20;
                    ws.Cells[1, 1] = "Free Products Promos Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "H4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Free Products Promos Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void FreeProductsReport_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}

