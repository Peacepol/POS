using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Purchase
{
    public partial class ReceiveDialog : Form
    {
        private string[] ReceiveItemInfo;
        public ReceiveDialog()
        {
            InitializeComponent();
        }

        private void ReceiveDialog_Load(object sender, EventArgs e)
        {

        }

        public string[] GetReceiveInfo
        {
            get { return ReceiveItemInfo; }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            ReceiveItemInfo = new string[3];
            ReceiveItemInfo[0] = this.dtReceivedDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            ReceiveItemInfo[1] = this.txtSupInvoice.Text;
            ReceiveItemInfo[2] = this.txtReference.Text;
            this.DialogResult = DialogResult.OK;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
