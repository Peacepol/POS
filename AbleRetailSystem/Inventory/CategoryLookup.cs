using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Inventory
{
    public partial class CategoryLookup : Form
    {
        private string[] Cat;
        DataTable dt;
        private string catId;
        private bool forMain;
        private bool forSub;

        public CategoryLookup(bool mainCatonly = false, bool subCatonly = false)
        {
            forMain = mainCatonly;
            forSub = subCatonly;
            InitializeComponent();
        }
        public string[] GetCat
        {
            get { return Cat; }
        }
        private void CategoryLookup_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }
        void LoadCategories()
        {
            dt = new DataTable();
            string sqlLoadCategories = @"SELECT * FROM Category ";
            if (forMain)
            {
                sqlLoadCategories += "WHERE MainCategoryID = '0'";
            }
            else if (forSub) {
                sqlLoadCategories += "WHERE MainCategoryID != '0'";
            }

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
            if (forSub)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MainCategoryID"].ToString() != "0")
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

        }
       void  MainCategoryDetail(string ID)
        {
            Cat = new string[6];
            for (int x = 0; x < dt.Rows.Count; x++)
            {
              
                DataRow DRow = dt.Rows[x];
                if (ID == DRow["CategoryID"].ToString())
                {
                    Cat[0] = DRow["CategoryID"].ToString();
                    Cat[1] = DRow["CategoryCode"].ToString();
                    Cat[2] = DRow["IncomeGLCode"].ToString();
                    Cat[3] = DRow["COSGLCode"].ToString();
                    Cat[4] = DRow["InventoryGLCode"].ToString();
                    this.DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
            }
         
        }

        private void treeCategory_DoubleClick(object sender, EventArgs e)
        {
            catId = treeCategory.SelectedNode.Tag.ToString();
            MainCategoryDetail(catId);
        }
    }//END
}
