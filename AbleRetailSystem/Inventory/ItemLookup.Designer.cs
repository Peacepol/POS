namespace RestaurantPOS.Inventory
{
    partial class ItemLookup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgridItems = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnHand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandardCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AverageCostEx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesTaxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxCollectedAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RateTaxSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseTaxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxPaidAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RateTaxPurchase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCounted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssetAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsBought = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COSAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockcheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isAutoBuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BundleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescriptionSimple = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtPartNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSuppPartNum = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtSerialNum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCustomList1 = new System.Windows.Forms.TextBox();
            this.txtCustomList2 = new System.Windows.Forms.TextBox();
            this.txtCustomList3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtItemNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.chkWithQty = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridItems
            // 
            this.dgridItems.AllowUserToAddRows = false;
            this.dgridItems.AllowUserToDeleteRows = false;
            this.dgridItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridItems.BackgroundColor = System.Drawing.Color.White;
            this.dgridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.PartNo,
            this.ItemNo,
            this.ItemName,
            this.OnHand,
            this.LastCost,
            this.StandardCost,
            this.AverageCostEx,
            this.SellingPrice,
            this.SalesTaxCode,
            this.TaxCollectedAccountID,
            this.RateTaxSales,
            this.PurchaseTaxCode,
            this.TaxPaidAccountID,
            this.RateTaxPurchase,
            this.IsCounted,
            this.AssetAccountID,
            this.IsBought,
            this.COSAccountID,
            this.SupplierID,
            this.CategoryID,
            this.BrandName,
            this.stockcheck,
            this.isAutoBuild,
            this.BundleType,
            this.ItemDescriptionSimple,
            this.ItemDescription,
            this.SupplierName});
            this.dgridItems.Location = new System.Drawing.Point(8, 165);
            this.dgridItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgridItems.MultiSelect = false;
            this.dgridItems.Name = "dgridItems";
            this.dgridItems.RowHeadersVisible = false;
            this.dgridItems.RowTemplate.Height = 24;
            this.dgridItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridItems.Size = new System.Drawing.Size(887, 338);
            this.dgridItems.TabIndex = 1;
            this.dgridItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellDoubleClick);
            this.dgridItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgridItems_CellFormatting);
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "Item ID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            // 
            // PartNo
            // 
            this.PartNo.HeaderText = "Part No";
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            // 
            // ItemNo
            // 
            this.ItemNo.HeaderText = "Item No";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // OnHand
            // 
            this.OnHand.HeaderText = "On Hand";
            this.OnHand.Name = "OnHand";
            this.OnHand.ReadOnly = true;
            // 
            // LastCost
            // 
            this.LastCost.HeaderText = "Last Cost";
            this.LastCost.Name = "LastCost";
            this.LastCost.ReadOnly = true;
            // 
            // StandardCost
            // 
            this.StandardCost.HeaderText = "Standard Cost";
            this.StandardCost.Name = "StandardCost";
            this.StandardCost.ReadOnly = true;
            // 
            // AverageCostEx
            // 
            this.AverageCostEx.HeaderText = "Average Cost";
            this.AverageCostEx.Name = "AverageCostEx";
            this.AverageCostEx.ReadOnly = true;
            // 
            // SellingPrice
            // 
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            // 
            // SalesTaxCode
            // 
            this.SalesTaxCode.HeaderText = "SalesTaxCode";
            this.SalesTaxCode.Name = "SalesTaxCode";
            this.SalesTaxCode.Visible = false;
            // 
            // TaxCollectedAccountID
            // 
            this.TaxCollectedAccountID.HeaderText = "TaxCollectedAccountID";
            this.TaxCollectedAccountID.Name = "TaxCollectedAccountID";
            this.TaxCollectedAccountID.Visible = false;
            // 
            // RateTaxSales
            // 
            this.RateTaxSales.HeaderText = "RateTaxSales";
            this.RateTaxSales.Name = "RateTaxSales";
            this.RateTaxSales.Visible = false;
            // 
            // PurchaseTaxCode
            // 
            this.PurchaseTaxCode.HeaderText = "PurchaseTaxCode";
            this.PurchaseTaxCode.Name = "PurchaseTaxCode";
            this.PurchaseTaxCode.Visible = false;
            // 
            // TaxPaidAccountID
            // 
            this.TaxPaidAccountID.HeaderText = "TaxPaidAccountID";
            this.TaxPaidAccountID.Name = "TaxPaidAccountID";
            this.TaxPaidAccountID.Visible = false;
            // 
            // RateTaxPurchase
            // 
            this.RateTaxPurchase.HeaderText = "RateTaxPurchase";
            this.RateTaxPurchase.Name = "RateTaxPurchase";
            this.RateTaxPurchase.Visible = false;
            // 
            // IsCounted
            // 
            this.IsCounted.HeaderText = "IsCounted";
            this.IsCounted.Name = "IsCounted";
            this.IsCounted.Visible = false;
            // 
            // AssetAccountID
            // 
            this.AssetAccountID.HeaderText = "AssetAccountID";
            this.AssetAccountID.Name = "AssetAccountID";
            this.AssetAccountID.Visible = false;
            // 
            // IsBought
            // 
            this.IsBought.HeaderText = "IsBought";
            this.IsBought.Name = "IsBought";
            this.IsBought.Visible = false;
            // 
            // COSAccountID
            // 
            this.COSAccountID.HeaderText = "COSAccountID ";
            this.COSAccountID.Name = "COSAccountID";
            this.COSAccountID.Visible = false;
            // 
            // SupplierID
            // 
            this.SupplierID.HeaderText = "SupplierID";
            this.SupplierID.Name = "SupplierID";
            this.SupplierID.Visible = false;
            // 
            // CategoryID
            // 
            this.CategoryID.HeaderText = "CategoryID";
            this.CategoryID.Name = "CategoryID";
            this.CategoryID.Visible = false;
            // 
            // BrandName
            // 
            this.BrandName.HeaderText = "BrandName";
            this.BrandName.Name = "BrandName";
            this.BrandName.Visible = false;
            // 
            // stockcheck
            // 
            this.stockcheck.HeaderText = "";
            this.stockcheck.Name = "stockcheck";
            this.stockcheck.Visible = false;
            // 
            // isAutoBuild
            // 
            this.isAutoBuild.HeaderText = "isAutoBuild";
            this.isAutoBuild.Name = "isAutoBuild";
            this.isAutoBuild.ReadOnly = true;
            this.isAutoBuild.Visible = false;
            // 
            // BundleType
            // 
            this.BundleType.HeaderText = "BundleType";
            this.BundleType.Name = "BundleType";
            this.BundleType.ReadOnly = true;
            this.BundleType.Visible = false;
            // 
            // ItemDescriptionSimple
            // 
            this.ItemDescriptionSimple.HeaderText = "Description";
            this.ItemDescriptionSimple.Name = "ItemDescriptionSimple";
            this.ItemDescriptionSimple.Visible = false;
            // 
            // ItemDescription
            // 
            this.ItemDescription.HeaderText = "ItemDescription";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.Visible = false;
            // 
            // SupplierName
            // 
            this.SupplierName.HeaderText = "Sname";
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(805, 117);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 36);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(781, 508);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(113, 30);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPartNum
            // 
            this.txtPartNum.Location = new System.Drawing.Point(105, 10);
            this.txtPartNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtPartNum.Name = "txtPartNum";
            this.txtPartNum.Size = new System.Drawing.Size(181, 20);
            this.txtPartNum.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Part No/Barcode:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 120);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description :";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(105, 117);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(181, 42);
            this.txtDesc.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(369, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Supplier :";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(424, 10);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(181, 20);
            this.txtSupplier.TabIndex = 10;
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(714, 10);
            this.txtBrand.Margin = new System.Windows.Forms.Padding(2);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(181, 20);
            this.txtBrand.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(669, 13);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Brand :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 40);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Supplier\'s Part Number :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 67);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Item Name :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 94);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Serial Number :";
            // 
            // txtSuppPartNum
            // 
            this.txtSuppPartNum.Location = new System.Drawing.Point(424, 37);
            this.txtSuppPartNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtSuppPartNum.Name = "txtSuppPartNum";
            this.txtSuppPartNum.Size = new System.Drawing.Size(181, 20);
            this.txtSuppPartNum.TabIndex = 10;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(105, 64);
            this.txtItemName.Margin = new System.Windows.Forms.Padding(2);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(181, 20);
            this.txtItemName.TabIndex = 10;
            // 
            // txtSerialNum
            // 
            this.txtSerialNum.Location = new System.Drawing.Point(105, 91);
            this.txtSerialNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtSerialNum.Name = "txtSerialNum";
            this.txtSerialNum.Size = new System.Drawing.Size(181, 20);
            this.txtSerialNum.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(634, 37);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Custom List 1 :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(634, 64);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Custom List 2 :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(634, 91);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Custom List 3 :";
            // 
            // txtCustomList1
            // 
            this.txtCustomList1.Location = new System.Drawing.Point(714, 34);
            this.txtCustomList1.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomList1.Name = "txtCustomList1";
            this.txtCustomList1.Size = new System.Drawing.Size(181, 20);
            this.txtCustomList1.TabIndex = 10;
            // 
            // txtCustomList2
            // 
            this.txtCustomList2.Location = new System.Drawing.Point(714, 61);
            this.txtCustomList2.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomList2.Name = "txtCustomList2";
            this.txtCustomList2.Size = new System.Drawing.Size(181, 20);
            this.txtCustomList2.TabIndex = 10;
            // 
            // txtCustomList3
            // 
            this.txtCustomList3.Location = new System.Drawing.Point(714, 88);
            this.txtCustomList3.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomList3.Name = "txtCustomList3";
            this.txtCustomList3.Size = new System.Drawing.Size(181, 20);
            this.txtCustomList3.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 39);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Item Number :";
            // 
            // txtItemNum
            // 
            this.txtItemNum.Location = new System.Drawing.Point(105, 36);
            this.txtItemNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtItemNum.Name = "txtItemNum";
            this.txtItemNum.Size = new System.Drawing.Size(181, 20);
            this.txtItemNum.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 211;
            this.label3.Text = "Category :";
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCategory.Location = new System.Drawing.Point(423, 62);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(182, 94);
            this.treeCategory.TabIndex = 210;
            this.treeCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterCheck);
            // 
            // chkWithQty
            // 
            this.chkWithQty.AutoSize = true;
            this.chkWithQty.Location = new System.Drawing.Point(625, 136);
            this.chkWithQty.Name = "chkWithQty";
            this.chkWithQty.Size = new System.Drawing.Size(125, 17);
            this.chkWithQty.TabIndex = 212;
            this.chkWithQty.Text = "Show Only In Stocks";
            this.chkWithQty.UseVisualStyleBackColor = true;
            // 
            // ItemLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 550);
            this.Controls.Add(this.chkWithQty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeCategory);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.txtCustomList3);
            this.Controls.Add(this.txtSerialNum);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtCustomList2);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.txtCustomList1);
            this.Controls.Add(this.txtSuppPartNum);
            this.Controls.Add(this.txtItemNum);
            this.Controls.Add(this.txtPartNum);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgridItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ItemLookup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Lookup";
            this.Load += new System.EventHandler(this.ItemLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgridItems;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtPartNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSuppPartNum;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtSerialNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCustomList1;
        private System.Windows.Forms.TextBox txtCustomList2;
        private System.Windows.Forms.TextBox txtCustomList3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtItemNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.CheckBox chkWithQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnHand;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandardCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn AverageCostEx;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesTaxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxCollectedAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RateTaxSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseTaxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxPaidAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RateTaxPurchase;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsCounted;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssetAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsBought;
        private System.Windows.Forms.DataGridViewTextBoxColumn COSAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn stockcheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn isAutoBuild;
        private System.Windows.Forms.DataGridViewTextBoxColumn BundleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescriptionSimple;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
    }
}