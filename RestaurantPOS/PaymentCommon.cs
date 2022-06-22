using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AbleRetailPOS
{
    class PaymentCommon
    {
        public class DiscountFields
        {
            private int mTermsReferenceID = 0;
            private int mShippingContactID = 0;
            private int mShippingMethodID = 0;
            private string mMemo = "";
            private string mCustomerPONumber = "";
            private string mSalesReference = "";
            private string mSupplierInvNumber = "";
            private string mPurchaseReference = "";
            private decimal mDiscount = 0;

            public int TermsReferenceID
            {
                get { return mTermsReferenceID; }
                set { mTermsReferenceID = value; }
            }
            public int ShippingContactID
            {
                get { return mShippingContactID; }
                set { mShippingContactID = value; }
            }
            public int ShippingMethodID
            {
                get { return mShippingMethodID; }
                set { mShippingMethodID = value; }
            }
            public string Memo
            {
                get { return mMemo; }
                set { mMemo = value; }
            }
            public string CustomerPONumber
            {
                get { return mCustomerPONumber; }
                set { mCustomerPONumber = value; }
            }
            public string SalesReference
            {
                get { return mSalesReference; }
                set { mSalesReference = value; }
            }
            public string SupplierInvNumber
            {
                get { return mSupplierInvNumber; }
                set { mSupplierInvNumber = value; }
            }
            public string PurchaseReference
            {
                get { return mPurchaseReference; }
                set { mPurchaseReference = value; }
            }
            public decimal Discount
            {
                get { return mDiscount; }
                set { mDiscount = value; }
            }
        }

        public static DataTable GetPaymentLines(int pPaymentID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT pl.*, 
                                      p.ProfileID, 
                                      p.TotalAmount, 
                                      p.Memo, 
                                      p.PaymentFor,
                                      p.AccountID,
                                      p.UserID,
                                      p.PaymentMethodID,
                                      p.TransactionDate,
                                      p.PaymentNumber
                               FROM PaymentLines as pl 
                               INNER JOIN Payment p ON pl.PaymentID = p.PaymentID 
                               WHERE pl.PaymentID = " + pPaymentID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END
        public static DataTable GetPaymentTenders(int pPaymentID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT pt.PaymentID, pt.Amount, pm.GLAccountCode from PaymentTender pt
                        inner join PaymentMethods pm on pt.PaymentMethodID = pm.id
                        where pt.PaymentID =  " + pPaymentID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END
        public static bool CreateJournalEntriesSP(int pID, string pPaymentMemo, string lAccountID = "" )
        {
            SqlConnection con = null;
            try
            {
                if(lAccountID == "")
                {
                    lAccountID = CommonClass.DRowPref["TradeDebtorGLCode"].ToString();//CommonClass.DRowLA["ReceivablesAccountID"].ToString();

                }

                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();


                string lTranDate = "";
                string lMemo = "";
                string lheaderentity = "";
                string lTranNo = "";
                DataTable ltb = GetPaymentLines(pID);
                if (ltb.Rows.Count > 0)
                {
                  
                    lTranNo = ltb.Rows[0]["PaymentNumber"].ToString();
                    lMemo = pPaymentMemo;//ltb.Rows[0]["Memo"].ToString();
                    lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    lheaderentity = ltb.Rows[0]["EntityID"].ToString();
                                     
            
                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        decimal lPmtAmount = Convert.ToDecimal(ltb.Rows[i]["Amount"].ToString());
                        string lLineMemo = ltb.Rows[i]["Memo"].ToString();
                        string lEntity = ltb.Rows[i]["EntityID"].ToString();

                        if (lPmtAmount < 0) // NEGATIVE SO DEBIT AMOUNT 
                        {
                            // NEGATIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                        DebitAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                   (lPmtAmount * -1) + ", '" + lTranNo + "', 'SP', " + lEntity + ")";
                            
                        }
                        else //POSITIVE SO CREDIT AMOUNT 
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                   lPmtAmount + ", '" + lTranNo + "', 'SP', " + lEntity + ")";
                           
                        }
                        
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@Memo", lMemo);
                        param.Add("@LineMemo", lLineMemo);
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);

                    }

                    //INSERT JOURNAL FOR Total Amount Received BROKEN DOWN BY PAYMENT METHOD GL CODE
                    DataTable ltbTender = GetPaymentTenders(pID);
                    if (ltbTender.Rows.Count > 0)
                    {
                        Dictionary<string, object> paramTender = new Dictionary<string, object>();
                        paramTender.Add("@Memo", lMemo);
                        for (int j = 0; j < ltbTender.Rows.Count; j++)
                        {
                            decimal lTotalAmountReceived = Convert.ToDecimal(ltbTender.Rows[j]["Amount"].ToString());
                            string lRecipientID = ltbTender.Rows[j]["GLAccountCode"].ToString();
                            

                            //INSERT JOURNAL FOR Total Amount Received
                            if (lTotalAmountReceived < 0)
                            {
                                //NEGATIVE SO CREDIT AMOUNT
                                sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, EntityID) " +
                                      " VALUES('" + lTranDate + "', @Memo, @Memo, '" + lRecipientID + "', " +
                                      (lTotalAmountReceived * -1) + ",'" + lTranNo + "', 'SP'," + lheaderentity + ")";
                            }
                            else
                            {
                                //DEBIT AMOUNT
                                sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID)  " +
                                      " VALUES('" + lTranDate + "', @Memo, @Memo, '" + lRecipientID + "', " +
                                      lTotalAmountReceived + ",'" + lTranNo + "', 'SP'," + lheaderentity + ")";
                            }

                                                   
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, paramTender);
                        }
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction. No PaymentLines found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public static bool CreateJournalEntriesCD(int pID, string pPaymentMemo)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                DataTable ltb = GetPaymentLines(pID);
                if (ltb.Rows.Count > 0)
                {
                    decimal lTotalAmountReceived = Convert.ToDecimal(ltb.Rows[0]["TotalAmount"].ToString());
                    string lRecipientID = ltb.Rows[0]["AccountID"].ToString();
                    string lTranNo = ltb.Rows[0]["PaymentNumber"].ToString();
                    string lMemo = pPaymentMemo;//ltb.Rows[0]["Memo"].ToString();
                    string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    string lheaderentity = ltb.Rows[0]["EntityID"].ToString();

                    //INSERT JOURNAL FOR Total Amount Received
                    if (lTotalAmountReceived < 0)
                    {
                        //NEGATIVE SO CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "', @Memo, @Memo, '" + lRecipientID + "', " +
                              (lTotalAmountReceived * -1) + ",'" + lTranNo + "', 'SP'," + lheaderentity + ")";
                    }
                    else
                    {
                        //DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "',  @Memo, @Memo, '" + lRecipientID + "', " +
                              lTotalAmountReceived + ",'" + lTranNo + "', 'SP'," + lheaderentity + ")";
                    }

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@Memo", lMemo);                   
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);

                    string lAccountID = CommonClass.DRowPref["TradeDebtorGLCode"].ToString();//CommonClass.DRowLA["ReceivablesDepositsID"].ToString();
                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        decimal lPmtAmount = Convert.ToDecimal(ltb.Rows[i]["Amount"].ToString());
                        string lLineMemo = ltb.Rows[i]["Memo"].ToString();
                        string lEntity = ltb.Rows[i]["EntityID"].ToString();

                        if (lPmtAmount < 0) // NEGATIVE SO DEBIT AMOUNT 
                        {
                            // NEGATIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                        DebitAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                   (lPmtAmount * -1) + ", '" + lTranNo + "', 'SP', " + lEntity + ")";
                            
                        }
                        else //POSITIVE SO CREDIT AMOUNT 
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                   lPmtAmount + ", '" + lTranNo + "', 'SP', " + lEntity + ")";
                            
                        }

                        Dictionary<string, object> paramlines = new Dictionary<string, object>();
                        paramlines.Add("@Memo", lMemo);
                        paramlines.Add("@LineMemo", lMemo);
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, paramlines);
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction. No PaymentLines found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public static bool CreateJournalEntriesBP(int pID, string pPaymentMemo)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                DataTable ltb = GetPaymentLines(pID);
                if (ltb.Rows.Count > 0)
                {

                    decimal lTotalAmountReceived = Convert.ToDecimal(ltb.Rows[0]["TotalAmount"].ToString());
                    string lPayAcctID = ltb.Rows[0]["AccountID"].ToString();
                    string lTranNo = ltb.Rows[0]["PaymentNumber"].ToString();
                    string lMemo = pPaymentMemo;/*ltb.Rows[0]["Memo"].ToString();*/
                    string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    string lheaderentity = ltb.Rows[0]["EntityID"].ToString();
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    

                    //INSERT JOURNAL FOR Total Amount Received
                    if (lTotalAmountReceived < 0)
                    {
                        //DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "', @Memo, @Memo, '" + lPayAcctID + "', " +
                              (lTotalAmountReceived * -1).ToString() + ",'" + lTranNo + "', 'BP', " + lheaderentity + ")";
                    }
                    else
                    {
                        //CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "', @Memo, @Memo, '" + lPayAcctID + "', " +
                              lTotalAmountReceived.ToString() + ",'" + lTranNo + "', 'BP', " + lheaderentity + ")";
                    }

                    param.Add("@Memo", lMemo);
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);

                    string lAccountID = CommonClass.DRowPref["TradeCreditorGLCode"].ToString();//CommonClass.DRowLA["PayablesAccountID"].ToString();/*ltb.Rows[i]["AccountID"].ToString();*/
                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        decimal lPmtAmount = Convert.ToDecimal(ltb.Rows[i]["Amount"].ToString());
                        string lLineMemo = ltb.Rows[i]["Memo"].ToString();
                        string lEntity = ltb.Rows[i]["EntityID"].ToString();

                        if (lPmtAmount < 0)
                        {
                            //NEGATIVE SO CREDIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', '@Memo, @LineMemo, '" + lAccountID + "', " +
                                   (lPmtAmount * -1) + ", '" + lTranNo + "', 'BP', " + lEntity + ")";
                           
                        }
                        else 
                        {
                            // POSITIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                        DebitAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                   lPmtAmount + ", '" + lTranNo + "', 'BP', " + lEntity + ")";
                            
                        }
                        Dictionary<string, object> paramline = new Dictionary<string, object>();
                        paramline.Add("@Memo", lMemo);
                        paramline.Add("@LineMemo", lLineMemo);
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, paramline);

                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction. No PaymentLines found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public static bool CreateJournalEntriesSD(int pID, string pPaymentMemo)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                DataTable ltb = GetPaymentLines(pID);
                if (ltb.Rows.Count > 0)
                {
                    decimal lTotalAmountReceived = Convert.ToDecimal(ltb.Rows[0]["TotalAmount"].ToString());
                    string lPayAcctID = ltb.Rows[0]["AccountID"].ToString();
                    string lTranNo = ltb.Rows[0]["PaymentNumber"].ToString();
                    string lMemo = pPaymentMemo;/*ltb.Rows[0]["Memo"].ToString();*/
                    string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    string lheaderentity = ltb.Rows[0]["EntityID"].ToString();
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@Memo", lMemo);
                  
                    //INSERT JOURNAL FOR Total Amount Received
                    if (lTotalAmountReceived < 0)
                    {
                        //DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "', @Memo, @Memo, '" + lPayAcctID + "', " +
                              (lTotalAmountReceived * -1).ToString() + ",'" + lTranNo + "', 'BP', " + lheaderentity + ")";
                    }
                    else
                    {
                        //CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "', @Memo', @Memo, '" + lPayAcctID + "', " +
                              lTotalAmountReceived.ToString() + ",'" + lTranNo + "', 'BP', " + lheaderentity + ")";
                    }
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);

                    string lAccountID = CommonClass.DRowPref["TradeCreditorGLCode"].ToString();//CommonClass.DRowLA["PayablesDepositsID"].ToString();
                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        decimal lPmtAmount = Convert.ToDecimal(ltb.Rows[i]["Amount"].ToString());
                        string lLineMemo = ltb.Rows[i]["Memo"].ToString();
                        string lEntity = ltb.Rows[i]["EntityID"].ToString();

                        if (lPmtAmount < 0)
                        {
                            //NEGATIVE SO CREDIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                   (lPmtAmount * -1) + ", '" + lTranNo + "', 'BP', " + lEntity + ")";
                            
                        }
                        else
                        {
                            // POSITIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                        DebitAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @LineMemo, '" + lAccountID + "', " +
                                   lPmtAmount + ", '" + lTranNo + "', 'BP', " + lEntity + ")";
                            
                        }
                        Dictionary<string, object> paramline = new Dictionary<string, object>();
                        paramline.Add("@Memo", lMemo);
                        paramline.Add("@LineMemo", lLineMemo);
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, paramline);

                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction. No PaymentLines found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static int UpdateSalesRecord(string tranDate, string pSalesNo, decimal pAmountApplied = 0, decimal pDiscount = 0)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);

                decimal lGrandTotal = 0;
                decimal lTotalPaid = 0;
                decimal lTotalDue = 0;

                SqlCommand amtcmd = new SqlCommand("SELECT GrandTotal, TotalPaid FROM Sales WHERE SalesNumber = '" + pSalesNo + "'", con);
                con.Open();

                SqlDataReader amtrdr = amtcmd.ExecuteReader();
                if (amtrdr.Read())
                {
                    lGrandTotal = Convert.ToDecimal(amtrdr["GrandTotal"]);
                    lTotalPaid = Convert.ToDecimal(amtrdr["TotalPaid"]);
                }

                lTotalPaid += pAmountApplied;
                lTotalDue = lGrandTotal - (lTotalPaid + pDiscount);

                string updatesalessql = "UPDATE Sales SET TotalPaid = @TotalPaid, TotalDue = @TotalDue";
                if (lTotalDue == 0)
                {
                    updatesalessql += ", InvoiceStatus = 'Closed', ClosedDate = getutcdate()";
                    

                }
                updatesalessql += " WHERE SalesNumber = '" + pSalesNo + "'";


                SqlCommand cmd = new SqlCommand(updatesalessql, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@TotalPaid", lTotalPaid);
                cmd.Parameters.AddWithValue("@TotalDue", lTotalDue);
                cmd.ExecuteNonQuery();

                SqlCommand cmdsalesid = new SqlCommand("SELECT SalesID FROM Sales WHERE SalesNumber = '" + pSalesNo + "'", con);
                int salesid = Convert.ToInt32(cmdsalesid.ExecuteScalar());

                return salesid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static int UpdatePurchaseRecord(string trandate, string pPurchaseNo, decimal pAmountApplied = 0, decimal pDiscount = 0)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);

                decimal lGrandTotal = 0;
                decimal lTotalPaid = 0;
                decimal lTotalDue = 0;

                SqlCommand amtcmd = new SqlCommand("SELECT GrandTotal, TotalPaid FROM Purchases WHERE PurchaseNumber = '" + pPurchaseNo + "'", con);
                con.Open();

                SqlDataReader amtrdr = amtcmd.ExecuteReader();
                if (amtrdr.Read())
                {
                    lGrandTotal = Convert.ToDecimal(amtrdr["GrandTotal"]);
                    lTotalPaid = Convert.ToDecimal(amtrdr["TotalPaid"]);
                }

                lTotalPaid += pAmountApplied;
                lTotalDue = lGrandTotal - (lTotalPaid + pDiscount);

                string updatesalessql = "UPDATE Purchases SET TotalPaid = @TotalPaid, TotalDue = @TotalDue";
                if (lTotalDue == 0)
                    updatesalessql += ", POStatus = 'Closed', ClosedDate = getutcdate()";
                updatesalessql += " WHERE PurchaseNumber = '" + pPurchaseNo + "'";

                SqlCommand cmd = new SqlCommand(updatesalessql, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@TotalPaid", lTotalPaid);
                cmd.Parameters.AddWithValue("@TotalDue", lTotalDue);
                cmd.ExecuteNonQuery();

                SqlCommand cmdpurchaseid = new SqlCommand("SELECT PurchaseID FROM Purchases WHERE PurchaseNumber = '" + pPurchaseNo + "'", con);
                int purchaseid = Convert.ToInt32(cmdpurchaseid.ExecuteScalar());

                return purchaseid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public class PaymentReturn
        {
            public int mPaymentID;
            public int mOldPaymentID;
            public decimal mTotalAmount;
        }

        public static PaymentReturn ReversePaymentRecord(string trandate, string pNewPaymentNo, string pOldPaymentNo, string pCurSeries, string pPaymentFor)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);

                string sql = @"INSERT INTO Payment
                             ( ProfileID,
                               TotalAmount,
                               Memo,
                               PaymentFor,
                               AccountID,
                               UserID,
                               PaymentMethodID,
                               TransactionDate,
                               PaymentNumber,
                               PaymentAuthorisationNumber, 
                               PaymentCardNumber, 
                               PaymentNameOnCard,
                               PaymentExpirationDate, 
                               PaymentCardNotes, 
                               PaymentBSB, 
                               PaymentBankAccountNumber, 
                               PaymentBankAccountName,
                               PaymentChequeNumber, 
                               PaymentBankNotes, 
                               PaymentNotes) ";
                sql += "SELECT ProfileID, TotalAmount * (-1) AS TotalAmount, 'Reversal ' + Memo AS Memo, PaymentFor, AccountID, UserID, PaymentMethodID, TransactionDate, '" + pNewPaymentNo + "' AS PaymentNumber, ";
                sql += @" PaymentAuthorisationNumber,
                            PaymentCardNumber,
                            PaymentNameOnCard,
                            PaymentExpirationDate, 
                            PaymentCardNotes,
                            PaymentBSB, 
                            PaymentBankAccountNumber, 
                            PaymentBankAccountName,
                            PaymentChequeNumber, 
                            PaymentBankNotes, 
                            PaymentNotes FROM Payment WHERE PaymentNumber = '" + pOldPaymentNo + "'; SELECT SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                con.Open();

                int NewPaymentID = Convert.ToInt32(cmd.ExecuteScalar());
                int OldPaymentID = 0;
                decimal RevTotalAmount = 0;
                if (NewPaymentID != 0)
                {
                    //Update series #   
                    if (pPaymentFor == "SP")
                        sql = "UPDATE TransactionSeries SET PaymentSeries = '" + pCurSeries + "'";
                    else if (pPaymentFor == "BP")
                        sql = "UPDATE TransactionSeries SET BillsPaymentSeries = '" + pCurSeries + "'";

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    string sqlpmtlines = "SELECT * FROM PaymentLines WHERE PaymentID = (SELECT TOP 1 PaymentID FROM Payment WHERE PaymentNumber = '" + pOldPaymentNo + "' ORDER BY PaymentID DESC)";
                    SqlCommand cmdpmtlines = new SqlCommand(sqlpmtlines, con);
                    SqlDataAdapter dapmtlines = new SqlDataAdapter(cmdpmtlines);
                    DataTable dtpmtlines = new DataTable();
                    dapmtlines.Fill(dtpmtlines);

                    foreach(DataRow dr in dtpmtlines.Rows)
                    {
                        //REVERSE PaymentLines  
                        sql = @"INSERT INTO PaymentLines
                              ( PaymentID,
                                EntityID,
                                Amount )
                              VALUES (
                                @PaymentID,
                                @EntityID,
                                @Amount )";

                        SqlCommand cmdlines = new SqlCommand(sql, con);    
                        OldPaymentID = Convert.ToInt32(dr["PaymentID"]);
                        cmdlines.Parameters.AddWithValue("@PaymentID", NewPaymentID);
                        cmdlines.Parameters.AddWithValue("@EntityID", dr["EntityID"]);
                        decimal reversedamt = Convert.ToDecimal(dr["Amount"]);
                        RevTotalAmount += reversedamt;
                        cmdlines.Parameters.AddWithValue("@Amount", reversedamt * -1);
                        cmdlines.ExecuteNonQuery();

                        if (pPaymentFor == "SP")
                        {
                            //Reverse the amount in the sales
                            string salesno = new SqlCommand("SELECT SalesNumber FROM Sales WHERE SalesID = " + Convert.ToInt32(dr["EntityID"]), con).ExecuteScalar().ToString();
                            UpdateSalesRecord(trandate, salesno, reversedamt * -1);
                        }
                        else if (pPaymentFor == "BP")
                        {
                            //Reverse the amount in purchases
                            string purchaseno = new SqlCommand("SELECT PurchaseNumber FROM Purchases WHERE PurchaseID = " + Convert.ToInt32(dr["EntityID"]), con).ExecuteScalar().ToString();
                            UpdatePurchaseRecord(trandate, purchaseno, reversedamt * -1);
                        }
                    }
                }
                PaymentReturn pr = new PaymentReturn();
                pr.mOldPaymentID = OldPaymentID;
                pr.mPaymentID = NewPaymentID;
                pr.mTotalAmount = RevTotalAmount;

                return pr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static PaymentReturn RemovePaymentRecord(string trandate, int pPaymentID, string pPaymentFor)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(CommonClass.ConStr);

                string sql = "DELETE FROM Payment WHERE PaymentID = " + pPaymentID;

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();

                string sqlpmtlines = "SELECT * FROM PaymentLines WHERE PaymentID = " + pPaymentID;
                SqlCommand cmdpmtlines = new SqlCommand(sqlpmtlines, con);
                SqlDataReader rdrpmtlines = cmdpmtlines.ExecuteReader();
                decimal revtotalamt = 0;

                while (rdrpmtlines.Read())
                {
                    decimal reversedamt = Convert.ToDecimal(rdrpmtlines["Amount"]);
                    revtotalamt += reversedamt;

                    if (pPaymentFor == "SP")
                    {
                        //Reverse the amount in the sales
                        string salesno = new SqlCommand("SELECT SalesNumber FROM Sales WHERE SalesID = " + Convert.ToInt32(rdrpmtlines["EntityID"]), con).ExecuteScalar().ToString();
                        UpdateSalesRecord(trandate, salesno, reversedamt * -1);
                    }
                    else if (pPaymentFor == "BP")
                    {
                        //Reverse the amount in purchases
                        string purchaseno = new SqlCommand("SELECT PurchaseNumber FROM Purchases WHERE PurchaseID = " + Convert.ToInt32(rdrpmtlines["EntityID"]), con).ExecuteScalar().ToString();
                        UpdatePurchaseRecord(trandate, purchaseno, reversedamt * -1);
                    }
                }

                DeletePaymentLines(pPaymentID);

                PaymentReturn pr = new PaymentReturn();
                pr.mPaymentID = pPaymentID;
                pr.mTotalAmount = revtotalamt;

                return pr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static bool ReverseJournalEntries(string pID, string pPmentFor, string pNewPaymentNo = "", string pOldID = "")
        {
            SqlConnection con = null;
            try
            {
                string sql = "";

                //GET THE HEADER 
                sql = @"SELECT TransactionDate,
                               Memo AS LineMemo,
                               AccountID,
                               TotalAmount AS Amount,
                               '' AS EntityID, 
                               'H' AS LineType 
                        FROM Payment WHERE PaymentID = " + pOldID +
                     @" UNION SELECT pl.EntryDate AS TransactionDate,
                                '' AS LineMemo,
                                p.AccountID,
                                sl.Amount,
                                sl.EntityID, 
                                'D' AS LineType 
                        FROM Payment p 
                        INNER JOIN PaymentLines pl ON p.PaymentID = pl.PaymentID 
                        WHERE p.PaymentID = " + pOldID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                string dte = "";
                string dteStr = "";
                foreach (DataRow dr in dt.Rows)
                {
                    dte = dt.Rows[0]["TransactionDate"].ToString();
                    dteStr = DateTime.Parse(dte).ToString("yyyy-MM-dd HH:mm:ss");

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@Memo", "Reversal " + dr["LineMemo"].ToString());
                    if (Convert.ToDouble(dr["Amount"].ToString()) > 0)
                    {
                        //CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, EntryDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type)  " +
                               "VALUES ('" + dteStr + "', getutcdate(), @Memo, '', " +
                               dr["AccountID"].ToString() + ", " + dr["Amount"].ToString() + ", '" + pNewPaymentNo + "', '" + pPmentFor + "')";
                    }
                    else
                    {
                        //DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, EntryDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type)  " +
                              "VALUES ('" + dteStr + "', getutcdate(), @Memo, '', " +
                              dr["AccountID"].ToString() + ", " + dr["Amount"].ToString() + " * -1, '" + pNewPaymentNo + "', '" + pPmentFor + "')";
                    }
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        public static int DeletePaymentLines(int pOldPaymentID)
        {
            SqlConnection con = null;
            try
            {
                //Reverse Journal Entry Record
                con = new SqlConnection(CommonClass.ConStr);

                string sql = "DELETE FROM PaymentLines WHERE PaymentID = " + pOldPaymentID;
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                int affectedrows = cmd.ExecuteNonQuery();

                return affectedrows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static void GeneratePaymentNumber(ref string pCurSeries, ref string pPaymentNo, string pPmentFor)
        {
            SqlConnection con_ua = null;
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);
                string selectSql_ua = "";

                if (pPmentFor == "SP")
                    selectSql_ua = "SELECT PaymentSeries, PaymentPrefix FROM TransactionSeries";
                else if (pPmentFor == "BP")
                    selectSql_ua = "SELECT BillsPaymentSeries AS PaymentSeries, BillsPaymentPrefix AS PaymentPrefix FROM TransactionSeries";

                SqlCommand cmd_ua = new SqlCommand(selectSql_ua, con_ua);
                con_ua.Open();
                string lSeries = "";
                int lCnt = 0;
                int lNewSeries = 0;

                using (SqlDataReader reader = cmd_ua.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lSeries = (reader["PaymentSeries"].ToString());
                            lCnt = lSeries.Length;
                            lSeries = lSeries.TrimStart('0');
                            lSeries = (lSeries == "" ? "0" : lSeries);

                            lNewSeries = Convert.ToInt16(lSeries) + 1;
                            pCurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                            pPaymentNo = (reader["PaymentPrefix"].ToString()).Trim(' ') + pCurSeries;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transaction Series Numbers not setup properly.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ua != null)
                    con_ua.Close();
            }
        }

        public static void UpdatePaymentNumber(ref string pCurSeries,  string pPmentFor)
        {
            SqlConnection con_ua = null;
            try
            {
                con_ua = new SqlConnection(CommonClass.ConStr);
                string selectSql_ua = "";

                if (pPmentFor == "SP")
                    selectSql_ua = "UPDATE TransactionSeries set PaymentSeries = '" + pCurSeries + "'";
                else if (pPmentFor == "BP")
                    selectSql_ua = "UPDATE TransactionSeries set BillsPaymentSeries = '" + pCurSeries + "'";

                SqlCommand cmd_ua = new SqlCommand();
                cmd_ua.Connection = con_ua;
                con_ua.Open();
                cmd_ua.CommandText = selectSql_ua;
                cmd_ua.ExecuteNonQuery();


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ua != null)
                    con_ua.Close();
            }
        }

        public static string GetCDPaymentNumber(string pSalesID)
        {
            SqlConnection con = null;
            string RetVal = "";
            try
            {
                string sql = @"SELECT  p.PaymentNumber 
                    from PaymentLines pl inner join Payment p on pl.PaymentID = p.PaymentID 
                    where pl.EntityID = " + pSalesID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable ltb = new DataTable();
                da.Fill(ltb);
                if(ltb.Rows.Count > 0)
                {
                    RetVal = ltb.Rows[0]["PaymentNumber"].ToString();
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
            return RetVal;
        } //END
        public static bool TransferCDJournal(string pSalesID, string pPaymentNumber, string pPaymentMemo, string lTranDate, decimal pAmount)
        {
            SqlConnection con = null;
            try
            {
                string lDebtorGL = CommonClass.DRowPref["TradeDebtorGLCode"].ToString();
                string lSaleDepositGL = CommonClass.DRowPref["SalesDepositGLCode"].ToString();
             

                string sql = "";
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();


               
                string lMemo = "";
                string lheaderentity = "";
                string lTranNo = "";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@Memo", pPaymentMemo);

                if (pAmount < 0) // NEGATIVE SO DEBIT AMOUNT 
                {
                    // NEGATIVE SO DEBIT AMOUNT  // FOR SalesDepositGL
                    sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                        CreditAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @Memo, '" + lSaleDepositGL + "', " +
                           (pAmount * -1) + ", '" + pPaymentNumber + "', 'SP', 0)";
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);

                    // NEGATIVE SO CREDIT AMOUNT  FOR TRADE DEBTOR
                    sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                        DebitAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @Memo, '" + lDebtorGL + "', " +
                           (pAmount * -1) + ", '" + pPaymentNumber + "', 'SP', 0)";
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
                }
                else 
                {
                    //POSITIVE SO DEBIT AMOUNT  // FOR SalesDepositGL
                    sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    DebitAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @Memo, '" + lSaleDepositGL + "', " +
                           pAmount + ", '" + pPaymentNumber + "', 'SP', 0)";
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
                    // POSITIVE SO CREDIT AMOUNT  FOR TRADE DEBTOR
                    sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, EntityID)
                                   VALUES('" + lTranDate + "', @Memo, @Memo, '" + lDebtorGL + "', " +
                           pAmount + ", '" + pPaymentNumber + "', 'SP', 0)";
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
                }
                return true;

                
                
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

    }
}
