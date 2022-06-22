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
    public partial class Currency : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        private bool IsNew = false;

        public Currency()
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

        private void Currency_Load(object sender, EventArgs e)
        {
            LoadCurrency();
            btnSave.Enabled = false;
            panel1.Enabled = false;
        }

        private void Record_Click(object sender, EventArgs e)
        {
            if (IsNew)
            {
                if (!IsDuplicate(CurrencySymbol.Text))
                {
                    newRecord();
                }
                else
                {
                    MessageBox.Show("Currency Symbol already exists.");
                }
            }
            else
            {
                UpdateRecord();
            }
        }

        void LoadCurrency(string pSelected = "")
        {
            int SelIndex = 0;
            listView1.Items.Clear();
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT CurrencyCode, CurrencyName, ExchangeRate, CurrencySymbol, WholeNumberWord, DecimalWord, CurrencyID FROM Currency";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["CurrencyCode"].ToString());
                    listitem.SubItems.Add(dr["CurrencyName"].ToString());
                    listitem.SubItems.Add(dr["ExchangeRate"].ToString());
                    listitem.SubItems.Add(dr["CurrencySymbol"].ToString());
                    listitem.SubItems.Add(dr["WholeNumberWord"].ToString());
                    listitem.SubItems.Add(dr["DecimalWord"].ToString());
                    listitem.SubItems.Add(dr["CurrencyID"].ToString());
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

                if (pSelected == null && listView1.Items.Count > 0)
                {
                    listView1.Items[SelIndex].Selected = true;
                    CurrencyCode.Text = listView1.SelectedItems[0].SubItems[0].Text;
                    CurrencyName.Text = listView1.SelectedItems[0].SubItems[1].Text;
                    string strsubitem = listView1.SelectedItems[0].SubItems[2].Text;
                    ExchangeRate.Value = strsubitem != "" ? Convert.ToDecimal(strsubitem) : 0;
                    CurrencySymbol.Text = listView1.SelectedItems[0].SubItems[3].Text;
                    WholeNumberWord.Text = listView1.SelectedItems[0].SubItems[4].Text;
                    DecimalWord.Text = listView1.SelectedItems[0].SubItems[5].Text;
                    lblCurrencyID.Text = listView1.SelectedItems[0].SubItems[6].Text;
                    SelIndex = 0;
                    listView1.Focus();
                    listView1.Items[SelIndex].Selected = true;
                }
                if (listView1.Items.Count > 0)
                {
                    btnEdit.Enabled = CanEdit;
                    btnDelete.Enabled = CanDelete;
                }
                else
                {
                    btnEdit.Enabled = false ;
                    btnDelete.Enabled = false;
                }
                btnAddNew.Enabled = CanAdd;
                if (CanDelete)
                {
                    btnDelete.Enabled = EnableDelete(lblCurrencyID.Text);
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

        void newRecord()
        {
            SqlConnection con = null;
            try
            {
                if (CurrencyCode.Text == "" 
                    || CurrencyName.Text == "" 
                    || ExchangeRate.Value < 0
                    || CurrencySymbol.Text == "" 
                    || WholeNumberWord.Text == "" 
                    || DecimalWord.Text == "")
                {
                    MessageBox.Show("Required fields are missing.");
                }
                else
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("INSERT INTO Currency (CurrencyCode, CurrencyName, ExchangeRate, CurrencySymbol, WholeNumberWord, DecimalWord) VALUES (@CurrencyCode, @CurrencyName, @ExchangeRate, @CurrencySymbol, @WholeNumberWord, @DecimalWord)", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode.Text);
                    cmd.Parameters.AddWithValue("@CurrencyName", CurrencyName.Text);
                    cmd.Parameters.AddWithValue("@ExchangeRate", ExchangeRate.Value);
                    cmd.Parameters.AddWithValue("@CurrencySymbol", CurrencySymbol.Text);
                    cmd.Parameters.AddWithValue("@WholeNumberWord", WholeNumberWord.Text);
                    cmd.Parameters.AddWithValue("@DecimalWord", DecimalWord.Text);

                    con.Open();

                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Currency " + CurrencyCode.Text, CurrencyCode.Text);
                        string titles = "INFORMATION";
                        MessageBox.Show("Currency Record has been created.", titles);
                        IsNew = false;
                      
                        LoadCurrency(CurrencyCode.Text);
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

        void UpdateRecord()
        {
            SqlConnection con = null;
            try
            {
                if (CurrencyCode.Text == "" 
                    || CurrencyName.Text == "" 
                    || ExchangeRate.Value < 0 
                    || CurrencySymbol.Text == "" 
                    || WholeNumberWord.Text == "" 
                    || DecimalWord.Text == "")
                {
                    MessageBox.Show("(*) Required fields are missing.");
                }
                else
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("UPDATE Currency SET CurrencyCode = @CurrencyCode, CurrencyName = @CurrencyName, ExchangeRate = @ExchangeRate, CurrencySymbol = @CurrencySymbol, WholeNumberWord = @WholeNumberWord, DecimalWord = @DecimalWord WHERE currencyID = '" + lblCurrencyID.Text + "'", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode.Text);
                    cmd.Parameters.AddWithValue("@CurrencyName", CurrencyName.Text);
                    cmd.Parameters.AddWithValue("@ExchangeRate", ExchangeRate.Value);
                    cmd.Parameters.AddWithValue("@CurrencySymbol", CurrencySymbol.Text);
                    cmd.Parameters.AddWithValue("@WholeNumberWord", WholeNumberWord.Text);
                    cmd.Parameters.AddWithValue("@DecimalWord", DecimalWord.Text);

                    con.Open();

                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Edited Currency "+ CurrencyCode.Text, CurrencyCode.Text);
                        string titles = "INFORMATION";
                        MessageBox.Show("Currency record has been updated.", titles);
                        LoadCurrency(CurrencyCode.Text);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                if (lblCurrencyID.Text != "")
                {
                    string titles = "Delete Currency Record";
                    DialogResult dialogResult = MessageBox.Show("Do you wish to continue deleting currency? (yes/no)", titles, MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        con = new SqlConnection(CommonClass.ConStr);
                        SqlCommand cmd = new SqlCommand("DELETE FROM currency WHERE CurrencyID = '" + lblCurrencyID.Text + "'", con);

                        con.Open();

                        int rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 0)
                        {
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Deleted Currency " + CurrencyCode.Text, lblCurrencyID.Text);
                            MessageBox.Show("Currency record has been deleted.", "INFORMATION");
                            LoadCurrency();
                        }
                    }
                }
                else
                    MessageBox.Show("Please select a currency to delete.", "INFORMATION");
                btnRefresh.PerformClick();
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

        private void listView1_Click(object sender, EventArgs e)
        {
            CurrencyCode.Text = listView1.SelectedItems[0].SubItems[0].Text;
            CurrencyName.Text = listView1.SelectedItems[0].SubItems[1].Text;
            string strsubitems = listView1.SelectedItems[0].SubItems[2].Text;
            ExchangeRate.Value = strsubitems != "" ? Convert.ToDecimal(strsubitems) : 0;
            CurrencySymbol.Text = listView1.SelectedItems[0].SubItems[3].Text;
            WholeNumberWord.Text = listView1.SelectedItems[0].SubItems[4].Text;
            DecimalWord.Text = listView1.SelectedItems[0].SubItems[5].Text;
            lblCurrencyID.Text = listView1.SelectedItems[0].SubItems[6].Text;
            if (CanDelete)
            {
                btnDelete.Enabled = EnableDelete(lblCurrencyID.Text);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            lblCurrencyID.Text = "";
            CurrencyCode.Clear();
            CurrencyName.Clear();
            ExchangeRate.Value = 1;
            CurrencySymbol.Clear();
            WholeNumberWord.Clear();
            DecimalWord.Clear();
            codeGen();
            btnAddNew.Enabled = false;
            IsNew = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            panel1.Enabled = true;
        }

        void codeGen()
        {
            SqlConnection con_ua = null;
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);
                string selectSql_ua = "SELECT TOP 1 (currencyID + 1) AS CurrencyCode FROM Currency ORDER BY CurrencyID DESC";
                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = "C-" + (reader["CurrencyCode"].ToString());
                            CurrencyCode.Text = name;
                        }
                    }
                    else
                    {
                        CurrencyCode.Text = "C-1";
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ua != null)
                    con_ua.Close();
            }
        }

        private static bool EnableDelete(string pCurrencyID)
        {
            return true;
        }

        private bool IsDuplicate(string pCurrencySymbol)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM Currency WHERE currencysymbol = '" + pCurrencySymbol + "'";
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCurrency();
            btnSave.Enabled = false;
            if(listView1.Items.Count > 0)
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
            CurrencyCode.Text = "";
            CurrencyName.Text = "";
            string strsubitems = "";
            ExchangeRate.Value = 0;
            CurrencySymbol.Text = "";
            WholeNumberWord.Text = "";
            DecimalWord.Text = "";
            lblCurrencyID.Text = "";

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnAddNew.Enabled = false;
            btnSave.Enabled = true;
            panel1.Enabled = true;
            IsNew = false;
        }
    }//END
}


