namespace AbleRetailPOS.Inventory
{
    partial class RedeemItem
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.rdoPartNumber = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoItemNumber = new System.Windows.Forms.RadioButton();
            this.rdoSupplierItemNumber = new System.Windows.Forms.RadioButton();
            this.rdoItemDescription = new System.Windows.Forms.RadioButton();
            this.rdoItemName = new System.Windows.Forms.RadioButton();
            this.rdoSupplierName = new System.Windows.Forms.RadioButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgridRedeemItems = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPointsReq = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRedeemType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGCAmount = new System.Windows.Forms.TextBox();
            this.gpbValidity = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridRedeemItems)).BeginInit();
            this.gpbValidity.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgridItems
            // 
            this.dgridItems.AllowUserToAddRows = false;
            this.dgridItems.AllowUserToDeleteRows = false;
            this.dgridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridItems.BackgroundColor = System.Drawing.Color.White;
            this.dgridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgridItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridItems.Location = new System.Drawing.Point(2, 22);
            this.dgridItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgridItems.MultiSelect = false;
            this.dgridItems.Name = "dgridItems";
            this.dgridItems.RowHeadersVisible = false;
            this.dgridItems.RowTemplate.Height = 24;
            this.dgridItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridItems.Size = new System.Drawing.Size(380, 486);
            this.dgridItems.TabIndex = 1;
            this.dgridItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellDoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(817, 18);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(66, 29);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rdoPartNumber
            // 
            this.rdoPartNumber.AutoSize = true;
            this.rdoPartNumber.Location = new System.Drawing.Point(79, 11);
            this.rdoPartNumber.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPartNumber.Name = "rdoPartNumber";
            this.rdoPartNumber.Size = new System.Drawing.Size(84, 17);
            this.rdoPartNumber.TabIndex = 3;
            this.rdoPartNumber.TabStop = true;
            this.rdoPartNumber.Text = "Part Number";
            this.rdoPartNumber.UseVisualStyleBackColor = true;
            this.rdoPartNumber.CheckedChanged += new System.EventHandler(this.rdoPartNumber_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search By:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rdoItemNumber
            // 
            this.rdoItemNumber.AutoSize = true;
            this.rdoItemNumber.Location = new System.Drawing.Point(165, 10);
            this.rdoItemNumber.Margin = new System.Windows.Forms.Padding(2);
            this.rdoItemNumber.Name = "rdoItemNumber";
            this.rdoItemNumber.Size = new System.Drawing.Size(85, 17);
            this.rdoItemNumber.TabIndex = 5;
            this.rdoItemNumber.TabStop = true;
            this.rdoItemNumber.Text = "Item Number";
            this.rdoItemNumber.UseVisualStyleBackColor = true;
            this.rdoItemNumber.CheckedChanged += new System.EventHandler(this.rdoItemNumber_CheckedChanged);
            // 
            // rdoSupplierItemNumber
            // 
            this.rdoSupplierItemNumber.AutoSize = true;
            this.rdoSupplierItemNumber.Location = new System.Drawing.Point(266, 11);
            this.rdoSupplierItemNumber.Margin = new System.Windows.Forms.Padding(2);
            this.rdoSupplierItemNumber.Name = "rdoSupplierItemNumber";
            this.rdoSupplierItemNumber.Size = new System.Drawing.Size(126, 17);
            this.rdoSupplierItemNumber.TabIndex = 6;
            this.rdoSupplierItemNumber.TabStop = true;
            this.rdoSupplierItemNumber.Text = "Supplier Item Number";
            this.rdoSupplierItemNumber.UseVisualStyleBackColor = true;
            this.rdoSupplierItemNumber.CheckedChanged += new System.EventHandler(this.rdoSupplierItemNumber_CheckedChanged);
            // 
            // rdoItemDescription
            // 
            this.rdoItemDescription.AutoSize = true;
            this.rdoItemDescription.Location = new System.Drawing.Point(165, 32);
            this.rdoItemDescription.Margin = new System.Windows.Forms.Padding(2);
            this.rdoItemDescription.Name = "rdoItemDescription";
            this.rdoItemDescription.Size = new System.Drawing.Size(101, 17);
            this.rdoItemDescription.TabIndex = 7;
            this.rdoItemDescription.TabStop = true;
            this.rdoItemDescription.Text = "Item Description";
            this.rdoItemDescription.UseVisualStyleBackColor = true;
            this.rdoItemDescription.CheckedChanged += new System.EventHandler(this.rdoItemDescription_CheckedChanged);
            // 
            // rdoItemName
            // 
            this.rdoItemName.AutoSize = true;
            this.rdoItemName.Location = new System.Drawing.Point(79, 32);
            this.rdoItemName.Margin = new System.Windows.Forms.Padding(2);
            this.rdoItemName.Name = "rdoItemName";
            this.rdoItemName.Size = new System.Drawing.Size(76, 17);
            this.rdoItemName.TabIndex = 8;
            this.rdoItemName.TabStop = true;
            this.rdoItemName.Text = "Item Name";
            this.rdoItemName.UseVisualStyleBackColor = true;
            this.rdoItemName.CheckedChanged += new System.EventHandler(this.rdoItemName_CheckedChanged);
            // 
            // rdoSupplierName
            // 
            this.rdoSupplierName.AutoSize = true;
            this.rdoSupplierName.Location = new System.Drawing.Point(266, 32);
            this.rdoSupplierName.Margin = new System.Windows.Forms.Padding(2);
            this.rdoSupplierName.Name = "rdoSupplierName";
            this.rdoSupplierName.Size = new System.Drawing.Size(89, 17);
            this.rdoSupplierName.TabIndex = 9;
            this.rdoSupplierName.TabStop = true;
            this.rdoSupplierName.Text = "Main Supplier";
            this.rdoSupplierName.UseVisualStyleBackColor = true;
            this.rdoSupplierName.CheckedChanged += new System.EventHandler(this.rdoSupplierName_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(426, 23);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(387, 20);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgridRedeemItems
            // 
            this.dgridRedeemItems.AllowUserToAddRows = false;
            this.dgridRedeemItems.AllowUserToDeleteRows = false;
            this.dgridRedeemItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridRedeemItems.BackgroundColor = System.Drawing.Color.White;
            this.dgridRedeemItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridRedeemItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgridRedeemItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridRedeemItems.Location = new System.Drawing.Point(539, 22);
            this.dgridRedeemItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgridRedeemItems.MultiSelect = false;
            this.dgridRedeemItems.Name = "dgridRedeemItems";
            this.dgridRedeemItems.RowHeadersVisible = false;
            this.dgridRedeemItems.RowTemplate.Height = 24;
            this.dgridRedeemItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridRedeemItems.Size = new System.Drawing.Size(381, 486);
            this.dgridRedeemItems.TabIndex = 11;
            this.dgridRedeemItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridRedeemItems_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Items";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(540, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Available items for redemption";
            // 
            // txtPointsReq
            // 
            this.txtPointsReq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPointsReq.Location = new System.Drawing.Point(9, 76);
            this.txtPointsReq.Name = "txtPointsReq";
            this.txtPointsReq.Size = new System.Drawing.Size(132, 20);
            this.txtPointsReq.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Points required";
            // 
            // cmbRedeemType
            // 
            this.cmbRedeemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRedeemType.FormattingEnabled = true;
            this.cmbRedeemType.Items.AddRange(new object[] {
            "Gift Certificate",
            "Item"});
            this.cmbRedeemType.Location = new System.Drawing.Point(9, 32);
            this.cmbRedeemType.Name = "cmbRedeemType";
            this.cmbRedeemType.Size = new System.Drawing.Size(132, 21);
            this.cmbRedeemType.TabIndex = 20;
            this.cmbRedeemType.SelectedIndexChanged += new System.EventHandler(this.cmbRedeemType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(7, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "GC amount";
            // 
            // txtGCAmount
            // 
            this.txtGCAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGCAmount.Enabled = false;
            this.txtGCAmount.Location = new System.Drawing.Point(9, 119);
            this.txtGCAmount.Name = "txtGCAmount";
            this.txtGCAmount.Size = new System.Drawing.Size(132, 20);
            this.txtGCAmount.TabIndex = 22;
            // 
            // gpbValidity
            // 
            this.gpbValidity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbValidity.Controls.Add(this.label8);
            this.gpbValidity.Controls.Add(this.label7);
            this.gpbValidity.Controls.Add(this.dtpEndDate);
            this.gpbValidity.Controls.Add(this.dtpStartDate);
            this.gpbValidity.Enabled = false;
            this.gpbValidity.Location = new System.Drawing.Point(7, 147);
            this.gpbValidity.Name = "gpbValidity";
            this.gpbValidity.Size = new System.Drawing.Size(134, 112);
            this.gpbValidity.TabIndex = 24;
            this.gpbValidity.TabStop = false;
            this.gpbValidity.Text = "GC validity";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(7, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "End date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(7, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Start date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(3, 76);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(131, 20);
            this.dtpEndDate.TabIndex = 1;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(3, 33);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(131, 20);
            this.dtpStartDate.TabIndex = 0;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Image = global::AbleRetailPOS.Properties.Resources.edit24;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpdate.Location = new System.Drawing.Point(10, 320);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(131, 45);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::AbleRetailPOS.Properties.Resources.Delete24;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(9, 371);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(132, 45);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::AbleRetailPOS.Properties.Resources.Add24;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(9, 269);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(132, 45);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::AbleRetailPOS.Properties.Resources.clear24;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(832, 570);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 40);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgridItems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgridRedeemItems, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 54);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(922, 510);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.gpbValidity);
            this.groupBox1.Controls.Add(this.txtPointsReq);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtGCAmount);
            this.groupBox1.Controls.Add(this.cmbRedeemType);
            this.groupBox1.Location = new System.Drawing.Point(387, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 484);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // RedeemItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 622);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.rdoSupplierName);
            this.Controls.Add(this.rdoItemName);
            this.Controls.Add(this.rdoItemDescription);
            this.Controls.Add(this.rdoSupplierItemNumber);
            this.Controls.Add(this.rdoItemNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdoPartNumber);
            this.Controls.Add(this.btnSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RedeemItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Redeemable Items";
            this.Load += new System.EventHandler(this.ItemLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridRedeemItems)).EndInit();
            this.gpbValidity.ResumeLayout(false);
            this.gpbValidity.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgridItems;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton rdoPartNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoItemNumber;
        private System.Windows.Forms.RadioButton rdoSupplierItemNumber;
        private System.Windows.Forms.RadioButton rdoItemDescription;
        private System.Windows.Forms.RadioButton rdoItemName;
        private System.Windows.Forms.RadioButton rdoSupplierName;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgridRedeemItems;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPointsReq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRedeemType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGCAmount;
        private System.Windows.Forms.GroupBox gpbValidity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}