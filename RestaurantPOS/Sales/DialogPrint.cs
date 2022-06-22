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
    public partial class DialogPrint : Form
    {
        public string messagetxt = "";
        public DialogPrint(string msg)
        {
            InitializeComponent();
            messagetxt = msg;
        }

        private void DialogPrint_Load(object sender, EventArgs e)
        {
            txtShow.Text = messagetxt;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
