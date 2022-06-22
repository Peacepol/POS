namespace RestaurantPOS.Inventory
{
    partial class PriceTags
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
            this.btnGenPriceTag = new System.Windows.Forms.Button();
            this.gbGenPrice = new System.Windows.Forms.GroupBox();
            this.rbReg = new System.Windows.Forms.RadioButton();
            this.rbSale = new System.Windows.Forms.RadioButton();
            this.cbPriceLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgItemList = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceLevel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnSelectItems = new System.Windows.Forms.Button();
            this.btnRemoveRow = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbGenPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenPriceTag
            // 
            this.btnGenPriceTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenPriceTag.Location = new System.Drawing.Point(232, 22);
            this.btnGenPriceTag.Name = "btnGenPriceTag";
            this.btnGenPriceTag.Size = new System.Drawing.Size(92, 72);
            this.btnGenPriceTag.TabIndex = 144;
            this.btnGenPriceTag.Text = "Generate Price Tag";
            this.btnGenPriceTag.UseVisualStyleBackColor = true;
            this.btnGenPriceTag.Click += new System.EventHandler(this.btnGenPriceTag_Click);
            // 
            // gbGenPrice
            // 
            this.gbGenPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGenPrice.Controls.Add(this.rbReg);
            this.gbGenPrice.Controls.Add(this.rbSale);
            this.gbGenPrice.Controls.Add(this.cbPriceLevel);
            this.gbGenPrice.Controls.Add(this.label2);
            this.gbGenPrice.Controls.Add(this.btnGenPriceTag);
            this.gbGenPrice.Enabled = false;
            this.gbGenPrice.Location = new System.Drawing.Point(360, 7);
            this.gbGenPrice.Name = "gbGenPrice";
            this.gbGenPrice.Size = new System.Drawing.Size(352, 100);
            this.gbGenPrice.TabIndex = 145;
            this.gbGenPrice.TabStop = false;
            this.gbGenPrice.Text = "Price Tag";
            // 
            // rbReg
            // 
            this.rbReg.AutoSize = true;
            this.rbReg.Checked = true;
            this.rbReg.Location = new System.Drawing.Point(150, 22);
            this.rbReg.Name = "rbReg";
            this.rbReg.Size = new System.Drawing.Size(62, 17);
            this.rbReg.TabIndex = 148;
            this.rbReg.TabStop = true;
            this.rbReg.Text = "Regular";
            this.rbReg.UseVisualStyleBackColor = true;
            // 
            // rbSale
            // 
            this.rbSale.AutoSize = true;
            this.rbSale.Location = new System.Drawing.Point(91, 22);
            this.rbSale.Name = "rbSale";
            this.rbSale.Size = new System.Drawing.Size(46, 17);
            this.rbSale.TabIndex = 147;
            this.rbSale.Text = "Sale";
            this.rbSale.UseVisualStyleBackColor = true;
            this.rbSale.CheckedChanged += new System.EventHandler(this.rbSale_CheckedChanged);
            // 
            // cbPriceLevel
            // 
            this.cbPriceLevel.FormattingEnabled = true;
            this.cbPriceLevel.Location = new System.Drawing.Point(91, 49);
            this.cbPriceLevel.Name = "cbPriceLevel";
            this.cbPriceLevel.Size = new System.Drawing.Size(121, 21);
            this.cbPriceLevel.TabIndex = 146;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 145;
            this.label2.Text = "Price Level :";
            // 
            // dgItemList
            // 
            this.dgItemList.AllowUserToAddRows = false;
            this.dgItemList.AllowUserToDeleteRows = false;
            this.dgItemList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Selected,
            this.ItemName,
            this.Quantity,
            this.PriceLevel});
            this.dgItemList.Location = new System.Drawing.Point(15, 7);
            this.dgItemList.Name = "dgItemList";
            this.dgItemList.RowHeadersVisible = false;
            this.dgItemList.Size = new System.Drawing.Size(334, 409);
            this.dgItemList.TabIndex = 149;
            this.dgItemList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgItemList_EditingControlShowing);
            // 
            // ID
            // 
            this.ID.HeaderText = "ItemID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Selected
            // 
            this.Selected.HeaderText = "Selected";
            this.Selected.Name = "Selected";
            this.Selected.Width = 60;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Quantity.Width = 60;
            // 
            // PriceLevel
            // 
            this.PriceLevel.HeaderText = "Price Level";
            this.PriceLevel.Name = "PriceLevel";
            this.PriceLevel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PriceLevel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PriceLevel.Visible = false;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.Location = new System.Drawing.Point(360, 113);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(92, 72);
            this.btnHistory.TabIndex = 149;
            this.btnHistory.Text = "Price History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnSelectItems
            // 
            this.btnSelectItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectItems.Location = new System.Drawing.Point(360, 191);
            this.btnSelectItems.Name = "btnSelectItems";
            this.btnSelectItems.Size = new System.Drawing.Size(92, 72);
            this.btnSelectItems.TabIndex = 150;
            this.btnSelectItems.Text = "Select Items";
            this.btnSelectItems.UseVisualStyleBackColor = true;
            this.btnSelectItems.Click += new System.EventHandler(this.btnSelectItems_Click);
            // 
            // btnRemoveRow
            // 
            this.btnRemoveRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRow.Location = new System.Drawing.Point(360, 269);
            this.btnRemoveRow.Name = "btnRemoveRow";
            this.btnRemoveRow.Size = new System.Drawing.Size(92, 72);
            this.btnRemoveRow.TabIndex = 151;
            this.btnRemoveRow.Text = "Remove Row";
            this.btnRemoveRow.UseVisualStyleBackColor = true;
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAll.Location = new System.Drawing.Point(360, 347);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(92, 72);
            this.btnRemoveAll.TabIndex = 152;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Items.AddRange(new object[] {
            "A4(210x297mm)",
            "A5(148x210mm)",
            "A7(74x105mm)"});
            this.cmbPaperSize.Location = new System.Drawing.Point(591, 113);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(121, 21);
            this.cmbPaperSize.TabIndex = 153;
            this.cmbPaperSize.Text = "A4(210x297mm)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(521, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 154;
            this.label1.Text = "Paper Size :";
            // 
            // PriceTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 428);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPaperSize);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveRow);
            this.Controls.Add(this.btnSelectItems);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.dgItemList);
            this.Controls.Add(this.gbGenPrice);
            this.Name = "PriceTags";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price Tags";
            this.Load += new System.EventHandler(this.PriceTags_Load);
            this.gbGenPrice.ResumeLayout(false);
            this.gbGenPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGenPriceTag;
        private System.Windows.Forms.GroupBox gbGenPrice;
        private System.Windows.Forms.ComboBox cbPriceLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbReg;
        private System.Windows.Forms.RadioButton rbSale;
        private System.Windows.Forms.DataGridView dgItemList;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnSelectItems;
        private System.Windows.Forms.Button btnRemoveRow;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewComboBoxColumn PriceLevel;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label label1;
    }
}