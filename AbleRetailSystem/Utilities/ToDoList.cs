using RestaurantPOS.Purchase;
using RestaurantPOS.Sales;
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

namespace RestaurantPOS.Utilities
{
    public partial class ToDoList : Form
    {
        private DataTable dt;
        private DataTable dx;
        private string PurchaseType = "";
        private string Pid;
        private int index;
        private string lEntityID = "";
        private string lShipID = "";
        private bool isSale = false;
        public ToDoList()
        {
            InitializeComponent();
        }

        private void ToDoList_Load(object sender, EventArgs e)
        {
            FillToDoListTab(TabTodoList.SelectedIndex);
            //if (TabTodoList.SelectedIndex == 0)
            //{
            //    btn.Text = "Mail Merge";
            //}
            //else if (TabTodoList.SelectedIndex == 1 || TabTodoList.SelectedIndex == 5)
            //{
            //    btn.Text = "Pay Bills";
            //}
            //else if (TabTodoList.SelectedIndex == 2 || TabTodoList.SelectedIndex == 3 || TabTodoList.SelectedIndex == 4)
            //{
            //    btn.Text = "Record";
            //}
            //else if (TabTodoList.SelectedIndex == 6)
            //{
            //    btn.Text = "Record as Actual";
            //}
            //else if (TabTodoList.SelectedIndex == 7)
            //{
            //    btn.Text = "Order/Build";
            //}
            //else if (TabTodoList.SelectedIndex == 8)
            //{
            //    btn.Text = "Remove";
            //}
        }
        private void FillToDoListTab(int pTabIndex)
        {
            switch (pTabIndex)
            {
                case 0:
                    dgAR.Rows.Clear();
                    Populatedg(ref dgAR, "AR");
                    btn.Text = "Receive Payment";
                    break;
                case 1:
                    dgAP.Rows.Clear();
                    Populatedg(ref dgAP, "AP");
                    btn.Text = "Pay Bills";
                    break;
                case 2:
                    dgRacurringTransaction.Rows.Clear();
                    Populatedg(ref dgRacurringTransaction, "Recurring Transaction");
                    btn.Text = "Record";
                    break;
                case 3:
                    dgRecurringPurchases.Rows.Clear();
                    Populatedg(ref dgRecurringPurchases, "Recurring Purchases");
                    btn.Text = "Record";
                    break;
                case 4:
                    dgRecurringSales.Rows.Clear();
                    Populatedg(ref dgRecurringSales, "Recurring Sales");
                    btn.Text = "Record";
                    break;
                case 5:
                    dgExpiringDiscount.Rows.Clear();
                    Populatedg(ref dgExpiringDiscount, "Expiring Discount");
                    btn.Text = "Pay Bills";
                    break;
                case 6:
                    dgOrders.Rows.Clear();
                    Populatedg(ref dgOrders, "Orders");
                    btn.Text = "Record as Actual";
                   
                    break;
                case 7:
                    dgStockAlert.Rows.Clear();
                    Populatedg(ref dgStockAlert, "Stock Alert");
                    btn.Text = "Order/Build";
                    break;
            }
        }

