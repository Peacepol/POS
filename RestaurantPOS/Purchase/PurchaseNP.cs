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

namespace AbleRetailPOS.Purchase
{
    public partial class PurchaseNP : Form
    {
        public PurchaseNP()
        {
            InitializeComponent();
        }

        private void PurchaseNP_Load(object sender, EventArgs e)
        {

        }

        private void btn_PurchaseEnter_Click(object sender, EventArgs e)
        {
            if (CommonClass.EnterPurchasefrm == null
            || CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.SELF);
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
                
             this.Cursor = Cursors.Default;
        }

        private void btn_PurchaseReg_Click(object sender, EventArgs e)
        {
            if (CommonClass.PurchaseRegfrm == null
            || CommonClass.PurchaseRegfrm.IsDisposed)
                {
                    CommonClass.PurchaseRegfrm = new PurchaseRegister();
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PurchaseRegfrm.MdiParent = this.MdiParent;
                CommonClass.PurchaseRegfrm.Show();
                CommonClass.PurchaseRegfrm.Focus();
                if (CommonClass.PurchaseRegfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PurchaseRegfrm.Close();
                }
            this.Cursor = Cursors.Default;
        }

        private void btn_PurchasePayment_Click(object sender, EventArgs e)
        {
            if (CommonClass.PRPaymentsfrm == null
                || CommonClass.PRPaymentsfrm.IsDisposed)
            {
                CommonClass.PRPaymentsfrm = new PurchasePayments(CommonClass.InvocationSource.SELF);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PRPaymentsfrm.MdiParent = this.MdiParent;
            CommonClass.PRPaymentsfrm.Show();
            CommonClass.PRPaymentsfrm.Focus();
            if (CommonClass.PRPaymentsfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PRPaymentsfrm.Close();
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

        private void btnReplenish_Click(object sender, EventArgs e)
        {
            CommonClass.Replenishmentfrm = new Replenishment();
            this.Cursor = Cursors.WaitCursor;
            CommonClass.Replenishmentfrm.MdiParent = this.MdiParent;
            CommonClass.Replenishmentfrm.Show();
            CommonClass.Replenishmentfrm.Focus();
            if (CommonClass.Replenishmentfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.Replenishmentfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }
    }
}
