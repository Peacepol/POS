namespace RestaurantPOS.Reports.PurchaseReports
{
    partial class AllPurchaseReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllPurchaseReport));
            this.btnCancel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.PEdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.PSdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtShipVia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmployee = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.sdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.fromNum = new System.Windows.Forms.NumericUpDown();
            this.toNum = new System.Windows.Forms.NumericUpDown();
            this.pbCustomer = new System.Windows.Forms.PictureBox();
            this.PurchaseStatuscb = new System.Windows.Forms.ComboBox();
            this.DateRangesStart = new System.Windows.Forms.ComboBox();
            this.DateRangesEnd = new System.Windows.Forms.ComboBox();
            this.pbEmployee = new System.Windows.Forms.PictureBox();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rdoAsc = new System.Windows.Forms.RadioButton();
            this.rdoDesc = new System.Windows.Forms.RadioButton();
            this.btnSortGrid = new System.Windows.Forms.Button();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnExportExcell = new System.Windows.Forms.Button();
            this.dgReport = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.fromNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(212, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 38);
            this.btnCancel.TabIndex = 96;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 94;
            this.label11.Text = "P.O. Status :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(185, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 93;
            this.label9.Text = "To :";
            // 
            // PEdateTimePicker
            // 
            this.PEdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PEdateTimePicker.Location = new System.Drawing.Point(217, 157);
            this.PEdateTimePicker.Name = "PEdateTimePicker";
            this.PEdateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.PEdateTimePicker.TabIndex = 92;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 91;
            this.label10.Text = "Promised Date :";
            // 
            // PSdateTimePicker
            // 
            this.PSdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PSdateTimePicker.Location = new System.Drawing.Point(90, 157);
            this.PSdateTimePicker.Name = "PSdateTimePicker";
            this.PSdateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.PSdateTimePicker.TabIndex = 90;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "Ship via :";
            // 
            // txtShipVia
            // 
            this.txtShipVia.Location = new System.Drawing.Point(90, 104);
            this.txtShipVia.Name = "txtShipVia";
            this.txtShipVia.Size = new System.Drawing.Size(213, 20);
            this.txtShipVia.TabIndex = 88;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 83;
            this.label5.Text = "Created By :";
            // 
            // txtEmployee
            // 
            this.txtEmployee.Location = new System.Drawing.Point(90, 78);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(213, 20);
            this.txtEmployee.TabIndex = 82;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "Amount from :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "To :";
            // 
            // edateTimePicker
            // 
            this.edateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edateTimePicker.Location = new System.Drawing.Point(217, 19);
            this.edateTimePicker.Name = "edateTimePicker";
            this.edateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.edateTimePicker.TabIndex = 76;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Date from :";
            // 
            // sdateTimePicker
            // 
            this.sdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdateTimePicker.Location = new System.Drawing.Point(90, 19);
            this.sdateTimePicker.Name = "sdateTimePicker";
            this.sdateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.sdateTimePicker.TabIndex = 74;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(130, 223);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(76, 38);
            this.btnDisplay.TabIndex = 73;
            this.btnDisplay.Text = "Print";
            this.btnDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // fromNum
            // 
            this.fromNum.Location = new System.Drawing.Point(90, 50);
            this.fromNum.Name = "fromNum";
            this.fromNum.Size = new System.Drawing.Size(90, 20);
            this.fromNum.TabIndex = 97;
            // 
            // toNum
            // 
            this.toNum.AllowDrop = true;
            this.toNum.InterceptArrowKeys = false;
            this.toNum.Location = new System.Drawing.Point(217, 50);
            this.toNum.Name = "toNum";
            this.toNum.Size = new System.Drawing.Size(90, 20);
            this.toNum.TabIndex = 98;
            this.toNum.ValueChanged += new System.EventHandler(this.toNum_ValueChanged);
            // 
            // pbCustomer
            // 
            this.pbCustomer.Image = ((System.Drawing.Image)(resources.GetObject("pbCustomer.Image")));
            this.pbCustomer.Location = new System.Drawing.Point(307, 105);
            this.pbCustomer.Name = "pbCustomer";
            this.pbCustomer.Size = new System.Drawing.Size(19, 19);
            this.pbCustomer.TabIndex = 202;
            this.pbCustomer.TabStop = false;
            this.pbCustomer.Click += new System.EventHandler(this.pbCustomer_Click);
            // 
            // PurchaseStatuscb
            // 
            this.PurchaseStatuscb.FormattingEnabled = true;
            this.PurchaseStatuscb.Items.AddRange(new object[] {
            "All PO",
            "New",
            "Active",
            "Backordered",
            "Completed"});
            this.PurchaseStatuscb.Location = new System.Drawing.Point(90, 189);
            this.PurchaseStatuscb.Name = "PurchaseStatuscb";
            this.PurchaseStatuscb.Size = new System.Drawing.Size(216, 21);
            this.PurchaseStatuscb.TabIndex = 203;
            this.PurchaseStatuscb.Text = "All Purchase Orders";
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
            this.DateRangesStart.Location = new System.Drawing.Point(90, 130);
            this.DateRangesStart.Name = "DateRangesStart";
            this.DateRangesStart.Size = new System.Drawing.Size(90, 21);
            this.DateRangesStart.TabIndex = 204;
            this.DateRangesStart.Text = "Date Range";
            this.DateRangesStart.SelectedIndexChanged += new System.EventHandler(this.DateRangesStart_SelectedIndexChanged);
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
            this.DateRangesEnd.Location = new System.Drawing.Point(217, 130);
            this.DateRangesEnd.Name = "DateRangesEnd";
            this.DateRangesEnd.Size = new System.Drawing.Size(90, 21);
            this.DateRangesEnd.TabIndex = 205;
            this.DateRangesEnd.Text = "Date Range";
            this.DateRangesEnd.SelectedIndexChanged += new System.EventHandler(this.DateRangesEnd_SelectedIndexChanged);
            // 
            // pbEmployee
            // 
            this.pbEmployee.Image = ((System.Drawing.Image)(resources.GetObject("pbEmployee.Image")));
            this.pbEmployee.Location = new System.Drawing.Point(307, 80);
            this.pbEmployee.Name = "pbEmployee";
            this.pbEmployee.Size = new System.Drawing.Size(19, 19);
            this.pbEmployee.TabIndex = 206;
            this.pbEmployee.TabStop = false;
            this.pbEmployee.Click += new System.EventHandler(this.pbEmployee_Click);
            // 
            // cmbSort
            // 
            this.cmbSort.Enabled = false;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(28, 288);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(214, 21);
            this.cmbSort.TabIndex = 319;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 272);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 320;
            this.label6.Text = "Sort Grid By :";
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Checked = true;
            this.rdoAsc.Location = new System.Drawing.Point(61, 315);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 321;
            this.rdoAsc.TabStop = true;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.UseVisualStyleBackColor = true;
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Location = new System.Drawing.Point(160, 315);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 322;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.UseVisualStyleBackColor = true;
            // 
            // btnSortGrid
            // 
            this.btnSortGrid.Enabled = false;
            this.btnSortGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortGrid.Location = new System.Drawing.Point(248, 288);
            this.btnSortGrid.Name = "btnSortGrid";
            this.btnSortGrid.Size = new System.Drawing.Size(78, 22);
            this.btnSortGrid.TabIndex = 323;
            this.btnSortGrid.Text = "Sort Grid";
            this.btnSortGrid.UseVisualStyleBackColor = true;
            this.btnSortGrid.Click += new System.EventHandler(this.btnSortGrid_Click);
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(28, 351);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 30);
            this.btnPrintGrid.TabIndex = 324;
            this.btnPrintGrid.Text = "Print Grid";
            this.btnPrintGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintGrid.UseVisualStyleBackColor = true;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::RestaurantPOS.Properties.Resources.refresh24X24;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(39, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 38);
            this.button1.TabIndex = 325;
            this.button1.Text = "Display";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExportExcell
            // 
            this.btnExportExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcell.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcell.Image")));
            this.btnExportExcell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcell.Location = new System.Drawing.Point(207, 351);
            this.btnExportExcell.Name = "btnExportExcell";
            this.btnExportExcell.Size = new System.Drawing.Size(100, 30);
            this.btnExportExcell.TabIndex = 326;
            this.btnExportExcell.Text = "       Export";
            this.btnExportExcell.UseVisualStyleBackColor = true;
            this.btnExportExcell.Click += new System.EventHandler(this.btnExportExcell_Click);
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
            this.dgReport.Location = new System.Drawing.Point(333, 1);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.Size = new System.Drawing.Size(817, 501);
            this.dgReport.TabIndex = 346;
            // 
            // AllPurchaseReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 503);
            this.Controls.Add(this.dgReport);
            this.Controls.Add(this.btnExportExcell);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.btnSortGrid);
            this.Controls.Add(this.rdoDesc);
            this.Controls.Add(this.rdoAsc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.pbEmployee);
            this.Controls.Add(this.DateRangesEnd);
            this.Controls.Add(this.DateRangesStart);
            this.Controls.Add(this.PurchaseStatuscb);
            this.Controls.Add(this.pbCustomer);
            this.Controls.Add(this.toNum);
            this.Controls.Add(this.fromNum);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.PEdateTimePicker);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.PSdateTimePicker);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtShipVia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sdateTimePicker);
            this.Controls.Add(this.btnDisplay);
            this.Name = "AllPurchaseReport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customiser - All Purchase";
            this.Load += new System.EventHandler(this.AllPurchaseReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fromNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker PEdateTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker PSdateTimePicker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtShipVia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmployee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker edateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker sdateTimePicker;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.NumericUpDown fromNum;
        private System.Windows.Forms.NumericUpDown toNum;
        private System.Windows.Forms.PictureBox pbCustomer;
        private System.Windows.Forms.ComboBox PurchaseStatuscb;
        private System.Windows.Forms.ComboBox DateRangesStart;
        private System.Windows.Forms.ComboBox DateRangesEnd;
        private System.Windows.Forms.PictureBox pbEmployee;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdoAsc;
        private System.Windows.Forms.RadioButton rdoDesc;
        private System.Windows.Forms.Button btnSortGrid;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnExportExcell;
        private System.Windows.Forms.DataGridView dgReport;
    }
}