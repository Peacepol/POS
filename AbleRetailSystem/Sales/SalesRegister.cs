using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Sales
{
    public partial class SalesRegister : Form
    {
        private string CustomerID;
        
        private DataGridViewRow dgRow;
        private int index;
        private string salestype ="" ;
        SqlConnection con = new SqlConnection(CommonClass.ConStr);
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;

        public SalesRegister()
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
        public string SalesType
        {
            get { return salestype; }
            set { salestype = value; }
        }

        private void SalesRegister_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            toOrder_btn.Enabled = CanEdit;
            toInvoice_btn.Enabled = CanEdit;
            delSale_btn.Enabled = CanDelete;
            foreach (DataGridViewColumn column in dgCInvoice.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgOInvoice.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgOrder.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgQuote.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgRetCredit.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgSalesRegAll.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgLayby.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            if (CommonClass.SessionID != 0)
            {
                flowLayoutPanel1.Enabled = true;
                newSale_btn.Enabled = CanAdd;
                dgOInvoice.Enabled = true;
                dgOrder.Enabled = true;
            }
            else
            {
                flowLayoutPanel1.Enabled = false;
                newSale_btn.Enabled = false;
                dgOInvoice.Enabled = false;
                dgOrder.Enabled = false;

            }
            

            sdateTimePicker.Value = DateTime.Now;
            edateTimePicker.Value = DateTime.Now;
            if (this.custBox.Text == "Customer")
            {
                custNameSearch.Visible = true;
                pbCustomer.Visible = true;
            }
            else
            {
                custNameSearch.Visible = false;
                pbCustomer.Visible = false;
            }
            FillSalesTab(TCSaleReg.SelectedIndex);

        }
        public void Populatedg(ref DataGridView dgpopulate, string type)
        {
            DataTable dt = new DataTable();
            string selectSql = "";
            switch (type)
            {
                case "All":
                    selectSql = @"SELECT * FROM Sales INNER JOIN Profile ON Sales.CustomerID = Profile.ID
                                  WHERE TransactionDate BETWEEN  @sdate AND @edate";
                    break;
                case "Quote":
                    selectSql = @"SELECT * FROM Sales INNER JOIN Profile ON Sales.CustomerID = Profile.ID
                                WHERE TransactionDate BETWEEN  @sdate AND @edate AND Sales.SalesType = 'QUOTE'";
                    break;
                case "Order":
                    selectSql = @"SELECT * FROM Sales INNER JOIN Profile ON Sales.CustomerID = Profile.ID 
                                WHERE TransactionDate BETWEEN  @sdate AND @edate AND Sales.SalesType = 'ORDER'";
                    break;
                case "Lay-By":
                    selectSql = @"SELECT * FROM Sales INNER JOIN Profile ON Sales.CustomerID = Profile.ID 
                                WHERE TransactionDate BETWEEN  @sdate AND @edate AND Sales.SalesType = 'LAY-BY'";
                    break;
                case "Open Invoice":
                    selectSql = @"SELECT * FROM Sales INNER JOIN Profile ON Sales.CustomerID = Profile.ID
                                WHERE TransactionDate BETWEEN  @sdate AND @edate AND Sales.SalesType IN ('INVOICE', 'SINVOICE')
                                AND Sales.InvoiceStatus = 'Open'";
                    break;
                case "Return Credit":
                    selectSql = @"SELECT * FROM Sales INNER JOIN Profile ON Sales.CustomerID = Profile.ID
                                WHERE TransactionDate BETWEEN  @sdate AND @edate AND Sales.TotalDue < 0";
                    break;
                case "Closed Invoice":
                    selectSql = @"SELECT * FROM Sales INNER JOIN Profile ON Sales.CustomerID = Profile.ID
                                WHERE TransactionDate BETWEEN  @sdate AND @edate AND Sales.SalesType IN ('INVOICE', 'SINVOICE')
                                AND Sales.InvoiceStatus = 'Closed'";
                    break;
            }

                if (this.custBox.Text == "Customer")
                {
                    selectSql += " AND Profile.Name = '" + custNameSearch.Text + "'";
                }




            DateTime sdate = Convert.ToDateTime(sdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
            DateTime edate = Convert.ToDateTime(edateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();


            Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@sdate", sdate);
                param.Add("@edate", edate);
               
                CommonClass.runSql(ref dt, selectSql, param);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgpopulate.Rows.Add();
                    dgpopulate.Rows[x].Cells[0].Value = dr["SalesID"].ToString();
                    dgpopulate.Rows[x].Cells[1].Value = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToLocalTime().ToShortDateString();
                    dgpopulate.Rows[x].Cells[2].Value = dr["SalesNumber"].ToString();
                    dgpopulate.Rows[x].Cells[3].Value = dr["CustomerPONumber"].ToString();
                    dgpopulate.Rows[x].Cells[4].Value = dr["Name"].ToString();
                    dgpopulate.Rows[x].Cells[5].Value = dr["GrandTotal"].ToString();
                    dgpopulate.Rows[x].Cells[6].Value = dr["TotalDue"].ToString();
                    dgpopulate.Rows[x].Cells[7].Value = (dr["PromiseDate"]== DBNull.Value ? null : Convert.ToDateTime(dr["PromiseDate"].ToString()).ToLocalTime().ToShortDateString());
                    dgpopulate.Rows[x].Cells[8].Value = dr["SalesType"].ToString();     
                    dgpopulate.Rows[x].Cells[10].Value = dr["ClosedDate"].ToString() != ""? Convert.ToDateTime(dr["ClosedDate"].ToString()).ToLocalTime().ToShortDateString() : "";

                    // dgpopulate.Rows[x].Cells[8].Value = dr["CreditAmount"].ToString();
                    //  dgpopulate.Rows[x].Cells[9].Value = dr["DateClosed"].ToString();
                }
                dt.Clear();     
        }

        private void newSale_btn_Click(object sender, EventArgs e)
        {        
            if (newSale_btn.Text == "New Order")
            {
                SalesType = "ORDER";
            }
            else if(newSale_btn.Text == "New Invoice")
            {
                SalesType = "INVOICE";
            }
            else if (newSale_btn.Text == "New Quote")
            {
                SalesType = "QUOTE";
            }

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

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                CustomerID = lProfile[0];
                custNameSearch.Text = lProfile[2];
            }
            else
            {
                // MessageBox.Show("Dialog not OK");
            }
        }

        private void TCSaleReg_Selected(object sender, TabControlEventArgs e)
        {
            FillSalesTab(TCSaleReg.SelectedIndex);
        }

        private void FillSalesTab(int pTabIndex)
        {
            switch (pTabIndex)
            {
                case 0:
                    dgSalesRegAll.Rows.Clear();
                    Populatedg(ref dgSalesRegAll, "All");
                    toOrder_btn.Visible = false;
                    toInvoice_btn.Visible = false;
                    delSale_btn.Visible = false;
                    receivePay_btn.Visible = false;
                    newSale_btn.Text = "New Sale";
                    break;
                case 1:
                    dgQuote.Rows.Clear();
                    Populatedg(ref dgQuote, "Quote");
                    if (dgQuote.RowCount > 0)
                    {
                        toOrder_btn.Visible = true;
                        toInvoice_btn.Visible = true;
                        delSale_btn.Visible = true;
                    }
                    receivePay_btn.Visible = false;
                    newSale_btn.Text = "New Quote";

                    break;
                case 2:
                    dgOrder.Rows.Clear();
                    Populatedg(ref dgOrder, "Order");
                    toOrder_btn.Visible = false;
                    if (dgOrder.RowCount > 0)
                    {
                        toInvoice_btn.Visible = true;
                        receivePay_btn.Visible = true;
                    }
                    delSale_btn.Visible = false;
                    newSale_btn.Text = "New Order";
                    break;
                case 3:
                    dgLayby.Rows.Clear();
                    Populatedg(ref dgLayby, "Lay-By");
                    toOrder_btn.Visible = false;
                    if (dgLayby.RowCount > 0)
                    {
                        toInvoice_btn.Visible = true;
                        receivePay_btn.Visible = true;
                    }
                    delSale_btn.Visible = false;
                    newSale_btn.Text = "New Lay-By";
                    break;
                case 4:
                    dgOInvoice.Rows.Clear();
                    Populatedg(ref dgOInvoice, "Open Invoice");
                    toOrder_btn.Visible = false;
                    toInvoice_btn.Visible = false;
                    delSale_btn.Visible = false;
                    if (dgOInvoice.RowCount > 0)
                        receivePay_btn.Visible = true;
                    newSale_btn.Text = "New Invoice";
                    
                    break;
                case 5:
                    dgRetCredit.Rows.Clear();
                    Populatedg(ref dgRetCredit, "Return Credit");
                    toOrder_btn.Visible = false;
                    toInvoice_btn.Visible = false;
                    delSale_btn.Visible = false;
                    receivePay_btn.Visible = false;
                    newSale_btn.Text = "New Sale";
                    break;

                case 6:
                    dgCInvoice.Rows.Clear();
                    Populatedg(ref dgCInvoice, "Closed Invoice");
                    toOrder_btn.Visible = false;
                    toInvoice_btn.Visible = false;
                    delSale_btn.Visible = false;
                    receivePay_btn.Visible = false;
                    newSale_btn.Text = "New Sale";
                    break;
            }
        }

        private void edateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FillSalesTab(TCSaleReg.SelectedIndex);
        }

        private void sdateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FillSalesTab(TCSaleReg.SelectedIndex);
        }

        private void custBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.custBox.Text == "Customer")
            {
                custNameSearch.Visible = true;
                pbCustomer.Visible = true;
            }
            else
            {
                custNameSearch.Visible = false;
                pbCustomer.Visible = false;
            }
        }

        private void pbCustomer_Click_1(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

        private void custNameSearch_TextChanged(object sender, EventArgs e)
        {
            FillSalesTab(TCSaleReg.SelectedIndex);
        }

        private void dgSalesRegAll_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            dgSalesRegAll.Rows[e.RowIndex].Selected = true;

            string sID = dgSalesRegAll.Rows[e.RowIndex].Cells[0].Value.ToString();
            SalesType = dgSalesRegAll.Rows[e.RowIndex].Cells[08].Value.ToString();
            if (SalesType != "SINVOICE")
            {
                if (CommonClass.EnterSalesfrm != null
               && !CommonClass.EnterSalesfrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
                CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, sID);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                CommonClass.EnterSalesfrm.Show();
                CommonClass.EnterSalesfrm.Focus();
                if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterSalesfrm.Close();
                }
            }else //HISTORICAL INVOICE
            {
                //if (CommonClass.ARBalanceEntryFrm == null
                //|| CommonClass.ARBalanceEntryFrm.IsDisposed)
                //{
                    
                //}
                CommonClass.ARBalanceEntryFrm = new ARBalanceEntry("Accounts Receivable Starting Balances", "", sID);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ARBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.ARBalanceEntryFrm.Show();
                CommonClass.ARBalanceEntryFrm.Focus();
                if (CommonClass.ARBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ARBalanceEntryFrm.Close();
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void dgQuote_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            dgQuote.Rows[e.RowIndex].Selected = true;
            string sID = dgQuote.Rows[e.RowIndex].Cells[0].Value.ToString();
            SalesType = "QUOTE";

            if (CommonClass.EnterSalesfrm == null
                || CommonClass.EnterSalesfrm.IsDisposed)
            {
                CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, sID);
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

        private void dgOrder_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            dgOrder.Rows[e.RowIndex].Selected = true;
            string sID = dgOrder.Rows[e.RowIndex].Cells[0].Value.ToString();
            SalesType = "ORDER";

            if (CommonClass.EnterSalesfrm == null
                || CommonClass.EnterSalesfrm.IsDisposed)
            {
                CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, sID);
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
        private void dgLayby_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            dgLayby.Rows[e.RowIndex].Selected = true;
            string sID = dgLayby.Rows[e.RowIndex].Cells[0].Value.ToString();
            SalesType = "LAY-BY";

            if (CommonClass.EnterSalesfrm == null
                || CommonClass.EnterSalesfrm.IsDisposed)
            {
                CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, sID);
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
        private void dgOInvoice_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            dgOInvoice.Rows[e.RowIndex].Selected = true;
            string sID = dgOInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
            SalesType = dgOInvoice.Rows[e.RowIndex].Cells[8].Value.ToString();
            if (SalesType != "SINVOICE")
            {
                if (CommonClass.EnterSalesfrm == null
                || CommonClass.EnterSalesfrm.IsDisposed)
                {
                    CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, sID);
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                CommonClass.EnterSalesfrm.Show();
                CommonClass.EnterSalesfrm.Focus();
                if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterSalesfrm.Close();
                }
            }else //HISTORICAL INVOICE
            {
             
                CommonClass.ARBalanceEntryFrm = new ARBalanceEntry("Accounts Receivable Starting Balances", "", sID);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ARBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.ARBalanceEntryFrm.Show();
                CommonClass.ARBalanceEntryFrm.Focus();
                if (CommonClass.ARBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ARBalanceEntryFrm.Close();
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void dgRetCredit_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            dgRetCredit.Rows[e.RowIndex].Selected = true;
            string sID = dgRetCredit.Rows[e.RowIndex].Cells[0].Value.ToString();
            SalesType = dgRetCredit.Rows[e.RowIndex].Cells[8].Value.ToString();
            if (SalesType != "SINVOICE")
            {
                if (CommonClass.EnterSalesfrm == null
                    || CommonClass.EnterSalesfrm.IsDisposed)
                {
                    CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, sID);
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                CommonClass.EnterSalesfrm.Show();
                CommonClass.EnterSalesfrm.Focus();
                if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterSalesfrm.Close();
                }
            }else //HISTORICAL INVOICE
            {
               // if (CommonClass.ARBalanceEntryFrm == null
               //|| CommonClass.ARBalanceEntryFrm.IsDisposed)
               // {
                    
               // }
                CommonClass.ARBalanceEntryFrm = new ARBalanceEntry("Accounts Receivable Starting Balances", "", sID);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ARBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.ARBalanceEntryFrm.Show();
                CommonClass.ARBalanceEntryFrm.Focus();
                if (CommonClass.ARBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ARBalanceEntryFrm.Close();
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void dgCInvoice_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            dgCInvoice.Rows[e.RowIndex].Selected = true;
            string sID = dgCInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
            SalesType = dgCInvoice.Rows[e.RowIndex].Cells[8].Value.ToString();
            if (SalesType != "SINVOICE")
            {
                if (CommonClass.EnterSalesfrm == null
                    || CommonClass.EnterSalesfrm.IsDisposed)
                {
                    CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, sID);
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                CommonClass.EnterSalesfrm.Show();
                CommonClass.EnterSalesfrm.Focus();
                if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterSalesfrm.Close();
                }
            }else //HISTORICAL INVOICE
            {
               // if (CommonClass.ARBalanceEntryFrm == null
               //|| CommonClass.ARBalanceEntryFrm.IsDisposed)
               // {
                   
               // }
                CommonClass.ARBalanceEntryFrm = new ARBalanceEntry("Accounts Receivable Starting Balances", "", sID);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.ARBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.ARBalanceEntryFrm.Show();
                CommonClass.ARBalanceEntryFrm.Focus();
                if (CommonClass.ARBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.ARBalanceEntryFrm.Close();
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void dgSalesRegAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgSalesRegAll.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
        }
        private void dgQuote_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgQuote.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
        }

        private void dgOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgOrder.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
        }
        private void dgLayby_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgLayby.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
        }

        private void dgOInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgOInvoice.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
        }

        private void dgRetCredit_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgRetCredit.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
        }

        private void dgCInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgCInvoice.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
        }

        private void toOrder_btn_Click(object sender, EventArgs e)
        {
            //Otype = true;
            if(index >= 0)
            {
                string Sid = dgQuote.CurrentRow.Cells[0].Value.ToString();
                SalesType = "ORDER";
                //Setsalestype(Sid.ToString());
                if (CommonClass.EnterSalesfrm == null
                  || CommonClass.EnterSalesfrm.IsDisposed)
                {
                    CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.CHANGETO, Sid.ToString(), SalesType);
                }
                Cursor = Cursors.WaitCursor;
                CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                CommonClass.EnterSalesfrm.Show();
                CommonClass.EnterSalesfrm.Focus();
                if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterSalesfrm.Close();
                }
                Cursor = Cursors.Default;
            }
        }

        private void toInvoice_btn_Click(object sender, EventArgs e)
        {
            //Itype = true;
            string Sid ="";
            if(index >= 0)
            {
                if (TCSaleReg.SelectedIndex == 1)//your specific tabname
                {
                    Sid = dgQuote.Rows[index].Cells[0].Value.ToString();
                }
                else if(TCSaleReg.SelectedIndex == 2)
                {
                    Sid = dgOrder.Rows[index].Cells[0].Value.ToString();
                }

                //Setsalestype(Sid.ToString());
                SalesType = "INVOICE";
                if (CommonClass.EnterSalesfrm == null
                    || CommonClass.EnterSalesfrm.IsDisposed)
                {
                    CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.CHANGETO, Sid.ToString(), SalesType);
                }
                Cursor = Cursors.WaitCursor;
                CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                CommonClass.EnterSalesfrm.Show();
                CommonClass.EnterSalesfrm.Focus();
                if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterSalesfrm.Close();
                }
                Cursor = Cursors.Default;
            }
        }

        private void receivePay_btn_Click(object sender, EventArgs e)
        {
            string Sid = "";
            if(index >= 0)
            {
                if (TCSaleReg.SelectedIndex == 2)//your specific tabname
                {
                    Sid = dgOrder.Rows[index].Cells[0].Value.ToString();
                }
                else if (TCSaleReg.SelectedIndex == 3)
                {
                    Sid = dgOInvoice.Rows[index].Cells[0].Value.ToString();
                }
                if (CommonClass.SRPaymentsfrm != null
                    && !CommonClass.SRPaymentsfrm.IsDisposed)
                {
                    CommonClass.SRPaymentsfrm.Close();
                }
                CommonClass.SRPaymentsfrm = new SalesReceivePayment(CommonClass.InvocationSource.REGISTER, Sid);
                Cursor = Cursors.WaitCursor;
                CommonClass.SRPaymentsfrm.MdiParent = this.MdiParent;
                CommonClass.SRPaymentsfrm.Show();
                CommonClass.SRPaymentsfrm.Focus();
                if (CommonClass.SRPaymentsfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SRPaymentsfrm.Close();
                }
                Cursor = Cursors.Default;
            }
        }

        private void delSale_btn_Click(object sender, EventArgs e)
        {
            string Sid = "";
            Sid = dgQuote.Rows[index].Cells[0].Value.ToString();

                string sql = "DELETE FROM SalesLines where SalesID = " + Sid+ "; DELETE FROM Sales where SalesID = " + Sid;

            int rowsAffected = CommonClass.runSql(sql);
                if(rowsAffected > 0)
                {
                    MessageBox.Show("Quote succesfully deleted.", "Information");
                    FillSalesTab(TCSaleReg.SelectedIndex);
                }
        
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgRetCredit_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Credit Amount
                 && e.RowIndex != dgRetCredit.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgCInvoice_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Credit Amount
                 && e.RowIndex != dgRetCredit.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgOInvoice_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Credit Amount
                 && e.RowIndex != dgRetCredit.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Credit Amount
                 && e.RowIndex != dgRetCredit.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgQuote_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Credit Amount
                 && e.RowIndex != dgRetCredit.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgSalesRegAll_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Credit Amount
                 && e.RowIndex != dgRetCredit.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgOInvoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
