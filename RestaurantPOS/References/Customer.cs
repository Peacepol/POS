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
using AbleRetailPOS.Inventory;

namespace AbleRetailPOS
{
    public partial class Customer : Form
    {
        private string ID = "";
        private static string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanSave = false;
        private bool CanDelete = false;
        private int SaveMode = 0; //0 - View, 1 - Edit, 2 - New
        private DataRow CustomerRow;
        private DataTable TbRep = null;
        private DataTable TbContacts = null;
        private DataTable dtJobs = null;
        private DataGridViewRow selected_dgvrow = null;
        private bool isNew = false;
        private string LocID = "";
        private string LoyaltyID = "";
        private string ConID = "";

        public Customer(string pID, string pFormCode, int pMode = 0, bool pEdit = false)
        {
            InitializeComponent();
            ID = pID;
            thisFormCode = pFormCode;
            CanSave = pEdit;
            SaveMode = pMode;
            setPriceLevelCombo();
            InitTbContacts(ID);
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(pFormCode, out FormRights);
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
            foreach (DataGridViewColumn column in dgridJobs.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgridTran.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgvPayments.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgContractPricing.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
           

            LoadTerms();
            ApplyItemFieldAccess(CommonClass.UserID);

            if (SaveMode == 2)
            {
                //Create New.
                btnSpecimen.Enabled = false;
                btnSalesStatement.Enabled = false;
                populatedg();
                txtCompanyOrCustomer.Visible = false;
                CustomerOrCompanyName.Visible = false;
                tabPaymentsMade.Size = new Size(719, 588);
                tabPaymentsMade.Location = new Point(12, 14);
            }
            else
            {
                LoadCustomer(ID);
                txtCompanyOrCustomer.Visible = true;
                CustomerOrCompanyName.Visible = true;
                txtCompanyOrCustomer.Text = txtName.Text;
            }
            btnSave.Enabled = CanSave;

        }
        public void ApplyItemFieldAccess(String FieldID)
        {
            CommonClass.GetAccess(FieldID);
            //TABACCOUNTS CONTROLS
            foreach (Control c in gpAccount.Controls)
            {
                //COMBOBOX IN TABPROFILE
                if (c is ComboBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.ComboBox")
                    {
                        ComboBox chk = (ComboBox)c;
                        CheckFieldsRights(chk);
                    }                    
                }
                //NUMERICUPDOWN IN TABACCOUNTS
                if (c is NumericUpDown)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.NumericUpDown")
                    {
                        NumericUpDown chk = (NumericUpDown)c;
                        CheckRights(chk);
                    }
                }
            }
            foreach (Control c in TabAcountDetails.Controls)
            {
                //BUTTON IN TABACCOUNTDETAILS
                if (c is Button)
                {
                    Button btn = (Button)c;
                    CheckButtonRights(btn);
                }
            }
        }
        private void CheckFieldsRights(ComboBox item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        private void CheckRights(NumericUpDown item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        private void CheckButtonRights(Button item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        private void setPriceLevelCombo()
        {
            DataTable ltb = new DataTable();
            ltb.Columns.Add("level", typeof(int));
            ltb.Columns.Add("levelname", typeof(string));
            for (int i = 0; i <= 12; i++)
            {
                DataRow lRow = ltb.NewRow();
                lRow[0] = i;
                lRow[1] = "Level " + i.ToString();
                ltb.Rows.Add(lRow);
            }
            this.cboPriceLevel.DataSource = ltb;
            this.cboPriceLevel.ValueMember = "level";
            this.cboPriceLevel.DisplayMember = "levelname";
            this.cboPriceLevel.SelectedIndex = 0;
        }

        private void LoadTerms()
        {
            string selectSql = "SELECT * FROM TermsOfPayment";

            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, selectSql);

            cboTerms.DataSource = dt;
            cboTerms.ValueMember = "TermsOfPaymentID";
            cboTerms.DisplayMember = "Description";
            cboTerms.SelectedIndex = 0;
        }

        private void InitTbContacts(string pID)
        {
            string strcon = " WHERE ProfileID = " + (pID == "" ? "0" : pID);
            string selectSql = @"SELECT Location, Street, City, State, PostCode, Country, 
                                Phone , Fax, Email, Website, ContactPerson, ProfileID, Comments ,TypeOfContact 
                                FROM Contacts " + strcon;

            TbContacts = new DataTable();
            CommonClass.runSql(ref TbContacts, selectSql);

            if (TbContacts.Rows.Count == 0)
            {
                for (int i = 1; i <= TbContacts.Rows.Count; i++)
                {
                    DataRow nr = TbContacts.NewRow();
                    nr["Location"] = i.ToString();
                    TbContacts.Rows.Add(nr);
                }
            }
        }

        private void LoadCustomer(string pID)
        {
            btnSpecimen.Enabled = true;
            btnSalesStatement.Enabled = true;
            string selectSql = @"SELECT p.*, pmt.PaymentMethod, c.ContractPrice, c.ContractID, 
                                i.ItemName, i.PartNumber, s.Level0, c.ExpiryDate, c.IsExpiry  
                                FROM Profile p 
                                LEFT JOIN PaymentMethods pmt ON p.MethodOfPaymentID = pmt.id
                                LEFT JOIN ContractPricing c ON c.CustomerID = p.ID
                                LEFT JOIN Items i ON i.ID = c.ItemID
                                LEFT JOIN ItemsSellingPrice s ON s.ItemID = i.ID
                                WHERE p.type = 'Customer' AND p.ID = " + pID;
            DataTable dt = new DataTable();

            CommonClass.runSql(ref dt, selectSql);

            if (dt.Rows.Count > 0)
            {
                CustomerRow = dt.Rows[0];
                //INFORMATION
                lblID.Text = CustomerRow["ID"].ToString();
                txtName.Text = CustomerRow["Name"].ToString();
                txtCustNum.Text = CustomerRow["ProfileIDNumber"].ToString();
                chkActive.Checked = (CustomerRow["IsInactive"].ToString() == "0" ? true : false);
                LocID = CustomerRow["LocationID"] == null ? "0" : CustomerRow["LocationID"].ToString();

                if (CustomerRow["Designation"].ToString() == "Company")
                {
                    rdoCompany.Checked = true;
                }
                else
                {
                    rdoIndividual.Checked = true;
                }

                txtTaxCode.Text = CustomerRow["TaxCode"].ToString();
                txtFreightTaxCode.Text = CustomerRow["FreightTaxCode"].ToString();
                chkCustTaxCode.Checked = (CustomerRow["UseProfileTaxCode"].ToString() == "1" ? true : false);

                txtABN.Text = CustomerRow["ABN"].ToString();
                //txtABNBranch.Text = CustomerRow["ABNBranch"].ToString();
                //txtGSTIDNumber.Text = CustomerRow["GSTIDNumber"].ToString();
                txtTaxIDNumber.Text = CustomerRow["TaxIDNumber"].ToString();
                txtMethodofPayment.Text = CustomerRow["PaymentMethod"].ToString();
                txtShippingMethod.Text = CustomerRow["ShippingMethodID"].ToString();
                cboPriceLevel.SelectedValue = Convert.ToInt16(CustomerRow["ItemPriceLevel"].ToString());

                string bal = "0";
                string discount = "0";
                DataRow[] dr = TbContacts.Select("Location = " + 1);
                if (dr.Length > 0)
                {
                    DataRow rw = dr[0];
                    txtContactName.Text = rw["ContactPerson"].ToString();
                    txtEmail.Text = rw["Email"].ToString();
                    txtStreet.Text = rw["Street"].ToString();
                    txtCity.Text = rw["City"].ToString();
                    txtState.Text = rw["State"].ToString();
                    txtCountry.Text = rw["Country"].ToString();
                    txtPostcode.Text = rw["Postcode"].ToString();
                    txtPhone.Text = rw["Phone"].ToString();
                    txtFax.Text = rw["Fax"].ToString();
                    txtWWW.Text = rw["Website"].ToString();
                    txtProfileNotes.Text = rw["Comments"].ToString();
                }
                //Account Information
               
                cboTerms.SelectedValue = CustomerRow["TermsOfPayment"].ToString();
                switch (CustomerRow["TermsOfPayment"].ToString())
                {
                    case "DM"://Day of the Month
                        bal = CustomerRow["BalanceDueDate"].ToString();
                        discount = CustomerRow["DiscountDate"].ToString();

                        break;
                    case "DMEOM": //Day of the Month after EOM
                        bal = CustomerRow["BalanceDueDate"].ToString();
                        discount = CustomerRow["DiscountDate"].ToString();
                        break;
                    case "SD": //Specific Days
                        bal = CustomerRow["BalanceDueDays"].ToString();
                        discount = CustomerRow["DiscountDays"].ToString();
                        break;
                    case "SDEOM"://Specifc Day after EOM
                        bal = CustomerRow["BalanceDueDays"].ToString();
                        discount = CustomerRow["DiscountDays"].ToString();
                        break;
                    default: //CASH
                        break;
                }

                txtBalance.Value = bal != "" ? Convert.ToDecimal(bal) : 0;
                txtDiscount.Value = discount != "" ? Convert.ToDecimal(discount) : 0;
                string strcredlimit = CustomerRow["CreditLimit"].ToString();
                txtCreditLimit.Value = strcredlimit != "" ? Convert.ToDecimal(strcredlimit) : 0;
                string strearlypaymdiscpercent = CustomerRow["EarlyPaymentDiscountPercent"].ToString();
                txtEarlyPayment.Value = strearlypaymdiscpercent != "" ? Convert.ToDecimal(strearlypaymdiscpercent) : 0;
                string strltepaymchargepercent = CustomerRow["LatePaymentChargePercent"].ToString();
                txtLatePaymentCharge.Value = strltepaymchargepercent != "" ? Convert.ToDecimal(strltepaymchargepercent) : 0;
                string strvoldisc = CustomerRow["VolumeDiscount"].ToString();
                txtVolumeDiscount.Value = strvoldisc != "" ? Convert.ToDecimal(strvoldisc) : 0;
                txtSellingNotes.Text = CustomerRow["SellingNotes"].ToString();

                //AR BALANCE
                decimal lCBal = (CustomerRow["CurrentBalance"].ToString() == "" ? 0 : Convert.ToDecimal(CustomerRow["CurrentBalance"]));
                lblARBalance.Text = Math.Round(lCBal, 2).ToString("C");
                string pointssql = @"SELECT SUM(PointsAccumulated) AS TotalPoints 
                                FROM AccumulatedPoints 
                                WHERE CustomerID = @CustomerID";
                DataTable dx = new DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CustomerID", ID);
                CommonClass.runSql(ref dx, pointssql, param);
                if (dx.Rows.Count == 1 && dx.Rows[0]["TotalPoints"].ToString() != "")
                {
                    lblCustomerPoints.Text = dx.Rows[0]["TotalPoints"].ToString();
                }
                foreach(DataRow dtr in dt.Rows)
                {
                    int newrowindex = dgContractPricing.Rows.Add();
                    // ConID = CustomerRow["ContractID"].ToString();
                    string dte = dtr["ExpiryDate"].ToString();
                    if (dte != "")
                    {
                        expiryDate.Value = DateTime.Parse(dte).ToLocalTime();
                    }

                    if (dtr["IsExpiry"].ToString() == "true")
                    {
                        chkNeverExpire.Checked = true;
                    }
                    else
                    {
                        chkNeverExpire.Checked = false;
                    }
                    dgContractPricing["ContractID", newrowindex].Value = dtr["ContractID"].ToString();
                    dgContractPricing["PartNumber", newrowindex].Value = dtr["PartNumber"].ToString();
                    dgContractPricing["ItemsName", newrowindex].Value = dtr["ItemName"].ToString();
                    dgContractPricing["ContractPrice", newrowindex].Value = dtr["ContractPrice"].ToString();
                    dgContractPricing["SellingPrice", newrowindex].Value = dtr["Level0"].ToString();
                    dgContractPricing["ExpDate", newrowindex].Value = dtr["ExpiryDate"].ToString() == ""?"": Convert.ToDateTime(dtr["ExpiryDate"].ToString()).ToShortDateString();
                }
                populatedg();
            }
        }

        void populatedg()
        {
            dgContractPricing.ClearSelection();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
            dgContractPricing.Rows.Add();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveMode == 1)
            {
                UpdateCustomer(ID);
                txtCompanyOrCustomer.Text = txtName.Text;
            }
            else if (SaveMode == 2)
            {
                NewCustomer();
            }
        }

        private bool IsDuplicateProfileNo(string pNo)
        {
            string selectSql = "SELECT * from Profile WHERE type = 'Customer' AND ProfileIDNumber = '" + pNo + "'";

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

        private void NewCustomer()
        {
            SqlConnection con = null;
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Customer Name Required.");
                }
                else
                {
                    if (this.txtCustNum.Text != "")
                    {
                        if (IsDuplicateProfileNo(this.txtCustNum.Text))
                        {
                            MessageBox.Show("Customer Number already exists.");
                            return;
                        }
                    }
                    DateTime time = DateTime.Now;            // Use current time.
                    string format = "MM/dd/yyyy HH:mm:ss";   // Use this format.
                    string dts = time.ToString(format);

                    string sqli = @"INSERT INTO Profile (Type,ProfileIDNumber,Name,Designation,IsInactive,UseProfileTaxCode,FreightTaxCode,TaxCode,ABN,TaxIDNumber,
                                                         MethodOfPaymentID,ShippingMethodID,TermsOfPayment,CreditLimit,BalanceDueDays,BalanceDueDate,DiscountDays,DiscountDate,
                                                         EarlyPaymentDiscountPercent,LatePaymentChargePercent,VolumeDiscount,SellingNotes,ItemPriceLevel,ContactID) 
                                   VALUES (@Type, @ProfileIDNumber, @Name, @Designation, @IsInactive, @UseProfileTaxCode, @FreightTaxCode, @TaxCode, @ABN, @TaxIDNumber, 
                                             (SELECT id FROM PaymentMethods WHERE PaymentMethod = '" + txtMethodofPayment.Text + @"'), @ShippingMethodID, @TermsOfPayment, @CreditLimit,
                                            @BalanceDueDays, @BalanceDueDate, @DiscountDays, @DiscountDate, @EarlyPaymentDiscountPercent, @LatePaymentChargePercent, @VolumeDiscount, @SellingNotes, @ItemPriceLevel,@ContactID);  
                                   SELECT SCOPE_IDENTITY();";

                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(sqli, con);
                    cmd.CommandType = CommandType.Text;

                    //Customer Info
                    cmd.Parameters.AddWithValue("@Type", "Customer");
                    cmd.Parameters.AddWithValue("@ProfileIDNumber", txtCustNum.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    if (rdoCompany.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Designation", "Company");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Designation", "Individual");
                    }

                    if (chkActive.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@IsInactive", "0");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IsInactive", "1");
                    }

                    //common field
                    if (chkCustTaxCode.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@UseProfileTaxCode", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UseProfileTaxCode", "0");
                    }

