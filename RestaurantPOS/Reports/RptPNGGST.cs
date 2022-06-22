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
using Excel = Microsoft.Office.Interop.Excel;


namespace AbleRetailPOS
{
    public partial class RptPNGGST : Form
    {
       
        private static int PeriodCount = 0;
        private static string[] PeriodFrom;
        private static string[] PeriodTo;
        private string[] PeriodColName;
        private DateTime[] PeriodDate;
        private string Period1 = "";
        private string Period2 = "";
        private string Period3 = "";
        private string Period4 = "";
        private string Period5 = "";
        private string Period6 = "";
        private string Period7 = "";
        private string Period8 = "";
        private string Period9 = "";
        private string Period10 = "";
        private string Period11 = "";
        private string Period12 = "";

        private string IncTaxCodes = "";
        private string SDate = "";
        private string EDate = "";
        private string GSTMonth = "";
        private float G1 = 0;
        private float G2 = 0;
        private float G3 = 0;
        private float G8 = 0;
        private float G9 = 0;
        private float P_ABR = 0;
        private float P_NCD = 0;

        public RptPNGGST()
        {
            InitializeComponent();
        }

        private void RptPNGGST_Load(object sender, EventArgs e)
        {
            DateTime lMinDate = DateTime.Now.AddMonths(-12);
            DateTime lMaxDate = DateTime.Now.AddMonths(24).AddDays(-1);
            LoadTaxCodes();

        }

      
        private void GeneratePeriodStr(DateTime pFrom, DateTime pTo)
        {
            PeriodColName = new string[12];
            PeriodFrom = new string[12];
            PeriodTo = new string[12];
            int ctr = 0;
            DateTime lPStart = pFrom;
            DateTime lPEnd = pTo;
            
            for(int i = 0; i < 12; i++)
            {
                if(lPStart > lPEnd)
                {
                    break;
                }
                PeriodFrom[i] = lPStart.ToString("yyyy-MM-dd");
                PeriodTo[i] = lPStart.AddMonths(1).ToString("yyyy-MM-dd");
                PeriodColName[i] = lPStart.ToString("MMM-yy");
                lPStart = lPStart.AddMonths(1);
                PeriodCount = i;

            }
            PeriodCount++;


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
                DataTable dt = new DataTable();
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
            if(txtFileName.Text == "")
            {
                MessageBox.Show("Please specify the filename and directory for the report.");
                return;
            }
            DateTime dtpfromutc = dtpfrom.Value.ToUniversalTime();
            DateTime dtptoutc = dtpto.Value.ToUniversalTime();
            GSTMonth = dtpto.Value.ToString("MMM");
            SDate = dtpfromutc.ToString("yyyy-MM-dd") + " 00:00:00";
            EDate = dtptoutc.ToString("yyyy-MM-dd") + " 23:59:59";
            IncTaxCodes = GetIncludedTax();
            fillG1();


        }
        private string GetIncludedTax()
        {
            string retAccts = "";
            foreach (DataGridViewRow dvr in this.dgAccounts.Rows)
            {
                if (dvr.Cells["TaxCode"].Value.ToString() != "")
                {
                    if ((bool)dvr.Cells["Include"].Value)
                    {
                        retAccts += (retAccts != "" ? ",'" + dvr.Cells["TaxCode"].Value.ToString() + "'" : "'" + dvr.Cells["TaxCode"].Value.ToString() + "'");
                    }
                }
            }
            return retAccts;
        }










