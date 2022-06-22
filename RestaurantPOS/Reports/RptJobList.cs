using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;


namespace AbleRetailPOS
{
    public partial class RptJobList : Form
    {
        private static DataTable TbRep;
        private static DataTable TbSub;
        private bool CanView = false;
        public RptJobList()
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

        private void RptJobList_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            LoadJobs();
            LoadCustomers();
        }

        private void LoadJobs()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string sql = "SELECT CAST('true' AS bit) AS Include, JobCode, JobName,JobID FROM Jobs WHERE IsHeader = 'D' ORDER BY JobCode";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                this.dgJobs.DataSource = dt;
                this.dgJobs.Columns[3].Visible = false;
                this.dgJobs.Columns[1].Frozen = true;
                this.dgJobs.Columns[1].HeaderText = "JobCode";
                this.dgJobs.Columns[2].Frozen = true;
                this.dgJobs.Columns[2].HeaderText = "Job Name";

                con.Close();
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
        private void LoadCustomers()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string sql = "SELECT CAST('true' AS bit) AS Include, p.Name, p.ID FROM Jobs as j Inner Join Profile as p on j.CustomerID = p.ID";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                this.dgCustomers.DataSource = dt;              
                this.dgCustomers.Columns[1].Frozen = true;               
                this.dgCustomers.Columns[2].Visible = false;


                con.Close();
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
            LoadReport();
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {          
                DataTable tbSub1 = new DataTable();            
                string IncJobs = GetIncludedJobs();
                if (IncJobs != "")
                {
                    string IncProfile = GetIncludedProfile();
                    string conCus = (IncProfile == "" ? "" : " and j.CustomerID in (" + IncProfile + ")");

                    string sql = "";
                    sql = @"SELECT j.*,jh.JobCode as headercode, jh.JobName as headername from Jobs as j LEFT JOIN 
                    (SELECT * from Jobs where IsHeader = 'H') as jh on j.ParentJobID = jh.JobID 
                    where j.IsHeader = 'D' and j.JobID in (" + IncJobs + ") " + conCus;

                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    TbRep = new DataTable();
                    da.Fill(TbRep);
                    string lRptFile = "JobList.rpt";

                    Reports.ReportParams acctlistparams = new Reports.ReportParams();
                    acctlistparams.PrtOpt = 1;
                    acctlistparams.ReportName = lRptFile;
                    acctlistparams.Rec.Add(TbRep);
                    acctlistparams.RptTitle = "Job List";
                    acctlistparams.Params = "compname";
                    acctlistparams.PVals = CommonClass.CompName.Trim();

                    CommonClass.ShowReport(acctlistparams);
                }
                else
                {
                    MessageBox.Show("Report contains no data");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private string GetIncludedJobs()
        {
            string retJobs = "";
            foreach (DataGridViewRow dvr in this.dgJobs.Rows)
            {
                if (dvr.Cells["JobID"].Value.ToString() != "")
                {
                    if ((bool)dvr.Cells["Include"].Value)
                    {
                        retJobs += (retJobs != "" ? "," + dvr.Cells["JobID"].Value.ToString() : dvr.Cells["JobID"].Value.ToString());
                    }
                }
            }
            return retJobs;
        }
        private string GetIncludedProfile()
        {
            string retProfile = "";
          
            foreach (DataGridViewRow dvr in this.dgCustomers.Rows)
            {
                if (dvr.Cells["ID"].Value.ToString() != "")
                {
                    if ((bool)dvr.Cells["Include"].Value)
                    {
                        retProfile += (retProfile != "" ? "," + dvr.Cells["ID"].Value.ToString() : dvr.Cells["ID"].Value.ToString());
                    }
                }
            }
            return retProfile;
        }

      
        private void lblSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach(DataGridViewRow dr in this.dgJobs.Rows)
            {
                dr.Cells["Include"].Value = true;
            }
        }

        private void lblUnselect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dr in this.dgJobs.Rows)
            {
                dr.Cells["Include"].Value = false;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dr in this.dgCustomers.Rows)
            {
                dr.Cells["Include"].Value = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dr in this.dgCustomers.Rows)
            {
                dr.Cells["Include"].Value = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
