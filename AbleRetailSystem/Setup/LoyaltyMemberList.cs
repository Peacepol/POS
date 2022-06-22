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
    public partial class LoyaltyMemberList : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        private string selectedProfileID = ""; 

        public LoyaltyMemberList()
        {
            InitializeComponent();

            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            FormRights.TryGetValue("Add", out outx);
            if (outx == true)
            {
                CanAdd = true;
            }
            outx = false;
            FormRights.TryGetValue("Edit", out outx);
            if (outx == true)
            {
                CanEdit = true;
            }
            outx = false;
            FormRights.TryGetValue("Delete", out outx);
            if (outx == true)
            {
                CanDelete = true;
            }
          
            string outy = "";
            CommonClass.AppFormCode.TryGetValue(this.Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadMembers();
        }

        private void LoadMembers(string pSearch = "", bool activeOnly = false)
        {
            listView1.Items.Clear();

            string selectSql = @"SELECT * FROM LoyaltyMember";
            string sqlcon = "";

            if (this.rdoName.Checked)
            {
                sqlcon = " WHERE Name LIKE '%" + this.txtsearch.Text + "%'";
            }
            if (this.rdoNumber.Checked)
            {
                sqlcon = " WHERE Number LIKE '%" + this.txtsearch.Text + "%'";
            }
            if (this.rdoDate.Checked)
            {
                sqlcon = " WHERE CONVERT(varchar(30), StartDate, 120) LIKE '%" + dtStartDate.Value.ToString("yyyy-MM-dd") + "%' AND CONVERT(varchar(30), EndDate, 120) LIKE '%" + dtEndDate.Value.ToString("yyyy-MM-dd") + "%'";
            }
            if (this.rdoCity.Checked)
            {
                sqlcon = " WHERE City LIKE '%" + this.txtsearch.Text + "%'";
            }
            if (this.rdoState.Checked)
            {
                sqlcon = " WHERE State LIKE '%" + this.txtsearch.Text + "%'";
            }
            if (this.rdoPostCode.Checked)
            {
                sqlcon = " WHERE Postcode LIKE '%" + this.txtsearch.Text + "%'";
            }
            if (this.rdoCountry.Checked)
            {
                sqlcon = " WHERE Country LIKE '%" + this.txtsearch.Text + "%'";
            }
            if (this.rdoLoyaltyNum.Checked)
            {
                sqlcon = " WHERE Number LIKE '%" + this.txtsearch.Text + "%'";
            }
            if (pSearch == "Active")
            {
                if (chkActive.Checked)
                {
                    sqlcon = " WHERE IsActive = 1";
                } else
                {
                    sqlcon = " WHERE IsActive = 0";
                }
            }

            selectSql += sqlcon;

            DataTable dt = new DataTable();

            CommonClass.runSql(ref dt, selectSql);

            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem listitem = new ListViewItem(dr["Number"].ToString());
                listitem.SubItems.Add(dr["Name"].ToString());
                listitem.SubItems.Add(dr["City"].ToString());
                listitem.SubItems.Add(dr["State"].ToString());
                listitem.SubItems.Add(dr["PostCode"].ToString());
                listitem.SubItems.Add(dr["Country"].ToString());
                DateTime sdate = Convert.ToDateTime(dr["StartDate"]);
                DateTime edate = Convert.ToDateTime(dr["EndDate"]);
                listitem.SubItems.Add(sdate.ToString("yyyy - MM - dd"));
                listitem.SubItems.Add(edate.ToString("yyyy - MM - dd"));
                listitem.SubItems.Add(dr["IsActive"].ToString()=="0"? "No" : "Yes");
                listitem.SubItems.Add(dr["ID"].ToString());
                listView1.Items.Add(listitem);
            }

            listView1.View = View.Details;
            for (int x = 0; x <= listView1.Items.Count - 1; ++x)
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
            if(dt.Rows.Count == 0)
            {
                this.btnDelete.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMembers(this.txtsearch.Text);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            selectedProfileID = listView1.SelectedItems[0].SubItems[9].Text;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            LoyaltyMemberDetail MemberDetailsFrm = new LoyaltyMemberDetail(this.Text,"", 2, CanAdd);
            if (MemberDetailsFrm.ShowDialog() == DialogResult.OK)
            {
                LoadMembers(txtsearch.Text);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deletesql = "DELETE FROM LoyaltyMember WHERE ID = " + selectedProfileID;

            int rowsaffected = CommonClass.runSql(deletesql);

            if (rowsaffected > 0)
            {
                MessageBox.Show("Record deleted successfully");
                for (int i = 0; i < listView1.Items.Count; ++i)
                {
                    if (listView1.Items[i].Selected)
                    {
                        listView1.Items[i].Remove();
                        i--;
                    }
                }

                listView1.Refresh();
            }

            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadMembers();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            LoyaltyMemberDetail MemberDetailsFrm = new LoyaltyMemberDetail(this.Text,selectedProfileID, 1, CanEdit);
            if (MemberDetailsFrm.ShowDialog() == DialogResult.OK)
            {
                LoadMembers(txtsearch.Text);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadMembers();
            this.txtsearch.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdoDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDate.Checked)
            {
                dtStartDate.Enabled = true;
                dtEndDate.Enabled = true;
                txtsearch.Enabled = false;
            }
            else
            {
                dtStartDate.Enabled = false;
                dtEndDate.Enabled = false;
                txtsearch.Enabled = true;
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadMembers("Active", chkActive.Checked);
        }
    }
}
