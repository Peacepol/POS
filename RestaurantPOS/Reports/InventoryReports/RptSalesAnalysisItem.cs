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
using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;

namespace AbleRetailPOS.Reports.InventoryReports
{
    public partial class RptSalesAnalysisItem : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        private string categories = "";
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection con;
        string selectSql = "";
        string reportName;
        string reportTitle;
        string shippingID;
        string salespersonID;
        bool promised = false;
        DateTime TimeNow = DateTime.Now;
        private bool CanView = false;
        public RptSalesAnalysisItem()
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
            sdatePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edatePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
        void LoadReport()
        {
            
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
        private void GetReportData()
        {
            filterCategories();
            selectSql = @"Select  i.PartNumber,i.ItemNumber,  i.ItemName,  p.Name , s.SalesNumber, s.TransactionDate,
                        sl.ShipQty, sl.SubTotal , sl.TotalCost, sl.SubTotal - sl.TotalCost as Profit,'0.00%' as Margin
                        From Items i 
                        INNER JOIN SalesLines sl ON i.ID = sl.EntityID
                        INNER JOIN Sales s On s.SalesID = sl.SalesID
                        INNER JOIN Category c on i.CategoryID = c.CategoryID
                        INNER JOIN Profile p ON p.ID = s.CustomerID  WHERE SalesType = 'INVOICE' AND s.TransactionDate BETWEEN @sdate AND @edate";
            if (list1 != "")
            {
                selectSql += " AND c i.CList1 = @CList1";
            }
            if (list2 != "")
            {
                selectSql += " AND i.CList2 = @CList2";
            }
            if (list3 != "")
            {
                selectSql += " AND i.CList3 = @CList3";
            }
            if (supplierID != "")
            {
                selectSql += " AND i.SupplierID = @supplier";
            }
            if (categories.Length > 0)
            {
                selectSql += " AND i.CategoryID in (" + categories + ")";
            }
            try
            {
               
                con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);
                con.Open();
                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();
              

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);

                da = new SqlDataAdapter();
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
        void LoadReport(int pViewType = 0)
        {

            if (pViewType == 0)
            {
                reportName = "ItemSalesAnalysis.rpt";
                reportTitle = "Item Sales Analysis";
                Reports.ReportParams SalesItemDetails = new Reports.ReportParams();
                SalesItemDetails.PrtOpt = 1;
                SalesItemDetails.Rec.Add(TbRep);
                SalesItemDetails.ReportName = reportName;
                SalesItemDetails.RptTitle = reportTitle;
                SalesItemDetails.Params = "compname";
                SalesItemDetails.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(SalesItemDetails);
            }
            else
            {

                CalculateTotal(1);
                this.dgReport.DataSource = TbGrid;
                FormatGrid();
                foreach(DataGridViewRow dgvr in dgReport.Rows)
                {
                    if(dgvr.Cells[0].Value.ToString() == "TOTAL")
                    {

                        dgReport.Rows[dgvr.Index].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[dgvr.Index].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    }
                }
                //FillSortCombo();


            }

        }
        private void CalculateTotal(int pSortIndex = 1, string pSortMode = "asc")
        {
            TbGrid = TbRep.Clone();
            float TotalAmount = 0;
            float TotalQty = 0;
            float TotalCost = 0;
            float TotalProfit = 0;
            float ItemTotalQty = 0;
            float ItemTotalAmount = 0;
            float ItemTotalCost = 0;
            float ItemTotalProfit = 0;
            float lMargin = 0;
            string lPartNo = "";
            float lProfit = 0;
            float lSubTotal = 0;
            DataRow rw;
            DateTime lTranDate;
            DateTime lPromiseDate;
            for (int i = 0; i < TbRep.Rows.Count; i++)
            {
                //if(lPartNo != TbRep.Rows[i]["PartNumber"].ToString())
                //{
                //    if (i != 0)
                //    {
                //        rw = TbGrid.NewRow();
                //        rw[0] = "TOTAL";
                //        rw[6] = ItemTotalQty;
                //        rw[7] = ItemTotalAmount;
                //        TbGrid.Rows.Add(rw);
                //        ItemTotalQty = 0;
                //        ItemTotalAmount = 0;
                //    }
                //}

                TotalAmount += float.Parse(TbRep.Rows[i]["SubTotal"].ToString());
                TotalQty += float.Parse(TbRep.Rows[i]["ShipQty"].ToString());
                TotalCost += float.Parse(TbRep.Rows[i]["TotalCost"].ToString());
                TotalProfit += float.Parse(TbRep.Rows[i]["Profit"].ToString());
                ItemTotalAmount += float.Parse(TbRep.Rows[i]["SubTotal"].ToString());
                ItemTotalCost += float.Parse(TbRep.Rows[i]["TotalCost"].ToString());
                ItemTotalProfit += float.Parse(TbRep.Rows[i]["Profit"].ToString());
                ItemTotalQty += float.Parse(TbRep.Rows[i]["ShipQty"].ToString());
                lTranDate = Convert.ToDateTime(TbRep.Rows[i]["TransactionDate"].ToString()).ToLocalTime();
                lSubTotal = float.Parse(TbRep.Rows[i]["SubTotal"].ToString());
                lProfit = float.Parse(TbRep.Rows[i]["Profit"].ToString());
                lMargin = 0;
                if (lSubTotal > 0 && lProfit != 0)
                {
                    lMargin = lProfit / lSubTotal;
                }

                

                rw = TbGrid.NewRow();
                rw[0] = TbRep.Rows[i][0];
                rw[1] = TbRep.Rows[i][1];
                rw[2] = TbRep.Rows[i][2];
                rw[3] = TbRep.Rows[i][3];
                rw[4] = TbRep.Rows[i][4];
                rw[5] = lTranDate.ToShortDateString();
                rw[6] = TbRep.Rows[i][6];
                rw[7] = TbRep.Rows[i][7];
                rw[8] = TbRep.Rows[i][8];
                rw[9] = TbRep.Rows[i][9];
                rw[10] = Math.Round(lMargin *100, 2).ToString() + "%";
                TbGrid.Rows.Add(rw);
                lPartNo = TbRep.Rows[i]["PartNumber"].ToString();

            }
                       

            //rw = TbGrid.NewRow();
            //rw[0] = "TOTAL";
            //rw[6] = ItemTotalQty;
            //rw[7] = ItemTotalAmount;
            //TbGrid.Rows.Add(rw);

            rw = TbGrid.NewRow();
            rw[0] = "GRAND TOTAL";
            rw[6] = TotalQty;
            rw[7] = TotalAmount;
            rw[8] = TotalCost;
            rw[9] = TotalProfit;
            rw[10] = Math.Round(TotalProfit/ TotalAmount * 100,2).ToString() + "%";
            TbGrid.Rows.Add(rw);
        }

        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Part Number";
            this.dgReport.Columns[1].HeaderText = "Item Number";
            this.dgReport.Columns[2].HeaderText = "Item Name";
            this.dgReport.Columns[3].HeaderText = "Customer Name";
            this.dgReport.Columns[4].HeaderText = "Sales Number";
            this.dgReport.Columns[5].HeaderText = "Date";
            this.dgReport.Columns[6].HeaderText = "Quantity";
            this.dgReport.Columns[7].HeaderText = "Total Amount(Ex)";
            this.dgReport.Columns[8].HeaderText = "Total Cost";
            this.dgReport.Columns[9].HeaderText = "Profit";
            this.dgReport.Columns[10].HeaderText = "Profit Margin";

            this.dgReport.Columns[6].DefaultCellStyle.Format = "F";
            this.dgReport.Columns[7].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[8].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[9].DefaultCellStyle.Format = "C2";
            //this.dgReport.Columns[10].DefaultCellStyle.Format = "##.#0%";
            this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;

        }
     


        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(0);
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
           
