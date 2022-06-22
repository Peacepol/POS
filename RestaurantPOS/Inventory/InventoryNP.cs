using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Inventory
{
    public partial class InventoryNP : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        public InventoryNP()
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

        private void btn_Items_Click(object sender, EventArgs e)
        {
          
            if (CommonClass.ItemListfrm == null
          || CommonClass.ItemListfrm.IsDisposed)
            {
                CommonClass.ItemListfrm = new ItemList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemListfrm.MdiParent = this.MdiParent;
            CommonClass.ItemListfrm.Show();
            CommonClass.ItemListfrm.Focus();
            if (CommonClass.ItemListfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemListfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void InventoryNP_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void ItemList_btn_Click(object sender, EventArgs e)
        {
           
        }

        private void btnItemRegister_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemRegisterFrm == null
        || CommonClass.ItemRegisterFrm.IsDisposed)
            {
                CommonClass.ItemRegisterFrm = new ItemRegister();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemRegisterFrm.MdiParent = this.MdiParent;
            CommonClass.ItemRegisterFrm.Show();
            CommonClass.ItemRegisterFrm.Focus();
            if (CommonClass.ItemRegisterFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemRegisterFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnBuildItems_Click(object sender, EventArgs e)
        {
            if (CommonClass.BuildItemsFrm == null
            || CommonClass.BuildItemsFrm.IsDisposed)
            {
                CommonClass.BuildItemsFrm = new BuildItems();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.BuildItemsFrm.MdiParent = this.MdiParent;
            CommonClass.BuildItemsFrm.Show();
            CommonClass.BuildItemsFrm.Focus();
            if (CommonClass.BuildItemsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.BuildItemsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnStockAdjustments_Click(object sender, EventArgs e)
        {
            if (CommonClass.StockAdjustmentsFrm == null
           || CommonClass.StockAdjustmentsFrm.IsDisposed)
            {
                CommonClass.StockAdjustmentsFrm = new StockAdjustments();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.StockAdjustmentsFrm.MdiParent = this.MdiParent;
            CommonClass.StockAdjustmentsFrm.Show();
            CommonClass.StockAdjustmentsFrm.Focus();
            if (CommonClass.StockAdjustmentsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.StockAdjustmentsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnAutoBuild_Click(object sender, EventArgs e)
        {
            if (CommonClass.AutoBuildItemsFrm == null
           || CommonClass.AutoBuildItemsFrm.IsDisposed)
            {
                CommonClass.AutoBuildItemsFrm = new AutoBuildItems();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.AutoBuildItemsFrm.MdiParent = this.MdiParent;
            CommonClass.AutoBuildItemsFrm.Show();
            CommonClass.AutoBuildItemsFrm.Focus();
            if (CommonClass.AutoBuildItemsFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.AutoBuildItemsFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnStocktake_Click(object sender, EventArgs e)
        {
            if (CommonClass.StocktakeFrm == null
       || CommonClass.StocktakeFrm.IsDisposed)
            {
                CommonClass.StocktakeFrm = new Stocktake();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.StocktakeFrm.MdiParent = this.MdiParent;
            CommonClass.StocktakeFrm.Show();
            CommonClass.StocktakeFrm.Focus();
            if (CommonClass.StocktakeFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.StocktakeFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPriceUpdate_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemPriceUpdateFrm == null
        || CommonClass.ItemPriceUpdateFrm.IsDisposed)
            {
                CommonClass.ItemPriceUpdateFrm = new ItemPriceUpdate();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ItemPriceUpdateFrm.MdiParent = this.MdiParent;
            CommonClass.ItemPriceUpdateFrm.Show();
            CommonClass.ItemPriceUpdateFrm.Focus();
            if (CommonClass.ItemPriceUpdateFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ItemPriceUpdateFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnTransactionLookup_Click(object sender, EventArgs e)
        {
            CommonClass.TransactionsLookupFrm = new TransactionsLookup();
            this.Cursor = Cursors.WaitCursor;
            CommonClass.TransactionsLookupFrm.MdiParent = this.MdiParent;
            CommonClass.TransactionsLookupFrm.Show();
            CommonClass.TransactionsLookupFrm.Focus();
            if (CommonClass.TransactionsLookupFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.TransactionsLookupFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommonClass.Categories = new Categories();
            this.Cursor = Cursors.WaitCursor;
            CommonClass.Categories.MdiParent = this.MdiParent;
            CommonClass.Categories.Show();
            CommonClass.Categories.Focus();
            if (CommonClass.Categories.DialogResult == DialogResult.Cancel)
            {
                CommonClass.Categories.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPriceTags_Click(object sender, EventArgs e)
        {
            CommonClass.PriceTags = new PriceTags();
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PriceTags.MdiParent = this.MdiParent;
            CommonClass.PriceTags.Show();
            CommonClass.PriceTags.Focus();
            if (CommonClass.PriceTags.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PriceTags.Close();
            }
            this.Cursor = Cursors.Default;
        }
    }
}
