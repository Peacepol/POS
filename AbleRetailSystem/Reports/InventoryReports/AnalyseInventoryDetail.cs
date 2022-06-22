using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Subro.Controls;
using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;

namespace RestaurantPOS.Reports.InventoryReports
{
    public partial class AnalyseInventoryDetail : Form
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
        public AnalyseInventoryDetail()
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
            Cursor.Current = Cursors.WaitCursor;
            filterCategories();
            LoadReport();

            Reports.ReportParams itemparams = new Reports.ReportParams();
            itemparams.PrtOpt = 1;
            itemparams.Rec.Add(TbRep);
            itemparams.ReportName = "AnalyseInventoryDetail.rpt";
            itemparams.RptTitle = "Analyse Inventory Detail";

            itemparams.Params = "compname";
            itemparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(itemparams);
            Cursor.Current = Cursors.Default;


        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            filterCategories();
            LoadReport();
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                string lPrevItem = "";
                string[] RowArray;
                int rIndex = 0;
                dgReport.Rows.Clear();
                for (int i= 0; i< TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];

                    if (lPrevItem != dr["ItemName"].ToString() && lPrevItem != "")
                    {
                        RowArray = new string[9];
                        RowArray[0] = "TOTAL :";
                        RowArray[5] = totalCommitted(lPrevItem).ToString("F");
                        RowArray[6] = totalOrder(lPrevItem).ToString("F");
                        RowArray[7] = totalAvailable(lPrevItem).ToString("F");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                    }
                        if (lPrevItem != dr["ItemName"].ToString())
                    {
                        RowArray = new string[9];
                        RowArray[0] = ">>";
                        RowArray[1] = dr["ItemName"].ToString();
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray = new string[9];
                        RowArray[0] = dr["IDNumber"].ToString();
                        RowArray[1] = dr["ItemName"].ToString();
                        RowArray[2] = dr["ProfileName"].ToString();
                        RowArray[3] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                        RowArray[4] = Convert.ToDateTime(dr["PromiseDate"].ToString()).ToShortDateString();
                        RowArray[5] = float.Parse(dr["Committed"].ToString()).ToString("F");
                        RowArray[6] = float.Parse(dr["OnOrder"].ToString()).ToString("F");
                        RowArray[7] = float.Parse(dr["Available"].ToString()).ToString("F");
                        dgReport.Rows.Add(RowArray);
                        lPrevItem = dr["ItemName"].ToString();
                    }
                    else
                    {
                        RowArray = new string[9];
                        RowArray[0] = dr["IDNumber"].ToString();
                        RowArray[1] = dr["ItemName"].ToString();
                        RowArray[2] = dr["ProfileName"].ToString();
                        RowArray[3] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                        RowArray[4] = Convert.ToDateTime(dr["PromiseDate"].ToString()).ToShortDateString();
                        RowArray[5] = float.Parse(dr["Committed"].ToString()).ToString("F");
                        RowArray[6] = float.Parse(dr["OnOrder"].ToString()).ToString("F");
                        RowArray[7] = float.Parse(dr["Available"].ToString()).ToString("F");
                        dgReport.Rows.Add(RowArray);
                    }
                        
