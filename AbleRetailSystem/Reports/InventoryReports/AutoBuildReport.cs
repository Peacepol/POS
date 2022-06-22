using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Reports.InventoryReports
{
    public partial class AutoBuildReport : Form
    {
        private string supplierID = "";
        private string list1 = "";
        private string list2 = "";
        private string list3 = "";
        string categories = "";
        DataTable TbRep;
        private bool CanView = false;
        public AutoBuildReport()
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

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            if (txtSupplier.Text == "")
            {
                supplierID = "";
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            filterCategories();
            LoadReport();
        }
        private void LoadReport()
        {
            string sqlcat = "";
            string sql = @"
SELECT a.PartItemQty, a.ItemID, a.PartItemID,
i.ItemNumber, i.PartNumber, i.ItemName, i.ID,
p.AverageCostEx, q.OnHandQty
FROM Items i INNER JOIN  ItemsAutoBuild a ON a.PartItemID = i.ID
INNER JOIN ItemsCostPrice p ON p.ItemID = i.ID
INNER JOIN ItemsQty q ON q.ItemID = i.ID ";
            string wherecon = "";
            if (list1 != "")
            {
                wherecon += wherecon == "" ? " WHERE i.CList1 = @CList1 ": " AND i.CList1 = @CList1";
            }
            if (list2 != "")
            {
                wherecon += wherecon == "" ? " WHERE i.CList2 = @CList2 " : " AND i.CList2 = @CList2";
            }
            if (list3 != "")
            {
                wherecon += wherecon == "" ? " WHERE i.CList3 = @CList3 " : " AND i.CList3 = @CList3";
            }
            if (supplierID != "")
            {
                wherecon += wherecon == "" ? " WHERE i.SupplierID = @supplier " : " AND i.SupplierID = @supplier";
            }

            if (categories.Length > 0)
            {
                wherecon += wherecon == "" ? " WHERE CategoryID in (" + categories + ")" : " AND CategoryID in (" + categories + ")";
            }
            if (cmbSort.Text == "Item Number")
            {
                wherecon +=  " ORDER BY ItemNumber DESC";
            }
            else if (cmbSort.Text == "Part Number")
            {
                wherecon +=  " ORDER BY PartNumber DESC";
            }
            sql += wherecon;
            TbRep = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@supplier", supplierID);
            param.Add("@CList1", list1);
            param.Add("@CList2", list2);
            param.Add("@CList3", list3);
            CommonClass.runSql(ref TbRep, sql, param);

            string sql2= @"SELECT a.PartItemQty, a.ItemID, a.PartItemID,
i.ItemNumber, i.PartNumber, i.ItemName,
p.AverageCostEx, q.OnHandQty
FROM ItemsAutoBuild a INNER JOIN Items i ON a.ItemID = i.ID
INNER JOIN ItemsCostPrice p ON p.ItemID = i.ID
INNER JOIN ItemsQty q ON q.ItemID = i.ID";
            sql2 += wherecon;


            DataTable TbRep2 = new DataTable();
            CommonClass.runSql(ref TbRep2, sql2,param);


            Reports.ReportParams itemparams = new Reports.ReportParams();
            itemparams.PrtOpt = 1;
            itemparams.Rec.Add(TbRep);
            itemparams.Rec.Add(TbRep2);
            itemparams.ReportName = "AutoBuildItems.rpt";
            itemparams.RptTitle = "Auto-Build Items";
            itemparams.Params = "compname";
            itemparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(itemparams);

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

        private void AutoBuildReport_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadCategory();
            cmbSort.SelectedIndex = 1;
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

    }
}
