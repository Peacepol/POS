namespace RestaurantPOS.References
{
    partial class Reminder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reminder));
            this.dgReminder = new System.Windows.Forms.DataGridView();
            this.Transaction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TranType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntityID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingMethodID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.record_btn = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgReminder)).BeginInit();
            this.SuspendLayout();
            // 
            // dgReminder
            // 
            this.dgReminder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReminder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Transaction,
            this.DueDate,
            this.Amount,
            this.TranType,
            this.EntityID,
            this.ShippingMethodID});
            this.dgReminder.Location = new System.Drawing.Point(10, 12);
            this.dgReminder.Name = "dgReminder";
            this.dgReminder.RowHeadersVisible = false;
            this.dgReminder.Size = new System.Drawing.Size(456, 170);
            this.dgReminder.TabIndex = 0;
            this.dgReminder.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgReminder_CellClick);
            this.dgReminder.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgReminder_CellDoubleClick);
            // 
            // Transaction
            // 
            this.Transaction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Transaction.HeaderText = "Transaction";
            this.Transaction.Name = "Transaction";
            this.Transaction.ReadOnly = true;
            this.Transaction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DueDate
            // 
            this.DueDate.HeaderText = "DueDate";
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;
            this.DueDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Amount.Width = 120;
            // 
            // TranType
            // 
            this.TranType.HeaderText = "TranType";
            this.TranType.Name = "TranType";
            this.TranType.ReadOnly = true;
            this.TranType.Visible = false;
            // 
            // EntityID
            // 
            this.EntityID.HeaderText = "EntityID";
            this.EntityID.Name = "EntityID";
            this.EntityID.ReadOnly = true;
            this.EntityID.Visible = false;
            // 
            // ShippingMethodID
            // 
            this.ShippingMethodID.HeaderText = "ShippingMethodID";
            this.ShippingMethodID.Name = "ShippingMethodID";
            this.ShippingMethodID.ReadOnly = true;
            this.ShippingMethodID.Visible = false;
            // 
            // record_btn
            // 
            this.record_btn.Image = ((System.Drawing.Image)(resources.GetObject("record_btn.Image")));
            this.record_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.record_btn.Location = new System.Drawing.Point(368, 188);
            this.record_btn.Name = "record_btn";
            this.record_btn.Size = new System.Drawing.Size(100, 34);
            this.record_btn.TabIndex = 193;
            this.record_btn.Text = "Record";
            this.record_btn.UseVisualStyleBackColor = true;
            this.record_btn.Click += new System.EventHandler(this.record_btn_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(368, 228);
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClose.Size = new System.Drawing.Size(100, 34);
            this.btnClose.TabIndex = 194;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSkip.Location = new System.Drawing.Point(12, 197);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(124, 25);
            this.btnSkip.TabIndex = 195;
            this.btnSkip.Text = "Skip This Period";
            this.btnSkip.UseVisualStyleBackColor = true;
            // 
            // Reminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 267);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.record_btn);
            this.Controls.Add(this.dgReminder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Reminder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reminder";
            this.Load += new System.EventHandler(this.Reminder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgReminder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgReminder;
        private System.Windows.Forms.Button record_btn;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transaction;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranType;
        private System.Windows.Forms.DataGridViewTextBoxColumn EntityID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingMethodID;
    }
}