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
    public partial class AnalysePurchaseRpt : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private bool CanView = false;

        public AnalysePurchaseRpt()
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

    
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void AnalysePurchaseRpt_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void GetReportData()
        {
            SqlConnection con = null;
            try
            {


                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                string sql = @"SELECT cs.*, GrandTotal/TotalReceived as PurchasePercent from 
                    (SELECT p.ProfileIDNumber, p.Name, sum(GrandTotal) as GrandTotal FROM ReceiveItems r
                    INNER JOIN Profile p ON r.SupplierID = p.ID WHERE TransactionDate BETWEEN @sdate AND @edate group by p.ProfileIDNumber, p.Name ) cs,
                    (SELECT sum(GrandTotal) as TotalReceived FROM ReceiveItems WHERE TransactionDate BETWEEN @sdate AND @edate) ts";


                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
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
                Reports.ReportParams AnalysePurchase = new Reports.ReportParams();
                AnalysePurchase.PrtOpt = 1;
                AnalysePurchase.Rec.Add(TbRep);
                AnalysePurchase.ReportName = "AnalysePurchase.rpt";
                AnalysePurchase.RptTitle = "Analyse Supploer Purchases";
                AnalysePurchase.Params = "compname|sdate|edate";
                AnalysePurchase.PVals = CommonClass.CompName.Trim() + "|" + sdatePicker.Value + "|" + edatePicker.Value;

                CommonClass.ShowReport(AnalysePurchase);



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
            float TotalPercent = 0;

            for (int i = 0; i < TbGrid.Rows.Count; i++)
            {

                TotalAmount += float.Parse(TbGrid.Rows[i]["GrandTotal"].ToString());
                TotalPercent += float.Parse(TbGrid.Rows[i]["PurchasePercent"].ToString());

            }

            DataView dv = TbGrid.DefaultView;
            dv.Sort = TbGrid.Columns[pSortIndex].ColumnName + " " + pSortMode;
            TbGrid = dv.ToTable();

            DataRow rw = TbGrid.NewRow();
            rw[0] = "TOTAL";
            rw[2] = TotalAmount;
            rw[3] = TotalPercent;
            TbGrid.Rows.Add(rw);
        }

        private void FormatGrid()
        {
            this.dgReport.Columns[0].HeaderText = "Profile Number";
            this.dgReport.Columns[1].HeaderText = "Supplier Name";
            this.dgReport.Columns[2].HeaderText = "Total Purchases";
            this.dgReport.Columns[3].HeaderText = "Purchases Percentage";
            this.dgReport.Columns[2].DefaultCellStyle.Format = "C2";
            this.dgReport.Columns[3].DefaultCellStyle.Format = "##.#0%";
            this.dgReport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            string lTitle = "Analyse Supplier Purchases Report" + Environment.NewLine + " Date From " + sdatePicker.Value.ToShortDateString() + " To " + edatePicker.Value.ToShortDateString();
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;

            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = lTitle;
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);

            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ProfileIDNumber", 150);
            dgPrinter.ColumnWidths.Add("Name", 200);
            dgPrinter.ColumnWidths.Add("GrandTotal", 200);
            dgPrinter.ColumnWidths.Add("PurchasePercent", 150);

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
                    this.dgReport.Columns[0].HeaderText = "Profile Number";
                    this.dgReport.Columns[1].HeaderText = "Supplier Name";
                    this.dgReport.Columns[2].HeaderText = "Total Purchases";
                    this.dgReport.Columns[3].HeaderText = "Purchases Percentage";

                    ws.Cells[4, 1] = "Profile Number";
                    ws.Cells[4, 2] = "Supplier Name";
                    ws.Cells[4, 3] = "Total Purchases";
                    ws.Cells[4, 4] = "Purchases Percentage";
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
                        if (item.Cells[3].Value != null && item.Cells[3].ToString() != "")
                        {
                            if (item.Cells[3].Value.ToString() != "0")
                            {
                                ws.Cells[i, 4] = item.Cells[3].Value.ToString() + "%";
                            }
                            else
                            {
                                ws.Cells[i, 4] = "0.00%";
                            }
                        }

                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "D3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Analyse Purchases Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "D4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("C4").EntireColumn.NumberFormat = ".00";
                    ws.get_Range("D4").EntireColumn.NumberFormat = "###,##%";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Analyse Purchases Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
