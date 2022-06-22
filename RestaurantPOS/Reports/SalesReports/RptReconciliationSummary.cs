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

namespace AbleRetailPOS.Reports.SalesReports
{
    public partial class RptReconciliationSummary : Form
    {
        private DataTable TbRep;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection con;
        string selectSql = "";
        private bool CanView = false;
        public RptReconciliationSummary()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
            }
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        void LoadReport()
        {
            selectSql = @"Select TotalDue, TransactionDate, Name, ActualDueDate, (SELECT CURRENT_TIMESTAMP ) AS StartDate FROM Sales 
                                    INNER JOIN Profile ON Sales.CustomerID = Profile.ID 
									LEFT JOIN Terms t ON t.TermsID = Sales.TermsReferenceID
									WHERE SalesType = 'ORDER' OR InvoiceStatus = 'Open'";
            if (cmbAegingMethod.Text != "")
            {
                if (cmbAegingMethod.Text == "Number of Days since P.O. date")
                {
                    selectSql += " AND TransactionDate <= @edate";
                }
                else if (cmbAegingMethod.Text == "Days override using Purchase Terms")
                {
                    selectSql += " AND ActualDueDate <= @edate";
                }
            }

            try
            {
                con = new SqlConnection(CommonClass.ConStr);
                cmd = new SqlCommand(selectSql, con);

                DateTime edate = edateTimePicker.Value;
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@edate", edate);
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);

                Reports.ReportParams AgeingDetail = new Reports.ReportParams();
                AgeingDetail.PrtOpt = 1;
                AgeingDetail.Rec.Add(TbRep);
                AgeingDetail.ReportName = "SalesReconciliationSummary.rpt";
                AgeingDetail.RptTitle = "Sales Reconciliation Summary";
                AgeingDetail.Params = "compname|asofDate";
                AgeingDetail.PVals = CommonClass.CompName.Trim()+"|"+ edateTimePicker.Text;

                CommonClass.ShowReport(AgeingDetail);
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RptReconciliationSummary_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
    }
}
