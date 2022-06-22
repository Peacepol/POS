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


namespace RestaurantPOS
{
    public partial class ReferencesNP : Form
    {
        public ReferencesNP()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }      

        private void btnCustomers_Click(object sender, EventArgs e)
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

        private void btnSuppliers_Click(object sender, EventArgs e)
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
            if (CommonClass.CurrencyFrm == null || CommonClass.CurrencyFrm.IsDisposed)
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

        private void btnJob_Click(object sender, EventArgs e)
        {
            if (CommonClass.JobFrm == null || CommonClass.JobFrm.IsDisposed)
            {
                CommonClass.JobFrm = new Job();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.JobFrm.MdiParent = this.MdiParent;
            CommonClass.JobFrm.Show();
            CommonClass.JobFrm.Focus();
            if (CommonClass.JobFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.JobFrm.Close();
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

        private void btnShippingMethod_Click(object sender, EventArgs e)
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
    }//end
}