        public void Populatedg(ref DataGridView dgpopulate, string type)
        {
            SqlConnection con_ = new SqlConnection(CommonClass.ConStr);
            string selectSql = "";
            string orderQtysql = "";
            DateTime tdate = DateTime.Now.ToUniversalTime();
            switch (type)
            {
                case "AR":
                    selectSql = @"SELECT * FROM Sales s 
                                  INNER JOIN Profile p ON s.CustomerID = p.ID
                                  WHERE s.SalesType = 'INVOICE'
                                  AND s.InvoiceStatus = 'Open'";
                    break;
                case "AP":
                    selectSql = @"SELECT * FROM Purchases 
                                  INNER JOIN Profile ON Purchases.SupplierID = Profile.ID
                                  WHERE Purchases.PurchaseType IN ('BILL','SBILL') 
                                  AND POStatus = 'Open'";
                    break;
                case "Recurring Transaction":
                    selectSql = @"SELECT m.MoneyOutNumber as TransNum,
                                    p.Name,
                                    r.Frequency,
                                    r.LastPosted,
                                    r.NotifyDate AS [Next Due],
                                    m.TotalAllocated AS Amount,
                                    0 AS ShippingMethodID, r.TranType, r.EntityID 
                                FROM Recurring r
                                INNER JOIN MoneyOut m ON m.MoneyOutID = r.EntityID
                                INNER JOIN Profile p ON p.ID = m.ProfileID
                                WHERE r.TranType = 'MO'
                                UNION 
                                SELECT m.MoneyInNumber as TransNum, 
                                    p.Name,
                                    r.Frequency,
                                    r.LastPosted,
                                    r.NotifyDate AS [Next Due],
                                    m.TotalAllocated AS Amount,
                                    0 AS ShippingMethodID, r.TranType , r.EntityID 
                                FROM Recurring r
                                INNER JOIN MoneyIn m ON m.MoneyInID = r.EntityID
                                INNER JOIN Profile p ON p.ID = m.ProfileID
                                WHERE r.TranType = 'MI'
                                UNION
                                SELECT m.RecordJournalNumber as TransNum,
                                        m.Memo as Name,
                                        r.Frequency,
                                        r.LastPosted,
                                        r.NotifyDate AS [Next Due],
                                       (m.TotalDebit + m.TotalCredit) AS Amount,
                                        0 AS ShippingMethodID , r.TranType  , r.EntityID
                                       FROM Recurring r
                                       INNER JOIN RecordJournal m ON m.RecordJournalID = r.EntityID
                                       INNER JOIN RecordJournalLine jl ON jl.RecordJournalID = m.RecordJournalID 
                                       WHERE r.TranType = 'JE' AND r.NotifyDate <= @tdate";
                    break;
                case "Recurring Sales":
                    selectSql = @"SELECT r.EntityID,
                                    p.Name,
                                    r.Frequency,
                                    r.LastPosted,
                                    r.NotifyDate  AS [Next Due],
                                    m.GrandTotal AS Amount, 
                                    r.TranType,
                                    m.ShippingMethodID
                                FROM Recurring r
                                INNER JOIN Sales m ON m.SalesID = r.EntityID
                                INNER JOIN Profile p ON p.ID = m.CustomerID
                                WHERE r.TranType IN ('SQ','SO','SI') AND r.NotifyDate <= @tdate";
                    break;
                case "Recurring Purchases":
                    selectSql = @"SELECT r.EntityID,
                                    p.Name,
                                    r.Frequency,
                                    r.LastPosted,
                                    r.NotifyDate AS [Next Due],
                                    m.GrandTotal AS Amount,
									r.TranType,
                                    m.ShippingMethodID
                                FROM Recurring r
                                INNER JOIN Purchases m ON m.PurchaseID = r.EntityID
                                INNER JOIN Profile p ON p.ID = m.SupplierID
                                WHERE r.TranType IN ('PQ','PO','PB','RI') AND r.NotifyDate <= @tdate";
                    break;
                case "Expiring Discount":
                    selectSql = @"SELECT PurchaseID as EntityID, Name, PurchaseNumber as TransNum, TransactionDate, GrandTotal 
                                FROM Purchases
                                INNER JOIN Profile ON Purchases.SupplierID = Profile.ID 
                                Inner JOIN Terms t ON t.TermsID = Purchases.TermsReferenceID 
                                WHERE Purchases.PurchaseType IN ('BILL','SBILL')
                                AND Purchases.POStatus = 'Open' And GETUTCDATE() <=  DATEADD(DD,t.DiscountDays + 1 ,TransactionDate)";
                                //Union
                                //SELECT SalesID as EntityID, Name, SalesNumber as TransNum, TransactionDate, GrandTotal 
                                //FROM Sales
                                //INNER JOIN Profile ON Sales.CustomerID = Profile.ID 
                                //Inner JOIN Terms t ON t.TermsID = Sales.TermsReferenceID 
                                //WHERE Sales.SalesType = 'INVOICE' 
                                //AND Sales.InvoiceStatus = 'Open' And GETUTCDATE() <=  DATEADD(DD,t.DiscountDays,TransactionDate)";
                    break;
                case "Orders":
                    selectSql = @"SELECT s.SalesID as EntityID, p.Name, s.SalesNumber as TransNum , TotalDue  , isSales = 'true' , isPurchase = 'false', s.ShippingMethodID
                                FROM Profile p
                                INNER JOIN Sales s ON s.CustomerID = p.ID
                                WHERE  s.SalesType = 'ORDER' AND TotalDue > 0 
                                UNION
                                SELECT ps.PurchaseID as EntityID, p.Name, ps.PurchaseNumber as TransNum, TotalDue  , isSales = 'false' , isPurchase = 'true' , ps.ShippingMethodID 
                                FROM Profile p
                                INNER JOIN Purchases ps ON ps.SupplierID = p.ID							
                                WHERE ps.PurchaseType = 'ORDER' AND TotalDue > 0";
                    break;
                case "Stock Alert"://
                    selectSql = @"SELECT * FROM Items i 
                                INNER JOIN ItemsQty q ON q.ItemID = i.ID WHERE  IsBought = 1 AND q.OnHandQty <= q.MinQty OR q.CommitedQty != 0";
                    orderQtysql = @"SELECT i.ID,
                                    (SELECT COUNT(SalesID) FROM Sales WHERE SalesType='ORDER' AND InvoiceStatus='Order') AS Committed,
                                    0 AS OnOrder,0 AS Available,iq.OnHandQty
                                    FROM Items i
                                    INNER JOIN ItemsQty iq ON iq.ItemID = i.ID
                                    INNER JOIN SalesLines sl ON sl.EntityID = i.ID
                                    INNER JOIN Sales s ON sl.SalesID = s.SalesID
                                    INNER JOIN Profile p ON p.ID = s.CustomerID
                                    WHERE s.SalesType = 'ORDER'
                                    AND s.InvoiceStatus = 'Order'
                                    UNION
                                    SELECT 0 AS committed,i.ID,
                                    ((SELECT COUNT(PurchaseID) FROM Purchases WHERE PurchaseType = 'ORDER' AND POStatus = 'Order') + (sl.OrderQty - sl.ReceiveQty)) AS OnOrder,
                                    (iq.OnHandQty) - 0 + ((SELECT COUNT(PurchaseID) FROM Purchases WHERE PurchaseType = 'ORDER' AND POStatus = 'Order') + (sl.OrderQty - sl.ReceiveQty)) AS Available,
                                    iq.OnHandQty
                                    FROM Items i
                                    INNER JOIN ItemsQty iq ON iq.ItemID = i.ID
                                    INNER JOIN PurchaseLines sl ON sl.EntityID = i.ID
                                    INNER JOIN Purchases s ON sl.PurchaseID = s.PurchaseID
                                    INNER JOIN Profile p ON p.ID = s.SupplierID
                                    WHERE s.PurchaseType = 'ORDER'
                                    AND POStatus = 'Order'";
                    break;
                case "Contact Alert":
                    selectSql = @"SELECT * FROM Purchases 
                                INNER JOIN Profile ON Purchases.SupplierID = Profile.ID
                                WHERE Purchases.PurchaseType IN ('BILL','SBILL')
                                AND Purchases.POStatus = 'Open'";
                    break;
            }

            try
            {
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                cmd_.Parameters.Clear();
                cmd_.Parameters.AddWithValue("@tdate", tdate);
                cmd_.CommandType = CommandType.Text;
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                dt = new DataTable();
                da.SelectCommand = cmd_;
                da.Fill(dt);

                if (TabTodoList.SelectedIndex == 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)//AR
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                        dgpopulate.Rows[x].Cells[0].Value = dr["SalesID"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["Name"].ToString();
                        dgpopulate.Rows[x].Cells[3].Value = dr["SalesNumber"].ToString();
                        dgpopulate.Rows[x].Cells[4].Value = Convert.ToDateTime(dr["PromiseDate"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[5].Value = dr["GrandTotal"].ToString();
                    }
                }
                else if (TabTodoList.SelectedIndex == 1)//AP
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                        dgpopulate.Rows[x].Cells[0].Value = dr["PurchaseID"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["Name"].ToString();
                        dgpopulate.Rows[x].Cells[3].Value = dr["PurchaseNumber"].ToString();
                        dgpopulate.Rows[x].Cells[4].Value = Convert.ToDateTime(dr["PromiseDate"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[5].Value = dr["GrandTotal"].ToString();
                    }
                }
                else if (TabTodoList.SelectedIndex == 2)//Recurring Transactions
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                        dgpopulate.Rows[x].Cells[0].Value = dr["TransNum"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["Name"].ToString();
                        dgpopulate.Rows[x].Cells[3].Value = dr["Frequency"].ToString();
                        dgpopulate.Rows[x].Cells[4].Value = Convert.ToDateTime(dr["LastPosted"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[5].Value = dr["Next Due"].ToString();
                        dgpopulate.Rows[x].Cells[6].Value = dr["TranType"].ToString();
                        dgpopulate.Rows[x].Cells[7].Value = dr["EntityID"].ToString();

                    }
                }
                else if (TabTodoList.SelectedIndex == 3)//Recurring Purchase
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                       
                        dgpopulate.Rows[x].Cells[0].Value = dr["EntityID"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["Name"].ToString();
                        dgpopulate.Rows[x].Cells[3].Value = dr["Frequency"].ToString();
                        dgpopulate.Rows[x].Cells[4].Value = Convert.ToDateTime(dr["LastPosted"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[5].Value = dr["Next Due"].ToString();
                        dgpopulate.Rows[x].Cells[6].Value = dr["TranType"].ToString();
                        dgpopulate.Rows[x].Cells[7].Value = dr["ShippingMethodID"].ToString();
                        
                    }
                }
                else if (TabTodoList.SelectedIndex == 4)//Recurring Sale
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                        dgpopulate.Rows[x].Cells[0].Value = dr["EntityID"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["Name"].ToString();
                        dgpopulate.Rows[x].Cells[3].Value = dr["Frequency"].ToString();
                        dgpopulate.Rows[x].Cells[4].Value = Convert.ToDateTime(dr["LastPosted"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[5].Value = dr["Next Due"].ToString();
                        dgpopulate.Rows[x].Cells[6].Value = dr["TranType"].ToString();
                        dgpopulate.Rows[x].Cells[7].Value = dr["ShippingMethodID"].ToString();
                    }
                }
                else if (TabTodoList.SelectedIndex == 5)//Expiring Discount
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                        dgpopulate.Rows[x].Cells[0].Value = dr["EntityID"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["Name"].ToString();
                        dgpopulate.Rows[x].Cells[3].Value = dr["TransNum"].ToString();
                        dgpopulate.Rows[x].Cells[4].Value = Convert.ToDateTime(dr["TransactionDate"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[5].Value = dr["GrandTotal"].ToString();
                    }
                }
                else if (TabTodoList.SelectedIndex == 6)//Orders
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                        dgpopulate.Rows[x].Cells[0].Value = dr["EntityID"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["ShippingMethodID"].ToString();
                        dgpopulate.Rows[x].Cells[2].Value = dr["isSales"].ToString();
                        if (dr["isSales"].ToString() == "true")
                            dgpopulate.Rows[x].Cells[3].Value = dr["Name"].ToString();
                        else
                        dgpopulate.Rows[x].Cells[5].Value = dr["Name"].ToString();
                        dgpopulate.Rows[x].Cells[4].Value = dr["isPurchase"].ToString();
                        dgpopulate.Rows[x].Cells[6].Value = dr["TransNum"].ToString();
                        dgpopulate.Rows[x].Cells[7].Value = Convert.ToDateTime(dr["PromiseDate"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[8].Value = dr["TotalDue"].ToString();
                    }
                }
                else if (TabTodoList.SelectedIndex == 7)//Stock Alert
                {
                    SqlCommand cmdx = new SqlCommand(orderQtysql, con_);
                    cmdx.Parameters.Clear();
                    cmdx.CommandType = CommandType.Text;

                    SqlDataAdapter dao = new SqlDataAdapter();
                    dx = new DataTable();
                    dao.SelectCommand = cmdx;
                    dao.Fill(dx);
                    int commited = 0;
                    int OnOrder = 0;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        dgpopulate.Rows.Add();
                       dgpopulate.Rows[x].Cells[0].Value = dr["ID"].ToString();
                        dgpopulate.Rows[x].Cells[1].Value = dr["MinQty"].ToString(); dgpopulate.Rows[x].Cells[1].Value = dr["MinQty"].ToString();
                        dgpopulate.Rows[x].Cells[2].Value = dr["ItemName"].ToString(); ;
                        dgpopulate.Rows[x].Cells[3].Value = dr["OnHandQty"].ToString();
                        for(int y=0; y < dx.Rows.Count; y++)
                        {
                            DataRow dy = dx.Rows[y];
                            commited += Convert.ToInt32(dy["Committed"].ToString());
                            OnOrder += Convert.ToInt32(dy["OnOrder"].ToString());
                        }

                        //dgpopulate.Rows[x].Cells[4].Value = Convert.ToDateTime(dr["PromiseDate"]).ToLocalTime().ToShortDateString();
                        dgpopulate.Rows[x].Cells[5].Value = ((Convert.ToInt32(dr["OnHandQty"].ToString()) - commited) < 0 ? "0" : (Convert.ToInt32(dr["OnHandQty"].ToString()) - commited).ToString()) ;
                        dgpopulate.Rows[x].Cells[1].Value = (dgpopulate.Rows[x].Cells[5].Value.ToString() == "0"? dr["MinQty"].ToString() :(Convert.ToInt32(dr["MinQty"].ToString()) - Convert.ToInt32(dgpopulate.Rows[x].Cells[5].Value.ToString())).ToString());
                        dgpopulate.Rows[x].Cells[6].Value = commited.ToString();
                        dgpopulate.Rows[x].Cells[4].Value = OnOrder.ToString();

                    }
                    if (dt.Rows.Count > 0)
                    {
                        dgpopulate.Rows[0].Selected = true;
                    }

                    dt.Clear();
                }
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

        private void TabTodoList_Selected(object sender, TabControlEventArgs e)
        {
            FillToDoListTab(TabTodoList.SelectedIndex);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (btn.Text == "Receive Payment")
            {
                MailMerge(ref dgAR);
            }
            else if (btn.Text == "Pay Bills")
            {
              if(TabTodoList.SelectedIndex == 1)
                PayBills(ref dgAP);
              else
                    PayBills(ref dgExpiringDiscount);
            }
            else if (btn.Text == "Record")
            {
                if (index < 0)
                    return;
                if (TabTodoList.SelectedIndex == 2)
                {
                    Recurring(ref dgRacurringTransaction);
                }
                else if(TabTodoList.SelectedIndex == 3)
                {
                    Recurring(ref dgRecurringPurchases);
                }
                else if(TabTodoList.SelectedIndex == 4)
                {
                    Recurring(ref dgRecurringSales);
                }

            } else if (btn.Text == "Record as Actual")
            {
                if (!isSale)
                    OrderPuchase(lEntityID);
                else
                    OrderSale(lEntityID);
            }
            else if (btn.Text == "Order/Build")
            {
                StockAlert();
            }else if (btn.Text == "Remove")
            {

            }
        }
      
        private DataGridViewRow GetCurrentRow()
        {
            DataGridViewRow DgRow = null;
            switch (TabTodoList.SelectedIndex)
            {
                case 0:
                    if (dgAP.CurrentRow.Index >= 0)
                    {
                        DgRow = dgAP.CurrentRow;
                    }
                    break;
                case 1:
                    if (dgAR.CurrentRow.Index >= 0)
                    {
                        DgRow = dgAR.CurrentRow;
                    }
                    break;
                case 2:
                    if (dgRacurringTransaction.CurrentRow.Index >= 0)
                    {
                        DgRow = dgRacurringTransaction.CurrentRow;
                    }
                    break;
                case 3:
                    if (dgRecurringPurchases.CurrentRow.Index >= 0)
                    {
                        DgRow = dgRecurringPurchases.CurrentRow;
                    }
                    break;
                case 4:
                    if (dgRecurringSales.CurrentRow.Index >= 0)
                    {
                        DgRow = dgRecurringSales.CurrentRow;
                    }
                    break;
                case 5:
                    if (dgExpiringDiscount.CurrentRow.Index >= 0)
                    {
                        DgRow = dgExpiringDiscount.CurrentRow;
                    }
                    break;
                case 6:
                    if (dgOrders.CurrentRow.Index >= 0)
                    {
                        DgRow = dgOrders.CurrentRow;
                    }
                    break;
                case 7:
                    if (dgStockAlert.CurrentRow.Index >= 0)
                    {
                        DgRow = dgStockAlert.CurrentRow;
                    }
                    break;
            }
            return DgRow;
        }
        private void MailMerge(ref DataGridView dgReceive)
        {
            if (dgReceive.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvr = dgReceive.SelectedRows[0];
                //PurchaseType = "BILL";
                if (CommonClass.SRPaymentsfrm != null
                    && CommonClass.SRPaymentsfrm.IsDisposed)
                {
                    CommonClass.SRPaymentsfrm.Close();
                }
                CommonClass.SRPaymentsfrm = new SalesReceivePayment(CommonClass.InvocationSource.REGISTER, Pid);
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
        private void PayBills( ref DataGridView dgPay)
        {
            if (dgPay.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvr = dgPay.SelectedRows[0];
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
        private void Recurring(ref DataGridView dgRecur)
        {
            if (dgRecur.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvr = dgRecur.SelectedRows[0];
                switch (dgvr.Cells[6].Value.ToString())
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

        private void TabTodoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillToDoListTab(TabTodoList.SelectedIndex);
        }

        private void dgRacurringTransaction_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgRacurringTransaction.Rows[e.RowIndex].Selected = true;
            lEntityID = dgRacurringTransaction.Rows[e.RowIndex].Cells["EntityID"].Value.ToString();
           // lShipID = dgRacurringTransaction.Rows[e.RowIndex].Cells["ShippingMethodID"].Value.ToString();
            Recurring(ref dgRacurringTransaction);
        }

        private void dgRecurringPurchases_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgRecurringPurchases.Rows[e.RowIndex].Selected = true;
            lEntityID = dgRecurringPurchases.Rows[e.RowIndex].Cells[0].Value.ToString();
            lShipID = dgRecurringPurchases.Rows[e.RowIndex].Cells[7].Value.ToString();
            Recurring(ref dgRecurringPurchases);
        }

        private void dgRecurringSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgRecurringSales.Rows[e.RowIndex].Selected = true;
            lEntityID = dgRecurringSales.Rows[e.RowIndex].Cells[0].Value.ToString();
            lShipID = dgRecurringSales.Rows[e.RowIndex].Cells[7].Value.ToString();
            Recurring(ref dgRecurringSales);
        }

        private void dgRecurringSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgRecurringSales.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            dgRecurringSales.Rows[e.RowIndex].Selected = true;
            lEntityID = dgRecurringSales.Rows[e.RowIndex].Cells[0].Value.ToString();
            lShipID = dgRecurringSales.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void dgRecurringPurchases_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgRecurringPurchases.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            lEntityID = dgRecurringPurchases.Rows[e.RowIndex].Cells[0].Value.ToString();
            lShipID = dgRecurringPurchases.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void dgRacurringTransaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgRacurringTransaction.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            dgRacurringTransaction.Rows[e.RowIndex].Selected = true;
            lEntityID = dgRacurringTransaction.Rows[e.RowIndex].Cells["EntityID"].Value.ToString();
           // lShipID = dgRacurringTransaction.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void dgAP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgAP.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            Pid = dgAP.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dgAP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgAP.Rows[e.RowIndex].Selected = true;
            Pid = dgAP.Rows[e.RowIndex].Cells[0].Value.ToString();
            PayBills(ref dgAP);
        }

        private void dgAR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgAR.Rows[e.RowIndex].Selected = true;
            Pid = dgAR.Rows[e.RowIndex].Cells[0].Value.ToString();
            MailMerge(ref dgAR);
        }

        private void dgAR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgAR.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            Pid = dgAR.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        void StockAlert()
        {
            this.Cursor = Cursors.WaitCursor;
            if (CommonClass.EnterPurchasefrm != null
                    && !CommonClass.EnterPurchasefrm.IsDisposed)
                CommonClass.EnterPurchasefrm.Close();
            CommonClass.EnterPurchasefrm = new Purchase.EnterPurchase(CommonClass.InvocationSource.TODOLIST, Pid, lShipID, "ORDER");
            CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
            CommonClass.EnterPurchasefrm.Show();
            CommonClass.EnterPurchasefrm.Focus();
            if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EnterPurchasefrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void dgStockAlert_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgStockAlert.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            Pid = dgStockAlert.Rows[e.RowIndex].Cells[0].Value.ToString();
            lShipID = dgStockAlert.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dgOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgOrders.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            lShipID = dgOrders.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (dgOrders.Rows[e.RowIndex].Cells[2].Value.ToString() == "true")
            {
                lEntityID = dgOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                isSale = true;
            }
            else
            { 
                lEntityID = dgOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                isSale = false;
            }

        }
        void OrderPuchase(string Pid)
        {
            this.Cursor = Cursors.WaitCursor;
            if (CommonClass.EnterPurchasefrm != null
                    && !CommonClass.EnterPurchasefrm.IsDisposed)
                CommonClass.EnterPurchasefrm.Close();
            CommonClass.EnterPurchasefrm = new Purchase.EnterPurchase(CommonClass.InvocationSource.CHANGETO, Pid, lShipID, "BILL");
            CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
            CommonClass.EnterPurchasefrm.Show();
            CommonClass.EnterPurchasefrm.Focus();
            if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EnterPurchasefrm.Close();
            }
            this.Cursor = Cursors.Default;
        }
        void OrderSale(string Sid)
        {
            string SalesType = "INVOICE";
            if (CommonClass.EnterSalesfrm == null
                || CommonClass.EnterSalesfrm.IsDisposed)
            {
                CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.CHANGETO, Sid, SalesType);
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

        private void dgOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgOrders.Rows[e.RowIndex].Selected = true;
            index = e.RowIndex;
            lShipID = dgOrders.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (dgOrders.Rows[e.RowIndex].Cells[2].Value.ToString() == "true")
                OrderSale(dgOrders.Rows[e.RowIndex].Cells[0].Value.ToString());
            else
                OrderPuchase(dgOrders.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void dgExpiringDiscount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgExpiringDiscount.Rows[e.RowIndex].Selected = true;
            Pid = dgExpiringDiscount.Rows[e.RowIndex].Cells[0].Value.ToString();
            PayBills(ref dgExpiringDiscount);
        }

        private void dgExpiringDiscount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgExpiringDiscount.Rows[e.RowIndex].Selected = true;
            Pid = dgExpiringDiscount.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dgAR_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5
                && e.RowIndex != this.dgAR.NewRowIndex)
            {
                if (e.Value != null)
                {
                    decimal d = decimal.Parse(e.Value.ToString(), NumberStyles.Any);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgAP_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5
                && e.RowIndex != this.dgAR.NewRowIndex)
            {
                if (e.Value != null)
                {
                    decimal d = decimal.Parse(e.Value.ToString(), NumberStyles.Any);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgExpiringDiscount_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5
                && e.RowIndex != this.dgAR.NewRowIndex)
            {
                if (e.Value != null)
                {
                    decimal d = decimal.Parse(e.Value.ToString(), NumberStyles.Any);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8
                && e.RowIndex != this.dgAR.NewRowIndex)
            {
                if (e.Value != null)
                {
                    decimal d = decimal.Parse(e.Value.ToString(), NumberStyles.Any);
                    e.Value = d.ToString("C2");
                }
            }
        }
    }
}
