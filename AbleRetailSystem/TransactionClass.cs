using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using CrystalDecisions;
using Microsoft.VisualBasic;
using System.Drawing.Printing;

namespace RestaurantPOS
{
    public class TransactionClass
    {//FOR CALCULATING ACCOUNT BALANCE OF HEADER ACCOUNTS
        public static decimal getHeaderBalance(string pID, int pLevel)
        {
            decimal retval = 0;
            switch (pLevel)
            {
                case 1:
                    retval = getHBalL1(pID);
                    break;
                case 2:
                    retval = getHBalL2(pID);
                    break;
                default:
                    retval = getHBalL0(pID);
                    break;
            }
            return retval;
        }

        public static decimal getHBalL0(string pID)
        {
            decimal retval = 0;
            decimal dAcBal = 0;
            decimal hAcBal = 0;
            //GET SUM OF ALL DETAIL ACCTS UNDER IT LEVEL 1
            string sql5 = "SELECT ISNULL(SUM(CurrentAccountBalance), 0) AS Balance FROM Accounts WHERE IsHeader = 'D' AND AccountLevel = 1 AND ParentAccountID = " + pID;
            SqlConnection connection5 = null;
            try
            {
                connection5 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd5 = new SqlCommand(sql5, connection5);
                SqlDataAdapter da1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();
                connection5.Open();
                da1.SelectCommand = cmd5;
                da1.Fill(dt1);
                dAcBal = Convert.ToDecimal(dt1.Rows[0]["Balance"].ToString());
                //GET SUM OF ALL HEADER ACCTS UNDER IT IT - LEVEL 1
                sql5 = "SELECT * FROM Accounts WHERE IsHeader = 'H' AND AccountLevel = 1 AND ParentAccountID = " + pID;
                cmd5 = new SqlCommand(sql5, connection5);
                da1 = new SqlDataAdapter();
                da1.SelectCommand = cmd5;
                dt1 = new DataTable();
                da1.Fill(dt1);
                foreach (DataRow dr in dt1.Rows)
                {
                    Console.WriteLine(dr["AccountName"].ToString());
                    hAcBal += getHBalL1(dr["AccountID"].ToString());
                }
                retval = dAcBal + hAcBal;
                return retval;
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return retval;
            }
            finally
            {
                if (connection5 != null)
                    connection5.Close();
            }
        }

        public static decimal getHBalL1(string pID)
        {
            decimal retval = 0;
            decimal dAcBal = 0;
            decimal hAcBal = 0;

            //GET SUM OF ALL DETAIL ACCTS UNDER IT
            string sql5 = "SELECT ISNULL(SUM(CurrentAccountBalance), 0) AS Balance FROM Accounts WHERE IsHeader = 'D' AND AccountLevel = 2 AND ParentAccountID = " + pID;
            SqlConnection connection5 = null;
            try
            {
                connection5 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd5 = new SqlCommand(sql5, connection5);
                SqlDataAdapter da1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();
                connection5.Open();
                da1.SelectCommand = cmd5;
                da1.Fill(dt1);
                dAcBal = Convert.ToDecimal(dt1.Rows[0]["Balance"].ToString());
                //GET SUM OF ALL HEADER ACCTS UNDER IT
                sql5 = "SELECT * FROM Accounts WHERE IsHeader = 'H' AND AccountLevel = 2 AND ParentAccountID = " + pID;
                cmd5 = new SqlCommand(sql5, connection5);
                da1 = new SqlDataAdapter();
                da1.SelectCommand = cmd5;
                dt1 = new DataTable();
                da1.Fill(dt1);
                foreach (DataRow dr in dt1.Rows)
                {
                    Console.WriteLine(dr["AccountName"].ToString());
                    hAcBal += getHBalL2(dr["AccountID"].ToString());
                }
                retval = dAcBal + hAcBal;
                return retval;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return retval;
            }
            finally
            {
                if (connection5 != null)
                    connection5.Close();
            }
        }

