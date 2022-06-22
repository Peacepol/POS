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
    public partial class RptPurchaseReconciliationDetails : Form
    {
        DataTable TbRep;
        public RptPurchaseReconciliationDetails()
        {
            InitializeComponent();
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReportReconciliationDetail();
        }

        private void LoadReportReconciliationDetail()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.TransactionDate,
                                p.PromiseDate, 
                                Profile.Name,
                                p.TotalDue, 
                                p.TransactionDate,
                                (SELECT CURRENT_TIMESTAMP) AS StarDate, 
                                p.SupplierINVNumber,
                                tp.Description,
                                c.Phone, 
                                c.ContactPerson, 
                                Profile.ID, 
                                c.ProfileID, 
                                Profile.ID, 
                                p.PurchaseID
                        FROM Purchases p 
                        INNER JOIN Profile ON p.SupplierID = Profile.ID 
                        LEFT JOIN Contacts c ON c.ProfileID = Profile.ID
                        LEFT JOIN TermsOfPayment tp ON tp.TermsOfPaymentID = Profile.TermsOfPayment
                        LEFT JOIN Terms t ON t.TermsID = p.TermsReferenceID
                        WHERE p.POStatus = 'Open' OR p.PurchaseType = 'ORDER'
                        AND c.Location = Profile.LocationID";
                if (cbAgeMethod.Text == "Number of Days since P.O. date")
                {
                    sql += " AND p.TransactionDate <= @edate ";
                }
                else
                {
                    sql += " AND t.ActualDueDate <= @edate ";
                }

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                DateTime edate = edateTimePicker.Value;
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                cmd.Parameters.AddWithValue("@edate", edate.ToUniversalTime());

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                TbRep = new DataTable();
                da.Fill(TbRep);

                Reports.ReportParams recondetails = new Reports.ReportParams();
                recondetails.PrtOpt = 1;
                recondetails.Rec.Add(TbRep);
                recondetails.ReportName = "PurchaseReconciliationDetails.rpt";
                recondetails.RptTitle = "Payables Reconciliation [Details]";

                recondetails.Params = "compname|asofDate";
                recondetails.PVals = CommonClass.CompName.Trim()+"|"+ edateTimePicker.Text;

                CommonClass.ShowReport(recondetails);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
