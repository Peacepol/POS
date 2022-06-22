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
using System.IO;
using Newtonsoft.Json;

namespace RestaurantPOS.Sales
{


    public partial class EmailStatement : Form
    {
        private StatementMailer mailer;
        private string mailhost = "mail.tawamist.com";
        private Int32 port = 2525;
        private string mailuser = "ableacctg@tawamist.com";
        private string mailpass = "-kJKL,eTyqv;";
        private string profileids = "";

        public EmailStatement()
        {
            InitializeComponent();
            string sourceFileName = @Application.StartupPath + "\\smtp.json";
            
            if (!File.Exists(sourceFileName))
                return;

            System.IO.StreamReader openfile = null;
            try
            {
                openfile = new System.IO.StreamReader(sourceFileName);
                string smtpjson = openfile.ReadToEnd();
                smtpjson = smtpjson.Replace("\t", "");

                EmailFields smtpinfo = JsonConvert.DeserializeObject<EmailFields>(smtpjson);
                if (smtpinfo != null)
                {
                    mailhost = smtpinfo.smtphost;
                    port = smtpinfo.smtpport;
                    mailuser = smtpinfo.usermail;
                    mailpass = smtpinfo.userpassword;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (openfile != null)
                    openfile.Close();
            }
        }

        private void EmailStatement_Load(object sender, EventArgs e)
        {
            //Set default values of e-mail
            txtSubject.Text = "From " + CommonClass.CompName;
            txtEmailAddr.Text = mailuser;
            rTxtMessage.Text = "Please contact us immediately if you are unable to detach or download your Statement. Thank you.";

            SqlConnection con = null;
            try
            {
                string lCustomersSql = @"SELECT DISTINCT p.ID,
                                            0 AS Chk,  
                                            p.Name,
                                            c.email,
                                            p.CurrentBalance
                                        FROM Sales s
                                        INNER JOIN Profile p ON s.CustomerID = p.ID
                                        INNER JOIN Contacts c ON p.ID = c.ProfileID
                                        WHERE InvoiceStatus = 'Open' AND c.Location = p.LocationID
                                        AND p.Type = 'Customer'";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(lCustomersSql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataTable dtCloned = dt.Clone();
                dtCloned.Columns[1].DataType = typeof(bool);
                foreach (DataRow row in dt.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                dgEmailList.DataSource = dtCloned;
                dgEmailList.Columns[0].Visible = false;
                dgEmailList.Columns[1].Width = 42;
                dgEmailList.Columns[1].HeaderText = "Select";
                dgEmailList.Columns[2].HeaderText = "Customer";
                dgEmailList.Columns[2].ReadOnly = true;
                dgEmailList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgEmailList.Columns[3].ReadOnly = true;
                dgEmailList.Columns[3].HeaderText = "Email Address";
                dgEmailList.Columns[4].ReadOnly = true;
                dgEmailList.Columns[4].HeaderText = "Balance";
                dgEmailList.Columns[4].DefaultCellStyle.Format = "C2";
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

        private void dgEmailList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 1)
                dgEmailList.CurrentCell.ReadOnly = true;
            else if (e.ColumnIndex == 1)
            {
                profileids = "";
                foreach (DataGridViewRow dgvrow in dgEmailList.Rows)
                {
                    if ((bool)dgvrow.Cells[1].EditedFormattedValue
                        && dgvrow.Cells[3].Value.ToString() != "")
                    {
                        if (profileids != "")
                            profileids += ",";

                        profileids += dgEmailList.CurrentRow.Cells[0].Value.ToString();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            mailer = new StatementMailer(mailhost, port, mailuser, mailpass);
            mailer.From = txtEmailAddr.Text;
            mailer.Subject = txtSubject.Text;
            mailer.Body = rTxtMessage.Text;
            mailer.Profiles = profileids;

            DateTime edate = dtpStatementDt.Value.ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);

            //Send the email to all selected profile
            if (profileids != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                if (mailer.MailAttachments(edate))
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Customer statements sent succesfully");
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No email sent");
                }
            }
            else
            {
                MessageBox.Show("No Customer selected, or customer have no email address");
            }
        }
    }
}
