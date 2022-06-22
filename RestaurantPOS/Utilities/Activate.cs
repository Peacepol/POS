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

namespace AbleRetailPOS.Utilities
{
    public partial class Activate : Form
    {
        private string mIsReactivate = "Activate";

        public Activate(string pIsReactivate = "Activate")
        {
            mIsReactivate = pIsReactivate;
            InitializeComponent();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (rtxtActivationKey.Text == "")
            {
                MessageBox.Show("Activation key cannot be empty");
                return;
            }

            if (mIsReactivate == "Reactivate" && txtInvoiceNo.Text == "")
            {
                MessageBox.Show("Invoice number cannot be empty");
                return;
            }

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string checkkeysql = "SELECT CompanyName, CompanyRegistrationNumber, SerialNumber, MaxTerminal FROM DataFileInformation";
                SqlCommand cmd = new SqlCommand(checkkeysql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() && reader.HasRows)
                {
                    string[] activationelements;


                    if (mIsReactivate != "Reactivate")
                    {
                        string activatesql = "UPDATE DataFileInformation SET IsActive = 1, ActivationDate = GETUTCDATE(), MaxTerminal = @MaxTerminal";

                        SqlCommand activatecmd = new SqlCommand(activatesql, con);
                        activationelements = CommonClass.decodeActivationKey2(rtxtActivationKey.Text);
                        if(activationelements.Count() != 4)
                        {
                            MessageBox.Show("Activation failed");
                            return;
                        }
                        activatecmd.Parameters.AddWithValue("@MaxTerminal", activationelements[3]);
                        string dbcompname = reader["CompanyName"].ToString().Trim();
                        string dbcompregno = reader["CompanyRegistrationNumber"].ToString().Trim();
                        string dbserialno = reader["SerialNumber"].ToString().Trim();
                        if (dbcompname == activationelements[0]
                            && dbcompregno == activationelements[1]
                            && dbserialno == activationelements[2])
                        {
                            int rowsaffected = activatecmd.ExecuteNonQuery();
                            if (rowsaffected == 1)
                            {    if (btnActivate.Text == "Activate")
                                {
                                    MessageBox.Show("Activation successful");
                                }
                                else
                                {
                                    MessageBox.Show("Successfully updated");
                                }
                          
                            }
                            else
                            {
                                MessageBox.Show("Activation failed");
                            }
                        }
                    }
                    else
                    {
                        string lEnReactivationKey = CommonClass.SHA512(rtxtActivationKey.Text);
                        string activatesql = "UPDATE DataFileInformation SET IsActive = 1, ActivationDate = GETUTCDATE(), MaxTerminal = @MaxTerminal, Reactivation = '" + lEnReactivationKey + "'";

                        SqlCommand activatecmd = new SqlCommand(activatesql, con);
                        
                        activationelements = CommonClass.decodeReActivationKey(rtxtActivationKey.Text);
                        if (activationelements.Count() != 3)
                        {
                            MessageBox.Show("Re-Activation failed");
                            return;
                        }
                        activatecmd.Parameters.AddWithValue("@MaxTerminal", activationelements[2]);
                        string dbcompname = reader["CompanyName"].ToString().Trim().ToUpper().Substring(0, 4);
                        string uiinvoiceno = txtInvoiceNo.Text.ToUpper().Substring(0, 4);
                        if (dbcompname == activationelements[0]
                            && uiinvoiceno == activationelements[1])
                        {
                            int rowsaffected = activatecmd.ExecuteNonQuery();
                            if (rowsaffected == 1)
                            {
                                MessageBox.Show("Re-activation successful");
                            }
                        }
                    }
                }

                Hide();
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

        private void Activate_Load(object sender, EventArgs e)
        {
            lblComp.Text = CommonClass.CompName;
            this.Text = mIsReactivate == "Reactivate" ? "Re-Activate" : "Activate";

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string checkkeysql = "SELECT IsActive, SerialNumber, CompanyRegistrationNumber FROM DataFileInformation";
                SqlCommand cmd = new SqlCommand(checkkeysql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (mIsReactivate != "Reactivate")
                {
                    if (reader.Read()
                        && reader.HasRows
                        && (bool)reader["IsActive"])
                    {
                        txtSerialNo.Text = reader["SerialNumber"].ToString();
                        lblRegNo.Text = reader["CompanyRegistrationNumber"].ToString();
                        label67.Text = "Your product is already activated";
                      //  rtxtActivationKey.Enabled = false;
                        btnActivate.Text = "Update";
                    }
                    else
                    {
                        txtSerialNo.Text = reader["SerialNumber"].ToString();
                        lblRegNo.Text = reader["CompanyRegistrationNumber"].ToString();
                    }
                }
                else
                {
                    if (reader.Read()
                        && reader.HasRows
                        && (bool)reader["IsActive"])
                    {
                        txtSerialNo.Text = reader["SerialNumber"].ToString();
                        lblRegNo.Text = reader["CompanyRegistrationNumber"].ToString();
                        lblInvoiceNo.Visible = true;
                        txtInvoiceNo.Visible = true;
                    }
                    else
                    {
                        txtSerialNo.Text = reader["SerialNumber"].ToString();
                        lblRegNo.Text = reader["CompanyRegistrationNumber"].ToString();
                        label1.Text = "Call the phone number below to get your activation key and invoice number.";
                        label67.Text = "Input the activation key and invoice number";
                        label68.Text = "Enter values to re-activate";
                        btnActivate.Text = "Re-Activate";
                        lblInvoiceNo.Visible = true;
                        txtInvoiceNo.Visible = true;
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

        private void linkSupport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "mailto:support@able.com.pg?subject=Activation - " + CommonClass.CompName;
            proc.Start();
        }
    }
}
