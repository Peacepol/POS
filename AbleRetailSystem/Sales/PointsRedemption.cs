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

namespace RestaurantPOS
{
    public partial class PointsRedemption : Form
    {
        private CommonClass.RedemptionType mRedeemType;
        private string mCustomerID = "";
        private string mcustomerName = "";
        private string PriceLevel = "Level0";
        private DataGridViewRow mDgvRow = null;
        private Sales.EnterSales mSaleRef = null;
        private Sales.QuickSales mQSaleRef = null;
        private float pointexchangeRate;
        private float amountValue;

        public PointsRedemption(CommonClass.RedemptionType pRtype, Sales.EnterSales saleref, Sales.QuickSales qsaleref, string pCustID = "", string pCustName = "")
        {
            mSaleRef = saleref;
            mQSaleRef = qsaleref;
            mRedeemType = pRtype;
            mCustomerID = pCustID;
            mcustomerName = pCustName;
            InitializeComponent();
        }

        private void PointsRedemption_Load(object sender, EventArgs e)
        {
            cmbRedeemType.SelectedIndex = (int) mRedeemType;
            txtTotalPointsAvailable.Text = "0";

            if (mCustomerID != "")
            {
                PriceLevel = GetCustomerPriceLevel(mCustomerID);
                LoadPoints();
                LoadPointsExchangeRate();
                ShowRedemptionItems();
                customerText.Text = mcustomerName;
            }
        }

        private void LoadPoints()
        {
            string pointssql = @"SELECT SUM(PointsAccumulated) AS TotalPoints
                                 FROM AccumulatedPoints 
                                 WHERE CustomerID = @CustomerID";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CustomerID", mCustomerID);
            CommonClass.runSql(ref dt, pointssql, param);
            if (dt.Rows.Count == 1 && dt.Rows[0]["TotalPoints"].ToString() != "")
            {
                txtTotalPointsAvailable.Text = dt.Rows[0]["TotalPoints"].ToString();           
            }
        }

        private void LoadPointsExchangeRate()
        {
            string pointssql = @"SELECT PointsValue,AmountValue , CustomerID 
                                 FROM PointsExchangeRate 
                                 WHERE CustomerID IN (@CustomerID,-1)";
            DataTable dt = new DataTable();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CustomerID", mCustomerID);
            CommonClass.runSql(ref dt, pointssql, param);
            if (dt.Rows.Count >0)
            {
               foreach(DataRow x in dt.Rows)
                {
                    if(x["CustomerID"].ToString() == mCustomerID)
                    {
                        pointexchangeRate = float.Parse(x["PointsValue"].ToString());
                        amountValue = float.Parse(x["AmountValue"].ToString());
                        return;
                    }
                    else
                    {
                        pointexchangeRate = float.Parse(x["PointsValue"].ToString());
                        amountValue = float.Parse(x["AmountValue"].ToString());
                    }
                   
                }
                   
            }
               
            
        }
        private void ShowRedemptionItems()
        {
            string sql = "";
            switch (mRedeemType)
            {
                case CommonClass.RedemptionType.GIFTCERTIFICATE:
                    sql = @"SELECT i.ID,
                                i.ItemName, 
                                i.ItemDescription, 
                                pi.requiredpoints,
                                pi.promotype ,
                                gc.ID as redeemid
                            FROM PromotionItems pi
                            INNER JOIN Items i ON (pi.itemid = i.ID) inner join GiftCertificate gc on gc.promoid = pi.id 
                            WHERE pi.promotype='Gift Certificate' 
                            AND pi.requiredpoints <= @TotalPoints";
                    break;
                case CommonClass.RedemptionType.ITEM:
                    sql = @"SELECT i.ID,
                                i.ItemName, 
                                i.ItemDescription, 
                                pi.requiredpoints,
                                pi.promotype
                            FROM PromotionItems pi
                            INNER JOIN Items i ON (pi.itemid = i.ID)
                            WHERE pi.promotype='Item' 
                            AND pi.requiredpoints <= @TotalPoints";
                    break;
                case CommonClass.RedemptionType.PRICEDISCOUNT:

                     txtUsePts.Text = txtTotalPointsAvailable.Text;
                    txtEquivAmt.Text = ((float.Parse(txtUsePts.Text) / pointexchangeRate) * amountValue).ToString();
                    double d = double.Parse(txtEquivAmt.Text, NumberStyles.Currency);
                    txtEquivAmt.Text = d.ToString("C2");
                    break;
            }

            if (mRedeemType != CommonClass.RedemptionType.PRICEDISCOUNT)
            {
                DataTable dt = new DataTable();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@TotalPoints", txtTotalPointsAvailable.Text);
                CommonClass.runSql(ref dt, sql, param);
                foreach (DataRow drow in dt.Rows)
                {
                    drow["ItemDescription"] = CommonClass.htmlToText(drow["ItemDescription"].ToString());
                }

                dgvItems.DataSource = dt;
                dgvItems.Columns[0].Visible = false;
                dgvItems.Columns[1].HeaderText = "Name";
                dgvItems.Columns[1].Width = 170;
                dgvItems.Columns[2].HeaderText = "Description";
                dgvItems.Columns[2].Width = 280;
                dgvItems.Columns[3].HeaderText = "Required Points";
                dgvItems.Columns[3].Width = 90;
                dgvItems.Columns[4].HeaderText = "Type";
                dgvItems.Columns[5].Visible = false;
                dgvItems.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbAccount_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                mCustomerID = lProfile[0];
                this.customerText.Text = lProfile[2];
                if (mCustomerID != "")
                {
                    PriceLevel = GetCustomerPriceLevel(mCustomerID);
                }

                LoadPoints();
                LoadPointsExchangeRate();
                ShowRedemptionItems();
            }
        }

