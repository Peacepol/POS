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
    public partial class CustomerList : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        private string selectedProfileID = ""; 

        public CustomerList()
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
            LoadCustomers();
        }

        private void LoadCustomers(string pSearch = "", bool activeOnly = false)
        {
            listView1.Items.Clear();

                string selectSql = @"  SELECT p.ID, p.ProfileIDNumber, p.Name, p.IsInactive, p.CurrentBalance, c.Email, c.Phone , l.Number 
                                            FROM Profile p
                                            Inner Join Contacts c ON p.LocationID = c.Location 
                                            LEFT JOIN LoyaltyMember l ON p.ID = l.ProfileID 
                                            WHERE c.ProfileID = p.ID AND p.Type ='Customer'";

                string sqlcon = "";
                if (pSearch != "")
                {
                    if (this.rdoName.Checked)
                    {
                        sqlcon = " AND p.Name LIKE @textSearch";
                    }
                    if (this.rdoID.Checked)
                    {
                        sqlcon = " AND p.ProfileIDNumber LIKE @textSearch";
                    }
                    if (this.rdoPhone.Checked)
                    {
                        sqlcon = " AND c.Phone LIKE @textSearch";
                    }
                    if (this.rdoEmail.Checked)
                    {
                        sqlcon = " AND c.Email LIKE @textSearch";
                    }
                    if (this.rdoCity.Checked)
                    {
                        sqlcon = " AND c.City LIKE @textSearch";
                    }
                    if (this.rdoState.Checked)
                    {
                        sqlcon = " AND c.State LIKE @textSearch";
                    }
                    if (this.rdoPostCode.Checked)
                    {
                        sqlcon = " AND c.Postcode LIKE @textSearch";
                    }
                    if (this.rdoCountry.Checked)
                    {
                        sqlcon = " AND c.Country LIKE @textSearch";
                    }
                    if (this.rdoLoyal.Checked)
                    {
                        sqlcon = " AND l.Number LIKE @textSearch";
                    }
                    if (pSearch == "Active")
                    {
                        if (chkActive.Checked)
                        {
                            sqlcon = " AND IsInactive = '0'";
                        }
                        else
                        {
                            sqlcon = " AND IsInactive = '1'";
                        }
                    }
                }
                selectSql += sqlcon;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@textSearch", "%" + txtsearch.Text + "%");
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, selectSql,param);


                decimal lCBal;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["Name"].ToString());
                    listitem.SubItems.Add(dr["ProfileIDNumber"].ToString());
                    listitem.SubItems.Add(dr["IsInactive"].ToString()== "0"? "Yes" : "No");
                    listitem.SubItems.Add(dr["Phone"].ToString());
                    listitem.SubItems.Add(dr["Email"].ToString());
                    lCBal = dr["CurrentBalance"].ToString() == "" ? 0 : Convert.ToDecimal(dr["CurrentBalance"].ToString());
                    listitem.SubItems.Add(Math.Round(lCBal, 2).ToString("C"));
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
            LoadCustomers(this.txtsearch.Text);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            selectedProfileID = listView1.SelectedItems[0].SubItems[6].Text;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Customer CustomerDetailsFrm = new Customer("", thisFormCode, 2, CanAdd);
            if (CustomerDetailsFrm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomers(this.txtsearch.Text);
            }
        }
        private static bool IsTranNothing(String pID)
        { DataTable dx = new DataTable();

            if (pID == "")
            {
                MessageBox.Show("Select a Customer!");
                pID = "0";
            }
            string selectSqla = "SELECT CustomerID as ProfileID FROM Sales WHERE CustomerID = " + pID;

                CommonClass.runSql(ref dx, selectSqla);
               
                    if (dx.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsTranNothing(selectedProfileID))
            {
                if (selectedProfileID != "")
                {
                    string deletesql = "DELETE FROM Profile WHERE ID = " + selectedProfileID;
                    deletesql += " DELETE FROM Contacts WHERE ProfileID = " + selectedProfileID;

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
                }             
            }
            else
            {
                MessageBox.Show("There are transactions created on this Account already. You can not delete this Customer Account.");
            }
            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadCustomers();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            //this.txtsearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            //this.txtsearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();

            SqlConnection con_ = null;
            con_ = new SqlConnection(CommonClass.ConStr);
            string selectSql = @"SELECT ID, ProfileIDNumber, Name, IsInactive, Phone, Email, CurrentBalance 
                                FROM Profile p INNER JOIN Contacts c ON p.LocationID = c.Location
                                WHERE c.ProfileID = p.ID AND type = 'Customer'";
            SqlCommand cmd_ = new SqlCommand(selectSql, con_);
            con_.Open();

            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            da.SelectCommand = cmd_;
            da.Fill(dt);
            decimal lCBal;
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow dr = dt.Rows[x];

                list.Add(dr["Name"].ToString());
                list.Add(dr["ProfileIDNumber"].ToString());
                list.Add(dr["IsInactive"].ToString());
                list.Add(dr["Phone"].ToString());
                list.Add(dr["Email"].ToString());
                lCBal = dr["CurrentBalance"].ToString() == "" ? 0 : Convert.ToDecimal(dr["CurrentBalance"].ToString());
                list.Add(Math.Round(lCBal, 2).ToString("C"));
                list.Add(dr["ID"].ToString());
            }
            //this.txtsearch.AutoCompleteCustomSource = list;
            //this.txtsearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
           /* Customer CustomerDetailsFrm = new Customer(selectedProfileID, thisFormCode, 1, CanEdit);
            if (CustomerDetailsFrm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomers(this.txtsearch.Text);
            }
           */
            OpenCustomerForm(selectedProfileID, thisFormCode, 1, CanEdit);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadCustomers();
            this.txtsearch.Text = "";
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadCustomers("Active", chkActive.Checked);
        }

        private void OpenCustomerForm(string pID, string pFormCode, int pMode = 0, bool pEdit = false)
        {

            Customer CustomerDetailsFrm = new Customer(pID, pFormCode, pMode, pEdit);
            this.Cursor = Cursors.WaitCursor;
            CustomerDetailsFrm.MdiParent = this.MdiParent;
            CustomerDetailsFrm.Show();
            CustomerDetailsFrm.Focus();
            if (CustomerDetailsFrm.DialogResult == DialogResult.Cancel)
            {
                CustomerDetailsFrm.Close();
            }
            if (CustomerDetailsFrm.DialogResult == DialogResult.OK)
            {
                LoadCustomers();
            }
            this.Cursor = Cursors.Default;
        }
    }
}
