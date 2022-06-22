namespace JournalExportTool
{
    partial class ExportJournalTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportJournalTable));
            this.btnExport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTranType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.edate = new System.Windows.Forms.DateTimePicker();
            this.sdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccpacDb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccpacServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdbNameRetail = new System.Windows.Forms.TextBox();
            this.txtRetailServer = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCon = new System.Windows.Forms.Button();
            this.gbAccpac = new System.Windows.Forms.GroupBox();
            this.chkWAAccpac = new System.Windows.Forms.CheckBox();
            this.checkAccpacCon = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbRestaurantPOS = new System.Windows.Forms.GroupBox();
            this.chkWARetail = new System.Windows.Forms.CheckBox();
            this.checkRetailDb = new System.Windows.Forms.CheckBox();
            this.chkbConsolidate = new System.Windows.Forms.CheckBox();
            this.dgJournal = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Job = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TranType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCleared = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsDeposited = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EntityID = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrintExport = new System.Windows.Forms.Button();
            this.btnExportExcell = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpJournalDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalDebit = new System.Windows.Forms.NumericUpDown();
            this.txtTotalCredit = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gbAccpac.SuspendLayout();
            this.gbRestaurantPOS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgJournal)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCredit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExport.Location = new System.Drawing.Point(283, 644);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(145, 35);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export to ACCPAC";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label5.Location = new System.Drawing.Point(12, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 28);
            this.label5.TabIndex = 9;
            this.label5.Text = "Transaction: \r\nType";
            // 
            // cmbTranType
            // 
            this.cmbTranType.Enabled = false;
            this.cmbTranType.FormattingEnabled = true;
            this.cmbTranType.Items.AddRange(new object[] {
            "Sales",
            "Purchases",
            "Stock Transactions",
            "All"});
            this.cmbTranType.Location = new System.Drawing.Point(85, 94);
            this.cmbTranType.Name = "cmbTranType";
            this.cmbTranType.Size = new System.Drawing.Size(205, 21);
            this.cmbTranType.TabIndex = 8;
            this.cmbTranType.SelectedIndexChanged += new System.EventHandler(this.cmbTranType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "to";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label7.Location = new System.Drawing.Point(13, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 14);
            this.label7.TabIndex = 7;
            this.label7.Text = "Date Range :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // edate
            // 
            this.edate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edate.Location = new System.Drawing.Point(134, 47);
            this.edate.Name = "edate";
            this.edate.Size = new System.Drawing.Size(111, 20);
            this.edate.TabIndex = 6;
            this.edate.ValueChanged += new System.EventHandler(this.edate_ValueChanged);
            // 
            // sdate
            // 
            this.sdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdate.Location = new System.Drawing.Point(134, 13);
            this.sdate.Name = "sdate";
            this.sdate.Size = new System.Drawing.Size(111, 20);
            this.sdate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database Name :";
            // 
            // txtAccpacDb
            // 
            this.txtAccpacDb.Location = new System.Drawing.Point(101, 57);
            this.txtAccpacDb.Name = "txtAccpacDb";
            this.txtAccpacDb.Size = new System.Drawing.Size(252, 20);
            this.txtAccpacDb.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label3.Location = new System.Drawing.Point(20, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Server Name :";
            // 
            // txtAccpacServer
            // 
            this.txtAccpacServer.Location = new System.Drawing.Point(101, 31);
            this.txtAccpacServer.Name = "txtAccpacServer";
            this.txtAccpacServer.Size = new System.Drawing.Size(252, 20);
            this.txtAccpacServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Name :";
            // 
            // txtdbNameRetail
            // 
            this.txtdbNameRetail.Location = new System.Drawing.Point(101, 52);
            this.txtdbNameRetail.Name = "txtdbNameRetail";
            this.txtdbNameRetail.Size = new System.Drawing.Size(252, 20);
            this.txtdbNameRetail.TabIndex = 2;
            // 
            // txtRetailServer
            // 
            this.txtRetailServer.Location = new System.Drawing.Point(101, 23);
            this.txtRetailServer.Name = "txtRetailServer";
            this.txtRetailServer.Size = new System.Drawing.Size(252, 20);
            this.txtRetailServer.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(359, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCon
            // 
            this.btnCon.Location = new System.Drawing.Point(359, 31);
            this.btnCon.Name = "btnCon";
            this.btnCon.Size = new System.Drawing.Size(75, 23);
            this.btnCon.TabIndex = 0;
            this.btnCon.Text = "Connect";
            this.btnCon.UseVisualStyleBackColor = true;
            this.btnCon.Click += new System.EventHandler(this.btnCon_Click);
            // 
            // gbAccpac
            // 
            this.gbAccpac.Controls.Add(this.chkWAAccpac);
            this.gbAccpac.Controls.Add(this.checkAccpacCon);
            this.gbAccpac.Controls.Add(this.label4);
            this.gbAccpac.Controls.Add(this.txtAccpacDb);
            this.gbAccpac.Controls.Add(this.label3);
            this.gbAccpac.Controls.Add(this.txtAccpacServer);
            this.gbAccpac.Controls.Add(this.btnCon);
            this.gbAccpac.Font = new System.Drawing.Font("Arial", 8.25F);
            this.gbAccpac.Location = new System.Drawing.Point(14, 134);
            this.gbAccpac.Name = "gbAccpac";
            this.gbAccpac.Size = new System.Drawing.Size(457, 109);
            this.gbAccpac.TabIndex = 4;
            this.gbAccpac.TabStop = false;
            this.gbAccpac.Text = "ACCPAC";
            // 
            // chkWAAccpac
            // 
            this.chkWAAccpac.AutoSize = true;
            this.chkWAAccpac.Location = new System.Drawing.Point(10, 85);
            this.chkWAAccpac.Name = "chkWAAccpac";
            this.chkWAAccpac.Size = new System.Drawing.Size(165, 18);
            this.chkWAAccpac.TabIndex = 209;
            this.chkWAAccpac.Text = "Use Windows Authentication";
            this.chkWAAccpac.UseVisualStyleBackColor = true;
            // 
            // checkAccpacCon
            // 
            this.checkAccpacCon.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkAccpacCon.AutoSize = true;
            this.checkAccpacCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.checkAccpacCon.Enabled = false;
            this.checkAccpacCon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkAccpacCon.Location = new System.Drawing.Point(374, 57);
            this.checkAccpacCon.Name = "checkAccpacCon";
            this.checkAccpacCon.Size = new System.Drawing.Size(28, 24);
            this.checkAccpacCon.TabIndex = 11;
            this.checkAccpacCon.Text = "✔";
            this.checkAccpacCon.UseVisualStyleBackColor = false;
            this.checkAccpacCon.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label4.Location = new System.Drawing.Point(7, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "Database Name :";
            // 
            // gbRestaurantPOS
            // 
            this.gbRestaurantPOS.Controls.Add(this.chkWARetail);
            this.gbRestaurantPOS.Controls.Add(this.checkRetailDb);
            this.gbRestaurantPOS.Controls.Add(this.label2);
            this.gbRestaurantPOS.Controls.Add(this.label1);
            this.gbRestaurantPOS.Controls.Add(this.txtdbNameRetail);
            this.gbRestaurantPOS.Controls.Add(this.txtRetailServer);
            this.gbRestaurantPOS.Controls.Add(this.btnConnect);
            this.gbRestaurantPOS.Font = new System.Drawing.Font("Arial", 8.25F);
            this.gbRestaurantPOS.Location = new System.Drawing.Point(12, 10);
            this.gbRestaurantPOS.Name = "gbRestaurantPOS";
            this.gbRestaurantPOS.Size = new System.Drawing.Size(459, 118);
            this.gbRestaurantPOS.TabIndex = 3;
            this.gbRestaurantPOS.TabStop = false;
            this.gbRestaurantPOS.Text = "Able Retail";
            // 
            // chkWARetail
            // 
            this.chkWARetail.AutoSize = true;
            this.chkWARetail.Location = new System.Drawing.Point(9, 85);
            this.chkWARetail.Name = "chkWARetail";
            this.chkWARetail.Size = new System.Drawing.Size(165, 18);
            this.chkWARetail.TabIndex = 208;
            this.chkWARetail.Text = "Use Windows Authentication";
            this.chkWARetail.UseVisualStyleBackColor = true;
            // 
            // checkRetailDb
            // 
            this.checkRetailDb.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkRetailDb.AutoSize = true;
            this.checkRetailDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.checkRetailDb.Enabled = false;
            this.checkRetailDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkRetailDb.Location = new System.Drawing.Point(378, 50);
            this.checkRetailDb.Name = "checkRetailDb";
            this.checkRetailDb.Size = new System.Drawing.Size(28, 24);
            this.checkRetailDb.TabIndex = 10;
            this.checkRetailDb.Text = "✔";
            this.checkRetailDb.UseVisualStyleBackColor = false;
            this.checkRetailDb.Visible = false;
            // 
            // chkbConsolidate
            // 
            this.chkbConsolidate.AutoSize = true;
            this.chkbConsolidate.Location = new System.Drawing.Point(15, 134);
            this.chkbConsolidate.Name = "chkbConsolidate";
            this.chkbConsolidate.Size = new System.Drawing.Size(151, 17);
            this.chkbConsolidate.TabIndex = 207;
            this.chkbConsolidate.Text = "Consolidate same account";
            this.chkbConsolidate.UseVisualStyleBackColor = true;
            this.chkbConsolidate.CheckedChanged += new System.EventHandler(this.chkbConsolidate_CheckedChanged);
            // 
            // dgJournal
            // 
            this.dgJournal.AllowUserToAddRows = false;
            this.dgJournal.AllowUserToDeleteRows = false;
            this.dgJournal.AllowUserToOrderColumns = true;
            this.dgJournal.AllowUserToResizeColumns = false;
            this.dgJournal.AllowUserToResizeRows = false;
            this.dgJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgJournal.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgJournal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgJournal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Memo,
            this.TransactionNumber,
            this.AccountNum,
            this.AccountName,
            this.Debit,
            this.Credit,
            this.Job,
            this.TranType,
            this.IsCleared,
            this.IsDeposited,
            this.EntityID,
            this.LocationID,
            this.AccountID});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgJournal.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgJournal.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgJournal.Location = new System.Drawing.Point(6, 249);
            this.dgJournal.MultiSelect = false;
            this.dgJournal.Name = "dgJournal";
            this.dgJournal.RowHeadersVisible = false;
            this.dgJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgJournal.Size = new System.Drawing.Size(789, 367);
            this.dgJournal.TabIndex = 205;
            this.dgJournal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgJournal_KeyDown);
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Date.FillWeight = 60F;
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 80;
            this.Date.Name = "Date";
            this.Date.Width = 80;
            // 
            // Memo
            // 
            this.Memo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Memo.FillWeight = 150F;
            this.Memo.HeaderText = "Memo";
            this.Memo.MinimumWidth = 150;
            this.Memo.Name = "Memo";
            this.Memo.Width = 156;
            // 
            // TransactionNumber
            // 
            this.TransactionNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TransactionNumber.FillWeight = 80F;
            this.TransactionNumber.HeaderText = "Transaction No";
            this.TransactionNumber.MinimumWidth = 80;
            this.TransactionNumber.Name = "TransactionNumber";
            this.TransactionNumber.Width = 82;
            // 
            // AccountNum
            // 
            this.AccountNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AccountNum.FillWeight = 80F;
            this.AccountNum.HeaderText = "Account No";
            this.AccountNum.MinimumWidth = 80;
            this.AccountNum.Name = "AccountNum";
            this.AccountNum.Width = 83;
            // 
            // AccountName
            // 
            this.AccountName.HeaderText = "Account Name";
            this.AccountName.Name = "AccountName";
            // 
            // Debit
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.Debit.DefaultCellStyle = dataGridViewCellStyle2;
            this.Debit.FillWeight = 4.176044F;
            this.Debit.HeaderText = "Debit";
            this.Debit.MinimumWidth = 100;
            this.Debit.Name = "Debit";
            // 
            // Credit
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Credit.DefaultCellStyle = dataGridViewCellStyle3;
            this.Credit.FillWeight = 4.176044F;
            this.Credit.HeaderText = "Credit";
            this.Credit.MinimumWidth = 100;
            this.Credit.Name = "Credit";
            // 
            // Job
            // 
            this.Job.FillWeight = 4.176044F;
            this.Job.HeaderText = "Job";
            this.Job.MinimumWidth = 100;
            this.Job.Name = "Job";
            // 
            // TranType
            // 
            this.TranType.HeaderText = "TranType";
            this.TranType.Name = "TranType";
            this.TranType.Visible = false;
            // 
            // IsCleared
            // 
            this.IsCleared.HeaderText = "IsCleared";
            this.IsCleared.Name = "IsCleared";
            this.IsCleared.ReadOnly = true;
            this.IsCleared.Visible = false;
            // 
            // IsDeposited
            // 
            this.IsDeposited.HeaderText = "IsDeposited";
            this.IsDeposited.Name = "IsDeposited";
            this.IsDeposited.ReadOnly = true;
            this.IsDeposited.Visible = false;
            // 
            // EntityID
            // 
            this.EntityID.HeaderText = "EntityID";
            this.EntityID.Name = "EntityID";
            this.EntityID.ReadOnly = true;
            this.EntityID.Visible = false;
            // 
            // LocationID
            // 
            this.LocationID.HeaderText = "[LocationID]";
            this.LocationID.Name = "LocationID";
            this.LocationID.ReadOnly = true;
            this.LocationID.Visible = false;
            // 
            // AccountID
            // 
            this.AccountID.HeaderText = "AccountID";
            this.AccountID.Name = "AccountID";
            this.AccountID.ReadOnly = true;
            this.AccountID.Visible = false;
            // 
            // btnPrintExport
            // 
            this.btnPrintExport.Enabled = false;
            this.btnPrintExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPrintExport.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintExport.Image")));
            this.btnPrintExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintExport.Location = new System.Drawing.Point(710, 664);
            this.btnPrintExport.Name = "btnPrintExport";
            this.btnPrintExport.Size = new System.Drawing.Size(85, 35);
            this.btnPrintExport.TabIndex = 206;
            this.btnPrintExport.Text = "Print";
            this.btnPrintExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintExport.UseVisualStyleBackColor = true;
            this.btnPrintExport.Click += new System.EventHandler(this.btnPrintExport_Click);
            // 
            // btnExportExcell
            // 
            this.btnExportExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcell.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcell.Image")));
            this.btnExportExcell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcell.Location = new System.Drawing.Point(665, 623);
            this.btnExportExcell.Name = "btnExportExcell";
            this.btnExportExcell.Size = new System.Drawing.Size(130, 35);
            this.btnExportExcell.TabIndex = 265;
            this.btnExportExcell.Text = "          Export Excell";
            this.btnExportExcell.UseVisualStyleBackColor = true;
            this.btnExportExcell.Click += new System.EventHandler(this.btnExportExcell_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShow);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbTranType);
            this.groupBox1.Controls.Add(this.chkbConsolidate);
            this.groupBox1.Controls.Add(this.sdate);
            this.groupBox1.Controls.Add(this.edate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(478, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 230);
            this.groupBox1.TabIndex = 266;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Oprtions";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(136, 188);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(154, 23);
            this.btnShow.TabIndex = 209;
            this.btnShow.Text = "Show Transactions";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 208;
            this.label8.Text = "from";
            // 
            // dtpJournalDate
            // 
            this.dtpJournalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpJournalDate.Location = new System.Drawing.Point(115, 627);
            this.dtpJournalDate.Name = "dtpJournalDate";
            this.dtpJournalDate.Size = new System.Drawing.Size(152, 20);
            this.dtpJournalDate.TabIndex = 210;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label9.Location = new System.Drawing.Point(9, 630);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 14);
            this.label9.TabIndex = 267;
            this.label9.Text = "Journal Entry Date:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtTotalDebit
            // 
            this.txtTotalDebit.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalDebit.DecimalPlaces = 2;
            this.txtTotalDebit.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalDebit.Location = new System.Drawing.Point(115, 653);
            this.txtTotalDebit.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.txtTotalDebit.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.txtTotalDebit.Name = "txtTotalDebit";
            this.txtTotalDebit.ReadOnly = true;
            this.txtTotalDebit.Size = new System.Drawing.Size(152, 20);
            this.txtTotalDebit.TabIndex = 268;
            this.txtTotalDebit.ThousandsSeparator = true;
            // 
            // txtTotalCredit
            // 
            this.txtTotalCredit.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalCredit.DecimalPlaces = 2;
            this.txtTotalCredit.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalCredit.Location = new System.Drawing.Point(115, 679);
            this.txtTotalCredit.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.txtTotalCredit.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.ReadOnly = true;
            this.txtTotalCredit.Size = new System.Drawing.Size(152, 20);
            this.txtTotalCredit.TabIndex = 269;
            this.txtTotalCredit.ThousandsSeparator = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label10.Location = new System.Drawing.Point(47, 659);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 14);
            this.label10.TabIndex = 270;
            this.label10.Text = "Total Debit:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label11.Location = new System.Drawing.Point(47, 685);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 271;
            this.label11.Text = "Total Credit:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ExportJournalTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 703);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTotalCredit);
            this.Controls.Add(this.txtTotalDebit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpJournalDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExportExcell);
            this.Controls.Add(this.btnPrintExport);
            this.Controls.Add(this.dgJournal);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gbAccpac);
            this.Controls.Add(this.gbRestaurantPOS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ExportJournalTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accpac Export Tool";
            this.Load += new System.EventHandler(this.ExportJournalTable_Load);
            this.gbAccpac.ResumeLayout(false);
            this.gbAccpac.PerformLayout();
            this.gbRestaurantPOS.ResumeLayout(false);
            this.gbRestaurantPOS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgJournal)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCredit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTranType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker edate;
        private System.Windows.Forms.DateTimePicker sdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccpacDb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAccpacServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdbNameRetail;
        private System.Windows.Forms.TextBox txtRetailServer;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCon;
        private System.Windows.Forms.GroupBox gbAccpac;
        private System.Windows.Forms.GroupBox gbRestaurantPOS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkRetailDb;
        private System.Windows.Forms.CheckBox checkAccpacCon;
        private System.Windows.Forms.DataGridView dgJournal;
        private System.Windows.Forms.Button btnPrintExport;
        private System.Windows.Forms.CheckBox chkbConsolidate;
        private System.Windows.Forms.Button btnExportExcell;
        private System.Windows.Forms.CheckBox chkWAAccpac;
        private System.Windows.Forms.CheckBox chkWARetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Memo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Job;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCleared;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDeposited;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EntityID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountID;
        private System.Windows.Forms.DateTimePicker dtpJournalDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown txtTotalDebit;
        private System.Windows.Forms.NumericUpDown txtTotalCredit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

