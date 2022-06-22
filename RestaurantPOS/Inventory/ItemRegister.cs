using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Globalization;
using AbleRetailPOS.Sales;
using AbleRetailPOS.Purchase;
using Microsoft.Office.Interop.Excel;

namespace AbleRetailPOS.Inventory
{
    public partial class ItemRegister : Form
    {
        private string thisFormCode = "";
        private bool CanView = false;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string SelItemID = "";
        private string SelItemName = "";
        private bool IsLoading = false;
        private System.Data.DataTable dt = null;
        private string itemName = "";
        public ItemRegister()
        {
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                FormRights.TryGetValue("Add", out outx);
                CanAdd = outx;
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                CanEdit = outx;
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                CanDelete = outx;
            }
            string outy = "";
            CommonClass.AppFormCode.TryGetValue(this.Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
        }

        private void ItemRegister_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            foreach (DataGridViewColumn column in dgridItem.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            IsLoading = true;
            this.sdateTimePicker.Value = DateTime.Now;
            this.edateTimePicker.Value = DateTime.Now;
            IsLoading = false;
        }
        private void ShowTransactions()
        {
            if (IsLoading)
                return;

            if (cmb_searby.SelectedIndex == 1)
            {
                if (this.txtItem.Text == "")
                {
                    MessageBox.Show("Please select and item.");
                    return;
                }
            }

            SqlConnection connection = null;
            try
            {

                DateTime lsdate = Convert.ToDateTime(sdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime ledate = Convert.ToDateTime(edateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                string itemcon = "";
                itemcon = (cmb_searby.SelectedIndex == 1 ? " and i.ID = " + SelItemID : "");
                string itemcon1 = "";
                itemcon1 = (cmb_searby.SelectedIndex == 1 ? " and ItemID = " + SelItemID : "");
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                //CONVERT(datetime, SWITCHOFFSET(CONVERT(datetimeoffset, t.TransactionDate), DATENAME(TzOffset, SYSDATETIMEOFFSET()))) AS TranDate
                string sql = "";
                sql += @"SELECT i.*, CAST(0 as float) as [On Hand], ISNULL(b.BegQty,0) as BegQty from ( SELECT  t.TransactionDate as [Transaction Date], t.TranType,p.PurchaseNumber as [Transaction Number], i.PartNumber as [Part Number], i.ItemName as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx  as [Total Cost],t.SourceTranID, 'P' as formtype, t.ItemID
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join Purchases as p on t.SourceTranID = p.PurchaseID where TranType in ('RI','PB') and ( t.TransactionDate >= '" + lsdate.ToString("yyyy-MM-dd HH:mm:ss") + "' and t.TransactionDate <= '" + ledate.ToString("yyyy-MM-dd HH:mm:ss") + "')  " + itemcon;

                sql += @"UNION SELECT t.TransactionDate as [Transaction Date], t.TranType,p.SalesNumber as  [Transaction Number],  i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'S' as formtype, t.ItemID
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join Sales as p on t.SourceTranID = p.SalesID where TranType IN ('SI', 'SII') and ( t.TransactionDate >= '" + lsdate.ToString("yyyy-MM-dd HH:mm:ss") + "' and t.TransactionDate <= '" + ledate.ToString("yyyy-MM-dd HH:mm:ss") + "')  " + itemcon;

                sql += @"UNION SELECT t.TransactionDate as [Transaction Date], t.TranType,p.ItemAdjNumber as  [Transaction Number],  i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'A' as formtype, t.ItemID
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join ItemsAdjustment as p on t.SourceTranID = p.ItemAdjID where TranType in ('IA','IB') and ( t.TransactionDate >= '" + lsdate.ToString("yyyy-MM-dd HH:mm:ss") + "' and t.TransactionDate <= '" + ledate.ToString("yyyy-MM-dd HH:mm:ss") + "')  " + itemcon + " ) as i ";

                sql += " LEFT JOIN ( SELECT ItemID, Sum(QtyAdjustment) as BegQty from ItemTransaction where TransactionDate < '" + lsdate.ToString("yyyy-MM-dd HH:mm:ss") + "' " + itemcon1 + " group by ItemID ) as b on i.ItemID = b.ItemID order by i.[Transaction Date]";


                SqlCommand cmd_ = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter();
                dt = new System.Data.DataTable();
                da.SelectCommand = cmd_;
                da.Fill(dt);
                float RBal = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        RBal = float.Parse(dt.Rows[i]["BegQty"].ToString());

                    }

                    DateTime lTranUTC = (DateTime)dt.Rows[i]["Transaction Date"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    dt.Rows[i]["Transaction Date"] = lTranLocal;
                    RBal += float.Parse(dt.Rows[i]["Quantity Adjustment"].ToString());
                    dt.Rows[i]["On Hand"] = RBal;
                }

                this.dgridItem.DataSource = dt;
                this.dgridItem.Columns["Quantity Adjustment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgridItem.Columns[6].DefaultCellStyle.Format = "C2";
                this.dgridItem.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgridItem.Columns[7].Visible = false;
                this.dgridItem.Columns[8].Visible = false;
                this.dgridItem.Columns[9].Visible = false;
                this.dgridItem.Columns[10].Visible = true;
                this.dgridItem.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgridItem.Columns[11].Visible = false;
                if (itemcon == "")
                {
                    this.dgridItem.Columns[10].Visible = false;
                }
                if (dt.Rows.Count > 0)
                    btnPrint.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void ShowTransactionsOLD()
        {
            if (IsLoading)
                return;

            if(cmb_searby.SelectedIndex == 1)
            {
                if(this.txtItem.Text == "")
                {
                    MessageBox.Show("Please select and item.");
                    return;
                }
            }

            SqlConnection connection = null;
            try
            {
                DateTime lsdate = this.sdateTimePicker.Value.ToUniversalTime();
                DateTime ledate = this.edateTimePicker.Value.ToUniversalTime();
                string itemcon = "";
                itemcon = (cmb_searby.SelectedIndex == 1 ? " and i.ID = " + SelItemID : "");
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                //CONVERT(datetime, SWITCHOFFSET(CONVERT(datetimeoffset, t.TransactionDate), DATENAME(TzOffset, SYSDATETIMEOFFSET()))) AS TranDate
                string sql = @"SELECT * from ( SELECT  t.TransactionDate as [Transaction Date], t.TranType,p.PurchaseNumber as [Transaction Number], i.PartNumber as [Part Number], i.ItemName as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx  as [Total Cost],t.SourceTranID, 'P' as formtype
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join Purchases as p on t.SourceTranID = p.PurchaseID where TranType in ('RI','PB') and ( t.TransactionDate >= '" + lsdate.ToString("yyyy-MM-dd") + "' and t.TransactionDate <= '" + ledate.ToString("yyyy-MM-dd") + "')  " + itemcon;

                sql += @"UNION SELECT t.TransactionDate as [Transaction Date], t.TranType,p.SalesNumber as  [Transaction Number],  i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'P' as formtype
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join Sales as p on t.SourceTranID = p.SalesID where TranType = 'SI' and ( t.TransactionDate >= '" + lsdate.ToString("yyyy-MM-dd") + "' and t.TransactionDate <= '" + ledate.ToString("yyyy-MM-dd") + "')  " + itemcon ;

                sql += @"UNION SELECT t.TransactionDate as [Transaction Date], t.TranType,p.ItemAdjNumber as  [Transaction Number],  i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'A' as formtype
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join ItemsAdjustment as p on t.SourceTranID = p.ItemAdjID where TranType in ('IA','IB') and ( t.TransactionDate >= '" + lsdate.ToString("yyyy-MM-dd") + "' and t.TransactionDate <= '" + ledate.ToString("yyyy-MM-dd") + "')  " + itemcon + " ) as i order by i.[Transaction Date] desc";


                SqlCommand cmd_ = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter();
                dt = new System.Data.DataTable();
                da.SelectCommand = cmd_;
                da.Fill(dt);
                foreach(DataRow rw in dt.Rows)
                {
                    DateTime lTranUTC = (DateTime)rw["Transaction Date"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    rw["Transaction Date"] = lTranLocal;
                }
                this.dgridItem.DataSource = dt;
                this.dgridItem.Columns[6].DefaultCellStyle.Format = "C2";
                this.dgridItem.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgridItem.Columns[7].Visible = false;
                this.dgridItem.Columns[8].Visible = false;
                if (dt.Rows.Count > 0)
                    btnPrint.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {      
            ShowTransactions();
        }

        private void pbItem_Click(object sender, EventArgs e)
        {
            ItemLookup ItemDlg = new Inventory.ItemLookup(ItemLookupSource.SELF);
            if (ItemDlg.ShowDialog() == DialogResult.OK)
            {
                DataGridViewRow dr = ItemDlg.GetSelectedItem;
                if(dr != null)
                {
                    SelItemID = dr.Cells["ItemID"].Value.ToString(); 
                    SelItemName = dr.Cells["PartNo"].Value.ToString() + " - " + dr.Cells["ItemNo"].Value.ToString();
                    itemName = dr.Cells["ItemName"].Value.ToString();
                }
                this.txtItem.Text = SelItemName;
                this.lblItemID.Text = SelItemID;             
            }
        }

        private void cmb_searby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_searby.SelectedIndex == 1)
            {
                this.txtItem.Visible = true;
                this.pbItem.Visible = true;
                pbItem_Click(sender, e);
            }
            else
            {
                this.txtItem.Visible = false;
                this.pbItem.Visible = false;
            }
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
           
            ShowTransactions();
        }

        private void edateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ShowTransactions();
        }

        private void sdateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ShowTransactions();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadRegisterReport();
        }

        private void LoadRegisterReport()
        {
            Reports.ReportParams registerlistparams = new Reports.ReportParams();
            registerlistparams.PrtOpt = 1;
            if (dt.Rows.Count > 0)
            {
                registerlistparams.Rec.Add(dt);
            }

            registerlistparams.ReportName = "ItemRegister.rpt";
            registerlistparams.RptTitle = "Items Register";

            registerlistparams.Params = "compname";
            registerlistparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(registerlistparams);
        }
        private void ShowTran(string pTranType, string pTranNo)
        {

            switch (pTranType.Trim())
            {
               
                case "RI":
                case "PB":
                    LoadPurchase(pTranNo);
                    break;
                
                case "SI":
                    LoadSales(pTranNo);
                    break;
                case "IB":
                    LoadBuildItems(pTranNo);
                    break;
                case "IA":
                    LoadStockAdjustments(pTranNo);
                    break;

                case "BD":
                    break;
            }
        }
        private void LoadPurchase(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Purchases where PurchaseNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                dt = new System.Data.DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lPID = dt.Rows[0]["PurchaseID"].ToString();
                    string lShippingID = dt.Rows[0]["ShippingContactID"].ToString();
                    string lpurchaseType = dt.Rows[0]["PurchaseType"].ToString();
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, lPID, lShippingID, lpurchaseType);
                    this.Cursor = Cursors.WaitCursor;
                    CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                    CommonClass.EnterPurchasefrm.Show();
                    CommonClass.EnterPurchasefrm.Focus();
                    if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                    {
                        CommonClass.EnterPurchasefrm.Close();
                    }
                    this.Cursor = Cursors.Default;
                }


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }
        private void LoadSales(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Sales where SalesNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                dt = new System.Data.DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lSalesID = dt.Rows[0]["SalesID"].ToString();

                    CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, lSalesID);
                    this.Cursor = Cursors.WaitCursor;
                    CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                    CommonClass.EnterSalesfrm.Show();
                    CommonClass.EnterSalesfrm.Focus();
                    if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                    {
                        CommonClass.EnterSalesfrm.Close();
                    }
                    this.Cursor = Cursors.Default;
                }


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }
        private void LoadStockAdjustments(string pTranNo)
        {
            try
            {
                StockAdjustments StockAdjustmentsFrm = new StockAdjustments(CommonClass.InvocationSource.SELF, null, null, pTranNo);
                this.Cursor = Cursors.WaitCursor;
                StockAdjustmentsFrm.MdiParent = this.MdiParent;
                StockAdjustmentsFrm.Show();
                StockAdjustmentsFrm.Focus();
                if (StockAdjustmentsFrm.DialogResult == DialogResult.Cancel || StockAdjustmentsFrm.DialogResult == DialogResult.OK)
                {
                    StockAdjustmentsFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadBuildItems(string pTranNo)
        {
            try
            {
                BuildItems BuildItemsFrm = new BuildItems(CommonClass.InvocationSource.SELF, null,pTranNo);
                this.Cursor = Cursors.WaitCursor;
                BuildItemsFrm.MdiParent = this.MdiParent;
                BuildItemsFrm.Show();
                BuildItemsFrm.Focus();
                if (BuildItemsFrm.DialogResult == DialogResult.Cancel || BuildItemsFrm.DialogResult == DialogResult.OK)
                {
                    BuildItemsFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgridItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string lTranType = this.dgridItem.Rows[e.RowIndex].Cells[1].Value.ToString();
            string lTranNo = this.dgridItem.Rows[e.RowIndex].Cells[2].Value.ToString();
            ShowTran(lTranType, lTranNo);
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
                    ws.Cells[4, 3] = "Transaction Number";
                    ws.Cells[4, 4] = "Part Number";
                    ws.Cells[4, 5] = "Item Name";
                    ws.Cells[4, 6] = "Quantity Adjustment";
                    ws.Cells[4, 7] = "Total Cost";
                    if (cmb_searby.Text == "Item")
                    {
                        ws.Cells[4, 8] = "On Hand";
                    }

                    int i = 5;
                    foreach (DataGridViewRow item in dgridItem.Rows)
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
                        if (cmb_searby.Text == "Item")
                        {
                            if (item.Cells[7].Value != null)
                            {
                                ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                            }
                        }

                        i++;
                    }
                    if (cmb_searby.Text == "Item")
                    {
                        Range cellRange = ws.get_Range("A1", "H3");
                        cellRange.Merge(false);
                        cellRange.Interior.Color = System.Drawing.Color.White;
                        cellRange.Font.Color = System.Drawing.Color.Gray;
                        cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        cellRange.Font.Size = 26;
                        ws.Cells[1, 1] = itemName + " Item Register";

                        //Style Table
                        cellRange = ws.get_Range("A4", "H4");
                        cellRange.Font.Bold = true;
                        cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    }
                    else
                    {
                        Range cellRange = ws.get_Range("A1", "G3");
                        cellRange.Merge(false);
                        cellRange.Interior.Color = System.Drawing.Color.White;
                        cellRange.Font.Color = System.Drawing.Color.Gray;
                        cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        cellRange.Font.Size = 26;
                        ws.Cells[1, 1] = "Item Register";

                        //Style Table
                        cellRange = ws.get_Range("A4", "G4");
                        cellRange.Font.Bold = true;
                        cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    }


                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    if (cmb_searby.Text == "Item")
                    {
                        MessageBox.Show(itemName + " Item Register has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Item Register has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
