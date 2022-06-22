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
using RestaurantPOS.Sales;

namespace RestaurantPOS
{
    public partial class SalesReceivePayment : Form
    {
        bool IsLoading = true;
        private string TranNo;
        private int mPaymentID = 0;
        private string CurSeries = "";
        private string CurInvoiceSeries = "";
        private string gPaymentNo = "";
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanView = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string RecipientAccountID = "";
        private Dictionary<string, string> PaymentDetails;
        private DataTable dtopensales = null;
        private decimal TotalDiscount;
        private CommonClass.InvocationSource InvokeSrc;
        private DataTable PaymentInfoTb = null;
        //private string AR_ChequeID;
        private string AmountChange = "";
        DataTable dt = new DataTable();

        public SalesReceivePayment(CommonClass.InvocationSource pInvokeSrc, string pTranNo = "")
        {
            InitializeComponent();
            InvokeSrc = pInvokeSrc;
            TranNo = pTranNo;
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                if (outx == true)
                {
                    CanView = true;
                }
                outx = false;
                FormRights.TryGetValue("Add", out outx);
                if (outx == true)
                {
                    CanAdd = true;
                }
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                if (outx == true)
                {
                    CanDelete = true;
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

            InitPaymentInfoTb();
            PaymentDetails = new Dictionary<string, string>();
            PaymentDetails.Add("AmountPaid", "0");
            PaymentDetails.Add("PaymentMethodID", "");
            PaymentDetails.Add("PaymentMethod", "");
            PaymentDetails.Add("PaymentAuthorisationNumber", "");
            PaymentDetails.Add("PaymentCardNumber", "");
            PaymentDetails.Add("PaymentNameOnCard", "");
            PaymentDetails.Add("PaymentExpirationDate", "");
            PaymentDetails.Add("PaymentCardNotes", "");
            PaymentDetails.Add("PaymentBSB", "");
            PaymentDetails.Add("PaymentBankAccountNumber", "");
            PaymentDetails.Add("PaymentBankAccountName", "");
            PaymentDetails.Add("PaymentChequeNumber", "");
            PaymentDetails.Add("PaymentBankNotes", "");
            PaymentDetails.Add("PaymentNotes", "");
            TotalReceivedAmount.ReadOnly = true;
        }
        private void InitPaymentInfoTb()
        {
            PaymentInfoTb = new DataTable();
            PaymentInfoTb.Columns.Add("RecipientAccountID", typeof(string));
            PaymentInfoTb.Columns.Add("AmountPaid", typeof(decimal));
            PaymentInfoTb.Columns.Add("PaymentMethodID", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentMethod", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentAuthorisationNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentCardNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentNameOnCard", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentExpirationDate", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentCardNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBSB", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankAccountNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankAccountName", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentChequeNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentGCNo", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentGCNotes", typeof(string));


            DataRow rw = PaymentInfoTb.NewRow();

            rw["RecipientAccountID"] = "0";
            rw["AmountPaid"] = 0;
            rw["PaymentMethodID"] = "0";
            rw["PaymentMethod"] = "";
            rw["PaymentAuthorisationNumber"] = "";
            rw["PaymentCardNumber"] = "";
            rw["PaymentNameOnCard"] = "";
            rw["PaymentExpirationDate"] = "";
            rw["PaymentCardNotes"] = "";
            rw["PaymentBSB"] = "";
            rw["PaymentBankAccountNumber"] = "";
            rw["PaymentBankAccountName"] = "";
            rw["PaymentChequeNumber"] = "";
            rw["PaymentBankNotes"] = "";
            rw["PaymentNotes"] = "";
            rw["PaymentGCNo"] = "";
            rw["PaymentGCNotes"] = "";


            PaymentInfoTb.Rows.Add(rw);
        }
        private void SalesReceivePayment_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            foreach (DataGridViewColumn column in dgridRecvPayment.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgridRecvPayment.CellFormatting += new DataGridViewCellFormattingEventHandler(dgridRecvPayment_CellFormatting);

          
            RecipientAccountID = CommonClass.DRowPref["SalesDepositGLCode"].ToString();//CommonClass.DRowLA["ReceivablesChequeID"].ToString();
           
            if (dgridRecvPayment.Rows.Count == 0)
            {
                btnRecord.Enabled = false;
                btnDelete.Enabled = false;
            }

            if (TranNo != "")
            {
                LoadFeededPayment();
            }
           
            if (TranNo == "")
            {
                
                btnDelete.Enabled = false;
            }
            IsLoading = false;
        }

      
        private void dgridRecvPayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow currentselectedrow = dgridRecvPayment.CurrentRow;
            int currentsalesid = Convert.ToInt32(currentselectedrow.Cells["SalesID"].Value);

            switch (e.ColumnIndex)
            {
                case 2: //Sales Number
                    ShowSalesInfo(currentsalesid);
                    break;
                default:
                    break;
            }

            DataGridViewRow index = dgridRecvPayment.CurrentRow;
            if (RecipientAccountID == "" || InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
            {
                index.Cells["Discount"].ReadOnly = true;
                index.Cells["AmountApplied"].ReadOnly = true;
            }
            else
            {
                try
                {
                    if (e.RowIndex == -1)
                        return;

                    index.Cells["Discount"].ReadOnly = false;
                    index.Cells["AmountApplied"].ReadOnly = false;

                    switch (e.ColumnIndex)
                    {
                        case 6: //Discount
                        case 8: //AmountApplied
                            this.dgridRecvPayment.CurrentCell = this.dgridRecvPayment.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            this.dgridRecvPayment.BeginEdit(true);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgridRecvPayment_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 5     //Amount
                || e.ColumnIndex == 6   //Discount
                || e.ColumnIndex == 7   //TotalDue
                || e.ColumnIndex == 8   //AmountApplied
                /*|| e.ColumnIndex == 9*/)
                && e.RowIndex != this.dgridRecvPayment.NewRowIndex)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string paymentno =CommonClass.runSql("SELECT PaymentNumber FROM Payment WHERE PaymentID = " + mPaymentID, CommonClass.RunSqlInsertMode.SCALAR ).ToString();
              if (btnDelete.Text == "Delete")
                {
                    DeletePaymentRecord(paymentno, mPaymentID);
                } 
        }

        //private void pbAccount_Click(object sender, EventArgs e)
        //{
        //    if (rdoAccount.Checked == true)
        //    {
        //        //ShowDepositToAccountLookup();
        //    }
        //}

        //private void rdoAccount_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdoAccount.Checked == true)
        //    {
        //        //if (this.lblRecipientAccountID.Text == "")
        //        //{
        //        //    ShowDepositToAccountLookup();
        //        //}
        //    }
        //}

        //private void rdoUFunds_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.rdoUFunds.Checked)
        //    {
        //        setUFBank();
        //    }
        //}

        private void pbProfile_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {

                string[] lProfile = ProfileDlg.GetProfile;
                this.lblProfileID.Text = lProfile[0];
                this.lblProfileName.Text = lProfile[2];
                this.Memo.Text = "Payment; " + lProfile[2] ;
                dgridRecvPayment.Rows.Clear();
                LoadTransaction(Int32.Parse(lProfile[0]));
                InitPaymentInfoTb();
            }
        }

        private void txtTotalPaid_TextChanged(object sender, EventArgs e)
        {
            CalcOutOfBalance();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if(PaymentInfoTb.Rows[0]["PaymentMethodID"].ToString() != "0")
            {
                if (dgridRecvPayment.Rows.Count > 0)
                {
                    int paymentid = RecordPayment();
                }
            }
            else
            {
                MessageBox.Show("Please enter payment details.");
            }
           
        }

        void CalcOutOfBalance()
        {
            this.TotalApplied.Value = 0;
            this.TotalAmt.Value = 0;
            decimal outnum = 0;

            for (int i = 0; i < this.dgridRecvPayment.Rows.Count; i++)
            {
                if (this.dgridRecvPayment.Rows[i].Cells["TotalDue"].Value != null
                    && this.dgridRecvPayment.Rows[i].Cells["TotalDue"].Value.ToString() != "")
                {
                    Decimal.TryParse(this.dgridRecvPayment.Rows[i].Cells["TotalDue"].Value.ToString(), out outnum);
                    TotalAmt.Value += outnum;
                    outnum = 0;
                    if (dgridRecvPayment.Rows[i].Cells["AmountApplied"].Value != null)
                        Decimal.TryParse(dgridRecvPayment.Rows[i].Cells["AmountApplied"].Value.ToString(), out outnum);

                    TotalApplied.Value += outnum;
                }
            }

            this.TotalPaid.Value = this.TotalReceivedAmount.Value;
            OutOfBalance.Value = this.TotalReceivedAmount.Value - TotalApplied.Value;
        }

        private void Recalcline(int pColIndex, int pRowIndex)
        {
            if (pRowIndex < 0)
                return;

            if (!IsLoading)
            {
                DataGridViewRow dgvRows = dgridRecvPayment.Rows[pRowIndex];

                decimal lTotalDue = 0;
                decimal lDiscount = 0;
                decimal lAmount = 0;
                decimal lCredits = 0;
                decimal lTotalPaid = 0;

                if (pColIndex == 6) //Discount
                {
                    if (dgvRows.Cells["Amount"].Value != null)
                    {
                        if (dgvRows.Cells["Discount"].Value != null
                            && dgvRows.Cells["Discount"].Value.ToString() != "")
                            lDiscount = decimal.Parse(dgvRows.Cells["Discount"].Value.ToString(), NumberStyles.Currency);
                        if (dgvRows.Cells["Amount"].Value.ToString() != "")
                            lAmount = decimal.Parse(dgvRows.Cells["Amount"].Value.ToString(), NumberStyles.Currency);
                        if (dgvRows.Cells["TotalSettled"].Value.ToString() != "")
                            lTotalPaid = decimal.Parse(dgvRows.Cells["TotalSettled"].Value.ToString(), NumberStyles.Currency);

                        lTotalDue = lAmount - lTotalPaid - lDiscount - lCredits;
                        dgvRows.Cells["TotalDue"].Value = lTotalDue;
                    }
                }
            }
        }

        private void ShowSalesInfo(int pSalesID)
        {
            if(CommonClass.EnterSalesfrm == null
                || CommonClass.EnterSalesfrm.IsDisposed)
            {
                CommonClass.EnterSalesfrm = new Sales.EnterSales(CommonClass.InvocationSource.REGISTER, pSalesID.ToString());
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.EnterSalesfrm.MdiParent = this.MdiParent;
            CommonClass.EnterSalesfrm.Show();
            CommonClass.EnterSalesfrm.Focus();
            if (CommonClass.EnterSalesfrm.DialogResult == DialogResult.Cancel)
            {
                CommonClass.EnterSalesfrm.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void TotalReceivedAmount_ValueChanged(object sender, EventArgs e)
        {
            CalcOutOfBalance();
        }

        private void dgridRecvPayment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6     //Discount
                || e.ColumnIndex == 8  //AmountApplied
                /*|| e.ColumnIndex == 9*/)
            {
                Recalcline(e.ColumnIndex, e.RowIndex);
                CalcOutOfBalance();
            }
        }

        private int RecordPayment()
        {
            int lPaymentID = 0;
            double d = (double)this.OutOfBalance.Value;
            if (d != 0)
            {
                MessageBox.Show("Transaction is Out of Balance.", "Critical Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                PaymentCommon.GeneratePaymentNumber(ref CurSeries, ref gPaymentNo,"SP");

                if (gPaymentNo != "")
                {
                    DateTime time = DateTime.Now;
                    DateTime dtpfromutc = dtpdate.Value.ToUniversalTime();
                    DateTime timeutc = time.ToUniversalTime();

                    string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                    lPaymentID = CreatePaymentRecord(gPaymentNo);
                    PaymentCommon.UpdatePaymentNumber(ref CurSeries, "SP");
                    if (PaymentCommon.CreateJournalEntriesSP(lPaymentID, Memo.Text))
                    {
                        TransactionClass.UpdateProfileBalances(lblProfileID.Text, TotalApplied.Value * -1);
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Payment Transaction No. " + gPaymentNo, lPaymentID.ToString());
                        MessageBox.Show("Payment record created successfully", "Payment entry information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if(AmountChange!= "")
                        {
                            if (Convert.ToDecimal(AmountChange) > 0)
                            {
                                ChangeAmount dispChange = new ChangeAmount(AmountChange);
                                dispChange.ShowDialog();
                            }
                        }
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else if (gPaymentNo == "")
                {
                    MessageBox.Show("Invalid Payment Number");
                }
            }
            return lPaymentID;
        }

        private string GenerateDiscountInvoiceNum()
        {
            string invoicenum = "";
            DataTable dt = new DataTable();
            string sql = "SELECT SalesInvoiceSeries, SalesInvoicePrefix FROM TransactionSeries";
               
            CommonClass.runSql(ref dt, sql);
            string lSeries = "";
            int lCnt = 0;
            int lNewSeries = 0;

            if (dt.Rows.Count >0)
            {
                DataRow x = dt.Rows[0];
                lSeries = (x["SalesInvoiceSeries"].ToString());
                lCnt = lSeries.Length;
                lSeries = lSeries.TrimStart('0');
                lSeries = (lSeries == "" ? "0" : lSeries);
                lNewSeries = Convert.ToInt16(lSeries) + 1;
                CurInvoiceSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                invoicenum = (x["SalesInvoicePrefix"].ToString()).Trim(' ') + CurInvoiceSeries;
            }
            else
            {
                MessageBox.Show("Transaction Series Numbers not setup properly.");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            return invoicenum; 
        }

        private bool CreateDiscountJournalEntries(int pID, string pInvoiceNum, decimal pDiscountApplied)
        {
            string sql = "";

            DataTable ltb = Sales.EnterSales.GetSalesLines(pID);
            if (ltb.Rows.Count > 0)
            {
                decimal lGrandTotal = Convert.ToDecimal(ltb.Rows[0]["GrandTotal"].ToString());
                //string lRecipientID = RecipientAccountID;
                string lSalesNum = pInvoiceNum;
                string lMemo = ltb.Rows[0]["Memo"].ToString();
                string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");

                //INSERT JOURNAL FOR Total Amount Received
                if (pDiscountApplied < 0)
                {
                    //NEGATIVE SO CREDIT AMOUNT
                    sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type) " +
                            "VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + RecipientAccountID + ", " +
                            (pDiscountApplied * -1).ToString() + ",'" + lSalesNum + "', 'SI')";
                }
                else
                {
                    //DEBIT AMOUNT
                    sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type) " +
                            "VALUES('" + lTranDate + "','" + lMemo + "','" + lMemo + "'," + RecipientAccountID + ", " +
                            pDiscountApplied + ",'" + lSalesNum + "','SI')";
                }

                CommonClass.runSql(sql);

                for (int i = 0; i < ltb.Rows.Count; i++)
                {
                    string lAccountID = ltb.Rows[i]["EntityID"].ToString();
                    decimal lTaxEx = pDiscountApplied;
                    decimal lTaxInc = pDiscountApplied;
                    decimal lTaxAmt = lTaxInc - lTaxEx;
                    string lTaxCollectedAccountID = ltb.Rows[i]["TaxCollectedAccountID"].ToString();
                    string lJobID = "0";
                    if (ltb.Rows[i]["JobID"] != null)
                        lJobID = (ltb.Rows[i]["JobID"].ToString() == "" ? "0" : ltb.Rows[i]["JobID"].ToString());

                    if (lTaxEx < 0) // NEGATIVE SO DEBIT AMOUNT 
                    {
                        // NEGATIVE SO DEBIT AMOUNT 
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID) VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lAccountID + ", " + (lTaxEx * -1) + ", '" + lSalesNum + "', 'SI', " + lJobID + ")";
                        CommonClass.runSql(sql);
                        //THIS IS FOR THE TAX COMPONENT
                        if (lTaxAmt != 0)
                        {
                            lTaxAmt = lTaxEx - lTaxInc;
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID)";
                            sql += " VALUES('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lTaxCollectedAccountID + ", " + lTaxAmt.ToString() + ", '" + lSalesNum + "', 'SI', " + lJobID + ")";
                            CommonClass.runSql(sql);
                        }
                    }
                    else //POSITIVE SO CREDIT AMOUNT 
                    {

                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID) VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lAccountID + ", " + lTaxEx + ", '" + lSalesNum + "', 'SI', " + lJobID + ")";
                        CommonClass.runSql(sql);
                        //THIS IS FOR THE TAX COMPONENT
                        if (lTaxAmt != 0 && lTaxCollectedAccountID != "")
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID) VALUES ('" + lTranDate + "', '" + lMemo + "', '" + lMemo + "', " + lTaxCollectedAccountID + ", " + lTaxAmt.ToString() + ", '" + lSalesNum + "', 'SI', " + lJobID + ")";
                            CommonClass.runSql(sql);
                        }
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("There was an error creating the transaction. No Sales Lines found.");
                return false;
            }        
        }

        void CreateDiscountSaleInvoice(PaymentCommon.DiscountFields pDiscountParams)
        {
            string invoiceno = GenerateDiscountInvoiceNum();
            Dictionary<string, object> paramsave = new Dictionary<string, object>();
            string savesql = @"INSERT INTO Sales (
                                    SalesType,
                                    CustomerID,
                                    UserID,
                                    SalesNumber,
                                    TransactionDate,
                                    PromiseDate,
                                    SubTotal,
                                    TaxTotal,
                                    Memo,
                                    LayoutType,
                                    GrandTotal,
                                    InvoiceStatus,
                                    Comments, 
                                    IsTaxInclusive, 
                                    TotalPaid,
                                    TotalDue,
                                    TermsReferenceID,
                                    ShippingContactID,
                                    ShippingMethodID,
                                    ClosedDate) 
                                VALUES
                                    ( 'INVOICE',
                                    @CustomerID,
                                    @UserID,
                                    @SalesNum,
                                    @TransactionDate,
                                    @PromiseDate,
                                    @SubTotal,
                                    @TaxTotal,
                                    @Memo, 
                                    'Service',
                                    @GrandTotal,
                                    'Closed',
                                    'Negative Invoice for discount',
                                    'Y',
                                    @TotalPaid,
                                    @TotalDue,
                                    @TermsReferenceID,
                                    @ShippingContactID,
                                    @ShippingMethodID,
                                    getutcdate() )"; 

            //Sales Data
            DateTime time = DateTime.Now;
            DateTime dtpfromutc = dtpdate.Value.ToUniversalTime();
            DateTime timeutc = time.ToUniversalTime();

            string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

            paramsave.Add("@CustomerID", lblProfileID.Text);
            paramsave.Add("@UserID", CommonClass.UserID);
            paramsave.Add("@SalesNum", invoiceno);
            paramsave.Add("@TransactionDate", trandate);
            paramsave.Add("@PromiseDate", trandate);
            paramsave.Add("@SubTotal", pDiscountParams.Discount * -1);
            paramsave.Add("@TaxTotal", 0);
            paramsave.Add("@TotalPaid", pDiscountParams.Discount * -1);
            paramsave.Add("@TotalDue", 0);
            paramsave.Add("@Memo", "Discount - " + pDiscountParams.Memo);
            paramsave.Add("@GrandTotal", pDiscountParams.Discount * -1);
            paramsave.Add("@TermsReferenceID", pDiscountParams.TermsReferenceID);
            paramsave.Add("@ShippingContactID", pDiscountParams.ShippingContactID);
            paramsave.Add("@ShippingMethodID", pDiscountParams.ShippingMethodID);
            int NewSalesID = CommonClass.runSql(savesql, CommonClass.RunSqlInsertMode.SCALAR, paramsave);

            //SalesLines
            Dictionary<string, object> paramLines = new Dictionary<string, object>();
            string salesLinesql = @"INSERT INTO SalesLines (
                                            SalesID,
                                            Description,
                                            TotalAmount,
                                            TransactionDate,
                                            TaxCode,
                                            UnitPrice, 
                                            ActualUnitPrice, 
                                            OrderQty,
                                            ShipQty,
                                            EntityID, 
                                            SubTotal,
                                            TaxAmount,
                                            TaxCollectedAccountID,
                                            JobID ) 
                                        VALUES ( @SalesID,
                                                @Description,
                                                @TotalAmount,
                                                @TransactionDate,
                                                @TaxCode,
                                                @UnitPrice,
                                                @ActualUnitPrice,
                                                @OrderQty,
                                                @ShipQty,
                                                @EntityID,
                                                @SubTotal,
                                                @TaxAmount,
                                                @TaxCollectedAccountID,
                                                @JobID )";

              
            paramLines.Add("@SalesID", NewSalesID);
            paramLines.Add("@Description", "Discount - " + Memo.Text);
            paramLines.Add("@TotalAmount", pDiscountParams.Discount * -1);
            paramLines.Add("@TaxCode", "N-T");
            paramLines.Add("@UnitPrice", pDiscountParams.Discount * -1);
            paramLines.Add("@ActualUnitPrice", pDiscountParams.Discount * -1);
            paramLines.Add("@OrderQty", 1);
            paramLines.Add("@ShipQty", 1);
            paramLines.Add("@EntityID", RecipientAccountID);
            paramLines.Add("@TaxAmount", 0);
            paramLines.Add("@TaxCollectedAccountID", "0");
            paramLines.Add("@JobID", 0);

            int rowcount = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.SCALAR, paramLines);

