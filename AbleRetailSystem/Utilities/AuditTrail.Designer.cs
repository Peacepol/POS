namespace RestaurantPOS.Utilities
{
    partial class AuditTrail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditTrail));
            this.edateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.edate = new System.Windows.Forms.Label();
            this.sdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.sdate = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.dgAudit = new System.Windows.Forms.DataGridView();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtForm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbForm = new System.Windows.Forms.PictureBox();
            this.pbCustomer = new System.Windows.Forms.PictureBox();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AffectedRecord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OldData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // edateTimePicker
            // 
            this.edateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edateTimePicker.Location = new System.Drawing.Point(254, 45);
            this.edateTimePicker.Name = "edateTimePicker";
            this.edateTimePicker.Size = new System.Drawing.Size(106, 20);
            this.edateTimePicker.TabIndex = 193;
            this.edateTimePicker.ValueChanged += new System.EventHandler(this.edateTimePicker_ValueChanged);
            // 
            // edate
            // 
            this.edate.AutoSize = true;
            this.edate.Location = new System.Drawing.Point(229, 45);
            this.edate.Name = "edate";
            this.edate.Size = new System.Drawing.Size(19, 13);
            this.edate.TabIndex = 192;
            this.edate.Text = "to:";
            // 
            // sdateTimePicker
            // 
            this.sdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdateTimePicker.Location = new System.Drawing.Point(76, 45);
            this.sdateTimePicker.Name = "sdateTimePicker";
            this.sdateTimePicker.Size = new System.Drawing.Size(98, 20);
            this.sdateTimePicker.TabIndex = 191;
            this.sdateTimePicker.ValueChanged += new System.EventHandler(this.sdateTimePicker_ValueChanged);
            // 
            // sdate
            // 
            this.sdate.AutoSize = true;
            this.sdate.Location = new System.Drawing.Point(16, 45);
            this.sdate.Name = "sdate";
            this.sdate.Size = new System.Drawing.Size(56, 13);
            this.sdate.TabIndex = 190;
            this.sdate.Text = "Date from:";
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(24, 15);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(46, 13);
            this.lblFilter.TabIndex = 189;
            this.lblFilter.Text = "User ID:";
            // 
            // dgAudit
            // 
            this.dgAudit.AllowUserToAddRows = false;
            this.dgAudit.AllowUserToDeleteRows = false;
            this.dgAudit.AllowUserToOrderColumns = true;
            this.dgAudit.AllowUserToResizeColumns = false;
            this.dgAudit.AllowUserToResizeRows = false;
            this.dgAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAudit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.Fullname,
            this.Date,
            this.FormName,
            this.AuditAction,
            this.AffectedRecord,
            this.OldData,
            this.NewData,
            this.LocationID});
            this.dgAudit.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgAudit.Location = new System.Drawing.Point(12, 80);
            this.dgAudit.Name = "dgAudit";
            this.dgAudit.RowHeadersVisible = false;
            this.dgAudit.Size = new System.Drawing.Size(1045, 358);
            this.dgAudit.TabIndex = 194;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(76, 13);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(100, 20);
            this.txtUserID.TabIndex = 196;
            this.txtUserID.Text = "All";
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            // 
            // txtForm
            // 
            this.txtForm.Location = new System.Drawing.Point(270, 12);
            this.txtForm.Name = "txtForm";
            this.txtForm.Size = new System.Drawing.Size(100, 20);
            this.txtForm.TabIndex = 199;
            this.txtForm.Text = "All";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 197;
            this.label1.Text = "Form :";
            // 
            // pbForm
            // 
            this.pbForm.BackColor = System.Drawing.SystemColors.Control;
            this.pbForm.Image = ((System.Drawing.Image)(resources.GetObject("pbForm.Image")));
            this.pbForm.Location = new System.Drawing.Point(376, 12);
            this.pbForm.Name = "pbForm";
            this.pbForm.Size = new System.Drawing.Size(19, 19);
            this.pbForm.TabIndex = 198;
            this.pbForm.TabStop = false;
            this.pbForm.Click += new System.EventHandler(this.pbForm_Click);
            // 
            // pbCustomer
            // 
            this.pbCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.pbCustomer.Image = ((System.Drawing.Image)(resources.GetObject("pbCustomer.Image")));
            this.pbCustomer.Location = new System.Drawing.Point(180, 15);
            this.pbCustomer.Name = "pbCustomer";
            this.pbCustomer.Size = new System.Drawing.Size(19, 19);
            this.pbCustomer.TabIndex = 195;
            this.pbCustomer.TabStop = false;
            this.pbCustomer.Click += new System.EventHandler(this.pbCustomer_Click);
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(957, 45);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 31);
            this.btnPrintGrid.TabIndex = 264;
            this.btnPrintGrid.Text = "Print Grid";
            this.btnPrintGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintGrid.UseVisualStyleBackColor = true;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // Username
            // 
            this.Username.HeaderText = "Username";
            this.Username.Name = "Username";
            this.Username.ReadOnly = true;
            this.Username.Width = 80;
            // 
            // Fullname
            // 
            this.Fullname.HeaderText = "Full Name";
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 80;
            // 
            // FormName
            // 
            this.FormName.HeaderText = "Form Name";
            this.FormName.Name = "FormName";
            this.FormName.ReadOnly = true;
            // 
            // AuditAction
            // 
            this.AuditAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AuditAction.HeaderText = "Audit Action";
            this.AuditAction.Name = "AuditAction";
            this.AuditAction.ReadOnly = true;
            this.AuditAction.Width = 300;
            // 
            // AffectedRecord
            // 
            this.AffectedRecord.HeaderText = "Affected Record";
            this.AffectedRecord.Name = "AffectedRecord";
            // 
            // OldData
            // 
            this.OldData.HeaderText = "Old Data";
            this.OldData.Name = "OldData";
            this.OldData.ReadOnly = true;
            // 
            // NewData
            // 
            this.NewData.HeaderText = "New Data";
            this.NewData.Name = "NewData";
            this.NewData.ReadOnly = true;
            // 
            // LocationID
            // 
            this.LocationID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocationID.HeaderText = "Location ID";
            this.LocationID.Name = "LocationID";
            // 
            // AuditTrail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 450);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.txtForm);
            this.Controls.Add(this.pbForm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.pbCustomer);
            this.Controls.Add(this.dgAudit);
            this.Controls.Add(this.edateTimePicker);
            this.Controls.Add(this.edate);
            this.Controls.Add(this.sdateTimePicker);
            this.Controls.Add(this.sdate);
            this.Controls.Add(this.lblFilter);
            this.Name = "AuditTrail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Audit Trail";
            this.Load += new System.EventHandler(this.dgAudit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker edateTimePicker;
        private System.Windows.Forms.Label edate;
        private System.Windows.Forms.DateTimePicker sdateTimePicker;
        private System.Windows.Forms.Label sdate;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.DataGridView dgAudit;
        private System.Windows.Forms.PictureBox pbCustomer;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtForm;
        private System.Windows.Forms.PictureBox pbForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn AffectedRecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn OldData;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewData;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationID;
    }
}