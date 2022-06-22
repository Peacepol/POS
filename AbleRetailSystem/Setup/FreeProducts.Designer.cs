namespace RestaurantPOS.Setup
{
    partial class FreeProducts
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
            this.dgFreeProducts = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.TxtNote = new System.Windows.Forms.Label();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TaxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgFreeProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgFreeProducts
            // 
            this.dgFreeProducts.AllowUserToAddRows = false;
            this.dgFreeProducts.AllowUserToDeleteRows = false;
            this.dgFreeProducts.AllowUserToOrderColumns = true;
            this.dgFreeProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFreeProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.PartNum,
            this.ItemName,
            this.Quantity,
            this.Price,
            this.TotalPrice,
            this.Selected,
            this.TaxCode,
            this.CostPrice});
            this.dgFreeProducts.Location = new System.Drawing.Point(13, 28);
            this.dgFreeProducts.Name = "dgFreeProducts";
            this.dgFreeProducts.RowHeadersVisible = false;
            this.dgFreeProducts.Size = new System.Drawing.Size(517, 200);
            this.dgFreeProducts.TabIndex = 0;
            this.dgFreeProducts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFreeProducts_CellContentClick);
            this.dgFreeProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFreeProducts_CellDoubleClick);
            this.dgFreeProducts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFreeProducts_CellEndEdit);
            this.dgFreeProducts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgFreeProducts_CellFormatting);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(418, 234);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 33);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(300, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 33);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(182, 234);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(112, 33);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // TxtNote
            // 
            this.TxtNote.AutoSize = true;
            this.TxtNote.Location = new System.Drawing.Point(72, 9);
            this.TxtNote.Name = "TxtNote";
            this.TxtNote.Size = new System.Drawing.Size(0, 13);
            this.TxtNote.TabIndex = 4;
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Visible = false;
            // 
            // PartNum
            // 
            this.PartNum.HeaderText = "Part Number";
            this.PartNum.Name = "PartNum";
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // TotalPrice
            // 
            this.TotalPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TotalPrice.HeaderText = "TotalPrice";
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.ReadOnly = true;
            // 
            // Selected
            // 
            this.Selected.HeaderText = "Selected";
            this.Selected.Name = "Selected";
            this.Selected.Visible = false;
            this.Selected.Width = 60;
            // 
            // TaxCode
            // 
            this.TaxCode.HeaderText = "TaxCode";
            this.TaxCode.Name = "TaxCode";
            this.TaxCode.ReadOnly = true;
            this.TaxCode.Visible = false;
            // 
            // CostPrice
            // 
            this.CostPrice.HeaderText = "CostPrice";
            this.CostPrice.Name = "CostPrice";
            this.CostPrice.Visible = false;
            // 
            // FreeProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 279);
            this.Controls.Add(this.TxtNote);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgFreeProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FreeProducts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Free Products";
            this.Load += new System.EventHandler(this.FreeProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFreeProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgFreeProducts;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label TxtNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostPrice;
    }
}