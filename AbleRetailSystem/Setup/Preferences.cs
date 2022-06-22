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
namespace RestaurantPOS
{
    public partial class Preferences : Form
    {
        private bool CanEdit = false;
        private string thisFormCode = "";
        private string customerID="";
        DataTable dtPref;
        DataTable dtSeries;
        public Preferences()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
                outx = false;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtPref.Rows.Count > 0)
            {
                Dictionary<string, object> updateparam = new Dictionary<string, object>();
                string updatesql = @"UPDATE Preference SET LocalCurrency = (SELECT CurrencyCode 
                                    FROM Currency WHERE CurrencySymbol = @currency),
                                    IsTranEditable = @istraneditable,
                                    IsItemPriceInclusive = @IsItemPriceInclusive,
                                    KeepQuotations = @KeepQuotations ,
                                    CustomerMandatory = @CustomerMandatory,
                                    DefaultCustomerID = @DefaultCustomerID,
                                    TradeCreditorGLCode = @TradeCreditorGLCode,
                                    TradeDebtorGLCode = @TradeDebtorGLCode,
                                    SalesFreightGLCode = @SalesFreightGLCode,
                                    PurchaseFreightGLCode = @PurchaseFreightGLCode,
                                    SalesDepositGLCode = @SalesDepositGLCode,
                                    IsTaxInclusive = @IsTaxInclusive, 
                                    StandardShift = @StandardShift,
                                    AutoEndSession = @AutoEnd ";
                updateparam.Add("@currency", cmbCurSymbol.Text);
                updateparam.Add("@istraneditable", cbTranEditable.Checked);
                updateparam.Add("@IsItemPriceInclusive", chkIncItemPrice.Checked);
                updateparam.Add("@KeepQuotations", chkKeepQuote.Checked);
                updateparam.Add("@CustomerMandatory", customerMandatory.Checked);
                updateparam.Add("@IsTaxInclusive", chkIncEnterSales.Checked);
                updateparam.Add("@AutoEnd", chkAutoEndSession.Checked);

                if (ShiftTimeSpan.Text == "  :")
                    updateparam.Add("@StandardShift", "00:00");
                else
                    updateparam.Add("@StandardShift", ShiftTimeSpan.Text);

                if (!customerMandatory.Checked)
                {
                    if (customerID != "")
                        updateparam.Add("@DefaultCustomerID", customerID);
                    else
                    {
                        MessageBox.Show("Please select a default customer");
                        return;
                    }
                }
                else
                {
                    updateparam.Add("@DefaultCustomerID", 0);
                }
                updateparam.Add("@TradeDebtorGLCode", txtTradeDebtor.Text);
                updateparam.Add("@TradeCreditorGLCode", txtTradeCreditor.Text);
                updateparam.Add("@PurchaseFreightGLCode", txtPFreight.Text);
                updateparam.Add("@SalesFreightGLCode", txtSFreight.Text);
                updateparam.Add("@SalesDepositGLCode", txtSalesDepGLCode.Text);
               // updateparam.Add("@PurchasePaymentGLCode", txtPurPaymentGLCode.Text);

                int rowsaffectedpref = CommonClass.runSql(updatesql, CommonClass.RunSqlInsertMode.QUERY, updateparam);

