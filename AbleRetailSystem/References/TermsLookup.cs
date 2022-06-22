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

namespace RestaurantPOS.References
{
    public partial class TermsLookup : Form
    {
        private string[] Profile;
        private string TermSelected;
        private string ID = "";
        private static string thisFormCode = "";
        private bool CanEdit = false;
        private int SaveMode = 0; //0 - View, 1 - Edit, 2 - New
        private DataRow CustomerRow;
        private DataTable TbRep = null;
        private decimal BalanceDueDate;
        private decimal DiscountDate;
        private decimal BalanceDueDays;
        private decimal DiscountDays;
        private string TermPayID;
        private DataRow TermsRow;

        private bool IsLoading = false;
        public TermsLookup( DataRow pTermsRow)
        {
            InitializeComponent();           
            TermsRow = pTermsRow;
        }

        public DataRow GetTerms
        {
            get { return TermsRow; }
        }
        private void LoadTerms()
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

        //private void LoadCustomer(string pID = "")
        //{
        //    SqlConnection con_ = null;
        //    try
        //    {
        //        con_ = new SqlConnection(CommonClass.ConStr);
        //        string selectSql = "SELECT * FROM Profile P INNER JOIN TermsOfPayment T ON T.TermsOfPaymentID = P.TermsOfPayment WHERE P.ID = " + ID;
        //        SqlCommand cmd_ = new SqlCommand(selectSql, con_);
        //        con_.Open();

        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = cmd_;
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        if (dt.Rows.Count > 0)
        //        {
        //            CustomerRow = dt.Rows[0];
        //            //INFORMATION
        //            lblID.Text = CustomerRow["ID"].ToString();
        //            string name = CustomerRow["Name"].ToString();
        //            txtCustomerName.Text = "Terms for " + name;
        //            decimal bal = 0;
        //            decimal discount =0;
        //            cboTerms.SelectedValue = CustomerRow["TermsOfPayment"].ToString();
        //            bal = txtBalance.Value;
        //            discount = txtDiscount.Value;


        //            txtBalance.Value = bal != 0 ? Convert.ToDecimal(bal) : 0;
        //            txtDiscount.Value = discount != 0 ? Convert.ToDecimal(discount) : 0;
        //            string strcredlimit = CustomerRow["CreditLimit"].ToString();
        //            string strearlypaymdiscpercent = CustomerRow["EarlyPaymentDiscountPercent"].ToString();
        //            txtEarlyPayment.Value = strearlypaymdiscpercent != "" ? Convert.ToDecimal(strearlypaymdiscpercent) : 0;
        //            string strltepaymchargepercent = CustomerRow["LatePaymentChargePercent"].ToString();
        //            txtLatePaymentCharge.Value = strltepaymchargepercent != "" ? Convert.ToDecimal(strltepaymchargepercent) : 0;
        //            string strvoldisc = CustomerRow["VolumeDiscount"].ToString();
        //            txtVolumeDiscount.Value = strvoldisc != "" ? Convert.ToDecimal(strvoldisc) : 0;

        //            decimal baldate = 0;
        //            //  baldate = txtBalance.Value;
        //            decimal discountdate = txtDiscount.Value;
        //            decimal discountdays = txtDiscount.Value;
 
        //            //Day of the month
        //            BalanceDueDate = baldate;
        //            DiscountDate = discountdate;

        //            //Specific Day
        //            BalanceDueDays = txtBalance.Value;
        //            DiscountDays = discountdays;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if (con_ != null)
        //            con_.Close();
        //    }
        //}

        private void TermsLookup_Load(object sender, EventArgs e)
        {
            IsLoading = true;
            LoadTerms();
            if (TermsRow != null)
            {
                this.cboTerms.SelectedValue = TermsRow["TermsOfPaymentID"].ToString();
                this.txtBalance.Text = TermsRow["BalanceDueDays"].ToString();
                this.txtDiscount.Text = TermsRow["DiscountDays"].ToString();
                this.txtVolumeDiscount.Text = TermsRow["VolumeDiscount"].ToString();
                this.txtEarlyPayment.Text = TermsRow["EarlyPaymentDiscountPercent"].ToString();
                this.txtLatePaymentCharge.Text = TermsRow["LatePaymentChargePercent"].ToString();
            }
            IsLoading = false;

        }

