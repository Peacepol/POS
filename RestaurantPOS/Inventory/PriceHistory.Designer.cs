namespace AbleRetailPOS.Inventory
{
    partial class PriceHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceHistory));
            this.dgPriceChanges = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceBefore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PercentChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eDate = new System.Windows.Forms.DateTimePicker();
            this.sDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgPriceChanges)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPriceChanges
            // 
            this.dgPriceChanges.AllowUserToAddRows = false;
            this.dgPriceChanges.AllowUserToDeleteRows = false;
            this.dgPriceChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgPriceChanges.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgPriceChanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPriceChanges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.Selected,
            this.ItemName,
            this.PriceBefore,
            this.PriceAfter,
            this.PriceLevel,
            this.CalcMethod,
            this.PercentChange,
            this.ChangedBy,
            this.DateChange});
            this.dgPriceChanges.Location = new System.Drawing.Point(12, 66);
            this.dgPriceChanges.Name = "dgPriceChanges";
            this.dgPriceChanges.RowHeadersVisible = false;
            this.dgPriceChanges.Size = new System.Drawing.Size(867, 358);
            this.dgPriceChanges.TabIndex = 142;
            this.dgPriceChanges.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPriceChanges_CellFormatting);
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.Visible = false;
            // 
            // Selected
            // 
            this.Selected.HeaderText = "";
            this.Selected.Name = "Selected";
            this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Selected.Width = 30;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            // 
            // PriceBefore
            // 
            this.PriceBefore.HeaderText = "Before";
            this.PriceBefore.Name = "PriceBefore";
            // 
            // PriceAfter
            // 
            this.PriceAfter.HeaderText = "Price After";
            this.PriceAfter.Name = "PriceAfter";
            // 
            // PriceLevel
            // 
            this.PriceLevel.HeaderText = "Price Level";
            this.PriceLevel.Name = "PriceLevel";
            // 
            // CalcMethod
            // 
            this.CalcMethod.HeaderText = "Calculation Method";
            this.CalcMethod.Name = "CalcMethod";
            // 
            // PercentChange
            // 
            this.PercentChange.HeaderText = "Percent Changed";
            this.PercentChange.Name = "PercentChange";
            // 
            // ChangedBy
            // 
            this.ChangedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ChangedBy.HeaderText = "Changed By";
            this.ChangedBy.Name = "ChangedBy";
            // 
            // DateChange
            // 
            this.DateChange.HeaderText = "Date Changed";
            this.DateChange.Name = "DateChange";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 146;
            this.label3.Text = "to";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 145;
            this.label2.Text = "Date Range From";
            // 
            // eDate
            // 
            this.eDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.eDate.Location = new System.Drawing.Point(244, 12);
            this.eDate.Name = "eDate";
            this.eDate.Size = new System.Drawing.Size(98, 20);
            this.eDate.TabIndex = 144;
            // 
            // sDate
            // 
            this.sDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sDate.Location = new System.Drawing.Point(108, 12);
            this.sDate.Name = "sDate";
            this.sDate.Size = new System.Drawing.Size(104, 20);
            this.sDate.TabIndex = 143;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerate.Image")));
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(364, 12);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(93, 40);
            this.btnGenerate.TabIndex = 147;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::AbleRetailPOS.Properties.Resources.Add24;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(818, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 40);
            this.button1.TabIndex = 148;
            this.button1.Text = "OK";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PriceHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 436);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.eDate);
            this.Controls.Add(this.sDate);
            this.Controls.Add(this.dgPriceChanges);
            this.Name = "PriceHistory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price History";
            this.Load += new System.EventHandler(this.PriceHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPriceChanges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPriceChanges;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker eDate;
        private System.Windows.Forms.DateTimePicker sDate;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceBefore;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceAfter;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn PercentChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateChange;
    }
}