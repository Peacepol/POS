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

namespace AbleRetailPOS.Inventory
{
    public partial class BuildItems : Form
    {
        bool IsLoading = true;
        private string CurSeries = "";
        private string AdjNumber = "";

        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanView = false;
        private bool CanEdit = false;
        private bool CanDelete = false;

        CommonClass.InvocationSource SrcOfInvoke;
        private DataTable ToBuildITems;
        public BuildItems(CommonClass.InvocationSource pSrcInvoke = CommonClass.InvocationSource.SELF, DataTable pAutoBuild= null, string pAdjNo = "")
        {
            InitializeComponent();
          
            SrcOfInvoke = pSrcInvoke;
            ToBuildITems = pAutoBuild;
            AdjNumber = pAdjNo;
            thisFormCode = this.Text;
            Dictionary<string, bool> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            bool outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("View", out outx);
                CanView = outx;
                FormRights.TryGetValue("Add", out outx);
                CanAdd = outx;
                btnRecord.Enabled = CanAdd;
                FormRights.TryGetValue("Edit", out outx);
                CanEdit = outx;
                FormRights.TryGetValue("Delete", out outx);
                CanDelete = outx;
            }
        }

        void PopulateDataGridView()
        {
            IsLoading = true;
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            dgridItems.Rows.Add();
            IsLoading = false;
        }



