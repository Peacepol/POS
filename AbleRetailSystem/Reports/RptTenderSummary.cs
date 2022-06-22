using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
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

namespace RestaurantPOS.Reports
{
    public partial class RptTenderSummary : Form
    {
        private System.Data.DataTable Tbrep;
        private System.Data.DataTable TbGrid;
        private System.Data.DataTable dtSalesBreakdown;
        private bool CanView = false;


        string selectSql = "";
        string reportName;
        string reportTitle;
        string shippingID;
        string salespersonID;
        bool promised = false;
        private int index = 1;
        private string sort = " asc";
        public RptTenderSummary()
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
            LoadSummaryReport();
        }
        private void LoadSummaryReport()
        {
            Reports.ReportParams SessionSummary = new Reports.ReportParams();
            SessionSummary.PrtOpt = 1;
            string cashAR = "";
            //DataTable dtSalesBreakdown = new DataTable();
            //CommonClass.runSql(ref dtSalesBreakdown, printSalesBreakdown);

            if (Tbrep.Rows.Count > 0)
            {
                SessionSummary.Rec.Add(Tbrep);
                if (dtSalesBreakdown.Rows.Count > 0)
                {
                    SessionSummary.SubRpt = "InvoiceType";
                    SessionSummary.tblSubRpt = dtSalesBreakdown;
                }

                SessionSummary.Params = "compname|StartDate|EndDate";
                SessionSummary.PVals = CommonClass.CompName.Trim() + "|" + dtmTxFrom.Value.ToShortDateString() + " |" + dtmTxTo.Value.ToShortDateString();

                SessionSummary.ReportName = "TenderSummary.rpt";
                SessionSummary.RptTitle = "Tender Summary Report";

                CommonClass.ShowReport(SessionSummary);

            }
        }

        private void dtmTxTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtmTxFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        void LoadReport()
        {
            string printSessionSummaryReport = @"SELECT s.PaymentMethod, s.PaymentMethodID, s.Amount , s.SessionID, cps.TotalCount, (cps.TotalCount - s.Amount ) as Discrepancy, SessionKey, SessionStart, SessionEnd from
                        (SELECT  DISTINCT SUM( pt.Amount) as Amount, pd.PaymentMethodID, pm.PaymentMethod, p.SessionID FROM PaymentTender pt 
                        inner join PaymentDetails pd on pt.PaymentID = pd.PaymentID and pt.id = pd.PaymentDetailsID
                        inner join Payment p on pt.PaymentID = p.PaymentID
                        inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
						inner join Sales s on pl.EntityID = s.SalesID
						left join PaymentMethods pm on pd.PaymentMethodID = pm.id	
                        WHERE pt.Amount <> 0 and s.TransactionDate BETWEEN @sdate AND @edate group by pd.PaymentMethodID, pm.PaymentMethod,p.SessionID ) s";

            printSessionSummaryReport += @" left join (SELECT distinct SessionID, PaymentMethodID, TotalCount from CountPerSession) cps ON cps.SessionID = s.SessionID and s.PaymentMethodID = cps.PaymentMethodID";
            printSessionSummaryReport += @" left join Sessions ss on s.SessionID = ss.SessionID";

            Dictionary<string, object> paramSummary = new Dictionary<string, object>();

            DateTime sdate = dtmTxFrom.Value;
            DateTime edate = dtmTxTo.Value;
            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
            paramSummary.Add("@sdate", sdate);
            paramSummary.Add("@edate", edate);
            Tbrep = new System.Data.DataTable();
            CommonClass.runSql(ref Tbrep, printSessionSummaryReport, paramSummary);

            dtSalesBreakdown = new System.Data.DataTable();
            string printSalesBreakdown = @"SELECT InvoiceType, SessionID, SUM(GrandTotal) as GrandTotal  FROM Sales where SalesType = 'INVOICE' group by InvoiceType,SessionID";
            CommonClass.runSql(ref dtSalesBreakdown, printSalesBreakdown);

        }
        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Payment Method";
            this.dgReport.Columns[2].HeaderText = "Total Count";

