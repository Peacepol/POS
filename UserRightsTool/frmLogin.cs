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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCncel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cmbAuthentication.Text == "")
            {
                MessageBox.Show("No authentication mode is selected. Choose one from the dropdown");
                return;
            }

            else if (cmbAuthentication.Text == "Username / Password"
                 && (txtUsername.Text == "" || txtPassword.Text == ""))
            {
                MessageBox.Show("Username and Password must be filled.");
                return;
            }

            if (txtServerName.Text != "")
            {
                string connstr = "Data Source = " + txtServerName.Text + "; Initial Catalog = master; MultipleActiveResultSets = true";

                if (cmbAuthentication.Text == "Windows Authentication")
                {
                    connstr += "; Integrated Security = true";
                }
                else if (cmbAuthentication.Text == "Username / Password")
                {
                    connstr += "; User ID = " + txtUsername.Text + "; Password = " + txtPassword.Text;
                }

                SqlConnection con = null;
                try
                {
                    con = new SqlConnection(connstr);
                    con.Open();

                    frmMain mainFrm = new frmMain(this, connstr);
                    mainFrm.Show();
                    Hide();
                }
                catch (SqlException exception)
                {
                    MessageBox.Show("Unable to connect to the server. Make sure it exists and running");
                    Console.WriteLine(exception.ToString());
                }
                finally
                {
                    if (con != null)
                        con.Close();
                }
            }
            else
            {
                MessageBox.Show("The server name and database name are required, please fill them up.");
            }
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthentication.Text == "Username / Password")
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
