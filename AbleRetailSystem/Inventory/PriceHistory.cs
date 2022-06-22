using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Inventory
{
    public partial class PriceHistory : Form
    {
        DataTable tbHistory;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        private string thisFormCode = "";
        public PriceHistory(DataTable TbRef = null)
        {
            tbHistory = TbRef;
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                FormRights.TryGetValue("Add", out outx);
                CanAdd = outx;
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                CanEdit = outx;
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                CanDelete = outx;
            }
            string outy = "";
            CommonClass.AppFormCode.TryGetValue(this.Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
        }
        public DataTable GetPrices
        {
            get { return tbHistory; }
        }

        private void PriceHistory_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            button1.Enabled = CanAdd;
        }
        private void LoadPriceChanges()
        {
            dgPriceChanges.Rows.Clear();
            Dictionary<string, object> param = new Dictionary<string, object>();
            DataTable dt = new DataTable();
            string pricechangesql = @"SELECT p.*, user_fullname, ItemName FROM PriceChange p 
INNER JOIN Users u ON u.user_id = p.UserID
INNER JOIN Items i ON i.ID = p.ItemID 
                 WHERE ChangeDate BETWEEN @sDate AND @eDate ";
            DateTime sdate = sDate.Value.ToUniversalTime();
            DateTime edate = eDate.Value.ToUniversalTime();
            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
            param.Add("@sDate", sdate);
            param.Add("@eDate", edate);
            CommonClass.runSql(ref dt, pricechangesql, param);
            if (dt.Rows.Count > 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dx = dt.Rows[x];
                    dgPriceChanges.Rows.Add();
                    dgPriceChanges.Rows[x].Cells[0].Value = dx["ItemID"].ToString();
                    dgPriceChanges.Rows[x].Cells[1].Value = "false";
                    dgPriceChanges.Rows[x].Cells[2].Value = dx["ItemName"].ToString();
                    dgPriceChanges.Rows[x].Cells[3].Value = dx["PriceBefore"].ToString();
                    dgPriceChanges.Rows[x].Cells[4].Value = dx["PriceAfter"].ToString();
                    dgPriceChanges.Rows[x].Cells[5].Value = dx["PriceLevel"].ToString();
                    dgPriceChanges.Rows[x].Cells[6].Value = dx["CalcMethod"].ToString();
                    dgPriceChanges.Rows[x].Cells[7].Value = dx["PercentChange"].ToString();
                    dgPriceChanges.Rows[x].Cells[8].Value = dx["user_fullname"].ToString();
                    dgPriceChanges.Rows[x].Cells[9].Value = Convert.ToDateTime(dx["ChangeDate"].ToString()).ToShortDateString();

                }
            }
            else
            {
                MessageBox.Show("Contains no data.", "Information");
            }
        }

        private void dgPriceChanges_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 //Price
           || e.ColumnIndex == 4 //Amount
           && e.RowIndex != this.dgPriceChanges.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
            if (e.ColumnIndex == 7
             && e.RowIndex != this.dgPriceChanges.NewRowIndex)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    string p = e.Value.ToString().Replace("%", "");
                    float d = float.Parse(p);
                    e.Value = Math.Round(d, 2).ToString() + "%";
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadPriceChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] RowArray;
            foreach (DataGridViewRow dgvr in dgPriceChanges.Rows)
            {
                if (bool.Parse(dgvr.Cells["Selected"].Value.ToString()))
                {
                    RowArray = new string[2];
                    RowArray[0] = dgvr.Cells["ItemID"].Value.ToString();
                    RowArray[1] = dgvr.Cells["ItemName"].Value.ToString();
                    tbHistory.Rows.Add(RowArray);
                }
            }
            DialogResult = DialogResult.OK;
        }
    }
}
