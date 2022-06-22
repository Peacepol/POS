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

namespace UserRightsTool
{
    public partial class frmMain : Form
    {
        private string mConnectionStr;
        private frmLogin mReferer;

        public frmMain(frmLogin referer, string connstr)
        {
            mConnectionStr = connstr;
            mReferer = referer;
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(mConnectionStr);
                string dblistsql = "SELECT name FROM master.sys.databases";
                con.Open();

                SqlCommand cmd = new SqlCommand(dblistsql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        cmbDatabase.Items.Add(reader[0]);
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

        private void btnCncel_Click(object sender, EventArgs e)
        {
            if (mReferer != null && !mReferer.IsDisposed)
                mReferer.Close();

            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string dbloginsql = @"IF NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE name = 'ableacctg')
                                BEGIN
                                    CREATE LOGIN ableacctg WITH PASSWORD = '5!37e5CCt9'
                                END";
            string windowsauthuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            string[] sqlsecurity =
            {
                dbloginsql,
                "ALTER AUTHORIZATION ON DATABASE::[" + cmbDatabase.Text.Trim() + "] TO ableacctg",
                "USE master",
                "DENY VIEW ANY DATABASE TO PUBLIC"/*,
                "ALTER SERVER ROLE sysadmin DROP MEMBER [" + windowsauthuser + "]"*/
            };

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(mConnectionStr);
                con.Open();
                foreach (string securityleaf in sqlsecurity)
                {
                    SqlCommand securitycmd = new SqlCommand(securityleaf, con);
                    securitycmd.ExecuteNonQuery();
                }
                MessageBox.Show("Security rights applied to " + cmbDatabase.Text);
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

        private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDbName.Text = cmbDatabase.Text;
        }
    }
}
