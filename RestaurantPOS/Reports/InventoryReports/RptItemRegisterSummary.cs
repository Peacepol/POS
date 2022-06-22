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

namespace AbleRetailPOS.Reports.InventoryReports
{
    public partial class RptItemRegisterSummary : Form
    {
        private bool CanView = false;
        public RptItemRegisterSummary()
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
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {
                string sql = @"SELECT ItemNumber,
                                    ItemName,
                                    OnHandQty,
                                    AverageCostEx,
                                    Level0 
                                FROM Items i 
                                INNER JOIN ItemsQty iq ON i.ID = iq.ItemID 
                                INNER JOIN ItemsCostPrice icp ON icp.ItemID = i.ID
                                INNER JOIN ItemsSellingPrice isp ON isp.ItemID = i.ID
                                INNER JOIN ItemTransaction itx ON itx.ItemID = i.ID
                                WHERE itx.TransactionDate <= @edate";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();

                DateTime edate = dtpAsOf.Value;
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59).ToUniversalTime();
                cmd.Parameters.AddWithValue("@edate", edate);

                da.Fill(TbRep);

                Reports.ReportParams itemregsmryparams = new Reports.ReportParams();
                itemregsmryparams.PrtOpt = 1;
                itemregsmryparams.Rec.Add(TbRep);

                itemregsmryparams.ReportName = "ItemRegisterSummary.rpt";
                itemregsmryparams.RptTitle = "Item Register Summary";

                itemregsmryparams.Params = "compname|asofdate";
                itemregsmryparams.PVals = CommonClass.CompName.Trim() + "|" + dtpAsOf.Value.ToString("yyyy-MM-dd");

                CommonClass.ShowReport(itemregsmryparams);
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

        private void RptItemRegisterSummary_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
