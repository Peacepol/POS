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
    public partial class CreateTaxCode : Form
    {
        private static string TAXCODE;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        private bool IsNew = false;
        private DataTable dt;

        public CreateTaxCode()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(Text, out FormRights);
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

        private void CreateTaxCode_Load(object sender, EventArgs e)
        {
            LoadTaxCodes();
            txtGLCollecected.Clear();
            groupBox1.Enabled = false;
            btnSave.Enabled = false;
        }

        private void txtTaxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                char ch = e.KeyChar;
                if (ch == (char)Keys.Back)
                {
                    e.Handled = false;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void New_()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            if (txtTaxCode.Text == "" 
                || txtRate.Text == "" 
                || txtDesc.Text == "")
            {
                MessageBox.Show("Required fields are missing.");
            }
            else
            {
                string sqlInsert = @"INSERT INTO TaxCodes (TaxCode, TaxPercentageRate, TaxCodeDescription, TaxCollectedAccountID, TaxPaidAccountID) VALUES (@TaxCode, @TaxPercentageRate, @TaxCodeDescription, @TaxCollectedAccountID, @TaxPaidAccountID)";
                param.Add("@TaxCode", txtTaxCode.Text);
                param.Add("@TaxPercentageRate", txtRate.Value);
                param.Add("@TaxCodeDescription", txtDesc.Text);
                param.Add("@TaxCollectedAccountID", txtGLCollecected.Text);
                param.Add("@TaxPaidAccountID", txtGLPaid.Text);
                int i = CommonClass.runSql(sqlInsert, CommonClass.RunSqlInsertMode.QUERY, param);
                if (i != 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Tax Code " + txtTaxCode.Text);
                    string titles = "INFORMATION";
                    MessageBox.Show("Tax Code successfully created.", titles);
                    IsNew = false;
                    LoadTaxCodes(txtTaxCode.Text);
                    btnRefresh.PerformClick();
                }
            }
        }

        private void LoadTaxCodes(string pSelected = "")
        {
            txtTaxCode.ReadOnly = true;
            listView1.Items.Clear();
            int SelIndex = 0;

            string selectSql = "SELECT t.TaxCode, t.TaxCodeDescription, t.TaxPercentageRate, t.TaxCollectedAccountID, t.TaxPaidAccountID FROM taxcodes t ORDER BY t.TaxCode ASC";
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, selectSql);
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow dr = dt.Rows[x];
                decimal lbal = Convert.ToDecimal(dr["TaxPercentageRate"].ToString());
                ListViewItem listitem = new ListViewItem(dr["TaxCode"].ToString());
                listitem.SubItems.Add(dr["TaxCodeDescription"].ToString());
                listitem.SubItems.Add((Math.Round(lbal, 2).ToString("0.00")));
                listitem.SubItems.Add(dr["TaxCollectedAccountID"].ToString());
                listitem.SubItems.Add(dr["TaxPaidAccountID"].ToString());
                //listitem.SubItems.Add(dr["TaxCollectedAccountID"].ToString());
                //listitem.SubItems.Add(dr["TaxPaidAccountID"].ToString());
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
            listView1.Focus();
            if (dt.Rows.Count > 0)
            {
                btnAddNew.Enabled = CanAdd;
                btnDelete.Enabled = CanDelete;
                btnEdit.Enabled = CanEdit;
            }
            else
            {
                btnAddNew.Enabled = CanAdd;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }  

        void Update_()
        {
          Dictionary<string, object> Updateparam = new Dictionary<string, object>();
          if (txtTaxCode.Text == "" ||  txtDesc.Text == "")
          {
           MessageBox.Show("Required fields are missing.");
          }
          else
          {
              string titles = "Update Record";
              DialogResult dialogResult = MessageBox.Show("Do you wish to continue saving your changes? (yes/no)", titles, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string sqlUpdate = @"UPDATE TaxCodes SET TaxCode = @TaxCode, TaxPercentageRate = @TaxPercentageRate, TaxCodeDescription = @TaxCodeDescription, TaxCollectedAccountID = @TaxCollectedAccountID, TaxPaidAccountID = @TaxPaidAccountID WHERE TaxCode = '" + TAXCODE + "'";
                    Updateparam.Add("@TaxCode", txtTaxCode.Text);
                    Updateparam.Add("@TaxPercentageRate", txtRate.Value);
                    Updateparam.Add("@TaxCodeDescription", txtDesc.Text);
                    Updateparam.Add("@TaxCollectedAccountID", txtGLCollecected.Text);
                    Updateparam.Add("@TaxPaidAccountID", txtGLPaid.Text);
                    int i = CommonClass.runSql(sqlUpdate, CommonClass.RunSqlInsertMode.QUERY, Updateparam);
                    if (i != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Tax Code " + txtTaxCode.Text, txtTaxCode.Text);
                        MessageBox.Show("Tax Code Record has been updated.", "INFORMATION");
                        LoadTaxCodes(txtTaxCode.Text);
                        btnRefresh.PerformClick();
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    LoadTaxCodes(txtTaxCode.Text);
                    btnRefresh.PerformClick();
                }
            }
        }            

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTaxCodes(this.txtTaxCode.Text);
            txtTaxCode.ReadOnly = false;
            txtTaxCode.Clear();
            txtRate.Value = 0;
            txtDesc.Clear();
            txtGLCollecected.Clear();
            txtGLPaid.Clear();
            //btns
            groupBox1.Enabled = false;
            btnAddNew.Enabled = CanAdd;
            btnDelete.Enabled = CanDelete;
            btnSave.Enabled = false;
            btnEdit.Enabled = CanEdit;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            if (txtTaxCode.Text != "")
            {
                string sql = @"SELECT TaxCode FROM Profile
                            WHERE TaxCode =  '" + txtTaxCode.Text + "'";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand scmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = scmd;
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Tax Code Can Not Be Deleted Due To an Existing Transaction");
                }
                else
                {
                    try
                    {
                        string titles = "Delete Tax Code";
                        DialogResult dialogResult = MessageBox.Show("Do you wish to continue deleting tax code? (yes/no)", titles, MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            con = new SqlConnection(CommonClass.ConStr);
                            SqlCommand cmd = new SqlCommand("DELETE FROM TaxCodes WHERE TaxCode = '" + TAXCODE + "'", con);

                            con.Open();

                            int i = cmd.ExecuteNonQuery();

                            if (i != 0)
                            {
                                MessageBox.Show("Tax Code has been deleted.", "INFORMATION");
                            }

                            LoadTaxCodes();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            LoadTaxCodes();
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
            }
            else
            {
                MessageBox.Show("Please select a Tax code to delete.", "Information");
            }
            btnRefresh.PerformClick();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (IsNew)
            {
                New_();
            }
            else
            {
                Update_();
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            TAXCODE = listView1.SelectedItems[0].SubItems[0].Text;
            txtTaxCode.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtDesc.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtRate.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtGLCollecected.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtGLPaid.Text = listView1.SelectedItems[0].SubItems[4].Text;
            //lblCollectedID.Text = listView1.SelectedItems[0].SubItems[5].Text;
            //lblPaidID.Text = listView1.SelectedItems[0].SubItems[6].Text;
            btnEdit.PerformClick();
        }

        private void CreateTaxCode_Resize(object sender, EventArgs e)
        {
            if (WindowState == System.Windows.Forms.FormWindowState.Maximized)
            {
                MaximumSize = new Size(945, Screen.PrimaryScreen.Bounds.Height);
            }
            else if(WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                MaximumSize = new Size(945, 600);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            txtTaxCode.ReadOnly = false;
            txtTaxCode.Clear();
            txtRate.Value = 0;
            txtDesc.Clear();
            txtGLCollecected.Clear();
            txtGLPaid.Clear();
            btnAddNew.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            txtGLCollecected.Enabled = true;
            txtGLPaid.Enabled = true;
            TAXCODE = "";
            IsNew = true;
        }

        private void txtTaxCode_TextChanged(object sender, EventArgs e)
        {

        }

        private static bool EnableDelete(string pTaxCode)
        {
            return true;
        }
        private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAddNew.Enabled = false;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            IsNew = false;
            groupBox1.Enabled = true;
            txtGLPaid.ReadOnly = false;
            txtGLCollecected.ReadOnly = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtGLPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
            }
        }

        private void txtGLCollecected_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtGLPaid_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } //end
}

