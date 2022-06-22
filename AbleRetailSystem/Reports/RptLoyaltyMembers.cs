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
    public partial class RptLoyaltyMembers : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;
        public RptLoyaltyMembers()
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            dgReport.Rows.Clear();
            // FormatGrid();
            string[] RowArray;
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                // dgReport.DataSource = TbGrid;
                int i = 0;
                foreach (DataRow dr in TbRep.Rows)
                {

                    dgReport.Rows.Add();
                    dgReport.Rows[i].Cells["Number"].Value = dr["Number"].ToString();
                    dgReport.Rows[i].Cells["MemberName"].Value = dr["MemberName"].ToString();
                    dgReport.Rows[i].Cells["City"].Value = dr["City"].ToString();
                    dgReport.Rows[i].Cells["State"].Value = dr["State"].ToString();
                    dgReport.Rows[i].Cells["PostCode"].Value = dr["PostCode"].ToString();
                    dgReport.Rows[i].Cells["Country"].Value = dr["Country"].ToString();
                    dgReport.Rows[i].Cells["StartDate"].Value = Convert.ToDateTime(dr["StartDate"]).ToString("MM - dd - yyyy");
                    dgReport.Rows[i].Cells["EndDate"].Value = Convert.ToDateTime(dr["EndDate"]).ToString("MM - dd - yyyy");
                    dgReport.Rows[i].Cells["Active"].Value = dr["IsActive"].ToString() == "0" ? "No" : "Yes";
                    dgReport.Rows[i].Cells["Customer"].Value = dr["CustomerName"].ToString();
                    if (dr["Total"] != null && dr["Total"].ToString() != "" && dr["Total"].ToString() != "0")
                    {
                        dgReport.Rows[i].Cells["Points"].Value = float.Parse(dr["Total"].ToString() == null ? "0" : dr["Total"].ToString()).ToString("F");
                    }
                    else
                    {
                        dgReport.Rows[i].Cells["Points"].Value = "0.00";
                    }
                    
                    i++;
                }

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
        void LoadReport()
        {
            string selectSql = @"SELECT ProfileID,
Number,
(lm.Name ) as MemberName ,
Street ,
City,
State,
PostCode,
Country ,
StartDate,
EndDate,
IsActive ,
(p.Name) as CustomerName ,
(SELECT SUM(PointsAccumulated) AS TotalPoints 
                                FROM AccumulatedPoints 
                                WHERE CustomerID = lm.ID)  as Total 
FROM LoyaltyMember lm INNER JOIN Profile p ON p.ID = lm.ProfileID ";
            string sqlcon = " WHERE StartDate Between @sdate and @edate ";

            if (rdoActive.Checked)
            {
                sqlcon += " AND IsActive = 1";
            }
            else if (InActive.Checked)
            {
                sqlcon += " AND IsActive = 0";
            }
            else
            {

            }
            selectSql += sqlcon;

            TbRep = new System.Data.DataTable();
            DateTime sdate = sdatePicker.Value;
            DateTime edate = edatePicker.Value;

            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00).ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@sdate", sdate);
            param.Add("@edate", edate);
            CommonClass.runSql(ref TbRep, selectSql, param);

        }
        void FormatGrid()
        {
            dgReport.ColumnCount = 11;
            dgReport.Columns[0].Name = "Number";
            dgReport.Columns[1].Name = "Member Name";
            dgReport.Columns[2].Name = "City";
            dgReport.Columns[3].Name = "State";
            dgReport.Columns[4].Name = "PostCode";
            dgReport.Columns[5].Name = "Country";
            dgReport.Columns[6].Name = "StartDate";
            dgReport.Columns[7].Name = "EndDate";
            dgReport.Columns[8].Name = "Active";
            dgReport.Columns[9].Name = "Customer";
            dgReport.Columns[10].Name = "Points";
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Loyalty Members";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("Number", 100);
            dgPrinter.ColumnWidths.Add("MemberName", 100);
            dgPrinter.ColumnWidths.Add("City", 100);
            dgPrinter.ColumnWidths.Add("PostCode", 90);
            dgPrinter.ColumnWidths.Add("State", 100);
            dgPrinter.ColumnWidths.Add("Country", 100);
            dgPrinter.ColumnWidths.Add("StartDate", 90);
            dgPrinter.ColumnWidths.Add("EndDate", 90);
            dgPrinter.ColumnWidths.Add("Active", 50);
            dgPrinter.ColumnWidths.Add("Customer", 100);
            dgPrinter.ColumnWidths.Add("Points", 50);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
            dgPrinter.PrintPreviewDataGridView(dgReport);
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


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            Reports.ReportParams loyalty = new Reports.ReportParams();
            loyalty.PrtOpt = 1;
            loyalty.ReportName = "LoyaltyMembers.rpt";
            loyalty.Rec.Add(TbRep);
            loyalty.RptTitle = "Loyalty Members";
            loyalty.Params = "compname";
            loyalty.PVals = CommonClass.CompName.Trim();
            CommonClass.ShowReport(loyalty);
            Cursor.Current = Cursors.Default;
        }

        private void btnSortGrid_Click(object sender, EventArgs e)
        {
            sort = (rdoAsc.Checked == true ? " asc" : " desc");
            index = cmbSort.SelectedIndex;
            btnDisplay.PerformClick();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    ws.Cells[4, 1] = "Number";
                    ws.Cells[4, 2] = "Member Name";
                    ws.Cells[4, 3] = "City";
                    ws.Cells[4, 4] = "State";
                    ws.Cells[4, 5] = "Post Code";
                    ws.Cells[4, 6] = "Country";
                    ws.Cells[4, 7] = "Start Date";
                    ws.Cells[4, 8] = "End Date";
                    ws.Cells[4, 9] = "Active";
                    ws.Cells[4, 10] = "Customer Name";
                    ws.Cells[4, 11] = "Points";
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
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i,7] = item.Cells[6].Value.ToString();
                        }
                        if (item.Cells[7].Value != null)
                        {
                            ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                        }
                        if (item.Cells[8].Value != null)
                        {
                            ws.Cells[i, 9] = item.Cells[8].Value.ToString();
                        }
                        if (item.Cells[9].Value != null)
                        {
                            ws.Cells[i, 10] = item.Cells[9].Value.ToString();
                        }
                        if (item.Cells[10].Value != null)
                        {
                            ws.Cells[i, 11] = item.Cells[10].Value.ToString();
                        }
                        //if (item.Cells[11].Value != null)
                        //{
                        //    ws.Cells[i, 12] = item.Cells[11].Value.ToString();
                        //}

                        // ws.Cells[i, 4] = Math.Round(float.Parse(item.Cells[3].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "K3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Loyalty Members";

                    //Style Table
                    cellRange = ws.get_Range("A4", "K4");
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
                    MessageBox.Show("Loyalty Members has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 //   this.Close();
                }
            }
        }

        private void RptLoyaltyMembers_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
