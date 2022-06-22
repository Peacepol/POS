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
    public partial class List3Lookup : Form
    {
        private string[] List3;
        private string RecType;
        public List3Lookup(string pRecType = "Items")
        {
            InitializeComponent();
            RecType = pRecType;
            this.lblNote.Text = RecType + " Custom List 3";
        }

        public string[] GetList3
        {
            get { return List3; }
        }   

        private void List3Lookup_Load(object sender, EventArgs e)
        {         
            listView1.Items.Clear();

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomList3 where RecordType = '" + RecType  + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["List3Name"].ToString());
                    listitem.SubItems.Add(dr["id"].ToString());
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
            List3 = new string[2];

            List3[0] = listView1.SelectedItems[0].SubItems[0].Text;
            List3[1] = listView1.SelectedItems[0].SubItems[1].Text;
            DialogResult = DialogResult.OK;               
        }

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }
    }
}