            this.dgReport.Columns[1].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[2].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[3].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //this.dgReport.Columns[0].Visible = false;
            //this.dgReport.Columns[1].Visible = false;
           this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
        }
        public decimal GrandTotal()
        {
            decimal x = 0;
            foreach (DataRow dr in Tbrep.Rows)
            {
                x += decimal.Parse(dr["Amount"].ToString());
            }
            return x;
        }
        public decimal TotalCount()
        {
            decimal x = 0;
            foreach (DataRow dr in Tbrep.Rows)
            {
                if (dr["TotalCount"] != null && dr["TotalCount"].ToString() != "")
                {
                    x += decimal.Parse(dr["TotalCount"].ToString());
                }
            }
            return x;
        }
        public double TotalDiscrepancy()
        {
            double x = 0;
            foreach (DataRow dr in Tbrep.Rows)
            {
                string discrepancy = dr["Discrepancy"].ToString();
                if (discrepancy != null && discrepancy != "" && discrepancy != "0")
                {
                    x += double.Parse(dr["Discrepancy"].ToString());
                }
            }
            return x;
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            if (Tbrep.Rows.Count != 0)
            {
                TbGrid = Tbrep.Copy();

                if (TbGrid.Rows.Count > 0)
                {
                    DataView dv = TbGrid.DefaultView;
                    dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                    TbGrid = dv.ToTable();
                    string lPrevItem = "";
                    string lPrevPayMethod = "";
                    string[] RowArray;
                    int rIndex;
                    dgReport.Rows.Clear();
                    for (int i = 0; i < TbGrid.Rows.Count; i++)
                    {
                        DataRow dr = TbGrid.Rows[i];
                        if (lPrevItem != dr["SessionID"].ToString())
                        {
                            lPrevItem = dr["SessionID"].ToString();
                            RowArray = new string[9];
                            RowArray[0] = "Session ID: " + dr["SessionID"].ToString();
                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                            lPrevPayMethod = "";
                        }
                        if (lPrevPayMethod != dr["PaymentMethod"].ToString())
                        {
                            lPrevPayMethod = dr["PaymentMethod"].ToString();
                            RowArray = new string[9];
                            RowArray[0] = dr["PaymentMethod"].ToString();
                            double Amount = Double.Parse(dr["Amount"].ToString());
                            RowArray[1] = Amount.ToString("C");
                            if (dr["TotalCount"] != null && dr["TotalCount"].ToString() != "")
                            {
                                double TotalCount = Double.Parse(dr["TotalCount"].ToString());
                                RowArray[2] = TotalCount.ToString("C");
                            }
                            if (dr["Discrepancy"] != null && dr["Discrepancy"].ToString() != "")
                            {
                                double discrepancy = Double.Parse(dr["Discrepancy"].ToString());
                                RowArray[3] = discrepancy.ToString("C");
                            }
                            dgReport.Rows.Add(RowArray);
                        }
                    }
                    RowArray = new string[9];
                    RowArray[0] = "GRAND TOTAL :";
                    RowArray[1] = double.Parse(GrandTotal().ToString()).ToString("C");
                    RowArray[2] = double.Parse(TotalCount().ToString()).ToString("C");
                    RowArray[3] = double.Parse(TotalDiscrepancy().ToString()).ToString("C");
                    dgReport.Rows.Add(RowArray);
                }

                //  dgReport.Columns["PartNumber"].Visible = false;
                FormatGrid();
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //FillSortCombo();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Report Contains no Data!");
            }
          }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Tender Summary Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            //dgPrinter.ColumnWidths.Add("Main Category", 80);
            //dgPrinter.ColumnWidths.Add("Sub Category", 80);
            //dgPrinter.ColumnWidths.Add("PartNum", 70);
            //dgPrinter.ColumnWidths.Add("ItemNum", 80);
            //dgPrinter.ColumnWidths.Add("ItemDesc", 80);
            //dgPrinter.ColumnWidths.Add("OnHand", 80);
            //dgPrinter.ColumnWidths.Add("Committed", 80);
            //dgPrinter.ColumnWidths.Add("OnOrder", 80);
            //dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);

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
                    // app.Visible = false;
                    ws.Cells[4, 1] = "Payment Method";
                    ws.Cells[4, 2] = "Amount";
                    ws.Cells[4, 3] = "Total Count";
                    ws.Cells[4, 4] = "Discrepancy";

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
                        if (item.Cells[3].Value != null)
                        {
                            ws.Cells[i, 4] = item.Cells[3].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "D3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 20;
                    ws.Cells[1, 1] = "Tender Summary Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "D4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Tender Summary Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptTenderSummary_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
    }
   
