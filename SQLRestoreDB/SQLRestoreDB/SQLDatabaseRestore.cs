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
using System.Data.SqlTypes;
using System.Configuration;
using System.Data.Common;
using System.IO;
using Newtonsoft.Json;
using AbleRetailPOS;

namespace SQLRestoreDB
{
    public partial class SQLRestoreDB : Form
    {
        private SqlConnection con;
        private SqlCommand com;
        private SqlDataReader sdr;
        string sql = "";
        string connectionString = "";
        public string a;
        public string b;
        public string c;
        public SQLRestoreDB()
        {
            InitializeComponent();
        }

        private void SQLRestoreDB_Load(object sender, EventArgs e)
        {
            BtnDisconnect.Enabled = false;
            BtnRestore.Enabled = false;
            BtnConnect.Enabled = false;
            txtReplicateName.Enabled = false;
            txtBackupPath.Enabled = false;

            txtUserID.Enabled = false;
            txtPassword.Enabled = false;
            txtDataSource.Enabled = false;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAuthentication.Text == "SQL Server Authentication")
                {
                    connectionString = "Data Source = " + txtDataSource.Text + "; User Id = " + txtUserID.Text + "; Password = " + txtPassword.Text + ";";
                    Validate.Text = "Connected";
                    Validate.ForeColor = Color.Green;
                }
                if (cmbAuthentication.Text == "Windows Authentication")
                {
                    connectionString = "Data Source = " + txtDataSource.Text + "; Initial Catalog = master; Integrated Security=True";
                    Validate.Text = "Connected";
                    Validate.ForeColor = Color.Green;
                }

                con = new SqlConnection(connectionString);
                con.Open();

                sql = "SELECT * FROM sys.databases d WHERE d.database_id > 4";
                com = new SqlCommand(sql, con);
                sdr = com.ExecuteReader();

                txtDataSource.Enabled = false;
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;
                BtnConnect.Enabled = false;
                BtnDisconnect.Enabled = true;
                txtReplicateName.Enabled = true;
                txtDataSource.Enabled = true;
                BtnRestore.Enabled = true;
                txtBackupPath.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Validate.Text = "Invalid Server \n   Name";
                Validate.ForeColor = Color.Red;
            }
            finally
            {
                if (sdr != null)
                    sdr.Dispose();
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            txtReplicateName.Enabled = false;
            txtDataSource.Enabled = false;
            BtnRestore.Enabled = false;
            txtBackupPath.Enabled = false;
            BtnDisconnect.Enabled = false;
            BtnRestore.Enabled = false;
            BtnConnect.Enabled = true;
            txtUserID.Enabled = false;
            txtPassword.Enabled = false;
            txtDataSource.Enabled = true;
            Validate.Text = "Disconnected";
            Validate.ForeColor = Color.Red;
        }

