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

namespace RestaurantPOS.Reports.PurchaseReports
{
    public partial class AnalysePuchaseFYComparison : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private bool CanView = false;


        public AnalysePuchaseFYComparison()
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
       

        private void AnalysePuchaseFYComparison_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetReportData()
        {
            SqlConnection con = null;
            try
            {


                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();
                DateTime sdate2 = Convert.ToDateTime(sdatePicker2.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate2 = Convert.ToDateTime(edatePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                string sql = @"SELECT d1.ProfileIDNumber, d1.Name, d1.GrandTotal, ISNULL(d2.GrandTotal,0) as GrandTotal2, (ISNULL(d2.GrandTotal,0) - d1.GrandTotal) as diffamount, CAST(0 as float) as diffpercent  
                    from (SELECT cs.* from (SELECT p.ID, p.ProfileIDNumber, p.Name, sum(GrandTotal) as GrandTotal FROM ReceiveItems r
                    INNER JOIN Profile p ON r.SupplierID = p.ID WHERE TransactionDate BETWEEN @sdate AND @edate group by p.ID, p.ProfileIDNumber, p.Name ) cs ) d1 
                    LEFT JOIN 
                    (SELECT cs.* from (SELECT p.ID,  sum(GrandTotal) as GrandTotal FROM ReceiveItems r
                    INNER JOIN Profile p ON r.SupplierID = p.ID WHERE TransactionDate BETWEEN @sdate2 AND @edate2  group by p.ID) cs) d2
                    on d1.ID = d2.ID";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                cmd.Parameters.AddWithValue("@sdate2", sdate2);
                cmd.Parameters.AddWithValue("@edate2", edate2);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new System.Data.DataTable();
                da.Fill(TbRep);

                float DiffPercent = 0;
                float Total1 = 0;
                float Total2 = 0;
                float Diff = 0;
                //TO CALCULATE Difference % TbRep;
                for (int i = 0; i < TbRep.Rows.Count; i++)
                {
                    Total1 = float.Parse(TbRep.Rows[i]["GrandTotal"].ToString());
                    Total2 = float.Parse(TbRep.Rows[i]["GrandTotal2"].ToString());
                    Diff = float.Parse(TbRep.Rows[i]["diffamount"].ToString());
                    if (Total2 != 0)
                    {
                        DiffPercent = (Total2 - Total1) / Total2;
                        TbRep.Rows[i]["diffpercent"] = DiffPercent;
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
        void LoadReport(int pViewType = 0)
        {

            if (pViewType == 0)
            {
                Reports.ReportParams salesparams = new Reports.ReportParams();
                salesparams.PrtOpt = 1;
                salesparams.Rec.Add(TbRep);
                salesparams.ReportName = "AnalysePurchaseDRComparison.rpt";
                salesparams.RptTitle = "Analyse Supplier Purchases Date Range Comparison";
                salesparams.Params = "compname|sdate|edate|sdate2|edate2";
                salesparams.PVals = CommonClass.CompName.Trim() + "|" + sdatePicker.Value + "|" + edatePicker.Value + "|" + sdatePicker2.Value + "|" + edatePicker2.Value;

                CommonClass.ShowReport(salesparams);


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
            float DiffPercent = 0;
            float Total1 = 0;
            float Total2 = 0;
            float Diff = 0;
            float TotalAmount1 = 0;
            float TotalAmount2 = 0;
            float TotalDiff = 0;
            for (int i = 0; i < TbGrid.Rows.Count; i++)
            {
                Total1 = float.Parse(TbGrid.Rows[i]["GrandTotal"].ToString());
                Total2 = float.Parse(TbGrid.Rows[i]["GrandTotal2"].ToString());
                Diff = float.Parse(TbGrid.Rows[i]["diffamount"].ToString());
                TotalAmount1 += Total1;
                TotalAmount2 += Total2;
                TotalDiff += Total2 - Total1;

            }
            DataView dv = TbGrid.DefaultView;
            dv.Sort = TbGrid.Columns[pSortIndex].ColumnName + " " + pSortMode;
            TbGrid = dv.ToTable();

            DataRow rw = TbGrid.NewRow();
            rw[0] = "TOTAL";
            rw[2] = TotalAmount1;
            rw[3] = TotalAmount2;
            rw[4] = TotalDiff;
            DiffPercent = 0;
            if (TotalAmount2 != 0)
            {
                DiffPercent = TotalDiff / TotalAmount2;
            }
            rw[5] = DiffPercent;
            TbGrid.Rows.Add(rw);
        }

        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Profile Number";
            this.dgReport.Columns[1].HeaderText = "Customer Name";
            this.dgReport.Columns[2].HeaderText = "Total Purchases Range 1";
            this.dgReport.Columns[3].HeaderText = "Total Purchases Range 2";
            this.dgReport.Columns[4].HeaderText = "Difference";
            this.dgReport.Columns[5].HeaderText = "Difference %";

            this.dgReport.Columns[2].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[3].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[4].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[5].DefaultCellStyle.Format = "0.#0%";

            this.dgReport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

      

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(0);
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            string lTitle = "Analyse Customer Sales Date Range Comparison Report" + Environment.NewLine + " Date From " + sdatePicker.Value.ToShortDateString() + " To " + edatePicker.Value.ToShortDateString();
            lTitle += Environment.NewLine + " Versus";
            lTitle += Environment.NewLine + " Date From " + sdatePicker2.Value.ToShortDateString() + " To " + edatePicker2.Value.ToShortDateString();
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;

            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = lTitle;
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);

            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ProfileIDNumber", 100);
            dgPrinter.ColumnWidths.Add("Name", 200);
            dgPrinter.ColumnWidths.Add("GrandTotal", 100);
            dgPrinter.ColumnWidths.Add("GrandTotal2", 100);
            dgPrinter.ColumnWidths.Add("diffamount", 100);
            dgPrinter.ColumnWidths.Add("diffpercent", 100);

            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            string lSortMode = (rdoAsc.Checked == true ? "asc" : "desc");
            CalculateTotal(this.cbSort.SelectedIndex, lSortMode);
            this.dgReport.DataSource = TbGrid;
            FormatGrid();
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
                    ws.Cells[4, 1] = "Profile Number";
                    ws.Cells[4, 2] = "Supplier Name";
                    ws.Cells[4, 3] = "Total Purchases Range 1";
                    ws.Cells[4, 4] = "Total Purchases Range 2";
                    ws.Cells[4, 5] = "Difference";
                    ws.Cells[4, 6] = "Difference %";
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
                            ws.Cells[i, 3] = Math.Round(float.Parse(item.Cells[2].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[3].Value != null)
                        {
                            ws.Cells[i, 4] = Math.Round(float.Parse(item.Cells[3].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = Math.Round(float.Parse(item.Cells[4].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[5].Value != null && item.Cells[5].ToString() != "")
                        {
                            if (item.Cells[5].Value.ToString() != "0")
                            {
                                ws.Cells[i, 6] = item.Cells[5].Value.ToString() + "%";
                            }
                            else
                            {
                                ws.Cells[i, 6] = "0.00%";
                            }
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
                    ws.Cells[1, 1] = "Analyse Purchases Comparison Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "F4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = "0";
                    ws.get_Range("C4").EntireColumn.NumberFormat = ".00";
                    ws.get_Range("D4").EntireColumn.NumberFormat = ".00";
                    ws.get_Range("F4").EntireColumn.NumberFormat = "###,##%";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Analyse Purchases Comparison Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void btnDisplay_Click_1(object sender, EventArgs e)
        {
            GetReportData();
            LoadReport(1);
        }
    }
}
