using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Subro.Controls;

namespace AbleRetailPOS.Inventory
{
    public partial class Categories : Form
    {
        private bool isNew = false;
        private bool forEdit = false;
        private string CatID;
        private string MainCat;
        DataTable dt;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        public Categories()
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

  

        private void Categories_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            rbSub.Checked = true;
            gbDetail.Enabled = false;
            gbCodes.Enabled = false;
            btnSave.Enabled = false;
            LoadCategories();
            btnDelete.Enabled = CanDelete;
            btnEdit.Enabled = CanEdit;
            btnAddNew.Enabled = CanAdd;


        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            gbDetail.Enabled = true;
            btnSave.Enabled = true;
            isNew = true;
            rbSub.Checked = true;
            if (rbSub.Checked)
                gbCodes.Enabled = true;
            ClearFields();
        }
        void ClearFields()
        {
            lblMainID.Text = "";
            lblCatID.Text = "";
            txtCatCode.Text = "";
            Description.Text = "";
            txtIncomeCode.Text = "";
            txtCOSCode.Text = "";
            txtInventoryCode.Text = "";
            txtItemType.Text = "";
        }
        private void rbSub_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSub.Checked)
            {
                gbCodes.Enabled = true;
                txtMainCatID.Enabled = true;
                CatID = "";
            }
            else
            {
                gbCodes.Enabled = false;
                txtMainCatID.Text = "";
                lblMainID.Text = "";
                CatID = "";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gbDetail.Enabled = false;
            gbCodes.Enabled = false;
            btnDelete.Enabled = CanDelete;
            btnEdit.Enabled = CanEdit;
            btnAddNew.Enabled = CanAdd;
            btnSave.Enabled = false;
            txtMainCatID.Text = "";
            lblMainID.Text = "";
            isNew = false;
            ClearFields();
            LoadCategories();
            CatID = "";
            forEdit = false;
           
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (forEdit)
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = false;
                btnAddNew.Enabled = false;
                btnSave.Enabled = true;
                gbDetail.Enabled = true;
                btnSave.Enabled = true;
                isNew = false;
                if (rbSub.Checked)
                    gbCodes.Enabled = true;
                else
                    gbCodes.Enabled = false;
            }
            else
            {
                MessageBox.Show("Select category to edit. \n Double click the category to edit.", "Category Info");
            }
         
        }
        private int SaveCategory()
        {
            string saveSql = @"INSERT INTO Category ( MainCategoryID, CategoryCode,Description ,IncomeGLCode, COSGLCode, InventoryGLCode,ItemType,ShowInMenu)
                                Values (@MainCatID, @CatCode, @Description , @IncomeCode , @CosCode , @InventoryCode , @ItemType, @ShowInMenu )";
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (rbSub.Checked  &&  txtMainCatID.Text != "")
                param.Add("@MainCatID", lblMainID.Text);
            else if(rbMain.Checked)
            {
                param.Add("@MainCatID", "0");
            }
            else
            {
                MessageBox.Show("Please select a main category", "Category Information");
                return -1;
            }

               
            param.Add("@CatCode", txtCatCode.Text);
            param.Add("@Description", Description.Text);
            param.Add("@IncomeCode", txtIncomeCode.Text);
            param.Add("@CosCode", txtCOSCode.Text);
            param.Add("@InventoryCode", txtInventoryCode.Text);
            param.Add("@ItemType", txtItemType.Text);
            param.Add("@ShowInMenu", chkShowInMenu.Checked);
            int x = CommonClass.runSql(saveSql, CommonClass.RunSqlInsertMode.SCALAR, param);
            if (x > 0)
            {
                MessageBox.Show("Category successfully added", "Category Info");

            }
            return x;

        }
        private int UpdateCategory(string pCatID)
        {
            string updateSql = @"UPDATE Category SET MainCategoryID = @MainCatID ,
                                CategoryCode = @CatCode,
                                Description=@Description,
                                IncomeGLCode=@IncomeCode,
                                COSGLCode=@CosCode,
                                InventoryGLCode= @InventoryCode,
                                ItemType = @ItemType,
                                ShowInMenu = @ShowInMenu WHERE CategoryID = " + pCatID;
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (rbSub.Checked || lblMainID.Text != "")
                param.Add("@MainCatID", lblMainID.Text);
            else if(rbMain.Checked)
                param.Add("@MainCatID", "0");
            else
            {
                MessageBox.Show("Please select a main category", "Category Information");
                return -1;
            }

            param.Add("@CatCode", txtCatCode.Text);
            param.Add("@Description",Description.Text);
            param.Add("@IncomeCode", txtIncomeCode.Text);
            param.Add("@CosCode", txtCOSCode.Text);
            param.Add("@InventoryCode", txtInventoryCode.Text);
            param.Add("@ItemType", txtItemType.Text);
            param.Add("@ShowInMenu", chkShowInMenu.Checked);
            int x = CommonClass.runSql(updateSql, CommonClass.RunSqlInsertMode.QUERY, param);
            if (x > 0)
            {
                MessageBox.Show("Category successfully updated", "Category Info");

            }
            return x;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int x = 0;
            if (isNew)
            {
              x = SaveCategory();
            }
            else
            {
                x = UpdateCategory(this.lblCatID.Text);
            }
            if (x > 0)
            {
                btnRefresh.PerformClick();
            }
            
        }
        void LoadCategories()
        {
            treeCategory.Nodes.Clear();
            dt = new DataTable();
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

        void CatDetails()
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow DRow = dt.Rows[x];
                if (CatID == DRow["CategoryID"].ToString())
                {
                    if (DRow["MainCategoryID"].ToString() == "0")
                    {
                        rbMain.Checked = true;
                        txtMainCatID.Text = "";
                    }
                    else
                    {
                        rbSub.Checked = true;
                        txtMainCatID.Text = MainCategoryDetail(DRow["MainCategoryID"].ToString());
                        lblMainID.Text = DRow["MainCategoryID"].ToString();
                    }
                    txtCatCode.Text = DRow["CategoryCode"].ToString();
                    Description.Text = DRow["Description"].ToString();
                    txtIncomeCode.Text = DRow["IncomeGLCode"].ToString();
                    txtCOSCode.Text = DRow["COSGLCode"].ToString();
                    txtInventoryCode.Text = DRow["InventoryGLCode"].ToString();
                    txtItemType.Text = DRow["ItemType"].ToString();
                    lblCatID.Text = CatID;
                    chkShowInMenu.Checked = (bool)DRow["ShowInMenu"];
                }
            }
        }
       public string MainCategoryDetail(string ID)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow DRow = dt.Rows[x];
                if (ID == DRow["CategoryID"].ToString() && DRow["MainCategoryID"].ToString() == "0")
                {
                      
                    return DRow["CategoryCode"].ToString();
                }
            }
            return "";
        }

        private void rbMain_CheckedChanged(object sender, EventArgs e)
        {
            txtMainCatID.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string delCatsql = "";
            if (CatID != "")
            {
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt,"Select CategoryID FROM Items");
                foreach(DataRow dx in dt.Rows)
                {
                    if(CatID == dx["CategoryID"].ToString())
                    {
                        MessageBox.Show("Category is being used by an item. Remove connection of category to item first.", "Category Info");
                        return;
                    }
                }
                delCatsql = "DELETE FROM Category WHERE CategoryID = " + CatID;
                int x = CommonClass.runSql(delCatsql);
                if(x > 0)
                {
                    MessageBox.Show("Category Deleted", "Category Info");
                
                }
            }
               
        }

        private void treeCategory_DoubleClick(object sender, EventArgs e)
        {
            CatID = treeCategory.SelectedNode.Tag.ToString();
            if (isNew)
            {
                if (rbSub.Checked)
                {
                    MainCat = CatID;
                    lblMainID.Text = CatID;
                    txtMainCatID.Text = MainCategoryDetail(CatID);
                    lblCatID.Text = CatID;
                }
                return;
            }
            else
            {
                CatDetails();
                forEdit = true;
            }

        }

        private void treeCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CatID = treeCategory.SelectedNode.Tag.ToString();
            if (isNew)
            {
                if (rbSub.Checked)
                {
                    MainCat = CatID;
                    lblMainID.Text = CatID;
                    txtMainCatID.Text = MainCategoryDetail(CatID);
                    lblCatID.Text = CatID;
                }
                return;
            }
            else
            {
                CatDetails();
               
            }

        }

        private void pbCat_Click(object sender, EventArgs e)
        {
            CategoryLookup catLookup = new CategoryLookup(true, false);
            if (catLookup.ShowDialog() == DialogResult.OK)
            {
                string[] catDet = catLookup.GetCat;
                CatID = catDet[0];
                txtMainCatID.Text = catDet[1];
                lblMainID.Text = CatID;
            }
        }
    }
}
