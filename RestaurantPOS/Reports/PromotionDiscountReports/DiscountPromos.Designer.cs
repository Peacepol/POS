namespace AbleRetailPOS.Reports.PromotionDiscountReports
{
    partial class DiscountPromos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscountPromos));
            this.btnExportExcell = new System.Windows.Forms.Button();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.dgReport = new System.Windows.Forms.DataGridView();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmountEx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmountInc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalDiscountAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromoCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtmTxTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmTxFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.customerText = new System.Windows.Forms.TextBox();
            this.pbAccount = new System.Windows.Forms.PictureBox();
            this.pbSalesperson = new System.Windows.Forms.PictureBox();
            this.txtSalesperson = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesperson)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportExcell
            // 
            this.btnExportExcell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcell.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcell.Image")));
            this.btnExportExcell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcell.Location = new System.Drawing.Point(16, 406);
            this.btnExportExcell.Name = "btnExportExcell";
            this.btnExportExcell.Size = new System.Drawing.Size(100, 36);
            this.btnExportExcell.TabIndex = 284;
            this.btnExportExcell.Text = "       Export";
            this.btnExportExcell.UseVisualStyleBackColor = true;
            this.btnExportExcell.Click += new System.EventHandler(this.btnExportExcell_Click);
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::AbleRetailPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(228, 406);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 36);
            this.btnPrintGrid.TabIndex = 283;
            this.btnPrintGrid.Text = "Print Grid";
            this.btnPrintGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintGrid.UseVisualStyleBackColor = true;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = global::AbleRetailPOS.Properties.Resources.print24;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(228, 364);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 36);
            this.btnGenerate.TabIndex = 281;
            this.btnGenerate.Text = "Print";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplay.Image")));
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(16, 364);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(100, 36);
            this.btnDisplay.TabIndex = 282;
            this.btnDisplay.Text = "     Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
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
            this.CustomerName,
            this.SalesNumber,
            this.TransactionDate,
            this.PartNumber,
            this.ItemNumber,
            this.ItemName,
            this.Qty,
            this.OriginalUnitPrice,
            this.UnitPrice,
            this.TotalAmountEx,
            this.TotalAmountInc,
            this.Discount,
            this.TotalDiscountAmount,
            this.User,
            this.PromoCode});
            this.dgReport.Location = new System.Drawing.Point(334, 18);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.Size = new System.Drawing.Size(1072, 424);
            this.dgReport.TabIndex = 280;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // SalesNumber
            // 
            this.SalesNumber.HeaderText = "Sales Number";
            this.SalesNumber.Name = "SalesNumber";
            this.SalesNumber.ReadOnly = true;
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
            // OriginalUnitPrice
            // 
            this.OriginalUnitPrice.HeaderText = "Original Unit Price";
            this.OriginalUnitPrice.Name = "OriginalUnitPrice";
            this.OriginalUnitPrice.ReadOnly = true;
            // 
            // UnitPrice
            // 
            this.UnitPrice.HeaderText = "Unit Price";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            // 
            // TotalAmountEx
            // 
            this.TotalAmountEx.HeaderText = "Total Amount (Ex)";
            this.TotalAmountEx.Name = "TotalAmountEx";
            this.TotalAmountEx.ReadOnly = true;
            // 
            // TotalAmountInc
            // 
            this.TotalAmountInc.HeaderText = "Total Amount (Inc)";
            this.TotalAmountInc.Name = "TotalAmountInc";
            this.TotalAmountInc.ReadOnly = true;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Discount Percentage";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            // 
            // TotalDiscountAmount
            // 
            this.TotalDiscountAmount.HeaderText = "Total Discount Amount";
            this.TotalDiscountAmount.Name = "TotalDiscountAmount";
            this.TotalDiscountAmount.ReadOnly = true;
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            // 
            // PromoCode
            // 
            this.PromoCode.HeaderText = "Promo Code";
            this.PromoCode.Name = "PromoCode";
            this.PromoCode.ReadOnly = true;
            // 
            // dtmTxTo
            // 
            this.dtmTxTo.Location = new System.Drawing.Point(59, 45);
            this.dtmTxTo.Name = "dtmTxTo";
            this.dtmTxTo.Size = new System.Drawing.Size(232, 20);
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
            this.dtmTxFrom.Size = new System.Drawing.Size(232, 20);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtmTxTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtmTxFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 80);
            this.groupBox1.TabIndex = 277;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transaction Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 286;
            this.label3.Text = "Category :";
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCategory.Location = new System.Drawing.Point(71, 150);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(233, 208);
            this.treeCategory.TabIndex = 285;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(13, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 287;
            this.label4.Text = "Customer:";
            // 
            // customerText
            // 
            this.customerText.Location = new System.Drawing.Point(71, 98);
            this.customerText.Name = "customerText";
            this.customerText.Size = new System.Drawing.Size(233, 20);
            this.customerText.TabIndex = 288;
            // 
            // pbAccount
            // 
            this.pbAccount.BackColor = System.Drawing.SystemColors.Control;
            this.pbAccount.Image = ((System.Drawing.Image)(resources.GetObject("pbAccount.Image")));
            this.pbAccount.Location = new System.Drawing.Point(309, 99);
            this.pbAccount.Name = "pbAccount";
            this.pbAccount.Size = new System.Drawing.Size(19, 19);
            this.pbAccount.TabIndex = 289;
            this.pbAccount.TabStop = false;
            this.pbAccount.Click += new System.EventHandler(this.pbAccount_Click);
            // 
            // pbSalesperson
            // 
            this.pbSalesperson.BackColor = System.Drawing.Color.Transparent;
            this.pbSalesperson.Image = ((System.Drawing.Image)(resources.GetObject("pbSalesperson.Image")));
            this.pbSalesperson.Location = new System.Drawing.Point(309, 128);
            this.pbSalesperson.Name = "pbSalesperson";
            this.pbSalesperson.Size = new System.Drawing.Size(19, 19);
            this.pbSalesperson.TabIndex = 291;
            this.pbSalesperson.TabStop = false;
            this.pbSalesperson.Click += new System.EventHandler(this.pbSalesperson_Click);
            // 
            // txtSalesperson
            // 
            this.txtSalesperson.Location = new System.Drawing.Point(71, 124);
            this.txtSalesperson.Name = "txtSalesperson";
            this.txtSalesperson.Size = new System.Drawing.Size(232, 20);
            this.txtSalesperson.TabIndex = 292;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 290;
            this.label5.Text = "User :";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::AbleRetailPOS.Properties.Resources.clear24;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(122, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 359;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DiscountPromos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1418, 454);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbSalesperson);
            this.Controls.Add(this.txtSalesperson);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customerText);
            this.Controls.Add(this.pbAccount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeCategory);
            this.Controls.Add(this.btnExportExcell);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.dgReport);
            this.Controls.Add(this.groupBox1);
            this.Name = "DiscountPromos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Discount Promos";
            this.Load += new System.EventHandler(this.DiscountPromos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalesperson)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExportExcell;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.DataGridView dgReport;
        private System.Windows.Forms.DateTimePicker dtmTxTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtmTxFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox customerText;
        private System.Windows.Forms.PictureBox pbAccount;
        private System.Windows.Forms.PictureBox pbSalesperson;
        private System.Windows.Forms.TextBox txtSalesperson;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmountEx;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmountInc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalDiscountAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromoCode;
        private System.Windows.Forms.Button btnCancel;
    }
}