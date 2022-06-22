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

namespace AbleRetailPOS
{
    public partial class Supplier : Form
    {
        private string ID = "";
        private static string thisFormCode = "";
        private bool CanView = false;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanSave = false;
        private bool CanDelete = false;
        private int SaveMode = 0; //0 - View, 1 - Edit, 2 - New
        private DataRow SupplierRow;
        private DataTable TbRep = null;
        private DataGridViewRow selected_dgvrow = null;
        private DataTable TbContacts = null;

        public Supplier(string pID, string pFormCode, int pMode = 0, bool pEdit = false)
        {
            InitializeComponent();
            ID = pID;
            thisFormCode = pFormCode;
            CanSave = pEdit;
            SaveMode = pMode;
            InitTbContacts(ID);
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                FormRights.TryGetValue("Add", out outx);
                CanAdd = outx;
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                CanEdit = outx;
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                CanDelete = outx;
            }
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dgridTran.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dgvPayments.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            LoadTerms();
            if (SaveMode == 2)
            {
                //Create New
                txtSupplierOrCustomer.Visible = false;
                SupplierOrCompanyName.Visible = false;
                tabPaymentsMade.Size = new Size(719, 588);
                tabPaymentsMade.Location = new Point(12, 14);
            }
            else
            {
                LoadSupplier(ID);
                txtSupplierOrCustomer.Visible = true;
                SupplierOrCompanyName.Visible = true;
                txtSupplierOrCustomer.Text = txtName.Text;

            }
            btnSave.Enabled = CanSave;
        }

        private void LoadTerms()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "Select * from TermsOfPayment";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboTerms.DataSource = dt;
                cboTerms.ValueMember = "TermsOfPaymentID";
                cboTerms.DisplayMember = "Description";
                cboTerms.SelectedIndex = 0;         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void LoadSupplier(string pID)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT p.*, pmt.PaymentMethod, pmt.GLAccountCode FROM Profile p LEFT JOIN PaymentMethods pmt ON p.MethodOfPaymentID = pmt.id WHERE p.type = 'Supplier' and p.ID = " + pID;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    SupplierRow = dt.Rows[0];
                    //INFORMATION
                    lblID.Text = SupplierRow["ID"].ToString();
                    txtName.Text = SupplierRow["Name"].ToString();
                    txtSupNum.Text = SupplierRow["ProfileIDNumber"].ToString();
                    chkActive.Checked = (SupplierRow["IsInactive"].ToString() == "0" ? true : false);
                    if (SupplierRow["Designation"].ToString() == "Company")
                    {
                        rdoCompany.Checked = true;
                    }
                    else
                    {
                        rdoIndividual.Checked = true;
                    }

                    LoadAddress(ID, cbmLocation.SelectedIndex + 1);

                    //Account Information
                    //if (SupplierRow["ExpenseAccountID"].ToString() != "")
                    //{
                    //    selectSql = "SELECT AccountID, AccountNumber, AccountName from Accounts where AccountID = " + SupplierRow["ExpenseAccountID"].ToString();
                    //    cmd_ = new SqlCommand(selectSql, con_);
                    //    da = new SqlDataAdapter();
                    //    da.SelectCommand = cmd_;
                    //    dt = new DataTable();
                    //    da.Fill(dt);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    txtExpenseAccount.Text = SupplierRow["GLAccountCode"].ToString();
                    //        lblExpenseID.Text = dt.Rows[0]["AccountID"].ToString();
                    //        lblExpenseDesc.Text = dt.Rows[0]["AccountName"].ToString();
                    //    }
                    //}

                    txtTaxCode.Text = SupplierRow["TaxCode"].ToString();
                    txtFreightTaxCode.Text = SupplierRow["FreightTaxCode"].ToString();
                    chkCustTaxCode.Checked = (SupplierRow["UseProfileTaxCode"].ToString() == "1" ? true : false);

                    txtABN.Text = SupplierRow["ABN"].ToString();
                    //txtABNBranch.Text = SupplierRow["ABNBranch"].ToString();
                    //txtGSTIDNumber.Text = SupplierRow["GSTIDNumber"].ToString();
                    txtTaxIDNumber.Text = SupplierRow["TaxIDNumber"].ToString();
                    txtMethodofPayment.Text = SupplierRow["PaymentMethod"].ToString();
                    txtShippingMethod.Text = SupplierRow["ShippingMethodID"].ToString();

                    string bal = "0";
                    string discount = "0";