                    if (TbRep.Rows.Count-1 == i)
                    {
                        RowArray = new string[9];
                        RowArray[0] = "TOTAL :";
                        RowArray[5] = totalCommitted(dr["ItemName"].ToString()).ToString("F");
                        RowArray[6] = totalOrder(dr["ItemName"].ToString()).ToString("F");
                        RowArray[7] = totalAvailable(dr["ItemName"].ToString()).ToString("F");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    }
                 
                }
                //dgReport.DataSource = TbRep;
                //dgReport.Columns["OnHandQty"].Visible = false;
                //var grouper = new Subro.Controls.DataGridViewGrouper(dgReport);
                //grouper.SetGroupOn("ItemName");
                //grouper.DisplayGroup += grouper_DisplayGroup;
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
        void grouper_DisplayGroup(object sender, GroupDisplayEventArgs e)
        {
            e.Summary =  " Total Committed : " + totalCommitted(e.Group.Value.ToString()) +"   Total Order : " + totalOrder(e.Group.Value.ToString()) + "   Total Available : " + totalAvailable(e.Group.Value.ToString());
        }
        public decimal totalCommitted(string ItemName)
        {
            decimal x = 0;
            foreach(DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["ItemName"].ToString() )
                {
                    x += decimal.Parse(dr["Committed"].ToString());
                }
            }
            return x;
        }
        public decimal totalOrder(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["ItemName"].ToString())
                {
                    x += decimal.Parse(dr["OnOrder"].ToString());
                }
            }
            return x;
        }
        public decimal totalAvailable(string ItemName)
        {
            decimal x = 0;
            decimal y = 0;
            decimal i = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["ItemName"].ToString())
                {
                    x += decimal.Parse(dr["OnOrder"].ToString());
                    y += decimal.Parse(dr["Committed"].ToString());
                    i = decimal.Parse(dr["OnHandQty"].ToString());
                }
            }
           
            return  i-x+y;
        }

        private void LoadReport()
        {

            string sql = @"SELECT 
                            PartNumber,
                            ItemNumber,
                            ItemName,
                            s.SalesNumber AS IDNumber,
                            p.Name AS ProfileName,
                            s.TransactionDate,
                            s.PromiseDate,
                            (SELECT COUNT(SalesID) FROM Sales WHERE SalesType='ORDER'AND InvoiceStatus='Order') AS Committed,
                            0 AS OnOrder,
                            0 AS Available,
                            iq.OnHandQty
                            FROM Items i
                            LEFT JOIN ItemsQty iq ON iq.ItemID = i.ID
                            LEFT JOIN SalesLines sl ON sl.EntityID = i.ID
                            LEFT JOIN Sales s ON sl.SalesID = s.SalesID
                            LEFT JOIN Profile p ON p.ID = s.CustomerID
                            WHERE s.SalesType = 'ORDER'
                            AND s.InvoiceStatus = 'Order'
                            UNION
                            SELECT 
                            PartNumber,
                            ItemNumber,
                            ItemName,
                            s.PurchaseNumber AS IDNumber,
                            p.Name AS ProfileName,
                            s.TransactionDate,
                            s.PromiseDate,
                            0 AS committed,
                            ((SELECT COUNT(PurchaseID) FROM Purchases WHERE PurchaseType = 'ORDER' AND POStatus = 'Backordered') + (sl.OrderQty - sl.ReceiveQty)) AS OnOrder,
                            (iq.OnHandQty) - 0 + ((SELECT COUNT(PurchaseID) FROM Purchases WHERE PurchaseType = 'ORDER' AND POStatus = 'Backordered') + (sl.OrderQty - sl.ReceiveQty)) AS Available,
                            iq.OnHandQty
                            FROM Items i
                            INNER JOIN ItemsQty iq ON iq.ItemID = i.ID
                            LEFT JOIN PurchaseLines sl ON sl.EntityID = i.ID
                            LEFT JOIN Purchases s ON sl.PurchaseID = s.PurchaseID
                            LEFT JOIN Profile p ON p.ID = s.SupplierID
                            WHERE s.PurchaseType = 'ORDER'
                            AND POStatus = 'Backordered'";

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
            if (list1 != "")
            {
                sql += " OR i.CList1 = @CList1";
            }
            if (list2 != "")
            {
                sql += " OR i.CList2 = @CList2";
            }
            if (list3 != "")
            {
                sql += " OR i.CList3 = @CList3";
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
        


        private void AnalyseInventoryDetail_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadCategory();
            cmbIncludeItems.SelectedIndex = 3;
         
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
      

        private void dgReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
      //      if (e.ColumnIndex == 6 || e.ColumnIndex == 5

      //   && e.RowIndex != this.dgReport.NewRowIndex)
      //      {
      //          if (e.Value != null)
      //          {
      //              string d = DateTime.Parse(e.Value.ToString()).ToShortDateString(); ;
      //              e.Value = d.ToString();
      //          }
      //      }
      //      if (e.ColumnIndex == 7|| e.ColumnIndex == 8 || e.ColumnIndex == 9 

      //&& e.RowIndex != this.dgReport.NewRowIndex)
      //      {
      //          if (e.Value != null)
      //          {
      //              double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
      //              e.Value = d.ToString("F");
      //          }
      //      }
        }


        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? "asc" : "desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
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

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
             DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Analyse Inventory Detail Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("IDNum", 80);
            dgPrinter.ColumnWidths.Add("ItemName", 100);
            dgPrinter.ColumnWidths.Add("Supplier",120);
            dgPrinter.ColumnWidths.Add("DateOrdered", 80);
            dgPrinter.ColumnWidths.Add("DatePromised", 80);
            dgPrinter.ColumnWidths.Add("Committed", 90);
            dgPrinter.ColumnWidths.Add("OnOrder", 90);
            dgPrinter.ColumnWidths.Add("Available", 90);
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

        }

        private void btnExportExcel_Click_1(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    // app.Visible = false;
                    ws.Cells[4, 1] = "ID Number";
                    ws.Cells[4, 2] = "Item Name";
                    ws.Cells[4, 3] = "Supplier";
                    ws.Cells[4, 4] = "Date Ordered";
                    ws.Cells[4, 5] = "Date Promised";
                    ws.Cells[4, 6] = "Committed";
                    ws.Cells[4, 7] = "On Order";
                    ws.Cells[4, 8] = "Available";

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
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "H3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Analyse Inventory Detail Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "H4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Analyse Inventory Detail Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