        public static decimal getHBalL2(string pID)
        {
            decimal retval = 0;
            string sql5 = "SELECT ISNULL(SUM(CurrentAccountBalance), 0) AS Balance FROM Accounts WHERE IsHeader = 'D' AND AccountLevel = 3 AND ParentAccountID = " + pID;
            SqlConnection connection5 = null;
            try
            {
                connection5 = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd5 = new SqlCommand(sql5, connection5);
                SqlDataAdapter da1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();
                da1.SelectCommand = cmd5;
                da1.Fill(dt1);
                retval = Convert.ToDecimal(dt1.Rows[0]["Balance"].ToString());
                return retval;
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return retval;
            }
            finally
            {
                if (connection5 != null)
                    connection5.Close();
            }
        }
        //END

        //FOR CALCULATING OPENING BALANCE OF ACCOUNTS
        //public static decimal CalculateOpeningBalance(decimal pCurFYOPening, string pAccountID, string sdate)
        //{
        //    SqlConnection con = null;
        //    try
        //    {
        //        string sql = "SELECT AccountID, ISNULL(SUM(DebitAmount),0) AS Debit, ISNULL(SUM(CreditAmount), 0) AS Credit FROM Journal WHERE TransactionDate BETWEEN '" + CommonClass.FYBegDateStr + "' AND '" + sdate + "' AND AccountId = " + pAccountID + " GROUP BY AccountID";
        //        con = new SqlConnection(CommonClass.ConStr);
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        con.Open();

        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = cmd;
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        decimal lBal = pCurFYOPening;
        //        decimal lDebit = 0;
        //        decimal lCredit = 0;

        //        if (dt.Rows.Count > 0)
        //        {
        //            lDebit = Convert.ToDecimal((dt.Rows[0]["Debit"].ToString() != "" ? dt.Rows[0]["Debit"].ToString() : "0"));
        //            lCredit = Convert.ToDecimal((dt.Rows[0]["Credit"].ToString() != "" ? dt.Rows[0]["Credit"].ToString() : "0"));
        //            lBal = lBal + lDebit - lCredit;
        //        }

        //        return lBal;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return 0;
        //    }
        //    finally
        //    {
        //        if (con != null)
        //            con.Close();
        //    }
        //}

