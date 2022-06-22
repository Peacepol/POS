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
    public partial class RptAgeingDetail : Form
    {
        DataTable TbRep;
        public RptAgeingDetail()
        {
            InitializeComponent();
            edateTimePicker.Value = DateTime.Today.AddDays(-(DateTime.Today.Day) + (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)));
        }

        private void LoadReportAgeingDetail()
        {
            SqlConnection con = null;
            string sql = "";
            try
            {
                sql = @"SELECT p.EntryDate,
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
                        INNER JOIN Contacts c ON c.ContactID = Profile.ID
                        INNER JOIN TermsOfPayment tp ON tp.TermsOfPaymentID = Profile.TermsOfPayment
                        LEFT JOIN Terms t ON t.TermsID = p.TermsReferenceID
                        WHERE p.POStatus = 'Open' OR p.PurchaseType = 'ORDER'";
                if (cbAgeMethod.Text == "Number of Days since P.O. date")
                {
                    sql += " AND p.TransactionDate <= @edate ";
                }
                else
                {
                    sql += " AND ActualDueDate <= @edate ";
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

                Reports.ReportParams AgeingDetail = new Reports.ReportParams();
                AgeingDetail.PrtOpt = 1;
                AgeingDetail.Rec.Add(TbRep);
                AgeingDetail.ReportName = "AgePayableDetails.rpt";
                AgeingDetail.RptTitle = "Aged Payables [Detail]";

                AgeingDetail.Params = "compname";
                AgeingDetail.PVals = CommonClass.CompName.Trim();

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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadReportAgeingDetail();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