        private void BtnBrowseRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Backup Files(*.bak)|*.bak|All Files(*.*)|*.*";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBackupPath.Text = dlg.FileName;
            }

            a = txtBackupPath.Text;
            c = Path.GetFileName(a);

            int index = c.LastIndexOf('.');
            b = index == -1 ? c : c.Substring(0, index);
        }

        private void RestoreAndReplicate()
        {
            System.IO.StreamReader file = null;
            System.IO.StreamWriter writefile = null;
            try
            {
                if (txtBackupPath.Text == ""
                   || txtReplicateName.Text == "")
                {
                    MessageBox.Show("Please select a backup file");
                    return;
                }
                con = new SqlConnection(connectionString);
                con.Open();
                sql = @"DECLARE @filelist table 
                        ( 
                            LogicalName varchar(256),
                            PhysicalName varchar(256),
	                        Type varchar(5),
	                        FileGroupName varchar(256),
	                        Size bigint,
	                        MaxSize bigint,
	                        Field bigint,
	                        CreateLSN bigint,
	                        DropLSN bigint,
	                        UniqueId varchar(512),
	                        ReadOnlyLSN bigint,
	                        ReadWriteLSN bigint,
	                        BackupSizeInBytes bigint,
	                        SourceBlockSize bigint,
	                        FileGroupId bigint,
	                        LogGroupGUID varchar(512),
	                        DifferentialBaseLSN bigint,
	                        DifferentialBaseGUID varchar(512),
	                        IsReadOnly bigint,
	                        IsPresent bigint,
	                        TDEThumbprint varchar(128),
                            Seq int NOT NULL identity(1,1)
                        ); 
                        DECLARE @backupFile varchar(max) = '" + txtBackupPath.Text + @"';
                        INSERT INTO @filelist EXEC('RESTORE FILELISTONLY FROM DISK = ''' + @backupFile + '''');

                        DECLARE @dbName varchar(256);
                        DECLARE @logName varchar(256);
                        SELECT @dbName = LogicalName FROM @filelist WHERE Type = 'D';
                        SELECT @logName = LogicalName FROM @filelist WHERE Type = 'L';
                        DECLARE @dataPath varchar(256);
                        SELECT @dataPath = CONVERT(sysname,SERVERPROPERTY('InstanceDefaultDataPath'));
                        DECLARE @newDBName varchar(256);
                        DECLARE @newLogName varchar(256);
                        SELECT @newDBName = @dataPath + '" + txtReplicateName.Text + @".mdf';
                        SELECT @newLogName = @dataPath + '" + txtReplicateName.Text + @".log';

                        RESTORE DATABASE " + txtReplicateName.Text + @"
                        FROM DISK = @backupFile
                        WITH REPLACE,
                        MOVE @dbName TO @newDBName,
                        MOVE @logName TO @newLogName;";

                com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                MessageBox.Show("Succesfully Restore Database");

                SqlCommand cmd = new SqlCommand(@"USE " + txtReplicateName.Text + @";
                                                  SELECT CompanyName, SerialNumber, CompanyRegistrationNumber FROM DataFileInformation", con);
                SqlDataReader dr = cmd.ExecuteReader();
                string lCompName = "";
                string lSerialNo = "";
                string lRegNo = "";
                if (dr.Read())
                {
                    lCompName = dr["CompanyName"].ToString().Trim();
                    lSerialNo = dr["SerialNumber"].ToString().Trim();
                    lRegNo = dr["CompanyRegistrationNumber"].ToString().Trim();
                }

                //code to update the config.json here
                string sourcePath = @Application.StartupPath;
                
                file = new System.IO.StreamReader(sourcePath + "\\config.json");
                string configjson = file.ReadToEnd();
                configjson = configjson.Replace("\t", "");

                file.Close();
                List<DeserializeTypes> compinfo = JsonConvert.DeserializeObject<List<DeserializeTypes>>(configjson);

                DeserializeTypes newcomp = new DeserializeTypes();
                newcomp.company_name = CommonClass.Encrypt(lCompName);
                newcomp.serial_number = CommonClass.Encrypt(lSerialNo);
                newcomp.registration_number = CommonClass.Encrypt(lRegNo);
                newcomp.database_name = txtReplicateName.Text;
                newcomp.server_name = txtDataSource.Text;
                newcomp.db_user = CommonClass.Encrypt("ableacctg");
                newcomp.db_pass = CommonClass.Encrypt("5!37e5CCt9");

                compinfo.Add(newcomp);

                string encryptedjson = JsonConvert.SerializeObject(compinfo, Formatting.Indented);
                writefile = new System.IO.StreamWriter(sourcePath + "\\config.json");
                writefile.Write(encryptedjson);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
                if (file != null)
                    file.Close();
                if (writefile != null)
                    writefile.Close();
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            RestoreAndReplicate();
            txtBackupPath.Text = "";
            txtReplicateName.Text = "";
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthentication.Text == "SQL Server Authentication")
            {
                BtnConnect.Enabled = true;
                txtUserID.Enabled = true;
                txtPassword.Enabled = true;
                txtDataSource.Enabled = true;
                txtDataSource.Text = "";
            }
            else if (cmbAuthentication.Text == "Windows Authentication")
            {
                BtnConnect.Enabled = true;
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;
                txtDataSource.Enabled = true;
            }
        }

        private void BtnBrowsePathFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //txtDataPath.Text = dlg.SelectedPath;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnConnect.PerformClick();
            }
        }

        private void DefaultLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (DefaultLogin.Checked)
            {
                cmbAuthentication.SelectedIndex = 0;
                txtUserID.Text = "ableacctg";
                txtPassword.Text = "5!37e5CCt9";
            }
            else
            {
                cmbAuthentication.SelectedIndex = 1;
            }
        }
    }
}
