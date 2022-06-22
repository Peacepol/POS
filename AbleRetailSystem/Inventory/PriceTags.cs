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

namespace RestaurantPOS.Inventory
{
    public partial class PriceTags : Form
    {
        int ItemId = 0;
        string SelItemID = "";
        DataTable dtItems;
        private DataTable dtHistory;
        BindingList<string> Price;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        private string thisFormCode = "";
        public PriceTags()
        {
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                outx = false;
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

        void priceLevelChange()
        {
          
            string[] priceLevel;
            if (rbSale.Checked)
            {
                priceLevel = new string[] { "SalesPrice0",
                                            "SalesPrice1",
                                            "SalesPrice2",
                                            "SalesPrice3",
                                            "SalesPrice4",
                                            "SalesPrice5",
                                            "SalesPrice6",
                                            "SalesPrice7",
                                            "SalesPrice8",
                                            "SalesPrice9",
                                            "SalesPrice10",
                                            "SalesPrice11",
                                            "SalesPrice12" };
            }
            else
            {
                priceLevel = new string[] { "Level0",
                                            "Level1",
                                            "Level2",
                                            "Level3",
                                            "Level4",
                                            "Level5",
                                            "Level6",
                                            "Level7",
                                            "Level8",
                                            "Level9",
                                            "Level10",
                                            "Level11",
                                            "Level12" };
            }
            Price = new BindingList<string>(priceLevel);
            cbPriceLevel.DataSource = Price;
            //if (dgItemList.Rows.Count > 0)
            //{
            //    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dgItemList.CurrentRow.Cells["PriceLevel"];
            //    cbCell.DataSource = Price;
            //}

        }

        private void rbSale_CheckedChanged(object sender, EventArgs e)
        {
            priceLevelChange();
        }

        private void btnGenPriceTag_Click(object sender, EventArgs e)
        {
            DataTable TbRep = new DataTable();
            TbRep.Columns.Add("Price", typeof(float));
            TbRep.Columns.Add("ItemName", typeof(string));
            TbRep.Columns.Add("PartNumber", typeof(string));
            TbRep.Columns.Add("ItemNumber", typeof(string));
            TbRep.Columns.Add("CategoryCode", typeof(string));
            TbRep.Columns.Add("ItemDescription", typeof(string));

            foreach (DataGridViewRow dgv in dgItemList.Rows)
            {
                string pricetags = "SELECT " + cbPriceLevel.Text + " as Price, ItemName, PartNumber ,ItemNumber,CategoryCode, ItemDescription FROM ItemsSellingPrice isp INNER JOIN Items i ON i.ID =isp.ItemID LEFT JOIN Category c ON i.CategoryID = c.CategoryID WHERE i.ID =" + dgv.Cells["ID"].Value.ToString();
                DataTable tempTable = new DataTable();
                CommonClass.runSql(ref tempTable, pricetags);
                DataRow dr = tempTable.Rows[0];
                for (int x = 0; x < int.Parse(dgv.Cells["Quantity"].Value.ToString()); x++)
                {
                    DataRow rw = TbRep.NewRow();
                    rw["Price"] = dr[0];
                    rw["ItemName"] = dr[1].ToString();
                    rw["PartNumber"] = dr[2];
                    rw["ItemNumber"] = dr[3].ToString();
                    rw["CategoryCode"] = dr[4];
                    rw["ItemDescription"] = dr[5];

                    TbRep.Rows.Add(rw);
                }
            }
            Reports.ReportParams PriceTag = new Reports.ReportParams();
            PriceTag.PrtOpt = 1;
            PriceTag.Rec.Add(TbRep);
            if (cmbPaperSize.Text == "A4(210x297mm)")
            {
                PriceTag.ReportName = "GeneratePriceTags.rpt";
            }
            else if (cmbPaperSize.Text == "A5(148x210mm)")
            {
                PriceTag.ReportName = "GeneratePriceTagsA5.rpt";
            }
            else if (cmbPaperSize.Text == "A7(74x105mm)")
            {
                PriceTag.ReportName = "GeneratePriceTagsA7.rpt";
            }
            PriceTag.RptTitle = "GeneratePriceTags";
            PriceTag.Params = "PriceLevel";
            PriceTag.PVals = cbPriceLevel.Text;
            CommonClass.ShowReport(PriceTag);
        }

        private void PriceTags_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            btnSelectItems.Enabled = CanAdd;
            btnRemoveRow.Enabled = CanAdd;
            btnRemoveAll.Enabled = CanAdd;
            btnHistory.Enabled = CanAdd;
            // LoadItems();
            gbGenPrice.Enabled = CanAdd;
            dgItemList.Columns[1].Visible = false;
            priceLevelChange();
            


        }
        void LoadItems()
        {
            DataTable itemdt = new DataTable();
            string itemsql = "Select ItemName, ID  From Items";
            CommonClass.runSql(ref itemdt, itemsql);
            if(itemdt.Rows.Count > 0)
            {
               for(int i = 0; i< itemdt.Rows.Count; i++)
                {
                    DataRow dr = itemdt.Rows[i];
                    dgItemList.Rows.Add();
                    dgItemList.Rows[i].Cells["ID"].Value = dr["ID"];
                    dgItemList.Rows[i].Cells["Selected"].Value = "false";
                    dgItemList.Rows[i].Cells["ItemName"].Value = dr["ItemName"];
                    dgItemList.Rows[i].Cells["Quantity"].Value = "0";
                }
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            dtHistory = new DataTable();
            dtHistory.Columns.Add("ItemID", typeof(string));
            dtHistory.Columns.Add("ItemName", typeof(string));

            string[] RowArray;

            PriceHistory pricehis = new PriceHistory(dtHistory);
            if (pricehis.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow dr in dtHistory.Rows)
                {
                    if (checkifExist(dr["ItemID"].ToString()))
                    {
                        RowArray = new string[4];
                        RowArray[0] = dr["ItemID"].ToString();
                        RowArray[1] = "true";
                        RowArray[2] = dr["ItemName"].ToString();
                        RowArray[3] = "1";
                        dgItemList.Rows.Add(RowArray);
                    }
                }
            }
            
           
        }

