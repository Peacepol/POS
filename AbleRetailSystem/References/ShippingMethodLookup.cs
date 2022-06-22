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

namespace RestaurantPOS
{
    public partial class ShippingMethodLookup : Form
    {
        private string[] ShippingMethod;
        public ShippingMethodLookup()
        {
            InitializeComponent();
            
        }

        public string[] GetShippingMethod
        {
            get { return ShippingMethod; }
        }

        private void selectShippingMethodList_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM ShippingMethods";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["ShippingMethod"].ToString());
                    listitem.SubItems.Add(dr["ShippingID"].ToString());
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

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ShippingMethod = new string[2];
            ShippingMethod[0] = listView1.SelectedItems[0].SubItems[0].Text;
            ShippingMethod[1] = listView1.SelectedItems[0].SubItems[1].Text;
            DialogResult = DialogResult.OK;
        }

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }
    }
}