                    cboTerms.SelectedValue = SupplierRow["TermsOfPayment"].ToString();
                    switch (SupplierRow["TermsOfPayment"].ToString())
                    {
                        case "DM"://Day of the Month
                            bal = SupplierRow["BalanceDueDays"].ToString();// txtBalance.Value.ToString();
                            discount = SupplierRow["DiscountDays"].ToString(); //txtDiscount.Value.ToString();
                            break;
                        case "DMEOM": //Day of the Month after EOM
                            bal = SupplierRow["BalanceDueDays"].ToString(); //txtBalance.Value.ToString();
                            discount = SupplierRow["DiscountDays"].ToString(); //txtDiscount.Value.ToString();
                            break;
                        case "SD": //Specific Days
                            bal = SupplierRow["BalanceDueDays"].ToString(); //txtBalance.Value.ToString();
                            discount = SupplierRow["DiscountDays"].ToString(); //txtDiscount.Value.ToString();
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            bal = SupplierRow["BalanceDueDays"].ToString(); //txtBalance.Value.ToString();
                            discount = SupplierRow["DiscountDays"].ToString(); //txtDiscount.Value.ToString();
                            break;
                        default: //CASH
                            break;
                    }

                    txtBalance.Value = bal != "" ? Convert.ToDecimal(bal) : 0;
                    txtDiscount.Value = discount != "" ? Convert.ToDecimal(discount) : 0;
                    string strcredlimit = SupplierRow["CreditLimit"].ToString();
                    txtCreditLimit.Value = strcredlimit != "" ? Convert.ToDecimal(strcredlimit) : 0;
                    string strearlpaymdiscpercent = SupplierRow["EarlyPaymentDiscountPercent"].ToString();
                    txtEarlyPayment.Value = strearlpaymdiscpercent != "" ? Convert.ToDecimal(strearlpaymdiscpercent) : 0;
                    string strlatepaymchrgepercent = SupplierRow["LatePaymentChargePercent"].ToString();
                    txtLatePaymentCharge.Value = strlatepaymchrgepercent != "" ? Convert.ToDecimal(strlatepaymchrgepercent) : 0;
                    string strvoldisc = SupplierRow["VolumeDiscount"].ToString();
                    txtVolumeDiscount.Value = strvoldisc != "" ? Convert.ToDecimal(strvoldisc) : 0;
                    txtSupplierNotes.Text = SupplierRow["SupplierNotes"].ToString();

                    //PAYMENT TAB
                    txtPaymentBSB.Text = SupplierRow["PaymentBSB"].ToString();
                    txtPaymentAcctName.Text = SupplierRow["PaymentBankAccountName"].ToString();
                    txtPaymentAcctNo.Text = SupplierRow["PaymentBankAccountNumber"].ToString();

