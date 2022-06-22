using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbleAccountingSystem;

namespace CodeGenerator
{
    public partial class frmCodeGenerator : Form
    {
        public frmCodeGenerator()
        {
            InitializeComponent();
        }

        private void frmCodeGenerator_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerateSerialReg_Click(object sender, EventArgs e)
        {
            if (txtCompName.Text != "")
            {
                txtSerialNo.Text = CommonClass.generateRandomNumber(txtCompName.Text).ToString();
                txtRegNo.Text = CommonClass.StringMixer(6);
            }
        }

        private void btnGenerateActivation_Click(object sender, EventArgs e)
        {
            if (txtCompName.Text != "" && txtRegNo.Text != "" && txtSerialNo.Text != "")
            {
                rtxtActivationKey.Text = CommonClass.generateActivationCode(txtCompName.Text.Trim(), txtRegNo.Text.Trim(), txtSerialNo.Text.Trim());
            }
        }

        private void btnGenerateReact_Click(object sender, EventArgs e)
        {
            if (txtCompNameReact.Text == ""
                && txtInvoiceNumber.Text == "")
            {
                MessageBox.Show("All fields are required");
                return;
            }

            rtxtActKeyReact.Text = CommonClass.generateReActivationCode(txtCompNameReact.Text, txtInvoiceNumber.Text);
        }
    }
}
