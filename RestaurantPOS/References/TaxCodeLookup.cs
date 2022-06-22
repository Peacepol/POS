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
    public partial class TaxCodeLookup : Form
    {
        private string[] Tax;
        private string TaxSearch;
        public TaxCodeLookup(string pTaxSearch = "")
        {
            InitializeComponent();
            TaxSearch = pTaxSearch;
        }

        public string[] GetTax
        {
            get { return Tax; }
        }

        private void selectTaxCodeList_Load(object sender, EventArgs e)
        {
            TaxSearch_tb.Text = TaxSearch;
            LoadTax();
        }
        void LoadTax(bool isAuto = true)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT 
                                       TaxCode,	
                                       TaxCodeDescription,	
                                       TaxCollectedAccountID, 
                                       TaxPaidAccountID, 
                                       TaxPercentageRate 
                                   FROM TaxCodes";
                //WHERE (TaxCollectedAccountID IS NOT NULL 
                //AND TaxPaidAccountID IS NOT NULL
                //AND TaxCollectedAccountID <> 0
                //AND TaxPaidAccountID <> 0)
                //OR TaxCode = 'N-T'";

                string sqlcon = "";

                if (TaxSearch != "")
                {
                    sqlcon = " Where TaxCode LIKE '%" + TaxSearch_tb.Text + "%'";
                }
                selectSql += sqlcon;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                if (TaxSearch != "" && dt.Rows.Count == 0)
                {
                    TaxSearch = "";
                    LoadTax();
                }
                else if (dt.Rows.Count == 1 && isAuto)
                {
                    DataRow dr = dt.Rows[0];
                    Tax = new string[5];
                    Tax[0] = dr["TaxCode"].ToString();
                    Tax[1] = dr["TaxCodeDescription"].ToString();
                    Tax[2] = dr["TaxPercentageRate"].ToString();
                    Tax[3] = dr["TaxCollectedAccountID"].ToString();
                    Tax[4] = dr["TaxPaidAccountID"].ToString();
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    listView1.Items.Clear();
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        ListViewItem listitem = new ListViewItem(dr["TaxCode"].ToString());
                        listitem.SubItems.Add(dr["TaxCodeDescription"].ToString());
                        listitem.SubItems.Add(dr["TaxPercentageRate"].ToString());
                        listitem.SubItems.Add(dr["TaxCollectedAccountID"].ToString());
                        listitem.SubItems.Add(dr["TaxPaidAccountID"].ToString());
                        listView1.Items.Add(listitem);
                    }
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
            if(listView1.SelectedItems[0].SubItems[0].Text != "" )
            {                 
                Tax = new string[5];
                Tax[0] = listView1.SelectedItems[0].SubItems[0].Text;
                Tax[1] = listView1.SelectedItems[0].SubItems[1].Text;
                Tax[2] = listView1.SelectedItems[0].SubItems[2].Text;
                Tax[3] = listView1.SelectedItems[0].SubItems[3].Text;
                Tax[4] = listView1.SelectedItems[0].SubItems[4].Text;
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void TaxSearch_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                TaxSearch = TaxSearch_tb.Text;
                LoadTax(false);
            }
        }
    }
}
