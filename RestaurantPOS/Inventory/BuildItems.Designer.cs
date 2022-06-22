namespace AbleRetailPOS.Inventory
{
    partial class BuildItems
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.trandate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTranNo = new System.Windows.Forms.Label();
            this.grpTran = new System.Windows.Forms.GroupBox();
            this.lblID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.dgridItems = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutOfBalance = new System.Windows.Forms.NumericUpDown();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Job = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpTran.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutOfBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(140, 69);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(332, 20);
            this.txtMemo.TabIndex = 213;
            // 
            // trandate
            // 
            this.trandate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.trandate.Location = new System.Drawing.Point(140, 43);
            this.trandate.Name = "trandate";
            this.trandate.Size = new System.Drawing.Size(200, 20);
            this.trandate.TabIndex = 212;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 211;
            this.label8.Text = "Transaction Date:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 210;
            this.label7.Text = "Memo: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 214;
            this.label1.Text = "Item Adjustment No:";
            // 
            // lblTranNo
            // 
            this.lblTranNo.AutoSize = true;
            this.lblTranNo.Location = new System.Drawing.Point(125, 16);
            this.lblTranNo.Name = "lblTranNo";
            this.lblTranNo.Size = new System.Drawing.Size(0, 13);
            this.lblTranNo.TabIndex = 215;
            // 
            // grpTran
            // 
            this.grpTran.Controls.Add(this.lblID);
            this.grpTran.Controls.Add(this.label3);
            this.grpTran.Controls.Add(this.lblTranNo);
            this.grpTran.Controls.Add(this.label1);
            this.grpTran.Location = new System.Drawing.Point(497, 12);
            this.grpTran.Name = "grpTran";
            this.grpTran.Size = new System.Drawing.Size(266, 64);
            this.grpTran.TabIndex = 216;
            this.grpTran.TabStop = false;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(125, 37);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 13);
            this.lblID.TabIndex = 217;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 216;
            this.label3.Text = "Record ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 217;
            this.label2.Text = "Item Adjustment Type:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(140, 23);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(58, 13);
            this.lblType.TabIndex = 218;
            this.lblType.Text = "Build Items";
            // 
            // dgridItems
            // 
            this.dgridItems.AllowUserToAddRows = false;
            this.dgridItems.AllowUserToDeleteRows = false;
            this.dgridItems.AllowUserToResizeColumns = false;
            this.dgridItems.AllowUserToResizeRows = false;
            this.dgridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNumber,
            this.Description,
            this.Quantity,
            this.UnitCost,
            this.Amount,
            this.Account,
            this.Job,
            this.LineMemo,
            this.ItemID,
            this.AccountID,
            this.JobID});
            this.dgridItems.Location = new System.Drawing.Point(12, 113);
            this.dgridItems.Name = "dgridItems";
            this.dgridItems.RowHeadersVisible = false;
            this.dgridItems.Size = new System.Drawing.Size(776, 440);
            this.dgridItems.TabIndex = 219;
            this.dgridItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellContentClick);
            this.dgridItems.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellDoubleClick);
            this.dgridItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellEndEdit);
            this.dgridItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgridItems_CellFormatting);
            this.dgridItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgridItems_EditingControlShowing);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 577);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 220;
            this.label4.Text = "Out Of Balance:";
            // 
            // txtOutOfBalance
            // 
            this.txtOutOfBalance.DecimalPlaces = 2;
            this.txtOutOfBalance.Location = new System.Drawing.Point(131, 573);
            this.txtOutOfBalance.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.txtOutOfBalance.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.txtOutOfBalance.Name = "txtOutOfBalance";
            this.txtOutOfBalance.Size = new System.Drawing.Size(120, 20);
            this.txtOutOfBalance.TabIndex = 221;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemove.Image = global::AbleRetailPOS.Properties.Resources.remove24;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(595, 573);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(98, 34);
            this.btnRemove.TabIndex = 224;
            this.btnRemove.Text = "    Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::AbleRetailPOS.Properties.Resources.print24;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(478, 573);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(111, 34);
            this.btnPrint.TabIndex = 223;
            this.btnPrint.Text = "Print Report";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnRecord
            // 
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRecord.ForeColor = System.Drawing.Color.Black;
            this.btnRecord.Image = global::AbleRetailPOS.Properties.Resources.Save24;
            this.btnRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecord.Location = new System.Drawing.Point(699, 573);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(89, 34);
            this.btnRecord.TabIndex = 222;
            this.btnRecord.Text = "Record";
            this.btnRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click_1);
            // 
            // PartNumber
            // 
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.Name = "PartNumber";
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            // 
            // UnitCost
            // 
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.UnitCost.DefaultCellStyle = dataGridViewCellStyle1;
            this.UnitCost.HeaderText = "Unit Cost";
            this.UnitCost.Name = "UnitCost";
            // 
            // Amount
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle2;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // Account
            // 
            this.Account.HeaderText = "Account";
            this.Account.Name = "Account";
            this.Account.Visible = false;
            // 
            // Job
            // 
            this.Job.HeaderText = "Job";
            this.Job.Name = "Job";
            // 
            // LineMemo
            // 
            this.LineMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LineMemo.HeaderText = "Memo";
            this.LineMemo.Name = "LineMemo";
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.Visible = false;
            // 
            // AccountID
            // 
            this.AccountID.HeaderText = "AccountID";
            this.AccountID.Name = "AccountID";
            this.AccountID.Visible = false;
            // 
            // JobID
            // 
            this.JobID.HeaderText = "JobID";
            this.JobID.Name = "JobID";
            this.JobID.Visible = false;
            // 
            // BuildItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 622);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.txtOutOfBalance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgridItems);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpTran);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.trandate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.MaximizeBox = false;
            this.Name = "BuildItems";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build Inventory";
            this.Load += new System.EventHandler(this.BuildItems_Load);
            this.grpTran.ResumeLayout(false);
            this.grpTran.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutOfBalance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.DateTimePicker trandate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTranNo;
        private System.Windows.Forms.GroupBox grpTran;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.DataGridView dgridItems;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtOutOfBalance;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn Job;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobID;
    }
}