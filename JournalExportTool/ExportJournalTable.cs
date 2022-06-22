using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using CrystalDecisions;
using Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace JournalExportTool
{
    public partial class ExportJournalTable : Form
    {
        public static string retailServer = "";
        public System.Data.DataTable dtRetail;
        public System.Data.DataTable dtAccounts;
        public System.Data.DataTable dtNonExist;
        public static string AccpacServer = "";
        public static AbleRetailPOS.RptViewer RptViewerFrm;
        private static string CurSeries = "";
        private static string JournalNo = "";
        private static string AbleRetailUserID = "";
        private static string CurrentEarningsID = "";
        private static string CurrencyID = "";
        public ExportJournalTable()
        {
            InitializeComponent();
        }
        public enum RunSqlInsertMode
        {
            QUERY = 0,
            SCALAR
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtRetailServer.Text == "")
                return;
            if (txtdbNameRetail.Text == "")
                return;
            if (chkWARetail.Checked)
            {
                retailServer = "Data Source = " + txtRetailServer.Text + "; Initial Catalog = master; Integrated Security=True";
            }
            else
            {
                retailServer = "Data Source = " + txtRetailServer.Text + " ; Initial Catalog = master; MultipleActiveResultSets = true";
                retailServer += "; User ID = 'ableacctg' ; Password = '5!37e5CCt9'";
            }

           

            string checkdbexistencesql = "SELECT name FROM master.sys.databases WHERE name = '" + txtdbNameRetail.Text + "'";
            System.Data.DataTable dt = new System.Data.DataTable();
            runSql(retailServer, ref dt, checkdbexistencesql);
            if (dt.Rows.Count > 0)
            {
                checkRetailDb.Visible = true;
                checkRetailDb.Text = "✔";
                checkRetailDb.BackColor = Color.LimeGreen;
                checkRetailDb.Checked = true;
                cmbTranType.Enabled = true;
                retailServer = retailServer.Replace("Initial Catalog = master", "Initial Catalog = " + txtdbNameRetail.Text);
                ExportEnable();

            }
            else
            {
                checkRetailDb.Checked = false;
                checkRetailDb.Visible = true;
                checkRetailDb.Text = "X";
                checkRetailDb.BackColor = Color.DarkRed;
            }
          //  retailServer = retailServer.Replace("Initial Catalog = master", "Initial Catalog = " + txtdbNameRetail.Text);

        }
        public static int runSql(string servercon ,ref System.Data.DataTable dtOutput, string sql, Dictionary<string, object> valueParams = null)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(servercon);
                SqlCommand cmd = new SqlCommand(sql, con);
                if (valueParams != null)
                {
                    foreach (KeyValuePair<string, object> param in valueParams)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dtOutput.Clear();
                da.Fill(dtOutput);
                return dtOutput.Rows.Count;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void cmbTranType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetailTablePopulate();
        }

        void RetailTablePopulate()
        {
            dgJournal.Rows.Clear();

            string journalTypesql = @"SELECT * FROM Journal WHERE TransactionDate BETWEEN @sdate AND @edate";

            float lTotalDebit = 0;
            float lTotalCredit = 0;
            float lDebit = 0;
            float lCredit = 0;
            if (chkbConsolidate.Checked)
            {
                journalTypesql = @"SELECT DISTINCT AccountID, 
	                                SUM(DebitAmount) AS DebitAmount, 
	                                SUM(CreditAmount) AS CreditAmount, 
	                                MAX(Memo) AS Memo, 
	                                MAX(TransactionDate) AS TransactionDate,
	                                MAX(EntryDate) AS EntryDate,
	                                MAX(JobID) AS JobID,
	                                MAX(AllocationMemo) AS AllocationMemo,
	                                MAX(Category) AS Category,
	                                MAX(TransactionNumber) AS TransactionNumber,
	                                MAX(Type) AS Type,
	                                SUM(CASE(IsCleared) WHEN 1 THEN 1 ELSE 0 END) AS IsCleared,
	                                SUM(CASE(IsDeposited) WHEN 1 THEN 1 ELSE 0 END) AS IsDeposited,
	                                MAX(LocationID) AS LocationID,
	                                MAX(EntityID) AS EntityID
                                  FROM Journal 
                                  WHERE TransactionDate BETWEEN @sdate AND @edate";
            }

            switch (cmbTranType.Text)
            {
                case "Sales":
                    journalTypesql += @" AND Type IN ('HS','SI','SP','SO','SQ','SL') ";
                    break;
                case "Purchases":
                    journalTypesql += @" AND Type IN ('PO','PB','PQ','RI','BP') ";
                    break;
                case "Stock Adjustments":
                    journalTypesql += @" AND Type IN ('IA') ";
                    break;
                case "All":
                    //journalTypesql += @" ";
                    break;
            }

            if (chkbConsolidate.Checked)
            {
                journalTypesql += " GROUP BY AccountID, Type";
            }
          
            DateTime startDate = sdate.Value.ToUniversalTime();
            DateTime endDate = edate.Value.ToUniversalTime();
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@sdate", startDate);
            param.Add("@edate", endDate);
            dtRetail = new System.Data.DataTable();
            runSql(retailServer,ref dtRetail, journalTypesql, param);
           if(dtRetail.Rows.Count > 0)
            {
                for(int i = 0; i< dtRetail.Rows.Count; i++)
                {
                    DataRow dr = dtRetail.Rows[i];
                    dgJournal.Rows.Add();
                    dgJournal.Rows[i].Cells["Date"].Value = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                    dgJournal.Rows[i].Cells["Memo"].Value = dr["Memo"].ToString();
                    dgJournal.Rows[i].Cells["TransactionNumber"].Value = dr["TransactionNumber"].ToString();
                    dgJournal.Rows[i].Cells["AccountNum"].Value = dr["AccountID"].ToString();

                    lDebit = float.Parse(dr["DebitAmount"].ToString() == "" ? "0" : dr["DebitAmount"].ToString());
                    dgJournal.Rows[i].Cells["Debit"].Value = lDebit.ToString("C");

                    lCredit = float.Parse(dr["CreditAmount"].ToString() == "" ? "0" : dr["CreditAmount"].ToString());
                    dgJournal.Rows[i].Cells["Credit"].Value = lCredit.ToString("C");

                    dgJournal.Rows[i].Cells["Job"].Value = dr["JobID"].ToString();
                    dgJournal.Rows[i].Cells["TranType"].Value = dr["Type"].ToString();
                    dgJournal.Rows[i].Cells["IsCleared"].Value = dr["IsCleared"].ToString();
                    dgJournal.Rows[i].Cells["IsDeposited"].Value = dr["IsDeposited"].ToString();
                    dgJournal.Rows[i].Cells["LocationID"].Value = dr["LocationID"].ToString();
                    dgJournal.Rows[i].Cells["EntityID"].Value = dr["EntityID"].ToString();
                    lTotalDebit += lDebit;
                    lTotalCredit += lCredit;
                }
                ExportEnable();
            }
            this.txtTotalDebit.Value = Convert.ToDecimal(lTotalDebit);
            this.txtTotalCredit.Value = Convert.ToDecimal(lTotalCredit);

        }

        private void btnCon_Click(object sender, EventArgs e)
        {
            if (txtAccpacServer.Text == "")
                return;
            if (txtAccpacDb.Text == "")
                return;
            if (chkWAAccpac.Checked)
            {
                AccpacServer = "Data Source = " + txtRetailServer.Text + "; Initial Catalog = master; Integrated Security=True";
            }
            else
            {
                AccpacServer = "Data Source = " + txtAccpacServer.Text + "; Initial Catalog = master; MultipleActiveResultSets = true";
                AccpacServer += "; User ID = 'ableacctg'; Password = '5!37e5CCt9'";

            }
           
            string checkdbexistencesql = "SELECT name FROM master.sys.databases WHERE name = '" + txtAccpacDb.Text + "'";
            System.Data.DataTable dt = new System.Data.DataTable();
            runSql(AccpacServer,ref dt, checkdbexistencesql);
            if (dt.Rows.Count > 0)
            {
                checkAccpacCon.Visible = true;
                checkAccpacCon.Text = "✔";
                checkAccpacCon.BackColor = Color.LimeGreen;
                checkAccpacCon.Checked = true;
                AccpacServer = AccpacServer.Replace("Initial Catalog = master", "Initial Catalog = " + txtAccpacDb.Text);
                dtAccounts = AccountsTable();
                GetAccpacPreferences();
                AbleRetailUserID = GetAbleRetailUserID();
                if(AbleRetailUserID == "")
                {
                    MessageBox.Show("Cannot export to Able ACCPAC. Please add a user with username sys-ableretail in Able ACCPAC.");
                }
                else
                {
                    ExportEnable();
                }
                
            }
            else
            {
                checkAccpacCon.Visible = true;
                checkAccpacCon.Text = "X";
                checkAccpacCon.BackColor = Color.DarkRed;
                checkAccpacCon.Checked = false;
            }
        }


        public System.Data.DataTable AccountsTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string accountSQL= @"SELECT * FROM Accounts";
            runSql(AccpacServer, ref dt, accountSQL);
            if(dt.Rows.Count > 0)
            {
                return dt;
            }else
            {
                MessageBox.Show("Contains no data.");
                return null;
            }
        }
        private static string GetAbleRetailUserID()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string accountSQL = @"SELECT * FROM Users where user_name = 'sys-ableretail'";
            runSql(AccpacServer, ref dt, accountSQL);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["user_id"].ToString();
            }
            else
            {
                return CreateAbleRetailUser();
            }
        }
        private void GetAccpacPreferences()
        {
            System.Data.DataTable dt = new System.Data.DataTable();           
            string prefSQL = @"SELECT TOP 1 p.*, c.* FROM Preference p LEFT JOIN Currency c ON p.LocalCurrency = c.CurrencyCode";
            runSql(AccpacServer, ref dt, prefSQL);
            if (dt.Rows.Count > 0)
            {
                
                CurrencyID = dt.Rows[0]["CurrencyID"].ToString();
            }
            else
            {
                MessageBox.Show("Contains no data for ACCPAC Preferences.");
                
            }
            string LinkedAcctsSQL = @"SELECT TOP 1 * FROM LinkedAccounts";
            runSql(AccpacServer, ref dt, LinkedAcctsSQL);
           
            if (dt.Rows.Count > 0)
            {

                CurrentEarningsID = dt.Rows[0]["CurrentEarningsID"].ToString();
            }
            else
            {
                MessageBox.Show("Contains no data for ACCPAC Linked Accounts.");

            }
        }
        //Hashing function for User password
        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                //Convert to text
                var hashedInputStringBuilder = new System.Text.StringBuilder(64);
                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }


        private static string CreateAbleRetailUser()
        {
            SqlConnection con_ = null;
            string retVal = "";
            try
            {
                con_ = new SqlConnection(AccpacServer);
                string sql = @"INSERT INTO Users (
                                            user_name,
                                            user_pwd,
                                            user_fullname,
                                            user_inactive)
                                        VALUES  (
                                            @user_name,
                                            @user_pwd,
                                            @user_fullname,
                                            @user_inactive)";

                Dictionary<string, object> param = new Dictionary<string, object>();
               
                param.Add("@user_name", "sys-ableretail");
                param.Add("@user_pwd", SHA512("AbleRetail"));
                param.Add("@user_fullname", "System Able Retail Import User");
                param.Add("@user_inactive", 0);

                int i = runSql(sql, RunSqlInsertMode.SCALAR, param);
                if(i > 0)
                {
                    string useraccesssql = @"INSERT INTO User_Access (user_id, form_code, u_view, u_add, u_edit, u_delete)
                                              SELECT " + i.ToString() + " as user_id , form_code, u_view, u_add, u_edit, u_delete FROM User_Access WHERE user_id = 1";
                    int j = runSql(useraccesssql);
                }
                retVal = i.ToString();


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
            return retVal;
        }

        void ExportEnable()
        {
            if (checkRetailDb.Checked)
            {
                if (checkAccpacCon.Checked)
                {
                    if(dtAccounts!= null)
                    {
                        if (dgJournal.Rows.Count > 0)
                        {
                            btnExport.Enabled = true;
                        }
                    }
                }
            }             
        }

        private void btnExport_Click_OLD(object sender, EventArgs e)
        {
            int x = 0;
            string nonexist = "";
            foreach (DataGridViewRow dgvr in dgJournal.Rows)
            {
                for(int i = 0; i<dtAccounts.Rows.Count; i++)
                {
                    DataRow dr = dtAccounts.Rows[i];
                    double lDebit = 0;
                    double lCredit = 0;
                    if (dgvr.Cells["AccountNum"].Value.ToString().Trim() == dr["AccountNumber"].ToString().Trim())
                    {
                        string AccountID = dr["AccountID"].ToString();
                        string insertJournalsql = @"Insert into Journal (TransactionDate
                                                  ,Memo
                                                  ,AccountID
                                                  ,DebitAmount
                                                  ,CreditAmount
                                                  ,JobID
                                                  ,AllocationMemo
                                                  ,Category
                                                  ,TransactionNumber
                                                  ,Type
                                                  ,IsCleared
                                                  ,IsDeposited
                                                  ,LocationID
                                                  ,EntityID )Values (
                                                    @TransactionDate
                                                  ,@Memo
                                                  ,@AccountID
                                                  ,@DebitAmount
                                                  ,@CreditAmount
                                                  ,@JobID
                                                  ,@AllocationMemo
                                                  ,@Category
                                                  ,@TransactionNumber
                                                  ,@Type
                                                  ,@IsCleared
                                                  ,@IsDeposited
                                                  ,@LocationID
                                                  ,@EntityID)";
                        Dictionary<string, object> insertParam = new Dictionary<string, object>();
                        insertParam.Add("@TransactionDate", Convert.ToDateTime(dgvr.Cells["Date"].Value));
                        insertParam.Add("@Memo", dgvr.Cells["Memo"].Value.ToString());
                        insertParam.Add("@AccountID", AccountID);
                        lDebit = double.Parse(dgvr.Cells["Debit"].Value.ToString(), NumberStyles.Currency);
                        insertParam.Add("@DebitAmount", lDebit);
                        lCredit = double.Parse(dgvr.Cells["Credit"].Value.ToString(), NumberStyles.Currency);
                        insertParam.Add("@CreditAmount", lCredit);
                        insertParam.Add("@JobID", dgvr.Cells["Job"].Value.ToString());
                        insertParam.Add("@AllocationMemo", dgvr.Cells["Memo"].Value.ToString());
                        insertParam.Add("@Category","");
                        insertParam.Add("@TransactionNumber", dgvr.Cells["TransactionNumber"].Value.ToString());
                        insertParam.Add("@Type", dgvr.Cells["TranType"].Value.ToString());
                        insertParam.Add("@IsCleared", dgvr.Cells["IsCleared"].Value.ToString());
                        insertParam.Add("@IsDeposited", dgvr.Cells["IsDeposited"].Value.ToString());
                        insertParam.Add("@LocationID", dgvr.Cells["LocationID"].Value.ToString());
                        insertParam.Add("@EntityID", dgvr.Cells["EntityID"].Value.ToString());;
                        x += runSql(insertJournalsql, RunSqlInsertMode.QUERY, insertParam);

                    }//End if

                }//End For Loop
               
                dtNonExist = new System.Data.DataTable();
                string sqlCheckAccount = @"Select * FROM Accounts WHERE  AccountNumber = @AccountNum";
                Dictionary<string, object> param = new Dictionary<string, object>();
                string acctNum = dgvr.Cells["AccountNum"].Value.ToString();
                param.Add("@AccountNum", acctNum);
                int j = runSql(AccpacServer, ref dtNonExist, sqlCheckAccount, param);
                if(j < 0)
                {
                    nonexist += dgvr.Cells["AccountNum"].Value.ToString() + ",";
                }
            }//EndForeach

            if (x > 0)
            {
                MessageBox.Show(x + " Journal Record Successfully Exported!", "Export Info");
                btnPrintExport.Enabled = true;
            }
            if (nonexist != "")
            {
                nonexist = nonexist.Remove(nonexist.Length - 1);
                MessageBox.Show(nonexist + " Account number does not exist." , "Information");
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            //CHECK FOR NON-EXISTENT ACCOUNTNUMBER
            dtAccounts = AccountsTable();
            string lAccountNo = "";
            string lAccountID = "";
            bool ErrorExist = false;
            foreach (DataGridViewRow dgvr in dgJournal.Rows)
            {

                lAccountNo = (dgvr.Cells["AccountNum"].Value != null ? dgvr.Cells["AccountNum"].Value.ToString().Trim() : "");
                if(lAccountNo != "")
                {
                    DataRow[] dtRow = dtAccounts.Select("AccountNumber = '" + lAccountNo + "'");
                    if (dtRow.GetUpperBound(0) >= 0)
                    {
                        lAccountID = dtRow[0]["AccountID"].ToString();
                        dgvr.Cells["AccountID"].Value = lAccountID;
                        dgvr.DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.Red;
                        ErrorExist = true;
                    }

                }
                
            }

            if (ErrorExist)
            {
                MessageBox.Show("Some account numbers do not exists in ACCPAC. Please fix the non-existent account first before export can proceed.");
            }
            else
            {
                StartACCPACImport();
            }
        }
        
       
        public static int runSql(string sql, RunSqlInsertMode mode = RunSqlInsertMode.QUERY, Dictionary<string, object> valueParams = null)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(AccpacServer);
                con.Open();
                if (mode == RunSqlInsertMode.SCALAR)
                {
                    sql += "; SELECT SCOPE_IDENTITY()";
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                if (valueParams != null)
                {
                    foreach (KeyValuePair<string, object> param in valueParams)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                int returnvalue = 0;
                switch (mode)
                {
                    case RunSqlInsertMode.QUERY:
                        returnvalue = cmd.ExecuteNonQuery();
                        break;
                    case RunSqlInsertMode.SCALAR:
                        returnvalue = Convert.ToInt32(cmd.ExecuteScalar());
                        break;
                }
                return returnvalue;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnPrintExport_Click(object sender, EventArgs e)
        {
            Reports.ReportParams ExportReport = new Reports.ReportParams();
            ExportReport.PrtOpt = 1;
            ExportReport.Rec.Add(dtRetail);
            ExportReport.ReportName = "ExportReport.rpt";
            ExportReport.RptTitle = "Export Summary";
            ExportReport.Params = "sDate|eDate";
            ExportReport.PVals = sdate.Value.ToShortDateString()+"|"+edate.Value.ToShortDateString();

            ShowReport(ExportReport);
        }

        public static void ShowReport(Reports.ReportParams pParams)
        {
            string rptPath = System.Windows.Forms.Application.StartupPath + "\\Reports\\" + pParams.ReportName;
            CrystalDecisions.CrystalReports.Engine.ReportDocument crReport = null;

            if (pParams.Rec != null)
            {
                for (int i = 0; i < pParams.Rec.Count; ++i)
                {
                    if (pParams.Rec[i] != null
                        && pParams.Rec[i].Rows.Count > 0)
                    {
                        if (i == 0)
                        {
                            crReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                            crReport.Load(rptPath, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);
                        }

                        if (crReport != null)
                            crReport.Database.Tables[i].SetDataSource(pParams.Rec[i]);
                    }
                    else
                    {
                        MessageBox.Show("Report Contains No Data.");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Report Contains No Data.");
                return;
            }

            if (pParams.tblSubRpt != null)
            {
                crReport.OpenSubreport(pParams.SubRpt).SetDataSource(pParams.tblSubRpt);
            }

            foreach (KeyValuePair<string, System.Data.DataTable> child in pParams.children)
            {
                if (child.Value != null)
                {
                    crReport.OpenSubreport(child.Key).SetDataSource(child.Value);
                }
            }

            if (pParams.Params != "")
            {
                string[] a = pParams.Params.Split('|');
                string[] b = pParams.PVals.Split('|');
                for (int i = 0; i < a.Length; i++)
                {
                    crReport.SetParameterValue(a[i], b[i]);
                }
            }

            if (pParams.HideSec != "")
            {
                if (Information.IsNumeric(pParams.HideSec))
                {
                    int vs;
                    vs = Convert.ToInt32(pParams.HideSec);
                    crReport.ReportDefinition.Sections[vs].SectionFormat.EnableSuppress = true;
                }
                else
                {
                    crReport.ReportDefinition.Sections[pParams.HideSec].SectionFormat.EnableSuppress = true;
                }
            }

            if (pParams.PapSize != "")
            {
                PrintDocument doctoprint = new PrintDocument();
                for (int iterate = 0; iterate < doctoprint.PrinterSettings.PaperSizes.Count - 1; ++iterate)
                {
                    int rawKind;
                    if (doctoprint.PrinterSettings.PaperSizes[iterate].PaperName == pParams.PapSize)
                    {
                        rawKind = doctoprint.PrinterSettings.PaperSizes[iterate].RawKind;
                        crReport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawKind;
                        crReport.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                        break;
                    }
                }
            }

            switch (pParams.PrtOpt)
            {
                case 0: //Print
                    System.Drawing.Printing.PrintDocument docs = new System.Drawing.Printing.PrintDocument();
                    PrintDialog printDialg = new PrintDialog();
                    printDialg.UseEXDialog = true;
                    if (printDialg.ShowDialog() == DialogResult.OK)
                    {
                        crReport.PrintOptions.PrinterName = printDialg.PrinterSettings.PrinterName;
                        crReport.PrintToPrinter(printDialg.PrinterSettings.Copies, false, printDialg.PrinterSettings.FromPage, printDialg.PrinterSettings.ToPage);
                    }
                    break;
                case 1: //Preview
                    RptViewerFrm = new AbleRetailPOS.RptViewer();
                    RptViewerFrm.crViewer.ReportSource = crReport;
                    RptViewerFrm.ShowDialog();
                    break;
                case 2: //Save to file
                    if (pParams.fname == "")
                    {
                        MessageBox.Show("Please specify the file to save to.");
                        return;
                    }
                    crReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pParams.fname);
                    break;
            }
        }

        private void chkbConsolidate_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbTranType.Text != "")
                RetailTablePopulate();
        }

        private void ExportJournalTable_Load(object sender, EventArgs e)
        {
            //cmbTranType.SelectedIndex = 0;
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
                    ws.Cells[4, 1] = "Transaction Date";
                    ws.Cells[4, 2] = "Memo";
                    ws.Cells[4, 3] = "Transaction # ";
                    ws.Cells[4, 4] = "Account #";
                    ws.Cells[4, 5] = "Account Name";
                    ws.Cells[4, 6] = "Debit";
                    ws.Cells[4, 7] = "Credit";
                    ws.Cells[4, 8] = "Job";
                    int i = 5;
                    foreach (DataGridViewRow item in dgJournal.Rows)
                    {

                        if (item.Cells[0].Value != null && item.Cells[0].Value.ToString() != "")
                        {
                            ws.Cells[i, 1] = Convert.ToDateTime(item.Cells[0].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[1].Value != null)
                        {
                            ws.Cells[i, 2] = item.Cells[1].Value.ToString();
                        }
                        if (item.Cells[2].Value != null)
                        {
                            ws.Cells[i, 3] = item.Cells[2].Value.ToString();
                        }
                        if (item.Cells[3].Value != null)
                        {
                            ws.Cells[i, 4] = item.Cells[3].Value.ToString();
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[3].Value.ToString();
                        }
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null)
                        {
                            ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "H3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Journal Reports";

                    //Style Table
                    cellRange = ws.get_Range("A4", "H4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Journal Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            RetailTablePopulate();
        }

        private void dgJournal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgJournal.CurrentCell.ColumnIndex == 3)
            {
                e.Handled = true;               
                dgJournal.BeginEdit(true);
            }
        }

        private void StartACCPACImport()
        {
            GenerateJournalNumber();
            int i = CreateJournalEntryRecord(JournalNo);
            if(i > 0)
            {
                CreateJournalEntries(i.ToString());
                CreateCurrentEarningsTran(JournalNo);
                UpdateAccountBalances(JournalNo);
            }

            
        }

        private void GenerateJournalNumber()
        {
            SqlConnection con_ua = null;
            try
            {
                con_ua = new SqlConnection(AccpacServer);
                string selectSql_ua = "SELECT JournalEntrySeries, JournalEntryPrefix FROM TransactionSeries";
                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                string lSeries = "";

                int lCnt = 0;
                int lNewSeries = 0;

                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lSeries = (reader["JournalEntrySeries"].ToString());
                            lCnt = lSeries.Length;
                            lSeries = lSeries.TrimStart('0');
                            lSeries = (lSeries == "" ? "0" : lSeries);
                            lNewSeries = Convert.ToInt16(lSeries) + 1;
                            CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                            JournalNo = (reader["JournalEntryPrefix"].ToString()).Trim(' ') + CurSeries;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transaction Series Numbers not setup properly.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ua != null)
                    con_ua.Close();
            }
        }

        
        public int CreateJournalEntryRecord(string pJNo)
        {
            SqlConnection con = null;
            try
            {
                DateTime dtpfromutc = dtpJournalDate.Value.ToUniversalTime();
                string ltype = "";
                string trandate = dtpfromutc.ToString("yyyy-MM-dd 00:00:00");
                string lMemo = cmbTranType.Text + " Able Retail Transactions Import " + dtpJournalDate.Text;
                //RECORD Journal Entry Record
                con = new SqlConnection(AccpacServer);
                SqlCommand cmd = new SqlCommand("INSERT INTO RecordJournal (RecordJournalNumber,TransactionDate,TotalDebit,TotalCredit,Memo,IsTaxInclusive,CurrencyID,Type,UserID) " +
                    " VALUES (@RecordJournalNumber,@TransactionDate,@TotalDebit,@TotalCredit,@Memo,@IsTaxInclusive,@CurrencyID,@Type,@UserID);" +
                    "SELECT SCOPE_IDENTITY()", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@RecordJournalNumber", pJNo);
                cmd.Parameters.AddWithValue("@TransactionDate", trandate);
                cmd.Parameters.AddWithValue("@TotalDebit", txtTotalDebit.Value);
                cmd.Parameters.AddWithValue("@TotalCredit", txtTotalCredit.Value);
                cmd.Parameters.AddWithValue("@Memo", lMemo);
                cmd.Parameters.AddWithValue("@IsTaxInclusive", "0");
                cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                if (cmbTranType.Text == "Purchases")
                {
                    ltype = "B";
                }
                else if (cmbTranType.Text == "Sales")
                {
                    ltype = "I";
                }
                else
                {
                    ltype = "J";
                }
                cmd.Parameters.AddWithValue("@Type", ltype);
                cmd.Parameters.AddWithValue("@UserID", AbleRetailUserID);

                con.Open();
                int RecordJournalID = Convert.ToInt32(cmd.ExecuteScalar());
                if (RecordJournalID != 0)
                {
                    //Update series #                              
                    string sql = "UPDATE TransactionSeries SET JournalEntrySeries = '" + CurSeries + "'";
                    cmd.CommandText = sql;
                    int res2 = cmd.ExecuteNonQuery();

                    //CREATE RecordJournalLine  
                    string AccountID = "";
                    string JobID = "";
                    string TaxCode = "";
                    string tDebit = "";
                    string tCredit = "";
                    string LineMemo = "";
                    string TaxEx = "";
                    string TaxInc = "";
                    string TaxAccountID = "";
                    string TaxRate = "";
                    float lDebit = 0;
                    float lCredit = 0;

                    for (int i = 0; i < this.dgJournal.Rows.Count; i++)
                    {
                        if (this.dgJournal.Rows[i].Cells["AccountID"].Value != null)
                        {
                            if (this.dgJournal.Rows[i].Cells["AccountID"].Value.ToString() != "")
                            {
                                TaxAccountID = "0";
                                TaxRate = "0";
                                lDebit = float.Parse(this.dgJournal.Rows[i].Cells["Debit"].Value.ToString(), NumberStyles.Currency);
                                tDebit = (lDebit == 0 ? "" : lDebit.ToString());

                                lCredit= float.Parse(this.dgJournal.Rows[i].Cells["Credit"].Value.ToString(), NumberStyles.Currency);
                                tCredit = (lCredit == 0 ? "" : lCredit.ToString());

                                TaxEx = (tDebit == "" ? tCredit : tDebit);
                                TaxInc = TaxEx;
                                AccountID = this.dgJournal.Rows[i].Cells["AccountID"].Value.ToString();
                                JobID = (this.dgJournal.Rows[i].Cells["Job"].Value != null ? this.dgJournal.Rows[i].Cells["Job"].Value.ToString() : "0");
                                JobID = (JobID == "" ? "0" : JobID);
                                TaxCode = "N-T";
                                LineMemo = (this.dgJournal.Rows[i].Cells["Memo"].Value != null ? this.dgJournal.Rows[i].Cells["Memo"].Value.ToString() : "");
                                LineMemo += (this.dgJournal.Rows[i].Cells["TransactionNumber"].Value != null ? this.dgJournal.Rows[i].Cells["TransactionNumber"].Value.ToString() : "");
                                LineMemo = LineMemo.Replace("'", "''");


                                sql = "INSERT INTO RecordJournalLine(RecordJournalID,TransactionDate,AccountID,Debit,Credit,JobID,TaxCode,LineMemo,TaxExclusiveAmount,TaxInclusiveAmount,TaxAccountID,CurrencyID)" +
                                        "Select RecordJournalID,TransactionDate," + AccountID + " as AccountID," + (tDebit.ToString() == "" ? " NULL " : tDebit.ToString()) + " as Debit, " + (tCredit.ToString() == "" ? " NULL " : tCredit.ToString()) + " as Credit," + JobID + " as JobID,'" + TaxCode + "' as TaxCode," +
                                        "'" + LineMemo + "' as LineMemo," + TaxEx.ToString() + " as TaxExclusiveAmount," + TaxInc.ToString() + " as TaxInclusiveAmount,'" + TaxAccountID + "' as TaxAccountID," + CurrencyID + " as CurrencyID " +
                                        " from RecordJournal where RecordJournalID = " + RecordJournalID;

                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    
                }
                return RecordJournalID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private bool CreateJournalEntries(string pID)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";
                con = new SqlConnection(AccpacServer);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                string lAccountID = "";
                string lTranNo = "";
                decimal lDebit = 0;
                decimal lCredit = 0;
                decimal lTaxEx = 0;
                decimal lTaxInc = 0;
                decimal lTaxAmt = 0;
                string lTaxAccountID = "";
                string lJobID = "";
                string lLineMemo = "";
                string lMemo = "";
                string lCol = "";
                string lTranDate = "";



                System.Data.DataTable ltb = GetRecordJournalLines(pID);
                if (ltb.Rows.Count > 0)
                {
                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        lTranDate = ((DateTime)ltb.Rows[i]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        lTranNo = ltb.Rows[i]["RecordJournalNumber"].ToString();
                        lAccountID = ltb.Rows[i]["AccountID"].ToString();
                        if(ltb.Rows[i]["Debit"] != null)
                        {
                            if (ltb.Rows[i]["Debit"].ToString() != "")
                            {
                                lDebit = Convert.ToDecimal(ltb.Rows[i]["Debit"].ToString());
                            }
                            else
                            {
                                lDebit = 0;
                            }
                        }
                        else
                        {
                            lDebit = 0;
                        }
                        //lDebit = (ltb.Rows[i]["Debit"] == null? 0 :Convert.ToDecimal(ltb.Rows[i]["Debit"].ToString()));
                        //lDebit = Convert.ToDecimal(ltb.Rows[i]["Debit"].ToString());
                        //lCredit = (ltb.Rows[i]["Credit"] == null ? 0 : Convert.ToDecimal(ltb.Rows[i]["Credit"].ToString()));

                        if (ltb.Rows[i]["Credit"] != null)
                        {
                            if (ltb.Rows[i]["Credit"].ToString() != "")
                            {
                                lCredit = Convert.ToDecimal(ltb.Rows[i]["Credit"].ToString());
                            }
                            else
                            {
                                lCredit = 0;
                            }
                        }
                        else
                        {
                            lCredit = 0;
                        }

                        lTaxEx = Convert.ToDecimal(ltb.Rows[i]["TaxExclusiveAmount"].ToString());
                        lTaxInc = Convert.ToDecimal(ltb.Rows[i]["TaxInclusiveAmount"].ToString());
                        lTaxAmt = lTaxInc - lTaxEx;
                        lTaxAccountID = ltb.Rows[0]["TaxAccountID"].ToString();
                        lJobID = (ltb.Rows[i]["JobID"].ToString() == "" ? "0" : ltb.Rows[i]["JobID"].ToString());
                        lLineMemo = ltb.Rows[i]["LineMemo"].ToString();
                        lMemo = ltb.Rows[i]["Memo"].ToString();


                        if (lDebit != 0)//DEBIT
                        {
                            lCol = "DebitAmount";

                        }
                        else//CREDIT
                        {
                            lCol = "CreditAmount";
                        }

                        sql = "INSERT INTO Journal(TransactionDate,Memo,AllocationMemo,AccountID," + lCol + ",TransactionNumber,Type,JobID) " +
                               " VALUES('" + lTranDate + "','" + lMemo + "','" + lLineMemo + "'," + lAccountID + "," + lTaxEx.ToString() + ",'" + lTranNo + "','JE'," + lJobID + ")";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        //TAX COMPONENT
                        if (lTaxAmt != 0 && lTaxAccountID != "")
                        {

                            sql = "INSERT INTO Journal(TransactionDate,Memo,AllocationMemo,AccountID," + lCol + ",TransactionNumber,Type,JobID) " +
                           " VALUES('" + lTranDate + "','" + lMemo + "','" + lLineMemo + "'," + lTaxAccountID + "," + lTaxAmt.ToString() + ",'" + lTranNo + "','JE'," + lJobID + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction.No Record Journal Lines found.");
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

        public static bool CreateCurrentEarningsTran(string pTranNo)
        {
            SqlConnection con = null;
            try
            {
                string sql = "SELECT j.TransactionDate, j.EntryDate, j.Memo, j.AccountID, ISNULL(j.CreditAmount, 0) AS CreditAmount, ISNULL(j.DebitAmount, 0) AS DebitAmount, j.TransactionNumber, j.Type, j.JobID, a.AccountClassificationID FROM Journal j INNER JOIN Accounts a on j.AccountID = a.AccountID WHERE a.AccountClassificationID IN ('I','OI','EXP','OE','COS') AND TransactionNumber = '" + pTranNo + "'";
                con = new SqlConnection(AccpacServer);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter da1 = new SqlDataAdapter();
                System.Data.DataTable dt1 = new System.Data.DataTable();
                da1.SelectCommand = cmd;
                da1.Fill(dt1);
                decimal lDebit = 0;
                decimal lCredit = 0;
                decimal lNet = 0;
                string lTranDateStr = "";

                foreach (DataRow rw in dt1.Rows)
                {
                    lDebit = Convert.ToDecimal(rw["DebitAmount"].ToString());
                    lCredit = Convert.ToDecimal(rw["CreditAmount"].ToString());
                    lTranDateStr = Convert.ToDateTime(rw["TransactionDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    if (rw["AccountClassificationID"].ToString().Trim() == "I"
                        || rw["AccountClassificationID"].ToString().Trim() == "OI")
                    {
                        lNet = lCredit - lDebit;
                        //CREDIT CURRENT EARNINGS
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AccountID, CreditAmount, TransactionNumber, Type, JobID)
                              VALUES('" + lTranDateStr + "','" + rw["Memo"].ToString() + "'," + CurrentEarningsID + ","
                              + lNet.ToString() + ",'SYS-" + rw["TransactionNumber"].ToString() + "','" + rw["Type"].ToString() + "'," + rw["JobID"].ToString() + ")";
                    }
                    else //"COS,EXP,OE"
                    {
                        lNet = lDebit - lCredit;
                        //DEBIT CURRENT EARNINGS
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AccountID, DebitAmount, TransactionNumber, Type, JobID)
                              VALUES('" + lTranDateStr + "','" + rw["Memo"].ToString() + "'," + CurrentEarningsID + ","
                              + lNet.ToString() + ",'SYS-" + rw["TransactionNumber"].ToString() + "','" + rw["Type"].ToString() + "'," + rw["JobID"].ToString() + ")";

                    }
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                return true;
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

        public static bool UpdateAccountBalances(string pTranNo)
        {
            SqlConnection con = null;
            try
            {
                //LOOP THRU EACH JOURNAL ENTRY TO UPDATE ACCOUNT BALANCE
                string lDebit = "0";
                string lCredit = "0";
                string lAcct = "";
                string sql = "";
                string lAccountClass = "";

                con = new SqlConnection(AccpacServer);
                sql = "SELECT j.AccountID, ISNULL(j.DebitAmount, 0) AS Debit, ISNULL(j.CreditAmount, 0) AS Credit, a.AccountClassificationID FROM Journal j INNER JOIN Accounts a ON j.AccountID = a.AccountID WHERE TransactionNumber IN ( '" + pTranNo + "', 'SYS-" + pTranNo + "')";
                //SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                System.Data.DataTable dt = new System.Data.DataTable();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow rw in dt.Rows)
                {
                    lAcct = rw["AccountID"].ToString();
                    lDebit = (rw["Debit"].ToString() == "" ? "0" : rw["Debit"].ToString());
                    lCredit = (rw["Credit"].ToString() == "" ? "0" : rw["Credit"].ToString());
                    sql = "UPDATE Accounts SET CurrentAccountBalance = CurrentAccountBalance ";
                    lAccountClass = rw["AccountClassificationID"].ToString().Trim();
                    switch (lAccountClass.Trim())
                    {
                        case "A":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "L":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "COS":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "I":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "OI":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "EXP":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "OE":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "EQ":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                    }
                    sql += " WHERE AccountID = " + lAcct;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                }

                return true;
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

        private static System.Data.DataTable GetRecordJournalLines(string pRecordJournalID)
        {
            SqlConnection con = null;
            System.Data.DataTable RTb = null;
            try
            {
                string sql = "SELECT l.*, r.RecordJournalNumber, r.Memo from RecordJournalLine as l inner join RecordJournal as r on l.RecordJournalID = r.RecordJournalID where r.RecordJournalID = " + pRecordJournalID;
                con = new SqlConnection(AccpacServer);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new System.Data.DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        
        private void edate_ValueChanged(object sender, EventArgs e)
        {
            this.dtpJournalDate.Value = this.edate.Value;
        }
    }//End
}
