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
using System.Globalization;

namespace RestaurantPOS.Inventory
{
    public partial class ItemPriceUpdate : Form
    {
        private string WhereCon = "";
        private int RoundingMode = 0;
        private int ApplyMode = 0;
        string SelItemID = "";

        private static DataTable ItemPriceChanges;
        private static DataTable ItemOldPriceChanges;
        DataTable dtPrice;
        private string thisFormCode = "";
        private bool CanView = false;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;

        public ItemPriceUpdate()
        {
            InitializeComponent();
            ItemOldPriceChanges = new DataTable();
            ItemOldPriceChanges.Columns.Add("colname", typeof(string));
            ItemOldPriceChanges.Columns.Add("itemID", typeof(string));
            ItemOldPriceChanges.Columns.Add("colvalue", typeof(object));
            ItemOldPriceChanges.Columns.Add("CalcMethod", typeof(string));
            ItemOldPriceChanges.Columns.Add("PercentChange", typeof(string));
            ItemPriceChanges = ItemOldPriceChanges.Clone();
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
      
            //CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            //bool outy = false;
            //if (FormRights != null && FormRights.Count > 0)
            //{
                
            //    outy = false;
            //    FormRights.TryGetValue("Edit", out outy);
            //    if (outy == true)
            //    {
            //        CanEdit = true;
            //    }
              
            //}
        }

        private void ItemPriceUpdate_Load(object sender, EventArgs e)
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
            this.cboPriceLevel.SelectedIndex = 0;
            this.btnRecord.Enabled = CanEdit;
            btnRecordALL.Enabled = CanEdit;
            if (CommonClass.IsItemPriceInclusive)
            {
                this.lblNote.Text = "*Price below should be Tax Inclusive.";
            }else
            {
                this.lblNote.Text = "*Price below should be Tax Exclusive.";
            }
            for (int i = 0; i < 13; i++)
            {
                dgridItems.Columns.Add("Level" + i, "Level" + i);
            }

            for (int i = 0; i < 13; i++)
            {
                dgridItems.Columns.Add("SalesPrice" + i, "SalesPrice" + i);
            }
            FormatGrid();
        }
       

