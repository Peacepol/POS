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
using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;

namespace AbleRetailPOS
{
    public partial class RptTaxTransactionsS : Form
    {
        private static System.Data.DataTable TbRep;
        private static System.Data.DataTable TbSub;
        private static System.Data.DataTable TbGrid;
        string lRptFile = "";
        string sdate = "";
        string edateparam = "";
        private int index = 0;
        private string sort = " asc";
        private string subtile = "";
        private bool CanView = false;

        public RptTaxTransactionsS()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
            }
        }

        private void RptTaxTransactionsS_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadTaxCodes();
        }

        private void LoadTaxCodes()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);             
                string sql = "SELECT CAST('true' AS bit) AS Include,TaxCode,TaxCodeDescription,TaxPercentageRate from TaxCodes order by TaxCode";             

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                this.dgAccounts.DataSource = dt;
                this.dgAccounts.Columns[3].Visible = false;
                this.dgAccounts.Columns[1].Frozen = true;
                this.dgAccounts.Columns[1].HeaderText = "Tax Code";
                this.dgAccounts.Columns[2].Frozen = true;
                this.dgAccounts.Columns[2].HeaderText = "Description";
                this.dgAccounts.Columns[3].Frozen = true;
                this.dgAccounts.Columns[3].HeaderText = "Rate";

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;   
            LoadReport();
            Reports.ReportParams acctlistparams = new Reports.ReportParams();
            acctlistparams.PrtOpt = 1;
            acctlistparams.ReportName = lRptFile;
            acctlistparams.Rec.Add(TbRep);
            acctlistparams.RptTitle = "Tax Transactions Summary";
            acctlistparams.Params = "compname|startdate|enddate";
            acctlistparams.PVals = CommonClass.CompName.Trim() + "|" + sdate + "|" + edateparam;
            CommonClass.ShowReport(acctlistparams);
            Cursor.Current = Cursors.Default;
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {
                System.Data.DataTable tbSub1 = new System.Data.DataTable();
                DateTime dtpfromutc = Convert.ToDateTime(dtpfrom.Value.ToString("yyyy-MM-dd 00:00:00")).ToUniversalTime();
                DateTime dtptoutc = Convert.ToDateTime(dtpto.Value.ToString("yyyy-MM-dd 23:59:59")).ToUniversalTime();

                sdate = dtpfromutc.ToString("yyyy-MM-dd HH:mm:ss");
                edateparam = dtptoutc.ToString("yyyy-MM-dd HH:mm:ss");

                string IncTaxCodes = GetIncludedTax();
               // string pAccts = (IncAccts != "" ? " AND j.AccountID IN (" + IncAccts + ")" : "");
               

              
                string sql = "";
                sql = @"SELECT d.TaxCode,t.TaxCodeDescription,SaleValue,PurchaseValue,TaxCollected,TaxPaid,TransactionDate,TranID,TransactionNo, profilename,  t.TaxPercentageRate from ( ";

                //sql += @" SELECT m.TransactionDate,m.MoneyInID as TranID,m.MoneyInNumber as TransactionNo,l.TaxInclusiveAmount as SaleValue, null as PurchaseValue, (l.TaxInclusiveAmount - l.TaxExclusiveAmount) as TaxCollected, null as TaxPaid, p.Name as profilename, l.TaxCode 
                //    FROM MoneyIn as m inner join MoneyInLines as l on m.MoneyInID = l.MoneyInID inner join Profile as p on m.ProfileID = p.id where l.TransactionDate BETWEEN '" + sdate + "' AND '" + edateparam + "' and l.TaxCode IN (" + IncTaxCodes + ") ";

                //sql += @" UNION SELECT m.TransactionDate,m.MoneyOutID as TranID,m.MoneyOutNumber as TransactionNo,null as SaleValue, l.TaxInclusiveAmount as PurchaseValue, null as TaxCollected, (l.TaxInclusiveAmount - l.TaxExclusiveAmount) as TaxPaid, p.Name as profilename, l.TaxCode 
                //    FROM MoneyOut as m inner join MoneyOutLines as l on m.MoneyOutID = l.MoneyOutID inner join Profile as p on m.ProfileID = p.id where l.TransactionDate BETWEEN '" + sdate + "' AND '" + edateparam + "' and l.TaxCode IN (" + IncTaxCodes + ")";

                //For Sale & General Journal entry
                sql += @" SELECT m.TransactionDate,m.RecordJournalID as TranID,m.RecordJournalNumber as TransactionNo,TaxExclusiveAmount as SaleValue, null as PurchaseValue, (l.TaxInclusiveAmount - l.TaxExclusiveAmount) as TaxCollected, null as TaxPaid, m.Memo as profilename, l.TaxCode
                    FROM RecordJournal as m inner join RecordJournalLine as l on m.RecordJournalID = l.RecordJournalID where Type in ('I', 'J') and TaxCode != '' and l.TransactionDate BETWEEN '" + sdate + "' AND '" + edateparam + "' and l.TaxCode IN (" + IncTaxCodes + ")";

                //For Purchase
                sql += @" UNION SELECT m.TransactionDate,m.RecordJournalID as TranID,m.RecordJournalNumber as TransactionNo,null as SaleValue, TaxExclusiveAmount as PurchaseValue, null as TaxCollected, (l.TaxInclusiveAmount - l.TaxExclusiveAmount) as TaxPaid, m.Memo as profilename, l.TaxCode
                    FROM RecordJournal as m inner join RecordJournalLine as l on m.RecordJournalID = l.RecordJournalID where Type = 'B' and TaxCode != '' and l.TransactionDate BETWEEN '" + sdate + "' AND '" + edateparam + "' and l.TaxCode IN (" + IncTaxCodes + ") ";

                // For Sales
                sql += @"UNION SELECT  TransactionDate, s.SalesID as TranID, SalesNumber as TransactionNo,  sum(TotalAmount) as SaleValue, null as PurchaseValue, sum(TaxAmount) as TaxCollected, null as TaxPaid, s.Name as profileName,TaxCode from 
                    (SELECT s.SalesNumber, s.SalesID, s.TransactionDate,p.Name, l.TaxCode, l.TaxRate, l.TotalAmount, l.SubTotal, l.TaxAmount from Sales s inner join SalesLines l on s.SalesID = l.SalesID inner join Profile as p on s.CustomerID = p.id where s.SalesType = 'INVOICE' and s.TransactionDate BETWEEN '" + sdate + "' AND '" + edateparam + "'  and l.TaxCode IN (" + IncTaxCodes + ")";
                sql += @" UNION SELECT s.SalesNumber, s.SalesID, s.TransactionDate,p.Name, s.FreightTaxCode as TaxCode,s.FreightTaxRate as TaxRate,(s.FreightSubTotal + s.FreightTax) as TotalAmount, s.FreightSubTotal as SubTotal, s.FreightTax as TaxAmount from Sales s inner join Profile as p on s.CustomerID = p.id where  s.SalesType = 'INVOICE' and s.TransactionDate BETWEEN '" + sdate + "' AND '" + edateparam + "' and s.FreightTaxCode IN (" + IncTaxCodes + ") ";
                sql += @"    ) as s  group by SalesNumber,s.SalesID,  TransactionDate, s.Name,  TaxCode";

                //For Purchases
                sql += @" UNION SELECT TransactionDate, s.PurchaseID as TranID, PurchaseNumber as TransactionNo, null as SaleValue, sum(TotalAmount) as PurchaseValue,null as TaxCollected,  sum(TaxAmount) as TaxPaid, s.Name as profileName,s.TaxCode from
                    (SELECT s.PurchaseNumber, s.PurchaseID, s.TransactionDate, p.Name, l.TaxCode, l.TaxRate, l.TotalAmount, l.SubTotal, l.TaxAmount from Purchases s inner join PurchaseLines l on s.PurchaseID = l.PurchaseID inner join Profile as p on s.SupplierID = p.id where s.PurchaseType = 'BILL' and s.TransactionDate  BETWEEN '" + sdate + "' AND '" + edateparam + "'  and l.TaxCode IN (" + IncTaxCodes + ")";
                sql += @" UNION SELECT s.PurchaseNumber, s.PurchaseID, s.TransactionDate, p.Name, p.FreightTaxCode as TaxCode, s.FreightTaxRate as TaxRate, (s.FreightSubTotal + s.FreightTax) as TotalAmount, s.FreightSubTotal as SubTotal, s.FreightTax as TaxAmount from Purchases s inner join Profile as p on s.SupplierID = p.id where s.PurchaseType = 'BILL' and s.TransactionDate BETWEEN '" + sdate + "' AND '" + edateparam + "' and s.FreightTaxCode IN (" + IncTaxCodes + ") ";
                sql += @" ) as s  group by PurchaseNumber, PurchaseID, TransactionDate, s.Name,  s.TaxCode";

                //ENDING CLAUSE
                sql += @") as d inner join TaxCodes as t on d.TaxCode = t.TaxCode";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql,con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new System.Data.DataTable();               
                da.Fill(TbRep);
                foreach (DataRow rw in TbRep.Rows)
                {
                    DateTime lTranUTC = (DateTime)rw["TransactionDate"];
                    DateTime lTranLocal = lTranUTC.ToLocalTime();
                    lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                    rw["TransactionDate"] = lTranLocal;
                }
               
               
                if (this.rdoC.Checked)
                {
                    lRptFile = "TaxTransactionsSC.rpt";

                }
                else if(this.rdoP.Checked)
                {
                    lRptFile = "TaxTransactionsSP.rpt";
                }
                else
                {
                    lRptFile = "TaxTransactionsSCP.rpt";
                }          

              
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private string GetIncludedTax()
        {
            string retAccts = "";
            foreach(DataGridViewRow dvr in this.dgAccounts.Rows)
            {
                if(dvr.Cells["TaxCode"].Value.ToString().Trim() != "")
                {
                    if ((bool)dvr.Cells["Include"].Value)
                    {
                        retAccts += (retAccts != "" ? ",'" + dvr.Cells["TaxCode"].Value.ToString() + "'" : "'" + dvr.Cells["TaxCode"].Value.ToString() + "'");
                    }
                }             
            }
            return retAccts;
        }

        private static System.Data.DataTable CalculateActivity(System.Data.DataTable pTb, string pSDate, string pEDate, bool pExNoTran)
        {
            Decimal lOpening = 0;           
            foreach (DataRow dr in pTb.Rows)
            {
                string ID = dr["AccountID"].ToString();
                lOpening = 0;// TransactionClass.CalculateOpeningBalance(Convert.ToDecimal(dr["ThisYearOpeningBalance"].ToString()), dr["AccountID"].ToString(), pSDate);
                System.Data.DataTable ltbSub = new System.Data.DataTable();
                ltbSub = GetAccountTran(dr["AccountID"].ToString(), lOpening, pSDate, pEDate, pExNoTran);
                dr["BeginningBalance"] = lOpening;
                if (TbSub.Rows.Count > 0)
                {
                    TbSub.Merge(ltbSub);
                }
                else
                {
                    TbSub = ltbSub.Copy();
                }
            }       
            return pTb;
        }

        private  static System.Data.DataTable GetAccountTran(string pAccountID, decimal pOpeningBal, string psdate, string pedate,bool exNoTran)
        {
            SqlConnection con = null;
            try
            {
                System.Data.DataTable ltbSub = new System.Data.DataTable();
                string sql = "";

                if (exNoTran)
                {
                    sql = @"SELECT a.AccountID,a.AccountNumber,a.AccountName, a.AccountClassificationID,a.ThisYearOpeningBalance,j.TransactionNumber,j.TransactionDate,j.Type,ISNULL(j.DebitAmount,0) as Debit,ISNULL(j.CreditAmount,0) as Credit,j.JobID,js.JobCode,CAST(0 as float) as BeginningBalance,CAST(0 as float) as EndingBalance 
                          FROM ( Accounts as a inner join Journal as j on a.AccountID = j.AccountID )  left join Jobs as js on j.JobID =  js.JobID where  TransactionDate BETWEEN '" + psdate + "' AND '" + pedate + "' and j.AccountID = " + pAccountID;
                }
                else
                {
                    sql = @"SELECT a.AccountID,a.AccountNumber,a.AccountName, a.AccountClassificationID,a.ThisYearOpeningBalance,j.TransactionNumber,j.TransactionDate,j.Type,ISNULL(j.DebitAmount,0) as Debit,ISNULL(j.CreditAmount,0) as Credit,j.JobID,js.JobCode,CAST(0 as float) as BeginningBalance,CAST(0 as float) as EndingBalance 
                          FROM ( Accounts as a left join Journal as j on a.AccountID = j.AccountID )  left join Jobs as js on j.JobID =  js.JobID where  TransactionDate BETWEEN '" + psdate + "' AND '" + pedate + "' and j.AccountID = " + pAccountID;
                }

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();               

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new System.Data.DataTable();
                da.Fill(ltbSub);
                Decimal lOpening = pOpeningBal;
                Decimal lNet = 0;
                Decimal lDebit = 0;
                Decimal lCredit = 0;
                Decimal lEnding = 0;
                foreach (DataRow dr in ltbSub.Rows)
                {
                    string ID = dr["AccountID"].ToString();
                    lDebit = Convert.ToDecimal(dr["Debit"].ToString());
                    lCredit = Convert.ToDecimal(dr["Credit"].ToString());
                    dr["BeginningBalance"] = lOpening;

                    switch ((dr["AccountClassificationID"].ToString()))
                    {
                        case "A":
                            lNet = lDebit - lCredit;
                            break;
                        case "COS":
                            lNet = lDebit - lCredit;
                            break;
                        case "EXP":
                            lNet = lDebit - lCredit;
                            break;
                        case "OE":
                            lNet = lDebit - lCredit;
                            break;
                        case "L":
                            lNet = lCredit - lDebit;
                            break;
                        case "I":
                            lNet = lCredit - lDebit;
                            break;
                        case "OI":
                            lNet = lCredit - lDebit;
                            break;
                        case "EQ":
                            lNet = lCredit - lDebit;
                            break;
                    }
                    lEnding = lOpening + lNet;                    
                    dr["EndingBalance"] = lEnding;
                    lOpening = lEnding;
                }

                return ltbSub;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void lblSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach(DataGridViewRow dr in this.dgAccounts.Rows)
            {
                dr.Cells["Include"].Value = true;
            }
        }

        private void lblUnselect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dr in this.dgAccounts.Rows)
            {
                dr.Cells["Include"].Value = false;
            }
        }

        private void dgReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            LoadReport();
            TbGrid = TbRep.Copy();
            string lPrevItem = "";
            DataRow rw;
            string[] RowArray;
            int rIndex;
            DataView dv = TbGrid.DefaultView;
            dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
            TbGrid = dv.ToTable();
            if (TbGrid.Rows.Count > 0)
            {
                FormatGrid();
                if (this.rdoC.Checked)
                { 
                   for(int x = 0; x < TbGrid.Rows.Count; x++)
                    {
                        DataRow dr = TbGrid.Rows[x];
                        if (lPrevItem != dr["TaxCode"].ToString().Trim())
                        {
                            RowArray = new string[6];
                            RowArray[0] = dr["TaxCode"].ToString();
                            RowArray[1] = dr["TaxCodeDescription"].ToString();
                            RowArray[2] = (float.Parse(dr["TaxPercentageRate"].ToString()) / 100).ToString("P");
                            RowArray[3] = TotalSValue(dr["TaxCode"].ToString()).ToString("C2");
                            RowArray[4] = TotalTaxCollected(dr["TaxCode"].ToString()).ToString("C2");
                            dgReport.Rows.Add(RowArray);
                        }
                        lPrevItem = dr["TaxCode"].ToString().Trim();
                        if (TbRep.Rows.Count - 1 == x)
                        {
                            RowArray = new string[6];
                            RowArray[0] = " TOTAL :";
                            RowArray[3] = TotalSValue().ToString("C2");
                            RowArray[4] = TotalTaxCollected().ToString("C2");
                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        }
                    }
                   
                }
                else if (this.rdoP.Checked)
                {
                    for (int x = 0; x < TbGrid.Rows.Count; x++)
                    {
                        DataRow dr = TbGrid.Rows[x];
                        if (lPrevItem != dr["TaxCode"].ToString().Trim())
                        {
                            RowArray = new string[6];
                            RowArray[0] = dr["TaxCode"].ToString();
                            RowArray[1] = dr["TaxCodeDescription"].ToString();
                            RowArray[2] = (float.Parse(dr["TaxPercentageRate"].ToString()) / 100).ToString("P");
                            RowArray[3] = TotalPValue().ToString("C2");
                            RowArray[4] = TotalTaxPaid().ToString("C2");
                            dgReport.Rows.Add(RowArray);
                        }
                        lPrevItem = dr["TaxCode"].ToString().Trim();
                        if (TbRep.Rows.Count - 1 == x)
                        {
                            RowArray = new string[6];
                            RowArray[0] = " TOTAL :";
                            RowArray[3] = TotalPValue().ToString("C2");
                            RowArray[4] = TotalTaxPaid().ToString("C2");
                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < TbGrid.Rows.Count; x++)
                    {
                        DataRow dr = TbGrid.Rows[x];
                        if (lPrevItem != dr["TaxCode"].ToString().Trim())
                        {
                            RowArray = new string[7];
                            RowArray[0] = dr["TaxCode"].ToString();
                            RowArray[1] = dr["TaxCodeDescription"].ToString();
                            RowArray[2] = (float.Parse(dr["TaxPercentageRate"].ToString())/100).ToString("P");
                            RowArray[3] = TotalSValue(dr["TaxCode"].ToString()).ToString("C2");
                            RowArray[4] = TotalPValue(dr["TaxCode"].ToString()).ToString("C2");
                            RowArray[5] = TotalTaxCollected(dr["TaxCode"].ToString()).ToString("C2");
                            RowArray[6] = TotalTaxPaid(dr["TaxCode"].ToString()).ToString("C2");
                            dgReport.Rows.Add(RowArray);
                        }
                        lPrevItem = dr["TaxCode"].ToString().Trim();
                        if (TbRep.Rows.Count - 1 == x)
                        {
                            RowArray = new string[7];
                            RowArray[0] = " TOTAL :";
                            RowArray[3] = TotalSValue().ToString("C2");
                            RowArray[4] = TotalPValue().ToString("C2");
                            RowArray[5] = TotalTaxCollected().ToString("C2");
                            RowArray[6] = TotalTaxPaid().ToString("C2");
                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        }
                    }
                }
               
                FillSortCombo();
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                Cursor.Current = Cursors.Default;
            }
        }
       public decimal TotalPValue(string TaxCode)
        {
            decimal x = 0;
            foreach(DataRow dr in TbRep.Rows)
            {
                if (TaxCode == dr["TaxCode"].ToString())
                {
                   x += decimal.Parse(dr["PurchaseValue"].ToString() == "" ? "0" : dr["PurchaseValue"].ToString());
                }
            }
            return x;
        }
        public decimal TotalPValue()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["PurchaseValue"].ToString() == "" ? "0" : dr["PurchaseValue"].ToString());
            }
            return x;

        }
        public decimal TotalTaxPaid(string TaxCode)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (TaxCode == dr["TaxCode"].ToString())
                {
                    x += decimal.Parse(dr["TaxPaid"].ToString() == "" ? "0" : dr["TaxPaid"].ToString());
                }
            }
            return x;
        }
        public decimal TotalTaxPaid()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["TaxPaid"].ToString() == "" ? "0" : dr["TaxPaid"].ToString()); 
            }
            return x;

        }
        public decimal TotalTaxCollected(string TaxCode)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (TaxCode == dr["TaxCode"].ToString())
                {
                    x += decimal.Parse(dr["TaxCollected"].ToString() == "" ? "0" : dr["TaxCollected"].ToString());
                }

            }
            return x;

        }
        public decimal TotalTaxCollected()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["TaxCollected"].ToString() == "" ? "0" : dr["TaxCollected"].ToString());
            }
            return x;
        }

        public decimal TotalSValue(string TaxCode)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (TaxCode == dr["TaxCode"].ToString())
                {
                    x += decimal.Parse(dr["SaleValue"].ToString());
                }

            }
            return x;
        }
        public decimal TotalSValue()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["SaleValue"].ToString());
            }
            return x;
        }
        void FormatGrid()
        {
            if (this.rdoC.Checked)
            {
                dgReport.ColumnCount = 5;
                dgReport.Columns[0].Name = "Tax Code";
                dgReport.Columns[1].Name = "Description";
                dgReport.Columns[2].Name = "Rate";
                dgReport.Columns[3].Name = "Sale Value";
                dgReport.Columns[4].Name = "Tax Collected";
                subtile = " - Tax Collected";
            }
            else if (this.rdoP.Checked)
            {
                dgReport.ColumnCount = 5;
                dgReport.Columns[0].Name = "Tax Code";
                dgReport.Columns[1].Name = "Description";
                dgReport.Columns[2].Name = "Rate";
                dgReport.Columns[3].Name = "Purchase Value";
                dgReport.Columns[4].Name = "Tax Paid";
                subtile = " - Tax Paid";
            }
            else
            {
                dgReport.ColumnCount = 7;
                dgReport.Columns[0].Name = "Tax Code";
                dgReport.Columns[1].Name = "Description";
                dgReport.Columns[2].Name = "Rate";
                dgReport.Columns[3].Name = "Sale Value";
                dgReport.Columns[4].Name = "Purchase Value";
                dgReport.Columns[5].Name = "Tax Collected";
                dgReport.Columns[6].Name = "Tax Paid";
                subtile = "";
                this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void FillSortCombo()
        {
            if (this.cmbSort.Items.Count == 0)
            {
                for (int i = 0; i < dgReport.ColumnCount; i++)
                {
                    this.cmbSort.Items.Add(dgReport.Columns[i].HeaderText);
                }
                this.cmbSort.Enabled = true;
                this.btnSortGrid.Enabled = true;
                this.cmbSort.SelectedIndex = 0;
            }
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? " asc" : " desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
            btnDisplay.PerformClick();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Tax Transaction Summary"+subtile+" \n From " + dtpfrom.Value.ToShortDateString() + " to " + dtpto.Value.ToShortDateString();
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("Tax Code", 100);
            dgPrinter.ColumnWidths.Add("Description", 150);
            dgPrinter.ColumnWidths.Add("Rate", 80);
            //dgPrinter.ColumnWidths.Add("PartNumber", 100);
            //dgPrinter.ColumnWidths.Add("PurchaseNumber", 100);
            //dgPrinter.ColumnWidths.Add("OrderQty", 80);
            //dgPrinter.ColumnWidths.Add("ReceiveQty", 80);
            //dgPrinter.ColumnWidths.Add("TotalAmount", 100);
            //dgPrinter.ColumnWidths.Add("Date", 80);
            //  dgPrinter.ColumnWidths.Add("", 100);
            //dgPrinter.ColumnWidths.Add("POStatus", 100);
            ////dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportExcell_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    ws.Cells[4, 1] = "Tax Code";
                    ws.Cells[4, 2] = "Description";
                    ws.Cells[4, 3] = "Rate";
                    if (this.rdoC.Checked)
                    {
                        ws.Cells[4, 4] = "Sale Value";
                        ws.Cells[4, 5] = "Tax Collected";
                    }
                    else if (this.rdoP.Checked)
                    {
                        ws.Cells[4, 4] = "Purchase Value";
                        ws.Cells[4, 5] = "Tax Paid";
                    }
                    else
                    {
                        ws.Cells[4, 4] = "Sale Value";
                        ws.Cells[4, 5] = "Purchase Value";
                        ws.Cells[4, 6] = "Tax Collected";
                        ws.Cells[4, 7] = "Tax Paid";
                    }
                   
                   
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {

                        if (item.Cells[0].Value != null)
                        {
                            ws.Cells[i, 1] = item.Cells[0].Value.ToString();
                        }
                        if (item.Cells[1].Value != null)
                        {
                            ws.Cells[i, 2] = item.Cells[1].Value.ToString();
                        }
                        if (item.Cells[2].Value != null)
                        {
                            ws.Cells[i, 3] = item.Cells[2].Value.ToString();
                        }
                        if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                        {
                            ws.Cells[i, 4] = item.Cells[3].Value.ToString();
                        }
                        if (item.Cells[4].Value != null && item.Cells[4].Value.ToString() != "")
                        {
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        }
                        if (!this.rdoC.Checked && !this.rdoP.Checked)
                        {
                            if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                            {
                                ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                            }
                            if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                            {
                                ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                            }

                        }
                       
                        //if (item.Cells[7].Value != null)
                        //{
                        //    ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                        //}
                        //if (item.Cells[8].Value != null)
                        //{
                        //    ws.Cells[i, 9] = item.Cells[8].Value.ToString();
                        //}
                        //if (item.Cells[9].Value != null)
                        //{
                        //    ws.Cells[i, 10] = item.Cells[9].Value.ToString();
                        //}
                        //if (item.Cells[10].Value != null)
                        //{
                        //    ws.Cells[i, 11] = item.Cells[10].Value.ToString();
                        //}
                        //if (item.Cells[11].Value != null)
                        //{
                        //    ws.Cells[i, 12] = item.Cells[11].Value.ToString();
                        //}

                        // ws.Cells[i, 4] = Math.Round(float.Parse(item.Cells[3].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        i++;
                    }
                    Range cellRange = null;
                    if (!this.rdoC.Checked && !this.rdoP.Checked)
                    {
                       cellRange = ws.get_Range("A1", "G3");
                    }
                    else
                    {
                       cellRange = ws.get_Range("A1", "E3");
                    }
                     
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Tax Transactions Summary";

                    //Style Table
                    if (!this.rdoC.Checked && !this.rdoP.Checked)
                    {
                        cellRange = ws.get_Range("A4", "G4");
                    }else
                    {
                        cellRange = ws.get_Range("A4", "E4");
                    }
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";
                    ws.get_Range("D4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.get_Range("E4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.get_Range("F4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.get_Range("G4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Loyalty Members has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //   this.Close();
                }
            }
        }
    }
}
