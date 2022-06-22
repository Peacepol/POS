namespace AbleRetailPOS
{
    partial class TransactionsLookup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionsLookup));
            this.TabLookup = new System.Windows.Forms.TabControl();
            this.TabCustomer = new System.Windows.Forms.TabPage();
            this.dgCustomer = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblCustomerID = new System.Windows.Forms.Label();
            this.pbCustomer = new System.Windows.Forms.PictureBox();
            this.btnSearchCustomer = new System.Windows.Forms.Button();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.CusEDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.CusSDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.rdoCustomer = new System.Windows.Forms.RadioButton();
            this.rdoAllCustomers = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.TabSupplier = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblSupplierID = new System.Windows.Forms.Label();
            this.pbSupplier = new System.Windows.Forms.PictureBox();
            this.btnSearchSuppliers = new System.Windows.Forms.Button();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.SupEDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.SupSDate = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.rdoSupplier = new System.Windows.Forms.RadioButton();
            this.rdoAllSuppliers = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.dgSup = new System.Windows.Forms.DataGridView();
            this.TabItem = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblItemID = new System.Windows.Forms.Label();
            this.pbItem = new System.Windows.Forms.PictureBox();
            this.btnSearchItems = new System.Windows.Forms.Button();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.ItemEDate = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.ItemSDate = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.rdoItem = new System.Windows.Forms.RadioButton();
            this.rdoAllItems = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.TabInvoice = new System.Windows.Forms.TabPage();
            this.dgInvoice = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowInvoice = new System.Windows.Forms.Button();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.InvEDate = new System.Windows.Forms.DateTimePicker();
            this.edate = new System.Windows.Forms.Label();
            this.InvSDate = new System.Windows.Forms.DateTimePicker();
            this.sdate = new System.Windows.Forms.Label();
            this.rdoPO = new System.Windows.Forms.RadioButton();
            this.rdoInvoiceNo = new System.Windows.Forms.RadioButton();
            this.rdoAllInvoices = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.TabLookup.SuspendLayout();
            this.TabCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomer)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).BeginInit();
            this.TabSupplier.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSup)).BeginInit();
            this.TabItem.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.TabInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabLookup
            // 
            this.TabLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabLookup.Controls.Add(this.TabCustomer);
            this.TabLookup.Controls.Add(this.TabSupplier);
            this.TabLookup.Controls.Add(this.TabItem);
            this.TabLookup.Controls.Add(this.TabInvoice);
            this.TabLookup.Location = new System.Drawing.Point(13, 13);
            this.TabLookup.Name = "TabLookup";
            this.TabLookup.SelectedIndex = 0;
            this.TabLookup.Size = new System.Drawing.Size(883, 636);
            this.TabLookup.TabIndex = 0;
            // 
            // TabCustomer
            // 
            this.TabCustomer.Controls.Add(this.dgCustomer);
            this.TabCustomer.Controls.Add(this.groupBox4);
            this.TabCustomer.Location = new System.Drawing.Point(4, 22);
            this.TabCustomer.Name = "TabCustomer";
            this.TabCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.TabCustomer.Size = new System.Drawing.Size(875, 610);
            this.TabCustomer.TabIndex = 1;
            this.TabCustomer.Text = "Customer";
            this.TabCustomer.UseVisualStyleBackColor = true;
            // 
            // dgCustomer
            // 
            this.dgCustomer.AllowUserToAddRows = false;
            this.dgCustomer.AllowUserToDeleteRows = false;
            this.dgCustomer.AllowUserToOrderColumns = true;
            this.dgCustomer.AllowUserToResizeColumns = false;
            this.dgCustomer.AllowUserToResizeRows = false;
            this.dgCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgCustomer.BackgroundColor = System.Drawing.Color.White;
            this.dgCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCustomer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgCustomer.Location = new System.Drawing.Point(10, 83);
            this.dgCustomer.Name = "dgCustomer";
            this.dgCustomer.RowHeadersVisible = false;
            this.dgCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCustomer.Size = new System.Drawing.Size(855, 510);
            this.dgCustomer.TabIndex = 6;
            this.dgCustomer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCustomer_CellDoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.lblCustomerID);
            this.groupBox4.Controls.Add(this.pbCustomer);
            this.groupBox4.Controls.Add(this.btnSearchCustomer);
            this.groupBox4.Controls.Add(this.txtCustomer);
            this.groupBox4.Controls.Add(this.CusEDate);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.CusSDate);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.rdoCustomer);
            this.groupBox4.Controls.Add(this.rdoAllCustomers);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(10, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(855, 80);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.AutoSize = true;
            this.lblCustomerID.Location = new System.Drawing.Point(357, 45);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(0, 13);
            this.lblCustomerID.TabIndex = 195;
            // 
            // pbCustomer
            // 
            this.pbCustomer.Image = ((System.Drawing.Image)(resources.GetObject("pbCustomer.Image")));
            this.pbCustomer.Location = new System.Drawing.Point(574, 15);
            this.pbCustomer.Name = "pbCustomer";
            this.pbCustomer.Size = new System.Drawing.Size(18, 19);
            this.pbCustomer.TabIndex = 194;
            this.pbCustomer.TabStop = false;
            this.pbCustomer.Click += new System.EventHandler(this.pbCustomer_Click);
            // 
            // btnSearchCustomer
            // 
            this.btnSearchCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchCustomer.Image = global::AbleRetailPOS.Properties.Resources.search32;
            this.btnSearchCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchCustomer.Location = new System.Drawing.Point(700, 15);
            this.btnSearchCustomer.Name = "btnSearchCustomer";
            this.btnSearchCustomer.Size = new System.Drawing.Size(99, 46);
            this.btnSearchCustomer.TabIndex = 193;
            this.btnSearchCustomer.Text = "Search";
            this.btnSearchCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchCustomer.UseVisualStyleBackColor = true;
            this.btnSearchCustomer.Click += new System.EventHandler(this.btnSearchCustomer_Click);
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(242, 15);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(326, 20);
            this.txtCustomer.TabIndex = 192;
            // 
            // CusEDate
            // 
            this.CusEDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.CusEDate.Location = new System.Drawing.Point(231, 45);
            this.CusEDate.Name = "CusEDate";
            this.CusEDate.Size = new System.Drawing.Size(106, 20);
            this.CusEDate.TabIndex = 191;
            this.CusEDate.ValueChanged += new System.EventHandler(this.CusEDate_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(206, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 190;
            this.label10.Text = "to:";
            // 
            // CusSDate
            // 
            this.CusSDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.CusSDate.Location = new System.Drawing.Point(80, 45);
            this.CusSDate.Name = "CusSDate";
            this.CusSDate.Size = new System.Drawing.Size(98, 20);
            this.CusSDate.TabIndex = 189;
            this.CusSDate.ValueChanged += new System.EventHandler(this.CusSDate_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 188;
            this.label11.Text = "Date from:";
            // 
            // rdoCustomer
            // 
            this.rdoCustomer.AutoSize = true;
            this.rdoCustomer.Location = new System.Drawing.Point(176, 16);
            this.rdoCustomer.Name = "rdoCustomer";
            this.rdoCustomer.Size = new System.Drawing.Size(69, 17);
            this.rdoCustomer.TabIndex = 2;
            this.rdoCustomer.Text = "Customer";
            this.rdoCustomer.UseVisualStyleBackColor = true;
            this.rdoCustomer.CheckedChanged += new System.EventHandler(this.rdoCustomer_CheckedChanged);
            // 
            // rdoAllCustomers
            // 
            this.rdoAllCustomers.AutoSize = true;
            this.rdoAllCustomers.Checked = true;
            this.rdoAllCustomers.Location = new System.Drawing.Point(82, 16);
            this.rdoAllCustomers.Name = "rdoAllCustomers";
            this.rdoAllCustomers.Size = new System.Drawing.Size(88, 17);
            this.rdoAllCustomers.TabIndex = 1;
            this.rdoAllCustomers.TabStop = true;
            this.rdoAllCustomers.Text = "All Customers";
            this.rdoAllCustomers.UseVisualStyleBackColor = true;
            this.rdoAllCustomers.CheckedChanged += new System.EventHandler(this.rdoAllCustomers_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Search By:";
            // 
            // TabSupplier
            // 
            this.TabSupplier.Controls.Add(this.groupBox5);
            this.TabSupplier.Controls.Add(this.dgSup);
            this.TabSupplier.Location = new System.Drawing.Point(4, 22);
            this.TabSupplier.Name = "TabSupplier";
            this.TabSupplier.Size = new System.Drawing.Size(875, 610);
            this.TabSupplier.TabIndex = 2;
            this.TabSupplier.Text = "Supplier";
            this.TabSupplier.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.lblSupplierID);
            this.groupBox5.Controls.Add(this.pbSupplier);
            this.groupBox5.Controls.Add(this.btnSearchSuppliers);
            this.groupBox5.Controls.Add(this.txtSupplier);
            this.groupBox5.Controls.Add(this.SupEDate);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.SupSDate);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.rdoSupplier);
            this.groupBox5.Controls.Add(this.rdoAllSuppliers);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new System.Drawing.Point(9, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(855, 80);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            // 
            // lblSupplierID
            // 
            this.lblSupplierID.AutoSize = true;
            this.lblSupplierID.Location = new System.Drawing.Point(357, 45);
            this.lblSupplierID.Name = "lblSupplierID";
            this.lblSupplierID.Size = new System.Drawing.Size(0, 13);
            this.lblSupplierID.TabIndex = 195;
            // 
            // pbSupplier
            // 
            this.pbSupplier.Image = ((System.Drawing.Image)(resources.GetObject("pbSupplier.Image")));
            this.pbSupplier.Location = new System.Drawing.Point(574, 15);
            this.pbSupplier.Name = "pbSupplier";
            this.pbSupplier.Size = new System.Drawing.Size(18, 19);
            this.pbSupplier.TabIndex = 194;
            this.pbSupplier.TabStop = false;
            this.pbSupplier.Click += new System.EventHandler(this.pbSupplier_Click);
            // 
            // btnSearchSuppliers
            // 
            this.btnSearchSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchSuppliers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchSuppliers.Image = global::AbleRetailPOS.Properties.Resources.search32;
            this.btnSearchSuppliers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchSuppliers.Location = new System.Drawing.Point(700, 15);
            this.btnSearchSuppliers.Name = "btnSearchSuppliers";
            this.btnSearchSuppliers.Size = new System.Drawing.Size(99, 46);
            this.btnSearchSuppliers.TabIndex = 193;
            this.btnSearchSuppliers.Text = "Search";
            this.btnSearchSuppliers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchSuppliers.UseVisualStyleBackColor = true;
            this.btnSearchSuppliers.Click += new System.EventHandler(this.btnSearchSuppliers_Click);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(242, 15);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(326, 20);
            this.txtSupplier.TabIndex = 192;
            // 
            // SupEDate
            // 
            this.SupEDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.SupEDate.Location = new System.Drawing.Point(231, 45);
            this.SupEDate.Name = "SupEDate";
            this.SupEDate.Size = new System.Drawing.Size(106, 20);
            this.SupEDate.TabIndex = 191;
            this.SupEDate.ValueChanged += new System.EventHandler(this.SupEDate_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(206, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 190;
            this.label9.Text = "to:";
            // 
            // SupSDate
            // 
            this.SupSDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.SupSDate.Location = new System.Drawing.Point(80, 45);
            this.SupSDate.Name = "SupSDate";
            this.SupSDate.Size = new System.Drawing.Size(98, 20);
            this.SupSDate.TabIndex = 189;
            this.SupSDate.ValueChanged += new System.EventHandler(this.SupSDate_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 188;
            this.label13.Text = "Date from:";
            // 
            // rdoSupplier
            // 
            this.rdoSupplier.AutoSize = true;
            this.rdoSupplier.Location = new System.Drawing.Point(176, 16);
            this.rdoSupplier.Name = "rdoSupplier";
            this.rdoSupplier.Size = new System.Drawing.Size(63, 17);
            this.rdoSupplier.TabIndex = 2;
            this.rdoSupplier.Text = "Supplier";
            this.rdoSupplier.UseVisualStyleBackColor = true;
            this.rdoSupplier.CheckedChanged += new System.EventHandler(this.rdoSupplier_CheckedChanged);
            // 
            // rdoAllSuppliers
            // 
            this.rdoAllSuppliers.AutoSize = true;
            this.rdoAllSuppliers.Checked = true;
            this.rdoAllSuppliers.Location = new System.Drawing.Point(82, 16);
            this.rdoAllSuppliers.Name = "rdoAllSuppliers";
            this.rdoAllSuppliers.Size = new System.Drawing.Size(82, 17);
            this.rdoAllSuppliers.TabIndex = 1;
            this.rdoAllSuppliers.TabStop = true;
            this.rdoAllSuppliers.Text = "All Suppliers";
            this.rdoAllSuppliers.UseVisualStyleBackColor = true;
            this.rdoAllSuppliers.CheckedChanged += new System.EventHandler(this.rdoAllSuppliers_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Search By:";
            // 
            // dgSup
            // 
            this.dgSup.AllowUserToAddRows = false;
            this.dgSup.AllowUserToDeleteRows = false;
            this.dgSup.AllowUserToOrderColumns = true;
            this.dgSup.AllowUserToResizeColumns = false;
            this.dgSup.AllowUserToResizeRows = false;
            this.dgSup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSup.BackgroundColor = System.Drawing.Color.White;
            this.dgSup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgSup.Location = new System.Drawing.Point(10, 84);
            this.dgSup.Name = "dgSup";
            this.dgSup.RowHeadersVisible = false;
            this.dgSup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSup.Size = new System.Drawing.Size(855, 510);
            this.dgSup.TabIndex = 6;
            this.dgSup.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSup_CellDoubleClick);
            // 
            // TabItem
            // 
            this.TabItem.Controls.Add(this.groupBox6);
            this.TabItem.Controls.Add(this.dgItems);
            this.TabItem.Location = new System.Drawing.Point(4, 22);
            this.TabItem.Name = "TabItem";
            this.TabItem.Size = new System.Drawing.Size(875, 610);
            this.TabItem.TabIndex = 3;
            this.TabItem.Text = "Item";
            this.TabItem.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.lblItemName);
            this.groupBox6.Controls.Add(this.lblItemID);
            this.groupBox6.Controls.Add(this.pbItem);
            this.groupBox6.Controls.Add(this.btnSearchItems);
            this.groupBox6.Controls.Add(this.txtItem);
            this.groupBox6.Controls.Add(this.ItemEDate);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.ItemSDate);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.rdoItem);
            this.groupBox6.Controls.Add(this.rdoAllItems);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Location = new System.Drawing.Point(9, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(855, 80);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(434, 20);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(0, 13);
            this.lblItemName.TabIndex = 197;
            // 
            // lblItemID
            // 
            this.lblItemID.AutoSize = true;
            this.lblItemID.Location = new System.Drawing.Point(357, 45);
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.Size = new System.Drawing.Size(0, 13);
            this.lblItemID.TabIndex = 195;
            // 
            // pbItem
            // 
            this.pbItem.Image = ((System.Drawing.Image)(resources.GetObject("pbItem.Image")));
            this.pbItem.Location = new System.Drawing.Point(406, 16);
            this.pbItem.Name = "pbItem";
            this.pbItem.Size = new System.Drawing.Size(18, 19);
            this.pbItem.TabIndex = 194;
            this.pbItem.TabStop = false;
            this.pbItem.Click += new System.EventHandler(this.pbItem_Click);
            // 
            // btnSearchItems
            // 
            this.btnSearchItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchItems.Image = global::AbleRetailPOS.Properties.Resources.search32;
            this.btnSearchItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchItems.Location = new System.Drawing.Point(700, 15);
            this.btnSearchItems.Name = "btnSearchItems";
            this.btnSearchItems.Size = new System.Drawing.Size(99, 46);
            this.btnSearchItems.TabIndex = 193;
            this.btnSearchItems.Text = "Search";
            this.btnSearchItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchItems.UseVisualStyleBackColor = true;
            this.btnSearchItems.Click += new System.EventHandler(this.btnSearchItems_Click);
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(242, 15);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(158, 20);
            this.txtItem.TabIndex = 192;
            // 
            // ItemEDate
            // 
            this.ItemEDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ItemEDate.Location = new System.Drawing.Point(231, 45);
            this.ItemEDate.Name = "ItemEDate";
            this.ItemEDate.Size = new System.Drawing.Size(106, 20);
            this.ItemEDate.TabIndex = 191;
            this.ItemEDate.ValueChanged += new System.EventHandler(this.ItemEDate_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(206, 48);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 13);
            this.label16.TabIndex = 190;
            this.label16.Text = "to:";
            // 
            // ItemSDate
            // 
            this.ItemSDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ItemSDate.Location = new System.Drawing.Point(80, 45);
            this.ItemSDate.Name = "ItemSDate";
            this.ItemSDate.Size = new System.Drawing.Size(98, 20);
            this.ItemSDate.TabIndex = 189;
            this.ItemSDate.ValueChanged += new System.EventHandler(this.ItemSDate_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(20, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 188;
            this.label17.Text = "Date from:";
            // 
            // rdoItem
            // 
            this.rdoItem.AutoSize = true;
            this.rdoItem.Location = new System.Drawing.Point(176, 16);
            this.rdoItem.Name = "rdoItem";
            this.rdoItem.Size = new System.Drawing.Size(45, 17);
            this.rdoItem.TabIndex = 2;
            this.rdoItem.Text = "Item";
            this.rdoItem.UseVisualStyleBackColor = true;
            this.rdoItem.CheckedChanged += new System.EventHandler(this.rdoItem_CheckedChanged);
            // 
            // rdoAllItems
            // 
            this.rdoAllItems.AutoSize = true;
            this.rdoAllItems.Checked = true;
            this.rdoAllItems.Location = new System.Drawing.Point(82, 16);
            this.rdoAllItems.Name = "rdoAllItems";
            this.rdoAllItems.Size = new System.Drawing.Size(64, 17);
            this.rdoAllItems.TabIndex = 1;
            this.rdoAllItems.TabStop = true;
            this.rdoAllItems.Text = "All Items";
            this.rdoAllItems.UseVisualStyleBackColor = true;
            this.rdoAllItems.CheckedChanged += new System.EventHandler(this.rdoAllItems_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Search By:";
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.AllowUserToOrderColumns = true;
            this.dgItems.AllowUserToResizeColumns = false;
            this.dgItems.AllowUserToResizeRows = false;
            this.dgItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgItems.Location = new System.Drawing.Point(9, 84);
            this.dgItems.Name = "dgItems";
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(855, 510);
            this.dgItems.TabIndex = 6;
            this.dgItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellDoubleClick);
            // 
            // TabInvoice
            // 
            this.TabInvoice.Controls.Add(this.dgInvoice);
            this.TabInvoice.Controls.Add(this.groupBox1);
            this.TabInvoice.Location = new System.Drawing.Point(4, 22);
            this.TabInvoice.Name = "TabInvoice";
            this.TabInvoice.Size = new System.Drawing.Size(875, 610);
            this.TabInvoice.TabIndex = 4;
            this.TabInvoice.Text = "Invoice";
            this.TabInvoice.UseVisualStyleBackColor = true;
            // 
            // dgInvoice
            // 
            this.dgInvoice.AllowUserToAddRows = false;
            this.dgInvoice.AllowUserToDeleteRows = false;
            this.dgInvoice.AllowUserToOrderColumns = true;
            this.dgInvoice.AllowUserToResizeColumns = false;
            this.dgInvoice.AllowUserToResizeRows = false;
            this.dgInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgInvoice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgInvoice.BackgroundColor = System.Drawing.Color.White;
            this.dgInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInvoice.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgInvoice.Location = new System.Drawing.Point(10, 84);
            this.dgInvoice.Name = "dgInvoice";
            this.dgInvoice.RowHeadersVisible = false;
            this.dgInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInvoice.Size = new System.Drawing.Size(855, 510);
            this.dgInvoice.TabIndex = 2;
            this.dgInvoice.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgInvoice_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShowInvoice);
            this.groupBox1.Controls.Add(this.txtInvoice);
            this.groupBox1.Controls.Add(this.InvEDate);
            this.groupBox1.Controls.Add(this.edate);
            this.groupBox1.Controls.Add(this.InvSDate);
            this.groupBox1.Controls.Add(this.sdate);
            this.groupBox1.Controls.Add(this.rdoPO);
            this.groupBox1.Controls.Add(this.rdoInvoiceNo);
            this.groupBox1.Controls.Add(this.rdoAllInvoices);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(855, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnShowInvoice
            // 
            this.btnShowInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowInvoice.Image = global::AbleRetailPOS.Properties.Resources.search32;
            this.btnShowInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShowInvoice.Location = new System.Drawing.Point(700, 15);
            this.btnShowInvoice.Name = "btnShowInvoice";
            this.btnShowInvoice.Size = new System.Drawing.Size(99, 46);
            this.btnShowInvoice.TabIndex = 194;
            this.btnShowInvoice.Text = "Search";
            this.btnShowInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShowInvoice.UseVisualStyleBackColor = true;
            this.btnShowInvoice.Click += new System.EventHandler(this.btnShowInvoice_Click);
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(378, 15);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(212, 20);
            this.txtInvoice.TabIndex = 192;
            // 
            // InvEDate
            // 
            this.InvEDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.InvEDate.Location = new System.Drawing.Point(231, 45);
            this.InvEDate.Name = "InvEDate";
            this.InvEDate.Size = new System.Drawing.Size(106, 20);
            this.InvEDate.TabIndex = 191;
            // 
            // edate
            // 
            this.edate.AutoSize = true;
            this.edate.Location = new System.Drawing.Point(206, 48);
            this.edate.Name = "edate";
            this.edate.Size = new System.Drawing.Size(19, 13);
            this.edate.TabIndex = 190;
            this.edate.Text = "to:";
            // 
            // InvSDate
            // 
            this.InvSDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.InvSDate.Location = new System.Drawing.Point(80, 45);
            this.InvSDate.Name = "InvSDate";
            this.InvSDate.Size = new System.Drawing.Size(98, 20);
            this.InvSDate.TabIndex = 189;
            // 
            // sdate
            // 
            this.sdate.AutoSize = true;
            this.sdate.Location = new System.Drawing.Point(20, 48);
            this.sdate.Name = "sdate";
            this.sdate.Size = new System.Drawing.Size(56, 13);
            this.sdate.TabIndex = 188;
            this.sdate.Text = "Date from:";
            // 
            // rdoPO
            // 
            this.rdoPO.AutoSize = true;
            this.rdoPO.Location = new System.Drawing.Point(264, 16);
            this.rdoPO.Name = "rdoPO";
            this.rdoPO.Size = new System.Drawing.Size(94, 17);
            this.rdoPO.TabIndex = 3;
            this.rdoPO.Text = "Customer PO#";
            this.rdoPO.UseVisualStyleBackColor = true;
            this.rdoPO.CheckedChanged += new System.EventHandler(this.rdoPO_CheckedChanged);
            // 
            // rdoInvoiceNo
            // 
            this.rdoInvoiceNo.AutoSize = true;
            this.rdoInvoiceNo.Location = new System.Drawing.Point(176, 16);
            this.rdoInvoiceNo.Name = "rdoInvoiceNo";
            this.rdoInvoiceNo.Size = new System.Drawing.Size(70, 17);
            this.rdoInvoiceNo.TabIndex = 2;
            this.rdoInvoiceNo.Text = "Invoice #";
            this.rdoInvoiceNo.UseVisualStyleBackColor = true;
            this.rdoInvoiceNo.CheckedChanged += new System.EventHandler(this.rdoInvoiceNo_CheckedChanged);
            // 
            // rdoAllInvoices
            // 
            this.rdoAllInvoices.AutoSize = true;
            this.rdoAllInvoices.Checked = true;
            this.rdoAllInvoices.Location = new System.Drawing.Point(82, 16);
            this.rdoAllInvoices.Name = "rdoAllInvoices";
            this.rdoAllInvoices.Size = new System.Drawing.Size(79, 17);
            this.rdoAllInvoices.TabIndex = 1;
            this.rdoAllInvoices.TabStop = true;
            this.rdoAllInvoices.Text = "All Invoices";
            this.rdoAllInvoices.UseVisualStyleBackColor = true;
            this.rdoAllInvoices.CheckedChanged += new System.EventHandler(this.rdoAllInvoices_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search By:";
            // 
            // TransactionsLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 661);
            this.Controls.Add(this.TabLookup);
            this.Name = "TransactionsLookup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transactions Lookup";
            this.Load += new System.EventHandler(this.TransactionsLookup_Load);
            this.TabLookup.ResumeLayout(false);
            this.TabCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomer)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).EndInit();
            this.TabSupplier.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSup)).EndInit();
            this.TabItem.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.TabInvoice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabLookup;
        private System.Windows.Forms.TabPage TabCustomer;
        private System.Windows.Forms.TabPage TabSupplier;
        private System.Windows.Forms.TabPage TabItem;
        private System.Windows.Forms.TabPage TabInvoice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoInvoiceNo;
        private System.Windows.Forms.RadioButton rdoAllInvoices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoPO;
        private System.Windows.Forms.DataGridView dgInvoice;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.DateTimePicker InvEDate;
        private System.Windows.Forms.Label edate;
        private System.Windows.Forms.DateTimePicker InvSDate;
        private System.Windows.Forms.Label sdate;
        private System.Windows.Forms.Button btnShowInvoice;
        private System.Windows.Forms.DataGridView dgCustomer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblCustomerID;
        private System.Windows.Forms.PictureBox pbCustomer;
        private System.Windows.Forms.Button btnSearchCustomer;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.DateTimePicker CusEDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker CusSDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rdoCustomer;
        private System.Windows.Forms.RadioButton rdoAllCustomers;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblSupplierID;
        private System.Windows.Forms.PictureBox pbSupplier;
        private System.Windows.Forms.Button btnSearchSuppliers;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.DateTimePicker SupEDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker SupSDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton rdoSupplier;
        private System.Windows.Forms.RadioButton rdoAllSuppliers;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dgSup;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblItemID;
        private System.Windows.Forms.PictureBox pbItem;
        private System.Windows.Forms.Button btnSearchItems;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.DateTimePicker ItemEDate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker ItemSDate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RadioButton rdoItem;
        private System.Windows.Forms.RadioButton rdoAllItems;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.Label lblItemName;
    }
}