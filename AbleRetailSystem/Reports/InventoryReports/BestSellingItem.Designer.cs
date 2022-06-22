namespace RestaurantPOS.Reports.InventoryReports
{
    partial class BestSellingItem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BestSellingItem));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdispCount = new System.Windows.Forms.NumericUpDown();
            this.sDate = new System.Windows.Forms.DateTimePicker();
            this.eDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbArrange = new System.Windows.Forms.ComboBox();
            this.rdBest = new System.Windows.Forms.RadioButton();
            this.rdLeast = new System.Windows.Forms.RadioButton();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.dgReport = new System.Windows.Forms.DataGridView();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainCat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubCat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtySold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProfitMargin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnSortGrid = new System.Windows.Forms.Button();
            this.rdoDesc = new System.Windows.Forms.RadioButton();
            this.rdoAsc = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtdispCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(208, 345);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(83, 40);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Print";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Display Top ";
            // 
            // txtdispCount
            // 
            this.txtdispCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdispCount.Location = new System.Drawing.Point(95, 32);
            this.txtdispCount.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtdispCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.txtdispCount.Name = "txtdispCount";
            this.txtdispCount.Size = new System.Drawing.Size(68, 20);
            this.txtdispCount.TabIndex = 6;
            this.txtdispCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // sDate
            // 
            this.sDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sDate.Location = new System.Drawing.Point(99, 62);
            this.sDate.Name = "sDate";
            this.sDate.Size = new System.Drawing.Size(104, 20);
            this.sDate.TabIndex = 7;
            // 
            // eDate
            // 
            this.eDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eDate.Location = new System.Drawing.Point(235, 62);
            this.eDate.Name = "eDate";
            this.eDate.Size = new System.Drawing.Size(98, 20);
            this.eDate.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Date Range From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Arrange by :";
            // 
            // cmbArrange
            // 
            this.cmbArrange.FormattingEnabled = true;
            this.cmbArrange.Items.AddRange(new object[] {
            "Total Quantity Sold",
            "Total Sales Amount",
            "Total Profit"});
            this.cmbArrange.Location = new System.Drawing.Point(93, 97);
            this.cmbArrange.Name = "cmbArrange";
            this.cmbArrange.Size = new System.Drawing.Size(179, 21);
            this.cmbArrange.TabIndex = 12;
            // 
            // rdBest
            // 
            this.rdBest.AutoSize = true;
            this.rdBest.Checked = true;
            this.rdBest.Location = new System.Drawing.Point(26, 9);
            this.rdBest.Name = "rdBest";
            this.rdBest.Size = new System.Drawing.Size(103, 17);
            this.rdBest.TabIndex = 13;
            this.rdBest.TabStop = true;
            this.rdBest.Text = "Best Selling Item";
            this.rdBest.UseVisualStyleBackColor = true;
            // 
            // rdLeast
            // 
            this.rdLeast.AutoSize = true;
            this.rdLeast.Location = new System.Drawing.Point(164, 9);
            this.rdLeast.Name = "rdLeast";
            this.rdLeast.Size = new System.Drawing.Size(108, 17);
            this.rdLeast.TabIndex = 14;
            this.rdLeast.Text = "Least Selling Item";
            this.rdLeast.UseVisualStyleBackColor = true;
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCategory.Location = new System.Drawing.Point(93, 124);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(198, 215);
            this.treeCategory.TabIndex = 15;
            this.treeCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterCheck);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Category :";
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
            this.PartNumber,
            this.ItemNo,
            this.MainCat,
            this.SubCat,
            this.QtySold,
            this.TotalCost,
            this.TotalSales,
            this.TotalProfit,
            this.TotalProfitMargin});
            this.dgReport.Location = new System.Drawing.Point(345, 1);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.RowTemplate.Height = 30;
            this.dgReport.Size = new System.Drawing.Size(805, 500);
            this.dgReport.TabIndex = 213;
            this.dgReport.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgReport_CellFormatting);
            // 
            // PartNumber
            // 
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.Name = "PartNumber";
            this.PartNumber.ReadOnly = true;
            // 
            // ItemNo
            // 
            this.ItemNo.HeaderText = "Item No";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            // 
            // MainCat
            // 
            this.MainCat.HeaderText = "Main Category";
            this.MainCat.Name = "MainCat";
            this.MainCat.ReadOnly = true;
            // 
            // SubCat
            // 
            this.SubCat.HeaderText = "SubCategory";
            this.SubCat.Name = "SubCat";
            this.SubCat.ReadOnly = true;
            // 
            // QtySold
            // 
            this.QtySold.HeaderText = "Qty Sold";
            this.QtySold.Name = "QtySold";
            this.QtySold.ReadOnly = true;
            // 
            // TotalCost
            // 
            this.TotalCost.HeaderText = "Total Cost";
            this.TotalCost.Name = "TotalCost";
            this.TotalCost.ReadOnly = true;
            // 
            // TotalSales
            // 
            this.TotalSales.HeaderText = "Total Sales(Exclusive)";
            this.TotalSales.Name = "TotalSales";
            this.TotalSales.ReadOnly = true;
            // 
            // TotalProfit
            // 
            this.TotalProfit.HeaderText = "Total Profit";
            this.TotalProfit.Name = "TotalProfit";
            this.TotalProfit.ReadOnly = true;
            // 
            // TotalProfitMargin
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TotalProfitMargin.DefaultCellStyle = dataGridViewCellStyle2;
            this.TotalProfitMargin.HeaderText = "Total Profit Margin";
            this.TotalProfitMargin.Name = "TotalProfitMargin";
            this.TotalProfitMargin.ReadOnly = true;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplay.Image")));
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(95, 345);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(92, 40);
            this.btnDisplay.TabIndex = 214;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnSortGrid
            // 
            this.btnSortGrid.Enabled = false;
            this.btnSortGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortGrid.Location = new System.Drawing.Point(255, 405);
            this.btnSortGrid.Name = "btnSortGrid";
            this.btnSortGrid.Size = new System.Drawing.Size(78, 22);
            this.btnSortGrid.TabIndex = 257;
            this.btnSortGrid.Text = "Sort Grid";
            this.btnSortGrid.UseVisualStyleBackColor = true;
            this.btnSortGrid.Click += new System.EventHandler(this.btnSortGrid_Click);
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Location = new System.Drawing.Point(167, 432);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 256;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.UseVisualStyleBackColor = true;
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Checked = true;
            this.rdoAsc.Location = new System.Drawing.Point(68, 432);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 255;
            this.rdoAsc.TabStop = true;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 389);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 254;
            this.label6.Text = "Sort Grid By :";
            // 
            // cmbSort
            // 
            this.cmbSort.Enabled = false;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(35, 405);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(214, 21);
            this.cmbSort.TabIndex = 253;
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(87, 465);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 36);
            this.btnPrintGrid.TabIndex = 261;
            this.btnPrintGrid.Text = "Print Grid";
            this.btnPrintGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintGrid.UseVisualStyleBackColor = true;
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(208, 465);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 36);
            this.btnExportExcel.TabIndex = 270;
            this.btnExportExcel.Text = "       Export";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // BestSellingItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 503);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.btnSortGrid);
            this.Controls.Add(this.rdoDesc);
            this.Controls.Add(this.rdoAsc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.dgReport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.treeCategory);
            this.Controls.Add(this.rdLeast);
            this.Controls.Add(this.rdBest);
            this.Controls.Add(this.cmbArrange);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.eDate);
            this.Controls.Add(this.sDate);
            this.Controls.Add(this.txtdispCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BestSellingItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Best or Least Selling Item";
            this.Load += new System.EventHandler(this.BestSellingItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtdispCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtdispCount;
        private System.Windows.Forms.DateTimePicker sDate;
        private System.Windows.Forms.DateTimePicker eDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbArrange;
        private System.Windows.Forms.RadioButton rdBest;
        private System.Windows.Forms.RadioButton rdLeast;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgReport;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnSortGrid;
        private System.Windows.Forms.RadioButton rdoDesc;
        private System.Windows.Forms.RadioButton rdoAsc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MainCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtySold;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalProfit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalProfitMargin;
    }
}