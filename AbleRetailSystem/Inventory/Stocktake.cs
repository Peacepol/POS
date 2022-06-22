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
using System.Globalization;


namespace RestaurantPOS.Inventory
{
    public partial class Stocktake : Form
    {
        private bool IsLoading = false;
        private string CurSeries = "";
        private string AdjNumber = "";
        private string thisFormCode = "";
        private bool CanView = false;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string ExpenseAccountID = "0";
        string SelItemID = "";

        public Stocktake()
        {
            InitializeComponent();
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                outx = false;
                FormRights.TryGetValue("Add", out outx);
                CanAdd = outx;
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                CanEdit = outx;
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                CanDelete = outx;
            }
        }

        private void Stocktake_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }
            foreach (DataGridViewColumn column in dgridItems.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.btnRecord.Enabled = CanAdd;
        }     

        private void btnRecord_Click(object sender, EventArgs e)
        {
            string[] lAdjAccount = new string[2];
            lAdjAccount[0] = ExpenseAccountID;
            lAdjAccount[1] = ExpenseAccountNumber.Text.Trim();
            string[] lCountedQty = new string[2];
            float lQty = 0;
            DataTable SelItems;
            SelItems = new DataTable();
            SelItems.Columns.Add("ItemID", typeof(string));
            SelItems.Columns.Add("PartNumber", typeof(string));
            SelItems.Columns.Add("ItemName", typeof(string));
            SelItems.Columns.Add("DiscrepancyQty", typeof(float));
            SelItems.Columns.Add("Cost", typeof(float));
            SelItems.Columns.Add("VarianceValue", typeof(float));
            SelItems.Columns.Add("AssetAccountID", typeof(string));
            SelItems.Columns.Add("AccountNumber", typeof(string));

            DataRow sr;
            for (int i = 0; i<this.dgridItems.Rows.Count; i++)
            {
                if(this.dgridItems.Rows[i].Cells["DiscrepancyQty"].Value != null)
                {
                    lQty = float.Parse(this.dgridItems.Rows[i].Cells["DiscrepancyQty"].Value.ToString());
                    if(lQty != 0)
                    {
                        sr = SelItems.NewRow(); 
                        sr["ItemID"] = this.dgridItems.Rows[i].Cells["ItemID"].Value.ToString();
                        sr["PartNumber"] = this.dgridItems.Rows[i].Cells["PartNumber"].Value.ToString();
                        sr["ItemName"] = this.dgridItems.Rows[i].Cells["ItemName"].Value.ToString();
                        sr["DiscrepancyQty"] = float.Parse(this.dgridItems.Rows[i].Cells["DiscrepancyQty"].Value.ToString());                        
                        sr["Cost"] = float.Parse(this.dgridItems.Rows[i].Cells["Cost"].Value.ToString());
                        sr["VarianceValue"] = float.Parse(this.dgridItems.Rows[i].Cells["VarianceValue"].Value.ToString());
                        sr["AssetAccountID"] = this.dgridItems.Rows[i].Cells["AssetAccountID"].Value.ToString();
                        sr["AccountNumber"] = this.dgridItems.Rows[i].Cells["AccountNumber"].Value.ToString();

                        SelItems.Rows.Add(sr);
                    }
                }
            }
            if(SelItems.Rows.Count > 0)
            {
                StockAdjustments StockAdj = new StockAdjustments(CommonClass.InvocationSource.STOCKTAKE, SelItems, lAdjAccount, "", this.chkInitial.Checked);
                if (StockAdj.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Stocktake completed successfully.");
                    this.Close();
                }
            }
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            LoadAllCountableItems();
        }


        private void LoadAllCountableItems()
        {
            string sql = @"SELECT q.ItemID, i.PartNumber, i.ItemName, q.OnHandQty, q.OnHandQty as CountedQty, 0 as DiscrepancyQty, c.AverageCostEx as UnitCost, q.OnHandQty * c.AverageCostEx as StockValue, 0 as VarianceValue, AssetAccountID,  AssetAccountID as AccountNumber
                            FROM ((Items as i inner join ItemsQty as q on i.ID = q.ItemID)  inner join ItemsCostPrice as c on i.ID = c.ItemID ) where i.IsCounted = 1 ";

            if(SelItemID != "")
            {
                sql += " AND i.ID in ("+SelItemID+")";
            }
            DataTable ltb = new DataTable();
            CommonClass.runSql(ref ltb, sql);

            string[] lrow;
            for(int i =0; i < ltb.Rows.Count; i++)
            {
                lrow = new string[11];
                lrow[0] = ltb.Rows[i][0].ToString();
                lrow[1] = ltb.Rows[i][1].ToString();
                lrow[2] = ltb.Rows[i][2].ToString();
                lrow[3] = ltb.Rows[i][3].ToString();
                lrow[4] = ltb.Rows[i][4].ToString();
                lrow[5] = ltb.Rows[i][5].ToString();
                lrow[6] = ltb.Rows[i][6].ToString();
                lrow[7] = ltb.Rows[i][7].ToString();
                lrow[8] = ltb.Rows[i][8].ToString();
                lrow[9] = ltb.Rows[i][9].ToString();
                lrow[10] = ltb.Rows[i][10].ToString();
                this.dgridItems.Rows.Add(lrow);

            }
            this.dgridItems.Refresh();
        }

        private void dgridItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvrow = dgridItems.CurrentRow;
            if (e.RowIndex < 0)
                return;

            switch (e.ColumnIndex)
            {
                case 1: //PartNumber
                    ShowItemLookup("");
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    
                    break;               
                case 4:
                    this.dgridItems.CurrentCell = this.dgridItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgridItems.BeginEdit(true);
                    break;               
                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }

        private void dgridItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 1 || e.ColumnIndex == 4)
            {
                this.dgridItems.CurrentCell = this.dgridItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.dgridItems.BeginEdit(true);
            }

        }

        private void dgridItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 //Unit Cost
               || e.ColumnIndex == 7 //Stock Value
               || e.ColumnIndex == 8//Variance Value
               && e.RowIndex != this.dgridItems.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgridItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colindex = (int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex);

            e.Control.KeyPress -= Numeric_KeyPress;

            if (colindex == 4)
            {
                e.Control.KeyPress += Numeric_KeyPress;
            }
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
               && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // only allow one negative char before the number
            if (e.KeyChar == '-'
                && (sender as TextBox).Text.IndexOf('-') == 0)
            {
                e.Handled = true;
            }
        }

        private void dgridItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                Recalcline(e.ColumnIndex, e.RowIndex);               

            }
       
            if (e.RowIndex == (this.dgridItems.Rows.Count - 1))
            {
                this.dgridItems.Rows.Add();
            }
        }

        public void ShowItemLookup(string itemNum)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum, "", "PartNumber");
            DataGridViewRow dgvRows = dgridItems.CurrentRow;
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                dgvRows.Cells["ItemID"].Value = ItemRows.Cells[0].Value.ToString();
                dgvRows.Cells["PartNumber"].Value = ItemRows.Cells[2].Value;
                dgvRows.Cells["Description"].Value = ItemRows.Cells[3].Value.ToString();
                dgvRows.Cells["UnitCost"].Value = Math.Round(Convert.ToDouble(ItemRows.Cells["AverageCostEx"].Value.ToString()), 2);
            }
        }

        private void Recalcline(int pColIndex, int pRowIndex)
        {
            if (pRowIndex < 0)
                return;

            if (!IsLoading)
            {
                DataGridViewRow dgvRows = dgridItems.Rows[pRowIndex];

                float lUnitCost = 0;
                float lVarianceValue = 0;
                float lStockValue = 0;
                float lOnHand = 0;
                float lCounted = 0;             
                float lVarQty = 0;

                if (dgvRows.Cells["Cost"].Value != null && dgvRows.Cells["NewCountQty"].Value != null)
                {
                    lOnHand = (dgvRows.Cells["OnHandQty"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["OnHandQty"].Value.ToString()));
                    lCounted = (dgvRows.Cells["NewCountQty"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["NewCountQty"].Value.ToString()));
                    lUnitCost = (dgvRows.Cells["Cost"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["Cost"].Value.ToString(), NumberStyles.Currency));
                    lStockValue = lCounted * lUnitCost;
                    lVarQty = lCounted - lOnHand;
                    lVarianceValue = lVarQty * lUnitCost;
                    dgvRows.Cells["StockValue"].Value = lStockValue;
                    dgvRows.Cells["DiscrepancyQty"].Value = lVarQty;
                    dgvRows.Cells["VarianceValue"].Value = lVarianceValue;
                }
            }
        }

        private void ExpenseAccountNumber_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            DataTable lTb = new DataTable();
            lTb.Columns.Add("ItemID", typeof(string));
            lTb.Columns.Add("PartNumber", typeof(string));
            lTb.Columns.Add("ItemName", typeof(string));
            lTb.Columns.Add("OnHandQty", typeof(float));
            lTb.Columns.Add("CountedQty", typeof(float));
            lTb.Columns.Add("DiscrepancyQty", typeof(float));
            lTb.Columns.Add("UnitCost", typeof(float));
            lTb.Columns.Add("StockValue", typeof(float));
            lTb.Columns.Add("VarianceValue", typeof(float));

            DataRow rw;
            for(int i = 0; i < this.dgridItems.Rows.Count; i++)
            {
                if(dgridItems.Rows[i].Cells["ItemID"].Value != null)
                {
                    if (dgridItems.Rows[i].Cells["ItemID"].Value.ToString() != "")
                    {
                        rw = lTb.NewRow();
                        rw["ItemID"] = dgridItems.Rows[i].Cells["ItemID"].Value.ToString();
                        rw["PartNumber"] = dgridItems.Rows[i].Cells["PartNumber"].Value.ToString();
                        rw["ItemName"] = dgridItems.Rows[i].Cells["ItemName"].Value.ToString();
                        rw["OnHandQty"] = float.Parse(dgridItems.Rows[i].Cells["OnHandQty"].Value.ToString());
                        rw["CountedQty"] = float.Parse(dgridItems.Rows[i].Cells["NewCountQty"].Value.ToString());
                        rw["DiscrepancyQty"] = float.Parse(dgridItems.Rows[i].Cells["DiscrepancyQty"].Value.ToString());
                        rw["UnitCost"] = float.Parse(dgridItems.Rows[i].Cells["Cost"].Value.ToString(), NumberStyles.Currency);
                        rw["StockValue"] = float.Parse(dgridItems.Rows[i].Cells["StockValue"].Value.ToString(), NumberStyles.Currency);
                        rw["VarianceValue"] = float.Parse(dgridItems.Rows[i].Cells["VarianceValue"].Value.ToString(), NumberStyles.Currency);
                        lTb.Rows.Add(rw);
                    }                    
                }          
            }
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");
            Reports.ReportParams registerlistparams = new Reports.ReportParams();
            registerlistparams.PrtOpt = 1;
            if (lTb.Rows.Count > 0)
            {
                registerlistparams.Rec.Add(lTb);
            }

            registerlistparams.ReportName = "StocktakeVariance.rpt";
            registerlistparams.RptTitle = "Stocktake Variance Report";

            registerlistparams.Params = "compname|stocktakedate";
            registerlistparams.PVals = CommonClass.CompName.Trim() + "|" + sdate;

            CommonClass.ShowReport(registerlistparams);
        }

        private void btnSpecificItems_Click(object sender, EventArgs e)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.STOCKTAKE, "", "", "PartNumber");
            if (Items.ShowDialog() == DialogResult.OK)
            {
                string dr = Items.GetitemIDs;
                if(dr != null)
                {
                    SelItemID = dr;
                    LoadAllCountableItems();
                }
            }
        }

      
    }
}
