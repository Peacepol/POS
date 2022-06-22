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
using System.Text.RegularExpressions;

namespace AbleRetailPOS
{
    public partial class CreateShippingMethod : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        private DataTable dt; 
        public CreateShippingMethod()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count() > 0)
            {
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
			}

            string outy = "";
            CommonClass.AppFormCode.TryGetValue(Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = Text;
            }
        }
       
        void New_()
        {
            SqlConnection con = null;
            try
            {
                if (txtShippingMethod.Text == "")
                {
                    MessageBox.Show("Required fields are missing.");
                }
                else
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("INSERT INTO ShippingMethods (ShippingMethod) VALUES (@txt1)", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@txt1", txtShippingMethod.Text);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();

                    if (i != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Shipping Method " + txtShippingMethod.Text, txtShippingMethod.Text);
                        string titles = "INFORMATION";
                        MessageBox.Show("Shipping Method Record has been created.", titles);
                        LoadShippingMethod(txtShippingMethod.Text);
                    }                  
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
			finally
            {
                if (con != null)
                    con.Close();
            }
        }
       
        private void LoadShippingMethod(string pSelected = "")
        {
            int SelIndex = 0;
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

                btnDelete.Enabled = CanDelete;
                btnSave.Enabled = CanEdit;
                btnAddNew.Enabled = CanAdd;
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
	                if (pSelected == listView1.Items[x].SubItems[0].Text)
	                {
	                    SelIndex = x;
	                }
            	}

	            if (pSelected == "" && listView1.Items.Count > 0)
	            {
	                listView1.Items[SelIndex].Selected = true;
	                txtShippingMethod.Text = listView1.SelectedItems[0].SubItems[0].Text;

	                SelIndex = 0;
	            }

	            listView1.Focus();
	            listView1.Items[SelIndex].Selected = true;
	            btnSave.Enabled = false;
	            btnAddNew.Enabled = CanAdd;
	            btnDelete.Enabled = CanDelete;
	            txtShippingMethod.ReadOnly = true;
	            if (CanDelete)
	            {
	                btnDelete.Enabled = EnableDelete(txtShippingMethod.Text);
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
        private void formload()
        {
            LoadShippingMethod();
            listView1.Enabled = true;
            txtShippingMethod.ReadOnly = true;
            txtShippingMethod.Text = "";
            if (listView1.Items.Count == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnAddNew.Enabled = CanAdd;
            }
            else
            {
                btnEdit.Enabled = CanEdit;
                btnDelete.Enabled = CanDelete;               
                btnAddNew.Enabled = CanAdd;
                btnSave.Enabled = false;
            }
           
        }
        private void CreateShippingMethod_Load(object sender, EventArgs e)
        {
            formload();
        }  

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }    

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtShippingMethod.Text == "")
            {
                MessageBox.Show("Please Select Shipping Method");
            }
            else
            {
                SqlConnection con = null;
                string sql = @"SELECT ShippingMethodID FROM Profile
                            WHERE ShippingMethodID = '" + ID.Text + "'";
                sql += @" UNION SELECT ShippingMethodID FROM Sales
                            WHERE ShippingMethodID = '" + ID.Text + "'";
                sql += @" UNION SELECT ShippingMethodID FROM Purchases
                            WHERE ShippingMethodID = '" + ID.Text + "'";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand scmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = scmd;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Shipping method can not be deleted due to an existing transaction or used as a default shipping method of a profile.");
                }
                else
                {
                    try
                    {
                        string titles = "Delete Record";
                        DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            con = new SqlConnection(CommonClass.ConStr);
                            SqlCommand cmd = new SqlCommand("DELETE FROM shippingMethods WHERE shippingMethod = '" + txtShippingMethod.Text + "'", con);

                            con.Open();

                            int rowsaffected = cmd.ExecuteNonQuery();

                            if (rowsaffected != 0)
                            {
                                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Deleted Shipping Method " + txtShippingMethod.Text, txtShippingMethod.Text);
                                MessageBox.Show("Shipping Method Record has been deleted.", titles);
                                LoadShippingMethod();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (con != null)
                            con.Close();
                    }
                    btnRefresh.PerformClick();
                }
            }            
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string id = ID.Text;
            string name = txtShippingMethod.Text;
            if (ID.Text == "")
            {
                if (!IsDuplicate(txtShippingMethod.Text))
                {
                    New_();
                    btnRefresh.PerformClick();
                }
                else
                {
                    MessageBox.Show("Record Already exists.");
                    btnRefresh.PerformClick();
                }
            }
            else
            {
                UpdateList(id, name);
                btnRefresh.PerformClick();
            }
            
        }
        void UpdateList(string pID, string pName)
        {

            pName = this.txtShippingMethod.Text;

            SqlConnection con = null;
            try
            {
                string titles = "Update Shipping Method Record for " + pName;
                DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("Update shippingMethods set shippingMethod = @ShippingMethod where ShippingID = " + pID, con);
                 
                    cmd.Parameters.AddWithValue("@ShippingMethod", pName);
                    con.Open();

                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Shipping Record " + pName + " ID " + pID, pName);
                        MessageBox.Show("Record has been updated.", "INFORMATION");
                        btnRefresh.PerformClick();
                    }
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private bool IsDuplicate(string pShippingMethod)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM ShippingMethods WHERE shippingmethod = @ShippingMethod";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();
                cmd_.Parameters.AddWithValue("@ShippingMethod", pShippingMethod);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            txtShippingMethod.ReadOnly = false;
        	txtShippingMethod.Text = listView1.SelectedItems[0].SubItems[0].Text;
            ID.Text = listView1.SelectedItems[0].SubItems[1].Text;
            btnEdit.PerformClick();
        }
		
        private static bool EnableDelete(string pMethod)
        {
            return true;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtShippingMethod.Text = "";
            txtShippingMethod.ReadOnly = false;
            this.btnAddNew.Enabled = false;            
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.listView1.Enabled = false;
            this.btnSave.Enabled = true;
            ID.Text = "";
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            formload();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = false;            
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = true;

            listView1.Enabled = true;
            this.btnSave.Enabled = true;
            this.txtShippingMethod.Enabled = true;
        }
    } //end
}
