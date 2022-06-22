namespace AbleRetailPOS.Inventory
{
    partial class Stocktake
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgridItems = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnHandQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewCountQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscrepancyQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarianceValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssetAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRecord = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadAll = new System.Windows.Forms.Button();
            this.ExpenseAccountNumber = new System.Windows.Forms.TextBox();
            this.chkInitial = new System.Windows.Forms.CheckBox();
            this.btnSpecificItems = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridItems
            // 
            this.dgridItems.AllowUserToAddRows = false;
            this.dgridItems.AllowUserToDeleteRows = false;
            this.dgridItems.AllowUserToOrderColumns = true;
            this.dgridItems.AllowUserToResizeColumns = false;
            this.dgridItems.AllowUserToResizeRows = false;
            this.dgridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.PartNumber,
            this.ItemName,
            this.OnHandQty,
            this.NewCountQty,
            this.DiscrepancyQty,
            this.Cost,
            this.StockValue,
            this.VarianceValue,
            this.AssetAccountID,
            this.AccountNumber});
            this.dgridItems.Location = new System.Drawing.Point(13, 67);
            this.dgridItems.Name = "dgridItems";
            this.dgridItems.RowHeadersVisible = false;
            this.dgridItems.Size = new System.Drawing.Size(959, 436);
            this.dgridItems.TabIndex = 0;
            this.dgridItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellContentClick);
            this.dgridItems.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellDoubleClick);
            this.dgridItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellEndEdit);
            this.dgridItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgridItems_CellFormatting);
            this.dgridItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgridItems_EditingControlShowing);
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "Item ID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            // 
            // PartNumber
            // 
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.Name = "PartNumber";
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // OnHandQty
            // 
            this.OnHandQty.HeaderText = "On Hand Qty";
            this.OnHandQty.Name = "OnHandQty";
            this.OnHandQty.ReadOnly = true;
            // 
            // NewCountQty
            // 
            this.NewCountQty.HeaderText = "Counted Qty";
            this.NewCountQty.Name = "NewCountQty";
            // 
            // DiscrepancyQty
            // 
            this.DiscrepancyQty.HeaderText = "Discrepancy Qty";
            this.DiscrepancyQty.Name = "DiscrepancyQty";
            this.DiscrepancyQty.ReadOnly = true;
            // 
            // Cost
            // 
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.Cost.DefaultCellStyle = dataGridViewCellStyle1;
            this.Cost.HeaderText = "Unit Cost";
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            // 
            // StockValue
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.StockValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.StockValue.HeaderText = "Total Stock Value";
            this.StockValue.Name = "StockValue";
            this.StockValue.ReadOnly = true;
            // 
            // VarianceValue
            // 
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.VarianceValue.DefaultCellStyle = dataGridViewCellStyle3;
            this.VarianceValue.HeaderText = "Total Variance";
            this.VarianceValue.Name = "VarianceValue";
            this.VarianceValue.ReadOnly = true;
            // 
            // AssetAccountID
            // 
            this.AssetAccountID.HeaderText = "AccountID";
            this.AssetAccountID.Name = "AssetAccountID";
            // 
            // AccountNumber
            // 
            this.AccountNumber.HeaderText = "AccountNumber";
            this.AccountNumber.Name = "AccountNumber";
            // 
            // btnRecord
            // 
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecord.Location = new System.Drawing.Point(812, 515);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(160, 34);
            this.btnRecord.TabIndex = 223;
            this.btnRecord.Text = "Process New Count";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 225;
            this.label2.Text = "Memo:";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(66, 35);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(270, 20);
            this.txtMemo.TabIndex = 226;
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(13, 515);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(160, 34);
            this.btnReport.TabIndex = 227;
            this.btnReport.Text = "Print Variance Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 228;
            this.label1.Text = "Adjustment Expense Account GL code:";
            // 
            // btnLoadAll
            // 
            this.btnLoadAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLoadAll.ForeColor = System.Drawing.Color.Black;
            this.btnLoadAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadAll.Location = new System.Drawing.Point(855, 27);
            this.btnLoadAll.Name = "btnLoadAll";
            this.btnLoadAll.Size = new System.Drawing.Size(108, 34);
            this.btnLoadAll.TabIndex = 232;
            this.btnLoadAll.Text = "Load All Items";
            this.btnLoadAll.UseVisualStyleBackColor = true;
            this.btnLoadAll.Click += new System.EventHandler(this.btnLoadAll_Click);
            // 
            // ExpenseAccountNumber
            // 
            this.ExpenseAccountNumber.BackColor = System.Drawing.Color.White;
            this.ExpenseAccountNumber.ForeColor = System.Drawing.Color.Black;
            this.ExpenseAccountNumber.Location = new System.Drawing.Point(211, 6);
            this.ExpenseAccountNumber.MaxLength = 100;
            this.ExpenseAccountNumber.Name = "ExpenseAccountNumber";
            this.ExpenseAccountNumber.Size = new System.Drawing.Size(125, 20);
            this.ExpenseAccountNumber.TabIndex = 233;
            this.ExpenseAccountNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ExpenseAccountNumber_KeyPress);
            // 
            // chkInitial
            // 
            this.chkInitial.AutoSize = true;
            this.chkInitial.Location = new System.Drawing.Point(604, 6);
            this.chkInitial.Name = "chkInitial";
            this.chkInitial.Size = new System.Drawing.Size(124, 17);
            this.chkInitial.TabIndex = 234;
            this.chkInitial.Text = "Initial Inventory Entry";
            this.chkInitial.UseVisualStyleBackColor = true;
            // 
            // btnSpecificItems
            // 
            this.btnSpecificItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSpecificItems.ForeColor = System.Drawing.Color.Black;
            this.btnSpecificItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpecificItems.Location = new System.Drawing.Point(709, 27);
            this.btnSpecificItems.Name = "btnSpecificItems";
            this.btnSpecificItems.Size = new System.Drawing.Size(130, 34);
            this.btnSpecificItems.TabIndex = 235;
            this.btnSpecificItems.Text = "Load Specific Items";
            this.btnSpecificItems.UseVisualStyleBackColor = true;
            this.btnSpecificItems.Click += new System.EventHandler(this.btnSpecificItems_Click);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnImport.ForeColor = System.Drawing.Color.Black;
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(584, 27);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(108, 34);
            this.btnImport.TabIndex = 236;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // Stocktake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSpecificItems);
            this.Controls.Add(this.chkInitial);
            this.Controls.Add(this.ExpenseAccountNumber);
            this.Controls.Add(this.btnLoadAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.dgridItems);
            this.MaximizeBox = false;
            this.Name = "Stocktake";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stocktake";
            this.Load += new System.EventHandler(this.Stocktake_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridItems;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadAll;
        private System.Windows.Forms.TextBox ExpenseAccountNumber;
        private System.Windows.Forms.CheckBox chkInitial;
        private System.Windows.Forms.Button btnSpecificItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnHandQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewCountQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscrepancyQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarianceValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssetAccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountNumber;
        private System.Windows.Forms.Button btnImport;
    }
}