        private void fillG1()
        {
            try
            {

           
                Cursor = Cursors.WaitCursor;
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(Application.StartupPath +"\\G1.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //FILL COMPANY INFO
                DataTable lCTb = fillCompanyInfo();
                string lProvince = "";
                if (lCTb.Rows.Count > 0)
                {
                    string lSalesTaxNo = lCTb.Rows[0]["SalesTaxNumber"].ToString();
                    string[] lTIN = lSalesTaxNo.Split();
                    if(lTIN.Length > 9)
                    {
                        xlWorkSheet.Columns["CE"].Cells["20"].Value = lTIN[0];
                        xlWorkSheet.Columns["CH"].Cells["20"].Value = lTIN[1];
                        xlWorkSheet.Columns["CK"].Cells["20"].Value = lTIN[2];
                        xlWorkSheet.Columns["CN"].Cells["20"].Value = lTIN[3]; 
                        xlWorkSheet.Columns["CQ"].Cells["20"].Value = lTIN[4]; 
                        xlWorkSheet.Columns["CT"].Cells["20"].Value = lTIN[5];
                        xlWorkSheet.Columns["CW"].Cells["20"].Value = lTIN[6];
                        xlWorkSheet.Columns["CZ"].Cells["20"].Value = lTIN[7];
                        xlWorkSheet.Columns["DC"].Cells["20"].Value = lTIN[8];
                    }
                    xlWorkSheet.Columns["AB"].Cells["32"].Value = lCTb.Rows[0]["CompanyName"].ToString();
                    xlWorkSheet.Columns["AK"].Cells["36"].Value = lCTb.Rows[0]["ContactPerson"].ToString();
                    xlWorkSheet.Columns["AB"].Cells["40"].Value = lCTb.Rows[0]["Phone"].ToString();
                    xlWorkSheet.Columns["AB"].Cells["44"].Value = lCTb.Rows[0]["Email"].ToString();
                    xlWorkSheet.Columns["AG"].Cells["48"].Value = lCTb.Rows[0]["Add1"].ToString();
                    xlWorkSheet.Columns["CA"].Cells["48"].Value = lCTb.Rows[0]["Add2"].ToString();
                    xlWorkSheet.Columns["AW"].Cells["52"].Value = lCTb.Rows[0]["Street"].ToString();
                    xlWorkSheet.Columns["AG"].Cells["56"].Value = lCTb.Rows[0]["POBox"].ToString();
                    xlWorkSheet.Columns["AG"].Cells["60"].Value = lCTb.Rows[0]["Country"].ToString();

                    xlWorkSheet.Columns["CA"].Cells["60"].Value = lCTb.Rows[0]["State"].ToString();
                    xlWorkSheet.Columns["AW"].Cells["64"].Value = lCTb.Rows[0]["POBox"].ToString();
                    lProvince = lCTb.Rows[0]["State"].ToString();


                }
                else 
                {
                    return;
                }

                //FILL FIGURES
                GetSalesTaxes();
                GetPurchaseTaxes();
           
                //G1
                xlWorkSheet.Columns["CI"].Cells["82"].Value = G1;

                //G2
                xlWorkSheet.Columns["BI"].Cells["86"].Value = G2;

                //G3
                xlWorkSheet.Columns["BI"].Cells["90"].Value = G3;

                //G8
                xlWorkSheet.Columns["CI"].Cells["113"].Value = G8;
                //G9
                xlWorkSheet.Columns["BI"].Cells["117"].Value = G9;

                switch (lProvince.Trim())
                {
                    case "Autonomous Region of Bougainville":
                        xlWorkSheet.Columns["Y"].Cells["168"].Value = G1 - G2- G3;
                        break;

                    case "Central":
                        xlWorkSheet.Columns["Y"].Cells["173"].Value = G1 - G2 - G3;
                        break;
                    case "Chimbu":
                        xlWorkSheet.Columns["Y"].Cells["176"].Value = G1 - G2 - G3;
                        break;
                    case "East New Britain":
                        xlWorkSheet.Columns["Y"].Cells["179"].Value = G1 - G2 - G3;
                        break;
                    case "East Sepik":
                        xlWorkSheet.Columns["Y"].Cells["182"].Value = G1 - G2 - G3;
                        break;
                    case "Eastern Highlands":
                        xlWorkSheet.Columns["Y"].Cells["185"].Value = G1 - G2 - G3;
                        break;
                    case "Enga":
                        xlWorkSheet.Columns["Y"].Cells["188"].Value = G1 - G2 - G3;
                        break;
                    case "Gulf":
                        xlWorkSheet.Columns["Y"].Cells["191"].Value = G1 - G2 - G3;
                        break;
                    case "Hela":
                        xlWorkSheet.Columns["Y"].Cells["194"].Value = G1 - G2 - G3;
                        break;
                    case "Jiwaka":
                        xlWorkSheet.Columns["Y"].Cells["197"].Value = G1 - G2 - G3;
                        break;
                    case "Madang":
                        xlWorkSheet.Columns["Y"].Cells["200"].Value = G1 - G2 - G3;
                        break;
                    case "Manus":
                        xlWorkSheet.Columns["Y"].Cells["203"].Value = G1 - G2 - G3;
                        break;
                    case "Milne Bay":
                        xlWorkSheet.Columns["Y"].Cells["206"].Value = G1 - G2 - G3;
                        break;
                    case "Morobe":
                        xlWorkSheet.Columns["Y"].Cells["209"].Value = G1 - G2 - G3;
                        break;
                    case "National Capital District":
                        xlWorkSheet.Columns["Y"].Cells["212"].Value = G1 - G2 - G3;
                        break;
                    case "New Ireland":
                        xlWorkSheet.Columns["Y"].Cells["215"].Value = G1 - G2 - G3;
                        break;
                    case "Oro":
                        xlWorkSheet.Columns["Y"].Cells["218"].Value = G1 - G2 - G3;
                        break;
                    case "Sandaun":
                        xlWorkSheet.Columns["Y"].Cells["221"].Value = G1 - G2 - G3;
                        break;
                    case "Southern Highlands":
                        xlWorkSheet.Columns["Y"].Cells["224"].Value = G1 - G2 - G3;
                        break;
                    case "West New Britain":
                        xlWorkSheet.Columns["Y"].Cells["227"].Value = G1 - G2 - G3;
                        break;
                    case "Western Highlands":
                        xlWorkSheet.Columns["Y"].Cells["230"].Value = G1 - G2 - G3;
                        break;                        
                    case "Western Province":
                        xlWorkSheet.Columns["Y"].Cells["233"].Value = G1 - G2 - G3;
                        break;

                }


                xlWorkBook.SaveAs(this.txtFileName.Text);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                Cursor = Cursors.Default;
                MessageBox.Show("Form G1 generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private DataTable fillCompanyInfo()
        {
            SqlConnection con = null;
            DataTable dt = null;
            try
            {
                string sql = @"SELECT * from DataFileInformation";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dt;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void GetSalesTaxes()
        {
            SqlConnection con = null;
            DataTable dt = null;
            
            try
            {
                string sql = @"SELECT ts.TaxCode, t.TaxPercentageRate, sum(TaxInclusiveAmount) as Taxable, sum(TaxAmount) as TaxAmount FROM (
                    SELECT m.TransactionDate, m.MoneyInID, m.MoneyInNumber,m.ProfileID, sum(l.TaxInclusiveAmount) as TaxInclusiveAmount, sum(l.TaxExclusiveAmount) as TaxExclusiveAmount, sum(l.TaxInclusiveAmount - l.TaxExclusiveAmount ) as TaxAmount, l.TaxCode 
                    from MoneyIn m inner join MoneyInLines l on m.MoneyInID = l.MoneyInID where m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and l.TaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.MoneyInID, m.MoneyInNumber,m.ProfileID,l.TaxCode ";

                sql += @" UNION SELECT m.TransactionDate, m.RecordJournalID, m.RecordJournalNumber,0 as ProfileID, sum(l.TaxInclusiveAmount) as TaxInclusiveAmount, sum(l.TaxExclusiveAmount) as TaxExclusiveAmount, sum(l.TaxInclusiveAmount - l.TaxExclusiveAmount ) as TaxAmount, l.TaxCode
                    from RecordJournal m inner join RecordJournalLine l on m.RecordJournalID = l.RecordJournalID where m.Type in ('I','J')  and m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and l.TaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.RecordJournalID, m.RecordJournalNumber,l.TaxCode";

                sql += @" UNION SELECT m.TransactionDate, m.SalesID, m.SalesNumber,m.CustomerID as ProfileID, sum(l.TotalAmount) as TaxInclusiveAmount, sum(l.SubTotal) as TaxExclusiveAmount, sum(TaxAmount) as TaxAmount, l.TaxCode 
                    from Sales m inner join SalesLines l on m.SalesID = l.SalesID where m.SalesType = 'INVOICE' and m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and l.TaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.SalesID, m.SalesNumber,m.CustomerID,l.TaxCode ";

                sql += @" UNION SELECT m.TransactionDate, m.SalesID, m.SalesNumber,m.CustomerID as ProfileID, sum(m.FreightTax + m.FreightSubTotal) as TaxInclusiveAmount, sum(m.FreightSubTotal) as TaxExclusiveAmount, sum(m.FreightTax) as TaxAmount, m.FreightTaxCode as TaxCode 
                    from Sales m where m.SalesType = 'INVOICE' and m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and m.FreightTaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.SalesID, m.SalesNumber,m.CustomerID,m.FreightTaxCode ";

                sql += @") ts inner join TaxCodes as t on ts.TaxCode = t.TaxCode group by ts.TaxCode, t.TaxPercentageRate";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                string lTaxCode = "";
                float lTaxRate = 0;
                float lTaxable = 0;
                float lTaxAmt = 0;
                G1 = 0;
                G2 = 0;
                G3 = 0;
                for (int i = 0; i<dt.Rows.Count; i++)
                {
                    lTaxCode = dt.Rows[i]["TaxCode"].ToString();
                    lTaxable = float.Parse(dt.Rows[i]["Taxable"].ToString());
                    lTaxAmt = float.Parse(dt.Rows[i]["TaxAmount"].ToString());
                    lTaxRate = float.Parse(dt.Rows[i]["TaxPercentageRate"].ToString());
                    G1 += lTaxable;
                    if(lTaxCode.Trim() == "EXEMPT")
                    {
                        G2 += lTaxable;
                    }
                    if (lTaxCode.Trim() != "GST" && lTaxCode.Trim() != "EXEMPT" && lTaxRate != 0)
                    {
                        G3 += lTaxable;
                    }

                }
                
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

        private void GetPurchaseTaxes()
        {
            SqlConnection con = null;
            DataTable dt = null;

            try
            {
                string sql = @"SELECT ts.TaxCode, t.TaxPercentageRate, sum(TaxInclusiveAmount) as Taxable, sum(TaxAmount) as TaxAmount FROM (
                    SELECT m.TransactionDate, m.MoneyOutID, m.MoneyOutNumber,m.ProfileID, sum(l.TaxInclusiveAmount) as TaxInclusiveAmount, sum(l.TaxExclusiveAmount) as TaxExclusiveAmount, sum(l.TaxInclusiveAmount - l.TaxExclusiveAmount ) as TaxAmount, l.TaxCode 
                    from MoneyOut m inner join MoneyOutLines l on m.MoneyOutID = l.MoneyOutID where m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and l.TaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.MoneyOutID, m.MoneyOutNumber,m.ProfileID,l.TaxCode ";

                sql += @" UNION SELECT m.TransactionDate, m.RecordJournalID, m.RecordJournalNumber,0 as ProfileID, sum(l.TaxInclusiveAmount) as TaxInclusiveAmount, sum(l.TaxExclusiveAmount) as TaxExclusiveAmount, sum(l.TaxInclusiveAmount - l.TaxExclusiveAmount ) as TaxAmount, l.TaxCode
                    from RecordJournal m inner join RecordJournalLine l on m.RecordJournalID = l.RecordJournalID where m.Type in ('B')  and m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and l.TaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.RecordJournalID, m.RecordJournalNumber,l.TaxCode";

                sql += @" UNION SELECT m.TransactionDate, m.PurchaseID, m.PurchaseNumber,m.SupplierID as ProfileID, sum(l.TotalAmount) as TaxInclusiveAmount, sum(l.SubTotal) as TaxExclusiveAmount, sum(TaxAmount) as TaxAmount, l.TaxCode 
                    from Purchases m inner join PurchaseLines l on m.PurchaseID = l.PurchaseID where m.PurchaseType = 'BILL' and m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and l.TaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.PurchaseID, m.PurchaseNumber,m.SupplierID,l.TaxCode ";

                sql += @" UNION SELECT m.TransactionDate, m.PurchaseID, m.PurchaseNumber,m.SupplierID as ProfileID, sum(m.FreightTax + m.FreightSubTotal) as TaxInclusiveAmount, sum(m.FreightSubTotal) as TaxExclusiveAmount, sum(m.FreightTax) as TaxAmount, m.FreightTaxCode as TaxCode 
                    from Purchases m where m.PurchaseType = 'BILL' and m.TransactionDate BETWEEN '" + SDate + "' AND '" + EDate + "' and m.FreightTaxCode in (" + IncTaxCodes + ") group by m.TransactionDate, m.PurchaseID, m.PurchaseNumber,m.SupplierID,m.FreightTaxCode ";

                sql += @") ts inner join TaxCodes as t on ts.TaxCode = t.TaxCode group by ts.TaxCode, t.TaxPercentageRate";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                string lTaxCode = "";
                float lTaxRate = 0;
                float lTaxable = 0;
                float lTaxAmt = 0;
                G8 = 0;
                G9 = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lTaxCode = dt.Rows[i]["TaxCode"].ToString();
                    lTaxable = float.Parse(dt.Rows[i]["Taxable"].ToString());
                    lTaxAmt = float.Parse(dt.Rows[i]["TaxAmount"].ToString());
                    lTaxRate = float.Parse(dt.Rows[i]["TaxPercentageRate"].ToString());
                    G8 += lTaxable;
                    if (lTaxCode.Trim() == "EXEMPT")
                    {
                        G9 += lTaxable;
                    }
                  

                }

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

        private void button1_Click(object sender, EventArgs e)
        {
            BrowseDlg.ShowDialog();
            this.txtFileName.Text = BrowseDlg.SelectedPath + "\\FormG1.xlsx";
        }

        private void BrowseDlg_HelpRequest(object sender, EventArgs e)
        {

        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

