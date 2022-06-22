namespace AbleRetailPOS
{
    partial class PointsAccumulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PointsAccumulation));
            this.AccumulationTabControl = new System.Windows.Forms.TabControl();
            this.DetailsTab = new System.Windows.Forms.TabPage();
            this.btnFreeitems = new System.Windows.Forms.Button();
            this.cbPromoType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pbCustomer = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCustomerList = new System.Windows.Forms.ComboBox();
            this.chkLoyaltyOnly = new System.Windows.Forms.CheckBox();
            this.IsActive = new System.Windows.Forms.CheckBox();
            this.txtPoints = new System.Windows.Forms.NumericUpDown();
            this.cmbRewardName = new System.Windows.Forms.ComboBox();
            this.dgAccumulationPoints = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edate = new System.Windows.Forms.DateTimePicker();
            this.sdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPromoCode = new System.Windows.Forms.TextBox();
            this.CategoriesTab = new System.Windows.Forms.TabPage();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.ItemInvetoryTab = new System.Windows.Forms.TabPage();
            this.dgInventoryItems = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierTab = new System.Windows.Forms.TabPage();
            this.dgSupplier = new System.Windows.Forms.DataGridView();
            this.SupplierID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSupplier = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandTab = new System.Windows.Forms.TabPage();
            this.dgBrand = new System.Windows.Forms.DataGridView();
            this.chkBrand = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemBrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.AccumulationTabControl.SuspendLayout();
            this.DetailsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAccumulationPoints)).BeginInit();
            this.CategoriesTab.SuspendLayout();
            this.ItemInvetoryTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInventoryItems)).BeginInit();
            this.SupplierTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSupplier)).BeginInit();
            this.BrandTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrand)).BeginInit();
            this.SuspendLayout();
            // 
            // AccumulationTabControl
            // 
            this.AccumulationTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccumulationTabControl.Controls.Add(this.DetailsTab);
            this.AccumulationTabControl.Controls.Add(this.CategoriesTab);
            this.AccumulationTabControl.Controls.Add(this.ItemInvetoryTab);
            this.AccumulationTabControl.Controls.Add(this.SupplierTab);
            this.AccumulationTabControl.Controls.Add(this.BrandTab);
            this.AccumulationTabControl.Location = new System.Drawing.Point(13, 13);
            this.AccumulationTabControl.Name = "AccumulationTabControl";
            this.AccumulationTabControl.SelectedIndex = 0;
            this.AccumulationTabControl.Size = new System.Drawing.Size(799, 539);
            this.AccumulationTabControl.TabIndex = 7;
            // 
            // DetailsTab
            // 
            this.DetailsTab.Controls.Add(this.btnFreeitems);
            this.DetailsTab.Controls.Add(this.cbPromoType);
            this.DetailsTab.Controls.Add(this.label7);
            this.DetailsTab.Controls.Add(this.pbCustomer);
            this.DetailsTab.Controls.Add(this.label6);
            this.DetailsTab.Controls.Add(this.cmbCustomerList);
            this.DetailsTab.Controls.Add(this.chkLoyaltyOnly);
            this.DetailsTab.Controls.Add(this.IsActive);
            this.DetailsTab.Controls.Add(this.txtPoints);
            this.DetailsTab.Controls.Add(this.cmbRewardName);
            this.DetailsTab.Controls.Add(this.dgAccumulationPoints);
            this.DetailsTab.Controls.Add(this.label3);
            this.DetailsTab.Controls.Add(this.label2);
            this.DetailsTab.Controls.Add(this.edate);
            this.DetailsTab.Controls.Add(this.sdate);
            this.DetailsTab.Controls.Add(this.label5);
            this.DetailsTab.Controls.Add(this.label4);
            this.DetailsTab.Controls.Add(this.label1);
            this.DetailsTab.Controls.Add(this.txtPromoCode);
            this.DetailsTab.Location = new System.Drawing.Point(4, 24);
            this.DetailsTab.Name = "DetailsTab";
            this.DetailsTab.Padding = new System.Windows.Forms.Padding(3);
            this.DetailsTab.Size = new System.Drawing.Size(791, 511);
            this.DetailsTab.TabIndex = 0;
            this.DetailsTab.Text = "Details";
            this.DetailsTab.UseVisualStyleBackColor = true;
            // 
            // btnFreeitems
            // 
            this.btnFreeitems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFreeitems.Location = new System.Drawing.Point(444, 106);
            this.btnFreeitems.Name = "btnFreeitems";
            this.btnFreeitems.Size = new System.Drawing.Size(109, 23);
            this.btnFreeitems.TabIndex = 187;
            this.btnFreeitems.Text = "Select Items";
            this.btnFreeitems.UseVisualStyleBackColor = true;
            this.btnFreeitems.Visible = false;
            this.btnFreeitems.Click += new System.EventHandler(this.btnFreeitems_Click);
            // 
            // cbPromoType
            // 
            this.cbPromoType.FormattingEnabled = true;
            this.cbPromoType.Items.AddRange(new object[] {
            "Default",
            "Buy Quantity ( X )",
            "Buy Amount ( X )"});
            this.cbPromoType.Location = new System.Drawing.Point(137, 20);
            this.cbPromoType.Name = "cbPromoType";
            this.cbPromoType.Size = new System.Drawing.Size(225, 23);
            this.cbPromoType.TabIndex = 186;
            this.cbPromoType.SelectedIndexChanged += new System.EventHandler(this.cbPromoType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 28);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 15);
            this.label7.TabIndex = 185;
            this.label7.Text = "Promo Type :";
            // 
            // pbCustomer
            // 
            this.pbCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCustomer.Image = ((System.Drawing.Image)(resources.GetObject("pbCustomer.Image")));
            this.pbCustomer.Location = new System.Drawing.Point(747, 57);
            this.pbCustomer.Name = "pbCustomer";
            this.pbCustomer.Size = new System.Drawing.Size(19, 19);
            this.pbCustomer.TabIndex = 184;
            this.pbCustomer.TabStop = false;
            this.pbCustomer.WaitOnLoad = true;
            this.pbCustomer.Click += new System.EventHandler(this.pbCustomer_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(617, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "For specific customer";
            // 
            // cmbCustomerList
            // 
            this.cmbCustomerList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomerList.FormattingEnabled = true;
            this.cmbCustomerList.Location = new System.Drawing.Point(620, 57);
            this.cmbCustomerList.Name = "cmbCustomerList";
            this.cmbCustomerList.Size = new System.Drawing.Size(121, 23);
            this.cmbCustomerList.TabIndex = 22;
            // 
            // chkLoyaltyOnly
            // 
            this.chkLoyaltyOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLoyaltyOnly.AutoSize = true;
            this.chkLoyaltyOnly.Location = new System.Drawing.Point(460, 39);
            this.chkLoyaltyOnly.Name = "chkLoyaltyOnly";
            this.chkLoyaltyOnly.Size = new System.Drawing.Size(151, 19);
            this.chkLoyaltyOnly.TabIndex = 21;
            this.chkLoyaltyOnly.Text = "For loyalty member";
            this.chkLoyaltyOnly.UseVisualStyleBackColor = true;
            // 
            // IsActive
            // 
            this.IsActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IsActive.AutoSize = true;
            this.IsActive.Location = new System.Drawing.Point(715, 117);
            this.IsActive.Name = "IsActive";
            this.IsActive.Size = new System.Drawing.Size(67, 19);
            this.IsActive.TabIndex = 20;
            this.IsActive.Text = "Active";
            this.IsActive.UseVisualStyleBackColor = true;
            // 
            // txtPoints
            // 
            this.txtPoints.DecimalPlaces = 2;
            this.txtPoints.Location = new System.Drawing.Point(137, 78);
            this.txtPoints.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.txtPoints.Minimum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            -2147483648});
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(225, 23);
            this.txtPoints.TabIndex = 19;
            this.txtPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbRewardName
            // 
            this.cmbRewardName.FormattingEnabled = true;
            this.cmbRewardName.Location = new System.Drawing.Point(137, 49);
            this.cmbRewardName.Name = "cmbRewardName";
            this.cmbRewardName.Size = new System.Drawing.Size(225, 23);
            this.cmbRewardName.TabIndex = 18;
            this.cmbRewardName.SelectedIndexChanged += new System.EventHandler(this.cmbRewardName_SelectedIndexChanged);
            // 
            // dgAccumulationPoints
            // 
            this.dgAccumulationPoints.AllowUserToAddRows = false;
            this.dgAccumulationPoints.AllowUserToDeleteRows = false;
            this.dgAccumulationPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAccumulationPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgAccumulationPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAccumulationPoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgAccumulationPoints.Location = new System.Drawing.Point(0, 140);
            this.dgAccumulationPoints.MultiSelect = false;
            this.dgAccumulationPoints.Name = "dgAccumulationPoints";
            this.dgAccumulationPoints.RowHeadersVisible = false;
            this.dgAccumulationPoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAccumulationPoints.Size = new System.Drawing.Size(791, 359);
            this.dgAccumulationPoints.TabIndex = 17;
            this.dgAccumulationPoints.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAccumulationPoints_CellClick);
            this.dgAccumulationPoints.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAccumulationPoints_CellDoubleClick);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(650, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "to";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Date Range :";
            // 
            // edate
            // 
            this.edate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edate.Location = new System.Drawing.Point(678, 6);
            this.edate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.edate.Name = "edate";
            this.edate.Size = new System.Drawing.Size(104, 23);
            this.edate.TabIndex = 13;
            // 
            // sdate
            // 
            this.sdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdate.Location = new System.Drawing.Point(538, 6);
            this.sdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sdate.Name = "sdate";
            this.sdate.Size = new System.Drawing.Size(104, 23);
            this.sdate.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Promo Code :";
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Location = new System.Drawing.Point(8, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(122, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Points :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Reward :";
            // 
            // txtPromoCode
            // 
            this.txtPromoCode.Location = new System.Drawing.Point(137, 107);
            this.txtPromoCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPromoCode.Name = "txtPromoCode";
            this.txtPromoCode.Size = new System.Drawing.Size(225, 23);
            this.txtPromoCode.TabIndex = 9;
            // 
            // CategoriesTab
            // 
            this.CategoriesTab.Controls.Add(this.treeCategory);
            this.CategoriesTab.Location = new System.Drawing.Point(4, 24);
            this.CategoriesTab.Name = "CategoriesTab";
            this.CategoriesTab.Padding = new System.Windows.Forms.Padding(3);
            this.CategoriesTab.Size = new System.Drawing.Size(729, 511);
            this.CategoriesTab.TabIndex = 1;
            this.CategoriesTab.Text = "Categories";
            this.CategoriesTab.UseVisualStyleBackColor = true;
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategory.Location = new System.Drawing.Point(3, 3);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(723, 505);
            this.treeCategory.TabIndex = 0;
            this.treeCategory.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeCategory_BeforeCheck);
            this.treeCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterCheck);
            // 
            // ItemInvetoryTab
            // 
            this.ItemInvetoryTab.Controls.Add(this.btnAddItem);
            this.ItemInvetoryTab.Controls.Add(this.btnRemoveItem);
            this.ItemInvetoryTab.Controls.Add(this.dgInventoryItems);
            this.ItemInvetoryTab.Location = new System.Drawing.Point(4, 24);
            this.ItemInvetoryTab.Name = "ItemInvetoryTab";
            this.ItemInvetoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.ItemInvetoryTab.Size = new System.Drawing.Size(791, 511);
            this.ItemInvetoryTab.TabIndex = 2;
            this.ItemInvetoryTab.Text = "Inventory Items";
            this.ItemInvetoryTab.UseVisualStyleBackColor = true;
            // 
            // dgInventoryItems
            // 
            this.dgInventoryItems.AllowUserToAddRows = false;
            this.dgInventoryItems.AllowUserToDeleteRows = false;
            this.dgInventoryItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgInventoryItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgInventoryItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInventoryItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.PartNumber,
            this.ItemName,
            this.ItemDescription,
            this.ItemSupplier,
            this.ItemBrand,
            this.ItemPrice});
            this.dgInventoryItems.Location = new System.Drawing.Point(3, 3);
            this.dgInventoryItems.Name = "dgInventoryItems";
            this.dgInventoryItems.RowHeadersVisible = false;
            this.dgInventoryItems.Size = new System.Drawing.Size(785, 464);
            this.dgInventoryItems.TabIndex = 0;
            this.dgInventoryItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgInventoryItems_CellDoubleClick);
            this.dgInventoryItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgInventoryItems_CellEndEdit);
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.Visible = false;
            // 
            // PartNumber
            // 
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.Name = "PartNumber";
            this.PartNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PartNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.HeaderText = "Name";
            this.ItemName.Name = "ItemName";
            // 
            // ItemDescription
            // 
            this.ItemDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemDescription.HeaderText = "Description";
            this.ItemDescription.Name = "ItemDescription";
            // 
            // ItemSupplier
            // 
            this.ItemSupplier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemSupplier.HeaderText = "Supplier";
            this.ItemSupplier.Name = "ItemSupplier";
            // 
            // ItemBrand
            // 
            this.ItemBrand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemBrand.HeaderText = "Brand";
            this.ItemBrand.Name = "ItemBrand";
            // 
            // ItemPrice
            // 
            this.ItemPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemPrice.HeaderText = "Price";
            this.ItemPrice.Name = "ItemPrice";
            // 
            // SupplierTab
            // 
            this.SupplierTab.BackColor = System.Drawing.Color.Transparent;
            this.SupplierTab.Controls.Add(this.dgSupplier);
            this.SupplierTab.Location = new System.Drawing.Point(4, 24);
            this.SupplierTab.Name = "SupplierTab";
            this.SupplierTab.Padding = new System.Windows.Forms.Padding(3);
            this.SupplierTab.Size = new System.Drawing.Size(729, 511);
            this.SupplierTab.TabIndex = 3;
            this.SupplierTab.Text = "Supplier";
            // 
            // dgSupplier
            // 
            this.dgSupplier.AllowUserToAddRows = false;
            this.dgSupplier.AllowUserToDeleteRows = false;
            this.dgSupplier.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSupplier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SupplierID,
            this.chkSupplier,
            this.SupplierName,
            this.SupplierAccount,
            this.SupplierPhone,
            this.SupplierCity});
            this.dgSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSupplier.Location = new System.Drawing.Point(3, 3);
            this.dgSupplier.Name = "dgSupplier";
            this.dgSupplier.RowHeadersVisible = false;
            this.dgSupplier.Size = new System.Drawing.Size(723, 505);
            this.dgSupplier.TabIndex = 0;
            // 
            // SupplierID
            // 
            this.SupplierID.HeaderText = "SupplierID";
            this.SupplierID.Name = "SupplierID";
            this.SupplierID.Visible = false;
            // 
            // chkSupplier
            // 
            this.chkSupplier.HeaderText = "";
            this.chkSupplier.Name = "chkSupplier";
            this.chkSupplier.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chkSupplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // SupplierName
            // 
            this.SupplierName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SupplierName.HeaderText = "Supplier Name";
            this.SupplierName.Name = "SupplierName";
            // 
            // SupplierAccount
            // 
            this.SupplierAccount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SupplierAccount.HeaderText = "Account";
            this.SupplierAccount.Name = "SupplierAccount";
            // 
            // SupplierPhone
            // 
            this.SupplierPhone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SupplierPhone.HeaderText = "Phone";
            this.SupplierPhone.Name = "SupplierPhone";
            // 
            // SupplierCity
            // 
            this.SupplierCity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SupplierCity.HeaderText = "City";
            this.SupplierCity.Name = "SupplierCity";
            // 
            // BrandTab
            // 
            this.BrandTab.Controls.Add(this.dgBrand);
            this.BrandTab.Location = new System.Drawing.Point(4, 24);
            this.BrandTab.Name = "BrandTab";
            this.BrandTab.Padding = new System.Windows.Forms.Padding(3);
            this.BrandTab.Size = new System.Drawing.Size(729, 511);
            this.BrandTab.TabIndex = 4;
            this.BrandTab.Text = "Brand";
            this.BrandTab.UseVisualStyleBackColor = true;
            // 
            // dgBrand
            // 
            this.dgBrand.AllowUserToAddRows = false;
            this.dgBrand.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgBrand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBrand.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkBrand,
            this.BrandName,
            this.ItemBrandName});
            this.dgBrand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBrand.Location = new System.Drawing.Point(3, 3);
            this.dgBrand.Name = "dgBrand";
            this.dgBrand.RowHeadersVisible = false;
            this.dgBrand.Size = new System.Drawing.Size(723, 505);
            this.dgBrand.TabIndex = 0;
            // 
            // chkBrand
            // 
            this.chkBrand.HeaderText = "";
            this.chkBrand.Name = "chkBrand";
            this.chkBrand.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // BrandName
            // 
            this.BrandName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BrandName.HeaderText = "Brand";
            this.BrandName.Name = "BrandName";
            // 
            // ItemBrandName
            // 
            this.ItemBrandName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemBrandName.HeaderText = "Item Name";
            this.ItemBrandName.Name = "ItemBrandName";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNew.Location = new System.Drawing.Point(17, 554);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(97, 42);
            this.btnAddNew.TabIndex = 8;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(120, 554);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(91, 42);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "        Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(620, 554);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(91, 42);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "    Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(717, 554);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 42);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "      Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(523, 554);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 42);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "        Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddItem.Location = new System.Drawing.Point(6, 482);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 4;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveItem.Location = new System.Drawing.Point(665, 482);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(111, 23);
            this.btnRemoveItem.TabIndex = 3;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // PointsAccumulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 606);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.AccumulationTabControl);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PointsAccumulation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promotions Setup";
            this.Load += new System.EventHandler(this.PointsAccumulation_Load);
            this.AccumulationTabControl.ResumeLayout(false);
            this.DetailsTab.ResumeLayout(false);
            this.DetailsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAccumulationPoints)).EndInit();
            this.CategoriesTab.ResumeLayout(false);
            this.ItemInvetoryTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInventoryItems)).EndInit();
            this.SupplierTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSupplier)).EndInit();
            this.BrandTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBrand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabControl AccumulationTabControl;
        private System.Windows.Forms.TabPage DetailsTab;
        private System.Windows.Forms.NumericUpDown txtPoints;
        private System.Windows.Forms.ComboBox cmbRewardName;
        private System.Windows.Forms.DataGridView dgAccumulationPoints;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker edate;
        private System.Windows.Forms.DateTimePicker sdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPromoCode;
        private System.Windows.Forms.TabPage CategoriesTab;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TabPage ItemInvetoryTab;
        private System.Windows.Forms.DataGridView dgInventoryItems;
        private System.Windows.Forms.TabPage SupplierTab;
        private System.Windows.Forms.DataGridView dgSupplier;
        private System.Windows.Forms.TabPage BrandTab;
        private System.Windows.Forms.DataGridView dgBrand;
        private System.Windows.Forms.CheckBox IsActive;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemBrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierCity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCustomerList;
        private System.Windows.Forms.CheckBox chkLoyaltyOnly;
        private System.Windows.Forms.PictureBox pbCustomer;
        private System.Windows.Forms.ComboBox cbPromoType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFreeitems;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemPrice;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
    }
}