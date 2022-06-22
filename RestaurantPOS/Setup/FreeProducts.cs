using AbleRetailPOS.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Setup
{
    public partial class FreeProducts : Form
    {
        private DataTable FreeProductDT;
        CommonClass.FreeItemInvocation SrcOfInvoke;
        private int promoID;
        private int promovalue;
        private string thisFormCode = "";

        public FreeProducts(CommonClass.FreeItemInvocation pSrcInvoke, DataTable FreeProductTb = null, int PromoID = 0, int PointValue = 0)
        {
            FreeProductDT = FreeProductTb;
            promovalue = PointValue;
            SrcOfInvoke = pSrcInvoke;
            promoID = PromoID;
            InitializeComponent();
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
        public DataTable GetFreeProductTable
        {
            get { return FreeProductDT; }
        }

        private void FreeProducts_Load(object sender, EventArgs e)
        {
            if (SrcOfInvoke == CommonClass.FreeItemInvocation.ENTERSALES)
            {
                TxtNote.Text = "*Select " + promovalue + " Free Product.";

                dgFreeProducts.Columns["Selected"].Visible = true;
                dgFreeProducts.Columns["Price"].Visible = false;
                dgFreeProducts.Columns["TotalPrice"].Visible = false;
                LoadPromoItems(promoID);
                btnRemove.Visible = false;
                dgFreeProducts.Columns["PartNum"].ReadOnly = true;
                dgFreeProducts.Columns["TotalPrice"].ReadOnly = true;
                dgFreeProducts.Columns["Price"].ReadOnly = true;
                dgFreeProducts.Columns["Quantity"].ReadOnly = true;

            }
            else
            {
                PopulateDg();
                if (promoID != 0)
                {

                    LoadPromoItems(promoID);
                }
                else
                {
                    if (FreeProductDT.Rows.Count > 0)
                    {
                        for (int x = 0; x < FreeProductDT.Rows.Count; x++)
                        {
                            DataRow dr = FreeProductDT.Rows[x];
                            dgFreeProducts.Rows.Add();
                            DataGridViewRow dgvrows = dgFreeProducts.Rows[x];
                            dgvrows.Cells["ItemID"].Value = dr["itemid"].ToString();
                            dgvrows.Cells["PartNum"].Value = dr["PartNum"].ToString();
                            dgvrows.Cells["ItemName"].Value = dr["ItemName"].ToString();
                            dgvrows.Cells["Quantity"].Value = dr["quantity"].ToString();
                            dgvrows.Cells["Price"].Value = dr["Price"].ToString();
                            dgvrows.Cells["TotalPrice"].Value = float.Parse(dr["Price"].ToString()) * float.Parse(dr["quantity"].ToString());
                        }
                    }
                }
            }
        }
        void PopulateDg()
        {
            dgFreeProducts.Rows.Add();
            dgFreeProducts.Rows.Add();
            dgFreeProducts.Rows.Add();
            dgFreeProducts.Rows.Add();
            dgFreeProducts.Rows.Add();
            dgFreeProducts.Rows.Add();
        }
        private void dgFreeProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            switch (e.ColumnIndex)
            {
                case 1:
                    ShowItemLookup("", "PartNumber");
                    break;
                
            }
        }
        public void ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum,"" ,whereCon);

            DataGridViewRow dgvRows = dgFreeProducts.CurrentRow;
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                dgvRows.Cells["ItemID"].Value = ItemRows.Cells["ItemID"].Value.ToString();
                dgvRows.Cells["PartNum"].Value = ItemRows.Cells["PartNo"].Value;
                dgvRows.Cells["ItemName"].Value = ItemRows.Cells["ItemName"].Value;
                dgvRows.Cells["Price"].Value = ItemRows.Cells["SellingPrice"].Value; 
                dgvRows.Cells["Quantity"].Value = 1;
                dgvRows.Cells["TotalPrice"].Value = float.Parse(dgvRows.Cells["Price"].Value.ToString()) * float.Parse(dgvRows.Cells["Quantity"].Value.ToString());
                dgvRows.Cells["TaxCode"].Value = ItemRows.Cells["SalesTaxCode"].Value;
                dgFreeProducts.Rows.Add();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SrcOfInvoke == CommonClass.FreeItemInvocation.ENTERSALES)
            {
                int x = 0;
                
                foreach (DataGridViewRow dgvr in dgFreeProducts.Rows)
                {
                   
                    if (dgvr.Cells["Selected"].Value != null)
                    {
                        if (bool.Parse(dgvr.Cells["Selected"].Value.ToString()))
                        {
                            if(x > promovalue)
                            {
                                DialogResult PrintInvoice = MessageBox.Show("You've selected more than the required free item. Are you sure you want to continue", "Information", MessageBoxButtons.YesNo);
                                if (PrintInvoice == DialogResult.Yes)
                                {
                                   
                                }
                                else
                                {
                                    return;
                                }
                                
                            }
                                
                            FreeProductDT.Rows.Add();
                            FreeProductDT.Rows[x]["ItemID"] = dgvr.Cells["ItemID"].Value.ToString();
                            FreeProductDT.Rows[x]["Quantity"] = dgvr.Cells["Quantity"].Value.ToString();
                            FreeProductDT.Rows[x]["PartNum"] = dgvr.Cells["PartNum"].Value.ToString();
                            FreeProductDT.Rows[x]["ItemName"] = dgvr.Cells["ItemName"].Value.ToString();
                            FreeProductDT.Rows[x]["SalesTaxCode"] = dgvr.Cells["TaxCode"].Value.ToString();
                            FreeProductDT.Rows[x]["CostPrice"] = dgvr.Cells["CostPrice"].Value.ToString();
                            FreeProductDT.Rows[x]["Price"] = dgvr.Cells["Price"].Value.ToString();
                            x++;
                        }
                    }
                    dgFreeProducts.Columns["Selected"].Visible = true;
                }
            }
            else
            {
                int x = 0;
                foreach (DataGridViewRow dx in dgFreeProducts.Rows)
                {
                    if (dx.Cells["ItemID"].Value != null && dx.Cells["ItemID"].Value.ToString() != "")
                    {
                        if (x >= FreeProductDT.Rows.Count)
                            FreeProductDT.Rows.Add();

                        FreeProductDT.Rows[x]["ItemID"] = dx.Cells["ItemID"].Value.ToString();
                        FreeProductDT.Rows[x]["Quantity"] = dx.Cells["Quantity"].Value.ToString();
                        FreeProductDT.Rows[x]["PartNum"] = dx.Cells["PartNum"].Value.ToString();
                        FreeProductDT.Rows[x]["ItemName"] = dx.Cells["ItemName"].Value.ToString();
                        FreeProductDT.Rows[x]["Price"] = dx.Cells["Price"].Value.ToString();
                        x++;
                    }
                }
            }
            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Promotions Free Product for Promo ID " + promoID.ToString(), promoID.ToString());
            DialogResult = DialogResult.OK;
        }
        void LoadPromoItems(int promoID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string promosql = @"Select i.PartNumber, p.quantity ,i.ItemName,p.itemid , ix.Level0 ,i.SalesTaxCode, c.AverageCostEx
                                From PromotionItems p 
                                Inner Join Items i On p.itemid = i.ID
                                Inner Join ItemsSellingPrice ix On i.ID = ix.ItemID 
                                Inner Join ItemsCostPrice c On i.ID = c.ItemID
                                LEFT JOIN TaxCodes t ON i.SalesTaxCode = t.taxcode 
                                WHERE c.LocationID = 1 and promoid= @PromoID";
            param.Add("@PromoID", promoID);
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, promosql, param);
            if(dt.Rows.Count > 0)
            {
               for (int i = 0; i< dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dgFreeProducts.Rows.Add();
                    dgFreeProducts.Rows[i].Cells["ItemID"].Value = dr["itemid"].ToString();
                    dgFreeProducts.Rows[i].Cells["PartNum"].Value = dr["PartNumber"].ToString();
                    dgFreeProducts.Rows[i].Cells["Quantity"].Value = dr["quantity"].ToString();
                    dgFreeProducts.Rows[i].Cells["ItemName"].Value = dr["ItemName"].ToString();
                    dgFreeProducts.Rows[i].Cells["Price"].Value = dr["Level0"].ToString();
                    dgFreeProducts.Rows[i].Cells["TotalPrice"].Value = float.Parse(dr["Level0"].ToString()) * float.Parse(dr["quantity"].ToString());
                    dgFreeProducts.Rows[i].Cells["Selected"].Value = "false";
                    dgFreeProducts.Rows[i].Cells["TaxCode"].Value= dr["SalesTaxCode"].ToString();
                    dgFreeProducts.Rows[i].Cells["CostPrice"].Value = dr["AverageCostEx"].ToString();
                }
            }
        }

        private void dgFreeProducts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgFreeProducts.Rows.Count <= 0)
                return;

            switch (e.ColumnIndex)
            {
                case 3:
                    dgFreeProducts.CurrentRow.Cells["TotalPrice"].Value = float.Parse(dgFreeProducts.CurrentRow.Cells["Quantity"].Value.ToString()) * float.Parse(dgFreeProducts.CurrentRow.Cells["Price"].Value.ToString());
                    break;
                case 1:
                    string partN = dgFreeProducts.Rows[e.RowIndex].Cells["PartNum"].Value == null ? "" : dgFreeProducts.Rows[e.RowIndex].Cells["PartNum"].Value.ToString();
                    ShowItemLookup(partN, "PartNumber");
                    break;
            }
        }

        private void dgFreeProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgFreeProducts.Rows.Count <= 0)
                return;
            if (e.ColumnIndex == 4|| e.ColumnIndex == 5)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dgFreeProducts.SelectedCells)
            {
                if (oneCell.RowIndex >= 0 && oneCell.RowIndex < (dgFreeProducts.Rows.Count - 1))
                    dgFreeProducts.Rows.RemoveAt(oneCell.RowIndex);
            }
            dgFreeProducts.Rows.Add();
        }

        private void dgFreeProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if(e.ColumnIndex == 1)
            {
                this.dgFreeProducts.BeginEdit(true);
            }
        }
    }//END
}
