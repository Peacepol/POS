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

namespace AbleRetailPOS.Purchase
{
    public partial class PurchaseRegister : Form
    {
        private string SupplierID;
        private DataTable dt;

        private string purchasetype = "";
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        private bool CanReceive = false;
        SqlConnection con = new SqlConnection(CommonClass.ConStr);

        public PurchaseRegister()
        {
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                outx = false;
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

        private void newPurchase_btn_Click(object sender, EventArgs e)
        {
            if (newPurchase_btn.Text == "New Order")
            {
                PurchaseType = "ORDER";
            }
            else if (newPurchase_btn.Text == "New Bill")
            {
                PurchaseType = "BILL";
            }
            else if (newPurchase_btn.Text == "New Quote")
            {
                PurchaseType = "QUOTE";
            }
            else
            {
                PurchaseType = "ORDER";
            }

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

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void ShowSupplierAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                SupplierID = lProfile[0];
                suppNameSearch.Text = lProfile[2];
            }
        }

        private void PurchaseRegister_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            //foreach (DataGridViewColumn column in dgCompletedOrders.Columns)
            //{
            //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            //foreach (DataGridViewColumn column in dgActiveOrders.Columns)
            //{
            //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            //foreach (DataGridViewColumn column in dgNewOrder.Columns)
            //{
            //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            
            //foreach (DataGridViewColumn column in dgReceivedOrders.Columns)
            //{
            //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            sdateTimePicker.Value = DateTime.Now;
            edateTimePicker.Value = DateTime.Now; 
            if (this.custBox.Text == "Supplier")
            {
                suppNameSearch.Visible = true;
                pbSupplier.Visible = true;
            }
            else
            {
                suppNameSearch.Visible = false;
                pbSupplier.Visible = false;
            }
            ApplyButtonRights();
            FillPurchaseTab(TCPurchaseReg.SelectedIndex);
        }

        private void ApplyButtonRights()
        {
            Dictionary<string, Boolean> lDic;
           
            //FOR RECEIVE ITEMS
            if (CommonClass.UserAccess.TryGetValue(btnReceive.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        btnReceive.Visible = true;
                    }
                    else
                    {
                        btnReceive.Visible = false;
                        
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        btnReceive.Enabled = true;
                        CanReceive = true;
                    }
                    else
                    {
                        btnReceive.Enabled = false;
                        CanReceive = false;
                    }
                }
            }

