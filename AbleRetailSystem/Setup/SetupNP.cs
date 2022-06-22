using RestaurantPOS.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Setup
{
    public partial class SetupNP : Form
    {
        public SetupNP()
        {
            InitializeComponent();
        }

        private void btnTaxCodes_Click(object sender, EventArgs e)
        {
            if (CommonClass.TaxCodeFrm == null || CommonClass.TaxCodeFrm.IsDisposed)
            {
                CommonClass.TaxCodeFrm = new CreateTaxCode();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.TaxCodeFrm.MdiParent = this.MdiParent;
            CommonClass.TaxCodeFrm.Show();
            CommonClass.TaxCodeFrm.Focus();
            if (CommonClass.TaxCodeFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.TaxCodeFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCurrency_Click(object sender, EventArgs e)
        {
            if (CommonClass.CurrencyFrm == null
               || CommonClass.CurrencyFrm.IsDisposed)
            {
                CommonClass.CurrencyFrm = new Currency();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CurrencyFrm.MdiParent = this.MdiParent;
            CommonClass.CurrencyFrm.Show();
            CommonClass.CurrencyFrm.Focus();
            if (CommonClass.CurrencyFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CurrencyFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPaymentMethod_Click(object sender, EventArgs e)
        {
            if (CommonClass.PaymentMethodFrm == null || CommonClass.PaymentMethodFrm.IsDisposed)
            {
                CommonClass.PaymentMethodFrm = new CreatePaymentMethod();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PaymentMethodFrm.MdiParent = this.MdiParent;
            CommonClass.PaymentMethodFrm.Show();
            CommonClass.PaymentMethodFrm.Focus();
            if (CommonClass.PaymentMethodFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PaymentMethodFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnShippingMethods_Click(object sender, EventArgs e)
        {
            if (CommonClass.ShippingMethodFrm == null || CommonClass.ShippingMethodFrm.IsDisposed)
            {
                CommonClass.ShippingMethodFrm = new CreateShippingMethod();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ShippingMethodFrm.MdiParent = this.MdiParent;
            CommonClass.ShippingMethodFrm.Show();
            CommonClass.ShippingMethodFrm.Focus();
            if (CommonClass.ShippingMethodFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ShippingMethodFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (CommonClass.CustomerListFrm == null || CommonClass.CustomerListFrm.IsDisposed)
            {
                CommonClass.CustomerListFrm = new CustomerList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.CustomerListFrm.MdiParent = this.MdiParent;
            CommonClass.CustomerListFrm.Show();
            CommonClass.CustomerListFrm.Focus();
            if (CommonClass.CustomerListFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.CustomerListFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            if (CommonClass.SupplierListFrm == null || CommonClass.SupplierListFrm.IsDisposed)
            {
                CommonClass.SupplierListFrm = new SupplierList();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SupplierListFrm.MdiParent = this.MdiParent;
            CommonClass.SupplierListFrm.Show();
            CommonClass.SupplierListFrm.Focus();
            if (CommonClass.SupplierListFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SupplierListFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnAccountsReceivable_Click(object sender, EventArgs e)
        {
            if (CommonClass.ARBalancesFrm == null || CommonClass.ARBalancesFrm.IsDisposed)
            {
                CommonClass.ARBalancesFrm = new ARBalances();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.ARBalancesFrm.MdiParent = this.MdiParent;
            CommonClass.ARBalancesFrm.Show();
            CommonClass.ARBalancesFrm.Focus();
            if (CommonClass.ARBalancesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.ARBalancesFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnItemList_Click(object sender, EventArgs e)
        {
            if (CommonClass.ItemListfrm == null || CommonClass.ItemListfrm.IsDisposed)
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

        private void btnDataInformation_Click(object sender, EventArgs e)
        {
            if (CommonClass.DataInfoFrm == null || CommonClass.DataInfoFrm.IsDisposed)
            {
                CommonClass.DataInfoFrm = new DataInformation();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.DataInfoFrm.MdiParent = this.MdiParent;
            CommonClass.DataInfoFrm.Show();
            CommonClass.DataInfoFrm.Focus();
            if (CommonClass.DataInfoFrm.DialogResult == DialogResult.Cancel
                || CommonClass.DataInfoFrm.DialogResult == DialogResult.OK)
            {
                CommonClass.DataInfoFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPreference_Click(object sender, EventArgs e)
        {
            if (CommonClass.PreferencesFrm == null || CommonClass.PreferencesFrm.IsDisposed)
            {
                CommonClass.PreferencesFrm = new Preferences();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PreferencesFrm.MdiParent = this.MdiParent;
            CommonClass.PreferencesFrm.Show();
            CommonClass.PreferencesFrm.Focus();
            if (CommonClass.PreferencesFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PreferencesFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if (CommonClass.UserMaintenanceFrm == null
                || CommonClass.UserMaintenanceFrm.IsDisposed)
            {
                CommonClass.UserMaintenanceFrm = new UserMaintenance();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.UserMaintenanceFrm.MdiParent = this.MdiParent;
            CommonClass.UserMaintenanceFrm.Show();
            CommonClass.UserMaintenanceFrm.Focus();
            if (CommonClass.UserMaintenanceFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.UserMaintenanceFrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void btnCategories_Click(object sender, EventArgs e)
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
    }
}