        private void dgItemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colindex = (int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex);
            e.Control.KeyPress -= Numeric_KeyPress;

            if (colindex == 3 )
            {

                e.Control.KeyPress += TextboxNumeric_KeyPress;
            }
            else
            {
                e.Control.KeyPress -= TextboxNumeric_KeyPress;
            }
        }
        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
            }
        }

        private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
              && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // only allow one negative char before the number
            if (e.KeyChar == '-'
                && (sender as TextBox).Text.IndexOf('-') == 0)
            {
                e.Handled = true;
            }
        }

        private void btnSelectItems_Click(object sender, EventArgs e)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.STOCKTAKE, "", "", "PartNumber");
            if (Items.ShowDialog() == DialogResult.OK)
            {
                string dr = Items.GetitemIDs;
                if (dr != null)
                {
                    SelItemID = dr;
                    SearchItems();
                   
                }
            }
        }

        private void SearchItems()
        {
            string strCon = "";
            dtItems = new DataTable();
            strCon = " where IsSold = 1";//ALL
            strCon += " AND IsInactive = 0";//s.*
            if (SelItemID != "")
            {
                strCon += " AND ID in (" + SelItemID + ")";
            }
            string selectSql = @"Select ItemName, ID  From Items " + strCon;
            CommonClass.runSql(ref dtItems, selectSql);
            // this.dgridItems.DataSource = dtPrice;
            string[] RowArray;
          
           // RowArray[0] = dr["Name"].ToString();
            if (dtItems.Rows.Count > 0)
            {
                   

                    if (dgItemList.Rows.Count > 0)
                {
                    for (int x = 0; x < dtItems.Rows.Count; x++)
                    {
                        DataRow dr = dtItems.Rows[x];
                        if (checkifExist(dr["ID"].ToString()))
                        {
                            RowArray = new string[4];
                            RowArray[0] = dr["ID"].ToString();
                            RowArray[1] = "true";
                            RowArray[2] = dr["ItemName"].ToString();
                            RowArray[3] = "1";
                            dgItemList.Rows.Add(RowArray);
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    for (int x = 0; x < dtItems.Rows.Count; x++)
                    {
                        DataRow dr = dtItems.Rows[x];
                        dgItemList.Rows.Add();
                            dgItemList.Rows[x].Cells["ID"].Value = dr["ID"];
                            dgItemList.Rows[x].Cells["Selected"].Value = "true";
                            dgItemList.Rows[x].Cells["ItemName"].Value = dr["ItemName"];
                        //DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dgItemList.Rows[x].Cells["PriceLevel"];
                        //cbCell.DataSource = Price;
                        //dgItemList.Rows[x].Cells["PriceLevel"].Value = "Level0";
                        dgItemList.Rows[x].Cells["Quantity"].Value = "1";
                    }
                }
            }
           // FormatGrid();
        }
        public bool checkifExist(string itemID)
        {
            foreach (DataGridViewRow dgvr in dgItemList.Rows)
            {
                if (dgvr.Cells["ID"].Value.ToString() == itemID)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count <= 0)
                return;

            dgItemList.Rows.Remove(dgItemList.CurrentRow);
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            dgItemList.Rows.Clear();
        }
    }
}
