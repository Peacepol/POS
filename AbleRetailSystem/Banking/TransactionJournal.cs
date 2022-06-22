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
using RestaurantPOS.Purchase;
using RestaurantPOS.Sales;
using RestaurantPOS.Inventory;

namespace RestaurantPOS
{
    public partial class TransactionJournal : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        private string thisFormCode = "";
        private DataTable dt;

        public TransactionJournal()
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

        private void TransactionJournal_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            foreach (DataGridViewColumn column in dgDis.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgGeneral.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgInventory.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgPurchases.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgReceipts.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgridAll.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgSales.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dtpfrom.Value = DateTime.Now;
            dtpto.Value = DateTime.Now;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            switch (TabTJ.SelectedIndex)
            {
                case 0:
                    populateGrid(ref dgridAll, "All");
                    break;
                case 1:
                    populateGrid(ref dgDis, "Disbursement");
                    break;
                case 2:
                    populateGrid(ref dgReceipts, "Receipt");
                    break;
                case 3:
                    populateGrid(ref dgGeneral, "General");
                    break;
                case 4:
                    populateGrid(ref dgSales, "Sales");
                    break;
                case 5:
                    populateGrid(ref dgPurchases, "Purchases");
                    break;
                case 6:
                    populateGrid(ref dgInventory, "Inventory");
                    break;
            }       
        }
    
