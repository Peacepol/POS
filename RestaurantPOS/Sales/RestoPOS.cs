using Microsoft.VisualBasic;
using Newtonsoft.Json;
using AbleRetailPOS.Inventory;
using AbleRetailPOS.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Sales
{
    public partial class RestoPOS : Form
    {
        DataTable dtSubCat = new DataTable();
        public int startindex = 0;
        public int endindex = 25;
        public int catstartindex = 0;
        public int catendindex = 12;
        string ID;
        string SubID;
        DataTable dtItems = new DataTable();
        int rowin = 0;
        private string CustomerID;
        public DataTable dtv = new DataTable();
        bool IsLoading = true;
        private string memberID;
        private string CurSeries = "";
        public bool editTblNum = false;
        bool isContractPrice = false;
        private string SalesPersonID = CommonClass.UserID;
        //IsIngredient
        private DataTable IngredientTb = null;

        int ItemOnHand = 0;
        //Customer Details
        string customerName = "";
        
        string shipMethod = "";
        private string ProfileTax = "";
        private int shipAddressID = 0;
        private string ShippingMethodID;
        private string TaxID = "";
        private string Note = "";

        //FOR FREIGHT
        private string FreightTaxCode = "";
        private string FreightTaxAccountID = "0";
        private float FreightTaxRate = 0;
        private float FreightTax = 0;
        private float FreightAmountEx = 0;
        private float FreightAmountInc = 0;

        //For Payment Info
        private DataTable PaymentInfoTb = null;
        private string gPaymentNo = "";
        private Decimal PrevPaid = 0;
        private int NewSalesID = 0;
        // private string AmountChange = "";
        private string AR_AccountID;
        private string AR_CustomerDepositsID;
        private string AR_FreightAccountID;
        private string salestype = "INVOICE";
        //ITEMS
        int ShipQ = 0;
        private List<RuleCriteriaPoints> itemPromos;
        private List<RuleCriteria> isLoyal;
        bool ismember = false;
        string member = "Not a Member";

        private float lTaxEx = 0;
        private float lTaxInc = 0;
        private float lTaxRate = 0;
        private float lAmount = 0;


        //User Right Access
        private string thisFormCode = "";
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private bool CanView = false;
        private bool changeqty = false;

        //Free Product
        private float FreeProductAmt;
        private bool isFreeProduct;
        DataTable FreeTable = null;
        //For Void
        string supervisor = "Not a Salesperson";
        string prevAmt;
        string password = "";
        string username = "";
        string formcode = "";
        public string invoicenum = "";
        CommonClass.InvocationSource SrcOfInvoke;
        string XsalesID;
        SqlCommand cmd;

        //For Shipping ID
        string RestoDineIn = "0";
        string RestoTakeAway = "0";
        string RestoDelivery = "0";
      
        public CommonClass.InvocationSource SourceOfInvoke
        {
            get { return SrcOfInvoke; }
            set { SrcOfInvoke = value; }
        }
        public DataGridView GetSalesLinesGridView
        {
            get { return dgEnterSales; }
        }
        public RestoPOS(CommonClass.InvocationSource pSrcInvoke, string pSalesID = "", string pSalesType = "")
        {
            SrcOfInvoke = pSrcInvoke;
            XsalesID = pSalesID;
            InitializeComponent(); 
            InitIngredientInfoTb();
            InitVoidTable();
            InitPaymentInfoTb();
            LoadDeliveryMethods();
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
        private void LoadDeliveryMethods()
        {
            string pointssql = @"Select RestoDeliveryShipping, RestoDineInShipping, RestoTakeAwayShipping from Preference";
            DataTable dt = new DataTable();
           
            CommonClass.runSql(ref dt, pointssql);
            if (dt.Rows.Count > 0)
            {
                RestoDelivery = dt.Rows[0]["RestoDeliveryShipping"].ToString();
                RestoDineIn = dt.Rows[0]["RestoDineInShipping"].ToString();
                RestoTakeAway = dt.Rows[0]["RestoTakeAwayShipping"].ToString();
            }
           

        }
        private void InitIngredientInfoTb()
        {
            IngredientTb = new DataTable();
            IngredientTb.Columns.Add("ItemID", typeof(string));
            IngredientTb.Columns.Add("Ship", typeof(decimal));
            IngredientTb.Columns.Add("PartNumber", typeof(string));
            IngredientTb.Columns.Add("Description", typeof(string));
            IngredientTb.Columns.Add("Amount", typeof(string));
            IngredientTb.Columns.Add("TaxRate", typeof(string));
            IngredientTb.Columns.Add("TaxCode", typeof(string));
            IngredientTb.Columns.Add("TaxCollectedAccountID", typeof(string));
            IngredientTb.Columns.Add("Cost", typeof(string));
            IngredientTb.Columns.Add("TotalCost", typeof(string));


            //DataRow rw = IngredientTb.NewRow();

            //rw["ItemID"] = "";
            //rw["Ship"] = 0;
            //rw["PartNumber"] = "0";
            //rw["Description"] = "";
            //rw["Amount"] = "";
            //rw["TaxRate"] = "";
            //rw["TaxCode"] = "";
            //rw["TaxCollectedAccountID"] = "";
            //rw["Cost"] = "";
            //rw["TotalCost"] = "";

            //IngredientTb.Rows.Add(rw);
        }
        private void RestoPOS_Load(object sender, EventArgs e)
        {
            txtCustPoints.Text = "0";
            btnPrevP.Enabled = false;
            rdoDineIn.Checked = true;
            ShippingMethodID = RestoDineIn;
            InitFreeTable();
            string sqlSubCat = @"Select * From Category WHERE MainCategoryID != '0' and ShowInMenu = 1";
            CommonClass.runSql(ref dtSubCat, sqlSubCat);
            LoadSubCat();
          

            PopulateGrid();
            LoadDefaultCustomer();
            int lRowIndex = dgSubCat.CurrentRow.Index;
            int lColIndex = dgSubCat.CurrentCell.ColumnIndex;
            if (dgSubCat.Rows[lRowIndex].Cells[lColIndex].Tag != null)
            {
                SubID = dgSubCat.Rows[lRowIndex].Cells[lColIndex].Tag.ToString();
            }
            else
            {
                SubID = "0";
            }
            LoadItems(SubID);
            DataTable dt = new DataTable();
            string selectSql = "SELECT * FROM Preference";
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AR_AccountID = (dt.Rows[i]["TradeDebtorGLCode"].ToString());
                    AR_FreightAccountID = (dt.Rows[i]["SalesFreightGLCode"].ToString());
                    AR_CustomerDepositsID = (dt.Rows[i]["SalesDepositGLCode"].ToString());


                    if (XsalesID == "")
                    {
                        if (salestype == "INVOICE")
                        {
                            PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_AccountID;
                        }
                        else
                        {
                            PaymentInfoTb.Rows[0]["RecipientAccountID"] = AR_CustomerDepositsID;
                        }
                    }

                    if (AR_AccountID == "0")
                    {
                        string titles = "Information";
                        DialogResult noCustomerReceipts = MessageBox.Show("Setup GL code in Preference first.", titles, MessageBoxButtons.OK);
                        if (noCustomerReceipts == DialogResult.OK)
                        {
                            this.BeginInvoke(new MethodInvoker(Close));
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Setup GL code in Preference first.");
            }
            IsLoading = false;
        }
        public bool iSelectedItem()
        {
            bool selecteditem = dgEnterSales.CurrentRow.Selected;
            return selecteditem;
        }
        void LoadDefaultCustomer()
        {
            if (!CommonClass.MandatoryCustomer)
            {
                
                if(CommonClass.DefaultCustomerID != null)
                {
                    DataTable dt = new DataTable();
                    string sqldefcust = @"Select p.*, isnull(s.ShippingID,0) as ShippingID FROM Profile p INNER JOIN Contacts c ON p.ID = c.ProfileID 
                                LEFT JOIN ShippingMethods s on p.ShippingMethodID = s.ShippingMethod WHERE ID = " + CommonClass.DefaultCustomerID;
                    CommonClass.runSql(ref dt, sqldefcust);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow c = dt.Rows[0];
                        CustomerID = c["ID"].ToString();
                        customerName = c["Name"].ToString();
                        shipMethod = c["ShippingMethodID"].ToString();
                        //TermRefID = c["TermsOfPayment"].ToString();
                        ProfileTax = c["TaxCode"].ToString();
                        //MethodPaymentID = c["MethodOfPaymentID"].ToString();
                        //CustomerBalance = (c["CurrentBalance"].ToString() == "" ? 0 : float.Parse(c["CurrentBalance"].ToString(), NumberStyles.Currency));
                        //CustomerCreditLimit = (c["CreditLimit"].ToString() == "" ? 0 : float.Parse(c["CreditLimit"].ToString(), NumberStyles.Currency));
                        //ShippingMethodID = (c["ShippingID"].ToString() == "" ? "0" : c["ShippingID"].ToString());
                        //InvoiceNumTxt.Visible = true;
                        //cmb_shippingcontact.SelectedIndex = Convert.ToInt16(c["LocationID"].ToString()) - 1;
                        //LoadContacts(Convert.ToInt32(CustomerID), this.cmb_shippingcontact.SelectedIndex + 1);
                        //LoadFreightTax(ProfileTax);
                        LoadPoints();
                        //FormCheck();
                    }
                }
            }
        }
        private void LoadPoints()
        {
            string pointssql = @"SELECT SUM(PointsAccumulated) AS TotalPoints 
                                 FROM AccumulatedPoints 
                                 WHERE CustomerID = @CustomerID";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CustomerID", CustomerID);
            CommonClass.runSql(ref dt, pointssql, param);
            if (dt.Rows.Count == 1 && dt.Rows[0]["TotalPoints"].ToString() != "")
            {
                txtCustPoints.Text = dt.Rows[0]["TotalPoints"].ToString();
            }
            else
            {
                txtCustPoints.Text = "0";
            }
            txtCustPoints.Visible = false;

        }
        void LoadSubCat()
        {
            int r = 0;
            int c = 0;
            dgSubCat.Rows.Clear();
            dgSubCat.Rows.Add();
            dgSubCat.Rows.Add();
            dgSubCat.Rows.Add();
            for (int i = catstartindex; i < dtSubCat.Rows.Count; i++)
            {
              if(dtSubCat.Rows.Count > 12)
                {
                    btn_nextcat.Enabled = true;
                }
                else
                {
                    btn_nextcat.Enabled = false;
                }
                DataRow dr = dtSubCat.Rows[i];
                if(i < catendindex)
                {
                    if (c < 4)
                    {
                        dgSubCat.Rows[r].Cells[c].Value = dr["CategoryCode"].ToString();
                        dgSubCat.Rows[r].Cells[c].Tag = dr["CategoryID"].ToString();
                        c++;
                    }
                    else
                    {
                        c = 0;
                        r++;
                        dgSubCat.Rows[r].Cells[c].Value = dr["CategoryCode"].ToString();
                        dgSubCat.Rows[r].Cells[c].Tag = dr["CategoryID"].ToString();
                        c++;

                    }
                }
                else
                {
                    return;
                }
               
                
            }
        }
        void PopulateGrid()
        {
            dgItems.Rows.Add();
            dgItems.Rows.Add();
            dgItems.Rows.Add();
            dgItems.Rows.Add();
            dgItems.Rows.Add();
        }
        public void SetPartName(string pPartNum)
        {
            string ptnum = pPartNum;
            string WhereCon = "PartNumber";
            
            int lastFilledRow = 0;
            if (customerName != "")
            {
                ShowItemLookup(ptnum, WhereCon);
                int cur = dgEnterSales.Rows.Count - 1;
                dgEnterSales.Rows[cur].Selected = true;
                dgEnterSales.CurrentCell = dgEnterSales[3, cur];
                itemcalc(cur);
                changeqty = false;
                for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
                {
                    if (this.dgEnterSales.Rows[i].Cells["PartNumber"] != null)
                    {
                        Recalcline(8, i);
                        lastFilledRow = i;
                        //rowin = lastFilledRow;
                    }
                }
            }
            CalcOutOfBalance();
            if (lastFilledRow < dgEnterSales.Rows.Count)
            {
                dgEnterSales.FirstDisplayedScrollingRowIndex = lastFilledRow;
            }

        }
        private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                ID = dgItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
                SetPartName(ID);
            }
        }
        private void InitFreeTable()
        {
            FreeTable = new DataTable();
            FreeTable.Columns.Add("ItemID", typeof(int));
            FreeTable.Columns.Add("Quantity", typeof(string));
            FreeTable.Columns.Add("ItemName", typeof(string));
            FreeTable.Columns.Add("PartNum", typeof(string));
            FreeTable.Columns.Add("SalesTaxCode", typeof(string));
            FreeTable.Columns.Add("Price", typeof(float));
            FreeTable.Columns.Add("CostPrice", typeof(float));
            //FreeTable.Rows.Add();
        }
         private float ComputePromo(string accumulation, float points, float pointsvalue, float PurchasePrice, int promoID, int CRowIndex)
        {
            float ltaxexprice = 0;
            if(CRowIndex != dgEnterSales.CurrentRow.Index)
            {
                dgEnterSales.Rows[CRowIndex].Selected = true;
            }
            //check current row
           
            DataGridViewRow dgvRows = dgEnterSales.Rows[CRowIndex];
            //IF DEFAULT PROMOTYPE, GET THE FINAL PRICE OF THE ITEM
            if (CommonClass.IsTaxcInclusiveEnterSales)
            {
                if (dgvRows.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx / (1 + (lTaxRate / 100));
                    ltaxexprice = PurchasePrice / (1 + (lTaxRate / 100));
                }
            }
            else
            {
                if (dgEnterSales.CurrentRow.Cells["TaxRate"].Value != null)
                {
                    float lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                    //ltaxexprice = mPriceEx;
                    ltaxexprice = PurchasePrice;
                }
            }

            if (accumulation == "Points (X)")
            {
                points += pointsvalue * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());
            }
            else if (accumulation == "Points (X) percentage")
            {
                points = ((pointsvalue / 100) * ltaxexprice) * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());
            }

            else if (accumulation == "Points (X) percentage profit")
            {
                //Get the total cost (costprice)
                float costprice = float.Parse(dgvRows.Cells["CostPrice"].Value == null ? "0" : dgvRows.Cells["CostPrice"].Value.ToString());
                //get the profit
                float profit = ltaxexprice - costprice;
                points = ((pointsvalue / 100) * profit) * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());
            }

            float lOrigUnitPrice = 0;
            if (dgvRows.Cells["ActualUnitPrice"].Value != null)
            {
                //IF NON-DEFAULT PROMO TYPE, GET THE ACTUAL UNIT PRICE 
                lOrigUnitPrice = float.Parse(dgvRows.Cells["ActualUnitPrice"].Value.ToString());
            }
            ltaxexprice = lOrigUnitPrice;
           
            if (accumulation == "Fixed (X) Discount")
            {
                float difPrice = ltaxexprice - pointsvalue;
                if (PurchasePrice > difPrice)
                {
                    //Change only if difprice will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = difPrice;
                }

                points = 0;
            }
            else if (accumulation == "Percentage Discount")
            {
                float discountedprice = ltaxexprice - (ltaxexprice * (pointsvalue / 100));
                if (PurchasePrice > discountedprice)
                {
                    //Change only if pointsvalue will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = discountedprice;
                }

                points = 0;
            }
            else if (accumulation == "Price (X)")
            {
                if (PurchasePrice > pointsvalue)
                {
                    //Change only if pointsvalue will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = pointsvalue;
                }

                points = 0;
            }
            else if (accumulation == "Free Product")
            {
                int lNewRowindex;
                if (dgvRows.Cells["PartNumber"].ToString() != "")
                {
                    FreeProducts FreeLookup = new FreeProducts(CommonClass.FreeItemInvocation.ENTERSALES, FreeTable, promoID, (int)pointsvalue);
                    if (FreeLookup.ShowDialog() == DialogResult.OK)
                    {
                        FreeTable = FreeLookup.GetFreeProductTable;
                        if (FreeTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < FreeTable.Rows.Count; i++)
                            {
                                DataRow dgRow = FreeTable.Rows[i];
                                dgEnterSales.Rows.Add();
                                lNewRowindex = dgEnterSales.Rows.Count - 1;
                                
                                dgEnterSales.Rows[lNewRowindex].Cells["ItemID"].Value = dgRow["ItemID"].ToString();
                                
                                dgEnterSales.Rows[lNewRowindex].Cells["PartNumber"].Value = dgRow["PartNum"].ToString();
                                dgEnterSales.Rows[lNewRowindex].Cells["Description"].Value = "Free Product " + dgRow["ItemName"].ToString();
                                dgEnterSales.Rows[lNewRowindex].Cells["Price"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["ActualUnitPrice"].Value = float.Parse(dgRow["Price"].ToString());
                                dgEnterSales.Rows[lNewRowindex].Cells["Amount"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxExclusiveAmount"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxInclusiveAmount"].Value = 0;
                                dgEnterSales.Rows[lNewRowindex].Cells["Ship"].Value = 1;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxCode"].Value = dgRow["SalesTaxCode"].ToString() == "" ? "N-T" : dgRow["SalesTaxCode"].ToString();
                                DataRow rTx = CommonClass.getTaxDetails(dgEnterSales.Rows[lNewRowindex].Cells["TaxCode"].Value.ToString());
                                
                                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                                string lTaxPaidAccountID = "";
                                lTaxPaidAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxCollectedAccountID"].Value = lTaxPaidAccountID;
                                dgEnterSales.Rows[lNewRowindex].Cells["TaxRate"].Value = ltaxrate;
                                dgEnterSales.Rows[lNewRowindex].Cells["PromoID"].Value = promoID;
                                dgEnterSales.Rows[lNewRowindex].Cells["CostPrice"].Value = float.Parse(dgRow["CostPrice"].ToString());                                
                                Recalcline(8, lNewRowindex);
                            }
                        }
                    }
                    //FreeProductAmt = 0;
                    isFreeProduct = true;
                    FreeTable.Clear();
                }

            }
            else if (accumulation == "Percentage (X) Discount From List")
            {
                float listprice = GetItemListPrice(dgvRows.Cells["ItemID"].Value.ToString());
                float percentDiscount = ((pointsvalue / 100) * listprice) * float.Parse(dgvRows.Cells[1].Value == null ? "0" : dgvRows.Cells[1].Value.ToString());

                float discounted = listprice - percentDiscount;
                if (PurchasePrice > discounted)
                {
                    //Change only if discounted will be lesser than the purchase price
                    dgvRows.Cells["Price"].Value = discounted;
                }
                points = 0;
            }

            dgvRows.Cells["PromoID"].Value = promoID;
            return points;
        }
        private float isItemPromo(int ItemID, float PurchasePrice, int CRowIndex, string pType = "")
        {
            string itemPromoJson;
            float points = 0;
            float pointsvalue = 0;
            DataTable dtPromo = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            string lpromotype = (pType == "" ? " PromotionType <> 'Default'" : " PromotionType = 'Default'");
            CommonClass.runSql(ref dtPromo, "SELECT * FROM Promos WHERE isActive = 1 AND GETDATE() BETWEEN StartDate AND EndDate AND " + lpromotype);
            string accumulation = "";
            if (dtPromo.Rows.Count > 0)
            {
                foreach (DataRow x in dtPromo.Rows)
                {
                    string file = x["RuleCriteriaID"].ToString();
                    accumulation = x["PointAccumulationCriteria"].ToString();
                    pointsvalue = float.Parse(x["PointsValue"].ToString());
                    x["RuleCriteria"].ToString().Replace("\t", "");
                    itemPromoJson = file.Replace("\t", "");
                    itemPromos = JsonConvert.DeserializeObject<List<RuleCriteriaPoints>>(itemPromoJson);
                    isLoyal = JsonConvert.DeserializeObject<List<RuleCriteria>>(x["RuleCriteria"].ToString().Replace("\t", ""));
                    bool includepromo = false;
                    foreach (RuleCriteria criteria in isLoyal)
                    {
                        if (criteria.CriteriaName == "Loyalty"
                            && criteria.CriteriaValue == "False")
                        {
                            if (isLoyal.Count == 1)
                                includepromo = true;
                        }
                        else if (criteria.CriteriaName == "Loyalty"
                                && criteria.CriteriaValue == "True")
                        {
                            includepromo = ismember;
                        }

                        if (!includepromo)
                        {
                            if (criteria.CriteriaName == "Customer"
                                && criteria.CriteriaValue != "")
                            {
                                if (CustomerID == criteria.CriteriaValue)
                                {
                                    includepromo = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (includepromo)
                    {
                        foreach (RuleCriteriaPoints promo in itemPromos)
                        {
                            if (promo.CriteriaName == "Item")
                            {
                                if (promo.CriteriaValue == ItemID.ToString())
                                {
                                    return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                }
                            }
                            else if (promo.CriteriaName == "Supplier")
                            {
                                if (dgEnterSales.Rows[CRowIndex].Cells["Supplier"].Value != null)
                                {
                                    if (dgEnterSales.Rows[CRowIndex].Cells["Supplier"].Value.ToString() == promo.CriteriaValue)
                                    {
                                        return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                    }
                                }
                            }                            
                            else if (promo.CriteriaName == "Category")
                            {
                                if (dgEnterSales.Rows[CRowIndex].Cells["CategoryID"].Value != null)
                                {
                                    if (dgEnterSales.Rows[CRowIndex].Cells["CategoryID"].Value.ToString() == promo.CriteriaValue)
                                    {
                                        return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                    }
                                }
                            }
                            else if (promo.CriteriaName == "Brand")
                            {
                                if (dgEnterSales.Rows[CRowIndex].Cells["Brand"].Value != null)
                                {
                                    if (dgEnterSales.Rows[CRowIndex].Cells["Brand"].Value.ToString() == promo.CriteriaValue)
                                    {
                                        return ComputePromo(accumulation, points, pointsvalue, PurchasePrice, int.Parse(x["PromoID"].ToString()), CRowIndex);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return points;
        }
        void itemcalc(int RoWindex)
        {
            double shipVal = 0;
            double priceVal = 0;
            double disc = 0;
            double discValue = 0;
            double woDisc = 0;
            double amtVal = 0;
            DataGridViewRow dgvRows = dgEnterSales.Rows[RoWindex];
            if (dgvRows.Cells["Ship"].Value != null && dgvRows.Cells["Ship"].Value.ToString() != "")
            {
                shipVal = Convert.ToDouble(dgvRows.Cells["Ship"].Value.ToString());
                dgvRows.Cells["Ship"].Value = shipVal.ToString();
            }
            else
            {
                dgvRows.Cells["Ship"].Value = 1;
                shipVal = double.Parse(dgvRows.Cells["Ship"].Value.ToString());
            }

            if (salestype == "INVOICE") //APPLY PROMO ON INVOICES ONLY
            {
                if (dgvRows.Cells["Price"].Value != null)
                {
                    //MessageBox.Show("Price:" + dgvRows.Cells["Price"].Value.ToString());
                    //APPLY NON DEFAULT PROMOS
                    isItemPromo(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), float.Parse(dgvRows.Cells["Price"].Value.ToString()), RoWindex);
                    //APPLY DEFAULT PROMOS for Loyalty Members

                    if (memberID != "")
                    {
                        // MessageBox.Show("Price:" + dgvRows.Cells["Price"].Value.ToString());
                        dgvRows.Cells["Points"].Value = isItemPromo(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), float.Parse(dgvRows.Cells["Price"].Value.ToString()), RoWindex,"Default");
                    }
                    priceVal = double.Parse(dgvRows.Cells["Price"].Value.ToString(), NumberStyles.Currency);
                }
            }
            else
            {
                if (dgvRows.Cells["Price"].Value != null)
                {
                    priceVal = double.Parse(dgvRows.Cells["Price"].Value.ToString(), NumberStyles.Currency);
                }
            }
            if (dgvRows.Cells["Discount"].Value == null || dgvRows.Cells["Discount"].Value.ToString() == "")
            {
                woDisc = shipVal * priceVal;
            }
            else
            {
                disc = (dgvRows.Cells["Discount"].Value.ToString() == "" ? 0 : Convert.ToDouble(dgvRows.Cells["Discount"].Value.ToString().Replace("%", "").Trim()));
                woDisc = shipVal * priceVal;
                discValue = shipVal * priceVal * (disc / 100);
            }
            amtVal = woDisc - discValue;

            dgvRows.Cells["Amount"].Value = woDisc - discValue;
            prevAmt = dgvRows.Cells["Amount"].Value.ToString();


            //dgvRows.Cells["Price"].Value = woDisc - discValue;

            //if (dgvRows.Cells["AutoBuild"].Value != null && bool.Parse(dgvRows.Cells["AutoBuild"].Value.ToString()))
            //{
            //    int lTempIndex = dgEnterSales.CurrentRow.Index;
            //    if (dgvRows.Cells["BundleType"].Value.ToString() == "Dynamic")
            //        IsAutoBuild(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), RoWindex);
            //    else if (dgvRows.Cells["BundleType"].Value.ToString() == "Ingredient")
            //        IsIngredientBuild(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), RoWindex);
            //}
            //VerifyItemQty(RoWindex, (XsalesID == "" ? "0" : XsalesID));

        }
        private void IsAutoBuild(int itemID, int rowindex)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ia.PartItemID,
                                    ia.PartItemQty, 
                                    i.PartNumber,
                                    i.ItemName,
                                    i.SalesTaxCode,
                                    t.TaxCollectedAccountID, 
                                    t.TaxPercentageRate AS RateTaxSales 
                            FROM ItemsAutoBuild ia 
                            INNER JOIN Items i ON i.ID = ia.PartItemID 
                            LEFT JOIN TaxCodes t ON i.SalesTaxCode = t.taxcode
                            WHERE ia.ItemID = " + itemID;
            CommonClass.runSql(ref dt, sql);
            if (dt.Rows.Count > 0)
            {
                int lPartIndex = 0;
                lPartIndex = rowindex;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dgRow = dt.Rows[i];
                    lPartIndex++;
                    if (!changeqty)
                    {
                    dgEnterSales.Rows.Add();
                    }
                    dgEnterSales.Rows[lPartIndex].Cells["ItemID"].Value = dgRow["PartItemID"].ToString();
                   //if()
                    dgEnterSales.Rows[lPartIndex].Cells["Ship"].Value = float.Parse(dgRow["PartItemQty"].ToString()) * float.Parse(dgEnterSales.Rows[rowindex].Cells["Ship"].Value.ToString());
                    dgEnterSales.Rows[lPartIndex].Cells["PartNumber"].Value = dgRow["PartNumber"].ToString();
                    dgEnterSales.Rows[lPartIndex].Cells["Description"].Value = dgRow["ItemName"].ToString();
                    dgEnterSales.Rows[lPartIndex].Cells["Amount"].Value = 0;
                    //rowin++;
                    string taxcode = dgRow["SalesTaxCode"].ToString();
                    if (taxcode != "")
                    {
                        dgEnterSales.Rows[lPartIndex].Cells["TaxRate"].Value = dgRow["RateTaxSales"];
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCode"].Value = dgRow["SalesTaxCode"];
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCollectedAccountID"].Value = dgRow["TaxCollectedAccountID"];
                    }
                    else
                    {
                        dgEnterSales.Rows[lPartIndex].Cells["TaxRate"].Value = 0;
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCode"].Value = "N-T";
                        dgEnterSales.Rows[lPartIndex].Cells["TaxCollectedAccountID"].Value = 0;
                    }

                    dgEnterSales.Rows[lPartIndex].Cells["Price"].Value = 0;
                    dgEnterSales.Rows[lPartIndex].Cells["ActualUnitPrice"].Value = 0;
                    dgEnterSales.Rows[lPartIndex].Cells["Amount"].Value = 0;

                    Recalcline(8, lPartIndex); 
                    int c = VerifyItemQty(lPartIndex);
                    if (c == 0)
                    {
                        dgEnterSales.Rows[rowindex].Cells["Ship"].Value = 0;
                        dgEnterSales.Rows[lPartIndex].Cells["Ship"].Value = 0;
                        changeqty = true;
                        itemcalc(rowindex);
                    }
                }
            }
        }
        public decimal GetPrevShipQty(string pSalesID, string pItemID, string pLocID = "1")
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT * from SalesLines where SalesID = " + pSalesID + " and EntityID = " + pItemID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                decimal lShipQty = 0;
                if (RTb.Rows.Count > 0)
                {
                    lShipQty = Convert.ToDecimal(RTb.Rows[0]["ShipQty"].ToString());
                }
                return lShipQty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END
        private int VerifyItemQty(int pRIndex, string pSalesID = "0")
        {
            //CHECK ITEM ON HAND QTY
            if (this.salestype == "INVOICE")
            {
                if (dgEnterSales.Rows[pRIndex].Cells["ItemID"].Value != null)
                {
                    string lItemID = dgEnterSales.Rows[pRIndex].Cells["ItemID"].Value.ToString();
                    decimal lShipQty = Convert.ToDecimal(dgEnterSales.Rows[pRIndex].Cells["Ship"].Value.ToString());
                    if (lItemID != "" || lItemID != "0")
                    {
                        DataTable ltb = GetNewEndingQty(lItemID);
                        if ((bool)ltb.Rows[0]["IsCounted"])
                        {
                            decimal lOnHandQty = Convert.ToDecimal(ltb.Rows[0]["OnHandQty"].ToString());
                            decimal lPrevShipQty = GetPrevShipQty(pSalesID, lItemID);
                            decimal lNewQty = lOnHandQty + lPrevShipQty - lShipQty;

                            if (lNewQty < 0)
                            {
                                MessageBox.Show("Not Enough Quantity.");
                                dgEnterSales.Rows[pRIndex].Cells["Ship"].Value = lOnHandQty;
                                DataGridViewCellEventArgs le = new DataGridViewCellEventArgs(1, pRIndex);

                                dgEnterSales_CellEndEdit(null, le);
                                return 0;
                            }
                        }

                    }


                }
            }
            return 1;
        }
      
        void PopulateDataGridView()
        {
            IsLoading = true;
            for (int i = 0; i <= 1; i++)
            {
                //dgEnterSales.Rows.Add();
            }
            IsLoading = false;
        }
        public void Recalcline(int pColIndex, int pRowIndex)
        {
            if (pRowIndex < 0)
                return;
            if (!IsLoading)
            {
                DataGridViewRow dgvRows = dgEnterSales.Rows[pRowIndex];

                float lTaxEx = 0;
                float lTaxInc = 0;
                float lTaxRate = 0;
                float lAmount = 0;

                if (pColIndex > 0)
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (dgvRows.Cells["Amount"].Value != null
                            && dgvRows.Cells["TaxRate"].Value != null)
                        {
                            lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                            lAmount = float.Parse(dgvRows.Cells["Amount"].Value.ToString(), NumberStyles.Currency);
                            lTaxEx = lAmount / (1 + (lTaxRate / 100));
                            lTaxInc = lAmount;
                            dgvRows.Cells["TaxExclusiveAmount"].Value = lTaxEx;
                            dgvRows.Cells["TaxInclusiveAmount"].Value = lTaxInc;
                        }
                    }
                    else
                    {
                        if (dgvRows.Cells["Amount"].Value != null
                            && dgvRows.Cells["TaxRate"].Value != null)
                        {
                            lTaxRate = float.Parse(dgvRows.Cells["TaxRate"].Value.ToString());
                            lAmount = float.Parse(dgvRows.Cells["Amount"].Value.ToString(), NumberStyles.Currency);
                            lTaxInc = lAmount * (1 + (lTaxRate / 100));
                            lTaxEx = lAmount;
                            dgvRows.Cells["TaxExclusiveAmount"].Value = lTaxEx;
                            dgvRows.Cells["TaxInclusiveAmount"].Value = lTaxInc;
                        }
                    }
                }
                else
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (dgvRows.Cells["TaxInclusiveAmount"].Value != null)
                        {
                            dgvRows.Cells["Amount"].Value = dgvRows.Cells["TaxInclusiveAmount"].Value;
                        }
                    }
                    else
                    {
                        if (dgvRows.Cells["TaxExclusiveAmount"].Value != null)
                        {
                            dgvRows.Cells["Amount"].Value = dgvRows.Cells["TaxExclusiveAmount"].Value;
                        }
                    }
                }
            }
        }

        public void CalcOutOfBalance()
        {
            this.TotalAmount.Value = 0;
            this.TaxAmount.Value = 0;
            BalanceDue_txt.Value = 0;
            decimal TotalTaxEx = 0;
            decimal TotalTaxInc = 0;
            decimal outnum;
            decimal TaxEx = 0;
            decimal TaxInc = 0;
            decimal CurAmt = 0;

            for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
            {
                if (this.dgEnterSales.Rows[i].Cells["Amount"].Value != null)
                {
                    if (this.dgEnterSales.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        Decimal.TryParse(this.dgEnterSales.Rows[i].Cells["Amount"].Value.ToString(), out outnum);
                        this.TotalAmount.Value += outnum;
                        CurAmt = outnum;
                        if (CurAmt != 0)
                        {
                            if(dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value != null)
                            {
                                Decimal.TryParse(dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value.ToString(), out outnum);
                                TaxEx = outnum;
                                TotalTaxEx += TaxEx;
                                Decimal.TryParse(this.dgEnterSales.Rows[i].Cells["TaxInclusiveAmount"].Value.ToString(), out outnum);
                                TaxInc = outnum;
                                TotalTaxInc += TaxInc;
                            }
                            
                        }
                    }
                }
            }

            this.TaxAmount.Value = (TotalTaxInc - TotalTaxEx) + (decimal)FreightTax;
            this.subtotalAmountText.Value = TotalTaxEx;
            TotalAmount.Value = TotalTaxInc + (decimal)FreightAmountInc;
            this.BalanceDue_txt.Value = TotalAmount.Value - PaidToday_txt.Value;
        }

        void LoadItems(string cID)
        {
            dgItems.Rows.Clear();
            PopulateGrid();
            dtItems = new DataTable();
            string sqlItemCat = @"Select * From Items WHERE CategoryID = '" + cID + "'";
            CommonClass.runSql(ref dtItems, sqlItemCat);
            int r = 0;
            int c = 0;
            if (dtItems.Rows.Count < 25)
            {
                btnNextP.Enabled = false;
            }
            else
            {
                btnNextP.Enabled = true;
            }
            for (int i = startindex; i < dtItems.Rows.Count; i++)
            {
                DataRow dr = dtItems.Rows[i];
                if (i < endindex)
                {
                    if (c < 5)
                    {
                        dgItems.Rows[r].Cells[c].Value = dr["ItemName"].ToString();
                        dgItems.Rows[r].Cells[c].Tag = dr["PartNumber"].ToString();
                        c++;
                    }
                    else
                    {
                        c = 0;
                        r++;
                        dgItems.Rows[r].Cells[c].Value = dr["ItemName"].ToString();
                        dgItems.Rows[r].Cells[c].Tag = dr["PartNumber"].ToString();
                        c++;
                    }
                }
                else
                {
                    return;
                }

            }
        }

        private void dgSubCat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if( dgSubCat.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                startindex = 0;
                endindex = 25;
                SubID = dgSubCat.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
                LoadItems(SubID);
            }
            

            //MessageBox.Show(s);



        }

        private void btnNextP_Click(object sender, EventArgs e)
        {
            btnPrevP.Enabled = true;
            startindex = startindex + 25;
            endindex = endindex + 25;
            LoadItems(ID);
            if (endindex > dtItems.Rows.Count)
            {
                btnNextP.Enabled = false;
            }

        }

        private void btnPrevP_Click(object sender, EventArgs e)
        {
            if (startindex == 0)
            {
                btnPrevP.Enabled = false;
                btnNextP.Enabled = true;
                return;
            }
            startindex = startindex - 25;
            endindex = endindex - 25;
            LoadItems(ID);
            if (startindex == 0)
            {
                btnPrevP.Enabled = false;
                btnNextP.Enabled = true;
            }
        }

        private void rdoDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDelivery.Checked)
            {

                rdoDelivery.BackColor = Color.LimeGreen;
                ShippingMethodID = RestoDelivery;
                btnTable.Text = "Order #";
            }
            else
                rdoDelivery.BackColor = default;
        }

        private void rdoTakeOut_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTakeOut.Checked)
            {

                rdoTakeOut.BackColor = Color.LimeGreen;
                ShippingMethodID = RestoTakeAway;
                btnTable.Text = "Order #";
            }
            else
                rdoTakeOut.BackColor = default;
        }

        private void rdoDineIn_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDineIn.Checked)
            {
                rdoDineIn.BackColor = Color.LimeGreen;
                ShippingMethodID = RestoDineIn;
                btnTable.Text = "Table #";
            }
            else
                rdoDineIn.BackColor = default;
        }
        public bool ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.ENTERSALES, itemNum, CustomerID, whereCon);
            DataGridViewRow ItemRows;
            if (Items.ShowDialog() == DialogResult.OK)
            {
                ItemRows = Items.GetSelectedItem;
                if (!CheckExistItemInGrid(ItemRows.Cells[1].Value.ToString()))
                {
                    dgEnterSales.Rows.Add();
                    int lLastIndex = dgEnterSales.Rows.Count - 1;
                    dgEnterSales.Rows[lLastIndex].Selected = true;
                    DataGridViewRow dgvRows = dgEnterSales.Rows[lLastIndex];
                    dgvRows.Cells["ItemID"].Value = ItemRows.Cells[0].Value.ToString();
                    dgvRows.Cells["Ship"].Value = qtyvalidation(1);
                    dgvRows.Cells["PartNumber"].Value = ItemRows.Cells[1].Value;
                    dgvRows.Cells["Description"].Value = ItemRows.Cells[3].Value.ToString();
                    ItemOnHand = Convert.ToInt32(ItemRows.Cells[4].Value.ToString());
                    dgvRows.Cells["TaxCode"].Value = ItemRows.Cells["SalesTaxCode"].Value.ToString();
                    dgvRows.Cells["CostPrice"].Value = ItemRows.Cells["AverageCostEx"].Value.ToString();
                    dgvRows.Cells["Price"].ReadOnly = false;
                    dgvRows.Cells["Description"].ReadOnly = false;
                    dgvRows.Cells["Amount"].ReadOnly = false;
                    dgvRows.Cells["Job"].ReadOnly = false;
                    dgvRows.Cells["TaxCode"].ReadOnly = false;
                    dgvRows.Cells["Supplier"].Value = int.Parse(ItemRows.Cells["SupplierID"].Value.ToString());
                    dgvRows.Cells["CategoryID"].Value = int.Parse(ItemRows.Cells["CategoryID"].Value.ToString());
                    dgvRows.Cells["Brand"].Value = ItemRows.Cells["BrandName"].Value.ToString();
                    dgvRows.Cells["AutoBuild"].Value = ItemRows.Cells["IsAutoBuild"].Value.ToString();
                    dgvRows.Cells["BundleType"].Value = ItemRows.Cells["BundleType"].Value.ToString();
                    float ltaxrate = 0;
                    DataRow rTx = CommonClass.getTaxDetails(ItemRows.Cells["SalesTaxCode"].Value.ToString());
                    //if (rTx.ItemArray.Length > 0)
                    //{
                    ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                    string lTaxCollectedAccountID = "";
                    lTaxCollectedAccountID = ((rTx["TaxCollectedAccountID"] == null || rTx["TaxCollectedAccountID"].ToString() == "") ? "0" : rTx["TaxCollectedAccountID"].ToString());
                    dgvRows.Cells["TaxCollectedAccountID"].Value = lTaxCollectedAccountID;
                    dgvRows.Cells["TaxRate"].Value = ltaxrate;
                    //}
                    //Assume that Price is already Inclusive
                    float lSellingPriceInc = float.Parse(ItemRows.Cells[8].Value.ToString(), NumberStyles.Currency);
                    float lSellingPriceEx = (ltaxrate != 0 ? lSellingPriceInc / (1 + (ltaxrate / 100)) : lSellingPriceInc);
                    if (!CommonClass.IsItemPriceInclusive)//Recalc if not inclusive
                    {
                        lSellingPriceEx = lSellingPriceInc;
                        lSellingPriceInc = (ltaxrate != 0 ? lSellingPriceEx * (1 + (ltaxrate / 100)) : lSellingPriceEx);
                    }
                    //Fill in the grid

                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        dgvRows.Cells["Price"].Value = lSellingPriceInc;
                        dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceInc;

                    }
                    else
                    {
                        dgvRows.Cells["Price"].Value = lSellingPriceEx;
                        dgvRows.Cells["ActualUnitPrice"].Value = lSellingPriceEx;

                    }
                    ContractPriceAmount(CustomerID, ItemRows.Cells[0].Value.ToString(), lLastIndex);
                    float lAmount = float.Parse(dgvRows.Cells["Price"].Value.ToString()) * float.Parse(dgvRows.Cells["Ship"].Value.ToString());
                    dgvRows.Cells["Amount"].Value = lAmount;

                    if (dgvRows.Cells["AutoBuild"].Value != null && bool.Parse(dgvRows.Cells["AutoBuild"].Value.ToString()))
                    {
                        int lTempIndex = dgEnterSales.CurrentRow.Index;
                        if (dgvRows.Cells["BundleType"].Value.ToString() == "Dynamic")
                            IsAutoBuild(int.Parse(dgvRows.Cells["ItemID"].Value.ToString()), lLastIndex);
                      
                    }
                    else
                    {
                        VerifyItemQty(lLastIndex, (XsalesID == "" ? "0" : XsalesID));
                    }

                }

                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ContractPriceAmount(string cusID, string itemid, int pRowIndex)
        {
            bool lRet = false;
            DateTime dtime = DateTime.Now;
            //DateTime dtpfromutc = datenow.Value.ToUniversalTime();
            DateTime timeutc = dtime.ToUniversalTime();
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sqlSelect = @"SELECT ContractPrice, IsExpiry, ExpiryDate FROM ContractPricing 
            WHERE CustomerID = " + cusID + "AND ExpiryDate > @DateNow and ItemID = " + itemid;
            param.Add("@DateNow", dtime);
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sqlSelect, param);
            DataGridViewRow dgvRows = dgEnterSales.Rows[pRowIndex];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvRows.Cells["Price"].Value = dr["ContractPrice"].ToString();
                }
                lRet = true;
            }
            return lRet;
        }
      
        public bool CheckExistItemInGrid(string pNum)
        {
            int shipCount = 0;
            if (dgEnterSales.Rows.Count > 0)
            {
                for (int i = 0; i < dgEnterSales.Rows.Count; i++)
                {
                    DataGridViewRow dgvr = dgEnterSales.Rows[i];
                    if (dgvr.Cells["PartNumber"].Value != null)
                    {
                        if (dgvr.Cells["PartNumber"].Value.ToString() == pNum)
                        {
                            if (dgvr.Cells["Price"].Value != null && dgvr.Cells["Price"].Value.ToString() != "0")//free Table validation
                            {
                                shipCount += 1;
                                changeqty = true;
                                dgvr.Cells["Ship"].Value = int.Parse(dgvr.Cells["Ship"].Value.ToString()) + 1;
                                if (dgvr.Cells["AutoBuild"].Value != null && bool.Parse(dgvr.Cells["AutoBuild"].Value.ToString()))
                                {
                                    int lTempIndex = dgEnterSales.CurrentRow.Index;
                                    if (dgvr.Cells["BundleType"].Value.ToString() == "Dynamic")
                                        IsAutoBuild(int.Parse(dgvr.Cells["ItemID"].Value.ToString()), i);
                                    
                                }
                                else
                                {
                                    VerifyItemQty(i, (XsalesID == "" ? "0" : XsalesID));
                                }

                                itemcalc(i);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public decimal qtyvalidation(decimal itemqty)

        {
            if (itemqty < 0)
            {
                if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                {
                    VoidValidation vv = new VoidValidation("Return Item Override");
                    if (vv.ShowDialog() == DialogResult.OK)
                    {
                        DataRow rw = dtv.NewRow();

                        rw["UserName"] = vv.GetUsername;
                        rw["AuditAction"] = "Quantity over ride to " + itemqty + " by " + vv.GetUsername;
                        dtv.Rows.Add(rw);
                    }
                    else
                    {
                        itemqty = 1;
                    }
                }

            }
            return itemqty;
        }
        private void InitVoidTable()
        {
            dtv.Columns.Add("UserName", typeof(string));
            dtv.Columns.Add("AuditAction", typeof(string));
        }
        private void InitPaymentInfoTb()
        {
            PaymentInfoTb = new DataTable();
            PaymentInfoTb.Columns.Add("RecipientAccountID", typeof(string));
            PaymentInfoTb.Columns.Add("AmountPaid", typeof(decimal));
            PaymentInfoTb.Columns.Add("PaymentMethodID", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentMethod", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentAuthorisationNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentCardNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentNameOnCard", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentExpirationDate", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentCardNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBSB", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankAccountNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankAccountName", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentChequeNumber", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentBankNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentNotes", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentGCNo", typeof(string));
            PaymentInfoTb.Columns.Add("PaymentGCNotes", typeof(string));


            DataRow rw = PaymentInfoTb.NewRow();

            rw["RecipientAccountID"] = AR_AccountID;
            rw["AmountPaid"] = 0;
            rw["PaymentMethodID"] = "0";
            rw["PaymentMethod"] = "";
            rw["PaymentAuthorisationNumber"] = "";
            rw["PaymentCardNumber"] = "";
            rw["PaymentNameOnCard"] = "";
            rw["PaymentExpirationDate"] = "";
            rw["PaymentCardNotes"] = "";
            rw["PaymentBSB"] = "";
            rw["PaymentBankAccountNumber"] = "";
            rw["PaymentBankAccountName"] = "";
            rw["PaymentChequeNumber"] = "";
            rw["PaymentBankNotes"] = "";
            rw["PaymentNotes"] = "";
            rw["PaymentGCNo"] = "";
            rw["PaymentGCNotes"] = "";


            PaymentInfoTb.Rows.Add(rw);
        }
        private void btnTender_Click(object sender, EventArgs e)
        {
            decimal lTotalDue = this.TotalAmount.Value;
            //if (salestype_cb.Text == "ORDER" || salestype_cb.Text == "LAY-BY")
            //{ //This is if getting customer deposit. Paid Today value is passed on as total due if user indicated an amount to pay otherwise pass the Total Amount.
            //    lTotalDue = (this.PaidToday_txt.Value == 0 ? lTotalDue : this.PaidToday_txt.Value);
            //}
            TenderDetails TenderDlg = new TenderDetails(PaymentInfoTb, lTotalDue, CanAdd);
            if (TenderDlg.ShowDialog() == DialogResult.OK)
            {
                PaymentInfoTb = TenderDlg.GetPaymentInfo;
                this.PaidToday_txt.Value = TenderDlg.GetPayedAmount;
                AmountChange.Value = Convert.ToDecimal(TenderDlg.GetChangemount);
                CalcOutOfBalance();
                // this.PaymentMethodTxt.Text = PaymentInfoTb.Rows[0]["PaymentMethod"].ToString();
                this.BalanceDue_txt.Visible = true;
                this.lblBalanceDue.Visible = true;
            }
        }
        private float GetItemListPrice(string pItemID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            float listprice = 0;
            try
            {
                string sql = @"SELECT * from ItemsSellingPrice where ItemID = " + pItemID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                if (RTb.Rows.Count > 0)
                {
                    listprice = float.Parse(RTb.Rows[0]["Level0"].ToString());
                }

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
            return listprice;
        } //END

        public DataTable CheckOnHandQtyNewSale()
        {
            DataTable lTb = new DataTable();
            lTb.Columns.Add("Part Number", typeof(string));
            lTb.Columns.Add("Error", typeof(string));
            DataRow rw;
            DataTable lResTb;
            string lItemID = "";
            decimal lShipQty = 0;
            string lPartNumber = "";
            for (int i = 0; i < dgEnterSales.Rows.Count; i++)
            {
                if (dgEnterSales.Rows[i].Cells["PartNumber"].Value != null)
                {
                    if (dgEnterSales.Rows[i].Cells["PartNumber"].Value.ToString() != "")
                    {
                        if (dgEnterSales.Rows[i].Cells["Ship"].Value != null && dgEnterSales.Rows[i].Cells["Ship"].Value.ToString() != "")
                        {
                            lShipQty = Convert.ToDecimal(dgEnterSales.Rows[i].Cells["Ship"].Value.ToString());
                        }
                        lItemID = dgEnterSales.Rows[i].Cells["ItemID"].Value.ToString();
                        lPartNumber = dgEnterSales.Rows[i].Cells["PartNumber"].Value.ToString();

                        lResTb = GetNewEndingQty(lItemID);
                        if (lResTb.Rows.Count > 0)
                        {
                            decimal lOnHandQty = Convert.ToDecimal(lResTb.Rows[0]["OnHandQty"].ToString());
                            decimal lNewQty = lOnHandQty - lShipQty;
                            if (lNewQty < 0)
                            {
                                rw = lTb.NewRow();
                                rw["Part Number"] = lPartNumber;
                                rw["Error"] = "Not Enough Quantity";
                                lTb.Rows.Add(rw);
                            }
                        }
                        else
                        {
                            rw = lTb.NewRow();
                            rw["Part Number"] = lPartNumber;
                            rw["Error"] = "Missing Item";
                            lTb.Rows.Add(rw);
                        }
                    }
                }
            }
            return lTb;
        } //END

        private DataTable GetNewEndingQty(string pItemID, string pLocID = "1")
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = @"SELECT OnHandQty, i.IsCounted from Items i inner join ItemsQty q on i.ID = q.ItemID where LocationID = " + pLocID + " and q.ItemID = " + pItemID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        private void dgEnterSales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4
                || e.ColumnIndex == 2
                || e.ColumnIndex == 5
                || e.ColumnIndex == 6)
            {
              // itemcalc(e.RowIndex);
                Recalcline(e.ColumnIndex, e.RowIndex);
                CalcOutOfBalance();
            }
            if (e.RowIndex == (this.dgEnterSales.Rows.Count - 1))
            {
                //this.dgEnterSales.Rows.Add();
            }
        }

        private void dgEnterSales_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            
            if (e.ColumnIndex == 4)
            {
                if (dgEnterSales.Rows.Count != 0)
                {
                    if (dgEnterSales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dgEnterSales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                    {
                     //   changeqty = true;
                        itemcalc(e.RowIndex);
                        Recalcline(e.ColumnIndex, e.RowIndex);
                        Recalc();
                        CalcOutOfBalance();
                       
                    }
                }               
            }
        }

        private void dgEnterSales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 //Price
             || e.ColumnIndex == 7 //Amount
             && e.RowIndex != this.dgEnterSales.NewRowIndex)
            {
                if (e.Value != null && e.Value.ToString() != "0" && e.Value.ToString() != "")
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
                else
                {
                    e.Value = "";
                }
            }
            else if (e.ColumnIndex == 6 //Discount
             && e.RowIndex != this.dgEnterSales.NewRowIndex)
            {
                if (e.Value != null)
                {
                    string p = e.Value.ToString().Replace("%", "");
                    float d = float.Parse(p);
                    e.Value = Math.Round(d, 2).ToString() + "%";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int lRowIndex = dgEnterSales.CurrentCell.RowIndex;
            if (dgEnterSales.Rows.Count > 0)
            {
                if (dgEnterSales.Rows[lRowIndex].Cells["PartNumber"].Value != null)
                {
                    if (iSelectedItem())
                    {
                        rowin = dgEnterSales.CurrentCell.RowIndex;
                    }
                    if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                    {
                        VoidValidation DlgVoid = new VoidValidation("Quantity Override");
                        if (DlgVoid.ShowDialog() == DialogResult.OK)
                        {
                            password = DlgVoid.GetPassword;
                            username = DlgVoid.GetUsername;
                            KeyboardOnScreen kbos = new KeyboardOnScreen("Change Quantity Value", "Enter Qty Value : ");
                            if (kbos.ShowDialog() == DialogResult.OK)
                            {
                                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                                string strValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                                if (strValue == "-")
                                {
                                    pValue = strValue + pValue;
                                }

                                dgEnterSales.Rows[rowin].Cells["Ship"].Value = qtyvalidation(decimal.Parse(pValue));
                            }
                        }
                    }
                    else
                    {
                        KeyboardOnScreen kbos = new KeyboardOnScreen("Change Quantity Value", "Enter Qty Value : ");
                        if (kbos.ShowDialog() == DialogResult.OK)
                        {
                            string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                            if (kbos.GetValue == "-" + pValue)
                            {
                                pValue = "-" + pValue;
                            }
                            changeqty = true;
                            dgEnterSales.Rows[rowin].Cells["Ship"].Value = qtyvalidation(decimal.Parse(pValue));
                        }
                    }
                    if (dgEnterSales.Rows[lRowIndex].Cells["AutoBuild"].Value != null && bool.Parse(dgEnterSales.Rows[lRowIndex].Cells["AutoBuild"].Value.ToString()))
                    {
                        int lTempIndex = dgEnterSales.CurrentRow.Index;
                        if (dgEnterSales.Rows[lRowIndex].Cells["BundleType"].Value.ToString() == "Dynamic")
                            IsAutoBuild(int.Parse(dgEnterSales.Rows[lRowIndex].Cells["ItemID"].Value.ToString()), lRowIndex);
                        
                    }
                    else
                    {
                        VerifyItemQty(lRowIndex, (XsalesID == "" ? "0" : XsalesID));
                    }
                    itemcalc(rowin);
                    Recalcline(4, rowin);

                    //Recalc();
                    CalcOutOfBalance();
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }

        }
        void Recalc()
        {
            DataRow rTx = CommonClass.getTaxDetails(ProfileTax);
            if (rTx.ItemArray.Length > 0)
            {
                float ltaxrate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
                string lTaxCollectedAccountID = "";
                lTaxCollectedAccountID = (rTx["TaxCollectedAccountID"] == null ? "0" : rTx["TaxCollectedAccountID"].ToString());
                lTaxRate = ltaxrate;

                if (!IsLoading)
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            lAmount = float.Parse(TotalAmount.Value.ToString(), NumberStyles.Currency);
                            lTaxEx = lAmount / (1 + (lTaxRate / 100));
                            lTaxInc = lAmount;
                            TotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                    else
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            lTaxInc = lAmount * (1 + (lTaxRate / 100));
                            lTaxEx = lAmount;
                            TotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
                else
                {
                    if (CommonClass.IsTaxcInclusiveEnterSales)
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            TotalAmount.Value = Convert.ToDecimal(lTaxInc);
                        }
                    }
                    else
                    {
                        if (TotalAmount.Value.ToString() != null)
                        {
                            TotalAmount.Value = Convert.ToDecimal(lTaxEx);
                        }
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int RemoveIndex = dgEnterSales.CurrentRow.Index;
            if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
            {
                VoidValidation DlgVoid = new VoidValidation("Remove Line Override");
                if (DlgVoid.ShowDialog() == DialogResult.OK)
                {
                    password = DlgVoid.GetPassword;
                    username = DlgVoid.GetUsername;
                    
                    if (RemoveIndex >= 0 && RemoveIndex <= dgEnterSales.Rows.Count)
                    {
                        dgEnterSales.Rows.RemoveAt(RemoveIndex);
                    }
                        
                    CalcOutOfBalance();
                    dgEnterSales.Refresh();
                 
                    
                }
            }
            else
            {
                if (RemoveIndex >= 0 && RemoveIndex <= dgEnterSales.Rows.Count)
                {
                    dgEnterSales.Rows.RemoveAt(RemoveIndex);
                }
                CalcOutOfBalance();
                dgEnterSales.Refresh();
                //dgEnterSales.Rows[rowin].Selected = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (customerName != "" || checksale())
            {
                // TendertoTable();
                salestype = "INVOICE";
                float TotalEndingBalance = 0;
                if (XsalesID == "")// || SrcOfInvoke == CommonClass.InvocationSource.REMINDER)//|| SrcOfInvoke == CommonClass.InvocationSource.USERECURRING
                {
                    if (salestype == "INVOICE")
                    {
                        //string tAmount = String.Format("{0:0.##}", TotalAmount.Value);
                        decimal pAmount = AmountChange.Value;
                        if (PaidToday_txt.Value >= TotalAmount.Value)
                        {
                            SaveQuickSale();
                        }
                        else
                        {
                            MessageBox.Show("Invoice must be paid in full.");
                        }
                    }
                    else
                    {
                        
                        SaveQuickSale();
                    }
                }

            }
        }
        private void SaveIngredientData()
        {
            string lTranDate = salesDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            if (IngredientTb.Rows.Count > 0)
            {
                foreach (DataRow dr in IngredientTb.Rows)
                {
                    if (dr["ItemID"] != null)
                    {
                        if (dr["ItemID"].ToString() != "")
                        {
                            Dictionary<string, object> paramIngredient = new Dictionary<string, object>();
                            string sql = @"INSERT INTO Ingredient (ItemID, Qty, PartNumber, Amount, TaxRate, TaxCode, TaxCollectedAccountID, Cost, TotalCost, Description, SalesID) 
                            VALUES (@ItemID,@Qty,@PartNumber, @Amount, @TaxRate, @TaxCode , @TaxCollectedAccountID, @Cost, @TotalCost, @Description, @SalesID)";
                            paramIngredient.Add("@ItemID", dr["ItemID"].ToString());
                            if (dr["Ship"].ToString() != "")
                            {
                                paramIngredient.Add("@Qty", float.Parse(dr["Ship"].ToString()));
                            }
                            else
                            {
                                paramIngredient.Add("@Qty", 0);
                            }
                            paramIngredient.Add("@PartNumber", dr["PartNumber"].ToString());
                            paramIngredient.Add("@Amount", dr["Amount"].ToString());
                            paramIngredient.Add("@TaxRate", dr["TaxRate"].ToString());
                            paramIngredient.Add("@TaxCode", dr["TaxCode"].ToString());
                            paramIngredient.Add("@TaxCollectedAccountID", dr["TaxCollectedAccountID"].ToString());
                            paramIngredient.Add("@Cost", dr["Cost"].ToString());
                            paramIngredient.Add("@TotalCost", dr["TotalCost"].ToString());
                            paramIngredient.Add("@Description", dr["Description"].ToString());
                            paramIngredient.Add("@SalesID", NewSalesID);
                            if (dr["ItemID"].ToString() != "" && dr["ItemID"].ToString() != "0")
                            {
                                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramIngredient);
                            }
                            if (salestype == "INVOICE")
                            {
                                string CostPrice = "0";
                                string TotalCost = "0";
                                DataTable lItemTb = GetItem(dr["ItemID"].ToString());
                                if (lItemTb.Rows.Count > 0)
                                {
                                    if (float.Parse(dr["Cost"].ToString()) == 0)
                                    {
                                        //CostPrice = AverageCostEx
                                       CostPrice = lItemTb.Rows[0]["AverageCostEx"].ToString();
                                       CostPrice = (CostPrice == "" ? "0" : CostPrice);
                                       TotalCost = (float.Parse(dr["Ship"].ToString()) * float.Parse(CostPrice)).ToString();
                                       TotalCost = (TotalCost == "" ? "0" : TotalCost);
                                    }
                                    else
                                    {
                                        CostPrice = dr["Cost"].ToString();
                                        CostPrice = (CostPrice == "" ? "0" : CostPrice);
                                        TotalCost = (float.Parse(dr["Ship"].ToString()) * float.Parse(CostPrice)).ToString();
                                        TotalCost = (TotalCost == "" ? "0" : TotalCost);
                                    }
                                    bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                                    float lQty = float.Parse(dr["Ship"].ToString());
                                    float lQtySUOM = float.Parse(lItemTb.Rows[0]["QtyPerSellingUnit"].ToString());
                                    float lTranQty = lQty * lQtySUOM;
                                    if (lIsCounted)
                                    {
                                      
                                      string  salesLinesql = "UPDATE ItemsQty SET OnHandQty = OnHandQty - " + lTranQty.ToString() + " WHERE ItemID = " + dr["ItemID"].ToString() ;
                                     int   count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY);
                                        // INSERT SOLD ITEMS IN ITEM TRANSACTION
                                        salesLinesql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                                    VALUES('" + lTranDate + "'," + dr["ItemID"].ToString() + "," + lTranQty + "," + lTranQty + " *(-1)," + CostPrice + "," + TotalCost + ",'SI'," + NewSalesID + "," + CommonClass.UserID + ")";
                                        //paramItemQty.Add("@TransactionDate", salesDate.Value.ToUniversalTime());
                                        count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY);
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        public bool checksale()
        {
            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
            {
                if (dgEnterSales.Rows.Count > 0)
                {
                    if (dgvr.Cells["AccountID"].Value != null && dgvr.Cells["AccountID"].Value.ToString() != "")
                        return true;
                }

            }
            return false;
        }
        private void GenerateInvoiceNum()
        {
            DataTable dt = new DataTable();
            string selectSql = @"SELECT SalesOrderSeries, 
                                               SalesOrderPrefix, 
                                               SalesQuoteSeries, 
                                               SalesQuotePrefix,
                                               SalesInvoiceSeries,
                                               SalesInvoicePrefix 
                                        FROM TransactionSeries";

            CommonClass.runSql(ref dt, selectSql);
            string lSeries = "";
            int lCnt = 0;
            int lNewSeries = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (salestype == "ORDER")
                    {
                        lSeries = (dt.Rows[i]["SalesOrderSeries"].ToString());
                        lCnt = lSeries.Length;
                        lSeries = lSeries.TrimStart('0');
                        lSeries = (lSeries == "" ? "0" : lSeries);
                        lNewSeries = Convert.ToInt16(lSeries) + 1;
                        CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                        invoicenum = (dt.Rows[i]["SalesOrderPrefix"].ToString()).Trim(' ') + CurSeries;
                    }
                    else if (salestype == "QUOTE")
                    {
                        lSeries = (dt.Rows[i]["SalesQuoteSeries"].ToString());
                        lCnt = lSeries.Length;
                        lSeries = lSeries.TrimStart('0');
                        lSeries = (lSeries == "" ? "0" : lSeries);
                        lNewSeries = Convert.ToInt16(lSeries) + 1;
                        CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                        invoicenum = (dt.Rows[i]["SalesQuotePrefix"].ToString()).Trim(' ') + CurSeries;
                    }
                    else
                    {
                        lSeries = (dt.Rows[i]["SalesInvoiceSeries"].ToString());
                        lCnt = lSeries.Length;
                        lSeries = lSeries.TrimStart('0');
                        lSeries = (lSeries == "" ? "0" : lSeries);
                        lNewSeries = Convert.ToInt16(lSeries) + 1;
                        CurSeries = lNewSeries.ToString().PadLeft(lCnt, '0');
                        invoicenum = (dt.Rows[i]["SalesInvoicePrefix"].ToString()).Trim(' ') + CurSeries;
                    }
                }
            }
            else
            {
                MessageBox.Show("Transaction Series Numbers not setup properly.");
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
        public static DataTable GetItem(string pItemID)
        {
            SqlConnection con = null;
            DataTable RTb = null;
            try
            {
                string sql = "SELECT i.*, q.OnHandQty,q.CommitedQty, c.AverageCostEx from ( Items i inner join ItemsQty q on i.ID = q.ItemID ) inner join ItemsCostPrice c on i.ID = c.ItemID where i.ID = " + pItemID;
                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                RTb = new DataTable();
                da.Fill(RTb);
                return RTb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return RTb;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        public int SaveQuickSale(bool pIsRecurring = false)
        {
           
            int count = 0;
            int NewTermID = 0;
            GenerateInvoiceNum();
            string layout = "";
            Dictionary<string, object> param = new Dictionary<string, object>();

            string savesql = @"INSERT INTO Sales ( SalesType,
                                                       CustomerID,
                                                       UserID,
                                                       SalesNumber,
                                                       TransactionDate,
                                                       ShippingMethodID,
                                                       SubTotal,
                                                       FreightSubTotal,
                                                       FreightTax, 
                                                       TaxTotal,
                                                       LayoutType,
                                                       GrandTotal,
                                                       InvoiceStatus,
                                                       IsTaxInclusive,
                                                       ShippingContactID,
                                                       ClosedDate,
                                                       TotalPaid,
                                                       TotalDue,
                                                       FreightTaxCode,
                                                       FreightTaxRate,
                                                       SalesPersonID , 
                                                       SessionID,
                                                       InvoiceType,
                                                       PromiseDate,
                                                       Comments,
                                                       TableNumber,
                                                       OrderStatus,
                                                        Memo) 
                                              VALUES ( @SalesType,
                                                       @CustomerID,
                                                       @UserID,
                                                       @SalesNum,
                                                       @TransDate,
                                                       @ShippingMethodID,
                                                       @SubTotal,
                                                       @FreightSubTotal,
                                                       @FreightTax, 
                                                       @TaxTotal,
                                                       @Layout,
                                                       @GrandTotal,
                                                       @InvoiceStatus,
                                                       @isTaxInclusive,
                                                       @ShippingID,
                                                       @ClosedDate,
                                                       @TotalPaid,
                                                       @TotalDue,
                                                       @FreightTaxCode,
                                                       @FreightTaxRate,
                                                       @SalesPersonID ,
                                                       @SessionID,
                                                       @InvoiceType,
                                                       @PromiseDate,
                                                       @Comments,
                                                       @TableNumber,
                                                       @OrderStatus,
                                                       @Memo);  
                                            SELECT SCOPE_IDENTITY()";

            param.Add("@SalesType", salestype);
            param.Add("@CustomerID", CustomerID);
            param.Add("@UserID", CommonClass.UserID);
            param.Add("@SalesNum", invoicenum);
            param.Add("@TransDate", salesDate.Value.ToUniversalTime());
            param.Add("@ShippingID", shipAddressID);
            param.Add("@SessionID", CommonClass.SessionID);
            param.Add("@InvoiceType", "Cash");
            param.Add("@PromiseDate", DateTime.Now.ToUniversalTime());

            layout = "Item";

            param.Add("@InvoiceStatus", "Closed");
            param.Add("@ClosedDate", DateTime.Now.ToUniversalTime());

            param.Add("@Layout", layout);
            param.Add("@ShippingMethodID", ShippingMethodID == null || ShippingMethodID == "" ? "" : ShippingMethodID);
            param.Add("@SubTotal", subtotalAmountText.Value);
            param.Add("@FreightSubTotal", FreightAmountEx);
            param.Add("@FreightTax", FreightTax);
            param.Add("@TaxTotal", TaxAmount.Value);
            param.Add("@TotalPaid", 0);
            param.Add("@TotalDue", BalanceDue_txt.Value);
            param.Add("@GrandTotal", TotalAmount.Value);

            param.Add("@TaxID", TaxID);
            param.Add("@FreightTaxCode", "N-T");//txtFTaxCode.Text
            param.Add("@FreightTaxRate", FreightTaxRate);
            param.Add("@SalesPersonID", SalesPersonID);
            param.Add("@Comments", Note);
            param.Add("@TableNumber", tblNum.Text);
            param.Add("@OrderStatus", "New");
            param.Add("@Memo", "Sale:" + invoicenum);

            if (CommonClass.IsTaxcInclusiveEnterSales)
            {
                param.Add("@isTaxInclusive", "Y");
            }
            else
            {
                param.Add("@isTaxInclusive", "N");
            }
            NewSalesID = CommonClass.runSql(savesql, CommonClass.RunSqlInsertMode.SCALAR, param);

            //SalesLines
            string Descript;
            string Amount = "";
            string Job = "";
            string Tax = "";
            string TaxRate = "";
            string OrderQty = "";
            string BackOrderQty = "";
            string ShipQty = "";
            string UnitPrice = "";
            string ActualPrice = "";
            string Discount = "";
            string CostPrice = "";
            string TotalCost = "";
            string payment = "";
            int entity = 0;
            string promoid = "";
            Dictionary<string, object> paramItemQty = new Dictionary<string, object>();
            for (int i = 0; i < this.dgEnterSales.Rows.Count; i++)
            {
                Dictionary<string, object> paramSalesLine = new Dictionary<string, object>();
                if (this.dgEnterSales.Rows[i].Cells["Description"].Value != null)
                {
                    if (this.dgEnterSales.Rows[i].Cells["Description"].Value.ToString() != "")
                    {
                        Descript = String.Format("{0}", dgEnterSales.Rows[i].Cells["Description"].Value.ToString());
                        Amount = dgEnterSales.Rows[i].Cells["Amount"].Value.ToString();
                        double dAmount = double.Parse(Amount, NumberStyles.Currency);
                        entity = Convert.ToInt32(dgEnterSales.Rows[i].Cells["ItemID"].Value);

                        TaxID = dgEnterSales.Rows[i].Cells["TaxCollectedAccountID"].Value.ToString();
                        Job = dgEnterSales.Rows[i].Cells["JobID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["JobID"].Value.ToString();
                        Tax = dgEnterSales.Rows[i].Cells["TaxCode"].Value.ToString();
                        TaxRate = dgEnterSales.Rows[i].Cells["TaxRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["TaxRate"].Value.ToString();
                        double taxEx = Convert.ToDouble(dgEnterSales.Rows[i].Cells["TaxExclusiveAmount"].Value.ToString());
                        double TaxAm = dAmount - taxEx;


                        string salesLinesql = "";
                      
                        ShipQty = dgEnterSales.Rows[i].Cells["Ship"].Value.ToString();
                        UnitPrice = dgEnterSales.Rows[i].Cells["Price"].Value.ToString();
                        ActualPrice = dgEnterSales.Rows[i].Cells["ActualUnitPrice"].Value.ToString();
                        Discount = dgEnterSales.Rows[i].Cells["DiscountRate"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["DiscountRate"].Value.ToString();
                        ShipQ = Convert.ToInt32(ShipQty);
                        CostPrice = dgEnterSales.Rows[i].Cells["CostPrice"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["CostPrice"].Value.ToString();
                        TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();
                        promoid = dgEnterSales.Rows[i].Cells["PromoID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString();
                        string lTranDate = salesDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        if (salestype == "INVOICE")
                        {
                        
                            DataTable lItemTb = GetItem(entity.ToString());
                            if (lItemTb.Rows.Count > 0)
                            {
                                if((bool)lItemTb.Rows[0]["IsAutoBuild"] && lItemTb.Rows[0]["BundleType"].ToString() == "Ingredient")
                                {
                                    IsIngredientBuild(entity, i);
                                }

                                if (float.Parse(CostPrice) == 0)
                                {
                                    //CostPrice = AverageCostEx
                                    CostPrice = lItemTb.Rows[0]["AverageCostEx"].ToString();
                                    CostPrice = (CostPrice == "" ? "0" : CostPrice);
                                    TotalCost = (float.Parse(ShipQty) * float.Parse(CostPrice)).ToString();
                                    TotalCost = (TotalCost == "" ? "0" : CostPrice);
                                }
                                bool lIsCounted = (bool)lItemTb.Rows[0]["IsCounted"];
                                float lQty = float.Parse(ShipQty);
                                float lQtySUOM = float.Parse(lItemTb.Rows[0]["QtyPerSellingUnit"].ToString());
                                float lTranQty = lQty * lQtySUOM;
                                if (lIsCounted)
                                {
                                   
                                    salesLinesql = "UPDATE ItemsQty SET OnHandQty = OnHandQty - " + lTranQty.ToString() + " WHERE ItemID = " + entity;
                                    count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY);
                                    // INSERT SOLD ITEMS IN ITEM TRANSACTION
                                    salesLinesql = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                                    VALUES('" + lTranDate + "'," + entity + "," + lTranQty + "," + lTranQty + " *(-1)," + CostPrice + "," + TotalCost + ",'SI'," + NewSalesID + "," + CommonClass.UserID + ")";
                                    //paramItemQty.Add("@TransactionDate", salesDate.Value.ToUniversalTime());
                                    count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.QUERY, paramItemQty);
                                }
                            }
                        }
                        promoid = dgEnterSales.Rows[i].Cells["PromoID"].Value == null ? "0" : dgEnterSales.Rows[i].Cells["PromoID"].Value.ToString();
                        promoid = promoid == "" ? "0" : promoid;

                        salesLinesql = "INSERT INTO SalesLines (SalesID, Description, TotalAmount, TransactionDate, TaxCode, UnitPrice, ActualUnitPrice, DiscountPercent, OrderQty, ShipQty, EntityID, JobID, SubTotal, TaxAmount, TaxCollectedAccountID,TaxRate, CostPrice, TotalCost, PromoID, KitchenStatus)" +
                                       " VALUES (@SalesID, @Description, @TotalAmount,@TransactionDate,@TaxCode,@UnitPrice,@ActualUnitPrice,@DiscountPercent,@OrderQty,@ShipQty,@EntityID,@JobID,@SubTotal,@TaxAmount,@TaxCollectedAccountID,@TaxRate,@CostPrice,@TotalCost,@PromoID, @KitchenStatus)";

                        paramSalesLine.Add("@SalesID", NewSalesID);
                        paramSalesLine.Add("@Description", Descript);
                        paramSalesLine.Add("@TotalAmount", dAmount);
                        paramSalesLine.Add("@TransactionDate", lTranDate);
                        paramSalesLine.Add("@TaxCode", Tax);
                        paramSalesLine.Add("@UnitPrice", UnitPrice);
                        paramSalesLine.Add("@ActualUnitPrice", ActualPrice);
                        paramSalesLine.Add("@DiscountPercent", Discount);
                        paramSalesLine.Add("@OrderQty", OrderQty);
                        paramSalesLine.Add("@ShipQty", ShipQty);
                        paramSalesLine.Add("@EntityID", entity);
                        paramSalesLine.Add("@JobID", Job); 
                        paramSalesLine.Add("@SubTotal", taxEx);
                        paramSalesLine.Add("@TaxAmount", TaxAm);
                        paramSalesLine.Add("@TaxCollectedAccountID", TaxID);
                        paramSalesLine.Add("@TaxRate", TaxRate);
                        paramSalesLine.Add("@CostPrice", CostPrice);
                        paramSalesLine.Add("@TotalCost", TotalCost);
                        paramSalesLine.Add("@PromoID", promoid);
                        paramSalesLine.Add("@KitchenStatus", "New");
                        

                        count = CommonClass.runSql(salesLinesql, CommonClass.RunSqlInsertMode.SCALAR, paramSalesLine);

                        double lPoints = dgEnterSales.Rows[i].Cells["Points"].Value != null ? Math.Round(float.Parse(dgEnterSales.Rows[i].Cells["Points"].Value.ToString()), 2) : 0;

                        if (lPoints != 0)
                        {
                            Dictionary<string, object> parampts = new Dictionary<string, object>();
                            parampts.Add("@PromoID", dgEnterSales.Rows[i].Cells["PromoID"].Value);
                            parampts.Add("@Points", lPoints);
                            parampts.Add("@CustomerID", memberID);
                            parampts.Add("@TransDate", salesDate.Value.ToUniversalTime());
                            parampts.Add("@SalesLineID", count);
                            parampts.Add("@ItemID", entity);

                            string promosql = "";

                            if (lPoints < 0)
                            {
                                parampts.Add("@RedemptionType", dgEnterSales.Rows[i].Cells["RedemptionType"].Value);
                                parampts.Add("@RedeemID", dgEnterSales.Rows[i].Cells["RedeemID"].Value.ToString());
                                promosql = @"INSERT INTO AccumulatedPoints (
                                                            PromoID, 
                                                            TransactionDate, 
                                                            PointsAccumulated, 
                                                            CustomerID, 
                                                            SalesLineID,
                                                            ItemID,
                                                            RedemptionType,
                                                            RedeemID) 
                                                VALUES (
                                                        @PromoID, 
                                                        @TransDate, 
                                                        @Points, 
                                                        @CustomerID, 
                                                        @SalesLineID,
                                                        @ItemID,
                                                        @RedemptionType,
                                                        @RedeemID)";
                                if (dgEnterSales.Rows[i].Cells["RedemptionType"].Value.ToString() == "Gift Certificate")
                                {
                                    string gcsql = @"Update GiftCertificate set issuedSalesID = " + NewSalesID + " where ID = " + dgEnterSales.Rows[i].Cells["RedeemID"].Value.ToString();
                                    CommonClass.runSql(gcsql);
                                }
                            }

                            else
                            {
                                promosql = @"INSERT INTO AccumulatedPoints (
                                                            PromoID, 
                                                            TransactionDate, 
                                                            PointsAccumulated, 
                                                            CustomerID, 
                                                            SalesLineID,
                                                            ItemID ) 
                                                    VALUES (
                                                            @PromoID, 
                                                            @TransDate, 
                                                            @Points, 
                                                            @CustomerID, 
                                                            @SalesLineID,
                                                            @ItemID )";
                            }

                            CommonClass.runSql(promosql, CommonClass.RunSqlInsertMode.QUERY, parampts);
                        }
                    }
                }
            }
           // UpdateItemQty();
            SaveIngredientData();
            UpdateTransSeries(ref cmd);
            if (SrcOfInvoke == CommonClass.InvocationSource.USERECURRING ||
                SrcOfInvoke == CommonClass.InvocationSource.REMINDER)
            {
                UpdateNotify();
            }

            if (salestype == "INVOICE")
            {
                CreateJournalEntries(NewSalesID);
                if (PaidToday_txt.Value != 0)
                {
                    RecordPayment(invoicenum, PaidToday_txt.Value, 0);
                }
            }
            CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Sales  " + salestype + " No. " + invoicenum);
            saveValidationLog(NewSalesID);
            if (SrcOfInvoke == CommonClass.InvocationSource.REMINDER || SrcOfInvoke == CommonClass.InvocationSource.SAVERECURRING)
            {
                MessageBox.Show("Sales Record has been created");
                return NewSalesID;
            }
            if (count != 0 && SrcOfInvoke != CommonClass.InvocationSource.SAVERECURRING)
            {
                ChangeAmount DlgChange = new ChangeAmount(AmountChange.Value.ToString());
                DlgChange.ShowDialog();

                string titles = "Information";
                DialogPrint printsale = new DialogPrint("Would you like to print the new  " + salestype + " created?");
                if (printsale.ShowDialog() == DialogResult.OK)
                {
                    LoadItemLayoutReport(NewSalesID.ToString());
                    foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
                    {
                        if (dgvr.Cells["RedemptionType"].Value != null)
                        {
                            if (dgvr.Cells["RedemptionType"].Value.ToString() == "Gift Certificate")
                            {
                                PrintGC();
                            }
                        }
                    }
                }
                
                
                DialogPrint createNew = new DialogPrint("Sales Record has been created. Would you like to enter a new " + salestype + "?");
                if (createNew.ShowDialog() == DialogResult.OK)
                {   //clear for new datas
                    dgEnterSales.Rows.Clear();
                    dgEnterSales.Refresh();
                    customerName = "";
                    // PayeeInfo.Clear();
                    PopulateDataGridView();
                    invoicenum = "";
                    // InvoiceNumTxt.Visible = false;
                    TotalAmount.Value = 0;
                    BalanceDue_txt.Value = 0;
                    PaidToday_txt.Value = 0;
                    AmountChange.Value = 0;
                    salesDate.Value = DateTime.Now;
                    rdoDineIn.Checked = true;

                    // ShippingmethodText.Clear();/ RadioButton change 
                    // InitContactInfoTb();
                    // LoadPaymentMethods();
                    LoadDefaultCustomer();
                    memberID = "";
                    txtCustPoints.Visible = false;
                    btnRedeem.Visible = false;
                    //lblPoints.Visible = false;

                    //QuickSalesTabControl.SelectedIndex = 0;
                    memberID = "";
                    PaymentInfoTb.Clear();
                    InitPaymentInfoTb();
                    IngredientTb.Clear();
                    InitIngredientInfoTb();
                    InitFreeTable();
                    InitPaymentInfoTb();
                    InitVoidTable();
                    dtv.Rows.Clear();
                    rowin = 0;
                }
                else
                {
                    CommonClass.RestoPOS.Close();
                }
            }
            return NewSalesID;
        }
        public void PrintGC()
        {
            DataTable TbRep = new DataTable();
            TbRep.Columns.Add("GCNumber", typeof(string));
            TbRep.Columns.Add("GCAmount", typeof(float));
            TbRep.Columns.Add("ItemNumber", typeof(string));
            TbRep.Columns.Add("EndDate", typeof(string));
            TbRep.Columns.Add("ProfileName", typeof(string));

            foreach (DataGridViewRow dgvr in dgEnterSales.Rows)
            {
                if (dgvr.Cells["RedeemID"].Value != null)
                {
                    string sql = @"SELECT DISTINCT g.GCNumber, g.GCAmount, i.ItemNumber, g.EndDate,
					p.Name as ProfileName
                    FROM Sales
                    INNER JOIN Profile p on Sales.CustomerID = p.ID                    
                    INNER JOIN SalesLines sl ON sl.SalesID = Sales.SalesID
                    INNER JOIN Items i ON sl.EntityID = i.ID                   
					INNER JOIN GiftCertificate g ON g.ItemID = i.ID
                    WHERE g.ID = " + dgvr.Cells["RedeemID"].Value.ToString();
                    DataTable dt = new DataTable();
                    CommonClass.runSql(ref dt, sql);
                    DataRow dr = dt.Rows[0];
                    for (int x = 0; x < int.Parse(dgvr.Cells["Ship"].Value.ToString()); x++)
                    {
                        DataRow rw = TbRep.NewRow();
                        rw["GCNumber"] = "*" + dr[0] + "*";
                        rw["GCAmount"] = dr[1];
                        rw["ItemNumber"] = dr[2].ToString();
                        rw["EndDate"] = dr[3];
                        rw["ProfileName"] = dr[4].ToString();
                        TbRep.Rows.Add(rw);
                    }

                }
            }
            Reports.ReportParams GCReceipt = new Reports.ReportParams();
            GCReceipt.PrtOpt = 1;
            GCReceipt.Rec.Add(TbRep);
            GCReceipt.ReportName = "GCReceipt.rpt";
            GCReceipt.RptTitle = "Gift Certificate";
            GCReceipt.Params = "compname|CompAddress|TIN";
            GCReceipt.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim();

            CommonClass.ShowReport(GCReceipt);
        }

        private void LoadItemLayoutReport(string pSalesID)
        {
            Reports.ReportParams salesItemlayoutparams = new Reports.ReportParams();
            salesItemlayoutparams.PrtOpt = 1;

            SqlConnection con = new SqlConnection(CommonClass.ConStr);
            string printSale = @"SELECT SalesType, SalesNumber , Sales.TransactionDate  , Sales.Comments, Sales.SubTotal , p.Name as ProfileName,
                    c.Street as ShipStreet, c.City as ShipCity, c.State as ShipState,
                   c2.Street as BillStreet, c2.City as BillCity, c2.State as BillState, 
                    sl.Description ,sl.TotalAmount, sl.TaxCode,Sales.TaxTotal,FreightTax,TotalPaid, GrandTotal,TotalDue,
                    ShippingMethod, i.ItemNumber, sl.ShipQty, sl.OrderQty, sl.CostPrice, sl.UnitPrice, sl.DiscountPercent, u.user_fullname, Sales.SalesID, ISNULL(Sales.TableNumber,'') as TableNumber
                    FROM Sales
                    INNER JOIN Profile p on Sales.CustomerID = p.ID
                    Left JOIN ShippingMethods  sm On sm.ShippingID = Sales.ShippingMethodID
                    LEFT JOIN Contacts c ON c.Location = Sales.ShippingContactID and c.ProfileID = Sales.CustomerID  
                    LEFT JOIN Contacts c2 ON c2.Location = Sales.BillingContactID and c2.ProfileID = Sales.CustomerID 
                    INNER JOIN SalesLines sl ON sl.SalesID = Sales.SalesID
                    INNER JOIN Items i ON sl.EntityID = i.ID
                    INNER JOIN Users u ON u.user_id = Sales.SalesPersonID 
					WHERE Sales.SalesID = '" + pSalesID + "'";
            SqlCommand cmd = new SqlCommand(printSale, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            salesItemlayoutparams.Rec.Add(dt);
            string tenders = @"SELECT DISTINCT pd.PaymentDetailsID, pd.PaymentMethodID,pt.Amount,pm.PaymentMethod,p.TransactionDate FROM PaymentTender pt 
                        inner join PaymentDetails pd on pt.PaymentID = pd.PaymentID and pt.id = pd.PaymentDetailsID
                        inner join Payment p on pt.PaymentID = p.PaymentID
                        inner join PaymentLines pl on pt.PaymentID = pl.PaymentID
                        left join PaymentMethods pm on pd.PaymentMethodID = pm.id
                        where pt.Amount <> 0 and pl.EntityID = " + pSalesID;
            SqlConnection con2 = new SqlConnection(CommonClass.ConStr);
            SqlCommand cmd2 = new SqlCommand(tenders, con2);

            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            decimal lchange = 0;
            string lTenders = "";

            for (int x = 0; x < dt2.Rows.Count; x++)
            {
                if (dt2.Rows[x]["PaymentMethod"].ToString().ToUpper() == "CASH")
                {
                    if (Convert.ToDecimal(dt2.Rows[x]["Amount"].ToString()) < 0)
                    {
                        dt2.Rows[x]["PaymentMethod"] = "Change";
                    }
                }
                lTenders += dt2.Rows[x]["PaymentMethod"].ToString() + "  " + Convert.ToDecimal(dt2.Rows[x]["Amount"].ToString()).ToString("C2") + "/";
            }

            salesItemlayoutparams.ReportName = "Receipt76logoResto.rpt";
            salesItemlayoutparams.RptTitle = "Item Layout 76mm";
            salesItemlayoutparams.PrtOpt = 0;
            salesItemlayoutparams.Params = "compname|CompAddress|TIN|LogoPath|tenders";
            salesItemlayoutparams.PVals = CommonClass.CompName.Trim() + "|" + CommonClass.CompAddress.Trim() + "|" + CommonClass.CompSalesTaxNo.Trim() + "|" + CommonClass.CompLogoPath + "|" + lTenders;

            CommonClass.ShowReport(salesItemlayoutparams);
        }
        public static DataTable GetSalesLines(int pSalesID)
        {
            DataTable RTb = new DataTable();

            string sql = @"SELECT l.*, s.SalesNumber, s.GrandTotal, s.Memo, s.FreightSubTotal, s.FreightTax, s.FreightTaxCode, s.FreightTaxRate, i.AssetAccountID, 
                    i.IsCounted, i.IncomeAccountID, i.IsSold, i.COSAccountID, i.IsBought FROM ( SalesLines l INNER JOIN Sales s ON l.SalesID = s.SalesID ) left join Items as i on l.EntityID =i.ID WHERE l.SalesID = " + pSalesID;
            CommonClass.runSql(ref RTb, sql);
            return RTb;
        } //END

        public static DataTable GetUsedIngredients(string pSalesID)
        {
            DataTable RTb = new DataTable();

            string sql = @"SELECT ing.*, i.AssetAccountID, i.COSAccountID, i.IsCounted, i.IsBought
  FROM Ingredient ing inner join Items i on ing.ItemID = i.ID where ing.SalesID = " + pSalesID;
            CommonClass.runSql(ref RTb, sql);
            return RTb;
        } //END

        private bool CreateJournalEntries(int pID)
        {
            SqlConnection con = null;
            try
            {
                string sql = "";

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                DataTable ltb = GetSalesLines(pID);
                if (ltb.Rows.Count > 0)
                {
                    decimal lGrandTotal = Convert.ToDecimal(ltb.Rows[0]["GrandTotal"].ToString());
                    string lRecipientID = AR_AccountID;
                    string lSalesNum = invoicenum;
                    string lMemo = String.Format("{0}", ltb.Rows[0]["Memo"].ToString());
                    string lTranDate = ((DateTime)ltb.Rows[0]["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    decimal lFreightEx = Convert.ToDecimal(ltb.Rows[0]["FreightSubTotal"].ToString());
                    decimal lFreightTax = Convert.ToDecimal(ltb.Rows[0]["FreightTax"].ToString());
                    string lSalesID = ltb.Rows[0]["SalesID"].ToString();
                    cmd.Parameters.AddWithValue("@lMemo", lMemo);
                    //INSERT JOURNAL FOR Total Amount Received
                    if (TotalAmount.Value < 0)
                    {
                        //NEGATIVE SO CREDIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, EntityID)  " +
                              " VALUES('" + lTranDate + "',  @lMemo, @lMemo, '" + lRecipientID + "', " +
                             (TotalAmount.Value * -1).ToString() + ",'" + lSalesNum + "', 'SI', " + lSalesID + ")";
                    }
                    else
                    {
                        //DEBIT AMOUNT
                        sql = "INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, EntityID) " +
                               " VALUES('" + lTranDate + "', @lMemo, @lMemo,'" + lRecipientID + "', " +
                              TotalAmount.Value + ",'" + lSalesNum + "','SI', " + lSalesID + ")";
                    }
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    //INSERT JOURNAL FOR FREIGHT 
                    if (lFreightEx != 0)
                    {
                        if (lFreightEx < 0) // NEGATIVE SO DEBIT AMOUNT 
                        {
                            // NEGATIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate + "',  @lMemo, @lMemo, '" + AR_FreightAccountID + "', " +
                                  (lFreightEx * -1).ToString() + ", '" + lSalesNum + "', 'SI',0, '" + lSalesID + "')";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lFreightTax != 0 && FreightTaxAccountID != "")
                            {

                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    DebitAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "',  @lMemo, @lMemo, '" + FreightTaxAccountID + "', " +
                                      (lFreightTax * -1).ToString() + ", '" + lSalesNum + "', 'SI',0, '" + lSalesID + "')"; ;
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else //POSITIVE SO CREDIT AMOUNT 
                        {
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                   VALUES('" + lTranDate + "', @lMemo, @lMemo, '" + AR_FreightAccountID + "', " +
                                   lFreightEx.ToString() + ", '" + lSalesNum + "', 'SI',0, '" + lSalesID + "')";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lFreightTax != 0 && FreightTaxAccountID != "")
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID, 
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID)
                                      VALUES('" + lTranDate + "',  @lMemo, @lMemo, '" + FreightTaxAccountID + "', " +
                                     lFreightTax.ToString() + ", '" + lSalesNum + "','SI',0, '" + lSalesID + "')";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    for (int i = 0; i < ltb.Rows.Count; i++)
                    {
                        string lAccountID = "";
                        decimal lTaxEx = Convert.ToDecimal(ltb.Rows[i]["SubTotal"].ToString());
                        decimal lTaxInc = Convert.ToDecimal(ltb.Rows[i]["TotalAmount"].ToString());
                        decimal lTaxAmt = lTaxInc - lTaxEx;
                        string lTaxCollectedAccountID = (ltb.Rows[i]["TaxCollectedAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["TaxCollectedAccountID"].ToString());
                        string lJobID = (ltb.Rows[i]["JobID"].ToString() == "" ? "0" : ltb.Rows[i]["JobID"].ToString());
                        string lIncomeAccountID = (ltb.Rows[i]["IncomeAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["IncomeAccountID"].ToString());
                        string lAssetAccountID = (ltb.Rows[i]["AssetAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["AssetAccountID"].ToString());
                        string lCOSAccountID = (ltb.Rows[i]["COSAccountID"].ToString() == "" ? "0" : ltb.Rows[i]["COSAccountID"].ToString());
                        bool lIsCounted = (ltb.Rows[i]["IsCounted"].ToString() == "" ? false : (bool)ltb.Rows[i]["IsCounted"]);

                        //  string lLineMemo = ltb.Rows[i]["LineMemo"].ToString();
                        string lEntity = ltb.Rows[i]["EntityID"].ToString();
                        lAccountID = lIncomeAccountID;

                        if (lTaxEx < 0) // NEGATIVE SO DEBIT AMOUNT 
                        {
                            //FOR INCOME
                            // NEGATIVE SO DEBIT AMOUNT 
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,DebitAmount, TransactionNumber, Type, JobID, EntityID) VALUES('" + lTranDate + "','" + lMemo + "','" + lMemo + "'," + lAccountID + "," + (lTaxEx * -1) + ",'" + lSalesNum + "', 'SI','" + lJobID + "','" + lEntity + "')";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0)
                            {
                                lTaxAmt = lTaxEx - lTaxInc;
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo, AccountID,
                                                    DebitAmount, TransactionNumber, Type, JobID, EntityID)";
                                sql += " VALUES('" + lTranDate + "', @lMemo, @lMemo,'" + lTaxCollectedAccountID + "', " + lTaxAmt.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + ",'" + lSalesID + "')";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            if (lIsCounted)
                            {
                                decimal lTotalCost = (ltb.Rows[i]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(ltb.Rows[i]["TotalCost"].ToString()));
                                //FOR INVENTORY
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', @lMemo, @lMemo,'" + lAssetAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                //FOR COST OF SALES
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lCOSAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else //POSITIVE SO CREDIT AMOUNT 
                        {
                            //FOR INCOME
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,CreditAmount, TransactionNumber, Type, JobID, EntityID) 
VALUES ('" + lTranDate + "',  @lMemo, @lMemo, '" + lAccountID + "', " + lTaxEx + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            //THIS IS FOR THE TAX COMPONENT
                            if (lTaxAmt != 0 && lTaxCollectedAccountID != "")
                            {
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID,
                                                    CreditAmount, TransactionNumber, Type, JobID, EntityID) VALUES ('" + lTranDate + "', @lMemo, @lMemo,'" + lTaxCollectedAccountID + "'," + lTaxAmt.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + ",'" + lSalesID + "')";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            if (lIsCounted)
                            {
                                decimal lTotalCost = (ltb.Rows[i]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(ltb.Rows[i]["TotalCost"].ToString()));
                                //FOR INVENTORY
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', @lMemo, @lMemo,'" + lAssetAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                //FOR COST OF SALES
                                sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lCOSAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI'," + lJobID + "," + lEntity + ")";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }


                        }

                       
                    }
                    //GET USED INGREDIENTS
                    
                    DataTable lTbIng = GetUsedIngredients(pID.ToString());
                    for (int j = 0; j < lTbIng.Rows.Count; j++)
                    {
                        decimal lTotalCost = (lTbIng.Rows[j]["TotalCost"].ToString() == "" ? 0 : Convert.ToDecimal(lTbIng.Rows[j]["TotalCost"].ToString()));
                        string lAssetAccountID = (lTbIng.Rows[j]["AssetAccountID"].ToString() == "" ? "0" : lTbIng.Rows[j]["AssetAccountID"].ToString());
                        string lCOSAccountID = (lTbIng.Rows[j]["COSAccountID"].ToString() == "" ? "0" : lTbIng.Rows[j]["COSAccountID"].ToString());
                        if (lTotalCost < 0)
                        {
                            //FOR INGREDIENT INVENTORY
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', @lMemo, @lMemo,'" + lAssetAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                            //FOR INGREDIENT COST OF SALES
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', @lMemo, @lMemo,'" + lCOSAccountID + "'," + (lTotalCost * -1).ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //FOR INVENTORY
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, CreditAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "', @lMemo, @lMemo,'" + lAssetAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                            //FOR COST OF SALES
                            sql = @"INSERT INTO Journal(TransactionDate, Memo, AllocationMemo,AccountID, DebitAmount, TransactionNumber, Type, JobID, EntityID) 
                                        VALUES ('" + lTranDate + "',  @lMemo, @lMemo,'" + lCOSAccountID + "'," + lTotalCost.ToString() + ",'" + lSalesNum + "', 'SI',0," + pID + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();

                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("There was an error creating the transaction. No Sales Lines found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        void UpdateTransSeries(ref SqlCommand pCmd)
        {
            string sql = "";
            if (salestype == "ORDER")
            {
                sql = "UPDATE TransactionSeries SET SalesOrderSeries = '" + CurSeries + "'";
            }
            else if (salestype == "QUOTE")
            {
                sql = "UPDATE TransactionSeries SET SalesQuoteSeries = '" + CurSeries + "'";
            }
            else
            {
                sql = "UPDATE TransactionSeries SET SalesInvoiceSeries = '" + CurSeries + "'";
            }

            int res2 = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY);
        }
        private void UpdateNotify()
        {
            string lTranType;
            switch (salestype)
            {
                case "QUOTE":
                    lTranType = "SQ";
                    break;
                case "ORDER":
                    lTranType = "SO";
                    break;
                case "INVOICE":
                    lTranType = "SI";
                    break;
                default:
                    lTranType = "";
                    break;
            }
            string frequency = "";
            DateTime sdate = DateTime.Now;
            DateTime edate = DateTime.MaxValue;
            Dictionary<string, object> param = new Dictionary<string, object>();
            DataTable dtRecur = new DataTable();
            string selectSql_ua = "SELECT * FROM Recurring WHERE EntityID = " + XsalesID + " AND TranType = '" + lTranType + "'";
            CommonClass.runSql(ref dtRecur, selectSql_ua);
            if (dtRecur.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecur.Rows.Count; i++)
                {
                    frequency = (dtRecur.Rows[i]["Frequency"].ToString());
                }
                switch (frequency)
                {
                    case "Daily":
                        sdate = sdate.AddDays(1);
                        sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                        break;
                    case "Weekly":
                        sdate = sdate.AddDays(7);
                        sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                        break;
                    case "Monthly":
                        sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                        break;
                    case "Quarterly":
                        sdate = sdate.AddMonths(3);
                        sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                        break;
                    case "Every 6 Months":
                        sdate = sdate.AddMonths(6);
                        sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
                        break;
                }

                string sql = @"UPDATE Recurring SET NotifyDate = @newNotifyDate WHERE EntityID = " + XsalesID + " AND TranType = '" + lTranType + "'";
                param.Add("@newNotifyDate", sdate);
                CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
            }
        }
        private int RecordPayment(string pSalesNo, decimal pAmountApplied, decimal pDiscount)
        {
            int lPaymentID = 0;
            PaymentCommon.GeneratePaymentNumber(ref CurSeries, ref gPaymentNo, "SP");

            if (gPaymentNo != "")
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                lPaymentID = CreatePaymentRecord(gPaymentNo);
                PaymentCommon.UpdatePaymentNumber(ref CurSeries, "SP");

                if (PaymentCommon.CreateJournalEntriesSP(lPaymentID, "Payment;" + customerName))
                {
                    //TransactionClass.UpdateProfileBalances(CustomerID, PaidToday_txt.Value * -1);

                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Payment Transaction No. " + gPaymentNo, lPaymentID.ToString());
                }
            }
            return lPaymentID;



        }
        public void saveValidationLog(int salesID)
        {
            foreach (DataRow drv in dtv.Rows)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, drv["AuditAction"].ToString(), salesID.ToString());
            }
        }
        private int CreatePaymentRecord(string pPaymentNo, string pSource = "")
        {
            SqlConnection con = null;
            try
            {
                DateTime time = DateTime.Now;
                DateTime dtpfromutc = salesDate.Value.ToUniversalTime();
                DateTime timeutc = time.ToUniversalTime();

                string trandate = dtpfromutc.ToString("yyyy-MM-dd") + " " + timeutc.ToString("HH:mm:ss");

                con = new SqlConnection(CommonClass.ConStr);

                string strpaymentsql = @"INSERT INTO Payment (
                                            ProfileID,
                                            TotalAmount,
                                            Memo,
                                            PaymentFor,
                                            AccountID,
                                            UserID,
                                            PaymentMethodID,
                                            TransactionDate,
                                            PaymentNumber,
                                            SessionID,
                                            Source
                                            )
                                        VALUES ( 
                                            @ProfileID, 
                                            @TotalAmount, 
                                            @Memo, 
                                            'Sales',
                                            @AccountID,
                                            @UserID,
                                            @PaymentMethodID,
                                            @TransactionDate,
                                            @PaymentNumber, 
                                            @SessionID,
                                            @Source
                                        ); SELECT SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(strpaymentsql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ProfileID", CustomerID);
                cmd.Parameters.AddWithValue("@PaymentNumber", pPaymentNo);
                cmd.Parameters.AddWithValue("@TotalAmount", PaidToday_txt.Value);
                cmd.Parameters.AddWithValue("@Memo", "Payment " + customerName);
                cmd.Parameters.AddWithValue("@AccountID", PaymentInfoTb.Rows[0]["RecipientAccountID"].ToString());
                cmd.Parameters.AddWithValue("@UserID", CommonClass.UserID);
                cmd.Parameters.AddWithValue("@TransactionDate", trandate);
                cmd.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[0]["PaymentMethodID"]);
                cmd.Parameters.AddWithValue("@SessionID", CommonClass.SessionID);
                cmd.Parameters.AddWithValue("@Source", pSource);


                con.Open();
                int paymentid = Convert.ToInt32(cmd.ExecuteScalar());

                if (paymentid != 0)
                {//FullPayment
                    Dictionary<string, object> paramPayLines = new Dictionary<string, object>();
                    string Amount = PaidToday_txt.Value.ToString();
                    decimal deciamt = decimal.Parse(Amount, NumberStyles.Currency);
                    string strpaymentlnesql = @"INSERT INTO PaymentLines (
                                                          PaymentID,
                                                          EntityID,
                                                          Amount,
                                                          EntryDate )
                                                      VALUES (
                                                          @PaymentID,
                                                          @EntityID,
                                                          @Amount,
                                                          @EntryDate )";
                    paramPayLines.Add("@PaymentID", paymentid);
                    paramPayLines.Add("@EntityID", NewSalesID);
                    paramPayLines.Add("@Amount", deciamt);
                    paramPayLines.Add("@EntryDate", trandate);
                    CommonClass.runSql(strpaymentlnesql, CommonClass.RunSqlInsertMode.QUERY, paramPayLines);
                    decimal change = AmountChange.Value;


                    for (int i = 0; i < PaymentInfoTb.Rows.Count; i++)
                    {
                        //Tender details
                        string sqlTender = @"INSERT INTO PaymentTender (
                                                          PaymentID,
                                                          Amount,
                                                          PaymentMethodID)
                                                      VALUES (
                                                          @PaymentID,
                                                          @Amount,
                                                          @PaymentMethodID ) ; SELECT SCOPE_IDENTITY()";
                        SqlCommand cmdTender = new SqlCommand(sqlTender, con);
                        cmdTender.Parameters.AddWithValue("@PaymentID", paymentid);
                        cmdTender.Parameters.AddWithValue("@Amount", PaymentInfoTb.Rows[i]["AmountPaid"].ToString());
                        cmdTender.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"]);
                        //cmdTender.ExecuteNonQuery();
                        int ptid = Convert.ToInt32(cmdTender.ExecuteScalar());
                        //Update GC
                        string lMethodId = PaymentInfoTb.Rows[i]["PaymentMethodID"].ToString().Trim();
                        //MessageBox.Show("PaymentMethodID:" + lMethodId);
                        if (lMethodId == "16") ;
                        {
                            //MessageBox.Show("GC:" + PaymentInfoTb.Rows[i]["PaymentMethodID"].ToString());
                            string gcsql = @"Update GiftCertificate set IsUsed = '1',usedSalesID = '" + NewSalesID + "' where GCNumber = '" + PaymentInfoTb.Rows[i]["PaymentGCNo"].ToString() + "'";
                            CommonClass.runSql(gcsql);
                        }
                        //Details
                        string sqlDetails = @"SET IDENTITY_INSERT PaymentDetails ON;
                                            INSERT INTO PaymentDetails (
                                                            PaymentDetailsID,
                                                            PaymentID,
                                                            PaymentMethodID,
                                                            PaymentAuthorisationNumber, 
                                                            PaymentCardNumber, 
                                                            PaymentNameOnCard,
                                                            PaymentExpirationDate, 
                                                            CardNotes, 
                                                            PaymentBSB, 
                                                            PaymentBankAccountNumber, 
                                                            PaymentBankAccountName,
                                                            PaymentChequeNumber, 
                                                            BankNotes, 
                                                            PaymentNotes,
                                                            PaymentGCNo,
                                                            PaymentGCNotes)
                                                      VALUES (
                                                        @PaymentDetailsID,
                                                        @PaymentID,
                                                        @PaymentMethodID,
                                                        @PaymentAuthorisationNumber, 
                                                        @PaymentCardNumber, 
                                                        @PaymentNameOnCard,
                                                        @PaymentExpirationDate, 
                                                        @PaymentCardNotes, 
                                                        @PaymentBSB, 
                                                        @PaymentBankAccountNumber, 
                                                        @PaymentBankAccountName,
                                                        @PaymentChequeNumber, 
                                                        @PaymentBankNotes, 
                                                        @PaymentNotes,
                                                        @PaymentGCNo,
                                                        @PaymentGCNotes)";
                        SqlCommand cmdDetails = new SqlCommand(sqlDetails, con);
                        cmdDetails.Parameters.AddWithValue("@PaymentDetailsID", ptid);
                        cmdDetails.Parameters.AddWithValue("@PaymentID", paymentid);
                        cmdDetails.Parameters.AddWithValue("@PaymentMethodID", PaymentInfoTb.Rows[i]["PaymentMethodID"]);
                        cmdDetails.Parameters.AddWithValue("@PaymentAuthorisationNumber", PaymentInfoTb.Rows[i]["PaymentAuthorisationNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentCardNumber", PaymentInfoTb.Rows[i]["PaymentCardNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentNameOnCard", PaymentInfoTb.Rows[i]["PaymentNameOnCard"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentExpirationDate", PaymentInfoTb.Rows[i]["PaymentExpirationDate"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentCardNotes", PaymentInfoTb.Rows[i]["PaymentCardNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBSB", PaymentInfoTb.Rows[i]["PaymentBSB"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankAccountNumber", PaymentInfoTb.Rows[i]["PaymentBankAccountNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankAccountName", PaymentInfoTb.Rows[i]["PaymentBankAccountName"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentChequeNumber", PaymentInfoTb.Rows[i]["PaymentChequeNumber"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentBankNotes", PaymentInfoTb.Rows[i]["PaymentBankNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentNotes", PaymentInfoTb.Rows[i]["PaymentNotes"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentGCNo", PaymentInfoTb.Rows[i]["PaymentGCNo"].ToString());
                        cmdDetails.Parameters.AddWithValue("@PaymentGCNotes", PaymentInfoTb.Rows[i]["PaymentGCNotes"].ToString());
                        cmdDetails.ExecuteNonQuery();
                    }
                    PaymentCommon.UpdateSalesRecord(trandate, invoicenum, deciamt);
                }
                return paymentid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            KeyboardOnScreen kbos = new KeyboardOnScreen("Enter Note", "Enter Note:");
            if (kbos.ShowDialog() == DialogResult.OK)
            {
                Note = kbos.GetValue.ToString();
            }
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn1.Text;

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn3.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn6.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn9.Text;
        }

        private void btnEndEdit_Click(object sender, EventArgs e)
        {
            editTblNum = false;
        }

        private void BackSpace_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                if (tblNum.Text.Length > 0 || tblNum.Text != "")
                {
                    tblNum.Text = tblNum.Text.Remove(tblNum.Text.Length - 1, 1);
                }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btnDot.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (editTblNum)
                tblNum.Text += btn0.Text;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            editTblNum = true;
            tblNum.Focus();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            tblNum.Clear();
            dgEnterSales.Rows.Clear();
            PopulateDataGridView();
            invoicenum = "";
            TotalAmount.Value = 0;
            BalanceDue_txt.Value = 0;
            PaidToday_txt.Value = 0;
            AmountChange.Value = 0;
            rowin = 0;
        }

        private void btnSalesPerson_Click(object sender, EventArgs e)
        {
            if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
            {
                VoidValidation DlgVoid = new VoidValidation("Sales Person Override");
                if (DlgVoid.ShowDialog() == DialogResult.OK)
                {
                    SalespersonLookup SalespersonDlg = new SalespersonLookup();
                    if (SalespersonDlg.ShowDialog() == DialogResult.OK)
                    {
                        string[] lSales = SalespersonDlg.GetSalesperson;
                        SalesPersonID = lSales[0];
                        btnSalesPerson.Text = "SALES PERSON: " + lSales[1];
                    }
                }
            }
            else
            {
                SalespersonLookup SalespersonDlg = new SalespersonLookup();
                if (SalespersonDlg.ShowDialog() == DialogResult.OK)
                {
                    string[] lSales = SalespersonDlg.GetSalesperson;
                    SalesPersonID = lSales[0];
                    btnSalesPerson.Text = "SALES PERSON: "+ lSales[1];
                }
            }
        }

        private void dgEnterSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            rowin = e.RowIndex;
        }
        void LoadMember(int ProfileID)
        {
            string membersql = @"Select p.*, isnull(s.ShippingID,0) as ShippingID FROM Profile p INNER JOIN Contacts c ON p.ID = c.ProfileID LEFT JOIN ShippingMethods s on p.ShippingMethodID = s.ShippingMethod WHERE ID = '" + ProfileID + "'";

            DataTable memberdt = new DataTable();
            CommonClass.runSql(ref memberdt, membersql);
            if (memberdt.Rows.Count > 0)
            {
                DataRow c = memberdt.Rows[0];
                CustomerID = c["ID"].ToString();
                customerName = c["Name"].ToString();
               // ShippingmethodText.Text = c["ShippingMethodID"].ToString();
               // TermRefID = c["TermsOfPayment"].ToString();
                ProfileTax = c["TaxCode"].ToString();
                //  MethodPaymentID = c["MethodOfPaymentID"].ToString();
                //  CustomerBalance = (c["CurrentBalance"].ToString() == "" ? 0 : float.Parse(c["CurrentBalance"].ToString(), NumberStyles.Currency));
                //  CustomerCreditLimit = (c["CreditLimit"].ToString() == "" ? 0 : float.Parse(c["CreditLimit"].ToString(), NumberStyles.Currency));
                //TermsText.Visible = true;
                //  MemoText.Text = "Sale; " + c["Name"].ToString();
                // ShippingMethodID = (c["ShippingID"].ToString() == "" ? "0" : c["ShippingID"].ToString());
                /// InvoiceNumTxt.Visible = true;
                //cmb_shippingcontact.SelectedIndex = Convert.ToInt16(c["LocationID"].ToString()) - 1;

                //   LoadDefaultTerms(CustomerID);
                //  LoadFreightTax(ProfileTax);
                //  LoadPaymentMethod();

                //    FormCheck();
                // ApplyVoidAccess();
                LoadPoints();
            }

        }
        private void btnLoyalty_Click(object sender, EventArgs e)
        {
            memberID = CustomerID;
            member = Interaction.InputBox("Please Enter Member Loyalty Number", "Member Loyalty", "");
            string loyaltysql = "SELECT * FROM LoyaltyMember where Number = '" + member + "'";
            DataTable loyaldt = new DataTable();
            CommonClass.runSql(ref loyaldt, loyaltysql);
            if (loyaldt.Rows.Count > 0)
            {
                ismember = true;
                memberID = loyaldt.Rows[0]["ID"].ToString();
                LoadMember(int.Parse(loyaldt.Rows[0]["ProfileID"].ToString()));
                //this.lblLoyalty.Text = member;
                btnRedeem.Visible = true;
                txtCustPoints.Visible = true;
            }
            else
            {
                MessageBox.Show("Member Number does not exists.");
               //this.lblLoyalty.Text = "";
               btnRedeem.Visible = false;
                txtCustPoints.Visible = false;
            }
        }
        private void IsIngredientBuild(int itemID, int rowindex)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ia.PartItemID,
                                    ia.PartItemQty, 
                                    i.PartNumber,
                                    i.ItemName,
                                    i.SalesTaxCode,
                                    t.TaxCollectedAccountID, 
                                    t.TaxPercentageRate AS RateTaxSales,
                                    ict.AverageCostEx
                            FROM ItemsAutoBuild ia 
                            INNER JOIN Items i ON i.ID = ia.PartItemID 
                            LEFT JOIN TaxCodes t ON i.SalesTaxCode = t.taxcode
                            LEFT JOIN ItemsCostPrice ict ON ict.ItemID = i.ID
                            WHERE ia.ItemID = " + itemID;
            CommonClass.runSql(ref dt, sql);
            float lCost = 0;
            float lTCost = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dgRow = dt.Rows[i];
                    DataRow rw = IngredientTb.NewRow();
                    rw["ItemID"] = dgRow["PartItemID"].ToString();
                    rw["Ship"] = float.Parse(dgRow["PartItemQty"].ToString()) * float.Parse(dgEnterSales.Rows[rowindex].Cells["Ship"].Value.ToString());
                    //MessageBox.Show("TOTAL ING QTY:" + rw["Ship"].ToString());
                    rw["PartNumber"] = dgRow["PartNumber"].ToString();
                    rw["Description"] = dgRow["ItemName"].ToString();
                    rw["Amount"] = 0;
                    string taxcode = dgRow["SalesTaxCode"].ToString();
                    if (taxcode != "")
                    {
                        rw["TaxRate"] = dgRow["RateTaxSales"];
                        rw["TaxCode"] = dgRow["SalesTaxCode"];
                        rw["TaxCollectedAccountID"] = dgRow["TaxCollectedAccountID"];
                    }
                    else
                    {
                        rw["TaxRate"] = 0;
                        rw["TaxCode"] = "N-T";
                        rw["TaxCollectedAccountID"] = 0;
                    }
                    rw["Cost"] = dgRow["AverageCostEx"].ToString();
                   
                    //MessageBox.Show(dgRow["AverageCostEx"].ToString());
                    rw["TotalCost"] = float.Parse(dgRow["PartItemQty"].ToString()) * float.Parse(dgEnterSales.Rows[rowindex].Cells["Ship"].Value.ToString()) * float.Parse(dgRow["AverageCostEx"].ToString());
                    IngredientTb.Rows.Add(rw);
                    lCost += float.Parse(rw["Cost"].ToString());
                    lTCost += float.Parse(rw["TotalCost"].ToString());

                }
            }
            dgEnterSales.Rows[rowindex].Cells["CostPrice"].Value = lTCost;
           
        }
        public void UpdateItemQty()
        {
            if (IngredientTb.Rows.Count > 0)
            {
                foreach (DataRow dr in IngredientTb.Rows)
                {
                    if (dr["ItemId"] != null)
                    {
                        if (dr["ItemId"].ToString() != "")
                        {

                            Dictionary<string, object> paramItemTransaction = new Dictionary<string, object>();

                            // INSERT ADJUSTED ITEMS IN ITEM TRANSACTION

                            string sqlItemTransaction = @"INSERT INTO ItemTransaction(TransactionDate,ItemId,TransactionQty,QtyAdjustment,CostEx,TotalCostEx,TranType,SourceTranID,UserID) 
                                            VALUES(@TransDate , @lItemID ,@lQty, @lQtyAdjustment , @CostPrice ,@TotalCost ,'SII'," + NewSalesID + "," + CommonClass.UserID + ")";
                            paramItemTransaction.Add("@TransDate", this.salesDate.Value.ToUniversalTime());
                            paramItemTransaction.Add("@lItemID", dr["ItemID"].ToString());
                            paramItemTransaction.Add("@lQty", dr["Ship"].ToString());
                            if (dr["Ship"].ToString() != "")
                            {
                                paramItemTransaction.Add("@lQtyAdjustment", float.Parse(dr["Ship"].ToString()) * (-1));
                            }
                            else
                            {
                                paramItemTransaction.Add("@lQtyAdjustment", 0);
                            }
                            paramItemTransaction.Add("@TotalCost", dr["TotalCost"].ToString());
                            paramItemTransaction.Add("@CostPrice", dr["Cost"].ToString());

                            // UPDATE ITEM QTY ON HAND
                            CommonClass.runSql(sqlItemTransaction, CommonClass.RunSqlInsertMode.QUERY, paramItemTransaction);
                            string sql = @"UPDATE ItemsQty SET OnHandQty = ((SELECT OnHandQty FROM ItemsQty WHERE ItemID=@ItemID) - " + dr["Ship"].ToString() + ")"
                                         + " WHERE ItemID=@ItemID";
                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param.Add("@ItemID", dr["ItemID"].ToString());
                            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                        }
                    }
                }
            }
        }

        private void btnRedeem_Click(object sender, EventArgs e)
        {
            if (float.Parse(txtCustPoints.Text) <= 0)
            {
                MessageBox.Show("No customer points available");
                return;
            }

            if (CommonClass.PointRedemption == null
            || CommonClass.PointRedemption.IsDisposed)
            {
                CommonClass.PointRedemption = new PointsRedemption(CommonClass.RedemptionType.GIFTCERTIFICATE, this, null, null,CustomerID, customerName);
            }
            this.Cursor = Cursors.WaitCursor;
            CommonClass.PointRedemption.MdiParent = this.MdiParent;
            CommonClass.PointRedemption.Show();
            CommonClass.PointRedemption.Focus();
            if (CommonClass.PointRedemption.DialogResult == DialogResult.Cancel)
            {
                CommonClass.PointRedemption.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int lRowIndex = dgEnterSales.CurrentCell.RowIndex;
            rowin = lRowIndex;
            if (dgEnterSales.Rows.Count > 0)
            {

                if (dgEnterSales.Rows[rowin].Cells["PartNumber"].Value != null)
                {
                    if ((CommonClass.isSalesperson || CommonClass.isTechnician) && (!CommonClass.isSupervisor || !CommonClass.isAdministrator))
                    {
                        VoidValidation DlgVoid = new VoidValidation("Price Override");
                        if (DlgVoid.ShowDialog() == DialogResult.OK)
                        {
                            password = DlgVoid.GetPassword;
                            username = DlgVoid.GetUsername;
                            KeyboardOnScreen kbos = new KeyboardOnScreen("Price Value Override", "Enter Price Value : ");
                            if (kbos.ShowDialog() == DialogResult.OK)
                            {
                                string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                                string strValue = string.Concat(kbos.GetValue.ToString().Where(char.IsLetter));
                                if (strValue == "-")
                                {
                                    pValue = strValue + pValue;
                                }
                                dgEnterSales.Rows[rowin].Cells["Price"].Value = pValue;
                            }
                            DataRow rw = dtv.NewRow();
                            rw["UserName"] = DlgVoid.GetUsername;
                            rw["AuditAction"] = "Price override to " + dgEnterSales.Rows[rowin].Cells["Price"].Value + " by " + DlgVoid.GetUsername;
                            dtv.Rows.Add(rw);

                        }
                    }
                    else
                    {
                        KeyboardOnScreen kbos = new KeyboardOnScreen("Price Value Override", "Enter Price Value : ");
                        if (kbos.ShowDialog() == DialogResult.OK)
                        {
                            string pValue = string.Concat(kbos.GetValue.ToString().Where(char.IsDigit));
                            if (kbos.GetValue == "-" + pValue)
                            {
                                pValue = "-" + pValue;
                            }
                            dgEnterSales.Rows[rowin].Cells["Price"].Value = pValue;
                        }
                        DataRow rw = dtv.NewRow();
                        rw["AuditAction"] = "Price override to " + dgEnterSales.Rows[rowin].Cells["Price"].Value + " by " + CommonClass.UserName;
                        dtv.Rows.Add(rw);
                    }
                    itemcalc(rowin);
                    Recalcline(5, rowin);
                    Recalc();
                    CalcOutOfBalance();

                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }
        }

        private void btn_nextcat_Click(object sender, EventArgs e)
        {
            btn_backcat.Enabled = true;
            catstartindex = catstartindex + 12;
            catendindex = catendindex + 12;
            LoadSubCat();
            if (catendindex > dgSubCat.Rows.Count)
            {
                btn_nextcat.Enabled = false;
            }
        }

        private void btn_backcat_Click(object sender, EventArgs e)
        {
            if (catstartindex == 0)
            {
                btn_backcat.Enabled = false;
                btn_nextcat.Enabled = true;
                return;
            }
            catstartindex = catstartindex - 12;
            catendindex = catendindex - 12;
            LoadSubCat();
            if (catstartindex == 0)
            {
                btn_backcat.Enabled = false;
                btn_nextcat.Enabled = true;
            }
        }
    }
}
