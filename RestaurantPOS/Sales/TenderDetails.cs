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
using System.Text.RegularExpressions;
using System.Globalization;

namespace AbleRetailPOS
{
    public partial class TenderDetails : Form
    {
        private DataTable PaymentInfoTb;
        private Boolean CanEdit = false;
        private string RecipientAccountID = "0";
        private int xMethodID = 0;
        private string ChangePaymentMethodID = "0";
        private int GCRowID = 0;
        private decimal ChangeAmount = 0;
        public TenderDetails(DataTable pPaymentInfo = null, decimal pDue = 0, Boolean pCanEdit = true)
        {
            InitializeComponent();
            PaymentInfoTb = pPaymentInfo;
            CanEdit = pCanEdit;
            this.txtTotalAmount.Value = pDue;
            this.btnClear.Enabled = CanEdit;
            this.btnRecord.Enabled = CanEdit;
        }

        public DataTable GetPaymentInfo
        {
            get { return PaymentInfoTb; }
        }
        public decimal GetPayedAmount
        {
            get { return AmountPaid.Value - AmountChange.Value; }
        }
        public string GetChangemount
        {
            get { return AmountChange.Value.ToString(); }
        }

        void LoadPayment()
        {
            for (int x = 0; x < PaymentInfoTb.Rows.Count; x++)
            {
                DataRow dr = PaymentInfoTb.Rows[x];

                for (int i = 0; i < dgPaymentMethod.Rows.Count; i++)
                {
                    DataGridViewRow dgRow = dgPaymentMethod.Rows[i];
                    if (dgRow.Cells[0].Value.ToString() == dr["PaymentMethodID"].ToString())
                    {
                        decimal lamt = Convert.ToDecimal(dr["AmountPaid"].ToString());
                        if(lamt > 0 )
                        {
                            dgRow.Cells[3].Value = dr["AmountPaid"].ToString();
                            dgRow.Cells[4].Value = dr["PaymentAuthorisationNumber"].ToString();
                            dgRow.Cells[5].Value = dr["PaymentCardNumber"].ToString();
                            dgRow.Cells[6].Value = dr["PaymentNameOnCard"].ToString();
                            dgRow.Cells[7].Value = dr["PaymentExpirationDate"].ToString();
                            dgRow.Cells[8].Value = dr["PaymentCardNotes"].ToString();
                            dgRow.Cells[9].Value = dr["PaymentBSB"].ToString();
                            dgRow.Cells[10].Value = dr["PaymentBankAccountNumber"].ToString();
                            dgRow.Cells[11].Value = dr["PaymentBankAccountName"].ToString();
                            dgRow.Cells[12].Value = dr["PaymentChequeNumber"].ToString();
                            dgRow.Cells[13].Value = dr["PaymentBankNotes"].ToString();
                            dgRow.Cells[14].Value = dr["PaymentNotes"].ToString();
                            dgRow.Cells[15].Value = dr["PaymentGCNo"].ToString();
                            dgRow.Cells[16].Value = dr["PaymentGCNotes"].ToString();
                        }else
                        {
                            if(dgRow.Cells[0].Value.ToString() == ChangePaymentMethodID)
                            {
                                AmountChange.Value = lamt;
                            }
                            else
                            {
                                dgRow.Cells[3].Value = dr["AmountPaid"].ToString();
                                dgRow.Cells[4].Value = dr["PaymentAuthorisationNumber"].ToString();
                                dgRow.Cells[5].Value = dr["PaymentCardNumber"].ToString();
                                dgRow.Cells[6].Value = dr["PaymentNameOnCard"].ToString();
                                dgRow.Cells[7].Value = dr["PaymentExpirationDate"].ToString();
                                dgRow.Cells[8].Value = dr["PaymentCardNotes"].ToString();
                                dgRow.Cells[9].Value = dr["PaymentBSB"].ToString();
                                dgRow.Cells[10].Value = dr["PaymentBankAccountNumber"].ToString();
                                dgRow.Cells[11].Value = dr["PaymentBankAccountName"].ToString();
                                dgRow.Cells[12].Value = dr["PaymentChequeNumber"].ToString();
                                dgRow.Cells[13].Value = dr["PaymentBankNotes"].ToString();
                                dgRow.Cells[14].Value = dr["PaymentNotes"].ToString();
                                dgRow.Cells[15].Value = dr["PaymentGCNo"].ToString();
                                dgRow.Cells[16].Value = dr["PaymentGCNotes"].ToString();
                            }
                           
                        }
                       
                    }
                }
            }
        }
       
