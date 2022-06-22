namespace RestaurantPOS.Reports.InventoryReports
{
    partial class AnalyseInventoryDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyseInventoryDetail));
            this.txtList3 = new System.Windows.Forms.TextBox();
            this.txtList2 = new System.Windows.Forms.TextBox();
            this.txtList1 = new System.Windows.Forms.TextBox();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIncludeItems = new System.Windows.Forms.ComboBox();
            this.pbList3 = new System.Windows.Forms.PictureBox();
            this.pbList2 = new System.Windows.Forms.PictureBox();
            this.pbList1 = new System.Windows.Forms.PictureBox();
            this.pbSupplier = new System.Windows.Forms.PictureBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.dgReport = new System.Windows.Forms.DataGridView();
            this.IDNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOrdered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatePromised = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Committed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Available = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDisplay = new System.Windows.Forms.Button();
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
            // txtList3
            // 
            this.txtList3.Location = new System.Drawing.Point(88, 90);
            this.txtList3.Margin = new System.Windows.Forms.Padding(2);
            this.txtList3.Name = "txtList3";
            this.txtList3.Size = new System.Drawing.Size(182, 20);
            this.txtList3.TabIndex = 198;
            this.txtList3.TextChanged += new System.EventHandler(this.txtList3_TextChanged);
            // 
            // txtList2
            // 
            this.txtList2.Location = new System.Drawing.Point(88, 63);
            this.txtList2.Margin = new System.Windows.Forms.Padding(2);
            this.txtList2.Name = "txtList2";
            this.txtList2.Size = new System.Drawing.Size(182, 20);
            this.txtList2.TabIndex = 197;
            this.txtList2.TextChanged += new System.EventHandler(this.txtList2_TextChanged);
            // 
            // txtList1
            // 
            this.txtList1.Location = new System.Drawing.Point(88, 36);
            this.txtList1.Margin = new System.Windows.Forms.Padding(2);
            this.txtList1.Name = "txtList1";
            this.txtList1.Size = new System.Drawing.Size(182, 20);
            this.txtList1.TabIndex = 196;
            this.txtList1.TextChanged += new System.EventHandler(this.txtList1_TextChanged);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(88, 114);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(181, 20);
            this.txtSupplier.TabIndex = 194;
            this.txtSupplier.TextChanged += new System.EventHandler(this.txtSupplier_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 114);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 193;
            this.label5.Text = "Supplier :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 93);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 192;
            this.label12.Text = "Custom List 3 :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 66);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 191;
            this.label11.Text = "Custom List 2 :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 190;
            this.label10.Text = "Custom List 1 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 188;
            this.label1.Text = "Include Items:";
            // 
            // cmbIncludeItems
            // 
            this.cmbIncludeItems.FormattingEnabled = true;
            this.cmbIncludeItems.Items.AddRange(new object[] {
            "Only Bought",
            "Only Sold",
            "Only Inventoried",
            "All"});
            this.cmbIncludeItems.Location = new System.Drawing.Point(88, 11);
            this.cmbIncludeItems.Margin = new System.Windows.Forms.Padding(2);
            this.cmbIncludeItems.Name = "cmbIncludeItems";
            this.cmbIncludeItems.Size = new System.Drawing.Size(181, 21);
            this.cmbIncludeItems.TabIndex = 187;
            // 
            // pbList3
            // 
            this.pbList3.Image = ((System.Drawing.Image)(resources.GetObject("pbList3.Image")));
            this.pbList3.Location = new System.Drawing.Point(274, 90);
            this.pbList3.Name = "pbList3";
            this.pbList3.Size = new System.Drawing.Size(19, 19);
            this.pbList3.TabIndex = 201;
            this.pbList3.TabStop = false;
            this.pbList3.Click += new System.EventHandler(this.pbList3_Click);
            // 
            // pbList2
            // 
            this.pbList2.Image = ((System.Drawing.Image)(resources.GetObject("pbList2.Image")));
            this.pbList2.Location = new System.Drawing.Point(274, 62);
            this.pbList2.Name = "pbList2";
            this.pbList2.Size = new System.Drawing.Size(19, 19);
            this.pbList2.TabIndex = 200;
            this.pbList2.TabStop = false;
            this.pbList2.Click += new System.EventHandler(this.pbList2_Click);
            // 
            // pbList1
            // 
            this.pbList1.Image = ((System.Drawing.Image)(resources.GetObject("pbList1.Image")));
            this.pbList1.Location = new System.Drawing.Point(274, 37);
            this.pbList1.Name = "pbList1";
            this.pbList1.Size = new System.Drawing.Size(19, 19);
            this.pbList1.TabIndex = 199;
            this.pbList1.TabStop = false;
            this.pbList1.Click += new System.EventHandler(this.pbList1_Click);
            // 
            // pbSupplier
            // 
            this.pbSupplier.Image = ((System.Drawing.Image)(resources.GetObject("pbSupplier.Image")));
            this.pbSupplier.Location = new System.Drawing.Point(274, 115);
            this.pbSupplier.Name = "pbSupplier";
            this.pbSupplier.Size = new System.Drawing.Size(19, 19);
            this.pbSupplier.TabIndex = 195;
            this.pbSupplier.TabStop = false;
            this.pbSupplier.Click += new System.EventHandler(this.pbSupplier_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(195, 360);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 40);
            this.btnGenerate.TabIndex = 189;
            this.btnGenerate.Text = "Print";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 209;
            this.label3.Text = "Category :";
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCategory.Location = new System.Drawing.Point(88, 139);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(182, 215);
            this.treeCategory.TabIndex = 208;
            this.treeCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterCheck);
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
            this.IDNum,
            this.ItemName,
            this.Supplier,
            this.DateOrdered,
            this.DatePromised,
            this.Committed,
            this.OnOrder,
            this.Available});
            this.dgReport.Location = new System.Drawing.Point(341, 5);
            this.dgReport.Name = "dgReport";
            this.dgReport.ReadOnly = true;
            this.dgReport.RowHeadersVisible = false;
            this.dgReport.Size = new System.Drawing.Size(782, 500);
            this.dgReport.TabIndex = 213;
            this.dgReport.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgReport_CellFormatting);
            // 
            // IDNum
            // 
            this.IDNum.HeaderText = "ID Number";
            this.IDNum.Name = "IDNum";
            this.IDNum.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // Supplier
            // 
            this.Supplier.HeaderText = "Supplier";
            this.Supplier.Name = "Supplier";
            this.Supplier.ReadOnly = true;
            // 
            // DateOrdered
            // 
            this.DateOrdered.HeaderText = "Date Ordered";
            this.DateOrdered.Name = "DateOrdered";
            this.DateOrdered.ReadOnly = true;
            // 
            // DatePromised
            // 
            this.DatePromised.HeaderText = "Date Promised";
            this.DatePromised.Name = "DatePromised";
            this.DatePromised.ReadOnly = true;
            // 
            // Committed
            // 
            this.Committed.HeaderText = "Committed";
            this.Committed.Name = "Committed";
            this.Committed.ReadOnly = true;
            // 
            // OnOrder
            // 
            this.OnOrder.HeaderText = "OnOrder";
            this.OnOrder.Name = "OnOrder";
            this.OnOrder.ReadOnly = true;
            // 
            // Available
            // 
            this.Available.HeaderText = "Available";
            this.Available.Name = "Available";
            this.Available.ReadOnly = true;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplay.Image")));
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(88, 360);
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
            this.btnSortGrid.Location = new System.Drawing.Point(223, 423);
            this.btnSortGrid.Name = "btnSortGrid";
            this.btnSortGrid.Size = new System.Drawing.Size(78, 22);
            this.btnSortGrid.TabIndex = 262;
            this.btnSortGrid.Text = "Sort Grid";
            this.btnSortGrid.UseVisualStyleBackColor = true;
            this.btnSortGrid.Click += new System.EventHandler(this.btnSortGrid_Click);
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Location = new System.Drawing.Point(135, 451);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 261;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.UseVisualStyleBackColor = true;
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Checked = true;
            this.rdoAsc.Location = new System.Drawing.Point(36, 451);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 260;
            this.rdoAsc.TabStop = true;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 408);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 259;
            this.label4.Text = "Sort Grid By :";
            // 
            // cmbSort
            // 
            this.cmbSort.Enabled = false;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(36, 424);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(181, 21);
            this.cmbSort.TabIndex = 258;
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintGrid.Image = global::RestaurantPOS.Properties.Resources.print24;
            this.btnPrintGrid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintGrid.Location = new System.Drawing.Point(195, 474);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(100, 31);
            this.btnPrintGrid.TabIndex = 263;
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
            this.btnExportExcel.Location = new System.Drawing.Point(89, 475);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExportExcel.TabIndex = 264;
            this.btnExportExcel.Text = "       Export";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click_1);
            // 
            // AnalyseInventoryDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 517);
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
            this.Controls.Add(this.cmbIncludeItems);
            this.Name = "AnalyseInventoryDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Analyse Inventory Detail";
            this.Load += new System.EventHandler(this.AnalyseInventoryDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbList3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIncludeItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.DataGridView dgReport;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnSortGrid;
        private System.Windows.Forms.RadioButton rdoDesc;
        private System.Windows.Forms.RadioButton rdoAsc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Button btnPrintGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOrdered;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatePromised;
        private System.Windows.Forms.DataGridViewTextBoxColumn Committed;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Available;
        private System.Windows.Forms.Button btnExportExcel;
    }
}