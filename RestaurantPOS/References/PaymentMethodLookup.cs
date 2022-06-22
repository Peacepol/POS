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
    public partial class PaymentMethodLookup : Form
    {
        private string[] PaymentMethod;
        public PaymentMethodLookup()
        {
            InitializeComponent();          
        }

        public string[] GetPaymentMethod
        {
            get { return PaymentMethod; }
        }   

        private void selectPaymentMethodList_Load(object sender, EventArgs e)
        {         
            listView1.Items.Clear();

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT PaymentMethod, id, GLAccountCode FROM PaymentMethods";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["PaymentMethod"].ToString());                    
                    listitem.SubItems.Add(dr["id"].ToString());
                    listitem.SubItems.Add(dr["GLAccountCode"].ToString());

                    listView1.Items.Add(listitem);
                }

                listView1.View = View.Details;
                for (int x = 0; x <= listView1.Items.Count - 1; x++)
                {
                    if (listView1.Items[x].Index % 2 == 0)
                    {
                        listView1.Items[x].BackColor = System.Drawing.ColorTranslator.FromHtml("#ebf5ff");
                    }
                    else
                    {
                        listView1.Items[x].BackColor = Color.White;
                    }
                }
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

        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            PaymentMethod = new string[15];
            PaymentMethod[0] = listView1.SelectedItems[0].SubItems[0].Text;//PaymentMethod
            PaymentMethod[1] = listView1.SelectedItems[0].SubItems[1].Text;//id
            PaymentMethod[2] = listView1.SelectedItems[0].SubItems[2].Text;//GL-Account Code

            DialogResult = DialogResult.OK;               
        }

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }
    }
}
