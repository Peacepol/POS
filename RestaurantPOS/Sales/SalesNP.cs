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

namespace AbleRetailPOS.Sales
{
    public partial class SalesNP : Form
    {
        private DataTable TbRep;
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;

        public SalesNP()
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
        
        private void entersales_btn_Click(object sender, EventArgs e)
        {
            if (CommonClass.EnterSalesfrm == null
                || CommonClass.EnterSalesfrm.IsDisposed)
            {
                CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
            CommonClass.EnterSalesfrm.Show();
            CommonClass.EnterSalesfrm.Focus();
            if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EnterSalesfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void salesReg_btn_Click(object sender, EventArgs e)
        {
            if (CommonClass.SalesRegfrm == null
                || CommonClass.SalesRegfrm.IsDisposed)
            {
                CommonClass.SalesRegfrm = new SalesRegister();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SalesRegfrm.MdiParent = this.MdiParent;
            CommonClass.SalesRegfrm.Show();
            CommonClass.SalesRegfrm.Focus();
            if (CommonClass.SalesRegfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SalesRegfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void receivePayment_btn_Click(object sender, EventArgs e)
        {
            if (CommonClass.SRPaymentsfrm == null
                || CommonClass.SRPaymentsfrm.IsDisposed)
            {
                CommonClass.SRPaymentsfrm = new SalesReceivePayment(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.SRPaymentsfrm.MdiParent = this.MdiParent;
            CommonClass.SRPaymentsfrm.Show();
            CommonClass.SRPaymentsfrm.Focus();
            if (CommonClass.SRPaymentsfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.SRPaymentsfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            if (CommonClass.TranJournalFrm == null || CommonClass.TranJournalFrm.IsDisposed)
            {
                CommonClass.TranJournalFrm = new TransactionJournal();
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.TranJournalFrm.MdiParent = this.MdiParent;
            CommonClass.TranJournalFrm.Show();
            CommonClass.TranJournalFrm.Focus();
            if (CommonClass.TranJournalFrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.TranJournalFrm.Close();
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
            if (CommonClass.QuickSalesfrm == null
                || CommonClass.QuickSalesfrm.IsDisposed)
            {
                CommonClass.QuickSalesfrm = new QuickSales(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.QuickSalesfrm.MdiParent = this.MdiParent;
            CommonClass.QuickSalesfrm.Show();
            CommonClass.QuickSalesfrm.Focus();
            if (CommonClass.QuickSalesfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.QuickSalesfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void SalesNP_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }

            if (CommonClass.SessionID != 0)
            {
                btnQuickSale.Enabled = true;
                receivePayment_btn.Enabled = true;
                btnRestoPOS.Enabled = true;
            }
            else
            {
                btnQuickSale.Enabled = false;
                receivePayment_btn.Enabled = false;
                btnRestoPOS.Enabled = false;
            }
        }

        private void btnRestoPOS_Click(object sender, EventArgs e)
        {
            if (CommonClass.RestoPOS == null
              || CommonClass.RestoPOS.IsDisposed)
            {
                CommonClass.RestoPOS = new RestoPOS(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.RestoPOS.MdiParent = this.MdiParent;
            CommonClass.RestoPOS.Show();
            CommonClass.RestoPOS.Focus();
            if (CommonClass.RestoPOS.DialogResult == DialogResult.Cancel)
            {
                CommonClass.RestoPOS.Close();
            }
            this.Cursor = Cursors.Default;
        }
    }
}
