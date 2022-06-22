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

namespace AbleRetailPOS.Utilities
{
    public partial class AuditTrail : Form
    {
       
        private DataTable dt;
        SqlConnection con = new SqlConnection(CommonClass.ConStr);
        private DataTable TbRep;
        SqlCommand cmd;
        SqlDataAdapter da;
        private string selectSql = "";
        public AuditTrail()
        {
            InitializeComponent();
        }
        
        public void Populatedg(ref DataGridView dgpopulate)
        {
            dgAudit.Rows.Clear();
            SqlConnection con_ = new SqlConnection(CommonClass.ConStr);
             selectSql = @" SELECT s.*, u.user_fullname, f.form_name , u.user_name
                                    FROM SystemAuditTrail s
                                    INNER JOIN Users u ON u.user_id = s.UserID
                                    LEFT JOIN Forms f ON f.form_code = s.FormCode
                                    WHERE AuditDate BETWEEN @sdate and @edate ";
            try
            {
                if ( this.txtUserID.Text != "All")
                {
                   selectSql += " AND u.user_id = " + txtUserID.Text;
                }
                if (this.txtForm.Text != "All" )
                {
                    selectSql += " AND f.form_code = '" + txtForm.Text +"'";
                }

                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                cmd_.Parameters.Clear();
                cmd_.CommandType = CommandType.Text;
                con_.Open();
                DateTime sdate = sdateTimePicker.Value.ToUniversalTime();
                DateTime edate = edateTimePicker.Value.ToUniversalTime();
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd_.Parameters.AddWithValue("@sdate", sdate);
                cmd_.Parameters.AddWithValue("@edate", edate);

                SqlDataAdapter da = new SqlDataAdapter();
                dt = new DataTable();
                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgpopulate.Rows.Add();
                    dgpopulate.Rows[x].Cells[0].Value = dr["user_name"].ToString();
                    dgpopulate.Rows[x].Cells[1].Value = dr["user_fullname"].ToString();
                    dgpopulate.Rows[x].Cells[2].Value = Convert.ToDateTime(dr["AuditDate"].ToString()).ToLocalTime().ToShortDateString();
                    dgpopulate.Rows[x].Cells[3].Value = dr["form_name"].ToString();
                    dgpopulate.Rows[x].Cells[4].Value = dr["AuditAction"].ToString();
                    dgpopulate.Rows[x].Cells[5].Value = dr["AffectedRecordID"].ToString();
                    dgpopulate.Rows[x].Cells[6].Value = dr["OldData"].ToString();
                    dgpopulate.Rows[x].Cells[7].Value = dr["NewData"].ToString();
                    dgpopulate.Rows[x].Cells[8].Value = dr["LocationID"].ToString();
                }
                dt.Clear();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

            private void dgAudit_Load(object sender, EventArgs e)
        {
            Populatedg(ref dgAudit);
        }

       void ShowUser()
        {
            UserLookup userLookup = new UserLookup();
            if (userLookup.ShowDialog() == DialogResult.OK)
            {
                string[] lUser = userLookup.GetUserDetail;
                txtUserID.Text = lUser[0].ToString();
            }
            else
            {
                // MessageBox.Show("Dialog not OK");
            }
            Populatedg(ref dgAudit);
        }
        void ShowForm()
        {
            FormLookup formLookup = new FormLookup();
            if (formLookup.ShowDialog() == DialogResult.OK)
            {
                string[] lUser = formLookup.GetFormDetail;
                txtForm.Text = lUser[0].ToString();
            }
            else
            {
                // MessageBox.Show("Dialog not OK");
            }
            Populatedg(ref dgAudit);
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowUser();
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            if (this.txtUserID.Text != "")
            {
                Populatedg(ref dgAudit);
            }
        }

        private void sdateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Populatedg(ref dgAudit);
        }

        private void edateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Populatedg(ref dgAudit);
        }

        private void pbForm_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        void LoadReport()
        {
            try
            {
                
                SqlConnection con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);
                //con.Open();
                DateTime sdate = sdateTimePicker.Value.ToUniversalTime();
                DateTime edate = edateTimePicker.Value.ToUniversalTime();
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@sdate", sdate);
                cmd.Parameters.AddWithValue("@edate", edate);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);
                Reports.ReportParams Audit = new Reports.ReportParams();
                Audit.PrtOpt = 1;
                Audit.Rec.Add(TbRep);
                Audit.ReportName = "SystemAuditTrail.rpt";
                Audit.RptTitle = "Audit Trail";
                Audit.Params = "compname";
                Audit.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(Audit);
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

        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            DGVPrinter dgPrinter = new DGVPrinter();

            dgPrinter.Title = CommonClass.CompName;
            dgPrinter.TitleFont = new System.Drawing.Font("Tahoma", (float)12.5);
            dgPrinter.SubTitle = "System Audit Trail";
            dgPrinter.SubTitleFont = new System.Drawing.Font("Tahoma", (float)11);
            dgPrinter.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            dgPrinter.ColumnWidths.Add("User Name", 70);
            dgPrinter.ColumnWidths.Add("Full Name", 70);
            dgPrinter.ColumnWidths.Add("Date", 70);
            dgPrinter.ColumnWidths.Add("Form Name", 60);
            dgPrinter.ColumnWidths.Add("Audit Action", 50);
            dgPrinter.ColumnWidths.Add("Affected Record", 30);
            dgPrinter.ColumnWidths.Add("Old Data", 50);
            dgPrinter.ColumnWidths.Add("New Data", 50);
            dgPrinter.ColumnWidths.Add("Location ID", 30);
            dgPrinter.PageSettings.Landscape = true;
            dgPrinter.PageNumbers = true;
            dgPrinter.PageNumberInHeader = false;
            dgPrinter.ColumnWidth = DGVPrinter.ColumnWidthSetting.DataWidth;
            dgPrinter.HeaderCellAlignment = StringAlignment.Near;
            dgPrinter.FooterSpacing = 15;
            dgPrinter.PrintPreviewDataGridView(dgAudit);
        }
    }
}
