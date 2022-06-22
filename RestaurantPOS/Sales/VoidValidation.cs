using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Sales
{
    public partial class VoidValidation : Form
    {
        private string pUsername;
        private string pPassword;


        public VoidValidation( string pTitle = "")
        {
            InitializeComponent();
            if(pTitle != "")
            {
                lblTitle.Text = pTitle;
            }
        }
        public string GetUsername
        {
            get { return pUsername; }
        }
        public string GetPassword
        {
            get { return pPassword; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string admin;
            string sup;
            if (CommonClass.isAdministrator == true || CommonClass.isSupervisor == true)
            {
                pUsername = txtUsername.Text;
                pPassword = txtPassword.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                string Sql = "SELECT * FROM Users WHERE user_name = '" + txtUsername.Text + "' AND user_pwd = '" + CommonClass.SHA512(txtPassword.Text) + "'";
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, Sql);
                foreach (DataRow dr in dt.Rows)
                {
                    admin = dr["IsAdministrator"].ToString();
                    sup = dr["IsSupervisor"].ToString();

                    if (bool.Parse(admin) || bool.Parse(sup))
                    {
                        pUsername = txtUsername.Text;
                        pPassword = txtPassword.Text;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username and Password.");
                    }
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Enter Username Value", "Enter Username : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtUsername.Text = pValue;
            }
        }

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Enter Password Value", "Enter Password : ");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                string pValue = kbos.GetValue;
                txtPassword.Text = pValue;
            }
        }

        private void VoidValidation_Load(object sender, EventArgs e)
        {

        }
    }
}
