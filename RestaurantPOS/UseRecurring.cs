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

namespace AbleRetailPOS
{
    public partial class UseRecurring : Form
    {
        private CommonClass.InvocationSource mInvokeSrc;
        private Form lRecurringForm = null;

        public UseRecurring(CommonClass.InvocationSource pInvokeSrc)
        {
            mInvokeSrc = pInvokeSrc;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UseRecurring_Load(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                string recurringsql;
                switch (mInvokeSrc)
                {
                    case CommonClass.InvocationSource.MONEYIN:
                        recurringsql = @"SELECT r.EntityID, 
                                            p.Name,
                                            r.Frequency,
                                            r.LastPosted,
                                            'temp' AS [Next Due],
                                            m.TotalAllocated AS Amount,
                                            0 AS ShippingMethodID
                                         FROM Recurring r
                                         INNER JOIN MoneyIn m ON m.MoneyInID = r.EntityID
                                         INNER JOIN Profile p ON p.ID = m.ProfileID
                                         WHERE r.TranType = 'MI'";
                        break;
                    case CommonClass.InvocationSource.MONEYOUT:
                        recurringsql = @"SELECT r.EntityID, 
                                            p.Name,
                                            r.Frequency,
                                            r.LastPosted,
                                            'temp' AS [Next Due],
                                            m.TotalAllocated AS Amount,
                                            0 AS ShippingMethodID
                                         FROM Recurring r
                                         INNER JOIN MoneyOut m ON m.MoneyOutID = r.EntityID
                                         INNER JOIN Profile p ON p.ID = m.ProfileID
                                         WHERE r.TranType = 'MO'";
                        break;
                    case CommonClass.InvocationSource.PURCHASE:
                        recurringsql = @"SELECT r.EntityID,
                                            p.Name,
                                            r.Frequency,
                                            r.LastPosted,
                                            'temp' AS [Next Due],
                                            m.GrandTotal AS Amount,
                                            m.ShippingMethodID
                                         FROM Recurring r
                                         INNER JOIN Purchases m ON m.PurchaseID = r.EntityID
                                         INNER JOIN Profile p ON p.ID = m.SupplierID
                                         WHERE r.TranType IN ('PQ','PO','PB','RI')";
                        break;
                    case CommonClass.InvocationSource.SALES:
                        recurringsql = @"SELECT r.EntityID,
                                            p.Name,
                                            r.Frequency,
                                            r.LastPosted,
                                            'temp' AS [Next Due],
                                            m.GrandTotal AS Amount,
                                            m.ShippingMethodID
                                         FROM Recurring r
                                         INNER JOIN Sales m ON m.SalesID = r.EntityID
                                         INNER JOIN Profile p ON p.ID = m.CustomerID
                                         WHERE r.TranType IN ('SQ','SO','SI')";
                        break;
                    case CommonClass.InvocationSource.JOURNALENTRY:
                        recurringsql = @"SELECT r.EntityID,
                                            p.AccountName AS Name,
                                            r.Frequency,
                                            r.LastPosted,
                                            'temp' AS [Next Due],
                                            (m.TotalDebit + m.TotalCredit) AS Amount,
                                            0 AS ShippingMethodID
                                         FROM Recurring r
                                         INNER JOIN RecordJournal m ON m.RecordJournalID = r.EntityID
                                         INNER JOIN RecordJournalLine jl ON jl.RecordJournalID = m.RecordJournalID
                                         INNER JOIN Accounts p ON p.AccountID = jl.AccountID
                                         WHERE r.TranType = 'JE'";
                        break;
                    default:
                        recurringsql = "";
                        break;
                }
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(recurringsql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRecurringList.DataSource = dt;
                dgvRecurringList.Columns[0].Visible = false; // EntityID
                dgvRecurringList.Columns[1].HeaderText = "Name";
                dgvRecurringList.Columns[1].ReadOnly = true;
                dgvRecurringList.Columns[2].HeaderText = "Frequency";
                dgvRecurringList.Columns[2].ReadOnly = true;
                dgvRecurringList.Columns[3].HeaderText = "Last Posted";
                dgvRecurringList.Columns[3].ReadOnly = true;
                dgvRecurringList.Columns[4].HeaderText = "Next Due";
                dgvRecurringList.Columns[4].ReadOnly = true;
                dgvRecurringList.Columns[5].HeaderText = "Amount";
                dgvRecurringList.Columns[5].ReadOnly = true;
                dgvRecurringList.Columns[6].Visible = false; // ShippingMethodID
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvRecurringList.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvr = dgvRecurringList.SelectedRows[0];
                string lEntityID = dgvr.Cells["EntityID"].Value.ToString();
                string lShipID = dgvr.Cells["ShippingMethodID"].Value.ToString();

                if (lRecurringForm != null && !lRecurringForm.IsDisposed)
                    lRecurringForm.Close();

                switch (mInvokeSrc)
                {
                    case CommonClass.InvocationSource.PURCHASE:
                        if (CommonClass.EnterPurchasefrm != null
                            && !CommonClass.EnterPurchasefrm.IsDisposed)
                            CommonClass.EnterPurchasefrm.Close();
                        lRecurringForm = new Purchase.EnterPurchase(CommonClass.InvocationSource.USERECURRING, lEntityID, lShipID);
                        break;
                    case CommonClass.InvocationSource.SALES:
                        if (CommonClass.EnterSalesfrm != null
                            && !CommonClass.EnterSalesfrm.IsDisposed)
                            CommonClass.EnterSalesfrm.Close();
                        lRecurringForm = new Sales.EnterSales(CommonClass.InvocationSource.USERECURRING, lEntityID);
                        break;
                }

                this.Cursor = Cursors.WaitCursor;
                lRecurringForm.MdiParent = this.MdiParent;
                lRecurringForm.Show();
                lRecurringForm.Focus();
                this.Cursor = Cursors.Default;
                Close();
            }
            else
            {
                MessageBox.Show("There is no record selected");
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13) //Enter key is pressed
            {
                foreach (DataGridViewRow row in dgvRecurringList.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtSearch.Text))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }
    }
}
