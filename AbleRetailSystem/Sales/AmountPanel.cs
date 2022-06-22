using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Sales
{
    public partial class AmountPanel : Form
    {
        public AmountPanel(string paymentMethod, decimal pDue = 0)
        {
            InitializeComponent();
            this.txtTotalAmount.Value = pDue;
            txtPayment.Text = paymentMethod;
        }
        public string GetPayedAmount
        {
            get { return txtAmount.Text; }
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn1.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn3.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn6.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn9.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btn0.Text;
        }

        private void BackSpace_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text.Length > 0)
            {
                txtAmount.Text = txtAmount.Text.Remove(txtAmount.Text.Length - 1, 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtAmount.Text += btnDot.Text;
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
        private void AmountPanel_Load(object sender, EventArgs e)
        {
            generateImage();
        }
        public void generateImage()
        {
            if (txtPayment.Text == "Cash")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.Cash;
            }
            else if (txtPayment.Text == "Cheque")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.Cheque;
            }
            else if (txtPayment.Text == "Complimentary")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.business_and_finance;
            }
            else if (txtPayment.Text == "Credit Card")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.credit_card;
            }
            else if (txtPayment.Text == "Debit Card")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.pay;
            }
            else if (txtPayment.Text == "Direct Deposit")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.DirectDeposit;
            }
            else if (txtPayment.Text == "Eftpos")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.eftpos;
            }
            else if (txtPayment.Text == "GC")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.gift_card;
            }
            else if (txtPayment.Text == "Other")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.Others;
            }
            else if (txtPayment.Text == "Paypal")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.paypal;
            }
            else if (txtPayment.Text == "Salary Sacrifice")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.SalarySacrifice;
            }
            else if (txtPayment.Text == "Staff Deduction")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.staffDeduction;
            }
            else if (txtPayment.Text == "Voucher")
            {
                pbPaymentImage.Image = global::RestaurantPOS.Properties.Resources.coupon;
            }
            pbPaymentImage.Visible = true;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
            if (e.KeyChar == (char)13)
            {
                btnApply.PerformClick();
            }
            if (e.KeyChar == (char)48)
            {
                //txtAmount.Text += 0;
            }
            if (e.KeyChar == (char)49)
            {
                //txtAmount.Text += 1;
            }
            if (e.KeyChar == (char)50)
            {
               /// txtAmount.Text += 2;
            }
            if (e.KeyChar == (char)51)
            {
               // txtAmount.Text += 3;
            }
            if (e.KeyChar == (char)52)
            {
               // txtAmount.Text += 4;
            }
            if (e.KeyChar == (char)53)
            {
               // txtAmount.Text += 5;
            }
            if (e.KeyChar == (char)54)
            {
               // txtAmount.Text += 6;
            }
            if (e.KeyChar == (char)55)
            {
               // txtAmount.Text += 7;
            }
            if (e.KeyChar == (char)56)
            {
               // txtAmount.Text += 8;
            }
            if (e.KeyChar == (char)57)
            {
               // txtAmount.Text += 9;
            }
            if (e.KeyChar == (char)46)
            {
               // txtAmount.Text += ".";
            }

        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text.Length > 3)
            {
                txtAmount.Text = string.Format("{0:n0}", double.Parse(txtAmount.Text));
                txtAmount.SelectionStart = txtAmount.Text.Length;
            }
        }
    }
}
