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
    public partial class AutoBuildItems : Form
    {
        private string CurSeries = "";
        private string AdjNumber = "";
        private string thisFormCode = "";
        private bool CanView = false;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        public AutoBuildItems()
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

        private void AutoBuildItems_Load(object sender, EventArgs e)
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
            LoadAutoBuildItems();
            this.btnRecord.Enabled = CanAdd;
        }

        private void LoadAutoBuildItems()
        {
            DataTable ltb = new DataTable();
            
                string sql = @"SELECT ID, PartNumber, ItemName, q.OnHandQty, ISNULL(o.OnOrderQty,0) as OnOrderQty, ISNULL(c.CommittedQty,0) as CommittedQty, 
                (q.OnHandQty + ISNULL(o.OnOrderQty,0) - ISNULL(c.CommittedQty,0)) as AvailableQty, NULL as QtyToBuild, i.AssetAccountID, p.AverageCostEx FROM  ((( Items as i inner join ItemsQty as q on i.ID = q.ItemID ) left join 
                ( SELECT l.EntityID as ItemID, sum(OrderQty-ReceiveQty) as OnOrderQty  FROM (PurchaseLines as l inner join Purchases as p on l.PurchaseID = p.PurchaseID ) 
                inner join Items as i on l.EntityID = i.ID where i.IsAutoBuild = 1 and P.PurchaseType = 'ORDER' and p.LayoutType = 'Item'  group by l.EntityID ) as  o on i.ID = o.ItemID ) left join
                ( SELECT l.EntityID as ItemID, sum(OrderQty-ShipQty) as CommittedQty  FROM (SalesLines as l inner join Sales as s on l.SalesID = s.SalesID ) 
                inner join Items as i on l.EntityID = i.ID where i.IsAutoBuild = 1 and s.SalesType = 'ORDER' and s.LayoutType = 'Item'  group by l.EntityID ) as c on i.ID = c.ItemID )
                inner join ItemsCostPrice as p on i.ID = p.ItemID where i.IsAutoBuild = 1";

            CommonClass.runSql(ref ltb, sql);

                this.dgridItems.DataSource = ltb;
                this.dgridItems.Columns[0].Visible = false;
                this.dgridItems.Columns[1].ReadOnly = true;
                this.dgridItems.Columns[1].HeaderText = "Part Number";
                this.dgridItems.Columns[2].ReadOnly = true;
                this.dgridItems.Columns[2].HeaderText = "Item Name";
                this.dgridItems.Columns[3].ReadOnly = true;
                this.dgridItems.Columns[3].HeaderText = "On Hand Qty";
                this.dgridItems.Columns[4].ReadOnly = true;
                this.dgridItems.Columns[4].HeaderText = "On Order Qty";
                this.dgridItems.Columns[5].ReadOnly = true;
                this.dgridItems.Columns[5].HeaderText = "Committed Qty";
                this.dgridItems.Columns[6].ReadOnly = true;
                this.dgridItems.Columns[6].HeaderText = "Available Qty";
                this.dgridItems.Columns[7].ReadOnly = false;
                this.dgridItems.Columns[7].HeaderText = "Qty To Build";
                this.dgridItems.Columns[8].ReadOnly = true;
                this.dgridItems.Columns[9].Visible = false;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            string[] lToBuild = new string[2];
            float lQty = 0;
            DataTable SelItems;
            SelItems = new DataTable();
            SelItems.Columns.Add("ItemID", typeof(string));
            SelItems.Columns.Add("PartNumber", typeof(string));
            SelItems.Columns.Add("ItemName", typeof(string));
            SelItems.Columns.Add("QtyToBuild", typeof(float));
            SelItems.Columns.Add("AssetAccountID", typeof(string));
            SelItems.Columns.Add("AverageCostEx", typeof(float));
            DataRow sr;
            for (int i = 0; i<this.dgridItems.Rows.Count; i++)
            {
                if(this.dgridItems.Rows[i].Cells["QtyToBuild"].Value != null && this.dgridItems.Rows[i].Cells["QtyToBuild"].Value.ToString() != "")
                {
                    lQty = float.Parse(this.dgridItems.Rows[i].Cells["QtyToBuild"].Value.ToString());
                    if(lQty != 0)
                    {
                        sr = SelItems.NewRow(); 
                        sr["ItemID"] = this.dgridItems.Rows[i].Cells["ID"].Value.ToString();
                        sr["PartNumber"] = this.dgridItems.Rows[i].Cells["PartNumber"].Value.ToString();
                        sr["ItemName"] = this.dgridItems.Rows[i].Cells["ItemName"].Value.ToString();
                        sr["QtyToBuild"] = float.Parse(this.dgridItems.Rows[i].Cells["QtyToBuild"].Value.ToString());
                        sr["AssetAccountID"] = this.dgridItems.Rows[i].Cells["AssetAccountID"].Value.ToString();
                        sr["AverageCostEx"] = float.Parse(this.dgridItems.Rows[i].Cells["AverageCostEx"].Value.ToString());
                        SelItems.Rows.Add(sr);
                    }
                }
            }
            if(SelItems.Rows.Count > 0)
            {
                BuildItems BuildItemsDlg = new BuildItems(CommonClass.InvocationSource.AUTOBUILD, SelItems);
                if (BuildItemsDlg.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Auto Build Inventory Items completed successfully.");
                    this.Close();
                }
            }

        }

        
        
      


    }
}
