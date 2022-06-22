using RestaurantPOS.Inventory;
using RestaurantPOS.Setup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS
{
    public partial class PointsAccumulation : Form
    {
        SqlCommand cmd;
        List<RuleCriteriaPoints> RuleCriteria = new List<RuleCriteriaPoints>();
        DataTable dt = new DataTable();
        DataTable Duplicatedt = new DataTable();
        DataTable FreeTable = null;
        private List<RuleCriteriaPoints> itemPromos;
        int PromoID;
        string pID = "";
        string ItemIDs;
        string itemList;
        string supplierList;
        string selectedNode;
        string[] nodes;
        int count = 0;
        private float mPriceEx = 0;

        bool isContractPrice = false;
        private float ContractPrice = 0;
        Dictionary<string, string> mCustomerIDs = new Dictionary<string, string>();
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";

        public PointsAccumulation()
        {
            InitializeComponent();
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
        }

        private void PointsAccumulation_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dgAccumulationPoints.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            LoadCategory();
            LoadBrands();
            LoadPromos();
            LoadSupplier();
            formCheck();
            InitFreeTable();
        }
        private void formCheck()
        {
            txtPoints.Value = 0;
            txtPromoCode.Text = "";
            cmbRewardName.Text = "";

            cmbRewardName.Enabled = false;
            cbPromoType.Enabled = false;
            txtPromoCode.Enabled = false;
            txtPoints.Enabled = false;
            sdate.Enabled = false;
            edate.Enabled = false;

            chkLoyaltyOnly.Checked = false;
            cmbCustomerList.DataSource = null;
            cmbCustomerList.Items.Clear();

            if (dgAccumulationPoints.Rows.Count == 0)
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                
            }
            else
            {
                btnDelete.Enabled = CanDelete;
                btnEdit.Enabled = CanEdit;
               


            }
            dgAccumulationPoints.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnAddNew.Enabled = CanAdd;
            dgAccumulationPoints.ClearSelection();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            cbPromoType.Enabled = true;
            cmbRewardName.Enabled = true;
            txtPromoCode.Enabled = true;
            txtPoints.Enabled = true;
            sdate.Enabled = true;
            edate.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnAddNew.Enabled = false;
            dgAccumulationPoints.Enabled = false;
            //Clear Brand
            //Clear Supplier
            LoadCategory();
            LoadBrands();
            LoadSupplier();
            ItemIDs = "";
            LoadInventoryItems();
            //Clear Inventory Items
            //dgInventoryItems.Rows.Clear();
            //dgInventoryItems.Rows.Add();

            pID = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPromos();
            formCheck();
            
        }

        private void savePromo()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string pCode = txtPromoCode.Text;
            DateTime EndDate = edate.Value.ToUniversalTime();
            EndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, 23, 59, 59);
            DateTime StartDate = sdate.Value.ToUniversalTime();
            StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 00, 00, 00);

            string savesql = @"INSERT INTO Promos ( PromoCode,
                                                    isActive,
                                                    PointsValue,
                                                    PointAccumulationCriteria,
                                                    StartDate,
                                                    EndDate,
                                                    RuleCriteria,
                                                    RuleCriteriaID,
                                                    PromotionType ) 
                                            VALUES ( @PromoCode,
                                                    @isActive,
                                                    @PointsValue,
                                                    @PointAccumulationCriteria,
                                                    @StartDate,
                                                    @EndDate,
                                                    @RuleCriteria,
                                                    @RuleCriteriaID,
                                                    @PromotionType )";
            param.Add("@PromoCode", pCode);
            param.Add("@isActive", IsActive.Checked);
            param.Add("@PointsValue", txtPoints.Value);
            param.Add("@PointAccumulationCriteria", cmbRewardName.Text);
            param.Add("@StartDate", StartDate);
            param.Add("@EndDate", EndDate);
            param.Add("@RuleCriteria", memberCriteria());
            param.Add("@RuleCriteriaID", toJson());
            param.Add("@PromotionType", cbPromoType.Text);
            PromoID = CommonClass.runSql(savesql, CommonClass.RunSqlInsertMode.SCALAR, param);
            if (cmbRewardName.Text == "Free Product")
            {
                SaveFreeProductItem(PromoID);
            }
            if (PromoID > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added Promotions/Points Setup with Promo ID " + PromoID.ToString(), PromoID.ToString());

                string titles = "Information";
                DialogResult createNew = MessageBox.Show("Promo Record has been created. Would you like to enter a new promo?", titles, MessageBoxButtons.YesNo);
                if (createNew == DialogResult.Yes)
                {   //clear for new datas
                    btnRefresh.PerformClick();
                }
                else if (createNew == DialogResult.No)
                {
                    CommonClass.PointAccumulation.Close();
                }
            }
        }
        private void SaveFreeProductItem(int PromoID)
        {

            for (int i = 0; i < FreeTable.Rows.Count; i++)
            {
                string freeItemSql = @"INSERT INTO PromotionItems (itemid, quantity, promoid, promotype )VALUES(@ItemID, @Quantity, @PromoID, @promotype)";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ItemID", FreeTable.Rows[i]["ItemID"].ToString());
                param.Add("@quantity", FreeTable.Rows[i]["Quantity"].ToString());
                param.Add("@PromoID", PromoID);
                param.Add("@promotype", "Free Item");
                CommonClass.runSql(freeItemSql, CommonClass.RunSqlInsertMode.QUERY, param);

            }
        }
        private void DeleteProductItem(int PromoID)
        {
            for (int i = 0; i < FreeTable.Rows.Count; i++)
            {
                string deletefreeItem = @"DELETE PromotionItems WHERE promoid = " + PromoID;
                CommonClass.runSql(deletefreeItem);
            }
        }

        public string toJson()
        {
            foreach (DataGridViewRow item in dgInventoryItems.Rows)//items
            {
                if (item.Cells[1].Value != null)
                {
                    RuleCriteriaPoints newRuleCriteria = new RuleCriteriaPoints();
                    newRuleCriteria.CriteriaName = "Item";
                    newRuleCriteria.CriteriaValue = item.Cells[0].Value.ToString();
                    RuleCriteria.Add(newRuleCriteria);
                }
            }

            foreach (DataGridViewRow supplier in dgSupplier.Rows)//iSupplier
            {
                if (bool.Parse(supplier.Cells[1].Value.ToString()))
                {
                    RuleCriteriaPoints newRuleCriteria = new RuleCriteriaPoints();
                    newRuleCriteria.CriteriaName = "Supplier";
                    newRuleCriteria.CriteriaValue = supplier.Cells[0].Value.ToString();
                    RuleCriteria.Add(newRuleCriteria);
                }
            }

            foreach (DataGridViewRow brand in dgBrand.Rows)//Brand
            {
                if (bool.Parse(brand.Cells[0].Value.ToString()))
                {
                    RuleCriteriaPoints newRuleCriteria = new RuleCriteriaPoints();
                    newRuleCriteria.CriteriaName = "Brand";
                    newRuleCriteria.CriteriaValue = brand.Cells[1].Value.ToString();
                    RuleCriteria.Add(newRuleCriteria);
                }
            }

            foreach (TreeNode mainNodes in treeCategory.Nodes)//Category
            {
                if (mainNodes.Checked)// mainNodes.Nodes.Count != 0)
                {
                    RuleCriteriaPoints category = new RuleCriteriaPoints();
                    category.CriteriaName = "Category";
                    category.CriteriaValue = mainNodes.Tag.ToString();
                    RuleCriteria.Add(category);
                }
                foreach (TreeNode subnodes in mainNodes.Nodes)
                {
                    if (subnodes.Checked)
                    {
                        RuleCriteriaPoints category = new RuleCriteriaPoints();
                        category.CriteriaName = "Category";
                        category.CriteriaValue = subnodes.Tag.ToString();
                        RuleCriteria.Add(category);
                    }
                }
            }

            string JSONString = JsonConvert.SerializeObject(RuleCriteria, Formatting.Indented);

            return JSONString;
        }

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                mCustomerIDs.Clear();
                //add the ability to select multiple items in profile lookup
                mCustomerIDs.Add(lProfile[0], lProfile[2]);
                cmbCustomerList.DataSource = new BindingSource(mCustomerIDs, null);
                cmbCustomerList.DisplayMember = "Value";
                cmbCustomerList.ValueMember = "Key";
            }
        }

        public string memberCriteria()
        {
            List<RuleCriteriaPoints> lrulecriteria = new List<RuleCriteriaPoints>();

            RuleCriteriaPoints lmembercriteria = new RuleCriteriaPoints();
            lmembercriteria.CriteriaName = "Loyalty";
            lmembercriteria.CriteriaValue = chkLoyaltyOnly.Checked.ToString();
            lrulecriteria.Add(lmembercriteria);

            foreach (KeyValuePair<string, string> pmembercriteria in mCustomerIDs)
            {
                lmembercriteria = new RuleCriteriaPoints();
                lmembercriteria.CriteriaName = "Customer";
                lmembercriteria.CriteriaValue = pmembercriteria.Key;
                lrulecriteria.Add(lmembercriteria);
            }

            string JSONString = JsonConvert.SerializeObject(lrulecriteria, Formatting.Indented);

            return JSONString;
        }

        private void Recurse(TreeNode nodes)
        {
            RuleCriteriaPoints category = new RuleCriteriaPoints();
            category.CriteriaName = "Category";
            category.CriteriaValue = nodes.Text;
            RuleCriteria.Add(category);
            foreach (TreeNode subnode in nodes.Nodes)
            {
                Recurse(subnode);
            }
        }

        public void LoadPromos()
        {
            dt.Rows.Clear();
            //dgAccumulationPoints.Rows.Clear();
            DataTable ltb = new DataTable();
            string select = @"SELECT PromoID as ID, PromoCode, StartDate,EndDate, PointsValue as Points,PointAccumulationCriteria as Reward,IsActive as Active,RuleCriteriaID,RuleCriteria as MemberCriteriaID,PromotionType as PromoType FROM PROMOS";
            CommonClass.runSql(ref ltb, select);
            DataGridViewRow DRow;
            dgAccumulationPoints.DataSource = ltb;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dgAccumulationPoints.Rows.Add();
            //    DRow = dgAccumulationPoints.Rows[i];
            //    DRow.Cells["ID"].Value = dt.Rows[i]["PromoID"].ToString();
            //    DRow.Cells["PromoCode"].Value = dt.Rows[i]["PromoCode"].ToString();
            //    DRow.Cells["StartDate"].Value = dt.Rows[i]["StartDate"].ToString();
            //    DRow.Cells["EndDate"].Value = dt.Rows[i]["EndDate"].ToString();
            //    DRow.Cells["Points"].Value = dt.Rows[i]["PointsValue"].ToString();
            //    DRow.Cells["Reward"].Value = dt.Rows[i]["PointAccumulationCriteria"].ToString();
            //    DRow.Cells["Active"].Value = dt.Rows[i]["IsActive"].ToString() == "True" ? "Yes" : "No";
            //    DRow.Cells["RuleCriteriaID"].Value = dt.Rows[i]["RuleCriteriaID"].ToString();
            //    DRow.Cells["MemberCriteriaID"].Value = dt.Rows[i]["RuleCriteria"].ToString();
            //    DRow.Cells["PromoType"].Value = dt.Rows[i]["PromotionType"].ToString();
            //}
            for (int a = 0; a < this.dgAccumulationPoints.Rows.Count; a++)
            {
                if (this.dgAccumulationPoints.Rows[a].Cells["StartDate"].Value != null)
                {
                    if (this.dgAccumulationPoints.Rows[a].Cells["StartDate"].Value.ToString() != "")
                    {
                        this.dgAccumulationPoints.Rows[a].Cells["StartDate"].Value = Convert.ToDateTime(this.dgAccumulationPoints.Rows[a].Cells["StartDate"].Value.ToString()).ToShortDateString();
                    }
                }
                if (this.dgAccumulationPoints.Rows[a].Cells["EndDate"].Value != null)
                {
                    if (this.dgAccumulationPoints.Rows[a].Cells["EndDate"].Value.ToString() != "")
                    {
                        this.dgAccumulationPoints.Rows[a].Cells["EndDate"].Value = Convert.ToDateTime(this.dgAccumulationPoints.Rows[a].Cells["EndDate"].Value.ToString()).ToShortDateString();
                    }
                }
                if (this.dgAccumulationPoints.Rows[a].Cells["Active"].Value != null)
                {

                }
                else
                {

                }
            }
            for (int b = 0; b < this.dgAccumulationPoints.Rows.Count; b++)
            {
                if (this.dgAccumulationPoints.Rows[b].Cells["EndDate"].Value != null)
                {
                    if (this.dgAccumulationPoints.Rows[b].Cells["EndDate"].Value.ToString() != "")
                    {
                        this.dgAccumulationPoints.Rows[b].Cells["EndDate"].Value = Convert.ToDateTime(this.dgAccumulationPoints.Rows[b].Cells["EndDate"].Value.ToString()).ToShortDateString();
                    }
                }
            }
            this.dgAccumulationPoints.Columns["RuleCriteriaID"].Visible = false;
            this.dgAccumulationPoints.Columns["MemberCriteriaID"].Visible = false;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string code = "";
            code = txtPromoCode.Text;
            if (pID == "")
            {
                if (!IsDuplicate(code))
                {
                    if (cmbRewardName.Text != "")
                    {
                        if (cmbRewardName.Text == "Free Product")
                        {
                            if (FreeTable.Rows.Count <= 0)
                            {
                                MessageBox.Show("Please select an item as free product", "Promo Information Details");
                                return;
                            }
                        }
                        savePromo();
                        btnRefresh.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Please fill the required field.");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Record Already exists.");
                    btnRefresh.PerformClick();
                }
            }
            else
            {
                UpdatePromo(pID);
                btnRefresh.PerformClick();
            }
        }

        private void LoadPromo()
        {
            string itemPromoJson;
            ItemIDs = "";
            dgAccumulationPoints.ClearSelection();
            mCustomerIDs.Clear();
            //cmbCustomerList.Items.Clear();

            if (dgAccumulationPoints.Rows.Count > 0)
            {
                txtPromoCode.Text = dgAccumulationPoints.CurrentRow.Cells["PromoCode"].Value.ToString();
                string ptValue = dgAccumulationPoints.CurrentRow.Cells["Points"].Value.ToString();
                txtPoints.Value = ptValue != "" ? Convert.ToDecimal(ptValue) : 0;
                string dts = dgAccumulationPoints.CurrentRow.Cells["StartDate"].Value.ToString();
                sdate.Value = DateTime.Parse(dts); //.ToLocalTime(); no need to convert to local
                string dte = dgAccumulationPoints.CurrentRow.Cells["EndDate"].Value.ToString();
                edate.Value = DateTime.Parse(dte); //.ToLocalTime(); no need to convert to local
               // MessageBox.Show(dgAccumulationPoints.CurrentRow.Cells["Reward"].Value.ToString());
                cmbRewardName.Text = dgAccumulationPoints.CurrentRow.Cells["Reward"].Value.ToString();
                string active = dgAccumulationPoints.CurrentRow.Cells["Active"].Value.ToString();
                cbPromoType.Text = dgAccumulationPoints.CurrentRow.Cells["PromoType"].Value.ToString();
                // string file = x["RuleCriteriaID"].ToString();
                //MessageBox.Show(cmbRewardName.Text);
                string file = dgAccumulationPoints.CurrentRow.Cells["RuleCriteriaID"].Value.ToString();
                itemPromoJson = file.Replace("\t", "");
                itemPromos = JsonConvert.DeserializeObject<List<RuleCriteriaPoints>>(itemPromoJson);
                foreach (RuleCriteriaPoints promo in itemPromos)
                {
                    if (promo.CriteriaName.ToString() == "Supplier")
                    {
                        foreach (DataGridViewRow row in dgSupplier.Rows)
                        {
                            if (row.Cells[0].Value.ToString() == promo.CriteriaValue.ToString())
                            {
                                row.Cells[1].Value = true;
                            }
                        }
                    }
                    if (promo.CriteriaName.ToString() == "Item")
                    {                        
                        ItemIDs += promo.CriteriaValue.ToString() + ",";                         
                    }
                    if (promo.CriteriaName.ToString() == "Brand")
                    {
                        foreach (DataGridViewRow row in dgBrand.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == promo.CriteriaValue.ToString())
                            {
                                row.Cells[0].Value = true;
                            }
                        }
                    }
                    if (promo.CriteriaName.ToString() == "Category")
                    {
                        foreach (TreeNode mainNodes in treeCategory.Nodes)
                        {
                            if (mainNodes.Tag.ToString() == promo.CriteriaValue.ToString())
                            {
                                mainNodes.Checked = true;
                            }
                            foreach (TreeNode subnode in mainNodes.Nodes)
                            {
                                if (subnode.Tag.ToString() == promo.CriteriaValue.ToString())
                                {
                                    subnode.Checked = true;
                                }
                            }
                        }
                    }

                }

                string strmemberjson = dgAccumulationPoints.CurrentRow.Cells["MemberCriteriaID"].Value.ToString();
                strmemberjson = strmemberjson.Replace("\t", "");
                List<RuleCriteriaPoints> lmembercriteria = JsonConvert.DeserializeObject<List<RuleCriteriaPoints>>(strmemberjson);
                foreach (RuleCriteriaPoints member in lmembercriteria)
                {
                    if (member.CriteriaName == "Loyalty")
                    {
                        chkLoyaltyOnly.Checked = Convert.ToBoolean(member.CriteriaValue);
                    }
                    else if (member.CriteriaName == "Customer")
                    {
                        DataTable dt = new DataTable();
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@ID", member.CriteriaValue);
                        CommonClass.runSql(ref dt, "SELECT Name FROM Profile WHERE ID=@ID", param);
                        string lcustomername = "";
                        if (dt.Rows.Count > 0)
                            lcustomername = dt.Rows[0]["Name"].ToString();

                        mCustomerIDs.Add(member.CriteriaValue, lcustomername);
                    }
                }
                if (mCustomerIDs.Count > 0)
                {
                    cmbCustomerList.DataSource = new BindingSource(mCustomerIDs, null);
                    cmbCustomerList.DisplayMember = "Value";
                    cmbCustomerList.ValueMember = "Key";
                }

                if (active == "True")
                {
                    IsActive.Checked = true;
                }
                else
                {
                    IsActive.Checked = false;
                }

                btnDelete.Enabled = CanDelete;
            }
        }

        private void dgAccumulationPoints_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //foreach (DataGridViewRow row in dgSupplier.Rows)
            //{
            //    row.Cells[1].Value = false;
            //}
            //foreach (DataGridViewRow row in dgInventoryItems.Rows)
            //{
            //    row.Cells[1].Value = false;
            //}
            //foreach (DataGridViewRow row in dgBrand.Rows)
            //{
            //    row.Cells[0].Value = false;
            //}
            //foreach (TreeNode mainNodes in treeCategory.Nodes)
            //{
            //    mainNodes.Checked = false;
            //    foreach (TreeNode subnode in mainNodes.Nodes)
            //    {
            //        subnode.Checked = false;
            //    }
            //}
            // LoadPromo();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cbPromoType.Enabled = true;
            txtPoints.Enabled = true;
            txtPromoCode.Enabled = true;
            cmbRewardName.Enabled = true;
            sdate.Enabled = true;
            edate.Enabled = true;
            btnAddNew.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            //dgAccumulationPoints.Enabled = false;
            btnEdit.Enabled = false;
        }

        private void UpdatePromo(string promoID)
        {
            Dictionary<string, object> UpdateParam = new Dictionary<string, object>();

            string iactive;
            string ptValue = dgAccumulationPoints.CurrentRow.Cells["Points"].Value.ToString();
            // txtPoints.Value = ptValue != "" ? Convert.ToDecimal(ptValue) : 0;
            float PointValue = float.Parse(ptValue);
            DateTime EndDate = edate.Value.ToUniversalTime();
            EndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, 23, 59, 59);
            DateTime StartDate = sdate.Value.ToUniversalTime();
            StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 00, 00, 00);
            string sqlUpdate = @"UPDATE Promos SET PromoCode = @PromoCode, 
                                                   isActive = @isActive, 
                                                   PointsValue = @PointsValue, 
                                                   PointAccumulationCriteria = @PointAccumulationCriteria, 
                                                   StartDate = @StartDate, 
                                                   EndDate = @EndDate, 
                                                   RuleCriteria = @RuleCriteria,
                                                   RuleCriteriaID = @RuleCriteriaID,
                                                   PromotionType = @PromotionType 
                                 WHERE PromoID = " + promoID + "";

            UpdateParam.Add("@PromoCode", txtPromoCode.Text);
            UpdateParam.Add("@isActive", IsActive.Checked);
            UpdateParam.Add("@PointsValue", txtPoints.Value);
            UpdateParam.Add("@PointAccumulationCriteria", cmbRewardName.Text);
            UpdateParam.Add("@StartDate", StartDate);
            UpdateParam.Add("@EndDate", EndDate);
            UpdateParam.Add("@RuleCriteria", memberCriteria());
            UpdateParam.Add("@RuleCriteriaID", toJson());
            UpdateParam.Add("@PromotionType", cbPromoType.Text);

            string titles = "Update Promo Record for " + cmbRewardName.Text;
            if (cmbRewardName.Text == "Free Product")
            {
                DeleteProductItem(int.Parse(promoID));
                SaveFreeProductItem(int.Parse(promoID));
            }
            DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                CommonClass.runSql(sqlUpdate, CommonClass.RunSqlInsertMode.QUERY, UpdateParam);
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Promotions/Points Setup for Promo ID " + promoID.ToString(), promoID.ToString());
                MessageBox.Show("Record has been updated.", "INFORMATION");
                btnRefresh.PerformClick();
                LoadPromos();
            }
        }

        private bool IsDuplicate(string pCode)
        {
            string selectSql = "SELECT * FROM PROMOS WHERE PromoCode ='" + pCode + "'";
            CommonClass.runSql(ref Duplicatedt, selectSql);
            if (Duplicatedt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeletePromo(pID);
        }

        private void DeletePromo(string pID)
        {
            string sqlDelete = @"DELETE FROM Promos WHERE PromoID = " + pID;

            string titles = "Delete Promo Record for " + cmbRewardName.Text;
            DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                CommonClass.runSql(sqlDelete);
                MessageBox.Show("Record has been Deleted.", "INFORMATION");
                btnRefresh.PerformClick();
                LoadPromos();
            }
        }

        private void LoadInventoryItems()
        {
            this.dgInventoryItems.Columns["ItemPrice"].DefaultCellStyle.Format = "C2";
            string Id = ItemIDs;
            if (Id != "")
            {
                Id = ItemIDs.Remove(ItemIDs.Length - 1);
            }
            else
            {
                Id = "0";
            }
            dt.Rows.Clear();
            dgInventoryItems.Rows.Clear();
            string sqlLoadInventoryItems = @"SELECT i.ID, i.ItemDescription, i.ItemName, p.Name, s.Level0, i.BrandName, i.PartNumber From Items i 
            LEFT JOIN Profile p ON i.SupplierID = p.ID
            INNER JOIN ItemsSellingPrice s ON s.ItemID = i.ID WHERE ItemID IN (" + Id + ")";
            CommonClass.runSql(ref dt, sqlLoadInventoryItems);
            DataRow DRow;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgInventoryItems.Rows.Add();

                DRow = dt.Rows[i];
                dgInventoryItems.Rows[i].Cells["ItemID"].Value = DRow["ID"].ToString();
                dgInventoryItems.Rows[i].Cells["ItemName"].Value = DRow["ItemName"].ToString();
                ////DRow.Cells["ItemDescription"].Value = dt.Rows[i]["ItemDescription"].ToString();
                CommonClass.htmlToText(DRow["ItemDescription"].ToString());
                //HTMLEditor.Document.OpenNew(true).Write(DRow["ItemDescription"].ToString());
                dgInventoryItems.Rows[i].Cells["ItemDescription"].Value = CommonClass.htmlToText(DRow["ItemDescription"].ToString());
                //HTMLEditor.Document.Body.InnerText;
                dgInventoryItems.Rows[i].Cells["ItemSupplier"].Value = DRow["Name"].ToString();                
                dgInventoryItems.Rows[i].Cells["ItemPrice"].Value = Math.Round(float.Parse(DRow["Level0"].ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                dgInventoryItems.Rows[i].Cells["ItemBrand"].Value = DRow["BrandName"].ToString();
                dgInventoryItems.Rows[i].Cells["PartNumber"].Value = DRow["PartNumber"].ToString();
            }
            dgInventoryItems.Rows.Add();
        }

        private void LoadSupplier()
        {
            dt.Rows.Clear();
            dgSupplier.Rows.Clear();
            string sqlLoadSupplier = @"SELECT p.ID, p.Name, p.ExpenseAccountID, c.Phone, c.City FROM Profile p 
            INNER JOIN Contacts c ON p.LocationID = c.Location
            WHERE c.ProfileID = p.ID AND p.Type ='Supplier'";
            CommonClass.runSql(ref dt, sqlLoadSupplier);
            DataRow DRow;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgSupplier.Rows.Add();
                DRow = dt.Rows[i];
                dgSupplier.Rows[i].Cells[1].Value = "false";
                dgSupplier.Rows[i].Cells["SupplierID"].Value = DRow["ID"].ToString();
                dgSupplier.Rows[i].Cells["SupplierName"].Value = DRow["Name"].ToString();
                dgSupplier.Rows[i].Cells["SupplierAccount"].Value = DRow["ExpenseAccountID"].ToString();
                dgSupplier.Rows[i].Cells["SupplierPhone"].Value = DRow["Phone"].ToString();
                dgSupplier.Rows[i].Cells["SupplierCity"].Value = DRow["City"].ToString();
            }
        }

        private void LoadBrands()
        {
            dt.Rows.Clear();
            dgBrand.Rows.Clear();
            //string sqlLoadBrand = @"SELECT DISTINCT BrandName,ItemName FROM Items";
            string sqlLoadBrand = @"SELECT DISTINCT BrandName FROM Items";
            CommonClass.runSql(ref dt, sqlLoadBrand);
            DataRow DRow;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgBrand.Rows.Add();
                DRow = dt.Rows[i];
                dgBrand.Rows[i].Cells[0].Value = "false";
                dgBrand.Rows[i].Cells["BrandName"].Value = DRow["BrandName"].ToString();
                //dgBrand.Rows[i].Cells["ItemBrandName"].Value = DRow["ItemName"].ToString();
            }
        }

        private void LoadCategory()
        {
            dt.Rows.Clear();
            dgBrand.Rows.Clear();

            dt = new DataTable();
            string sqlLoadCategories = @"SELECT * FROM Category";
            CommonClass.runSql(ref dt, sqlLoadCategories);
            treeCategory.Nodes.Clear();
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
            //selectedNode += e.Node.Text;

            //count = 1;
            //for (int i = 0; i < count; i++)
            //{
            //    nodes = new string[i + 1];
            //    nodes[i] = selectedNode;
            //}
            //count++;
        }

        private void treeCategory_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            // count--;
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

        private void dgAccumulationPoints_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pID = dgAccumulationPoints.CurrentRow.Cells[0].Value.ToString();
            LoadPromo();
            LoadInventoryItems();
            //btnEdit.PerformClick();
        }

        private void cbPromoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPromoType.Text == "Buy Quantity ( X )")
            {
                string[] buyQty = new string[] { "Free Product",
                                                 "Fixed (X) Discount",
                                                 "Percentage Discount",
                                                 "Price (X)"};
                BindingList<string> BuyQty = new BindingList<string>(buyQty);
                cmbRewardName.DataSource = BuyQty;
            }
            else if (cbPromoType.Text == "Buy Amount ( X )")
            {
                string[] buyAmount = new string[] { "Percentage (X) Discount From List" };
                BindingList<string> BuyAmt = new BindingList<string>(buyAmount);
                cmbRewardName.DataSource = BuyAmt;
            }
            else
            {
                string[] DefaultPromo = new string[] { "Points (X)",
                                                        "Points (X) percentage",
                                                        "Points (X) percentage profit" };
                BindingList<string> defPromo = new BindingList<string>(DefaultPromo);
                cmbRewardName.DataSource = defPromo;
            }

            if (pID != "")
            {
                cmbRewardName.Text = dgAccumulationPoints.CurrentRow.Cells["Reward"].Value.ToString();
            }
                
        }

        private void cmbRewardName_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbRewardName.Text)
            {
                case "Free Product":
                    label4.Text = "Number of Items :";
                    txtPoints.DecimalPlaces = 0;
                    txtPoints.Value = 1;
                    btnFreeitems.Visible = true;
                    break;
                case "Fixed (X) Discount":
                    label4.Text = "Discount Amount :";
                    btnFreeitems.Visible = false;
                    txtPoints.DecimalPlaces = 2;
                    break;
                case "Percentage Discount":
                case "Percentage (X) Discount From List":
                    label4.Text = "Discount %:";
                    btnFreeitems.Visible = false;
                    txtPoints.DecimalPlaces = 2;
                    break;
                case "Price (X)":
                    label4.Text = "Price :";
                    btnFreeitems.Visible = false;
                    txtPoints.DecimalPlaces = 2;
                    break;
                case "Points (X)":
                    label4.Text = "Points :";
                    btnFreeitems.Visible = false;
                    txtPoints.DecimalPlaces = 2;
                    break;
                case "Points (X) percentage":
                case "Points (X) percentage profit":
                    label4.Text = "Points % :";
                    btnFreeitems.Visible = false;
                    txtPoints.DecimalPlaces = 2;
                    break;
                default:
                    label4.Text = "Points :";
                    btnFreeitems.Visible = false;
                    txtPoints.DecimalPlaces = 2;
                    break;

            }

        }
        private void InitFreeTable()
        {
            FreeTable = new DataTable();
            FreeTable.Columns.Add("ItemID", typeof(int));
            FreeTable.Columns.Add("Quantity", typeof(string));
            FreeTable.Columns.Add("ItemName", typeof(string));
            FreeTable.Columns.Add("PartNum", typeof(string));
            FreeTable.Columns.Add("Price", typeof(float));
            FreeTable.Columns.Add("SalesTaxCode", typeof(string));

            // FreeTable.Rows.Add();
        }

        private void btnFreeitems_Click(object sender, EventArgs e)
        {
            if (pID != "")
            {
                FreeProducts FreeLookup = new FreeProducts(CommonClass.FreeItemInvocation.POINTSACCUMULATION, FreeTable, int.Parse(pID));
                if (FreeLookup.ShowDialog() == DialogResult.OK)
                {
                    FreeTable = FreeLookup.GetFreeProductTable;
                }
            }
            else
            {
                FreeProducts FreeLookup = new FreeProducts(CommonClass.FreeItemInvocation.POINTSACCUMULATION, FreeTable);
                if (FreeLookup.ShowDialog() == DialogResult.OK)
                {
                    FreeTable = FreeLookup.GetFreeProductTable;
                }
            }
        }

        private void dgInventoryItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgInventoryItems.Rows[e.RowIndex].Cells["PartNumber"].Value == null)
            {
                ShowItemLookup("", "PartNumber");
                this.dgInventoryItems.Columns["ItemPrice"].DefaultCellStyle.Format = "C2";
                dgInventoryItems.Rows.Add();
            }
        }
        public bool ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum, "", whereCon);

            DataGridViewRow dgvRows = dgInventoryItems.CurrentRow;
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                dgvRows.Cells["ItemID"].Value = ItemRows.Cells[0].Value.ToString();
                dgvRows.Cells["PartNumber"].Value = ItemRows.Cells[1].Value;
                dgvRows.Cells["ItemDescription"].Value = CommonClass.htmlToText(ItemRows.Cells["ItemDescription"].Value.ToString());
                dgvRows.Cells["ItemPrice"].ReadOnly = false;
                dgvRows.Cells["ItemDescription"].ReadOnly = false;
                dgvRows.Cells["ItemName"].Value = ItemRows.Cells["ItemName"].Value.ToString();
                dgvRows.Cells["ItemBrand"].Value = ItemRows.Cells["BrandName"].Value.ToString();
                dgvRows.Cells["ItemSupplier"].Value = ItemRows.Cells["SupplierName"].Value.ToString();

                float ltaxrate = 0;
                DataRow rTx = CommonClass.getTaxDetails(ItemRows.Cells["SalesTaxCode"].Value.ToString());
                //if (rTx.ItemArray.Length > 0)
                //{
                //ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                //string lTaxCollectedAccountID = "";
                //lTaxCollectedAccountID = ((rTx["TaxCollectedAccountID"] == null || rTx["TaxCollectedAccountID"].ToString() == "") ? "0" : rTx["TaxCollectedAccountID"].ToString());
                //dgvRows.Cells["TaxCollectedAccountID"].Value = lTaxCollectedAccountID;
                //dgvRows.Cells["TaxRate"].Value = ltaxrate;
                //}
                //Assume that Price is already Inclusive
                float lSellingPriceInc = float.Parse(ItemRows.Cells["SellingPrice"].Value.ToString(), NumberStyles.Currency);
                float lSellingPriceEx = (ltaxrate != 0 ? lSellingPriceInc / (1 + (ltaxrate / 100)) : lSellingPriceInc);
                if (!CommonClass.IsItemPriceInclusive)//Recalc if not inclusive
                {
                    lSellingPriceEx = lSellingPriceInc;
                    lSellingPriceInc = (ltaxrate != 0 ? lSellingPriceEx * (1 + (ltaxrate / 100)) : lSellingPriceEx);
                }
                //Fill in the grid
                if (CommonClass.IsTaxcInclusiveEnterSales)
                {
                    dgvRows.Cells["ItemPrice"].Value = lSellingPriceInc;
                    //dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceInc;
                }
                else
                {
                    dgvRows.Cells["ItemPrice"].Value = lSellingPriceEx;
                    //dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceEx;
                }
                mPriceEx = lSellingPriceEx;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void dgInventoryItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (dgInventoryItems.CurrentCell.Value != null)
                {
                    ShowItemLookup(dgInventoryItems.CurrentCell.Value.ToString(), "PartNumber");
                    dgInventoryItems.Rows.Add();
                }
                else
                {
                    ShowItemLookup("", "PartNumber");
                    dgInventoryItems.Rows.Add();
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if(dgInventoryItems.CurrentCell.RowIndex < 0)
            {
                return;
            }
            if (dgInventoryItems.CurrentCell.Value != null)
            {
                ShowItemLookup(dgInventoryItems.CurrentCell.Value.ToString(), "PartNumber");
                dgInventoryItems.Rows.Add();
            }
            else
            {
                ShowItemLookup("", "PartNumber");
                dgInventoryItems.Rows.Add();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            int lRowIndex = dgInventoryItems.CurrentCell.RowIndex;
            if(lRowIndex < 0)
            {
                return;

            }
            foreach (DataGridViewRow item in this.dgInventoryItems.SelectedRows)
            {
                dgInventoryItems.Rows.RemoveAt(item.Index);
            }
            dgInventoryItems.Rows.RemoveAt(lRowIndex);

        }
    }//END
}
