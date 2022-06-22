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

namespace RestaurantPOS
{
    public partial class CreatePaymentMethod : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";

        //Dictionary<string, object> param = new Dictionary<string, object>();

        private DataTable dt;
        public CreatePaymentMethod()
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

        private void LoadPaymentMethods(string pSelected = "")
        {
            int SelIndex = 0;
            listView1.Items.Clear();
                string selectSql = "SELECT * FROM PaymentMethods";
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, selectSql);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["PaymentMethod"].ToString());
                    listitem.SubItems.Add(dr["id"].ToString());
                    listitem.SubItems.Add(dr["GLAccountCode"].ToString());                    
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
                    txtPaymentMethod.Text = listView1.SelectedItems[0].SubItems[0].Text;
                    ID.Text = listView1.SelectedItems[0].SubItems[1].Text;
                    SelIndex = 0;
                    listView1.Items[SelIndex].Selected = true;
                }
                listView1.Focus();
                this.btnAddNew.Enabled = CanAdd;
                this.btnSave.Enabled = CanEdit;
                this.btnDelete.Enabled = CanDelete;
                this.txtPaymentMethod.ReadOnly = true;
                this.txtGLAccountCode.ReadOnly = true;
            if (CanDelete)
                {
                    this.btnDelete.Enabled = EnableDelete(txtPaymentMethod.Text);
                }  
        }

        private bool IsDuplicate(string pPaymentMethod)
        {
          string selectSql = "SELECT * FROM PaymentMethods WHERE paymentmethod ='" + pPaymentMethod + "'";
          DataTable dt = new DataTable();
          CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
                {
                    return true;
                }
            else
                {
                    return false;
                }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
          if (txtPaymentMethod.Text == "")
              {
                  MessageBox.Show("Required fields are missing.");
              }
                else
                {
                Dictionary<string, object> paramPaymentMethod = new Dictionary<string, object>();
                string sqlInsert = @"INSERT INTO PaymentMethods (PaymentMethod) VALUES (@txt1)";
                paramPaymentMethod.Add("@txt1", txtPaymentMethod.Text);

                int rowsaffected = CommonClass.runSql(sqlInsert, CommonClass.RunSqlInsertMode.QUERY, paramPaymentMethod);
                    if (rowsaffected != 0)
                    {
                        string titles = "INFORMATION";
                        MessageBox.Show("Record has been created.", titles);
                    }
                    LoadPaymentMethods();
                }
        }

      void New_()
        {
           if (txtPaymentMethod.Text == "")
                {
                    MessageBox.Show("Payment Method Name Required.");
                }
           else
                {
                Dictionary<string, object> paramPaymentMethods = new Dictionary<string, object>();
                string sqlInsert = @"INSERT INTO PaymentMethods (PaymentMethod, GLAccountCode) VALUES (@PaymentMethod, @GLAccountCode)";
                paramPaymentMethods.Add("@PaymentMethod", txtPaymentMethod.Text);
                paramPaymentMethods.Add("@GLAccountCode", txtGLAccountCode.Text);

                int rowsaffected = CommonClass.runSql(sqlInsert, CommonClass.RunSqlInsertMode.QUERY, paramPaymentMethods);
                if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Payment Method " + txtPaymentMethod.Text, txtPaymentMethod.Text);
                        string titles = "INFORMATION";
                        MessageBox.Show("Payment Method Record has been created.", titles);
                        LoadPaymentMethods(txtPaymentMethod.Text);
                    }              
                }
        }

        private void formload()
        {
            LoadPaymentMethods();
            listView1.Enabled = true;
            txtPaymentMethod.Text = "";
            txtGLAccountCode.Clear();
            if (listView1.Items.Count == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
            btnSave.Enabled = false;
           // btnDelete.Enabled = false;
        }
        private void CreatePaymentMethod_Load(object sender, EventArgs e)
        {
            formload();
        }

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void Record_Click(object sender, EventArgs e)
        {
            string id = ID.Text;
            string name = txtPaymentMethod.Text;
            string glcode = txtGLAccountCode.Text;
            if (ID.Text == "")
            {
                if (!IsDuplicate(this.txtPaymentMethod.Text))
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
                UpdateList(id, name, glcode);
                btnRefresh.PerformClick();
            }

        }
        void UpdateList(string pID, string pName, string pGlCode)
        {

            pName = this.txtPaymentMethod.Text;
            pGlCode = this.txtGLAccountCode.Text;

            string titles = "Update Payment Method Record for " + pName;
            DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    string sqlUpdate = @"Update PaymentMethods set PaymentMethod = '" + pName + "', GLAccountCode = '"+ pGlCode + "' where id = " + pID;
                    int rowsaffected = CommonClass.runSql(sqlUpdate, CommonClass.RunSqlInsertMode.QUERY);
                    if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Payment Method " + pName + " ID " + pID, pName);
                        MessageBox.Show("Record has been updated.", "INFORMATION");
                        btnRefresh.PerformClick();
                    }
                }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            formload();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtPaymentMethod.Text == "")
            {
                MessageBox.Show("Please Select Payment Method");
            }else
            {
                string sql = @"SELECT PaymentMethod FROM Profile pl
                            INNER JOIN PaymentMethods p ON p.id = pl.MethodOfPaymentID
                            WHERE PaymentMethod = '" + txtPaymentMethod.Text + "'";
                dt = new DataTable();
                CommonClass.runSql(ref dt, sql);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Payment Method Can Not Be Deleted Due To an Existing Transaction");
                }
                else
                {
                    string titles = "Delete Payment Method";
                    DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {

                        string sqlDelete = @"DELETE FROM PaymentMethods where PaymentMethod = '" + txtPaymentMethod.Text + "'";
                        int rowsaffected = CommonClass.runSql(sqlDelete);
                        if (rowsaffected != 0)
                        {
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Deleted Payment Method " + this.txtPaymentMethod.Text + " ID " + this.txtPaymentMethod.Text, this.txtPaymentMethod.Text);
                            MessageBox.Show("Record has been deleted.", "INFORMATION");
                            LoadPaymentMethods();
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        LoadPaymentMethods();
                    }
                    btnRefresh.PerformClick();
                }
            }

            
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            txtPaymentMethod.ReadOnly = false;
            txtGLAccountCode.ReadOnly = false;
            txtPaymentMethod.Text = listView1.SelectedItems[0].SubItems[0].Text;
            ID.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtGLAccountCode.Text = listView1.SelectedItems[0].SubItems[2].Text;
            btnEdit.PerformClick();
        }

        private static bool EnableDelete(string pMethod)
        {
            return true;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.txtPaymentMethod.Text = "";
            this.ID.Text = "";
            this.txtGLAccountCode.Text = "";
            this.btnAddNew.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;


            this.txtGLAccountCode.ReadOnly = false;
            this.txtPaymentMethod.ReadOnly = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.btnAddNew.Enabled = false;
            this.btnSave.Enabled = true;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = true;

            listView1.Enabled = true;
            this.txtPaymentMethod.Enabled = true;
        }

        private void txtGLAccountCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '-')
            {
                e.Handled = true;
            }
        }
    }
}