                    //AP BALANCE
                    decimal lCBal = (SupplierRow["CurrentBalance"].ToString() == "" ? 0 : Convert.ToDecimal(SupplierRow["CurrentBalance"]));
                    lblAPBalance.Text = Math.Round(lCBal, 2).ToString("C");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveMode == 1)
            {
                UpdateSupplier(ID);
                txtSupplierOrCustomer.Text = txtName.Text;
            }
            else if (SaveMode == 2)
            {
                NewSupplier();
            }
        }

        private void NewSupplier()
        {
            SqlConnection con = null;
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Supplier Name Required.");
                }
                else
                {
                    string sqli = @"INSERT INTO Profile (Type,ProfileIDNumber,Name,Designation,IsInactive,UseProfileTaxCode,ExpenseAccountID,FreightTaxCode,TaxCode,ABN,TaxIDNumber,
                                                         MethodOfPaymentID,ShippingMethodID,TermsOfPayment,CreditLimit,BalanceDueDays,BalanceDueDate,DiscountDays,DiscountDate,
                                                         EarlyPaymentDiscountPercent,LatePaymentChargePercent,VolumeDiscount) 
                                   VALUES (@Type, @ProfileIDNumber, @Name, @Designation, @IsInactive, @UseProfileTaxCode, @ExpenseAccountID, @FreightTaxCode, @TaxCode, @ABN, @TaxIDNumber, 
                                             (SELECT id FROM PaymentMethods WHERE PaymentMethod = '" + txtMethodofPayment.Text + @"'), @ShippingMethodID, @TermsOfPayment, @CreditLimit,
                                            @BalanceDueDays, @BalanceDueDate, @DiscountDays, @DiscountDate, @EarlyPaymentDiscountPercent, @LatePaymentChargePercent, @VolumeDiscount);  
                                   SELECT SCOPE_IDENTITY();";

                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(sqli, con);
                    cmd.CommandType = CommandType.Text;

                    //Supplier Info
                    cmd.Parameters.AddWithValue("@Type", "Supplier");
                    cmd.Parameters.AddWithValue("@ProfileIDNumber", txtSupNum.Text);
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

                    cmd.Parameters.AddWithValue("@ExpenseAccountID", lblExpenseID.Text);
                    cmd.Parameters.AddWithValue("@FreightTaxCode", txtFreightTaxCode.Text);
                    cmd.Parameters.AddWithValue("@TaxCode", txtTaxCode.Text);
                    cmd.Parameters.AddWithValue("@ABN", txtABN.Text);
                  //  cmd.Parameters.AddWithValue("@ABNBranch", txtABNBranch.Text);
                    cmd.Parameters.AddWithValue("@TaxIDNumber", txtTaxIDNumber.Text);
                    //cmd.Parameters.AddWithValue("@GSTIDNumber", txtGSTIDNumber.Text);
                    cmd.Parameters.AddWithValue("@TermsOfPayment", cboTerms.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreditLimit", txtCreditLimit.Value);

                    decimal baldays = 0;
                    decimal baldate = 0;
                    decimal discountdays = 0;
                    decimal discountdate = 0;

                    switch (cboTerms.SelectedValue.ToString())
                    {
                        case "DM"://Day of the Month                            
                            baldate = this.txtBalance.Value;
                            discountdate = this.txtDiscount.Value;
                            break;
                        case "DMEOM": //Day of the Month after EOM
                            baldate = this.txtBalance.Value;
                            discountdate = this.txtDiscount.Value;
                            break;
                        case "SD": //Specific Days
                            baldays = this.txtBalance.Value;
                            discountdays = this.txtDiscount.Value;
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            baldays = this.txtBalance.Value;
                            discountdays = this.txtDiscount.Value;
                            break;
                        default: //CASH
                            break;
                    }

                    cmd.Parameters.AddWithValue("@BalanceDueDays", baldays);
                    cmd.Parameters.AddWithValue("@BalanceDueDate", baldate);
                    cmd.Parameters.AddWithValue("@DiscountDays", discountdays);
                    cmd.Parameters.AddWithValue("@DiscountDate", discountdate);
                    cmd.Parameters.AddWithValue("@EarlyPaymentDiscountPercent", txtEarlyPayment.Value);
                    cmd.Parameters.AddWithValue("@LatePaymentChargePercent", txtLatePaymentCharge.Value);
                    cmd.Parameters.AddWithValue("@VolumeDiscount", txtVolumeDiscount.Text);
                    cmd.Parameters.AddWithValue("@ShippingMethodID", this.txtShippingMethod.Text);
                    cmd.Parameters.AddWithValue("@SupplierNotes", txtSupplierNotes.Text);

                    //PAYMENT TAB
                    cmd.Parameters.AddWithValue("@PaymentBSB", txtPaymentBSB.Text);
                    cmd.Parameters.AddWithValue("@PaymentBankAccountName", txtPaymentAcctName.Text);
                    cmd.Parameters.AddWithValue("@PaymentBankAccountNumber", txtPaymentAcctNo.Text);

                    con.Open();

                    int profileid = Convert.ToInt32(cmd.ExecuteScalar());
                    if (profileid > 0)
                    {
                        UpdateContactTb(cbmLocation.SelectedIndex + 1, "TypeOfContact", this.cbmLocation.Text);

                        foreach (DataRow rw in TbContacts.Rows)
                        {
                            string sql2 = "INSERT INTO Contacts(Location, Street, City, State, PostCode, Country, Phone , Fax, Email, Website, ContactPerson, ProfileID, Comments, TypeOfContact)" +
                                      " VALUES (@Location, @Street, @City, @State, @Postcode, @Country, @Phone, @Fax, @Email, @Website, @ContactPerson," + profileid + ", @Comments, @TypeOfContact)";
                            cmd = new SqlCommand(sql2, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Location", rw["Location"].ToString());
                            cmd.Parameters.AddWithValue("@Street", rw["Street"].ToString());
                            cmd.Parameters.AddWithValue("@City", rw["City"].ToString());
                            cmd.Parameters.AddWithValue("@State", rw["State"].ToString());
                            cmd.Parameters.AddWithValue("@Postcode", rw["Postcode"].ToString());
                            cmd.Parameters.AddWithValue("@Country", rw["Country"].ToString());
                            cmd.Parameters.AddWithValue("@Phone", rw["Phone"].ToString());
                            cmd.Parameters.AddWithValue("@Fax", rw["Fax"].ToString());
                            cmd.Parameters.AddWithValue("@Email", rw["Email"].ToString());
                            cmd.Parameters.AddWithValue("@ContactPerson", rw["ContactPerson"].ToString());
                            cmd.Parameters.AddWithValue("@Website", rw["Website"].ToString());
                            cmd.Parameters.AddWithValue("@Comments", rw["Comments"].ToString());
                            cmd.Parameters.AddWithValue("@TypeOfContact", rw["TypeOfContact"].ToString());
                            int j = cmd.ExecuteNonQuery();
                        }
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Supplier " + txtName.Text);
                        string titles = "Information";
                        MessageBox.Show("Supplier Record has been created.", titles);
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

        private void UpdateSupplier(string pID)
        {
            SqlConnection con = null;
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Supplier Name Required.");
                }
                else
                {
                    string sqli = "UPDATE Profile set " +
                        "Type = @Type," +
                        "ProfileIDNumber = @ProfileIDNumber," +
                        "name = @name," +
                        "Designation = @Designation," +
                        "IsInactive = @IsInactive," +
                        " UseProfileTaxCode = @UseProfileTaxCode," +
                        "ExpenseAccountID = @ExpenseAccountID," +
                        "FreightTaxCode = @FreightTaxCode," +
                        "TaxCode = @TaxCode," +
                        "ABN = @ABN," +
                        "TaxIDNumber = @TaxIDNumber, " +
                        " MethodOfPaymentID = (SELECT id FROM PaymentMethods WHERE PaymentMethod = '" + txtMethodofPayment.Text + @"'), 
                        ShippingMethodID = @ShippingMethodID," +
                        " TermsOfPayment = @TermsOfPayment," +
                        "CreditLimit = @CreditLimit," +
                        "BalanceDueDays = @BalanceDueDays," +
                        "BalanceDueDate = @BalanceDueDate," +
                        "DiscountDays = @DiscountDays," +
                        "DiscountDate = @DiscountDate," +
                        "EarlyPaymentDiscountPercent = @EarlyPaymentDiscountPercent," +
                        "LatePaymentChargePercent = @LatePaymentChargePercent," +
                        "VolumeDiscount = @VolumeDiscount," +
                        "SupplierNotes = @SupplierNotes," +
                        "PaymentBSB = @PaymentBSB," +
                        "PaymentBankAccountName = @PaymentBankAccountName," +
                        "PaymentBankAccountNumber = @PaymentBankAccountNumber where id = " + pID;

                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(sqli, con);
                    cmd.CommandType = CommandType.Text;

                    //Supplier Info
                    cmd.Parameters.AddWithValue("@Type", "Supplier");
                    cmd.Parameters.AddWithValue("@ProfileIDNumber", this.txtSupNum.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    if (this.rdoCompany.Checked == true) { cmd.Parameters.AddWithValue("@Designation", "Company"); } else { cmd.Parameters.AddWithValue("@Designation", "Individual"); }
                    if (chkActive.Checked == true) { cmd.Parameters.AddWithValue("@IsInactive", "0"); } else { cmd.Parameters.AddWithValue("@IsInactive", "1"); }

                    //common field
                    if (chkCustTaxCode.Checked == true) { cmd.Parameters.AddWithValue("@UseProfileTaxCode", "1"); } else { cmd.Parameters.AddWithValue("@UseProfileTaxCode", "0"); }
                    cmd.Parameters.AddWithValue("@ExpenseAccountID", lblExpenseID.Text);

                    cmd.Parameters.AddWithValue("@FreightTaxCode", txtFreightTaxCode.Text);
                    cmd.Parameters.AddWithValue("@TaxCode", txtTaxCode.Text);
                    cmd.Parameters.AddWithValue("@ABN", txtABN.Text);
                   // cmd.Parameters.AddWithValue("@ABNBranch", txtABNBranch.Text);
                    cmd.Parameters.AddWithValue("@TaxIDNumber", txtTaxIDNumber.Text);
                   // cmd.Parameters.AddWithValue("@GSTIDNumber", txtGSTIDNumber.Text);

                    cmd.Parameters.AddWithValue("@TermsOfPayment", this.cboTerms.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CreditLimit", txtCreditLimit.Value.ToString());

                    string baldays = "0";
                    string baldate = "0";
                    string discountdays = "0";
                    string discountdate = "0";

                    switch (this.cboTerms.SelectedValue.ToString())
                    {
                        case "DM"://Day of the Month                            
                            baldate = this.txtBalance.Value.ToString();
                            discountdate = this.txtDiscount.Value.ToString();
                            break;
                        case "DMEOM": //Day of the Month after EOM
                            baldate = this.txtBalance.Value.ToString();
                            discountdate = this.txtDiscount.Value.ToString();
                            break;
                        case "SD": //Specific Days
                            baldays = this.txtBalance.Value.ToString();
                            discountdays = this.txtDiscount.Value.ToString();
                            break;
                        case "SDEOM"://Specifc Day after EOM
                            baldays = this.txtBalance.Value.ToString();
                            discountdays = this.txtDiscount.Value.ToString();
                            break;
                        default: //CASH
                            break;
                    }

                    cmd.Parameters.AddWithValue("@BalanceDueDays", baldays);
                    cmd.Parameters.AddWithValue("@BalanceDueDate", baldate);
                    cmd.Parameters.AddWithValue("@DiscountDays", discountdays);
                    cmd.Parameters.AddWithValue("@DiscountDate", discountdate);
                    cmd.Parameters.AddWithValue("@EarlyPaymentDiscountPercent", txtEarlyPayment.Value.ToString());
                    cmd.Parameters.AddWithValue("@LatePaymentChargePercent", txtLatePaymentCharge.Value.ToString());
                    cmd.Parameters.AddWithValue("@VolumeDiscount", txtVolumeDiscount.Text);
                    cmd.Parameters.AddWithValue("@ShippingMethodID", txtShippingMethod.Text);
                    cmd.Parameters.AddWithValue("@SupplierNotes", txtSupplierNotes.Text);
                    //PAYMENT TAB
                    cmd.Parameters.AddWithValue("@PaymentBSB", txtPaymentBSB.Text);
                    cmd.Parameters.AddWithValue("@PaymentBankAccountName", txtPaymentAcctName.Text);
                    cmd.Parameters.AddWithValue("@PaymentBankAccountNumber", txtPaymentAcctNo.Text);

                    con.Open();
                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected > 0)
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
                                   WHERE ProfileID = " + pID + " AND Location = " + (cbmLocation.SelectedIndex + 1);

                        cmd = new SqlCommand(sql2, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@State", txtState.Text);
                        cmd.Parameters.AddWithValue("@Postcode", txtPostcode.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Fax", txtFax.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactName.Text);
                        cmd.Parameters.AddWithValue("@Website", txtWWW.Text);
                        cmd.Parameters.AddWithValue("@Comments", txtProfileNotes.Text);
                        cmd.Parameters.AddWithValue("@TypeOfContact", cbmLocation.Text);
                        ;

                        int j = cmd.ExecuteNonQuery();

                        if (j > 0)
                        {
                            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Edited Supplier ID " + lblID.Text, lblID.Text);
                            string titles = "Information";
                            MessageBox.Show("Supplier Record has been updated.", titles);
                            DialogResult = DialogResult.OK;
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

        private void cboTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isFieldEnabled = false;
            switch (this.cboTerms.SelectedValue.ToString())
            {

                case "DM"://Day of the Month
                    this.lblBalance.Text = "Balance Due Date";
                    this.lblDiscount.Text = "Discount Due Date";
                    this.lblBalanceNote.Text = "Specify Date of the Month (1-31)";
                    this.lblDiscountNote.Text = "Specify Date of the Month (1-31)";
                    isFieldEnabled = true;

                    break;
                case "DMEOM": //Day of the Month after EOM
                    this.lblBalance.Text = "Balance Due Date";
                    this.lblDiscount.Text = "Discount Date";
                    this.lblBalanceNote.Text = "Specify Date of the Month (1-31)";
                    this.lblDiscountNote.Text = "Specify Date of the Month (1-31)";
                    isFieldEnabled = true;
                    break;
                case "SD": //Specific Days
                    this.lblBalance.Text = "Balance Due Days";
                    this.lblDiscount.Text = "Discount Days";
                    this.lblBalanceNote.Text = "Specify # of Days";
                    this.lblDiscountNote.Text = "Specify # of Days";
                    isFieldEnabled = true;
                    break;
                case "SDEOM"://Specifc Day after EOM
                    this.lblBalance.Text = "Balance Due Days";
                    this.lblDiscount.Text = "Discount Days";
                    this.lblBalanceNote.Text = "Specify # of Days";
                    this.lblDiscountNote.Text = "Specify # of Days";
                    isFieldEnabled = true;
                    break;
                default: //CASH

                    this.txtBalance.Value = 0;
                    this.txtCreditLimit.Value = 0;
                    this.txtDiscount.Value = 0;

                    this.txtEarlyPayment.Value = 0;

                    this.txtLatePaymentCharge.Value = 0;
                    this.lblBalanceNote.Text = "     ";
                    this.lblDiscountNote.Text = "     ";
                    isFieldEnabled = false;
                    break;
            }
            this.txtBalance.Enabled = isFieldEnabled;
            this.txtCreditLimit.Enabled = isFieldEnabled;
            this.txtDiscount.Enabled = isFieldEnabled;
            this.txtEarlyPayment.Enabled = isFieldEnabled;
            this.txtLatePaymentCharge.Enabled = isFieldEnabled;
        }

        private void pbTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                this.txtTaxCode.Text = Tax[0];
            }
        }

        private void pbFTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                this.txtFreightTaxCode.Text = Tax[0];
            }
        }

        private void pbPayment_Click(object sender, EventArgs e)
        {
            PaymentMethodLookup DlgPaymentMethod = new PaymentMethodLookup();
            if (DlgPaymentMethod.ShowDialog() == DialogResult.OK)
            {
                string[] lPMethod = DlgPaymentMethod.GetPaymentMethod;
                this.txtMethodofPayment.Text = lPMethod[0];
                this.txtExpenseAccount.Text = lPMethod[2];
            }
        }

        private void pbShipping_Click(object sender, EventArgs e)
        {
            ShippingMethodLookup DlgShippingMethod = new ShippingMethodLookup();
            if (DlgShippingMethod.ShowDialog() == DialogResult.OK)
            {
                string[] ShipList = DlgShippingMethod.GetShippingMethod;
                this.txtShippingMethod.Text = ShipList[0];
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
            SqlConnection con = null;
            try
            {

                string sql = "";
                if (cbPurchaseType.Text == "QUOTE")
                {

                    sql = @"SELECT PurchaseNumber AS TransactionNumber,
                                s.TransactionDate, 
                               s.Memo,
                                GrandTotal AS Amount, 
                               j.Type,
                                PurchaseID AS ID,
								Name,
								ProfileIDNumber
                       FROM Purchases s INNER JOIN Profile p ON s.SupplierID = p.ID
                       INNER JOIN Journal j ON j.TransactionNumber = s.PurchaseNumber
                       WHERE SupplierID = '" + pProfileID + "'" +
                       " AND s.TransactionDate BETWEEN @sdate AND @edate AND PurchaseID = j.EntityID AND j.Type = 'PQ'";
                }
                else if (cbPurchaseType.Text == "ORDER")
                {
                    sql = @"SELECT PurchaseNumber AS TransactionNumber,
                                s.TransactionDate, 
                               s.Memo,
                                GrandTotal AS Amount, 
                               j.Type,
                                PurchaseID AS ID,
								Name,
								ProfileIDNumber
                       FROM Purchases s INNER JOIN Profile p ON s.SupplierID = p.ID
                       INNER JOIN Journal j ON j.TransactionNumber = s.PurchaseNumber
                       WHERE SupplierID = '" + pProfileID + "'" +
                       " AND s.TransactionDate BETWEEN @sdate AND @edate AND PurchaseID = j.EntityID AND j.Type = 'PO' ";
                }
                else if (cbPurchaseType.Text == "BILL")
                {
                    sql = @"SELECT PurchaseNumber AS TransactionNumber,
                                s.TransactionDate, 
                               s.Memo,
                                GrandTotal AS Amount, 
                               j.Type,
                                PurchaseID AS ID,
								Name,
								ProfileIDNumber
                       FROM Purchases s INNER JOIN Profile p ON s.SupplierID = p.ID
                       INNER JOIN Journal j ON j.TransactionNumber = s.PurchaseNumber
                       WHERE SupplierID = '" + pProfileID + "'" +
                       " AND s.TransactionDate BETWEEN @sdate AND @edate AND PurchaseID = j.EntityID AND j.Type = 'PB'";
                }
                else
                {
                    sql = @"SELECT MoneyInNumber AS TransactionNumber, 
                                      TransactionDate, 
                                      Memo,
                                      TotalAmountReceived AS Amount, 
                                      'MI' AS Type,
                                      MoneyInID AS ID,
                              		  Name,
								      ProfileIDNumber
						    FROM MoneyIn m
						    INNER JOIN Profile p ON p.ID = m.MoneyInID WHERE ProfileID = '" + pProfileID + "'" +
                              " AND TransactionDate BETWEEN @sdate AND @edate ";

                    sql += @"UNION SELECT MoneyOutNumber AS TransactionNumber, 
                                TransactionDate, 
                                Memo,
                                TotalSpentAmount AS Amount, 
                                'MO' AS Type,
                                MoneyOutID AS ID,
								Name,
								ProfileIDNumber
                       FROM MoneyOut o
					   INNER JOIN Profile p ON p.ID = o.MoneyOutID WHERE ProfileID = '" + pProfileID + "'" +
                           " AND TransactionDate BETWEEN @sdate AND @edate ";

                    //Purchase
                    sql += @"UNION SELECT PurchaseNumber AS TransactionNumber, 
                                TransactionDate, 
                                Memo,
                                GrandTotal AS Amount, 
                                'PB' AS Type,
                                PurchaseID AS ID,
								Name,
								ProfileIDNumber
                       FROM Purchases p
					   INNER JOIN Profile pro ON pro.ID = p.SupplierID
                       WHERE SupplierID = '" + pProfileID + "'" +
                           " AND TransactionDate BETWEEN @sdate AND @edate";
                }

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@sdate", pDateFrom);
                cmd.Parameters.AddWithValue("@edate", pDateTo);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);

                this.dgridTran.DataSource = TbRep;
                for (int i = 0; i < this.dgridTran.Rows.Count; i++)
                {
                    if (this.dgridTran.Rows[i].Cells["TransactionDate"].Value != null)
                    {
                        if (this.dgridTran.Rows[i].Cells["TransactionDate"].Value.ToString() != "")
                        {
                            this.dgridTran.Rows[i].Cells["TransactionDate"].Value= Convert.ToDateTime(this.dgridTran.Rows[i].Cells["TransactionDate"].Value.ToString()).ToShortDateString();
                        }
                    }
                }
                this.dgridTran.Columns[0].HeaderText = "Transaction No";
                this.dgridTran.Columns[1].HeaderText = "Date";
                this.dgridTran.Columns[2].HeaderText = "Memo";
                this.dgridTran.Columns[3].HeaderText = "Amount";
                this.dgridTran.Columns[4].HeaderText = "Type";
                this.dgridTran.Columns[3].DefaultCellStyle.Format = "C2";

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

        private void LoadReport()
        {
            string profiletype = "Supplier";
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadAddress(string pID, int pIndex)
        {
            DataRow[] dr = TbContacts.Select("Location = " + pIndex);
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
                cbmLocation.Text = rw["TypeOfContact"].ToString();
            }
        }

        private void cbmLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "TypeOfContact", this.cbmLocation.Text);
            LoadAddress(ID, cbmLocation.SelectedIndex + 1);
        }
        private void InitTbContacts(string pID)
        {

            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string strcon = " where ProfileID = " + (pID == "" ? "0" : pID);
                string selectSql = @"SELECT Location, Street, City, State, PostCode, Country, 
                                    Phone , Fax, Email, Website, ContactPerson, ProfileID, Comments, TypeOfContact 
                                    From Contacts " + strcon;
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                TbContacts = new DataTable();
                da.Fill(TbContacts);
                if (TbContacts.Rows.Count == 0)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        DataRow nr = TbContacts.NewRow();
                        nr["Location"] = i.ToString();
                        TbContacts.Rows.Add(nr);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
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
        private void txtContactName_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "ContactPerson", this.txtContactName.Text);
        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "Street", this.txtStreet.Text);
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "City", this.txtCity.Text);
        }
        private void txtState_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "State", this.txtState.Text);
        }

        private void txtPostcode_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "PostCode", this.txtPostcode.Text);
        }

        private void txtCountry_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "Country", this.txtCountry.Text);
        }

        private void txtProfileNotes_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "Comments", this.txtProfileNotes.Text);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "Email", this.txtEmail.Text);
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "Phone", this.txtPhone.Text);
        }

        private void txtFax_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "Fax", this.txtFax.Text);
        }

        private void txtWWW_TextChanged(object sender, EventArgs e)
        {
            UpdateContactTb(cbmLocation.SelectedIndex + 1, "Website", this.txtWWW.Text);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            DateTime dtpfromutc = this.paymentDateFrom.Value.ToUniversalTime();
            DateTime dtptoutc = this.paymentDateTo.Value.ToUniversalTime();

            string sdate = dtpfromutc.ToString("yyyy-MM-dd") + " 00:00:00";
            string edate = dtptoutc.ToString("yyyy-MM-dd") + " 23:59:59";
            LoadPayments("", "", cbInvoiceTo.Text, cbInvoiceTo.Text);
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = CanSave;
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = CanSave;
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = CanSave;
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
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
                if (CommonClass.PRPaymentsfrm != null
                && !CommonClass.PRPaymentsfrm.IsDisposed)
                {
                    CommonClass.PRPaymentsfrm.Close();
                }
                CommonClass.PRPaymentsfrm = new PurchasePayments(CommonClass.InvocationSource.SUPPLIER,
                                                                selected_dgvrow.Cells["PaymentID"].Value.ToString());
                this.Cursor = Cursors.WaitCursor;
                CommonClass.PRPaymentsfrm.MdiParent = this.MdiParent;
                CommonClass.PRPaymentsfrm.Show();
                CommonClass.PRPaymentsfrm.Focus();
                if (CommonClass.PRPaymentsfrm.DialogResult == DialogResult.Cancel)
                {
                    CommonClass.PRPaymentsfrm.Close();
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
                case "PB":
                    if (CommonClass.EnterPurchasefrm == null || CommonClass.EnterPurchasefrm.IsDisposed)
                    {
                        CommonClass.EnterPurchasefrm = new Purchase.EnterPurchase(CommonClass.InvocationSource.SELF, transactionid.ToString());
                    }
                    this.Cursor = Cursors.WaitCursor;
                    CommonClass.EnterPurchasefrm.MdiParent = this.MdiParent;
                    CommonClass.EnterPurchasefrm.Show();
                    CommonClass.EnterPurchasefrm.Focus();
                    if (CommonClass.EnterPurchasefrm.DialogResult == DialogResult.Cancel
                        || CommonClass.EnterPurchasefrm.DialogResult == DialogResult.OK)
                    {
                        CommonClass.EnterPurchasefrm.Close();
                    }

                    this.Cursor = Cursors.Default;
                    break;
            }
        }

        private void dgvPayments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3
                && e.RowIndex != this.dgvPayments.NewRowIndex)
            {
                if (e.Value != null)
                {
                    decimal d = decimal.Parse(e.Value.ToString(), NumberStyles.Any);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void btnSearchPayment_Click(object sender, EventArgs e)
        {
            DateTime dtpfromutc = this.paymentDateFrom.Value.ToUniversalTime();
            DateTime dtptoutc = this.paymentDateTo.Value.ToUniversalTime();

            string sdate = dtpfromutc.ToString("yyyy-MM-dd") + " 00:00:00";
            string edate = dtptoutc.ToString("yyyy-MM-dd") + " 23:59:59";
            LoadPayments(sdate, edate,cbInvoiceFrom.Text, cbInvoiceTo.Text);
        }
        void LoadPayments(string pDateFrom, string pDateTo, string InFrom, string InTo)
        {
            SqlConnection con = null;
            try
            {
                if (ID != "")
                {
                    string paymentsql = @"SELECT p.PaymentID,s.PurchaseNumber, PaymentNumber, p.TransactionDate, TotalAmount, PaymentFor, p.Memo 
                                            FROM Payment p 
                                            INNER JOIN PaymentLines pl ON pl.PaymentID= p.PaymentID 
                                            INNER JOIN Purchases s ON s.PurchaseID = pl.EntityID 
                                            WHERE ProfileID = " + ID + " ";
                    if (pDateFrom != "" || pDateTo != "")
                    {
                        paymentsql += " AND p.TransactionDate BETWEEN @sdate and @edate";
                    }
                    if (cbInvoiceFrom.Text != "ALL" || cbInvoiceTo.Text != "ALL")
                    {
                        paymentsql += " AND s.PurchaseNumber BETWEEN @INfrom and @INto ";
                    }
                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(paymentsql, con);
                    cmd.Parameters.AddWithValue("@sdate", pDateFrom);
                    cmd.Parameters.AddWithValue("@edate", pDateTo);
                    cmd.Parameters.AddWithValue("@INfrom", InFrom);
                    cmd.Parameters.AddWithValue("@INto", InTo);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dgvPayments.DataSource = dt;
                        dgvPayments.Columns[0].Visible = false;
                        dgvPayments.Columns[1].Visible = false;
                        cbInvoiceTo.Items.Clear();
                        cbInvoiceFrom.Items.Clear();
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            DataRow dr = dt.Rows[x];
                            cbInvoiceFrom.Items.Add(dr["PurchaseNumber"].ToString());
                            cbInvoiceTo.Items.Add(dr["PurchaseNumber"].ToString());
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
    }
}
