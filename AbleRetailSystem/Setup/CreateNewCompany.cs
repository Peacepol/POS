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
using Microsoft.SqlServer;
using Newtonsoft.Json;
using System.IO;
using System.Net.Mail;

namespace RestaurantPOS
{
    public partial class CreateNewCompany : Form
    {
        private Dictionary<string, string> selectedaccountlist = new Dictionary<string, string>();

        private string newcompanyconnstr;
        private SortedDictionary<string, string> industryclassification;
        private SortedDictionary<string, SortedList<string, string>> typeofbusiness;
        private List<Account> predefacct;

        public CreateNewCompany()
        {
            InitializeComponent();
        }

        private void CreateNewCompany_Load(object sender, EventArgs e)
        {
        }

        private void btnCompanyInfo_Click(object sender, EventArgs e)
        {
            tabGrouping.SelectedIndex = 1;
        }

        public bool IsEmailValid(string txtEmail)
        {
            try
            {
                MailAddress m = new MailAddress(txtEmail);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            tabGrouping.SelectedIndex = 3;
            
            lblCompanyName.Text = txtCompanyName.Text;
            lblABN.Text = txtSerialNo.Text;
            lblAddress.Text = txtAdd1.Text;
            lblPhoneNo.Text = txtPhoneNo.Text;
            lblFaxNo.Text = txtFaxNo.Text;
            lblEmail.Text = txtEmail.Text;

            lblRegNo.Text = txtRegistrationNo.Text;
            lblSerialNo.Text = txtSerialNo.Text;

            if (txtCompanyName.Text == ""
                || txtAdd1.Text == ""
                || txtPhoneNo.Text == ""
                || txtEmail.Text == ""
                || txtSerialNo.Text == ""
                || txtRegistrationNo.Text == "")
            {
                btnProceed.Enabled = false;
                label21.Text = "Check required field(s).";
                label21.ForeColor = Color.Red;
            }
            else if(IsEmailValid(txtEmail.Text) == false)
            {
                btnProceed.Enabled = false;
                label21.Text = "Invalid email format.";
                label21.ForeColor = Color.Red;
            }
            else
            {
                btnProceed.Enabled = true;
                label21.ForeColor = Color.LightGray;
                label21.Text = "If this information is correct, please click the Proceed button. Click Update to modify it.";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCompanyDatabase_Click(object sender, EventArgs e)
        {
            tabGrouping.SelectedIndex = 2;
            txtServerName.Focus();
            if (txtDatabaseName.Text == "")
            {
                int i = (txtCompanyName.Text + " ").IndexOf(" ");
                string defdbname = (txtRegistrationNo.Text + txtCompanyName.Text.Substring(0, i)).Trim();
                txtDatabaseName.Text = defdbname;
            }
            if(txtServerName.Text == "")
            {
                string strComputerName = Environment.MachineName.ToString();
                txtServerName.Text = strComputerName + "\\SQLEXPRESS";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnCompanyInfo_Click(sender, e);
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            string currapppath = AppDomain.CurrentDomain.BaseDirectory;
            Cursor.Current = Cursors.WaitCursor;

            if (createCompanyFile(currapppath + "\\templatedata\\able_template_sql.sql"/*, txtDatabaseName.Text*/))
            {
                newcompanyconnstr = newcompanyconnstr.Replace("Initial Catalog = master", "Initial Catalog = " + txtDatabaseName.Text);
                //newcompanyconnstr = newcompanyconnstr.Replace("Integrated Security = true", "Network Library = DBMSSOCN; User ID = ablecctg; Password = 5!37e5CCt9");
                SqlConnection con = null;
                try
                {
                    con = new SqlConnection(newcompanyconnstr);

                    string[] sqlbatches =
                    {
                        "DELETE FROM DataFileInformation"
                    };

                    con.Open();

                    foreach (string batch in sqlbatches)
                    {
                        SqlCommand schemafixcmd = new SqlCommand(batch, con);
                        int rowaffected = schemafixcmd.ExecuteNonQuery();
                        if (rowaffected > 0)
                        {
                            Console.WriteLine("Successfully executed sql statement: " + batch);
                        }
                    }

                    string writefileinfosql = @"INSERT INTO DataFileInformation (
                                                    CompanyName, 
                                                    ABN,
                                                    CompanyRegistrationNumber,
                                                    SerialNumber, 
                                                    Address, 
                                                    Phone, 
                                                    FaxNumber, 
                                                    Email,
                                                    Add1,
                                                    Add2,
                                                    Street,
                                                    City,
                                                    State,                                                    
                                                    Country,
                                                    POBox,
                                                    ContactPerson)
                                                VALUES ( @CompanyName,
                                                         @ABN,
                                                         @CompanyRegistrationNumber,
                                                         @SerialNumber,
                                                         @Address, 
                                                         @Phone, 
                                                         @FaxNumber, 
                                                         @Email,
                                                         @Add1,
                                                         @Add2,
                                                         @Street,
                                                         @City,
                                                         @State,                                                        
                                                         @Country,
                                                         @POBox,
                                                         @ContactPerson )";

                    SqlCommand cmd = new SqlCommand(writefileinfosql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
                    cmd.Parameters.AddWithValue("@ABN", txtABN.Text);
                    cmd.Parameters.AddWithValue("@CompanyRegistrationNumber", txtRegistrationNo.Text);
                    cmd.Parameters.AddWithValue("@SerialNumber", txtSerialNo.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAdd1.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@FaxNumber", txtFaxNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Add1", txtAdd1.Text);
                    cmd.Parameters.AddWithValue("@Add2", txtAdd2.Text);
                    cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@State", txtState.Text);
                    cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                    cmd.Parameters.AddWithValue("@POBox", txtPOBox.Text);
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected > 0)
                    {
                        Cursor.Current = Cursors.Default;
                        string titles = "Congratulations!";
                        DialogResult result = MessageBox.Show("Company file has been created for " + txtCompanyName.Text.ToString() + " company.", titles);
                        if (result == DialogResult.OK)
                        {
                            string windowsauthuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                            string[] usersecurity = 
                            {
                                "USE master",
                                "DENY VIEW ANY DATABASE TO PUBLIC"/*,
                                "ALTER SERVER ROLE sysadmin DROP MEMBER [" + windowsauthuser + "]"*/
                            };
                            foreach(string securityleaf in usersecurity)
                            {
                                SqlCommand securitycmd = new SqlCommand(securityleaf, con);
                                securitycmd.ExecuteNonQuery();
                            }
                            //code to update the config.json here
                            string sourcePath = @Application.StartupPath;

                            System.IO.StreamReader file = null;
                            System.IO.StreamWriter writefile = null;
                            try
                            {
                                file = new System.IO.StreamReader(sourcePath + "\\config.json");
                                string configjson = file.ReadToEnd();
                                configjson = configjson.Replace("\t", "");

                                file.Close();
                                List<DeserializeTypes> compinfo = JsonConvert.DeserializeObject<List<DeserializeTypes>>(configjson);

                                DeserializeTypes newcomp = new DeserializeTypes();
                                newcomp.company_name = CommonClass.Encrypt(txtCompanyName.Text);
                                newcomp.serial_number = CommonClass.Encrypt(txtSerialNo.Text);
                                newcomp.registration_number = CommonClass.Encrypt(txtRegistrationNo.Text);
                                newcomp.database_name = txtDatabaseName.Text;
                                newcomp.server_name = txtServerName.Text;
                                newcomp.db_user = CommonClass.Encrypt("ableacctg");
                                newcomp.db_pass = CommonClass.Encrypt("5!37e5CCt9");

                                compinfo.Add(newcomp);

                                string encryptedjson = JsonConvert.SerializeObject(compinfo, Formatting.Indented);
                                writefile = new System.IO.StreamWriter(sourcePath + "\\config.json");
                                writefile.Write(encryptedjson);

                                this.DialogResult = DialogResult.OK;
                                Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                if (file != null)
                                    file.Close();

                                if (writefile != null)
                                    writefile.Close();
                            }
                        }                           
                    }                   
                }
                catch (SqlException exception)
                {
                    Console.WriteLine(exception.ToString());
                }
                finally
                {
                    if (con != null)
                        con.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to create company database. Unable to proceed");
            }
        }

        private void btnCompInfoPrev_Click(object sender, EventArgs e)
        {
            tabGrouping.SelectedIndex = 0;
        }

        private void btnCompInfoNext_Click(object sender, EventArgs e)
        {
            btnCompanyDatabase_Click(sender, e); 
        }

        private void btnCreateCompDbPrev_Click(object sender, EventArgs e)
        {
            btnCompanyInfo_Click(sender, e);
        }

        private void btnCreateCompDbNext_Click(object sender, EventArgs e)
        {
            //if (cmbAuthentication.Text == "")
            //{
            //    MessageBox.Show("No authentication mode is selected. Choose one from the dropdown");
            //    return;
            //}

            //else if (cmbAuthentication.Text == "Username / Password"
            //         && (txtUsername.Text == "" || txtPassword.Text == ""))
            //{
            //    MessageBox.Show("Username and Password must be filled.");
            //    return;
            //}

            if (txtServerName.Text != ""
                && txtDatabaseName.Text != "")
            {
                newcompanyconnstr = "Data Source = " + txtServerName.Text + "; Initial Catalog = master; MultipleActiveResultSets = true";

                //if (cmbAuthentication.Text == "Windows Authentication")
                //{
                    newcompanyconnstr += "; Integrated Security = true";
                //}
                //else if ( cmbAuthentication.Text == "Username / Password")
                //{
                //    newcompanyconnstr += "; Network Library = DBMSSOCN; User ID = ablecctg; Password = 5!37e5CCt9";
                //}

                SqlConnection con = null;
                try
                {
                    con = new SqlConnection(newcompanyconnstr);
                    string checkdbexistencesql = "SELECT name FROM master.sys.databases WHERE name = '" + txtDatabaseName.Text + "'";

                    SqlCommand cmd = new SqlCommand(checkdbexistencesql, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Database name is existing in the server. Please type a new database name");
                        return;
                    }
                    else
                    {
                        btnSummary_Click(sender, e);
                    }
                }
                catch (SqlException exception)
                {
                    MessageBox.Show("Unable to connect to the server. Make sure it exists and running");
                    Console.WriteLine(exception.ToString());
                }
                finally
                {
                    if (con != null)
                        con.Close();
                }
            }
            else
            {
                MessageBox.Show("The server name and database name are required, please fill them up.");
            }
        }

        private bool createCompanyFile(string sourcefilename)
        {
            string templatedbscript = File.ReadAllText(sourcefilename);
            if (txtDatabaseName.Text == "")
            {
                MessageBox.Show("Target database name is empty");
                return false;
            }

            templatedbscript = templatedbscript.Replace("ABLE_TEMPLATE_DB", txtDatabaseName.Text);

            string[] sqlbatches = templatedbscript.Split(new[] { "GO" + Environment.NewLine }, StringSplitOptions.None);

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(newcompanyconnstr);
                con.Open();

                foreach (string sqlleaf in sqlbatches)
                {
                    if (sqlleaf != "")
                    {
                        SqlCommand uploadsqlcmd = new SqlCommand(sqlleaf, con);
                        uploadsqlcmd.ExecuteNonQuery();
                    }
                }
                string dbloginsql = @"IF NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE name = 'ableacctg')
                                    BEGIN
                                        CREATE LOGIN ableacctg WITH PASSWORD = '5!37e5CCt9'
                                    END";

                string[] sqlsecurity =
                {
                    dbloginsql,
                    "ALTER AUTHORIZATION ON DATABASE::[" + txtDatabaseName.Text + "] TO ableacctg"
                };

                foreach (string sqlleaf in sqlsecurity)
                {
                    SqlCommand securityprofilecmd = new SqlCommand(sqlleaf, con);
                    int res = securityprofilecmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnFileAsstNext_Click_1(object sender, EventArgs e)
        {
            btnCompanyInfo_Click(sender, e);
        }

        private void gbSummary_Enter(object sender, EventArgs e)
        {

        }

        private void gbCompanyDatabase_Enter(object sender, EventArgs e)
        {
        }

        private void gbCompanyInformation_Enter(object sender, EventArgs e)
        {
        }
    }
}
