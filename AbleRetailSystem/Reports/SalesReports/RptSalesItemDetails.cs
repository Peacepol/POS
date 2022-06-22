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
    public partial class RptSalesItemDetails : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection con;
        string selectSql = "";
        string reportName;
        string reportTitle;
        string shippingID;
        string salespersonID;
        bool promised = false;
        DateTime TimeNow = DateTime.Now;
        private bool CanView = false;
        public RptSalesItemDetails()
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
        void LoadReport()
        {
            
        }
        private void GetReportData()
        {
            selectSql = @"Select  i.PartNumber,i.ItemNumber,  i.ItemName,  p.Name , s.SalesNumber, s.TransactionDate, sl.ShipQty, sl.SubTotal as TotalAmount,s.InvoiceStatus , s.PromiseDate  
                                From Items i 
                                INNER JOIN SalesLines sl ON i.ID = sl.EntityID
                                INNER JOIN Sales s On s.SalesID = sl.SalesID
                                INNER JOIN Profile p ON p.ID = s.CustomerID
                                ";
            if (cmbSalesStatus.Text == "All Invoices")
            {
                selectSql += " AND SalesType in ('INVOICE','SINVOICE')";
            }
            else if (cmbSalesStatus.Text == "Open Sales")
            {
                selectSql += " AND SalesType in ('INVOICE','SINVOICE') AND InvoiceStatus = 'OPEN'";

            }
            else if (cmbSalesStatus.Text == "Credits")
            {
                selectSql += " AND SalesType in ('INVOICE','SINVOICE') and s.GrandTotal < 0";

            }
            else if (cmbSalesStatus.Text == "Closed Sales")
            {

                selectSql += " AND SalesType in ('INVOICE','SINVOICE') AND InvoiceStatus = 'Closed'";
            }

            else if (cmbSalesStatus.Text == "Orders")
            {

                selectSql += " AND SalesType = 'ORDER'";
            }
            else if (cmbSalesStatus.Text == "Quotes")
            {

                selectSql += " AND SalesType = 'QUOTE'";
            }
            else if (cmbSalesStatus.Text == "Lay-By")
            {

                selectSql += " AND SalesType = 'LAY-BY'";
            }
            else
            {
                //ALL
            }

            if (txtAmountFrom.Text != "")
            {
                selectSql += "AND GrandTotal BETWEEN " + txtAmountFrom.Text + " AND " + txtAmountTo.Text + "";
            }
            else
            {
                //All Amounts
            }
            if (txtShipVia.Text != "")
            {
                selectSql += "AND Sales.ShippingMethodID = " + shippingID + "";
            }
            else
            {
                //All Shipping Method
            }
            if (txtEmployee.Text != "")
            {
                selectSql += "AND SalesPersonID = " + salespersonID + "";
            }
            else
            {
                //ALL Sales Person
            }
            
            if (promised == true)
            {
                selectSql += " AND s.PromiseDate  BETWEEN @psdate AND @pedate ";
            }
            selectSql += " ORDER BY i.PartNumber";
            try
            {
               
                con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);
                con.Open();
                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();
                DateTime psdate = Convert.ToDateTime(PSdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime pedate = Convert.ToDateTime(PEdateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();


                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@psdate", psdate);
                cmd.Parameters.AddWithValue("@pedate", pedate);
                da = new SqlDataAdapter();
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
                reportName = "SalesItemDetails.rpt";
                reportTitle = "Sales Item Details";
                Reports.ReportParams SalesItemDetails = new Reports.ReportParams();
                SalesItemDetails.PrtOpt = 1;
                SalesItemDetails.Rec.Add(TbRep);
                SalesItemDetails.ReportName = reportName;
                SalesItemDetails.RptTitle = reportTitle;
                SalesItemDetails.Params = "compname";
                SalesItemDetails.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(SalesItemDetails);
            }
            else
            {

                CalculateTotal(1);
                this.dgReport.DataSource = TbGrid;
                FormatGrid();
                foreach(DataGridViewRow dgvr in dgReport.Rows)
                {
                    if(dgvr.Cells[0].Value.ToString() == "TOTAL")
                    {

                        dgReport.Rows[dgvr.Index].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[dgvr.Index].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    }
                }
                //FillSortCombo();


            }

        }
        private void CalculateTotal(int pSortIndex = 1, string pSortMode = "asc")
        {
            TbGrid = TbRep.Clone();
            float TotalAmount = 0;
            float TotalQty = 0;
            float ItemTotalQty = 0;
            float ItemTotalAmount = 0;
            string lPartNo = "";
            DataRow rw;
            DateTime lTranDate;
            DateTime lPromiseDate;
            for (int i = 0; i < TbRep.Rows.Count; i++)
            {
                if(lPartNo != TbRep.Rows[i]["PartNumber"].ToString())
                {
                    if (i != 0)
                    {
                        rw = TbGrid.NewRow();
                        rw[0] = "TOTAL";
                        rw[6] = ItemTotalQty;
                         rw[7] = ItemTotalAmount;
                        TbGrid.Rows.Add(rw);
                        ItemTotalQty = 0;
                        ItemTotalAmount = 0;
                    }
                }

                TotalAmount += float.Parse(TbRep.Rows[i]["TotalAmount"].ToString());
                TotalQty += float.Parse(TbRep.Rows[i]["ShipQty"].ToString());
                ItemTotalAmount += float.Parse(TbRep.Rows[i]["TotalAmount"].ToString());
                ItemTotalQty += float.Parse(TbRep.Rows[i]["ShipQty"].ToString());
                lTranDate = Convert.ToDateTime(TbRep.Rows[i]["TransactionDate"].ToString()).ToLocalTime();
                lPromiseDate = Convert.ToDateTime(TbRep.Rows[i]["PromiseDate"].ToString()).ToLocalTime();              
                rw = TbGrid.NewRow();
                rw[0] = TbRep.Rows[i][0];
                rw[1] = TbRep.Rows[i][1];
                rw[2] = TbRep.Rows[i][2];
                rw[3] = TbRep.Rows[i][3];
                rw[4] = TbRep.Rows[i][4];
                rw[5] = lTranDate.ToShortDateString();
                rw[6] = TbRep.Rows[i][6];
                rw[7] = TbRep.Rows[i][7];
                rw[8] = TbRep.Rows[i][8];
                rw[9] = lPromiseDate.ToShortDateString();
                TbGrid.Rows.Add(rw);
                lPartNo = TbRep.Rows[i]["PartNumber"].ToString();

            }
                       

            rw = TbGrid.NewRow();
            rw[0] = "TOTAL";
            rw[6] = ItemTotalQty;
            rw[7] = ItemTotalAmount;
            TbGrid.Rows.Add(rw);

            rw = TbGrid.NewRow();
            rw[0] = "GRAND TOTAL";
            rw[6] = TotalQty;
            rw[7] = TotalAmount;
            TbGrid.Rows.Add(rw);
        }

        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Part Number";
            this.dgReport.Columns[1].HeaderText = "Item Number";
            this.dgReport.Columns[2].HeaderText = "Item Name";
            this.dgReport.Columns[3].HeaderText = "Customer Name";
            this.dgReport.Columns[4].HeaderText = "Sales Number";
            this.dgReport.Columns[5].HeaderText = "Date";
            this.dgReport.Columns[6].HeaderText = "Quantity";
            this.dgReport.Columns[7].HeaderText = "Total Amount(Ex)";
            this.dgReport.Columns[8].HeaderText = "Invoice Status";
            this.dgReport.Columns[9].HeaderText = "Promise Date";

            this.dgReport.Columns[6].DefaultCellStyle.Format = "F";
            this.dgReport.Columns[7].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;

        }
     


        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_SalespersonLookup_Click(object sender, EventArgs e)
        {
            SalespersonLookup SalespersonDlg = new SalespersonLookup();
            if (SalespersonDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lSales = SalespersonDlg.GetSalesperson;
                salespersonID = lSales[0];
                txtEmployee.Text = lSales[1];

            }
        }

        private void btn_ShippingmethodLookup_Click(object sender, EventArgs e)
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;
                txtShipVia.Text = ShipList[0];
                shippingID = ShipList[1];
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void DateRangesStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRangesStart.SelectedIndex != 0)
            {
                DateRangesEnd.SelectedIndex = DateRangesStart.SelectedIndex;
                PSdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex, 1, 00, 00, 00);
                PEdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesEnd.SelectedIndex, (DateTime.DaysInMonth(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex)), 23, 59, 59);
                promised = true;
            }
            if (DateRangesStart.SelectedIndex == 0)
            {
                promised = false;
            }
        }

        private void DateRangesEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRangesEnd.SelectedIndex != 0)
            {
                PEdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesEnd.SelectedIndex, (DateTime.DaysInMonth(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex)), 23, 59, 59);
            }
            else
            {
                promised = false;
            }
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
            dgPrinter.SubTitle = "Sales Item Details Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("PartNumber", 90);
            dgPrinter.ColumnWidths.Add("ItemNumber", 90);
            dgPrinter.ColumnWidths.Add("ItemName", 150);
            dgPrinter.ColumnWidths.Add("Name", 150);
            dgPrinter.ColumnWidths.Add("SalesNumber", 100);
            dgPrinter.ColumnWidths.Add("Date", 70);
            dgPrinter.ColumnWidths.Add("ShipQty", 70);
            dgPrinter.ColumnWidths.Add("TotalAmount", 100);
            dgPrinter.ColumnWidths.Add("InvoiceStatus", 80);
            dgPrinter.ColumnWidths.Add("PromiseDate", 70);
            dgPrinter.PageSettings.Landscape = true;
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;
            dgPrinter.FooterSpacing = 15;
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
                    ws.Cells[4, 1] = "Part Number";
                    ws.Cells[4, 2] = "Item Number";
                    ws.Cells[4, 3] = "Item Name";
                    ws.Cells[4, 4] = "Customer Name";
                    ws.Cells[4, 5] = "Sales Number";
                    ws.Cells[4, 6] = "Date";
                    ws.Cells[4, 7] = "Quantity";
                    ws.Cells[4, 8] = "Total Amount(Ex)";
                    ws.Cells[4, 9] = "Invoice Statu";
                    ws.Cells[4, 10] = "Promised Date";

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
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            ws.Cells[i, 6] = Convert.ToDateTime(item.Cells[5].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            double TotalAmount = double.Parse(item.Cells[7].Value.ToString());
                            ws.Cells[i, 8] = TotalAmount.ToString("C");
                        }
                        if (item.Cells[8].Value != null)
                        {
                            ws.Cells[i, 9] = item.Cells[8].Value.ToString();
                        }
                        if (item.Cells[9].Value != null && item.Cells[9].Value.ToString() != "")
                        {
                            ws.Cells[i, 10] = Convert.ToDateTime(item.Cells[9].Value.ToString()).ToShortDateString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "J3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Sales Item Details Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "J4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("B5").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("J5").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("G5").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    ws.get_Range("G5").EntireColumn.NumberFormat = "0.00";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales Item Details Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void RptSalesItemDetails_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
