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

namespace AbleRetailPOS.Reports.PurchaseReports
{
    public partial class RptBillTransaction : Form
    {
        string ShippingID;
        bool promised = false;
        string EmployeeID;

        public RptBillTransaction()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void LoadReportBillTransaction()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT pur.PurchaseNumber, pur.TaxTotal, pur.TotalDue, pur.Memo, Pur.POStatus, pur.SupplierINVNumber,
                            pl.TransactionDate, pur.GrandTotal,
                            pay.Amount, pay.EntryDate, 
                            py.PaymentNumber,
                            pro.Name, pro.ProfileIDNumber
                            FROM Purchases pur 
                            INNER JOIN Profile pro ON pur.SupplierID = pro.ID
                            INNER JOIN PurchaseLines pl ON pur.PurchaseID = pl.PurchaseID
                            LEFT JOIN PaymentLines pay ON pay.EntityID = pur.PurchaseID
                            LEFT JOIN Payment py ON py.PaymentID = pay.PaymentID
                            Where pur.TransactionDate BETWEEN @sdate and @edate ";

                if (PurchaseStatuscb.Text != "")
                {
                    if (PurchaseStatuscb.Text == "All Bills")
                    {
                        sql += " AND pur.PurchaseType = 'BILL' ";
                    }
                    else if (PurchaseStatuscb.Text == "Open")
                    {
                        sql += " AND pur.POStatus  = 'Open' ";
                    }
                    else if (PurchaseStatuscb.Text == "Debit")
                    {
                        sql += " AND pur.POStatus  = 'Debit' ";
                    }
                    else if (PurchaseStatuscb.Text == "Closed")
                    {
                        sql += " AND pur.POStatus  = 'Closed' ";
                    }
                    else if (PurchaseStatuscb.Text == "Orders")
                    {
                        sql += " AND pur.POStatus = 'Order' ";
                    }
                    else if (PurchaseStatuscb.Text == "Quotes")
                    {
                        sql += " AND pur.POStatus = 'Quote' ";
                    }
                }
                if (txtShipVia.Text != "")
                {
                    sql += " AND pur.ShippingMethodID = " + ShippingID;
                }
                if (toNum.Value != 0)
                {
                    sql += " AND pur.GrandTotal BETWEEN '" + fromNum.Value + "' AND '" + toNum.Value + "'";
                }
                if (promised == true)
                {
                    sql += " AND pur.PromiseDate BETWEEN @psdate AND @pedate ";
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

                SqlCommand cmdtxseries = new SqlCommand("SELECT PurchaseBillPrefix, PaymentPrefix FROM TransactionSeries", con);
                da.SelectCommand = cmdtxseries;
                DataTable TbRep2 = new DataTable();
                da.Fill(TbRep2);

                ReportParams billtx = new ReportParams();
                billtx.PrtOpt = 1;
                billtx.Rec.Add(TbRep);
                billtx.Rec.Add(TbRep2);
                billtx.ReportName = "BillTransaction.rpt";
                billtx.RptTitle = "Bill Transactions [Accrual]";

                billtx.Params = "compname";
                billtx.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(billtx);
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
            LoadReportBillTransaction();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
