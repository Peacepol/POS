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

namespace AbleRetailPOS.Reports
{
    public partial class RptProfileListOptions : Form
    {
        private System.Data.DataTable TbRep;
        private int index = 1;
        private string sort = " asc";
        private System.Data.DataTable TbGrid;
        private bool CanView = false;

        public RptProfileListOptions()
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

        private void RptProfileListOptions_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            cmbProfileTypes.SelectedIndex = 0;
            cmbVerbosity.SelectedIndex = 0;
        }


        private void LoadReport()
        {
            string sql = "";
            if (cmbVerbosity.Text == "Detail")
            {
                sql = @"SELECT
	                    Name,
                        ProfileIDNumber,
                        Street,
	                    City,
	                    State,
	                    Postcode,
	                    Country,
	                    Phone,
	                    Fax,
	                    Email,
	                    ContactPerson,
	                    Website,
	                    ShippingMethodID,
	                    ABN,
	                    ABNBranch,
	                    Comments,
	                    Type,
	                    CurrentBalance,
	                    TaxIDNumber,
	                    GSTIDNumber,
	                    prfile.TaxCode,
	                    FreightTaxCode,
	                    MethodOfPaymentID,
	                    TermsOfPayment,
	                    DiscountDays,
	                    DiscountDate,
	                    EarlyPaymentDiscountPercent,
	                    VolumeDiscount,
	                    CreditLimit,
	                    IncomeAccountID,
	                    ExpenseAccountID,
                        SellingNotes,
                        SupplierNotes,
	                    pmt.PaymentMethod
                    FROM Profile prfile
                    LEFT JOIN Contacts c ON c.Location = prfile.LocationID
                    LEFT JOIN ShippingMethods shp ON (shp.ShippingMethod = prfile.ShippingMethodID)
                    LEFT JOIN PaymentMethods pmt ON (pmt.ID = prfile.MethodOfPaymentID)
                    WHERE c.ProfileID = prfile.ID";

                if (cmbProfileTypes.Text != "All")
                    sql += " AND Type = '" + cmbProfileTypes.Text + "'";
            }
            else if (cmbVerbosity.Text == "Summary")
            {
                sql = @"SELECT 
                            Name, 
                            p.Type, 
                            p.ProfileIDNumber, 
                            p.CurrentBalance, 
							c.Phone
                        FROM Profile p
                        LEFT JOIN Contacts c ON c.Location = p.LocationID
						WHERE c.ProfileID = p.ID";

                if (cmbProfileTypes.Text != "All")
                    sql += " AND Type = '" + cmbProfileTypes.Text + "'";
            }

            SqlConnection con = null;
            try
            {
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cmbProfileTypes.Text == ""
                || cmbVerbosity.Text == "")
            {
                MessageBox.Show("Profile Type and Verbosity Cannot be empty");
                return;
            }

            LoadReport();
            Reports.ReportParams profilelistparams = new Reports.ReportParams();
            profilelistparams.PrtOpt = 1;
            profilelistparams.Rec.Add(TbRep);
            if (cmbVerbosity.Text == "Detail")
            {
                profilelistparams.ReportName = "ProfileListD.rpt";
                profilelistparams.RptTitle = "Profile List Detail";
            }
            else if (cmbVerbosity.Text == "Summary")
            {
                profilelistparams.ReportName = "ProfileListS.rpt";
                profilelistparams.RptTitle = "Profile List Summary";
            }

            profilelistparams.Params = "compname";
            profilelistparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(profilelistparams);
            Cursor.Current = Cursors.Default;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cmbProfileTypes.Text == ""
                || cmbVerbosity.Text == "")
            {
                MessageBox.Show("Profile Type and Verbosity Cannot be empty");
                return;
            }
            dgReport.Rows.Clear();
            LoadReport();