            if (rowcount > 0)
            {
                CommonClass.runSql("UPDATE TransactionSeries SET SalesInvoiceSeries = '" + CurInvoiceSeries + "'");

                CreateDiscountJournalEntries(NewSalesID, invoiceno, pDiscountParams.Discount * -1);
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

        private void dgridRecvPayment_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= TextboxNumeric_KeyPress;
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 5)
            {
                e.Control.KeyPress += TextboxNumeric_KeyPress;
            }
        }

        private void btnPaymentDetails_Click(object sender, EventArgs e)
        {
           
            TenderDetails TenderDlg = new TenderDetails(PaymentInfoTb, this.TotalApplied.Value, CanAdd);
            if (TenderDlg.ShowDialog() == DialogResult.OK)
            {
                PaymentInfoTb = TenderDlg.GetPaymentInfo;
                this.TotalReceivedAmount.Value = TenderDlg.GetPayedAmount;
                AmountChange = TenderDlg.GetChangemount;
            }
           
        }

     

        private void LoadFeededPayment()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                //GET THE HEADER 
                string sql = @"SELECT s.*, 
                                  p.ProfileIDNumber, 
                                  p.Name,
                                  p.CurrentBalance,
                                  p.MethodOfPaymentID as PaymentMethodID,
                                  pmtds.PaymentMethod";
                if (RecipientAccountID != "")
                    sql += ", " + RecipientAccountID + " AS AccountID ";
                if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                    sql += @", pts.Memo AS PaymentMemo,
                               pts.PaymentNumber, 
                               pts.TransactionDate AS PaymentDate, 
                               pl.Amount AS AmountApplied ";

