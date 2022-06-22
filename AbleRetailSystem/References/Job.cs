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
using System.Text.RegularExpressions;
using System.Globalization;

namespace RestaurantPOS
{
    public partial class Job : Form
    {
        private bool IsNew = false;
        private static DataTable JTb;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        private DataGridViewRow CurJobRow;

        public Job()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Add", out outx);
                if (outx == true)
                {
                    CanAdd = true;
                }
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                if (outx == true)
                {
                    CanDelete = true;
                }
            }

            string outy = "";
            CommonClass.AppFormCode.TryGetValue(this.Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
        }

        private void Job_Load(object sender, EventArgs e)
        {
            LoadData();
            // FillData();
            //Form check
            btnSave.Enabled = false;
            groupBox1.Enabled = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsNew)
            {
                if (CheckJobCodeExist(this.JobCode.Text))
                {
                    NewRecord();
                }
                else
                {
                    MessageBox.Show("Job Code already exists.");
                }
            }
            else
            {
                UpdateRecord();
            }
            //btnSave.Enabled = false;
            //btnDelete.Enabled = true;
            //btnRefresh.Enabled = true;
        }

        private bool CheckJobCodeExist(string pJobCode)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM Jobs WHERE JobCode = '" + pJobCode + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable ltb = new DataTable();

                da.Fill(ltb);

                if (ltb.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void NewRecord()
        {
            SqlConnection con = null;
            try
            {
                if (JobName.Text == "" || JobCode.Text == "")
                {
                    MessageBox.Show("Job Code and Job Name are required.");
                }
                else
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Jobs (JobCode, JobName, JobDescription, ContactName, Manager, 
                                                    PercentCompleted, StartDate, FinishDate,CustomerID,ParentJobID,IsInactive, IsHeader) 
                                                    VALUES(@JobCode, @JobName, @JobDescription, @ContactName, @Manager, 
                                                    @PercentCompleted, @StartDate, @FinishDate,@CustomerID,@ParentJobID,@IsInactive, @IsHeader)", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@JobCode", JobCode.Text.ToString());
                    cmd.Parameters.AddWithValue("@JobName", JobName.Text.ToString());
                    cmd.Parameters.AddWithValue("@JobDescription", Description.Text.ToString());
                    cmd.Parameters.AddWithValue("@ContactName", Contact.Text.ToString());
                    cmd.Parameters.AddWithValue("@Manager", CustomerName.Text.ToString());
                    cmd.Parameters.AddWithValue("@PercentCompleted", Percent.Text);
                    cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.ToUniversalTime().ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@FinishDate", dtpEndDate.Value.ToUniversalTime().ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ParentJobID", this.lblParentID.Text.ToString());
                    cmd.Parameters.AddWithValue("@CustomerID", this.lblCustomerID.Text.ToString());
                    cmd.Parameters.AddWithValue("@IsInactive", (chkInactive.Checked ? true : false));
                    cmd.Parameters.AddWithValue("@IsHeader", (rdoDetail.Checked ? "D" : "H"));

                    con.Open();

                    int i = cmd.ExecuteNonQuery();

                    if (i != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Job " + JobCode.Text.ToString());
                        string titles = "INFORMATION";
                        MessageBox.Show("Job Record has been created.", titles);
                        IsNew = false;
                        LoadData(JobCode.Text);
                        //FillData();
                        btnRefresh.PerformClick();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void UpdateRecord()
        {
            SqlConnection con = null;
            try
            {
                if (JobName.Text == "")
                {
                    MessageBox.Show("Job Name is required.");
                }
                else
                {
                    string titles = "Update Job Record";
                    DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        con = new SqlConnection(CommonClass.ConStr);
                        SqlCommand cmd = new SqlCommand(@"UPDATE Jobs SET JobName = @JobName, JobDescription = @JobDescription,
                                                        ContactName = @ContactName, Manager = @Manager, PercentCompleted = @PercentCompleted, 
                                                        StartDate = @StartDate, FinishDate = @FinishDate, CustomerID = @CustomerID,
                                                        ParentJobID = @ParentJobID,IsInactive = @IsInactive, IsHeader = @IsHeader 
                                                        WHERE JobID = '" + lblJobID.Text + "'", con);
                        cmd.CommandType = CommandType.Text;
                      
                        cmd.Parameters.AddWithValue("@JobName", JobName.Text.ToString());
                        cmd.Parameters.AddWithValue("@JobDescription", Description.Text.ToString());
                        cmd.Parameters.AddWithValue("@ContactName", Contact.Text.ToString());
                        cmd.Parameters.AddWithValue("@Manager", CustomerName.Text.ToString());
                        cmd.Parameters.AddWithValue("@PercentCompleted", Percent.Text);
                        cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.ToUniversalTime().ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@FinishDate", dtpEndDate.Value.ToUniversalTime().ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@ParentJobID", this.lblParentID.Text.ToString());
                        cmd.Parameters.AddWithValue("@CustomerID", this.lblCustomerID.Text.ToString());
                        cmd.Parameters.AddWithValue("@IsInactive", (chkInactive.Checked ? true : false));
                        cmd.Parameters.AddWithValue("@IsHeader", (rdoDetail.Checked ? "D" : "H"));
                        con.Open();

                        int i = cmd.ExecuteNonQuery();

                        if (i != 0)
                        {
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Edited Job Number " + JobCode.Text, lblJobID.Text);
                            MessageBox.Show("Job Record has been updated.", "INFORMATION");
                            LoadData(JobCode.Text);
                            //FillData();
                            btnRefresh.PerformClick();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                if (lblJobID.Text != "")
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd_ = new SqlCommand(@"SELECT * FROM Jobs j
                                                        LEFT JOIN SalesLines s ON s.JobID = j.JobID 
                                                        LEFT JOIN PurchaseLines p ON p.JobID = j.JobID 
                                                        WHERE j.JobID = '" + lblJobID.Text + "'", con);
                    con.Open();
                    int x = cmd_.ExecuteNonQuery();
                    if (x < 0)// if zero means no record
                    {
                        string titles = "Delete Job Record";
                        DialogResult dialogResult = MessageBox.Show("Do you wish to continue deleting job? (yes/no)", titles, MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            SqlCommand cmd = new SqlCommand(@"DELETE FROM Jobs WHERE JobCode = '" + JobCode.Text + "'", con);

                            int i = cmd.ExecuteNonQuery();
                            if (i != 0)
                            {
                                MessageBox.Show("Job Record has been deleted.", "INFORMATION");
                            }
                        }
                    }
                    else//has record
                    {
                        MessageBox.Show("Job Record cannot be deleted. There is a transaction that use this job. ", "INFORMATION");

                    }
                }
                else
                {
                    MessageBox.Show("Please select job to delete. ", "INFORMATION");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            btnRefresh.PerformClick();
        }

        private void LoadData(string pSelected = "")
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT j.*, p.Name FROM Jobs j LEFT JOIN Profile p ON j.customerid = p.id";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                JTb = new DataTable();
                da.Fill(JTb);
                dgJobs.DataSource = JTb;
                dgJobs.Columns[0].HeaderText = "Job ID";
                dgJobs.Columns[1].HeaderText = "Job Code";
                dgJobs.Columns[2].HeaderText = "Job Name";
                dgJobs.Columns[3].Visible = false;
                dgJobs.Columns[4].Visible = false;
                dgJobs.Columns[5].Visible = false;
                dgJobs.Columns[6].Visible = false;
                dgJobs.Columns[7].Visible = false;
                dgJobs.Columns[8].Visible = false;
                dgJobs.Columns[9].Visible = false;
                dgJobs.Columns[10].Visible = false;
                dgJobs.Columns[11].Visible = false; // Income
                dgJobs.Columns[12].Visible = false; //Cost
                dgJobs.Columns[13].Visible = false;//Expenses
                dgJobs.Columns[14].Visible = false;
                dgJobs.Columns[15].Visible = false;
                dgJobs.Columns[16].Visible = false;
                dgJobs.Columns[17].Visible = true;//Customer Name

                for (int i = 0; i < dgJobs.Rows.Count; i++)
                {
                    if (dgJobs.Rows[i].Cells["JobCode"].Value.ToString() == pSelected)
                    {
                        dgJobs.FirstDisplayedScrollingRowIndex = i;
                        dgJobs.CurrentCell = dgJobs.Rows[i].Cells[0];
                        break;
                    }
                }
                if (pSelected == "" && dgJobs.Rows.Count > 0)
                {
                    dgJobs.FirstDisplayedScrollingRowIndex = 0;
                    dgJobs.CurrentCell = dgJobs.Rows[0].Cells[0];
                }
                if (dgJobs.Rows.Count > 0)
                {
                    btnDelete.Enabled = CanDelete;
                   btnEdit.Enabled = CanEdit;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
                btnAddNew.Enabled = CanAdd;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            //FillData();
            groupBox1.Enabled = false;
            JobCode.ReadOnly = false;
            JobCode.Text = "";
            JobName.Text = "";
            Contact.Text = "";
            CustomerName.Text = "";
            Description.Text = "";
            Manager.Text = "";
            dtpEndDate.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now;
            Percent.Value = 0;
            lblJobID.Text = "";
            lblCustomerID.Text = "";
            lblParentID.Text = "";
            ParentJob.Text = "";
            rdoDetail.Checked = true;
            chkInactive.Checked = false;
            //Buttons 
            if (dgJobs.Rows.Count > 0)
            {
                btnSave.Enabled = false;
                btnAddNew.Enabled = CanAdd;
                btnDelete.Enabled = CanDelete;
                btnEdit.Enabled = CanEdit;
            }
            else
            {
                btnSave.Enabled = false;
                btnAddNew.Enabled = CanAdd;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
            
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            IsNew = true;
            groupBox1.Enabled = true;
            JobCode.ReadOnly = false;
            JobCode.Text = "";
            JobName.Text = "";
            Contact.Text = "";
            CustomerName.Text = "";
            Description.Text = "";
            Manager.Text = "";
            dtpEndDate.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now;
            Percent.Value = 0;
            lblJobID.Text = "";
            lblCustomerID.Text = "";
            lblParentID.Text = "";
            ParentJob.Text = "";
            rdoDetail.Checked = true;
            chkInactive.Checked = false;
            //Buttons 
            btnSave.Enabled = true;
            btnAddNew.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
        }
   
        private static bool EnableDelete(string pJobID)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM Journal WHERE JobID = '" + pJobID + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable ltb = new DataTable();

                da.Fill(ltb);

                if (ltb.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void dgJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillData();
            btnEdit.PerformClick();
            //form checks
        }

        private void FillData()
        {
            if(dgJobs.Rows.Count > 0)
            {
                CurJobRow = dgJobs.CurrentRow;
                lblJobID.Text = CurJobRow.Cells["JobID"].Value.ToString();
                JobCode.Text = CurJobRow.Cells["JobCode"].Value.ToString();
                JobName.Text = CurJobRow.Cells["JobName"].Value.ToString();
                Contact.Text = CurJobRow.Cells["ContactName"].Value.ToString();
                Description.Text = CurJobRow.Cells["JobDescription"].Value.ToString();

                Percent.Value = (CurJobRow.Cells["PercentCompleted"].Value == null | CurJobRow.Cells["PercentCompleted"].Value == DBNull.Value ? 0 : Convert.ToDecimal(CurJobRow.Cells["PercentCompleted"].Value));
                DateTime lSDate = (CurJobRow.Cells["StartDate"].Value == null | CurJobRow.Cells["StartDate"].Value == DBNull.Value ? dtpStartDate.MinDate : Convert.ToDateTime(CurJobRow.Cells["StartDate"].Value.ToString()).ToLocalTime());
                dtpStartDate.Value = lSDate;
                DateTime lEDate = (CurJobRow.Cells["FinishDate"].Value == null | CurJobRow.Cells["FinishDate"].Value == DBNull.Value ? dtpEndDate.MinDate : Convert.ToDateTime(CurJobRow.Cells["FinishDate"].Value.ToString()).ToLocalTime());
                dtpEndDate.Value = lEDate;
                Manager.Text = CurJobRow.Cells["Manager"].Value.ToString();
                lblCustomerID.Text = CurJobRow.Cells["CustomerID"].Value.ToString();
                CustomerName.Text = CurJobRow.Cells["Name"].Value.ToString();
                if (CanDelete)
                {
                    btnDelete.Enabled = EnableDelete(CurJobRow.Cells["JobID"].Value.ToString());
                }
                this.chkInactive.Checked = (bool)CurJobRow.Cells["IsInactive"].Value;
                if(CurJobRow.Cells["IsHeader"].Value.ToString() == "D")
                {
                    this.rdoDetail.Checked = true;
                }
                else
                {
                    this.rdoHeader.Checked = true;
                }
                this.lblParentID.Text = CurJobRow.Cells["ParentJobID"].Value.ToString();
                if (CurJobRow.Cells["ParentJobID"].Value.ToString() != "")
                {
                    FillParentID(CurJobRow.Cells["ParentJobID"].Value.ToString());
                }
                else
                {
                    this.ParentJob.Text = "";
                    this.lblParentID.Text = "";
                }
                JobCode.ReadOnly = true;
            }           
        }

        private void FillParentID(string pID)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT JobID,JobCode,JobName from Jobs where JobID = " + pID;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt;
                dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    this.ParentJob.Text = dt.Rows[0]["JobCode"].ToString() + " - " + dt.Rows[0]["JobName"].ToString();
                }
                else
                {
                    this.ParentJob.Text = "";
                    this.lblParentID.Text = "";
                }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectJobs DlgJob = new SelectJobs("H");

            if (DlgJob.ShowDialog() == DialogResult.OK)
            {
                string[] Job = DlgJob.GetJob;
             
                this.ParentJob.Text = Job[1] + " - " + Job[2];
                this.lblParentID.Text = Job[0];
            }
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup();
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                this.lblCustomerID.Text = lProfile[0];
                this.CustomerName.Text = lProfile[1];           
            }        
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnAddNew.Enabled = false;
            
            btnSave.Enabled = true;
            IsNew = false;
            groupBox1.Enabled = true;
        }
    }
}
