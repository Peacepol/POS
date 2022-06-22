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
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;

namespace AbleRetailPOS
{
    public partial class LoyaltyMemberDetail : Form
    {
        private string ID = "";
        private bool CanSave = false;
        private int SaveMode = 0; //0 - View, 1 - Edit, 2 - New
        private DataTable TbRep = null;
        private DataGridViewRow selected_dgvrow = null;
        private bool isNew = false;
        private string LocID = "";
        private string mProfileID;
        private string thisFormCode = "";
        private bool CanEdit = false;
        private bool CanAdd = false;
        private bool CanDelete = false;


        public LoyaltyMemberDetail(string pFormCode,string pID = "", int pMode = 0, bool pSave = false)
        {
            InitializeComponent();
            ID = pID;
            CanSave = pSave;
            SaveMode = pMode;
            thisFormCode = pFormCode;

            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(thisFormCode, out FormRights);
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

        private void Customer_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = CanSave;
            txtNumber.Text = CommonClass.generateMemberNumber();

            if (SaveMode == 2)
            {
                //Create New.
               
                if(ID != "")
                    LoadForNewMember(ID);
            }
            else
            {
                LoadMember(ID);           
            }
        }


        private void LoadMember(string pID)
        {
            string sql = @"SELECT l.*, 
                                p.Name AS CustomerName
                           FROM LoyaltyMember l
                           INNER JOIN Profile p ON l.ProfileID=p.ID
                           WHERE l.ID=@ID";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", pID);

            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sql, param);