            GetReportData();
            LoadReport(1);
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
             DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Item Sales Analysis Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("PartNumber", 90);
            dgPrinter.ColumnWidths.Add("ItemNumber", 90);
            dgPrinter.ColumnWidths.Add("ItemName", 150);
            dgPrinter.ColumnWidths.Add("Name", 150);
            dgPrinter.ColumnWidths.Add("SalesNumber", 100);
            dgPrinter.ColumnWidths.Add("Date", 70);
            dgPrinter.ColumnWidths.Add("ShipQty", 70);
            dgPrinter.ColumnWidths.Add("TotalAmount", 100);
            dgPrinter.ColumnWidths.Add("Total Cost", 80);
            dgPrinter.ColumnWidths.Add("Profit", 70);
            dgPrinter.ColumnWidths.Add("Margin", 70);
            dgPrinter.PageSettings.Landscape = true;
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.PrintPreviewDataGridView(dgReport);
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
                    ws.Cells[4, 1] = "Part Number";
                    ws.Cells[4, 2] = "Item Number";
                    ws.Cells[4, 3] = "Item Name";
                    ws.Cells[4, 4] = "Customer Name";
                    ws.Cells[4, 5] = "Sales Number";
                    ws.Cells[4, 6] = "Date";
                    ws.Cells[4, 7] = "Quantity";
                    ws.Cells[4, 8] = "Total Amount(Ex)";
                    ws.Cells[4, 9] = "Total Cost";
                    ws.Cells[4, 10] = "Profit";
                    ws.Cells[4, 11] = "Margin";

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
                            ws.Cells[i, 6] = Convert.ToDateTime(item.Cells[5].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            double TotalAmount = double.Parse(item.Cells[7].Value.ToString());
                            ws.Cells[i, 8] = TotalAmount.ToString("C");
                        }
                        if (item.Cells[8].Value != null && item.Cells[8].Value.ToString() != "")
                        {
                            double TotalCost = double.Parse(item.Cells[8].Value.ToString());
                            ws.Cells[i, 9] = TotalCost.ToString("C");
                        }
                        if (item.Cells[9].Value != null && item.Cells[9].Value.ToString() != "")
                        {
                            double TotalProfit = double.Parse(item.Cells[9].Value.ToString());
                            ws.Cells[i, 10] = TotalProfit.ToString("C");
                        }
                        if (item.Cells[10].Value != null && item.Cells[10].Value.ToString() != "")
                        {
                            ws.Cells[i, 11] = item.Cells[10].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "K3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Item Sales Analysis";

                    //Style Table
                    cellRange = ws.get_Range("A4", "K4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("B5").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("J5").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("G5").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    ws.get_Range("G5").EntireColumn.NumberFormat = "0.00";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Item Sales Analysis Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptSalesItemDetails_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
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

       
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void treeCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            treeCategory.BeginUpdate();
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
            treeCategory.EndUpdate();
        }

        private void cancel_btn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