                    sql += @"FROM Sales s 
                              INNER JOIN Profile p ON s.CustomerID = p.ID
                              LEFT JOIN PaymentMethods pmtds ON p.MethodOfPaymentID = pmtds.id
                              ";
                if (InvokeSrc == CommonClass.InvocationSource.REGISTER)
                {
                    sql += " WHERE s.SalesID = " + Convert.ToInt64(TranNo);
                }
                else if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                {
                    sql += @" INNER JOIN PaymentLines pl ON pl.EntityID = s.SalesID 
                            INNER JOIN Payment pts ON pts.PaymentID = pl.PaymentID
                            WHERE pl.PaymentID = " + Convert.ToInt64(TranNo);

                    btnRecord.Enabled = false;
                    TotalReceivedAmount.Enabled = false;
                }

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dtopensales = new DataTable();
                da.Fill(dtopensales);

                bool isTherePaymentNotYetFull = false;

                if (dtopensales.Rows.Count > 0)
                {
                    if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                    {
                        btnPrint.Visible = true;
                        btnDelete.Enabled = CanDelete;
                    }

                    int salesid = (int)dtopensales.Rows[0]["SalesID"];
                    //PaymentMethod.Text = dtopensales.Rows[0]["PaymentMethod"].ToString();
                    PaymentDetails["PaymentMethodID"] = dtopensales.Rows[0]["PaymentMethodID"].ToString();
                    //PaymentDetails["PaymentMethod"] = PaymentMethod.Text;

                    if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                    {
                        Memo.Text = dtopensales.Rows[0]["PaymentMemo"].ToString();
                        lblPaymentNo.Text = dtopensales.Rows[0]["PaymentNumber"].ToString();
                        lblPaymentNo.Visible = true;
                        label5.Visible = true;
                    }
                    else
                    {
                        Memo.Text = "Payment, " + dtopensales.Rows[0]["Memo"].ToString();
                    }

                    lblProfileName.Text = dtopensales.Rows[0]["Name"].ToString();
                    lblProfileID.Text = dtopensales.Rows[0]["CustomerID"].ToString();
                    if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                        dtpdate.Value = Convert.ToDateTime(dtopensales.Rows[0]["PaymentDate"].ToString()).ToLocalTime();
                    else
                        dtpdate.Value = DateTime.Now;

                    foreach (DataRow dtRow in dtopensales.Rows)
                    {
                        int newrowindex = dgridRecvPayment.Rows.Add();
                        //the ff. fields are hidden only used to pull back the data when creating a negative invoice for discounts
                        dgridRecvPayment["SalesTermsReferenceID", newrowindex].Value = dtRow["TermsReferenceID"];
                        dgridRecvPayment["SalesShippingContactID", newrowindex].Value = dtRow["ShippingContactID"];
                        dgridRecvPayment["SalesShippingMethodID", newrowindex].Value = dtRow["ShippingMethodID"];
                        dgridRecvPayment["SalesMemo", newrowindex].Value = dtRow["Memo"];
                        dgridRecvPayment["SalesCustomerPONumber", newrowindex].Value = dtRow["CustomerPONumber"];
                        dgridRecvPayment["SalesReference", newrowindex].Value = dtRow["SalesReference"];

                        dgridRecvPayment["SalesID", newrowindex].Value = dtRow["SalesID"];
                        dgridRecvPayment["AccountID", newrowindex].Value = RecipientAccountID;
                        dgridRecvPayment["InvoiceNo", newrowindex].Value = dtRow["SalesNumber"];
                        dgridRecvPayment["Status", newrowindex].Value = dtRow["InvoiceStatus"];
                        dgridRecvPayment["Date", newrowindex].Value = dtRow["TransactionDate"].ToString();
                        dgridRecvPayment["TotalSettled", newrowindex].Value = dtRow["TotalPaid"];

                        decimal totalpaid = (dtRow["TotalPaid"] != null) ? Convert.ToDecimal(dtRow["TotalPaid"]) : 0;

                        //if (totalpaid > 0 && InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                        //{
                        //    dgridRecvPayment["AmountApplied", newrowindex].Value = totalpaid;
                        //}
                        if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                        {
                            decimal lAmountApplied = (dtRow["AmountApplied"] != null) ? Convert.ToDecimal(dtRow["AmountApplied"]) : 0;
                            dgridRecvPayment["AmountApplied", newrowindex].Value = lAmountApplied;
                        }

                        decimal grandtotal = Convert.ToDecimal(dtRow["GrandTotal"]);
                        decimal totaldue = grandtotal - totalpaid;

                        if (InvokeSrc == CommonClass.InvocationSource.REGISTER
                            || InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                        {
                            dgridRecvPayment["Amount", newrowindex].Value = grandtotal;
                            dgridRecvPayment["TotalDue", newrowindex].Value = totaldue;
                        }
                        
                        if (totaldue != 0)
                            isTherePaymentNotYetFull = true;
                    }
                }

                int paymentid = 0;
                if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER)
                {
                    paymentid = Convert.ToInt32(TranNo);
                }
                else if (InvokeSrc == CommonClass.InvocationSource.REGISTER)
                {
                    string pmtidsql = "SELECT PaymentID FROM PaymentLines WHERE EntityID = " + Convert.ToInt64(TranNo);
                    SqlCommand cmdchk = new SqlCommand(pmtidsql, con);
                    con.Open();
                    object ret = cmdchk.ExecuteScalar();
                    paymentid = ret != null ? Convert.ToInt32(ret) : 0;
                }               

