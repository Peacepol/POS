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

namespace AbleRetailPOS
{
    public partial class RecurringSetup : Form
    {
        private string mUserID;
        private string mTransactionName;
        private string mTransactionType;
        private Form mInvoker = null;

        public RecurringSetup(Form pSrcOfInvoker, string pTranName, string pTranType)
        {
            mTransactionName = pTranName;
            mTransactionType = pTranType;
            mInvoker = pSrcOfInvoker;
            InitializeComponent();
        }

        private void pbAccount_Click(object sender, EventArgs e)
        {
            SalespersonLookup UserDlg = new SalespersonLookup();
            if (UserDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lUsers = UserDlg.GetSalesperson;
                mUserID = lUsers[0];
                txtRemindUser.Text = lUsers[1];
            }
        }

        private void pbNotifyUser_Click(object sender, EventArgs e)
        {
            SalespersonLookup UserDlg = new SalespersonLookup();
            if (UserDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lUsers = UserDlg.GetSalesperson;
                mUserID = lUsers[0];
                txtNotifyUser.Text = lUsers[1];
            }
        }

        private void RecurringSetup_Load(object sender, EventArgs e)
        {
            cmbFrequency.SelectedIndex = 0;
            cmbRecTrans.SelectedIndex = 0;
            rdoIndefinitely.Checked = true;
            rdoRemind.Checked = true;
            txtTransactionName.Text = mTransactionName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mInvoker != null)
            {
                int lEntityID = 0;
                switch (mTransactionType)
                {
                    case "SI":
                    case "SO":
                    case "SQ":
                        ((Sales.EnterSales)mInvoker).SourceOfInvoke = CommonClass.InvocationSource.SAVERECURRING;
                        lEntityID = ((Sales.EnterSales)mInvoker).SaveSale(true);
                        break;
                    case "PQ":
                    case "PO":
                    case "PB":
                    case "RI":
                        ((Purchase.EnterPurchase)mInvoker).SourceOfInvoke = CommonClass.InvocationSource.SAVERECURRING;
                        lEntityID = ((Purchase.EnterPurchase)mInvoker).SavePurchase(true);
                        break;
                }
                SqlConnection con = null;
                try
                {
                    string sql = @"INSERT INTO Recurring (
                                        EntityID, 
                                        TranType, 
                                        StartDate, 
                                        EndDate, 
                                        Frequency, 
                                        LastPosted,";
                    if (rdoRemind.Checked && cmbRecTrans.Text != "never")
                        sql += @"NotifyUserID, NotifyDate, ";
                    sql += @"AutomaticRecord)
                                   VALUES (
                                        @EntityID, 
                                        @TranType, 
                                        @StartDate, 
                                        @EndDate, 
                                        @Frequency, 
                                        @LastPosted,";
                    if (rdoRemind.Checked && cmbRecTrans.Text != "never")
                        sql += "@NotifyUserID, @NotifyDate, ";
                    sql += "@AutomaticRecord)";

                    con = new SqlConnection(CommonClass.ConStr);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@EntityID", lEntityID);
                    cmd.Parameters.AddWithValue("@TranType", mTransactionType);
                    DateTime sdate = dtpStartOn.Value.ToUniversalTime();
                    sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                    cmd.Parameters.AddWithValue("@StartDate", sdate);
                    DateTime edate = DateTime.MaxValue;
                    if (rdoNumberOfTimes.Checked)
                    {
                        Int16 lNoOfTimes = Convert.ToInt16(txtNoOfTimes.Text);
                        switch (cmbFrequency.Text)
                        {
                            case "Daily":
                                edate = sdate.AddDays(lNoOfTimes);
                                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                                break;
                            case "Weekly":
                                edate = sdate.AddDays(7 * lNoOfTimes);
                                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                                break;
                            case "Monthly":
                                edate = sdate.AddMonths(lNoOfTimes);
                                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                                break;
                            case "Quarterly":
                                edate = sdate.AddMonths(3 * lNoOfTimes);
                                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                                break;
                            case "Every 6 Months":
                                edate = sdate.AddMonths(6 * lNoOfTimes);
                                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                                break;
                        }
                    }
                    else if(rdoUntilDate.Checked)
                    {
                        edate = dtpEdate.Value.ToUniversalTime();
                        edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                    }

                    cmd.Parameters.AddWithValue("@EndDate", edate);
                    cmd.Parameters.AddWithValue("@Frequency", cmbFrequency.Text);
                    cmd.Parameters.AddWithValue("@LastPosted", sdate);

                    if (rdoRemind.Checked && cmbRecTrans.Text != "never")
                    {
                        cmd.Parameters.AddWithValue("@NotifyUserID", Convert.ToInt32(mUserID));
                        switch (cmbRecTrans.Text)
                        {
                            case "on its due date":
                                cmd.Parameters.AddWithValue("@NotifyDate", sdate);
                                break;
                            case "#days in advance":
                                {
                                    DateTime timeNow = DateTime.UtcNow;
                                    int daysInAdvance = Convert.ToInt32(txtDaysInAdvance.Text);
                                    TimeSpan dateDiff = sdate - timeNow;
                                    DateTime notifdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                                    if (dateDiff.TotalDays >= daysInAdvance)
                                    {
                                        notifdate = notifdate.AddDays(daysInAdvance * -1);
                                    }
                                    else
                                    {
                                        MessageBox.Show(String.Format("#days in advance is earlier than today {0}Notification date is set today", Environment.NewLine));
                                    }
                                    cmd.Parameters.AddWithValue("@NotifyDate", notifdate);
                                }
                                break;
                        }
                    }
                    cmd.Parameters.AddWithValue("@AutomaticRecord", rdoDueAndNotify.Checked);
                    con.Open();
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected > 0)
                    {
                        MessageBox.Show("Recurring Transaction created successfully");
                    }
                    else
                    {
                        MessageBox.Show("No Recurring Transaction is created");
                    }
                    Close();
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
        }

        private void rdoUntilDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpEdate.Enabled = rdoUntilDate.Checked;
        }

        private void rdoNumberOfTimes_CheckedChanged(object sender, EventArgs e)
        {
            txtNoOfTimes.Enabled = rdoNumberOfTimes.Checked;
        }

        private void rdoRemind_CheckedChanged(object sender, EventArgs e)
        {
            cmbRecTrans.Enabled = rdoRemind.Checked;
            txtDaysInAdvance.Enabled = rdoRemind.Checked;
        }

        private void rdoDueAndNotify_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
