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
    public partial class ItemErrorInfo : Form
    {
        private DataTable TbError;
        public ItemErrorInfo(DataTable pTbError)
        {
            InitializeComponent();
            TbError = pTbError;
        }

        private void ItemErrorInfo_Load(object sender, EventArgs e)
        {
            this.dgridError.DataSource = TbError;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
