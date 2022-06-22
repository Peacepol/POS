namespace RestaurantPOS.Reports.PromotionDiscountReports
{
    partial class SalesByPromos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesByPromos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtmTxTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmTxFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPromoCode = new System.Windows.Forms.TextBox();
            this.dgReport = new System.Windows.Forms.DataGridView();
            this.PromoCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromoType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmountEx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Profit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitMargin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExportExcell = new System.Windows.Forms.Button();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSortGrid = new System.Windows.Forms.Button();
            this.rdoDesc = new System.Windows.Forms.RadioButton();
            this.rdoAsc = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtmTxTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtmTxFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 80);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transaction Date";
            // 
            // dtmTxTo
            // 
            this.dtmTxTo.Location = new System.Drawing.Point(59, 45);
            this.dtmTxTo.Name = "dtmTxTo";
            this.dtmTxTo.Size = new System.Drawing.Size(200, 20);
            this.dtmTxTo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "To: ";
            // 
            // dtmTxFrom
            // 
            this.dtmTxFrom.Location = new System.Drawing.Point(59, 19);
            this.dtmTxFrom.Name = "dtmTxFrom";
            this.dtmTxFrom.Size = new System.Drawing.Size(200, 20);
            this.dtmTxFrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "From: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Promo Code :";
            // 
            // txtPromoCode
            // 
            this.txtPromoCode.Location = new System.Drawing.Point(91, 98);
            this.txtPromoCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPromoCode.Name = "txtPromoCode";
            this.txtPromoCode.Size = new System.Drawing.Size(186, 20);
            this.txtPromoCode.TabIndex = 11;
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
            this.dgReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PromoCode,
            this.PromoType,
            this.SalesNumber,
            this.CustomerName,
            this.TransactionDate,
            this.PartNumber,
            this.ItemNumber,
            this.ItemName,
            this.Qty,
            this.TotalAmountEx,
            this.TotalCost,
            this.Profit,
            this.ProfitMargin});
            this.dgReport.Location = new System.Drawing.Point(316, 0);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.Size = new System.Drawing.Size(964, 454);
            this.dgReport.TabIndex = 270;
            // 
            // PromoCode
            // 
            this.PromoCode.HeaderText = "Promo Code";
            this.PromoCode.Name = "PromoCode";
            this.PromoCode.ReadOnly = true;
            // 
            // PromoType
            // 
            this.PromoType.HeaderText = "Promo Type";
            this.PromoType.Name = "PromoType";
            this.PromoType.ReadOnly = true;
            // 
            // SalesNumber
            // 
            this.SalesNumber.HeaderText = "Sales Number";
            this.SalesNumber.Name = "SalesNumber";
            this.SalesNumber.ReadOnly = true;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // TransactionDate
            // 
            this.TransactionDate.HeaderText = "Transaction Date";
            this.TransactionDate.Name = "TransactionDate";
            this.TransactionDate.ReadOnly = true;
            // 
            // PartNumber
            // 
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.Name = "PartNumber";
            this.PartNumber.ReadOnly = true;
            // 
            // ItemNumber
            // 
            this.ItemNumber.HeaderText = "Item Number";
            this.ItemNumber.Name = "ItemNumber";
            this.ItemNumber.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Quantity";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // TotalAmountEx
            // 
            this.TotalAmountEx.HeaderText = "Total Amount";
            this.TotalAmountEx.Name = "TotalAmountEx";
            this.TotalAmountEx.ReadOnly = true;
            // 
            // TotalCost
            // 
            this.TotalCost.HeaderText = "Total Cost";
            this.TotalCost.Name = "TotalCost";
            this.TotalCost.ReadOnly = true;
            // 
            // Profit
            // 
            this.Profit.HeaderText = "Profit";
            this.Profit.Name = "Profit";
            this.Profit.ReadOnly = true;
            // 
            // ProfitMargin
            // 
            this.ProfitMargin.HeaderText = "Profit Margin";
            this.ProfitMargin.Name = "ProfitMargin";
            this.ProfitMargin.ReadOnly = true;
            // 
            // btnExportExcell
            // 
            this.btnExportExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcell.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcell.Image")));
            this.btnExportExcell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcell.Location = new System.Drawing.Point(186, 246);
            this.btnExportExcell.Name = "btnExportExcell";
            this.btnExportExcell.Size = new System.Drawing.Size(93, 30);
            this.btnExportExcell.TabIndex = 276;
            this.btnExportExcell.Text = "       Export";
            this.btnExportExcell.UseVisualStyleBackColor = true;
            this.btnExportExcell.Click += new System.EventHandler(this.btnExportExcell_Click);
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(17, 246);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 30);
            this.btnPrintGrid.TabIndex = 275;
            this.btnPrintGrid.Text = "Print Grid";
            this.btnPrintGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintGrid.UseVisualStyleBackColor = true;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplay.Image")));
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(15, 126);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(100, 38);
            this.btnDisplay.TabIndex = 274;
            this.btnDisplay.Text = "     Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(184, 124);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(93, 40);
            this.btnGenerate.TabIndex = 273;
            this.btnGenerate.Text = "Print";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSortGrid
            // 
            this.btnSortGrid.Enabled = false;
            this.btnSortGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortGrid.Location = new System.Drawing.Point(226, 196);
            this.btnSortGrid.Name = "btnSortGrid";
            this.btnSortGrid.Size = new System.Drawing.Size(78, 22);
            this.btnSortGrid.TabIndex = 368;
            this.btnSortGrid.Text = "Sort Grid";
            this.btnSortGrid.UseVisualStyleBackColor = true;
            this.btnSortGrid.Click += new System.EventHandler(this.btnSortGrid_Click);
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Location = new System.Drawing.Point(138, 223);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 367;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.UseVisualStyleBackColor = true;
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Checked = true;
            this.rdoAsc.Location = new System.Drawing.Point(39, 223);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 366;
            this.rdoAsc.TabStop = true;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 180);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 365;
            this.label6.Text = "Sort Grid By :";
            // 
            // cmbSort
            // 
            this.cmbSort.Enabled = false;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(6, 196);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(214, 21);
            this.cmbSort.TabIndex = 364;
            // 
            // SalesByPromos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 454);
            this.Controls.Add(this.btnSortGrid);
            this.Controls.Add(this.rdoDesc);
            this.Controls.Add(this.rdoAsc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.btnExportExcell);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dgReport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPromoCode);
            this.Controls.Add(this.groupBox1);
            this.Name = "SalesByPromos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Sales By Promos";
            this.Load += new System.EventHandler(this.SalesByPromos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtmTxTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtmTxFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPromoCode;
        private System.Windows.Forms.DataGridView dgReport;
        private System.Windows.Forms.Button btnExportExcell;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromoCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromoType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmountEx;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Profit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitMargin;
        private System.Windows.Forms.Button btnSortGrid;
        private System.Windows.Forms.RadioButton rdoDesc;
        private System.Windows.Forms.RadioButton rdoAsc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSort;
    }
}