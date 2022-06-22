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
using System.Configuration;
using System.Security.Cryptography;

namespace AbleRetailPOS
{
    public partial class SalespersonLookup : Form
    {
        private string[] Salesperson;
        public SalespersonLookup()
        {
            InitializeComponent();
            
        }

        public string[] GetSalesperson
        {
            get { return Salesperson; }
        }

        private void selectShippingMethodList_Load(object sender, EventArgs e)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT user_id, user_name, user_fullname FROM Users";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                this.dgriduser.DataSource = dt;
                this.dgriduser.Columns[0].Visible = false;
                this.dgriduser.Columns[1].HeaderText = "Username";
                this.dgriduser.Columns[2].HeaderText = "Salesperson";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }    

      

        private void dgriduser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Salesperson = new string[2];
            Salesperson[0] = dgriduser.CurrentRow.Cells[0].Value.ToString();
            Salesperson[1] = dgriduser.CurrentRow.Cells[1].Value.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
