using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace RestaurantPOS
{
    public partial class ARBalances : Form
    {
        private bool CanAdd = false;
        public ARBalances()
        {
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Add", out outx);
                if (outx == true)
                {
                    CanAdd = true;
                }
               
            }
        }

        private void ARBalances_Load(object sender, EventArgs e)
        {
            LoadARCustomers();
        }

        private void LoadARCustomers()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = @"SELECT ID,
                           ProfileIDNumber,
                           Name,
                           CurrentBalance
                           FROM Profile where Type = 'Customer' and TermsOfPayment <> 'CASH' ";
               
                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                string[] lToAddRow;
                this.dgridCustomers.Rows.Clear();
                foreach (DataRow drow in dt.Rows)
                {
                    lToAddRow = new string[4];
                    decimal lbal = drow["CurrentBalance"].ToString() != "" ? Convert.ToDecimal(drow["CurrentBalance"].ToString()) : 0;
                    lToAddRow[0] = drow["ID"].ToString();
                    lToAddRow[1] = drow["ProfileIDNumber"].ToString();
                    lToAddRow[2] = drow["Name"].ToString();
                    lToAddRow[3] = (Math.Round(lbal, 2).ToString("C"));
                    this.dgridCustomers.Rows.Add(lToAddRow);

                }

                foreach (DataGridViewColumn column in dgridCustomers.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            btnAddSale.Enabled = CanAdd;
        }

        private void btnAddSale_Click(object sender, EventArgs e)
        {
            if(dgridCustomers.RowCount > 0)
            {
                int i = this.dgridCustomers.CurrentRow.Index;
                if (i >= 0)
                {
                    string lProfileID = this.dgridCustomers.Rows[i].Cells["ProfileID"].Value.ToString();
                    ARBalanceEntry ARDlg = new ARBalanceEntry(this.Text, lProfileID);
                    if (ARDlg.ShowDialog() == DialogResult.OK)
                    {
                        LoadARCustomers();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a Customer ", "Information");
            }
           
        }
    }
}
