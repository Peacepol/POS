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
    public partial class RptSalesItemSummary : Form
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
        DateTime TimeNow = DateTime.Now;
        bool promised = false;
        private bool CanView = false;
        public RptSalesItemSummary()
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

        private void GetReportData()
        {
            selectSql = @" Select p.Name ,i.PartNumber, i.ItemNumber, i.ItemName, sum(sl.ShipQty) as ShipQty, sum(sl.SubTotal) as TotalAmount  From Items i
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
            //if (dateTimePicker3.Value != TimeNow)
            //{
            //    selectSql += "AND PromiseDate BETWEEN @pdateStart AND @pdateEnd";
            //}
            //else
            //{
            //    //All Promise Date
            //}
            if (promised == true)
            {
                selectSql += " AND s.PromiseDate  BETWEEN @psdate AND @pedate ";
            }
            selectSql += " GROUP BY  p.Name ,i.PartNumber, i.ItemNumber, i.ItemName ";
            try
            {

                con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);
                con.Open();
                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();
                DateTime psdate = Convert.ToDateTime(PSdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime pedate = Convert.ToDateTime(PEdateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

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
                reportName = "SalesItemSummary.rpt";
                reportTitle = "Sales Item Summary";
                Reports.ReportParams SalesItemSummary = new Reports.ReportParams();
                SalesItemSummary.PrtOpt = 1;
                SalesItemSummary.Rec.Add(TbRep);
                SalesItemSummary.ReportName = reportName;
                SalesItemSummary.RptTitle = reportTitle;
                SalesItemSummary.Params = "compname";
                SalesItemSummary.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(SalesItemSummary);

            }
            else
            {

                CalculateTotal(1);
                this.dgReport.DataSource = TbGrid;
                FormatGrid();
                FillSortCombo();


            }

        }
        private void CalculateTotal(int pSortIndex = 1, string pSortMode = "asc")
        {
            TbGrid = TbRep.Copy();
            float TotalAmount = 0;
            float TotalQty = 0;

            for (int i = 0; i < TbGrid.Rows.Count; i++)
            {

                TotalAmount += float.Parse(TbGrid.Rows[i]["TotalAmount"].ToString());
                TotalQty += float.Parse(TbGrid.Rows[i]["ShipQty"].ToString());

            }

            DataView dv = TbGrid.DefaultView;
            dv.Sort = TbGrid.Columns[pSortIndex].ColumnName + " " + pSortMode;
            TbGrid = dv.ToTable();

            DataRow rw = TbGrid.NewRow();
            rw[0] = "TOTAL";

            rw[4] = TotalQty;
            rw[5] = TotalAmount;
            TbGrid.Rows.Add(rw);
        }

        private void FormatGrid()
        {

            this.dgReport.Columns[0].HeaderText = "Customer Name";
            this.dgReport.Columns[1].HeaderText = "Part Number";
            this.dgReport.Columns[2].HeaderText = "Item Number";
            this.dgReport.Columns[3].HeaderText = "Item Name";
            this.dgReport.Columns[4].HeaderText = "Quantity";
            this.dgReport.Columns[5].HeaderText = "Total Amount";
            this.dgReport.Columns[5].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
        }
        private void FillSortCombo()
        {
            if (this.cbSort.Items.Count == 0)
            {
                for (int i = 0; i < dgReport.ColumnCount; i++)
                {
                    this.cbSort.Items.Add(dgReport.Columns[i].HeaderText);
                }
                this.cbSort.Enabled = true;
                this.btnSortGrid.Enabled = true;
                this.cbSort.SelectedIndex = 0;
            }


        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            string lSortMode = (rdoAsc.Checked == true ? "asc" : "desc");
            CalculateTotal(this.cbSort.SelectedIndex, lSortMode);
            this.dgReport.DataSource = TbGrid;
            FormatGrid();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;

            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Sales Item Summary Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);

            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("Name", 150);
            dgPrinter.ColumnWidths.Add("PartNumber", 100);
            dgPrinter.ColumnWidths.Add("ItemNumber", 100);
            dgPrinter.ColumnWidths.Add("ItemName", 120);
            dgPrinter.ColumnWidths.Add("ShipQty", 100);
            dgPrinter.ColumnWidths.Add("TotalAmount", 100);
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
                    // app.Visible = false;
                    ws.Cells[4, 1] = "Customer Name";
                    ws.Cells[4, 2] = "Part Number";
                    ws.Cells[4, 3] = "Item Number";
                    ws.Cells[4, 4] = "Item Name";
                    ws.Cells[4, 5] = "Quantity";
                    ws.Cells[4, 6] = "Total Amount";
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {
                        ws.Cells[i, 1] = item.Cells[0].Value.ToString();
                        ws.Cells[i, 2] = item.Cells[1].Value.ToString();
                        ws.Cells[i, 3] = item.Cells[2].Value.ToString();
                        ws.Cells[i, 4] = item.Cells[3].Value.ToString();
                        ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            double TotalAmount = double.Parse(item.Cells[5].Value.ToString());
                            ws.Cells[i, 6] = TotalAmount.ToString("C");
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "F3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Sales Item Summary Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "F4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.get_Range("E5").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("E5").EntireColumn.NumberFormat = ".00";
                    //ws.get_Range("H").EntireColumn.NumberFormat = "C2 #,###,###.00";

                    ws.Columns.AutoFit();
                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales Item Summary Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

        }

        private void RptSalesItemSummary_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
