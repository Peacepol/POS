using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Sales
{
    public partial class KeyboardOnScreen : Form
    {
        private string pValue;
        string titlepage;
        string entervalue;
        public KeyboardOnScreen(string pTitle, string pEnterValue)
        {
            titlepage = pTitle;
            entervalue = pEnterValue;
            InitializeComponent();
        }
        public string GetValue
        {
            get { return pValue; }
        }

        private void btnq_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnq.Text;
        }

        private void btnw_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnq.Text;
        }

        private void btne_Click(object sender, EventArgs e)
        {
            txtValue.Text += btne.Text;
        }

        private void btnr_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnr.Text;
        }

        private void btnt_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnt.Text;
        }

        private void btny_Click(object sender, EventArgs e)
        {
            txtValue.Text += btny.Text;
        }

        private void btnu_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnu.Text;
        }

        private void btni_Click(object sender, EventArgs e)
        {
            txtValue.Text += btni.Text;
        }

        private void btno_Click(object sender, EventArgs e)
        {
            txtValue.Text += btno.Text;
        }

        private void btnp_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnp.Text;
        }

        private void btna_Click(object sender, EventArgs e)
        {
            txtValue.Text += btna.Text;
        }

        private void btns_Click(object sender, EventArgs e)
        {
            txtValue.Text += btns.Text;
        }

        private void btnd_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnd.Text;
        }

        private void btnf_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnf.Text;
        }

        private void btng_Click(object sender, EventArgs e)
        {
            txtValue.Text += btng.Text;
        }

        private void btnh_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnh.Text;
        }

        private void btnj_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnj.Text;
        }

        private void btnk_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnk.Text;
        }

        private void btnl_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnl.Text;
        }

        private void btnz_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnz.Text;
        }

        private void btnx_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnx.Text;
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnc.Text;
        }

        private void btnv_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnv.Text;
        }

        private void btnb_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnb.Text;
        }

        private void btnn_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnn.Text;
        }

        private void btnm_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnm.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn3.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn6.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn9.Text;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            txtValue.Text += btnDot.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtValue.Text += btn0.Text;
        }

        private void BackSpace_Click(object sender, EventArgs e)
        {
            if (txtValue.TextLength > 0)
            {
                txtValue.Text = txtValue.Text.Remove(txtValue.TextLength - 1, 1);
            }
        }

        private void btnEndEdit_Click(object sender, EventArgs e)
        {
            if (txtValue.Text != "")
            {
                pValue = txtValue.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("No Value Found!");
            }
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEndEdit.PerformClick();
            }
        }

        private void KeyboardOnScreen_Load(object sender, EventArgs e)
        {
            this.Text = titlepage;
            txtTitle.Text = titlepage;
            lblValue.Text = entervalue;
            if (txtTitle.Text == "Enter Password Value")
            {
                txtValue.PasswordChar = '*';
            } 
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtTitle.Text == "Enter Discount Value" || txtTitle.Text == "Price Value Override")
                {
                    char ch = e.KeyChar;
                    if (ch == (char)Keys.Back)
                    {
                        e.Handled = false;
                    }
                    else if (!char.IsDigit(ch) && ch != '.')
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSpace_Click(object sender, EventArgs e)
        {
            txtValue.Text += " ";
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            txtValue.Text += "-";
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            txtValue.Text += "+";
        }

        private void btnSlash_Click(object sender, EventArgs e)
        {
            txtValue.Text += "/";
        }

        private void btnAsteris_Click(object sender, EventArgs e)
        {
            txtValue.Text += "*";
        }
    }
}