            TbGrid = TbRep.Copy();
            string lPrevItem = "";
            DataRow rw;
            string[] RowArray;
            int rIndex;
            if (TbGrid.Rows.Count > 0)
            {
                FormatGrid();
                if (cmbVerbosity.Text == "Detail")
                {
                    DataView dv = TbGrid.DefaultView;
                    dv.Sort = TbGrid.Columns[index].ColumnName + " " + sort;
                    TbGrid = dv.ToTable();
                    foreach (DataRow dr in TbGrid.Rows)
                    {
                        RowArray = new string[5];
                        RowArray[0] = "Name :";
                        RowArray[1] = dr["Name"].ToString();
                        RowArray[2] ="Type";
                        RowArray[3] = dr["Type"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Profile ID :";
                        RowArray[1] = dr["ProfileIDNumber"].ToString();
                        RowArray[2] = "Balance:";
                        RowArray[3] = float.Parse(dr["CurrentBalance"].ToString()).ToString("C2");
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Street :";
                        RowArray[1] = dr["Street"].ToString();
                        RowArray[2] = "Tax ID :";
                        RowArray[3] = dr["TaxIDNumber"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "City :";
                        RowArray[1] = dr["City"].ToString();
                        RowArray[2] = "GST ID :";
                        RowArray[3] = dr["GSTIDNumber"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Country:";
                        RowArray[1] = dr["Country"].ToString();
                        RowArray[2] = "Freight Tax Code:";
                        RowArray[3] = dr["FreightTaxCode"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Phone:";
                        RowArray[1] = dr["Phone"].ToString();
                        RowArray[2] = "Payment Method:";
                        RowArray[3] = dr["PaymentMethod"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Fax :";
                        RowArray[1] = dr["Fax"].ToString();
                        RowArray[2] = "Terms:";
                        RowArray[3] = dr["TermsOfPayment"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Email:";
                        RowArray[1] = dr["Email"].ToString();
                        RowArray[2] = "Discount Days/Date:";
                        RowArray[3] = dr["DiscountDays"].ToString() +" / " + dr["DiscountDate"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Website:";
                        RowArray[1] = dr["Website"].ToString();
                        RowArray[2] = "Early Payment Discount:";
                        RowArray[3] = decimal.Parse(dr["EarlyPaymentDiscountPercent"].ToString()).ToString("P");
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Contact:";
                        RowArray[1] = dr["ContactPerson"].ToString();
                        RowArray[2] = "Volume Discount:";
                        RowArray[3] = decimal.Parse(dr["VolumeDiscount"].ToString()).ToString("P");
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Shipping Method:";
                        RowArray[1] = dr["ShippingMethodID"].ToString();
                        RowArray[2] = "Credit Limit:";
                        RowArray[3] = float.Parse(dr["CreditLimit"].ToString()).ToString("C2");
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "ABN:";
                        RowArray[1] = dr["ABN"].ToString();
                        RowArray[2] = "Income Account No:";
                        RowArray[3] = dr["IncomeAccountID"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "ABN Branch:";
                        RowArray[1] = dr["ABNBranch"].ToString();
                        RowArray[2] = "Expense Account No:";
                        RowArray[3] = dr["ExpenseAccountID"].ToString();
                        dgReport.Rows.Add(RowArray);

                        RowArray[0] = "Note:";
                        RowArray[1] = dr["SupplierNotes"].ToString() +"" + dr["SellingNotes"].ToString();
                        RowArray[2] = "";
                        RowArray[3] = "";
                        dgReport.Rows.Add(RowArray);

                        dgReport.Rows.Add();
                        rIndex = dgReport.Rows.Count - 1;
                        dgReport.Rows[rIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgReport.Rows[rIndex].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    }
                    dgReport.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    dgReport.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font(dgReport.Font, FontStyle.Bold);
                    this.dgReport.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgReport.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (cmbVerbosity.Text == "Summary")
                {
                    foreach (DataRow dr in TbGrid.Rows)
                    {
                        RowArray = new string[5];
                        RowArray[0] = dr["Name"].ToString();
                        RowArray[1] = dr["Phone"].ToString();
                        RowArray[2] = dr["Type"].ToString();
                        RowArray[3] = float.Parse(dr["CurrentBalance"].ToString()).ToString("C2");
                        RowArray[4] = dr["ProfileIDNumber"].ToString();
                        dgReport.Rows.Add(RowArray);
                    }
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
        void FormatGrid()
        {
            if (cmbVerbosity.Text == "Detail")
            {
                dgReport.ColumnCount = 4;
                dgReport.Columns[0].Name = "";
                dgReport.Columns[1].Name = "";
                dgReport.Columns[2].Name = "";
                dgReport.Columns[3].Name = "";
                dgReport.Columns[0].Width = 100;
                dgReport.Columns[2].Width = 150;
            }
            else if (cmbVerbosity.Text == "Summary")
            {
                dgReport.ColumnCount = 5;
                dgReport.Columns[0].Name = "Name";
                dgReport.Columns[1].Name = "Phone";
                dgReport.Columns[2].Name = "Type";
                dgReport.Columns[3].Name = "Current Balance";
                dgReport.Columns[4].Name = "Identifier";
                dgReport.Columns[0].Width = 150;
                dgReport.Columns[1].Width = 100;
                dgReport.Columns[2].Width = 100;
                dgReport.Columns[4].Width = 100;
            }

        }

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();
            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "Profile List Report";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("Name",150);
            dgPrinter.ColumnWidths.Add("Current Balance", 200);
            //dgPrinter.ColumnWidths.Add("CustomerPONumber", 100);
            //dgPrinter.ColumnWidths.Add("Name", 100);
            // dgPrinter.ColumnWidths.Add("ClosedDate", 100);
            //   dgPrinter.ColumnWidths.Add("TotalDue", 100);
            // dgPrinter.ColumnWidths.Add("GrandTotal", 100);
            //dgPrinter.ColumnWidths.Add("PDate", 100);
            //dgPrinter.ColumnWidths.Add("Available", 70);
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            dgPrinter.HeaderCellAlignment = StringAlignment.Center;
            dgPrinter.FooterSpacing = 15;
            // dgPrinter.printDocument.DefaultPageSettings.Landscape = false;
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
                    if (cmbVerbosity.Text == "Detail")
                    {
                        ws.Cells[4, 1] = "   ";
                        ws.Cells[4, 2] = "          ";
                        ws.Cells[4, 3] = "   ";
                        ws.Cells[4, 4] = "          ";
                    }
                    else if(cmbVerbosity.Text == "Summary")
                    {
                        ws.Cells[4, 1] = "Name";
                        ws.Cells[4, 2] = "Phone";
                        ws.Cells[4, 3] = "Type";
                        ws.Cells[4, 4] = "Current Balance";
                        ws.Cells[4, 5] = "Identifier";
                    }
                   
                    
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
                        if (cmbVerbosity.Text == "Summary")
                        {
                            if (item.Cells[4].Value != null)
                            {
                                ws.Cells[i, 5] = item.Cells[4].Value.ToString();
                            }
                        }
                        // ws.Cells[i, 4] = Math.Round(float.Parse(item.Cells[3].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        i++;
                    }
                    Range cellRange = ws.get_Range("A1", "E3");
              
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    if (cmbVerbosity.Text == "Summary")
                    {
                        ws.Cells[1, 1] = "Profile List Summary";
                       
                    }
                    else
                    {
                        ws.Cells[1, 1] = "Profile List Detail";
                      
                    }
                    //Style Table
                    cellRange = ws.get_Range("A4", "E4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //ws.get_Range("A5").EntireColumn.NumberFormat = "0";
                    if (cmbVerbosity.Text == "Summary")
                    {
                        ws.get_Range("D4").EntireColumn.NumberFormat = "$#,##0.00";
                    }
                    //ws.get_Range("G4").EntireColumn.NumberFormat = "$#,##0.00";
                    //ws.get_Range("H4").EntireColumn.NumberFormat = "$#,##0.00";
                    //ws.get_Range("I4").EntireColumn.NumberFormat = "$#,##0.00";
                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Loyalty Members has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //   this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
