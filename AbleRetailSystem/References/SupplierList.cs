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
    public partial class SupplierList : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        private string selectedProfileID = "";
        public SupplierList()
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

        private void SupplierList_Load(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadSuppliers();
        }

        private void LoadSuppliers(string pSearch = "")
        {
            listView1.Items.Clear();
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = @"SELECT p.ID, p.ProfileIDNumber, p.Name, p.IsInactive, p.CurrentBalance, c.Email, c.Phone 
                                    FROM Profile p Inner Join Contacts c ON p.LocationID = c.Location
                                    WHERE c.ProfileID = p.ID AND p.Type = 'Supplier'";
                string sqlcon = "";
                if (pSearch != "")
                {
                    if (this.rdoName.Checked)
                    {
                        sqlcon = " AND Name LIKE '%" + this.txtsearch.Text + "%'";
                    }
                    if (this.rdoID.Checked)
                    {
                        sqlcon = " AND ProfileIDNumber LIKE '%" + this.txtsearch.Text + "%'";
                    }
                    if (this.rdoPhone.Checked)
                    {
                        sqlcon = " AND Phone LIKE '%" + this.txtsearch.Text + "%'";
                    }
                    if (this.rdoEmail.Checked)
                    {
                        sqlcon = " AND Email LIKE '%" + this.txtsearch.Text + "%'";
                    }
                    if (this.rdoCity.Checked)
                    {
                        sqlcon = " AND City LIKE '%" + this.txtsearch.Text + "%'";
                    }
                    if (this.rdoState.Checked)
                    {
                        sqlcon = " AND State LIKE '%" + this.txtsearch.Text + "%'";
                    }
                    if (this.rdoPostCode.Checked)
                    {
                        sqlcon = " AND Postcode LIKE '%" + this.txtsearch.Text + "%'";
                    }
                    if (this.rdoCountry.Checked)
                    {
                        sqlcon = " AND Country LIKE '%" + this.txtsearch.Text + "%'";
                    }
                }
                selectSql += sqlcon;
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
                    ListViewItem listitem = new ListViewItem(dr["Name"].ToString());
                    listitem.SubItems.Add(dr["ProfileIDNumber"].ToString());
                    listitem.SubItems.Add(dr["IsInactive"].ToString() == "0"?"Yes" : "No");
                    listitem.SubItems.Add(dr["Phone"].ToString());
                    listitem.SubItems.Add(dr["Email"].ToString());
                    lCBal = dr["CurrentBalance"].ToString() == "" ? 0 : Convert.ToDecimal(dr["CurrentBalance"].ToString());
                    listitem.SubItems.Add(Math.Round(lCBal, 2).ToString("C"));
                    listitem.SubItems.Add(dr["ID"].ToString());
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
                if(dt.Rows.Count == 0)
                {
                    btnDelete.Enabled = false;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSuppliers(this.txtsearch.Text);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            selectedProfileID = listView1.SelectedItems[0].SubItems[6].Text;

           
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Supplier SupplierDetailsFrm = new Supplier("", thisFormCode, 2, CanAdd);
            if (SupplierDetailsFrm.ShowDialog() == DialogResult.OK)
            {
                LoadSuppliers(this.txtsearch.Text);
            }
            listView1.Refresh();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            this.txtsearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtsearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();

            SqlConnection con_ = null;
            con_ = new SqlConnection(CommonClass.ConStr);
            string selectSql = @"SELECT ID, ProfileIDNumber, Name, IsInactive, Phone, Email, CurrentBalance 
                                FROM Profile p INNER JOIN Contacts c ON p.LocationID = c.Location
                                WHERE c.ProfileID = p.ID AND type = 'Supplier'";
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
            this.txtsearch.AutoCompleteCustomSource = list;
            this.txtsearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsTranNothing(selectedProfileID))
            {
                if (selectedProfileID != "")
                {
                    string deletesql = "DELETE FROM Profile WHERE ID = " + selectedProfileID;
                    deletesql += " DELETE FROM Contacts WHERE ProfileID = " + selectedProfileID;
                    SqlConnection con_ = null;
                    try
                    {
                        con_ = new SqlConnection(CommonClass.ConStr);
                        SqlCommand cmd = new SqlCommand(deletesql, con_);
                        con_.Open();
                        int rowsaffected = cmd.ExecuteNonQuery();

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
            }
            else
            {
                MessageBox.Show("There are transactions created on this Account already. You can not delete this Supplier Account.");
            }
            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadSuppliers();
        }
        private static bool IsTranNothing(String pID)
        {
            SqlConnection cona = null;
            try
            {
                cona = new SqlConnection(CommonClass.ConStr);
                if (pID == "")
                {
                    MessageBox.Show("Select a Supplier!");
                    pID = "0";
                }
                string selectSqla = "SELECT SupplierID as ProfileID FROM Purchases WHERE SupplierID = " + pID;
                SqlCommand cmda = new SqlCommand(selectSqla, cona);
                cona.Open();

                using (SqlDataReader reader = cmda.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (cona != null)
                    cona.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = CanAdd;
            this.btnDelete.Enabled = CanDelete;
            LoadSuppliers();
            this.txtsearch.Text = "";
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Supplier SupplierDetailsFrm = new Supplier(selectedProfileID, thisFormCode, 1, CanEdit);
            if (SupplierDetailsFrm.ShowDialog() == DialogResult.OK)
            {
                LoadSuppliers(this.txtsearch.Text);
            }
        }
    }
}
