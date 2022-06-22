namespace AbleRetailPOS.Setup
{
    partial class SessionTransactions
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
            this.dgSession = new System.Windows.Forms.DataGridView();
            this.InvoiceNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtFloat = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalSale = new System.Windows.Forms.NumericUpDown();
            this.btnSaveFloat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOpeningFund = new System.Windows.Forms.NumericUpDown();
            this.dgTender = new System.Windows.Forms.DataGridView();
            this.PaymentMeth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discrepancy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentMethodID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSummary = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAR = new System.Windows.Forms.CheckBox();
            this.chkEntryDate = new System.Windows.Forms.CheckBox();
            this.chkPayment = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTerminalName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSessionStart = new System.Windows.Forms.Label();
            this.lblTerminalID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalCollection = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgSession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningFund)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTender)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // dgSession
            // 
            this.dgSession.AllowUserToAddRows = false;
            this.dgSession.AllowUserToDeleteRows = false;
            this.dgSession.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSession.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceNum,
            this.TotalAmount,
            this.TotalPaid,
            this.Balance});
            this.dgSession.Location = new System.Drawing.Point(12, 31);
            this.dgSession.Name = "dgSession";
            this.dgSession.RowHeadersVisible = false;
            this.dgSession.Size = new System.Drawing.Size(409, 516);
            this.dgSession.TabIndex = 0;
            this.dgSession.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgSession_CellFormatting);
            // 
            // InvoiceNum
            // 
            this.InvoiceNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InvoiceNum.HeaderText = "Invoice #";
            this.InvoiceNum.Name = "InvoiceNum";
            this.InvoiceNum.ReadOnly = true;
            // 
            // TotalAmount
            // 
            this.TotalAmount.HeaderText = "Total Amount";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            // 
            // TotalPaid
            // 
            this.TotalPaid.HeaderText = "Total Paid";
            this.TotalPaid.Name = "TotalPaid";
            this.TotalPaid.ReadOnly = true;
            // 
            // Balance
            // 
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            // 
            // txtFloat
            // 
            this.txtFloat.DecimalPlaces = 2;
            this.txtFloat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFloat.Location = new System.Drawing.Point(447, 210);
            this.txtFloat.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtFloat.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.txtFloat.Name = "txtFloat";
            this.txtFloat.Size = new System.Drawing.Size(151, 29);
            this.txtFloat.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(444, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Float :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(444, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total Session Sales:";
            // 
            // txtTotalSale
            // 
            this.txtTotalSale.DecimalPlaces = 2;
            this.txtTotalSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalSale.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalSale.Location = new System.Drawing.Point(447, 91);
            this.txtTotalSale.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtTotalSale.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.txtTotalSale.Name = "txtTotalSale";
            this.txtTotalSale.Size = new System.Drawing.Size(151, 29);
            this.txtTotalSale.TabIndex = 3;
            // 
            // btnSaveFloat
            // 
            this.btnSaveFloat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveFloat.Location = new System.Drawing.Point(440, 266);
            this.btnSaveFloat.Name = "btnSaveFloat";
            this.btnSaveFloat.Size = new System.Drawing.Size(151, 87);
            this.btnSaveFloat.TabIndex = 5;
            this.btnSaveFloat.Text = "End Session";
            this.btnSaveFloat.UseVisualStyleBackColor = true;
            this.btnSaveFloat.Click += new System.EventHandler(this.btnSaveFloat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(444, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Opening Fund:";
            // 
            // txtOpeningFund
            // 
            this.txtOpeningFund.DecimalPlaces = 2;
            this.txtOpeningFund.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningFund.Location = new System.Drawing.Point(447, 31);
            this.txtOpeningFund.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtOpeningFund.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.txtOpeningFund.Name = "txtOpeningFund";
            this.txtOpeningFund.Size = new System.Drawing.Size(151, 29);
            this.txtOpeningFund.TabIndex = 6;
            // 
            // dgTender
            // 
            this.dgTender.AllowUserToAddRows = false;
            this.dgTender.AllowUserToDeleteRows = false;
            this.dgTender.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTender.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PaymentMeth,
            this.Amount,
            this.Count,
            this.Discrepancy,
            this.PaymentMethodID});
            this.dgTender.Location = new System.Drawing.Point(613, 31);
            this.dgTender.Name = "dgTender";
            this.dgTender.RowHeadersVisible = false;
            this.dgTender.Size = new System.Drawing.Size(436, 516);
            this.dgTender.TabIndex = 8;
            this.dgTender.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTotalTender_CellContentClick);
            this.dgTender.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTotalTender_CellEndEdit);
            this.dgTender.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgTotalTender_CellFormatting);
            this.dgTender.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgTotalTender_EditingControlShowing);
            // 
            // PaymentMeth
            // 
            this.PaymentMeth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PaymentMeth.HeaderText = "Payment Methods";
            this.PaymentMeth.Name = "PaymentMeth";
            this.PaymentMeth.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // Count
            // 
            this.Count.HeaderText = "Total Count";
            this.Count.Name = "Count";
            // 
            // Discrepancy
            // 
            this.Discrepancy.HeaderText = "Discrepancy";
            this.Discrepancy.Name = "Discrepancy";
            this.Discrepancy.ReadOnly = true;
            // 
            // PaymentMethodID
            // 
            this.PaymentMethodID.HeaderText = "PaymentMethodID";
            this.PaymentMethodID.Name = "PaymentMethodID";
            this.PaymentMethodID.ReadOnly = true;
            this.PaymentMethodID.Visible = false;
            // 
            // chkSummary
            // 
            this.chkSummary.AutoSize = true;
            this.chkSummary.Checked = true;
            this.chkSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSummary.Location = new System.Drawing.Point(20, 30);
            this.chkSummary.Name = "chkSummary";
            this.chkSummary.Size = new System.Drawing.Size(144, 17);
            this.chkSummary.TabIndex = 9;
            this.chkSummary.Text = "Session Summary Report";
            this.chkSummary.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAR);
            this.groupBox1.Controls.Add(this.chkEntryDate);
            this.groupBox1.Controls.Add(this.chkPayment);
            this.groupBox1.Controls.Add(this.chkSummary);
            this.groupBox1.Location = new System.Drawing.Point(427, 372);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 167);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reports to Display";
            // 
            // chkAR
            // 
            this.chkAR.AutoSize = true;
            this.chkAR.Checked = true;
            this.chkAR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAR.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chkAR.Location = new System.Drawing.Point(20, 127);
            this.chkAR.Name = "chkAR";
            this.chkAR.Size = new System.Drawing.Size(129, 18);
            this.chkAR.TabIndex = 12;
            this.chkAR.Text = "List of A/R Payments";
            this.chkAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAR.ThreeState = true;
            this.chkAR.UseCompatibleTextRendering = true;
            this.chkAR.UseVisualStyleBackColor = true;
            // 
            // chkEntryDate
            // 
            this.chkEntryDate.AutoSize = true;
            this.chkEntryDate.Checked = true;
            this.chkEntryDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEntryDate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chkEntryDate.Location = new System.Drawing.Point(20, 90);
            this.chkEntryDate.Name = "chkEntryDate";
            this.chkEntryDate.Size = new System.Drawing.Size(125, 31);
            this.chkEntryDate.TabIndex = 11;
            this.chkEntryDate.Text = "List of Transaction \nBy Entry Date";
            this.chkEntryDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEntryDate.ThreeState = true;
            this.chkEntryDate.UseCompatibleTextRendering = true;
            this.chkEntryDate.UseVisualStyleBackColor = true;
            // 
            // chkPayment
            // 
            this.chkPayment.AutoSize = true;
            this.chkPayment.Checked = true;
            this.chkPayment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPayment.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chkPayment.Location = new System.Drawing.Point(20, 53);
            this.chkPayment.Name = "chkPayment";
            this.chkPayment.Size = new System.Drawing.Size(130, 31);
            this.chkPayment.TabIndex = 10;
            this.chkPayment.Text = "List of Transactions \nBy Payment Method";
            this.chkPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPayment.ThreeState = true;
            this.chkPayment.UseCompatibleTextRendering = true;
            this.chkPayment.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Terminal ID:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(151, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Terminal Name:";
            // 
            // lblTerminalName
            // 
            this.lblTerminalName.AutoSize = true;
            this.lblTerminalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalName.Location = new System.Drawing.Point(239, 12);
            this.lblTerminalName.Name = "lblTerminalName";
            this.lblTerminalName.Size = new System.Drawing.Size(0, 13);
            this.lblTerminalName.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(613, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Session Start:";
            // 
            // lblSessionStart
            // 
            this.lblSessionStart.AutoSize = true;
            this.lblSessionStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionStart.Location = new System.Drawing.Point(706, 12);
            this.lblSessionStart.Name = "lblSessionStart";
            this.lblSessionStart.Size = new System.Drawing.Size(0, 13);
            this.lblSessionStart.TabIndex = 16;
            // 
            // lblTerminalID
            // 
            this.lblTerminalID.AutoSize = true;
            this.lblTerminalID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalID.Location = new System.Drawing.Point(83, 12);
            this.lblTerminalID.Name = "lblTerminalID";
            this.lblTerminalID.Size = new System.Drawing.Size(0, 13);
            this.lblTerminalID.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(444, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Total Session Collection:";
            // 
            // txtTotalCollection
            // 
            this.txtTotalCollection.DecimalPlaces = 2;
            this.txtTotalCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCollection.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTotalCollection.Location = new System.Drawing.Point(447, 152);
            this.txtTotalCollection.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtTotalCollection.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.txtTotalCollection.Name = "txtTotalCollection";
            this.txtTotalCollection.Size = new System.Drawing.Size(151, 29);
            this.txtTotalCollection.TabIndex = 17;
            // 
            // SessionTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 551);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTotalCollection);
            this.Controls.Add(this.lblSessionStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTerminalName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTerminalID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgTender);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOpeningFund);
            this.Controls.Add(this.btnSaveFloat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTotalSale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFloat);
            this.Controls.Add(this.dgSession);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SessionTransactions";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session Transactions";
            this.Load += new System.EventHandler(this.SessionTransactions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgSession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningFund)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTender)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCollection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgSession;
        private System.Windows.Forms.NumericUpDown txtFloat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtTotalSale;
        private System.Windows.Forms.Button btnSaveFloat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtOpeningFund;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.DataGridView dgTender;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMeth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discrepancy;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentMethodID;
        private System.Windows.Forms.CheckBox chkSummary;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkPayment;
        private System.Windows.Forms.CheckBox chkEntryDate;
        private System.Windows.Forms.CheckBox chkAR;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTerminalName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSessionStart;
        private System.Windows.Forms.Label lblTerminalID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtTotalCollection;
    }
}