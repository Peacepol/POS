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
    public partial class ProfileLookup : Form
    {
        private string[] Profile;
        private string ProfileType = "";
        public ProfileLookup(string pType = "")
        {
            InitializeComponent();
            ProfileType = pType;
        }

        public string[] GetProfile
        {
            get { return Profile; }
        }

        private void ProfileLookup_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile(string pSearch = "", bool activeOnly = false)
        {
            listView1.Items.Clear();
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string conStr = (ProfileType == "" ? " WHERE Type <> '' " : " WHERE Type = '" + ProfileType + "'");

                string selectSql = @"SELECT p.ID, 
                                        p.ProfileIDNumber, 
                                        p.Name, 
                                        p.CurrentBalance, 
                                        p.ShippingMethodID, 
                                        p.MethodOfPaymentID, 
                                        p.TermsOfPayment, 
                                        p.ContactID, 
                                        p.TaxCode, 
                                        c.Phone, 
                                        c.Email, 
                                        s.ShippingID, 
                                        p.CreditLimit,
                                        pmtds.PaymentMethod,
                                        c.Location, 
                                        l.Number 
                                    FROM Profile p 
                                    LEFT JOIN Contacts c ON p.ID = c.ProfileID 
                                    LEFT JOIN PaymentMethods pmtds ON pmtds.id = p.MethodOfPaymentID 
                                    LEFT JOIN LoyaltyMember l ON p.ID = l.ProfileID 
                                    LEFT JOIN ShippingMethods s ON p.ShippingMethodID = s.ShippingMethod " + conStr;
                string sqlcon = "";
                if (pSearch != "")
                {
                    if (this.rdoName.Checked)
                    {
                        sqlcon = " and p.Name LIKE  @textsearch";
                    }
                    if (this.rdoID.Checked)
                    {
                        sqlcon = " and p.ProfileIDNumber LIKE @textsearch";
                    }
                    if (this.rdoPhone.Checked)
                    {
                        sqlcon = " and c.Phone LIKE @textsearch";
                    }
                    if (this.rdoEmail.Checked)
                    {
                        sqlcon = " and c.Email LIKE @textsearch";
                    }
                    if (this.rdoCity.Checked)
                    {
                        sqlcon = " and c.City LIKE @textsearch";
                    }
                    if (this.rdoState.Checked)
                    {
                        sqlcon = " and c.State LIKE @textsearch";
                    }
                    if (this.rdoPostCode.Checked)
                    {
                        sqlcon = " and c.Postcode LIKE @textsearch";
                    }
                    if (this.rdoCountry.Checked)
                    {
                        sqlcon = " and c.Country LIKE @textsearch";
                    }
                    if (this.rdoLoyal.Checked)
                    {
                        sqlcon = " AND l.Number LIKE @textsearch";
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

                sqlcon += " AND Location = 1"; // Pre select the first address by default
                selectSql += sqlcon;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                SqlDataAdapter da = new SqlDataAdapter();
                cmd_.Parameters.AddWithValue("@textsearch", "%" + this.txtsearch.Text + "%");
                DataTable dt = new DataTable();
                da.SelectCommand = cmd_;
                da.Fill(dt);

                decimal lCBal;
                for (int x = 0; x < dt.Rows.Count; ++x)
                {
                    DataRow dr = dt.Rows[x];
                    ListViewItem listitem = new ListViewItem(dr["ID"].ToString());
                    listitem.SubItems.Add(dr["ProfileIDNumber"].ToString());
                    listitem.SubItems.Add(dr["Name"].ToString());
                    listitem.SubItems.Add(dr["Phone"].ToString());
                    listitem.SubItems.Add(dr["Email"].ToString());
                    lCBal = dr["CurrentBalance"].ToString() == "" ? 0 : Convert.ToDecimal(dr["CurrentBalance"].ToString());
                    listitem.SubItems.Add(Math.Round(lCBal, 2).ToString("C"));
                    listitem.SubItems.Add(dr["ShippingMethodID"].ToString());
                    listitem.SubItems.Add(dr["TermsOfPayment"].ToString());
                    listitem.SubItems.Add(dr["TaxCode"].ToString());
                    listitem.SubItems.Add(dr["ContactID"].ToString());
                    listitem.SubItems.Add(dr["MethodOfPaymentID"].ToString());
                    listitem.SubItems.Add(dr["ShippingID"].ToString());
                    listitem.SubItems.Add(dr["CreditLimit"].ToString());
                    listitem.SubItems.Add(dr["PaymentMethod"].ToString());
                    listitem.SubItems.Add(dr["Location"].ToString());
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
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProfile(this.txtsearch.Text);
        }

        private void listView1_Click(object sender, EventArgs e)
        {         
            Profile = new string[15];
            Profile[0] = listView1.SelectedItems[0].SubItems[0].Text;//ID
            Profile[1] = listView1.SelectedItems[0].SubItems[1].Text;//ProfileIDNumber
            Profile[2] = listView1.SelectedItems[0].SubItems[2].Text;//Name
            Profile[3] = listView1.SelectedItems[0].SubItems[5].Text;//CurrentBalance
            Profile[4] = listView1.SelectedItems[0].SubItems[6].Text;//Shipmethod
            Profile[5] = listView1.SelectedItems[0].SubItems[7].Text;//TermsOfPayment
            Profile[6] = listView1.SelectedItems[0].SubItems[8].Text;//TaxCode
            Profile[7] = listView1.SelectedItems[0].SubItems[9].Text;//contactID
            Profile[8] = listView1.SelectedItems[0].SubItems[10].Text;//MethodOfPayment
            Profile[9] = listView1.SelectedItems[0].SubItems[3].Text;//Phone
            Profile[10] = listView1.SelectedItems[0].SubItems[4].Text;//Email
            Profile[11] = listView1.SelectedItems[0].SubItems[11].Text;//Shipping ID
            Profile[12] = listView1.SelectedItems[0].SubItems[12].Text;//Credit limit
            Profile[13] = listView1.SelectedItems[0].SubItems[13].Text;//PaymentMethod
            Profile[14] = listView1.SelectedItems[0].SubItems[14].Text;//Location

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadProfile("Active", chkActive.Checked);
        }
    }
}