                if (InvokeSrc == CommonClass.InvocationSource.CUSTOMER
                    || InvokeSrc == CommonClass.InvocationSource.REGISTER)
                {                  
                    if (mPaymentID != 0)
                    {
                        //if (isTherePaymentNotYetFull)
                        //    btnRecord.Enabled = true;
                        //else
                            btnRecord.Enabled = false;
                            btnDelete.Text = "Delete";
                            this.btnDelete.Enabled = CanDelete;
                            this.dgridRecvPayment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
                    }
                    else
                    {
                        btnRecord.Enabled = CanEdit;
                        btnDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void LoadTransaction(int pCustomerID = 0)
        {

                //GET THE HEADER 
                string sql = @"SELECT s.*, 
                                  p.ProfileIDNumber, 
                                  p.Name,
                                  p.CurrentBalance,
                                  p.MethodOfPaymentID as PaymentMethodID,
                                  pmtds.PaymentMethod ";
                       if (RecipientAccountID != "")
                           sql += ", " + RecipientAccountID + " AS AccountID ";
                       sql += @"FROM Sales s 
                              INNER JOIN Profile p ON s.CustomerID = p.ID
                              LEFT JOIN PaymentMethods pmtds ON p.MethodOfPaymentID = pmtds.id
                              WHERE s.SalesType IN ('INVOICE', 'SINVOICE',  'ORDER', 'LAY-BY')
                              AND s.InvoiceStatus in ('Open', 'Order', 'Lay-by') 
                              AND s.SalesNumber <> 'RECURRING'
                              AND s.CustomerID = " + pCustomerID;


                dtopensales = new DataTable();
                CommonClass.runSql(ref dtopensales, sql);

                bool isTherePaymentNotYetFull = false;

                if (dtopensales.Rows.Count > 0)
                {
                    int salesid = (int)dtopensales.Rows[0]["SalesID"];
                    //PaymentMethod.Text = dtopensales.Rows[0]["PaymentMethod"].ToString();
                    //PaymentDetails["PaymentMethod"] = PaymentMethod.Text;
                    PaymentDetails["PaymentMethodID"] = dtopensales.Rows[0]["PaymentMethodID"].ToString();
                    GetPaymentMethodName(dtopensales.Rows[0]["PaymentMethodID"].ToString());
                    //Memo.Text = dtopensales.Rows[0]["Memo"].ToString();
                    dtpdate.Value = DateTime.Now;

                    foreach (DataRow dtRow in dtopensales.Rows)
                    {
                        int newrowindex = dgridRecvPayment.Rows.Add();
                        dgridRecvPayment["SalesID", newrowindex].Value = dtRow["SalesID"];
                        dgridRecvPayment["AccountID", newrowindex].Value = RecipientAccountID;
                        dgridRecvPayment["InvoiceNo", newrowindex].Value = dtRow["SalesNumber"];
                        dgridRecvPayment["Status", newrowindex].Value = dtRow["InvoiceStatus"];
                        dgridRecvPayment["Date", newrowindex].Value = Convert.ToDateTime(dtRow["TransactionDate"]).ToLocalTime().ToString();
                        dgridRecvPayment["Amount", newrowindex].Value = dtRow["GrandTotal"];
                        dgridRecvPayment["TotalSettled", newrowindex].Value = dtRow["TotalPaid"];
                        //the ff. fields are hidden only used to pull back the data when creating a negative invoice for discounts
                        dgridRecvPayment["SalesTermsReferenceID", newrowindex].Value = dtRow["TermsReferenceID"];
                        dgridRecvPayment["SalesShippingContactID", newrowindex].Value = dtRow["ShippingContactID"];
                        dgridRecvPayment["SalesShippingMethodID", newrowindex].Value = dtRow["ShippingMethodID"];
                        dgridRecvPayment["SalesMemo", newrowindex].Value = dtRow["Memo"];
                        dgridRecvPayment["SalesCustomerPONumber", newrowindex].Value = dtRow["CustomerPONumber"];
                        dgridRecvPayment["SalesReference", newrowindex].Value = dtRow["SalesReference"];

                        decimal discount = 0;

                        if (dgridRecvPayment["Discount", newrowindex].Value != null
                            && dgridRecvPayment["Discount", newrowindex].Value.ToString() != "")
                        {
                            discount = Convert.ToDecimal(dgridRecvPayment["Discount", newrowindex].Value.ToString());
                        }
                        decimal totalpaid = (dtRow["TotalPaid"] != null) ? Convert.ToDecimal(dtRow["TotalPaid"]) : 0;

                        decimal grandtotal = Convert.ToDecimal(dtRow["GrandTotal"]);
                        decimal totaldue = grandtotal - totalpaid;

                        dgridRecvPayment["TotalDue", newrowindex].Value = totaldue - discount;

                        if (totaldue != 0)
                            isTherePaymentNotYetFull = true;
                    }
                }

            //SqlCommand cmdchk = new SqlCommand("SELECT TOP 1 PaymentID FROM Payment WHERE ProfileID = " + pCustomerID + " ORDER BY PaymentID DESC", con);
            //object ret = cmdchk.ExecuteScalar();
            //int paymentid = ret != null ? Convert.ToInt32(ret) : 0;
            int paymentid= CommonClass.runSql("SELECT TOP 1 PaymentID FROM Payment WHERE ProfileID = " + pCustomerID + " ORDER BY PaymentID DESC", CommonClass.RunSqlInsertMode.SCALAR);

                if (mPaymentID != 0)
                {
                    if (isTherePaymentNotYetFull)
                        btnRecord.Enabled = CanAdd;
                    else
                        btnRecord.Enabled = false;

                    btnDelete.Enabled = false;
                }
                else
                {
                    btnRecord.Enabled = CanAdd;
                    btnDelete.Enabled = false;
                }
        }

        private int CreatePaymentRecord(string pPaymentNo)
        {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = dtpdate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();
                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");
                Dictionary<string, object> paramPayment = new Dictionary<string, object>();
                string strpaymentsql = @"INSERT INTO Payment (
                                            ProfileID,
                                            TotalAmount,
                                            Memo,
                                            PaymentFor,
                                            AccountID,
                                            UserID,
                                            PaymentMethodID,
                                            TransactionDate,
                                            PaymentNumber,
                                            SessionID,
                                            Source
                                            )
                                        VALUES ( 
                                            @ProfileID, 
                                            @TotalAmount, 
                                            @Memo, 
                                            'Sales',
                                            @AccountID,
                                            @UserID,
                                            @PaymentMethodID,
                                            @TransactionDate,
                                            @PaymentNumber,
                                            @SessionID,
                                            'P'
                                             )";
            
                paramPayment.Add("@ProfileID", lblProfileID.Text);
                paramPayment.Add("@PaymentNumber", pPaymentNo);
                paramPayment.Add("@TotalAmount", TotalPaid.Value);
                paramPayment.Add("@Memo", Memo.Text);
                paramPayment.Add("@AccountID", RecipientAccountID);
                paramPayment.Add("@UserID", CommonClass.UserID);
                paramPayment.Add("@PaymentMethodID", PaymentInfoTb.Rows[0]["PaymentMethodID"]);          
                paramPayment.Add("@TransactionDate", trandate);
                paramPayment.Add("@SessionID", CommonClass.SessionID);


            string outx = "";             

                //PaymentDetails.TryGetValue("PaymentAuthorisationNumber", out outx);
                //cmd.Parameters.AddWithValue("@PaymentAuthorisationNumber", outx);

                //PaymentDetails.TryGetValue("PaymentCardNumber", out outx);
                //cmd.Parameters.AddWithValue("@PaymentCardNumber", outx);

                //PaymentDetails.TryGetValue("PaymentNameOnCard", out outx);
                //cmd.Parameters.AddWithValue("@PaymentNameOnCard", outx);

                //PaymentDetails.TryGetValue("PaymentExpirationDate", out outx);
                //cmd.Parameters.AddWithValue("@PaymentExpirationDate", outx);

                //PaymentDetails.TryGetValue("PaymentCardNotes", out outx);
                //cmd.Parameters.AddWithValue("@PaymentCardNotes", outx);

                //PaymentDetails.TryGetValue("PaymentBSB", out outx);
                //cmd.Parameters.AddWithValue("@PaymentBSB", outx);

                //PaymentDetails.TryGetValue("PaymentBankAccountNumber", out outx);
                //cmd.Parameters.AddWithValue("@PaymentBankAccountNumber", outx);

                //PaymentDetails.TryGetValue("PaymentBankAccountName", out outx);
                //cmd.Parameters.AddWithValue("@PaymentBankAccountName", outx);

                //PaymentDetails.TryGetValue("PaymentChequeNumber", out outx);
                //cmd.Parameters.AddWithValue("@PaymentChequeNumber", outx);

                //PaymentDetails.TryGetValue("PaymentBankNotes", out outx);
                //cmd.Parameters.AddWithValue("@PaymentBankNotes", outx);

                //PaymentDetails.TryGetValue("PaymentNotes", out outx);
                //cmd.Parameters.AddWithValue("@PaymentNotes", outx);

                int paymentid = CommonClass.runSql(strpaymentsql, CommonClass.RunSqlInsertMode.SCALAR, paramPayment);
                TotalDiscount = 0;

                if (paymentid != 0)
                {
                    for (int i = 0; i < dgridRecvPayment.Rows.Count; i++)
                    {
                        if (dgridRecvPayment.Rows[i].Cells["AmountApplied"].Value != null
                            && dgridRecvPayment.Rows[i].Cells["AmountApplied"].Value.ToString() != "")
                        {
                            Dictionary<string, object> paramPayLines = new Dictionary<string, object>();
                            string Amount = (dgridRecvPayment.Rows[i].Cells["AmountApplied"].Value != null ? dgridRecvPayment.Rows[i].Cells["AmountApplied"].Value.ToString() : "0");
                            decimal deciamt = decimal.Parse(Amount, NumberStyles.Currency);
                            int SalesID = dgridRecvPayment.Rows[i].Cells["SalesID"].Value != null ? Convert.ToInt16(dgridRecvPayment.Rows[i].Cells["SalesID"].Value) : 0;
                            string strpaymentlnesql = @"INSERT INTO PaymentLines (
                                                          PaymentID,
                                                          EntityID,
                                                          Amount,
                                                          EntryDate )
                                                      VALUES (
                                                          @PaymentID,
                                                          @EntityID,
                                                          @Amount,
                                                          @EntryDate )";
                         
                            paramPayLines.Add("@PaymentID", paymentid);
                            paramPayLines.Add("@EntityID", SalesID);
                            paramPayLines.Add("@Amount", deciamt);
                            paramPayLines.Add("@EntryDate", trandate);
                            CommonClass.runSql(strpaymentlnesql, CommonClass.RunSqlInsertMode.QUERY, paramPayLines);
                            if(TotalApplied.Value > TotalAmt.Value)
                            {
                                decimal changeamt = TotalAmt.Value - TotalApplied.Value;
                                strpaymentlnesql = @"INSERT INTO PaymentLines (
                                                          PaymentID,
                                                          EntityID,
                                                          Amount,
                                                          EntryDate )
                                                      VALUES (
                                                          @PaymentID,
                                                          @EntityID,
                                                          @ChangeAmount,
                                                          @EntryDate )";
      
                                paramPayLines.Add("@ChangeAmount", changeamt);
                                CommonClass.runSql(strpaymentlnesql, CommonClass.RunSqlInsertMode.QUERY, paramPayLines);
                            }

                            string strDiscount = dgridRecvPayment.Rows[i].Cells["Discount"].Value != null ? dgridRecvPayment.Rows[i].Cells["Discount"].Value.ToString() : "0";
                            decimal lDiscount = strDiscount != "" ? Decimal.Parse(strDiscount, NumberStyles.Currency) : 0;

                            if (lDiscount > 0)
                            {
                                TotalDiscount += lDiscount;
                                PaymentCommon.DiscountFields discountparams = new PaymentCommon.DiscountFields();
                                discountparams.TermsReferenceID = dgridRecvPayment.Rows[i].Cells["SalesTermsReferenceID"].Value != null ? Convert.ToInt32(dgridRecvPayment.Rows[i].Cells["SalesTermsReferenceID"].Value) : 0;
                                discountparams.ShippingContactID = dgridRecvPayment.Rows[i].Cells["SalesShippingContactID"].Value != null ? Convert.ToInt32(dgridRecvPayment.Rows[i].Cells["SalesShippingContactID"].Value) : 0;
                                discountparams.ShippingMethodID = dgridRecvPayment.Rows[i].Cells["SalesShippingMethodID"].Value != null ? Convert.ToInt32(dgridRecvPayment.Rows[i].Cells["SalesShippingMethodID"].Value) : 0;
                                discountparams.SalesReference = dgridRecvPayment.Rows[i].Cells["SalesReference"].Value != null ? dgridRecvPayment.Rows[i].Cells["SalesReference"].Value.ToString() : "";
                                discountparams.Memo = dgridRecvPayment.Rows[i].Cells["SalesMemo"].Value != null ? dgridRecvPayment.Rows[i].Cells["SalesMemo"].Value.ToString() : "";
                                discountparams.CustomerPONumber = dgridRecvPayment.Rows[i].Cells["SalesCustomerPONumber"].Value != null ? dgridRecvPayment.Rows[i].Cells["SalesCustomerPONumber"].Value.ToString() : "";
                                discountparams.Discount = dgridRecvPayment.Rows[i].Cells["Discount"].Value != null ? Convert.ToDecimal(dgridRecvPayment.Rows[i].Cells["Discount"].Value.ToString()) : 0;

                                CreateDiscountSaleInvoice(discountparams);
                            }
                            string lSalesNo = dgridRecvPayment.Rows[i].Cells["InvoiceNo"].Value != null ? dgridRecvPayment.Rows[i].Cells["InvoiceNo"].Value.ToString() : "0";
                      
                            PaymentCommon.UpdateSalesRecord(trandate, lSalesNo, deciamt, lDiscount);
                        }
                    }

                    for (int i = 0; i < PaymentInfoTb.Rows.Count; i++)
                    {
                        //Tender details
                        Dictionary<string, object> paramTender = new Dictionary<string, object>();
                        string sqlTender = @"INSERT INTO PaymentTender (
                                                          PaymentID,
                                                          Amount,
                                                          PaymentMethodID)
                                                      VALUES (
                                                          @PaymentID,
                                                          @Amount,
                                                          @PaymentMethodID )";

                        paramTender.Add("@PaymentID", paymentid);
                        paramTender.Add("@Amount", PaymentInfoTb.Rows[i]["AmountPaid"].ToString());
                        paramTender.Add("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"].ToString());

