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

    public partial class RedeemItem : Form
    {
        private string pItemNum;
        private string PriceLevel = "Level0";
        private static bool IsLoading = false;
        private static bool IsMain = false;
        private static string WhereCon = "";
        private static DataGridViewRow ItemRow;

        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";

        class ItemForRedeem
        {
            private int mId;
            private double mRequiredPoints;
            private string mPromoType;
            private double mGCAmount;
            private string mGCNumber;
            private DateTime mStartDate;
            private DateTime mEndDate;
            private bool mIsGCUsed;

            private int promoID;
            private int mGCID;

            public int ID
            {
                get { return mId; }
                set { mId = value; }
            }

            public double RequiredPoints
            {
                get { return mRequiredPoints; }
                set { mRequiredPoints = value; }
            }

            public string PromoType
            {
                get { return mPromoType; }
                set { mPromoType = value; }
            }

            public double GCAmount
            {
                get { return mGCAmount; }
                set { mGCAmount = value; }
            }

            public string GCNumber
            {
                get { return mGCNumber; }
                set { mGCNumber = value; }
            }

            public DateTime StartDate
            {
                get { return mStartDate; }
                set { mStartDate = value; }
            }

            public DateTime EndDate
            {
                get { return mEndDate; }
                set { mEndDate = value; }
            }

            public bool GCUsed
            {
                get { return mIsGCUsed; }
                set { mIsGCUsed = value; }
            }
            public int GCID
            {
                get { return mGCID; }
                set { mGCID = value; }
            }
            public int PromoID
            {
                get { return promoID; }
                set { promoID = value; }
            }
        }

        List<ItemForRedeem> mItemsForRedeem = new List<ItemForRedeem>();

        public RedeemItem(string ItemNum = "", string pCustomerID = "", bool isMain = false)
        {
            InitializeComponent();
            pItemNum = ItemNum;
            IsMain = isMain;


            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Add", out outx);
                if (outx == true)
                {
                    CanAdd = true;
                }
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                if (outx == true)
                {
                    CanDelete = true;
                }
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

            WhereCon = "PartNumber";
            if (pCustomerID != "")
            {
                PriceLevel = GetCustomerPriceLevel(pCustomerID);
            }
        }

        public DataGridViewRow GetSelectedItem
        {
            get { return ItemRow; }
        }


        private void ItemLookup_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dgridItems.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.rdoPartNumber.Checked = true;
            WhereCon = "PartNumber";
            txtSearch.Text = pItemNum;
            SearchItems(WhereCon, txtSearch.Text, ref dgridItems);
            SearchItems(WhereCon, txtSearch.Text, ref dgridRedeemItems, true, true);

            if (txtSearch.Text == pItemNum
                && pItemNum != "")
            {
                btnSearch.PerformClick();
            }

            cmbRedeemType.SelectedIndex = 0;
            txtPointsReq.Text = "0";          
            btnAdd.Enabled = CanAdd;
            btnUpdate.Enabled = CanEdit;
            btnDelete.Enabled = CanDelete;
           // btnSave.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchItems(WhereCon, txtSearch.Text, ref dgridItems, false);
            SearchItems(WhereCon, txtSearch.Text, ref dgridRedeemItems, false, true);
            btnAdd.Enabled = CanAdd;
            btnUpdate.Enabled = CanEdit;
            btnDelete.Enabled = CanDelete;
           // btnSave.Enabled = false;
        }

        private void SearchItems(string pWhereCol, string pSearchText, ref DataGridView pDgrid, bool isLoad = true, bool isForRedeem = false)
        {
            IsLoading = true;
            string strCon = "";
            string strMain = "";
            string strPurchaseCon = " i.IsSold = 1 AND ";
            switch (pWhereCol)
            {
                case "PartNumber":
                    strCon = " WHERE " + strPurchaseCon + " i.PartNumber LIKE '%" + pSearchText + "%'";
                    break;
                case "ItemNumber":
                    strCon = " WHERE " + strPurchaseCon + " i.ItemNumber LIKE '%" + pSearchText + "%'";
                    break;
                case "SupplierPartNumber":
                    strCon = " WHERE " + strPurchaseCon + " i.SupplierPartNumber LIKE '%" + pSearchText + "%'";
                    break;
                case "ItemName":
                    strCon = " WHERE " + strPurchaseCon + " i.ItemName LIKE '%" + pSearchText + "%'";
                    break;
                case "ItemDescription":
                    strCon = " WHERE " + strPurchaseCon + " i.ItemDescription LIKE '%" + pSearchText + "%'";
                    break;
                case "Supplier":
                    strCon = " WHERE " + strPurchaseCon + " p.Name LIKE '%" + pSearchText + "%'";
                    break;
                default:
                    strCon = " WHERE " + strPurchaseCon + " i.ID = 0";
                    break;
            }

            strCon += " AND i.IsInactive = 0 ";

            if (IsMain)
            {
                strMain = " AND i.isMain = 1" ;
            }
            string selectSql = "";
            string strPromoJoin = "";
            string strPromoFields = "";
            if (isForRedeem)
            {
                selectSql = " SELECT pi.ItemID, i.ItemName, pi.id as PromoID, ISNULL(pi.requiredpoints,0) as requiredpoints, pi.promotype, gc.GCAmount, gc.GCNumber, gc.StartDate, gc.EndDate, gc.IsUsed , gc.ID as GCID ";
                selectSql += @"FROM PromotionItems pi left join Items i on pi.ItemID = i.ID  LEFT JOIN GiftCertificate gc ON pi.ID = gc.promoid WHERE gc.issuedSalesID is null";

            }
            else
            {
                selectSql = @"SELECT DISTINCT i.ID, 
                                                 i.PartNumber, 
                                                 i.ItemNumber, 
                                                 i.ItemName, 
                                                 q.OnHandQty, 
                                                 c.LastCostEx,
                                                 c.StandardCostEx, 
                                                 c.AverageCostEx, 
                                                 s." + PriceLevel
                                                + @", 
                                                 i.SalesTaxCode,
                                                 t.TaxCollectedAccountID, 
                                                 t.TaxPercentageRate AS RateTaxSales,
                                                 i.PurchaseTaxCode, 
                                                 tp.TaxPaidAccountID,  
                                                 tp.TaxPercentageRate AS RateTaxPurchase,
                                                 i.IsCounted, 
                                                 i.AssetAccountID, 
                                                 i.IsBought, 
                                                 i.COSAccountID, 
                                                 i.CategoryID 
                                FROM 
                                ((((( Items i INNER JOIN ItemsSellingPrice s ON i.ID = s.ItemID )
                                INNER JOIN ItemsCostPrice c ON i.ID = c.ItemID ) 
                                INNER JOIN ItemsQty q ON i.ID = q.ItemID )
                                LEFT JOIN Profile p ON i.SupplierID = p.ID )
                                LEFT JOIN TaxCodes t ON i.SalesTaxCode = t.taxcode )
                                LEFT JOIN TaxCodes tp ON i.PurchaseTaxCode = tp.taxcode "
                               + strCon
                               + strMain;
            }
            

            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, selectSql);
            string[] RowArray;
            if (isForRedeem)
            {
                pDgrid.Rows.Clear();
                pDgrid.Columns.Clear();
                RowArray = new string[9];
                foreach (DataColumn col in dt.Columns)
                {
                    pDgrid.Columns.Add(col.ColumnName, col.Caption);
                }

                foreach (DataRow row in dt.Rows)
                {
                    int index = pDgrid.Rows.Add();
                    foreach (DataColumn col in dt.Columns)
                    {
                        pDgrid[col.ColumnName, index].Value = row[col];
                    }

                    //ItemForRedeem lItemRedeem = new ItemForRedeem();
                    
                    //RowArray[0] = row["ItemID"].ToString();
                    //RowArray[1] = row["PromoID"].ToString();
                    //RowArray[2] = row["requiredpoints"].ToString();
                    //RowArray[3] = row["promotype"].ToString();
                    //if (lItemRedeem.PromoType == "Gift Certificate"
                    //    && row["GCNumber"] != System.DBNull.Value)
                    //{
                    //    RowArray[4] = row["GCAmount"].ToString();
                    //    RowArray[5] = row["GCNumber"].ToString();
                    //    RowArray[6] = Convert.ToDateTime(row["StartDate"]).ToString();
                    //    RowArray[7] = Convert.ToDateTime(row["EndDate"]).ToString();
                    //    RowArray[8] = row["IsUsed"].ToString();
                    //    RowArray[9] =row["GCID"].ToString();
                    //}
                    //mItemsForRedeem.Add(lItemRedeem);
                }
            }
            else
            {
                pDgrid.DataSource = dt;
            }

            if (isLoad 
                && pItemNum != "" 
                && dt.Rows.Count == 1)
            {
                pDgrid.CurrentRow.Selected = true;
                FormatGrid(ref pDgrid, isForRedeem);
                ItemRow = pDgrid.CurrentRow;
                DialogResult = DialogResult.OK;
                Hide();
            }
            else
            {
                FormatGrid(ref pDgrid, isForRedeem);
            }

            IsLoading = false;
        }

        private void FormatGrid(ref DataGridView dgv, bool isForRedeem = false)
        {
           
            int hidecolstart = 9;
            if (isForRedeem)
            {
                //dgv.Columns[0].Visible = false;
               // dgv.Columns[1].Visible = false;
                dgv.Columns[6].Visible = false;
                dgv.Columns[7].Visible = false;
                dgv.Columns[8].Visible = false;
                dgv.Columns[9].Visible = false;
                hidecolstart = 11;
            }
            else
            {
                dgv.Columns[0].HeaderText = "Item ID";
                dgv.Columns[1].HeaderText = "Part No";
                dgv.Columns[2].HeaderText = "Item No";
                dgv.Columns[3].HeaderText = "Name";
                dgv.Columns[4].HeaderText = "On Hand";
                dgv.Columns[5].HeaderText = "Last Cost";
                dgv.Columns[6].HeaderText = "Standard Cost";
                dgv.Columns[7].HeaderText = "Average Cost";
                dgv.Columns[8].HeaderText = "Selling Price";
                dgv.Columns[5].DefaultCellStyle.Format = "C2";
                dgv.Columns[6].DefaultCellStyle.Format = "C2";
                dgv.Columns[7].DefaultCellStyle.Format = "C2";
                dgv.Columns[8].DefaultCellStyle.Format = "C2";

            }

            for (int i = hidecolstart; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].Visible = false;
            }
        }

        private void rdoPartNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPartNumber.Checked)
            {
                WhereCon = "PartNumber";
            }
        }

        private void rdoItemNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoItemNumber.Checked)
            {
                WhereCon = "PartNumber";
            }
        }

        private void rdoSupplierItemNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSupplierItemNumber.Checked)
            {
                WhereCon = "ItemNumber";
            }
        }

        private void rdoItemName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoItemName.Checked)
            {
                WhereCon = "ItemName";
            }
        }

        private void rdoItemDescription_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoItemDescription.Checked)
            {
                WhereCon = "ItemDescription";
            }
        }

        private void rdoSupplierName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSupplierName.Checked)
            {
                WhereCon = "Supplier";
            }
        }

        private void dgridItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemRow = this.dgridItems.CurrentRow;
            this.DialogResult = DialogResult.OK;
        }

        public static string GetCustomerPriceLevel(string pCustomerID)
        {
            string retstr = "Level0";
            
            string sql = @"SELECT ItemPriceLevel FROM Profile WHERE ID = " + pCustomerID;
               
            DataTable ltb = new DataTable();
            CommonClass.runSql(ref ltb, sql);
               
            if (ltb.Rows.Count > 0)
            {
                retstr = "Level" + ltb.Rows[0]["ItemPriceLevel"].ToString();
            }

            return retstr;          
        } //END

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private string generateGCNumber()
        {
            int lExistID = 0;
            string lNewRndNo = CommonClass.StringMixer(10);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@GCNumber", lNewRndNo);
            lExistID = CommonClass.runSql("SELECT ID FROM GiftCertificate WHERE GCNumber=@GCNumber", CommonClass.RunSqlInsertMode.SCALAR, param);
            if (lExistID > 0)
            {
                return generateGCNumber();
            }
            else
            {
                return lNewRndNo;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPointsReq.Text == "" || Convert.ToDouble(txtPointsReq.Text) <= 0)
            {
                MessageBox.Show("Points required must be a valid number greater than 0");
                return;
            }
            string GCnum = "";
            string[] RowArray;
            DataGridViewRow dgvRow = dgridItems.SelectedRows.Count > 0 ? dgridItems.SelectedRows[0] : null;
            if (dgvRow != null)
            {
                RowArray = new string[9];// ItemForRedeem lItemRedeem = new ItemForRedeem();
                RowArray[0] = dgvRow.Cells["ID"].Value.ToString();
                RowArray[2] = Convert.ToDouble(txtPointsReq.Text).ToString();
                RowArray[3] = cmbRedeemType.Text;
                if (cmbRedeemType.Text == "Gift Certificate")
                {
                    RowArray[4] = txtGCAmount.Text; GCnum = generateGCNumber();
                    RowArray[5] = GCnum;
                    RowArray[6] = dtpStartDate.Value.ToString();
                    RowArray[7] = dtpEndDate.Value.ToString();
                   // RowArray[9] = dtpEndDate.Value.ToString();
                }

                //mItemsForRedeem.Add(RowArray);

                int index = dgridRedeemItems.Rows.Add(RowArray);

                dgridRedeemItems.Rows[index].Cells["requiredpoints"].Value = Convert.ToDouble(txtPointsReq.Text).ToString();
                dgridRedeemItems.Rows[index].Cells["promotype"].Value = cmbRedeemType.Text;
                dgridRedeemItems.Rows[index].Cells["StartDate"].Value = dtpStartDate.Value.ToString();
                dgridRedeemItems.Rows[index].Cells["EndDate"].Value = dtpEndDate.Value.ToString();
                //if (cmbRedeemType.Text == "Gift Certificate")
                //{
                //    dgridRedeemItems.Columns.Add("GCAmount", "GC Amount");
                //    dgridRedeemItems.Rows[index].Cells["GCAmount"].Value = lItemRedeem.GCAmount;
                //}
                dgridRedeemItems.Refresh();
               // btnSave.Enabled = true;
            }
            string sql = @"INSERT INTO PromotionItems(itemid, requiredpoints, promotype) 
                                            VALUES(@itemid, @requiredpoints, @promotype)";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@itemid", dgvRow.Cells["ID"].Value);
            param.Add("@requiredpoints", txtPointsReq.Text);
            param.Add("@promotype", cmbRedeemType.Text);

            int rowsaffected = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);

            if (cmbRedeemType.Text == "Gift Certificate")
            {
                sql = @"INSERT INTO GiftCertificate (ItemID, GCAmount, GCNumber, StartDate, EndDate, IsUsed,promoid)
                            VALUES (@ItemID, @GCAmount, @GCNumber, @StartDate, @EndDate, @IsUsed,@promoid)";

                Dictionary<string, object> paramgc = new Dictionary<string, object>();
                paramgc.Add("@ItemID", dgvRow.Cells["ID"].Value);
                paramgc.Add("@GCAmount", txtGCAmount.Text);
                paramgc.Add("@GCNumber", GCnum);
                paramgc.Add("@StartDate", dtpStartDate.Value);
                paramgc.Add("@EndDate", dtpEndDate.Value);
                paramgc.Add("@IsUsed", "0") ;
                paramgc.Add("@promoid", rowsaffected);

                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramgc);
            }
            if (rowsaffected > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added Promotion Item for Promo ID " + rowsaffected.ToString(), rowsaffected.ToString());
                MessageBox.Show("Promotion Item Added");
                btnSearch.PerformClick();
            }
        }

        public DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            }
            return clonedRow;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPointsReq.Clear();
            txtGCAmount.Clear();
            btnSearch.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvRow = dgridRedeemItems.SelectedRows.Count > 0 ? dgridRedeemItems.SelectedRows[0] : null;

            string sql = @"Delete PromotionItems WHERE id = " + dgridRedeemItems.CurrentRow.Cells["PromoID"].Value.ToString();

          
            int rowsaffected = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY);

            if (cmbRedeemType.Text == "Gift Certificate")
            {
                sql = @"Delete GiftCertificate WHERE ID = @GCID";

                Dictionary<string, object> paramgc = new Dictionary<string, object>();
                paramgc.Add("@GCID", dgvRow.Cells["GCID"].Value);

                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramgc);
            }
            if (rowsaffected > 0)
            {
                MessageBox.Show("Promotion has been deleted");
                btnSearch.PerformClick();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string sql = @"DELETE FROM PromotionItems;
            //               DELETE FROM GiftCertificate";
            //CommonClass.runSql(sql);

            //int rowsaffected = 0;

            //foreach (ItemForRedeem itemunit in mItemsForRedeem)
            //{
            //    sql = @"INSERT INTO PromotionItems(itemid, requiredpoints, promotype) 
            //                                VALUES(@itemid, @requiredpoints, @promotype)";

            //    Dictionary<string, object> param = new Dictionary<string, object>();
            //    param.Add("@itemid", itemunit.ID);
            //    param.Add("@requiredpoints", itemunit.RequiredPoints);
            //    param.Add("@promotype", itemunit.PromoType);

            //    rowsaffected = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);

            //    if (itemunit.PromoType == "Gift Certificate")
            //    {
            //        sql = @"INSERT INTO GiftCertificate (ItemID, GCAmount, GCNumber, StartDate, EndDate, IsUsed,promoid)
            //                VALUES (@ItemID, @GCAmount, @GCNumber, @StartDate, @EndDate, @IsUsed,@promoid)";

            //        Dictionary<string, object> paramgc = new Dictionary<string, object>();
            //        paramgc.Add("@ItemID", itemunit.ID);
            //        paramgc.Add("@GCAmount", itemunit.GCAmount);
            //        paramgc.Add("@GCNumber", itemunit.GCNumber);
            //        paramgc.Add("@StartDate", itemunit.StartDate);
            //        paramgc.Add("@EndDate", itemunit.EndDate);
            //        paramgc.Add("@IsUsed", itemunit.GCUsed);
            //        paramgc.Add("@promoid", rowsaffected);

            //        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramgc);
            //    }
            //}

            //if (rowsaffected > 0)
            //{
            //    MessageBox.Show("Promotion Items updated");
            //}
            //btnSave.Enabled = false;
        }

        private void cmbRedeemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRedeemType.Text == "Gift Certificate")
            {
                label6.Enabled = true;
                txtGCAmount.Enabled = true;
                gpbValidity.Enabled = true;
            }
            else
            {
                label6.Enabled = false;
                txtGCAmount.Enabled = false;
                gpbValidity.Enabled = false;
            }
             txtPointsReq.Clear();
            txtGCAmount.Clear();
            dtpStartDate.Value =DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
        }

        private void dgridRedeemItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgridRedeemItems.Rows[e.RowIndex].Cells["promotype"].Value.ToString() == "Gift Certificate")
                {

                    txtPointsReq.Text = dgridRedeemItems.Rows[e.RowIndex].Cells["requiredpoints"].Value.ToString();
                    txtGCAmount.Text = dgridRedeemItems.Rows[e.RowIndex].Cells["GCAmount"].Value.ToString();
                    cmbRedeemType.Text = dgridRedeemItems.Rows[e.RowIndex].Cells["promotype"].Value.ToString();
                    dtpStartDate.Value = Convert.ToDateTime(dgridRedeemItems.Rows[e.RowIndex].Cells["StartDate"].Value.ToString());
                    dtpEndDate.Value = Convert.ToDateTime(dgridRedeemItems.Rows[e.RowIndex].Cells["EndDate"].Value.ToString());
                }
                else if (dgridRedeemItems.Rows[e.RowIndex].Cells["promotype"].Value.ToString() == "Item")
                {
                    txtPointsReq.Text = dgridRedeemItems.Rows[e.RowIndex].Cells["requiredpoints"].Value.ToString();
                    cmbRedeemType.Text = dgridRedeemItems.Rows[e.RowIndex].Cells["promotype"].Value.ToString();
                    txtGCAmount.Clear();
                    dtpStartDate.Value = DateTime.Now;
                    dtpEndDate.Value = DateTime.Now;
                }
                btnUpdate.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvRow = dgridRedeemItems.SelectedRows.Count > 0 ? dgridRedeemItems.SelectedRows[0] : null;
          
                //if (dgvRow != null)
                //{
                //    for (int indx = 0; indx < mItemsForRedeem.Count; ++indx)
                //    {
                //        if (mItemsForRedeem[indx].ID == Convert.ToInt32(dgvRow.Cells["ItemID"].Value))
                //        {
                //            mItemsForRedeem[indx].RequiredPoints = Convert.ToDouble(txtPointsReq.Text);
                //            if (cmbRedeemType.Text == "Gift Certificate")
                //            {
                //                mItemsForRedeem[indx].GCAmount = Convert.ToDouble(txtGCAmount.Text);
                //                mItemsForRedeem[indx].StartDate = dtpStartDate.Value;
                //                mItemsForRedeem[indx].EndDate = dtpEndDate.Value;
                //            }
                //            break;
                //        }
                //    }

                //    dgridRedeemItems.Refresh();
                //}

                dgridRedeemItems.CurrentRow.Cells["requiredpoints"].Value = txtPointsReq.Text;
                dgridRedeemItems.CurrentRow.Cells["promotype"].Value = cmbRedeemType.Text;
               // btnUpdate.Enabled = false;
           // btnSave.Enabled = true;

            string sql = @"Update PromotionItems set itemid = @itemid, requiredpoints =  @requiredpoints, promotype = @promotype WHERE id = "+ dgridRedeemItems.CurrentRow.Cells["PromoID"].Value.ToString() ;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@itemid", dgvRow.Cells["ItemID"].Value);
            param.Add("@requiredpoints", txtPointsReq.Text);
            param.Add("@promotype", cmbRedeemType.Text);

            int rowsaffected = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

            if (cmbRedeemType.Text == "Gift Certificate")
            {
                sql = @"Update GiftCertificate set ItemID = @ItemID, GCAmount = @GCAmount,StartDate = @StartDate, EndDate = @EndDate WHERE ID = @GCID";

                Dictionary<string, object> paramgc = new Dictionary<string, object>();
                paramgc.Add("@ItemID", dgvRow.Cells["ItemID"].Value);
                paramgc.Add("@GCAmount", txtGCAmount.Text);
                paramgc.Add("@StartDate", dtpStartDate.Value);
                paramgc.Add("@EndDate", dtpEndDate.Value);
                paramgc.Add("@GCID", dgvRow.Cells["GCID"].Value);

                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramgc);
            }
            if (rowsaffected > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Promotion Item for Promo ID " + rowsaffected.ToString(), rowsaffected.ToString());
                MessageBox.Show("Promotion Item Updated");
                btnSearch.PerformClick();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
