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
using AbleRetailPOS.References;


namespace AbleRetailPOS.Inventory
{
    public partial class ItemList : Form
    {
        private static bool IsLoading = false;
        private static string WhereCon = "";
        private string txtSearch = "";
        string categories = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;

        public ItemList()
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
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Item ItemDlg = new Item();
            ItemDlg.ShowDialog();
            SearchItems(WhereCon, txtSearch);

        }
        public void ApplyItemFieldAccess(String FieldID)
        {
            CommonClass.GetAccess(FieldID);
            foreach (DataGridViewRow dgvr in dgridItems.Rows)
            {
                if (dgvr.Cells[5].Value.ToString() != "")
                {
                    LastCostFieldRights();
                }
                //Column 6 is for the Selling price. Should show always
                //if (dgvr.Cells[6].Value.ToString() != "")
                //{
                //    BaseSellingPriceFieldRights();
                //    AverageCostPriceFieldRights();
                //}
            }
        }
        private void LastCostFieldRights()
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue("txtLastCost", out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        this.dgridItems.Columns[5].Visible = valstr;
                    }
                    else
                    {
                        this.dgridItems.Columns[5].Visible = valstr;
                    }
                }
            }
        }
        private void BaseSellingPriceFieldRights()
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue("txtStandardCost", out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        this.dgridItems.Columns[6].Visible = valstr;
                    }
                    else
                    {
                        this.dgridItems.Columns[6].Visible = valstr;
                    }
                }
            }
        }
        private void AverageCostPriceFieldRights()
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue("txtAverageCost", out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        this.dgridItems.Columns[6].Visible = valstr;
                    }
                    else
                    {
                        this.dgridItems.Columns[6].Visible = valstr;
                    }
                }
            }
        }

        private void ItemList_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }   
            foreach (DataGridViewColumn column in dgridItems.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            WhereCon = "PartNumber";
            SearchItems(WhereCon, txtSearch);
            FormatGrid();
            LoadCategory();
            btnAddNew.Enabled = CanAdd;
            btnDelete.Enabled = CanDelete;
        }

        private void txtFind()
        {
            txtSearch = "";
            WhereCon = "";
            if (txtPartNum.Text != "")
            {
                txtSearch = txtPartNum.Text;
                WhereCon = "Part Number";
            }
            if (txtItemNum.Text != "")
            {
                txtSearch = txtItemNum.Text;
                WhereCon = "Item Number";
            }
            if (categories != "")
            {
                txtSearch = categories;
                WhereCon = "Category";
            }
            if (txtDesc.Text != "")
            {
                txtSearch = txtDesc.Text;
                WhereCon = "Item Description";
            }
            if (txtSupplier.Text != "")
            {
                txtSearch = txtSupplier.Text;
                WhereCon = "Supplier";
            }
            if (txtBrand.Text != "")
            {
                txtSearch = txtBrand.Text;
                WhereCon = "Brand";
            }
            if (txtSuppPartNum.Text != "")
            {
                txtSearch = txtSuppPartNum.Text;
                WhereCon = "Supplier Part Number";
            }
            if (txtItemName.Text != "")
            {
                txtSearch = txtItemName.Text;
                WhereCon = "Item Name";
            }
           
            if (txtCustomList1.Text != "")
            {
                txtSearch = txtCustomList1.Text;
                WhereCon = "Custom List 1";
            }
            if (txtCustomList2.Text != "")
            {
                txtSearch = txtCustomList2.Text;
                WhereCon = "Custom List 2";
            }
            if (txtCustomList3.Text != "")
            {
                txtSearch = txtCustomList3.Text;
                WhereCon = "Custom List 3";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtFind();
            SearchItems(WhereCon, txtSearch);
        }

        private void SearchItems(string pWhereCol, string pSearchText)
        {
            filterCategories();
            IsLoading = true;
                string strCon = "";

            if (txtPartNum.Text != "")
            {
                strCon += " AND ( i.PartNumber LIKE @txtPartNum OR bc.BarcodeData LIKE @txtPartNum )";
            }
            else
            {
                strCon = " where i.PartNumber LIKE '%%' ";
            }

            if (txtItemNum.Text != "")
            {
                strCon += " OR i.ItemNumber LIKE @txtItemNum";
            }
            if (categories != "")
            {
                strCon += " AND g.CategoryID in (" + categories + ")";
            }
           
            if (txtDesc.Text != "")
            {
                strCon += " OR i.ItemDescriptionSimple LIKE @ItemDescriptionSimple ";
            }
            if (txtSupplier.Text != "")
            {
                strCon += " OR p.Name LIKE @txtSupplier ";
            }
            if (txtBrand.Text != "")
            {
                strCon += " OR i.PartNumber LIKE @txtBrand";
            }
            if (txtSuppPartNum.Text != "")
            {
                strCon += " OR i.SupplierPartNumber LIKE @txtSuppPartNum";
            }
            if (txtItemName.Text != "")
            {
                strCon += " AND i.ItemName LIKE @txtItemName ";
            }
            
            if (txtCustomList1.Text != "")
            {
                strCon += " OR c1.List1Name LIKE @txtCustomList1 ";
            }
            if (txtCustomList2.Text != "")
            {
                strCon += " OR c2.List2Name LIKE @txtCustomList2 ";
            }
            if (txtCustomList3.Text != "")
            {
                strCon += " OR c3.List3Name LIKE @txtCustomList3 ";
            }
            strCon += " AND i.IsInactive = 0";


            string selectSql = @"Select DISTINCT i.ID, i.PartNumber, i.ItemNumber, i.ItemName, q.OnHandQty, c.LastCostEx, s.Level0 from 
                    ((((( Items as i inner join ItemsSellingPrice as s on i.ID = s.ItemID )
                    inner join ItemsCostPrice as c on i.ID = c.ItemID ) 
                    inner join ItemsQty as q on i.ID = q.ItemID )
                    left join Profile as p on i.SupplierID = p.ID )
                    left join TaxCodes as t on i.SalesTaxCode = t.taxcode )
                    left join TaxCodes as tp on i.PurchaseTaxCode = tp.taxcode 
                    LEFT JOIN Barcodes bc ON bc.ItemID = i.ID 
                    LEFT JOIN Category g ON g.CategoryID = i.CategoryID 
                    LEFT JOIN CustomList1 c1 ON c1.ID = i.CList1 
                    LEFT JOIN CustomList2 c2 ON c2.ID = i.CList2 
                    LEFT JOIN CustomList3 c3 ON c3.ID = i.CList3 " + strCon + " ORDER BY ID ASC";


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@txtPartNum", "%" + txtPartNum.Text + "%");
            param.Add("@txtItemNum", "%" + txtItemNum.Text + "%");
            param.Add("@txtItemName", "%"+ txtItemName.Text +"%");
            param.Add("@ItemDescriptionSimple", "%" + txtDesc.Text + "%");
            param.Add("@txtSupplier", "%" + txtSupplier.Text + "%");
            param.Add("@txtBrand", "%" + txtBrand.Text + "%");
            param.Add("@txtSuppPartNum", "%" + txtSuppPartNum.Text + "%");
            param.Add("@txtCustomList1", "%" + txtCustomList1.Text + "%");
            param.Add("@txtCustomList2", "%" + txtCustomList2.Text + "%");
            param.Add("@txtCustomList3", "%" + txtCustomList3.Text + "%");
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, selectSql, param);
                this.dgridItems.DataSource = dt;
           
            IsLoading = false;
        }

        private void FormatGrid()
        {
            this.dgridItems.Columns[0].HeaderText = "Item ID";
            this.dgridItems.Columns[1].HeaderText = "Part No";
            this.dgridItems.Columns[2].HeaderText = "Item No";
            this.dgridItems.Columns[3].HeaderText = "Name";
            this.dgridItems.Columns[4].HeaderText = "On Hand";
            this.dgridItems.Columns[5].HeaderText = "Last Cost";
            this.dgridItems.Columns[6].HeaderText = "Base Selling Price";
            this.dgridItems.Columns[5].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns[6].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns[3].Width = 250;
            ApplyItemFieldAccess(CommonClass.UserID);
        }

        private void dgridItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (!CanView)
            //{
            //    return;
            //}
            try
            {
                int lItemId = (int)this.dgridItems.CurrentRow.Cells[0].Value;
                Item ItemFrm = new Item(lItemId);
                this.Cursor = Cursors.WaitCursor;
                ItemFrm.MdiParent = this.MdiParent;
                ItemFrm.Show();
                ItemFrm.Focus();
                if (ItemFrm.DialogResult == DialogResult.Cancel || ItemFrm.DialogResult == DialogResult.OK)
                {
                    ItemFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgridItems.RowCount > 0)
            {
                int lItemId = (int)this.dgridItems.CurrentRow.Cells[0].Value;
                if (lItemId >= 0)
                {
                    Item ItemDlg = new Item(lItemId);
                    ItemDlg.ShowDialog();
                }
            }
          
        }

        private void dgridItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int lItemId = (int)this.dgridItems.CurrentRow.Cells[0].Value;
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sql = @"SELECT sl.EntityID
                                                  FROM SalesLines sl
                                                  INNER JOIN Sales s ON s.SalesID = sl.SalesID
                                                  WHERE s.LayoutType = 'Item'
                                                  AND sl.EntityID = @ItemID
                                                  UNION
                                                  SELECT pl.EntityID
                                                  FROM PurchaseLines pl
                                                  INNER JOIN Purchases p ON p.PurchaseID = pl.PurchaseID
                                                  WHERE p.LayoutType = 'Item'
                                                  AND pl.EntityID = @ItemID
                                                  UNION
                                                  SELECT il.ItemID as EntityID
                                                  FROM ItemsAdjustmentLines il
                                                  INNER JOIN ItemsAdjustment i ON i.ItemAdjID = il.ItemAdjID
                                                  WHERE il.ItemID = @ItemID";
            param.Add("ItemID", lItemId);

                object entityid =CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR , param);
                if (entityid != null
                    && Convert.ToInt32(entityid) > 0)
                {
                    btnDelete.Enabled = false;
                    //btnEdit.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                    //btnEdit.Enabled = true;
                }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (dgridItems.RowCount > 0)
            {
                int lItemId = (int)this.dgridItems.CurrentRow.Cells[0].Value;
                string lPartNo = this.dgridItems.CurrentRow.Cells[1].Value.ToString();
                if (lPartNo != "PDCODE")
                {
                    DialogResult yesno = MessageBox.Show("Are you sure you want to delete this item", "Confirm Delete", MessageBoxButtons.YesNo);
                    if (yesno == DialogResult.Yes)
                    {
                        string deletesql = @"DELETE FROM Items WHERE ID = @ItemID;
                                 DELETE FROM ItemsAutoBuild WHERE ItemID = @ItemID;
                                 DELETE FROM ItemsCostPrice WHERE ItemID = @ItemID;
                                 DELETE FROM ItemsQty WHERE ItemID = @ItemID;
                                 DELETE FROM ItemsSellingPrice WHERE ItemID = @ItemID;
                                 DELETE FROM ItemTransaction WHERE ItemID = @ItemID";
                        param.Add("@ItemID", lItemId);

                        int rowsaffected = CommonClass.runSql(deletesql, CommonClass.RunSqlInsertMode.QUERY, param);
                        if (rowsaffected > 0)
                        {
                            dgridItems.Rows.RemoveAt(dgridItems.CurrentRow.Index);
                            dgridItems.Refresh();
                            CommonClass.SaveSystemLogs(CommonClass.UserID, "Items", "Deleted Item with ItemID " + lItemId + " and Part Number " + lPartNo, lItemId.ToString(), "");
                            MessageBox.Show("Item deleted successfully");
                        }
                    }
                }
            }
        }

        private void dgridItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5
              || e.ColumnIndex == 6
           && e.RowIndex != this.dgridItems.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
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
            // only do it if the node became checked:
            if (e.Node.Checked)
            {
                // for all the nodes in the tree...
                foreach (TreeNode cur_node in e.Node.TreeView.Nodes)
                {
                    // ... which are not the freshly checked one...
                    if (cur_node != e.Node)
                    {
                        // ... uncheck them
                        cur_node.Checked = false;
                    }
                    foreach (TreeNode tn in cur_node.Nodes)
                    {
                        if (tn != e.Node && tn != cur_node)
                        {
                            // ... uncheck them
                            tn.Checked = false;
                        }
                    }
                }
            }
            treeCategory.EndUpdate();
        }

        private void txtPartNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtItemNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtSerialNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtSuppPartNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtCustomList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtCustomList2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtCustomList3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }
    }
}
