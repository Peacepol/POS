using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Reports
{
    public partial class RptTenderDetails : Form
    {
        private System.Data.DataTable Tbrep;
        private System.Data.DataTable TbGrid;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;


        public RptTenderDetails()
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
            LoadDetailReport();
            GenerateReport();
        }
        private void LoadDetailReport()
        {
            string cashAR = "";


            string DetailSummaryReport = @"SELECT  pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod,  pt.Amount, pl.EntityID, s.SalesNumber as TransactionNumber, 'Sales' as TranType, pf.Name, s.GrandTotal as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Sales s on pl.EntityID = s.SalesID
                inner join Profile pf on s.CustomerID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = '' AND pl.EntryDate BETWEEN @sdate AND @edate ";
            DetailSummaryReport += @"UNION SELECT pt.ID, pt.PaymentID, pl.EntryDate, pt.PaymentMethodID, pm.PaymentMethod, pt.Amount, pl.EntityID, p.PaymentNumber as TransactionNumber, 'AR Payment' as TranType, pf.Name, p.TotalAmount as TranTotal, u.user_name, ss.SessionID, ss.SessionKey, ss.SessionStart, ss.SessionEnd
                from PaymentTender pt 
                inner join Payment p on pt.PaymentID = p.PaymentID
                inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                inner join Profile pf on p.ProfileID = pf.ID
                inner join Users u on p.UserID = u.user_id
                left join Sessions ss on p.SessionID = ss.SessionID
                where p.Source = 'P' AND pl.EntryDate BETWEEN @sdate AND @edate";
            Dictionary<string, object> param = new Dictionary<string, object>();

            DateTime sdate = dtmTxFrom.Value;
            DateTime edate = dtmTxTo.Value;
            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
            param.Add("@sdate", sdate);
            param.Add("@edate", edate);

            Tbrep = new System.Data.DataTable();

            CommonClass.runSql(ref Tbrep, DetailSummaryReport, param);

        }
        public void GenerateReport()
        {
            Reports.ReportParams SessionDetail = new Reports.ReportParams();
            SessionDetail.PrtOpt = 1;
            SessionDetail.Rec.Add(Tbrep);
            SessionDetail.ReportName = "TenderDetails.rpt";
            SessionDetail.RptTitle = "Tender Details Report";
            SessionDetail.Params = "compname|StartDate|EndDate";
            SessionDetail.PVals = CommonClass.CompName.Trim() + "|" + dtmTxFrom.Value.ToShortDateString() + " |" + dtmTxTo.Value.ToShortDateString();
            CommonClass.ShowReport(SessionDetail);
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadDetailReport();
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
                            RowArray[0] = "Session ID: "+dr["SessionID"].ToString();
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
                            RowArray[0] = "     >> " + dr["PaymentMethod"].ToString();
                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                            RowArray = new string[9];
                            RowArray[0] = dr["TranType"].ToString();
                            RowArray[1] = dr["EntryDate"].ToString();
                            RowArray[2] = dr["TransactionNumber"].ToString();
                            RowArray[3] = dr["Name"].ToString();

                            if (dr["Amount"] != null && dr["Amount"].ToString() != "")
                            {
                                double amount = Double.Parse(dr["Amount"].ToString());
                                RowArray[4] = amount.ToString("C");
                            }
                            if (dr["TranTotal"] != null && dr["TranTotal"].ToString() != "")
                            {
                                double trantotal = Double.Parse(dr["TranTotal"].ToString());
                                RowArray[5] = trantotal.ToString("C");
                            }
                            dgReport.Rows.Add(RowArray);
                        }
                        else
                        {
                             RowArray = new string[9];
                            RowArray[0] = dr["TranType"].ToString();
                            RowArray[1] = dr["EntryDate"].ToString();
                            RowArray[2] = dr["TransactionNumber"].ToString();
                            RowArray[3] = dr["Name"].ToString();

                            if (dr["Amount"] != null && dr["Amount"].ToString() != "")
                            {
                                double amount = Double.Parse(dr["Amount"].ToString());
                                RowArray[4] = amount.ToString("C");
                            }
                            if (dr["TranTotal"] != null && dr["TranTotal"].ToString() != "")
                            {
                                double trantotal = Double.Parse(dr["TranTotal"].ToString());
                                RowArray[5] = trantotal.ToString("C");
                            }
                            dgReport.Rows.Add(RowArray);
                        }
                      
                    }
                    RowArray = new string[9];
                    RowArray[0] = "GRAND TOTAL :";
                    double GrandAmountTotal = Double.Parse(AmountTotal().ToString());
                    RowArray[4] = GrandAmountTotal.ToString("C");
                    double GrandTranTotal = Double.Parse(TotalTran().ToString());
                    RowArray[5] = GrandTranTotal.ToString("C");
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
        public decimal AmountTotal()
        {
            decimal x = 0;
            foreach (DataRow dr in Tbrep.Rows)
            {
                x += decimal.Parse(dr["Amount"].ToString());
            }
            return x;
        }
        public decimal TotalTran()
        {
            decimal x = 0;
            foreach (DataRow dr in Tbrep.Rows)
            {
                if (dr["TranTotal"] != null && dr["TranTotal"].ToString() != "")
                {
                    x += decimal.Parse(dr["TranTotal"].ToString());
                }
            }
            return x;
        }
        private void FormatGrid()
        {
            this.dgReport.Columns[4].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[5].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";

            //this.dgReport.Columns[0].Visible = false;
            //this.dgReport.Columns[1].Visible = false;
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Tender Details Report";
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
                    ws.Cells[4, 1] = "Transaction Type";
                    ws.Cells[4, 2] = "Date & Time";
                    ws.Cells[4, 3] = "Transaction Number";
                    ws.Cells[4, 4] = "Customer Name";
                    ws.Cells[4, 5] = "Total";
                    ws.Cells[4, 6] = "Amount Tendered";

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
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "F3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 20;
                    ws.Cells[1, 1] = "Tender Details Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "F4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Tender Details Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptTenderDetails_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
