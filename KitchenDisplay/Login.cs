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

namespace KitchenDisplay
{
    public partial class Login : Form
    {
        private DataGridViewRow CoRow;
        private KitchenDisplay mainpageref = null;
        private CoProForm splashref = null;


        public Login(DataGridViewRow pRow, KitchenDisplay mpref, CoProForm spref)
        {
            InitializeComponent();
            CoRow = pRow;
            mainpageref = mpref;
            splashref = spref;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CommonClass.AppLogin(
                    CoRow.Cells["ServerName"].Value.ToString(),
                    CoRow.Cells["DatabaseName"].Value.ToString(),
                    txtUsername.Text,
                    txtPassword.Text,
                    CoRow.Cells["dbuser"].Value.ToString(),
                    CoRow.Cells["dbpass"].Value.ToString())
                )
            {
                CommonClass.LoggedInCompany = CoRow.Cells["CompName"].Value.ToString();
                CommonClass.LoggedInSerialNo = CoRow.Cells["SerialNo"].Value.ToString();
                CommonClass.LoggedInRegNo = CoRow.Cells["RegistrationNo"].Value.ToString();
                CommonClass.LoggedInDbName = CoRow.Cells["DatabaseName"].Value.ToString();
                CommonClass.LoggedInServerName = CoRow.Cells["ServerName"].Value.ToString();
                CommonClass.DbUser = CoRow.Cells["dbuser"].Value.ToString();
                CommonClass.DbPass = CoRow.Cells["dbpass"].Value.ToString();

                Hide();
                mainpageref.Show();
            }
            else
            {
                MessageBox.Show("Username and Password combination does not exists.");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblCo.Text = CoRow.Cells["CompName"].Value.ToString();
            this.txtUsername.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUsername.Text = "";
            Hide();
            splashref.Show();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sender.Equals(btnLogin) && !mainpageref.Visible)
            {
                splashref.Close();
                mainpageref.Close();
            }
        }
    }
}
