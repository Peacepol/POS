namespace RestaurantPOS.Inventory
{
    partial class ItemPriceUpdate
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
            this.SelectedItem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCostEx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AverageCostEx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnLoadAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDiscountFrmBase = new System.Windows.Forms.CheckBox();
            this.chkFixedPrice = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPriceLevel = new System.Windows.Forms.ComboBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnSelected = new System.Windows.Forms.Button();
            this.chkRound99 = new System.Windows.Forms.CheckBox();
            this.chkRound5 = new System.Windows.Forms.CheckBox();
            this.chkNoRounding = new System.Windows.Forms.CheckBox();
            this.DiscountFromBaseAmt = new System.Windows.Forms.NumericUpDown();
            this.FixedPriceAmount = new System.Windows.Forms.NumericUpDown();
            this.txtFixed = new System.Windows.Forms.NumericUpDown();
            this.txtMarkup = new System.Windows.Forms.NumericUpDown();
            this.txtMargin = new System.Windows.Forms.NumericUpDown();
            this.chkFixed = new System.Windows.Forms.CheckBox();
            this.chkMarkup = new System.Windows.Forms.CheckBox();
            this.chkMargin = new System.Windows.Forms.CheckBox();
            this.rdoAC = new System.Windows.Forms.RadioButton();
            this.rdoLC = new System.Windows.Forms.RadioButton();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnRecordALL = new System.Windows.Forms.Button();
            this.rbSalePrice = new System.Windows.Forms.RadioButton();
            this.rbRegPrice = new System.Windows.Forms.RadioButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbSaleDate = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiscountFromBaseAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FixedPriceAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFixed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMargin)).BeginInit();
            this.gbSaleDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgridItems
            // 
            this.dgridItems.AllowUserToAddRows = false;
            this.dgridItems.AllowUserToDeleteRows = false;
            this.dgridItems.AllowUserToOrderColumns = true;
            this.dgridItems.AllowUserToResizeColumns = false;
            this.dgridItems.AllowUserToResizeRows = false;
            this.dgridItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedItem,
            this.ItemID,
            this.PartNumber,
            this.LocationID,
            this.ItemName,
            this.LastCostEx,
            this.AverageCostEx,
            this.TaxRate});
            this.dgridItems.Location = new System.Drawing.Point(12, 159);
            this.dgridItems.Name = "dgridItems";
            this.dgridItems.RowHeadersVisible = false;
            this.dgridItems.Size = new System.Drawing.Size(1152, 527);
            this.dgridItems.TabIndex = 1;
            // 
            // SelectedItem
            // 
            this.SelectedItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SelectedItem.HeaderText = "Selected";
            this.SelectedItem.Name = "SelectedItem";
            this.SelectedItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectedItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SelectedItem.Width = 55;
            // 
            // ItemID
            // 
            this.ItemID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemID.FillWeight = 51.05001F;
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Visible = false;
            // 
            // PartNumber
            // 
            this.PartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNumber.FillWeight = 51.05001F;
            this.PartNumber.HeaderText = "PartNumber";
            this.PartNumber.Name = "PartNumber";
            this.PartNumber.ReadOnly = true;
            // 
            // LocationID
            // 
            this.LocationID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocationID.FillWeight = 51.05001F;
            this.LocationID.HeaderText = "LocationID";
            this.LocationID.Name = "LocationID";
            this.LocationID.ReadOnly = true;
            this.LocationID.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.FillWeight = 51.05001F;
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // LastCostEx
            // 
            this.LastCostEx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LastCostEx.FillWeight = 51.05001F;
            this.LastCostEx.HeaderText = "LastCostEx";
            this.LastCostEx.Name = "LastCostEx";
            this.LastCostEx.ReadOnly = true;
            // 
            // AverageCostEx
            // 
            this.AverageCostEx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AverageCostEx.FillWeight = 51.05001F;
            this.AverageCostEx.HeaderText = "AverageCostEx";
            this.AverageCostEx.Name = "AverageCostEx";
            this.AverageCostEx.ReadOnly = true;
            // 
            // TaxRate
            // 
            this.TaxRate.HeaderText = "TaxRate";
            this.TaxRate.Name = "TaxRate";
            this.TaxRate.ReadOnly = true;
            this.TaxRate.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(64, 14);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(139, 36);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Load Selected Items";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnLoadAll
            // 
            this.btnLoadAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoadAll.ForeColor = System.Drawing.Color.Black;
            this.btnLoadAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadAll.Location = new System.Drawing.Point(64, 62);
            this.btnLoadAll.Name = "btnLoadAll";
            this.btnLoadAll.Size = new System.Drawing.Size(139, 34);
            this.btnLoadAll.TabIndex = 233;
            this.btnLoadAll.Text = "Load All Items";
            this.btnLoadAll.UseVisualStyleBackColor = true;
            this.btnLoadAll.Click += new System.EventHandler(this.btnLoadAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDiscountFrmBase);
            this.groupBox1.Controls.Add(this.chkFixedPrice);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboPriceLevel);
            this.groupBox1.Controls.Add(this.btnAll);
            this.groupBox1.Controls.Add(this.btnSelected);
            this.groupBox1.Controls.Add(this.chkRound99);
            this.groupBox1.Controls.Add(this.chkRound5);
            this.groupBox1.Controls.Add(this.chkNoRounding);
            this.groupBox1.Controls.Add(this.DiscountFromBaseAmt);
            this.groupBox1.Controls.Add(this.FixedPriceAmount);
            this.groupBox1.Controls.Add(this.txtFixed);
            this.groupBox1.Controls.Add(this.txtMarkup);
            this.groupBox1.Controls.Add(this.txtMargin);
            this.groupBox1.Controls.Add(this.chkFixed);
            this.groupBox1.Controls.Add(this.chkMarkup);
            this.groupBox1.Controls.Add(this.chkMargin);
            this.groupBox1.Controls.Add(this.rdoAC);
            this.groupBox1.Controls.Add(this.rdoLC);
            this.groupBox1.Location = new System.Drawing.Point(255, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(688, 143);
            this.groupBox1.TabIndex = 234;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calculation Basis";
            // 
            // chkDiscountFrmBase
            // 
            this.chkDiscountFrmBase.AutoSize = true;
            this.chkDiscountFrmBase.Location = new System.Drawing.Point(162, 118);
            this.chkDiscountFrmBase.Name = "chkDiscountFrmBase";
            this.chkDiscountFrmBase.Size = new System.Drawing.Size(121, 17);
            this.chkDiscountFrmBase.TabIndex = 16;
            this.chkDiscountFrmBase.Text = "Discount From Base";
            this.chkDiscountFrmBase.UseVisualStyleBackColor = true;
            this.chkDiscountFrmBase.CheckedChanged += new System.EventHandler(this.chkDiscountFrmBase_CheckedChanged);
            // 
            // chkFixedPrice
            // 
            this.chkFixedPrice.AutoSize = true;
            this.chkFixedPrice.Location = new System.Drawing.Point(162, 92);
            this.chkFixedPrice.Name = "chkFixedPrice";
            this.chkFixedPrice.Size = new System.Drawing.Size(78, 17);
            this.chkFixedPrice.TabIndex = 15;
            this.chkFixedPrice.Text = "Fixed Price";
            this.chkFixedPrice.UseVisualStyleBackColor = true;
            this.chkFixedPrice.CheckedChanged += new System.EventHandler(this.chkFixedPrice_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(548, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Price Level To Update:";
            // 
            // cboPriceLevel
            // 
            this.cboPriceLevel.FormattingEnabled = true;
            this.cboPriceLevel.Items.AddRange(new object[] {
            "Level0",
            "Level1",
            "Level2",
            "Level3",
            "Level4",
            "Level5",
            "Level6",
            "Level7",
            "Level8",
            "Level9",
            "Level10",
            "Level11",
            "Level12"});
            this.cboPriceLevel.Location = new System.Drawing.Point(547, 42);
            this.cboPriceLevel.Name = "cboPriceLevel";
            this.cboPriceLevel.Size = new System.Drawing.Size(121, 21);
            this.cboPriceLevel.TabIndex = 13;
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(383, 100);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(111, 23);
            this.btnAll.TabIndex = 12;
            this.btnAll.Text = "Apply To All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnSelected
            // 
            this.btnSelected.Location = new System.Drawing.Point(517, 100);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(111, 23);
            this.btnSelected.TabIndex = 11;
            this.btnSelected.Text = "Apply to Selected";
            this.btnSelected.UseVisualStyleBackColor = true;
            this.btnSelected.Click += new System.EventHandler(this.btnSelected_Click);
            // 
            // chkRound99
            // 
            this.chkRound99.AutoSize = true;
            this.chkRound99.Location = new System.Drawing.Point(374, 69);
            this.chkRound99.Name = "chkRound99";
            this.chkRound99.Size = new System.Drawing.Size(120, 17);
            this.chkRound99.TabIndex = 10;
            this.chkRound99.Text = "Always end with .99";
            this.chkRound99.UseVisualStyleBackColor = true;
            this.chkRound99.CheckedChanged += new System.EventHandler(this.chkRound99_CheckedChanged);
            // 
            // chkRound5
            // 
            this.chkRound5.AutoSize = true;
            this.chkRound5.Location = new System.Drawing.Point(374, 46);
            this.chkRound5.Name = "chkRound5";
            this.chkRound5.Size = new System.Drawing.Size(149, 17);
            this.chkRound5.TabIndex = 9;
            this.chkRound5.Text = "Round to Nearest 5 Cents";
            this.chkRound5.UseVisualStyleBackColor = true;
            this.chkRound5.CheckedChanged += new System.EventHandler(this.chkRound5_CheckedChanged);
            // 
            // chkNoRounding
            // 
            this.chkNoRounding.AutoSize = true;
            this.chkNoRounding.Checked = true;
            this.chkNoRounding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNoRounding.Location = new System.Drawing.Point(374, 23);
            this.chkNoRounding.Name = "chkNoRounding";
            this.chkNoRounding.Size = new System.Drawing.Size(89, 17);
            this.chkNoRounding.TabIndex = 8;
            this.chkNoRounding.Text = "No Rounding";
            this.chkNoRounding.UseVisualStyleBackColor = true;
            this.chkNoRounding.CheckedChanged += new System.EventHandler(this.chkNoRounding_CheckedChanged);
            // 
            // DiscountFromBaseAmt
            // 
            this.DiscountFromBaseAmt.DecimalPlaces = 2;
            this.DiscountFromBaseAmt.Location = new System.Drawing.Point(289, 117);
            this.DiscountFromBaseAmt.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.DiscountFromBaseAmt.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.DiscountFromBaseAmt.Name = "DiscountFromBaseAmt";
            this.DiscountFromBaseAmt.Size = new System.Drawing.Size(77, 20);
            this.DiscountFromBaseAmt.TabIndex = 7;
            // 
            // FixedPriceAmount
            // 
            this.FixedPriceAmount.DecimalPlaces = 2;
            this.FixedPriceAmount.Location = new System.Drawing.Point(289, 91);
            this.FixedPriceAmount.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.FixedPriceAmount.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.FixedPriceAmount.Name = "FixedPriceAmount";
            this.FixedPriceAmount.Size = new System.Drawing.Size(77, 20);
            this.FixedPriceAmount.TabIndex = 6;
            // 
            // txtFixed
            // 
            this.txtFixed.DecimalPlaces = 2;
            this.txtFixed.Location = new System.Drawing.Point(289, 68);
            this.txtFixed.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtFixed.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.txtFixed.Name = "txtFixed";
            this.txtFixed.Size = new System.Drawing.Size(77, 20);
            this.txtFixed.TabIndex = 7;
            // 
            // txtMarkup
            // 
            this.txtMarkup.DecimalPlaces = 2;
            this.txtMarkup.Location = new System.Drawing.Point(289, 42);
            this.txtMarkup.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtMarkup.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.txtMarkup.Name = "txtMarkup";
            this.txtMarkup.Size = new System.Drawing.Size(77, 20);
            this.txtMarkup.TabIndex = 6;
            // 
            // txtMargin
            // 
            this.txtMargin.DecimalPlaces = 2;
            this.txtMargin.Location = new System.Drawing.Point(289, 16);
            this.txtMargin.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.txtMargin.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.txtMargin.Name = "txtMargin";
            this.txtMargin.Size = new System.Drawing.Size(77, 20);
            this.txtMargin.TabIndex = 5;
            // 
            // chkFixed
            // 
            this.chkFixed.AutoSize = true;
            this.chkFixed.Location = new System.Drawing.Point(162, 67);
            this.chkFixed.Name = "chkFixed";
            this.chkFixed.Size = new System.Drawing.Size(108, 17);
            this.chkFixed.TabIndex = 4;
            this.chkFixed.Text = "Fixed Gross Profit";
            this.chkFixed.UseVisualStyleBackColor = true;
            this.chkFixed.CheckedChanged += new System.EventHandler(this.chkFixed_CheckedChanged);
            // 
            // chkMarkup
            // 
            this.chkMarkup.AutoSize = true;
            this.chkMarkup.Location = new System.Drawing.Point(162, 42);
            this.chkMarkup.Name = "chkMarkup";
            this.chkMarkup.Size = new System.Drawing.Size(102, 17);
            this.chkMarkup.TabIndex = 3;
            this.chkMarkup.Text = "Percent Markup";
            this.chkMarkup.UseVisualStyleBackColor = true;
            this.chkMarkup.CheckedChanged += new System.EventHandler(this.chkMarkup_CheckedChanged);
            // 
            // chkMargin
            // 
            this.chkMargin.AutoSize = true;
            this.chkMargin.Checked = true;
            this.chkMargin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMargin.Location = new System.Drawing.Point(162, 14);
            this.chkMargin.Name = "chkMargin";
            this.chkMargin.Size = new System.Drawing.Size(98, 17);
            this.chkMargin.TabIndex = 2;
            this.chkMargin.Text = "Percent Margin";
            this.chkMargin.UseVisualStyleBackColor = true;
            this.chkMargin.CheckedChanged += new System.EventHandler(this.chkMargin_CheckedChanged);
            // 
            // rdoAC
            // 
            this.rdoAC.AutoSize = true;
            this.rdoAC.Checked = true;
            this.rdoAC.Location = new System.Drawing.Point(13, 56);
            this.rdoAC.Name = "rdoAC";
            this.rdoAC.Size = new System.Drawing.Size(139, 17);
            this.rdoAC.TabIndex = 1;
            this.rdoAC.TabStop = true;
            this.rdoAC.Text = "Base from Average Cost";
            this.rdoAC.UseVisualStyleBackColor = true;
            // 
            // rdoLC
            // 
            this.rdoLC.AutoSize = true;
            this.rdoLC.Location = new System.Drawing.Point(13, 27);
            this.rdoLC.Name = "rdoLC";
            this.rdoLC.Size = new System.Drawing.Size(119, 17);
            this.rdoLC.TabIndex = 0;
            this.rdoLC.Text = "Base from Last Cost";
            this.rdoLC.UseVisualStyleBackColor = true;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.DarkRed;
            this.lblNote.Location = new System.Drawing.Point(22, 99);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(0, 13);
            this.lblNote.TabIndex = 235;
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecord.Location = new System.Drawing.Point(949, 703);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(215, 34);
            this.btnRecord.TabIndex = 236;
            this.btnRecord.Text = "Save Prices For Selected Items";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnRecordALL
            // 
            this.btnRecordALL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecordALL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRecordALL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRecordALL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecordALL.Location = new System.Drawing.Point(838, 703);
            this.btnRecordALL.Name = "btnRecordALL";
            this.btnRecordALL.Size = new System.Drawing.Size(105, 34);
            this.btnRecordALL.TabIndex = 237;
            this.btnRecordALL.Text = "Save All Prices";
            this.btnRecordALL.UseVisualStyleBackColor = true;
            this.btnRecordALL.Click += new System.EventHandler(this.btnRecordALL_Click);
            // 
            // rbSalePrice
            // 
            this.rbSalePrice.AutoSize = true;
            this.rbSalePrice.Location = new System.Drawing.Point(1060, 24);
            this.rbSalePrice.Name = "rbSalePrice";
            this.rbSalePrice.Size = new System.Drawing.Size(76, 17);
            this.rbSalePrice.TabIndex = 16;
            this.rbSalePrice.Text = " Sale Price";
            this.rbSalePrice.UseVisualStyleBackColor = true;
            this.rbSalePrice.CheckedChanged += new System.EventHandler(this.rbSalePrice_CheckedChanged);
            // 
            // rbRegPrice
            // 
            this.rbRegPrice.AutoSize = true;
            this.rbRegPrice.Checked = true;
            this.rbRegPrice.Location = new System.Drawing.Point(967, 24);
            this.rbRegPrice.Name = "rbRegPrice";
            this.rbRegPrice.Size = new System.Drawing.Size(89, 17);
            this.rbRegPrice.TabIndex = 15;
            this.rbRegPrice.TabStop = true;
            this.rbRegPrice.Text = "Regular Price";
            this.rbRegPrice.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(64, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(107, 20);
            this.dateTimePicker1.TabIndex = 238;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(64, 71);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(107, 20);
            this.dateTimePicker2.TabIndex = 239;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Start Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 240;
            this.label3.Text = "End Date";
            // 
            // gbSaleDate
            // 
            this.gbSaleDate.Controls.Add(this.label3);
            this.gbSaleDate.Controls.Add(this.dateTimePicker1);
            this.gbSaleDate.Controls.Add(this.label1);
            this.gbSaleDate.Controls.Add(this.dateTimePicker2);
            this.gbSaleDate.Location = new System.Drawing.Point(964, 47);
            this.gbSaleDate.Name = "gbSaleDate";
            this.gbSaleDate.Size = new System.Drawing.Size(200, 100);
            this.gbSaleDate.TabIndex = 241;
            this.gbSaleDate.TabStop = false;
            this.gbSaleDate.Text = "Sale Date";
            this.gbSaleDate.Visible = false;
            // 
            // ItemPriceUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 749);
            this.Controls.Add(this.gbSaleDate);
            this.Controls.Add(this.rbSalePrice);
            this.Controls.Add(this.rbRegPrice);
            this.Controls.Add(this.btnRecordALL);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoadAll);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgridItems);
            this.Name = "ItemPriceUpdate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price Update";
            this.Load += new System.EventHandler(this.ItemPriceUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiscountFromBaseAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FixedPriceAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFixed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMargin)).EndInit();
            this.gbSaleDate.ResumeLayout(false);
            this.gbSaleDate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridItems;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLoadAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRound99;
        private System.Windows.Forms.CheckBox chkRound5;
        private System.Windows.Forms.CheckBox chkNoRounding;
        private System.Windows.Forms.NumericUpDown txtFixed;
        private System.Windows.Forms.NumericUpDown txtMarkup;
        private System.Windows.Forms.NumericUpDown txtMargin;
        private System.Windows.Forms.CheckBox chkFixed;
        private System.Windows.Forms.CheckBox chkMarkup;
        private System.Windows.Forms.CheckBox chkMargin;
        private System.Windows.Forms.RadioButton rdoAC;
        private System.Windows.Forms.RadioButton rdoLC;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnSelected;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboPriceLevel;
        private System.Windows.Forms.Button btnRecordALL;
        private System.Windows.Forms.RadioButton rbSalePrice;
        private System.Windows.Forms.RadioButton rbRegPrice;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbSaleDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCostEx;
        private System.Windows.Forms.DataGridViewTextBoxColumn AverageCostEx;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxRate;
        private System.Windows.Forms.CheckBox chkDiscountFrmBase;
        private System.Windows.Forms.CheckBox chkFixedPrice;
        private System.Windows.Forms.NumericUpDown DiscountFromBaseAmt;
        private System.Windows.Forms.NumericUpDown FixedPriceAmount;
    }
}