using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ChangeCompanyNameRetail
{
    public partial class frmChangeCompanyName : Form
    {
        private bool addnew = false;
        private bool justdeleted = false;
        private DataGridViewRow SelCoRow = null;
        List <DeserializeTypes> compinfo = null;
        public frmChangeCompanyName()
        {
            InitializeComponent();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = odgBrowseConfig.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvCompInfo.Rows.Clear();
                txtConfigFile.Text = odgBrowseConfig.FileName;

                System.IO.StreamReader file = null;
                try
                {
                    file = new System.IO.StreamReader(txtConfigFile.Text);
                    string configjson = file.ReadToEnd();
                    configjson = configjson.Replace("\t", "");

                    compinfo = JsonConvert.DeserializeObject<List<DeserializeTypes>>(configjson);

                    foreach (DeserializeTypes compinfoleaf in compinfo)
                    {
                        string[] gridrow =
                        {
                            CommonClass.Decrypt(compinfoleaf.company_name),
                            CommonClass.Decrypt(compinfoleaf.serial_number),
                            CommonClass.Decrypt(compinfoleaf.registration_number),
                            compinfoleaf.database_name,
                            compinfoleaf.server_name
                        };

                        dgvCompInfo.Rows.Add(gridrow);
                    }

                    if (this.dgvCompInfo.Rows.Count > 0)
                    {
                        this.dgvCompInfo.FirstDisplayedScrollingRowIndex = 0;
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }
            }
        }

        private void dgvCompInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelCoRow = dgvCompInfo.CurrentRow;
            txtCompName.Text = SelCoRow.Cells["CoName"].Value.ToString();
            txtSerialNo.Text = SelCoRow.Cells["SerialNo"].Value.ToString();
            txtRegNo.Text = SelCoRow.Cells["RegCode"].Value.ToString();
            txtDbName.Text = SelCoRow.Cells["DBName"].Value.ToString();
            txtServerName.Text = SelCoRow.Cells["ServerAddress"].Value.ToString();
           
            this.Refresh();
           
            addnew = false;
            justdeleted = false;
        }


        private bool fieldCheck()
        {
            if (txtCompName.Text == "")
            {
                MessageBox.Show("Company Name is required");
                return false;
            }
            else if (txtSerialNo.Text == "")
            {
                MessageBox.Show("Serial Number is required");
                return false;
            }
            else if (txtRegNo.Text == "")
            {
                MessageBox.Show("Registration Number is required");
                return false;
            }
            else if (txtDbName.Text == "")
            {
                MessageBox.Show("Database Name is required");
                return false;
            }
            else if (txtServerName.Text == "")
            {
                MessageBox.Show("Server Name is required");
                return false;
            }
            

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!justdeleted && !fieldCheck())
                return;

            if (txtConfigFile.Text == "")
            {
                MessageBox.Show("No config file is open.");
                return;
            }
            if (ActivateCompanyFile())
            {
                compinfo[dgvCompInfo.CurrentRow.Index].company_name = CommonClass.Encrypt(txtNewCompName.Text);
                compinfo[dgvCompInfo.CurrentRow.Index].serial_number = CommonClass.Encrypt(txtSerialNo.Text);
                compinfo[dgvCompInfo.CurrentRow.Index].registration_number = CommonClass.Encrypt(txtRegNo.Text);
                compinfo[dgvCompInfo.CurrentRow.Index].database_name = txtDbName.Text;
                compinfo[dgvCompInfo.CurrentRow.Index].server_name = txtServerName.Text;
                compinfo[dgvCompInfo.CurrentRow.Index].db_user = CommonClass.Encrypt("ableacctg");
                compinfo[dgvCompInfo.CurrentRow.Index].db_pass = CommonClass.Encrypt("5!37e5CCt9");


                dgvCompInfo.CurrentRow.Cells[0].Value = txtNewCompName.Text;
                dgvCompInfo.CurrentRow.Cells[1].Value = txtSerialNo.Text;
                dgvCompInfo.CurrentRow.Cells[2].Value = txtRegNo.Text;
                dgvCompInfo.CurrentRow.Cells[3].Value = txtDbName.Text;
                dgvCompInfo.CurrentRow.Cells[4].Value = txtServerName.Text;

                string encryptedjson = JsonConvert.SerializeObject(compinfo, Formatting.Indented);

                System.IO.StreamWriter file = null;
                try
                {
                    file = new System.IO.StreamWriter(txtConfigFile.Text);
                    file.Write(encryptedjson);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }
                MessageBox.Show("Successfully Changed company name.");
            }
            else
            {
                MessageBox.Show("Error in changing company name.");
            }
        }

      
       

        private void frmCoProEditor_Load(object sender, EventArgs e)
        {

        }

        private  bool ActivateCompanyFile()
        {
            bool retValue = false;
            if (txtActivation.Text == "")
            {
                MessageBox.Show("Activation key cannot be empty");
                retValue= false;
            }

            SqlConnection con = null;
            try
            {
                string ConStr = "Data Source = " + txtServerName.Text + "; Initial Catalog = " + txtDbName.Text + "; MultipleActiveResultSets = true; User ID = ableacctg; Password = 5!37e5CCt9";
                con = new SqlConnection(ConStr);
                string checkkeysql = "SELECT CompanyName, CompanyRegistrationNumber, SerialNumber, MaxTerminal FROM DataFileInformation";
                SqlCommand cmd = new SqlCommand(checkkeysql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() && reader.HasRows)
                {
                   

                    string activatesql = "UPDATE DataFileInformation SET CompanyName = @CompanyName, IsActive = 1, ActivationDate = GETUTCDATE(), MaxTerminal = @MaxTerminal";

                    SqlCommand activatecmd = new SqlCommand(activatesql, con);
                    string[] activationelements;
                    activationelements = CommonClass.decodeActivationKey2(txtActivation.Text);
                    if (activationelements.Count() != 4)
                    {
                        MessageBox.Show("Activation failed");
                        retValue = false;
                    }
                    if (reader["CompanyName"].ToString().Trim() == txtCompName.Text)
                    {
                        activatecmd.Parameters.AddWithValue("@MaxTerminal", activationelements[3]);
                        activatecmd.Parameters.AddWithValue("@CompanyName", txtNewCompName.Text);
                        string dbcompname = txtNewCompName.Text;
                        string dbcompregno = reader["CompanyRegistrationNumber"].ToString().Trim();
                        string dbserialno = reader["SerialNumber"].ToString().Trim();
                        if (dbcompname == activationelements[0]
                            && dbcompregno == activationelements[1]
                            && dbserialno == activationelements[2])
                        {
                            int rowsaffected = activatecmd.ExecuteNonQuery();
                            if (rowsaffected == 1)
                            {
                                MessageBox.Show("Successfully Activated Company File");
                                retValue = true;
                            }
                            else
                            {
                                MessageBox.Show("Activation failed");
                                retValue = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Current Company Name does not match the company name in the database.");
                        retValue = false;
                    }
                }

               
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                retValue = false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return retValue;
        }
    }
}