                   // cmd.Parameters.AddWithValue("@IncomeAccountID", txtIncomeAccount.Text);

                    cmd.Parameters.AddWithValue("@FreightTaxCode", txtFreightTaxCode.Text);
                    cmd.Parameters.AddWithValue("@TaxCode", txtTaxCode.Text);
                    cmd.Parameters.AddWithValue("@ABN", txtABN.Text);
                   // cmd.Parameters.AddWithValue("@ABNBranch", txtABNBranch.Text);
                    cmd.Parameters.AddWithValue("@TaxIDNumber", txtTaxIDNumber.Text);
                 //   cmd.Parameters.AddWithValue("@GSTIDNumber", txtGSTIDNumber.Text);


                    cmd.Parameters.AddWithValue("@TermsOfPayment", cboTerms.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreditLimit", txtCreditLimit.Value);
                    cmd.Parameters.AddWithValue("@ItemPriceLevel", cboPriceLevel.SelectedValue);

                    decimal baldate = txtBalance.Value;
                    decimal discountdate = txtDiscount.Value;
                    decimal baldays = txtBalance.Value;
                    decimal discountdays = txtDiscount.Value;

                    cmd.Parameters.AddWithValue("@BalanceDueDays", baldays);
                    cmd.Parameters.AddWithValue("@BalanceDueDate", baldate);
                    cmd.Parameters.AddWithValue("@DiscountDays", discountdays);
                    cmd.Parameters.AddWithValue("@DiscountDate", discountdate);
                    cmd.Parameters.AddWithValue("@EarlyPaymentDiscountPercent", txtEarlyPayment.Value);
                    cmd.Parameters.AddWithValue("@LatePaymentChargePercent", txtLatePaymentCharge.Value);
                    cmd.Parameters.AddWithValue("@VolumeDiscount", txtVolumeDiscount.Text);
                    cmd.Parameters.AddWithValue("@ContactID", 1);

                    cmd.Parameters.AddWithValue("@ShippingMethodID", txtShippingMethod.Text);
                    cmd.Parameters.AddWithValue("@SellingNotes", txtSellingNotes.Text);

                    con.Open();
                    int profileid = Convert.ToInt32(cmd.ExecuteScalar());

                    if (profileid > 0)
                    {
                        NewContact(
                            profileid,
                            "1",
                            txtStreet.Text,
                            txtCity.Text,
                            txtState.Text,
                            txtPostcode.Text,
                            txtCountry.Text,
                            txtPhone.Text,
                            txtFax.Text,
                            txtEmail.Text,
                            txtContactName.Text,
                            txtWWW.Text,
                            txtProfileNotes.Text,
                            "Main");//Location 1 upon creation

                        if (!isContractPriceExists())
                        {
                            string itemID;
                            string contractPrice;
                            float contractP;
                            DateTime dtime = DateTime.Now;
                            DateTime dtpfromutc = expiryDate.Value.ToUniversalTime();
                            string expirydate = "";
                            bool isexpired; 
                            if (chkNeverExpire.Checked)
                            {
                                dtpfromutc = DateTime.MaxValue;
                                isexpired = false;
                                expiryDate.Enabled = false;
                            }
                            else
                            {
                                dtpfromutc = new DateTime(dtpfromutc.Year, dtpfromutc.Month, dtpfromutc.Day, 23, 59, 59);
                                isexpired = true;
                            }

                            for (int i = 0; i < this.dgContractPricing.Rows.Count; i++)
                            {
                                if (dgContractPricing.Rows[i].Cells["ItemID"].Value != null)
                                {
                                    itemID = dgContractPricing.Rows[i].Cells["ItemID"].Value.ToString();
                                    contractPrice = dgContractPricing.Rows[i].Cells["ContractPrice"].Value.ToString();

                                    Dictionary<string, object> contractparam = new Dictionary<string, object>();
                                    string sqlInsert = @"INSERT INTO ContractPricing(ItemID, CustomerID, ContractPrice, ExpiryDate, IsExpiry)
                                                         VALUES(@ItemID, @CustomerID, @ContractPrice, @ExpiryDate, @IsExpiry)";
                                    contractparam.Add("@ItemID", itemID);
                                    contractparam.Add("@CustomerID", profileid);
                                    contractparam.Add("@ContractPrice", contractPrice);
                                    contractparam.Add("@ExpiryDate", dtpfromutc);
                                    contractparam.Add("@IsExpiry", isexpired);
                                    CommonClass.runSql(sqlInsert, CommonClass.RunSqlInsertMode.QUERY, contractparam);
                                }
                            }
                            string title = "Information";
                            MessageBox.Show("Contract Record has been created.", title);
                        }

                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Customer " + txtName.Text);
                        string titles = "Information";
                        MessageBox.Show("Customer Record has been created.", titles);
                        DialogResult = DialogResult.OK;
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

        void NewContact(int profileID, string LocationID, string Street,
            string City, string State, string PostCode, string Country, string Phone,
            string Fax, string Email, string ContactPerson, string WWW, string Comments, string TypeOfContact)
        {

            Dictionary<string, object> paramcon = new Dictionary<string, object>();
            string sql2 = "INSERT INTO Contacts(Location, Street, City, State, PostCode, Country, Phone , Fax, Email, Website, ContactPerson, ProfileID, Comments, TypeOfContact)" +
                                      " VALUES (" + LocationID + ",@Street, @City, @State, @Postcode, @Country, @Phone, @Fax, @Email, @Website, @ContactPerson," + profileID + ", @Comments, @TypeOfContact)";

            paramcon.Add("@Street", Street);
            paramcon.Add("@City", City);
            paramcon.Add("@State", State);
            paramcon.Add("@Country", Country);
            paramcon.Add("@Postcode", PostCode);
            paramcon.Add("@Phone", Phone);
            paramcon.Add("@Fax", Fax);
            paramcon.Add("@Email", Email);
            paramcon.Add("@ContactPerson", ContactPerson);
            paramcon.Add("@Website", WWW);
            paramcon.Add("@Comments", Comments);
            paramcon.Add("@TypeOfContact", TypeOfContact);

            int i = CommonClass.runSql(sql2, CommonClass.RunSqlInsertMode.QUERY, paramcon);
            if (i > 0)
            {
                MessageBox.Show("Contact successfully Added", "Contact Info");
                LoadContact();
            }
        }

        private void UpdateCustomer(string pID)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Customer Name Required.");
            }
            else
            {
                DateTime time = DateTime.Now;            // Use current time.
                string format = "MM/dd/yyyy HH:mm:ss";   // Use this format.
                string dts = time.ToString(format);

                string sqli = "UPDATE Profile SET Type = @Type, ProfileIDNumber = @ProfileIDNumber, Name = @Name, Designation = @Designation, IsInactive = @IsInactive, " +
                    "UseProfileTaxCode = @UsePRofileTaxCode, FreightTaxCode = @FreightTaxCode, TaxCode = @TaxCode, ABN = @ABN, TaxIDNumber = @TaxIDNumber, " +
                    " MethodOfPaymentID = (SELECT id FROM PaymentMethods WHERE PaymentMethod = '" + txtMethodofPayment.Text + @"'), ShippingMethodID = @ShippingMethodID, " +
                    "TermsOfPayment = @TermsOfPayment, CreditLimit = @CreditLimit, BalanceDueDays = @BalanceDueDays, BalanceDueDate = @BalanceDueDate, DiscountDays = @DiscountDays, DiscountDate = @DiscountDate, " +
                    "EarlyPaymentDiscountPercent = @EarlyPaymentDiscountPercent, LatePaymentChargePercent = @LatePaymentChargePercent, VolumeDiscount = @VolumeDiscount, SellingNotes = @SellingNotes WHERE id = " + pID;

                Dictionary<string, object> param = new Dictionary<string, object>();

                //Customer Info
                param.Add("@Type", "Customer");
                param.Add("@ProfileIDNumber", txtCustNum.Text);
                param.Add("@name", txtName.Text);

                if (rdoCompany.Checked == true)
                {
                    param.Add("@Designation", "Company");
                }
                else
                {
                    param.Add("@Designation", "Individual");
                }
                if (chkActive.Checked == true)
                {
                    param.Add("@IsInactive", "0");
                }
                else
                {
                    param.Add("@IsInactive", "1");
                }

                //common field
                if (chkCustTaxCode.Checked == true)
                {
                    param.Add("@UseProfileTaxCode", "1");
                }
                else
                {
                    param.Add("@UseProfileTaxCode", "0");
                }

//param.Add("@IncomeAccountID", txtIncomeAccount.Text);
                param.Add("@FreightTaxCode", txtFreightTaxCode.Text);
                param.Add("@TaxCode", txtTaxCode.Text);
                param.Add("@ABN", txtABN.Text);
             //   param.Add("@ABNBranch", txtABNBranch.Text);
                param.Add("@TaxIDNumber", txtTaxIDNumber.Text);
              //  param.Add("@GSTIDNumber", txtGSTIDNumber.Text);
                param.Add("@TermsOfPayment", cboTerms.SelectedValue);
                param.Add("@CreditLimit", txtCreditLimit.Value);

                decimal baldate = txtBalance.Value;
                decimal discountdate = txtDiscount.Value;
                decimal baldays = txtBalance.Value;
                decimal discountdays = txtDiscount.Value;

                param.Add("@BalanceDueDays", baldays);
                param.Add("@BalanceDueDate", baldate);
                param.Add("@DiscountDays", discountdays);
                param.Add("@DiscountDate", discountdate);
                param.Add("@EarlyPaymentDiscountPercent", txtEarlyPayment.Value.ToString());
                param.Add("@LatePaymentChargePercent", txtLatePaymentCharge.Value.ToString());
                param.Add("@VolumeDiscount", txtVolumeDiscount.Text);
                param.Add("@ShippingMethodID", txtShippingMethod.Text);
                param.Add("@SellingNotes", txtSellingNotes.Text);

                int i = CommonClass.runSql(sqli, CommonClass.RunSqlInsertMode.QUERY, param);

                if (isContractPriceExists())
                {
                    DateTime dtime = DateTime.Now;
                    DateTime dtpfromutc = expiryDate.Value.ToUniversalTime();
                    bool isexpiry;
                    if (chkNeverExpire.Checked)
                    {
                        dtpfromutc = DateTime.MaxValue;
                        isexpiry = false;
                    }
                    else
                    {
                        dtpfromutc = new DateTime(dtpfromutc.Year, dtpfromutc.Month, dtpfromutc.Day, 23, 59, 59);
                        isexpiry = true;
                    }
                    for (int x = 0; x < dgContractPricing.Rows.Count; x++)
                    {
                        if (dgContractPricing.Rows[x].Cells["ContractPrice"].Value != null)
                        {
                            Dictionary<string, object> contractparam = new Dictionary<string, object>();
                            DataGridViewRow dgvRows = dgContractPricing.Rows[x];
                            string price = dgvRows.Cells["ContractPrice"].Value.ToString();
                            float cprice = float.Parse(price, NumberStyles.Currency);

                            string sqlUpdate = @"UPDATE ContractPricing SET ContractPrice = @ContractPrice, ExpiryDate = @ExpiryDate, IsExpiry = @IsExpiry WHERE CustomerID = " + pID;
                            contractparam.Add("@ContractPrice", cprice);
                            contractparam.Add("@ExpiryDate", dtpfromutc);
                            contractparam.Add("@IsExpiry", isexpiry);

                            CommonClass.runSql(sqlUpdate, CommonClass.RunSqlInsertMode.QUERY, contractparam);
                        }
                    }
                }
                else
                {
                    string itemID;
                    string contractPrice;
                    float contractP;
                    DateTime dtime = DateTime.Now;
                    DateTime dtpfromutc = expiryDate.Value.ToUniversalTime();
                    DateTime timeutc = dtime.ToUniversalTime();
                    bool isexpiry;
                    if (chkNeverExpire.Checked)
                    {
                        dtpfromutc = DateTime.MaxValue;
                        isexpiry = false;
                    }
                    else
                    {
                        dtpfromutc = new DateTime(dtpfromutc.Year, dtpfromutc.Month, dtpfromutc.Day, 23, 59, 59);
                        isexpiry = true;
                    }
                    for (int a = 0; a < this.dgContractPricing.Rows.Count; a++)
                    {
                        if (dgContractPricing.Rows[a].Cells["ItemID"].Value != null)
                        {
                            itemID = dgContractPricing.Rows[a].Cells["ItemID"].Value.ToString();
                            contractPrice = dgContractPricing.Rows[a].Cells["ContractPrice"].Value.ToString();

                            Dictionary<string, object> contractparam = new Dictionary<string, object>();
                            string sqlInsert = @"INSERT INTO ContractPricing(ItemID, CustomerID, ContractPrice, ExpiryDate, IsExpiry)
                                                    VALUES(@ItemID, @CustomerID, @ContractPrice, @ExpiryDate, @IsExpiry)";
                            contractparam.Add("@ItemID", itemID);
                            contractparam.Add("@CustomerID", ID);
                            contractparam.Add("@ContractPrice", contractPrice);
                            contractparam.Add("@ExpiryDate", dtpfromutc);
                            contractparam.Add("@IsExpiry", isexpiry);

                            CommonClass.runSql(sqlInsert, CommonClass.RunSqlInsertMode.QUERY, contractparam);
                        }
                    }
                    string title = "Information";
                    MessageBox.Show("Contract Record has been created.", title);
                }

                if (i > 0)
                {
                    UpdateContact(int.Parse(pID), "1",
                        txtStreet.Text,
                        txtCity.Text,
                        txtState.Text,
                        txtPostcode.Text,
                        txtCountry.Text,
                        txtPhone.Text,
                        txtFax.Text,
                        txtEmail.Text,
                        txtContactName.Text,
                        txtWWW.Text,
                        txtProfileNotes.Text, cbContactType.Text);

                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Edited Customer ID " + lblID.Text, lblID.Text);
                    string titles = "Information";
                    MessageBox.Show("Customer Record has been updated.", titles);
                    DialogResult = DialogResult.OK;
                }

                if (!isLoyalty.Checked && isLoyaltyMemberExists())
                {
                    string sql = "DELETE FROM LoyaltyMember WHERE ProfileID=@ProfileID";
                    Dictionary<string, object> delmemberparam = new Dictionary<string, object>();
                    delmemberparam.Add("@ProfileID", ID);
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, delmemberparam);
                }
            }
        }

