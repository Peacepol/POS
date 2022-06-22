using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Sales
{
    public partial class ContactsFillUpForm : Form
    {
        private DataTable ContactInfoTb;
        public string payeeInfo = "";
        public string CustomerID = "";

        public ContactsFillUpForm(DataTable pContacttInfo = null, string pID = "")
        {
            InitializeComponent();
            ContactInfoTb = pContacttInfo;
            CustomerID = pID;
        }

        public DataTable GetContactInfo
        {
            get { return ContactInfoTb; }
        }

        public string GetPayeeInfo
        {
            get { return payeeInfo;  }
        }

        private void ContactsFillUpForm_Load(object sender, EventArgs e)
        {
            cmbContactType.SelectedIndex = 2;
        }

        //private void UpdateContactInfoTb(string pID, string pColName, string pColValue)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //            ContactInfoTb.Rows[i][pColName] = pColValue;
        //            break;
        //    }
        //}

        private void record_btn_Click(object sender, EventArgs e)
        {
            RecordValues();
            payeeInfo = ContactInfoTb.Rows[0]["Street"].ToString() + Environment.NewLine + ContactInfoTb.Rows[0]["City"].ToString() + " " + ContactInfoTb.Rows[0]["State"].ToString() + Environment.NewLine + ContactInfoTb.Rows[0]["Country"].ToString() + " " + ContactInfoTb.Rows[0]["PostCode"].ToString();
            DialogResult = DialogResult.OK;
        }

        void RecordValues()
        {
            ContactInfoTb.Rows.Add();

            if (ContactInfoTb != null)
            {
                ContactInfoTb.Rows[0]["ContactPerson"] = txtContactName.Text;
                ContactInfoTb.Rows[0]["Street"] = txtStreet.Text;
                ContactInfoTb.Rows[0]["City"] = txtCity.Text;
                ContactInfoTb.Rows[0]["State"] = txtState.Text;
                ContactInfoTb.Rows[0]["Postcode"] = txtPostcode.Text;
                ContactInfoTb.Rows[0]["Country"] = txtCountry.Text;
                ContactInfoTb.Rows[0]["Email"] = txtEmail.Text;
                ContactInfoTb.Rows[0]["Phone"] = txtPhone.Text;
                ContactInfoTb.Rows[0]["Fax"] = txtFax.Text;
                ContactInfoTb.Rows[0]["Website"] = txtWWW.Text;
                ContactInfoTb.Rows[0]["Comments"] = txtProfileNotes.Text;
                string contacttype = "";
                string sql = "SELECT TOP 1 ContactID, TypeOfContact FROM Contacts WHERE ProfileID = @ProfileID AND TypeOfContact LIKE @TypeOfContact ORDER BY ContactID DESC";
                DataTable dt = new DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProfileID", CustomerID);
                param.Add("@TypeOfContact", "%" + cmbContactType.Text + "%");
                CommonClass.runSql(ref dt, sql, param);
                if (dt.Rows.Count > 0)
                {
                    string[] result = dt.Rows[0]["TypeOfContact"].ToString().Split('-');
                    if (result.Count() > 0)
                    {
                        contacttype = cmbContactType.Text + "-" + (Convert.ToInt32(result[1]) + 1);
                    }
                }
                else
                {
                    contacttype = cmbContactType.Text + "-1";
                }
                ContactInfoTb.Rows[0]["TypeOfContact"] = contacttype;
            }
        }
    }
}
