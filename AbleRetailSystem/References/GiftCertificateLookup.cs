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
    public partial class GiftCertificateLookup : Form
    {
        private string mGCSearch;
        public GiftCertificateLookup(string pGCSearch = "")
        {
            InitializeComponent();
            mGCSearch = pGCSearch;
        }

        private void selectTaxCodeList_Load(object sender, EventArgs e)
        {
            txtSearch.Text = mGCSearch;
            LoadGC();
        }

        void LoadGC()
        {
            string selectSql = @"SELECT 
                                    ItemID,	
                                    GCAmount,	
                                    GCNumber, 
                                    StartDate, 
                                    EndDate,
                                    IsUsed
                                FROM GiftCertificate";

            string sqlcon = "";

            if (mGCSearch != "")
            {
                sqlcon = " WHERE GCNumber LIKE @GCNumber";
            }

            selectSql += sqlcon;

            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@GCNumber", "%" + txtSearch.Text + "%");

            CommonClass.runSql(ref dt, selectSql, param);

            lstGiftCertificate.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                string lUsed = dr["IsUsed"].ToString() == "False" ? "No" : "Yes";
                ListViewItem listitem = new ListViewItem(dr["ItemID"].ToString());
                listitem.SubItems.Add(dr["GCNumber"].ToString());
                listitem.SubItems.Add(dr["GCAmount"].ToString());
                listitem.SubItems.Add(dr["StartDate"].ToString());
                listitem.SubItems.Add(dr["EndDate"].ToString());
                listitem.SubItems.Add(lUsed);
                lstGiftCertificate.Items.Add(listitem);
            }

            lstGiftCertificate.View = View.Details;
            for (int x = 0; x <= lstGiftCertificate.Items.Count - 1; x++)
            {
                if (lstGiftCertificate.Items[x].Index % 2 == 0)
                {
                    lstGiftCertificate.Items[x].BackColor = System.Drawing.ColorTranslator.FromHtml("#ebf5ff");
                }
                else
                {
                    lstGiftCertificate.Items[x].BackColor = Color.White;
                }
            }
        }
    
        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = lstGiftCertificate.Columns[e.ColumnIndex].Width;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                mGCSearch = txtSearch.Text;
                LoadGC();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