            foreach (DataRow drow in dt.Rows)
            {
                ID = drow["ID"].ToString();
                mProfileID = drow["ProfileID"].ToString();
                txtCustomerName.Text = drow["CustomerName"].ToString();
                label33.Text = "Customer ID: " + mProfileID;
                txtNumber.Text = drow["Number"].ToString();
                txtName.Text = drow["Name"].ToString();
                txtStreet.Text = drow["Street"].ToString();
                txtCity.Text = drow["City"].ToString();
                txtState.Text = drow["State"].ToString();
                txtPostcode.Text = drow["PostCode"].ToString();
                txtCountry.Text = drow["Country"].ToString();
                txtPhone.Text = drow["Phone"].ToString();
                txtFax.Text = drow["Fax"].ToString();
                txtEmail.Text = drow["Email"].ToString();
                dtStartDate.Value = Convert.ToDateTime(drow["StartDate"]).ToLocalTime();
                dtEndDate.Value = Convert.ToDateTime(drow["EndDate"]).ToLocalTime();
                chkActive.Checked = Convert.ToBoolean(drow["IsActive"]);
            }
        }
        private void LoadForNewMember(string pID)
        {
            string sql = @"SELECT p.ID,p.Name, Location, Street, City, State, PostCode, Country, 
                           Phone , Fax, Email, Website, ContactPerson, ProfileID
                           FROM  Profile p INNER JOIN Contacts c ON p.ID = c.ProfileID 
                           WHERE p.ID=@ID AND c.Location = p.LocationID";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", pID);

            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sql, param);
            
            foreach (DataRow drow in dt.Rows)
            {
                ID = drow["ID"].ToString();
                mProfileID = drow["ProfileID"].ToString();
                txtCustomerName.Text = drow["Name"].ToString();
                label33.Text = "Customer ID: " + mProfileID;
                txtName.Text = drow["Name"].ToString();
                txtStreet.Text = drow["Street"].ToString();
                txtCity.Text = drow["City"].ToString();
                txtState.Text = drow["State"].ToString();
                txtPostcode.Text = drow["PostCode"].ToString();
                txtCountry.Text = drow["Country"].ToString();
                txtPhone.Text = drow["Phone"].ToString();
                txtFax.Text = drow["Fax"].ToString();
                txtEmail.Text = drow["Email"].ToString();
            
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(SaveMode == 1)
            {
                UpdateMember(ID);
                this.DialogResult = DialogResult.OK;
            }
            else if (SaveMode == 2)
            {
                NewMember();
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool IsDuplicateProfile(string pProfileID)
        {
            string selectSql = "SELECT * FROM LoyaltyMember WHERE ProfileID=@ProfileID";

            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProfileID", pProfileID);

            CommonClass.runSql(ref dt, selectSql, param);
            if(dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;

                mProfileID = lProfile[0];
                txtCustomerName.Text = lProfile[2];
                label33.Text = "Customer ID: " + mProfileID;
            }
        }

        private void NewMember()
        {
            if (mProfileID != "" && !IsDuplicateProfile(mProfileID))
            {
                string sql = @"INSERT INTO LoyaltyMember(ProfileID,
                                                    Number,
                                                    Name,
                                                    Street,
                                                    City,
                                                    State,
                                                    PostCode,
                                                    Country,
                                                    Phone,
                                                    Fax,
                                                    Email,
                                                    StartDate,
                                                    EndDate,
                                                    IsActive)
                                            VALUES(@ProfileID,
                                                   @Number,
                                                   @Name,
                                                   @Street,
                                                   @City,
                                                   @State,
                                                   @PostCode,
                                                   @Country,
                                                   @Phone,
                                                   @Fax,
                                                   @Email,
                                                   @StartDate,
                                                   @EndDate,
                                                   @IsActive)";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProfileID", mProfileID);
                param.Add("@Number", txtNumber.Text);
                param.Add("@Name", txtName.Text);
                param.Add("@Street", txtStreet.Text);
                param.Add("@City", txtCity.Text);
                param.Add("@State", txtState.Text);
                param.Add("@PostCode", txtPostcode.Text);
                param.Add("@Country", txtCountry.Text);
                param.Add("@Phone", txtPhone.Text);
                param.Add("@Fax", txtFax.Text);
                param.Add("@Email", txtEmail.Text);
                param.Add("@StartDate", dtStartDate.Value.ToUniversalTime());
                param.Add("@EndDate", dtEndDate.Value.ToUniversalTime());
                param.Add("@IsActive", chkActive.Checked);

                int count = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                if (count > 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Loyalty Member " + txtName.Text);
                    string titles = "Information";
                    MessageBox.Show("Loyalty Member Record has been created.", titles);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid or duplicate customer");
                Close();
            }
        }

        private void UpdateMember(string pID)
        {
            string sql = @"UPDATE LoyaltyMember SET ProfileID = @ProfileID,
                                                    Number = @Number,
                                                    Name = @Name,
                                                    Street = @Street,
                                                    City = @City,
                                                    State = @State,
                                                    PostCode = @PostCode,
                                                    Country = @Country,
                                                    Phone = @Phone,
                                                    Fax = @Fax,
                                                    Email = @Email,
                                                    StartDate = @StartDate,
                                                    EndDate = @EndDate,
                                                    IsActive = @IsActive
                         WHERE ID=@ID";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            param.Add("@ProfileID", mProfileID);
            param.Add("@Number", txtNumber.Text);
            param.Add("@Name", txtName.Text);
            param.Add("@Street", txtStreet.Text);
            param.Add("@City", txtCity.Text);
            param.Add("@State", txtState.Text);
            param.Add("@PostCode", txtPostcode.Text);
            param.Add("@Country", txtCountry.Text);
            param.Add("@Phone", txtPhone.Text);
            param.Add("@Fax", txtFax.Text);
            param.Add("@Email", txtEmail.Text);
            param.Add("@StartDate", dtStartDate.Value.ToUniversalTime());
            param.Add("@EndDate", dtEndDate.Value.ToUniversalTime());
            param.Add("@IsActive", chkActive.Checked);
            int count = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            if (count > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Edited Loyalty Member ID " + lblID.Text, lblID.Text);
                string titles = "Information";
                MessageBox.Show("Loyalty Member Record has been updated.", titles);
                Close();
            }
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

    }
}