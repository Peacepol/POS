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
using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;

namespace RestaurantPOS.Reports.SalesReports
{
    public partial class RptCustomerLedger : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private bool CanView = false;
        private string CustomerID;
        public RptCustomerLedger()
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
            sdatePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edatePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
      
        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                this.lblCustomerID.Text = lProfile[0];
                this.customerText.Text = lProfile[2];

            }
        }
        
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            this.customerText.Enabled = !chkAll.Checked;
            this.pbCustomer.Enabled = !chkAll.Checked;
          
        }

        private void GetReportData()
        {
            SqlConnection con = null;
            try
            {


                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();
                string sqlext = "";
                string sqlext1 = "";
                if (!chkAll.Checked)
                {
                    if (customerText.Text != "")
                    {
                        sqlext = "AND  s.CustomerID ='" + CustomerID + "'";
                        sqlext1 = "AND  s.ProfileID ='" + CustomerID + "'";
                    }
                }
              
                string selectSql = @"SELECT l.Id, l.ProfileIDNumber, l.Name, l.TransactionDate, l.TransactionNumber, l.Amount, l.Memo, l.TranType, ISNULL(b.cfbal,0) cfbal, l.TransactionAmount, CAST(0 as float) as RunningBalance from (SELECT p.Name, s.TransactionDate , s.SalesNumber as TransactionNumber, s.GrandTotal as Amount, 
                     s.Memo, p.ID, p.ProfileIDNumber, 'SI' as TranType, s.GrandTotal  as TransactionAmount
                    FROM Sales s INNER JOIN Profile p ON s.CustomerID = p.ID where s.SalesType in ('SINVOICE','INVOICE') and s.TransactionDate BETWEEN @sdate AND @edate " + sqlext
                        + @"UNION 
                       SELECT p.Name, s.TransactionDate , s.PaymentNumber as TransactionNumber,  s.TotalAmount as Amount, 
                     s.Memo, p.ID, p.ProfileIDNumber, 'SP' as TranType, s.TotalAmount * -1 as TransactionAmount
                    FROM Payment s INNER JOIN Profile p ON s.ProfileID = p.ID where s.PaymentFor = 'Sales' and s.TransactionDate BETWEEN @sdate AND @edate " + sqlext1 + ") l "
                        + @"LEFT JOIN (
                    SELECT CustomerID, sum(charges) - sum(payment) as cfbal from (SELECT CustomerID, Sum(GrandTotal) as charges, 0 as payment from Sales where SalesType in ('SINVOICE','INVOICE') and TransactionDate < @sdate group by CustomerID
                    UNION SELECT ProfileID as CustomerID, 0 as charges, sum(TotalAmount) as payment from Payment where PaymentFor = 'Sales' and TransactionDate < @sdate group by ProfileID ) cf group by CustomerID
                    ) b
                    on l.ID = b.CustomerID order by l.ID, l.TransactionDate
                    ";
                //string selectSql = @"SELECT l.Id, l.ProfileIDNumber, l.Name, l.TransactionDate, l.TransactionNumber, l.Amount, l.Memo, l.TranType, ISNULL(b.cfbal,0) cfbal, l.TransactionAmount, CAST(0 as float) as RunningBalance from (SELECT p.Name, s.TransactionDate , s.SalesNumber as TransactionNumber, s.GrandTotal as Amount, 
                //     s.Memo, p.ID, p.ProfileIDNumber, 'SI' as TranType, s.GrandTotal  as TransactionAmount
                //    FROM Sales s INNER JOIN Profile p ON s.CustomerID = p.ID where s.SalesType in ('SINVOICE','INVOICE') and s.TransactionDate BETWEEN @sdate AND @edate
                //    UNION SELECT p.Name, s.TransactionDate , s.PaymentNumber as TransactionNumber,  s.TotalAmount as Amount, 
                //     s.Memo, p.ID, p.ProfileIDNumber, 'SP' as TranType, s.TotalAmount * -1 as TransactionAmount
                //    FROM Payment s INNER JOIN Profile p ON s.ProfileID = p.ID where s.PaymentFor = 'Sales' and s.TransactionDate BETWEEN @sdate AND @edate) l 
                //    LEFT JOIN (
                //    SELECT CustomerID, sum(charges) - sum(payment) as cfbal from (SELECT CustomerID, Sum(GrandTotal) as charges, 0 as payment from Sales where SalesType in ('SINVOICE','INVOICE') and TransactionDate < @sdate group by CustomerID
                //    UNION SELECT ProfileID as CustomerID, 0 as charges, sum(TotalAmount) as payment from Payment where PaymentFor = 'Sales' and TransactionDate < @sdate group by ProfileID ) cf group by CustomerID
                //    ) b
                //    on l.ID = b.CustomerID order by l.ID, l.TransactionDate
                //    ";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
            
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new System.Data.DataTable();
                da.Fill(TbRep);

               

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
        void LoadReport(int pViewType = 0)
        {

            if (pViewType == 0)
            {
                
              
                Reports.ReportParams CustomerLedger = new Reports.ReportParams();
                CustomerLedger.PrtOpt = 1;
                CustomerLedger.Rec.Add(TbRep);
            
                CustomerLedger.ReportName = "CustomerLedger.rpt";
                CustomerLedger.RptTitle = "Customer Ledger";
                CustomerLedger.Params = "compname|sdate|edate";
                CustomerLedger.PVals = CommonClass.CompName.Trim() + "|" + sdatePicker.Value + "|" + edatePicker.Value;
                CommonClass.ShowReport(CustomerLedger);
            }
            else
            {

                CalculateTotal(1);
                this.dgReport.DataSource = TbGrid;
                FormatGrid();
                


            }

        }
        private void CalculateTotal(int pSortIndex = 1, string pSortMode = "asc")
        {
            TbGrid = TbRep.Clone();
            float CBal = 0;            
            float RBal = 0;
            float TranAmount = 0;
            string lCustomerID = "";
            DataRow rw;
            for (int i = 0; i < TbRep.Rows.Count; i++)
            {
                if(lCustomerID != TbRep.Rows[i]["ID"].ToString())
                {
                    if(i > 0)
                    {
                        rw = TbGrid.NewRow();
                        rw[7] = "TOTAL";
                        rw[10] = RBal;
                        TbGrid.Rows.Add(rw);
                        TbGrid.Rows.Add();

                        CBal = float.Parse(TbRep.Rows[i]["cfbal"].ToString());
                        RBal = CBal;
                    }
                 
                }
                rw = TbGrid.NewRow();
                rw[0] = TbRep.Rows[i][0];
                rw[1] = TbRep.Rows[i][1];
                rw[2] = TbRep.Rows[i][2];
                rw[3] = TbRep.Rows[i][3];
                rw[4] = TbRep.Rows[i][4];
                rw[5] = TbRep.Rows[i][5];
                rw[6] = TbRep.Rows[i][6];
                rw[7] = TbRep.Rows[i][7];
                rw[8] = TbRep.Rows[i][8];
                rw[9] = TbRep.Rows[i][9];
                rw[10] = TbRep.Rows[i][10];
                lCustomerID = TbRep.Rows[i]["ID"].ToString();
                DateTime lTranDate = Convert.ToDateTime(TbRep.Rows[i]["TransactionDate"].ToString()).ToLocalTime();           
                rw["TransactionDate"] = lTranDate.ToShortDateString();
                TranAmount = float.Parse(TbRep.Rows[i]["TransactionAmount"].ToString());
                RBal += TranAmount;
                rw["RunningBalance"] = RBal;
                TbGrid.Rows.Add(rw);


            }
            rw = TbGrid.NewRow();
            rw[7] = "TOTAL";
            rw[10] = RBal;
            TbGrid.Rows.Add(rw);
            

        
        }

        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "ID";
            this.dgReport.Columns[0].Visible = false;
            this.dgReport.Columns[1].HeaderText = "Profile ID Number";
            this.dgReport.Columns[2].HeaderText = "Customer Name";
            this.dgReport.Columns[3].HeaderText = "Transaction Date";
            this.dgReport.Columns[4].HeaderText = "Transaction Number";
            this.dgReport.Columns[5].HeaderText = "Amount";
            this.dgReport.Columns[6].HeaderText = "Memo";
            this.dgReport.Columns[7].HeaderText = "Src";
            this.dgReport.Columns[8].HeaderText = "cfbal";
            this.dgReport.Columns[8].Visible = false;
            this.dgReport.Columns[9].HeaderText = "TransactionAmount";
            this.dgReport.Columns[9].Visible = false;
            this.dgReport.Columns[10].HeaderText = "Balance";

            this.dgReport.Columns[5].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[10].DefaultCellStyle.Format = "C2";
          

          
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
           
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            GetReportData();
            LoadReport(0);
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(1);
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Customer Ledger Report" + Environment.NewLine + "Date From " + sdatePicker.Value.ToShortDateString() + " To " + edatePicker.Value.ToShortDateString();
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ID", 0);
            dgPrinter.ColumnWidths.Add("ProfileIDNumber", 80);
            dgPrinter.ColumnWidths.Add("Name", 150);
            dgPrinter.ColumnWidths.Add("TransactionDate", 100);
            dgPrinter.ColumnWidths.Add("TransactionNumber", 100);
            dgPrinter.ColumnWidths.Add("Amount", 120);
            dgPrinter.ColumnWidths.Add("Memo", 220);
            dgPrinter.ColumnWidths.Add("TranType", 80);
            dgPrinter.ColumnWidths.Add("cfbal", 0);
            dgPrinter.ColumnWidths.Add("TransactionAmount", 0);
            dgPrinter.ColumnWidths.Add("RunningBalance", 120);

            dgPrinter.PageSettings.Landscape = true;
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    ws.Cells[4, 1] = "ID";
                    ws.Cells[4, 2] = "Profile ID Number";
                    ws.Cells[4, 3] = "Customer Name";
                    ws.Cells[4, 4] = "Transaction Date";
                    ws.Cells[4, 5] = "Transaction Number";
                    ws.Cells[4, 6] = "Src";
                    ws.Cells[4, 7] = "Memo";
                    ws.Cells[4, 8] = "Amount";
                    ws.Cells[4, 9] = "Balance";

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
                            ws.Cells[i, 4] = Convert.ToDateTime(item.Cells[3].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            ws.Cells[i, 6] = Math.Round(float.Parse(item.Cells[5].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null)
                        {
                            ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                        }
                        if (item.Cells[10].Value != null && item.Cells[10].Value.ToString() != "")
                        {
                            ws.Cells[i, 9] = Math.Round(float.Parse(item.Cells[10].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "I3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Customer Ledger Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "I4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Customer Ledger Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptCustomerLedger_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                CustomerID = lProfile[0];
                customerText.Text = lProfile[2];
                chkAll.Checked = false;
            }
        }
    }
}