            //FOR NEW PO
            if (CommonClass.EnterPurchasefrm == null
               || CommonClass.EnterPurchasefrm.IsDisposed)
            {
                CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.SELF);
            }


            if (CommonClass.UserAccess.TryGetValue(CommonClass.EnterPurchasefrm.Text, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("Add", out valstr))
                {
                    if (valstr == true)
                    {
                        newPurchase_btn.Visible = true;
                        newPurchase_btn.Enabled = true;
                    }
                    else
                    {
                        newPurchase_btn.Visible = false;
                    }
                }
              
            }
        }

        private void FillPurchaseTab(int pTabIndex)
        {
            bool lEnable = false;
            switch (pTabIndex)
            {
                case 0:
                    dgPurchaseRegAll.Rows.Clear();
                    Populatedg(ref dgPurchaseRegAll, "All");
                    btnReceive.Visible = lEnable;
                    if (dgPurchaseRegAll.RowCount > 0)
                    {
                        btnReceive.Visible = CanReceive;
                    }
                                      
                    break;
            
                case 1:
                    dgNewOrder.Rows.Clear();
                    Populatedg(ref dgNewOrder, "New Orders");
                    lEnable = false;
                    btnReceive.Visible = false;
                   
                    break;
                case 2:
                    dgActiveOrders.Rows.Clear();
                    Populatedg(ref dgActiveOrders, "Active Orders");
                             
                    btnReceive.Visible = false;
                    if (dgActiveOrders.RowCount > 0)
                    {
                        btnReceive.Visible = CanReceive;
                    }
                       
                    break;
                case 3:
                    dgReceivedOrders.Rows.Clear();
                    Populatedg(ref dgReceivedOrders, "Received Orders");
                  
                    btnReceive.Visible = false;
                    if (dgReceivedOrders.RowCount > 0)
                    {
                        btnReceive.Visible = CanReceive;
                    }
                       
                    newPurchase_btn.Text = "New Purchase";
                    break;
                case 4:
                    dgCompletedOrders.Rows.Clear();
                    Populatedg(ref dgCompletedOrders, "Completed Orders");                  
                  
                    break;
            }
            newPurchase_btn.Text = "New Purchase";

            
        }

        private DataGridViewRow GetCurrentRow()
        {
            DataGridViewRow DgRow = null;
            switch (TCPurchaseReg.SelectedIndex)
            {
                case 0:
                    if(dgPurchaseRegAll.CurrentRow.Index >= 0)
                    {
                        DgRow = dgPurchaseRegAll.CurrentRow;
                    }                   
                    break;
               
                case 1:
                    if (dgNewOrder.CurrentRow.Index >= 0)
                    {
                        DgRow = dgNewOrder.CurrentRow;
                    }
                    break;
                case 2:
                    if (dgActiveOrders.CurrentRow.Index >= 0)
                    {
                        DgRow = dgActiveOrders.CurrentRow;
                    }
                    break;
                case 3:
                    if (dgReceivedOrders.CurrentRow.Index >= 0)
                    {
                        DgRow = dgReceivedOrders.CurrentRow;
                    }
                    break;
                case 4:
                    if (dgCompletedOrders.CurrentRow.Index >= 0)
                    {
                        DgRow = dgCompletedOrders.CurrentRow;
                    }
                    break;
            }
            return DgRow;
        }

        public void Populatedg(ref DataGridView dgpopulate, string type)
        {
            SqlConnection con_ = new SqlConnection(CommonClass.ConStr);
            string selectSql = "";
            switch (type)
            {
                case "All":
                    selectSql = @"SELECT * FROM Purchases INNER JOIN Profile ON Purchases.SupplierID = Profile.ID
                                WHERE TransactionDate BETWEEN @sdate AND @edate";
                    break;
                case "New Orders":
                    selectSql = @"SELECT * FROM Purchases INNER JOIN Profile ON Purchases.SupplierID = Profile.ID 
                                WHERE TransactionDate BETWEEN @sdate AND @edate AND POStatus = 'New'";
                    break;
                case "Active Orders":
                    selectSql = @"SELECT * FROM Purchases INNER JOIN Profile ON Purchases.SupplierID = Profile.ID
                                WHERE TransactionDate BETWEEN @sdate AND @edate AND POStatus = 'Active'";
                    break;
                case "Received Orders":
                    selectSql = @"SELECT * FROM Purchases INNER JOIN Profile ON Purchases.SupplierID = Profile.ID
                                WHERE TransactionDate BETWEEN @sdate AND @edate AND POStatus = 'Backordered'";
                    break;
                case "Completed Orders":
                    selectSql = @"SELECT * FROM Purchases INNER JOIN Profile ON Purchases.SupplierID = Profile.ID
                                WHERE ClosedDate BETWEEN @sdate AND @edate AND POStatus = 'Completed'";
                    break;
            }
            try
            {
                if (this.custBox.Text == "Supplier")
                {
                    selectSql += " AND Profile.Name = '" + suppNameSearch.Text + "'";
                }

                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                cmd_.Parameters.Clear();
                cmd_.CommandType = CommandType.Text;
                con_.Open();
                
                DateTime sdate = Convert.ToDateTime(sdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                cmd_.Parameters.AddWithValue("@sdate", sdate);
                cmd_.Parameters.AddWithValue("@edate", edate);

                SqlDataAdapter da = new SqlDataAdapter();
                dt = new DataTable();
                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgpopulate.Rows.Add();
                    dgpopulate.Rows[x].Cells[0].Value = dr["PurchaseID"].ToString();
                    dgpopulate.Rows[x].Cells[1].Value = Convert.ToDateTime(dr["TransactionDate"]).ToLocalTime().ToShortDateString();
                    dgpopulate.Rows[x].Cells[2].Value = dr["PurchaseNumber"].ToString();
                    dgpopulate.Rows[x].Cells[3].Value = dr["SupplierINVNumber"].ToString();
                    dgpopulate.Rows[x].Cells[4].Value = dr["Name"].ToString();
                    dgpopulate.Rows[x].Cells[5].Value = dr["GrandTotal"].ToString();
                    dgpopulate.Rows[x].Cells[6].Value = dr["TotalDue"].ToString();
                    dgpopulate.Rows[x].Cells[7].Value = (dr["PromiseDate"] == null ? null :Convert.ToDateTime(dr["PromiseDate"]).ToLocalTime().ToShortDateString());
                    dgpopulate.Rows[x].Cells[8].Value = dr["POStatus"].ToString();
                    dgpopulate.Rows[x].Cells[9].Value = dr["ShippingMethodID"].ToString();
                    if(dr["ClosedDate"].ToString() != "")
                    {
                        dgpopulate.Rows[x].Cells[10].Value =  Convert.ToDateTime(dr["ClosedDate"]).ToLocalTime().ToShortDateString();
                    }
                    
                    dgpopulate.Rows[x].Cells[11].Value = dr["SupplierID"].ToString();
                }

                if (dt.Rows.Count > 0)
                {
                    dgpopulate.Rows[0].Selected = true;
                }

                dt.Clear();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void TCPurchaseReg_Selected(object sender, TabControlEventArgs e)
        {
            FillPurchaseTab(TCPurchaseReg.SelectedIndex);
        }

        private void edateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FillPurchaseTab(TCPurchaseReg.SelectedIndex);
        }

        private void sdateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FillPurchaseTab(TCPurchaseReg.SelectedIndex);
        }

        private void custBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.custBox.Text == "Supplier")
            {
                suppNameSearch.Visible = true;
                pbSupplier.Visible = true;
            }
            else
            {
                suppNameSearch.Visible = false;
                pbSupplier.Visible = false;
            }
        }

        private void pbSupplier_Click(object sender, EventArgs e)
        {
            ShowSupplierAccounts();
        }

        private void suppNameSearch_TextChanged(object sender, EventArgs e)
        {
            FillPurchaseTab(TCPurchaseReg.SelectedIndex);
        }

        private void dgRetCredit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgReceivedOrders.Rows[e.RowIndex].Selected = true;
            string Pid = dgReceivedOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ShippingID = dgReceivedOrders.Rows[e.RowIndex].Cells[09].Value.ToString();
            PurchaseType = dgPurchaseRegAll.Rows[e.RowIndex].Cells[08].Value.ToString();

           

            if (PurchaseType != "SBILL")
            {
                if (CommonClass.EnterPurchasefrm == null
                || CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, Pid, ShippingID, purchasetype);
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
            }
            else //HISTORICAL BILL
            {
                //if (CommonClass.APBalanceEntryFrm == null
                //|| CommonClass.APBalanceEntryFrm.IsDisposed)
                //{
                    
                //}
                CommonClass.APBalanceEntryFrm = new APBalanceEntry("Accounts Payable Starting Balances", "", Pid);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.APBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.APBalanceEntryFrm.Show();
                CommonClass.APBalanceEntryFrm.Focus();
                if (CommonClass.APBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.APBalanceEntryFrm.Close();
                }

            }



            this.Cursor = Cursors.Default;
        }

        private void dgPurchaseRegAll_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgPurchaseRegAll.Rows[e.RowIndex].Selected = true;
            string Pid = dgPurchaseRegAll.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ShippingID = dgPurchaseRegAll.Rows[e.RowIndex].Cells[09].Value.ToString();
            PurchaseType = dgPurchaseRegAll.Rows[e.RowIndex].Cells[08].Value.ToString();
            if(PurchaseType != "SBILL")
            {
               
             
                if (CommonClass.EnterPurchasefrm != null
                && !CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
                CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, Pid, ShippingID, purchasetype);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
            }else //HISTORICAL BILL
            {
              
                CommonClass.APBalanceEntryFrm = new APBalanceEntry("Accounts Payable Starting Balances", "", Pid);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.APBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.APBalanceEntryFrm.Show();
                CommonClass.APBalanceEntryFrm.Focus();
                if (CommonClass.APBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.APBalanceEntryFrm.Close();
                }

            }            
            this.Cursor = Cursors.Default;
        }

      

        private void dgOrder_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string PurchaseId = dgNewOrder.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ShippingID = dgNewOrder.Rows[e.RowIndex].Cells[09].Value.ToString();
            
            PurchaseType = "ORDER";

            if (CommonClass.EnterPurchasefrm == null
                || CommonClass.EnterPurchasefrm.IsDisposed)
            {
                CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, PurchaseId, ShippingID, purchasetype);
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
       
        private void toOrder_btn_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgr = GetCurrentRow();//your specific tabname
            if (dgr != null)
            {
                string pID = "";
                string ShippingID = "";
                pID = dgr.Cells[0].Value.ToString();
                ShippingID = dgr.Cells[09].Value.ToString();
                PurchaseType = "ORDER";

                if (CommonClass.EnterPurchasefrm == null
                  || CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.CHANGETO, pID, ShippingID, purchasetype);
                }
                Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
                Cursor = Cursors.Default;
            }
        }

        public string PurchaseType
        {
            get { return purchasetype; }
            set { purchasetype = value; }
        }

        private void tobill_btn_Click(object sender, EventArgs e)
        {
           
            DataGridViewRow dgr = GetCurrentRow();//your specific tabname
            if (dgr != null)
            {
                string pID = "";
                string ShippingID = "";
                pID = dgr.Cells[0].Value.ToString();
                ShippingID = dgr.Cells[09].Value.ToString();
                PurchaseType = "BILL";

                if (CommonClass.EnterPurchasefrm == null
                  || CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.CHANGETO, pID, ShippingID, purchasetype);
                }
                Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
                Cursor = Cursors.Default;
            }
           
        }

        private void delPurchase_btn_Click(object sender, EventArgs e)
        {
          
        }

        private void receiveItem_btn_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgr = GetCurrentRow();//your specific tabname
            if (dgr != null)
            {
                string pID = "";
                string ShippingID = "";
                pID = dgr.Cells[0].Value.ToString();
                ShippingID = dgr.Cells[09].Value.ToString();
                PurchaseType = "RECEIVE ITEMS";

                if (CommonClass.EnterPurchasefrm == null
                    || CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.CHANGETO, pID, ShippingID, purchasetype);
                }
                Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
                Cursor = Cursors.Default;
            }
        }

        private void paybill_btn_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgr = GetCurrentRow();//your specific tabname
            if (dgr != null)
            {               
                string Pid = dgr.Cells[0].Value.ToString();
                //string sID = dgr.Cells[11].Value.ToString();

                //PurchaseType = "BILL";
                if (CommonClass.PRPaymentsfrm != null
                    && CommonClass.PRPaymentsfrm.IsDisposed)
                {
                    CommonClass.PRPaymentsfrm.Close();
                }
                CommonClass.PRPaymentsfrm = new PurchasePayments(CommonClass.InvocationSource.REGISTER, Pid);
                Cursor = Cursors.WaitCursor;
                CommonClass.PRPaymentsfrm.MdiParent = this.MdiParent;
                CommonClass.PRPaymentsfrm.Show();
                CommonClass.PRPaymentsfrm.Focus();
                if (CommonClass.PRPaymentsfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PRPaymentsfrm.Close();
                }
                Cursor = Cursors.Default;
            }
        }

        private void dgOBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string PurchaseId = dgActiveOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ShippingID = dgActiveOrders.Rows[e.RowIndex].Cells[09].Value.ToString();

           
            PurchaseType = dgActiveOrders.Rows[e.RowIndex].Cells[08].Value.ToString();
                            

            if (PurchaseType != "SBILL")
            {
                if (CommonClass.EnterPurchasefrm == null
              || CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, PurchaseId, ShippingID);
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
            }
            else //HISTORICAL BILL
            {
                //if (CommonClass.APBalanceEntryFrm == null
                //|| CommonClass.APBalanceEntryFrm.IsDisposed)
                //{
                    
                //}
                CommonClass.APBalanceEntryFrm = new APBalanceEntry("Accounts Payable Starting Balances", "", PurchaseId);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.APBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.APBalanceEntryFrm.Show();
                CommonClass.APBalanceEntryFrm.Focus();
                if (CommonClass.APBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.APBalanceEntryFrm.Close();
                }

            }
            this.Cursor = Cursors.Default;
        }

        private void dgPurchaseRegAll_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5      //Amount
                 || e.ColumnIndex == 6   //Amount Due
                 || e.ColumnIndex == 11) //Debit Amount
                 && e.RowIndex != dgPurchaseRegAll.NewRowIndex)
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
            if ((e.ColumnIndex == 5      //Amount
                 || e.ColumnIndex == 6   //Amount Due
                 || e.ColumnIndex == 11) //Debit Amount
                 && e.RowIndex != dgPurchaseRegAll.NewRowIndex)
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
                 || e.ColumnIndex == 9) //Debit Amount
                 && e.RowIndex != dgPurchaseRegAll.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgOBills_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Debit Amount
                 && e.RowIndex != dgPurchaseRegAll.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgRetDebit_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Debit Amount
                 && e.RowIndex != dgPurchaseRegAll.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgCBills_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                 || e.ColumnIndex == 6  //Amount Due
                 || e.ColumnIndex == 9) //Debit Amount
                 && e.RowIndex != dgPurchaseRegAll.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private bool CheckIfReceived(string pPID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = "SELECT sum(ISNULL(ReceiveQty,0)) as TotalReceived from PurchaseLines where PurchaseID = " + pPID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                if(RTb.Rows.Count > 0)
                {
                    float lReceive = RTb.Rows[0]["TotalReceived"].ToString() == "" ? 0 : float.Parse(RTb.Rows[0]["TotalReceived"].ToString());
                    if(lReceive != 0)
                    {
                        return true;
                    }else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void dgCBills_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string PurchaseId = dgCompletedOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ShippingID = dgCompletedOrders.Rows[e.RowIndex].Cells[09].Value.ToString();


            PurchaseType = dgCompletedOrders.Rows[e.RowIndex].Cells[08].Value.ToString();


            if (PurchaseType != "SBILL")
            {
                if (CommonClass.EnterPurchasefrm == null
              || CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, PurchaseId, ShippingID);
                }
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
            }
            else //HISTORICAL BILL
            {
                //if (CommonClass.APBalanceEntryFrm == null
                //|| CommonClass.APBalanceEntryFrm.IsDisposed)
                //{
                    
                //}
                CommonClass.APBalanceEntryFrm = new APBalanceEntry("Accounts Payable Starting Balances", "", PurchaseId);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.APBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.APBalanceEntryFrm.Show();
                CommonClass.APBalanceEntryFrm.Focus();
                if (CommonClass.APBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.APBalanceEntryFrm.Close();
                }

            }
            this.Cursor = Cursors.Default;
        }

        private void dgCBills_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            DataGridViewRow dgr = GetCurrentRow();//your specific tabname
            if (dgr != null)
            {
                string lStat = dgr.Cells[8].Value.ToString().Trim();
                EnableButtons(lStat);

            }
        }

        private void EnableButtons(string pStat)
        {
            switch (pStat)
            {
                case "New":                   
                    btnReceive.Enabled = false;                    
                    break;
                case "Active":                   
                    btnReceive.Enabled = true;                   
                    break;
                case "Backordered":                   
                    btnReceive.Enabled = true;                    
                    break;
                default:                  
                    btnReceive.Enabled = false;
                    
                    break;
            }
        }

        private void dgNewOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgNewOrder.Rows[e.RowIndex].Selected = true;
            string Pid = dgNewOrder.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ShippingID = dgNewOrder.Rows[e.RowIndex].Cells[09].Value.ToString();
            PurchaseType = dgNewOrder.Rows[e.RowIndex].Cells[08].Value.ToString();
            if (PurchaseType != "SBILL")
            {


                if (CommonClass.EnterPurchasefrm != null
                && !CommonClass.EnterPurchasefrm.IsDisposed)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
                CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, Pid, ShippingID, purchasetype);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                CommonClass.EnterPurchasefrm.Show();
                CommonClass.EnterPurchasefrm.Focus();
                if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.EnterPurchasefrm.Close();
                }
            }
            else //HISTORICAL BILL
            {

                CommonClass.APBalanceEntryFrm = new APBalanceEntry("Accounts Payable Starting Balances", "", Pid);
                this.Cursor = Cursors.WaitCursor;
                CommonClass.APBalanceEntryFrm.MdiParent = this.MdiParent;
                CommonClass.APBalanceEntryFrm.Show();
                CommonClass.APBalanceEntryFrm.Focus();
                if (CommonClass.APBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.APBalanceEntryFrm.Close();
                }

            }
            this.Cursor = Cursors.Default;
        }

        private void dgNewOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5      //Amount
               || e.ColumnIndex == 6   //Amount Due
               || e.ColumnIndex == 11) //Debit Amount
               && e.RowIndex != dgNewOrder.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }
    }
}
