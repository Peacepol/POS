using CrystalReportsDataDefModelLib;
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

namespace AbleRetailPOS.References
{
    public partial class Reminder : Form
    {
        private int index;
        private string lEntityID = "";
        private string lShipID = "";
        public Reminder()
        {
            InitializeComponent();
        }

        private void Reminder_Load(object sender, EventArgs e)
        {
            
            DateTime tdate = DateTime.Now.ToUniversalTime();
            SqlConnection con = null;
            try
            {
                string recurringsql;
                recurringsql = @"SELECT m.Memo ,'temp' AS DueDate,
                                      m.GrandTotal AS Amount , 
                                      r.TranType , r.EntityID ,m.ShippingMethodID ,r.EndDate 
                                      FROM Recurring r
                                      INNER JOIN Sales m ON m.SalesID = r.EntityID 
                                      WHERE r.NotifyDate <= @tdate AND r.TranType IN ('SQ','SO','SI')
                                UNION
                                SELECT  m.Memo,'temp' AS DueDate,
                                        m.GrandTotal AS Amount ,
                                        r.TranType , r.EntityID ,m.ShippingMethodID ,r.EndDate 
                                        FROM Recurring r
                                        INNER JOIN Purchases m ON m.PurchaseID = r.EntityID 
                                        WHERE r.NotifyDate <= @tdate AND r.TranType IN ('PQ','PO','PB','RI') 
                                UNION
                                SELECT m.Memo,'temp' AS DueDate,
                                       (m.TotalDebit + m.TotalCredit) AS Amount,
                                        r.TranType , r.EntityID,  0 AS ShippingMethodID ,r.EndDate 
                                       FROM Recurring r
                                       INNER JOIN RecordJournal m ON m.RecordJournalID = r.EntityID
                                       INNER JOIN RecordJournalLine jl ON jl.RecordJournalID = m.RecordJournalID 
                                       WHERE r.NotifyDate <= @tdate AND r.TranType = 'JE'";


                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(recurringsql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@tdate", tdate);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgReminder.Rows.Add();
                    if ((Convert.ToDateTime(dr["EndDate"].ToString())) >= DateTime.Today.ToUniversalTime())
                    {
                        dgReminder.Rows[x].Cells[0].Value = dr["Memo"].ToString();
                        //   dgReminder.Rows[x].Cells[1].Value = dr[""].ToString();
                        dgReminder.Rows[x].Cells[2].Value = dr["Amount"].ToString();
                        dgReminder.Rows[x].Cells[3].Value = dr["TranType"].ToString();
                        dgReminder.Rows[x].Cells[4].Value = dr["EntityID"].ToString();
                        dgReminder.Rows[x].Cells[5].Value = dr["ShippingMethodID"].ToString();
                    }
                }
                  
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

        private void dgReminder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgReminder.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
             lEntityID = dgReminder.Rows[e.RowIndex].Cells["EntityID"].Value.ToString();
             lShipID = dgReminder.Rows[e.RowIndex].Cells["ShippingMethodID"].Value.ToString();
        }

        private void dgReminder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgReminder.Rows[e.RowIndex].Selected = true;
             lEntityID = dgReminder.Rows[e.RowIndex].Cells["EntityID"].Value.ToString();
             lShipID = dgReminder.Rows[e.RowIndex].Cells["ShippingMethodID"].Value.ToString();
            Recurring();
        }
        private void Recurring()
        {
            if (dgReminder.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvr = dgReminder.SelectedRows[0];
                switch (dgvr.Cells[3].Value.ToString())
                {
                    case "PQ":
                    case "PO":
                    case "PB":
                    case "RI":
                        this.Cursor = Cursors.WaitCursor;
                        if (CommonClass.EnterPurchasefrm != null
                                && !CommonClass.EnterPurchasefrm.IsDisposed)
                            CommonClass.EnterPurchasefrm.Close();
                        CommonClass.EnterPurchasefrm = new Purchase.EnterPurchase(CommonClass.InvocationSource.REMINDER, lEntityID, lShipID);
                        CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                        CommonClass.EnterPurchasefrm.Show();
                        CommonClass.EnterPurchasefrm.Focus();
                        if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                        {
                            CommonClass.EnterPurchasefrm.Close();
                        }
                        this.Cursor = Cursors.Default;
                        break;

                    case "SI":
                    case "SO":
                    case "SQ":
                        this.Cursor = Cursors.WaitCursor;
                        if (CommonClass.EnterSalesfrm != null
                                && !CommonClass.EnterSalesfrm.IsDisposed)
                            CommonClass.EnterSalesfrm.Close();
                        CommonClass.EnterSalesfrm = new Sales.EnterSales(CommonClass.InvocationSource.REMINDER, lEntityID, lShipID);
                        CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                        CommonClass.EnterSalesfrm.Show();
                        CommonClass.EnterSalesfrm.Focus();
                        if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                        {
                            CommonClass.EnterSalesfrm.Close();
                        }
                        this.Cursor = Cursors.Default;
                        break;
                }
            }
        }

        private void record_btn_Click(object sender, EventArgs e)
        {
            Recurring();
        }
        void UpdateLastPosted()
        {
            string updateSQL = "";
            SqlConnection con = new SqlConnection(CommonClass.ConStr);
            SqlCommand cmd = new SqlCommand(updateSQL, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
