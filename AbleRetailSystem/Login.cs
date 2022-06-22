using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS
{
    public partial class Login : Form
    {
        private DataGridViewRow CoRow;
        private MainPage mainpageref = null;
        private CoProForm splashref = null;
        
        public Login(DataGridViewRow pRow, MainPage mpref, CoProForm spref)
        {
            InitializeComponent();
            CoRow = pRow;
            mainpageref = mpref;
            splashref = spref;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblCo.Text = CoRow.Cells["CompName"].Value.ToString();
            this.txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CommonClass.AppLogin (
                    CoRow.Cells["ServerName"].Value.ToString(), 
                    CoRow.Cells["DatabaseName"].Value.ToString(), 
                    txtUsername.Text, 
                    txtPassword.Text,
                    CoRow.Cells["dbuser"].Value.ToString(),
                    CoRow.Cells["dbpass"].Value.ToString())
                )
            {
                CommonClass.SetAppFormCodes();
                if (mainpageref != null)
                    mainpageref.ApplyAccess(CommonClass.UserID);

                CommonClass.LoggedInCompany = CoRow.Cells["CompName"].Value.ToString();
                CommonClass.LoggedInSerialNo = CoRow.Cells["SerialNo"].Value.ToString();
                CommonClass.LoggedInRegNo = CoRow.Cells["RegistrationNo"].Value.ToString();
                CommonClass.LoggedInDbName = CoRow.Cells["DatabaseName"].Value.ToString();
                CommonClass.LoggedInServerName = CoRow.Cells["ServerName"].Value.ToString();
                CommonClass.DbUser = CoRow.Cells["dbuser"].Value.ToString();
                CommonClass.DbPass = CoRow.Cells["dbpass"].Value.ToString();

                Hide();
                mainpageref.Show();
                
                //LoginStatus();
            }
            else
            {
                MessageBox.Show("Username and Password combination does not exists.");
            }
        }
        public void LoginStatus()
        {
            DateTime dtNow = DateTime.Now;
            string updateLoginStatus = @"UPDATE Users SET IsLoggedIn = 1, StartSession = @StartSession WHERE user_id = " + CommonClass.UserID;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@StartSession", dtNow);
            CommonClass.runSql(updateLoginStatus, CommonClass.RunSqlInsertMode.QUERY, param);
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
