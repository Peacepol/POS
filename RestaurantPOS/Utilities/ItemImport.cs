using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Utilities
{
    public partial class ItemImport : Form
    {
        public string a;
        public string b;
        public string c;
        public string sourcePath;

        private bool CanEdit = false;
        DataTable dt;
        public ItemImport()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
            }
        }

        private void ItemImport_Load(object sender, EventArgs e)
        {

            sourcePath = @Application.StartupPath + "\\importLog.txt";
            if (File.Exists(sourcePath))
            {
                File.Delete(sourcePath);
            }
            File.Create(sourcePath);

            cmbDuplicate.Items.Add("New");
            cmbDuplicate.Items.Add("Update");
            cmbDuplicate.SelectedIndex = 0;
            btnSave.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (cmbImportType.Text != "")
            {
                OpenFileDialog dlg = new OpenFileDialog();

                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    int ImportRecord = 0, inValidItem = 0;
                    string Source = "";
                    string RetailFieldsDisplay = "";
                    string RetailFieldsValue = "";
                    int lReqFieldNo = 0;

                    dgItem.Rows.Clear();
                    if (dlg.FileName != "")
                    {
                        if (dlg.FileName.EndsWith(".csv") || dlg.FileName.EndsWith(".txt"))
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            dt = new DataTable();

                            dt = GetDataTabletFromCSVFile(dlg.FileName);
                            txtFileName.Text = dlg.SafeFileName;
                            Source = dlg.FileName;
                            if (dt.Rows != null && dt.Rows.ToString() != String.Empty)
                            {
                                DataGridViewComboBoxColumn comboBoxColumn1 = (DataGridViewComboBoxColumn)dgItem.Columns["ImportFields"];
                                if (comboBoxColumn1.Items.Count == 0)
                                {
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        comboBoxColumn1.Items.Add(column.ColumnName);

                                    }
                                }
                                else
                                {
                                    comboBoxColumn1.Items.Clear();
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        comboBoxColumn1.Items.Add(column.ColumnName);
                                    }
                                }
                                if (cmbImportType.Text == "Item")
                                {
                                    RetailFieldsValue = @"
                                    PartNumber,
                                    CategoryCode,                                    
                                    ItemNumber,
                                    ItemName,
                                    SupplierItemNumber,
                                    IsBought,
                                    IsSold,
                                    IsCounted, 
                                    COSAccountID,
                                    IncomeAccountID,
                                    AssetAccountID,
                                    BuyingUOM,
                                    QtyPerBuyingUOM,
                                    SupplierID,
                                    PurchaseTaxCode,
                                    SellingUOM,
                                    QtyPerSellingUnit,
                                    SalesTaxCode,
                                    BrandName,
                                    ItemDescriptionSimple,
                                    ItemDescription,
                                    BarcodeData,
                                    StandardCostEx,
                                    LastCostEx,
                                    MinQty,
                                    MaxQty,
                                    ReOrderQty
                                    ,CList1,CList2,CList3,CField1,CField2,CField3,IsInactive";
                                    RetailFieldsDisplay = @"Part Number, Category Code, Item Number,Item Name,Supplier Item Number
                                    ,Is Bought,Is Sold,Is Counted, COS Account ID,Income Account ID,Asset Account ID
                                    ,Buying Unit of Measure,Qty Per Buying UOM,Supplier ID,Purchase Tax Code
                                    ,Selling Unit of Measure,Qty Per Selling Unit,Sales Tax Code,Brand Name,Item Description,Item Long Description
                                    ,Barcode,Standard Cost,Last Cost,Minumum Qty,Maximum Qty,ReOrder Qty
                                    ,Custom List 1,Custom List 2,Custom List 3,Custom Field 1,Custom Field 2,Custom Field 3,Is Inactive";

                                    lReqFieldNo = 18;

                                }
                                else if (cmbImportType.Text == "Price")
                                {
                                    RetailFieldsValue = @"PartNumber,CalcMethod,PriceLevel,PercentChange,PriceAfter";
                                    RetailFieldsDisplay = @"PartNumber,CalcMethod,PriceLevel,*PercentChange,*Selling Price";
                                    lReqFieldNo = 4;
                                }
                                else if (cmbImportType.Text == "Customer" || cmbImportType.Text == "Supplier")
                                {
                                    RetailFieldsValue = @"ProfileIDNumber,Name, TaxCode, FreightTaxCode, ItemPriceLevel, TermsOfPayment, CreditLimit, BalanceDueDays,TaxIDNumber, ABN,ContactPerson,Street,City,State,Postcode,Country,Phone,Email";
                                    RetailFieldsDisplay = @"Profile Number ,Company Name,Tax Code,Freight Tax Code,Item Price Level,Terms Of Payment, Credit Limit, Balance Due Days,Tax ID Number,Business Number,Contact Person, Street,City,State,Postcode,Country,Phone,Email";
                                    lReqFieldNo = 7;
                                }

                                List<string> locations = RetailFieldsValue.Split(',').ToList<string>();
                                List<string> RealFields = RetailFieldsDisplay.Split(',').ToList<string>();
                                int count = 0;
                                for (int x = 0; x < locations.Count; x++)
                                {
                                    dgItem.Rows.Add();
                                    dgItem.Rows[x].Cells["RealFields"].Value = locations[x].ToString().Trim();
                                    if (cmbDuplicate.Text == "New")
                                    {

                                            dgItem.Rows[x].Cells["RetailFields"].Value = (x <= lReqFieldNo ? "*" + RealFields[x].ToString().Trim() : dgItem.Rows[x].Cells["RetailFields"].Value = RealFields[x].ToString().Trim());
                                    }
                                    else//Update Required
                                    {
                                            dgItem.Rows[x].Cells["RetailFields"].Value = (x <= 1? "*" + RealFields[x].ToString().Trim(): dgItem.Rows[x].Cells["RetailFields"].Value = RealFields[x].ToString().Trim()); 
                                    }
                                }
                            }
                            cmbImportType.Enabled = false;
                            cmbDuplicate.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Invalid File, Please select valid csv file.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("No Selected Import Type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = false;
            }

            Cursor.Current = Cursors.Default;

        }
        void CheckRequired()
        {
            if (cmbDuplicate.Text == "New")
            {
                if (cmbImportType.Text == "Item")
                {
                    for (int i = 0; i <= 18; i++)
                    {
                        btnSave.Enabled = dgItem.Rows[i].Cells["ImportFields"].Value != null ? true : false;
                    }
                }
                else if (cmbImportType.Text == "Price")
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        btnSave.Enabled = dgItem.Rows[i].Cells["ImportFields"].Value != null ? true : false;
                    }
                }
                else if (cmbImportType.Text == "Customer" || cmbImportType.Text == "Supplier")
                {
                    for (int i = 0; i <= 8; i++)
                    {
                        btnSave.Enabled = dgItem.Rows[i].Cells["ImportFields"].Value != null ? true : false;
                    }
                }

            }
            else//Updateif()
            {
                if (cmbImportType.Text == "Price")
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        btnSave.Enabled = dgItem.Rows[i].Cells["ImportFields"].Value != null ? true : false;
                    }
                }
                else 
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        btnSave.Enabled = dgItem.Rows[i].Cells["ImportFields"].Value != null ? true : false;
                    }
                }
            }

        }
        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { ",", "\t" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            return csvData;
        }
        public void InsertUpdateItemData()
        {
            string sql = "";
            string colfields = "";
            string paramfields = "";
            int x = 0; //RowsAdded
            int y = 0; //RowsUpdated
            int errors = 0;
            int skip = 0;
            foreach (DataRow dr in dt.Rows)//Importing Data
            {
                int lMinQty = 0;
                int lMaxQty = 0;
                int lReOrderQty = 0;
                float lStdCost = 0;
                float lLCost = 0;
                string lBarcode = "";
                int catID = 0;
                int matched = 0;// Start Check if Existing
                string lPartNo = "";
                lPartNo = dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString();
                matched = IsPartNumberExists(lPartNo);
                if (cmbDuplicate.Text == "New")
                {
                    if (matched == 0)
                    {
                        colfields = "";
                        paramfields = "";
                        string listid = "";
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        foreach (DataGridViewRow dgvr in dgItem.Rows)
                        {

                            if (dgvr.Cells["ImportFields"].Value != null)
                            {
                                if (dgvr.Cells["ImportFields"].Value.ToString() != "")
                                {
                                    if (dgvr.Cells["RealFields"].Value.ToString() == "CategoryCode")
                                    {
                                        colfields += "CategoryID,";
                                        paramfields += "@CategoryID,";
                                        catID = GetCategoryID(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                        param.Add("@CategoryID", catID);
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "CList1")
                                    {
                                        colfields += "CList1,";
                                        paramfields += "@CList1,";
                                        listid = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                        param.Add("@CList1", GetCListID("1", listid));

                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "CList2")
                                    {
                                        colfields += "CList2,";
                                        paramfields += "@CList2,";
                                        listid = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                        param.Add("@CList2", GetCListID("2", listid));

                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "CList3")
                                    {
                                        colfields += "CList3,";
                                        paramfields += "@CList3,";
                                        listid = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                        param.Add("@CList3", GetCListID("3", listid));

                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "MinQty")
                                    {

                                        lMinQty = Convert.ToInt32(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "MaxQty")
                                    {

                                        lMaxQty = Convert.ToInt32(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "ReOrderQty")
                                    {

                                        lReOrderQty = Convert.ToInt32(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "StandardCostEx")
                                    {
                                        string lscost = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                        lStdCost = float.Parse(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "LastCostEx")
                                    {
                                        string lslcost = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                        lLCost = float.Parse(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "BarcodeData")
                                    {

                                        lBarcode = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else
                                    {
                                        colfields += dgvr.Cells["RealFields"].Value.ToString() + ",";
                                        paramfields += "@" + dgvr.Cells["RealFields"].Value.ToString() + ",";
                                        param.Add("@" + dgvr.Cells["RealFields"].Value.ToString(), dgvr.Cells["ImportFields"].Value.ToString() != "" ? dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString() : "");

                                    }
                                }
                            }
                        }
                        paramfields = paramfields.Remove(paramfields.Length - 1);
                        colfields = colfields.Remove(colfields.Length - 1);

                        sql = @"INSERT INTO Items ( " + colfields + ",IsAutoBuild,isMain,AddedBy ) VALUES ( " + paramfields + ",0,0," + CommonClass.UserID + ")";


                        int itemID = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
                        if (itemID > 0)
                        {
                            x += 1;
                            CreateItemsSellingPriceRecord(itemID);
                            CreateItemsQtyRecord(itemID, lMinQty, lReOrderQty, lMaxQty);
                            CreateItemsCostPriceRecord(itemID, lStdCost, lLCost);
                            CreateItemsBarcode(itemID, lBarcode);
                            importLog("New Item Created. PartNumber :" + dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Matching field error.", "Warning!");
                            errors += 1;
                        }
                    }
                }
                else if (cmbDuplicate.Text == "Update")
                {
                    colfields = "";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    foreach (DataGridViewRow dgvr in dgItem.Rows)
                    {
                        if (dgvr.Cells["ImportFields"].Value != null)
                        {
                            if (dgvr.Cells["RealFields"].Value.ToString() != "PartNumber")
                            {
                                if (dgvr.Cells["RealFields"].Value.ToString() == "CategoryCode")
                                {
                                    colfields += "CategoryID=@CategoryID,";
                                    catID = GetCategoryID(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                    param.Add("@CategoryID", catID);
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "CList1")
                                {
                                    colfields += "CList1=@CList1,";
                                    param.Add("@CList1", GetCListID("1", dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString()));

                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "CList2")
                                {
                                    colfields += "CList2=@CList2,";
                                    param.Add("@CList2", GetCListID("2", dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString()));

                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "CList3")
                                {
                                    colfields += "CList3=@CList3,";
                                    param.Add("@CList3", GetCListID("3", dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString()));

                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "MinQty")
                                {

                                    lMinQty = Convert.ToInt32(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "MaxQty")
                                {

                                    lMaxQty = Convert.ToInt32(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "ReOrderQty")
                                {

                                    lReOrderQty = Convert.ToInt32(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "StandardCostEx")
                                {

                                    lStdCost = float.Parse(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "LastCostEx")
                                {

                                    lLCost = float.Parse(dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString());
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "BarcodeData")
                                {

                                    lBarcode = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else
                                {
                                    colfields += dgvr.Cells["RealFields"].Value.ToString() + " = " + "@" + dgvr.Cells["RealFields"].Value.ToString() + ",";
                                    param.Add("@" + dgvr.Cells["RealFields"].Value.ToString(), dgvr.Cells["ImportFields"].Value.ToString() != "" ? dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString() : "");

                                }

                            }
                        }
                    }
                    colfields = colfields.Remove(colfields.Length - 1);

                    sql = @"Update Items set  " + colfields + " where PartNumber = @PartNumber";
                    param.Add("@PartNumber", lPartNo);
                    y += CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                    UpdateItemsQtyRecord(matched, lMinQty, lReOrderQty, lMaxQty);
                    UpdateItemsCostPriceRecord(matched, lStdCost, lLCost);
                    CreateItemsBarcode(matched, lBarcode);
                    importLog("Item Updated. PartNumber :" + dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString());
                }
                else
                {
                    skip += 1;
                }
                matched = 0;
            }

            if (x > 0 || y > 0 || errors > 0)
            {
                MessageBox.Show(x + " Item Added. \n " + y + " Item Updated. \n " + errors + " Errors Encountered. \n " + skip + "Item Skipped. \n Item Imported Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        public void InsertUpdatePriceData()
        {
            string sql = "";
            string colfields = "";
            string paramfields = "";
            int x = 0; //RowsAdded
            int y = 0; //RowsUpdated
            string itemsql = "Select PartNumber, ID From Items";
            int ItemID = 0;
            DataTable dx = new DataTable();
            CommonClass.runSql(ref dx, itemsql);
            int matched = 0; ;
            foreach (DataRow dr in dt.Rows)//Importing Data
            {
                matched = IsPartNumberExists(dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString());
                if (matched != 0)
                {
                    colfields = "";
                    paramfields = "";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    foreach (DataGridViewRow dgvr in dgItem.Rows)
                    {
                        if (dgvr.Cells["ImportFields"].Value != null)
                        {
                            if (dgvr.Cells["RealFields"].Value.ToString() != "PartNumber")
                            {
                                colfields += dgvr.Cells["RealFields"].Value.ToString() + ",";
                                paramfields += "@" + dgvr.Cells["RealFields"].Value.ToString() + ",";
                            }

                            param.Add("@" + dgvr.Cells["RealFields"].Value.ToString(), dgvr.Cells["ImportFields"].Value.ToString() != "" ? dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString() : "");
                        }
                    }
                    colfields += "ItemID,ChangeDate,UserID,";
                    paramfields += "@ItemID,@ChangeDate,@UserID,";
                    param.Add("@ChangeDate", DateTime.Now.ToUniversalTime());
                    param.Add("@UserID", CommonClass.UserID);
                    param.Add("@ItemID", matched);
                    paramfields = paramfields.Remove(paramfields.Length - 1);
                    colfields = colfields.Remove(colfields.Length - 1);

                    sql = @"INSERT INTO PriceChange ( " + colfields + " ) VALUES ( " + paramfields + ")";

                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                    string strcol = dr[dgItem.Rows[2].Cells["ImportFields"].Value.ToString()].ToString();//Price Level
                    string strval = dr[dgItem.Rows[4].Cells["ImportFields"].Value.ToString()].ToString();

                    string ispsql = @"Update ItemsSellingPrice set " + strcol + " = '" + strval + "' WHERE ItemID = " + matched;
                    x += CommonClass.runSql(ispsql, CommonClass.RunSqlInsertMode.QUERY);
                    importLog("Update Price. Item Number :" + dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString());

                }
                else
                {
                    y += 1;
                }
            }

            if (x > 0 || y > 0)
            {
                MessageBox.Show(x + " Item Price Updated. \n " + y + " Skipped Item. \n Item Price Imported Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private int GetProfileID(string pNumber)
        {

            string itemsql = "Select ProfileIDNumber,ID From Profile where ProfileIDNumber = '"+ pNumber + "'";
            DataTable dx = new DataTable();
            CommonClass.runSql(ref dx, itemsql);
            if (dx.Rows.Count > 0)
            {
                return Convert.ToInt32(dx.Rows[0]["ID"].ToString());
            }
            else
            {
                return 0;
            }
        }

        public void InsertUpdateProfileData()
        {
            string sql = "";
            string colfields = "";
            string paramfields = "";
            int x = 0; //RowsAdded
            int y = 0; //RowsUpdated
            int a = 0; //ContactRowAdded
            int ProfileIDHolder = 0;
            string lStreet = ""; 
            string lCity = ""; 
            string lState = ""; 
            string lPostcode = ""; 
            string lCountry = ""; 
            string lPhone = ""; 
            string lEmail = "";
            string lContactPerson = "";

            foreach (DataRow dr in dt.Rows)//Importing Data
            {
                int matched = 0;// Start Check if Existing
                matched = GetProfileID(dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString()); // End Check if Existing return match true if partnumber exist in db

                if (matched ==0)
                {
                    colfields = "";
                    paramfields = "";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    foreach (DataGridViewRow dgvr in dgItem.Rows)
                    {
                        
                       
                        if (dgvr.Cells["ImportFields"].Value != null)
                        {
                            if (dgvr.Cells["ImportFields"].Value.ToString() != "")
                            {
                                if (dgvr.Cells["RealFields"].Value.ToString() == "Street")
                                {
                                    lStreet = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "City")
                                {
                                    lCity = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "State")
                                {
                                    lState = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "Postcode")
                                {
                                    lPostcode = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "Country")
                                {
                                    lCountry = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "Phone")
                                {
                                    lPhone = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "Email")
                                {
                                    lEmail = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else if (dgvr.Cells["RealFields"].Value.ToString() == "ContactPerson")
                                {
                                    lContactPerson = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                                else
                                {
                                    colfields += dgvr.Cells["RealFields"].Value.ToString() + ",";
                                    paramfields += "@" + dgvr.Cells["RealFields"].Value.ToString() + ",";

                                    if (dgvr.Cells["RealFields"].Value.ToString() == "ItemPriceLevel")
                                    {
                                        string lPLevel = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                        lPLevel = lPLevel.Replace("Level", "");
                                        param.Add("@ItemPriceLevel", lPLevel);
                                    }
                                    else
                                    {
                                          param.Add("@" + dgvr.Cells["RealFields"].Value.ToString(), dgvr.Cells["ImportFields"].Value.ToString() != "" ? dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString() : "");
                                    }

                                }
                            }
                        }
                    }
                    colfields += "Type,DiscountDays,Designation";
                    paramfields += "@ProfileType,@DiscountDays,@Designation";
                    if (cmbImportType.Text == "Supplier")
                    {
                        param.Add("@ProfileType", "Supplier");
                    }
                    else
                    {
                        param.Add("@ProfileType", "Customer");
                    }
                    param.Add("@DiscountDays", 0);
                    param.Add("@Designation", "Company");

                    sql = @"INSERT INTO Profile ( " + colfields + " ) VALUES ( " + paramfields + ")";
                    int NewProfileID = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
                    if(NewProfileID > 0)
                    {
                        Dictionary<string, object> paramContacts = new Dictionary<string, object>();
                        colfields = "ProfileID,TypeOfContact,Location,Street,City,State,Postcode,Country,Phone,Email,ContactPerson";
                        paramfields = "@ProfileID,@TypeOfContact,@Location,@Street,@City,@State,@Postcode,@Country,@Phone,@Email,@ContactPerson";
                        paramContacts.Add("@ProfileID", NewProfileID);
                        paramContacts.Add("@TypeOfContact", "Main");
                        paramContacts.Add("@Street", lStreet);
                        paramContacts.Add("@City", lCity);
                        paramContacts.Add("@State", lState);
                        paramContacts.Add("@Postcode", lPostcode);
                        paramContacts.Add("@Country", lCountry);
                        paramContacts.Add("@Phone", lPhone);
                        paramContacts.Add("@Email", lEmail);
                        paramContacts.Add("@ContactPerson", lContactPerson);
                        paramContacts.Add("@Location", 1);
                        sql = @"INSERT INTO Contacts ( " + colfields + " ) VALUES ( " + paramfields + ")";
                        a = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramContacts);

                        importLog("Profile Added. Profile Number :" + dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString());
                        x++;

                    }

                }
                else if (cmbDuplicate.Text == "Update")
                {
                    string lProfileNumber = "";
                    colfields = "";
                    Dictionary<string, object> paramProfile = new Dictionary<string, object>();
                    foreach (DataGridViewRow dgvr in dgItem.Rows)
                    {

                        if (dgvr.Cells["ImportFields"].Value != null)
                        {
                            if (dgvr.Cells["ImportFields"].Value.ToString() != "")
                            {
                                if (dgvr.Cells["RealFields"].Value.ToString() != "ProfileIDNumber")
                                {
                                    if (dgvr.Cells["RealFields"].Value.ToString() == "Street")
                                    {
                                        lStreet = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "City")
                                    {
                                        lCity = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "State")
                                    {
                                        lState = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "Postcode")
                                    {
                                        lPostcode = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "Country")
                                    {
                                        lCountry = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "Phone")
                                    {
                                        lPhone = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "Email")
                                    {
                                        lEmail = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else if (dgvr.Cells["RealFields"].Value.ToString() == "ContactPerson")
                                    {
                                        lContactPerson = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                    }
                                    else
                                    {
                                        colfields += dgvr.Cells["RealFields"].Value.ToString() + " = " + "@" + dgvr.Cells["RealFields"].Value.ToString() + ",";
                                        if (dgvr.Cells["RealFields"].Value.ToString() == "ItemPriceLevel")
                                        {
                                            string lPLevel = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                            lPLevel = lPLevel.Replace("Level", "");
                                            paramProfile.Add("@ItemPriceLevel", lPLevel);
                                        }
                                        else
                                        {
                                            paramProfile.Add("@" + dgvr.Cells["RealFields"].Value.ToString(), dgvr.Cells["ImportFields"].Value.ToString() != "" ? dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString() : "");
                                        }
                                    }
                                }
                                else
                                {
                                    lProfileNumber = dr[dgvr.Cells["ImportFields"].Value.ToString()].ToString();
                                }
                            }
                        }
                    }
                    colfields = colfields.Remove(colfields.Length - 1);
                    paramProfile.Add("@ProfileIDNumber", lProfileNumber);

                    sql = @"Update Profile set  " + colfields + " where ProfileIDNumber = @ProfileIDNumber";

                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramProfile);
                    Dictionary<string, object> paramContacts = new Dictionary<string, object>();
                    colfields = "";
                   
                    colfields += "Location = @LocationID";
                    if(lStreet != "")
                    {
                        colfields += ",Street = @Street";
                        paramContacts.Add("@Street", lStreet);
                    }
                    if (lCity != "")
                    {
                        colfields += ",City = @City";
                        paramContacts.Add("@City", lCity);
                    }
                    if (lState != "")
                    {
                        colfields += ",State = @State";
                        paramContacts.Add("@State", lState);
                    }
                    if (lPostcode != "")
                    {
                        colfields += ",Postcode = @Postcode";
                        paramContacts.Add("@Postcode", lPostcode);
                    }
                    if (lCountry != "")
                    {
                        colfields += ",Country = @Country";
                        paramContacts.Add("@Country", lCountry);
                    }
                    if (lPhone != "")
                    {
                        colfields += ",Phone = @Phone";
                        paramContacts.Add("@Phone", lPhone);
                    }
                    if (lEmail != "")
                    {
                        colfields += ",Email = @Email";
                        paramContacts.Add("@Email", lEmail);
                    }
                    if (lContactPerson != "")
                    {
                        colfields += ",ContactPerson = @ContactPerson";
                        paramContacts.Add("@ContactPerson", lContactPerson);
                    }
                    
                    paramContacts.Add("@LocationID", 1);
                    
                    paramContacts.Add("@ProfileIDNumber", matched);
                    sql = @"Update Contacts set  " + colfields + " where ProfileID = " + matched + " and TypeOfContact = 'Main'";
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, paramContacts);
                    importLog("Profile Updated. Profile Number :" + dr[dgItem.Rows[0].Cells["ImportFields"].Value.ToString()].ToString());
                    y++;

                }
                else
                {

                }

            }

            if (x > 0 || y > 0)
            {
                MessageBox.Show(x + " " + cmbImportType.Text + " Added. \n " + y + " " + cmbImportType.Text + " Updated. \n "+ " " + cmbImportType.Text + "  Imported Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void NewProfileContact(int profileID, string LocationID, string Street,
            string City, string State, string PostCode, string Country, string Phone,
            string Fax, string Email, string ContactPerson, string WWW, string Comments, string TypeOfContact)
        {

            Dictionary<string, object> paramcon = new Dictionary<string, object>();
            string sql2 = "INSERT INTO Contacts(Location, Street, City, State, PostCode, Country, Phone , Fax, Email, Website, ContactPerson, ProfileID, Comments, TypeOfContact)" +
                                      " VALUES (" + LocationID + ",@Street, @City, @State, @Postcode, @Country, @Phone, @Fax, @Email, @Website, @ContactPerson," + profileID + ", @Comments, @TypeOfContact)";

            paramcon.Add("@Street", Street);
            paramcon.Add("@City", City);
            paramcon.Add("@State", State);
            paramcon.Add("@Country", Country);
            paramcon.Add("@Postcode", PostCode);
            paramcon.Add("@Phone", Phone);
            paramcon.Add("@Fax", Fax);
            paramcon.Add("@Email", Email);
            paramcon.Add("@ContactPerson", ContactPerson);
            paramcon.Add("@Website", WWW);
            paramcon.Add("@Comments", Comments);
            paramcon.Add("@TypeOfContact", TypeOfContact);

            int i = CommonClass.runSql(sql2, CommonClass.RunSqlInsertMode.QUERY, paramcon);
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgItem.Rows[0].Cells["ImportFields"].Value == null)
            {
                return;
            }
            if (cmbImportType.Text == "Item")
            {
                InsertUpdateItemData();
            }
            else if (cmbImportType.Text == "Price")
            {
                InsertUpdatePriceData();
            }
            else if (cmbImportType.Text == "Supplier")
            {
                InsertUpdateProfileData();
            }
            else if (cmbImportType.Text == "Customer")
            {
                InsertUpdateProfileData();
            }

        }
    
    private int IsPartNumberExists(string pPartNo)
        {
            string itemsql = "Select PartNumber,ID From Items where PartNumber = '" + pPartNo + "'";
            DataTable dx = new DataTable();
            CommonClass.runSql(ref dx, itemsql);
            if (dx.Rows.Count > 0)
            {
                return Convert.ToInt32(dx.Rows[0]["ID"].ToString());
            }
            else
            {
                return 0;
            }

        }
        private int GetCListID(string pNo, string pListName)
        {
            string itemsql = "Select id From CustomList" + pNo + " where List" + pNo + "Name = '" + pListName + "'";
            DataTable dx = new DataTable();
            CommonClass.runSql(ref dx, itemsql);
            if (dx.Rows.Count > 0)
            {
                return Convert.ToInt32(dx.Rows[0]["id"].ToString());
            }
            else
            {
                return 0;
            }
        }

        private int GetCategoryID(string pCatCode)
        {
            string itemsql = "Select CategoryID From Category where MainCategoryID <> 0 and CategoryCode = '" + pCatCode + "'";
            DataTable dx = new DataTable();
            CommonClass.runSql(ref dx, itemsql);
            if (dx.Rows.Count > 0)
            {
                return Convert.ToInt32(dx.Rows[0]["CategoryID"].ToString());
            }
            else
            {
                return 0;
            }
        }

        private int CreateItemsSellingPriceRecord(int pItemID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            //RECORD Transfer Record
            string sql = @"INSERT INTO ItemsSellingPrice
                            ( ItemID,LocationID,Level0,Level1,Level2,Level3,Level4,Level5,Level6
                            ,Level7,Level8,Level9,Level10,Level11,Level12,
                            Level0QtyDiscount,
                            Level1QtyDiscount,
                            Level2QtyDiscount,
                            Level3QtyDiscount,
                            Level4QtyDiscount,
                            Level5QtyDiscount,
                            Level6QtyDiscount,
                            Level7QtyDiscount,
                            Level8QtyDiscount,
                            Level9QtyDiscount,
                            Level10QtyDiscount,
                            Level11QtyDiscount,
                            Level12QtyDiscount,SalesPrice0,SalesPrice1,SalesPrice2,SalesPrice3,SalesPrice4,SalesPrice5,SalesPrice6,SalesPrice7,SalesPrice8,SalesPrice9,SalesPrice10,SalesPrice11,SalesPrice12)
                            VALUES
                            ( @ItemID,@LocationID,@Level0,@Level1,@Level2,@Level3,@Level4,@Level5,@Level6
                            ,@Level7,@Level8,@Level9,@Level10,@Level11,@Level12,
                            @Level0QtyDiscount,
                            @Level1QtyDiscount,
                            @Level2QtyDiscount,
                            @Level3QtyDiscount,
                            @Level4QtyDiscount,
                            @Level5QtyDiscount,
                            @Level6QtyDiscount,
                            @Level7QtyDiscount,
                            @Level8QtyDiscount,
                            @Level9QtyDiscount,
                            @Level10QtyDiscount,
                            @Level11QtyDiscount,
                            @Level12QtyDiscount,@SalesPrice0,@SalesPrice1,@SalesPrice2,@SalesPrice3,@SalesPrice4,@SalesPrice5,@SalesPrice6,@SalesPrice7,@SalesPrice8,@SalesPrice9,@SalesPrice10,@SalesPrice11,@SalesPrice12)";

            param.Add("@ItemID", pItemID);
            param.Add("@LocationID", 1);
            for (int i = 0; i <= 12; i++)
            {
                param.Add("@Level" + i, 0);
            }
            for (int i = 0; i <= 12; i++)
            {
                param.Add("@Level" + i + "QtyDiscount", 0);
            }
            for (int i = 0; i <= 12; i++)
            {
                param.Add("@SalesPrice" + i, 0);
            }

            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

            return pItemID;
        }
        private int UpdateItemsCostPriceRecord(int pItemID, float pStdCost = 0, float pLCost = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            
            string qtysql = " ";
            for (int i = 22; i <= 23; i++)
            {
                if (dgItem.Rows[i].Cells["ImportFields"].Value != null)
                {
                    qtysql += ","+ dgItem.Rows[i].Cells["RealFields"].Value.ToString() + "= @" + dgItem.Rows[i].Cells["RealFields"].Value.ToString();
                }
            }
            string sql = @"UPDATE ItemsCostPrice SET LocationID=@LocationID"+ qtysql+" WHERE ItemID=@ItemID";
            param.Add("@ItemID", pItemID);
            param.Add("@LocationID", 1);
            param.Add("@LastCostEx", pLCost);
            param.Add("@StandardCostEx", pStdCost);
            param.Add("@AverageCostEx", pStdCost);

            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            return pItemID;
        }
        private int UpdateItemsQtyRecord(int pItemID, int pMinQty = 0, int pReOrderQty = 0, int pMaxQty = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            
            string qtysql = " ";
            for(int i = 24;i <= 26; i++)
            {
                if(dgItem.Rows[i].Cells["ImportFields"].Value != null)
                {
                    qtysql += ","+dgItem.Rows[i].Cells["RealFields"].Value.ToString() + "= @" + dgItem.Rows[i].Cells["RealFields"].Value.ToString();
                }
            }

            string sql = @"Update ItemsQty SET LocationID=@LocationID"+ qtysql + " Where ItemID = @ItemID ";
            param.Add("@ItemID", pItemID);
            param.Add("@LocationID", 1);
            param.Add("@MinQty", pMinQty);
            param.Add("@ReOrderQty", pReOrderQty);
            param.Add("@MaxQty", pMaxQty);

            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            return pItemID;
        }
        private int CreateItemsQtyRecord(int pItemID, int pMinQty = 0, int pReOrderQty = 0, int pMaxQty = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sql = @"INSERT INTO ItemsQty
                             ( ItemID,LocationID,BegQty,OnHandQty,MinQty,ReOrderQty,CommitedQty,MaxQty)
                               VALUES
                              ( @ItemID,@LocationID,@BegQty,@OnHandQty,@MinQty,@ReOrderQty,@CommitedQty,@MaxQty)";

            param.Add("@ItemID", pItemID);
            param.Add("@LocationID", 1);
            param.Add("@BegQty", 0);
            param.Add("@OnHandQty", 0);//Default
            param.Add("@MinQty", pMinQty);
            param.Add("@ReOrderQty", pReOrderQty);
            param.Add("@CommitedQty", 0);
            param.Add("@MaxQty", pMaxQty);

            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            return pItemID;
        }
        private int CreateItemsCostPriceRecord(int pItemID, float pStdCost = 0, float pLCost = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sql = @"INSERT INTO ItemsCostPrice
                             ( ItemID,LocationID,LastCostEx,StandardCostEx,AverageCostEx)
                               VALUES
                              ( @ItemID,@LocationID,@LastCostEx,@StandardCostEx,@AverageCostEx)";

            param.Add("@ItemID", pItemID);
            param.Add("@LocationID", 1);
            param.Add("@LastCostEx", pLCost);
            param.Add("@StandardCostEx", pStdCost);
            param.Add("@AverageCostEx", pStdCost);

            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            return pItemID;
        }
        private int CreateItemsBarcode(int pItemID, string pBarcode)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sql = @"INSERT INTO Barcodes
                             ( ItemID,BarcodeData,BarcodeType)
                               VALUES
                              ( @ItemID,@BarcodeData,'EAN')";

            param.Add("@ItemID", pItemID);
            param.Add("@BarcodeData", pBarcode);

            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            return pItemID;
        }
        private int UpdateItemSellingPrices(string pItemID)
        {
            string RoundingMethod = "";
            Dictionary<string, object> param = new Dictionary<string, object>();
            string setStr = "";
            string StrAudit = "";
            foreach (DataRow rw in dt.Rows)
            {
                if (setStr == "")
                {
                    setStr = rw[dgItem.Rows[2].Cells["ImportFields"].Value.ToString()].ToString() + " = @" + rw[dgItem.Rows[2].Cells["ImportFields"].Value.ToString()].ToString();
                }
                else
                {
                    setStr += "," + rw[dgItem.Rows[2].Cells["ImportFields"].Value.ToString()].ToString() + " = @" + rw[dgItem.Rows[2].Cells["ImportFields"].Value.ToString()].ToString();
                }
                param.Add("@" + rw[dgItem.Rows[2].Cells["ImportFields"].Value.ToString()].ToString(), rw[dgItem.Rows[4].Cells["ImportFields"].Value.ToString()].ToString());
                StrAudit = (StrAudit != "" ? "," : "") + rw[dgItem.Rows[2].Cells["ImportFields"].Value.ToString()] + " changed to " + rw[dgItem.Rows[4].Cells["ImportFields"].Value.ToString()].ToString();
            }

            string sql = @"UPDATE ItemsSellingPrice SET " + setStr + " where ItemID = " + pItemID;

            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

            //if (res > 0)
            //{
            //    CommonClass.SaveSystemLogs(CommonClass.UserID, "IMPORTDATA", "Modified Item Selling Price for Item ID " + pItemID, pItemID.ToString(), "", StrAudit);
            //}
            return res;
        }

        private void dgItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dgItem.Rows[e.RowIndex].Cells["ImportFields"].Value != null)
                CheckRequired();
        }

        public void importLog(string logMessage)
        {
            if (!File.Exists(sourcePath))
            {
                using (StreamWriter w = File.CreateText(sourcePath))
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                    w.WriteLine($"  :{logMessage}");
                    w.WriteLine("-------------------------------");
                    w.Close();
                }
            }
            else
            {
                using (StreamWriter w = File.AppendText(sourcePath))
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                    w.WriteLine($"  :{logMessage}");
                    w.WriteLine("-------------------------------");
                    w.Close();
                }
                
            }
          
        }

    }
}

