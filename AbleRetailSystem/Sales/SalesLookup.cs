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
    public partial class SalesLookup : Form
    {
        private string[] Sales;
        private int ProfileID;
     
        public SalesLookup(int pProfileID)
        {
            ProfileID = pProfileID;
            InitializeComponent();
        }

        public string[] GetSales
        {
            get { return Sales; }
        }

        private void selectSalesLookupList_Load(object sender, EventArgs e)
        {
                string selectSql = @"SELECT 
                                       s.SalesNumber,	
                                       s.SalesType,	
                                       s.InvoiceType, 
                                       s.InvoiceStatus, 
                                       p.Name AS CustomerName
                                   FROM Sales s 
                                   INNER JOIN Profile p ON s.CustomerID = p.ID
                                   WHERE p.ID = @profileid";
                DataTable dt = new DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@profileid", ProfileID);

                CommonClass.runSql(ref dt, selectSql, param);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["SalesNumber"].ToString());
                    listitem.SubItems.Add(dr["CustomerName"].ToString());
                    listitem.SubItems.Add(dr["SalesType"].ToString());
                    listitem.SubItems.Add(dr["InvoiceType"].ToString());
                    listitem.SubItems.Add(dr["InvoiceStatus"].ToString());
                    lvSales.Items.Add(listitem);
                }

                lvSales.View = View.Details;
                for (int x = 0; x <= lvSales.Items.Count - 1; x++)
                {
                    if (lvSales.Items[x].Index % 2 == 0)
                    {
                        lvSales.Items[x].BackColor = System.Drawing.ColorTranslator.FromHtml("#ebf5ff");
                    }
                    else
                    {
                        lvSales.Items[x].BackColor = Color.White;
                    }
                }
        }

        private void lvSales_DoubleClick(object sender, EventArgs e)
        {
            if(lvSales.SelectedItems[0].SubItems[0].Text != "" )
            {                 
                Sales = new string[5];
                Sales[0] = lvSales.SelectedItems[0].SubItems[0].Text;
                Sales[1] = lvSales.SelectedItems[0].SubItems[1].Text;
                Sales[2] = lvSales.SelectedItems[0].SubItems[2].Text;
                Sales[3] = lvSales.SelectedItems[0].SubItems[3].Text;
                Sales[4] = lvSales.SelectedItems[0].SubItems[4].Text;
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void lvSales_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = lvSales.Columns[e.ColumnIndex].Width;
        }      
    }
}
