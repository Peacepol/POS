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


namespace CoProEditor
{
    public partial class frmCoProEditor : Form
    {
        private bool addnew = false;
        private bool justdeleted = false;
        private DataGridViewRow SelCoRow = null;
        List <DeserializeTypes> compinfo = null;
        public frmCoProEditor()
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
                    lblAddNewEntry.Visible = false;
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
            lblAddNewEntry.Visible = false;
            addnew = false;
            justdeleted = false;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtCompName.Text = "";
            txtSerialNo.Text = "";
            txtRegNo.Text = "";
            txtDbName.Text = "";
            txtServerName.Text = "";
           
            lblAddNewEntry.Visible = true;
            addnew = true;
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
                MessageBox.Show("No config file is open, use 'Save As' instead to save to a new file");
                return;
            }

            if (!addnew)
            {
                if (SelCoRow != null && SelCoRow.Selected)
                {
                    compinfo[dgvCompInfo.CurrentRow.Index].company_name = CommonClass.Encrypt(txtCompName.Text);
                    compinfo[dgvCompInfo.CurrentRow.Index].serial_number = CommonClass.Encrypt(txtSerialNo.Text);
                    compinfo[dgvCompInfo.CurrentRow.Index].registration_number = CommonClass.Encrypt(txtRegNo.Text);
                    compinfo[dgvCompInfo.CurrentRow.Index].database_name = txtDbName.Text;
                    compinfo[dgvCompInfo.CurrentRow.Index].server_name = txtServerName.Text;
                    compinfo[dgvCompInfo.CurrentRow.Index].db_user = CommonClass.Encrypt("ableacctg");
                    compinfo[dgvCompInfo.CurrentRow.Index].db_pass = CommonClass.Encrypt("5!37e5CCt9");
                   

                    dgvCompInfo.CurrentRow.Cells[0].Value = txtCompName.Text;
                    dgvCompInfo.CurrentRow.Cells[1].Value = txtSerialNo.Text;
                    dgvCompInfo.CurrentRow.Cells[2].Value = txtRegNo.Text;
                    dgvCompInfo.CurrentRow.Cells[3].Value = txtDbName.Text;
                    dgvCompInfo.CurrentRow.Cells[4].Value = txtServerName.Text;
                }
            }
            else
            {
                DeserializeTypes newcompinfo = new DeserializeTypes();
                newcompinfo.company_name = CommonClass.Encrypt(txtCompName.Text);
                newcompinfo.serial_number = CommonClass.Encrypt(txtSerialNo.Text);
                newcompinfo.registration_number = CommonClass.Encrypt(txtRegNo.Text);
                newcompinfo.database_name = txtDbName.Text;
                newcompinfo.server_name = txtServerName.Text;
                
                newcompinfo.db_user = CommonClass.Encrypt("ableacctg");
                newcompinfo.db_pass = CommonClass.Encrypt("5!37e5CCt9");
                compinfo.Add(newcompinfo);
                string[] gridrow =
                {
                    txtCompName.Text,
                    txtSerialNo.Text,
                    txtRegNo.Text,
                    txtDbName.Text,
                    txtServerName.Text
                };

                dgvCompInfo.Rows.Add(gridrow);
                dgvCompInfo.ClearSelection();

                txtCompName.Text = "";
                txtSerialNo.Text = "";
                txtRegNo.Text = "";
                txtDbName.Text = "";
                txtServerName.Text = "";
               
            }

            string encryptedjson = JsonConvert.SerializeObject(compinfo, Formatting.Indented);

            System.IO.StreamWriter file = null;
            try
            {
                file = new System.IO.StreamWriter(txtConfigFile.Text);
                file.Write(encryptedjson);
                addnew = false;
                lblAddNewEntry.Visible = false;
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
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (sdgConfigFile.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter file = null;
                try
                {
                    if (txtCompName.Text != ""
                        && txtSerialNo.Text != ""
                        && txtRegNo.Text != ""
                        && txtDbName.Text != ""
                        && txtServerName.Text != ""
                       
                        )
                    {
                        DeserializeTypes newcompinfo = new DeserializeTypes();
                        newcompinfo.company_name = CommonClass.Encrypt(txtCompName.Text);
                        newcompinfo.serial_number = CommonClass.Encrypt(txtSerialNo.Text);
                        newcompinfo.registration_number = CommonClass.Encrypt(txtRegNo.Text);
                        newcompinfo.database_name = txtDbName.Text;
                        newcompinfo.server_name = txtServerName.Text;
                        newcompinfo.db_user = CommonClass.Encrypt("ableacctg");
                        newcompinfo.db_pass = CommonClass.Encrypt("5!37e5CCt9");

                        if (compinfo == null)
                            compinfo = new List<DeserializeTypes>();

                        compinfo.Add(newcompinfo);
                        string[] gridrow =
                        {
                            txtCompName.Text,
                            txtSerialNo.Text,
                            txtRegNo.Text,
                            txtDbName.Text,
                            txtServerName.Text
                        };

                        dgvCompInfo.Rows.Add(gridrow);
                    }
                    else
                    {
                        if (compinfo != null)
                            compinfo.Clear();

                        compinfo = null;
                    }

                    dgvCompInfo.ClearSelection();
                    txtConfigFile.Text = sdgConfigFile.FileName;

                    txtCompName.Text = "";
                    txtSerialNo.Text = "";
                    txtRegNo.Text = "";
                    txtDbName.Text = "";
                    txtServerName.Text = "";
                  

                    if (compinfo != null)
                    {
                        string encryptedjson = JsonConvert.SerializeObject(compinfo, Formatting.Indented);

                        file = new System.IO.StreamWriter(sdgConfigFile.FileName);
                        file.Write(encryptedjson);
                    }

                    lblAddNewEntry.Visible = false;
                    addnew = false;
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
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SelCoRow != null 
                && SelCoRow.Selected
                && compinfo != null
                && compinfo.Count > 0)
            {
                compinfo.RemoveAt(SelCoRow.Index);
                dgvCompInfo.Rows.RemoveAt(SelCoRow.Index);
                justdeleted = true;
                SelCoRow = null;

                dgvCompInfo.ClearSelection();
                txtCompName.Text = "";
                txtSerialNo.Text = "";
                txtRegNo.Text = "";
                txtDbName.Text = "";
                txtServerName.Text = "";
            }
        }

       

        private void frmCoProEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
