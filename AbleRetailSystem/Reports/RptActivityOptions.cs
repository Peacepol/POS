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
    public partial class RptActivityOptions : Form
    {
        private bool CanView = false;
        System.Data.DataTable TbRep;
        System.Data.DataTable TbGrid;
        private string dtfromstr = "";
        private string dttostr = "";
        private int index = 1;
        private string sort = " asc";
        public RptActivityOptions()
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
            if (dtmTxFrom.Text == ""
                || dtmTxTo.Text == ""
                || cmbVerbosity.Text == "")
            {
                MessageBox.Show("All options must be set");
                return;
            }

            LoadReport();
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {
               
                DateTime dtpfromutc = Convert.ToDateTime(dtmTxFrom.Value.ToString("yyyy-MM-dd") + " 00:00:00").ToUniversalTime();
                DateTime dtptoutc = Convert.ToDateTime(dtmTxTo.Value.ToString("yyyy-MM-dd") + " 23:59:59").ToUniversalTime();

                dtfromstr = dtpfromutc.ToString("yyyy-MM-dd HH:mm:ss");
                dttostr = dtptoutc.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "";

                if (cmbVerbosity.Text == "Summary")
                {
                    sql = @"SELECT  
                                jbs.JobCode, 
                                jbs.JobName, 
                                jrnal.CreditAmount, 
                                jrnal.DebitAmount, 
                                (jrnal.DebitAmount - jrnal.CreditAmount) AS NetActivity 
                            FROM Jobs jbs 
                            INNER JOIN Journal jrnal ON (jbs.JobID = jrnal.JobID)
                            WHERE jrnal.TransactionDate BETWEEN '" + dtfromstr
                            + "' AND '" + dttostr + "'";
                }
                else if (cmbVerbosity.Text == "Detail")
                {
                    sql = @"SELECT 
	                            jbs.JobCode, 
	                            jbs.JobName, 
	                            jrnal.CreditAmount, 
	                            jrnal.DebitAmount,
	                            jrnal.Memo,
	                            jrnal.Type,
	                            jrnal.TransactionNumber,
	                            jrnal.TransactionDate
                            FROM Jobs jbs 
                            INNER JOIN Journal jrnal 
                            ON (jbs.JobID = jrnal.JobID)
                            WHERE jrnal.TransactionDate BETWEEN '" + dtfromstr
                            + "' AND '" + dttostr + "'";
                }

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

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

        private void RptActivityOptions_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            this.cmbVerbosity.SelectedIndex = 0;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            LoadReport();
            string lPrevItem = "";
            string[] RowArray;
            TbGrid = TbRep.Copy();
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = "JobCode " + sort;
                TbGrid = dv.ToTable();
                int rIndex = 0;
                dgReport.Rows.Clear();
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    if (cmbVerbosity.Text == "Summary")
                    {
                        if (lPrevItem != dr["JobCode"].ToString())
                        {
                            RowArray = new string[9];
                            RowArray[0] = dr["JobCode"].ToString();
                            RowArray[1] = dr["JobName"].ToString();
                            RowArray[6] = TotalDebitPerJobName(dr["JobName"].ToString()).ToString("C");
                            RowArray[7] = TotalCreditPerJobName(dr["JobName"].ToString()).ToString("C");
                            RowArray[8] = TotalNetActivity(dr["JobName"].ToString()).ToString("C");
                            dgReport.Rows.Add(RowArray);
                        }
                        lPrevItem = dr["JobCode"].ToString();
                    }
                    else
                    {
                        if (lPrevItem != dr["JobCode"].ToString() && lPrevItem != "")
                        {
                            RowArray = new string[9];
                            RowArray[5] = "TOTAL :";
                            RowArray[7] = TotalCreditAmount(lPrevItem).ToString("C");
                            RowArray[6] = TotalDebitAmount(lPrevItem).ToString("C");

                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                        }
                        if (lPrevItem != dr["JobCode"].ToString())
                        {
                            RowArray = new string[9];
                            RowArray[0] = ">> " + dr["JobName"].ToString() + " - " + dr["JobCode"].ToString();
                            //RowArray[5] = TotalPerSupplier().ToString();
                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                            RowArray = new string[9];
                            RowArray[0] = dr["JobCode"].ToString();
                            RowArray[1] = dr["JobName"].ToString();
                            RowArray[2] = dr["TransactionNumber"].ToString();
                            RowArray[3] = dr["Type"].ToString();
                            RowArray[4] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                            RowArray[5] = dr["Memo"].ToString();
                            if (dr["DebitAmount"].ToString() != "")
                            {
                                RowArray[6] = float.Parse(dr["DebitAmount"].ToString()).ToString("C");
                            }
                            if (dr["CreditAmount"].ToString() != "")
                            {
                                RowArray[7] = float.Parse(dr["CreditAmount"].ToString()).ToString("C");
                            }

                            dgReport.Rows.Add(RowArray);
                        }
                        else
                        {
                            RowArray = new string[9];
                            RowArray[0] = dr["JobCode"].ToString();
                            RowArray[1] = dr["JobName"].ToString();
                            RowArray[2] = dr["TransactionNumber"].ToString();
                            RowArray[3] = dr["Type"].ToString();
                            RowArray[4] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                            RowArray[5] = dr["Memo"].ToString();
                            if (dr["DebitAmount"].ToString() != "")
                            {
                                RowArray[6] = float.Parse(dr["DebitAmount"].ToString()).ToString("C");
                            }
                            if (dr["CreditAmount"].ToString() != "")
                            {
                                RowArray[7] = float.Parse(dr["CreditAmount"].ToString()).ToString("C");
                            }

                            dgReport.Rows.Add(RowArray);
                        }
                        if (TbRep.Rows.Count - 1 == i)
                        {
                            RowArray = new string[9];
                            RowArray[5] = "TOTAL :";
                            RowArray[7] = TotalCreditAmount(dr["JobName"].ToString()).ToString("C");
                            RowArray[6] = TotalDebitAmount(dr["JobName"].ToString()).ToString("C");

                            dgReport.Rows.Add(RowArray);
                            rIndex = dgReport.Rows.Count - 1;
                            dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        }
                        lPrevItem = dr["JobCode"].ToString();
                    }
                }                //  dgReport.Columns["PartNumber"].Visible = false;
                foreach (DataGridViewColumn column in dgReport.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                FormatGrid();
            }
            else
            {
                MessageBox.Show("Contains No Data.", "Report Information");
            }
            Cursor.Current = Cursors.Default;
        }
        public decimal TotalCreditAmount(string SuppName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (SuppName == dr["JobName"].ToString())
                {
                    if (dr["CreditAmount"].ToString() != "")
                    {
                        x += decimal.Parse(dr["CreditAmount"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal TotalDebitAmount(string SuppName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (SuppName == dr["JobName"].ToString())
                {
                    if (dr["DebitAmount"].ToString() != "")
                    {
                        x += decimal.Parse(dr["DebitAmount"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal TotalCreditPerJobName(string SuppName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (SuppName == dr["JobName"].ToString())
                {
                    if (dr["CreditAmount"].ToString() != "")
                    {
                        x += decimal.Parse(dr["CreditAmount"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal TotalDebitPerJobName(string SuppName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (SuppName == dr["JobName"].ToString())
                {
                    if (dr["DebitAmount"].ToString() != "")
                    {
                        x += decimal.Parse(dr["DebitAmount"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal TotalNetActivity(string SuppName)
        {
            decimal x = 0;
            decimal y = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (SuppName == dr["JobName"].ToString())
                {
                    if (dr["DebitAmount"].ToString() != "")
                    {
                        x += decimal.Parse(dr["DebitAmount"].ToString());
                    }
                    if (dr["CreditAmount"].ToString() != "")
                    {
                        y += decimal.Parse(dr["CreditAmount"].ToString());
                    }
                }
            }
            return x - y;
        }
        private void FormatGrid()
        {

            if (cmbVerbosity.Text == "Summary")
            {
                this.dgReport.Columns[2].Visible = false;
                this.dgReport.Columns[3].Visible = false;
                this.dgReport.Columns[4].Visible = false;
                this.dgReport.Columns[5].Visible = false;
                this.dgReport.Columns[0].Visible = true;
                this.dgReport.Columns[1].Visible = true;
                this.dgReport.Columns[6].Visible = true;
                this.dgReport.Columns[7].Visible = true;
                this.dgReport.Columns[8].Visible = true;
            }
            else
            {
                this.dgReport.Columns[2].Visible = true;
                this.dgReport.Columns[3].Visible = true;
                this.dgReport.Columns[4].Visible = true;
                this.dgReport.Columns[5].Visible = true;
                this.dgReport.Columns[8].Visible = false;
                this.dgReport.Columns[0].Visible = true;
                this.dgReport.Columns[1].Visible = true;
                this.dgReport.Columns[6].Visible = true;
                this.dgReport.Columns[7].Visible = true;

            }
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            if (dtmTxFrom.Text == ""
                    || dtmTxTo.Text == ""
                         || cmbVerbosity.Text == "")
            {
                MessageBox.Show("All options must be set");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            Reports.ReportParams activityparams = new Reports.ReportParams();
            activityparams.PrtOpt = 1;
            activityparams.Rec.Add(TbRep);

            if (cmbVerbosity.Text == "Summary")
            {
                activityparams.ReportName = "ActivityS.rpt";
                activityparams.RptTitle = "Activity Summary";
            }
            else if (cmbVerbosity.Text == "Detail")
            {
                activityparams.ReportName = "ActivityD.rpt";
                activityparams.RptTitle = "Activity Detail";
            }

            activityparams.Params = "compname|sdate|edate";
            activityparams.PVals = CommonClass.CompName.Trim() + "|" + dtfromstr + "|" + dttostr;

            CommonClass.ShowReport(activityparams);
            Cursor.Current = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            if (cmbVerbosity.Text == "Summary")
            {
                DGVPrinter dgPrinter = new DGVPrinter();
                dgPrinter.Title = CommonClass.CompName;
                dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
                dgPrinter.SubTitle = "Activity Summary Report";
                dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
                dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                dgPrinter.ColumnWidths.Add("Job Code", 100);
                dgPrinter.ColumnWidths.Add("Job Name", 80);
                dgPrinter.ColumnWidths.Add("Debit Amount", 100);
                dgPrinter.ColumnWidths.Add("Credit Amount", 100);
                dgPrinter.ColumnWidths.Add("Net Activity", 100);
                dgPrinter.PageNumbers = true;
                dgPrinter.PageNumberInHeader = false;
                dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
                dgPrinter.HeaderCellAlignment = StringAlignment.Center;
                dgPrinter.FooterSpacing = 15;
                dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
                dgPrinter.PrintPreviewDataGridView(dgReport);
            }
            else
            {
                DGVPrinter dgPrinter = new DGVPrinter();
                dgPrinter.Title = CommonClass.CompName;
                dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
                dgPrinter.SubTitle = "Activity Details Report";
                dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
                dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                dgPrinter.ColumnWidths.Add("Job Code", 60);
                dgPrinter.ColumnWidths.Add("Job Name", 60);
                dgPrinter.ColumnWidths.Add("Transaction No.", 80);
                dgPrinter.ColumnWidths.Add("Type", 20);
                dgPrinter.ColumnWidths.Add("Transaction Date", 50);
                dgPrinter.ColumnWidths.Add("Memo", 80);
                dgPrinter.ColumnWidths.Add("Debit Amount", 100);
                dgPrinter.ColumnWidths.Add("Credit Amount", 100);
                dgPrinter.PageNumbers = true;
                dgPrinter.PageNumberInHeader = false;
                dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
                dgPrinter.HeaderCellAlignment = StringAlignment.Center;
                dgPrinter.FooterSpacing = 15;
                dgPrinter.printDocument.DefaultPageSettings.Landscape = true;
                dgPrinter.PrintPreviewDataGridView(dgReport);
            }
        }

        private void btnExportExcell_Click(object sender, EventArgs e)
        {
            if (cmbVerbosity.Text == "Summary")
            {
                using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
                {
                    if (sdf.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)app.ActiveSheet;
                        ws.Cells[4, 1] = "Job Code";
                        ws.Cells[4, 2] = "Job Name";
                        ws.Cells[4, 3] = "Debit Amount";
                        ws.Cells[4, 4] = "Credit Amount";
                        ws.Cells[4, 5] = "Net Activity";
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
                            if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                            {
                                ws.Cells[i, 3] = item.Cells[6].Value.ToString();
                            }
                            if (item.Cells[7].Value != null)
                            {
                                ws.Cells[i, 4] = item.Cells[7].Value.ToString();
                            }
                            if (item.Cells[8].Value != null)
                            {
                                ws.Cells[i, 5] = item.Cells[8].Value.ToString();
                            }

                            i++;
                        }
                        Range cellRange = ws.get_Range("A1", "E3");
                        cellRange.Merge(false);
                        cellRange.Interior.Color = System.Drawing.Color.White;
                        cellRange.Font.Color = System.Drawing.Color.Gray;
                        cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        cellRange.Font.Size = 18;
                        ws.Cells[1, 1] = "Activity Summary Reports";

                        //Style Table
                        cellRange = ws.get_Range("A4", "E4");
                        cellRange.Font.Bold = true;
                        cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                        ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                        ws.Columns.AutoFit();

                        wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        app.Quit();
                        MessageBox.Show("Activity Summary Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            else
            {
                using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
                {
                    if (sdf.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)app.ActiveSheet;
                        ws.Cells[4, 1] = "Supplier Name";
                        ws.Cells[4, 2] = "Purchase Number";
                        ws.Cells[4, 3] = "Transaction No,";
                        ws.Cells[4, 4] = "Type";
                        ws.Cells[4, 5] = "Transaction Date";
                        ws.Cells[4, 6] = "Memo";
                        ws.Cells[4, 7] = "Debit Amount";
                        ws.Cells[4, 8] = "Credit Amount";

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
                            if (item.Cells[4].Value != null && item.Cells[4].Value.ToString() != "")
                            {
                                ws.Cells[i, 5] = Convert.ToDateTime(item.Cells[4].Value.ToString()).ToShortDateString();
                            }
                            if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                            {
                                ws.Cells[i, 6] = item.Cells[5].Value.ToString();
                            }
                            if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                            {
                                ws.Cells[i, 7] = item.Cells[6].Value.ToString();
                            }
                            if (item.Cells[7].Value != null && item.Cells[7].Value.ToString() != "")
                            {
                                ws.Cells[i, 8] = item.Cells[7].Value.ToString();
                            }

                            i++;
                        }
                        Range cellRange = ws.get_Range("A1", "H3");
                        cellRange.Merge(false);
                        cellRange.Interior.Color = System.Drawing.Color.White;
                        cellRange.Font.Color = System.Drawing.Color.Gray;
                        cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                        cellRange.Font.Size = 26;
                        ws.Cells[1, 1] = "Activity Details Reports";

                        //Style Table
                        cellRange = ws.get_Range("A4", "H4");
                        cellRange.Font.Bold = true;
                        cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                        ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                        ws.Columns.AutoFit();

                        wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        app.Quit();
                        MessageBox.Show("Activity Details Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
        }
    }
}
