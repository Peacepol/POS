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
    public enum ItemLookupSource
    {
        SELF = 1,
        ENTERPURCHASE,  
        ENTERSALES,
        STOCKTAKE
    };
    public partial class ItemLookup : Form
    {
        private string pItemNum;
        private string PriceLevel = "Level0";
        private static bool IsLoading = false;
        private static bool IsMain = false;
        private static string WhereCon = "";
        private static DataGridViewRow ItemRow;
        private string itemIDs = "";
        private string txtSearch = "";
        string categories = "";



        ItemLookupSource SrcOfInvoke;
        public ItemLookup(ItemLookupSource pSrcInvoke, string ItemNum = "", string pCustomerID = "", string WhereConect = "PartNumber")
        {
            InitializeComponent();
            pItemNum = ItemNum;
            SrcOfInvoke = pSrcInvoke;
            WhereCon = WhereConect;
            if (pCustomerID != "")
            {
                PriceLevel = GetCustomerPriceLevel(pCustomerID);
            }
            //else
            //{
            //    SearchItems(WhereCon, this.txtSearch.Text, false);
            //}
        }

        public DataGridViewRow GetSelectedItem
        {
            get { return ItemRow; }
        }
        public string GetitemIDs
        {
            get { return itemIDs; }
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
        private void ItemLookup_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dgridItems.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            txtSearch = pItemNum;
            if (WhereCon == "PartNumber")
            {
                WhereCon = "Part Number";
                txtPartNum.Text = txtSearch;

            }
            if (WhereCon == "ItemName")
            {
                WhereCon = "Item Name";
                txtItemName.Text = txtSearch;
            }
            SearchItems();
            if (txtSearch == pItemNum)
            {
                if (txtSearch != "")
                {
                    btnSearch.PerformClick();
                }
            }
            if (SrcOfInvoke != ItemLookupSource.STOCKTAKE)
            {
                this.dgridItems.Size = new System.Drawing.Size(887, 474);
                btnOK.Visible = false;
            }
            LoadCategory();
            FormatGrid();

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
            if (txtSerialNum.Text != "")
            {
                txtSearch = txtSerialNum.Text;
                WhereCon = "Barcode";
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
            //txtFind();
           
            SearchItems(false);
        }
        //private void SearchItems(string pWhereCol = "", string pSearchText, bool isLoad = true)
        private void SearchItems(bool isLoad = true)
        {
            filterCategories();
            dgridItems.Rows.Clear();
            IsLoading = true;
            string strMain = "";
           
            string strPurchaseCon = (SrcOfInvoke == ItemLookupSource.ENTERPURCHASE ? " i.IsBought = 1 " :"");
            strPurchaseCon = (SrcOfInvoke == ItemLookupSource.ENTERSALES ? " i.IsSold = 1 " : "");
            string strCon = strPurchaseCon;
           
          
           
            if (txtPartNum.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + "( i.PartNumber LIKE @txtPartNum or bc.BarcodeData LIKE  @txtPartNum )";
            }
            if (txtItemNum.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " i.ItemNumber LIKE @txtItemNum";
            }
            if (categories != "")
            {
                strCon += (strCon != "" ? " and " : "") + " g.CategoryID in (" + categories + ")";
            }
            if (txtDesc.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " i.ItemDescriptionSimple LIKE @txtDesc";
            }
            if (txtSupplier.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " p.Name LIKE @txtSupplier";
            }
            if (txtBrand.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " i.PartNumber LIKE @txtBrand ";
            }
            if (txtSuppPartNum.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " i.SupplierPartNumber LIKE @txtSuppPartNum";
            }
            if (txtItemName.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " i.ItemName LIKE @txtItemName";
            }
            if (txtSerialNum.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " bc.BarcodeData LIKE @txtSerialNum";
            }
            if (txtCustomList1.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " c1.List1Name LIKE @txtCustomList1";
            }
            if (txtCustomList2.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " c2.List2Name LIKE @txtCustomList2 ";
            }
            if (txtCustomList3.Text != "")
            {
                strCon += (strCon != "" ? " and " : "") + " c3.List3Name LIKE @txtCustomList3";
            }
            if (chkWithQty.Checked)
            {
                strCon += (strCon != "" ? " and " : "") + " q.OnHandQty > 0 ";
            }
           string selectSql = @"SELECT DISTINCT i.ID, i.ItemDescriptionSimple, i.PartNumber, i.ItemNumber, i.ItemName, 
                    q.OnHandQty, c.LastCostEx,c.StandardCostEx, c.AverageCostEx, s." + PriceLevel + ", i.SalesTaxCode,";
            selectSql += @"t.TaxCollectedAccountID, t.TaxPercentageRate as RateTaxSales,
                    i.PurchaseTaxCode, tp.TaxPaidAccountID,  tp.TaxPercentageRate as RateTaxPurchase,i.IsCounted, i.AssetAccountID, i.IsBought, i.COSAccountID ,i.SupplierID, i.CategoryID,i.BrandName ,i.IsAutoBuild, i.BundleType,CONVERT(varchar(max), i.ItemDescription) as ItemDescription, p.Name FROM 
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
                    LEFT JOIN CustomList3 c3 ON c2.ID = i.CList3 " + (strCon != "" ? " where " : "") + strCon + " ORDER BY ID ASC";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@txtPartNum", "%"+ txtPartNum.Text+"%");
            param.Add("@txtItemNum", "%"+ txtItemNum.Text + "%");
            param.Add("@txtDesc", "%" + txtDesc.Text + "%");
            param.Add("@txtSupplier", "%" + txtSupplier.Text + "%");
            param.Add("@txtBrand", "%" + txtBrand.Text + "%");
            param.Add("@txtSuppPartNum", "%" + txtSuppPartNum.Text + "%");
            param.Add("@txtItemName", "%" + txtItemName.Text + "%");
            param.Add("@txtSerialNum", "%" + txtSerialNum.Text + "%");
            param.Add("@txtCustomList1", "%" + txtCustomList1.Text + "%");
            param.Add("@txtCustomList2", "%" + txtCustomList2.Text + "%");
            param.Add("@txtCustomList3", "%" + txtCustomList3.Text + "%");

            CommonClass.runSql(ref dt,selectSql,param);
            if (isLoad 
                && pItemNum != "" 
                && dt.Rows.Count == 1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow x = dt.Rows[i];
                    dgridItems.Rows.Add();
                    dgridItems.Rows[i].Cells["ItemID"].Value = x["ID"];//item ID 0
                    dgridItems.Rows[i].Cells["PartNo"].Value = x["PartNumber"];//Part N0 1
                    dgridItems.Rows[i].Cells["ItemNo"].Value = x["ItemNumber"];//Item No 2
                    dgridItems.Rows[i].Cells["ItemName"].Value = x["ItemName"];//Name 3
                    dgridItems.Rows[i].Cells["OnHand"].Value = x["OnHandQty"];//On Hand 4
                    dgridItems.Rows[i].Cells["LastCost"].Value = x["LastCostEx"]; //Last Cost
                    dgridItems.Rows[i].Cells["StandardCost"].Value = x["StandardCostEx"];//Standard Cost
                    dgridItems.Rows[i].Cells["AverageCostEx"].Value = x["AverageCostEx"];//AverageCostEx
                    dgridItems.Rows[i].Cells["SellingPrice"].Value = x[PriceLevel];//Selling Price
                    dgridItems.Rows[i].Cells["ItemDescriptionSimple"].Value = x["ItemDescriptionSimple"].ToString(); 
                    if (SrcOfInvoke != ItemLookupSource.ENTERPURCHASE)
                    {
                        this.dgridItems.Columns["StandardCost"].Visible = false;
                        this.dgridItems.Columns["LastCost"].Visible = false;
                        this.dgridItems.Columns["AverageCostEx"].Visible = false;
                        this.dgridItems.Columns["SellingPrice"].Visible = true;
                    }
                    dgridItems.Rows[i].Cells["SalesTaxCode"].Value = x["SalesTaxCode"];//
                    dgridItems.Rows[i].Cells["TaxCollectedAccountID"].Value = x["TaxCollectedAccountID"];//
                    dgridItems.Rows[i].Cells["RateTaxSales"].Value = x["RateTaxSales"];//
                    dgridItems.Rows[i].Cells["PurchaseTaxCode"].Value = x["PurchaseTaxCode"];//PurchaseTaxCode
                    dgridItems.Rows[i].Cells["TaxPaidAccountID"].Value = x["TaxPaidAccountID"];//
                    dgridItems.Rows[i].Cells["RateTaxPurchase"].Value = x["RateTaxPurchase"];//
                    dgridItems.Rows[i].Cells["IsCounted"].Value = x["IsCounted"];//
                    dgridItems.Rows[i].Cells["AssetAccountID"].Value = x["AssetAccountID"];//
                    dgridItems.Rows[i].Cells["IsBought"].Value = x["IsBought"];//
                    dgridItems.Rows[i].Cells["COSAccountID"].Value = x["COSAccountID"];//
                    dgridItems.Rows[i].Cells["SupplierID"].Value = x["SupplierID"];//
                    dgridItems.Rows[i].Cells["CategoryID"].Value = x["CategoryID"];//
                    dgridItems.Rows[i].Cells["BrandName"].Value = x["BrandName"];//
                    dgridItems.Rows[i].Cells["stockcheck"].Value = "false";
                    dgridItems.Rows[i].Cells["isAutoBuild"].Value = x["IsAutoBuild"];
                    dgridItems.Rows[i].Cells["BundleType"].Value = x["BundleType"];
                    dgridItems.Rows[i].Cells["ItemDescription"].Value = x["ItemDescription"];
                    dgridItems.Rows[i].Cells["SupplierName"].Value = x["Name"];

                }
                if (SrcOfInvoke == ItemLookupSource.STOCKTAKE)
                {
                    this.dgridItems.Columns[22].Visible = true;
                }
                dgridItems.CurrentRow.Selected = true;
                ItemRow = this.dgridItems.CurrentRow;
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            else
            {
                if(dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow x = dt.Rows[i];
                        dgridItems.Rows.Add();
                        dgridItems.Rows[i].Cells["ItemID"].Value = x["ID"];//item ID
                        dgridItems.Rows[i].Cells["PartNo"].Value = x["PartNumber"];//Part N0
                        dgridItems.Rows[i].Cells["ItemNo"].Value = x["ItemNumber"];//Item No
                        dgridItems.Rows[i].Cells["ItemName"].Value = x["ItemName"];//Name
                        dgridItems.Rows[i].Cells["OnHand"].Value = x["OnHandQty"];//On Hand
                        dgridItems.Rows[i].Cells["LastCost"].Value = x["LastCostEx"]; //Last Cost
                        dgridItems.Rows[i].Cells["StandardCost"].Value = x["StandardCostEx"];//Standard Cost
                        dgridItems.Rows[i].Cells["AverageCostEx"].Value = x["AverageCostEx"];//AverageCostEx
                        dgridItems.Rows[i].Cells["SellingPrice"].Value = x[PriceLevel];//Selling Price 
                        dgridItems.Rows[i].Cells["ItemDescriptionSimple"].Value = x["ItemDescriptionSimple"].ToString();
                        if (SrcOfInvoke != ItemLookupSource.ENTERPURCHASE)
                        {
                            this.dgridItems.Columns["StandardCost"].Visible = false;
                            this.dgridItems.Columns["AverageCostEx"].Visible = false;
                            this.dgridItems.Columns["LastCost"].Visible = false;
                            this.dgridItems.Columns["SellingPrice"].Visible = true;

                        }
                        dgridItems.Rows[i].Cells["SalesTaxCode"].Value = x["SalesTaxCode"];//
                        dgridItems.Rows[i].Cells["TaxCollectedAccountID"].Value = x["TaxCollectedAccountID"];//
                        dgridItems.Rows[i].Cells["RateTaxSales"].Value = x["RateTaxSales"];//
                        dgridItems.Rows[i].Cells["PurchaseTaxCode"].Value = x["PurchaseTaxCode"];//PurchaseTaxCode
                        dgridItems.Rows[i].Cells["TaxPaidAccountID"].Value = x["TaxPaidAccountID"];//
                        dgridItems.Rows[i].Cells["RateTaxPurchase"].Value = x["RateTaxPurchase"];//
                        dgridItems.Rows[i].Cells["IsCounted"].Value = x["IsCounted"];//
                        dgridItems.Rows[i].Cells["AssetAccountID"].Value = x["AssetAccountID"];//
                        dgridItems.Rows[i].Cells["IsBought"].Value = x["IsBought"];//
                        dgridItems.Rows[i].Cells["COSAccountID"].Value = x["COSAccountID"];//
                        dgridItems.Rows[i].Cells["SupplierID"].Value = x["SupplierID"];//
                        dgridItems.Rows[i].Cells["CategoryID"].Value = x["CategoryID"];//
                        dgridItems.Rows[i].Cells["BrandName"].Value = x["BrandName"];//
                        dgridItems.Rows[i].Cells["stockcheck"].Value = "false";
                        dgridItems.Rows[i].Cells["isAutoBuild"].Value = x["IsAutoBuild"];
                        dgridItems.Rows[i].Cells["BundleType"].Value = x["BundleType"];
                        dgridItems.Rows[i].Cells["ItemDescription"].Value = x["ItemDescription"];
                        dgridItems.Rows[i].Cells["SupplierName"].Value = x["Name"];
                    }
                }
                if (SrcOfInvoke == ItemLookupSource.STOCKTAKE)
                {
                    this.dgridItems.Columns["stockcheck"].Visible = true;
                }
            }

            IsLoading = false;
        }
        public void addRows()
        {
        }

        private void FormatGrid()
        {
            ApplyItemFieldAccess(CommonClass.UserID);
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
                if (dgvr.Cells[6].Value.ToString() != "")
                {
                    BaseSellingPriceFieldRights();
                }
                if (dgvr.Cells[7].Value.ToString() != "")
                {
                    AverageCostPriceFieldRights();
                }
                if (dgvr.Cells[8].Value.ToString() != "")
                {
                    BestSellingPriceFieldRights();
                    BestSellingPriceFieldRightsStandard();

                }
            }
        }
        private void BestSellingPriceFieldRights()
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue("txtAverageCost", out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        this.dgridItems.Columns[8].Visible = valstr;
                    }
                    else
                    {
                        this.dgridItems.Columns[8].Visible = valstr;
                    }
                }
            }
        }
        private void BestSellingPriceFieldRightsStandard()
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue("txtStandardCost", out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        this.dgridItems.Columns[8].Visible = valstr;
                    }
                    else
                    {
                        this.dgridItems.Columns[8].Visible = valstr;
                    }
                }
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
                        this.dgridItems.Columns[7].Visible = valstr;
                    }
                    else
                    {
                        this.dgridItems.Columns[7].Visible = valstr;
                    }
                }
            }
        }

        private void dgridItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SrcOfInvoke != ItemLookupSource.STOCKTAKE)
            {
                ItemRow = this.dgridItems.CurrentRow;
                this.DialogResult = DialogResult.OK;
            }

        }

        public static string GetCustomerPriceLevel(string pCustomerID)
        {
            string retstr = "Level0";
            
            string sql = @"SELECT ItemPriceLevel from Profile where ID = " + pCustomerID;

               
            DataTable ltb = new DataTable();
            CommonClass.runSql(ref ltb, sql);
               
            if (ltb.Rows.Count > 0)
            {
                retstr = "Level" + ltb.Rows[0]["ItemPriceLevel"].ToString();
            }
            return retstr;
            
        } //END

        private void dgridItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 //Price
                || e.ColumnIndex == 6 //Amount
                || e.ColumnIndex == 7 //Amount
                || e.ColumnIndex == 8 //Amount
                && e.RowIndex != this.dgridItems.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {            
            int i = 0;
            foreach(DataGridViewRow x in dgridItems.Rows)
            {
                if (bool.Parse(x.Cells[22].Value.ToString()))
                {
                    itemIDs += x.Cells[0].Value.ToString() + ",";
                    i++;
                }
            }
            if (i > 0)
            {
                itemIDs = itemIDs.Remove(itemIDs.Length - 1);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Must check atleast 1", "Information");
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
