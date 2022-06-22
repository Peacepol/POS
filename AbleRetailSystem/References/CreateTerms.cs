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
    public partial class CreateTerms : Form
    {
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        public CreateTerms()
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
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void FormLoad()
        {
            listView1.Items.Clear();
            txt1.Clear();
            txt3.Clear();
            txt4.Clear();
            txt5.Clear();
            txt6.Clear();
            txt7.Clear();
            txt8.Clear();

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT TermsOfPaymentID, LatePaymentChargePercent, EarlyPaymentDiscountPercent, DiscountDays, BalanceDueDays, DiscountDate,	BalanceDueDate, termsID FROM terms";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd_;
                da.Fill(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["TermsOfPaymentID"].ToString());
                    listitem.SubItems.Add(dr["LatePaymentChargePercent"].ToString());
                    listitem.SubItems.Add(dr["EarlyPaymentDiscountPercent"].ToString());
                    listitem.SubItems.Add(dr["DiscountDays"].ToString());
                    listitem.SubItems.Add(dr["BalanceDueDays"].ToString());
                    listitem.SubItems.Add(dr["DiscountDate"].ToString());
                    listitem.SubItems.Add(dr["BalanceDueDate"].ToString());
                    listitem.SubItems.Add(dr["termsID"].ToString());
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();

                PrivateClass_.termsI = "";
            }
        }

        private void CreateTerms_Load(object sender, EventArgs e)
        {
            txt1.ShortcutsEnabled = false;
            txt3.ShortcutsEnabled = false;
            txt4.ShortcutsEnabled = false;
            txt5.ShortcutsEnabled = false;
            txt6.ShortcutsEnabled = false;
            txt7.ShortcutsEnabled = false;
            txt8.ShortcutsEnabled = false;
            button2.Enabled = CanDelete;
            Save.Enabled = CanEdit;
            FormLoad();
        }

        private void txt3_keypress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char) Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !System.Decimal.TryParse(txt3.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txt4_keypress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char) Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !System.Decimal.TryParse(txt4.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txt5_keypress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char) Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !System.Decimal.TryParse(txt5.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txt6_keypress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char) Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !System.Decimal.TryParse(txt6.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txt7_keypress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char) Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !System.Decimal.TryParse(txt7.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        private void txt8_keypress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            decimal x;
            if (ch == (char) Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '.' || !System.Decimal.TryParse(txt8.Text + ch, out x))
            {
                e.Handled = true;
            }
        }

        void New_()
        {
            SqlConnection con = null;
            try
            {
                if (txt1.Text == "" 
                    || txt3.Text == "" 
                    || txt4.Text == "" 
                    || txt5.Text == "" 
                    || txt6.Text == "" 
                    || txt7.Text == "" 
                    || txt8.Text == "")
                {
                    MessageBox.Show("Required fields are missing.");
                }
                else
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("INSERT INTO Terms (TermsOfPaymentID, LatePaymentChargePercent, EarlyPaymentDiscountPercent, DiscountDays, BalanceDueDays, DiscountDate, BalanceDueDate) VALUES (@txt1, @txt3, @txt4, @txt5, @txt6, @txt7, @txt8)", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@txt1", txt1.Text);
                    cmd.Parameters.AddWithValue("@txt3", txt3.Text);
                    cmd.Parameters.AddWithValue("@txt4", txt4.Text);
                    cmd.Parameters.AddWithValue("@txt5", txt5.Text);
                    cmd.Parameters.AddWithValue("@txt6", txt6.Text);
                    cmd.Parameters.AddWithValue("@txt7", txt7.Text);
                    cmd.Parameters.AddWithValue("@txt8", txt8.Text);

                    con.Open();

                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 0)
                    {
                        string titles = "INFORMATION";
                        MessageBox.Show("Record has been created.", titles);
                    }
                    FormLoad();
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

       void Update_()
        {
            SqlConnection con = null;
            try
            {
                if (txt1.Text == "" 
                    || txt3.Text == "" 
                    || txt4.Text == "" 
                    || txt5.Text == "" 
                    || txt6.Text == "" 
                    || txt7.Text == "" 
                    || txt8.Text == "")
                {
                    MessageBox.Show("Required field is missing.");
                }
                else
                {
                    string titles = "Update Record";
                    DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        con = new SqlConnection(CommonClass.ConStr);
                        SqlCommand cmd = new SqlCommand("UPDATE terms SET TermsOfPaymentID = @txt1, LatePaymentChargePercent = @txt3, EarlyPaymentDiscountPercent = @txt4, DiscountDays = @txt5, BalanceDueDays = @txt6, DiscountDate = @txt7, BalanceDueDate = @txt8 WHERE termsID = @txt9 ", con);
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@txt1", txt1.Text);
                        cmd.Parameters.AddWithValue("@txt3", txt3.Text);
                        cmd.Parameters.AddWithValue("@txt4", txt4.Text);
                        cmd.Parameters.AddWithValue("@txt5", txt5.Text);
                        cmd.Parameters.AddWithValue("@txt6", txt6.Text);
                        cmd.Parameters.AddWithValue("@txt7", txt7.Text);
                        cmd.Parameters.AddWithValue("@txt8", txt8.Text);
                        cmd.Parameters.AddWithValue("@txt9", PrivateClass_.termsI);

                        con.Open();

                        int rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 0)
                        {
                            MessageBox.Show("Record has been updated.", "INFORMATION");
                        }

                        FormLoad();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        FormLoad();
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
                PrivateClass_.termsI = "";
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
        }

        public class PrivateClass_
        {
            public static string termsI; //taxcde
        }

        private void listView1_ColumnWidthchanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            FormLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                string titles = "Delete Record";
                DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand("DELETE FROM terms WHERE termsID = '" + PrivateClass_.termsI + "'", con);

                    con.Open();

                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 0)
                    {
                        MessageBox.Show("Record has been deleted.", "INFORMATION");
                    }

                    FormLoad();
                }
                else if (dialogResult == DialogResult.No)
                {
                    FormLoad();
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

        private void listView1_Click(object sender, EventArgs e)
        {
            string txt9_ = listView1.SelectedItems[0].SubItems[7].Text;

            txt1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txt3.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txt4.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txt5.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txt6.Text = listView1.SelectedItems[0].SubItems[4].Text;
            txt7.Text = listView1.SelectedItems[0].SubItems[5].Text;
            txt8.Text = listView1.SelectedItems[0].SubItems[6].Text;
            PrivateClass_.termsI = txt9_;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (PrivateClass_.termsI == "")
            {
                New_();
            }
            else
            {
                Update_();
            }
        }
    }
}
