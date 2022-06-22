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
    public partial class APBalances : Form
    {
        private bool CanAdd = false;
        public APBalances()
        {
            InitializeComponent();
        }

        private void APBalances_Load(object sender, EventArgs e)
        {
            LoadAPSuppliers();
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

        private void LoadAPSuppliers()
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
                           FROM Profile where Type = 'Supplier' and TermsOfPayment <> 'CASH' ";
               
                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                string[] lToAddRow;
               this.dgridSuppliers.Rows.Clear();
                foreach (DataRow drow in dt.Rows)
                {
                    lToAddRow = new string[4];
                    decimal lbal = drow["CurrentBalance"].ToString() != "" ? Convert.ToDecimal(drow["CurrentBalance"].ToString()) : 0;
                    lToAddRow[0] = drow["ID"].ToString();
                    lToAddRow[1] = drow["ProfileIDNumber"].ToString();
                    lToAddRow[2] = drow["Name"].ToString();
                    lToAddRow[3] = (Math.Round(lbal, 2).ToString("C"));
                    this.dgridSuppliers.Rows.Add(lToAddRow);

                }
                foreach (DataGridViewColumn column in dgridSuppliers.Columns)
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
            btnAddPurchase.Enabled = CanAdd;
        }

        private void btnAddSale_Click(object sender, EventArgs e)
        {
           if(dgridSuppliers.RowCount > 0)
            {
                int i = this.dgridSuppliers.CurrentRow.Index;
                if (i >= 0)
                {
                    string lProfileID = this.dgridSuppliers.Rows[i].Cells["ProfileID"].Value.ToString();
                    APBalanceEntry APDlg = new APBalanceEntry(this.Text, lProfileID);
                    if (APDlg.ShowDialog() == DialogResult.OK)
                    {
                        LoadAPSuppliers();
                    }
                }
            }
           else
            {
                MessageBox.Show("Please select a Customer", "Information");
            }
        }
    }
}
