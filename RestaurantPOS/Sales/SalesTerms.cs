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

namespace AbleRetailPOS.Sales
{
    public partial class SalesTerms : Form
    {
        DataRow ITerms;
        public SalesTerms(DataRow pTerms)
        {
            InitializeComponent();
            ITerms = pTerms;
        }
        public DataRow GetTerms
        {
            get { return ITerms; }
        }

        private void SalesTerms_Load(object sender, EventArgs e)
        {
            FillCombo();
        }
        private void FillCombo()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM TermsOfPayment";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboTerms.DataSource = dt;
                cboTerms.ValueMember = "TermsOfPaymentID";
                cboTerms.DisplayMember = "Description";
                if(ITerms["TermsOfPaymentID"].ToString() != "")
                {
                    cboTerms.SelectedValue = ITerms["TermsOfPaymentID"].ToString();
                }
                else
                {
                    cboTerms.SelectedValue = "CASH";

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

       
        private void btn_Record_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtBalance_ValueChanged(object sender, EventArgs e)
        {
            ITerms["BalanceDueDays"] = this.txtBalance.Value;
        }

        private void txtDiscount_ValueChanged(object sender, EventArgs e)
        {
            ITerms["DiscountDays"] = this.txtDiscount.Value;
        }

        private void txtVolumeDiscount_ValueChanged(object sender, EventArgs e)
        {
            ITerms["VolumeDiscount"] = this.txtVolumeDiscount.Value;
        }

        private void txtEarlyPayment_ValueChanged(object sender, EventArgs e)
        {
            ITerms["EarlyPaymentDiscountPercent"] = this.txtEarlyPayment.Value;
        }

        private void txtLatePaymentCharge_ValueChanged(object sender, EventArgs e)
        {
            ITerms["LatePaymentChargePercent"] = this.txtLatePaymentCharge.Value;
        }
    }
}
