namespace AbleRetailPOS.Reports.SalesReports
{
    partial class rptSalesReportDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptSalesReportDetail));
            this.cmbSalesStatus = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pdateTo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.pdateFrom = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtShipVia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmployee = new System.Windows.Forms.TextBox();
            this.txtAmountTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAmountFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.sdatePicker = new System.Windows.Forms.DateTimePicker();
            this.btn_SalespersonLookup = new System.Windows.Forms.PictureBox();
            this.btn_ShippingmethodLookup = new System.Windows.Forms.PictureBox();
            this.DateRange2 = new System.Windows.Forms.ComboBox();
            this.DateRange1 = new System.Windows.Forms.ComboBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.dgReport = new System.Windows.Forms.DataGridView();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.rdoAsc = new System.Windows.Forms.RadioButton();
            this.rdoDesc = new System.Windows.Forms.RadioButton();
            this.btnSortGrid = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btn_SalespersonLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShippingmethodLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSalesStatus
            // 
            this.cmbSalesStatus.FormattingEnabled = true;
            this.cmbSalesStatus.Items.AddRange(new object[] {
            "All Invoices",
            "Open Sales",
            "Orders",
            "Quotes",
            "Lay-By"});
            this.cmbSalesStatus.Location = new System.Drawing.Point(99, 203);
            this.cmbSalesStatus.Name = "cmbSalesStatus";
            this.cmbSalesStatus.Size = new System.Drawing.Size(221, 21);
            this.cmbSalesStatus.TabIndex = 47;
            this.cmbSalesStatus.Text = "All Invoices";
            this.cmbSalesStatus.SelectedIndexChanged += new System.EventHandler(this.cmbSalesStatus_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 46;
            this.label11.Text = "Sales Status :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(196, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "To :";
            // 
            // pdateTo
            // 
            this.pdateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.pdateTo.Location = new System.Drawing.Point(226, 176);
            this.pdateTo.Name = "pdateTo";
            this.pdateTo.Size = new System.Drawing.Size(94, 20);
            this.pdateTo.TabIndex = 44;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Promised Date :";
            // 
            // pdateFrom
            // 
            this.pdateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.pdateFrom.Location = new System.Drawing.Point(99, 176);
            this.pdateFrom.Name = "pdateFrom";
            this.pdateFrom.Size = new System.Drawing.Size(94, 20);
            this.pdateFrom.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Ship via :";
            // 
            // txtShipVia
            // 
            this.txtShipVia.Location = new System.Drawing.Point(89, 108);
            this.txtShipVia.Name = "txtShipVia";
            this.txtShipVia.Size = new System.Drawing.Size(213, 20);
            this.txtShipVia.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Employees :";
            // 
            // txtEmployee
            // 
            this.txtEmployee.Location = new System.Drawing.Point(89, 82);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(213, 20);
            this.txtEmployee.TabIndex = 34;
            // 
            // txtAmountTo
            // 
            this.txtAmountTo.Location = new System.Drawing.Point(216, 56);
            this.txtAmountTo.Name = "txtAmountTo";
            this.txtAmountTo.Size = new System.Drawing.Size(86, 20);
            this.txtAmountTo.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Amount from :";
            // 
            // txtAmountFrom
            // 
            this.txtAmountFrom.Location = new System.Drawing.Point(89, 56);
            this.txtAmountFrom.Name = "txtAmountFrom";
            this.txtAmountFrom.Size = new System.Drawing.Size(86, 20);
            this.txtAmountFrom.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "To :";
            // 
            // edatePicker
            // 
            this.edatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edatePicker.Location = new System.Drawing.Point(216, 25);
            this.edatePicker.Name = "edatePicker";
            this.edatePicker.Size = new System.Drawing.Size(86, 20);
            this.edatePicker.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Date from :";
            // 
            // sdatePicker
            // 
            this.sdatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdatePicker.Location = new System.Drawing.Point(89, 25);
            this.sdatePicker.Name = "sdatePicker";
            this.sdatePicker.Size = new System.Drawing.Size(86, 20);
            this.sdatePicker.TabIndex = 26;
            // 
            // btn_SalespersonLookup
            // 
            this.btn_SalespersonLookup.Image = ((System.Drawing.Image)(resources.GetObject("btn_SalespersonLookup.Image")));
            this.btn_SalespersonLookup.Location = new System.Drawing.Point(307, 83);
            this.btn_SalespersonLookup.Name = "btn_SalespersonLookup";
            this.btn_SalespersonLookup.Size = new System.Drawing.Size(19, 19);
            this.btn_SalespersonLookup.TabIndex = 230;
            this.btn_SalespersonLookup.TabStop = false;
            this.btn_SalespersonLookup.Click += new System.EventHandler(this.btn_SalespersonLookup_Click);
            // 
            // btn_ShippingmethodLookup
            // 
            this.btn_ShippingmethodLookup.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ShippingmethodLookup.Image = ((System.Drawing.Image)(resources.GetObject("btn_ShippingmethodLookup.Image")));
            this.btn_ShippingmethodLookup.Location = new System.Drawing.Point(307, 109);
            this.btn_ShippingmethodLookup.Name = "btn_ShippingmethodLookup";
            this.btn_ShippingmethodLookup.Size = new System.Drawing.Size(19, 19);
            this.btn_ShippingmethodLookup.TabIndex = 229;
            this.btn_ShippingmethodLookup.TabStop = false;
            this.btn_ShippingmethodLookup.Click += new System.EventHandler(this.btn_ShippingmethodLookup_Click);
            // 
            // DateRange2
            // 
            this.DateRange2.FormattingEnabled = true;
            this.DateRange2.Items.AddRange(new object[] {
            "DATE RANGE",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June"});
            this.DateRange2.Location = new System.Drawing.Point(227, 149);
            this.DateRange2.Name = "DateRange2";
            this.DateRange2.Size = new System.Drawing.Size(94, 21);
            this.DateRange2.TabIndex = 235;
            this.DateRange2.Text = "DATE RANGE";
            this.DateRange2.SelectedIndexChanged += new System.EventHandler(this.DateRange2_SelectedIndexChanged_1);
            // 
            // DateRange1
            // 
            this.DateRange1.FormattingEnabled = true;
            this.DateRange1.Items.AddRange(new object[] {
            "DATE RANGE",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June"});
            this.DateRange1.Location = new System.Drawing.Point(99, 149);
            this.DateRange1.Name = "DateRange1";
            this.DateRange1.Size = new System.Drawing.Size(94, 21);
            this.DateRange1.TabIndex = 236;
            this.DateRange1.Text = "DATE RANGE";
            this.DateRange1.SelectedIndexChanged += new System.EventHandler(this.DateRange1_SelectedIndexChanged);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplay.Image")));
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(14, 309);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(100, 36);
            this.btnDisplay.TabIndex = 238;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::AbleRetailPOS.Properties.Resources.print24;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(226, 309);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 36);
            this.btnPrint.TabIndex = 237;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::AbleRetailPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(226, 351);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 36);
            this.btnPrintGrid.TabIndex = 240;
            this.btnPrintGrid.Text = "Print Grid";
            this.btnPrintGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintGrid.UseVisualStyleBackColor = true;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // dgReport
            // 
            this.dgReport.AllowUserToAddRows = false;
            this.dgReport.AllowUserToDeleteRows = false;
            this.dgReport.AllowUserToResizeRows = false;
            this.dgReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgReport.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReport.Location = new System.Drawing.Point(337, 12);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.Size = new System.Drawing.Size(794, 470);
            this.dgReport.TabIndex = 239;
            // 
            // cbSort
            // 
            this.cbSort.Enabled = false;
            this.cbSort.FormattingEnabled = true;
            this.cbSort.Location = new System.Drawing.Point(20, 259);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(196, 21);
            this.cbSort.TabIndex = 241;
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Checked = true;
            this.rdoAsc.Location = new System.Drawing.Point(20, 286);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 242;
            this.rdoAsc.TabStop = true;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.UseVisualStyleBackColor = true;
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Location = new System.Drawing.Point(134, 286);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 243;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.UseVisualStyleBackColor = true;
            // 
            // btnSortGrid
            // 
            this.btnSortGrid.Enabled = false;
            this.btnSortGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortGrid.Location = new System.Drawing.Point(226, 259);
            this.btnSortGrid.Name = "btnSortGrid";
            this.btnSortGrid.Size = new System.Drawing.Size(86, 22);
            this.btnSortGrid.TabIndex = 244;
            this.btnSortGrid.Text = "Sort Grid";
            this.btnSortGrid.UseVisualStyleBackColor = true;
            this.btnSortGrid.Click += new System.EventHandler(this.btnSortGrid_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 245;
            this.label6.Text = "Sort Grid By:";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(14, 351);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 36);
            this.btnExportExcel.TabIndex = 252;
            this.btnExportExcel.Text = "       Export";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_btn.Image = global::AbleRetailPOS.Properties.Resources.clear24;
            this.cancel_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancel_btn.Location = new System.Drawing.Point(120, 309);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(100, 36);
            this.cancel_btn.TabIndex = 300;
            this.cancel_btn.Text = "      Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // rptSalesReportDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 494);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSortGrid);
            this.Controls.Add(this.rdoDesc);
            this.Controls.Add(this.rdoAsc);
            this.Controls.Add(this.cbSort);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.dgReport);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.DateRange2);
            this.Controls.Add(this.DateRange1);
            this.Controls.Add(this.btn_SalespersonLookup);
            this.Controls.Add(this.btn_ShippingmethodLookup);
            this.Controls.Add(this.cmbSalesStatus);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pdateTo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pdateFrom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtShipVia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.txtAmountTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAmountFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edatePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sdatePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "rptSalesReportDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Sales Detail";
            this.Load += new System.EventHandler(this.SalesReportDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_SalespersonLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShippingmethodLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbSalesStatus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker pdateTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker pdateFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtShipVia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmployee;
        private System.Windows.Forms.TextBox txtAmountTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAmountFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker edatePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker sdatePicker;
        private System.Windows.Forms.PictureBox btn_SalespersonLookup;
        private System.Windows.Forms.PictureBox btn_ShippingmethodLookup;
        private System.Windows.Forms.ComboBox DateRange2;
        private System.Windows.Forms.ComboBox DateRange1;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.DataGridView dgReport;
        private System.Windows.Forms.ComboBox cbSort;
        private System.Windows.Forms.RadioButton rdoAsc;
        private System.Windows.Forms.RadioButton rdoDesc;
        private System.Windows.Forms.Button btnSortGrid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button cancel_btn;
    }
}