                        CommonClass.runSql(sqlTender, CommonClass.RunSqlInsertMode.QUERY, paramTender);

                        //Details
                        Dictionary<string, object> paramDetails = new Dictionary<string, object>();
                        string sqlDetails = @"INSERT INTO PaymentDetails (
                                                            PaymentID,
                                                            PaymentMethodID,
                                                            PaymentAuthorisationNumber, 
                                                            PaymentCardNumber, 
                                                            PaymentNameOnCard,
                                                            PaymentExpirationDate, 
                                                            CardNotes, 
                                                            PaymentBSB, 
                                                            PaymentBankAccountNumber, 
                                                            PaymentBankAccountName,
                                                            PaymentChequeNumber, 
                                                            BankNotes, 
                                                            PaymentNotes,
                                                            PaymentGCNo,
                                                            PaymentGCNotes)
                                                      VALUES (
                                                        @PaymentID,
                                                        @PaymentMethodID,
                                                        @PaymentAuthorisationNumber, 
                                                        @PaymentCardNumber, 
                                                        @PaymentNameOnCard,
                                                        @PaymentExpirationDate, 
                                                        @PaymentCardNotes, 
                                                        @PaymentBSB, 
                                                        @PaymentBankAccountNumber, 
                                                        @PaymentBankAccountName,
                                                        @PaymentChequeNumber, 
                                                        @PaymentBankNotes, 
                                                        @PaymentNotes,
                                                        @PaymentGCNo,
                                                        @PaymentGCNotes)";
                        paramDetails.Add("@PaymentID", paymentid);
                        paramDetails.Add("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"].ToString());
                        paramDetails.Add("@PaymentAuthorisationNumber", PaymentInfoTb.Rows[i]["PaymentAuthorisationNumber"].ToString());
                        paramDetails.Add("@PaymentCardNumber", PaymentInfoTb.Rows[i]["PaymentCardNumber"].ToString());
                        paramDetails.Add("@PaymentNameOnCard", PaymentInfoTb.Rows[i]["PaymentNameOnCard"].ToString());
                        paramDetails.Add("@PaymentExpirationDate", PaymentInfoTb.Rows[i]["PaymentExpirationDate"].ToString());
                        paramDetails.Add("@PaymentCardNotes", PaymentInfoTb.Rows[i]["PaymentCardNotes"].ToString());
                        paramDetails.Add("@PaymentBSB", PaymentInfoTb.Rows[i]["PaymentBSB"].ToString());
                        paramDetails.Add("@PaymentBankAccountNumber", PaymentInfoTb.Rows[i]["PaymentBankAccountNumber"].ToString());
                        paramDetails.Add("@PaymentBankAccountName", PaymentInfoTb.Rows[i]["PaymentBankAccountName"].ToString());
                        paramDetails.Add("@PaymentChequeNumber", PaymentInfoTb.Rows[i]["PaymentChequeNumber"].ToString());
                        paramDetails.Add("@PaymentBankNotes", PaymentInfoTb.Rows[i]["PaymentBankNotes"].ToString());
                        paramDetails.Add("@PaymentNotes", PaymentInfoTb.Rows[i]["PaymentNotes"].ToString());
                        paramDetails.Add("@PaymentGCNo", PaymentInfoTb.Rows[i]["PaymentGCNo"].ToString());
                        paramDetails.Add("@PaymentGCNotes", PaymentInfoTb.Rows[i]["PaymentGCNotes"].ToString());
                        CommonClass.runSql(sqlDetails, CommonClass.RunSqlInsertMode.QUERY, paramDetails);
                    }
                }
                return paymentid;
        }

        void DeletePaymentRecord(string pOldPaymentNo, int pOldPaymentID = 0)
        {
            if (pOldPaymentNo == "" && pOldPaymentID == 0)
            {
                MessageBox.Show("Transaction cannot be deleted, Invalid Payment Number");
            }
            else
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = dtpdate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                PaymentCommon.PaymentReturn pr = PaymentCommon.RemovePaymentRecord(trandate, pOldPaymentID, "SP");
                //DELETE PAYMENT RECORD
                if (pr.mPaymentID > 0)
                {
                    //DELETE JOURNAL ENTRIES OF OLD
                    if (TransactionClass.DeleteJournalEntries(pOldPaymentNo) > 0)
                    {
                        //UPDATE ACCOUNT BALANCES

                        TransactionClass.UpdateProfileBalances(lblProfileID.Text, pr.mTotalAmount);
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Deleted Payment Transaction Number " + pOldPaymentNo, pOldPaymentNo);
                        MessageBox.Show("Deleted Payment Transaction successfully.", "Payment entry Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }

        //private void RecipientAccount_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        CommonClass.getAccount(glAccountCode.Text);
        //        List<string> list = new List<string>();
        //        list = CommonClass.GetAcc;
        //        if (list.Count == 0)
        //        {
        //            MessageBox.Show("Account Number does not exist");
        //        }
        //        else
        //        {
        //           // RecipientAccountName.Text = list[1];
        //            string strcuracctbal = list[2].ToString();
        //            decimal lbal = strcuracctbal != "" ? Convert.ToDecimal(list[2]) : 0;
        //            CurrentAccountBalance.Text = (Math.Round(lbal, 2).ToString("C"));
        //        }
        //    }
        //}

        //private void RecipientAccountName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        CommonClass.getAccountName(RecipientAccountName.Text);
        //        List<string> list = new List<string>();
        //        list = CommonClass.GetAccName;
        //        if (list.Count == 0)
        //        {
        //            MessageBox.Show("Account Name Does not Exist");
        //        }
        //        else
        //        {
        //            RecipientAccount.Text = list[0];
        //            string strcuracctbal = list[2].ToString();
        //            decimal lbal = strcuracctbal != "" ? Convert.ToDecimal(list[2]) : 0;
        //            CurrentAccountBalance.Text = (Math.Round(lbal, 2).ToString("C"));
        //        }
        //    }
        //}

        private void lblProfileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgridRecvPayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgridRecvPayment_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //if (e.ColumnIndex == 9) //ReturnCredits
            //{
            //    string strcredits = dgridRecvPayment[e.ColumnIndex, e.RowIndex].Value != null ? dgridRecvPayment[e.ColumnIndex, e.RowIndex].Value.ToString() : "";
            //    decimal retcredits = strcredits != "" ? Convert.ToDecimal(strcredits) : 0;
            //    if (retcredits > nudRetCredit.Value)
            //    {
            //        MessageBox.Show("Value entered is bigger than the available customer return credits");
            //        dgridRecvPayment[e.ColumnIndex, e.RowIndex].Value = 0;
            //    }
            //}
        }

        private void RecipientAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReceipt();
        }
        private void LoadReceipt()
        {
            string WholeWord = "";
            string DeciWord = "";
                string selectSql = "SELECT WholeNumberWord, DecimalWord FROM Currency";

                DataTable dt = new DataTable();
                CommonClass.runSql(ref dt, selectSql);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    WholeWord = dr["WholeNumberWord"].ToString();
                    DeciWord = dr["DecimalWord"].ToString();

                }

                string sql = @"SELECT Payment.TransactionDate, Payment.Memo, TotalAmount, p.Name, Payment.AccountID, 
                             Payment.PaymentID, PaymentNumber, pl.Amount, Address = (SELECT CONCAT(Street, ', ' ,City, ', ',
                             State, ', ', Postcode, ', ', Country) FROM Contacts WHERE ProfileID = p.ID AND Location = p.LocationID), s.SalesNumber
                             FROM Payment  
                             INNER JOIN Profile p ON p.ID = Payment.ProfileID
                             INNER JOIN PaymentLines pl ON pl.PaymentID = Payment.PaymentID
                             INNER JOIN Sales s ON s.SalesID = pl.EntityID 
                             WHERE PaymentFor = 'Sales' 
                             AND Payment.PaymentID IN (" + TranNo + ")";

                DataTable TbRep = new DataTable();
                CommonClass.runSql(ref TbRep, sql);


                Reports.ReportParams receiptparams = new Reports.ReportParams();
                receiptparams.PrtOpt = 1;
                receiptparams.Rec.Add(TbRep);

                receiptparams.ReportName = "PrintSalesReceipt.rpt";
                receiptparams.RptTitle = "Receipt";

              
                receiptparams.Params = "compname|Address|WholeNumWord|DecimalWord";
                receiptparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + WholeWord + "|" + DeciWord;

                CommonClass.ShowReport(receiptparams);

        }

        private string GetPaymentMethodName(string pID)
        {
            string lpmethod = "";
                DataTable ldt = new DataTable();
            CommonClass.runSql(ref ldt, "SELECT PaymentMethod, id, GLAccountCode FROM PaymentMethods where id = '" + pID + "'");
                if (ldt.Rows.Count > 0)
                {
                    lpmethod = ldt.Rows[0]["PaymentMethod"].ToString();
                }

            return lpmethod;
        }
    }
}


