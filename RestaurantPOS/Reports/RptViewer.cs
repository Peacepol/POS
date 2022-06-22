using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports;
namespace AbleRetailPOS
{
    public partial class RptViewer : Form
    {
        public RptViewer()
        {
            InitializeComponent();
        }

        private void RptViewer_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            crViewer.PrintReport();
        }
    }
}
