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

namespace RestaurantPOS.Reports.PurchaseReports
{
    public partial class ReceivedOrdersSummary : Form
    {
        System.Data.DataTable TbRep;
        System.Data.DataTable TbGrid;
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        string SupplierID;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public ReceivedOrdersSummary()
        {
            InitializeComponent();
            SupplierID = "";
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            Reports.ReportParams ClosedBills = new Reports.ReportParams();
            ClosedBills.PrtOpt = 1;
            ClosedBills.Rec.Add(TbRep);
            ClosedBills.ReportName = "ReceivedOrders.rpt";
            ClosedBills.RptTitle = "Received Orders";

            ClosedBills.Params = "compname|sdate|edate";
            ClosedBills.PVals = CommonClass.CompName.Trim() + "|" + sdateTimePicker.Value.ToString("yyyy-MM-dd") + "|" + edateTimePicker.Value.ToString("yyyy-MM-dd");

            CommonClass.ShowReport(ClosedBills);
            Cursor.Current = Cursors.Default;
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT 
                        r.TransactionDate,
                        r.ReceiveItemNumber,
                        p.PurchaseNumber, 
                        p.TransactionDate as PODate,
                        r.SupplierINVNumber,
                        pro.Name,
                        p.GrandTotal as POTotal, 
                        p.PromiseDate,
                        r.GrandTotal
                        FROM ReceiveItems r INNER JOIN Profile pro ON r.SupplierID = pro.ID INNER JOIN Purchases p ON r.PurchaseID = p.PurchaseID 
                        WHERE p.TransactionDate BETWEEN @sdate AND @edate ";

                if (SupplierID != "")
                {
                    sql += " AND r.SupplierID = " + SupplierID;
                }
                if (txtShipVia.Text != "")
                {
                    sql += " AND r.ShippingMethodID = " + ShippingID;
                }
                if (toNum.Value != 0)
                {
                    sql += " AND r.GrandTotal BETWEEN '" + fromNum.Value + "' AND '" + toNum.Value + "'";
                }
                if (promised == true)
                {
                    sql += " AND p.PromiseDate BETWEEN @psdate AND @pedate ";
                }
                if (txtEmployee.Text != "")
                {
                    sql += " AND UserID = " + EmployeeID;
                }
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                DateTime sdate = Convert.ToDateTime(sdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime edate = Convert.ToDateTime(edateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                DateTime psdate = Convert.ToDateTime(PSdateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime pedate = Convert.ToDateTime(PEdateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                cmd.Parameters.AddWithValue("@sdate", sdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@psdate", psdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@pedate", pedate.ToUniversalTime());

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

        void ShowShippingmethod()
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;

                txtShipVia.Text = ShipList[0];
                ShippingID = ShipList[1];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbShipping_Click(object sender, EventArgs e)
        {
            ShowShippingmethod();
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

        private void pbEmployee_Click(object sender, EventArgs e)
        {
            SalespersonLookup SalespersonDlg = new SalespersonLookup();
            if (SalespersonDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lSales = SalespersonDlg.GetSalesperson;
                EmployeeID = lSales[0];
                txtEmployee.Text = lSales[1];
            }
        }

        private void ClosedBillReports_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void pbSupplier_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                SupplierID = lProfile[0];
                this.txtSupplier.Text = lProfile[2];
            }
        }

        private void chkAllSuppliers_CheckedChanged(object sender, EventArgs e)
        {
            this.pbSupplier.Enabled = true;
            if (this.chkAllSuppliers.Checked)
            {
                this.txtSupplier.Text = "";
                SupplierID = "";
                this.pbSupplier.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                dgReport.DataSource = TbGrid;
                DataRow rw = TbGrid.NewRow();
                rw[1] = "TOTAL";
                rw[6] = GrandTotal();

                TbGrid.Rows.Add(rw);
                //  dgReport.Columns["PartNumber"].Visible = false;
                FormatGrid();
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                FillSortCombo();

            }
            else
            {
                MessageBox.Show("Contains No Data.", "Report Information");
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {

            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Receive Orders Summary Report \n From " + sdateTimePicker.Value.ToShortDateString() + " to " + edateTimePicker.Value.ToShortDateString();
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("TransactionDate", 90);
            dgPrinter.ColumnWidths.Add("ReceiveItemNumber", 80);
            dgPrinter.ColumnWidths.Add("PurchaseNumber", 80);
            dgPrinter.ColumnWidths.Add("PODate", 100);
            dgPrinter.ColumnWidths.Add("GrandTotal", 120);
            dgPrinter.ColumnWidths.Add("TotalDue", 100);
            dgPrinter.ColumnWidths.Add("SupplierINVNumber", 100);
            dgPrinter.ColumnWidths.Add("Name", 120);
            dgPrinter.ColumnWidths.Add("PromiseDate", 80);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            // dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? " asc" : " desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
        }
        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Date Received";
            this.dgReport.Columns[1].HeaderText = "Receive Item #";
            this.dgReport.Columns[2].HeaderText = "PO #";
            this.dgReport.Columns[3].HeaderText = "PO Date";
            this.dgReport.Columns[4].HeaderText = "Supplier Inv #";
            this.dgReport.Columns[5].HeaderText = "Name";
            this.dgReport.Columns[6].HeaderText = "Amount";
            this.dgReport.Columns[7].HeaderText = "Promised Date";
            //   this.dgReport.Columns[5].HeaderText = "Promised Date";
            this.dgReport.Columns[8].Visible = false;
            this.dgReport.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgReport.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgReport.Columns[6].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgReport.Rows[dgReport.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
        }
        public decimal GrandTotal()
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["POTotal"].ToString());
            }
            return x;
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

        private void btnExportExcell_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;

                    ws.Cells[4, 1] = "Date Received";
                    ws.Cells[4, 2] = "Receive Item #";
                    ws.Cells[4, 3] = "PO #";
                    ws.Cells[4, 4] = "PO Date";
                    ws.Cells[4, 5] = "Supplier Inv #";
                    ws.Cells[4, 6] = "Name";
                    ws.Cells[4, 7] = "Amount";
                    ws.Cells[4, 8] = "Promised Date";
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
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
                        if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                        {
                            ws.Cells[i, 4] = Convert.ToDateTime(item.Cells[3].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                        }
                        if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                        {
                            ws.Cells[i, 7] = Math.Round(float.Parse(item.Cells[6].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                        {
                            ws.Cells[i, 8] = Convert.ToDateTime(item.Cells[7].Value.ToString()).ToShortDateString();
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "H3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 22;
                    ws.Cells[1, 1] = "Received Order Summary Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "H4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Received Order Summary Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
