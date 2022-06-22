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

namespace RestaurantPOS.Reports.PurchaseReports
{
    public partial class RptPurchasePayablesJournal : Form
    {
        string ShippingID;
        bool promised = false;
        string EmployeeID;
        public RptPurchasePayablesJournal()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }
        private void LoadReport()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.PurchaseNumber, p.TransactionDate,
                        j.Memo, j.DebitAmount, j.CreditAmount,
                        o.JobCode
                        FROM Purchases p 
                        INNER JOIN Profile pro ON p.SupplierID = pro.ID
                        INNER JOIN PurchaseLines pl ON p.PurchaseID = pl.PurchaseID                        
                        INNER JOIN Journal j ON j.TransactionNumber = p.PurchaseNumber
						INNER JOIN Jobs o ON o.JobID = pl.JobID Where p.TransactionDate BETWEEN @sdate and @edate ";

                if (PurchaseStatuscb.Text != "")
                {
                    if (PurchaseStatuscb.Text == "Purchases")
                    {
                        sql += " AND p.PurchaseType = 'BILL' ";
                    }
                    else if (PurchaseStatuscb.Text == "Open")
                    {
                        sql += " AND p.POStatus  = 'Open' ";
                    }
                    else if (PurchaseStatuscb.Text == "Debit")
                    {
                        sql += " AND p.POStatus  = 'Debit' ";
                    }
                    else if (PurchaseStatuscb.Text == "Closed")
                    {
                        sql += " AND p.POStatus  = 'Closed' ";
                    }
                    else if (PurchaseStatuscb.Text == "Orders")
                    {
                        sql += " AND p.POStatus = 'Order' ";
                    }
                    else if (PurchaseStatuscb.Text == "Quotes")
                    {
                        sql += " AND p.POStatus = 'Quote' ";
                    }
                }
                if (toNum.Value != 0)
                {
                    sql += " AND p.GrandTotal BETWEEN '" + fromNum.Value + "' AND '" + toNum.Value + "'";
                }
                if (promised == true)
                {
                    sql += " AND p.PromiseDate BETWEEN @psdate AND @pedate ";
                }
                if (txtEmployee.Text != "")
                {
                    sql += " AND UserID = " + EmployeeID;
                }
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                DateTime sdate = sdateTimePicker.Value;
                DateTime edate = edateTimePicker.Value;
                DateTime psdate = PSdateTimePicker.Value;
                DateTime pedate = PEdateTimePicker.Value;

                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@sdate", sdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@psdate", psdate.ToUniversalTime());
                cmd.Parameters.AddWithValue("@pedate", pedate.ToUniversalTime());

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);

                ReportParams AllPurchases = new ReportParams();
                AllPurchases.PrtOpt = 1;
                AllPurchases.Rec.Add(TbRep);
                AllPurchases.ReportName = "PurchasesPayablesJournal.rpt";
                AllPurchases.RptTitle = "Purchases & Payables Journal";

                AllPurchases.Params = "compname";
                AllPurchases.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(AllPurchases);
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
