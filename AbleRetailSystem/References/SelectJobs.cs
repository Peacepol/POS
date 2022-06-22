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
    public partial class SelectJobs : Form
    {
        private string[] Job;
        private string JType;
        private string JobSearch;
        public SelectJobs(string pType = "D" ,string pJobSearch = "" )
        {
            InitializeComponent();
            JType = pType;
            JobSearch = pJobSearch;
            this.JobSearch_tb.Text = pJobSearch;
        }

        public string[] GetJob
        {
            get { return Job; }
        }

        private void SelectJobs_Load(object sender, EventArgs e)
        {
            LoadJob();
        }

        void LoadJob(bool isAuto = true)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM Jobs where IsHeader = '" + JType + "'";
                string sqlext = "";
                if (JobSearch != "")
                {
                    sqlext = " AND JobName LIKE '" + this.JobSearch_tb.Text + "%'";               
                }
                selectSql += sqlext;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (JobSearch != "" && dt.Rows.Count == 0)
                {
                    JobSearch = "";
                    LoadJob();
                }

                if (dt.Rows.Count == 1 && isAuto)
                {
                    DataRow dr = dt.Rows[0];
                    Job = new string[5];
                    Job[0] = dr["JobID"].ToString();
                    Job[1] = dr["JobCode"].ToString();
                    Job[2] = dr["JobName"].ToString();
                    Job[3] = dr["StartDate"].ToString();
                    Job[4] = dr["FinishDate"].ToString();
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    listView1.Items.Clear();
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        DataRow dr = dt.Rows[x];
                        ListViewItem listitem = new ListViewItem(dr["JobID"].ToString());
                        listitem.SubItems.Add(dr["JobCode"].ToString());
                        listitem.SubItems.Add(dr["JobName"].ToString());
                        listitem.SubItems.Add(dr["StartDate"].ToString());
                        listitem.SubItems.Add(dr["FinishDate"].ToString());
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

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[0].Text != "")
            {
                Job = new string[5];
                Job[0] = listView1.SelectedItems[0].SubItems[0].Text;
                Job[1] = listView1.SelectedItems[0].SubItems[1].Text;
                Job[2] = listView1.SelectedItems[0].SubItems[2].Text;
                Job[3] = listView1.SelectedItems[0].SubItems[3].Text;
                Job[4] = listView1.SelectedItems[0].SubItems[4].Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void JobSearch_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                JobSearch = JobSearch_tb.Text;
                LoadJob(false);
            }
        }
    }
}
