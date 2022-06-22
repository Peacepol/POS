using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
using Subro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Purchase
{
    public partial class Replenishment : Form
    {
        System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        private string categories = "";
        private int index = 1;
        private string sort = " asc";
        public Replenishment()
        {
            InitializeComponent();
        }
        
        private void GenerateData()
        {
            DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
            DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

            string sql = @"SELECT i.ID, 
                                i.PartNumber, 
                                i.SupplierItemNumber, 
                                i.ItemName,                                
                                iq.MaxQty,                                
                                iq.ReOrderQty,
                                isnull(iss.SalesQty,0) SalesQty,
                                iq.OnHandQty,
                                isnull(ic.CommittedQty,0) CommittedQty,
                                isnull(io.OnOrderQty,0) OnOrderQty,  
                                CAST(0 as float) RequiredQty,
                                icp.LastCostEx,
                                CAST(0 as float) OrderAmount,
                                isnull(p.Name,'') as Supplier,
                                CAST(0 as bit) Include,
                                i.SupplierID,
                                i.PurchaseTaxCode,
								t.TaxPercentageRate,
                                t.TaxPaidAccountID
                              FROM Items i 
                              INNER JOIN ItemsQty iq ON i.ID = iq.ItemID 
                              LEFT JOIN Profile p ON p.ID = i.SupplierID
                              INNER JOIN ItemsCostPrice icp ON icp.ItemID = i.ID
                              INNER JOIN ItemsSellingPrice isp ON isp.ItemID = i.ID 
                              INNER JOIN TaxCodes t ON i.PurchaseTaxCode = t.TaxCode
                              LEFT JOIN (
                                Select l.EntityID, sum(l.OrderQty - l.ReceiveQty) as OnOrderQty 
                                from PurchaseLines l inner join Purchases p on l.PurchaseID = p.PurchaseID 
                                where p.POStatus in ('Active','Backordered') and l.OrderQty > l.ReceiveQty group by l.EntityID
                              ) io on i.ID = io.EntityID
                              LEFT JOIN (
                                Select l.EntityID, SUM(l.ShipQty) as CommittedQty 
                                from SalesLines l inner join Sales s on l.SalesID = s.SalesID 
                                where s.SalesType in ('Lay-By','Order') group by l.EntityID
                              ) ic on i.ID = ic.EntityID
                              LEFT JOIN (
                                Select l.EntityID, SUM(l.ShipQty) as SalesQty 
                                from SalesLines l inner join Sales s on l.SalesID = s.SalesID 
                                where s.SalesType in ('Invoice') 
                                and s.TransactionDate BETWEEN @sdate AND @edate group by l.EntityID
                              ) iss on i.ID = iss.EntityID
                              WHERE i.IsBought = 1 and i.IsCounted = 1 and iq.OnHandQty < iq.MaxQty ";
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
            sql += " order by i.SupplierID";

            Dictionary <string, object> param = new Dictionary<string, object>();
                param.Add( "@supplier", supplierID);
                param.Add("@CList1", list1);
                param.Add("@CList2", list2);
                param.Add("@CList3", list3);
                param.Add("@sdate", sdate);
                param.Add("@edate", edate);
            TbRep = new System.Data.DataTable();

            CommonClass.runSql(ref TbRep, sql, param);
               
        }

        private void FormatGrid()
        {
            
            this.dgReport.Columns[0].Visible = false;
            this.dgReport.Columns[1].HeaderText = "Part Number";
            this.dgReport.Columns[1].ReadOnly = true;
            this.dgReport.Columns[2].HeaderText = "Supplier Item No";
            this.dgReport.Columns[2].ReadOnly = true;
            this.dgReport.Columns[3].HeaderText = "Item Name";
            this.dgReport.Columns[3].ReadOnly = true;
            this.dgReport.Columns[4].HeaderText = "Max";
            this.dgReport.Columns[4].ReadOnly = true;            
            this.dgReport.Columns[5].HeaderText = "Re-Order Qty";
            this.dgReport.Columns[5].ReadOnly = true;
            this.dgReport.Columns[6].HeaderText = "Sales Qty";
            this.dgReport.Columns[6].ReadOnly = true;
            this.dgReport.Columns[7].HeaderText = "On Hand";
            this.dgReport.Columns[7].ReadOnly = true;
            this.dgReport.Columns[8].HeaderText = "Committed Qty";
            this.dgReport.Columns[8].ReadOnly = true;
            this.dgReport.Columns[9].HeaderText = "On Order Qty";
            this.dgReport.Columns[9].ReadOnly = true;
            this.dgReport.Columns[10].HeaderText = "Required Qty";
            this.dgReport.Columns[10].ReadOnly = true;
            this.dgReport.Columns[11].HeaderText = "Last Cost";
            this.dgReport.Columns[11].ReadOnly = true;
            this.dgReport.Columns[12].HeaderText = "Order Amount";
            this.dgReport.Columns[12].ReadOnly = true;
            this.dgReport.Columns[13].HeaderText = "Supplier";
            this.dgReport.Columns[13].ReadOnly = true;
            this.dgReport.Columns[14].HeaderText = "Check To Include";
            this.dgReport.Columns[14].ReadOnly = false;
            this.dgReport.Columns[15].Visible = false;           
            this.dgReport.Columns[11].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[12].DefaultCellStyle.Format = "C2";           
            this.dgReport.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
           
            
        }
        private void GetOnOrder(string pItemID)
        {
            SqlConnection con = null;
           
            try
            {
                string sql = @"WHERE i.ID = " + pItemID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                System.Data.DataTable ltb;
                ltb = new System.Data.DataTable();
                da.Fill(ltb);
                
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
        private void GetCommitted(string pItemID)
        {

        }



        private void RptItemSummary_Load(object sender, EventArgs e)
        {
            LoadCategory();
            this.rdoMinMax.Checked = true;
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
            if(txtSupplier.Text == "")
            {
                supplierID = "";
            }
        }

        private void txtList3_TextChanged(object sender, EventArgs e)
        {
            if(txtList3.Text == "")
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
            filterCategories();
            GenerateData();
            TbGrid = TbRep.Clone();
            
            if (TbRep.Rows.Count > 0)
            {
                float Required = 0;
                float Max = 0;
                float Sales = 0;
                float OnOrder = 0;
                float OnHand = 0;
                float Committed = 0;
                float Cost = 0;
                for(int i = 0; i < TbRep.Rows.Count; i++)
                {
                    Cost = float.Parse(TbRep.Rows[i]["LastCostEx"].ToString());
                    Max = float.Parse(TbRep.Rows[i]["MaxQty"].ToString());
                    OnOrder = float.Parse(TbRep.Rows[i]["OnOrderQty"].ToString());
                    OnHand = float.Parse(TbRep.Rows[i]["OnHandQty"].ToString());
                    Sales = float.Parse(TbRep.Rows[i]["SalesQty"].ToString());
                    Committed = float.Parse(TbRep.Rows[i]["CommittedQty"].ToString());
                    if (rdoMinMax.Checked)
                    {
                       
                        Required = (Max + Committed) - (OnHand + OnOrder) ;                        

                    }else
                    {
                        Required = Sales - (OnHand + OnOrder);

                    }
                    Required = Required < 0 ? 0 : Required;
                    TbRep.Rows[i]["RequiredQty"] = Required;
                    TbRep.Rows[i]["OrderAmount"] = Required * Cost;
                    if(Required > 0)
                    {
                        DataRow gr = TbGrid.NewRow();
                        gr[0] = TbRep.Rows[i][0];
                        gr[1] = TbRep.Rows[i][1];
                        gr[2] = TbRep.Rows[i][2];
                        gr[3] = TbRep.Rows[i][3];
                        gr[4] = TbRep.Rows[i][4];
                        gr[5] = TbRep.Rows[i][5];
                        gr[6] = TbRep.Rows[i][6];
                        gr[7] = TbRep.Rows[i][7];
                        gr[8] = TbRep.Rows[i][8];
                        gr[9] = TbRep.Rows[i][9];
                        gr[10] = TbRep.Rows[i][10];
                        gr[11] = TbRep.Rows[i][11];
                        gr[12] = TbRep.Rows[i][12];
                        gr[13] = TbRep.Rows[i][13];
                        gr[14] = TbRep.Rows[i][14];
                        gr[15] = TbRep.Rows[i][15];
                        gr[16] = TbRep.Rows[i][16];
                        gr[17] = TbRep.Rows[i][17];
                        gr[18] = TbRep.Rows[i][18];
                        TbGrid.Rows.Add(gr);

                    }
                   
                }

            }
            else
            {
                MessageBox.Show("Contains No Data.", "Report Information");
            }
            dgReport.DataSource = TbGrid;
            FormatGrid();
            Cursor.Current = Cursors.Default;
        }
       
      

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Item Summary Report";
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

                    this.dgReport.Columns[0].Visible = false;
                    this.dgReport.Columns[1].HeaderText = "Part Number";
                    this.dgReport.Columns[1].ReadOnly = true;
                    this.dgReport.Columns[2].HeaderText = "Supplier Item No";
                    this.dgReport.Columns[2].ReadOnly = true;
                    this.dgReport.Columns[3].HeaderText = "Item Name";
                    this.dgReport.Columns[3].ReadOnly = true;
                    this.dgReport.Columns[4].HeaderText = "Max Qty";
                    this.dgReport.Columns[4].ReadOnly = true;
                    this.dgReport.Columns[5].HeaderText = "Re-Order Qty";
                    this.dgReport.Columns[5].ReadOnly = true;
                    this.dgReport.Columns[6].HeaderText = "Sales Qty";
                    this.dgReport.Columns[6].ReadOnly = true;
                    this.dgReport.Columns[7].HeaderText = "On Hand";
                    this.dgReport.Columns[7].ReadOnly = true;
                    this.dgReport.Columns[8].HeaderText = "Committed Qty";
                    this.dgReport.Columns[8].ReadOnly = true;
                    this.dgReport.Columns[9].HeaderText = "On Order Qty";
                    this.dgReport.Columns[9].ReadOnly = true;
                    this.dgReport.Columns[10].HeaderText = "Required Qty";
                    this.dgReport.Columns[10].ReadOnly = true;
                    this.dgReport.Columns[11].HeaderText = "Last Cost";
                    this.dgReport.Columns[11].ReadOnly = true;
                    this.dgReport.Columns[12].HeaderText = "Order Amount";
                    this.dgReport.Columns[12].ReadOnly = true;
                    this.dgReport.Columns[13].HeaderText = "Supplier";
                    this.dgReport.Columns[13].ReadOnly = true;
                    this.dgReport.Columns[14].HeaderText = "Check To Include";



                    ws.Cells[4, 1] = "Part Number";
                    ws.Cells[4, 2] = "Supplier Item No";
                    ws.Cells[4, 3] = "Item Name";
                    ws.Cells[4, 4] = "Max Qty";
                    ws.Cells[4, 5] = "Re-Order Qty";
                    ws.Cells[4, 6] = "Sales Qty";
                    ws.Cells[4, 7] = "On Hand";
                    ws.Cells[4, 8] = "Committed Qty";
                    ws.Cells[4, 9] = "On Order Qty";
                    ws.Cells[4, 10] = "Required Qty";
                    ws.Cells[4, 11] = "Last Cost";
                    ws.Cells[4, 12] = "Order Amount";
                    ws.Cells[4, 13] = "Supplier";
                    ws.Cells[4, 14] = "To Be Included";

                    int i = 5;

                    foreach (DataGridViewRow item in dgReport.Rows)
                    {
                        if (item.Cells[1].Value != null)
                        {
                            ws.Cells[i, 1] = item.Cells[1].Value.ToString();

                        }
                        if (item.Cells[2].Value != null)
                        {
                            ws.Cells[i, 2] = item.Cells[2].Value.ToString();
                        }
                        if (item.Cells[3].Value != null)
                        {
                            ws.Cells[i, 3] = item.Cells[3].Value.ToString();
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 4] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[5].Value.ToString();
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 6] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[7].Value.ToString();
                        }
                        if (item.Cells[8].Value != null)
                        {
                            ws.Cells[i, 8] = item.Cells[8].Value.ToString();
                        }
                        if (item.Cells[9].Value != null)
                        {
                            ws.Cells[i, 9] = item.Cells[9].Value.ToString();
                        }
                        if (item.Cells[10].Value != null)
                        {
                            ws.Cells[i, 10] = item.Cells[10].Value.ToString();
                        }
                        if (item.Cells[11].Value != null && item.Cells[11].Value.ToString() != "")
                        {
                            double LastCost = Double.Parse(item.Cells[11].Value.ToString());
                            ws.Cells[i, 11] = LastCost.ToString("C");
                        }
                        if (item.Cells[12].Value != null && item.Cells[12].Value.ToString() != "")
                        {
                            double Amt = Double.Parse(item.Cells[12].Value.ToString());
                            ws.Cells[i, 12] = Amt.ToString("C");
                        }
                        if (item.Cells[13].Value != null)
                        {
                            ws.Cells[i, 13] = item.Cells[13].Value.ToString();
                        }
                        if (item.Cells[14].Value != null)
                        {
                            ws.Cells[i, 14] = item.Cells[14].Value.ToString();
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
                    ws.Cells[1, 1] = "Replenishment Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "N4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("D4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                  

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Replenishment Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void rdoMinMax_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMinMax.Checked)
            {
                sdatePicker.Enabled = false;
                edatePicker.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHistory.Checked)
            {
                sdatePicker.Enabled = true;
                edatePicker.Enabled = true;
            }
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreatePO();
        }
        private static string[] GeneratePONum()
        {
            SqlConnection con_ua = null;
            string[] lPONumber = new string[2];
           
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);
                string selectSql_ua = "Select PurchaseOrderSeries, PurchaseOrderPrefix from TransactionSeries";
                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                string lSeries = "";
                int lCnt = 0;
                int lNewSeries = 0;
                string lCurSeries = "";
                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lSeries = (reader["PurchaseOrderSeries"].ToString());
                            lCnt = lSeries.Length;
                            lSeries = lSeries.TrimStart('0');
                            lSeries = (lSeries == "" ? "0" : lSeries);
                            lNewSeries = Convert.ToInt16(lSeries) + 1;
                            lCurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                            lPONumber[0] = (reader["PurchaseOrderPrefix"].ToString()).Trim(' ') + lCurSeries;
                            lPONumber[1] = lCurSeries;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Transaction Series Numbers not setup properly.");
                    
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ua != null)
                    con_ua.Close();
            }
            return lPONumber;
        }
        private int CreatePurchaseTerm(string pSupplierID)
        {
            int lTermID = 0;
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                string selectSql = "SELECT * FROM  Profile  WHERE ID = " + pSupplierID;
                CommonClass.runSql(ref dt, selectSql);
                DataRow SupplierRow;
                if (dt.Rows.Count > 0)
                {
                    SupplierRow = dt.Rows[0];

                    string TermsOfPaymentID = SupplierRow["TermsOfPayment"].ToString();                   
                    string VolumeDiscount = SupplierRow["VolumeDiscount"].ToString();                   
                    string EarlyPaymentDiscount = SupplierRow["EarlyPaymentDiscountPercent"].ToString();                  
                    string LatePaymenDiscount = SupplierRow["LatePaymentChargePercent"].ToString();
                    string BalanceDueDays = "0";
                    string DiscountDays = "0";
                    DateTime lTranDate = DateTime.Now.ToUniversalTime();
                    DateTime lDueDate = lTranDate;
                    DateTime lDiscountDate = lTranDate;
                    switch (TermsOfPaymentID)
                    {

                        case "SD": //Specific Days
                            BalanceDueDays = SupplierRow["BalanceDueDays"].ToString();
                            DiscountDays = SupplierRow["DiscountDays"].ToString();
                            lDueDate = lTranDate.AddDays(Convert.ToInt16(BalanceDueDays));
                            lDiscountDate = lTranDate.AddDays(Convert.ToInt16(DiscountDays));
                            break;

                        default: //CASH                         
                            BalanceDueDays = "0";                          
                            DiscountDays = "0";                          
                            break;
                    }

                    //INSERT NEW TERM
                   
                    string ActualDueDate = lDueDate.ToString("yyyy-MM-dd");
                    string ActualDiscountDate = lDiscountDate.ToString("yyyy-MM-dd");
                    string termsql = @"INSERT INTO Terms (TermsOfPaymentID, DiscountDays, BalanceDueDays, VolumeDiscount, ActualDueDate, ActualDiscountDate,EarlyPaymentDiscountPercent,LatePaymentChargePercent) 
                                   VALUES (@TermsOfPaymentID, @DiscountDays, @BalanceDueDays, @VolumeDiscount, @ActualDueDate,@ActualDiscountDate,@EarlyPaymentDiscountPercent,@LatePaymentChargePercent); 
                                   SELECT SCOPE_IDENTITY()";
                    Dictionary<string, object> param = new Dictionary<string, object>();

                    param.Add("@TermsOfPaymentID", TermsOfPaymentID);
                    param.Add("@DiscountDays", DiscountDays);
                    param.Add("@BalanceDueDays", BalanceDueDays);
                    param.Add("@VolumeDiscount", VolumeDiscount);
                    param.Add("@ActualDueDate", ActualDueDate);
                    param.Add("@ActualDiscountDate", ActualDiscountDate);
                    param.Add("@EarlyPaymentDiscountPercent", EarlyPaymentDiscount);
                    param.Add("@LatePaymentChargePercent", LatePaymenDiscount);
                    lTermID = CommonClass.runSql(termsql, CommonClass.RunSqlInsertMode.SCALAR, param);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lTermID;
        }

      
        private void CreatePO()
        {
            SqlConnection con = null;
            try
            {
                DataView view = new DataView(TbGrid);
                System.Data.DataTable TbSup = new System.Data.DataTable();
                TbSup = view.ToTable(true, "SupplierID");
                for (int i = 0; i < TbSup.Rows.Count; i++)
                {
                    string lfilter = "Include = 1 and SupplierID = " + TbSup.Rows[i][0].ToString();
                    DataRow[] lRw = TbGrid.Select(lfilter);
                    if (lRw.GetUpperBound(0) >= 0)
                    {
                        string[] lPODet = GeneratePONum();
                        string lPONum = lPODet[0];
                        string lSupplierID = TbSup.Rows[i][0].ToString();
                        if (lPONum != "")
                        {

                            int lNewTermID = 0;
                            DateTime lTranDate = DateTime.Now.ToUniversalTime();
                            lNewTermID = CreatePurchaseTerm(lSupplierID);
                            Dictionary<string, object> param = new Dictionary<string, object>();
                            string sqli = @"INSERT INTO Purchases (
                                        SupplierID, 
                                        UserID, 
                                        ShippingContactID, 
                                        BillingContactID, 
                                        TermsReferenceID, 
                                        PurchaseType, 
                                        LayoutType, 
                                        PurchaseNumber,
                                        TransactionDate, 
                                        SubTotal, 
                                        FreightSubTotal,
                                        FreightTax, 
                                        GrandTotal,    
                                        PurchaseReference, 
                                        ShippingMethodID, 
                                        PromiseDate, 
                                        Memo, 
                                        Comments, 
                                        POStatus, 
                                        TaxTotal, 
                                        IsTaxInclusive, 
                                        ClosedDate,
                                        FreightTaxCode,
                                        FreightTaxRate) 
                                    VALUES (
                                        @SupplierID, 
                                        @UserID, 
                                        @ShippingContactID, 
                                        @BillingContactID, 
                                        @TermsReferenceID, 
                                        @PurchaseType, 
                                        @LayoutType, 
                                        @PurchaseNumber, 
                                        @TransactionDate, 
                                        @SubTotal, 
                                        @FreightSubTotal, 
                                        @FreightTax,
                                        @GrandTotal, 
                                        @PurchaseReference, 
                                        @ShippingMethodID, 
                                        @PromiseDate, 
                                        @Memo, 
                                        @Comments, 
                                        @POStatus, 
                                        @TaxTotal, 
                                        @IsTaxInclusive, 
                                        @ClosedDate,
                                        @FreightTaxCode,
                                        @FreightTaxRate)";

                            param.Add("@SupplierID", lSupplierID);
                            param.Add("@PurchaseNumber", lPONum);
                            param.Add("@PromiseDate", DateTime.Now.ToUniversalTime());
                            param.Add("@TransactionDate", lTranDate);
                            param.Add("@UserID", CommonClass.UserID);
                            param.Add("@ShippingContactID", 0);
                            param.Add("@BillingContactID", 0);
                            param.Add("@TermsReferenceID", lNewTermID);
                            param.Add("@LayoutType", "item");
                            param.Add("@SubTotal", 0);
                            param.Add("@FreightSubTotal", 0);
                            param.Add("@FreightTax", 0);
                            param.Add("@GrandTotal", 0);

                            param.Add("@PurchaseReference", "");
                            param.Add("@ShippingMethodID", 0);
                            param.Add("@Memo", "Purchase " + lPONum);
                            param.Add("@Comments", "");
                            param.Add("@TaxTotal", 0);
                            param.Add("@FreightTaxCode", "");
                            param.Add("@FreightTaxRate", 0);
                            param.Add("@isTaxInclusive", "N");
                            param.Add("@POStatus", "New");
                            param.Add("@ClosedDate", System.DBNull.Value);
                            param.Add("@PurchaseType", "ORDER");

                            //con.Open();
                            int lNewPurchaseID = CommonClass.runSql(sqli, CommonClass.RunSqlInsertMode.SCALAR, param);


                            if (lNewPurchaseID != 0)
                            {
                                //PurchaseLines
                                string Descript;
                                string Amount = "0";
                                string Job = "0";
                                string Tax = "0";

                                string lTaxPaidAccountID = "0";
                                float lTaxInc = 0;
                                float lTaxEx = 0;
                                float lTaxRate = 0;
                                float lPriceEx = 0;
                                string lOrderedQty = "0";
                                string lReceivedQty = "0";
                                string lToDateQty = "0";
                                string lDiscountRate = "0";
                                float lSubTotal = 0;
                                float lGrandTotal = 0;
                                float lTaxTotal = 0;
                                int entity = 0;
                                foreach (DataRow rw in lRw)
                                {
                                    Descript = String.Format("{0}", rw["ItemName"].ToString());
                                    Amount = rw["OrderAmount"].ToString();
                                    float fAmount = float.Parse(Amount, NumberStyles.Currency);
                                    lTaxRate = float.Parse(rw["TaxPercentageRate"].ToString());
                                    lTaxInc = fAmount * (1 + (lTaxRate / 100));
                                    lTaxEx = fAmount;

                                    lPriceEx = float.Parse(rw["LastCostEx"].ToString(), NumberStyles.Currency);

                                    entity = Convert.ToInt32(rw["ID"].ToString());
                                    Tax = rw["PurchaseTaxCode"].ToString();
                                    lTaxPaidAccountID = rw["TaxPaidAccountID"].ToString();
                                    lOrderedQty = rw["RequiredQty"].ToString();
                                    string purchaseLinesql = @"INSERT INTO PurchaseLines(
                                            PurchaseID, JobID, EntityID, TransactionDate, OrderQty, ReceiveQty, UnitPrice, ActualUnitPrice, 
                                            DiscountPercent, SubTotal, TotalAmount, Description, TaxCode, TaxAmount, TaxPaidAccountID, TaxRate)
                                            VALUES(
                                            " + lNewPurchaseID + ", " + Job + "," + entity.ToString() + ",'" + lTranDate.ToString("yyyy-MM-dd HH:mm:ss") + "', " + lOrderedQty + ", " + lReceivedQty + ", " + lPriceEx.ToString() + ", " + lPriceEx.ToString() + " , " +
                                        lDiscountRate + ", " + lTaxEx.ToString() + ", " + lTaxInc.ToString() + ", '" + Descript + "','" + Tax + "', " + (lTaxInc - lTaxEx).ToString() + ", " + lTaxPaidAccountID + "," + lTaxRate.ToString() + ")";

                                    int lCount = CommonClass.runSql(purchaseLinesql, CommonClass.RunSqlInsertMode.SCALAR);
                                    lSubTotal += lTaxEx;
                                    lGrandTotal += lTaxInc;
                                    lTaxTotal += (lTaxInc - lTaxEx);
                                }
                                //Update totals of Purchase Table
                                string sqlupdate = "UPDATE Purchases set SubTotal = " + lSubTotal.ToString() + ", GrandTotal = " + lGrandTotal.ToString() + ", TaxTotal = " + lTaxTotal.ToString() + " where PurchaseID = " + lNewPurchaseID;
                                CommonClass.runSql(sqlupdate);
                            }
                            //UPDATE Transaction Series
                            sqli = " UPDATE TransactionSeries SET PurchaseOrderSeries = " + lPODet[1];
                            CommonClass.runSql(sqli);
                            CommonClass.SaveSystemLogs(CommonClass.UserID, "Purchases ORDER", "Created PO Number " + lPONum + " with Purchase ID " + lNewPurchaseID.ToString(), lNewPurchaseID.ToString());


                        }

                    }
                }
                MessageBox.Show("Purchase Order/s created. Please go to Purchase Register and approve the PO.");
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
    }
}