        private void chkMargin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMargin.Checked)
            {
                this.txtMargin.Enabled = true;
                this.chkMarkup.Checked = false;
                this.chkFixed.Checked = false;
                this.chkFixedPrice.Checked = false;
                this.chkDiscountFrmBase.Checked = false;
            }
            else
            {
                this.txtMargin.Enabled = false;
            }
        }

        private void chkMarkup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMarkup.Checked)
            {
                this.txtMarkup.Enabled = true;
                this.chkMargin.Checked = false;
                this.chkFixed.Checked = false;
                this.chkFixedPrice.Checked = false;
                this.chkDiscountFrmBase.Checked = false;
            }
            else
            {
                this.txtMarkup.Enabled = false;
            }
        }

        private void chkFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFixed.Checked)
            {
                this.txtFixed.Enabled = true;
                this.chkMargin.Checked = false;
                this.chkMarkup.Checked = false;
                this.chkFixedPrice.Checked = false;
                this.chkDiscountFrmBase.Checked = false;
            }
            else
            {
                this.txtFixed.Enabled = false;
            }
        }

        private void chkNoRounding_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoRounding.Checked)
            {
                chkRound5.Checked = false;
                chkRound99.Checked = false;
                RoundingMode = 0;
            }

        }

        private void chkRound5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRound5.Checked)
            {
                chkNoRounding.Checked = false;
                chkRound99.Checked = false;
                RoundingMode = 1;
            }
        }

        private void chkRound99_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRound99.Checked)
            {
                chkNoRounding.Checked = false;
                chkRound5.Checked = false;
                RoundingMode = 2;
            }

        }
        public bool checkifExist(string itemID)
        {
            foreach (DataGridViewRow dgvr in dgridItems.Rows)
            {
                if (dgvr.Cells["ItemID"].Value.ToString() == itemID)
                {
                    return false;
                }
            }
            return true;
        }
        private void SearchItems()
        {
            string strCon = "";
            dtPrice = new DataTable();
                strCon = " where IsSold = 1";//ALL
                strCon += " AND i.IsInactive = 0";//s.*
            if(SelItemID != "")
            {
                strCon += " AND i.ID in ("+ SelItemID + ")";
            }
                string selectSql = @"Select DISTINCT CAST(0 as bit) as SelectedItem, i.PartNumber, i.ItemNumber, i.ItemName, c.LastCostEx,c.AverageCostEx,s.*, ISNULL(t.TaxPercentageRate,0) as TaxRate from 
                        (((( Items as i inner join ItemsSellingPrice as s on i.ID = s.ItemID )
                        inner join ItemsCostPrice as c on i.ID = c.ItemID ) 
                        left join Profile as p on i.SupplierID = p.ID )
                        left join TaxCodes as t on i.SalesTaxCode = t.taxcode )
                        left join TaxCodes as tp on i.PurchaseTaxCode = tp.taxcode " + strCon;
                CommonClass.runSql(ref dtPrice, selectSql);
            // this.dgridItems.DataSource = dtPrice;

            if (dtPrice.Rows.Count > 0)
            {
                if (dgridItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPrice.Rows.Count; i++)
                    {                    
                        DataRow drExist = dtPrice.Rows[i];                       
                        if (checkifExist(drExist["ItemID"].ToString()))
                        {
                            dgridItems.Rows.Add();

                            for (int x = 0; x < dtPrice.Rows.Count; x++)
                            {
                                int rowindex = dgridItems.Rows.Count - 1;

                                DataRow dr = dtPrice.Rows[x];
                                dgridItems.Rows[rowindex].Cells[0].Value = dr["SelectedItem"];//checkbox
                                dgridItems.Rows[rowindex].Cells[1].Value = dr["ItemID"];//partnum
                                dgridItems.Rows[rowindex].Cells[2].Value = dr["PartNumber"];
                                dgridItems.Rows[rowindex].Cells[3].Value = dr["LocationID"];
                                dgridItems.Rows[rowindex].Cells[4].Value = dr["ItemName"];
                                dgridItems.Rows[rowindex].Cells[5].Value = dr["LastCostEx"];
                                dgridItems.Rows[rowindex].Cells[6].Value = dr["AverageCostEx"];
                                dgridItems.Rows[rowindex].Cells[7].Value = dr["TaxRate"];
                                for (int j = 0; j < 13; j++)
                                {
                                    dgridItems.Rows[rowindex].Cells["Level" + j].Value = dr["Level" + j];
                                }
                                for (int j = 0; j < 13; j++)
                                {
                                    dgridItems.Rows[rowindex].Cells["SalesPrice" + j].Value = dr["SalesPrice" + j];
                                }
                            }

                        }
                    }
 
                }
                else
                {
                    for (int x = 0; x < dtPrice.Rows.Count; x++)
                    {
                        DataRow dr = dtPrice.Rows[x];
                        dgridItems.Rows.Add();
                        dgridItems.Rows[x].Cells[0].Value = dr["SelectedItem"];//checkbox
                        dgridItems.Rows[x].Cells[1].Value = dr["ItemID"];//partnum
                        dgridItems.Rows[x].Cells[2].Value = dr["PartNumber"];
                        dgridItems.Rows[x].Cells[3].Value = dr["LocationID"];
                        dgridItems.Rows[x].Cells[4].Value = dr["ItemName"];
                        dgridItems.Rows[x].Cells[5].Value = dr["LastCostEx"];
                        dgridItems.Rows[x].Cells[6].Value = dr["AverageCostEx"];
                        dgridItems.Rows[x].Cells[7].Value = dr["TaxRate"];
                        for (int i = 0; i < 13; i++)
                        {
                            dgridItems.Rows[x].Cells["Level" + i].Value = dr["Level" + i];
                        }
                        for (int i = 0; i < 13; i++)
                        {
                            dgridItems.Rows[x].Cells["SalesPrice" + i].Value = dr["SalesPrice" + i];
                        }
                    }
                }
            }
                        FormatGrid();

        }
        private void FormatGrid()
        {
          
            //this.dgridItems.Columns["ItemID"].Visible = false;
            //this.dgridItems.Columns["LocationID"].Visible = false;
            //this.dgridItems.Columns["PartNumber"].ReadOnly = true;
            //this.dgridItems.Columns["ItemName"].ReadOnly = true;
            this.dgridItems.Columns[5].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns[4].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns[6].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level0"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level1"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level2"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level3"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level4"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level5"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level6"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level7"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level8"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level9"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level10"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level11"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["Level12"].DefaultCellStyle.Format = "C2";

            this.dgridItems.Columns["SalesPrice0"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice1"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice2"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice3"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice4"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice5"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice6"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice7"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice8"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice9"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice10"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice11"].DefaultCellStyle.Format = "C2";
            this.dgridItems.Columns["SalesPrice12"].DefaultCellStyle.Format = "C2";
            if (rbRegPrice.Checked)
            {
                for (int i = 0; i < 13; i++)
                {
                    dgridItems.Columns["SalesPrice" + i].Visible = false;
                }
                for (int i = 0; i < 13; i++)
                {
                    dgridItems.Columns["Level"+i].Visible = true;
                }
            }
            else
            {
                for(int i =0; i < 13; i++)
                {
                    dgridItems.Columns["Level" + i].Visible = false;
                }
                for (int i = 0; i <13; i++)
                {
                    dgridItems.Columns["SalesPrice" + i].Visible = true;
                }
            }
            //this.dgridItems.Columns["StartSaleDate"].Visible = false;
            //this.dgridItems.Columns["EndSalesDate"].Visible = false;
            //this.dgridItems.Columns["TaxRate"].Visible = false;


        }
        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            SearchItems();
            if (dgridItems.Rows.Count > 0)
            {
                removeRow();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.STOCKTAKE, "", "", "PartNumber");
            if (Items.ShowDialog() == DialogResult.OK)
            {
                string dr = Items.GetitemIDs;
                if (dr != null)
                {
                    SelItemID = dr;
                    SearchItems();
                    if (dgridItems.Rows.Count > 0)
                    {
                       
                        string prevID = dgridItems.CurrentRow.Cells["ItemID"].Value.ToString();
                        if (SelItemID != prevID)
                        {
                            removeRow();
                        }
                    }
                }
            }
            //SearchItems(WhereCon, this.txtSearch.Text);
        }
        public void removeRow()
        {
            foreach (DataGridViewRow dgvr in dgridItems.Rows)
            {
                if (dgvr.Cells["ItemID"].Value == null)
                {
                    dgridItems.Rows.Remove(dgvr);
                }
            }
        }
        private void btnAll_Click(object sender, EventArgs e)
        {
            string lCol = (rdoAC.Checked == true ? "AverageCostEx" : "LastCostEx");
            
            if (chkMargin.Checked)
            {
                CalculatePercentMargin((float)txtMargin.Value, lCol, 1, this.cboPriceLevel.SelectedItem.ToString());
            }else if (chkMarkup.Checked)
            {
                CalculatePercentMarkup((float)txtMarkup.Value, lCol, 1, this.cboPriceLevel.SelectedItem.ToString());
            }
            else if (chkDiscountFrmBase.Checked)
            {
                CalculateDiscountFromBase((float)DiscountFromBaseAmt.Value, lCol, 1, this.cboPriceLevel.SelectedItem.ToString());
            }
            else if (chkFixedPrice.Checked)
            {
                FixedPrice((float)FixedPriceAmount.Value, lCol, 1, this.cboPriceLevel.SelectedItem.ToString());
            }
            else
            {
                CalculateFixedGP((float)txtFixed.Value, lCol, 1, this.cboPriceLevel.SelectedItem.ToString());

            }
        }

        private void btnSelected_Click(object sender, EventArgs e)
        {
            string lCol = (rdoAC.Checked == true ? "AverageCostEx" : "LastCostEx");

            if (chkMargin.Checked)
            {
                CalculatePercentMargin((float)txtMargin.Value, lCol, 0, this.cboPriceLevel.SelectedItem.ToString());
            }
            else if (chkMarkup.Checked)
            {
                CalculatePercentMarkup((float)txtMarkup.Value, lCol, 0, this.cboPriceLevel.SelectedItem.ToString());
            }
            else if (chkFixed.Checked)
            {
                CalculateFixedGP((float)txtFixed.Value, lCol, 0, this.cboPriceLevel.SelectedItem.ToString());

            }
            else if (chkDiscountFrmBase.Checked)
            {
                CalculateDiscountFromBase((float)DiscountFromBaseAmt.Value, lCol, 0, this.cboPriceLevel.SelectedItem.ToString());
            }
            else if (chkFixedPrice.Checked)
            {
                FixedPrice((float)FixedPriceAmount.Value, lCol, 0, this.cboPriceLevel.SelectedItem.ToString());
            }
        }

        private void FixedPrice(float pRate, string pCol, int pApplyMode, string pLevel)
        {
            string calcMeth = "Fixed Price";
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = (pRate != 0 ? pRate / 100 : 0);
            for (int i = 0; i < this.dgridItems.Rows.Count; i++)
            {
                if (pApplyMode == 1)//All
                {
                    lPrice = pRate;
                    this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                    UpdatePricing(pLevel, float.Parse(this.dgridItems.Rows[i].Cells[pLevel].Value.ToString()), dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);
                }
                else
                {
                    lPrice = pRate;
                    this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                    UpdatePricing(pLevel, float.Parse(this.dgridItems.Rows[i].Cells[pLevel].Value.ToString()), dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);
                }
            }
        }
        private void CalculateDiscountFromBase(float pRate, string pCol, int pApplyMode, string pLevel)
        {
            string calcMeth = "Discount From Base";
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = (pRate != 0 ? pRate / 100 : 0);

            for (int i = 0; i < this.dgridItems.Rows.Count; i++)
            {

                if (pApplyMode == 1)//All
                {
                    lCost = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(), NumberStyles.Currency);
                    //lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                    lPrice = lCost - (lCost * lMRate / 100);

                    //if (CommonClass.IsItemPriceInclusive)
                    //{
                    //    lPrice = lPrice * (1 + (lTaxRate / 100));
                    //}
                                    
                    this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                    UpdatePricing(pLevel, float.Parse(this.dgridItems.Rows[i].Cells[pLevel].Value.ToString()), dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);
                }
                else
                {
                    if ((bool)this.dgridItems.Rows[i].Cells["SelectedItem"].Value)
                    {
                        lCost = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(), NumberStyles.Currency);
                        //lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                        lPrice = lCost - (lCost * lMRate / 100);
                        //if (CommonClass.IsItemPriceInclusive)
                        //{
                        //    lPrice = lPrice * (1 + (lTaxRate / 100));
                        //}
               
                        this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                        UpdatePricing(pLevel, float.Parse(this.dgridItems.Rows[i].Cells[pLevel].Value.ToString()), dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);
                    }
                }
            }
        }
        private void CalculatePercentMargin(float pRate, string pCol, int pApplyMode, string pLevel)
        {
            string calcMeth = "Percent Margin";
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = (pRate != 0 ? pRate / 100 : 0);
            
            for(int i = 0; i < this.dgridItems.Rows.Count; i++)
            {
                
                if (pApplyMode == 1)//All
                {
                    lCost  = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(),NumberStyles.Currency);
                    lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                    lPrice = lCost / (1 - lMRate);
                    
                    if (CommonClass.IsItemPriceInclusive)
                    {
                        lPrice = lPrice * (1 + (lTaxRate / 100));
                    }
                    lPrice = (float)ProcessRounding(lPrice,RoundingMode);
                    this.dgridItems.Rows[i].Cells[pLevel].Value = Math.Round(lPrice, 2);
                    UpdatePricing(pLevel, float.Parse(this.dgridItems.Rows[i].Cells[pLevel].Value.ToString()), dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);
                }
                else//Selected
                {
                    if ((bool)this.dgridItems.Rows[i].Cells["SelectedItem"].Value)
                    {
                        lCost = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(), NumberStyles.Currency);
                        lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                        lPrice = lCost / (1 - lMRate);

                        if (CommonClass.IsItemPriceInclusive)
                        {
                            lPrice = lPrice * (1 + (lTaxRate / 100));
                        }
                        lPrice = (float)ProcessRounding(lPrice, RoundingMode);
                        this.dgridItems.Rows[i].Cells[pLevel].Value = Math.Round(lPrice, 2);
                        UpdatePricing(pLevel, float.Parse(dgridItems.Rows[i].Cells[pLevel].Value.ToString()), dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i , calcMeth, pRate);
                    }
                }
            }

        }
        void UpdatePricing(string pLevel,float PriceValue ,string ItemID, int rowID, string CalcMeth , float percentchange )
        {
            DataRow[] Qr = ItemPriceChanges.Select("colname ='" + pLevel+"'");//New Price
            if (Qr.Length > 0)
            {
                foreach (DataRow r in Qr)
                {
                    ItemPriceChanges.Rows.Remove(r);
                }
            }
            DataRow rw = ItemPriceChanges.NewRow();
            rw["colname"] = pLevel;
            rw["colvalue"] = PriceValue.ToString();
            rw["itemID"] = ItemID;

            rw["CalcMethod"] = CalcMeth;
            rw["PercentChange"] = percentchange;
            ItemPriceChanges.Rows.Add(rw);

            DataRow[] Pr = ItemOldPriceChanges.Select("colname = '"+ pLevel+"'");//OldPrice to datatable
            if (Pr.Length > 0)
            {
                foreach (DataRow r in Pr)
                {
                    ItemOldPriceChanges.Rows.Remove(r);
                }
            }
            DataRow dr = dtPrice.Rows[rowID];
            DataRow Orw = ItemOldPriceChanges.NewRow();
            Orw["colname"] = pLevel;
            Orw["colvalue"] = dr[pLevel].ToString();
            Orw["itemID"] = ItemID;
            ItemOldPriceChanges.Rows.Add(Orw);
        }
        private void CalculatePercentMarkup(float pRate, string pCol, int pApplyMode, string pLevel)
        { string calcMeth = "Percent Markup";
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = (pRate != 0 ? pRate / 100 : 0);

            for (int i = 0; i < this.dgridItems.Rows.Count; i++)
            {

                if (pApplyMode == 1)
                {
                    lCost = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(), NumberStyles.Currency);
                    lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                    lPrice = lCost * (1 + lMRate);

                    if (CommonClass.IsItemPriceInclusive)
                    {
                        lPrice = lPrice * (1 + (lTaxRate / 100));
                    }
                    lPrice = (float)ProcessRounding(lPrice, RoundingMode);
                    this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                    UpdatePricing(pLevel, (float)this.dgridItems.Rows[i].Cells[pLevel].Value, dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i  , calcMeth, pRate);

                }
                else
                {
                    if ((bool)this.dgridItems.Rows[i].Cells["SelectedItem"].Value)
                    {
                        lCost = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(), NumberStyles.Currency);
                        lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                        lPrice = lCost * (1 + lMRate);

                        if (CommonClass.IsItemPriceInclusive)
                        {
                            lPrice = lPrice * (1 + (lTaxRate / 100));
                        }
                        lPrice = (float)ProcessRounding(lPrice, RoundingMode);
                        this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                        UpdatePricing(pLevel, (float)this.dgridItems.Rows[i].Cells[pLevel].Value, dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);
                    }
                }
            }

        }
        private void CalculateFixedGP(float pRate, string pCol, int pApplyMode, string pLevel)
        {
            string calcMeth = "Fixed Gross Profit";
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = pRate;

            for (int i = 0; i < this.dgridItems.Rows.Count; i++)
            {

                if (pApplyMode == 1)
                {
                    lCost = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(), NumberStyles.Currency);
                    lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                    lPrice = lCost + lMRate;

                    if (CommonClass.IsItemPriceInclusive)
                    {
                        lPrice = lPrice * (1 + (lTaxRate / 100));
                    }
                    this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                    UpdatePricing(pLevel, (float)this.dgridItems.Rows[i].Cells[pLevel].Value, dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);

                }
                else
                {
                    if ((bool)this.dgridItems.Rows[i].Cells["SelectedItem"].Value)
                    {
                        lCost = float.Parse(this.dgridItems.Rows[i].Cells[pCol].Value.ToString(), NumberStyles.Currency);
                        lTaxRate = float.Parse(this.dgridItems.Rows[i].Cells["TaxRate"].Value.ToString());
                        lPrice = lCost + lMRate;

                        if (CommonClass.IsItemPriceInclusive)
                        {
                            lPrice = lPrice * (1 + (lTaxRate / 100));
                        }
                        this.dgridItems.Rows[i].Cells[pLevel].Value = lPrice;
                        UpdatePricing(pLevel, (float)this.dgridItems.Rows[i].Cells[pLevel].Value, dgridItems.Rows[i].Cells["ItemID"].Value.ToString(), i, calcMeth, pRate);
                    }
                }
            }

        }

        private double ProcessRounding(float lPrice, int pRoundMode)
        {
            double lPriceRounded = lPrice;
            double lPriceFloor = 0;
            double lTemp = 0;
            double lTemp1 = 0;
            double lTemp2 = 0;


            if (pRoundMode == 1) //ROUND TO NEAREST five cents
            {
                lPriceFloor = Math.Floor(lPrice);
                lTemp = (lPrice - lPriceFloor) * 10;
                lTemp1 = Math.Floor(lTemp);
                lTemp2 = Math.Floor((lTemp - lTemp1) *10)/100;
                lTemp1 = lTemp1 / 10;

                if (lTemp2 > 0 && lTemp2 < 0.03)
                {
                    lTemp2 = 0;
                }
                if (lTemp2 >= 0.03 && lTemp2 < 0.08)
                {
                    lTemp2 = 0.05;
                }
                if (lTemp2 >= 0.08 && lTemp2 <= 0.099)
                {
                    lTemp2 = 0.1;
                }
                lPriceRounded = lPriceFloor + lTemp1 + lTemp2;

            }
            if (pRoundMode == 2) //ALWAYS ENDS WITH .99
            {
                lTemp = Math.Floor(lPrice);
                lPriceRounded = lTemp + 0.99;
            }
            return lPriceRounded;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            SavePrices(0);
        }

       void  SavePrices(int pApplyMode)
        {
                string sql = "";
                string lItemID = "";
                float lPrice = 0;
                float slPrice = 0;
                for (int i = 0; i<this.dgridItems.Rows.Count; i++)
                {
                    if (pApplyMode == 1)
                    {
                        lItemID = this.dgridItems.Rows[i].Cells["ItemID"].Value.ToString();
                        for(int j = 0; j <= 12; j++)
                        {
                            lPrice = float.Parse(this.dgridItems.Rows[i].Cells["Level"+j.ToString()].Value.ToString(), NumberStyles.Currency);
                            slPrice = float.Parse(this.dgridItems.Rows[i].Cells["SalesPrice" + j.ToString()].Value.ToString(), NumberStyles.Currency);
                            sql = "UPDATE ItemsSellingPrice set Level" + j.ToString() + " = " + lPrice.ToString() + ", SalesPrice" + j.ToString() + " = " + slPrice.ToString() + "  where ItemID = " + lItemID;
                            CommonClass.runSql(sql);
                        }
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Prices for Item with ID " + lItemID, lItemID);

                    }
                    else
                    {
                        if ((bool)this.dgridItems.Rows[i].Cells["SelectedItem"].Value)
                        {
                            lItemID = this.dgridItems.Rows[i].Cells["ItemID"].Value.ToString();
                            for (int j = 0; j <= 12; j++)
                            {
                                lPrice = float.Parse(this.dgridItems.Rows[i].Cells["Level" + j.ToString()].Value.ToString(), NumberStyles.Currency);
                                slPrice = float.Parse(this.dgridItems.Rows[i].Cells["SalesPrice" + j.ToString()].Value.ToString(), NumberStyles.Currency);
                                sql = "UPDATE ItemsSellingPrice set Level" + j.ToString() + " = " + lPrice.ToString() + ", SalesPrice" + j.ToString() + " = " + slPrice.ToString() + "  where ItemID = " + lItemID;
                                CommonClass.runSql(sql);
                            }
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Prices for Item with ID " + lItemID, lItemID);
                        }
                    }
               
                }
            SaveChanges();
            MessageBox.Show("Item Prices saved successfully.");
        }
        void SaveChanges()
        {
            int x = 0;
            if (ItemPriceChanges.Rows.Count > 0)
            {
                foreach (DataRow rw in ItemPriceChanges.Rows)
                {
                    Dictionary<string, object> priceupdateParam = new Dictionary<string, object>();
                    priceupdateParam.Add("@PriceLevel", rw["colname"].ToString());
                    priceupdateParam.Add("@PriceAfter", rw["colvalue"].ToString());

                    priceupdateParam.Add("@ItemID", rw["itemID"].ToString());
                    priceupdateParam.Add("@ChangeDate", DateTime.Now.ToUniversalTime());
                    priceupdateParam.Add("@UserID", CommonClass.UserID);
                    priceupdateParam.Add("@CalcMethod", rw["CalcMethod"].ToString());
                    priceupdateParam.Add("@PercentChange", rw["PercentChange"].ToString());
                    foreach (DataRow dx in ItemOldPriceChanges.Rows)
                    {
                        if (dx["colname"].ToString() == rw["colname"].ToString())
                        {
                            priceupdateParam.Add("@PriceBefore", dx["colvalue"].ToString());
                        }
                    }

                    string priceupdate = "INSERT INTO PriceChange (ItemID,PriceBefore,PriceAfter,ChangeDate,UserID,PriceLevel,CalcMethod,PercentChange)VALUES(@ItemID,@PriceBefore,@PriceAfter,@ChangeDate,@UserID,@PriceLevel,@CalcMethod,@PercentChange)";
                     x =  CommonClass.runSql(priceupdate, CommonClass.RunSqlInsertMode.QUERY, priceupdateParam);

                }
            }

            if (x > 0)
            {
                ItemPriceChanges.Rows.Clear();
                ItemOldPriceChanges.Rows.Clear();
            }
        }
        private void btnRecordALL_Click(object sender, EventArgs e)
        {
            SavePrices(1);
        }

        private void rbSalePrice_CheckedChanged(object sender, EventArgs e)
        {
            string[] priceLevel;
            if (rbSalePrice.Checked)
            {
                gbSaleDate.Visible = true;
                rbRegPrice.Checked = false;
                if(dgridItems.Rows.Count > 0)
                {
                    FormatGrid();
                }
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
                gbSaleDate.Visible = false;
                rbRegPrice.Checked = true;
                if (dgridItems.Rows.Count > 0)
                {
                    FormatGrid();
                }
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
            BindingList<string> PriceLevel = new BindingList<string>(priceLevel);
            cboPriceLevel.DataSource = PriceLevel;
        }

        private void chkFixedPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFixedPrice.Checked)
            {
                this.FixedPriceAmount.Enabled = true;
                this.chkMarkup.Checked = false;
                this.chkFixed.Checked = false;
                this.chkDiscountFrmBase.Checked = false;
            }
            else
            {
                this.FixedPriceAmount.Enabled = false;
            }
        }

        private void chkDiscountFrmBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscountFrmBase.Checked)
            {
                this.chkMargin.Checked = false;
                this.chkMarkup.Checked = false;
                this.chkFixed.Checked = false;
                this.chkFixedPrice.Checked = false;
                this.DiscountFromBaseAmt.Enabled = true;
            }
            else
            {
                this.DiscountFromBaseAmt.Enabled = false;
            }
        }
    }
}
