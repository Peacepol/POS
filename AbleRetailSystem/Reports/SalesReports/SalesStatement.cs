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

namespace RestaurantPOS.Reports.SalesReports
{
    public partial class SalesStatement : Form
    {
        private string CustomerID;
        private bool CanView = false;
        public SalesStatement()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
            }
        }

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                CustomerID = lProfile[0];
                this.customerText.Text = lProfile[2];

            }
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {
                string sql = @"Select GrandTotal, Name, SalesNumber ,TransactionDate,TotalPaid, Memo,Street,City ,State , Postcode , Country , p.ABN
                            From Sales s
                            INNER JOIN Profile p ON s.CustomerID = p.ID
                            INNER JOIN Contacts c ON p.ID = c.ProfileID
                             WHERE InvoiceStatus = 'Open' AND CustomerID=" + CustomerID + " AND c.Location = p.LocationID";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);

                Reports.ReportParams SalesStatement = new Reports.ReportParams();
                SalesStatement.PrtOpt = 1;
                SalesStatement.Rec.Add(TbRep);
                SalesStatement.ReportName = "SalesStatements.rpt";
                SalesStatement.RptTitle = "Sales Statement";


                CommonClass.ShowReport(SalesStatement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.customerText.Text != "")
            {
                LoadReport();
            }
         
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customerText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SalesStatement_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
