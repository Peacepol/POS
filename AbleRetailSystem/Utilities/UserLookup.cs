using Microsoft.VisualBasic.ApplicationServices;
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

namespace RestaurantPOS.Utilities
{
    public partial class UserLookup : Form
    {
        private string[] UserDetail;
        public UserLookup()
        {
            InitializeComponent();
        }
        public string[] GetUserDetail
        {
            get { return UserDetail; }
        }

        private void UserLookup_Load(object sender, EventArgs e)
        {
            LoadUser();
        }
        void LoadUser()
        {
            dgvUserList.Rows.Clear();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT 
                                   user_id
                                   ,user_name
                                   ,user_fullname
                               FROM Users";
                SqlCommand cmd_ = new SqlCommand(selectSql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgvUserList.Rows.Add();
                    dgvUserList.Rows[x].Cells[0].Value = dr["user_id"].ToString();
                    dgvUserList.Rows[x].Cells[1].Value = dr["user_name"].ToString();
                    dgvUserList.Rows[x].Cells[2].Value = dr["user_fullname"].ToString();
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

        private void txtUserProfileSearch_TextChanged(object sender, EventArgs e)
        {
           if(txtUserProfileSearch.Text == "")
            {
                LoadUser();
            }
            else
            {
                LoadSearch();
            }
        }
        void LoadSearch()
        {
            dgvUserList.Rows.Clear();
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
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgvUserList.Rows.Add();
                    dgvUserList.Rows[x].Cells[0].Value = dr["user_id"].ToString();
                    dgvUserList.Rows[x].Cells[1].Value = dr["user_name"].ToString();
                    dgvUserList.Rows[x].Cells[2].Value = dr["user_fullname"].ToString();
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
        private void dgvUserList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvUserList.Rows[e.RowIndex].Selected = true;
            UserDetail = new string[2];
            UserDetail[0] = dgvUserList.Rows[e.RowIndex].Cells[0].Value.ToString();
            UserDetail[1] = dgvUserList.Rows[e.RowIndex].Cells[1].Value.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
