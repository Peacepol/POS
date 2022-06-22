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
    public partial class CustomList1 : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        
        public CustomList1()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
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

        private void LoadList1Suppliers(string pSelected = "")
        {
            int SelIndex = 0;
            lvSup.Items.Clear();
            
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomList1 where RecordType = 'Suppliers'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["List1Name"].ToString());
                    listitem.SubItems.Add(dr["id"].ToString());
                    listitem.SubItems.Add(dr["RecordType"].ToString());
                    lvSup.Items.Add(listitem);
                }

                lvSup.View = View.Details;
                for (int x = 0; x <= lvSup.Items.Count - 1; x++)
                {
                    if (lvSup.Items[x].Index % 2 == 0)
                    {
                        lvSup.Items[x].BackColor = System.Drawing.ColorTranslator.FromHtml("#ebf5ff");
                    }
                    else
                    {
                        lvSup.Items[x].BackColor = Color.White;
                    }
                    if (pSelected == lvSup.Items[x].SubItems[0].Text)
                    {
                        SelIndex = x;
                    }
                }

                if (lvSup.Items.Count > 0)
                {
                    lvSup.Items[SelIndex].Selected = true;                   
                }

                if (lvSup.SelectedItems.Count > 0)
                {
                    txtSupListName.Text = lvSup.SelectedItems[0].SubItems[0].Text;
                    lblSupListID.Text = lvSup.SelectedItems[0].SubItems[1].Text;
                    
                }


                lvSup.Focus();
             
                this.btnAddNew.Enabled = CanAdd;
                this.btnSave.Enabled = CanEdit;
                this.btnDelete.Enabled = CanDelete;
                
                if (CanDelete)
                {
                    this.btnDelete.Enabled = EnableDelete(txtSupListName.Text);
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


        private void LoadList1Customers(string pSelected = "")
        {
            int SelIndex = 0;
            lvCus.Items.Clear();

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomList1 where RecordType = 'Customers'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["List1Name"].ToString());
                    listitem.SubItems.Add(dr["id"].ToString());
                    listitem.SubItems.Add(dr["RecordType"].ToString());
                    lvCus.Items.Add(listitem);
                }

                lvCus.View = View.Details;
                for (int x = 0; x <= lvCus.Items.Count - 1; x++)
                {
                    if (lvCus.Items[x].Index % 2 == 0)
                    {
                        lvCus.Items[x].BackColor = System.Drawing.ColorTranslator.FromHtml("#ebf5ff");
                    }
                    else
                    {
                        lvCus.Items[x].BackColor = Color.White;
                    }
                    if (pSelected == lvCus.Items[x].SubItems[0].Text)
                    {
                        SelIndex = x;
                    }
                }
              

                if (lvCus.Items.Count > 0)
                {
                    lvCus.Items[SelIndex].Selected = true;
                }

                if (lvCus.SelectedItems.Count > 0)
                {
                    txtCusListName.Text = lvCus.SelectedItems[0].SubItems[0].Text;
                    lblCusListID.Text = lvCus.SelectedItems[0].SubItems[1].Text;
                }

                lvCus.Focus();
              
                this.btnAddNew.Enabled = CanAdd;
                this.btnSave.Enabled = CanEdit;
                this.btnDelete.Enabled = CanDelete;
              
                if (CanDelete)
                {
                    this.btnDelete.Enabled = EnableDelete(txtSupListName.Text);
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

        private void LoadList1Items(string pSelected = "")
        {
            int SelIndex = 0;
            lvItem.Items.Clear();

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomList1 where RecordType = 'Items'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["List1Name"].ToString());
                    listitem.SubItems.Add(dr["id"].ToString());
                    listitem.SubItems.Add(dr["RecordType"].ToString());
                    lvItem.Items.Add(listitem);
                }

                lvItem.View = View.Details;
                for (int x = 0; x <= lvItem.Items.Count - 1; x++)
                {
                    if (lvItem.Items[x].Index % 2 == 0)
                    {
                        lvItem.Items[x].BackColor = System.Drawing.ColorTranslator.FromHtml("#ebf5ff");
                    }
                    else
                    {
                        lvItem.Items[x].BackColor = Color.White;
                    }
                    if (pSelected == lvItem.Items[x].SubItems[0].Text)
                    {
                        SelIndex = x;
                    }
                }
                if (lvItem.Items.Count > 0)
                {
                    lvItem.Items[SelIndex].Selected = true;
                }

                //if (lvItem.SelectedItems.Count > 0)
                //{
                //    txtItemListName.Text = lvItem.SelectedItems[0].SubItems[0].Text;
                //    lblItemListID.Text = lvItem.SelectedItems[0].SubItems[1].Text;

                //}

                lvItem.Focus();
          
                this.btnAddNew.Enabled = CanAdd;
                this.btnSave.Enabled = CanEdit;
                this.btnDelete.Enabled = CanDelete;
              
                if (CanDelete)
                {
                    this.btnDelete.Enabled = EnableDelete(txtSupListName.Text);
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

        private bool IsDuplicate(string pListName, string pRecordType)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomList1 WHERE List1name ='" + pListName + "' and RecordType = '" + pRecordType + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

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

     
        void New_(string pListName, string pRecordType)
        {
            switch (this.tabList.SelectedIndex)
            {
                case 0:
                    pListName = this.itemlistname.Text;

                    break;
                case 1:
                    pListName = this.txtCusListName.Text;

                    break;
                case 2:
                    pListName = this.txtSupListName.Text;

                    break;
            }
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand("INSERT INTO CustomList1 (RecordType, List1Name) VALUES (@RecordType, @List1Name); SELECT SCOPE_IDENTITY()", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@RecordType", pRecordType);
                cmd.Parameters.AddWithValue("@List1Name", pListName);

                con.Open();

                int rowsaffected = Convert.ToInt32(cmd.ExecuteScalar());
               
                if (rowsaffected != 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New " + pRecordType + " List 1 Record " + pListName, pListName);
                    string titles = "INFORMATION";
                    MessageBox.Show("Custom List 1Record has been created.", titles);
                    if(pRecordType == "Items")
                    {
                        LoadList1Items(rowsaffected.ToString());
                    }
                    if (pRecordType == "Customers")
                    {
                        LoadList1Customers(rowsaffected.ToString());
                    }
                    if (pRecordType == "Suppliers")
                    {
                        LoadList1Suppliers(rowsaffected.ToString());
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
        void UpdateList(string plistID, string plistName, string pRecType)
        {
            switch (this.tabList.SelectedIndex)
            {
                case 0:
                    plistName = this.itemlistname.Text;

                    break;
                case 1:
                    plistName = this.txtCusListName.Text;

                    break;
                case 2:
                    plistName = this.txtSupListName.Text;

                    break;
            }
            SqlConnection con = null;
            try
            {
                string titles = "Update Custom List Record for " + pRecType;
                DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("Update CustomList1 set List1Name = '" + plistName + "' where id = " + plistID, con);

                    con.Open();

                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated " + pRecType + " Custom List Record " + plistName + " ID " + plistID, plistName);
                        MessageBox.Show("Record has been updated.", "INFORMATION");
                        switch (this.tabList.SelectedIndex)
                        {
                            case 0:
                                LoadList1Items(plistID);
                                break;
                            case 1:
                                LoadList1Customers(plistID);
                                break;
                            case 2:
                                LoadList1Suppliers(plistID);
                                break;
                        }
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

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvSup.Columns[e.ColumnIndex].Width;
        }

        private void Record_Click(object sender, EventArgs e)
        {
            string listName = "";
            string lRecType = "";
            string lblID = "";
            switch (this.tabList.SelectedIndex)
            {
                case 0:
                    if (itemlistname.Text == ""
                        || lvItem.Items.Count == 0)
                        //return;
                    listName = this.itemlistname.Text;
                    lRecType = "Items";
                    lblID = this.lblItemListID.Text;
                    break;
                case 1:
                    if (txtCusListName.Text == ""
                        || lvCus.Items.Count == 0)
                        //return;
                    listName = this.txtCusListName.Text;
                    lRecType = "Customers";
                    lblID = this.lblCusListID.Text;
                    break;
                case 2:
                    if (txtSupListName.Text == ""
                        || lvSup.Items.Count == 0)
                       // return;
                    listName = this.txtSupListName.Text;
                    lRecType = "Suppliers";
                    lblID = this.lblSupListID.Text;
                    break;
            }
            if (itemlistname.Text != "" || txtCusListName.Text != "" || txtSupListName.Text != "")
            {
                if (lblID == "")
                {
                    if (!IsDuplicate(listName, lRecType))
                    {
                        New_(listName, lRecType);
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
                    UpdateList(lblID, listName, lRecType);
                    btnRefresh.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Input Item list Name!");
            }
            lvItem.Enabled = true;
            lvCus.Enabled = true;
            lvSup.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            switch (this.tabList.SelectedIndex)
            {
                case 0:
                    LoadList1Items();
                    break;
                case 1:
                    LoadList1Customers();
                    break;
                case 2:
                    LoadList1Suppliers();
                    break;
            }
            this.txtCusListName.Text = "";
            this.itemlistname.Text = "";
            this.txtSupListName.Text = "";
            lblCusListID.Text = "";
            lblItemListID.Text = "";
            lblSupListID.Text = "";
            formload();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string lRecordType = "";
            string lListID = "";
            string lListName = "";
            switch (this.tabList.SelectedIndex)
            {
                case 0:
                    if (lvItem.SelectedItems.Count <= 0)
                        return;
                    lRecordType = "Items";
                    lListID = this.lblItemListID.Text;
                    lListName = this.itemlistname.Text;
                    break;
                case 1:
                    if (lvCus.SelectedItems.Count <= 0)
                        return;
                    lRecordType = "Customers";
                    lListID = this.lblCusListID.Text;
                    lListName = this.txtCusListName.Text;
                    break;
                case 2:
                    if (lvSup.SelectedItems.Count <= 0)
                        return;
                    lRecordType = "Suppliers";
                    lListID = this.lblSupListID.Text;
                    lListName = this.txtSupListName.Text;
                    break;
            }

            SqlConnection con = null;
            try
            {
                string titles = "Delete Custom List Record for " + lRecordType;
                DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("DELETE FROM CustomList1 where id = " + lListID, con);

                    con.Open();

                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Deleted " + lRecordType + " Custom List Record " + lListName + " ID " + lListID, lListName);
                        MessageBox.Show("Record has been deleted.", "INFORMATION");
                        switch (this.tabList.SelectedIndex)
                        {
                            case 0:
                                LoadList1Items();
                                btnRefresh.PerformClick();
                                break;
                            case 1:
                                LoadList1Customers();
                                btnRefresh.PerformClick();
                                break;
                            case 2:
                                LoadList1Suppliers();
                                btnRefresh.PerformClick();
                                break;
                        }
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

        private static bool EnableDelete(string pMethod)
        {
            return true;
        }
        private void formload()
        {
            LoadList1Items();
            LoadList1Customers();
            LoadList1Suppliers();
            lvItem.Enabled = true;
            lvCus.Enabled = true;
            lvSup.Enabled = true;
            txtCusListName.Enabled = false;
            itemlistname.Enabled = false;
            txtSupListName.Enabled = false;
            if (tabList.SelectedIndex == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
            else if (tabList.SelectedIndex == 1)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
            else if (tabList.SelectedIndex == 2)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void CustomList1_Load(object sender, EventArgs e)
        {
            SetList1Name("Items", ref this.lblItem);
            SetList1Name("Customers", ref this.lblCustomer);
            SetList1Name("Suppliers", ref this.lblSupplier);

            formload();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

            this.txtCusListName.Enabled = true;
            this.itemlistname.Enabled = true;
            this.txtSupListName.Enabled = true;

            this.btnAddNew.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;

       

            switch (this.tabList.SelectedIndex)
            {
                case 0:
                    this.itemlistname.Text = "";                    
                    this.lblItemListID.Text = "";
                    break;
                case 1:
                    this.txtCusListName.Text = "";                   
                    this.lblCusListID.Text = "";
                    break;
                case 2:                  
                    this.txtSupListName.Text = "";                   
                    this.lblSupListID.Text = "";
                    break;
            }
        }

        private void lvItem_Click(object sender, EventArgs e)
        {
            this.itemlistname.Text = lvItem.SelectedItems[0].SubItems[0].Text;
            this.lblItemListID.Text = lvItem.SelectedItems[0].SubItems[1].Text;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            txtCusListName.Enabled = false;
            itemlistname.Enabled = false;
            txtSupListName.Enabled = false;
        }

        private void lvCus_Click(object sender, EventArgs e)
        {
            this.txtCusListName.Text = lvCus.SelectedItems[0].SubItems[0].Text;
            this.lblCusListID.Text = lvCus.SelectedItems[0].SubItems[1].Text;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            txtCusListName.Enabled = false;
            itemlistname.Enabled = false;
            txtSupListName.Enabled = false;
        }

        private void lvSup_Click(object sender, EventArgs e)
        {
            this.txtSupListName.Text = lvSup.SelectedItems[0].SubItems[0].Text;
            this.lblSupListID.Text = lvSup.SelectedItems[0].SubItems[1].Text;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            txtCusListName.Enabled = false;
            itemlistname.Enabled = false;
            txtSupListName.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            lvItem.Enabled = false;
            lvCus.Enabled = false;
            lvSup.Enabled = false;

            this.txtCusListName.Enabled = true;
            this.itemlistname.Enabled = true;
            this.txtSupListName.Enabled = true;
        }

        private void tabList_Click(object sender, EventArgs e)
        {
            formload();
        }

        private void SetList1Name(string pRecordType, ref Label plbl)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomNames WHERE RecordType = '" + pRecordType + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    plbl.Text = dt.Rows[0]["CList1Name"].ToString();
                }
                else
                {
                    plbl.Text = "";
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
}
