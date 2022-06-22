using AbleRetailPOS.References;
using Microsoft.Office.Interop.Excel;
using mshtml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Inventory
{
    public partial class Item : Form
    {
        private static int ItemID;
        private static System.Data.DataTable ItemMainChanges;
        private static System.Data.DataTable ItemQtyChanges;
        private static System.Data.DataTable ItemPriceChanges;
        private static System.Data.DataTable ItemOldPriceChanges;
        private static System.Data.DataTable ItemCostChanges;
        private static bool AutoBuildItemsChanged;
        private static bool ItemImageChanged;
        private static bool IsLoading = false;
        private bool CanView = false;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "Items";
        private string thisFieldCode = "Inactive";
        private bool NoTran = true;
        private int SaveMode;
        private int ParentID = 0;
        string filename = "";
        string File_extension = "";
        private int RoundingMode = 0;
        private IHTMLDocument2 doc;
        private bool imgupdate = false;
        private bool isnewImg = true;
        private int barcodeID;
        private int barcodemode = 0;
        private string customerID = "";
        private string supplierID = "";
        System.Data.DataTable dtPrice;
        System.Data.DataTable dtPromos = new System.Data.DataTable();



        public Item(int pItem = 0)
        {

            InitializeComponent();
            ItemID = pItem;
            ItemMainChanges = new System.Data.DataTable();
            ItemMainChanges.Columns.Add("colname", typeof(string));
            ItemMainChanges.Columns.Add("colvalue", typeof(object));
            ItemQtyChanges = ItemMainChanges.Clone();
            ItemPriceChanges = ItemMainChanges.Clone();
            ItemOldPriceChanges = ItemMainChanges.Clone();
            ItemPriceChanges.Columns.Add("CalcMethod", typeof(string));
            ItemPriceChanges.Columns.Add("PercentChange", typeof(string));
            ItemCostChanges = ItemMainChanges.Clone();
            SaveMode = (pItem != 0 ? 1 : 0);
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                cbFonts.Items.Add(font.Name);
            }

            for (int i = 1; i < 9; i++)
            {
                cbSize.Items.Add(i.ToString());
            }

            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue("Items", out FormRights);
            Boolean outx = false;
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
            CommonClass.AppFormCode.TryGetValue("Items", out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
            AutoBuildItemsChanged = false;
        }

        private void TabProfile_Click(object sender, EventArgs e)
        {

        }

        public void ApplyItemFieldAccess(String FieldID)
        {
            CommonClass.GetAccess(FieldID);
            //TABPROFILE CONTROLS
            foreach (Control c in TabProfile.Controls)
            {
                //CHECKBOX IN TABPROFILE
                if (c is System.Windows.Forms.CheckBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.CheckBox")
                    {
                        System.Windows.Forms.CheckBox chk = (System.Windows.Forms.CheckBox)c;
                        CheckItemFieldsRights(chk);
                    }
                    //TEXTBOX IN TABPROFILE
                }
                else if (c is System.Windows.Forms.TextBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        System.Windows.Forms.TextBox txtb = (System.Windows.Forms.TextBox)c;
                        TextItemFieldsRights(txtb, txtb.Name);
                    }
                    //PICTUREBOX IN TABPROFILE
                }
                else if (c is PictureBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        PictureBox PB = (PictureBox)c;
                        PictureButtonItemFieldsRights(PB);
                    }
                    //BUTTON IN TABPROFILE
                }
                else if (c is System.Windows.Forms.Button)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.Button")
                    {
                        System.Windows.Forms.Button btn = (System.Windows.Forms.Button)c;
                        ButtonItemFieldsRights(btn);
                    }
                }
            }

            //TAB DETAILS CONTROLS
            foreach (Control c in TabDetails.Controls)
            {
                if (c is WebBrowser)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.WebBrowser")
                    {
                        WebBrowser webBrowser = (WebBrowser)c;
                        WebBrowserItemFieldsRights(webBrowser);
                    }
                }

                foreach (Control ctrl in groupBox1.Controls)
                {
                    if (ctrl is PictureBox)
                    {
                        if (ctrl.GetType().ToString() == "System.Windows.Forms.PictureBox")
                        {
                            PictureBox PB = (PictureBox)ctrl;
                            PictureButtonItemFieldsRights(PB);
                        }
                    }
                    else if (ctrl is System.Windows.Forms.TextBox)
                    {
                        if (ctrl.GetType().ToString() == "System.Windows.Forms.TextBox")
                        {
                            System.Windows.Forms.TextBox txtb = (System.Windows.Forms.TextBox)ctrl;
                            TextItemFieldsRights(txtb, txtb.Name);
                        }
                    }
                }
                foreach (Control ctrl2 in groupBox2.Controls)
                {
                    if (ctrl2 is PictureBox)
                    {
                        if (ctrl2.GetType().ToString() == "System.Windows.Forms.PictureBox")
                        {
                            PictureBox PB = (PictureBox)ctrl2;
                            PictureButtonItemFieldsRights(PB);
                        }
                    }
                    else if (ctrl2 is System.Windows.Forms.TextBox)
                    {
                        if (ctrl2.GetType().ToString() == "System.Windows.Forms.TextBox")
                        {
                            System.Windows.Forms.TextBox txtb = (System.Windows.Forms.TextBox)ctrl2;
                            TextItemFieldsRights(txtb, txtb.Name);
                        }
                    }
                }

            }
            //TAB PURCHASE CONTROLS
            foreach (Control c in TabPurchase.Controls)
            {
                if (c is PictureBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        PictureBox PB = (PictureBox)c;
                        PictureButtonItemFieldsRights(PB);
                    }
                }
                else if (c is System.Windows.Forms.TextBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        System.Windows.Forms.TextBox txtb = (System.Windows.Forms.TextBox)c;
                        TextItemFieldsRights(txtb, txtb.Name);
                    }
                }
                else if (c is NumericUpDown)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.NumericUpDown")
                    {
                        NumericUpDown NumUpDown = (NumericUpDown)c;
                        NumericUpDownItemFieldsRights(NumUpDown);
                    }
                }
            }

            //TAB SALES CONTROLS
            foreach (Control c in TabSales.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        System.Windows.Forms.TextBox txtb = (System.Windows.Forms.TextBox)c;
                        TextItemFieldsRights(txtb, txtb.Name);
                    }
                }
                else if (c is NumericUpDown)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.NumericUpDown")
                    {
                        NumericUpDown NumUpDown = (NumericUpDown)c;
                        NumericUpDownItemFieldsRights(NumUpDown);
                    }
                }
                else if (c is PictureBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.PictureBox")
                    {
                        PictureBox PB = (PictureBox)c;
                        PictureButtonItemFieldsRights(PB);
                    }
                }
                else if (c is DataGridView)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.DataGridView")
                    {
                        DataGridView PB = (DataGridView)c;
                        DataGridItemFieldsRights(PB);
                    }
                }


                foreach (Control ctrl in groupBox5.Controls)
                {
                    if (ctrl is DateTimePicker)
                    {
                        if (ctrl.GetType().ToString() == "System.Windows.Forms.DateTimePicker")
                        {
                            DateTimePicker dtPicker = (DateTimePicker)ctrl;
                            DatTiemPickerItemFieldsRights(dtPicker);
                        }
                    }
                    else if (ctrl is System.Windows.Forms.TextBox)
                    {
                        if (ctrl.GetType().ToString() == "System.Windows.Forms.CheckBox")
                        {
                            System.Windows.Forms.TextBox txtb = (System.Windows.Forms.TextBox)ctrl;
                            TextItemFieldsRights(txtb, txtb.Name);
                        }
                    }
                    else if (ctrl is RadioButton)
                    {
                        if (ctrl.GetType().ToString() == "System.Windows.Forms.RadioButton")
                        {
                            RadioButton rdb = (RadioButton)ctrl;
                            RadioButtonItemFieldsRights(rdb);
                        }
                    }
                    else if (ctrl is System.Windows.Forms.CheckBox)
                    {
                        if (ctrl.GetType().ToString() == "System.Windows.Forms.CheckBox")
                        {
                            System.Windows.Forms.CheckBox chk = (System.Windows.Forms.CheckBox)ctrl;
                            CheckItemFieldsRights(chk);
                        }
                        //TEXTBOX IN TABPROFILE
                    }
                }
            }
            //TAB AUTOBUIL CONTROLS
            foreach (Control c in TabAutoBuild.Controls)
            {
                if (c is System.Windows.Forms.CheckBox)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.CheckBox")
                    {
                        System.Windows.Forms.CheckBox chk = (System.Windows.Forms.CheckBox)c;
                        CheckItemFieldsRights(chk);
                    }
                }
                else if (c is System.Windows.Forms.Button)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.Button")
                    {
                        System.Windows.Forms.Button btn = (System.Windows.Forms.Button)c;
                        ButtonItemFieldsRights(btn);
                    }
                }
                else if (c is DataGridView)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.DataGridView")
                    {
                        DataGridView PB = (DataGridView)c;
                        DataGridItemFieldsRights(PB);
                    }
                }
            }
            //TAB BARCODE CONTROLS
            foreach (Control c in tabBarcode.Controls)
            {
                if (c is System.Windows.Forms.Button)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.Button")
                    {
                        System.Windows.Forms.Button btn = (System.Windows.Forms.Button)c;
                        ButtonItemFieldsRights(btn);
                    }
                }
                else if (c is DataGridView)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.DataGridView")
                    {
                        DataGridView PB = (DataGridView)c;
                        DataGridItemFieldsRights(PB);
                    }
                }
                foreach (Control ctrl in gbBarcodeDetail.Controls)
                {
                    if (ctrl is System.Windows.Forms.TextBox)
                    {
                        if (ctrl.GetType().ToString() == "System.Windows.Forms.TextBox")
                        {
                            System.Windows.Forms.TextBox txtb = (System.Windows.Forms.TextBox)ctrl;
                            TextItemFieldsRights(txtb, txtb.Name);
                        }
                    }
                }
            }
        }

        private void CheckItemFieldsRights(System.Windows.Forms.CheckBox item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }

        private void TextItemFieldsRights(System.Windows.Forms.TextBox item, string itemName)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }

                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }

        private void ButtonItemFieldsRights(System.Windows.Forms.Button item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }

        private void PictureButtonItemFieldsRights(PictureBox item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }

        private void WebBrowserItemFieldsRights(WebBrowser item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Visible = true;
                    }
                    else
                    {
                        ((Control)item).Visible = false;
                    }
                }

                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Enabled = true;
                    }
                    else
                    {
                        ((Control)item).Enabled = false;
                    }
                }
            }
        }

        private void NumericUpDownItemFieldsRights(NumericUpDown item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Visible = true;
                    }
                    else
                    {
                        ((Control)item).Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Enabled = true;
                    }
                    else
                    {
                        ((Control)item).Enabled = false;
                    }
                }
            }
        }

        private void DatTiemPickerItemFieldsRights(DateTimePicker item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Visible = true;
                    }
                    else
                    {
                        ((Control)item).Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Enabled = true;
                    }
                    else
                    {
                        ((Control)item).Enabled = false;
                    }
                }
            }
        }

        private void DataGridItemFieldsRights(DataGridView item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Visible = true;
                    }
                    else
                    {
                        ((Control)item).Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Enabled = true;
                    }
                    else
                    {
                        ((Control)item).Enabled = false;
                    }
                }
            }
        }

        private void RadioButtonItemFieldsRights(RadioButton item)
        {
            Dictionary<string, Boolean> lDic;
            if (CommonClass.UserAccess.TryGetValue(item.Name, out lDic))
            {
                Boolean valstr = false;
                if (lDic.TryGetValue("View", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Visible = true;
                    }
                    else
                    {
                        ((Control)item).Visible = false;
                    }
                }
                if (lDic.TryGetValue("Edit", out valstr))
                {
                    if (valstr == true)
                    {
                        ((Control)item).Enabled = true;
                    }
                    else
                    {
                        ((Control)item).Enabled = false;
                    }
                }
            }
        }

        private void chkIsBought_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'IsBought' Or colname = 'COSAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
            }

            bool lEnabled = false;
            if (this.chkIsBought.Checked)
            {
                lEnabled = true;
            }
            this.grpCOS.Enabled = lEnabled;
            if (SaveMode > 0)
            {
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "IsBought";
                rw["colvalue"] = lEnabled;
                ItemMainChanges.Rows.Add(rw);
                rw = ItemMainChanges.NewRow();
                rw["colname"] = "COSAccountID";
                rw["colvalue"] = (lEnabled == true ? this.txtCostAcct.Text : "");
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void chkIsSold_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'IsSold' Or colname = 'IncomeAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }

            }
            bool lEnabled = false;
            if (this.chkIsSold.Checked)
            {
                lEnabled = true;
            }
            this.grpIncome.Enabled = lEnabled;
            if (SaveMode > 0)
            {

                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "IsSold";
                rw["colvalue"] = lEnabled;
                ItemMainChanges.Rows.Add(rw);
                rw = ItemMainChanges.NewRow();
                rw["colname"] = "IncomeAccountID";
                rw["colvalue"] = (lEnabled == true ? this.txtIncomeAcct.Text : "");
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void chkIsCounted_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'IsCounted' Or colname = 'AssetAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }

            }
            bool lEnabled = false;
            if (this.chkIsCounted.Checked)
            {
                lEnabled = true;
            }
            this.grpAsset.Enabled = lEnabled;
            if (SaveMode > 0)
            {
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "IsCounted";
                rw["colvalue"] = lEnabled;
                ItemMainChanges.Rows.Add(rw);
                rw = ItemMainChanges.NewRow();
                rw["colname"] = "AssetAccountID";
                rw["colvalue"] = (lEnabled == true ? this.txtAssetAcct.Text : "");
                ItemMainChanges.Rows.Add(rw);

            }
        }

        private void CheckTransactions(int pItemID)
        {
            string chkSql = @"SELECT sl.EntityID
                                    FROM SalesLines sl
                                    INNER JOIN Sales s ON s.SalesID = sl.SalesID
                                    WHERE s.LayoutType = 'Item'
                                    AND sl.EntityID = " + pItemID;
            chkSql += @" UNION
                                    SELECT pl.EntityID
                                    FROM PurchaseLines pl
                                    INNER JOIN Purchases p ON p.PurchaseID = pl.PurchaseID
                                    WHERE p.LayoutType = 'Item'
                                    AND pl.EntityID =" + pItemID;
            chkSql += @" UNION
                                    SELECT il.ItemID as EntityID
                                    FROM ItemsAdjustmentLines il
                                    INNER JOIN ItemsAdjustment i ON i.ItemAdjID = il.ItemAdjID
                                    WHERE il.ItemID = " + pItemID;
            int entity = CommonClass.runSql(chkSql, CommonClass.RunSqlInsertMode.SCALAR);
            if (entity > 0)
            {
                chkIsBought.Enabled = false;
                chkIsSold.Enabled = false;
                chkIsCounted.Enabled = false;
                txtCostAcct.Enabled = false;
                pbCat.Enabled = false;
                txtCategory.Enabled = false;
                //  pbCOS.Enabled = false;
                txtIncomeAcct.Enabled = false;
                //   pbIncome.Enabled = false;
                txtAssetAcct.Enabled = false;
                //    pbAsset.Enabled = false;
                chkAutoBuild.Enabled = false;
                NoTran = false;

                this.btnDelete.Enabled = false;
            }
            else
            {
                NoTran = true;
            }
        }

        Image ConvertBinarytoImage(byte[] binary)
        {
            using (MemoryStream ms = new MemoryStream(binary))
            {
                return Image.FromStream(ms);
            }
        }

        void LoadImages(int Item)
        {
            string selectSql = "SELECT * From Images WHERE ItemID = " + Item;
            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                pictureBox1.Image = ConvertBinarytoImage((byte[])dr["Specimen"]);
                isnewImg = false;
            }
        }

        private void Item_Load(object sender, EventArgs e)
        {
            if (!CanView)
            {
                MessageBox.Show("User Access Restriction", "User Restriction");
                this.BeginInvoke(new MethodInvoker(Close));
            }//this.lblPriceNote.Text += (CommonClass.IsItemPriceInclusive == true ? " Inclusive" : " Exclusive");
            HTMLEditor.DocumentText = "<html><body></body></html>";
            doc = HTMLEditor.Document.DomDocument as IHTMLDocument2;
            doc.designMode = "On";
            cbFonts.Text = this.HTMLEditor.Font.Name.ToString();
            cbSize.Text = this.HTMLEditor.Font.Size.ToString();
            if (ItemID != 0)
            {
                this.btnRecord.Enabled = CanEdit;
                this.btnDelete.Enabled = CanDelete;

                LoadItem(ItemID);
                CheckTransactions(ItemID);
                ItemNameLbl.Visible = true;
                txtItemNameLbl.Visible = true;
                PartNumLbl.Visible = true;
                txtPartNumberLabel.Visible = true;
                txtItemNameLbl.Text = txtItemName.Text;
                txtPartNumberLabel.Text = txtPartNumber.Text;
            }
            else
            {//NEW ITEM TO CREATE
                ItemNameLbl.Visible = false;
                txtItemNameLbl.Visible = false;
                PartNumLbl.Visible = false;
                txtPartNumberLabel.Visible = false;
                tabControl1.Size = new Size(879, 558);
                tabControl1.Location = new System.Drawing.Point(10, 6);
                dtPrice = new System.Data.DataTable();
            }
            SetListsName();
            SalesPrice();
            ApplyItemFieldAccess(CommonClass.UserID);
        }

        private void SetListsName()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomNames WHERE RecordType = 'Items'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                System.Data.DataTable dt = new System.Data.DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    this.lblList1Name.Text = dt.Rows[0]["CList1Name"].ToString();
                    this.lblList2Name.Text = dt.Rows[0]["CList2Name"].ToString();
                    this.lblList3Name.Text = dt.Rows[0]["CList3Name"].ToString();
                    this.lblField1Name.Text = dt.Rows[0]["CField1Name"].ToString();
                    this.lblField2Name.Text = dt.Rows[0]["CField2Name"].ToString();
                    this.lblField3Name.Text = dt.Rows[0]["CField3Name"].ToString();
                }
                else
                {
                    this.lblList1Name.Text = "";
                    this.lblList2Name.Text = "";
                    this.lblList3Name.Text = "";
                    this.lblField1Name.Text = "";
                    this.lblField2Name.Text = "";
                    this.lblField3Name.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private byte[] ConvertImageToBinary(System.Drawing.Image imageToConvert, string formatimg)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    if (formatimg == ".png")
                        imageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    else if (formatimg == ".jpeg" || formatimg == ".jpg")
                        imageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }

        private int SaveImage(int Item)
        {
            string sqlImg = "";
            int imgID;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Specimen", ConvertImageToBinary(pictureBox1.Image, File_extension.ToLower()));
            param.Add("@ItemID", Item);
            if (isnewImg)
            {
                sqlImg = "INSERT INTO Images(Specimen,ItemID) VALUES (@Specimen,@ItemID)";
                imgID = CommonClass.runSql(sqlImg, CommonClass.RunSqlInsertMode.SCALAR, param);
            }
            else
            {
                sqlImg = "UPDATE Images SET Specimen = @Specimen WHERE ItemID =" + Item;
                imgID = CommonClass.runSql(sqlImg, CommonClass.RunSqlInsertMode.QUERY, param);
            }


            //cmd.Parameters.AddWithValue("@Specimen", ConvertImageToBinary(pictureBox1.Image, File_extension));
            //// cmd.Parameters.AddWithValue("@LocationID", LocID);
            //cmd.Parameters.AddWithValue("@ItemID", Item);
            //con.Open();
            //  int imgid = Convert.ToInt32(cmd.ExecuteScalar());
            if (imgID > 0)
            {
                string titles = "Information";
            }
            return imgID;
        }

        private void CreateNewItem()
        {
            //INSERT INTO ITEM TABLE
            ItemID = CreateItemRecord();
            int lCostRec = CreateItemsCostPriceRecord(ItemID);
            int lQtyRec = CreateItemsQtyRecord(ItemID);
            int lPriceRec = CreateItemsSellingPriceRecord(ItemID);
            UpdateItemAutoBuildParts(ItemID.ToString());
            if (ItemID > 0)
            {
                if (pictureBox1.Image != null)
                {
                    SaveImage(ItemID);
                }
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created new item with Item ID" + ItemID, ItemID.ToString());
                MessageBox.Show("New Item Created successfully", "Item Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveMode = 1;
                LoadItem(ItemID);
                CheckTransactions(ItemID);
            }
        }

        private int CreateItemRecord()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            string sql = @"INSERT INTO Items
                            ( PartNumber, ItemNumber, SupplierItemNumber, ItemName, ItemDescription
                            , IsBought, IsSold, IsCounted, COSAccountID, IncomeAccountID, AssetAccountID
                            , CList1, CList2, CList3, CField1, CField2, CField3
                            , BuyingUOM, QtyPerBuyingUOM, SupplierID, PurchaseTaxCode
                            , SellingUOM, QtyPerSellingUnit, SalesTaxCode, IsAutoBuild, AddedBy, IsInactive, CategoryID, BrandName,ItemDescriptionSimple,BundleType  )
                            VALUES
                            ( @PartNumber, @ItemNumber, @SupplierItemNumber, @ItemName, @ItemDescription
                            , @IsBought, @IsSold, @IsCounted, @COSAccountID, @IncomeAccountID, @AssetAccountID
                            , @CList1, @CList2, @CList3, @CField1, @CField2, @CField3
                            , @BuyingUOM, @QtyPerBuyingUOM, @SupplierID, @PurchaseTaxCode
                            , @SellingUOM, @QtyPerSellingUnit, @SalesTaxCode, @IsAutoBuild, @AddedBy, @IsInactive,@CategoryID, @BrandName,@ItemDescriptionSimple,@BundleType)";

            param.Add("@PartNumber", this.txtPartNumber.Text);
            param.Add("@ItemNumber", this.txtItemNumber.Text);
            param.Add("@SupplierItemNumber", this.txtSupplierPartNo.Text);
            param.Add("@ItemName", this.txtItemName.Text);
            string itemDesc = HTMLEditor.DocumentText;
            param.Add("@ItemDescription", itemDesc);
            param.Add("@IsBought", this.chkIsBought.Checked);
            param.Add("@IsSold", this.chkIsSold.Checked);
            param.Add("@IsCounted", this.chkIsCounted.Checked);
            param.Add("@COSAccountID", (this.txtCostAcct.Text == "" ? "0" : this.txtCostAcct.Text));
            param.Add("@IncomeAccountID", (this.txtIncomeAcct.Text == "" ? "0" : this.txtIncomeAcct.Text));
            param.Add("@AssetAccountID", (this.txtAssetAcct.Text == "" ? "0" : this.txtAssetAcct.Text));
            param.Add("@CList1", (this.lblList1ID.Text == "" ? "0" : this.lblList1ID.Text));
            param.Add("@CList2", (this.lblList2ID.Text == "" ? "0" : this.lblList2ID.Text));
            param.Add("@CList3", (this.lblList3ID.Text == "" ? "0" : this.lblList3ID.Text));
            param.Add("@CField1", this.txtField1.Text);
            param.Add("@CField2", this.txtField2.Text);
            param.Add("@CField3", this.txtField3.Text);
            param.Add("@BuyingUOM", this.txtBUOM.Text);
            param.Add("@QtyPerBuyingUOM", this.txtQtyBUOM.Value);
            param.Add("@SupplierID", (this.lblSupplierID.Text == "" ? "0" : this.lblSupplierID.Text));
            param.Add("@PurchaseTaxCode", this.txtPTaxCode.Text);
            param.Add("@SellingUOM", this.txtSUOM.Text);
            param.Add("@QtyPerSellingUnit", this.txtQtySUOM.Value);
            param.Add("@SalesTaxCode", this.txtSTaxCode.Text);
            if (chkAutoBuild.Checked)
            {

            }
            param.Add("@IsAutoBuild", this.chkAutoBuild.Checked);
            param.Add("@AddedBy", CommonClass.UserID);
            param.Add("@IsInactive", cbInactive.Checked);
            // cmd.Parameters.AddWithValue("@CategoryID", );
            param.Add("@CategoryID", ParentID);
            param.Add("@BrandName", this.txtBrand.Text);
            param.Add("@ItemDescriptionSimple", itemDesc2.Text);

            if (chkAutoBuild.Checked)
            {
                if (rdoStatic.Checked)
                    param.Add("@BundleType", this.rdoStatic.Text);
                else if (rdoDynamic.Checked)
                    param.Add("@BundleType", this.rdoDynamic.Text);
                else
                    param.Add("@BundleType", this.rdoIngredient.Text);
            }
            else
            {
                param.Add("@BundleType", "");
            }




            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.SCALAR, param);
            return res;
        }

        private int CreateItemsCostPriceRecord(int pItemID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string sql = @"INSERT INTO ItemsCostPrice
                             ( ItemID,LocationID,LastCostEx,StandardCostEx,AverageCostEx)
                               VALUES
                              ( @ItemID,@LocationID,@LastCostEx,@StandardCostEx,@AverageCostEx)";

            param.Add("@ItemID", pItemID);
            param.Add("@LocationID", 1);
            param.Add("@LastCostEx", this.txtLastCost.Value);
            param.Add("@StandardCostEx", this.txtStandardCost.Value);
            param.Add("@AverageCostEx", this.txtAverageCost.Value);

            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            return pItemID;
        }

        private int CreateItemsQtyRecord(int pItemID)
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
            param.Add("@MinQty", this.txtMinQty.Value);
            param.Add("@ReOrderQty", this.txtReOrderQty.Value);
            param.Add("@CommitedQty", 0);
            param.Add("@MaxQty", txtMaxQty.Value);

            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            return pItemID;
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
                            Level12QtyDiscount,SalesPrice0,SalesPrice1,SalesPrice2,SalesPrice3,SalesPrice4,SalesPrice5,SalesPrice6,SalesPrice7,SalesPrice8,SalesPrice9,SalesPrice10,SalesPrice11,SalesPrice12,
                            CalculationBasis,
                            CostBasis ,
                            RoundingMethod,StartSaleDate,EndSalesDate)
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
                            @Level12QtyDiscount,@SalesPrice0,@SalesPrice1,@SalesPrice2,@SalesPrice3,@SalesPrice4,@SalesPrice5,@SalesPrice6,@SalesPrice7,@SalesPrice8,@SalesPrice9,@SalesPrice10,@SalesPrice11,@SalesPrice12 ,
                            @CalculationBasis,
                            @CostBasis ,
                            @RoundingMethod,@StartSaleDate,@EndSalesDate)";

            param.Add("@ItemID", pItemID);
            param.Add("@LocationID", 1);
            param.Add("@Level0", dgPriceLvl.Rows[0].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[0].Cells[3].Value.ToString());
            param.Add("@Level1", dgPriceLvl.Rows[1].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[1].Cells[3].Value.ToString());
            param.Add("@Level2", dgPriceLvl.Rows[2].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[2].Cells[3].Value.ToString());
            param.Add("@Level3", dgPriceLvl.Rows[3].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[3].Cells[3].Value.ToString());
            param.Add("@Level4", dgPriceLvl.Rows[4].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[4].Cells[3].Value.ToString());
            param.Add("@Level5", dgPriceLvl.Rows[5].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[5].Cells[3].Value.ToString());
            param.Add("@Level6", dgPriceLvl.Rows[6].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[6].Cells[3].Value.ToString());
            param.Add("@Level7", dgPriceLvl.Rows[7].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[7].Cells[3].Value.ToString());
            param.Add("@Level8", dgPriceLvl.Rows[8].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[8].Cells[3].Value.ToString());
            param.Add("@Level9", dgPriceLvl.Rows[9].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[9].Cells[3].Value.ToString());
            param.Add("@Level10", dgPriceLvl.Rows[10].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[10].Cells[3].Value.ToString());
            param.Add("@Level11", dgPriceLvl.Rows[11].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[11].Cells[3].Value.ToString());
            param.Add("@Level12", dgPriceLvl.Rows[12].Cells[3].Value == null ? "0" : dgPriceLvl.Rows[12].Cells[3].Value.ToString());
            //qty disc
            param.Add("@Level0QtyDiscount", dgPriceLvl.Rows[0].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[0].Cells[7].Value.ToString());
            param.Add("@Level1QtyDiscount", dgPriceLvl.Rows[1].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[1].Cells[7].Value.ToString());
            param.Add("@Level2QtyDiscount", dgPriceLvl.Rows[2].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[2].Cells[7].Value.ToString());
            param.Add("@Level3QtyDiscount", dgPriceLvl.Rows[3].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[3].Cells[7].Value.ToString());
            param.Add("@Level4QtyDiscount", dgPriceLvl.Rows[4].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[4].Cells[7].Value.ToString());
            param.Add("@Level5QtyDiscount", dgPriceLvl.Rows[5].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[5].Cells[7].Value.ToString());
            param.Add("@Level6QtyDiscount", dgPriceLvl.Rows[6].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[6].Cells[7].Value.ToString());
            param.Add("@Level7QtyDiscount", dgPriceLvl.Rows[7].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[7].Cells[7].Value.ToString());
            param.Add("@Level8QtyDiscount", dgPriceLvl.Rows[8].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[8].Cells[7].Value.ToString());
            param.Add("@Level9QtyDiscount", dgPriceLvl.Rows[9].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[9].Cells[7].Value.ToString());
            param.Add("@Level10QtyDiscount", dgPriceLvl.Rows[10].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[10].Cells[7].Value.ToString());
            param.Add("@Level11QtyDiscount", dgPriceLvl.Rows[11].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[11].Cells[7].Value.ToString());
            param.Add("@Level12QtyDiscount", dgPriceLvl.Rows[12].Cells[7].Value == null ? "0" : dgPriceLvl.Rows[12].Cells[7].Value.ToString());
            //SalePrice
            param.Add("@SalesPrice0", dgPriceLvl.Rows[0].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[0].Cells[6].Value.ToString());
            param.Add("@SalesPrice1", dgPriceLvl.Rows[1].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[1].Cells[6].Value.ToString());
            param.Add("@SalesPrice2", dgPriceLvl.Rows[2].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[2].Cells[6].Value.ToString());
            param.Add("@SalesPrice3", dgPriceLvl.Rows[3].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[3].Cells[6].Value.ToString());
            param.Add("@SalesPrice4", dgPriceLvl.Rows[4].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[4].Cells[6].Value.ToString());
            param.Add("@SalesPrice5", dgPriceLvl.Rows[5].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[5].Cells[6].Value.ToString());
            param.Add("@SalesPrice6", dgPriceLvl.Rows[6].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[6].Cells[6].Value.ToString());
            param.Add("@SalesPrice7", dgPriceLvl.Rows[7].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[7].Cells[6].Value.ToString());
            param.Add("@SalesPrice8", dgPriceLvl.Rows[8].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[8].Cells[6].Value.ToString());
            param.Add("@SalesPrice9", dgPriceLvl.Rows[9].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[9].Cells[6].Value.ToString());
            param.Add("@SalesPrice10", dgPriceLvl.Rows[10].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[10].Cells[6].Value.ToString());
            param.Add("@SalesPrice11", dgPriceLvl.Rows[11].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[11].Cells[6].Value.ToString());
            param.Add("@SalesPrice12", dgPriceLvl.Rows[12].Cells[6].Value == null ? "0" : dgPriceLvl.Rows[12].Cells[6].Value.ToString());
            string RoundingMethod = "";
            string setStr = "";
            if (rdoAC.Checked)
            {
                param.Add("@CalculationBasis", "Average Cost");
                param.Add("@CostBasis", txtAverageCost.Value);
            }
            else
            {
                param.Add("@CalculationBasis", "Last Cost");
                param.Add("@CostBasis", txtLastCost.Value);
            }
            if (chkNoRounding.Checked)
                RoundingMethod = "No Rounding";
            else if (chkRound5.Checked)
                RoundingMethod = "Rounding 5 cent";
            else if (chkRound99.Checked)
                RoundingMethod = "Rounding 99 cent";
            DateTime sdate = SaleStartDatePicker.Value.ToUniversalTime();
            sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 00, 00, 00);
            DateTime edate = SaleEndDatePicker.Value.ToUniversalTime();
            edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
            param.Add("@RoundingMethod", RoundingMethod);
            param.Add("@StartSaleDate", sdate);
            param.Add("@EndSalesDate", edate);
            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            if (res > 0)
            {
                if (ItemPriceChanges.Rows.Count > 0)
                {
                    if (ItemOldPriceChanges.Rows.Count > 0)
                    {
                        foreach (DataRow rw in ItemPriceChanges.Rows)
                        {
                            Dictionary<string, object> priceupdateParam = new Dictionary<string, object>();
                            priceupdateParam.Add("@PriceLevel", rw["colname"].ToString());
                            priceupdateParam.Add("@PriceAfter", rw["colvalue"].ToString());

                            priceupdateParam.Add("@ItemID", pItemID);
                            priceupdateParam.Add("@ChangeDate", DateTime.Now.ToUniversalTime());
                            priceupdateParam.Add("@UserID", CommonClass.UserID);
                            priceupdateParam.Add("@CalcMethod", rw["CalcMethod"].ToString());
                            priceupdateParam.Add("@PercentChange", rw["PercentChange"].ToString());

                            foreach (DataRow dx in ItemOldPriceChanges.Rows)
                            {
                                if (dx["colname"].ToString() == rw["colname"].ToString())
                                {
                                    priceupdateParam.Add("@PriceBefore", dx["colvalue"].ToString());
                                }
                            }

                            string priceupdate = "INSERT INTO PriceChange (ItemID,PriceBefore,PriceAfter,ChangeDate,UserID,PriceLevel,CalcMethod,PercentChange )VALUES(@ItemID,@PriceBefore,@PriceAfter,@ChangeDate,@UserID,@PriceLevel,@CalcMethod,@PercentChange )";
                            CommonClass.runSql(priceupdate, CommonClass.RunSqlInsertMode.QUERY, priceupdateParam);
                        }
                    }
                }
            }
            return pItemID;
        }

        private void CreateItemAutoBuild()
        {

        }

        private void LoadItem(int pItemID)
        {
            LoadItemsMain(pItemID);
            LoadItemsCostPrice(pItemID);
            LoadItemsQty(pItemID);
            LoadItemsSellingPrice(pItemID);
            LoadItemsAutoBuild(pItemID);
            LoadImages(pItemID);
        }

        private void LoadItemsMain(int pItemID)
        {
            IsLoading = true;
            string selectSql = @"SELECT i.*, p.Name, l1.List1Name, l2.List2Name, l3.List3Name 
                                    FROM  (((Items as i left join Profile as p on i.SupplierID = p.ID) 
                                    left join CustomList1 as l1 on i.Clist1 = l1.ID ) 
                                    left join CustomList2 as l2 on i.Clist2 = l2.ID ) 
                                    left join CustomList3 as l3 on i.Clist3 = l3.ID where i.ID = " + pItemID;

            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                cbInactive.Checked = (bool)dr["IsInactive"];
                this.txtBrand.Text = dr["BrandName"].ToString();
                this.txtPartNumber.Text = dr["PartNumber"].ToString();
                this.txtItemNumber.Text = dr["ItemNumber"].ToString();
                this.txtSupplierPartNo.Text = dr["SupplierItemNumber"].ToString();
                this.txtItemName.Text = dr["ItemName"].ToString();

                if (dr["ItemDescription"].ToString() != "")
                {
                    HTMLEditor.Document.OpenNew(false).Write(dr["ItemDescription"].ToString());

                }

                itemDesc2.Text = dr["ItemDescriptionSimple"].ToString();
                this.chkIsBought.Checked = (bool)dr["IsBought"];
                if (this.chkIsBought.Checked)
                {
                    //   string[] lAcctC = new string[2];
                    this.txtCostAcct.Text = dr["COSAccountID"].ToString();
                    //    this.lblCOSAccountID.Text = dr["COSAccountID"].ToString();
                    //    this.txtCostAcct.Text = lAcctC[0];
                    //    this.lblCOSAcctName.Text = lAcctC[2];
                }
                this.grpCOS.Enabled = this.chkIsBought.Checked;

                this.chkIsCounted.Checked = (bool)dr["IsCounted"];
                if (this.chkIsCounted.Checked)
                {
                    //   string[] lAcctA = new string[2];
                    this.txtAssetAcct.Text = dr["AssetAccountID"].ToString();
                    //   this.lblAssetAccountID.Text = dr["AssetAccountID"].ToString();
                    //  this.txtAssetAcct.Text = lAcctA[0];
                    //   this.lblAssetAcctName.Text = lAcctA[2];
                }
                this.grpAsset.Enabled = this.chkIsCounted.Checked;

                this.chkIsSold.Checked = (bool)dr["IsSold"];
                if (this.chkIsSold.Checked)
                {
                    //  string[] lAcctI = new string[2];
                    this.txtIncomeAcct.Text = dr["IncomeAccountID"].ToString();
                    //this.lblIncomeAccountID.Text = dr["IncomeAccountID"].ToString();
                    //  this.txtIncomeAcct.Text = lAcctI[0];
                    //this.lblIncomeAcctName.Text = lAcctI[2];
                }
                this.grpIncome.Enabled = this.chkIsCounted.Checked;
                this.txtList1.Text = dr["List1Name"].ToString();
                this.txtList2.Text = dr["List2Name"].ToString();
                this.txtList3.Text = dr["List3Name"].ToString();
                this.lblList1ID.Text = dr["CList1"].ToString();
                this.lblList2ID.Text = dr["CList2"].ToString();
                this.lblList3ID.Text = dr["CList3"].ToString();
                this.txtField1.Text = dr["CField1"].ToString();
                this.txtField2.Text = dr["CField2"].ToString();
                this.txtField3.Text = dr["CField3"].ToString();
                this.txtBUOM.Text = dr["BuyingUOM"].ToString();
                this.txtQtyBUOM.Value = Convert.ToDecimal(dr["QtyPerBuyingUOM"].ToString());
                this.txtSupplier.Text = dr["Name"].ToString();
                this.lblSupplierID.Text = dr["SupplierID"].ToString();
                this.txtPTaxCode.Text = dr["PurchaseTaxCode"].ToString();
                this.txtSUOM.Text = dr["SellingUOM"].ToString();
                this.txtQtySUOM.Value = Convert.ToDecimal(dr["QtyPerSellingUnit"].ToString());
                this.txtSTaxCode.Text = dr["SalesTaxCode"].ToString();
                this.chkAutoBuild.Checked = (bool)dr["IsAutoBuild"];

                if (!(bool)dr["isMain"])
                {
                    ParentID = int.Parse(dr["CategoryID"].ToString());
                    LoadItemParent(ParentID);
                }
                AutoBuildItemsChanged = false;
                if (this.chkAutoBuild.Checked)
                {
                    this.dgridParts.Enabled = true;
                    this.btnRemovePart.Enabled = true;
                    this.btnAddPart.Enabled = true;
                    if (dr["BundleType"].ToString() == "Static")
                        this.rdoStatic.Checked = true;
                    else if (dr["BundleType"].ToString() == "Dynamic")
                        this.rdoDynamic.Checked = true;
                    else
                        this.rdoIngredient.Checked = true;

                    this.rdoDynamic.Enabled = true;
                    this.rdoStatic.Enabled = true;
                    this.rdoIngredient.Enabled = true;

                    chkIsBought.Enabled = false;
                }
                else
                {
                    this.dgridParts.Enabled = false;
                    this.btnRemovePart.Enabled = false;
                    this.btnAddPart.Enabled = false;
                    this.rdoDynamic.Enabled = false;
                    this.rdoStatic.Enabled = false;
                    this.rdoIngredient.Enabled = false;

                }
            }
            IsLoading = false;
        }

        private void LoadItemsCostPrice(int pItemID)
        {
            IsLoading = true;

            string selectSql = @"SELECT * from ItemsCostPrice where ItemID = " + pItemID;

            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtLastCost.Value = Convert.ToDecimal(dr["LastCostEx"].ToString());
                this.txtStandardCost.Value = Convert.ToDecimal(dr["StandardCostEx"].ToString());
                this.txtAverageCost.Value = Convert.ToDecimal(dr["AverageCostEx"].ToString());
            }

            IsLoading = false;
        }

        private void LoadItemsQty(int pItemID)
        {
            IsLoading = true;
            string selectSql = @"SELECT * from ItemsQty where ItemID = " + pItemID;
            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                decimal lCommitedQty = 0;
                decimal lAvailableQty = 0;
                decimal lOnHandQty = 0;
                DataRow dr = dt.Rows[0];
                this.lblBegQty.Text = dr["BegQty"].ToString();
                this.lblOnHandQty.Text = dr["OnHandQty"].ToString();
                this.txtReOrderQty.Value = Convert.ToDecimal(dr["ReOrderQty"].ToString());
                this.txtMinQty.Value = Convert.ToDecimal(dr["MinQty"].ToString());
                txtMaxQty.Value = Convert.ToDecimal(dr["MaxQty"].ToString());
                lOnHandQty = Convert.ToDecimal(dr["OnHandQty"].ToString());
                lAvailableQty = lOnHandQty - lCommitedQty + lAvailableQty;
                this.lblAvailableQty.Text = lAvailableQty.ToString();
            }
            IsLoading = false;
        }

        private void LoadItemsSellingPrice(int pItemID)
        {
            IsLoading = true;
            string selectSql = @"SELECT s.*,ISNULL(t.TaxPercentageRate,0) as TaxRate from (((( Items as i inner join ItemsSellingPrice as s on i.ID = s.ItemID )
                    inner join ItemsCostPrice as c on i.ID = c.ItemID ) 
                    left join Profile as p on i.SupplierID = p.ID )
                    left join TaxCodes as t on i.SalesTaxCode = t.taxcode )
                    left join TaxCodes as tp on i.PurchaseTaxCode = tp.taxcode where s.ItemID = " + pItemID;

            dtPrice = new System.Data.DataTable();
            CommonClass.runSql(ref dtPrice, selectSql);
            if (dtPrice.Rows.Count > 0)
            {
                DataRow dr = dtPrice.Rows[0];
                if (dgPriceLvl.Rows.Count != 13)
                {
                    for (int i = 0; i < 13; i++)
                    {
                        dgPriceLvl.Rows.Add();
                        //  dgPriceLvl.Rows[i].Cells[7].Value = dr["TaxRate"].ToString();
                    }
                }
                dgPriceLvl.Rows[0].Cells[0].Value = "Base Selling Price";
                dgPriceLvl.Rows[1].Cells[0].Value = "Level 1 Price";
                dgPriceLvl.Rows[2].Cells[0].Value = "Level 2 Price";
                dgPriceLvl.Rows[3].Cells[0].Value = "Level 3 Price";
                dgPriceLvl.Rows[4].Cells[0].Value = "Level 4 Price";
                dgPriceLvl.Rows[5].Cells[0].Value = "Level 5 Price";
                dgPriceLvl.Rows[6].Cells[0].Value = "Level 6 Price";
                dgPriceLvl.Rows[7].Cells[0].Value = "Level 7 Price";
                dgPriceLvl.Rows[8].Cells[0].Value = "Level 8 Price";
                dgPriceLvl.Rows[9].Cells[0].Value = "Level 9 Price";
                dgPriceLvl.Rows[10].Cells[0].Value = "Level 10 Price";
                dgPriceLvl.Rows[11].Cells[0].Value = "Level 11 Price";
                dgPriceLvl.Rows[12].Cells[0].Value = "Level 12 Price";
                //Prive Value
                dgPriceLvl.Rows[0].Cells[3].Value = Convert.ToDecimal(dr["Level0"].ToString());
                dgPriceLvl.Rows[1].Cells[3].Value = Convert.ToDecimal(dr["Level1"].ToString());
                dgPriceLvl.Rows[2].Cells[3].Value = Convert.ToDecimal(dr["Level2"].ToString());
                dgPriceLvl.Rows[3].Cells[3].Value = Convert.ToDecimal(dr["Level3"].ToString());
                dgPriceLvl.Rows[4].Cells[3].Value = Convert.ToDecimal(dr["Level4"].ToString());
                dgPriceLvl.Rows[5].Cells[3].Value = Convert.ToDecimal(dr["Level5"].ToString());
                dgPriceLvl.Rows[6].Cells[3].Value = Convert.ToDecimal(dr["Level6"].ToString());
                dgPriceLvl.Rows[7].Cells[3].Value = Convert.ToDecimal(dr["Level7"].ToString());
                dgPriceLvl.Rows[8].Cells[3].Value = Convert.ToDecimal(dr["Level8"].ToString());
                dgPriceLvl.Rows[9].Cells[3].Value = Convert.ToDecimal(dr["Level9"].ToString());
                dgPriceLvl.Rows[10].Cells[3].Value = Convert.ToDecimal(dr["Level10"].ToString());
                dgPriceLvl.Rows[11].Cells[3].Value = Convert.ToDecimal(dr["Level11"].ToString());
                dgPriceLvl.Rows[12].Cells[3].Value = Convert.ToDecimal(dr["Level12"].ToString());

                dgPriceLvl.Rows[0].Cells[7].Value = (dr["Level0QtyDiscount"].ToString() == null ? "0" : dr["Level0QtyDiscount"].ToString());
                dgPriceLvl.Rows[1].Cells[7].Value = (dr["Level1QtyDiscount"].ToString() == null ? "0" : dr["Level1QtyDiscount"].ToString());
                dgPriceLvl.Rows[2].Cells[7].Value = (dr["Level2QtyDiscount"].ToString() == null ? "0" : dr["Level2QtyDiscount"].ToString());
                dgPriceLvl.Rows[3].Cells[7].Value = (dr["Level3QtyDiscount"].ToString() == null ? "0" : dr["Level3QtyDiscount"].ToString());
                dgPriceLvl.Rows[4].Cells[7].Value = (dr["Level4QtyDiscount"].ToString() == null ? "0" : dr["Level4QtyDiscount"].ToString());
                dgPriceLvl.Rows[5].Cells[7].Value = (dr["Level5QtyDiscount"].ToString() == null ? "0" : dr["Level5QtyDiscount"].ToString());
                dgPriceLvl.Rows[6].Cells[7].Value = (dr["Level6QtyDiscount"].ToString() == null ? "0" : dr["Level6QtyDiscount"].ToString());
                dgPriceLvl.Rows[7].Cells[7].Value = (dr["Level7QtyDiscount"].ToString() == null ? "0" : dr["Level7QtyDiscount"].ToString());
                dgPriceLvl.Rows[8].Cells[7].Value = (dr["Level8QtyDiscount"].ToString() == null ? "0" : dr["Level8QtyDiscount"].ToString());
                dgPriceLvl.Rows[9].Cells[7].Value = (dr["Level9QtyDiscount"].ToString() == null ? "0" : dr["Level9QtyDiscount"].ToString());
                dgPriceLvl.Rows[10].Cells[7].Value = (dr["Level10QtyDiscount"].ToString() == null ? "0" : dr["Level10QtyDiscount"].ToString());
                dgPriceLvl.Rows[11].Cells[7].Value = (dr["Level11QtyDiscount"].ToString() == null ? "0" : dr["Level11QtyDiscount"].ToString());
                dgPriceLvl.Rows[12].Cells[7].Value = (dr["Level12QtyDiscount"].ToString() == null ? "0" : dr["Level12QtyDiscount"].ToString());
                //Sale
                dgPriceLvl.Rows[0].Cells[6].Value = (dr["SalesPrice0"].ToString() == null ? "0" : dr["SalesPrice0"].ToString());
                dgPriceLvl.Rows[1].Cells[6].Value = (dr["SalesPrice1"].ToString() == null ? "0" : dr["SalesPrice1"].ToString());
                dgPriceLvl.Rows[2].Cells[6].Value = (dr["SalesPrice2"].ToString() == null ? "0" : dr["SalesPrice2"].ToString());
                dgPriceLvl.Rows[3].Cells[6].Value = (dr["SalesPrice3"].ToString() == null ? "0" : dr["SalesPrice3"].ToString());
                dgPriceLvl.Rows[4].Cells[6].Value = (dr["SalesPrice4"].ToString() == null ? "0" : dr["SalesPrice4"].ToString());
                dgPriceLvl.Rows[5].Cells[6].Value = (dr["SalesPrice5"].ToString() == null ? "0" : dr["SalesPrice5"].ToString());
                dgPriceLvl.Rows[6].Cells[6].Value = (dr["SalesPrice6"].ToString() == null ? "0" : dr["SalesPrice6"].ToString());
                dgPriceLvl.Rows[7].Cells[6].Value = (dr["SalesPrice7"].ToString() == null ? "0" : dr["SalesPrice7"].ToString());
                dgPriceLvl.Rows[8].Cells[6].Value = (dr["SalesPrice8"].ToString() == null ? "0" : dr["SalesPrice8"].ToString());
                dgPriceLvl.Rows[9].Cells[6].Value = (dr["SalesPrice9"].ToString() == null ? "0" : dr["SalesPrice9"].ToString());
                dgPriceLvl.Rows[10].Cells[6].Value = (dr["SalesPrice10"].ToString() == null ? "0" : dr["SalesPrice10"].ToString());
                dgPriceLvl.Rows[11].Cells[6].Value = (dr["SalesPrice11"].ToString() == null ? "0" : dr["SalesPrice11"].ToString());
                dgPriceLvl.Rows[12].Cells[6].Value = (dr["SalesPrice12"].ToString() == null ? "0" : dr["SalesPrice11"].ToString());

                this.dgPriceLvl.Columns[3].DefaultCellStyle.Format = "C2";
                this.dgPriceLvl.Columns[6].DefaultCellStyle.Format = "C2";
                if (dr["CalculationBasis"].ToString() == "Average Cost")
                {
                    rdoAC.Checked = true;
                }
                else
                {
                    rdoLC.Checked = true;
                }
                if (dr["RoundingMethod"].ToString() == "No Rounding")
                    chkNoRounding.Checked = true;
                else if (dr["RoundingMethod"].ToString() == "Rounding 5 cent")
                    chkRound5.Checked = true;
                else if (dr["RoundingMethod"].ToString() == "Rounding 99 cent")
                    chkRound99.Checked = true;

                if (dr["EndSalesDate"].ToString() != "")
                {
                    SaleEndDatePicker.Value = Convert.ToDateTime(dr["EndSalesDate"]);
                }
                int x = 0;
                foreach (DataGridViewRow dx in dgPriceLvl.Rows)
                {

                    System.Data.DataTable rx = new System.Data.DataTable();
                    string calcSql = "SELECT TOP 1 * FROM PriceChange WHERE PriceLevel = 'Level" + x + "' AND ItemID =" + pItemID + " ORDER BY ChangeID DESC";
                    CommonClass.runSql(ref rx, calcSql);
                    if (rx.Rows.Count > 0)
                    {
                        DataRow da = rx.Rows[0];
                        dx.Cells["RegCalcMethod"].Value = da["CalcMethod"].ToString();
                        dx.Cells["RegPerc"].Value = da["PercentChange"].ToString();
                    }
                    System.Data.DataTable ry = new System.Data.DataTable();
                    calcSql = "SELECT TOP 1 * FROM PriceChange WHERE PriceLevel = 'SalesPrice" + x + "' AND ItemID =" + pItemID + " ORDER BY ChangeID DESC";
                    CommonClass.runSql(ref ry, calcSql);
                    if (ry.Rows.Count > 0)
                    {
                        DataRow dg = ry.Rows[0];
                        dx.Cells["SaleCalcMethod"].Value = dg["CalcMethod"].ToString();
                        dx.Cells["SalePerc"].Value = dg["PercentChange"].ToString();
                    }
                    if (dx.Cells["RegCalcMethod"].Value != null)
                    {
                        if (dx.Cells["RegCalcMethod"].Value.ToString() == "Fixed Price")
                        {
                            dgPriceLvl.Rows[x].Cells[3].ReadOnly = false;
                        }
                        else
                        {
                            dgPriceLvl.Rows[x].Cells[3].ReadOnly = true;
                        }
                    }
                    if (dx.Cells["SaleCalcMethod"].Value != null)
                    {
                        if (dx.Cells["SaleCalcMethod"].Value.ToString() == "Fixed Price")
                        {
                            dgPriceLvl.Rows[x].Cells[6].ReadOnly = false;
                        }
                        else
                        {
                            dgPriceLvl.Rows[x].Cells[6].ReadOnly = true;
                        }
                    }
                    x++;
                }
                // this.dgPriceLvl.Columns[2].DefaultCellStyle.Format = "#.000%";
                //this.dgPriceLvl.Columns[7].DefaultCellStyle.Format = "C2";
                //this.dgPriceLvl.Columns[8].DefaultCellStyle.Format = "C2";
            }
            IsLoading = false;
        }

        private void LoadItemsAutoBuild(int pItemID)
        {
            IsLoading = true;
            this.dgridParts.Rows.Clear();
            string selectSql = @"SELECT a.*, i.PartNumber, i.ItemName , s.Level0
from ItemsAutoBuild as a 
inner join Items as i on a.PartItemID = i.ID 
inner join ItemsSellingPrice as s on s.ItemID = i.ID 
where a.ItemID = " + pItemID;
            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rw in dt.Rows)
                {
                    string[] toAddRow = new string[6];
                    toAddRow[0] = rw["PartItemID"].ToString();
                    toAddRow[1] = rw["PartNumber"].ToString();
                    toAddRow[2] = rw["ItemName"].ToString();
                    toAddRow[3] = rw["PartItemQty"].ToString();
                    toAddRow[4] = (decimal.Parse(rw["Level0"].ToString()) * decimal.Parse(rw["PartItemQty"].ToString())).ToString();
                    toAddRow[5] = rw["Level0"].ToString();
                    this.dgridParts.Rows.Add(toAddRow);
                }
            }

            IsLoading = false;
        }
        private void LoadItemParent(int pItemID)
        {
            IsLoading = true;
            string selectSql = @"SELECT CategoryID , CategoryCode FROM Category WHERE CategoryID = " + pItemID;

            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rw in dt.Rows)
                {
                    txtCategory.Text = rw["CategoryCode"].ToString();
                }
            }
            IsLoading = false;
        }

        //private string[] GetAccountIDInfo(string pAccountID)
        //{
        //    IsLoading = true;
        //    string[] retval = null;


        //        string selectSql = @"Select * from Accounts where AccountID = " + pAccountID;

        //        DataTable dt = new DataTable();
        //        CommonClass.runSql(ref dt, selectSql);


        //        if (dt.Rows.Count > 0)
        //        {
        //            retval = new string[3];
        //            retval[0] = dt.Rows[0]["AccountID"].ToString();
        //            retval[1] = dt.Rows[0]["AccountNumber"].ToString();
        //            retval[2] = dt.Rows[0]["AccountName"].ToString();


        //        }
        //        return retval;
        //}


        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (txtPartNumber.Text == ""
                || txtItemNumber.Text == ""
                || txtItemName.Text == "")
            {

                MessageBox.Show("Mandatory fields are blank");
                return;
            }

            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'ItemDescription'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rx = ItemMainChanges.NewRow();
                rx["colname"] = "ItemDescription";
                rx["colvalue"] = HTMLEditor.DocumentText;
                ItemMainChanges.Rows.Add(rx);
                UpdateItem();
                txtItemNameLbl.Text = txtItemName.Text;
                txtPartNumberLabel.Text = txtPartNumber.Text;
            }
            else
            {
                CreateNewItem();
                SaveMode = 1;
            }
        }

        private void pbPTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                this.txtPTaxCode.Text = Tax[0];

            }
        }

        private void pbSTaxCode_Click(object sender, EventArgs e)
        {
            TaxCodeLookup DlgTaxCode = new TaxCodeLookup("");
            if (DlgTaxCode.ShowDialog() == DialogResult.OK)
            {
                string[] Tax = DlgTaxCode.GetTax;
                this.txtSTaxCode.Text = Tax[0];

            }
        }

        private void pbSupplier_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                this.lblSupplierID.Text = lProfile[0];
                this.txtSupplier.Text = lProfile[2];


            }
        }

        private void pbList1_Click(object sender, EventArgs e)
        {
            List1Lookup L1Dlg = new List1Lookup("Items");
            string[] l1 = new string[2];

            if (L1Dlg.ShowDialog() == DialogResult.OK)
            {
                l1 = L1Dlg.GetList1;

                this.txtList1.Text = l1[0];
                this.lblList1ID.Text = l1[1];
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CList1'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CList1";
                rw["colvalue"] = this.lblList1ID.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void pbList2_Click(object sender, EventArgs e)
        {
            List2Lookup L2Dlg = new List2Lookup("Items");
            string[] l2 = new string[2];

            if (L2Dlg.ShowDialog() == DialogResult.OK)
            {
                l2 = L2Dlg.GetList2;

                this.txtList2.Text = l2[0];
                this.lblList2ID.Text = l2[1];
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CList2'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CList2";
                rw["colvalue"] = this.lblList2ID.Text;
                ItemMainChanges.Rows.Add(rw);

            }
        }

        private void pbList3_Click(object sender, EventArgs e)
        {
            List3Lookup L3Dlg = new List3Lookup("Items");
            string[] l3 = new string[2];

            if (L3Dlg.ShowDialog() == DialogResult.OK)
            {
                l3 = L3Dlg.GetList3;

                this.txtList3.Text = l3[0];
                this.lblList3ID.Text = l3[1];
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CList3'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CList3";
                rw["colvalue"] = this.lblList3ID.Text;
                ItemMainChanges.Rows.Add(rw);

            }
        }


        private void txtPartNumber_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'PartNumber'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "PartNumber";
                rw["colvalue"] = this.txtPartNumber.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtItemNumber_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'ItemNumber'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "ItemNumber";
                rw["colvalue"] = this.txtItemNumber.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtSupplierPartNo_TextChanged(object sender, EventArgs e)
        {

            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'SupplierItemNumber'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "SupplierItemNumber";
                rw["colvalue"] = this.txtSupplierPartNo.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'ItemName'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "ItemName";
                rw["colvalue"] = this.txtItemName.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtCostAcct_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'COSAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "COSAccountID";
                rw["colvalue"] = this.txtCostAcct.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtIncomeAcct_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'IncomeAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "IncomeAccountID";
                rw["colvalue"] = this.txtIncomeAcct.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtAssetAcct_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'AssetAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "AssetAccountID";
                rw["colvalue"] = this.txtAssetAcct.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtItemDescription_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'ItemDescription'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "ItemDescription";
                rw["colvalue"] = HTMLEditor.DocumentText;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtList1_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CList1'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CList1";
                rw["colvalue"] = this.lblList1ID.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtList2_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CList2'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CList2";
                rw["colvalue"] = this.lblList2ID.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtList3_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CList3'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CList3";
                rw["colvalue"] = this.lblList3ID.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtField1_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CField1'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CField1";
                rw["colvalue"] = this.txtField1.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtField2_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CField2'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CField2";
                rw["colvalue"] = this.txtField2.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtField3_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'CField3'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "CField3";
                rw["colvalue"] = this.txtField3.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtBUOM_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'BuyingUOM'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "BuyingUOM";
                rw["colvalue"] = this.txtBUOM.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtQtyBUOM_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'QtyPerBuyingUOM'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "QtyPerBuyingUOM";
                rw["colvalue"] = this.txtQtyBUOM.Value.ToString();
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtPTaxCode_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'PurchaseTaxCode'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "PurchaseTaxCode";
                rw["colvalue"] = this.txtPTaxCode.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'SupplierID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "SupplierID";
                rw["colvalue"] = this.lblSupplierID.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtSUOM_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'SellingUOM'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "SellingUOM";
                rw["colvalue"] = this.txtSUOM.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtQtySUOM_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'QtyPerSellingUnit'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "QtyPerSellingUnit";
                rw["colvalue"] = this.txtQtySUOM.Value;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void txtSTaxCode_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'SalesTaxCode'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "SalesTaxCode";
                rw["colvalue"] = this.txtSTaxCode.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void chkAutoBuild_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'IsAutoBuild'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "IsAutoBuild";
                rw["colvalue"] = this.chkAutoBuild.Checked;
                ItemMainChanges.Rows.Add(rw);

                if (chkAutoBuild.Checked)
                {
                    rw = ItemMainChanges.NewRow();
                    rw["colname"] = "BundleType";
                    if (rdoStatic.Checked)
                        rw["colvalue"] = this.rdoStatic.Text;
                    else if (rdoDynamic.Checked)
                        rw["colvalue"] = this.rdoDynamic.Text;
                    else
                        rw["colvalue"] = this.rdoIngredient.Text;

                    ItemMainChanges.Rows.Add(rw);
                    chkIsBought.Checked = false;
                }
                else
                {
                    rw = ItemMainChanges.NewRow();
                    rw["colname"] = "BundleType";
                    rw["colvalue"] = "";
                    ItemMainChanges.Rows.Add(rw);
                }

            }
            if (this.chkAutoBuild.Checked)
            {
                this.dgridParts.Enabled = true;
                this.btnRemovePart.Enabled = true;
                this.btnAddPart.Enabled = true;
                this.rdoDynamic.Enabled = true;
                this.rdoStatic.Enabled = true;
                this.rdoIngredient.Enabled = true;

                chkIsBought.Checked = false;
                chkIsBought.Enabled = false;

            }
            else
            {
                this.dgridParts.Enabled = false;
                this.btnRemovePart.Enabled = false;
                this.btnAddPart.Enabled = false;
                this.rdoDynamic.Enabled = false;
                this.rdoStatic.Enabled = false;
                this.rdoIngredient.Enabled = false;

                chkIsBought.Checked = true;
                chkIsBought.Enabled = true;
            }
        }

        private void txtMinQty_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemQtyChanges.Select("colname = 'MinQty'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemQtyChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemQtyChanges.NewRow();
                rw["colname"] = "MinQty";
                rw["colvalue"] = this.txtMinQty.Value;
                ItemQtyChanges.Rows.Add(rw);
            }
        }

        private void txtReOrderQty_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemQtyChanges.Select("colname = 'ReOrderQty'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemQtyChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemQtyChanges.NewRow();
                rw["colname"] = "ReOrderQty";
                rw["colvalue"] = this.txtReOrderQty.Value;
                ItemQtyChanges.Rows.Add(rw);
            }
        }

        private void txtStandardCost_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemCostChanges.Select("colname = 'StandardCostEx'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemCostChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemCostChanges.NewRow();
                rw["colname"] = "StandardCostEx";
                rw["colvalue"] = this.txtStandardCost.Value;
                ItemCostChanges.Rows.Add(rw);
                if (NoTran)
                {
                    this.txtAverageCost.Value = this.txtStandardCost.Value;

                }
            }
            else
            {
                this.txtAverageCost.Value = this.txtStandardCost.Value;
            }

        }

        private void txtAverageCost_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemCostChanges.Select("colname = 'AverageCostEx'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemCostChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemCostChanges.NewRow();
                rw["colname"] = "AverageCostEx";
                rw["colvalue"] = this.txtAverageCost.Value;
                ItemCostChanges.Rows.Add(rw);

            }

        }
        private void UpdateItem()
        {
            int x = 0;
            if (ItemMainChanges.Rows.Count > 0)
            {
                x = UpdateItemMain(ItemID.ToString());
            }
            if (ItemCostChanges.Rows.Count > 0)
            {
                x = UpdateItemCostPrice(ItemID.ToString());
            }
            if (ItemQtyChanges.Rows.Count > 0)
            {
                x = UpdateItemQty(ItemID.ToString());
            }
            if (ItemPriceChanges.Rows.Count > 0)
            {
                x = UpdateItemSellingPrices(ItemID.ToString());
            }
            if (AutoBuildItemsChanged)
            {
                x = UpdateItemAutoBuildParts(ItemID.ToString());
            }
            if (imgupdate)
            {
                x = SaveImage(ItemID);
            }
            if (x != 0)
            {
                MessageBox.Show("Updated Successfully", "Item Information");
                ItemOldPriceChanges.Rows.Clear();
                ItemPriceChanges.Rows.Clear();
            }
        }

        private int UpdateItemMain(string pItemID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string setStr = "";
            foreach (DataRow rw in ItemMainChanges.Rows)
            {
                if (setStr == "")
                {
                    setStr = rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
                else
                {
                    setStr += "," + rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
            }


            string sql = @"UPDATE Items SET " + setStr + " where ID = " + pItemID;

            string StrAudit = "";
            foreach (DataRow rw in ItemMainChanges.Rows)
            {
                param.Add("@" + rw["colname"].ToString(), rw["colvalue"]);
                StrAudit += (StrAudit != "" ? "," : "") + rw["colname"].ToString() + " changed to " + rw["colvalue"].ToString();
            }


            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            if (res > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Modified Item Details for Item ID " + pItemID, pItemID.ToString(), "", StrAudit);

            }
            return res;

        }
        private int UpdateItemCostPrice(string pItemID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string setStr = "";
            foreach (DataRow rw in ItemCostChanges.Rows)
            {
                if (setStr == "")
                {
                    setStr = rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
                else
                {
                    setStr += "," + rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
            }

            string sql = @"UPDATE ItemsCostPrice SET " + setStr + " where ItemID = " + pItemID;
            string StrAudit = "";
            foreach (DataRow rw in ItemCostChanges.Rows)
            {
                param.Add("@" + rw["colname"].ToString(), rw["colvalue"]);
                StrAudit += (StrAudit != "" ? "," : "") + rw["colname"].ToString() + " changed to " + rw["colvalue"].ToString();
            }

            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            if (res > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Modified Item Cost Price for " + pItemID, pItemID.ToString(), "", StrAudit);

            }
            return res;

        }
        private int UpdateItemQty(string pItemID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string setStr = "";
            foreach (DataRow rw in ItemQtyChanges.Rows)
            {
                if (setStr == "")
                {
                    setStr = rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
                else
                {
                    setStr += "," + rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
            }


            string sql = @"UPDATE ItemsQty SET " + setStr + " where ItemID = " + pItemID;

            string StrAudit = "";

            foreach (DataRow rw in ItemQtyChanges.Rows)
            {
                param.Add("@" + rw["colname"].ToString(), rw["colvalue"]);
                StrAudit += (StrAudit != "" ? "," : "") + rw["colname"].ToString() + " changed to " + rw["colvalue"].ToString();
            }

            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            if (res > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Modified Item " + pItemID, pItemID.ToString(), "", StrAudit);

            }
            return res;
        }
        private int UpdateItemSellingPrices(string pItemID)
        {
            string RoundingMethod = "";
            Dictionary<string, object> param = new Dictionary<string, object>();
            string setStr = "";
            if (rdoAC.Checked)
            {
                param.Add("@CalculationBasis", "Average Cost");
                param.Add("@CostBasis", txtAverageCost.Value);
            }
            else
            {
                param.Add("@CalculationBasis", "Last Cost");
                param.Add("@CostBasis", txtLastCost.Value);
            }
            if (chkNoRounding.Checked)
                RoundingMethod = "No Rounding";
            else if (chkRound5.Checked)
                RoundingMethod = "Rounding 5 cent";
            else if (chkRound99.Checked)
                RoundingMethod = "Rounding 99 cent";

            param.Add("@RoundingMethod", RoundingMethod);

            foreach (DataRow rw in ItemPriceChanges.Rows)
            {
                if (setStr == "")
                {
                    setStr = rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
                else
                {
                    setStr += "," + rw["colname"].ToString() + " = @" + rw["colname"].ToString();
                }
            }
            string sql = @"UPDATE ItemsSellingPrice SET CalculationBasis = @CalculationBasis, CostBasis = @CostBasis, RoundingMethod = @RoundingMethod," + setStr + " where ItemID = " + pItemID;

            string StrAudit = "";

            foreach (DataRow rw in ItemPriceChanges.Rows)
            {
                param.Add("@" + rw["colname"].ToString(), rw["colvalue"]);
                StrAudit += (StrAudit != "" ? "," : "") + rw["colname"].ToString() + " changed to " + rw["colvalue"].ToString();
            }

            int res = CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
            if (res > 0)
            {

                if (ItemPriceChanges.Rows.Count > 0)
                {
                    if (ItemOldPriceChanges.Rows.Count > 0)
                    {
                        foreach (DataRow rw in ItemPriceChanges.Rows)
                        {
                            Dictionary<string, object> priceupdateParam = new Dictionary<string, object>();
                            priceupdateParam.Add("@PriceLevel", rw["colname"].ToString());
                            priceupdateParam.Add("@PriceAfter", rw["colvalue"].ToString());

                            priceupdateParam.Add("@ItemID", ItemID);
                            priceupdateParam.Add("@ChangeDate", DateTime.Now.ToUniversalTime());
                            priceupdateParam.Add("@UserID", CommonClass.UserID);
                            priceupdateParam.Add("@CalcMethod", rw["CalcMethod"].ToString());
                            priceupdateParam.Add("@PercentChange", rw["PercentChange"].ToString());

                            foreach (DataRow dx in ItemOldPriceChanges.Rows)
                            {
                                if (dx["colname"].ToString() == rw["colname"].ToString())
                                {
                                    priceupdateParam.Add("@PriceBefore", dx["colvalue"].ToString());
                                }
                            }

                            string priceupdate = "INSERT INTO PriceChange (ItemID,PriceBefore,PriceAfter,ChangeDate,UserID,PriceLevel,CalcMethod,PercentChange )VALUES(@ItemID,@PriceBefore,@PriceAfter,@ChangeDate,@UserID,@PriceLevel,@CalcMethod,@PercentChange )";
                            CommonClass.runSql(priceupdate, CommonClass.RunSqlInsertMode.QUERY, priceupdateParam);
                        }
                    }
                }
            }
            if (res > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Modified Item Selling Price for Item ID " + pItemID, pItemID.ToString(), "", StrAudit);
            }
            return res;
        }

        private int UpdateItemAutoBuildParts(string pItemID)
        {
            string sql = "";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ItemID", pItemID);

            //DElETE OLD ITEMS FIRST
            sql = "DELETE FROM ItemsAutoBuild WHERE ItemID = @ItemID";
            CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);

            int ctr = 0;
            foreach (DataGridViewRow rw in this.dgridParts.Rows)
            {
                string lID = rw.Cells["PartID"].Value.ToString();
                string lQty = rw.Cells["Qty"].Value.ToString();
                sql = @"INSERT INTO ItemsAutoBuild(ItemID,PartItemID,PartItemQty) 
                        VALUES(" + pItemID + "," + lID + "," + lQty + ")";

                CommonClass.runSql(sql);
                ctr++;
            }
            if (ctr > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "AutoBuild Part Items modified for Item ID" + pItemID, pItemID.ToString());
            }
            return ctr;
        }

        private void lblCOSAccountID_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'COSAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "COSAccountID";
                rw["colvalue"] = this.txtCostAcct.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void lblIncomeAccountID_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'IncomeAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "IncomeAccountID";
                rw["colvalue"] = this.txtIncomeAcct.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void lblAssetAccountID_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'AssetAccountID'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "AssetAccountID";
                rw["colvalue"] = this.txtAssetAcct.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            ShowItemLookup("", "PartNumber");
        }

        public void ShowItemLookup(string itemNum, string whereCon)
        {
            ItemLookup Items = new ItemLookup(ItemLookupSource.SELF, itemNum, "", whereCon);

            if (Items.ShowDialog() == DialogResult.OK)
            {
                DataGridViewRow dgRow;

                dgRow = Items.GetSelectedItem;
                string[] toAddRow = new string[6];
                toAddRow[0] = dgRow.Cells["ItemID"].Value.ToString();
                toAddRow[1] = dgRow.Cells["PartNo"].Value.ToString();
                toAddRow[2] = dgRow.Cells["ItemName"].Value.ToString();
                toAddRow[3] = "1";
                toAddRow[4] = (decimal.Parse(dgRow.Cells["SellingPrice"].Value.ToString()) * 1).ToString();
                toAddRow[5] = dgRow.Cells["SellingPrice"].Value.ToString(); ;
                this.dgridParts.Rows.Add(toAddRow);
                AutoBuildItemsChanged = true;
                //if (rdoStatic.Checked)
                //{
                //    string sql = @"UPDATE ItemsQty SET OnHandQty = ((SELECT OnHandQty FROM ItemsQty WHERE ItemID=@ItemID) - 1)
                //               WHERE ItemID=@ItemID";
                //    Dictionary<string, object> param = new Dictionary<string, object>();
                //    param.Add("@ItemID", dgRow.Cells["ItemID"].Value);
                //    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                //}
            }
        }

        private void dgridParts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (IsLoading)
                return;

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)
                    dgridParts.Rows[0].Cells["Amount"].Value = decimal.Parse(dgridParts.Rows[e.RowIndex].Cells["Qty"].Value.ToString()) * decimal.Parse(dgridParts.Rows[e.RowIndex].Cells["SellingPrice"].Value.ToString());

            }

            AutoBuildItemsChanged = true;
        }

        private void btnRemovePart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgridParts.SelectedRows)
            {
                if (rdoStatic.Checked)
                {
                    string sql = @"UPDATE ItemsQty SET OnHandQty = ((SELECT OnHandQty FROM ItemsQty WHERE ItemID=@ItemID) + 1)
                               WHERE ItemID=@ItemID";
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@ItemID", dgvr.Cells["PartID"].Value);
                    CommonClass.runSql(sql, CommonClass.RunSqlInsertMode.QUERY, param);
                }
                dgridParts.Rows.RemoveAt(dgvr.Index);
            }
            AutoBuildItemsChanged = true;
            dgridParts.Refresh();
        }

        private void cbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'IsInactive'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
            }
            if (SaveMode > 0)
            {
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "IsInactive";
                rw["colvalue"] = cbInactive.Checked;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int lItemId = ItemID;
            string lPartNo = this.txtPartNumber.Text;
            DialogResult yesno = MessageBox.Show("Are you sure you want to delete this item", "Confirm Delete", MessageBoxButtons.YesNo);
            if (yesno == DialogResult.Yes)
            {
                string deletesql = @"DELETE FROM Items WHERE ID = @ItemID;
                                 DELETE FROM ItemsAutoBuild WHERE ItemID = @ItemID;
                                 DELETE FROM ItemsCostPrice WHERE ItemID = @ItemID;
                                 DELETE FROM ItemsQty WHERE ItemID = @ItemID;
                                 DELETE FROM ItemsSellingPrice WHERE ItemID = @ItemID;
                                 DELETE FROM ItemTransaction WHERE ItemID = @ItemID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ItemID", lItemId);
                int rowsaffected = CommonClass.runSql(deletesql, CommonClass.RunSqlInsertMode.QUERY, param);
                if (rowsaffected > 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, "Items", "Deleted Item with ItemID " + lItemId + " and Part Number " + lPartNo, lItemId.ToString(), "");
                    MessageBox.Show("Item deleted successfully");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openPic = new OpenFileDialog() { Filter = "Image |*.jpg; *.png; *.jpeg;", ValidateNames = true, Multiselect = false })
            {
                if (openPic.ShowDialog() == DialogResult.OK)
                {
                    filename = openPic.FileName;
                    pictureBox1.Image = Image.FromFile(filename);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    File_extension = Path.GetExtension(filename);
                    imgupdate = true;
                }
            }
        }

        private void dgPriceLvl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            string lCol = (rdoAC.Checked == true ? txtAverageCost.Value.ToString() : txtLastCost.Value.ToString());
            if (e.ColumnIndex == 2)
            {
                if (dgPriceLvl.CurrentRow.Cells[2].Value != null)//Regular Price
                {
                    if (dgPriceLvl.Rows[e.RowIndex].Cells[1].Value.ToString() == "Percent Margin")
                    {
                        CalculatePercentMargin(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[2].Value.ToString()), float.Parse(lCol), 3, e.RowIndex);
                        changesPrice(3, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[3].ReadOnly = true;
                    }
                    else if (dgPriceLvl.CurrentRow.Cells[1].Value.ToString() == "Percent Markup")

                    {
                        CalculatePercentMarkup(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[2].Value.ToString()), float.Parse(lCol), 3, e.RowIndex);
                        changesPrice(3, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[3].ReadOnly = true;

                    }
                    else if (dgPriceLvl.CurrentRow.Cells[1].Value.ToString() == "Fixed Gross")
                    {
                        CalculateFixedGP(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[2].Value.ToString()), float.Parse(lCol), 3, e.RowIndex);
                        changesPrice(3, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[3].ReadOnly = true;

                    }
                    else if (dgPriceLvl.CurrentRow.Cells[1].Value.ToString() == "Discount from Base")
                    {
                        CalculateDiscountFromBase(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[2].Value.ToString()), float.Parse(dgPriceLvl.Rows[0].Cells[3].Value.ToString()), 3, e.RowIndex);
                        changesPrice(3, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[3].ReadOnly = true;

                    }


                }
            }
            if (e.ColumnIndex == 5)
            {
                if (dgPriceLvl.CurrentRow.Cells[5].Value != null)//Sale price
                {
                    if (dgPriceLvl.Rows[e.RowIndex].Cells[4].Value.ToString() == "Percent Margin")
                    {
                        CalculatePercentMargin(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[5].Value.ToString()), float.Parse(lCol), 6, e.RowIndex);
                        changesPrice(6, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[6].ReadOnly = true;


                    }
                    else if (dgPriceLvl.CurrentRow.Cells[4].Value.ToString() == "Percent Markup")

                    {
                        CalculatePercentMarkup(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[5].Value.ToString()), float.Parse(lCol), 6, e.RowIndex);
                        changesPrice(6, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[6].ReadOnly = true;

                    }
                    else if (dgPriceLvl.CurrentRow.Cells[4].Value.ToString() == "Fixed Gross")
                    {
                        CalculateFixedGP(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[5].Value.ToString()), float.Parse(lCol), 6, e.RowIndex);
                        changesPrice(6, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[6].ReadOnly = true;

                    }
                    else if (dgPriceLvl.CurrentRow.Cells[4].Value.ToString() == "Discount from Base")
                    {
                        CalculateDiscountFromBase(float.Parse(dgPriceLvl.Rows[e.RowIndex].Cells[5].Value.ToString()), float.Parse(dgPriceLvl.Rows[0].Cells[6].Value.ToString()), 6, e.RowIndex);
                        changesPrice(6, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[6].ReadOnly = true;

                    }

                }
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 4)
            {
                if (dgPriceLvl.CurrentRow.Cells[1].Value != null)
                {
                    if (dgPriceLvl.CurrentRow.Cells[1].Value.ToString() == "Fixed Price")
                    {
                        this.dgPriceLvl.CurrentRow.Cells[2].Value = "0";
                        this.dgPriceLvl.CurrentRow.Cells[3].Value = this.dgPriceLvl.Rows[0].Cells[3].Value;
                        changesPrice(3, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[3].ReadOnly = false;

                    }
                }
                if (dgPriceLvl.CurrentRow.Cells[4].Value != null)//Sale price
                {
                    if (dgPriceLvl.CurrentRow.Cells[4].Value.ToString() == "Fixed Price")
                    {
                        this.dgPriceLvl.CurrentRow.Cells[5].Value = "0";
                        this.dgPriceLvl.CurrentRow.Cells[6].Value = this.dgPriceLvl.Rows[0].Cells[6].Value;
                        changesPrice(6, e.RowIndex);
                        dgPriceLvl.CurrentRow.Cells[6].ReadOnly = false;

                    }
                }
            }

            if (e.ColumnIndex == 3)
            {
                changesPrice(3, e.RowIndex);
            }
            if (e.ColumnIndex == 6)
            {
                changesPrice(6, e.RowIndex);
            }
            if (e.ColumnIndex == 7)
            {
                if (dgPriceLvl.CurrentRow.Cells[7].Value.ToString() != "" && dgPriceLvl.CurrentRow.Cells[7].Value.ToString() != "0")
                {
                    DataRow[] Qr = ItemPriceChanges.Select("colname = 'Level" + e.RowIndex + "QtyDiscount'");
                    if (Qr.Length > 0)
                    {
                        foreach (DataRow r in Qr)
                        {
                            ItemPriceChanges.Rows.Remove(r);
                        }
                    }
                    DataRow rw = ItemPriceChanges.NewRow();
                    rw["colname"] = "Level" + e.RowIndex + "QtyDiscount";
                    rw["colvalue"] = dgPriceLvl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    ItemPriceChanges.Rows.Add(rw);
                }
            }

        }
        void changesPrice(int colindex, int rowindex)
        {
            if (colindex == 3)
            {
                dgPriceLvl.Rows[rowindex].Cells[colindex].Value = (dgPriceLvl.Rows[rowindex].Cells[colindex].Value == null ? "" : dgPriceLvl.Rows[rowindex].Cells[colindex].Value);
                if (dgPriceLvl.Rows[rowindex].Cells[colindex].Value.ToString() != "" || dgPriceLvl.Rows[rowindex].Cells[colindex].Value.ToString() != "0")
                {
                    DataRow[] Qr = ItemPriceChanges.Select("colname = 'Level" + rowindex + "'");
                    if (Qr.Length > 0)
                    {
                        foreach (DataRow r in Qr)
                        {
                            ItemPriceChanges.Rows.Remove(r);
                        }
                    }
                    DataRow rw = ItemPriceChanges.NewRow();
                    rw["colname"] = "Level" + rowindex + "";
                    rw["colvalue"] = dgPriceLvl.Rows[rowindex].Cells[colindex].Value.ToString();
                    rw["CalcMethod"] = dgPriceLvl.Rows[rowindex].Cells[1].Value.ToString();
                    rw["PercentChange"] = dgPriceLvl.Rows[rowindex].Cells[2].Value.ToString();
                    ItemPriceChanges.Rows.Add(rw);

                    DataRow[] Pr = ItemOldPriceChanges.Select("colname = 'Level" + rowindex + "'");//OldPrice to datatable
                    if (Pr.Length > 0)
                    {
                        foreach (DataRow r in Pr)
                        {
                            ItemOldPriceChanges.Rows.Remove(r);
                        }
                    }
                    if (dtPrice.Rows.Count > 0)
                    {
                        DataRow dr = dtPrice.Rows[0];
                        DataRow Orw = ItemOldPriceChanges.NewRow();
                        Orw["colname"] = "Level" + rowindex + "";
                        Orw["colvalue"] = dr["Level" + rowindex + ""].ToString();

                        ItemOldPriceChanges.Rows.Add(Orw);
                    }
                    else
                    {//New ITem
                        DataRow Orw = ItemOldPriceChanges.NewRow();
                        Orw["colname"] = "Level" + rowindex + "";
                        Orw["colvalue"] = 0;
                        ItemOldPriceChanges.Rows.Add(Orw);
                    }
                }
            }
            if (colindex == 6)
            {
                if (dgPriceLvl.Rows[rowindex].Cells[colindex].Value.ToString() != "" && dgPriceLvl.Rows[rowindex].Cells[colindex].Value.ToString() != "0")
                {
                    DataRow[] Qr = ItemPriceChanges.Select("colname = 'SalesPrice" + rowindex + "'");
                    if (Qr.Length > 0)
                    {
                        foreach (DataRow r in Qr)
                        {
                            ItemPriceChanges.Rows.Remove(r);
                        }
                    }
                    DataRow rw = ItemPriceChanges.NewRow();
                    rw["colname"] = "SalesPrice" + rowindex + "";
                    rw["colvalue"] = dgPriceLvl.Rows[rowindex].Cells[colindex].Value.ToString();
                    rw["CalcMethod"] = dgPriceLvl.Rows[rowindex].Cells[4].Value.ToString();
                    rw["PercentChange"] = dgPriceLvl.Rows[rowindex].Cells[5].Value.ToString();
                    ItemPriceChanges.Rows.Add(rw);
                    DataRow[] Pr = ItemOldPriceChanges.Select("colname = 'SalesPrice" + rowindex + "'");//OldPrice to datatable
                    if (Pr.Length > 0)
                    {
                        foreach (DataRow r in Pr)
                        {
                            ItemOldPriceChanges.Rows.Remove(r);
                        }
                    }
                    if (dtPrice.Rows.Count > 0)
                    {
                        DataRow dr = dtPrice.Rows[0];
                        DataRow Orw = ItemOldPriceChanges.NewRow();
                        Orw["colname"] = "SalesPrice" + rowindex + "";
                        Orw["colvalue"] = dr["SalesPrice" + rowindex + ""].ToString();
                        ItemOldPriceChanges.Rows.Add(Orw);
                    }
                    else
                    {//New ITem
                        DataRow Orw = ItemOldPriceChanges.NewRow();
                        Orw["colname"] = "SalesPrice" + rowindex + "";
                        Orw["colvalue"] = 0;
                        ItemOldPriceChanges.Rows.Add(Orw); ;
                    }
                }
            }
            if (colindex == 7)
            {

            }
        }
        private void CalculatePercentMargin(float pRate, float cost, int pCol, int index)
        {
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = (pRate != 0 ? pRate / 100 : 0);
            lCost = cost;

            DataRow rTx = CommonClass.getTaxDetails(txtSTaxCode.Text == "" ? "N-T" : txtSTaxCode.Text);
            if (rTx.ItemArray.Length > 0)
            {
                lTaxRate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
            }
            //  lTaxRate = float.Parse(this.dgPriceLvl.Rows[index].Cells["TaxRate"].Value.ToString());
            lPrice = lCost / (1 - lMRate);

            if (CommonClass.IsItemPriceInclusive)
            {
                lPrice = lPrice * (1 + (lTaxRate / 100));
            }
            lPrice = (float)ProcessRounding(lPrice, RoundingMode);
            this.dgPriceLvl.Rows[index].Cells[pCol].Value = Math.Round(lPrice, 2);
        }
        private void CalculatePercentMarkup(float pRate, float cost, int pCol, int index)
        {
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = (pRate != 0 ? pRate / 100 : 0);
            lCost = cost;
            DataRow rTx = CommonClass.getTaxDetails(txtSTaxCode.Text == "" ? "N-T" : txtSTaxCode.Text);
            if (rTx.ItemArray.Length > 0)
            {
                lTaxRate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
            }
            lPrice = lCost * (1 + lMRate);

            if (CommonClass.IsItemPriceInclusive)
            {
                lPrice = lPrice * (1 + (lTaxRate / 100));
            }
            lPrice = (float)ProcessRounding(lPrice, RoundingMode);
            this.dgPriceLvl.Rows[index].Cells[pCol].Value = lPrice;
        }
        private void CalculateFixedGP(float pRate, float cost, int pCol, int index)
        {
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = pRate;

            lCost = cost;
            DataRow rTx = CommonClass.getTaxDetails(txtSTaxCode.Text == "" ? "N-T" : txtSTaxCode.Text);
            if (rTx.ItemArray.Length > 0)
            {
                lTaxRate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
            }
            lPrice = lCost + lMRate;

            if (CommonClass.IsItemPriceInclusive)
            {
                lPrice = lPrice * (1 + (lTaxRate / 100));
            }
            this.dgPriceLvl.Rows[index].Cells[pCol].Value = lPrice;
        }
        private void CalculateDiscountFromBase(float pRate, float cost, int pCol, int index)
        {
            float lCost = 0;
            float lTaxRate = 0;
            float lPrice = 0;
            float lMRate = pRate;

            lCost = cost;
            DataRow rTx = CommonClass.getTaxDetails(txtSTaxCode.Text == "" ? "N-T" : txtSTaxCode.Text);
            //if (rTx.ItemArray.Length > 0)
            //{
            //    lTaxRate = float.Parse(rTx["TaxPercentageRate"].ToString() == "" ? "0" : rTx["TaxPercentageRate"].ToString()); ;
            //}
            lPrice = lCost - (lCost * lMRate / 100);

            //if (CommonClass.IsItemPriceInclusive)
            //{
            //    lPrice = lPrice * (1 + (lTaxRate / 100));
            //}
            lPrice = (float)ProcessRounding(lPrice, RoundingMode);
            this.dgPriceLvl.Rows[index].Cells[pCol].Value = lPrice;
            this.dgPriceLvl.Rows[index].Cells[pCol].Value = lPrice;
        }

        private double ProcessRounding(float lPrice, int pRoundMode)
        {
            double lPriceRounded = lPrice;
            double lPriceFloor = 0;
            double lTemp = 0;
            double lTemp1 = 0;
            double lTemp2 = 0;

            if (pRoundMode == 1) //ROUND TO NEAREST five cents
            {
                lPriceFloor = Math.Floor(lPrice);
                lTemp = (lPrice - lPriceFloor) * 10;
                lTemp1 = Math.Floor(lTemp);
                lTemp2 = Math.Floor((lTemp - lTemp1) * 10) / 100;
                lTemp1 = lTemp1 / 10;

                if (lTemp2 > 0 && lTemp2 < 0.03)
                {
                    lTemp2 = 0;
                }
                if (lTemp2 >= 0.03 && lTemp2 < 0.08)
                {
                    lTemp2 = 0.05;
                }
                if (lTemp2 >= 0.08 && lTemp2 <= 0.099)
                {
                    lTemp2 = 0.1;
                }
                lPriceRounded = lPriceFloor + lTemp1 + lTemp2;
            }
            if (pRoundMode == 2) //ALWAYS ENDS WITH .99
            {
                lTemp = Math.Floor(lPrice);
                lPriceRounded = lTemp + 0.99;
            }
            return lPriceRounded;
        }

        private void dgPriceLvl_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 //Price
              || e.ColumnIndex == 6 //Amount
              && e.RowIndex != this.dgPriceLvl.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
            if (e.ColumnIndex == 2
                 || e.ColumnIndex == 5//Discount
             && e.RowIndex != this.dgPriceLvl.NewRowIndex)
            {
                if (e.Value != null)
                {
                    string p = e.Value.ToString().Replace("%", "");
                    float d = float.Parse(p);
                    e.Value = Math.Round(d, 2).ToString() + "%";
                }
            }
        }

        private void chkNoRounding_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoRounding.Checked)
            {
                chkRound5.Checked = false;
                chkRound99.Checked = false;
                RoundingMode = 0;
            }
            calcPrice();
        }

        private void chkRound5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRound5.Checked)
            {
                chkNoRounding.Checked = false;
                chkRound99.Checked = false;
                RoundingMode = 1;
            }
            calcPrice();
        }

        private void chkRound99_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRound99.Checked)
            {
                chkNoRounding.Checked = false;
                chkRound5.Checked = false;
                RoundingMode = 2;
            }
            calcPrice();
        }

        void calcPrice()
        {
            string lCol = (rdoAC.Checked == true ? txtAverageCost.Value.ToString() : txtLastCost.Value.ToString());
            int lRowCount = dgPriceLvl.Rows.Count;
            for (int i = 0; i < lRowCount; i++)
            {
                if (dgPriceLvl.Rows[i].Cells[2].Value != null)//Regular Price
                {
                    if (dgPriceLvl.Rows[i].Cells[1].Value.ToString() == "Percent Margin")
                    {
                        CalculatePercentMargin(float.Parse(dgPriceLvl.Rows[i].Cells[2].Value.ToString()), float.Parse(lCol), 3, i);
                    }
                    else if (dgPriceLvl.Rows[i].Cells[1].Value.ToString() == "Percent Markup")

                    {
                        CalculatePercentMarkup(float.Parse(dgPriceLvl.Rows[i].Cells[2].Value.ToString()), float.Parse(lCol), 3, i);
                    }
                    else if (dgPriceLvl.Rows[i].Cells[1].Value.ToString() == "Fixed Gross")
                    {
                        CalculateFixedGP(float.Parse(dgPriceLvl.Rows[i].Cells[2].Value.ToString()), float.Parse(lCol), 3, i);
                    }
                    else if (dgPriceLvl.CurrentRow.Cells[1].Value.ToString() == "Discount from Base")
                    {
                        CalculateDiscountFromBase(float.Parse(dgPriceLvl.Rows[i].Cells[2].Value.ToString()), float.Parse(dgPriceLvl.Rows[0].Cells[3].Value.ToString()), 3, i);

                    }
                    changesPrice(3, i);
                }
                if (dgPriceLvl.Rows[i].Cells[5].Value != null)//Sale price
                {
                    if (dgPriceLvl.Rows[i].Cells[4].Value.ToString() == "Percent Margin")
                    {
                        CalculatePercentMargin(float.Parse(dgPriceLvl.Rows[i].Cells[5].Value.ToString()), float.Parse(lCol), 6, i);
                    }
                    else if (dgPriceLvl.Rows[i].Cells[4].Value.ToString() == "Percent Markup")

                    {
                        CalculatePercentMarkup(float.Parse(dgPriceLvl.Rows[i].Cells[5].Value.ToString()), float.Parse(lCol), 6, i);
                    }
                    else if (dgPriceLvl.Rows[i].Cells[4].Value.ToString() == "Fixed Gross")
                    {
                        CalculateFixedGP(float.Parse(dgPriceLvl.Rows[i].Cells[5].Value.ToString()), float.Parse(lCol), 6, i);
                    }
                    else if (dgPriceLvl.CurrentRow.Cells[4].Value.ToString() == "Discount from Base")
                    {
                        CalculateDiscountFromBase(float.Parse(dgPriceLvl.Rows[i].Cells[5].Value.ToString()), float.Parse(dgPriceLvl.Rows[0].Cells[6].Value.ToString()), 3, i);


                    }
                    changesPrice(6, i);
                }
            }
        }
        private void cbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("fontName", false, cbFonts.Text);
        }

        private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("FontSize", true, cbSize.Text);
        }

        private void ckBold_CheckedChanged(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("bold", false, null);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("italic", false, null);
        }

        private void ckUnderline_CheckedChanged(object sender, EventArgs e)
        {
            HTMLEditor.Document.ExecCommand("underline", false, null);
        }

        private void TabSales_Enter(object sender, EventArgs e)
        {
            SalesPrice();
        }
        void SalesPrice()
        {
            if (dgPriceLvl.Rows.Count != 13)
            {
                for (int i = 0; i < 13; i++)
                {
                    dgPriceLvl.Rows.Add();
                    dgPriceLvl.Rows[i].Cells[1].Value = "Percent Margin";
                    dgPriceLvl.Rows[i].Cells[2].Value = 0;
                    dgPriceLvl.Rows[i].Cells[3].Value = 0;
                    dgPriceLvl.Rows[i].Cells[4].Value = "Percent Margin";
                    dgPriceLvl.Rows[i].Cells[5].Value = 0;
                    dgPriceLvl.Rows[i].Cells[6].Value = 0;
                    dgPriceLvl.Rows[i].Cells[7].Value = 0;
                }
            }
            dgPriceLvl.Rows[0].Cells[0].Value = "Base Selling Price";
            dgPriceLvl.Rows[1].Cells[0].Value = "Level 1 Price";
            dgPriceLvl.Rows[2].Cells[0].Value = "Level 2 Price";
            dgPriceLvl.Rows[3].Cells[0].Value = "Level 3 Price";
            dgPriceLvl.Rows[4].Cells[0].Value = "Level 4 Price";
            dgPriceLvl.Rows[5].Cells[0].Value = "Level 5 Price";
            dgPriceLvl.Rows[6].Cells[0].Value = "Level 6 Price";
            dgPriceLvl.Rows[7].Cells[0].Value = "Level 7 Price";
            dgPriceLvl.Rows[8].Cells[0].Value = "Level 8 Price";
            dgPriceLvl.Rows[9].Cells[0].Value = "Level 9 Price";
            dgPriceLvl.Rows[10].Cells[0].Value = "Level 10 Price";
            dgPriceLvl.Rows[11].Cells[0].Value = "Level 11 Price";
            dgPriceLvl.Rows[12].Cells[0].Value = "Level 12 Price";
        }

        private void dgPriceLvl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colindex = (int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex);
            e.Control.KeyPress -= Numeric_KeyPress;

            if (colindex == 2 || colindex == 4 || colindex == 7)
            {
                e.Control.KeyPress += TextboxNumeric_KeyPress;
            }
            else
            {
                e.Control.KeyPress -= TextboxNumeric_KeyPress;
            }
        }
        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
            }
        }
        private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
              && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // only allow one negative char before the number
            if (e.KeyChar == '-'
                && (sender as System.Windows.Forms.TextBox).Text.IndexOf('-') == 0)
            {
                e.Handled = true;
            }
        }

        private void btnPurchaseSearch_Click(object sender, EventArgs e)
        {
            SearchPurchase();
        }
        private void SearchPurchase()
        {
            string selectSql = @"SELECT p.PurchaseID, p.PurchaseNumber,Name, p.TransactionDate, pl.OrderQty ,p.POStatus , pl.ActualUnitPrice ,pl.TotalAmount 
                                FROM Purchases p 
                                INNER JOIN Profile ON p.SupplierID = Profile.ID 
                                INNER JOIN PurchaseLines pl ON pl.PurchaseID = p.PurchaseID 
                                INNER JOIN Items i ON i.ID = pl.EntityID 
                                WHERE p.LayoutType = 'Item' AND p.TransactionDate BETWEEN @sdate AND @edate ";
            switch (cbPurchaseType.Text)
            {
                case "ALL":
                    selectSql += " ";
                    break;
                case "New":
                    selectSql += @"AND p.POStatus = 'New'";
                    break;
                case "Active":
                    selectSql += @" AND p.POStatus = 'Active'";
                    break;
                case "Backordered":
                    selectSql += @" AND p.POStatus = 'Backordered'";
                    break;
                case "Completed":
                    selectSql += @" AND p.POStatus = 'Completed'";
                    break;

            }
            if (txtSup.Text != "")
            {
                if (customerID != "")
                {
                    selectSql += " AND p.SupplierID = " + supplierID;
                }
                else
                {
                    selectSql += " AND Profile.Name = " + txtSup.Text;
                }
            }
            selectSql += " AND i.ID = '" + ItemID + "'";

            Dictionary<string, object> param = new Dictionary<string, object>();
            DateTime spdate = dtpFrom.Value.ToUniversalTime();
            DateTime epdate = dtpTo.Value.ToUniversalTime();
            spdate = new DateTime(spdate.Year, spdate.Month, spdate.Day, 00, 00, 00);
            epdate = new DateTime(epdate.Year, epdate.Month, epdate.Day, 23, 59, 59);
            param.Add("@sdate", spdate);
            param.Add("@edate", epdate);

            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql, param);

            if (dt.Rows.Count == 0)
                MessageBox.Show("No Record found.");

            dgPurchase.Rows.Clear();
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow dr = dt.Rows[x];
                dgPurchase.Rows.Add();
                dgPurchase.Rows[x].Cells[0].Value = dr["PurchaseID"].ToString();
                dgPurchase.Rows[x].Cells[1].Value = dr["PurchaseNumber"].ToString();
                dgPurchase.Rows[x].Cells[2].Value = dr["Name"].ToString();
                dgPurchase.Rows[x].Cells[3].Value = Convert.ToDateTime(dr["TransactionDate"]).ToLocalTime().ToShortDateString();
                dgPurchase.Rows[x].Cells[4].Value = dr["OrderQty"].ToString();
                dgPurchase.Rows[x].Cells[5].Value = dr["POStatus"].ToString();
                dgPurchase.Rows[x].Cells[6].Value = dr["ActualUnitPrice"].ToString();
                dgPurchase.Rows[x].Cells[7].Value = dr["TotalAmount"].ToString(); ;
                //  dgPurchase.Rows[x].Cells[6].Value = dr["TotalDue"].ToString();
            }

            dt.Clear();

        }

        private void btnSaleSearch_Click(object sender, EventArgs e)
        {
            SearchSaleHistory();
        }
        private void SearchSaleHistory()
        {
            dgSalesHistory.Rows.Clear();
            string selectSql = @"SELECT s.SalesID ,s.TransactionDate, sl.ShipQty ,s.InvoiceStatus ,s.SalesNumber, sl.ActualUnitPrice ,sl.TotalAmount, Name 
                                FROM Sales s 
                                INNER JOIN Profile ON s.CustomerID = Profile.ID 
                                INNER JOIN SalesLines sl ON sl.SalesID = s.SalesID
                                INNER JOIN Items i ON i.ID = sl.EntityID 
                                WHERE s.LayoutType = 'Item' AND s.TransactionDate BETWEEN @sdate AND @edate ";
            switch (cbSaleStatus.Text)
            {
                case "All":
                    selectSql += @" ";
                    break;
                case "Quote":
                    selectSql += @" AND s.SalesType = 'QUOTE'";
                    break;
                case "Order":
                    selectSql += @" AND s.SalesType = 'ORDER'";
                    break;
                case "Open Invoice":
                    selectSql += @" AND s.SalesType IN ('INVOICE', 'SINVOICE')
                                AND s.InvoiceStatus = 'Open'";
                    break;
                case "Return Credit":
                    selectSql += @" AND s.TotalDue < 0";
                    break;
                case "Closed Invoice":
                    selectSql += @" AND s.SalesType IN ('INVOICE', 'SINVOICE')
                                AND s.InvoiceStatus = 'Closed'";
                    break;
                case "Lay-by":
                    selectSql += @" AND s.SalesType = 'LAY-BY' ";
                    break;
            }
            if (txtCustomer.Text != "")
            {
                if (customerID != "")
                {
                    selectSql += " AND s.CustomerID = " + customerID;
                }
                else
                {
                    selectSql += " AND Profile.Name = " + txtCustomer.Text;
                }
            }


            selectSql += " AND i.ID = '" + ItemID + "'";
            Dictionary<string, object> param = new Dictionary<string, object>();
            DateTime spdate = sdateTimePicker.Value.ToUniversalTime();
            DateTime epdate = edateTimePicker.Value.ToUniversalTime();
            spdate = new DateTime(spdate.Year, spdate.Month, spdate.Day, 00, 00, 00);
            epdate = new DateTime(epdate.Year, epdate.Month, epdate.Day, 23, 59, 59);
            param.Add("@sdate", spdate);
            param.Add("@edate", epdate);
            System.Data.DataTable dt = new System.Data.DataTable();
            CommonClass.runSql(ref dt, selectSql, param);
            if (dt.Rows.Count == 0)
                MessageBox.Show("No Record found.");
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DataRow dr = dt.Rows[x];
                dgSalesHistory.Rows.Add();
                dgSalesHistory.Rows[x].Cells[0].Value = dr["SalesID"].ToString();
                dgSalesHistory.Rows[x].Cells[1].Value = dr["SalesNumber"].ToString();
                dgSalesHistory.Rows[x].Cells[2].Value = dr["Name"].ToString();
                dgSalesHistory.Rows[x].Cells[3].Value = Convert.ToDateTime(dr["TransactionDate"]).ToLocalTime().ToShortDateString();
                dgSalesHistory.Rows[x].Cells[4].Value = dr["ShipQty"].ToString();
                dgSalesHistory.Rows[x].Cells[5].Value = dr["InvoiceStatus"].ToString();
                dgSalesHistory.Rows[x].Cells[6].Value = dr["ActualUnitPrice"].ToString();
                dgSalesHistory.Rows[x].Cells[7].Value = dr["TotalAmount"].ToString();
                //  dgPurchase.Rows[x].Cells[6].Value = dr["TotalDue"].ToString();
            }

            dt.Clear();
        }
        private void txtLastCost_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemCostChanges.Select("colname = 'LastCostEx'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemCostChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemCostChanges.NewRow();
                rw["colname"] = "LastCostEx";
                rw["colvalue"] = this.txtLastCost.Value;
                ItemCostChanges.Rows.Add(rw);

            }
        }

        private void pbCat_Click(object sender, EventArgs e)
        {
            if (NoTran)
            {
                CategoryLookup catLookup = new CategoryLookup();
                if (catLookup.ShowDialog() == DialogResult.OK)
                {
                    string[] catDet = catLookup.GetCat;
                    ParentID = int.Parse(catDet[0]);
                    txtCategory.Text = catDet[1];
                    txtCostAcct.Text = catDet[3];
                    txtIncomeAcct.Text = catDet[2];
                    txtAssetAcct.Text = catDet[4];
                    if (chkIsSold.Checked)
                    {
                        grpIncome.Enabled = true;
                    }
                    else
                    {
                        grpIncome.Enabled = false;
                    }
                    if (chkIsBought.Checked)
                    {
                        grpCOS.Enabled = true;
                    }
                    else
                    {
                        grpCOS.Enabled = false;
                    }
                    if (chkIsCounted.Checked)
                    {
                        grpAsset.Enabled = true;
                    }
                    else
                    {
                        grpAsset.Enabled = false;
                    }
                    DataRow rw = ItemMainChanges.NewRow();
                    rw["colname"] = "CategoryID";
                    rw["colvalue"] = ParentID;
                    ItemMainChanges.Rows.Add(rw);
                }
            }
            else
            {
                MessageBox.Show("Item already has an existing transaction.", "Item Warnning");
            }
        }

        private void tabBarcode_Enter(object sender, EventArgs e)
        {
            LoadBarcode(ItemID.ToString());
            btnCancelBarcode.PerformClick();
        }
        void LoadBarcode(string ItemID)
        {
            dgBarCodes.Rows.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string Barcodesql = @" Select BarcodeID, BarcodeData,BarcodeType FROM Barcodes WHERE ItemID =" + ItemID;
            CommonClass.runSql(ref dt, Barcodesql);
            if (dt.Rows.Count > 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    DataRow dr = dt.Rows[x];
                    dgBarCodes.Rows.Add();
                    dgBarCodes.Rows[x].Cells[0].Value = dr[0].ToString();
                    dgBarCodes.Rows[x].Cells[1].Value = "false";
                    //dgBarCodes.Rows[x].Cells[2].Value =CommonClass.base64Decode( dr[1].ToString());
                    dgBarCodes.Rows[x].Cells[2].Value = dr[1].ToString();
                    dgBarCodes.Rows[x].Cells[3].Value = dr[2].ToString();
                }
                btnEditBarcode.Enabled = true;
                btnDelBarCode.Enabled = true;
            }
            else
            {
                btnEditBarcode.Enabled = false;
                btnDelBarCode.Enabled = false;
            }
            gbBarcodeDetail.Enabled = false;

        }
        private void btnAddBarcode_Click(object sender, EventArgs e)
        {
            barcodemode = 1;
            btnSaveBarcode.Enabled = true;
            gbBarcodeDetail.Enabled = true;
            btnEditBarcode.Enabled = false;
            btnDelBarCode.Enabled = false;

        }

        private void dgBarCodes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            barcodeID = int.Parse(dgBarCodes.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (barcodemode == 2)
            {
                txtBarCode.Text = dgBarCodes.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtBarcodeType.Text = dgBarCodes.Rows[e.RowIndex].Cells[3].Value.ToString();
                gbBarcodeDetail.Enabled = true;
            }

        }

        private void btnSaveBarcode_Click(object sender, EventArgs e)
        {
            if (barcodemode == 1)
            {
                if (txtBarCode.Text == "")
                {
                    MessageBox.Show("Enter a Barcode Number first", "Barcode Information");
                    return;
                }
                AddBarcode();
                btnCancelBarcode.PerformClick();
            }
            else if (barcodemode == 2)
            {
                UpdateBarcode(barcodeID);
                btnCancelBarcode.PerformClick();
            }
            else if (barcodemode == 3)
            {
                string barcodeIDs = "";

                int i = 0;
                foreach (DataGridViewRow item in dgBarCodes.Rows)
                {

                    if (bool.Parse(item.Cells[1].Value.ToString()))
                    {
                        barcodeIDs += item.Cells[0].Value.ToString() + ",";
                        i++;
                    }
                }
                if (i > 0)
                {
                    barcodeIDs = barcodeIDs.Remove(barcodeIDs.Length - 1);
                    if (i > 1)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete multiple barcode? (yes/no)", "Delete Barcode", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DeleteBarcode(barcodeIDs);
                            btnCancelBarcode.PerformClick();
                            MessageBox.Show("Record has been Deleted.", "INFORMATION");
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", "Delete Barcode", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DeleteBarcode(barcodeIDs);
                            btnCancelBarcode.PerformClick();
                            MessageBox.Show("Record has been Deleted.", "INFORMATION");
                        }
                        DeleteBarcode(barcodeIDs);
                    }
                    barcodeIDs = "";
                }
                else
                {
                    MessageBox.Show("Must check atleast 1", "Information");
                }

            }
        }

        private void btnEditBarcode_Click(object sender, EventArgs e)
        {
            barcodemode = 2;
            btnSaveBarcode.Enabled = true;

            btnAddBarcode.Enabled = false;
            btnDelBarCode.Enabled = false;
        }
        void AddBarcode()
        {
            string Barcodesql = @" INSERT INTO Barcodes (BarcodeData,ItemID,BarcodeType) VALUES (@BarcodeData,@ItemID,@BarcodeType) ";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BarcodeData", txtBarCode.Text);
            param.Add("@ItemID", ItemID);
            param.Add("@BarcodeType", txtBarcodeType.Text);

            CommonClass.runSql(Barcodesql, CommonClass.RunSqlInsertMode.QUERY, param);
        }
        void UpdateBarcode(int BarcodeID)
        {
            string Barcodesql = @" UPDATE Barcodes SET BarcodeData = @BarcodeData,ItemID = @ItemID,BarcodeType = @BarcodeType WHERE BarcodeID = " + BarcodeID;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@BarcodeData", txtBarCode.Text);
            param.Add("@ItemID", ItemID);
            param.Add("@BarcodeType", txtBarcodeType.Text);

            CommonClass.runSql(Barcodesql, CommonClass.RunSqlInsertMode.QUERY, param);
        }
        void DeleteBarcode(string barcodeID)
        {
            string Barcodesql = @"DELETE FROM Barcodes WHERE BarcodeID in ( " + barcodeID + " )";
            CommonClass.runSql(Barcodesql);
        }

        private void btnCancelBarcode_Click(object sender, EventArgs e)
        {
            LoadBarcode(ItemID.ToString());
            btnSaveBarcode.Enabled = false;
            txtBarCode.Text = "";
            txtBarcodeType.Text = "";
            gbBarcodeDetail.Enabled = false;
            btnAddBarcode.Enabled = true;
            dgBarCodes.Columns[1].Visible = false;
            btnSaveBarcode.Text = "Save";
            barcodemode = 0;

        }

        private void btnDelBarCode_Click(object sender, EventArgs e)
        {
            barcodemode = 3;
            dgBarCodes.Columns[1].Visible = true;
            btnAddBarcode.Enabled = false;
            btnEditBarcode.Enabled = false;
            btnSaveBarcode.Enabled = true;
            btnSaveBarcode.Text = "OK";
        }

        public void ShowCustomerAccounts()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                customerID = lProfile[0];
                this.txtCustomer.Text = lProfile[2];
            }
        }
        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomerAccounts();
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            if (txtCustomer.Text == "")
            {
                customerID = "";
            }
        }

        private void pbSup_Click(object sender, EventArgs e)
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Supplier");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                supplierID = lProfile[0];
                this.txtSup.Text = lProfile[2];
            }
        }

        private void txtSup_TextChanged(object sender, EventArgs e)
        {
            if (txtSup.Text == "")
            {
                supplierID = "";
            }
        }

        private void PromoTab_Enter(object sender, EventArgs e)
        {

        }
        private void LoadReport()
        {
            SqlConnection con = null;
            try
            {
                Reports.ReportParams profileparams = new Reports.ReportParams();
                profileparams.PrtOpt = 1;
                profileparams.Rec.Add(dtPromos);
                profileparams.ReportName = "ItemsPromos.rpt";
                profileparams.RptTitle = "Transaction History";
                profileparams.Params = "compname";
                profileparams.PVals = CommonClass.CompName.Trim();

                CommonClass.ShowReport(profileparams);
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
        public void LoadPromos()
        {
            dtPromos.Rows.Clear();
            string promosql = "Select * From Promos WHERE StartDate = @StartDate AND EndDate = @EndDate AND isActive = '1'";
            Dictionary<string, object> param = new Dictionary<string, object>();

            DateTime dtpfromutc = this.dtStart.Value;
            DateTime dtptoutc = this.dtEnd.Value;
            string sdate = dtpfromutc.ToString("yyyy-MM-dd") + " 00:00:00";
            string edate = dtptoutc.ToString("yyyy-MM-dd") + " 23:59:59";
            param.Add("@StartDate", sdate);
            param.Add("@EndDate", edate);

            string itemPromoJson;

            CommonClass.runSql(ref dtPromos, promosql, param);
            if (dtPromos.Rows.Count > 0)
            {

                for (int i = 0; i < dtPromos.Rows.Count; i++)
                {
                    DataRow x = dtPromos.Rows[i];
                    string file = x["RuleCriteriaID"].ToString();
                    string accumulation = x["PointAccumulationCriteria"].ToString();
                    string pointsvalue = x["PointsValue"].ToString();
                    x["RuleCriteria"].ToString().Replace("\t", "");
                    itemPromoJson = file.Replace("\t", "");

                    List<RuleCriteriaPoints> itemPromos = JsonConvert.DeserializeObject<List<RuleCriteriaPoints>>(itemPromoJson);
                    List<RuleCriteria> isLoyal = JsonConvert.DeserializeObject<List<RuleCriteria>>(x["RuleCriteria"].ToString().Replace("\t", ""));
                    foreach (RuleCriteriaPoints promo in itemPromos)
                    {

                        if (promo.CriteriaName == "Item")
                        {
                            if (dtPromos.Rows.Count > dgPromos.Rows.Count)
                            {
                                if (promo.CriteriaValue == ItemID.ToString())
                                {
                                    dgPromos.Rows.Add();
                                    dgPromos.Rows[i].Cells[0].Value = x["PromoID"].ToString();
                                    dgPromos.Rows[i].Cells[1].Value = x["PromoCode"].ToString();
                                    dgPromos.Rows[i].Cells[2].Value = x["PromotionType"].ToString();
                                    dgPromos.Rows[i].Cells[3].Value = x["PointAccumulationCriteria"].ToString();
                                    dgPromos.Rows[i].Cells[4].Value = Convert.ToDateTime(x["StartDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[5].Value = Convert.ToDateTime(x["EndDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[6].Value = (bool)x["isActive"];
                                }
                            }

                        }
                        else if (promo.CriteriaName == "Category")
                        {
                            if (dtPromos.Rows.Count > dgPromos.Rows.Count)
                            {
                                if (promo.CriteriaValue == ParentID.ToString())
                                {
                                    dgPromos.Rows.Add();
                                    dgPromos.Rows[i].Cells[0].Value = x["PromoID"].ToString();
                                    dgPromos.Rows[i].Cells[1].Value = x["PromoCode"].ToString();
                                    dgPromos.Rows[i].Cells[2].Value = x["PromotionType"].ToString();
                                    dgPromos.Rows[i].Cells[3].Value = x["PointAccumulationCriteria"].ToString();
                                    dgPromos.Rows[i].Cells[4].Value = Convert.ToDateTime(x["StartDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[5].Value = Convert.ToDateTime(x["EndDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[6].Value = (bool)x["isActive"];
                                }
                            }
                        }
                        else if (promo.CriteriaName == "Supplier")
                        {
                            if (dtPromos.Rows.Count > dgPromos.Rows.Count)
                            {
                                if (promo.CriteriaValue == this.lblSupplierID.Text)
                                {
                                    dgPromos.Rows.Add();
                                    dgPromos.Rows[i].Cells[0].Value = x["PromoID"].ToString();
                                    dgPromos.Rows[i].Cells[1].Value = x["PromoCode"].ToString();
                                    dgPromos.Rows[i].Cells[2].Value = x["PromotionType"].ToString();
                                    dgPromos.Rows[i].Cells[3].Value = x["PointAccumulationCriteria"].ToString();
                                    dgPromos.Rows[i].Cells[4].Value = Convert.ToDateTime(x["StartDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[5].Value = Convert.ToDateTime(x["EndDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[6].Value = (bool)x["isActive"];
                                }
                            }
                        }
                        else if (promo.CriteriaName == "Brand")
                        {
                            if (dtPromos.Rows.Count > dgPromos.Rows.Count)
                            {
                                if (promo.CriteriaValue == this.txtBrand.Text)
                                {
                                    dgPromos.Rows.Add();
                                    dgPromos.Rows[i].Cells[0].Value = x["PromoID"].ToString();
                                    dgPromos.Rows[i].Cells[1].Value = x["PromoCode"].ToString();
                                    dgPromos.Rows[i].Cells[2].Value = x["PromotionType"].ToString();
                                    dgPromos.Rows[i].Cells[3].Value = x["PointAccumulationCriteria"].ToString();
                                    dgPromos.Rows[i].Cells[4].Value = Convert.ToDateTime(x["StartDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[5].Value = Convert.ToDateTime(x["EndDate"]).ToShortDateString();
                                    dgPromos.Rows[i].Cells[6].Value = (bool)x["isActive"];
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtMaxQty_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemQtyChanges.Select("colname = 'MaxQty'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemQtyChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemQtyChanges.NewRow();
                rw["colname"] = "MaxQty";
                rw["colvalue"] = this.txtMaxQty.Value;
                ItemQtyChanges.Rows.Add(rw);
            }
        }

        private void itemDesc2_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'ItemDescriptionSimple'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "ItemDescriptionSimple";
                rw["colvalue"] = this.itemDesc2.Text;
                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void rdoAC_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void SaleEndDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemPriceChanges.Select("colname = 'EndSalesDate'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemPriceChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemPriceChanges.NewRow();
                rw["colname"] = "EndSalesDate";
                DateTime edate = SaleEndDatePicker.Value.ToUniversalTime();
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);
                rw["colvalue"] = edate;
                ItemPriceChanges.Rows.Add(rw);
            }
        }

        private void SaleStartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemPriceChanges.Select("colname = 'StartSaleDate'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemPriceChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemPriceChanges.NewRow();
                rw["colname"] = "StartSaleDate";
                DateTime sdate = SaleStartDatePicker.Value.ToUniversalTime();
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 23, 59, 59);
                rw["colvalue"] = sdate;
                ItemPriceChanges.Rows.Add(rw);

            }
        }

        private void StockTake_Enter(object sender, EventArgs e)
        {
            ShowStockTake();
        }

        private void ShowStockTake()
        {
            if (IsLoading)
                return;

            DateTime lsdate = this.startTakePicker.Value.ToUniversalTime();
            DateTime ledate = this.endTakePicker.Value.ToUniversalTime();
            lsdate = new DateTime(lsdate.Year, lsdate.Month, lsdate.Day, 00, 00, 00);
            ledate = new DateTime(ledate.Year, ledate.Month, ledate.Day, 23, 59, 59);
            string itemcon = "";
            itemcon = " and i.ID = '" + ItemID + "'";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@sdate", lsdate);
            param.Add("@edate", ledate);
            //CONVERT(datetime, SWITCHOFFSET(CONVERT(datetimeoffset, t.TransactionDate), DATENAME(TzOffset, SYSDATETIMEOFFSET()))) AS TranDate
            string sql = @"SELECT * from ( SELECT  t.TransactionDate as [Transaction Date], t.TranType,p.PurchaseNumber as [Transaction Number], i.PartNumber as [Part Number], i.ItemName as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx  as [Total Cost],t.SourceTranID, 'P' as formtype
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join Purchases as p on t.SourceTranID = p.PurchaseID where TranType in ('RI','PB') and ( t.TransactionDate >= @sdate and t.TransactionDate <= @edate)  " + itemcon;

            sql += @"UNION SELECT t.TransactionDate as [Transaction Date], t.TranType,p.SalesNumber as  [Transaction Number],  i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'P' as formtype
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join Sales as p on t.SourceTranID = p.SalesID where TranType = 'SI' and ( t.TransactionDate >= @sdate and t.TransactionDate <= @edate)  " + itemcon;

            sql += @"UNION SELECT t.TransactionDate as [Transaction Date], t.TranType,p.ItemAdjNumber as  [Transaction Number],  i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'A' as formtype
                             FROM (ItemTransaction t inner join Items i on t.ItemID = i.id) inner join ItemsAdjustment as p on t.SourceTranID = p.ItemAdjID where TranType in ('IA','IB','ST') and ( t.TransactionDate >= @sdate and t.TransactionDate <= @edate)  " + itemcon + " ) as i order by i.[Transaction Date] desc";

            System.Data.DataTable dt = new System.Data.DataTable();

            CommonClass.runSql(ref dt, sql, param);
            foreach (DataRow rw in dt.Rows)
            {
                switch (rw["TranType"].ToString().Trim())
                {
                    case "SI":
                        rw["TranType"] = "Invoice";
                        break;

                    case "IA":
                        rw["TranType"] = "Inventory Adjustment";
                        break;
                    case "IB":
                        rw["TranType"] = "Item Bundle";
                        break;
                    case "PB":
                        rw["TranType"] = "Purchase Bill";
                        break;
                    case "RI":
                        rw["TranType"] = "Receive Item";
                        break;
                    case "ST":
                        rw["TranType"] = "StockTake";
                        break;
                }

                DateTime lTranUTC = (DateTime)rw["Transaction Date"];
                DateTime lTranLocal = lTranUTC.ToLocalTime();
                //  lTranLocal = new DateTime(lTranLocal.Year, lTranLocal.Month, lTranLocal.Day);
                rw["Transaction Date"] = lTranLocal.ToShortDateString();
            }
            this.dgridStockItem.DataSource = dt;
            this.dgridStockItem.Columns["Total Cost"].DefaultCellStyle.Format = "C2";
            this.dgridStockItem.Columns["Total Cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgridStockItem.Columns["SourceTranID"].Visible = false;
            this.dgridStockItem.Columns["formtype"].Visible = false;
            //if (dt.Rows.Count > 0)
            //    btnPrint.Enabled = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ShowStockTake();
        }

        private void rdoStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            if (SaveMode > 0)
            {
                DataRow[] Qr = ItemMainChanges.Select("colname = 'BundleType'");
                if (Qr.Length > 0)
                {
                    foreach (DataRow r in Qr)
                    {
                        ItemMainChanges.Rows.Remove(r);
                    }
                }
                DataRow rw = ItemMainChanges.NewRow();
                rw["colname"] = "BundleType";
                if (rdoStatic.Checked)
                    rw["colvalue"] = this.rdoStatic.Text;
                else if (rdoDynamic.Checked)
                    rw["colvalue"] = this.rdoDynamic.Text;
                else
                    rw["colvalue"] = this.rdoIngredient.Text;

                ItemMainChanges.Rows.Add(rw);
            }
        }

        private void btnLoadStockTakeHistory_Click(object sender, EventArgs e)
        {
            ShowStockTakeHistory();
        }

        private void ShowStockTakeHistory()
        {
            if (IsLoading)
                return;

            DateTime lsdate = this.StocktakeStart.Value.ToUniversalTime();
            DateTime ledate = this.StockTakeEnd.Value.ToUniversalTime();
            lsdate = new DateTime(lsdate.Year, lsdate.Month, lsdate.Day, 00, 00, 00);
            ledate = new DateTime(ledate.Year, ledate.Month, ledate.Day, 23, 59, 59);
            string itemcon = "";
            itemcon = " and i.ID = " + ItemID + "";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@sdate", lsdate);
            param.Add("@edate", ledate);
            string sql = @"SELECT t.TransactionDate as [Transaction Date], t.TranType,p.ItemAdjNumber as  [Transaction Number],  i.PartNumber as [Part Number], i.ItemName  as [Item Name], t.QtyAdjustment as [Quantity Adjustment],t.TotalCostEx as [Total Cost],t.SourceTranID, 'A' as formtype
                             FROM ItemTransaction t
                            inner join Items i on t.ItemID = i.id 
                            inner join ItemsAdjustment as p on t.SourceTranID = p.ItemAdjID
                            where isStockTake = '1' and ( t.TransactionDate >= @sdate and t.TransactionDate <= @edate)  " + itemcon;

            System.Data.DataTable dt = new System.Data.DataTable();

            CommonClass.runSql(ref dt, sql, param);
            foreach (DataRow rw in dt.Rows)
            {

                rw["TranType"] = "StockTake";
                DateTime lTranUTC = (DateTime)rw["Transaction Date"];
                DateTime lTranLocal = lTranUTC.ToLocalTime();
                rw["Transaction Date"] = lTranLocal.ToShortDateString();
            }
            this.dgStockTakeHistory.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                this.dgStockTakeHistory.Columns["Total Cost"].DefaultCellStyle.Format = "C2";
                this.dgStockTakeHistory.Columns["Total Cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgStockTakeHistory.Columns["SourceTranID"].Visible = false;
                this.dgStockTakeHistory.Columns["formtype"].Visible = false;
            }
        }

        private void StockTakeHistory_Enter(object sender, EventArgs e)
        {
            ShowStockTakeHistory();
        }

        private void rdoDynamic_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TabAutoBuild_Enter(object sender, EventArgs e)
        {
            if (this.chkAutoBuild.Checked)
            {
                this.dgridParts.Enabled = true;
                this.btnRemovePart.Enabled = true;
                this.btnAddPart.Enabled = true;
                this.rdoDynamic.Enabled = true;
                this.rdoStatic.Enabled = true;
                this.rdoIngredient.Enabled = true;

            }
            else
            {
                this.dgridParts.Enabled = false;
                this.btnRemovePart.Enabled = false;
                this.btnAddPart.Enabled = false;
                this.rdoDynamic.Enabled = false;
                this.rdoStatic.Enabled = false;
                this.rdoIngredient.Enabled = false;

            }
        }

        private void dgridParts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 //Price
              && e.RowIndex != this.dgridParts.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgridParts_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgPromos.Rows.Clear();
            LoadPromos();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void dgPurchase_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 6
                 || e.ColumnIndex == 7)
                 && e.RowIndex != dgPurchase.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void dgSalesHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 6
                || e.ColumnIndex == 7)
                && e.RowIndex != dgSalesHistory.NewRowIndex)
            {
                if (e.Value != null)
                {
                    double d = double.Parse(e.Value.ToString(), NumberStyles.Currency);
                    e.Value = d.ToString("C2");
                }
            }
        }

        private void btnExportExcell_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    ws.Cells[4, 1] = "Purchase Number";
                    ws.Cells[4, 2] = "Supplier Name";
                    ws.Cells[4, 3] = "Date";
                    ws.Cells[4, 4] = "Quantity";
                    ws.Cells[4, 5] = "Status";
                    ws.Cells[4, 6] = "Amount";
                    ws.Cells[4, 7] = "Grand Total";
                    int i = 5;
                    foreach (DataGridViewRow item in dgPurchase.Rows)
                    {

                        if (item.Cells[0].Value != null && item.Cells[0].Value.ToString() != "")
                        {
                            ws.Cells[i, 1] = item.Cells[1].Value.ToString();
                        }
                        if (item.Cells[1].Value != null)
                        {
                            ws.Cells[i, 2] = item.Cells[2].Value.ToString();
                        }
                        if (item.Cells[2].Value != null)
                        {
                            ws.Cells[i, 3] = Convert.ToDateTime(item.Cells[3].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[3].Value != null)
                        {
                            ws.Cells[i, 4] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[5].Value.ToString();
                        }
                        if (item.Cells[5].Value != null)
                        {
                            ws.Cells[i, 6] = Math.Round(float.Parse(item.Cells[6].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[6].Value != null)
                        {
                            ws.Cells[i, 7] = Math.Round(float.Parse(item.Cells[7].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "G3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = txtItemName.Text + "Purchase History";

                    //Style Table
                    cellRange = ws.get_Range("A4", "G4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Purchase History Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void btnSHExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    ws.Cells[4, 1] = "Sales Number";
                    ws.Cells[4, 2] = "Customer Name";
                    ws.Cells[4, 3] = "Date";
                    ws.Cells[4, 4] = "Quantity";
                    ws.Cells[4, 5] = "Status";
                    ws.Cells[4, 6] = "Amount";
                    ws.Cells[4, 7] = "Grand Total";
                    int i = 5;
                    foreach (DataGridViewRow item in dgSalesHistory.Rows)
                    {

                        if (item.Cells[0].Value != null && item.Cells[0].Value.ToString() != "")
                        {
                            ws.Cells[i, 1] = item.Cells[1].Value.ToString();
                        }
                        if (item.Cells[1].Value != null)
                        {
                            ws.Cells[i, 2] = item.Cells[2].Value.ToString();
                        }
                        if (item.Cells[2].Value != null && item.Cells[0].Value.ToString() != "")
                        {
                            ws.Cells[i, 3] = Convert.ToDateTime(item.Cells[3].Value.ToString()).ToShortDateString();
                        }
                        if (item.Cells[3].Value != null)
                        {
                            ws.Cells[i, 4] = item.Cells[4].Value.ToString();
                        }
                        if (item.Cells[4].Value != null)
                        {
                            ws.Cells[i, 5] = item.Cells[5].Value.ToString();
                        }
                        if (item.Cells[5].Value != null)
                        {
                            double Amt = Double.Parse(item.Cells[6].Value.ToString());
                            ws.Cells[i, 6] = Amt.ToString("C");
                            //  ws.Cells[i, 6] = Math.Round(float.Parse(item.Cells[6].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        if (item.Cells[6].Value != null)
                        {
                            double Amt = Double.Parse(item.Cells[7].Value.ToString());
                            ws.Cells[i, 7] = Amt.ToString("C");
                            // ws.Cells[i, 7] = Math.Round(float.Parse(item.Cells[7].Value.ToString()), 2).ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        i++;
                    }

                    Range cellRange = ws.get_Range("A1", "G3");
                    cellRange.Merge(false);
                    cellRange.Interior.Color = System.Drawing.Color.White;
                    cellRange.Font.Color = System.Drawing.Color.Gray;
                    cellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    cellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    cellRange.Font.Size = 26;
                    ws.Cells[1, 1] = txtItemName.Text + "Sales History ";

                    //Style Table
                    cellRange = ws.get_Range("A4", "G4");
                    cellRange.Font.Bold = true;
                    cellRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    cellRange.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    ws.get_Range("A4").EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.get_Range("A5").EntireColumn.NumberFormat = "0";

                    ws.Columns.AutoFit();

                    wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Sales History Report has been successfully exported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
