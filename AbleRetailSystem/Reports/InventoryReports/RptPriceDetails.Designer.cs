namespace RestaurantPOS.Reports.InventoryReports
{
    partial class RptPriceDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptPriceDetails));
            this.cmbPriceLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.pbList3 = new System.Windows.Forms.PictureBox();
            this.pbList2 = new System.Windows.Forms.PictureBox();
            this.pbList1 = new System.Windows.Forms.PictureBox();
            this.txtList3 = new System.Windows.Forms.TextBox();
            this.txtList2 = new System.Windows.Forms.TextBox();
            this.txtList1 = new System.Windows.Forms.TextBox();
            this.pbSupplier = new System.Windows.Forms.PictureBox();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.dgReport = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyPerSellingUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSortGrid = new System.Windows.Forms.Button();
            this.rdoDesc = new System.Windows.Forms.RadioButton();
            this.rdoAsc = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.btnPrintGrid = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbList3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPriceLevel
            // 
            this.cmbPriceLevel.FormattingEnabled = true;
            this.cmbPriceLevel.Items.AddRange(new object[] {
            "All",
            "Level0",
            "Level1",
            "Level2",
            "Level3",
            "Level4",
            "Level5",
            "Level6",
            "Level7",
            "Level8",
            "Level9",
            "Level10",
            "Level11",
            "Level12"});
            this.cmbPriceLevel.Location = new System.Drawing.Point(87, 11);
            this.cmbPriceLevel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPriceLevel.Name = "cmbPriceLevel";
            this.cmbPriceLevel.Size = new System.Drawing.Size(180, 21);
            this.cmbPriceLevel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Price Level:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(190, 359);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(78, 40);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "Print";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 227;
            this.label3.Text = "Category :";
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCategory.Location = new System.Drawing.Point(87, 139);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(182, 215);
            this.treeCategory.TabIndex = 226;
            this.treeCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterCheck);
            // 
            // pbList3
            // 
            this.pbList3.Image = ((System.Drawing.Image)(resources.GetObject("pbList3.Image")));
            this.pbList3.Location = new System.Drawing.Point(272, 90);
            this.pbList3.Name = "pbList3";
            this.pbList3.Size = new System.Drawing.Size(19, 19);
            this.pbList3.TabIndex = 223;
            this.pbList3.TabStop = false;
            this.pbList3.Click += new System.EventHandler(this.pbList3_Click);
            // 
            // pbList2
            // 
            this.pbList2.Image = ((System.Drawing.Image)(resources.GetObject("pbList2.Image")));
            this.pbList2.Location = new System.Drawing.Point(272, 62);
            this.pbList2.Name = "pbList2";
            this.pbList2.Size = new System.Drawing.Size(19, 19);
            this.pbList2.TabIndex = 222;
            this.pbList2.TabStop = false;
            this.pbList2.Click += new System.EventHandler(this.pbList2_Click);
            // 
            // pbList1
            // 
            this.pbList1.Image = ((System.Drawing.Image)(resources.GetObject("pbList1.Image")));
            this.pbList1.Location = new System.Drawing.Point(272, 37);
            this.pbList1.Name = "pbList1";
            this.pbList1.Size = new System.Drawing.Size(19, 19);
            this.pbList1.TabIndex = 221;
            this.pbList1.TabStop = false;
            this.pbList1.Click += new System.EventHandler(this.pbList1_Click);
            // 
            // txtList3
            // 
            this.txtList3.Location = new System.Drawing.Point(86, 90);
            this.txtList3.Margin = new System.Windows.Forms.Padding(2);
            this.txtList3.Name = "txtList3";
            this.txtList3.Size = new System.Drawing.Size(182, 20);
            this.txtList3.TabIndex = 220;
            // 
            // txtList2
            // 
            this.txtList2.Location = new System.Drawing.Point(86, 63);
            this.txtList2.Margin = new System.Windows.Forms.Padding(2);
            this.txtList2.Name = "txtList2";
            this.txtList2.Size = new System.Drawing.Size(182, 20);
            this.txtList2.TabIndex = 219;
            // 
            // txtList1
            // 
            this.txtList1.Location = new System.Drawing.Point(86, 36);
            this.txtList1.Margin = new System.Windows.Forms.Padding(2);
            this.txtList1.Name = "txtList1";
            this.txtList1.Size = new System.Drawing.Size(182, 20);
            this.txtList1.TabIndex = 218;
            // 
            // pbSupplier
            // 
            this.pbSupplier.Image = ((System.Drawing.Image)(resources.GetObject("pbSupplier.Image")));
            this.pbSupplier.Location = new System.Drawing.Point(272, 115);
            this.pbSupplier.Name = "pbSupplier";
            this.pbSupplier.Size = new System.Drawing.Size(19, 19);
            this.pbSupplier.TabIndex = 217;
            this.pbSupplier.TabStop = false;
            this.pbSupplier.Click += new System.EventHandler(this.pbSupplier_Click);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(86, 114);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(181, 20);
            this.txtSupplier.TabIndex = 216;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 114);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 215;
            this.label5.Text = "Supplier :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 93);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 214;
            this.label12.Text = "Custom List 3 :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 66);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 213;
            this.label11.Text = "Custom List 2 :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 212;
            this.label10.Text = "Custom List 1 :";
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplay.Image")));
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(87, 359);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(93, 40);
            this.btnDisplay.TabIndex = 229;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dgReport.ColumnHeadersHeight = 50;
            this.dgReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.ItemNum,
            this.QtyPerSellingUnit,
            this.Level0,
            this.Level1,
            this.Level2,
            this.Level3,
            this.Level4,
            this.Level5,
            this.Level6,
            this.Level7,
            this.Level8,
            this.Level9,
            this.Level10,
            this.Level11,
            this.Level12});
            this.dgReport.Location = new System.Drawing.Point(297, 11);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.Size = new System.Drawing.Size(884, 495);
            this.dgReport.TabIndex = 228;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // ItemNum
            // 
            this.ItemNum.HeaderText = "Item Number";
            this.ItemNum.Name = "ItemNum";
            this.ItemNum.ReadOnly = true;
            // 
            // QtyPerSellingUnit
            // 
            this.QtyPerSellingUnit.HeaderText = "Qty Per Selling Unit";
            this.QtyPerSellingUnit.Name = "QtyPerSellingUnit";
            this.QtyPerSellingUnit.ReadOnly = true;
            // 
            // Level0
            // 
            this.Level0.HeaderText = "Level 0";
            this.Level0.Name = "Level0";
            this.Level0.ReadOnly = true;
            // 
            // Level1
            // 
            this.Level1.HeaderText = "Level 1";
            this.Level1.Name = "Level1";
            this.Level1.ReadOnly = true;
            // 
            // Level2
            // 
            this.Level2.HeaderText = "Level 2";
            this.Level2.Name = "Level2";
            this.Level2.ReadOnly = true;
            // 
            // Level3
            // 
            this.Level3.HeaderText = "Level 3";
            this.Level3.Name = "Level3";
            this.Level3.ReadOnly = true;
            // 
            // Level4
            // 
            this.Level4.HeaderText = "Level 4";
            this.Level4.Name = "Level4";
            this.Level4.ReadOnly = true;
            // 
            // Level5
            // 
            this.Level5.HeaderText = "Level 5";
            this.Level5.Name = "Level5";
            this.Level5.ReadOnly = true;
            // 
            // Level6
            // 
            this.Level6.HeaderText = "Level 6";
            this.Level6.Name = "Level6";
            this.Level6.ReadOnly = true;
            // 
            // Level7
            // 
            this.Level7.HeaderText = "Level 7";
            this.Level7.Name = "Level7";
            this.Level7.ReadOnly = true;
            // 
            // Level8
            // 
            this.Level8.HeaderText = "Level 8";
            this.Level8.Name = "Level8";
            this.Level8.ReadOnly = true;
            // 
            // Level9
            // 
            this.Level9.HeaderText = "Level 9";
            this.Level9.Name = "Level9";
            this.Level9.ReadOnly = true;
            // 
            // Level10
            // 
            this.Level10.HeaderText = "Level 10";
            this.Level10.Name = "Level10";
            this.Level10.ReadOnly = true;
            // 
            // Level11
            // 
            this.Level11.HeaderText = "Level 11";
            this.Level11.Name = "Level11";
            this.Level11.ReadOnly = true;
            // 
            // Level12
            // 
            this.Level12.HeaderText = "Level 12";
            this.Level12.Name = "Level12";
            this.Level12.ReadOnly = true;
            // 
            // btnSortGrid
            // 
            this.btnSortGrid.Enabled = false;
            this.btnSortGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortGrid.Location = new System.Drawing.Point(208, 420);
            this.btnSortGrid.Name = "btnSortGrid";
            this.btnSortGrid.Size = new System.Drawing.Size(78, 22);
            this.btnSortGrid.TabIndex = 267;
            this.btnSortGrid.Text = "Sort Grid";
            this.btnSortGrid.UseVisualStyleBackColor = true;
            this.btnSortGrid.Click += new System.EventHandler(this.btnSortGrid_Click);
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Location = new System.Drawing.Point(106, 447);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 266;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.UseVisualStyleBackColor = true;
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Checked = true;
            this.rdoAsc.Location = new System.Drawing.Point(7, 447);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 265;
            this.rdoAsc.TabStop = true;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 405);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 264;
            this.label4.Text = "Sort Grid By :";
            // 
            // cmbSort
            // 
            this.cmbSort.Enabled = false;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(21, 421);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(181, 21);
            this.cmbSort.TabIndex = 263;
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(186, 470);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 36);
            this.btnPrintGrid.TabIndex = 268;
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
            this.btnExportExcel.Location = new System.Drawing.Point(80, 470);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 36);
            this.btnExportExcel.TabIndex = 269;
            this.btnExportExcel.Text = "       Export";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // RptPriceDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 518);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnPrintGrid);
            this.Controls.Add(this.btnSortGrid);
            this.Controls.Add(this.rdoDesc);
            this.Controls.Add(this.rdoAsc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.btnDisplay);
            this.Controls.Add(this.dgReport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeCategory);
            this.Controls.Add(this.pbList3);
            this.Controls.Add(this.pbList2);
            this.Controls.Add(this.pbList1);
            this.Controls.Add(this.txtList3);
            this.Controls.Add(this.txtList2);
            this.Controls.Add(this.txtList1);
            this.Controls.Add(this.pbSupplier);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPriceLevel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RptPriceDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Price List";
            this.Load += new System.EventHandler(this.RptPriceDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbList3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPriceLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.PictureBox pbList3;
        private System.Windows.Forms.PictureBox pbList2;
        private System.Windows.Forms.PictureBox pbList1;
        private System.Windows.Forms.TextBox txtList3;
        private System.Windows.Forms.TextBox txtList2;
        private System.Windows.Forms.TextBox txtList1;
        private System.Windows.Forms.PictureBox pbSupplier;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.DataGridView dgReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyPerSellingUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level12;
        private System.Windows.Forms.Button btnSortGrid;
        private System.Windows.Forms.RadioButton rdoDesc;
        private System.Windows.Forms.RadioButton rdoAsc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.Button btnExportExcel;
    }
}