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
//using Subro.Controls;

namespace RestaurantPOS
{
    public partial class UserMaintenance : Form
    {
        private DataRow SelUser;
        private DataRow SelUserProfile = null;
        private bool UserAdd = false;
        private bool CanEdit = false;
        private bool CanAdd = false;
        private bool CanDelete = false;
        private DateTime DateToday = new DateTime();


        public UserMaintenance()
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
        }

        private void UserMaintenance_Load(object sender, EventArgs e)
        {
            btnAddUser.Enabled = CanAdd;
            btnDeleteUser.Enabled = CanDelete;
            btnOkUserProfile.Enabled = false;
            btnSave.Enabled = false;
            btnEdit.Enabled = CanEdit;
            btnOk.Enabled = false;
            textdisable();
            UserAdd = false;
            LoadUser();
            SelUserProfile = null;
            dgvUserAccess.ReadOnly = true;
            dgSpecialAccessRight.ReadOnly = true;
            dgReportRights.ReadOnly = true;
            btnSaveRights.Enabled = false;
        }

        private void dgvUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            LoadUserAccess();
        }
        void LoadUserAccess()
        {
            UserAdd = false;
            SelUser = (dgvUserList.CurrentRow.DataBoundItem as DataRowView).Row;
            this.lblUser1.Text = SelUser["user_name"].ToString();
            string selectSql = @"SELECT 
                                    ua.form_code
                                    ,frm.category
                                    ,frm.form_name
                                    ,ua.u_view
                                    ,ua.u_add
                                    ,ua.u_edit
                                    ,ua.u_delete
                                 FROM User_Access ua
                                 INNER JOIN Forms frm
                                 ON (ua.form_code = frm.form_code) 
                                 WHERE frm.IsField = '0' AND ua.user_id = " + SelUser["user_id"] + " AND category NOT LIKE '%Reports%' ORDER BY category ASC";

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                DataTable TbUserAccess = new DataTable();
                TbUserAccess.Columns.Add("form_code");
                TbUserAccess.Columns.Add("category");
                TbUserAccess.Columns.Add("form_name");
                TbUserAccess.Columns.Add("u_view", typeof(bool));
                TbUserAccess.Columns.Add("u_add", typeof(bool));
                TbUserAccess.Columns.Add("u_edit", typeof(bool));
                TbUserAccess.Columns.Add("u_delete", typeof(bool));
                da.Fill(TbUserAccess);
                dgvUserAccess.DataSource = TbUserAccess;
                dgvUserAccess.Columns[0].HeaderText = "Form Code";
                dgvUserAccess.Columns[1].HeaderText = "Menu";
                dgvUserAccess.Columns[1].ReadOnly = true;
                dgvUserAccess.Columns[2].HeaderText = "Form Name";
                dgvUserAccess.Columns[2].ReadOnly = true;
                dgvUserAccess.Columns[3].HeaderText = "View";
                dgvUserAccess.Columns[4].HeaderText = "Add";
                dgvUserAccess.Columns[5].HeaderText = "Edit";
                dgvUserAccess.Columns[6].HeaderText = "Delete";

                dgvUserAccess.Columns[0].Visible = false;
                dgvUserAccess.Columns[1].Visible = true;
                dgvUserAccess.Columns[2].Visible = true;
                dgvUserAccess.Columns[3].Visible = true;
                dgvUserAccess.Columns[4].Visible = true;
                dgvUserAccess.Columns[5].Visible = true;
                dgvUserAccess.Columns[6].Visible = true;

                dgvUserAccess.Refresh();

                //var grouper = new Subro.Controls.DataGridViewGrouper(dgvUserAccess);
                //grouper.SetGroupOn("category");
            }
            catch (SqlException  exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            btnEditAccess.Enabled = CanEdit;
        }
        void LoadReportRightAccess()
        {
            UserAdd = false;
            SelUser = (dgUserProfileRights.CurrentRow.DataBoundItem as DataRowView).Row;
            this.lblUser3.Text = SelUser["user_name"].ToString();
            string selectSql = @"SELECT 
                                    ua.form_code
                                    ,frm.category
                                    ,frm.form_name
                                    ,ua.u_view
                                    ,ua.u_add
                                    ,ua.u_edit
                                    ,ua.u_delete
                                 FROM User_Access ua
                                 INNER JOIN Forms frm
                                 ON (ua.form_code = frm.form_code) 
                                 WHERE frm.IsField = '0' AND ua.user_id = " + SelUser["user_id"] + " AND category LIKE '%Reports%' ORDER BY category ASC";
                                

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                DataTable TbUserAccess = new DataTable();
                TbUserAccess.Columns.Add("form_code");
                TbUserAccess.Columns.Add("category");
                TbUserAccess.Columns.Add("form_name");
                TbUserAccess.Columns.Add("u_view", typeof(bool));

                da.Fill(TbUserAccess);
                dgReportRights.DataSource = TbUserAccess;
                dgReportRights.Columns[0].HeaderText = "Form Code";
                dgReportRights.Columns[1].HeaderText = "Menu";
                dgReportRights.Columns[1].ReadOnly = true;
                dgReportRights.Columns[2].HeaderText = "Form Name";
                dgReportRights.Columns[2].ReadOnly = true;
                dgReportRights.Columns[3].HeaderText = "View";

                dgReportRights.Columns[0].Visible = false;
                dgReportRights.Columns[1].Visible = true;
                dgReportRights.Columns[2].Visible = true;
                dgReportRights.Columns[3].Visible = true;


                dgReportRights.Refresh();

                //var grouper = new Subro.Controls.DataGridViewGrouper(dgvUserAccess);
                //grouper.SetGroupOn("category");
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            btnEditRights.Enabled = CanEdit;
        }
        void FormatGrid()
        {
            for (int x = 0; x < dgReportRights.Rows.Count; x++)
            {
                if (dgReportRights.Rows[x].Cells["category"].Value.ToString() == "Reports - Sales")
                {
                    dgReportRights.Columns[4].Visible = false;
                    dgReportRights.Columns[5].Visible = false;
                    dgReportRights.Columns[6].Visible = false;
                }
                if (dgReportRights.Rows[x].Cells["category"].Value.ToString() == "Reports - Purchase")
                {
                    dgReportRights.Columns[4].Visible = false;
                    dgReportRights.Columns[5].Visible = false;
                    dgReportRights.Columns[6].Visible = false;
                }
                //if (dgvUserAccess.Rows[x].Cells["category"].Value.ToString() == "Reports - Inventory")
                //{
                //    dgReportRights.Columns[4].Visible = false;
                //    dgReportRights.Columns[5].Visible = false;
                //    dgReportRights.Columns[6].Visible = false;
                //}
            }
        }
        void LoadItemUserAccess()
        {
            UserAdd = false;
            SelUser = (dgListOfUsers.CurrentRow.DataBoundItem as DataRowView).Row;
            this.lblUser2.Text = SelUser["user_name"].ToString();
            string selectSql = @"SELECT 
                                    ua.form_code
                                    ,frm.category
                                    ,frm.form_name
                                    ,frm.field_name
                                    ,ua.u_view
                                    ,ua.u_add
                                    ,ua.u_edit
                                    ,ua.u_delete
                                 FROM User_Access ua
                                 INNER JOIN Forms frm
                                 ON (ua.form_code = frm.form_code) 
                                 WHERE frm.IsField = '1' AND ua.user_id = " + SelUser["user_id"] + " ORDER BY category ASC";
                                 

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                DataTable TbItemUserAccess = new DataTable();
                TbItemUserAccess.Columns.Add("form_code");
                TbItemUserAccess.Columns.Add("category");
                TbItemUserAccess.Columns.Add("form_name");
                TbItemUserAccess.Columns.Add("field_name");

                TbItemUserAccess.Columns.Add("u_view", typeof(bool));
                TbItemUserAccess.Columns.Add("u_add", typeof(bool));
                TbItemUserAccess.Columns.Add("u_edit", typeof(bool));
                TbItemUserAccess.Columns.Add("u_delete", typeof(bool));
                da.Fill(TbItemUserAccess);
                dgSpecialAccessRight.DataSource = TbItemUserAccess;
                dgSpecialAccessRight.Columns[0].HeaderText = "Form Code";
                dgSpecialAccessRight.Columns[1].HeaderText = "Menu";
                dgSpecialAccessRight.Columns[1].ReadOnly = true;
                dgSpecialAccessRight.Columns[2].HeaderText = "";
                dgSpecialAccessRight.Columns[2].ReadOnly = false;

                dgSpecialAccessRight.Columns[3].HeaderText = "Field Name";
                dgSpecialAccessRight.Columns[3].ReadOnly = true;

                dgSpecialAccessRight.Columns[4].HeaderText = "View";
                dgSpecialAccessRight.Columns[5].HeaderText = "Add";
                dgSpecialAccessRight.Columns[6].HeaderText = "Edit";
                dgSpecialAccessRight.Columns[7].HeaderText = "Delete";

                dgSpecialAccessRight.Columns[0].Visible = false;
                dgSpecialAccessRight.Columns[1].Visible = true;
                dgSpecialAccessRight.Columns[2].Visible = false;
                dgSpecialAccessRight.Columns[3].Visible = true;
                dgSpecialAccessRight.Columns[4].Visible = true;
                dgSpecialAccessRight.Columns[5].Visible = false;
                dgSpecialAccessRight.Columns[6].Visible = true;
                dgSpecialAccessRight.Columns[7].Visible = false;

                dgSpecialAccessRight.Refresh();

                //var grouper = new Subro.Controls.DataGridViewGrouper(dgvUserAccess);
                //grouper.SetGroupOn("category");
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            btnItemEdit.Enabled = CanEdit;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool isUpdate = false;
            foreach (DataGridViewRow dgvr in dgvUserAccess.Rows)
            {
                if (dgvr.Cells["form_code"].Value != null
                    && String.Format("{0}", dgvr.Cells["form_code"].Value) != "")
                {
                    Int64 userid = (Int64)SelUser["user_id"];
                    string formcode = String.Format("{0}", dgvr.Cells["form_code"].Value);
                    bool canview, canedit, canadd, candelete;
                    canview = canedit = canadd = candelete = false;

                    if (dgvr.Cells["u_view"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_view"].Value) != "")
                        canview = (bool)dgvr.Cells["u_view"].Value;
                    if (dgvr.Cells["u_edit"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_edit"].Value) != "")
                        canedit = (bool)dgvr.Cells["u_edit"].Value;
                    if (dgvr.Cells["u_add"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_add"].Value) != "")
                        canadd = (bool)dgvr.Cells["u_add"].Value;
                    if (dgvr.Cells["u_delete"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_delete"].Value) != "")
                        candelete = (bool)dgvr.Cells["u_delete"].Value;

                    int viewrights = canview ? 1 : 0;
                    int editrights = canedit ? 1 : 0;
                    int addrights = canadd ? 1 : 0;
                    int deleterights = candelete ? 1 : 0;

                    SqlConnection con = null;
                    try
                    {
                        con = new SqlConnection(CommonClass.ConStr);
                        string writesql;
                        string querysql = @"SELECT 
                                                form_code
                                            FROM User_Access
                                            WHERE user_id = " + SelUser["user_id"];
                        querysql += " AND form_code = '" + formcode + "'";

                        SqlCommand cmd = new SqlCommand(querysql, con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            isUpdate = true;
                            writesql = "UPDATE User_Access SET ";
                            writesql += "u_view = " + viewrights;
                            writesql += ",u_edit = " + editrights;
                            writesql += ",u_add = " + addrights;
                            writesql += ",u_delete = " + deleterights;
                            writesql += " WHERE form_code = '" + formcode + "'";
                            writesql += " AND user_id = " + userid;
                        }
                        else //Will not be used for the moment
                        {
                            isUpdate = false;
                            writesql = "INSERT INTO User_Access(user_id, form_code, u_view, u_edit, u_add, u_delete) ";
                            writesql += " VALUES(" + userid + ",'" + formcode + "'," + viewrights + "," + editrights + "," + addrights + "," + deleterights + ")";
                        }

                        SqlCommand writecmd = new SqlCommand(writesql, con);
                        int writeAffectedRows = writecmd.ExecuteNonQuery();
                        if (writeAffectedRows > 0)
                        {
                            //Record write is successful
                            string outx = "";
                            string thisFormCode;
                            CommonClass.AppFormCode.TryGetValue(this.Text, out outx);
                            if (outx != null && outx != "")
                            {
                                thisFormCode = outx;
                            }
                            else
                            {
                                thisFormCode = this.Text;
                            }
                            if (isUpdate)
                            {
                                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Update User Access for UserId = " + userid, "");
                            }
                        }
                    }
                    catch (SqlException exception)
                    {
                        Console.WriteLine(exception.ToString());
                    }
                    finally
                    {
                        if (con != null)
                            con.Close();
                    }
                }
            }
            if (isUpdate)
            {
                MessageBox.Show("User rights is updated");
            
                MessageBox.Show("Restart the application to apply changes", "Information");

            }

            btnRefreshAccess.PerformClick();
            Cursor.Current = Cursors.Default;
        }

        private void btnOkUserProfile_Click(object sender, EventArgs e)
        {
            DateToday = DateTime.Now;
            if (txtPassword.Text != ""
                && txtPassword.Text != txtVerifyPassword.Text)
            {
                MessageBox.Show("Password does not match");
                return;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string writesql;
                Int64 userid = 0;

                if (!UserAdd)
                {
                    userid = (Int64)SelUserProfile["user_id"];
                    writesql = "UPDATE Users SET ";
                    writesql += "user_name = '" + txtUserName.Text + "'";
                    if (txtPassword.Text != "")
                        writesql += ",user_pwd = '" + CommonClass.SHA512(txtPassword.Text) + "'";
                    writesql += ",user_fullname = '" + txtFullName.Text + "'";
                    writesql += ",user_department = '" + txtDepartment.Text + "'";
                    writesql += ",user_inactive = " + (cbActive.Checked ? 0 : 1);
                    writesql += ",isSalesperson = '" + (IsSalesPerson.Checked ? 1 : 0) + "'";
                    writesql += ",IsSupervisor = '" + (isSupervisor.Checked ? 1 : 0) + "'";
                    writesql += ",IsTechnician = '" + (isTechnician.Checked ? 1 : 0) + "'";
                    writesql += ",IsAdministrator = '" + (isAdmin.Checked ? 1 : 0) + "'";
                    writesql += " WHERE user_id = " + userid;
                }
                else
                {
                    writesql = "INSERT INTO Users (user_name, user_pwd, user_fullname, user_department, user_inactive, date_created, date_lastmodified, date_lastaccess, isSalesperson,IsSupervisor, IsTechnician, IsAdministrator ) VALUES ( ";
                    writesql += "'" + txtUserName.Text + "'";
                    writesql += ",'" + CommonClass.SHA512(txtPassword.Text) + "'";
                    writesql += ",'" + txtFullName.Text + "'";
                    writesql += ",'" + txtDepartment.Text + "'";
                    writesql += "," + (cbActive.Checked ? 0 : 1);
                    writesql += ",'" + DateToday.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    writesql += ",'" + "" + "'";
                    writesql += ",'" + "" + "'"; 
                    writesql += ",'" + (IsSalesPerson.Checked ? 1 : 0) + "'";
                    writesql += ",'" + (isSupervisor.Checked ? 1 : 0) + "'";
                    writesql += ",'" + (isTechnician.Checked ? 1 : 0) + "'";
                    writesql += ",'" + (isAdmin.Checked ? 1 : 0) + "'";

                    writesql += " )";

                    string useraccesssql = @"INSERT INTO User_Access (user_id, form_code, u_view, u_add, u_edit, u_delete)
                                              SELECT (SELECT MAX(user_id) FROM Users), form_code, u_view, u_add, u_edit, u_delete
                                              FROM User_Access
                                              WHERE user_id = 1";
                    writesql = writesql + "; " + useraccesssql;
                }
                SqlCommand writecmd = new SqlCommand(writesql, con);
                con.Open();
                int writeAffecteRows = writecmd.ExecuteNonQuery();
                if (writeAffecteRows > 0)
                {
                    //Record write is successful
                    string outx = "";
                    string thisFormCode;
                    CommonClass.AppFormCode.TryGetValue(this.Text, out outx);
                    if (outx != null && outx != "")
                    {
                        thisFormCode = outx;
                    }
                    else
                    {
                        thisFormCode = Text;
                    }

                    if (!UserAdd)
                    {
                        MessageBox.Show("User record is updated");
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Update User Access for UserId = " + userid, "");
                    }
                    else
                    {
                        MessageBox.Show("User is added to the record");
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added new User", "");
                    }
                }
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                if (con != null)
                    con.Close();

                if (UserAdd)
                {
                    UserMaintenance_Load(sender, e);
                    clearfields();
                }
            }
            btnRefresh.PerformClick();
        }

        private void dgvUserListProfile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            dgvUserListProfile.Rows[e.RowIndex].Selected = true;
            SelUserProfile = (dgvUserListProfile.CurrentRow.DataBoundItem as DataRowView).Row;
            string selectSql = @"SELECT 
                                    user_name
                                    ,user_fullname
                                    ,user_department
                                    ,user_inactive
                                    ,IsSalesperson
                                    ,IsSupervisor
                                    ,IsTechnician
                                    ,IsAdministrator
                                 FROM Users
                                 WHERE user_id = " + SelUserProfile["user_id"];

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);
                con.Open();

                SqlDataReader uprofile_reader = cmd.ExecuteReader();

                while (uprofile_reader.Read())
                {
                    txtUserID.Text = String.Format("{0}", SelUserProfile["user_id"]);
                    txtUserName.Text = String.Format("{0}", uprofile_reader["user_name"]);
                    txtFullName.Text = String.Format("{0}", uprofile_reader["user_fullname"]);
                    txtDepartment.Text = String.Format("{0}", uprofile_reader["user_department"]);
                    IsSalesPerson.Checked = ((bool)uprofile_reader["IsSalesperson"]);
                    isTechnician.Checked = ((bool)uprofile_reader["IsTechnician"]);
                    isSupervisor.Checked = ((bool)uprofile_reader["IsSupervisor"]);
                    isAdmin.Checked = ((bool)uprofile_reader["IsAdministrator"]);

                    bool isActiveChecked = false;

                    if (uprofile_reader["user_inactive"] == null
                        || String.Format("{0}", uprofile_reader["user_inactive"]) == "")
                        isActiveChecked = true;
                    else
                    {
                        isActiveChecked = !((bool)uprofile_reader["user_inactive"]);
                    }
                    cbActive.Checked = isActiveChecked;
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            textenable();
            //clear
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtVerifyPassword.Text = "";
            txtDepartment.Text = "";
            IsSalesPerson.Checked = false;
            isSupervisor.Checked = false;
            isAdmin.Checked = false;
            isTechnician.Checked = false;
            cbActive.Checked = false;

            UserAdd = true;
            btnEdit.Enabled = false;
            btnDeleteUser.Enabled = false;
            btnOkUserProfile.Enabled = true;
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (SelUserProfile == null || txtUserID.Text == "")
            {
                MessageBox.Show("No user is selected");
            }
            else
            {
                SqlConnection con = null;
                try
                {
                    string titles = "Delete User Profile Record";
                    DialogResult dialogResult = MessageBox.Show("Do you wish to continue deleting user profile? (yes/no)", titles, MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        con = new SqlConnection(CommonClass.ConStr);
                        Int64 userid = (Int64)SelUserProfile["user_id"];
                        string audittrailsql = "SELECT UserID FROM SystemAuditTrail WHERE UserID = " + userid;
                        SqlCommand checkauditcmd = new SqlCommand(audittrailsql, con);
                        con.Open();
                        SqlDataReader auditdr = checkauditcmd.ExecuteReader();

                        if (auditdr.Read())
                        {
                            MessageBox.Show("Unable to delete user. It has existing transactions");
                            return;
                        }

                        string writesql = "DELETE FROM Users WHERE user_id = " + userid;
                        writesql += "; DELETE FROM User_Access WHERE user_id = " + userid;

                        SqlCommand writecmd = new SqlCommand(writesql, con);
                        int writeAffectedRows = writecmd.ExecuteNonQuery();
                        if (writeAffectedRows > 0)
                        {
                            //Record write is successful
                            string outx = "";
                            string thisFormCode;
                            CommonClass.AppFormCode.TryGetValue(this.Text, out outx);
                            if (outx != null && outx != "")
                            {
                                thisFormCode = outx;
                            }
                            else
                            {
                                thisFormCode = this.Text;
                            }
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Delete User with UserId = " + userid, "");
                        }
                    }
                }
                catch (SqlException exception)
                {
                    Console.WriteLine(exception.ToString());
                }
                finally
                {
                    if (con != null)
                        con.Close();

                    UserMaintenance_Load(sender, e);
                    clearfields();
                    //SelUserProfile.Delete();
                    SelUserProfile = null;
                    UserAdd = true;
                }
            }
            btnRefresh.PerformClick();
        }

        private void dgvUserAccess_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtUserProfileSearch_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT 
                                    user_id
                                    ,user_name
                                    ,user_fullname
                                   FROM Users
                                   WHERE user_name LIKE '" + txtUserProfileSearch.Text + "%'";
                selectSql += " OR user_fullname LIKE '" + txtUserProfileSearch.Text + "%'";
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(selectSql, con);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvUserListProfile.DataSource = dt;
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

        private void txtUserSearch_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT 
                                    user_id
                                    ,user_name
                                    ,user_fullname
                                   FROM Users
                                   WHERE user_name LIKE '" + txtUserSearch.Text + "%'";
                selectSql += " OR user_fullname LIKE '" + txtUserSearch.Text + "%'";
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(selectSql, con);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgvUserList.DataSource = dt;
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

        private void dgvUserListProfile_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvUserListProfile.Rows[0].Selected = true;
            dgvUserListProfile_CellClick(sender, e);
        }

        private void dgvUserList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dgvUserList.Rows[0].Selected = true;
            dgvUserList_CellClick(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            textenable();
            btnAddUser.Enabled = false;
            btnDeleteUser.Enabled = false;
            btnOkUserProfile.Enabled = true;//save
        }
        void textdisable()
        {
            txtUserName.Enabled = false;
            txtFullName.Enabled = false;
            txtPassword.Enabled = false;
            txtVerifyPassword.Enabled = false;
            txtDepartment.Enabled = false;
            cbActive.Enabled = false;
            IsSalesPerson.Enabled = false;
            isSupervisor.Enabled = false;
            isTechnician.Enabled = false;
            isAdmin.Enabled = false;
        }
        void textenable()
        {
            txtUserName.Enabled = true;
            txtFullName.Enabled = true;
            txtPassword.Enabled = true;
            txtVerifyPassword.Enabled = true;
            txtDepartment.Enabled = true;
            cbActive.Enabled = true;
            IsSalesPerson.Enabled = true;
            isSupervisor.Enabled = true;
            isTechnician.Enabled = true;
            isAdmin.Enabled = true;
        }
        void clearfields()
        {
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtVerifyPassword.Text = "";
            txtDepartment.Text = "";
            IsSalesPerson.Enabled = false;
            isSupervisor.Enabled = false;
            isTechnician.Enabled = false;
            isAdmin.Enabled = false;
            cbActive.Checked = false;
        }
        void LoadUser()
        {
            SqlConnection con = null;
            try
            {
                string selectSql = @"SELECT 
                                   user_id
                                   ,user_name
                                   ,user_fullname
                                   ,IsSalesperson
                                   ,IsSupervisor
                                   ,IsTechnician
                                   ,IsAdministrator
                               FROM Users";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(selectSql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                DataTable TbAcct = new DataTable();
                da.Fill(TbAcct);
                dgvUserList.DataSource = TbAcct;
                dgvUserList.Columns[0].HeaderText = "User ID";
                dgvUserList.Columns[1].HeaderText = "User Name";
                dgvUserList.Columns[2].HeaderText = "User Full Name";
                dgvUserList.Columns[0].Visible = true;
                dgvUserList.Columns[1].Visible = true;
                dgvUserList.Columns[2].Visible = true;

                dgvUserListProfile.DataSource = TbAcct;
                dgvUserListProfile.Columns[0].HeaderText = "User ID";
                dgvUserListProfile.Columns[1].HeaderText = "User Name";
                dgvUserListProfile.Columns[2].HeaderText = "User Full Name";
                //dgvUserListProfile.Columns[3].HeaderText = "Time In";
                //dgvUserListProfile.Columns[4].HeaderText = "Time Out";

                dgvUserListProfile.Columns[0].Visible = true;
                dgvUserListProfile.Columns[1].Visible = true;
                dgvUserListProfile.Columns[2].Visible = true;
                // dgvUserListProfile.Columns[3].Visible = true;
                //dgvUserListProfile.Columns[4].Visible = true;


                dgListOfUsers.DataSource = TbAcct;
                dgListOfUsers.Columns[0].HeaderText = "User ID";
                dgListOfUsers.Columns[1].HeaderText = "User Name";
                dgListOfUsers.Columns[2].HeaderText = "User Full Name";
                dgListOfUsers.Columns[0].Visible = true;
                dgListOfUsers.Columns[1].Visible = true;
                dgListOfUsers.Columns[2].Visible = true;

                dgUserProfileRights.DataSource = TbAcct;
                dgUserProfileRights.Columns[0].HeaderText = "User ID";
                dgUserProfileRights.Columns[1].HeaderText = "User Name";
                dgUserProfileRights.Columns[2].HeaderText = "User Full Name";
                dgUserProfileRights.Columns[0].Visible = true;
                dgUserProfileRights.Columns[1].Visible = true;
                dgUserProfileRights.Columns[2].Visible = true;
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SelUserProfile = null;
            btnAddUser.Enabled = CanAdd;
            btnDeleteUser.Enabled = CanDelete;
            btnOkUserProfile.Enabled = false;
            btnEdit.Enabled = CanEdit;
            btnOk.Enabled = CanEdit;
            textdisable();
            UserAdd = false;
            LoadUser();
            clearfields();
        }

        private void btnEditAccess_Click(object sender, EventArgs e)
        {
            if (SelUser != null)
            {
                btnOk.Enabled = true;
                dgvUserAccess.ReadOnly = false;
                dgSpecialAccessRight.ReadOnly = false;
                chkbAllAdd.Enabled = true;
                chkbAllEdit.Enabled = true;
                chkbAllDelete.Enabled = true;
                chkbAllView.Enabled = true;
                btnEditAccess.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please select a user to edit user access", "Informations");
            }
        }

        private void btnRefreshAccess_Click(object sender, EventArgs e)
        {
            dgvUserList.Rows[0].Selected = false;
            SelUser = null;
            btnOk.Enabled = false;
            dgvUserAccess.ReadOnly = true;
            dgSpecialAccessRight.ReadOnly = true;
            chkbAllAdd.Enabled = false;
            chkbAllEdit.Enabled = false;
            chkbAllDelete.Enabled = false;
            chkbAllView.Enabled = false;
            btnEditAccess.Enabled = CanEdit;

        }

        private void dgListOfUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            LoadItemUserAccess();
        }

        private void dgListOfUsers_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dgListOfUsers.Rows[0].Selected = true;
            dgListOfUsers_CellClick(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool isUpdate = false;
            foreach (DataGridViewRow dgvr in dgSpecialAccessRight.Rows)
            {
                if (dgvr.Cells["form_code"].Value != null
                    && String.Format("{0}", dgvr.Cells["form_code"].Value) != "")
                {
                    Int64 userid = (Int64)SelUser["user_id"];
                    string formcode = String.Format("{0}", dgvr.Cells["form_code"].Value);
                    bool canview, canedit, canadd, candelete;
                    canview = canedit = canadd = candelete = false;

                    if (dgvr.Cells["u_view"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_view"].Value) != "")
                        canview = (bool)dgvr.Cells["u_view"].Value;
                    if (dgvr.Cells["u_edit"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_edit"].Value) != "")
                        canedit = (bool)dgvr.Cells["u_edit"].Value;
                    if (dgvr.Cells["u_add"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_add"].Value) != "")
                        canadd = (bool)dgvr.Cells["u_add"].Value;
                    if (dgvr.Cells["u_delete"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_delete"].Value) != "")
                        candelete = (bool)dgvr.Cells["u_delete"].Value;

                    int viewrights = canview ? 1 : 0;
                    int editrights = canedit ? 1 : 0;
                    int addrights = canadd ? 1 : 0;
                    int deleterights = candelete ? 1 : 0;

                    SqlConnection con = null;
                    try
                    {
                        con = new SqlConnection(CommonClass.ConStr);
                        string writesql;
                        string querysql = @"SELECT 
                                            form_code
                                            FROM User_Access
                                            WHERE user_id = " + SelUser["user_id"];
                        querysql += " AND form_code = '" + formcode + "'";

                        SqlCommand cmd = new SqlCommand(querysql, con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            isUpdate = true;
                            writesql = "UPDATE User_Access SET ";
                            writesql += "u_view = " + viewrights;
                            writesql += ",u_edit = " + editrights;
                            writesql += ",u_add = " + addrights;
                            writesql += ",u_delete = " + deleterights;
                            writesql += " WHERE form_code = '" + formcode + "'";
                            writesql += " AND user_id = " + userid;
                        }
                        else //Will not be used for the moment
                        {
                            isUpdate = false;
                            writesql = "INSERT INTO User_Access(user_id, form_code, u_view, u_edit, u_add, u_delete) ";
                            writesql += " VALUES(" + userid + ",'" + formcode + "'," + viewrights + "," + editrights + "," + addrights + "," + deleterights + ")";
                        }

                        SqlCommand writecmd = new SqlCommand(writesql, con);
                        int writeAffectedRows = writecmd.ExecuteNonQuery();
                        if (writeAffectedRows > 0)
                        {
                            //Record write is successful
                            string outx = "";
                            string thisFormCode;
                            CommonClass.AppFormCode.TryGetValue(this.Text, out outx);
                            if (outx != null && outx != "")
                            {
                                thisFormCode = outx;
                            }
                            else
                            {
                                thisFormCode = this.Text;
                            }
                            if (isUpdate)
                            {
                                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Update User Access for UserId = " + userid, "");
                            }
                        }
                    }
                    catch (SqlException exception)
                    {
                        Console.WriteLine(exception.ToString());
                    }
                    finally
                    {
                        if (con != null)
                            con.Close();
                    }
                }
            }
            if (isUpdate)
            {
                MessageBox.Show("Item User rights is updated");
                MessageBox.Show("Restart the application to apply changes", "Information");
            }
            btnItemRefresh.PerformClick();


            Cursor.Current = Cursors.Default;
        }

        private void btnItemEdit_Click(object sender, EventArgs e)
        {
            if (SelUser != null)
            {
                btnSave.Enabled = true;
                dgListOfUsers.ReadOnly = false;
                dgSpecialAccessRight.ReadOnly = false;
                SelectAllEditSpecialRights.Enabled = true;
                SelectAllViewSpecialRights.Enabled = true;
                btnItemEdit.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please select a user to edit itme user access", "Informations");
            }
        }

        private void btnItemRefresh_Click(object sender, EventArgs e)
        {
            dgListOfUsers.Rows[0].Selected = false;
            SelUser = null;
            btnSave.Enabled = false;
            dgSpecialAccessRight.ReadOnly = true;
            SelectAllEditSpecialRights.Enabled = false;
            SelectAllViewSpecialRights.Enabled = false;
            btnItemEdit.Enabled = CanEdit;
        }

        private void txtUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgUserProfileRights_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            LoadReportRightAccess();
            FormatGrid();
        }

        private void btnEditRights_Click(object sender, EventArgs e)
        {
            if (SelUser != null)
            {
                btnSaveRights.Enabled = true;
                dgUserProfileRights.ReadOnly = false;
                dgReportRights.ReadOnly = false;
                SelectAllViewRights.Enabled = false;
                SelectAllViewRights.Enabled = true;
                btnEditRights.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please select a user to edit report access rights", "Informations");
            }
        }

        private void btnRefreshRights_Click(object sender, EventArgs e)
        {
            dgUserProfileRights.Rows[0].Selected = false;
            SelUser = null;
            btnSaveRights.Enabled = false;
            dgReportRights.ReadOnly = true;
            SelectAllViewRights.Enabled = false;
            SelectAllViewRights.Enabled = false;
            btnEditRights.Enabled = CanEdit;

        }

        private void btnSaveRights_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool isUpdate = false;
            foreach (DataGridViewRow dgvr in dgReportRights.Rows)
            {
                if (dgvr.Cells["form_code"].Value != null
                    && String.Format("{0}", dgvr.Cells["form_code"].Value) != "")
                {
                    Int64 userid = (Int64)SelUser["user_id"];
                    string formcode = String.Format("{0}", dgvr.Cells["form_code"].Value);
                    bool canview, canedit, canadd, candelete;
                    canview = canedit = canadd = candelete = false;

                    if (dgvr.Cells["u_view"].Value != null
                        && String.Format("{0}", dgvr.Cells["u_view"].Value) != "")
                        canview = (bool)dgvr.Cells["u_view"].Value;

                    int viewrights = canview ? 1 : 0;

                    SqlConnection con = null;
                    try
                    {
                        con = new SqlConnection(CommonClass.ConStr);
                        string writesql;
                        string querysql = @"SELECT 
                                            form_code
                                            FROM User_Access
                                            WHERE user_id = " + SelUser["user_id"];
                        querysql += " AND form_code = '" + formcode + "'";

                        SqlCommand cmd = new SqlCommand(querysql, con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            isUpdate = true;
                            writesql = "UPDATE User_Access SET ";
                            writesql += "u_view = " + viewrights;
                            writesql += " WHERE form_code = '" + formcode + "'";
                            writesql += " AND user_id = " + userid;
                        }
                        else //Will not be used for the moment
                        {
                            isUpdate = false;
                            writesql = "INSERT INTO User_Access(user_id, form_code, u_view) ";
                            writesql += " VALUES(" + userid + ",'" + formcode + "'," + viewrights + ")";
                        }

                        SqlCommand writecmd = new SqlCommand(writesql, con);
                        int writeAffectedRows = writecmd.ExecuteNonQuery();
                        if (writeAffectedRows > 0)
                        {
                            //Record write is successful
                            string outx = "";
                            string thisFormCode;
                            CommonClass.AppFormCode.TryGetValue(this.Text, out outx);
                            if (outx != null && outx != "")
                            {
                                thisFormCode = outx;
                            }
                            else
                            {
                                thisFormCode = this.Text;
                            }
                            if (isUpdate)
                            {
                                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Update User Access for UserId = " + userid, "");
                            }
                        }
                    }
                    catch (SqlException exception)
                    {
                        Console.WriteLine(exception.ToString());
                    }
                    finally
                    {
                        if (con != null)
                            con.Close();
                    }
                }
            }
            if (isUpdate)
            {
                MessageBox.Show("Report access rights is updated");
                MessageBox.Show("Restart the application to apply changes", "Information");
            }
            btnRefreshRights.PerformClick();
            Cursor.Current = Cursors.Default;
        }

        private void dgUserProfileRights_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dgUserProfileRights.Rows[0].Selected = true;
            dgUserProfileRights_CellClick(sender, e);
        }

        private void txtSearchUser_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT 
                                    user_id
                                    ,user_name
                                    ,user_fullname
                                   FROM Users
                                   WHERE user_name LIKE '" + txtUserSearch.Text + "%'";
                selectSql += " OR user_fullname LIKE '" + txtUserSearch.Text + "%'";
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(selectSql, con);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgListOfUsers.DataSource = dt;
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

        private void txtUserProfile_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT 
                                    user_id
                                    ,user_name
                                    ,user_fullname
                                   FROM Users
                                   WHERE user_name LIKE '" + txtUserSearch.Text + "%'";
                selectSql += " OR user_fullname LIKE '" + txtUserSearch.Text + "%'";
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(selectSql, con);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dgUserProfileRights.DataSource = dt;
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

        private void chkbAllDelete_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvUserAccess.Rows)
            {
                dr.Cells[6].Value = chkbAllDelete.Checked;
            }
        }

        private void chkbAllEdit_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvUserAccess.Rows)
            {
                dr.Cells[5].Value = chkbAllEdit.Checked;
            }
        }

        private void chkbAllAdd_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvUserAccess.Rows)
            {
                dr.Cells[4].Value = chkbAllAdd.Checked;
            }
        }

        private void SelectAllViewRights_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgReportRights.Rows)
            {
                dr.Cells[3].Value = SelectAllViewRights.Checked;
            }
        }

        private void SelectAllEditSpecialRights_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgSpecialAccessRight.Rows)
            {
                dr.Cells[6].Value = SelectAllEditSpecialRights.Checked;
            }
        }

        private void SelectAllViewSpecialRights_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgSpecialAccessRight.Rows)
            {
                dr.Cells[4].Value = SelectAllViewSpecialRights.Checked;
            }
        }

        private void chkbAllView_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvUserAccess.Rows)
            {
                dr.Cells[3].Value = chkbAllView.Checked;
            }
        }
    }
}