        //UPDATING ACCOUNT BALANCES FOR TRANSFER MONEY
        public static bool TMUpdateAccountBalances(string pTranNo)
        {
            SqlConnection con = null;
            try
            {
                //LOOP THRU EACH JOURNAL ENTRY TO UPDATE ACCOUNT BALANCE
                string lDebit = "0";
                string lCredit = "0";
                string lAcct = "";
                string sql = "";

                con = new SqlConnection(CommonClass.ConStr);
                sql = "SELECT j.AccountID, ISNULL(j.DebitAmount, 0) AS Debit, ISNULL(j.CreditAmount, 0) AS Credit, a.AccountClassificationID FROM Journal j INNER JOIN Accounts a ON j.AccountID = a.AccountID WHERE TransactionNumber = '" + pTranNo + "'";
                //SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow rw in dt.Rows)
                {
                    lAcct = rw["AccountID"].ToString();
                    lDebit = rw["Debit"].ToString();
                    lCredit = rw["Credit"].ToString();
                    sql = "UPDATE Accounts SET CurrentAccountBalance = CurrentAccountBalance ";
                    if (Convert.ToDouble(rw["Debit"].ToString()) == 0)
                    {
                        sql += " - " + lCredit;
                    }
                    else
                    {
                        sql += " + " + lDebit;
                    }

                    sql += " WHERE AccountID = '" + lAcct + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
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

        public static bool UpdateAccountBalances(string pTranNo)
        {
            SqlConnection con = null;
            try
            {
                //LOOP THRU EACH JOURNAL ENTRY TO UPDATE ACCOUNT BALANCE
                string lDebit = "0";
                string lCredit = "0";
                string lAcct = "";
                string sql = "";
                string lAccountClass = "";

                con = new SqlConnection(CommonClass.ConStr);
                sql = "SELECT j.AccountID, ISNULL(j.DebitAmount, 0) AS Debit, ISNULL(j.CreditAmount, 0) AS Credit, a.AccountClassificationID FROM Journal j INNER JOIN Accounts a ON j.AccountID = a.AccountID WHERE TransactionNumber IN ( '" + pTranNo + "', 'SYS-" + pTranNo + "')";
                //SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow rw in dt.Rows)
                {
                    lAcct = rw["AccountID"].ToString();
                    lDebit = (rw["Debit"].ToString() == "" ? "0" : rw["Debit"].ToString());
                    lCredit = (rw["Credit"].ToString() == "" ? "0" : rw["Credit"].ToString());
                    sql = "UPDATE Accounts SET CurrentAccountBalance = CurrentAccountBalance ";
                    lAccountClass = rw["AccountClassificationID"].ToString().Trim();
                    switch (lAccountClass)
                    {
                        case "A":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "L":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "COS":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "I":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "OI":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "EXP":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "OE":
                            sql += " + " + lDebit + " - " + lCredit;
                            break;
                        case "EQ":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                    }
                    sql += " WHERE AccountID = " + lAcct;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    
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

        public static bool ReverseAccountBalances(string pTranNo)
        {
            SqlConnection con = null;
            try
            {
                //LOOP THRU EACH JOURNAL ENTRY TO UPDATE ACCOUNT BALANCE
                string lDebit = "0";
                string lCredit = "0";
                string lAcct = "";
                string sql = "";
                string lAccountClass = "";

                con = new SqlConnection(CommonClass.ConStr);
                sql = "SELECT j.AccountID, ISNULL(j.DebitAmount, 0) AS Debit, ISNULL(j.CreditAmount, 0) AS Credit, a.AccountClassificationID FROM Journal j INNER JOIN Accounts a ON j.AccountID = a.AccountID WHERE TransactionNumber IN ('" + pTranNo + "', 'SYS-" + pTranNo + "')";
                //SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow rw in dt.Rows)
                {
                    lAcct = rw["AccountID"].ToString();
                    lDebit = (rw["Debit"].ToString() == "" ? "0" : rw["Debit"].ToString());
                    lCredit = (rw["Credit"].ToString() == "" ? "0" : rw["Credit"].ToString());
                    sql = "UPDATE Accounts SET CurrentAccountBalance = CurrentAccountBalance ";
                    lAccountClass = rw["AccountClassificationID"].ToString().Trim();
                    switch ((rw["AccountClassificationID"].ToString().Trim()))
                    {
                        case "A":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "L":
                            sql += " - " + lCredit + " + " + lDebit;
                            break;                      
                        case "COS":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "I":
                            sql += " - " + lCredit + " + " + lDebit;
                            break;
                        case "OI":
                            sql += " - " + lCredit + " + " + lDebit;
                            break;
                        case "EXP":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "OE":
                            sql += " - " + lDebit + " + " + lCredit;
                            break;
                        case "EQ":
                            sql += " - " + lCredit + " + " + lDebit;
                            break;
                    }

                    sql += " WHERE AccountID = " + lAcct;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
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

        public static int DeleteJournalEntries(string pOldTranNo)
        {
            SqlConnection con = null;
            try
            {
                //Reverse Journal Entry Record
                con = new SqlConnection(CommonClass.ConStr);

                string sql = "DELETE FROM Journal WHERE TransactionNumber IN ('" + pOldTranNo + "', 'SYS-" + pOldTranNo + "')";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                return cmd.ExecuteNonQuery();
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

        public static bool RunCmd(string pQry, SqlConnection pCon)
        {
            SqlConnection con = null;        
            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = con.CreateCommand();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
               
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        //public static bool CreateCurrentEarningsTran(string pTranNo)
        //{
        //    SqlConnection con = null;
        //    try
        //    {           
        //        string sql = "SELECT j.TransactionDate, j.EntryDate, j.Memo, j.AccountID, ISNULL(j.CreditAmount, 0) AS CreditAmount, ISNULL(j.DebitAmount, 0) AS DebitAmount, j.TransactionNumber, j.Type, j.JobID, a.AccountClassificationID FROM Journal j INNER JOIN Accounts a on j.AccountID = a.AccountID WHERE a.AccountClassificationID IN ('I','OI','EXP','OE','COS') AND TransactionNumber = '" + pTranNo + "'";
        //        con = new SqlConnection(CommonClass.ConStr);
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        con.Open();
        //        SqlDataAdapter da1 = new SqlDataAdapter();
        //        DataTable dt1 = new DataTable();
        //        da1.SelectCommand = cmd;
        //        da1.Fill(dt1);
        //        decimal lDebit = 0;
        //        decimal lCredit = 0;
        //        decimal lNet = 0;
        //        string lTranDateStr = "";
                
        //        foreach(DataRow rw in dt1.Rows)
        //        {
        //            lDebit = Convert.ToDecimal(rw["DebitAmount"].ToString());
        //            lCredit = Convert.ToDecimal(rw["CreditAmount"].ToString());
        //            lTranDateStr = Convert.ToDateTime(rw["TransactionDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
        //            if (rw["AccountClassificationID"].ToString() == "I" 
        //                || rw["AccountClassificationID"].ToString() == "OI")
        //            {
        //                lNet = lCredit - lDebit;
        //                //CREDIT CURRENT EARNINGS
        //                sql = @"INSERT INTO Journal(TransactionDate, Memo, AccountID, CreditAmount, TransactionNumber, Type, JobID)
        //                      VALUES('" + lTranDateStr + "','" + rw["Memo"].ToString() + "','" + CommonClass.DRowLA["CurrentEarningsID"].ToString() + "'," 
        //                      + lNet.ToString() + ",'SYS-" + rw["TransactionNumber"].ToString() + "','" + rw["Type"].ToString() + "'," + rw["JobID"].ToString() + ")";
        //            }
        //            else //"COS,EXP,OE"
        //            {
        //                lNet = lDebit - lCredit;
        //                //DEBIT CURRENT EARNINGS
        //                sql = @"INSERT INTO Journal(TransactionDate, Memo, AccountID, DebitAmount, TransactionNumber, Type, JobID)
        //                      VALUES('" + lTranDateStr + "','" + rw["Memo"].ToString() + "','" + CommonClass.DRowLA["CurrentEarningsID"].ToString() + "',"
        //                      + lNet.ToString() + ",'SYS-" + rw["TransactionNumber"].ToString() + "','" + rw["Type"].ToString() + "'," + rw["JobID"].ToString() + ")";

        //            }
        //            cmd.CommandText = sql;
        //            cmd.ExecuteNonQuery();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        if (con != null)
        //            con.Close();
        //    }
        //}
        public static void  UpdateProfileBalances(string ProfileID, decimal BalanceDue)
        {
            decimal CurBal = 0;
            SqlConnection con = null;
            try
            {
                string sql = "";

                con = new SqlConnection(CommonClass.ConStr);
                sql = "SELECT CurrentBalance FROM Profile WHERE ID =" + ProfileID;
                //SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
               
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow rw in dt.Rows)
                {
                    CurBal = Convert.ToDecimal(rw["CurrentBalance"].ToString());
                    CurBal = CurBal + BalanceDue;
                    sql = "UPDATE Profile SET CurrentBalance = "+ CurBal +"";
                    sql += " WHERE ID = " + ProfileID;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
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
        public static DataTable GetItem(string pItemID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = "SELECT i.*, q.OnHandQty, c.AverageCostEx from ( Items as i inner join ItemsQty as q on i.ID = q.ItemID ) inner join ItemsCostPrice as c on i.ID = c.ItemID where i.ID = " + pItemID;
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

    }
}
