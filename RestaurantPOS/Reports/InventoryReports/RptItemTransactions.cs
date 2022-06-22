using AbleRetailPOS.Inventory;
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

namespace AbleRetailPOS.Reports.InventoryReports
{
    public partial class RptItemTransactions : Form
    {
        string mSupplierID;
        string mUserID;
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        string categories = "";
        string ItemID = "";
        private bool CanView = false;
        public RptItemTransactions()
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
        }

        private void LoadReportItemTransactions()
        {
                string sql = "";
            string sqlfilter = "";
             
                if (toNum.Value != 0)
                {
                sqlfilter += " AND tx.GrandTotal BETWEEN " + fromNum.Value + " AND " + toNum.Value;
                }
                if (txtSpecificItem.Text == "")
                {
                    //if (txtProfile.Text != "")
                    //{
                    //    sql += " AND i.SupplierID = " + mSupplierID;
                    //}
                    if (txtMemo.Text != "")
                    {
                    sqlfilter += " AND tx.Memo = '" + String.Format("{0}", txtMemo.Text) + "'";
                    }
                    if (txtUser.Text != "")
                    {
                    sqlfilter += " AND tx.UserID = " + mUserID;
                    }
                    if (list1 != "")
                    {
                    sqlfilter += " AND i.CList1 = @CList1 ";
                    }
                    if (list2 != "")
                    {
                    sqlfilter += " AND i.CList2 = @CList2 ";
                    }
                    if (list3 != "")
                    {
                    sqlfilter += " AND i.CList3 = @CList3 ";
                    }
                    if (categories.Length > 0)
                    {
                    sqlfilter += " AND CategoryID in (" + categories + ") ";
                    }
                    if (supplierID != "")
                    {
                    sqlfilter += " AND i.SupplierID = @supplier ";
                    }
                }
                else
                {
                sqlfilter += " AND i.ID = @ItemID ";   
                }

            sqlfilter += " AND tx.TransactionDate BETWEEN @sdate AND @edate ";
               
               
            string sqlpurchase = @"SELECT j.TransactionNumber,
	                                tx.Memo,
	                                j.TransactionDate,
	                                j.DebitAmount,
	                                j.CreditAmount,
	                                j.Type,
	                                i.PartNumber,
                                    i.ItemName,
                                    i.ItemNumber   
                                  FROM Purchases tx
                                  INNER JOIN PurchaseLines pl ON pl.PurchaseID = tx.PurchaseID
                                  INNER JOIN Items i ON i.ID = pl.EntityID
                                  INNER JOIN Journal j ON j.TransactionNumber = tx.PurchaseNumber
                                  WHERE tx.LayoutType = 'Item' " + sqlfilter;
            string sqlsales = @"SELECT j.TransactionNumber,
	                                tx.Memo,
	                                j.TransactionDate,
	                                j.DebitAmount,
	                                j.CreditAmount,
	                                j.Type,
	                                i.PartNumber,
                                    i.ItemName,
                                    i.ItemNumber  
                                  FROM Sales tx
                                  INNER JOIN SalesLines sl ON sl.SalesID = tx.SalesID
                                  INNER JOIN Items i ON i.ID = sl.EntityID
                                  INNER JOIN Journal j ON j.TransactionNumber = tx.SalesNumber
                                  WHERE tx.LayoutType = 'Item' " + sqlfilter;

            switch (cmbSrcJournal.Text)
            {
                case "General":
                    break;
                case "Sales":
                    sql = sqlsales;
                    break;
                case "Purchases":
                    sql = sqlpurchase;
                    break;
                case "Disbursements":
                    break;
                case "Receipts":
                    break;
                case "Inventory":
                    break;
                default://All
                    sql = sqlsales + " UNION " + sqlpurchase;
                    break;
            }
            if (cmbSort.Text == "Item Number")
            {
                sql += " ORDER BY ItemNumber DESC ";
            }
            else if (cmbSort.Text == "Part Number")
            {
                sql += " ORDER BY PartNumber DESC";
            }
            DateTime sdate = sdateTimePicker.Value;
                DateTime edate = edateTimePicker.Value;

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();

            DataTable TbRep = new DataTable();
                
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@supplier", supplierID);
                param.Add("@CList1", list1);
                param.Add("@CList2", list2);
                param.Add("@CList3", list3);
                param.Add("@sdate", sdate);
                param.Add("@edate", edate);
            param.Add("@ItemID", ItemID);
            CommonClass.runSql(ref TbRep, sql, param);
                Reports.ReportParams itemtxparams = new Reports.ReportParams();
                itemtxparams.PrtOpt = 1;
                itemtxparams.Rec.Add(TbRep);
                itemtxparams.ReportName = "ItemTransactions.rpt";
                itemtxparams.RptTitle = "Item Transactions";

                itemtxparams.Params = "compname|startdate|enddate";
                itemtxparams.PVals = CommonClass.CompName.Trim() + "|" + sdateTimePicker.Value.ToString("yyyy-MM-dd") + "|" + edateTimePicker.Value.ToString("yyyy-MM-dd");

                CommonClass.ShowReport(itemtxparams);
         
        }

        void ShowProfileLookup()
        {
            ProfileLookup DlgProfile = new ProfileLookup();
            if (DlgProfile.ShowDialog() == DialogResult.OK)
            {
                string[] ProfileList = DlgProfile.GetProfile;

                txtProfile.Text = ProfileList[2]; //Profile Name
                mSupplierID = ProfileList[0];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            ShowProfileLookup();
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            filterCategories();
            LoadReportItemTransactions();
        }

        private void RptItemTransactions_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadCategory();
            cmbSort.SelectedIndex = 1;
            cmbSrcJournal.SelectedIndex = 6; //Select All by default
        }

        private void toNum_ValueChanged(object sender, EventArgs e)
        {

        }
        private void LoadCategory()
        {
            DataTable dt = new DataTable();
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

        private void pbItem_Click(object sender, EventArgs e)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, "", "", "PartNumber");
            
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                ItemID = ItemRows.Cells[0].Value.ToString();
               txtSpecificItem.Text = ItemRows.Cells[3].Value.ToString();
                categories = "";
                list1 = "";
                list2 = "";
                list3 = "";
                txtList1.Text = "";
                txtList2.Text = "";
                txtList3.Text = "";
                txtSupplier.Text = "";
                txtMemo.Text = "";
            }
        }

        private void txtSpecificItem_TextChanged(object sender, EventArgs e)
        {
            if(txtSpecificItem.Text == "")
            {
                ItemID = "";
            }
        }
    }
}