                if (rowsaffectedpref != 0)
                {
                    string titles = "Information";
                    MessageBox.Show("Record has been Updated.", titles);
                }
            }
            else
            {
                Dictionary<string, object>insertparam = new Dictionary<string, object>();
                string cmd_insertsql = @"INSERT INTO Preference (LocalCurrency, IsTranEditable, IsItemPriceInclusive, KeepQuotations, 
                                                                 CustomerMandatory, DefaultCustomerID, TradeCreditorGLCode, TradeDebtorGLCode, 
                                                                 IsTaxInclusive, SalesDepositGLCode,StandardShift,PurchaseFreightGLCode,SalesFreightGLCode, AutoEndSession )
                                        VALUES ((SELECT CurrencyCode FROM Currency WHERE CurrencySymbol = @currency),
                                                 @istraneditable, @IsItemPriceInclusive, @KeepQuotations, @CustomerMandatory,
                                                 @DefaultCustomerID, @TradeCreditorGLCode, @TradeDebtorGLCode, @IsTaxInclusive,
                                                 @SalesDepositGLCode,@StandardShift,@PurchaseFreightGLCode, @SalesFreightGLCode, @AutoEnd)";

                insertparam.Add("@currency", cmbCurSymbol.Text);
                insertparam.Add("@istraneditable", cbTranEditable.Checked);
                insertparam.Add("@IsItemPriceInclusive", chkIncItemPrice.Checked);
                insertparam.Add("@KeepQuotations", chkKeepQuote.Checked);
                insertparam.Add("@CustomerMandatory", customerMandatory.Checked);
                insertparam.Add("@IsTaxInclusive", chkIncEnterSales.Checked);
                insertparam.Add("@PurchaseFreightGLCode", txtPFreight.Text);
                insertparam.Add("@SalesFreightGLCode", txtSFreight.Text);
                insertparam.Add("@AutoEnd", chkAutoEndSession.Checked);
               
                
                //insertparam.Add("@StandardShift", ShiftTimeSpan.Text);
                if (ShiftTimeSpan.Text == "  :")
                    insertparam.Add("@StandardShift", "00:00");
                else
                    insertparam.Add("@StandardShift", ShiftTimeSpan.Text);
              
                if (!customerMandatory.Checked)
                {
                    if (customerID != "")
                        insertparam.Add("@DefaultCustomerID", customerID);
                    else
                    {
                        MessageBox.Show("Please select a default customer");
                        return;
                    }
                }
                else
                {
                    insertparam.Add("@DefaultCustomerID", 0);
                }
                insertparam.Add("@TradeCreditorGLCode", txtTradeCreditor.Text);
                insertparam.Add("@TradeDebtorGLCode", txtTradeDebtor.Text);
                insertparam.Add("@SalesDepositGLCode", txtSalesDepGLCode.Text);
                //insertparam.Add("@PurchasePaymentGLCode", txtPurPaymentGLCode.Text);

                int rowsinsertedpref = CommonClass.runSql(cmd_insertsql, CommonClass.RunSqlInsertMode.QUERY, insertparam);

                if (rowsinsertedpref != 0)
                {
                    string titles = "Information";
                    MessageBox.Show("Record has been save.", titles);
                }
            }
                
            if (dtSeries.Rows.Count > 0)
            {
                Dictionary<string, object> updateseriesparam = new Dictionary<string, object>();
                string updttxsql = @"UPDATE TransactionSeries SET 
                                        SalesOrderSeries = @salesorderseries,
                                        SalesOrderPrefix = @salesorderprefix,
                                        SalesQuoteSeries = @salesquoteseries,
                                        SalesQuotePrefix = @salesquoteprefix,
                                        SalesInvoiceSeries = @salesinvoiceseries,
                                        SalesInvoicePrefix = @salesinvoiceprefix,
                                        PurchaseOrderSeries = @purchaseorderseries,
                                        PurchaseOrderPrefix = @purchaseorderprefix,
                                        ReceivedItemsSeries = @receiveditemsseries,
                                        ReceivedItemsPrefix = @receiveditemsprefix,
                                        BuildItemsSeries = @builditemsseries,
                                        BuildItemsPrefix = @builditemsprefix,
                                        PaymentSeries = @paymentseries,
                                        PaymentPrefix = @paymentprefix,
                                        SaleLayBySeries = @SaleLayBySeries,
                                        SaleLayByPrefix = @SaleLayByPrefix";
                updateseriesparam.Add("@salesorderseries", txtSalesOrderSeries.Text);
                updateseriesparam.Add("@salesorderprefix", txtSalesOrderPrefix.Text);
                updateseriesparam.Add("@salesquoteseries", txtSalesQuoteSeries.Text);
                updateseriesparam.Add("@salesquoteprefix", txtSalesQuotePrefix.Text);
                updateseriesparam.Add("@salesinvoiceseries", txtSalesInvoiceSeries.Text);
                updateseriesparam.Add("@salesinvoiceprefix", txtSalesInvoicePrefix.Text);
                updateseriesparam.Add("@purchaseorderseries", txtPurchaseOrderSeries.Text);
                updateseriesparam.Add("@purchaseorderprefix", txtPurchaseOrderPrefix.Text);
                updateseriesparam.Add("@receiveditemsseries", txtReceivedItemsSeries.Text);
                updateseriesparam.Add("@receiveditemsprefix", txtReceivedItemsPrefix.Text);
                updateseriesparam.Add("@builditemsseries", txtBuildItemsSeries.Text);
                updateseriesparam.Add("@builditemsprefix", txtBuildItemsPrefix.Text);
                updateseriesparam.Add("@paymentseries", txtPaymentSeries.Text);
                updateseriesparam.Add("@paymentprefix", txtPaymentPrefix.Text);
                updateseriesparam.Add("@SaleLayBySeries", txtSalesLayBySeries.Text);
                updateseriesparam.Add("@SaleLayByPrefix", txtSalesLayByPrefix.Text);

                CommonClass.runSql(updttxsql,CommonClass.RunSqlInsertMode.QUERY, updateseriesparam);
            }
            else
            {
                Dictionary<string, object> insertseriesparam = new Dictionary<string, object>();
                string inserttxsql = @"INSERT INTO TransactionSeries 
                                        (
                                        SalesOrderSeries,
                                        SalesOrderPrefix,
                                        SalesQuoteSeries,
                                        SalesQuotePrefix,
                                        SalesInvoiceSeries,
                                        SalesInvoicePrefix,
                                        PurchaseOrderSeries,
                                        PurchaseOrderPrefix,
                                        ReceivedItemsSeries,
                                        ReceivedItemsPrefix,
                                        BuildItemsSeries,
                                        BuildItemsPrefix,
                                        PaymentSeries,
                                        PaymentPrefix,
                                        SaleLayBySeries,
                                        SaleLayByPrefix
                                        )
                                        VALUES 
                                        (
                                           
                                            @salesorderseries,
                                            @salesorderprefix,
                                            @salesquoteseries,
                                            @salesquoteprefix,
                                            @salesinvoiceseries,
                                            @salesinvoiceprefix,
                                            @purchaseorderseries,
                                            @purchaseorderprefix,
                                            @receiveditemsseries,
                                            @receiveditemsprefix,
                                            @builditemsseries,
                                            @builditemsprefix,
                                            @paymentseries,
                                            @paymentprefix,
                                            @SaleLayBySeries,
                                            @SaleLayByPrefix)";
                insertseriesparam.Add("@salesorderseries", txtSalesOrderSeries.Text);
                insertseriesparam.Add("@salesorderprefix", txtSalesOrderPrefix.Text);
                insertseriesparam.Add("@salesquoteseries", txtSalesQuoteSeries.Text);
                insertseriesparam.Add("@salesquoteprefix", txtSalesQuotePrefix.Text);
                insertseriesparam.Add("@salesinvoiceseries", txtSalesInvoiceSeries.Text);
                insertseriesparam.Add("@salesinvoiceprefix", txtSalesInvoicePrefix.Text);
                insertseriesparam.Add("@purchaseorderseries", txtPurchaseOrderSeries.Text);
                insertseriesparam.Add("@purchaseorderprefix", txtPurchaseOrderPrefix.Text);
                insertseriesparam.Add("@receiveditemsseries", txtReceivedItemsSeries.Text);
                insertseriesparam.Add("@receiveditemsprefix", txtReceivedItemsPrefix.Text);
                insertseriesparam.Add("@builditemsseries", txtBuildItemsSeries.Text);
                insertseriesparam.Add("@builditemsprefix", txtBuildItemsPrefix.Text);
                insertseriesparam.Add("@paymentseries", txtPaymentSeries.Text);
                insertseriesparam.Add("@paymentprefix", txtPaymentPrefix.Text);
                insertseriesparam.Add("@SaleLayBySeries", txtSalesLayBySeries.Text);
                insertseriesparam.Add("@SaleLayByPrefix", txtSalesLayByPrefix.Text);

                CommonClass.runSql(inserttxsql, CommonClass.RunSqlInsertMode.QUERY, insertseriesparam);
            }
            CommonClass.IsEditOK = cbTranEditable.Checked;
            CommonClass.IsItemPriceInclusive = chkIncItemPrice.Checked;
            CommonClass.IsTaxcInclusiveEnterSales = chkIncEnterSales.Checked;
            CommonClass.MandatoryCustomer = customerMandatory.Checked;
            CommonClass.ShiftTimeSpan = ShiftTimeSpan.Text;
            CommonClass.DRowPref["SalesDepositGLCode"] = txtSalesDepGLCode.Text;
            CommonClass.DRowPref["PurchaseFreightGLCode"] = txtPFreight.Text;
            CommonClass.DRowPref["SalesFreightGLCode"] = txtSFreight.Text;
            CommonClass.DRowPref["TradeCreditorGLCode"] = txtTradeCreditor.Text;
            CommonClass.DRowPref["TradeDebtorGLCode"] = txtTradeDebtor.Text;
            //CommonClass.DRowPref["PurchasePaymentGLCode"] = txtPurPaymentGLCode.Text;
            CommonClass.AutoEnd = chkAutoEndSession.Checked;

            if (!customerMandatory.Checked)
            {
                if (customerID != "")
                    CommonClass.DefaultCustomerID = customerID;
            }
            //CommonClass.LCurrencyID = cmbCurSymbol.
            //CommonClass.LCurrency = dt.Rows[0]["LocalCurrency"].ToString();
            CommonClass.LCurSymbol = cmbCurSymbol.Text;
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = CanEdit;
            pbCustomer.Enabled = false;
            txtDefCustomer.Enabled = false;
            label33.Enabled = false;
            DataTable dtcur = new DataTable();
            string selectSql = "SELECT * FROM Currency";
            CommonClass.runSql(ref dtcur ,selectSql);
            if (dtcur.Rows.Count>0)
            {
                foreach (DataRow x in dtcur.Rows)
                {
                    cmbCurSymbol.Items.Add(x["CurrencySymbol"].ToString());
                }
            }
            dtPref = new DataTable();
            string selprefsql = @"SELECT p.*, 
                                         c.CurrencySymbol, 
                                         Profile.Name 
                                    FROM Preference p 
                                    LEFT JOIN Currency c ON p.LocalCurrency = c.CurrencyCode 
                                    LEFT JOIN Profile ON Profile.ID = p.DefaultCustomerID";
            CommonClass.runSql(ref dtPref, selprefsql);
               
            if (dtPref.Rows.Count > 0)
            {
                DataRow x = dtPref.Rows[0];
                cmbCurSymbol.SelectedItem = x["CurrencySymbol"];
                cbTranEditable.Checked = (bool)x["IsTranEditable"];
                chkIncItemPrice.Checked = (bool)x["IsItemPriceInclusive"];
                chkIncEnterSales.Checked = (bool)x["IsTaxInclusive"];
                chkKeepQuote.Checked = (bool)x["KeepQuotations"];
                customerMandatory.Checked = (bool)x["CustomerMandatory"];
                customerID = x["DefaultCustomerID"].ToString();
                if (!customerMandatory.Checked)
                {
                    txtDefCustomer.Text = x["Name"].ToString();
                }
                txtTradeCreditor.Text = x["TradeCreditorGLCode"].ToString();
                txtTradeDebtor.Text = x["TradeDebtorGLCode"].ToString();
                txtPFreight.Text = x["PurchaseFreightGLCode"].ToString();
                txtSFreight.Text = x["SalesFreightGLCode"].ToString();
                //txtPurPaymentGLCode.Text = x["PurchasePaymentGLCode"].ToString();
                txtSalesDepGLCode.Text = x["SalesDepositGLCode"].ToString();
                ShiftTimeSpan.Text = x["StandardShift"].ToString();
                chkAutoEndSession.Checked = (bool)x["AutoEndSession"];
                if (chkAutoEndSession.Checked)
                    ShiftTimeSpan.Enabled = true;
                else
                    ShiftTimeSpan.Enabled = false;
                if (CommonClass.SessionRunning)
                {
                    chkAutoEndSession.Enabled = false;
                    ShiftTimeSpan.Enabled = false;
                }

            }
            else
            {
                cmbCurSymbol.SelectedIndex = 0;
            }
            dtSeries = new DataTable();
            string seltxseries = "SELECT * FROM TransactionSeries";
            CommonClass.runSql(ref dtSeries,seltxseries);
            if (dtSeries.Rows.Count > 0)
            {
                DataRow s = dtSeries.Rows[0];
                txtSalesOrderSeries.Text = s["SalesOrderSeries"].ToString();
                txtSalesOrderPrefix.Text = s["SalesOrderPrefix"].ToString();
                txtSalesQuoteSeries.Text = s["SalesQuoteSeries"].ToString();
                txtSalesQuotePrefix.Text = s["SalesQuotePrefix"].ToString();
                txtSalesInvoiceSeries.Text = s["SalesInvoiceSeries"].ToString();
                txtSalesInvoicePrefix.Text = s["SalesInvoicePrefix"].ToString();
                txtSalesLayBySeries.Text = s["SaleLayBySeries"].ToString();
                txtSalesLayByPrefix.Text = s["SaleLayByPrefix"].ToString();
                txtPurchaseOrderSeries.Text = s["PurchaseOrderSeries"].ToString();
                txtPurchaseOrderPrefix.Text = s["PurchaseOrderPrefix"].ToString();
                txtReceivedItemsSeries.Text = s["ReceivedItemsSeries"].ToString();
                txtReceivedItemsPrefix.Text = s["ReceivedItemsPrefix"].ToString();
                txtBuildItemsSeries.Text = s["BuildItemsSeries"].ToString();
                txtBuildItemsPrefix.Text = s["BuildItemsPrefix"].ToString();
                txtPaymentSeries.Text = s["PaymentSeries"].ToString();
                txtPaymentPrefix.Text = s["PaymentPrefix"].ToString();
            }
        }
        
        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                customerID = lProfile[0];
                this.txtDefCustomer.Text = lProfile[2];
            }
        }

        private void customerMandatory_CheckedChanged(object sender, EventArgs e)
        {
           if( customerMandatory.Checked)
            {
                pbCustomer.Enabled = false;
                txtDefCustomer.Enabled = false;
                label33.Enabled = false;
            }
            else
            {
                pbCustomer.Enabled = true;
                txtDefCustomer.Enabled = true;
                label33.Enabled = true;
            }
        }

        private void txtTradeCreditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && ch != '-')
            {
                e.Handled = true;
            }
        }

        private void chkAutoEndSession_CheckedChanged(object sender, EventArgs e)
        {
                if (chkAutoEndSession.Checked)
                {
                    ShiftTimeSpan.Enabled = true;
                }
                else
                {
                    ShiftTimeSpan.Enabled = false;
                }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

    } //end
}