        private void cboTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsLoading) return;
            bool isFieldEnabled = false;
            TermPayID = cboTerms.SelectedValue.ToString();
            switch (cboTerms.SelectedValue.ToString())
            {
                //case "DM"://Day of the Month
                //    lblBalance.Text = "Balance Due Date";
                //    lblDiscount.Text = "Discount Due Date";
                //    lblBalanceNote.Text = "Specify Date of the Month (1-31)";
                //    lblDiscountNote.Text = "Specify Date of the Month (1-31)";
                //    isFieldEnabled = true;
                //    break;
                //case "DMEOM": //Day of the Month after EOM
                //    lblBalance.Text = "Balance Due Date";
                //    lblDiscount.Text = "Discount Date";
                //    lblBalanceNote.Text = "Specify Date of the Month (1-31)";
                //    lblDiscountNote.Text = "Specify Date of the Month (1-31)";
                //    isFieldEnabled = true;
                //    break;

                //case "SDEOM"://Specifc Day after EOM
                //    lblBalance.Text = "Balance Due Days";
                //    lblDiscount.Text = "Discount Days";
                //    lblBalanceNote.Text = "Specify # of Days";
                //    lblDiscountNote.Text = "Specify # of Days";
                //    isFieldEnabled = true;
                //    break;
                case "SD": //Specific Days
                    lblBalance.Text = "Balance Due Days";
                    lblDiscount.Text = "Discount Days";
                    lblBalanceNote.Text = "Specify # of Days";
                    lblDiscountNote.Text = "Specify # of Days";
                    isFieldEnabled = true;
                    break;
                default: //CASH           
                    txtBalance.Value = 0;
                    txtDiscount.Value = 0;
                    txtEarlyPayment.Value = 0;
                    txtLatePaymentCharge.Value = 0;
                    lblBalanceNote.Text = "     ";
                    lblDiscountNote.Text = "     ";
                    txtVolumeDiscount.Value = 0;
                    isFieldEnabled = false;
                    break;
            }
            txtBalance.Enabled = isFieldEnabled;
            txtDiscount.Enabled = isFieldEnabled;
            txtEarlyPayment.Enabled = isFieldEnabled;
            txtLatePaymentCharge.Enabled = isFieldEnabled;
            TermsRow["TermsOfPaymentID"] = TermPayID;
            TermsRow["BalanceDueDays"]= txtBalance.Value;
            TermsRow["DiscountDays"]= txtDiscount.Value;
            TermsRow["VolumeDiscount"]= txtVolumeDiscount.Value;
            TermsRow["EarlyPaymentDiscountPercent"] = txtEarlyPayment.Value;
            TermsRow["LatePaymentChargePercent"] = txtLatePaymentCharge.Value;
        }

        private void btn_Record_Click(object sender, EventArgs e)
        {
            //decimal baldate = 0;
            //decimal discountdate = txtDiscount.Value;
            //decimal discountdays = txtDiscount.Value;

            ////Day of the month
            //BalanceDueDate = baldate;
            //DiscountDate = discountdate;

            ////Specific Day
            //BalanceDueDays = txtBalance.Value;
            //DiscountDays = discountdays;

            //Profile = new string[12];
            //Profile[0] = cboTerms.Text;
            //Profile[1] = txtBalance.Text;
            //Profile[2] = txtDiscount.Text;
            //Profile[3] = txtVolumeDiscount.Value.ToString();
            //Profile[4] = txtEarlyPayment.Value.ToString();
            //Profile[5] = txtLatePaymentCharge.Value.ToString();
            //Profile[6] = BalanceDueDate.ToString();
            //Profile[7] = DiscountDate.ToString();
            //Profile[9] = BalanceDueDays.ToString();
            //Profile[10] = DiscountDays.ToString();
            //Profile[11] = TermPayID.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtBalance_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading) return;
            TermsRow["BalanceDueDays"] = txtBalance.Value;
        }

        private void txtDiscount_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading) return;
            TermsRow["DiscountDays"] = txtDiscount.Value;
        }

        private void txtVolumeDiscount_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading) return;
            TermsRow["VolumeDiscount"] = txtVolumeDiscount.Value;
        }

        private void txtEarlyPayment_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading) return;
            TermsRow["EarlyPaymentDiscountPercent"] = txtEarlyPayment.Value;
        }

        private void txtLatePaymentCharge_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading) return;
            TermsRow["LatePaymentChargePercent"] = txtLatePaymentCharge.Value;
        }
    }
}
