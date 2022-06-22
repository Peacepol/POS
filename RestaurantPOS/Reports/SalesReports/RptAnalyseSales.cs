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

namespace AbleRetailPOS.Reports
{
    public partial class RptAnalyseSales : Form
    {
      
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private bool CanView = false;

        public RptAnalyseSales()
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
            
        }
        private string GetCustomerID(string pName)
        {
            string retval = "";
            SqlConnection con = null;
            try
            {
                         string sql = @"SELECT * from Profile where Type = 'Customer' and Name = @cname";


                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cname", pName);
                
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                System.Data.DataTable ltb = new System.Data.DataTable();
                da.Fill(ltb);
                if(ltb.Rows.Count > 0)
                {
                    retval = ltb.Rows[0]["ID"].ToString();
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
            return retval;
        }
        private void GetReportData()
        {
            SqlConnection con = null;
            try
            {
                
               
                DateTime sdate = Convert.ToDateTime(sdatePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime(); 
                DateTime edate = Convert.ToDateTime(edatePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                string sql = @"SELECT cs.*, GrandTotal/TotalSales as SalesPercent from 
                    (SELECT p.ProfileIDNumber, p.Name, sum(GrandTotal) as GrandTotal FROM Sales s
                    INNER JOIN Profile p ON s.CustomerID = p.ID WHERE TransactionDate BETWEEN @sdate AND @edate and SalesType in ('INVOICE','SINVOICE') group by p.ProfileIDNumber, p.Name ) cs,
                    (SELECT sum(GrandTotal) as TotalSales FROM Sales WHERE TransactionDate BETWEEN @sdate AND @edate and SalesType in ('INVOICE','SINVOICE')) ts";

              
               con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@sdate", sdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());
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
                Reports.ReportParams salesparams = new Reports.ReportParams();
                salesparams.PrtOpt = 1;
                salesparams.Rec.Add(TbRep);
                salesparams.ReportName = "CustomerAnalyseSale.rpt";
                salesparams.RptTitle = "Analyse Customer Sales";
                salesparams.Params = "compname|sdate|edate";
                salesparams.PVals = CommonClass.CompName.Trim() + "|" + sdatePicker.Value + "|" + edatePicker.Value;

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
            float TotalAmount = 0;
            float TotalPercent = 0;
          
            for (int i = 0; i < TbGrid.Rows.Count; i++)
            {
               
                TotalAmount += float.Parse(TbGrid.Rows[i]["GrandTotal"].ToString());
                TotalPercent += float.Parse(TbGrid.Rows[i]["SalesPercent"].ToString());
                
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
            this.dgReport.Columns[1].HeaderText = "Customer Name";
            this.dgReport.Columns[2].HeaderText = "Total Sales";
            this.dgReport.Columns[3].HeaderText = "Sales Percentage";           
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
                for (int i = 0; i < dgReport.ColumnCount ; i++)
                {
                    this.cbSort.Items.Add(dgReport.Columns[i].HeaderText);
                }
                this.cbSort.Enabled = true;
                this.btnSortGrid.Enabled = true;
                this.cbSort.SelectedIndex = 0;
            }


        }

       

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
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
            string lTitle = "Analyse Customer Sales Report" + Environment.NewLine + " Date From " + sdatePicker.Value.ToShortDateString() + " To " + edatePicker.Value.ToShortDateString();
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;

            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = lTitle;
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);

            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("ProfileIDNumber", 150);
            dgPrinter.ColumnWidths.Add("Name", 200);
            dgPrinter.ColumnWidths.Add("TotalSales", 150);
            dgPrinter.ColumnWidths.Add("SalesPercent", 150);
            
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.PrintPreviewDataGridView(dgReport);
        }

        private void RptAnalyseSales_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
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
                    ws.Cells[4, 2] = "Customer Name";
                    ws.Cells[4, 3] = "Total Sales";
                    ws.Cells[4, 4] = "Sales Percentage";
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
                            ws.Cells[i, 4] = item.Cells[3].Value.ToString() + "%";
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
                    ws.Cells[1, 1] = "Analyse Sales Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "D4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("C4").EntireColumn.NumberFormat = ".00";
                    ws.get_Range("D4").EntireColumn.NumberFormat = "###,##%";

                    //ws.get_Range("D4", "D4").EntireColumn.NumberFormat = "0";
                    //Range PercentRange = ws.get_Range("D4", "D4");
                    //PercentRange.NumberFormat = "###,##%";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Analyse Sales Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void edatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void rdoDesc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoAsc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sdatePicker_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