        private bool isContractPriceExists()
        {
            string sql = "SELECT * FROM ContractPricing WHERE CustomerID = @ProfileID";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProfileID", ID);
            CommonClass.runSql(ref dt, sql, param);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isLoyaltyMemberExists()
        {
            string sql = "SELECT * FROM LoyaltyMember WHERE ProfileID=@ProfileID";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProfileID", ID);
            CommonClass.runSql(ref dt, sql, param);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void UpdateContact(int profileID, string LocationID, string Street,
                            string City, string State, string PostCode, string Country, string Phone,
                            string Fax, string Email, string ContactPerson, string WWW, string Comments, string TypeOfContact)
        {
            string sql2 = @"UPDATE Contacts SET 
                                      Street = @Street, 
                                      City = @City, 
                                      State = @State, 
                                      Postcode = @Postcode, 
                                      Country = @Country, 
                                      Phone = @Phone, 
                                      Fax = @Fax, 
                                      Email = @Email, 
                                      Website = @Website, 
                                      ContactPerson = @ContactPerson,
                                      Comments = @Comments,
                                      TypeOfContact = @TypeOfContact
                            WHERE ProfileID = " + profileID + " AND Location = " + LocationID;

            //  cmd.Parameters.AddWithValue("@Location", LocationID);// Address 1 upon creation
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Street", Street);
            param.Add("@City", City);
            param.Add("@State", State);
            param.Add("@Country", Country);
            param.Add("@Postcode", PostCode);
            param.Add("@Phone", Phone);
            param.Add("@Fax", Fax);
            param.Add("@Email", Email);
            param.Add("@ContactPerson", ContactPerson);
            param.Add("@Website", WWW);
            param.Add("@Comments", Comments);
            param.Add("@TypeOfContact", TypeOfContact);
            int i = CommonClass.runSql(sql2, CommonClass.RunSqlInsertMode.QUERY, param);
            if (i > 0)
            {
                MessageBox.Show("Contact successfully updated", "Contact Info");
                btnConSave.Enabled = false;
                LoadContact();
            }
        }

        private void cboTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isFieldEnabled = false;
            switch (cboTerms.SelectedValue.ToString())
            {
                case "DM"://Day of the Month
                    lblBalance.Text = "Balance Due Date";
                    lblDiscount.Text = "Discount Due Date";
                    lblBalanceNote.Text = "Specify Date of the Month (1-31)";
                    lblDiscountNote.Text = "Specify Date of the Month (1-31)";
                    isFieldEnabled = true;
                    break;
                case "DMEOM": //Day of the Month after EOM
                    lblBalance.Text = "Balance Due Date";
                    lblDiscount.Text = "Discount Date";
                    lblBalanceNote.Text = "Specify Date of the Month (1-31)";
                    lblDiscountNote.Text = "Specify Date of the Month (1-31)";
                    isFieldEnabled = true;
                    break;
                case "SD": //Specific Days
                    lblBalance.Text = "Balance Due Days";
                    lblDiscount.Text = "Discount Days";
                    lblBalanceNote.Text = "Specify # of Days";
                    lblDiscountNote.Text = "Specify # of Days";
                    isFieldEnabled = true;
                    break;
                case "SDEOM"://Specifc Day after EOM
                    lblBalance.Text = "Balance Due Days";
                    lblDiscount.Text = "Discount Days";
                    lblBalanceNote.Text = "Specify # of Days";
                    lblDiscountNote.Text = "Specify # of Days";
                    isFieldEnabled = true;
                    break;
                default: //CASH           
                    txtBalance.Value = 0;
                    txtCreditLimit.Value = 0;
                    txtDiscount.Value = 0;
                    txtEarlyPayment.Value = 0;
                    txtLatePaymentCharge.Value = 0;
                    lblBalanceNote.Text = "     ";
                    lblDiscountNote.Text = "     ";
                    isFieldEnabled = false;
                    break;
            }
            txtBalance.Enabled = isFieldEnabled;
            txtCreditLimit.Enabled = isFieldEnabled;
            txtDiscount.Enabled = isFieldEnabled;
            txtEarlyPayment.Enabled = isFieldEnabled;
            txtLatePaymentCharge.Enabled = isFieldEnabled;
        }

        private void pbTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                txtTaxCode.Text = Tax[0];
            }
        }

        private void pbFTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                txtFreightTaxCode.Text = Tax[0];
            }
        }

        private void pbPayment_Click(object sender, EventArgs e)
        {
            PaymentMethodLookup DlgPaymentMethod = new PaymentMethodLookup();
            if (DlgPaymentMethod.ShowDialog() == DialogResult.OK)
            {
                string[] lPMethod = DlgPaymentMethod.GetPaymentMethod;
                txtMethodofPayment.Text = lPMethod[0];

            }
        }

        private void pbShipping_Click(object sender, EventArgs e)
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;
                txtShippingMethod.Text = ShipList[0];
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime dtpfromutc = this.dtpFrom.Value.ToUniversalTime();
            DateTime dtptoutc = this.dtpTo.Value.ToUniversalTime();

            string sdate = dtpfromutc.ToString("yyyy-MM-dd") + " 00:00:00";
            string edate = dtptoutc.ToString("yyyy-MM-dd") + " 23:59:59";
            SearchTrans(sdate, edate, ID);
        }

        private void SearchTrans(string pDateFrom, string pDateTo, string pProfileID)
        {
            string sql = "";
            if (cbSalesType.Text == "QUOTE")
            {

                sql = @"SELECT SalesNumber AS TransactionNumber,
                            s.TransactionDate, 
                            s.Memo,
                            GrandTotal AS Amount, 
                             SalesType as Type,
                            SalesID AS ID,
							Name,
							ProfileIDNumber
                    FROM Sales s INNER JOIN Profile p ON s.CustomerID = p.ID
                    WHERE CustomerID = '" + pProfileID + "'" +
                    " AND s.TransactionDate BETWEEN @sdate AND @edate AND SalesType = 'QUOTE' order by s.TransactionDate";
            }
            else if (cbSalesType.Text == "ORDER")
            {
                sql = @"SELECT SalesNumber AS TransactionNumber,
                            s.TransactionDate, 
                            s.Memo,
                            GrandTotal AS Amount, 
                             SalesType as Type,
                            SalesID AS ID,
							Name,
							ProfileIDNumber
                    FROM Sales s INNER JOIN Profile p ON s.CustomerID = p.ID
                    WHERE CustomerID = '" + pProfileID + "'" +
                    " AND s.TransactionDate BETWEEN @sdate AND @edate AND SalesType = 'ORDER' order by s.TransactionDate";
            }
            else if (cbSalesType.Text == "INVOICE")
            {
                sql = @"SELECT DISTINCT SalesNumber AS TransactionNumber,
                            s.TransactionDate, 
                            s.Memo,
                            GrandTotal AS Amount, 
                            SalesType as Type,
                            SalesID AS ID,
							Name,
							ProfileIDNumber
                    FROM Sales s INNER JOIN Profile p ON s.CustomerID = p.ID
                    WHERE CustomerID = '" + pProfileID + "'" +
                    " AND s.TransactionDate BETWEEN @sdate AND @edate  AND  SalesType IN ('INVOICE','SINVOICE') order by s.TransactionDate";
            }
            else
            {
               /* //Payment
                sql += @"SELECT PaymentNumber AS TransactionNumber, 
                            m.TransactionDate, 
                            m.Memo,
                            TotalAmount AS Amount, 
                            j.Type,
                            PaymentID AS ID,
							Name,
							ProfileIDNumber
                        FROM Payment m INNER JOIN Profile p ON m.ProfileID = p.ID
                        INNER JOIN Journal j ON j.TransactionNumber = m.PaymentNumber 
                        WHERE ProfileID = '" + pProfileID + "'" +
                        " AND m.TransactionDate BETWEEN @sdate AND @edate ";
                //Sales
                */
                sql += @"SELECT SalesNumber AS TransactionNumber, 
                            s.TransactionDate, 
                            s.Memo,
                            GrandTotal AS Amount, 
                            SalesType as Type,
                            SalesID AS ID,
							Name,
							ProfileIDNumber
                        FROM Sales s INNER JOIN Profile p ON s.CustomerID = p.ID                     
                        WHERE CustomerID = '" + pProfileID + "'" +
                        " AND s.TransactionDate BETWEEN @sdate AND @edate order by s.TransactionDate";

            }

       

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@sdate", pDateFrom);
            param.Add("@edate", pDateTo);
            TbRep = new DataTable();
            CommonClass.runSql(ref TbRep, sql, param);

            this.dgridTran.DataSource = TbRep;
            for (int i = 0; i < this.dgridTran.Rows.Count; i++)
            {
                if (this.dgridTran.Rows[i].Cells["TransactionDate"].Value != null)
                {
                    if (this.dgridTran.Rows[i].Cells["TransactionDate"].Value.ToString() != "")
                    {
                        this.dgridTran.Rows[i].Cells["TransactionDate"].Value = Convert.ToDateTime(this.dgridTran.Rows[i].Cells["TransactionDate"].Value.ToString()).ToShortDateString();
                    }
                }
            }
            this.dgridTran.Columns[0].HeaderText = "Transaction No";
            this.dgridTran.Columns[0].Width = 90;
            this.dgridTran.Columns[1].HeaderText = "Date";
            this.dgridTran.Columns[1].Width = 80;
            this.dgridTran.Columns[2].HeaderText = "Memo";
            this.dgridTran.Columns[2].Width = 240;
            this.dgridTran.Columns[3].HeaderText = "Amount";
            this.dgridTran.Columns[3].Width = 120;
            this.dgridTran.Columns[4].HeaderText = "Type";
            this.dgridTran.Columns[4].Width = 70;
            this.dgridTran.Columns[5].HeaderText = "Record ID";
            this.dgridTran.Columns[6].Width = 70;
            this.dgridTran.Columns[3].DefaultCellStyle.Format = "C2";
            this.dgridTran.Columns[6].Visible = false;
            this.dgridTran.Columns[7].Visible = false;
        }

        private void LoadJobs(string pProfileID, DateTime jStartDate , DateTime jEndDate)
        {
            if (pProfileID == "")
                return;
            DateTime lSdate = new DateTime(jStartDate.Year, jStartDate.Month, jStartDate.Day, 00, 00, 00);
            DateTime lEdate = new DateTime(jEndDate.Year, jEndDate.Month, jEndDate.Day, 23, 59, 59);

            Dictionary<string, object> jobParam = new Dictionary<string, object>();
            jobParam.Add("@sdate", lSdate);
            jobParam.Add("@edate", lEdate);
            jobParam.Add("@INfrom", jobInvoiceStart.Text);
            jobParam.Add("@INto", jobInvoiceEnd.Text);
            dgridJobs.DataSource = null;
            dgridJobs.Rows.Clear();

            string sql = @"SELECT j.JobID, JobCode, ContactName, PercentCompleted, StartDate, FinishDate, JobName, s.TransactionDate 
                        FROM Jobs j LEFT JOIN SalesLines sl ON sl.JobID = j.JobID
                        INNER JOIN Sales s ON s.SalesID = sl.SalesID 
                        WHERE s.CustomerID = " + pProfileID;
            if (jStartDate != null || jEndDate != null)
            {
                sql += " AND s.TransactionDate BETWEEN @sdate and @edate";
            }
            if (jobInvoiceStart.Text != "ALL" && jobInvoiceEnd.Text != "ALL")
            {
                sql += " AND s.SalesNumber BETWEEN @INfrom and @INto ";
            }

            /*DataTable*/
            dtJobs = new DataTable();

            CommonClass.runSql(ref dtJobs, sql,jobParam);
            if(dtJobs.Rows.Count > 0)
            {
                this.dgridJobs.DataSource = dtJobs;
                dgridJobs.Columns["JobID"].Visible = false;
                //dgridJobs.Columns["JobCode"].Visible = false;
                //dgridJobs.Columns["ContactName"].Visible = false;
                //dgridJobs.Columns["PercentCompleted"].Visible = false;
                //dgridJobs.Columns["StartDate"].Visible = false;
                //dgridJobs.Columns["FinishDate"].Visible = false;
                dgridJobs.Columns["TransactionDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
            else
            {
                MessageBox.Show("Contains no data.", "Customer jobs information");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            string profiletype = "Customer";
            SqlConnection con = null;
            try
            {
                Reports.ReportParams profileparams = new Reports.ReportParams();
                profileparams.PrtOpt = 1;
                profileparams.Rec.Add(TbRep);
                profileparams.ReportName = "ProfileTxHistory.rpt";
                profileparams.RptTitle = "Transaction History";
                profileparams.Params = "compname|profiletype|StartDate|EndDate|ProfileName";
                profileparams.PVals = CommonClass.CompName.Trim() + "|" + profiletype + "|" + dtpFrom.Value.ToShortDateString() + " |" + dtpTo.Value.ToShortDateString() + " |" + txtName.Text;

                CommonClass.ShowReport(profileparams);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void LoadContacts(int pIndex)
        {
            DataRow[] dr = TbContacts.Select("Location = " + pIndex);
            if (dr.Length > 0)
            {
                DataRow rw = dr[0];
                ContactPerson.Text = rw["ContactPerson"].ToString();
                Email.Text = rw["Email"].ToString();
                Street.Text = rw["Street"].ToString();
                City.Text = rw["City"].ToString();
                State.Text = rw["State"].ToString();
                Country.Text = rw["Country"].ToString();
                PostCode.Text = rw["Postcode"].ToString();
                Phone.Text = rw["Phone"].ToString();
                Fax.Text = rw["Fax"].ToString();
                Website.Text = rw["Website"].ToString();
                Comment.Text = rw["Comments"].ToString();
                cbContactType.Text = rw["TypeOfContact"].ToString();
                string sql = "SELECT ContactID, TypeOfContact FROM Contacts WHERE ProfileID = @ProfileID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProfileID", ID);
                param.Add("@Location", pIndex);
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, sql, param);
                List<KeyValuePair<string, string>> mylist = new List<KeyValuePair<string, string>>();
                if (cbContactType.Text == "Main")
                {
                    cbContactType.Enabled = false;
                }
                else
                {
                    cbContactType.Enabled = true;
                }
            }
        }

        private void UpdateContactTb(int pIndex, string pColName, string pColValue)
        {
            for (int i = 0; i < 5; i++)
            {
                if (TbContacts.Rows[i]["Location"].ToString() == pIndex.ToString())
                {
                    TbContacts.Rows[i][pColName] = pColValue;
                    break;
                }
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            if (ID == "")
                return;
            DateTime dtpfromutc = this.paymentDateFrom.Value.ToUniversalTime();
            DateTime dtptoutc = this.paymentDateTo.Value.ToUniversalTime();

            string sdate = dtpfromutc.ToString("yyyy-MM-dd") + " 00:00:00";
            string edate = dtptoutc.ToString("yyyy-MM-dd") + " 23:59:59";
            btnSave.Enabled = false;
            LoadInvoiceNumber(cbInvoiceFrom, cbInvoiceTo);
            LoadPayments("", "", cbInvoiceFrom.Text, cbInvoiceTo.Text);
        }

        void LoadPayments(string pDateFrom, string pDateTo, string InFrom, string InTo)
        {
            if (ID != "")
            {
                string paymentsql = @"SELECT p.PaymentID,s.SalesNumber, PaymentNumber, p.TransactionDate, TotalAmount, PaymentFor, p.Memo 
                                            FROM Payment p 
                                            INNER JOIN PaymentLines pl ON pl.PaymentID= p.PaymentID 
                                            INNER JOIN Sales s ON s.SalesID = pl.EntityID 
                                            WHERE ProfileID = " + ID + " ";
                if (pDateFrom != "" || pDateTo != "")
                {
                    paymentsql += " AND p.TransactionDate BETWEEN @sdate and @edate";
                }
                if (cbInvoiceFrom.Text != "ALL" && cbInvoiceTo.Text != "ALL")
                {
                    paymentsql += " AND s.SalesNumber BETWEEN @INfrom and @INto ";
                }
                Dictionary<string, object> paymentparam = new Dictionary<string, object>();
                paymentparam.Add("@sdate", pDateFrom);
                paymentparam.Add("@edate", pDateTo);
                paymentparam.Add("@INfrom", InFrom);
                paymentparam.Add("@INto", InTo);

                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, paymentsql, paymentparam);

                if (dt.Rows.Count > 0)
                {
                    dgvPayments.DataSource = dt;
                    dgvPayments.Columns[0].Visible = false;
                    dgvPayments.Columns[1].Visible = false;

                }
                else
                {
                    MessageBox.Show("Contains no data.", "Customer Payment Information");
                }

            }
        }
        private void LoadInvoiceNumber(ComboBox INfrom , ComboBox INto)
        {
            string sql = @"SELECT SalesNumber FROM Sales  WHERE CustomerID = " + ID + " ";
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sql);
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow dr = dt.Rows[x];
                INfrom.Items.Add(dr["SalesNumber"].ToString());
                INto.Items.Add(dr["SalesNumber"].ToString());
            }
        }

        private void tabPaymentsMade_Enter(object sender, EventArgs e)
        {
            if (ID == "")
                return;
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            if (ID == "")
                return;
            btnSave.Enabled = false;
            LoadInvoiceNumber(jobInvoiceStart, jobInvoiceEnd);
            LoadJobs(ID, jobStartDate.Value, jobStartDate.Value);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void dgvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            selected_dgvrow = dgvPayments.SelectedRows[0];
        }

        private void dgvPayments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            selected_dgvrow = dgvPayments.SelectedRows[0];
            if (selected_dgvrow != null && selected_dgvrow.Cells.Count > 0)
            {
                if (CommonClass.SRPaymentsfrm != null
                && !CommonClass.SRPaymentsfrm.IsDisposed)
                {
                    CommonClass.SRPaymentsfrm.Close();
                }
                CommonClass.SRPaymentsfrm = new SalesReceivePayment(CommonClass.InvocationSource.CUSTOMER,
                                                                    selected_dgvrow.Cells["PaymentID"].Value.ToString());
                this.Cursor = Cursors.WaitCursor;
                CommonClass.SRPaymentsfrm.MdiParent = this.MdiParent;
                CommonClass.SRPaymentsfrm.Show();
                CommonClass.SRPaymentsfrm.Focus();
                if (CommonClass.SRPaymentsfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.SRPaymentsfrm.Close();
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void dgridTran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow selectedtran = dgridTran.SelectedRows[0];
            string transactionno = selectedtran.Cells["TransactionNumber"].Value.ToString();
            int transactionid = Convert.ToInt32(selectedtran.Cells["ID"].Value);
            switch (selectedtran.Cells["Type"].Value.ToString())
            {
                case "SI":
                    CommonClass.EnterSalesfrm = new Sales.EnterSales(CommonClass.InvocationSource.SELF, transactionid.ToString());
                    this.Cursor = Cursors.WaitCursor;
                    CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
                    CommonClass.EnterSalesfrm.Show();
                    CommonClass.EnterSalesfrm.Focus();
                    if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel
                        || CommonClass.EnterSalesfrm.DialogResult == DialogResult.OK)
                    {
                        CommonClass.EnterSalesfrm.Close();
                    }
                    this.Cursor = Cursors.Default;
                    break;
                case "HS":
                    CommonClass.ARBalanceEntryFrm = new ARBalanceEntry("Accounts Receivable Starting Balances", "", transactionid.ToString());
                    this.Cursor = Cursors.WaitCursor;
                    CommonClass.ARBalanceEntryFrm.MdiParent = this.MdiParent;
                    CommonClass.ARBalanceEntryFrm.Show();
                    CommonClass.ARBalanceEntryFrm.Focus();
                    if (CommonClass.ARBalanceEntryFrm.DialogResult == DialogResult.Cancel)
                    {
                        CommonClass.ARBalanceEntryFrm.Close();
                    }
                    this.Cursor = Cursors.Default;
                    break;
            }
        }

        private void btnSalesStatement_Click(object sender, EventArgs e)
        {
            LoadReportSalesStatement();
        }

        private void LoadReportSalesStatement()
        {
            SqlConnection con = null;
            try
            {
                DateTime edate = DateTime.UtcNow;
                string sql = @"SELECT GrandTotal, Name, SalesNumber, TransactionDate, TotalPaid, Memo, c.Street, c.City, c.State, c.Postcode, c.Country, p.ABN, d.CompanyName, d.SalesTaxNumber, d.Add1, d.Add2, 
                                 d.City as CCity,  d.Street as CStreet, d.State as CState, d.Phone as CPhone
                                 From DataFileInformation d, Sales s
                                 INNER JOIN Profile p ON s.CustomerID = p.ID
                                 INNER JOIN Contacts c ON p.ID = c.ProfileID
                                 WHERE InvoiceStatus = 'Open' AND CustomerID=" + ID + " AND c.Location = p.LocationID";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);

                Reports.ReportParams SalesStatement = new Reports.ReportParams();
                SalesStatement.PrtOpt = 1;
                SalesStatement.Rec.Add(TbRep);
                SalesStatement.Params = "compname|StatementDate";
                SalesStatement.PVals = CommonClass.CompName.Trim() + "|" + edate.ToString("yyyy-MM-dd HH:mm:ss");
                SalesStatement.ReportName = "SalesStatements.rpt";
                SalesStatement.RptTitle = "Sales Statement";


                CommonClass.ShowReport(SalesStatement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void dgvPayments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4
                && e.RowIndex != this.dgvPayments.NewRowIndex)
            {
                if (e.Value != null)
                {
                    decimal d = decimal.Parse(e.Value.ToString(), NumberStyles.Any);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void btnSpecimen_Click(object sender, EventArgs e)
        {
            CommonClass.PurchaseOrderSpecimen = new References.PurchaseOrderSpecimen(ID, LocID);
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PurchaseOrderSpecimen.MdiParent = this.MdiParent;
            CommonClass.PurchaseOrderSpecimen.Show();
            CommonClass.PurchaseOrderSpecimen.Focus();
            if (CommonClass.PurchaseOrderSpecimen.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PurchaseOrderSpecimen.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnSearchPayment_Click(object sender, EventArgs e)
        {
            DateTime dtpfromutc = this.paymentDateFrom.Value.ToUniversalTime();
            DateTime dtptoutc = this.paymentDateTo.Value.ToUniversalTime();

            string sdate = dtpfromutc.ToString("yyyy-MM-dd") + " 00:00:00";
            string edate = dtptoutc.ToString("yyyy-MM-dd") + " 23:59:59";
            LoadPayments(sdate, edate, cbInvoiceFrom.Text, cbInvoiceTo.Text);
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnConSave.Enabled = false;
            grpContact.Enabled = false;
            dgContacts.Enabled = true;
            if (TbContacts.Rows.Count < 0)
                MessageBox.Show("Please save a main address first");
            LoadContact();
        }

        void LoadContact()
        {
            InitTbContacts(ID);
            dgContacts.Rows.Clear();
            if (TbContacts.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            for (int x = 0; x < TbContacts.Rows.Count; x++)
            {
                DataRow dr = TbContacts.Rows[x];
                dgContacts.Rows.Add();
                dgContacts.Rows[x].Cells["cContactPerson"].Value = dr["ContactPerson"].ToString();
                dgContacts.Rows[x].Cells["cAddress"].Value = dr["Street"].ToString() + " " + dr["City"].ToString() + " " + dr["State"].ToString() + " " + dr["PostCode"].ToString() + " " + dr["Country"].ToString();
                dgContacts.Rows[x].Cells["cContactNumber"].Value = dr["Phone"].ToString();
                dgContacts.Rows[x].Cells["cEmail"].Value = dr["Email"].ToString();
                dgContacts.Rows[x].Cells["DelCon"].Value = "false";
                dgContacts.Rows[x].Cells["LocationID"].Value = dr["Location"].ToString(); ;
            }
            string[] TypeContact = new string[] {
                                            "Shipping",
                                            "Billing",
                                            "Other" };
            BindingList<string> ContactType = new BindingList<string>(TypeContact);
            cbContactType.DataSource = ContactType;
            cbContactType.SelectedIndex = 0;
        }

        private void btnConSave_Click(object sender, EventArgs e)
        {
            if (isNew)
            {
                string contacttype = "";
                string sql = "SELECT TOP 1 ContactID, TypeOfContact FROM Contacts WHERE ProfileID = @ProfileID AND TypeOfContact LIKE @TypeOfContact ORDER BY ContactID DESC";
                DataTable dt = new DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProfileID", ID);
                param.Add("@TypeOfContact", "%" + cbContactType.Text + "%");
                CommonClass.runSql(ref dt, sql, param);
                if (dt.Rows.Count > 0)
                {
                    string[] result = dt.Rows[0]["TypeOfContact"].ToString().Split('-');
                    if (result.Count() > 0)
                    {
                        contacttype = cbContactType.Text + "-" + (Convert.ToInt32(result[1]) + 1);
                    }
                }
                else
                {
                    contacttype = cbContactType.Text + "-1";
                }

                NewContact(int.Parse(ID), 
                            "((SELECT MAX( Location ) FROM Contacts WHERE ProfileID = " + ID + " ) + 1 )",
                            Street.Text,
                            City.Text,
                            State.Text,
                            PostCode.Text,
                            Country.Text,
                            Phone.Text,
                            Fax.Text,
                            Email.Text,
                            ContactPerson.Text,
                            Website.Text,
                            Comment.Text,
                            contacttype);

            }
            else
            {
                UpdateContact(int.Parse(ID), 
                            LocID, 
                            Street.Text,
                            City.Text,
                            State.Text,
                            PostCode.Text,
                            Country.Text,
                            Phone.Text,
                            Fax.Text,
                            Email.Text,
                            ContactPerson.Text,
                            Website.Text,
                            Comment.Text, 
                            cbContactType.Text);
            }

            btnCancel.PerformClick();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isNew = true;
            grpContact.Enabled = true;
            btnConSave.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            ContactPerson.Text = "";
            Street.Text = "";
            City.Text = "";
            State.Text = "";
            PostCode.Text = "";
            Country.Text = "";
            Email.Text = "";
            Phone.Text = "";
            Fax.Text = "";
            Website.Text = "";
            Comment.Text = "";
            cbContactType.DataSource = null;
            cbContactType.Items.Clear();
            string[] TypeContact = new string[] {
                                            "Shipping",
                                            "Billing",
                                            "Other" };
            BindingList<string> ContactType = new BindingList<string>(TypeContact);
            cbContactType.DataSource = ContactType;
            cbContactType.SelectedIndex = 0;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isNew = false;          
            grpContact.Enabled = true;
            btnConSave.Enabled = true;
        }

        private void dgContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            dgContacts.Rows[e.RowIndex].Selected = true;
            LocID = dgContacts.Rows[e.RowIndex].Cells["LocationID"].Value.ToString();
            LoadContacts(int.Parse(LocID));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnConSave.Enabled = false;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = true;
            btnDelOK.Visible = false;
            grpContact.Enabled = false;
            dgContacts.Columns["DelCon"].Visible = false;
            ContactPerson.Text = "";
            Street.Text = "";
            City.Text = "";
            State.Text = "";
            PostCode.Text = "";
            Country.Text = "";
            Email.Text = "";
            Phone.Text = "";
            Fax.Text = "";
            Website.Text = "";
            Comment.Text = "";
            LocID = "";
            LoadContact();
            cbContactType.DataSource = null;
            cbContactType.Items.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LocID = "";
            dgContacts.Columns["DelCon"].Visible = true;

            btnDelOK.Visible = true;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnConSave.Enabled = false;
        }

        private int DeleteCon(string profileID, string LocationID)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string sql2 = @"DELETE FROM Contacts
                                   WHERE ProfileID = " + profileID + " AND Location in (" + LocationID + ")";
                SqlCommand cmd = new SqlCommand(sql2, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnDelOK_Click(object sender, EventArgs e)
        {
            int i = 0;
            string LocIDs = "";
            foreach (DataGridViewRow item in dgContacts.Rows)
            {
                if (bool.Parse(item.Cells["DelCon"].Value.ToString()))
                {
                    LocIDs += item.Cells["LocationID"].Value.ToString() + ",";
                    i++;
                }
            }
            if (i > 0)
            {
                LocIDs = LocIDs.Remove(LocIDs.Length - 1);
                int x = DeleteCon(ID, LocIDs);
                if (x != 0)
                {
                    MessageBox.Show("Contact Delete Successsfully", "Information");
                }
                LocIDs = "";
                btnDelOK.Visible = false;
                LoadContact();
                dgContacts.Columns["DelCon"].Visible = true;
            }
            else
            {
                MessageBox.Show("Must check atleast 1 to be deleted", "Information");
            }

        }

        private void dgContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void isLoyalty_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoyalty.Checked)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }
        }

        private void LoadPoints()
        {
            string pointssql = @"SELECT SUM(PointsAccumulated) AS TotalPoints
                                 FROM AccumulatedPoints 
                                 WHERE CustomerID = @CustomerID";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CustomerID", ID);
            CommonClass.runSql(ref dt, pointssql, param);
            if (dt.Rows.Count == 1 && dt.Rows[0]["TotalPoints"].ToString() != "")
            {
                lblCustomerPoints.Text = dt.Rows[0]["TotalPoints"].ToString();
            }
        }

        private void LoadLoyaltyMemberInfo()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ProfileID", ID);
            CommonClass.runSql(ref dt, "SELECT * FROM LoyaltyMember WHERE ProfileID=@ProfileID", param);
            btnLoyalty.Enabled = true;
            if (dt.Rows.Count > 0)
            {
                isLoyalty.Checked = true;

                LoadPoints();
                LoyaltyID = dt.Rows[0]["ID"].ToString();
                dtpStartDate.Value = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToLocalTime();
                dtpEndDate.Value = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToLocalTime();
                lblMemberNo.Text = dt.Rows[0]["Number"].ToString();
                lblActive.Text = dt.Rows[0]["IsActive"].ToString();
                btnLoyalty.Text = "View member details";
                isLoyalty.Enabled = true;
            }
            else
            {
                btnLoyalty.Text = "Add customer to loyalty";
            }
        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {
            if (ID == "")
                return;

            LoadLoyaltyMemberInfo();

            DataTable dx = new DataTable();
            string pointsAccSql = @"SELECT * FROM AccumulatedPoints ap 
            INNER JOIN Promos p ON p.PromoID = ap.PromoID 
            INNER JOIN SalesLines s ON s.SalesLineID = ap.SalesLineID 
            INNER JOIN Items i ON i.ID = s.EntityID 
            LEFT JOIN LoyaltyMember lm ON ap.CustomerID = lm.ProfileID
            WHERE PointsAccumulated > 0 AND CustomerID = " + ID;
            CommonClass.runSql(ref dx, pointsAccSql);
            if (dx.Rows.Count > 0)
            {
                for (int o = 0; o < dx.Rows.Count; o++)
                {
                    DataRow dt = dx.Rows[o];
                    dgvAccumulation.Rows.Add();
                    dgvAccumulation.Rows[o].Cells[0].Value = dt["PromoCode"].ToString();
                    dgvAccumulation.Rows[o].Cells[1].Value = Convert.ToDateTime(dt["TransactionDate"].ToString()).ToLocalTime().ToShortDateString();
                    dgvAccumulation.Rows[o].Cells[2].Value = dt["PointsAccumulated"].ToString();
                    dgvAccumulation.Rows[o].Cells[3].Value = dt["ItemName"].ToString();
                    //dgvAccumulation.Rows[o].Cells[3].Value = dt["PromoCode"].ToString();
                }
            }

            DataTable dp = new DataTable();
            string pointsRedql = @"SELECT * FROM AccumulatedPoints ap INNER JOIN Items i ON i.ID = ap.ItemID WHERE PointsAccumulated < 0 AND CustomerID = " + ID;
            CommonClass.runSql(ref dp, pointsRedql);
            if (dx.Rows.Count > 0)
            {
                for (int a = 0; a < dp.Rows.Count; a++)
                {
                    DataRow dt = dp.Rows[a];
                    dgvRedemption.Rows.Add();
                    dgvRedemption.Rows[a].Cells[0].Value = dt["RedemptionType"].ToString();
                    dgvRedemption.Rows[a].Cells[1].Value = Convert.ToDateTime(dt["TransactionDate"].ToString()).ToLocalTime().ToShortDateString();
                    dgvRedemption.Rows[a].Cells[2].Value = float.Parse(dt["PointsAccumulated"].ToString()) * -1;
                    dgvRedemption.Rows[a].Cells[3].Value = dt["ItemName"].ToString();

                    //dgvAccumulation.Rows[o].Cells[3].Value = dt["PromoCode"].ToString();
                }
            }
        }

        private void btnLoyalty_Click(object sender, EventArgs e)
        {
            LoyaltyMemberDetail MemberDetailsFrm;
            if (btnLoyalty.Text == "View member details")
            {
                MemberDetailsFrm = new LoyaltyMemberDetail("Loyalty Members",LoyaltyID, 1, CanEdit);
            }
            else
            {
                MemberDetailsFrm = new LoyaltyMemberDetail("Loyalty Members",ID, 2, CanAdd);
            }

            if (MemberDetailsFrm.ShowDialog() == DialogResult.OK)
            {
                LoadLoyaltyMemberInfo();
            }
        }

        private void grpContact_Enter(object sender, EventArgs e)
        {

        }

        private void dgContractPricing_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvrow = dgContractPricing.CurrentRow;
            if (e.RowIndex < 0)
                return;

            switch (e.ColumnIndex)
            {
                case 3: //PartNumber
                    if (txtName.Text != "")
                    {
                        ShowItemLookup("", "PartNumber");
                        dgContractPricing.CurrentRow.Cells[5].Selected = true;
                    }
                    break;
                //case 4:
                //    this.dgEnterSales.CurrentCell = this.dgEnterSales.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //    this.dgEnterSales.BeginEdit(true);
                //    break;
                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }
        public void ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum, "", whereCon);

            DataGridViewRow dgvRows = dgContractPricing.CurrentRow;
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                dgvRows.Cells["ItemID"].Value = ItemRows.Cells[0].Value.ToString();
                dgvRows.Cells["PartNumber"].Value = ItemRows.Cells[1].Value;
                dgvRows.Cells["ItemsName"].Value = ItemRows.Cells[3].Value.ToString();
                dgvRows.Cells["SellingPrice"].Value = ItemRows.Cells[8].Value.ToString();
                dgContractPricing.CurrentRow.Cells[5].Selected = true;

            }
        }

        private void dgContractPricing_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 //Contract Price
                 || e.ColumnIndex == 6 //Selling Price
                 && e.RowIndex != this.dgContractPricing.NewRowIndex)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }else
                {
                    e.Value = 0;
                }
            }
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell oneCell in dgContractPricing.SelectedCells)
            {
                if (oneCell.RowIndex >= 0 && oneCell.RowIndex < (dgContractPricing.Rows.Count - 1))
                    dgContractPricing.Rows.RemoveAt(oneCell.RowIndex);
            }
        }

        private void chkNeverExpire_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNeverExpire.Checked)
            {
                expiryDate.Enabled = false;
            }
            else
            {
                expiryDate.Enabled = true;
            }
        }


        private void btnJobSearch_Click(object sender, EventArgs e)
        {
            LoadJobs(ID, jobStartDate.Value.ToUniversalTime(), jobEndDate.Value.ToUniversalTime());
        }

        private void btnPromoReport_Click(object sender, EventArgs e)
        {
            string sql = @"Select ap.TransactionDate, PointsAccumulated, SalesNumber, GrandTotal
                            FROM AccumulatedPoints ap 
                            INNER JOIN SalesLines sl ON sl.SalesLineID = ap.SalesLineID  
                            INNER JOIN Sales s ON s.SalesID = sl.SalesID WHERE ap.CustomerID = @CustomerID And ap.TransactionDate Between @StartDate and @EndDate";

            string Forwardedsql = @"Select PointsAccumulated as ForwardBalance , ap.TransactionDate    
                           FROM AccumulatedPoints ap 
                           INNER JOIN SalesLines sl ON sl.SalesLineID = ap.SalesLineID  
                           INNER JOIN Sales s ON s.SalesID = sl.SalesID WHERE ap.CustomerID = @CustomerID And ap.TransactionDate < @StartDate ORDER BY TransactionDate ASC";

            DataTable TbRep = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            DateTime psdate = dtpStartDate.Value.ToUniversalTime();
            DateTime pedate = dtpEndDate.Value.ToUniversalTime();
            DateTime forwarddate = psdate;

            psdate = new DateTime(psdate.Year, psdate.Month, psdate.Day, 00, 00, 00);
            pedate = new DateTime(pedate.Year, pedate.Month, pedate.Day, 23, 59, 59);
            param.Add("@StartDate", psdate);
            param.Add("@EndDate", pedate);
            param.Add("@CustomerID", ID);
            DataTable ForwardDT = new DataTable();
            CommonClass.runSql(ref TbRep, sql ,param);
            CommonClass.runSql(ref ForwardDT, Forwardedsql, param);
            float x = 0;
            if (ForwardDT.Rows.Count > 0)
            {
                DataRow dr = ForwardDT.Rows[0];
                forwarddate = Convert.ToDateTime(dr["TransactionDate"].ToString());

                foreach(DataRow dxr in ForwardDT.Rows)
                {
                    x += float.Parse(dr["ForwardBalance"].ToString());
                }
            }

            Reports.ReportParams PromoStatement = new Reports.ReportParams();
            PromoStatement.PrtOpt = 1;
            PromoStatement.Rec.Add(TbRep);
            //PromoStatement.Rec.Add(ForwardDT);

            PromoStatement.ReportName = "PromoStatement.rpt";
            PromoStatement.RptTitle = "Promo Statement";

            PromoStatement.Params = "compname|FirstPointDate|startDate|TotalForwarded";
            PromoStatement.PVals = CommonClass.CompName.Trim() + "|" + forwarddate.ToShortDateString() + "|" + psdate.ToShortDateString() + "|" + x.ToString("0.00");

            CommonClass.ShowReport(PromoStatement);
        }

        private void btnPrintJob_Click(object sender, EventArgs e)
        {
            string profiletype = "Customer";

            Reports.ReportParams profileparams = new Reports.ReportParams();
            profileparams.PrtOpt = 1;
            profileparams.Rec.Add(dtJobs);
            profileparams.ReportName = "CustomerJobsList.rpt";
            profileparams.RptTitle = "Jobs List";
            profileparams.Params = "compname|StartDate|EndDate";
            profileparams.PVals = CommonClass.CompName.Trim() + "|" + dtpFrom.Value.ToShortDateString() + "|" + dtpTo.Value.ToShortDateString();

            CommonClass.ShowReport(profileparams);
        }

        private void btnPrintProfitLoss_Click(object sender, EventArgs e)
        {
            string profiletype = "Customer";

            string jobsprofitlosssql = @"SELECT
                                            j.CreditAmount, 
                                            j.DebitAmount,
                                            j.AccountID,
                                            j.Memo,
                                            j.Type
                                        FROM Journal j
                                        INNER JOIN Jobs jbs ON j.JobID=jbs.JobID
                                        INNER JOIN Sales s ON s.SalesNumber=j.TransactionNumber
                                        WHERE s.CustomerID=@CustomerID";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CustomerID", ID);
            DataTable pldt = new DataTable();
            CommonClass.runSql(ref pldt, jobsprofitlosssql, param);

            Reports.ReportParams profileparams = new Reports.ReportParams();
            profileparams.PrtOpt = 1;
            profileparams.Rec.Add(pldt);
            profileparams.ReportName = "CustomerJobsProfitandLoss.rpt";
            profileparams.RptTitle = "Jobs Profit and Loss";
            profileparams.Params = "compname|StartDate|EndDate";
            profileparams.PVals = CommonClass.CompName.Trim() + "|" + dtpFrom.Value.ToShortDateString() + "|" + dtpTo.Value.ToShortDateString();

            CommonClass.ShowReport(profileparams);
        }
    }
}