        private void dgridItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2
                || e.ColumnIndex == 3
                || e.ColumnIndex == 4)
            {
                Recalcline(e.ColumnIndex, e.RowIndex);
                CalcOutOfBalance();

            }

            if (e.ColumnIndex == 0) //PartNumber
            {
                //ClearRowItems(e.RowIndex);
                if (dgridItems.CurrentCell.Value != null)
                {
                    ShowItemLookup(dgridItems.CurrentCell.Value.ToString(), "PartNumber");
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                }
                else
                {
                    ShowItemLookup("", "PartNumber");
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                }
            }
            else if (e.ColumnIndex == 5)//Account Number
            {
                //ClearRowItems(e.RowIndex);
                if (dgridItems.CurrentCell.Value != null)
                {
                    //ShowAccountLookup(dgridItems.CurrentCell.Value.ToString());
                }
                else
                {
                    //ShowAccountLookup("");
                }
            }
            else if (e.ColumnIndex == 6)//Job
            {
                if (dgridItems.CurrentCell.Value != null)
                {
                    ShowJobLookup(dgridItems.CurrentCell.Value.ToString());
                }
            }

            if (e.RowIndex == (this.dgridItems.Rows.Count - 1))
            {
                this.dgridItems.Rows.Add();
            }


        }
        private void ClearRowItems(int pRowIndex)
        {
            dgridItems.Rows[pRowIndex].Cells["Description"].Value = null;
            dgridItems.Rows[pRowIndex].Cells["Price"].Value = null;
            dgridItems.Rows[pRowIndex].Cells["Discount"].Value = null;
            dgridItems.Rows[pRowIndex].Cells["Amount"].Value = null;
            dgridItems.Rows[pRowIndex].Cells["Job"].Value = null;

        }

        private void dgridItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvrow = dgridItems.CurrentRow;
            if (e.RowIndex < 0)
                return;

            switch (e.ColumnIndex)
            {
                case 0: //PartNumber
                    ShowItemLookup("", "PartNumber");
                    Recalcline(e.ColumnIndex, e.RowIndex);
                    CalcOutOfBalance();
                    break;
                case 2:
                case 3:
                case 4:
                    this.dgridItems.CurrentCell = this.dgridItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.dgridItems.BeginEdit(true);
                    break;
                case 5: //GL ACCOUNT
                    //ShowAccountLookup("");
                    break;
                case 6://Job
                    ShowJobLookup();
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
            if (e.ColumnIndex < 8)
            {
                this.dgridItems.CurrentCell = this.dgridItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.dgridItems.BeginEdit(true);
            }

        }

        private void dgridItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 //Price
           || e.ColumnIndex == 4 //Amount
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

            if (colindex == 2 || colindex == 3 || colindex == 4)
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


        //public void ShowAccountLookup(string accountnum) //Accounts
        //{
        //    AccountLookup AcctDlg = new AccountLookup("", accountnum);
        //    DataRow dr;
        //    DataGridViewRow dgvRows = dgridItems.CurrentRow;
        //    if (AcctDlg.ShowDialog() == DialogResult.OK)
        //    {
        //        dr = AcctDlg.GetAcct;
        //        dgvRows.Cells["AccountID"].Value = dr["AccountID"].ToString();
        //        dgvRows.Cells["Account"].Value = dr["AccountNumber"].ToString();

        //    }
        //    else
        //    {
        //        dgvRows.Cells["AccountNumber"].Value = "";
        //    }
        //}

        public void ShowJobLookup(string jobSearch = "") //Jobs
        {
            SelectJobs DlgJob = new SelectJobs("D", jobSearch);
            DataGridViewRow dgvRows = dgridItems.CurrentRow;
            if (DlgJob.ShowDialog() == DialogResult.OK)
            {
                string[] Jobs = DlgJob.GetJob;
                dgvRows.Cells["JobID"].Value = Jobs[0];
                dgvRows.Cells["Job"].Value = Jobs[2];
            }
            else
            {
                dgvRows.Cells["Job"].Value = "";
            }
        }



        public void ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum, "", whereCon);
            DataGridViewRow dgvRows = dgridItems.CurrentRow;
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                dgvRows.Cells["ItemID"].Value = ItemRows.Cells[0].Value.ToString();
                dgvRows.Cells["PartNumber"].Value = ItemRows.Cells[2].Value;
                dgvRows.Cells["Description"].Value = ItemRows.Cells[3].Value.ToString();
                dgvRows.Cells["UnitCost"].Value = Math.Round(Convert.ToDouble(ItemRows.Cells["AverageCostEx"].Value.ToString()),2);

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
                float lTotalCost = 0;
                float lQty = 0;

                if (pColIndex != 4) //UNIT COST & QTY
                {
                    if (dgvRows.Cells["UnitCost"].Value != null && dgvRows.Cells["Quantity"].Value != null)
                    {
                        lQty = (dgvRows.Cells["Quantity"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["Quantity"].Value.ToString()));
                        lUnitCost = (dgvRows.Cells["UnitCost"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["UnitCost"].Value.ToString(), NumberStyles.Currency));
                        lTotalCost = lQty * lUnitCost;
                        dgvRows.Cells["Amount"].Value = lTotalCost;

                    }
                }
                else //(pColIndex ==4) //AMOUNT
                {
                    if (dgvRows.Cells["Amount"].Value != null && dgvRows.Cells["Quantity"].Value != null)
                    {
                        lQty = (dgvRows.Cells["Quantity"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["Quantity"].Value.ToString()));
                        lTotalCost = (dgvRows.Cells["Amount"].Value.ToString() == "" ? 0 : float.Parse(dgvRows.Cells["Amount"].Value.ToString(), NumberStyles.Currency));
                        lUnitCost = lTotalCost / lQty;
                        dgvRows.Cells["UnitCost"].Value = lUnitCost;

                    }

                }

            }
        }
        void CalcOutOfBalance()
        {
            this.txtOutOfBalance.Value = 0;
            float CurAmt = 0;
            for (int i = 0; i < this.dgridItems.Rows.Count; i++)
            {
                if (this.dgridItems.Rows[i].Cells["Amount"].Value != null)
                {
                    if (this.dgridItems.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        CurAmt = float.Parse(this.dgridItems.Rows[i].Cells["Amount"].Value.ToString(), NumberStyles.Currency);
                        this.txtOutOfBalance.Value += (decimal)CurAmt;
                    }
                }
            }


        }

        private bool CheckAccount()
        {
            bool lRet = true;
            for (int i = 0; i < this.dgridItems.Rows.Count; i++)
            {
                if (this.dgridItems.Rows[i].Cells["PartNumber"].Value != null)
                {
                    if (this.dgridItems.Rows[i].Cells["AccountID"].Value == null)
                    {
                        lRet = false;
                        break;
                    }
                    else if (this.dgridItems.Rows[i].Cells["AccountID"].Value.ToString() == "")
                    {
                        lRet = false;
                        break;
                    }
                }
            }
            return lRet;
        }
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (CheckAccount())
            {
                RecordAdjustment();

            }
            else
            {
                MessageBox.Show("Please supply all Account Number to be used for adjustment.");
            }

        }
        private void GenerateAdjNum()
        {
            DataTable dt = new DataTable();
                string selectSql_ua = @"SELECT BuildItemsSeries, 
                                               BuildItemsPrefix 
                                        FROM TransactionSeries";
              
                string lSeries = "";
                int lCnt = 0;
                int lNewSeries = 0;
            CommonClass.runSql(ref dt , selectSql_ua);
                
                    if(dt.Rows.Count > 0)
                        {
                             DataRow dr = dt.Rows[0];
                            lSeries = (dr["BuildItemsSeries"].ToString());
                            lCnt = lSeries.Length;
                            lSeries = lSeries.TrimStart('0');
                            lSeries = (lSeries == "" ? "0" : lSeries);
                            lNewSeries = Convert.ToInt16(lSeries) + 1;
                            CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                            AdjNumber = (dr["BuildItemsPrefix"].ToString()).Trim(' ') + CurSeries;
                        }
                    else
                    {
                        MessageBox.Show("Transaction Series Numbers not setup properly.");
                        this.BeginInvoke(new MethodInvoker(Close));
                    }

         
        }

        private void RecordAdjustment()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            GenerateAdjNum();
            if (AdjNumber != "")
            {
                string savesql = @"INSERT INTO ItemsAdjustment(LocationID, ItemAdjNumber, TransactionDate,
                        UserID, Memo, Type) VALUES(@LocationID, @ItemAdjNumber, @TransactionDate,
                        @UserID, @Memo, @Type);SELECT SCOPE_IDENTITY()";
                    
                param.Add("@LocationID", 1);
                param.Add("@ItemAdjNumber", AdjNumber);
                param.Add("@TransactionDate", this.trandate.Value.ToUniversalTime());
                param.Add("@UserID", CommonClass.UserID);
                param.Add("@Memo", this.txtMemo.Text);
                param.Add("@Type", "IB");

                int ItemAdjID = CommonClass.runSql(savesql, CommonClass.RunSqlInsertMode.SCALAR, param);

                string lItemID = "";
                float lQty = 0;
                float lUnitCostEx = 0;
                float lAmountEx = 0;
                string lAccountID = "";
                string lJobID = "";
                string lLineMemo = "";
                int lcount = 0;
                for (int i = 0; i < this.dgridItems.Rows.Count; i++)
                {
                    if (this.dgridItems.Rows[i].Cells["PartNumber"].Value != null)
                    {
                        if (this.dgridItems.Rows[i].Cells["PartNumber"].Value.ToString() != "")
                        {
                            lItemID = (this.dgridItems.Rows[i].Cells["ItemID"].Value.ToString() == "" ? "0" : this.dgridItems.Rows[i].Cells["ItemID"].Value.ToString());                              
                            lAccountID = "0";
                            lJobID = "0";
                            lJobID =( this.dgridItems.Rows[i].Cells["JobID"].Value == null ? "0" : this.dgridItems.Rows[i].Cells["JobID"].Value.ToString());
                            lLineMemo = "";
                            lLineMemo = (this.dgridItems.Rows[i].Cells["LineMemo"].Value == null ? "" : this.dgridItems.Rows[i].Cells["ItemID"].Value.ToString());
                            lQty = (this.dgridItems.Rows[i].Cells["Quantity"].Value.ToString() == "" ? 0 : float.Parse(this.dgridItems.Rows[i].Cells["Quantity"].Value.ToString()));
                            lUnitCostEx = (this.dgridItems.Rows[i].Cells["UnitCost"].Value.ToString() == "" ? 0 : float.Parse(this.dgridItems.Rows[i].Cells["UnitCost"].Value.ToString()));
                            lAmountEx = (this.dgridItems.Rows[i].Cells["Amount"].Value.ToString() == "" ? 0 : float.Parse(this.dgridItems.Rows[i].Cells["Amount"].Value.ToString()));

                            savesql = @"INSERT INTO ItemsAdjustmentLines(ItemAdjID, ItemID, Qty, UnitCostEx, AmountEx, AccountID, JobID, LineMemo) ";
                            savesql += "VALUES (" + ItemAdjID + "," + lItemID  + "," + lQty + "," + lUnitCostEx + "," + lAmountEx + "," + lAccountID + "," + lJobID + ", @LineMemo)";
                            Dictionary<string, object> paramitem = new Dictionary<string, object>();
                            paramitem.Add("@LineMemo", lLineMemo);
                            lcount = CommonClass.runSql(savesql, CommonClass.RunSqlInsertMode.QUERY, paramitem);

                            // INSERT ADJUSTED ITEMS IN ITEM TRANSACTION
                            savesql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                        VALUES('"+ this.trandate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")+ "'," + lItemID + "," + lQty + "," + lQty + "," + lUnitCostEx + "," + lAmountEx + ",'IA'," + ItemAdjID + "," + CommonClass.UserID + ")";
                            
                            lcount = CommonClass.runSql(savesql);
                        if (lQty != 0)
                            {
                                DataTable ltb = TransactionClass.GetItem(lItemID);
                                if (ltb.Rows.Count > 0)
                                {
                                    float lOldQty = float.Parse(ltb.Rows[0]["OnHandQty"].ToString());
                                    float lOldCost = float.Parse(ltb.Rows[0]["AverageCostEx"].ToString());
                                    float lNewAveCost = ((lQty + lOldQty) == 0 ? 0 : ((lOldQty * lOldCost) + lAmountEx) / (lQty + lOldQty));
                                    float lLastCost = (lQty == 0 ? 0 : lAmountEx / lQty);
                                    //UPDATE onHAnd QTY
                                    savesql = "UPDATE ItemsQty set OnHandQty = " + (lQty + lOldQty).ToString() + " where ItemID = " + lItemID;
                                        
                                    lcount = CommonClass.runSql(savesql);
                                //UPDATE AVECOST
                                        savesql = "UPDATE ItemsCostPrice set PrevAverageCostEx = " + lOldCost + ", AverageCostEx = " + lNewAveCost + ", LastCostEx = " + lLastCost.ToString() + " , StandardCostEx = " + lLastCost.ToString() + " where ItemID = " + lItemID;
                                    lcount = CommonClass.runSql(savesql);
                            }
                            }
                        }
                    }
                }

                //UPDATE Transaction Series
                string updatesql = "UPDATE TransactionSeries SET BuildItemsSeries = '" + CurSeries + "'";
                lcount = CommonClass.runSql(updatesql);
                if (CreateJournalEntries(ItemAdjID, AdjNumber))
                {
                    //TransactionClass.CreateCurrentEarningsTran(AdjNumber);
                    //TransactionClass.UpdateAccountBalances(AdjNumber);
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Build Inventory Transaction  No. " + AdjNumber, AdjNumber);
                    string titles = "Information";
                    if(SrcOfInvoke == CommonClass.InvocationSource.SELF)
                    {
                        DialogResult createNew = MessageBox.Show("Build Inventory successful. Would you like to create build inventory transactions again?", titles, MessageBoxButtons.YesNo);
                        if (createNew == DialogResult.Yes)
                        {   //clear for new datas
                            this.dgridItems.Rows.Clear();
                            dgridItems.Refresh();
                            PopulateDataGridView();
                            trandate.Value = DateTime.Now;
                            txtMemo.Text = "";
                            this.lblID.Text = "";
                            this.lblTranNo.Text = "";
                            this.btnRecord.Enabled = CanAdd;
                            this.btnRemove.Enabled = CanAdd;
                        }
                        else if (createNew == DialogResult.No)
                        {
                            this.Close();
                        }
                    }
                    if (SrcOfInvoke == CommonClass.InvocationSource.AUTOBUILD)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }      
        }

        public static DataTable GetAjustmentLines(int pAdjID)
        {
            DataTable RTb = null;
            string sql = @"SELECT l.*, a.Memo, i.AssetAccountID, i.IsCounted, i.PartNumber, i.ItemName, j.JobName, j.JobCode, l.AccountID FROM ((( ItemsAdjustmentLines l INNER JOIN ItemsAdjustment a ON l.ItemAdjID = a.ItemAdjID ) left join Items as i on l.ItemID =i.ID ) left join Jobs as j on l.JobID = j.JobID )
                        WHERE l.ItemAdjID = " + pAdjID;
            RTb = new DataTable();
            CommonClass.runSql(ref RTb, sql);
            return RTb;          
        } //END

        private bool CreateJournalEntries(int pID, string pAdjNo)
        {
            string sql = "";
            string lAdjNumber = pAdjNo;
            string lMemo = this.txtMemo.Text;
            string lTranDate = this.trandate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                
            DataTable ltb = GetAjustmentLines(pID);
            if (ltb.Rows.Count > 0)
            {                  
                for (int i = 0; i < ltb.Rows.Count; i++)
                {
                    
                   
                  
                    string lJobID = (ltb.Rows[i]["JobID"].ToString() == "" ? "0" : ltb.Rows[i]["JobID"].ToString());                      
                    string lLineMemo = (ltb.Rows[i]["LineMemo"].ToString() == "" ? "" : ltb.Rows[i]["LineMemo"].ToString());
                    string lAssetAccountID = (ltb.Rows[i]["AssetAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["AssetAccountID"].ToString());
                    float lAmountEx = (ltb.Rows[i]["AmountEx"].ToString() == "" ? 0 : float.Parse(ltb.Rows[i]["AmountEx"].ToString()));
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@LineMemo", lLineMemo);
                    param.Add("@Memo", lMemo);

                    if (lAmountEx < 0) // NEGATIVE SO CREDIT AMOUNT 
                    {                    
                        // CREDIT Inventory Account 
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,CreditAmount, TransactionNumber, Type, JobID) 
                            VALUES('" + lTranDate + "',@Memo, @LineMemo,'" + lAssetAccountID + "'," + (lAmountEx * -1) + ",'" + lAdjNumber + "', 'IB'," + lJobID + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                    }
                    else //POSITIVE SO CREDIT AMOUNT 
                    {
                        // DEBIT Inventory Account 
                        sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,DebitAmount, TransactionNumber, Type, JobID) 
                            VALUES('" + lTranDate + "',@Memo, @LineMemo,'" + lAssetAccountID + "'," + lAmountEx + ",'" + lAdjNumber + "', 'IB'," + lJobID + ")";
                        CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("There was an error creating the transaction. No Adjustment Lines found.");
                return false;
            }          
        }

        private void btnRecord_Click_1(object sender, EventArgs e)
        {
            if(txtOutOfBalance.Value == 0)
            {
                if (btnRecord.Text == "Record")
                {
                    RecordAdjustment();
                }
            }
            else
            {
                MessageBox.Show("Transaction Out of Balance.");
            }         
        }

        private void BuildItems_Load(object sender, EventArgs e)
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
            if (SrcOfInvoke == CommonClass.InvocationSource.AUTOBUILD)
            {
                string lItemID = "";
                float lToBuildQty = 0;
                float lPartQty = 0;
                float lPartAmount = 0;
                float lItemTotal = 0;
                float lPartCost = 0;
                this.txtMemo.Text = "Auto Build Items:";
                for(int i = 0; i < ToBuildITems.Rows.Count; i++)
                {
                    lItemID = ToBuildITems.Rows[i]["ItemID"].ToString();
                    lToBuildQty = (float)ToBuildITems.Rows[i]["QtyToBuild"];
                    lItemTotal = 0;
                    string[] nrow;
                    DataTable ltbParts = GetAutoBuildItemParts(lItemID);
                    for(int j =0; j < ltbParts.Rows.Count; j++)
                    {
                        nrow = new string[10];
                        lPartQty = float.Parse(ltbParts.Rows[j]["PartItemQty"].ToString());
                        lPartCost = float.Parse(ltbParts.Rows[j]["AverageCostEx"].ToString());
                        lPartAmount = lPartQty * lToBuildQty * lPartCost ;
                        nrow[0] = ltbParts.Rows[j]["PartNumber"].ToString(); //Part Number
                        nrow[1] = ltbParts.Rows[j]["ItemName"].ToString(); //ItemName
                        nrow[2] = (lPartQty * lToBuildQty * -1).ToString(); // Qty
                        nrow[3] = lPartCost.ToString(); // Unit Cost
                        nrow[4] = (Math.Round(lPartAmount,2) * -1).ToString(); // Amount
                        nrow[8] = ltbParts.Rows[j]["PartItemID"].ToString(); //Item ID
                        lItemTotal += float.Parse(Math.Round(lPartAmount, 2).ToString());
                        this.dgridItems.Rows.Add(nrow);
                    }
                    nrow = new string[10]; // Item to be Built
                    nrow[0] = ToBuildITems.Rows[i]["PartNumber"].ToString(); //Part Number
                    nrow[1] = ToBuildITems.Rows[i]["ItemName"].ToString(); //ItemName
                    nrow[2] = lToBuildQty.ToString(); // Qty
                    nrow[3] = (lItemTotal/ lToBuildQty).ToString(); // Unit Cost
                    nrow[4] = lItemTotal.ToString(); // Amount
                    nrow[8] = lItemID; //Item ID                   
                    this.dgridItems.Rows.Add(nrow);
                }
                CalcOutOfBalance();
                this.btnRecord.Enabled = CanAdd;
                this.btnRemove.Enabled = CanAdd;
            }
            else
            {
                if (AdjNumber != "")
                {
                    LoadTransaction(AdjNumber);
                }
                else
                {
                    PopulateDataGridView();
                    this.btnRecord.Enabled = CanAdd;
                    this.btnRemove.Enabled = CanAdd;
                }
            }
        }

        private void LoadTransaction(string pTranNo)
        {
            DataTable dt = null;

            string sql = @"SELECT * from ItemsAdjustment where ItemAdjNumber = '" + pTranNo + "'";  
            dt = new DataTable();
            CommonClass.runSql(ref dt, sql);
            if (dt.Rows.Count > 0)
            {
                string pTranID = dt.Rows[0]["ItemAdjID"].ToString();
                this.lblID.Text = pTranID;
                this.lblTranNo.Text = pTranNo;
                this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
                DateTime trandateUTC = (DateTime)dt.Rows[0]["TransactionDate"];
                DateTime trandateLocal = trandateUTC.ToLocalTime();
                this.trandate.Value = trandateLocal;
                this.trandate.Enabled = false;
                this.txtMemo.Enabled = false;
                string lItemID = "";
                float lQty = 0;
                float lItemTotal = 0;
                float lCost = 0;
                string lJobID = "";
                string lJobName = "";
                string lMemo = "";
                string lAccountID = "0";
                string lAccountNo = "";
                DataTable dtlines = GetAjustmentLines(Convert.ToInt32(pTranID));
                for (int i = 0; i < dtlines.Rows.Count; i++)
                {
                    lItemID = dtlines.Rows[i]["ItemID"].ToString();
                    lQty = float.Parse(dtlines.Rows[i]["Qty"].ToString());
                    lCost = float.Parse(dtlines.Rows[i]["UnitCostEx"].ToString());
                    lItemTotal = float.Parse(dtlines.Rows[i]["AmountEx"].ToString());

                    lJobID = dtlines.Rows[i]["JobID"].ToString();
                    lJobName = "";
                    if (lJobID != "0")
                    {
                        lJobName = dtlines.Rows[i]["JobName"].ToString();
                    }

                    lAccountID = (dtlines.Rows[i]["AccountID"] != null ? dtlines.Rows[i]["AccountID"].ToString() : "");
                    lAccountNo = "";
                    if (lAccountID != "0")
                    {
                        lAccountNo = dtlines.Rows[i]["AccountNumber"].ToString();
                    }
                    lMemo = (dtlines.Rows[i]["LineMemo"] != null ? dtlines.Rows[i]["LineMemo"].ToString() : "");
                    string[] nrow;
                    nrow = new string[11]; // Item to be Built
                    nrow[0] = dtlines.Rows[i]["PartNumber"].ToString(); //Part Number
                    nrow[1] = dtlines.Rows[i]["ItemName"].ToString(); //ItemName
                    nrow[2] = lQty.ToString(); // Qty
                    nrow[3] = lCost.ToString(); // Unit Cost
                    nrow[4] = lItemTotal.ToString(); // Amount
                    nrow[5] = lAccountNo; // AccountNumber
                    nrow[6] = lJobName; // Job
                    nrow[7] = lMemo; // LineMemo
                    nrow[8] = lItemID; //Item ID      
                    nrow[9] = lAccountID; //Account ID      
                    nrow[10] = lJobID; // Job             
                    this.dgridItems.Rows.Add(nrow);
                }
                //CalcOutOfBalance();
                this.btnRecord.Enabled = false;
                this.btnRemove.Enabled = false;
                this.dgridItems.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
        }

        private DataTable GetAutoBuildItemParts(string pItemID)
        {
            DataTable ltb = new DataTable();
            string sql = @"SELECT b.*, i.PartNumber, i.ItemName, i.AssetAccountID, c.AverageCostEx from ( ItemsAutoBuild as b inner join Items as i on b.PartItemID = i.ID ) inner join ItemsCostPrice as c on b.PartItemID = c.ItemID where b.ItemID = " + pItemID;
            CommonClass.runSql(ref ltb, sql);
             return ltb;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(this.dgridItems.CurrentRow.Index != -1)
            {
                foreach (DataGridViewCell oneCell in dgridItems.SelectedCells)
                {
                    if (oneCell.RowIndex >= 0 && oneCell.RowIndex < (dgridItems.Rows.Count - 1))
                        dgridItems.Rows.RemoveAt(oneCell.RowIndex);
                }
                CalcOutOfBalance();
                dgridItems.Refresh();
            }
        }
    }
}
