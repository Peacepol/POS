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

namespace RestaurantPOS.References
{
    public partial class ReferralLookup : Form
    {
        public ReferralLookup()
        {
            InitializeComponent();
        }

        private void ReferralLookup_Load(object sender, EventArgs e)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT user_id, user_name, user_fullname FROM Users";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                this.dgridreferral.DataSource = dt;
                this.dgridreferral.Columns[0].Visible = false;
                this.dgridreferral.Columns[1].HeaderText = "Username";
                this.dgridreferral.Columns[1].HeaderText = "Salesperson";



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
    }
}

