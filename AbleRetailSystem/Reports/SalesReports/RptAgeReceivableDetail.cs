using DGVPrinterHelper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Reports.SalesReports
{
    public partial class RptAgeReceivableDetail : Form
    {
        private System.Data.DataTable TbRep;
        private System.Data.DataTable TbGrid;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection con;
        string selectSql = "";
        private int index = 1;
        private string sort = " asc";
        private bool CanView = false;

        public RptAgeReceivableDetail()
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
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        void LoadReport()
        {
            selectSql = @"Select Name,ProfileIDNumber,c.Phone,tp.Description,
                                TotalDue,
                                SalesNumber,
                                TransactionDate,
                                ActualDueDate,
                                (@edate ) AS StartDate
                                FROM Sales s 
                                INNER JOIN Profile ON s.CustomerID = Profile.ID 
                                LEFT JOIN Contacts c ON c.ContactID = Profile.ID
                                LEFT JOIN TermsOfPayment tp ON tp.TermsOfPaymentID = Profile.TermsOfPayment
                                LEFT JOIN Terms t ON t.TermsID = s.TermsReferenceID
                                WHERE s.SalesType = 'ORDER' OR s.InvoiceStatus = 'Open'";
            if (cmbAegingMethod.Text != "")
            {
                if (cmbAegingMethod.Text == "Number of Days since P.O. date")
                {
                    selectSql += " AND TransactionDate <= @edate";
                }
                else if (cmbAegingMethod.Text == "Days override using Purchase Terms")
                {
                    selectSql += " AND ActualDueDate <= @edate";
                }
            }

            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);

                DateTime edate = edateTimePicker.Value;
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadReport();
            Reports.ReportParams AgeingDetail = new Reports.ReportParams();
            AgeingDetail.PrtOpt = 1;
            AgeingDetail.Rec.Add(TbRep);
            AgeingDetail.ReportName = "AgeReceivableDetail.rpt";
            AgeingDetail.RptTitle = "Age Receivable Detail";
            AgeingDetail.Params = "compname";
            AgeingDetail.PVals = CommonClass.CompName.Trim();
            CommonClass.ShowReport(AgeingDetail);
            Cursor.Current = Cursors.Default;
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public decimal TotalDate1(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["Name"].ToString())
                {
                    if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-30))
                    {
                        x += decimal.Parse(dr["TotalDue"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal TotalDate2(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["Name"].ToString())
                {
                    if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-31) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-60))
                    {
                        x += decimal.Parse(dr["TotalDue"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal TotalDate3(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["Name"].ToString())
                {
                    if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-61) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-90))
                    {
                        x += decimal.Parse(dr["TotalDue"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal TotalDate4(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["Name"].ToString())
                {
                    if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-91))
                    {
                        x += decimal.Parse(dr["TotalDue"].ToString());
                    }
                }
            }
            return x;
        }
        public decimal BalanceDue(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (ItemName == dr["Name"].ToString())
                {
                    x += decimal.Parse(dr["TotalDue"].ToString());
                }
            }
            return x;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgReport.Rows.Clear();
            LoadReport();
            TbGrid = TbRep.Copy();
            string lPrevItem = "";
            DataRow rw;
            string[] RowArray;
            if (TbGrid.Rows.Count > 0)
            {
                DataView dv = TbGrid.DefaultView;
                dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                TbGrid = dv.ToTable();
                int rIndex = 0;
                // dgReport.DataSource = TbGrid;
                for (int i = 0; i < TbGrid.Rows.Count; i++)
                {
                    DataRow dr = TbGrid.Rows[i];
                    if (lPrevItem != dr["Name"].ToString() && lPrevItem != "")
                    {
                        RowArray = new string[9];
                        RowArray[0] = "TOTAL :";
                        RowArray[1] =  BalanceDue(lPrevItem).ToString("C");
                        RowArray[3] = TotalDate1(lPrevItem).ToString("C");
                        RowArray[4] = TotalDate2(lPrevItem).ToString("C");
                        RowArray[5] = TotalDate3(lPrevItem).ToString("C");
                        RowArray[6] = TotalDate4(lPrevItem).ToString("C");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                    }
                    if (lPrevItem != dr["Name"].ToString())
                    {
                        lPrevItem = dr["Name"].ToString();
                        RowArray = new string[9];
                        RowArray[0] =dr["Name"].ToString();
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray[0] = dr["ProfileIDNumber"].ToString();
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray[0] = dr["Phone"].ToString();
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        RowArray[0] = dr["Description"].ToString();
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                        RowArray[0] = dr["SalesNumber"].ToString();
                        RowArray[1] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                        RowArray[2]= float.Parse(dr["TotalDue"].ToString()).ToString("C");
                        if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-30))
                        {
                            RowArray[3] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                            RowArray[4] = float.Parse("0").ToString("C");
                            RowArray[5] = float.Parse("0").ToString("C");
                            RowArray[6] = float.Parse("0").ToString("C");

                        }
                        else if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-31) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-60))
                        {
                            RowArray[3] = float.Parse("0").ToString("C");
                            RowArray[4] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                            RowArray[5] = float.Parse("0").ToString("C");
                            RowArray[6] = float.Parse("0").ToString("C");
                        }
                        else if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-61) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-90))
                        {
                            RowArray[3] = float.Parse("0").ToString("C");
                            RowArray[4] = float.Parse("0").ToString("C");
                            RowArray[5] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                            RowArray[6] = float.Parse("0").ToString("C");
                        }
                        else if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-91))
                        {
                            RowArray[3] = float.Parse("0").ToString("C");
                            RowArray[4] = float.Parse("0").ToString("C");
                            RowArray[5] = float.Parse("0").ToString("C");
                            RowArray[6] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                        }
                        dgReport.Rows.Add(RowArray);
                    }
                    else
                    {
                        RowArray = new string[9];
                        RowArray[0] = dr["SalesNumber"].ToString();
                        RowArray[1] = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToShortDateString();
                        RowArray[2] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                        if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-30))
                        {
                            RowArray[3] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                            RowArray[4] = float.Parse("0").ToString("C");
                            RowArray[5] = float.Parse("0").ToString("C");
                            RowArray[6] = float.Parse("0").ToString("C");

                        }
                        else if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-31) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-60))
                        {
                            RowArray[3] = float.Parse("0").ToString("C");
                            RowArray[4] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                            RowArray[5] = float.Parse("0").ToString("C");
                            RowArray[6] = float.Parse("0").ToString("C");
                        }
                        else if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-61) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-90))
                        {
                            RowArray[3] = float.Parse("0").ToString("C");
                            RowArray[4] = float.Parse("0").ToString("C");
                            RowArray[5] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                            RowArray[6] = float.Parse("0").ToString("C");
                        }
                        else if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-91) )
                        {
                            RowArray[3] = float.Parse("0").ToString("C");
                            RowArray[4] = float.Parse("0").ToString("C");
                            RowArray[5] = float.Parse("0").ToString("C");
                            RowArray[6] = float.Parse(dr["TotalDue"].ToString()).ToString("C");
                        }

                        dgReport.Rows.Add(RowArray);
                    }
                    if (TbRep.Rows.Count - 1 == i)
                    {
                        RowArray = new string[9];
                        RowArray[0] = "TOTAL :";
                        RowArray[2] = BalanceDue(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : BalanceDue(lPrevItem).ToString("C");
                        RowArray[3] = TotalDate1(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C"): TotalDate1(lPrevItem).ToString("C");
                        RowArray[4] = TotalDate2(lPrevItem).ToString()== "0" ? float.Parse("0").ToString("C") : TotalDate2(lPrevItem).ToString("C");
                        RowArray[5] = TotalDate3(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : TotalDate3(lPrevItem).ToString("C");
                        RowArray[6] = TotalDate4(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : TotalDate4(lPrevItem).ToString("C");
                       // RowArray[6] = TotalDate4(lPrevItem).ToString("C");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        //GRANDTOTAL
                        RowArray[0] = "GRAND TOTAL :";
                        RowArray[2] = GrandBalanceDue(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : GrandBalanceDue(lPrevItem).ToString("C");
                        RowArray[3] = GrandTotalDate1(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : GrandTotalDate1(lPrevItem).ToString("C");
                        RowArray[4] = GrandTotalDate2(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : GrandTotalDate2(lPrevItem).ToString("C");
                        RowArray[5] = GrandTotalDate3(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : GrandTotalDate3(lPrevItem).ToString("C");
                        RowArray[6] = GrandTotalDate4(lPrevItem).ToString() == "0" ? float.Parse("0").ToString("C") : GrandTotalDate4(lPrevItem).ToString("C");
                        // RowArray[6] = TotalDate4(lPrevItem).ToString("C");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                        //AGING PERCENT
                        decimal d = 0;
                        RowArray = new string[9];
                        RowArray[0] = "AGING PERCENT :";
                        if (GrandTotalDate1(lPrevItem) != 0)
                        {
                            RowArray[3] = (GrandTotalDate1(lPrevItem) / GrandBalanceDue(lPrevItem)).ToString("P");
                        }
                        else
                        {
                            RowArray[3] = d.ToString("P");
                        }

                        if (GrandTotalDate2(lPrevItem) != 0)
                        {
                            RowArray[4] = (GrandTotalDate2(lPrevItem) / GrandBalanceDue(lPrevItem)).ToString("P");
                        }
                        else
                        {
                            RowArray[4] = d.ToString("P");
                        }

                        if (GrandTotalDate3(lPrevItem) != 0)
                        {
                            RowArray[5] = (GrandTotalDate3(lPrevItem)/GrandBalanceDue(lPrevItem)).ToString("P");
                        }
                        else
                        {
                            RowArray[5] = d.ToString("P");
                        }
                        if (GrandTotalDate4(lPrevItem) != 0)
                        {
                            RowArray[6] = (GrandTotalDate4(lPrevItem) / GrandBalanceDue(lPrevItem)).ToString("P");
                        }
                        else
                        {
                            RowArray[6] = d.ToString("P");
                        }

                        // RowArray[6] = TotalDate4(lPrevItem).ToString("C");
                        dgReport.Rows.Add(RowArray);
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);

                    }
                    lPrevItem = dr["Name"].ToString();
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
        public decimal GrandTotalDate1(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-30))
                {
                    x += decimal.Parse(dr["TotalDue"].ToString());
                }

            }
            return x;
        }
        public decimal GrandTotalDate2(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-31) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-60))
                    {
                        x += decimal.Parse(dr["TotalDue"].ToString());
                    }
            }
            return x;
        }
        public decimal GrandTotalDate3(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-61) && Convert.ToDateTime(dr["TransactionDate"]) >= Convert.ToDateTime(dr["StartDate"]).AddDays(-90))
                {
                    x += decimal.Parse(dr["TotalDue"].ToString());
                }
            }
            return x;
        }
        public decimal GrandTotalDate4(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                if (Convert.ToDateTime(dr["TransactionDate"]) <= Convert.ToDateTime(dr["StartDate"]).AddDays(-91))
                {
                    x += decimal.Parse(dr["TotalDue"].ToString());
                }
            }
            return x;
        }
        public decimal GrandBalanceDue(string ItemName)
        {
            decimal x = 0;
            foreach (DataRow dr in TbRep.Rows)
            {
                x += decimal.Parse(dr["TotalDue"].ToString());
            }
            return x;
        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Ageing Receivable Detail";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("Snum", 110);
            dgPrinter.ColumnWidths.Add("TotalDue", 110);
            dgPrinter.ColumnWidths.Add("Date1", 110);
            dgPrinter.ColumnWidths.Add("Date2", 110);
            dgPrinter.ColumnWidths.Add("Date3", 110);
            dgPrinter.ColumnWidths.Add("Date4", 110);
            dgPrinter.ColumnWidths.Add("Date", 70);
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

        private void btnExportExcell_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    ws.Cells[4, 1] = "Sales Number";
                    ws.Cells[4, 2] = "Date";
                    ws.Cells[4, 3] = "Total Due";
                    ws.Cells[4, 4] = "0 - 30";
                    ws.Cells[4, 5] = "31 - 60";
                    ws.Cells[4, 6] = "61 - 90";
                    ws.Cells[4, 7] = "91+";
                    int i = 5;
                    foreach (DataGridViewRow item in dgReport.Rows)
                    {

                        if (item.Cells[0].Value != null)
                        {
                            ws.Cells[i, 1] = item.Cells[0].Value.ToString();
                        }
                        if (item.Cells[1].Value != null && item.Cells[1].Value.ToString() != "")
                        {
                            ws.Cells[i, 2] = Convert.ToDateTime(item.Cells[1].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[2].Value != null && item.Cells[2].Value.ToString() != "")
                        {
                            ws.Cells[i, 3] = item.Cells[2].Value.ToString();//), 2).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                        {
                            ws.Cells[i, 4] = item.Cells[3].Value.ToString();//Math.Round(float.Parse(//), 2).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[4].Value != null && item.Cells[4].Value.ToString() != "")
                        {
                            ws.Cells[i, 5] = item.Cells[4].Value.ToString();//Math.Round(float.Parse(//), 2).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[5].Value != null && item.Cells[5].Value.ToString() != "")
                        {
                            ws.Cells[i, 6] = item.Cells[5].Value.ToString();// Math.Round(float.Parse(), 2).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[6].Value != null && item.Cells[6].Value.ToString() != "")
                        {
                            ws.Cells[i, 7] = item.Cells[6].Value.ToString();//Math.Round(float.Parse(), 2).ToString("C", System.Globalization.CultureInfo.CurrentCulture);
                        }
                      
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "G3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = "Age Receivable Details Report";

                    //Style Table
                    cellRange = ws.get_Range("A4", "G4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("D4").EntireColumn.NumberFormat = "$#,##0.00";
                    //ws.get_Range("E4").EntireColumn.NumberFormat = "$#,##0.00";
                    //ws.get_Range("F4").EntireColumn.NumberFormat = "$#,##0.00";
                    //ws.get_Range("G4").EntireColumn.NumberFormat = "$#,##0.00";
                    Range cr0 = ws.get_Range("C4", "C" + (i - 2));
                    cr0.NumberFormat = "$#,##0.00";
                    Range cr = ws.get_Range("D4", "D" + (i-2));
                    cr.NumberFormat= "$#,##0.00";
                    Range cr1 = ws.get_Range("E4", "E" + (i-2));
                    cr1.NumberFormat = "$#,##0.00";
                    Range cr2 = ws.get_Range("F4", "F" + (i - 2));
                    cr2.NumberFormat = "$#,##0.00";
                    Range cr3 = ws.get_Range("G4", "G" + (i - 2));
                    cr3.NumberFormat = "$#,##0.00";
                    //ws.get_Range("D"+(i-1)).EntireColumn.NumberFormat = "###,##%";
                    //    ws.get_Range("E"+ (i - 1)).EntireColumn.NumberFormat = "###,##%";
                    //    ws.get_Range("F"+ (i - 1)).EntireColumn.NumberFormat = "###,##%";
                    //    ws.get_Range("G"+ (i - 1)).EntireColumn.NumberFormat = "##,##%";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Age Receivable Details Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

        }

        private void RptAgeReceivableDetail_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