        private void populateGrid(ref DataGridView pdgvw, string type)
        {
           
           
            DateTime dfrom = Convert.ToDateTime(dtpfrom.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
            DateTime dto = Convert.ToDateTime(dtpto.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

            string sdate = dfrom.ToString("yyyy-MM-dd HH:mm:ss");
            string edate = dto.ToString("yyyy-MM-dd HH:mm:ss");

            string selectSql = "";

            switch (type)
            {
                case "All":
                    selectSql = @"SELECT j.*, 
                                    jb.JobCode 
                                FROM Journal j
                                LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                WHERE TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + sdate + "' AND '" + edate
                                + "' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                    break;
                case "Disbursement":
                    selectSql = @"SELECT j.*, 
                                      jb.JobCode 
                                  FROM Journal j
                                  LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                  WHERE j.Type IN ('MO','TM', 'BP') 
                                  AND TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + sdate + "' AND '" + edate
                                  + "' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                    break;
                case "Receipt":
                    selectSql = @"SELECT j.*,
                                    jb.JobCode 
                                FROM Journal j
                                LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                WHERE j.Type IN ('MI', 'SP') 
                                AND TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + sdate + "' AND '" + edate
                                + "' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                    break;
                case "General":
                    selectSql = @"SELECT j.*,
                                    jb.JobCode 
                                FROM Journal j 
                                LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                WHERE j.Type = 'JE' 
                                AND TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + sdate + "' AND '" + edate
                                + "' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                    break;
                case "Sales":
                    selectSql = @"SELECT j.*, 
                                    jb.JobCode 
                                FROM Journal j
                                LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                WHERE j.Type IN ('SI', 'SP', 'HS') 
                                AND TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + sdate + "' AND '" + edate
                                + "' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                    break;
                case "Purchases":
                    selectSql = @"SELECT j.*, 
                                    jb.JobCode 
                                FROM Journal j 
                                LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                WHERE j.Type IN ('PB', 'BP', 'HP') 
                                AND TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + sdate + "' AND '" + edate
                                + "' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                    break;
                case "Inventory":
                    selectSql = @"SELECT j.*, 
                                    jb.JobCode 
                                FROM Journal j 
                                LEFT JOIN Jobs jb ON j.JobID = jb.JobID 
                                WHERE j.Type IN ('RI', 'IA','IB') 
                                AND TransactionNumber NOT LIKE 'SYS-%' and TransactionDate BETWEEN '" + sdate + "' AND '" + edate
                                + "' ORDER BY j.TransactionDate, j.TransactionNumber, j.JournalNumberID";
                    break;
            }

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                dt = new DataTable();
                da.Fill(dt);

                string lPrevNo = "";
                string[] RowArray;
                pdgvw.Rows.Clear();
                int rIndex = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string lStrAmt = "";
                    decimal lDAmt = 0;
                    DateTime lTranDate;
                    if (lPrevNo != dt.Rows[i]["TransactionNumber"].ToString().Trim())
                    {
                        lTranDate = (DateTime)dt.Rows[i]["TransactionDate"];
                        lTranDate = lTranDate.ToLocalTime();

                        RowArray = new string[10];
                        RowArray[0] = ">>";
                        RowArray[1] = lTranDate.ToShortDateString();
                        RowArray[2] = dt.Rows[i]["Memo"].ToString();
                        RowArray[9] = dt.Rows[i]["Type"].ToString();

                        pdgvw.Rows.Add(RowArray);
                        //if (type == "All")
                        rIndex = pdgvw.Rows.Count - 1;

                        pdgvw.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        pdgvw.Rows[rIndex].DefaultCellStyle.Font = new Font(pdgvw.Font, FontStyle.Bold);

                        RowArray = new string[9];
                        RowArray[0] = "";
                        RowArray[1] = "";
                        RowArray[2] = "";
                        RowArray[3] = dt.Rows[i]["TransactionNumber"].ToString();
                        RowArray[4] = dt.Rows[i]["AccountID"].ToString();
                        RowArray[5] = "";
                        if (dt.Rows[i]["DebitAmount"].ToString().Trim() != "") //Debit
                        {
                            lStrAmt = dt.Rows[i]["DebitAmount"].ToString().Trim();
                            lDAmt = lStrAmt != "" ? Convert.ToDecimal(lStrAmt) : 0;
                            RowArray[6] = Math.Round(lDAmt, 2).ToString("C"); ;
                        }
                        else //Credit
                        {
                            lStrAmt = dt.Rows[i]["CreditAmount"].ToString().Trim();
                            lDAmt = lStrAmt != "" ? Convert.ToDecimal(lStrAmt) : 0;
                            RowArray[7] = Math.Round(lDAmt, 2).ToString("C"); ;
                        }
                        RowArray[8] = dt.Rows[i]["JobCode"].ToString();
                        pdgvw.Rows.Add(RowArray);
                    }
                    else
                    {
                        RowArray = new string[9];
                        RowArray[0] = "";
                        RowArray[1] = "";
                        RowArray[2] = "";
                        RowArray[3] = dt.Rows[i]["TransactionNumber"].ToString();
                        RowArray[4] = dt.Rows[i]["AccountID"].ToString();
                        RowArray[5] = "";
                        if (dt.Rows[i]["DebitAmount"].ToString().Trim() != "") //Debit
                        {
                            lStrAmt = dt.Rows[i]["DebitAmount"].ToString().Trim();
                            lDAmt = lStrAmt != "" ? Convert.ToDecimal(lStrAmt) : 0;
                            RowArray[6] = Math.Round(lDAmt, 2).ToString("C"); ;
                        }
                        else //Credit
                        {
                            lStrAmt = dt.Rows[i]["CreditAmount"].ToString().Trim();
                            lDAmt = lStrAmt != "" ? Convert.ToDecimal(lStrAmt) : 0;
                            RowArray[7] = Math.Round(lDAmt, 2).ToString("C"); ;
                        }

                        RowArray[8] = dt.Rows[i]["JobCode"].ToString();

                        pdgvw.Rows.Add(RowArray);
                    }
                    lPrevNo = dt.Rows[i]["TransactionNumber"].ToString().Trim();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            Reports.ReportParams generaljournalparams = new Reports.ReportParams();
            generaljournalparams.PrtOpt = 1;
            generaljournalparams.Rec.Add(dt);
            generaljournalparams.ReportName = "GeneralJournal.rpt";
            generaljournalparams.RptTitle = "General Journal";

            generaljournalparams.Params = "compname";
            generaljournalparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(generaljournalparams);
        }

        private void TabTJ_Selected(object sender, TabControlEventArgs e)
        {
            switch (TabTJ.SelectedIndex)
            {
                case 0:
                    populateGrid(ref dgridAll, "All");
                    break;
                case 1:
                    populateGrid(ref dgDis, "Disbursement");
                    break;
                case 2:
                    populateGrid(ref dgReceipts, "Receipt");
                    break;
                case 3:
                    populateGrid(ref dgGeneral, "General");
                    break;
                case 4:
                    populateGrid(ref dgSales, "Sales");
                    break;
                case 5:
                    populateGrid(ref dgPurchases, "Purchases");
                    break;
                case 6:
                    populateGrid(ref dgInventory, "Inventory");
                    break;
            }
        }

        private void dgridAll_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgridAll.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {

                string lTranNo = dgridAll.Rows[e.RowIndex + 1].Cells[3].Value.ToString().Trim();
                string lType = dgridAll.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();

                ShowTran(lType, lTranNo);
            }
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
                dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
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
                StockAdjustments StockAdjustmentsFrm = new StockAdjustments(CommonClass.InvocationSource.SELF,null,null,pTranNo);
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
   
        private void dgDis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if(dgDis.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                string lTranNo = dgDis.Rows[e.RowIndex + 1].Cells[3].Value.ToString().Trim();
                string lType = dgDis.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
                ShowTran(lType, lTranNo);
            }         
        }

        private void dgReceipts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgReceipts.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                string lTranNo = dgReceipts.Rows[e.RowIndex + 1].Cells[3].Value.ToString().Trim();
                string lType = dgReceipts.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
                ShowTran(lType, lTranNo);
            }
        }

        private void dgGeneral_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgGeneral.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                string lTranNo = dgGeneral.Rows[e.RowIndex + 1].Cells[3].Value.ToString().Trim();
                string lType = dgGeneral.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
                ShowTran(lType, lTranNo);
            }
        }

        private void dgSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgSales.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                string lTranNo = dgSales.Rows[e.RowIndex + 1].Cells[3].Value.ToString().Trim();
                string lType = dgSales.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
                ShowTran(lType, lTranNo);
            }
        }

        private void dgPurchases_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgPurchases.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                string lTranNo = dgPurchases.Rows[e.RowIndex + 1].Cells[3].Value.ToString().Trim();
                string lType = dgPurchases.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
                ShowTran(lType, lTranNo);
            }

        }

        private void dgInventory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgInventory.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
            {
                string lTranNo = dgInventory.Rows[e.RowIndex + 1].Cells[3].Value.ToString().Trim();
                string lType = dgInventory.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
                ShowTran(lType, lTranNo);
            }
        }
    } //end
}
