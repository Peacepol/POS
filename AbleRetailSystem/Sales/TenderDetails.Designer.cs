namespace RestaurantPOS
{
    partial class TenderDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TenderDetails));
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAuthorization = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgPaymentMethod = new System.Windows.Forms.DataGridView();
            this.PaymentMethodID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentMethodLogo = new System.Windows.Forms.DataGridViewImageColumn();
            this.PaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentAuthorisationNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentCardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentNameOnCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentExpirationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentCardNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentBSB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentBankAccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentBankAccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentChequeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentBankNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentGCNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentGCNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalAmount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.RecipientAccountName = new System.Windows.Forms.Label();
            this.AmountPaid = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCardNo = new System.Windows.Forms.TabPage();
            this.txtCardExpiry = new System.Windows.Forms.MaskedTextBox();
            this.txtNotes = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardNotes = new System.Windows.Forms.TextBox();
            this.txtCardName = new System.Windows.Forms.TextBox();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.tabBankNo = new System.Windows.Forms.TabPage();
            this.txtBankBSB = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBankNotes = new System.Windows.Forms.TextBox();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.txtBankAccountNo = new System.Windows.Forms.TextBox();
            this.txtBankAccountName = new System.Windows.Forms.TextBox();
            this.tabGC = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtGCNotes = new System.Windows.Forms.TextBox();
            this.txtGCNo = new System.Windows.Forms.TextBox();
            this.tabNotes = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPaymentNotes = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.AmountChange = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.BalanceDue = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaymentMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountPaid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabCardNo.SuspendLayout();
            this.tabBankNo.SuspendLayout();
            this.tabGC.SuspendLayout();
            this.tabNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmountChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BalanceDue)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.Location = new System.Drawing.Point(866, -184);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(160, 31);
            this.textBox7.TabIndex = 40;
            this.textBox7.Text = "Amount";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 17);
            this.label6.TabIndex = 41;
            this.label6.Text = "AMOUNT PAID:";
            // 
            // txtAuthorization
            // 
            this.txtAuthorization.Location = new System.Drawing.Point(546, 32);
            this.txtAuthorization.Name = "txtAuthorization";
            this.txtAuthorization.Size = new System.Drawing.Size(137, 20);
            this.txtAuthorization.TabIndex = 40;
            this.txtAuthorization.TextChanged += new System.EventHandler(this.txtAuthorization_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(442, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Authorization Code";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.BalanceDue);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.AmountChange);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.dgPaymentMethod);
            this.groupBox1.Controls.Add(this.txtTotalAmount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.RecipientAccountName);
            this.groupBox1.Controls.Add(this.AmountPaid);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(14, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 441);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // dgPaymentMethod
            // 
            this.dgPaymentMethod.AllowUserToAddRows = false;
            this.dgPaymentMethod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPaymentMethod.ColumnHeadersVisible = false;
            this.dgPaymentMethod.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PaymentMethodID,
            this.PaymentMethodLogo,
            this.PaymentMethod,
            this.Amount,
            this.PaymentAuthorisationNumber,
            this.PaymentCardNumber,
            this.PaymentNameOnCard,
            this.PaymentExpirationDate,
            this.PaymentCardNotes,
            this.PaymentBSB,
            this.PaymentBankAccountNumber,
            this.PaymentBankAccountName,
            this.PaymentChequeNumber,
            this.PaymentBankNotes,
            this.PaymentNotes,
            this.PaymentGCNo,
            this.PaymentGCNotes});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPaymentMethod.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgPaymentMethod.Location = new System.Drawing.Point(25, 50);
            this.dgPaymentMethod.Name = "dgPaymentMethod";
            this.dgPaymentMethod.RowHeadersVisible = false;
            this.dgPaymentMethod.Size = new System.Drawing.Size(356, 274);
            this.dgPaymentMethod.TabIndex = 189;
            this.dgPaymentMethod.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPaymentMethod_CellClick);
            this.dgPaymentMethod.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPaymentMethod_CellEndEdit);
            this.dgPaymentMethod.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPaymentMethod_CellFormatting);
            this.dgPaymentMethod.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgPaymentMethod_EditingControlShowing);
            // 
            // PaymentMethodID
            // 
            this.PaymentMethodID.HeaderText = "PaymentMethodID";
            this.PaymentMethodID.Name = "PaymentMethodID";
            this.PaymentMethodID.ReadOnly = true;
            this.PaymentMethodID.Visible = false;
            // 
            // PaymentMethodLogo
            // 
            this.PaymentMethodLogo.HeaderText = "Logo";
            this.PaymentMethodLogo.Name = "PaymentMethodLogo";
            this.PaymentMethodLogo.ReadOnly = true;
            this.PaymentMethodLogo.Visible = false;
            // 
            // PaymentMethod
            // 
            this.PaymentMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PaymentMethod.HeaderText = "PaymentMethod";
            this.PaymentMethod.Name = "PaymentMethod";
            this.PaymentMethod.ReadOnly = true;
            this.PaymentMethod.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PaymentMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Amount.Width = 110;
            // 
            // PaymentAuthorisationNumber
            // 
            this.PaymentAuthorisationNumber.HeaderText = "PaymentAuthorisationNumber";
            this.PaymentAuthorisationNumber.Name = "PaymentAuthorisationNumber";
            this.PaymentAuthorisationNumber.Visible = false;
            // 
            // PaymentCardNumber
            // 
            this.PaymentCardNumber.HeaderText = "PaymentCardNumber";
            this.PaymentCardNumber.Name = "PaymentCardNumber";
            this.PaymentCardNumber.Visible = false;
            // 
            // PaymentNameOnCard
            // 
            this.PaymentNameOnCard.HeaderText = "PaymentNameOnCard";
            this.PaymentNameOnCard.Name = "PaymentNameOnCard";
            this.PaymentNameOnCard.Visible = false;
            // 
            // PaymentExpirationDate
            // 
            this.PaymentExpirationDate.HeaderText = "PaymentExpirationDate";
            this.PaymentExpirationDate.Name = "PaymentExpirationDate";
            this.PaymentExpirationDate.Visible = false;
            // 
            // PaymentCardNotes
            // 
            this.PaymentCardNotes.HeaderText = "PaymentCardNotes";
            this.PaymentCardNotes.Name = "PaymentCardNotes";
            this.PaymentCardNotes.Visible = false;
            // 
            // PaymentBSB
            // 
            this.PaymentBSB.HeaderText = "PaymentBSB";
            this.PaymentBSB.Name = "PaymentBSB";
            this.PaymentBSB.Visible = false;
            // 
            // PaymentBankAccountNumber
            // 
            this.PaymentBankAccountNumber.HeaderText = "PaymentBankAccountNumber";
            this.PaymentBankAccountNumber.Name = "PaymentBankAccountNumber";
            this.PaymentBankAccountNumber.Visible = false;
            // 
            // PaymentBankAccountName
            // 
            this.PaymentBankAccountName.HeaderText = "PaymentBankAccountName";
            this.PaymentBankAccountName.Name = "PaymentBankAccountName";
            this.PaymentBankAccountName.Visible = false;
            // 
            // PaymentChequeNumber
            // 
            this.PaymentChequeNumber.HeaderText = "PaymentChequeNumber";
            this.PaymentChequeNumber.Name = "PaymentChequeNumber";
            this.PaymentChequeNumber.Visible = false;
            // 
            // PaymentBankNotes
            // 
            this.PaymentBankNotes.HeaderText = "PaymentBankNotes";
            this.PaymentBankNotes.Name = "PaymentBankNotes";
            this.PaymentBankNotes.Visible = false;
            // 
            // PaymentNotes
            // 
            this.PaymentNotes.HeaderText = "PaymentNotes";
            this.PaymentNotes.Name = "PaymentNotes";
            this.PaymentNotes.Visible = false;
            // 
            // PaymentGCNo
            // 
            this.PaymentGCNo.HeaderText = "PaymentGCNo";
            this.PaymentGCNo.Name = "PaymentGCNo";
            this.PaymentGCNo.Visible = false;
            // 
            // PaymentGCNotes
            // 
            this.PaymentGCNotes.HeaderText = "PaymentGCNotes";
            this.PaymentGCNotes.Name = "PaymentGCNotes";
            this.PaymentGCNotes.Visible = false;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.DecimalPlaces = 2;
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalAmount.Location = new System.Drawing.Point(235, 16);
            this.txtTotalAmount.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.txtTotalAmount.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(146, 26);
            this.txtTotalAmount.TabIndex = 188;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 17);
            this.label5.TabIndex = 185;
            this.label5.Text = "TOTAL AMOUNT DUE:";
            // 
            // RecipientAccountName
            // 
            this.RecipientAccountName.AutoSize = true;
            this.RecipientAccountName.Location = new System.Drawing.Point(294, 6);
            this.RecipientAccountName.Name = "RecipientAccountName";
            this.RecipientAccountName.Size = new System.Drawing.Size(0, 13);
            this.RecipientAccountName.TabIndex = 184;
            // 
            // AmountPaid
            // 
            this.AmountPaid.DecimalPlaces = 2;
            this.AmountPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountPaid.Location = new System.Drawing.Point(235, 337);
            this.AmountPaid.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.AmountPaid.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.AmountPaid.Name = "AmountPaid";
            this.AmountPaid.Size = new System.Drawing.Size(146, 26);
            this.AmountPaid.TabIndex = 43;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabCardNo);
            this.tabControl1.Controls.Add(this.tabBankNo);
            this.tabControl1.Controls.Add(this.tabGC);
            this.tabControl1.Controls.Add(this.tabNotes);
            this.tabControl1.Location = new System.Drawing.Point(439, 86);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(324, 269);
            this.tabControl1.TabIndex = 183;
            // 
            // tabCardNo
            // 
            this.tabCardNo.BackColor = System.Drawing.Color.White;
            this.tabCardNo.Controls.Add(this.txtCardExpiry);
            this.tabCardNo.Controls.Add(this.txtNotes);
            this.tabCardNo.Controls.Add(this.label4);
            this.tabCardNo.Controls.Add(this.label3);
            this.tabCardNo.Controls.Add(this.label1);
            this.tabCardNo.Controls.Add(this.txtCardNotes);
            this.tabCardNo.Controls.Add(this.txtCardName);
            this.tabCardNo.Controls.Add(this.txtCardNo);
            this.tabCardNo.Location = new System.Drawing.Point(4, 25);
            this.tabCardNo.Name = "tabCardNo";
            this.tabCardNo.Padding = new System.Windows.Forms.Padding(3);
            this.tabCardNo.Size = new System.Drawing.Size(316, 240);
            this.tabCardNo.TabIndex = 0;
            this.tabCardNo.Text = "Card Number";
            // 
            // txtCardExpiry
            // 
            this.txtCardExpiry.Location = new System.Drawing.Point(119, 50);
            this.txtCardExpiry.Mask = "00/00";
            this.txtCardExpiry.Name = "txtCardExpiry";
            this.txtCardExpiry.Size = new System.Drawing.Size(184, 20);
            this.txtCardExpiry.TabIndex = 113;
            this.txtCardExpiry.TextChanged += new System.EventHandler(this.txtCardExpiry_TextChanged);
            // 
            // txtNotes
            // 
            this.txtNotes.AutoSize = true;
            this.txtNotes.BackColor = System.Drawing.Color.Transparent;
            this.txtNotes.Location = new System.Drawing.Point(15, 111);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(35, 13);
            this.txtNotes.TabIndex = 46;
            this.txtNotes.Text = "Notes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(15, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Name on Card";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(15, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "Expiry Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Card Number";
            // 
            // txtCardNotes
            // 
            this.txtCardNotes.Location = new System.Drawing.Point(119, 110);
            this.txtCardNotes.Multiline = true;
            this.txtCardNotes.Name = "txtCardNotes";
            this.txtCardNotes.Size = new System.Drawing.Size(184, 100);
            this.txtCardNotes.TabIndex = 42;
            this.txtCardNotes.TextChanged += new System.EventHandler(this.txtCardNotes_TextChanged);
            // 
            // txtCardName
            // 
            this.txtCardName.Location = new System.Drawing.Point(119, 79);
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.Size = new System.Drawing.Size(184, 20);
            this.txtCardName.TabIndex = 43;
            this.txtCardName.TextChanged += new System.EventHandler(this.txtCardName_TextChanged);
            // 
            // txtCardNo
            // 
            this.txtCardNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCardNo.Location = new System.Drawing.Point(119, 21);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(184, 20);
            this.txtCardNo.TabIndex = 45;
            this.txtCardNo.TextChanged += new System.EventHandler(this.txtCardNo_TextChanged);
            // 
            // tabBankNo
            // 
            this.tabBankNo.BackColor = System.Drawing.Color.White;
            this.tabBankNo.Controls.Add(this.txtBankBSB);
            this.tabBankNo.Controls.Add(this.label13);
            this.tabBankNo.Controls.Add(this.label9);
            this.tabBankNo.Controls.Add(this.label10);
            this.tabBankNo.Controls.Add(this.label11);
            this.tabBankNo.Controls.Add(this.label12);
            this.tabBankNo.Controls.Add(this.txtBankNotes);
            this.tabBankNo.Controls.Add(this.txtChequeNo);
            this.tabBankNo.Controls.Add(this.txtBankAccountNo);
            this.tabBankNo.Controls.Add(this.txtBankAccountName);
            this.tabBankNo.Location = new System.Drawing.Point(4, 25);
            this.tabBankNo.Name = "tabBankNo";
            this.tabBankNo.Padding = new System.Windows.Forms.Padding(3);
            this.tabBankNo.Size = new System.Drawing.Size(316, 240);
            this.tabBankNo.TabIndex = 1;
            this.tabBankNo.Text = "Bank Number";
            // 
            // txtBankBSB
            // 
            this.txtBankBSB.Location = new System.Drawing.Point(119, 19);
            this.txtBankBSB.Mask = "000-000";
            this.txtBankBSB.Name = "txtBankBSB";
            this.txtBankBSB.Size = new System.Drawing.Size(126, 20);
            this.txtBankBSB.TabIndex = 58;
            this.txtBankBSB.TextChanged += new System.EventHandler(this.txtBankBSB_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(15, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 54;
            this.label13.Text = "Notes";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(15, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Cheque No.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(15, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "Account No.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(15, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Account Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(15, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "BSB";
            // 
            // txtBankNotes
            // 
            this.txtBankNotes.Location = new System.Drawing.Point(119, 139);
            this.txtBankNotes.Multiline = true;
            this.txtBankNotes.Name = "txtBankNotes";
            this.txtBankNotes.Size = new System.Drawing.Size(184, 82);
            this.txtBankNotes.TabIndex = 50;
            this.txtBankNotes.TextChanged += new System.EventHandler(this.txtBankNotes_TextChanged);
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Location = new System.Drawing.Point(119, 108);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(184, 20);
            this.txtChequeNo.TabIndex = 51;
            this.txtChequeNo.TextChanged += new System.EventHandler(this.txtChequeNo_TextChanged);
            // 
            // txtBankAccountNo
            // 
            this.txtBankAccountNo.Location = new System.Drawing.Point(119, 77);
            this.txtBankAccountNo.Name = "txtBankAccountNo";
            this.txtBankAccountNo.Size = new System.Drawing.Size(184, 20);
            this.txtBankAccountNo.TabIndex = 51;
            this.txtBankAccountNo.TextChanged += new System.EventHandler(this.txtBankAccountNo_TextChanged);
            // 
            // txtBankAccountName
            // 
            this.txtBankAccountName.Location = new System.Drawing.Point(119, 48);
            this.txtBankAccountName.Name = "txtBankAccountName";
            this.txtBankAccountName.Size = new System.Drawing.Size(184, 20);
            this.txtBankAccountName.TabIndex = 52;
            this.txtBankAccountName.TextChanged += new System.EventHandler(this.txtBankAccountName_TextChanged);
            // 
            // tabGC
            // 
            this.tabGC.BackColor = System.Drawing.Color.White;
            this.tabGC.Controls.Add(this.label2);
            this.tabGC.Controls.Add(this.label16);
            this.tabGC.Controls.Add(this.txtGCNotes);
            this.tabGC.Controls.Add(this.txtGCNo);
            this.tabGC.Location = new System.Drawing.Point(4, 25);
            this.tabGC.Name = "tabGC";
            this.tabGC.Padding = new System.Windows.Forms.Padding(3);
            this.tabGC.Size = new System.Drawing.Size(316, 240);
            this.tabGC.TabIndex = 3;
            this.tabGC.Text = "Gift Certificate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(17, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 117;
            this.label2.Text = "Notes";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(17, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 120;
            this.label16.Text = "GC Number";
            // 
            // txtGCNotes
            // 
            this.txtGCNotes.Location = new System.Drawing.Point(109, 73);
            this.txtGCNotes.Multiline = true;
            this.txtGCNotes.Name = "txtGCNotes";
            this.txtGCNotes.Size = new System.Drawing.Size(184, 100);
            this.txtGCNotes.TabIndex = 116;
            this.txtGCNotes.TextChanged += new System.EventHandler(this.txtGCNotes_TextChanged);
            // 
            // txtGCNo
            // 
            this.txtGCNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGCNo.Location = new System.Drawing.Point(109, 31);
            this.txtGCNo.Name = "txtGCNo";
            this.txtGCNo.Size = new System.Drawing.Size(184, 20);
            this.txtGCNo.TabIndex = 115;
            this.txtGCNo.TextChanged += new System.EventHandler(this.txtGCNo_TextChanged);
            this.txtGCNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGCNo_KeyPress);
            // 
            // tabNotes
            // 
            this.tabNotes.BackColor = System.Drawing.Color.White;
            this.tabNotes.Controls.Add(this.label8);
            this.tabNotes.Controls.Add(this.txtPaymentNotes);
            this.tabNotes.Location = new System.Drawing.Point(4, 25);
            this.tabNotes.Name = "tabNotes";
            this.tabNotes.Padding = new System.Windows.Forms.Padding(3);
            this.tabNotes.Size = new System.Drawing.Size(316, 240);
            this.tabNotes.TabIndex = 2;
            this.tabNotes.Text = "Notes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(24, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Notes";
            // 
            // txtPaymentNotes
            // 
            this.txtPaymentNotes.Location = new System.Drawing.Point(41, 44);
            this.txtPaymentNotes.Multiline = true;
            this.txtPaymentNotes.Name = "txtPaymentNotes";
            this.txtPaymentNotes.Size = new System.Drawing.Size(234, 100);
            this.txtPaymentNotes.TabIndex = 47;
            this.txtPaymentNotes.TextChanged += new System.EventHandler(this.txtPaymentNotes_TextChanged);
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecord.Location = new System.Drawing.Point(674, 418);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(89, 32);
            this.btnRecord.TabIndex = 185;
            this.btnRecord.Text = "Record";
            this.btnRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(438, 418);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 32);
            this.btnClear.TabIndex = 184;
            this.btnClear.Text = "Clear ";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // AmountChange
            // 
            this.AmountChange.DecimalPlaces = 2;
            this.AmountChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountChange.ForeColor = System.Drawing.Color.DarkRed;
            this.AmountChange.Location = new System.Drawing.Point(235, 369);
            this.AmountChange.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.AmountChange.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.AmountChange.Name = "AmountChange";
            this.AmountChange.Size = new System.Drawing.Size(146, 26);
            this.AmountChange.TabIndex = 191;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(22, 374);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(148, 17);
            this.label14.TabIndex = 190;
            this.label14.Text = "AMOUNT CHANGE:";
            // 
            // BalanceDue
            // 
            this.BalanceDue.DecimalPlaces = 2;
            this.BalanceDue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BalanceDue.Location = new System.Drawing.Point(235, 401);
            this.BalanceDue.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.BalanceDue.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.BalanceDue.Name = "BalanceDue";
            this.BalanceDue.Size = new System.Drawing.Size(146, 26);
            this.BalanceDue.TabIndex = 193;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(22, 406);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 17);
            this.label15.TabIndex = 192;
            this.label15.Text = "BALANCE DUE:";
            // 
            // TenderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(775, 474);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtAuthorization);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(623, 513);
            this.Name = "TenderDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tender Details";
            this.Load += new System.EventHandler(this.TenderDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaymentMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountPaid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabCardNo.ResumeLayout(false);
            this.tabCardNo.PerformLayout();
            this.tabBankNo.ResumeLayout(false);
            this.tabBankNo.PerformLayout();
            this.tabGC.ResumeLayout(false);
            this.tabGC.PerformLayout();
            this.tabNotes.ResumeLayout(false);
            this.tabNotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmountChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BalanceDue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAuthorization;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCardNo;
        private System.Windows.Forms.Label txtNotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCardNotes;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.TabPage tabBankNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBankNotes;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.TextBox txtBankAccountNo;
        private System.Windows.Forms.TextBox txtBankAccountName;
        private System.Windows.Forms.TabPage tabNotes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPaymentNotes;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.NumericUpDown AmountPaid;
        private System.Windows.Forms.MaskedTextBox txtBankBSB;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label RecipientAccountName;
        private System.Windows.Forms.NumericUpDown txtTotalAmount;
        private System.Windows.Forms.DataGridView dgPaymentMethod;
        private System.Windows.Forms.TabPage tabGC;
        private System.Windows.Forms.MaskedTextBox txtCardExpiry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCardName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtGCNotes;
        private System.Windows.Forms.TextBox txtGCNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethodID;
        private System.Windows.Forms.DataGridViewImageColumn PaymentMethodLogo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentAuthorisationNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentCardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentNameOnCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentExpirationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentCardNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentBSB;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentBankAccountNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentBankAccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentChequeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentBankNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentGCNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentGCNotes;
        private System.Windows.Forms.NumericUpDown BalanceDue;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown AmountChange;
        private System.Windows.Forms.Label label14;
    }
}