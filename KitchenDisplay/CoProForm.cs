using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace KitchenDisplay
{
    public partial class CoProForm : Form
    {
        private DataGridViewRow SelCoRow;
        private KitchenDisplay mainpageref;
        private const int TRIAL_PERIOD = 30; // days


        public CoProForm(KitchenDisplay mpref)
        {
            InitializeComponent();
            mainpageref = mpref;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        public DataGridViewRow GetCo
        {
            get { return SelCoRow; }
        }

        private void Login()
        {
            SelCoRow = this.dgridCo.CurrentRow;
            //Code to check the DataFileInformation if compname,serial,regno in config.json matches that in the DB
            string checkdbintegritysql = "SELECT CompanyName, CompanyRegistrationNumber, SerialNumber, CreationDate, IsActive, ActivationDate FROM DataFileInformation";

            string servername = SelCoRow.Cells["ServerName"].Value.ToString();
            string dbname = SelCoRow.Cells["DatabaseName"].Value.ToString();
            string dbuname = SelCoRow.Cells["dbuser"].Value.ToString();
            string dbpass = SelCoRow.Cells["dbpass"].Value.ToString();
            CommonClass.ConStr = "Data Source = " + servername + "; Initial Catalog = " + dbname + "; MultipleActiveResultSets = true";

            if (dbuname == "" && dbpass == "")
                CommonClass.ConStr += "; Integrated Security = true";
            else
                CommonClass.ConStr += "; User ID = " + dbuname + "; Password = " + dbpass;

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(checkdbintegritysql, con);
                con.Open();
                SqlDataReader resultreader = cmd.ExecuteReader();
                if (resultreader.HasRows)
                {
                    while (resultreader.Read())
                    {
                        if (resultreader["CompanyName"].ToString().Trim() == SelCoRow.Cells["CompName"].Value.ToString()
                            && resultreader["CompanyRegistrationNumber"].ToString() == SelCoRow.Cells["RegistrationNo"].Value.ToString()
                            && resultreader["SerialNumber"].ToString() == SelCoRow.Cells["SerialNo"].Value.ToString())
                        {
                            bool isactive = (bool)resultreader["IsActive"];
                            CommonClass.IsLocked = false;

                            if (!isactive)
                            {
                                DateTime dbutcdate = DateTime.SpecifyKind(DateTime.Parse(resultreader["CreationDate"].ToString()), DateTimeKind.Utc);
                                DateTime dblocaldate = dbutcdate.ToLocalTime();
                                DateTime nowdate = DateTime.Now;

                                TimeSpan diff = nowdate.Subtract(dblocaldate);
                                if (diff.Days > TRIAL_PERIOD)
                                {
                                    DialogResult dlgres = MessageBox.Show("Your trial period has expired. Do you want to activate?", "Activation", MessageBoxButtons.YesNo);
                                    if (dlgres == DialogResult.Yes)
                                    {
                                        CommonClass.InitCompanyFile();
                                        Activate frmActivate = new Activate("Activate");
                                        frmActivate.Show();
                                        CommonClass.IsLocked = true;
                                        return;
                                    }
                                    return;

                                }
                                else
                                {
                                    MessageBox.Show("You still have " + (TRIAL_PERIOD - diff.Days) + " days left in your trial period");
                                    CommonClass.IsLocked = false;
                                }
                            }
                            else
                            {
                                DateTime activationutcdate = DateTime.SpecifyKind(DateTime.Parse(resultreader["ActivationDate"].ToString()), DateTimeKind.Utc);
                                DateTime activationlocaldate = activationutcdate.ToLocalTime();
                                DateTime nowdate = DateTime.Now;
                                TimeSpan chronodiff = nowdate.Subtract(activationlocaldate);

                                if (chronodiff.Days > 31      // Current software was activated for at least a month.
                                    && nowdate.Month >= 10    // Month of October
                                    && nowdate.Month <= 12)   // Upto Month of December                                 
                                {
                                    DialogResult dlgres = MessageBox.Show("Reactivation season has come. Do you want to reactivate?", "Reactivation", MessageBoxButtons.YesNo);
                                    if (dlgres == DialogResult.Yes)
                                    {
                                        //mainpageref.reactivateToolStripMenuItem_Click(sender, e);
                                        Activate frmActivate = new Activate("Reactivate");
                                        frmActivate.Show();
                                        return;
                                    }
                                    else if (dlgres == DialogResult.No)
                                    {
                                        if (nowdate.Month == 12 && nowdate.Day == 31)
                                        {
                                            string deactivatesql = "UPDATE DataFileInformation SET IsActive = 0";
                                            SqlCommand deactcmd = new SqlCommand(deactivatesql, con);
                                            int rowsaffected = deactcmd.ExecuteNonQuery();
                                            if (rowsaffected > 0)
                                            {
                                                MessageBox.Show("You need to reactivate. Only viewing function is available");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("You have until the 31st of December to reactivate");
                                        }
                                    }
                                }
                            }

                            CommonClass.InitCompanyFile();
                            Hide();
                            Login LoginDlg = new Login(GetCo, mainpageref, this);
                            LoginDlg.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("CoPro entry does not match the database info");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to establish connection to the selected company database. Please make sure that database server is correct and configured properly to accept connection.");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void CoProForm_Load(object sender, EventArgs e)
        {
            dgridCo.Rows.Clear();
            string sourcePath = @Application.StartupPath;

            System.IO.StreamReader file = null;
            try
            {
                file = new System.IO.StreamReader(sourcePath + "\\config.json");
                string configjson = file.ReadToEnd();
                configjson = configjson.Replace("\t", "");

                List<DeserializeTypes> compinfo = JsonConvert.DeserializeObject<List<DeserializeTypes>>(configjson);

                foreach (DeserializeTypes compinfoleaf in compinfo)
                {
                    string[] gridrow = {
                        CommonClass.Decrypt(compinfoleaf.company_name),
                        CommonClass.Decrypt(compinfoleaf.serial_number),
                        CommonClass.Decrypt(compinfoleaf.registration_number),
                        compinfoleaf.database_name,
                        compinfoleaf.server_name,
                        CommonClass.Decrypt(compinfoleaf.db_user),
                        CommonClass.Decrypt(compinfoleaf.db_pass)
                    };
                    dgridCo.Rows.Add(gridrow);
                }

                if (this.dgridCo.Rows.Count > 0)
                {
                    this.dgridCo.FirstDisplayedScrollingRowIndex = 0;
                    btnLogin.Enabled = true;
                    btnLogin.Focus();
                }
                else
                {
                    this.btnLogin.Enabled = false;
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

        private void dgridCo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Login();
        }
    }
}
