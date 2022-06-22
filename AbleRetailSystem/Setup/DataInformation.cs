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

namespace RestaurantPOS
{
    public partial class DataInformation : Form
    {
        private bool CanEdit = false;
        private string thisFormCode = "";
        public DataInformation()
        {
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }

            }

            string outy = "";
            CommonClass.AppFormCode.TryGetValue(this.Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
        }

        private void DataInformation_Load(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string datainfosql = "SELECT * FROM DataFileInformation";
                SqlCommand cmd = new SqlCommand(datainfosql, con);
                con.Open();
                SqlDataReader dtainforeader = cmd.ExecuteReader();

                if (dtainforeader.HasRows
                    && dtainforeader.Read())
                {
                  
                    txtABN.Text = dtainforeader["ABN"].ToString();
                    //txtABNBranch.Text = dtainforeader["ABNBranch"].ToString();
                    //txtACN.Text = dtainforeader["ACN"].ToString();
                    txtSalesTaxNo.Text = dtainforeader["SalesTaxNumber"].ToString();                   
                    txtCompanyName.Text = dtainforeader["CompanyName"].ToString();
                    txtEmail.Text = dtainforeader["Email"].ToString();
                    txtFaxNo.Text = dtainforeader["FaxNumber"].ToString();
                    txtPhoneNo.Text = dtainforeader["Phone"].ToString();
                    txtRegistrationNo.Text = dtainforeader["CompanyRegistrationNumber"].ToString();
                    txtSerialNo.Text = dtainforeader["SerialNumber"].ToString();
                    txtAdd1.Text = dtainforeader["Add1"].ToString();
                    txtAdd2.Text = dtainforeader["Add2"].ToString();
                    txtStreet.Text = dtainforeader["Street"].ToString();
                    txtCity.Text = dtainforeader["City"].ToString();
                    txtState.Text = dtainforeader["State"].ToString();
                    txtCountry.Text = dtainforeader["Country"].ToString();
                    txtPOBox.Text = dtainforeader["POBox"].ToString();
                    txtContactPerson.Text = dtainforeader["ContactPerson"].ToString();
                    this.lblTerminal.Text = dtainforeader["MaxTerminal"].ToString();
                    this.lblLogo.Text = (dtainforeader["CompanyLogo"].ToString() != null ? Application.StartupPath : dtainforeader["CompanyLogo"].ToString());

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
            this.btnUpdate.Enabled = CanEdit;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                string datainfosql = @"UPDATE DataFileInformation SET                                        
                                       ABN = @abn,
                                       SalesTaxNumber = @salestaxno,                                      
                                       Phone = @phoneno,
                                       FaxNumber = @faxno,
                                       Email = @emailaddr,
                                       Add1 = @Add1, 
                                       Add2 = @Add2,
                                       Street = @Street,
                                       City = @City,
                                       State = @State,
                                       Country = @Country,
                                       POBox = @POBox,
                                       ContactPerson = @ContactPerson,
                                       CompanyLogo = @CompanyLogo";

                SqlCommand cmd = new SqlCommand(datainfosql, con);
                cmd.CommandType = CommandType.Text;              
                cmd.Parameters.AddWithValue("@abn", txtABN.Text);
                //cmd.Parameters.AddWithValue("@abnbranch", txtABNBranch.Text);
                //cmd.Parameters.AddWithValue("@acn", txtACN.Text);
                cmd.Parameters.AddWithValue("@salestaxno", txtSalesTaxNo.Text);                
                cmd.Parameters.AddWithValue("@phoneno", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@faxno", txtFaxNo.Text);
                cmd.Parameters.AddWithValue("@emailaddr", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Add1", txtAdd1.Text);
                cmd.Parameters.AddWithValue("@Add2", txtAdd2.Text);
                cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@State", txtState.Text);
                cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                cmd.Parameters.AddWithValue("@POBox", txtPOBox.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                cmd.Parameters.AddWithValue("@CompanyLogo", lblLogo.Text);
                con.Open();
                int rowsaffected = cmd.ExecuteNonQuery();

               if (rowsaffected > 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Company Information.");
                    MessageBox.Show("Company information successfully updated");
                    Hide();
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

        private void btnLogoChange_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; )|*.jpg; *.jpeg; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                // pictureBox1.Image = new Bitmap(open.FileName);
                // image file path  
                lblLogo.Text = open.SafeFileName;
            }

        }
    }
}
