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

namespace RestaurantPOS.Utilities
{
    public partial class FormLookup : Form
    {
        private string[] FormDetail;
        public FormLookup()
        {
            InitializeComponent();
        }
        public string[] GetFormDetail
        {
            get { return FormDetail; }
        }
        private void FormLookup_Load(object sender, EventArgs e)
        {
            LoadDg();
        }

        void LoadDg()
        {
            dgvFormList.Rows.Clear();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT
                                   form_code, form_name
                                   FROM Forms";
                SqlCommand cmd_ = new SqlCommand(selectSql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgvFormList.Rows.Add();
                    dgvFormList.Rows[x].Cells[0].Value = dr["form_code"].ToString();
                    dgvFormList.Rows[x].Cells[1].Value = dr["form_name"].ToString();
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void dgvFormList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvFormList.Rows[e.RowIndex].Selected = true;
            FormDetail = new string[2];
            FormDetail[0] = dgvFormList.Rows[e.RowIndex].Cells[0].Value.ToString();
            FormDetail[1] = dgvFormList.Rows[e.RowIndex].Cells[1].Value.ToString();
            DialogResult = DialogResult.OK;
        }

        private void txtFormSearh_TextChanged(object sender, EventArgs e)
        {
            if(txtFormSearh.Text == "")
            {
                LoadDg();
            }
            else
            {
                LoadSearch();
            }
        }
        void LoadSearch()
        {
            dgvFormList.Rows.Clear();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT 
                                   form_code, form_name 
                                   FROM Forms
                                   WHERE form_code LIKE '" + txtFormSearh.Text + "%'";
                selectSql += " OR form_name LIKE '" + txtFormSearh.Text + "%'";
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(selectSql, con);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgvFormList.Rows.Add();
                    dgvFormList.Rows[x].Cells[0].Value = dr["form_code"].ToString();
                    dgvFormList.Rows[x].Cells[1].Value = dr["form_name"].ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
    }
}
