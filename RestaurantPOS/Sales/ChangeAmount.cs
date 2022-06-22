using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Sales
{
    public partial class ChangeAmount : Form
    {
        string tChange = "";
        public ChangeAmount(string TotalChange)
        {
            tChange = TotalChange;
            InitializeComponent();
        }

        private void btnChangeDialog_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ChangeAmount_Load(object sender, EventArgs e)
        {
            if (tChange != "")
            {
                double d = double.Parse(tChange, NumberStyles.Currency);
                lblChange.Text = d.ToString("C2");
            }
        }

        private void lblChange_Click(object sender, EventArgs e)
        {

        }
    }
}
