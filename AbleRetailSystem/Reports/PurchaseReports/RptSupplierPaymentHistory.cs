﻿using System;
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
    public partial class RptSupplierPaymentHistory : Form
    {
        DataTable TbRep;
        string JobID;
        bool promised = false;
        public RptSupplierPaymentHistory()
        {
            InitializeComponent();
            sdateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + 1);
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void LoadReportPaymentHistory()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.PurchaseNumber, p.SupplierINVNumber, p.POStatus, 
                        j.Name, 
                        p.TransactionDate as DateofTransaction, pl.Amount,
                        pay.TransactionDate as PaymentDate, pay.PaymentNumber, pay.TotalAmount
                        FROM (Purchases p Inner join Profile j on p.SupplierID = j.ID) 
                        Inner Join PaymentLines pl on pl.EntityID = p.PurchaseID 
                        Inner join Payment pay on pay.PaymentID = pl.PaymentID 
                        WHERE  pay.PaymentFor = 'Purchase' AND p.TransactionDate BETWEEN @sdate AND @edate 
                        ";
                if ( toNum.Value != 0)
                {
                    sql += " AND p.GrandTotal BETWEEN '"+ fromNum.Value +"' AND '" + toNum.Value + "'" ;
                }
                if (promised == true)
                {
                    sql += " AND p.PromiseDate  BETWEEN @psdate AND @pedate ";
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
                TbRep = new DataTable();
                da.Fill(TbRep);
                Reports.ReportParams PaymentHistory = new Reports.ReportParams();
                PaymentHistory.PrtOpt = 1;
                PaymentHistory.Rec.Add(TbRep);
                PaymentHistory.ReportName = "SupplierPaymentHistory.rpt";
                PaymentHistory.RptTitle = "Supplier Payment History";

                PaymentHistory.Params = "compname";
                PaymentHistory.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(PaymentHistory);
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

        private void pbJobs_Click(object sender, EventArgs e)
        {
            ShowJobLookup(JobTb.Text);
        }
        public void ShowJobLookup(string jobSearch = "") //Jobs
        {
            SelectJobs DlgJob = new SelectJobs("D", jobSearch);

            if (DlgJob.ShowDialog() == DialogResult.OK)
            {
                string[] Jobs = DlgJob.GetJob;
                JobID = Jobs[0].ToString();
                JobTb.Text = Jobs[2].ToString();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReportPaymentHistory();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DateRangesStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRangesStart.SelectedIndex != 0)
            {
                DateRangesEnd.SelectedIndex = DateRangesStart.SelectedIndex;
                PSdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex, 1, 00, 00, 00);
                PEdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesEnd.SelectedIndex, (DateTime.DaysInMonth(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex)), 23, 59, 59);
                promised = true;
            }
            if (DateRangesStart.SelectedIndex == 0)
            {
                promised = false;
            }
        }

        private void DateRangesEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DateRangesEnd.SelectedIndex != 0)
            {
                PEdateTimePicker.Value = new DateTime(PSdateTimePicker.Value.Year, DateRangesEnd.SelectedIndex, (DateTime.DaysInMonth(PSdateTimePicker.Value.Year, DateRangesStart.SelectedIndex)), 23, 59, 59);
            }
            else
            {
                promised = false;
            }
        }
    }
}
