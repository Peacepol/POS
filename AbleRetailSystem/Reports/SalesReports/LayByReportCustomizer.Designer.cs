namespace RestaurantPOS.Reports.SalesReports
{
    partial class LayByReportCustomizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayByReportCustomizer));
            this.dgReport = new System.Windows.Forms.DataGridView();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.btnSortGrid = new System.Windows.Forms.Button();
            this.rdoDesc = new System.Windows.Forms.RadioButton();
            this.rdoAsc = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.DateRangesEnd = new System.Windows.Forms.ComboBox();
            this.DateRangesStart = new System.Windows.Forms.ComboBox();
            this.PEdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.PSdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
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
            this.btnExportExcell = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_SalespersonLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShippingmethodLookup)).BeginInit();
            this.SuspendLayout();
            // 
            // dgReport
            // 
            this.dgReport.AllowUserToAddRows = false;
            this.dgReport.AllowUserToDeleteRows = false;
            this.dgReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgReport.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReport.Location = new System.Drawing.Point(337, 7);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.Size = new System.Drawing.Size(810, 488);
            this.dgReport.TabIndex = 317;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = global::RestaurantPOS.Properties.Resources.refresh32;
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(52, 202);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(96, 38);
            this.btnDisplay.TabIndex = 316;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click_1);
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(19, 327);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 30);
            this.btnPrintGrid.TabIndex = 315;
            this.btnPrintGrid.Text = "Print Grid";
            this.btnPrintGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintGrid.UseVisualStyleBackColor = true;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // btnSortGrid
            // 
            this.btnSortGrid.Enabled = false;
            this.btnSortGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortGrid.Location = new System.Drawing.Point(239, 264);
            this.btnSortGrid.Name = "btnSortGrid";
            this.btnSortGrid.Size = new System.Drawing.Size(78, 22);
            this.btnSortGrid.TabIndex = 314;
            this.btnSortGrid.Text = "Sort Grid";
            this.btnSortGrid.UseVisualStyleBackColor = true;
            this.btnSortGrid.Click += new System.EventHandler(this.btnSortGrid_Click);
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Location = new System.Drawing.Point(151, 291);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 313;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.UseVisualStyleBackColor = true;
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Checked = true;
            this.rdoAsc.Location = new System.Drawing.Point(52, 291);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 312;
            this.rdoAsc.TabStop = true;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 248);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 311;
            this.label6.Text = "Sort Grid By :";
            // 
            // cmbSort
            // 
            this.cmbSort.Enabled = false;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(19, 264);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(214, 21);
            this.cmbSort.TabIndex = 310;
            // 
            // DateRangesEnd
            // 
            this.DateRangesEnd.FormattingEnabled = true;
            this.DateRangesEnd.Items.AddRange(new object[] {
            "Date Range",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.DateRangesEnd.Location = new System.Drawing.Point(222, 138);
            this.DateRangesEnd.Name = "DateRangesEnd";
            this.DateRangesEnd.Size = new System.Drawing.Size(94, 21);
            this.DateRangesEnd.TabIndex = 309;
            this.DateRangesEnd.Text = "DATE RANGE";
            // 
            // DateRangesStart
            // 
            this.DateRangesStart.FormattingEnabled = true;
            this.DateRangesStart.Items.AddRange(new object[] {
            "Date Range",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.DateRangesStart.Location = new System.Drawing.Point(94, 138);
            this.DateRangesStart.Name = "DateRangesStart";
            this.DateRangesStart.Size = new System.Drawing.Size(94, 21);
            this.DateRangesStart.TabIndex = 308;
            this.DateRangesStart.Text = "DATE RANGE";
            // 
            // PEdateTimePicker
            // 
            this.PEdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PEdateTimePicker.Location = new System.Drawing.Point(221, 165);
            this.PEdateTimePicker.Name = "PEdateTimePicker";
            this.PEdateTimePicker.Size = new System.Drawing.Size(94, 20);
            this.PEdateTimePicker.TabIndex = 307;
            // 
            // PSdateTimePicker
            // 
            this.PSdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PSdateTimePicker.Location = new System.Drawing.Point(94, 165);
            this.PSdateTimePicker.Name = "PSdateTimePicker";
            this.PSdateTimePicker.Size = new System.Drawing.Size(94, 20);
            this.PSdateTimePicker.TabIndex = 306;
            // 
            // cancel_btn
            // 
            this.cancel_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_btn.Image = global::RestaurantPOS.Properties.Resources.clear24;
            this.cancel_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancel_btn.Location = new System.Drawing.Point(238, 202);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(93, 40);
            this.cancel_btn.TabIndex = 305;
            this.cancel_btn.Text = "      Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(156, 201);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(77, 40);
            this.btnGenerate.TabIndex = 302;
            this.btnGenerate.Text = "Print";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(191, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 299;
            this.label9.Text = "To :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 298;
            this.label10.Text = "Promised Date :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 297;
            this.label8.Text = "Ship via :";
            // 
            // txtShipVia
            // 
            this.txtShipVia.Location = new System.Drawing.Point(94, 112);
            this.txtShipVia.Name = "txtShipVia";
            this.txtShipVia.Size = new System.Drawing.Size(213, 20);
            this.txtShipVia.TabIndex = 296;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 295;
            this.label5.Text = "Employees :";
            // 
            // txtEmployee
            // 
            this.txtEmployee.Location = new System.Drawing.Point(94, 86);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(213, 20);
            this.txtEmployee.TabIndex = 294;
            // 
            // txtAmountTo
            // 
            this.txtAmountTo.Location = new System.Drawing.Point(221, 60);
            this.txtAmountTo.Name = "txtAmountTo";
            this.txtAmountTo.Size = new System.Drawing.Size(86, 20);
            this.txtAmountTo.TabIndex = 293;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 292;
            this.label4.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 291;
            this.label3.Text = "Amount from :";
            // 
            // txtAmountFrom
            // 
            this.txtAmountFrom.Location = new System.Drawing.Point(94, 60);
            this.txtAmountFrom.Name = "txtAmountFrom";
            this.txtAmountFrom.Size = new System.Drawing.Size(86, 20);
            this.txtAmountFrom.TabIndex = 290;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 289;
            this.label2.Text = "To :";
            // 
            // edatePicker
            // 
            this.edatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edatePicker.Location = new System.Drawing.Point(221, 29);
            this.edatePicker.Name = "edatePicker";
            this.edatePicker.Size = new System.Drawing.Size(86, 20);
            this.edatePicker.TabIndex = 288;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 287;
            this.label1.Text = "Date from :";
            // 
            // sdatePicker
            // 
            this.sdatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdatePicker.Location = new System.Drawing.Point(94, 29);
            this.sdatePicker.Name = "sdatePicker";
            this.sdatePicker.Size = new System.Drawing.Size(86, 20);
            this.sdatePicker.TabIndex = 286;
            // 
            // btn_SalespersonLookup
            // 
            this.btn_SalespersonLookup.Image = ((System.Drawing.Image)(resources.GetObject("btn_SalespersonLookup.Image")));
            this.btn_SalespersonLookup.Location = new System.Drawing.Point(312, 87);
            this.btn_SalespersonLookup.Name = "btn_SalespersonLookup";
            this.btn_SalespersonLookup.Size = new System.Drawing.Size(19, 19);
            this.btn_SalespersonLookup.TabIndex = 319;
            this.btn_SalespersonLookup.TabStop = false;
            this.btn_SalespersonLookup.Click += new System.EventHandler(this.btn_SalespersonLookup_Click);
            // 
            // btn_ShippingmethodLookup
            // 
            this.btn_ShippingmethodLookup.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ShippingmethodLookup.Image = ((System.Drawing.Image)(resources.GetObject("btn_ShippingmethodLookup.Image")));
            this.btn_ShippingmethodLookup.Location = new System.Drawing.Point(312, 113);
            this.btn_ShippingmethodLookup.Name = "btn_ShippingmethodLookup";
            this.btn_ShippingmethodLookup.Size = new System.Drawing.Size(19, 19);
            this.btn_ShippingmethodLookup.TabIndex = 318;
            this.btn_ShippingmethodLookup.TabStop = false;
            this.btn_ShippingmethodLookup.Click += new System.EventHandler(this.btn_ShippingmethodLookup_Click);
            // 
            // btnExportExcell
            // 
            this.btnExportExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcell.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcell.Image")));
            this.btnExportExcell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcell.Location = new System.Drawing.Point(217, 327);
            this.btnExportExcell.Name = "btnExportExcell";
            this.btnExportExcell.Size = new System.Drawing.Size(100, 30);
            this.btnExportExcell.TabIndex = 328;
            this.btnExportExcell.Text = "       Export";
            this.btnExportExcell.UseVisualStyleBackColor = true;
            this.btnExportExcell.Click += new System.EventHandler(this.btnExportExcell_Click);
            // 
            // LayByReportCustomizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 503);
            this.Controls.Add(this.btnExportExcell);
            this.Controls.Add(this.btn_SalespersonLookup);
            this.Controls.Add(this.btn_ShippingmethodLookup);
            this.Controls.Add(this.dgReport);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.btnSortGrid);
            this.Controls.Add(this.rdoDesc);
            this.Controls.Add(this.rdoAsc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.DateRangesEnd);
            this.Controls.Add(this.DateRangesStart);
            this.Controls.Add(this.PEdateTimePicker);
            this.Controls.Add(this.PSdateTimePicker);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
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
            this.Name = "LayByReportCustomizer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Lay By Report";
            this.Load += new System.EventHandler(this.LayByReportCustomizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_SalespersonLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShippingmethodLookup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgReport;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.Button btnSortGrid;
        private System.Windows.Forms.RadioButton rdoDesc;
        private System.Windows.Forms.RadioButton rdoAsc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.ComboBox DateRangesEnd;
        private System.Windows.Forms.ComboBox DateRangesStart;
        private System.Windows.Forms.DateTimePicker PEdateTimePicker;
        private System.Windows.Forms.DateTimePicker PSdateTimePicker;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
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
        private System.Windows.Forms.Button btnExportExcell;
    }
}