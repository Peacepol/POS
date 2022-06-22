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
using System.Text.RegularExpressions;
using System.Globalization;
using RestaurantPOS.Inventory;
using RestaurantPOS.Sales;
using RestaurantPOS.Purchase;

namespace RestaurantPOS
{
    public partial class TransactionsLookup : Form
    {
        private string Entity = "";
        private string ActiveTab = "";
       
        public TransactionsLookup(string pType = "Account",string pEntity = "")
        {
            InitializeComponent();
            Entity = pEntity;
            ActiveTab = pType;
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TransactionsLookup_Load(object sender, EventArgs e)
        {
            switch (ActiveTab)
            {
                case "Customer":
                    TabLookup.SelectedIndex = 0;
                    break;
                case "Supplier":
                    TabLookup.SelectedIndex = 1;
                    break;
                case "Item":
                    TabLookup.SelectedIndex = 2;
                    break;
                case "Invoice":
                    TabLookup.SelectedIndex = 3;
                    LoadTranInvoice();
                    break;
                default:
                    TabLookup.SelectedIndex = 0;
                    break;

            }

            this.rdoAllSuppliers.Checked = true;
            this.rdoAllCustomers.Checked = true;
            this.rdoAllItems.Checked = true;
            this.rdoAllInvoices.Checked = true;

            Entity = ""; //Clear Entity ID to enable search based on user criteria if EntityID is supplied at form new.

        }
        private void GetEntityID(string pSearchStr)
        {
            SqlConnection con_ = null;

            try
            {
                string sqlcon = "Select * from Accounts where AccountID = " + pSearchStr;


                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sqlcon, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

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
        private void LoadTranCustomer(bool noDate = true)
        {
            SqlConnection con_ = null;

            try
            {
                string sqlcon = "";
                if (Entity != "")
                {
                    sqlcon = @"SELECT c.TransactionNumber,c.Type,c.TransactionDate,a.TradeDebtorGLCode, c.Memo, c.Debit, c.Credit, j.JobCode, c.RecordID FROM 
                        ((SELECT j.TransactionNumber, j.TransactionDate,j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit, j.JobID, s.SalesID as RecordID 
                        from Sales as s inner join Journal as j on s.SalesNumber = j.TransactionNumber where s.SalesType in ('INVOICE','SINVOICE') and(s.GrandTotal = j.DebitAmount or s.GrandTotal = j.CreditAmount) 
                        and CustomerID = " + Entity;
                    sqlcon += @"UNION SELECT j.TransactionNumber, j.TransactionDate,j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit,j.JobID, s.PaymentID as RecordID from Payment as s inner join Journal as j on s.PaymentNumber = j.TransactionNumber
                        where s.PaymentFor = 'Sales' and(s.TotalAmount = j.DebitAmount or s.TotalAmount = j.CreditAmount) and s.ProfileID = " + Entity;

                    sqlcon += ") as c inner join Preference as a on c.AccountID = a.TradeDebtorGLCode ) left join Jobs as j on c.JobID = c.JobID";
                }
                else
                {
                  
                    DateTime sdate = Convert.ToDateTime(CusSDate.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                    DateTime edate = Convert.ToDateTime(CusEDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();


                    string lfilter = "";
                    string lSfilter = "";
                    if (rdoCustomer.Checked)
                    {
                        if (txtCustomer.Text == "")
                        {
                            MessageBox.Show("Please specify a Customer.");
                            this.txtCustomer.Focus();
                            return;
                        }
                        lfilter = " s.ProfileID = " + this.lblCustomerID.Text;
                        lSfilter = " s.CustomerID = " + this.lblCustomerID.Text;
                    }

                   
                    sqlcon = @"SELECT c.TransactionNumber,c.Type,c.TransactionDate,a.TradeDebtorGLCode, c.Memo, c.Debit, c.Credit, j.JobCode, c.RecordID FROM 
                        ((SELECT j.TransactionNumber, j.TransactionDate, j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit, j.JobID, s.SalesID as RecordID 
                        from Sales as s inner join Journal as j on s.SalesNumber = j.TransactionNumber where s.SalesType in ('INVOICE','SINVOICE') and(s.GrandTotal = j.DebitAmount or s.GrandTotal = j.CreditAmount) 
                        and s.TransactionDate BETWEEN  '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + (lSfilter != "" ? " and " : "") + lSfilter;
                    sqlcon += @"UNION SELECT j.TransactionNumber, j.TransactionDate,j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit,j.JobID, s.PaymentID as RecordID from Payment as s inner join Journal as j on s.PaymentNumber = j.TransactionNumber
                        where s.PaymentFor = 'Sales' and(s.TotalAmount = j.DebitAmount or s.TotalAmount = j.CreditAmount) and s.TransactionDate BETWEEN  '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + (lfilter != "" ? " and " : "") + lfilter;

                    sqlcon += ") as c inner join Preference as a on c.AccountID = a.TradeDebtorGLCode ) left join Jobs as j on c.JobID = c.JobID";
                }



                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sqlcon, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow rw in dt.Rows)
                {
                    DateTime lTranUTC = (DateTime)rw["TransactionDate"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    rw["TransactionDate"] = lTranLocal;
                }
                this.dgCustomer.DataSource = dt;
                this.dgCustomer.Columns[0].HeaderText = "Transaction No";
                this.dgCustomer.Columns[2].HeaderText = "Transaction Date";
                this.dgCustomer.Columns[3].HeaderText = "Account";
                this.dgCustomer.Columns[4].HeaderText = "Memo";
                this.dgCustomer.Columns[5].DefaultCellStyle.Format = "C2";
                this.dgCustomer.Columns[6].DefaultCellStyle.Format = "C2";
                this.dgCustomer.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgCustomer.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgCustomer.Columns["RecordID"].Visible = false;

                this.dgCustomer.Columns[0].Width = 100;
                this.dgCustomer.Columns[1].Width = 50;
                this.dgCustomer.Columns[2].Width = 80;
                this.dgCustomer.Columns[3].Width = 80;
                this.dgCustomer.Columns[4].Width = 240;
                this.dgCustomer.Columns[5].Width = 100;
                this.dgCustomer.Columns[6].Width = 100;
                this.dgCustomer.Columns[7].Width = 100;

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
        private void LoadTranSupplier(bool noDate = true)
        {
            SqlConnection con_ = null;

            try
            {
                string sqlcon = "";
                if (Entity != "")
                {
                    sqlcon = @"SELECT c.TransactionNumber,c.Type,c.TransactionDate,a.TradeCreditorGLCode, c.Memo, c.Debit, c.Credit, j.JobCode, c.RecordID FROM 
                        ((SELECT j.TransactionNumber, j.TransactionDate,j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit, j.JobID, s.PurchaseID as RecordID 
                        from Purchases as s inner join Journal as j on s.PurchaseNumber = j.TransactionNumber where s.PurchaseType in ('ORDER','BILL','SBILL') and(s.GrandTotal = j.DebitAmount or s.GrandTotal = j.CreditAmount) 
                        and CustomerID = " + Entity;
                    sqlcon += @"UNION SELECT j.TransactionNumber, j.TransactionDate,j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit,j.JobID, s.PaymentID as RecordID from Payment as s inner join Journal as j on s.PaymentNumber = j.TransactionNumber
                        where s.PaymentFor = 'Purchase' and(s.TotalAmount = j.DebitAmount or s.TotalAmount = j.CreditAmount) and s.ProfileID = " + Entity;

                    sqlcon += ") as c inner join Preference as a on c.AccountID = a.TradeCreditorGLCode ) left join Jobs as j on c.JobID = c.JobID";
                }
                else
                {
                
                    DateTime sdate = Convert.ToDateTime(SupSDate.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                    DateTime edate = Convert.ToDateTime(SupEDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                    string lfilter = "";
                    string lSfilter = "";
                    if (rdoSupplier.Checked)
                    {
                        if (lblSupplierID.Text == "")
                        {
                            MessageBox.Show("Please specify a Supplier.");
                            this.txtCustomer.Focus();
                            return;
                        }
                        lfilter = " s.ProfileID = " + this.lblSupplierID.Text;
                        lSfilter = " s.SupplierID = " + this.lblSupplierID.Text;
                    }


                    sqlcon = @"SELECT c.TransactionNumber,c.Type,c.TransactionDate,a.TradeCreditorGLCode, c.Memo, c.Debit, c.Credit, j.JobCode, c.RecordID FROM 
                        ((SELECT j.TransactionNumber, j.TransactionDate, j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit, j.JobID, s.PurchaseID as RecordID 
                        from Purchases as s inner join Journal as j on s.PurchaseNumber = j.TransactionNumber where s.PurchaseType in ('ORDER','BILL','SBILL') and (s.GrandTotal = j.DebitAmount or s.GrandTotal = j.CreditAmount) 
                        and s.TransactionDate BETWEEN  '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + (lSfilter != "" ? " and " : "") + lSfilter;
                    sqlcon += @"UNION SELECT j.TransactionNumber, j.TransactionDate,j.Type, j.AccountID, CAST(j.Memo as varchar) as Memo,  j.DebitAmount as Debit, j.CreditAmount as Credit,j.JobID, s.PaymentID as RecordID from Payment as s inner join Journal as j on s.PaymentNumber = j.TransactionNumber
                        where s.PaymentFor = 'Purchase' and(s.TotalAmount = j.DebitAmount or s.TotalAmount = j.CreditAmount) and s.TransactionDate BETWEEN  '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + (lfilter != "" ? " and " : "") + lfilter;
                    sqlcon += ") as c inner join Preference as a on c.AccountID = a.TradeCreditorGLCode ) left join Jobs as j on c.JobID = c.JobID";
                }



                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sqlcon, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow rw in dt.Rows)
                {
                    DateTime lTranUTC = (DateTime)rw["TransactionDate"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    rw["TransactionDate"] = lTranLocal;
                }
                this.dgSup.DataSource = dt;
                this.dgSup.Columns[0].HeaderText = "Transaction No";
                this.dgSup.Columns[2].HeaderText = "Transaction Date";
                this.dgSup.Columns[3].HeaderText = "Account";
                this.dgSup.Columns[4].HeaderText = "Memo";
                this.dgSup.Columns[5].DefaultCellStyle.Format = "C2";
                this.dgSup.Columns[6].DefaultCellStyle.Format = "C2";
                this.dgSup.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgSup.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgSup.Columns["RecordID"].Visible = false;

                this.dgSup.Columns[0].Width = 100;
                this.dgSup.Columns[1].Width = 50;
                this.dgSup.Columns[2].Width = 80;
                this.dgSup.Columns[3].Width = 80;
                this.dgSup.Columns[4].Width = 240;
                this.dgSup.Columns[5].Width = 100;
                this.dgSup.Columns[6].Width = 100;
                this.dgSup.Columns[7].Width = 100;
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
        private void LoadTranAccount(bool noDate = true)
        {
            SqlConnection con_ = null;

            try
            {
                string sqlcon = "";
                if (Entity != "")
                {
                    sqlcon = @"SELECT j.TransactionNumber, j.Type, j.TransactionDate, a.AccountNumber,CAST(j.Memo as varchar) as Memo, 
                    j.DebitAmount as Debit, j.CreditAmount as Credit, jb.JobCode as Job from
                    (Journal as j inner join Accounts as a on j.AccountID = a.AccountID) left join Jobs as jb on j.JobID = jb.JobID where j.TransactionNumber NOT LIKE 'SYS-%' and j.AccountID = " + Entity;
                }

                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sqlcon, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow rw in dt.Rows)
                {
                    DateTime lTranUTC = (DateTime)rw["TransactionDate"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    rw["TransactionDate"] = lTranLocal;
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
        private void LoadTranInvoice(bool noDate = true)
        {
            SqlConnection con_ = null;

            try
            {
                string sqlcon = "";
                if (Entity != "")
                {
                    sqlcon = @"SELECT s.SalesNumber as TransactionNumber,
                            j.Type, s.TransactionDate,a.TradeDebtorGLCode, 
                            CAST(s.Memo as varchar) as Memo, 
                            s.GrandTotal as Charges,
                            null as Payments, '' as Job 
                            FROM ( Sales as s inner join Journal as j on s.SalesID = j.EntityID )   
                            INNER JOIN Preference as a  on  j.AccountID = a.TradeDebtorGLCode 
                            WHERE (ISNULL(j.DebitAmount,0) - ISNULL(j.CreditAmount,0))  = s.GrandTotal and j.EntityID = " + Entity;
                    sqlcon += @" UNION SELECT p.PaymentNumber as TransactionNumber,
                            'SP' as Type , p.TransactionDate, a.TradeDebtorGLCode,
                            CAST(p.Memo as varchar) as Memo, 
                            null as Charges, l.Amount as Payments, '' as Job 
                            FROM (( Payment as p inner join PaymentLines as l on p.PaymentID = l.PaymentID ) 
                            INNER JOIN Preference as a on p.AccountID = a.TradeDebtorGLCode ) 
                            INNER JOIN Sales as s on l.EntityID = s.SalesID where p.PaymentFor = 'Sales' and l.EntityID = " + Entity;

                }
                else
                {
                  
                    DateTime sdate = Convert.ToDateTime(InvSDate.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                    DateTime edate = Convert.ToDateTime(InvEDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                    string lfilter = "";
                    if (this.rdoInvoiceNo.Checked)
                    {
                        lfilter = " s.SalesNumber LIKE '" + this.txtInvoice.Text + "%'";
                    }
                    if (this.rdoPO.Checked)
                    {
                        lfilter = " s.CustomerPONumber LIKE '" + this.txtInvoice.Text + "%'";
                    }
                    sqlcon = @"SELECT s.SalesNumber as TransactionNumber, 
                                j.Type, s.TransactionDate,
                                a.TradeDebtorGLCode,
                                CAST(s.Memo as varchar) as Memo, 
                                s.GrandTotal as Charges,
                                null as Payments,
                                '' as Job 
                                FROM ( Sales as s inner join Journal as j on s.SalesID = j.EntityID )
                                INNER JOIN Preference as a  on  j.AccountID = a.TradeDebtorGLCode 
                                WHERE (ISNULL(j.DebitAmount,0) - ISNULL(j.CreditAmount,0))  = s.GrandTotal and s.TransactionDate 
                                BETWEEN '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                "AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") +"' "+ (lfilter != "" ? " and " : "") + lfilter;
                    sqlcon += @" UNION
                                SELECT p.PaymentNumber as TransactionNumber,
                                'SP' as Type, p.TransactionDate, a.TradeDebtorGLCode, CAST(p.Memo as varchar) as Memo, 
                                null as Charges, l.Amount as Payments, '' as Job 
                                FROM
                        (( Payment as p inner join PaymentLines as l on p.PaymentID = l.PaymentID ) 
                        inner join Preference as a on p.AccountID = a.TradeDebtorGLCode ) 
                        inner join Sales as s on l.EntityID = s.SalesID  where p.PaymentFor = 'Sales'" + (lfilter != "" ? " and " : "") + lfilter;



                }



                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sqlcon, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow rw in dt.Rows)
                {
                    DateTime lTranUTC = (DateTime)rw["TransactionDate"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    rw["TransactionDate"] = lTranLocal;
                }
                this.dgInvoice.DataSource = dt;
                this.dgInvoice.Columns[0].HeaderText = "Transaction No";
                this.dgInvoice.Columns[2].HeaderText = "Transaction Date";
                this.dgInvoice.Columns[3].HeaderText = "Account";
                this.dgInvoice.Columns[4].HeaderText = "Memo";
                this.dgInvoice.Columns[5].DefaultCellStyle.Format = "C2";
                this.dgInvoice.Columns[6].DefaultCellStyle.Format = "C2";
                this.dgInvoice.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgInvoice.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dgInvoice.Columns[0].Width = 100;
                this.dgInvoice.Columns[1].Width = 50;
                this.dgInvoice.Columns[2].Width = 80;
                this.dgInvoice.Columns[3].Width = 80;
                this.dgInvoice.Columns[4].Width = 240;
                this.dgInvoice.Columns[5].Width = 100;
                this.dgInvoice.Columns[6].Width = 100;
                this.dgInvoice.Columns[7].Width = 100;

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
       
        private void LoadTranItem(bool noDate = true)
        {
            SqlConnection con_ = null;

            try
            {
                string sqlcon = "";
                if (Entity != "")
                {
                    sqlcon = @"SELECT TransactionNumber,Type,TransactionDate,TradeCreditorGLCode, Memo, Debit,Credit, JobCode,RecordID FROM 
                        (( SELECT s.SalesNumber as TransactionNumber, 'SI' as Type, s.TransactionDate, CAST(s.Memo as varchar) as Memo, CASE when ShipQty < 0 then TotalCost else NULL END as Debit, CASE when ShipQty > 0 then TotalCost else NULL END as Credit, j.JobCode, s.SalesID as RecordID, l.EntityID
                        from ( Sales as s inner join SalesLines as l on s.SalesID = l.SalesID ) left join Jobs as j on l.JobID = j.JobID where s.LayoutType = 'Item' and  l.EntityID = " + Entity;

                    sqlcon += @"UNION SELECT s.PurchaseNumber as TransactionNumber, CASE when s.PurchaseType = 'ORDER' Then 'RI' else 'PB' END as Type, s.TransactionDate, CAST(s.Memo as varchar) as Memo, CASE when ReceiveQty > 0 then l.SubTotal else NULL END as Debit, CASE when ReceiveQty < 0 then l.SubTotal else NULL END as Credit, j.JobCode, s.PurchaseID as RecordID, l.EntityID
                    from ( Purchases as s inner join PurchaseLines as l on s.PurchaseID = l.PurchaseID ) left join Jobs as j on l.JobID = j.JobID where s.LayoutType = 'Item' and l.EntityID = " + Entity;

                    sqlcon += @"UNION SELECT s.ItemAdjNumber as TransactionNumber, s.Type, s.TransactionDate, CAST(s.Memo as varchar) as Memo, CASE when Qty > 0 then l.AmountEx else NULL END as Debit, CASE when Qty < 0 then l.AmountEx else NULL END as Credit, j.JobCode, s.ItemAdjID as RecordID , l.ItemID as EntityID
                    from ( ItemsAdjustment as s inner join ItemsAdjustmentLines as l on s.ItemAdjID = l.ItemAdjID ) left join Jobs as j on l.JobID = j.JobID where l.ItemID  = " + Entity ;
                    
                    sqlcon += @" ) as x inner join Items as i on x.EntityID = i.ID ) inner join Preference as a on i.AssetAccountID = a.TradeCreditorGLCode";


                }
                else
                {
                  
                    DateTime sdate = Convert.ToDateTime(ItemSDate.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                    DateTime edate = Convert.ToDateTime(ItemEDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                    string lfilter = "";
                    string lSfilter = "";
                    if (rdoItem.Checked)
                    {
                        if (lblItemID.Text == "")
                        {
                            MessageBox.Show("Please specify a Customer.");
                            this.txtCustomer.Focus();
                            return;
                        }
                        lfilter = " l.EntityID = " + this.lblItemID.Text;
                        lSfilter = " l.ItemID = " + this.lblItemID.Text;
                    }

                    sqlcon = @"SELECT TransactionNumber,Type,TransactionDate,TradeCreditorGLCode, Memo, Debit,Credit, JobCode,RecordID FROM 
                        (( SELECT s.SalesNumber as TransactionNumber, 'SI' as Type, s.TransactionDate, CAST(s.Memo as varchar) as Memo, CASE when ShipQty < 0 then TotalCost else NULL END as Debit, CASE when ShipQty > 0 then TotalCost else NULL END as Credit, j.JobCode, s.SalesID as RecordID, l.EntityID
                        from ( Sales as s inner join SalesLines as l on s.SalesID = l.SalesID ) left join Jobs as j on l.JobID = j.JobID where s.LayoutType = 'Item' and  s.TransactionDate BETWEEN  '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + (lfilter != "" ? " and " : "") + lfilter;

                    sqlcon += @"UNION SELECT s.PurchaseNumber as TransactionNumber, CASE when s.PurchaseType = 'ORDER' Then 'RI' else 'PB' END as Type, s.TransactionDate, CAST(s.Memo as varchar) as Memo, CASE when ReceiveQty > 0 then l.SubTotal else NULL END as Debit, CASE when ReceiveQty < 0 then l.SubTotal else NULL END as Credit, j.JobCode, s.PurchaseID as RecordID, l.EntityID
                    from ( Purchases as s inner join PurchaseLines as l on s.PurchaseID = l.PurchaseID ) left join Jobs as j on l.JobID = j.JobID where s.LayoutType = 'Item' and s.TransactionDate BETWEEN  '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + (lfilter != "" ? " and " : "") + lfilter;

                    sqlcon += @"UNION SELECT s.ItemAdjNumber as TransactionNumber, s.Type, s.TransactionDate, CAST(s.Memo as varchar) as Memo, CASE when Qty > 0 then l.AmountEx else NULL END as Debit, CASE when Qty < 0 then l.AmountEx else NULL END as Credit, j.JobCode, s.ItemAdjID as RecordID , l.ItemID as EntityID
                    from ( ItemsAdjustment as s inner join ItemsAdjustmentLines as l on s.ItemAdjID = l.ItemAdjID ) left join Jobs as j on l.JobID = j.JobID where s.TransactionDate BETWEEN  '" + sdate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + (lSfilter != "" ? " and " : "") + lSfilter;

                    sqlcon += @" ) as x inner join Items as i on x.EntityID = i.ID ) inner join Preference as a on i.AssetAccountID = a.TradeCreditorGLCode";


                }



                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sqlcon, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow rw in dt.Rows)
                {
                    DateTime lTranUTC = (DateTime)rw["TransactionDate"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    rw["TransactionDate"] = lTranLocal;
                }
                this.dgItems.DataSource = dt;
                this.dgItems.Columns[0].HeaderText = "Transaction No";
                this.dgItems.Columns[2].HeaderText = "Transaction Date";
                this.dgItems.Columns[3].HeaderText = "Account";
                this.dgItems.Columns[4].HeaderText = "Memo";
                this.dgItems.Columns[5].DefaultCellStyle.Format = "C2";
                this.dgItems.Columns[6].DefaultCellStyle.Format = "C2";
                this.dgItems.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgItems.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgItems.Columns["RecordID"].Visible = false;

                this.dgItems.Columns[0].Width = 100;
                this.dgItems.Columns[1].Width = 50;
                this.dgItems.Columns[2].Width = 80;
                this.dgItems.Columns[3].Width = 80;
                this.dgItems.Columns[4].Width = 240;
                this.dgItems.Columns[5].Width = 100;
                this.dgItems.Columns[6].Width = 100;
                this.dgItems.Columns[7].Width = 100;

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
        private void btnShowInvoice_Click(object sender, EventArgs e)
        {
            LoadTranInvoice();
        }

        private void btnSearchAccounts_Click(object sender, EventArgs e)
        {
            LoadTranAccount();
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                this.lblCustomerID.Text = lProfile[0];
                this.txtCustomer.Text = lProfile[2];
                
            }
        }

        private void pbSupplier_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                this.lblSupplierID.Text = lProfile[0];
                this.txtSupplier.Text = lProfile[2];

            }
        }

        private void rdoAllCustomers_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAllCustomers.Checked)
            {
                this.txtCustomer.Text = "";
                this.txtCustomer.Enabled = false;
                this.pbCustomer.Enabled = false;
                this.lblCustomerID.Text = "";
            }
         
        }

        private void rdoAllSuppliers_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAllSuppliers.Checked)
            {
                this.txtSupplier.Text = "";
                this.txtSupplier.Enabled = false;
                this.pbSupplier.Enabled = false;
                this.lblSupplierID.Text = "";
            }
        }

        private void rdoAllItems_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAllItems.Checked)
            {
                this.txtItem.Text = "";
                this.txtItem.Enabled = false;
                this.pbItem.Enabled = false;
                this.lblItemID.Text = "";
            }
        }

        private void pbItem_Click(object sender, EventArgs e)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.SELF);           
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                this.lblItemID.Text = ItemRows.Cells[0].Value.ToString();
                this.txtItem.Text = ItemRows.Cells[2].Value.ToString();
                this.lblItemName.Text = ItemRows.Cells[3].Value.ToString();


            }
        }

        private void rdoCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCustomer.Checked)
            {                
                this.txtCustomer.Enabled = true;
                this.pbCustomer.Enabled = true;                
            }
        }

        private void rdoSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSupplier.Checked)
            {
                this.txtSupplier.Enabled = true;
                this.pbSupplier.Enabled = true;
               
            }
        }

        private void rdoItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoItem.Checked)
            {
                this.txtItem.Enabled = true;
                this.pbItem.Enabled = true;
            }
        }

        private void rdoAllInvoices_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAllInvoices.Checked)
            {
                this.txtInvoice.Text = "";
                this.txtInvoice.Enabled = false;
               
            }
        }

        private void rdoInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoInvoiceNo.Checked)
            {
                this.txtInvoice.Enabled = true;

            }
        }

        private void rdoPO_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPO.Checked)
            {
                this.txtInvoice.Enabled = true;

            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            LoadTranCustomer();
        }

        private void CusEDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTranCustomer();
        }

        private void CusSDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTranCustomer();
        }

        private void btnSearchSuppliers_Click(object sender, EventArgs e)
        {
            LoadTranSupplier();
        }

        private void SupSDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTranSupplier();
        }

        private void SupEDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTranSupplier();
        }

        private void btnSearchItems_Click(object sender, EventArgs e)
        {
            LoadTranItem();
        }

        private void ItemSDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTranItem();
        }

        private void ItemEDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTranItem();
        }



        private void ShowTran(string pTranType, string pTranNo)
        {
            switch (pTranType.Trim())
            {
                case "RI":
                case "PB":
                    LoadPurchase(pTranNo);
                    break;
                case "HP":
                    LoadHistoricalPurchase(pTranNo);
                    break;

                case "SI":
                    LoadSales(pTranNo);
                    break;
                case "HS":
                    LoadHistoricalSale(pTranNo);
                    break;

                case "BP":
                    LoadPurchasePayment(pTranNo);
                    break;
                case "SP":
                    LoadSalesPayment(pTranNo);
                    break;
                case "IB":
                    LoadBuildItems(pTranNo);
                    break;
                case "IA":
                    LoadStockAdjustments(pTranNo);
                    break;
                case "BD":
                    break;
            }
        }

        private void LoadPurchase(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Purchases where PurchaseNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lPID = dt.Rows[0]["PurchaseID"].ToString();
                    string lShippingID = dt.Rows[0]["ShippingContactID"].ToString();
                    string lpurchaseType = dt.Rows[0]["PurchaseType"].ToString();
                    CommonClass.EnterPurchasefrm = new EnterPurchase(CommonClass.InvocationSource.REGISTER, lPID, lShippingID, lpurchaseType);
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

        private void LoadHistoricalPurchase(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Purchases where PurchaseNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lPID = dt.Rows[0]["PurchaseID"].ToString();
                    CommonClass.APBalanceEntryFrm = new APBalanceEntry("Accounts Payable Starting Balances", "", lPID);
                    this.Cursor = Cursors.WaitCursor;
                    CommonClass.APBalanceEntryFrm.MdiParent = this.MdiParent;
                    CommonClass.APBalanceEntryFrm.Show();
                    CommonClass.APBalanceEntryFrm.Focus();
                    if (CommonClass.APBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                    {
                        CommonClass.APBalanceEntryFrm.Close();
                    }
                    this.Cursor = Cursors.Default;
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
        private void LoadSales(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Sales where SalesNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lSalesID = dt.Rows[0]["SalesID"].ToString();

                    CommonClass.EnterSalesfrm = new EnterSales(CommonClass.InvocationSource.REGISTER, lSalesID);
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

        private void LoadHistoricalSale(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Sales where SalesNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lSalesID = dt.Rows[0]["SalesID"].ToString();

                    CommonClass.ARBalanceEntryFrm = new ARBalanceEntry("Accounts Receivable Starting Balances", "", lSalesID);
                    this.Cursor = Cursors.WaitCursor;
                    CommonClass.ARBalanceEntryFrm.MdiParent = this.MdiParent;
                    CommonClass.ARBalanceEntryFrm.Show();
                    CommonClass.ARBalanceEntryFrm.Focus();
                    if (CommonClass.ARBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                    {
                        CommonClass.ARBalanceEntryFrm.Close();
                    }
                    this.Cursor = Cursors.Default;
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

        private void LoadStockAdjustments(string pTranNo)
        {
            try
            {
                StockAdjustments StockAdjustmentsFrm = new StockAdjustments(CommonClass.InvocationSource.SELF, null, null, pTranNo);
                this.Cursor = Cursors.WaitCursor;
                StockAdjustmentsFrm.MdiParent = this.MdiParent;
                StockAdjustmentsFrm.Show();
                StockAdjustmentsFrm.Focus();
                if (StockAdjustmentsFrm.DialogResult == DialogResult.Cancel || StockAdjustmentsFrm.DialogResult == DialogResult.OK)
                {
                    StockAdjustmentsFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadBuildItems(string pTranNo)
        {
            try
            {
                BuildItems BuildItemsFrm = new BuildItems(CommonClass.InvocationSource.SELF, null, pTranNo);
                this.Cursor = Cursors.WaitCursor;
                BuildItemsFrm.MdiParent = this.MdiParent;
                BuildItemsFrm.Show();
                BuildItemsFrm.Focus();
                if (BuildItemsFrm.DialogResult == DialogResult.Cancel || BuildItemsFrm.DialogResult == DialogResult.OK)
                {
                    BuildItemsFrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSalesPayment(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Payment where PaymentNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lPaymentID = dt.Rows[0]["PaymentID"].ToString();

                    CommonClass.SRPaymentsfrm = new SalesReceivePayment(CommonClass.InvocationSource.CUSTOMER, lPaymentID);
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

        private void LoadPurchasePayment(string pTranNo)
        {
            SqlConnection con_ = null;
            try
            {
                string sql = "SELECT * from Payment where PaymentNumber = '" + pTranNo + "'";
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(sql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string lPaymentID = dt.Rows[0]["PaymentID"].ToString();

                    CommonClass.PRPaymentsfrm = new PurchasePayments(CommonClass.InvocationSource.SUPPLIER, lPaymentID);
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

        private void dgCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string lTranNo = this.dgCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            string lType = this.dgCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            ShowTran(lType, lTranNo);
        }

        private void dgSup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string lTranNo = this.dgSup.Rows[e.RowIndex].Cells[0].Value.ToString();
            string lType = this.dgSup.Rows[e.RowIndex].Cells[1].Value.ToString();
            ShowTran(lType, lTranNo);
        }

        private void dgItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string lTranNo = this.dgItems.Rows[e.RowIndex].Cells[0].Value.ToString();
            string lType = this.dgItems.Rows[e.RowIndex].Cells[1].Value.ToString();
            ShowTran(lType, lTranNo);
        }

        private void dgInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string lTranNo = this.dgInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
            string lType = this.dgInvoice.Rows[e.RowIndex].Cells[1].Value.ToString();
            ShowTran(lType, lTranNo);
        }
    }
}