        private void TenderDetails_Load(object sender, EventArgs e)
        {
            btnRecord.Enabled = CanEdit;
            btnClear.Enabled = CanEdit;
            LoadPaymentMethods();
            MethodValues();
            LoadPayment();
            TenderCalc();                       
        }

        private void LoadPaymentMethods()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd_ = new SqlCommand("SELECT PaymentMethod, ID FROM PaymentMethods ", con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd_;
                DataTable ldt = new DataTable();
                da.Fill(ldt);
                int ctr = 0;
                for(int i = 0; i < ldt.Rows.Count; i++)
                {
                    DataRow dr = ldt.Rows[i];
                    dgPaymentMethod.Rows.Add();
                    dgPaymentMethod.Rows[i].Cells[0].Value = dr["ID"].ToString();
                    dgPaymentMethod.Rows[i].Cells[2].Value = dr["PaymentMethod"].ToString();
                //    dgPaymentMethod.Rows[i].Cells[3].Value = "";
                    if(dr["PaymentMethod"].ToString().ToUpper() == "CASH")
                    {
                        ChangePaymentMethodID = dr["ID"].ToString();
                    }
                    if (dr["PaymentMethod"].ToString().ToUpper() == "GC")
                    {
                        GCRowID = ctr;
                    }
                    ctr++;
                }
                if(ldt. Rows.Count >0)
                dgPaymentMethod.Rows[0].Selected = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        void MethodValues()
        {
            if (PaymentInfoTb != null)
            {
                txtAuthorization.Text = (PaymentInfoTb.Rows[0]["PaymentAuthorisationNumber"] != null ? PaymentInfoTb.Rows[0]["PaymentAuthorisationNumber"].ToString() : "");
                txtCardNo.Text = (PaymentInfoTb.Rows[0]["PaymentCardNumber"] != null ? PaymentInfoTb.Rows[0]["PaymentCardNumber"].ToString() : "");
                txtCardName.Text = (PaymentInfoTb.Rows[0]["PaymentNameOnCard"] != null ? PaymentInfoTb.Rows[0]["PaymentNameOnCard"].ToString() : "");
                txtCardExpiry.Text = (PaymentInfoTb.Rows[0]["PaymentExpirationDate"] != null ? PaymentInfoTb.Rows[0]["PaymentExpirationDate"].ToString() : "");
                txtCardNotes.Text = (PaymentInfoTb.Rows[0]["PaymentCardNotes"] != null ? PaymentInfoTb.Rows[0]["PaymentCardNotes"].ToString() : "");
                txtBankBSB.Text = (PaymentInfoTb.Rows[0]["PaymentBSB"] != null ? PaymentInfoTb.Rows[0]["PaymentBSB"].ToString() : "");
                txtBankAccountName.Text = (PaymentInfoTb.Rows[0]["PaymentBankAccountNumber"] != null ? PaymentInfoTb.Rows[0]["PaymentBankAccountNumber"].ToString() : "");
                txtBankAccountNo.Text = (PaymentInfoTb.Rows[0]["PaymentBankAccountName"] != null ? PaymentInfoTb.Rows[0]["PaymentBankAccountName"].ToString() : "");
                txtChequeNo.Text = (PaymentInfoTb.Rows[0]["PaymentChequeNumber"] != null ? PaymentInfoTb.Rows[0]["PaymentChequeNumber"].ToString() : "");
                txtBankNotes.Text = (PaymentInfoTb.Rows[0]["PaymentBankNotes"] != null ? PaymentInfoTb.Rows[0]["PaymentBankNotes"].ToString() : "");
                txtPaymentNotes.Text = (PaymentInfoTb.Rows[0]["PaymentNotes"] != null ? PaymentInfoTb.Rows[0]["PaymentNotes"].ToString() : "");
                RecipientAccountID = (PaymentInfoTb.Rows[0]["RecipientAccountID"] != null ? PaymentInfoTb.Rows[0]["RecipientAccountID"].ToString() : "");
                txtGCNo.Text = (PaymentInfoTb.Rows[0]["PaymentGCNo"] != null ? PaymentInfoTb.Rows[0]["PaymentGCNo"].ToString() : "");
                txtGCNotes.Text = (PaymentInfoTb.Rows[0]["PaymentGCNotes"] != null ? PaymentInfoTb.Rows[0]["PaymentGCNotes"].ToString() : "");
                AmountChange.Value = ChangeAmount;
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            int x = 0;
            PaymentInfoTb.Rows.Clear();
            foreach (DataGridViewRow item in dgPaymentMethod.Rows)
            {
                if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() != "")
                {
                    
                    PaymentInfoTb.Rows.Add();
                    PaymentInfoTb.Rows[x]["RecipientAccountID"] = RecipientAccountID;
                    PaymentInfoTb.Rows[x]["AmountPaid"] = item.Cells[3].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentMethodID"] = item.Cells[0].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentMethod"] = item.Cells[2].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentAuthorisationNumber"] = item.Cells[4].Value == null ? "" : item.Cells[4].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentCardNumber"] = item.Cells[5].Value == null ? "" : item.Cells[5].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentNameOnCard"] = item.Cells[6].Value == null ? "" : item.Cells[6].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentExpirationDate"] = item.Cells[7].Value == null ? "" : item.Cells[7].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentCardNotes"] = item.Cells[8].Value == null ? "" : item.Cells[8].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBSB"] = item.Cells[9].Value == null ? "" : item.Cells[9].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBankAccountNumber"] = item.Cells[10].Value == null ? "" : item.Cells[10].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBankAccountName"] = item.Cells[11].Value == null ? "" : item.Cells[11].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentChequeNumber"] = item.Cells[12].Value == null ? "" : item.Cells[12].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentBankNotes"] = item.Cells[13].Value == null ? "" : item.Cells[13].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentNotes"] = item.Cells[14].Value == null ? "" : item.Cells[14].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentGCNo"] = item.Cells[15].Value == null ? "" : item.Cells[15].Value.ToString();
                    PaymentInfoTb.Rows[x]["PaymentGCNotes"] = item.Cells[16].Value == null ? "" : item.Cells[16].Value.ToString();
                    x++;
                }
            }
            if(AmountChange.Value != 0)
            {
                PaymentInfoTb.Rows.Add();
                PaymentInfoTb.Rows[x]["RecipientAccountID"] = RecipientAccountID;
                PaymentInfoTb.Rows[x]["AmountPaid"] = AmountChange.Value * (-1);
                PaymentInfoTb.Rows[x]["PaymentMethodID"] = ChangePaymentMethodID;
                PaymentInfoTb.Rows[x]["PaymentMethod"] = "";
                PaymentInfoTb.Rows[x]["PaymentAuthorisationNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentCardNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentNameOnCard"] = "";
                PaymentInfoTb.Rows[x]["PaymentExpirationDate"] = "";
                PaymentInfoTb.Rows[x]["PaymentCardNotes"] = "";
                PaymentInfoTb.Rows[x]["PaymentBSB"] = "";
                PaymentInfoTb.Rows[x]["PaymentBankAccountNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentBankAccountName"] = "";
                PaymentInfoTb.Rows[x]["PaymentChequeNumber"] = "";
                PaymentInfoTb.Rows[x]["PaymentBankNotes"] = "";
                PaymentInfoTb.Rows[x]["PaymentNotes"] = "";
                PaymentInfoTb.Rows[x]["PaymentGCNo"] = "";
                PaymentInfoTb.Rows[x]["PaymentGCNotes"] = "";

            }
            DialogResult = DialogResult.OK;
        }

         private void btnClear_Click(object sender, EventArgs e)
        {
            AmountPaid.Value = 0;            
            txtAuthorization.Text = "";          
            txtCardNo.Text = "";
            txtCardName.Text = "";
            txtCardExpiry.Text = "";
            txtCardNotes.Text = "";
            txtBankBSB.Text = "";
            txtBankAccountName.Text = "";
            txtBankAccountNo.Text = "";
            txtChequeNo.Text = "";
            txtBankNotes.Text = "";
            txtPaymentNotes.Text = "";
            txtGCNo.Text = "";
            txtGCNotes.Text = "";
        }

        void TenderCalc()
        {
            decimal amt = 0;
            for (int i = 0; i < this.dgPaymentMethod.Rows.Count; i++)
            {
                if (this.dgPaymentMethod.Rows[i].Cells["Amount"].Value != null)
                {
                    if (this.dgPaymentMethod.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        amt += decimal.Parse(dgPaymentMethod.Rows[i].Cells["Amount"].Value.ToString());
                    }
                }
            }
            AmountPaid.Value = amt;
            AmountChange.Value = 0;
            if (AmountPaid.Value > txtTotalAmount.Value)
            {
                AmountChange.Value = AmountPaid.Value - txtTotalAmount.Value;
            }
            BalanceDue.Value = txtTotalAmount.Value - (AmountPaid.Value - AmountChange.Value);
        }

        private void dgPaymentMethod_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colindex = (int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex);
            e.Control.KeyPress -= Numeric_KeyPress;

            if (colindex == 3)
            {
                e.Control.KeyPress += TextboxNumeric_KeyPress;
            }
            else
            {
                e.Control.KeyPress -= TextboxNumeric_KeyPress;
            }
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
            }
        }

        private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
              && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // only allow one negative char before the number
            if (e.KeyChar == '-'
                && (sender as TextBox).Text.IndexOf('-') == 0)
            {
                e.Handled = true;
            }
        }

        private void dgPaymentMethod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                TenderCalc();
            }
        }

        private void dgPaymentMethod_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        void UpdateTenderDetail(string colName , string newData)
        {
            dgPaymentMethod.CurrentRow.Cells[colName].Value = newData;
        }

        private void dgPaymentMethod_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            xMethodID = int.Parse(dgPaymentMethod.Rows[e.RowIndex].Cells[0].Value.ToString());
            switch (xMethodID)
            {
                case 5:
                case 6:
                case 10://Card
                    tabControl1.SelectedIndex = 0;
                    ((Control)this.tabCardNo).Enabled = true;
                    ((Control)this.tabBankNo).Enabled = false;
                    ((Control)this.tabNotes).Enabled = false;
                    ((Control)tabGC).Enabled = false;
                    break;
                case 7:
                case 3:
                case 12:
                case 14:
                case 15:
                case 13://Note
                    tabControl1.SelectedIndex = 3;
                    ((Control)this.tabCardNo).Enabled = false;
                    ((Control)this.tabBankNo).Enabled = false;
                    ((Control)this.tabNotes).Enabled = true;
                    ((Control)tabGC).Enabled = false;
                    break;
                case 4:
                case 9://Bank
                    tabControl1.SelectedIndex = 1;
                    ((Control)this.tabCardNo).Enabled = false;
                    ((Control)this.tabBankNo).Enabled = true;
                    ((Control)this.tabNotes).Enabled = false;
                    ((Control)tabGC).Enabled = false;
                    break;
                case 16://GC
                    tabControl1.SelectedIndex = 2;
                    ((Control)this.tabCardNo).Enabled = false;
                    ((Control)this.tabBankNo).Enabled = false;
                    ((Control)this.tabNotes).Enabled = false;
                    ((Control)tabGC).Enabled = true;
                    break;
                default:
                    ((Control)this.tabCardNo).Enabled = true;
                    ((Control)this.tabBankNo).Enabled = true;
                    ((Control)this.tabNotes).Enabled = true;
                    ((Control)tabGC).Enabled = true;
                    break;

            }
            txtAuthorization.Text =  dgPaymentMethod.Rows[e.RowIndex].Cells[4].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtCardNo.Text= dgPaymentMethod.Rows[e.RowIndex].Cells[5].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtCardName.Text =  dgPaymentMethod.Rows[e.RowIndex].Cells[6].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtCardExpiry.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[7].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtCardNotes.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[8].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtBankBSB.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[9].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtBankAccountName.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[10].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtBankAccountNo.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[11].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtChequeNo.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[12].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtBankNotes.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[13].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtPaymentNotes.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[14].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[14].Value.ToString();
            txtGCNo.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[15].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[15].Value.ToString();
            txtGCNotes.Text = dgPaymentMethod.Rows[e.RowIndex].Cells[16].Value == null ? "" : dgPaymentMethod.Rows[e.RowIndex].Cells[16].Value.ToString();
        }

        private void txtCardExpiry_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentExpirationDate", txtCardExpiry.Text);
        }

        private void txtAuthorization_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentAuthorisationNumber", txtAuthorization.Text);
        }

        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentCardNumber", txtCardNo.Text);
        }

        private void txtCardName_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentNameOnCard", txtCardName.Text);
        }

        private void txtCardNotes_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentCardNotes", txtCardNotes.Text);
        }

        private void txtGCNo_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentGCNo", txtGCNo.Text);
        }

        private void txtGCNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string sql = "SELECT * FROM GiftCertificate WHERE GCNumber=@GCNumber";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@GCNumber", txtGCNo.Text);
                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, sql, param);

                if (dt.Rows.Count >= 1)
                {
                    bool isUsed = (bool)dt.Rows[0]["IsUsed"];
                    if (isUsed)
                    {
                        MessageBox.Show("Gift Certificate is already used");
                        return;
                    }

                    DateTime gcsdate = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToLocalTime();
                    DateTime gcedate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToLocalTime();
                    DateTime today = DateTime.Now.ToLocalTime();

                    if (today >= gcsdate || today <= gcedate)
                    {
                        double gcamount = Convert.ToDouble(dt.Rows[0]["GCAmount"]);
                        if (gcamount > 0)
                        {

                            dgPaymentMethod.Rows[GCRowID].Cells["Amount"].Value = gcamount;
                            TenderCalc();
                            txtGCNotes.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Gift Certificate amount is 0");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gift Certificate is already expired");
                        return;
                    }
                } 
            }
        }

        private void txtBankBSB_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentBSB", txtBankBSB.Text);
        }

        private void txtBankAccountName_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentBankAccountName", txtBankAccountName.Text);
        }

        private void txtBankAccountNo_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentBankAccountNumber", txtBankAccountNo.Text);
        }

        private void txtChequeNo_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentChequeNumber", txtChequeNo.Text);
        }

        private void txtBankNotes_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentBankNotes", txtBankNotes.Text);
        }

        private void txtGCNotes_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentGCNotes", txtGCNotes.Text);
        }

        private void txtPaymentNotes_TextChanged(object sender, EventArgs e)
        {
            UpdateTenderDetail("PaymentNotes", txtPaymentNotes.Text);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn3.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn6.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn9.Text;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btnDot.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            dgPaymentMethod.CurrentRow.Cells["Amount"].Value += btn0.Text;
        }

        private void BackSpace_Click(object sender, EventArgs e)
        {
            if (dgPaymentMethod.CurrentRow.Cells["Amount"].Value.ToString().Length > 0 || dgPaymentMethod.CurrentRow.Cells["Amount"].Value.ToString() != "")
            {
                dgPaymentMethod.CurrentRow.Cells["Amount"].Value = dgPaymentMethod.CurrentRow.Cells["Amount"].Value.ToString().Remove(dgPaymentMethod.CurrentRow.Cells["Amount"].Value.ToString().Length - 1, 1);
            }
        }

        private void btnEndEdit_Click(object sender, EventArgs e)
        {
            this.dgPaymentMethod.CurrentCell = this.dgPaymentMethod.CurrentRow.Cells["Amount"];
            this.dgPaymentMethod.BeginEdit(true);
            //this.dgPaymentMethod.BeginEdit(false);
            this.dgPaymentMethod.EndEdit();
        }
    } // end
}