        public static string GetCustomerPriceLevel(string pCustomerID)
        {
            SqlConnection con = null;
            string retstr = "Level0";
            try
            {
                string sql = @"SELECT ItemPriceLevel FROM Profile WHERE ID = " + pCustomerID;

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable ltb;
                ltb = new DataTable();
                da.Fill(ltb);
                if (ltb.Rows.Count > 0)
                {
                    retstr = "Level" + ltb.Rows[0]["ItemPriceLevel"].ToString();
                }
                return retstr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return retstr;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        } //END

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            mDgvRow = dgvItems.SelectedRows[0];
        }

        private void btnRedeem_Click(object sender, EventArgs e)
        {
            if (cmbRedeemType.Text != "Price Discount")
            {
                DataGridViewRow mDgvRow = dgvItems.Rows.Count > 0 ? dgvItems.SelectedRows[0] : null;
                if (mDgvRow != null)
                {
                    if (mSaleRef != null || mQSaleRef != null)
                    {
                        string itemdetailssql = @"SELECT DISTINCT i.ID, 
                                                         i.PartNumber, 
                                                         i.ItemNumber, 
                                                         i.ItemName, 
                                                         q.OnHandQty, 
                                                         c.LastCostEx,
                                                         c.StandardCostEx, 
                                                         c.AverageCostEx, 
                                                         s." + PriceLevel + @" AS SellingPrice, 
                                                         i.SalesTaxCode,
                                                         t.TaxCollectedAccountID, 
                                                         t.TaxPercentageRate AS RateTaxSales,
                                                         i.PurchaseTaxCode,
                                                         i.IsCounted, 
                                                         i.AssetAccountID, 
                                                         i.IsBought, 
                                                         i.COSAccountID, 
                                                         i.CategoryID 
                                                FROM 
                                                ((((( Items i INNER JOIN ItemsSellingPrice s ON i.ID = s.ItemID )
                                                INNER JOIN ItemsCostPrice c ON i.ID = c.ItemID ) 
                                                INNER JOIN ItemsQty q ON i.ID = q.ItemID )
                                                LEFT JOIN Profile p ON i.SupplierID = p.ID )
                                                LEFT JOIN TaxCodes t ON i.SalesTaxCode = t.taxcode )
                                                WHERE i.IsSold = 1
                                                AND i.ID = " + mDgvRow.Cells["ID"].Value;

                        DataTable dtitemdetail = new DataTable();
                        CommonClass.runSql(ref dtitemdetail, itemdetailssql);
                        DataGridView lSalesLineGrid = new DataGridView();
                        if (mSaleRef != null)
                        {
                             lSalesLineGrid = mSaleRef.GetSalesLinesGridView;
                        }
                        if (mQSaleRef != null)
                        {
                            lSalesLineGrid = mQSaleRef.GetSalesLinesGridView;
                        }

                        foreach (DataGridViewRow ldgvRow in lSalesLineGrid.Rows)
                        {
                            if (ldgvRow.Cells["ItemID"].Value == null)
                            {
                                ldgvRow.Cells["ItemID"].Value = mDgvRow.Cells["ID"].Value;
                                ldgvRow.Cells["PartNumber"].Value = dtitemdetail.Rows[0]["PartNumber"];
                                ldgvRow.Cells["Description"].Value = "Redeemed Item - " + mDgvRow.Cells["ItemDescription"].Value.ToString();
                                ldgvRow.Cells["JobID"].Value = 0;
                                ldgvRow.Cells["BackOrder"].Value = 0;
                                ldgvRow.Cells["Ship"].Value = 1;
                                string taxcode = dtitemdetail.Rows[0]["SalesTaxCode"].ToString();
                                if (taxcode != "")
                                {
                                    ldgvRow.Cells["TaxRate"].Value = dtitemdetail.Rows[0]["RateTaxSales"];
                                    ldgvRow.Cells["TaxCode"].Value = dtitemdetail.Rows[0]["SalesTaxCode"];
                                    ldgvRow.Cells["TaxCollectedAccountID"].Value = dtitemdetail.Rows[0]["TaxCollectedAccountID"];
                                }
                                else
                                {
                                    ldgvRow.Cells["TaxRate"].Value = 0;
                                    ldgvRow.Cells["TaxCode"].Value = "N-T";
                                    ldgvRow.Cells["TaxCollectedAccountID"].Value = 0;
                                }

                                ldgvRow.Cells["Price"].Value = dtitemdetail.Rows[0]["SellingPrice"];
                                ldgvRow.Cells["ActualUnitPrice"].Value = dtitemdetail.Rows[0]["SellingPrice"];
                                ldgvRow.Cells["Amount"].Value = 0;
                                ldgvRow.Cells["PromoID"].Value = -1;//redeemid/
                                ldgvRow.Cells["RedeemID"].Value = mDgvRow.Cells["redeemid"].Value;
                                ldgvRow.Cells["Points"].Value = Convert.ToDouble(mDgvRow.Cells["requiredpoints"].Value) * -1;
                                ldgvRow.Cells["RedemptionType"].Value = cmbRedeemType.Text;
                                if (mSaleRef != null)
                                {
                                    mSaleRef.Recalcline(8, ldgvRow.Index);
                                    mSaleRef.CalcOutOfBalance();
                                }
                                else
                                {
                                    mQSaleRef.Recalcline(8, ldgvRow.Index);
                                    mQSaleRef.CalcOutOfBalance();
                                }

                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                if (mSaleRef != null)
                {
                    DataGridView lSalesLineGrid = mSaleRef.GetSalesLinesGridView;
                    foreach (DataGridViewRow ldgvRow in lSalesLineGrid.Rows)
                    {
                        if (ldgvRow.Cells["ItemID"].Value == null)
                        {
                            ldgvRow.Cells["ItemID"].Value = 2;
                            ldgvRow.Cells["PartNumber"].Value = "PDCODE";
                            ldgvRow.Cells["Description"].Value = "Redeemed Item - Price discount for points";
                            ldgvRow.Cells["JobID"].Value = 0;
                            ldgvRow.Cells["BackOrder"].Value = 0;
                            ldgvRow.Cells["Ship"].Value = -1;
                            ldgvRow.Cells["TaxRate"].Value = 0;
                            ldgvRow.Cells["TaxCode"].Value = "N-T";
                            ldgvRow.Cells["TaxCollectedAccountID"].Value = "";
                            double discountprice = double.Parse(txtEquivAmt.Text, NumberStyles.Currency);
                            ldgvRow.Cells["Price"].Value = discountprice;
                            ldgvRow.Cells["ActualUnitPrice"].Value = discountprice;
                            ldgvRow.Cells["Amount"].Value = discountprice * -1;
                            ldgvRow.Cells["PromoID"].Value = -1; 
                            ldgvRow.Cells["Points"].Value = Convert.ToDouble(txtUsePts.Text) * -1;
                            ldgvRow.Cells["RedemptionType"].Value = cmbRedeemType.Text;
                            if (mSaleRef != null)
                            {
                                mSaleRef.Recalcline(8, ldgvRow.Index);
                                mSaleRef.CalcOutOfBalance();
                            }
                            else
                            {
                                mQSaleRef.Recalcline(8, ldgvRow.Index);
                                mQSaleRef.CalcOutOfBalance();
                            }
                            break;
                        }
                    }
                }
            }
            Close();
        }

        private void cmbRedeemType_TextChanged(object sender, EventArgs e)
        {
            mRedeemType = (CommonClass.RedemptionType)cmbRedeemType.SelectedIndex;
            if (mCustomerID != "")
            {
                LoadPoints();
                LoadPointsExchangeRate();
                ShowRedemptionItems();
            }
        }

        private void cmbRedeemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRedeemType.SelectedIndex == 2)
            {
                label3.Visible = true;
                txtEquivAmt.Visible = true;
                label5.Visible = true;
                txtUsePts.Visible = true;
                btnUsePts.Visible = true;

                dgvItems.DataSource = null;
                dgvItems.Rows.Clear();
                dgvItems.Columns.Clear();
            }
            else
            {
                label3.Visible = false;
                txtEquivAmt.Visible = false;
                label5.Visible = false;
                txtUsePts.Visible = false;
                btnUsePts.Visible = false;
            }
        }

        private void btnUsePts_Click(object sender, EventArgs e)
        {
            if (mCustomerID != "")
            {
                if (float.Parse(txtUsePts.Text) > float.Parse(txtTotalPointsAvailable.Text))
                {
                    MessageBox.Show("Points cannot be greater than the total points available");
                    return;
                }
                txtEquivAmt.Text = ((float.Parse(txtUsePts.Text) / pointexchangeRate) * amountValue).ToString();
                double d = double.Parse(txtEquivAmt.Text, NumberStyles.Currency);
                txtEquivAmt.Text = d.ToString("C2");
            }
        }
    }
}
