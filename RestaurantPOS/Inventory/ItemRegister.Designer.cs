namespace AbleRetailPOS.Inventory
{
    partial class ItemRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemRegister));
            this.txtItem = new System.Windows.Forms.TextBox();
            this.edateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.edate = new System.Windows.Forms.Label();
            this.sdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.lbl = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.pbItem = new System.Windows.Forms.PictureBox();
            this.cmb_searby = new System.Windows.Forms.ComboBox();
            this.dgridItem = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblItemID = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItem)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(227, 26);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(364, 20);
            this.txtItem.TabIndex = 208;
            this.txtItem.Visible = false;
            this.txtItem.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // edateTimePicker
            // 
            this.edateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edateTimePicker.Location = new System.Drawing.Point(227, 53);
            this.edateTimePicker.Name = "edateTimePicker";
            this.edateTimePicker.Size = new System.Drawing.Size(106, 20);
            this.edateTimePicker.TabIndex = 207;
            this.edateTimePicker.ValueChanged += new System.EventHandler(this.edateTimePicker_ValueChanged);
            // 
            // edate
            // 
            this.edate.AutoSize = true;
            this.edate.Location = new System.Drawing.Point(202, 53);
            this.edate.Name = "edate";
            this.edate.Size = new System.Drawing.Size(19, 13);
            this.edate.TabIndex = 206;
            this.edate.Text = "to:";
            // 
            // sdateTimePicker
            // 
            this.sdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdateTimePicker.Location = new System.Drawing.Point(76, 53);
            this.sdateTimePicker.Name = "sdateTimePicker";
            this.sdateTimePicker.Size = new System.Drawing.Size(98, 20);
            this.sdateTimePicker.TabIndex = 205;
            this.sdateTimePicker.ValueChanged += new System.EventHandler(this.sdateTimePicker_ValueChanged);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(16, 53);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(56, 13);
            this.lbl.TabIndex = 204;
            this.lbl.Text = "Date from:";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(16, 29);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(58, 13);
            this.lblCustomer.TabIndex = 203;
            this.lblCustomer.Text = "Search by:";
            // 
            // pbItem
            // 
            this.pbItem.BackColor = System.Drawing.SystemColors.Control;
            this.pbItem.Image = ((System.Drawing.Image)(resources.GetObject("pbItem.Image")));
            this.pbItem.Location = new System.Drawing.Point(597, 26);
            this.pbItem.Name = "pbItem";
            this.pbItem.Size = new System.Drawing.Size(19, 19);
            this.pbItem.TabIndex = 209;
            this.pbItem.TabStop = false;
            this.pbItem.Visible = false;
            this.pbItem.Click += new System.EventHandler(this.pbItem_Click);
            // 
            // cmb_searby
            // 
            this.cmb_searby.FormattingEnabled = true;
            this.cmb_searby.Items.AddRange(new object[] {
            "All Items",
            "Item"});
            this.cmb_searby.Location = new System.Drawing.Point(76, 26);
            this.cmb_searby.Name = "cmb_searby";
            this.cmb_searby.Size = new System.Drawing.Size(121, 21);
            this.cmb_searby.TabIndex = 202;
            this.cmb_searby.Text = "All Items";
            this.cmb_searby.SelectedIndexChanged += new System.EventHandler(this.cmb_searby_SelectedIndexChanged);
            // 
            // dgridItem
            // 
            this.dgridItem.AllowUserToAddRows = false;
            this.dgridItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgridItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridItem.BackgroundColor = System.Drawing.Color.White;
            this.dgridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridItem.Location = new System.Drawing.Point(9, 111);
            this.dgridItem.Name = "dgridItem";
            this.dgridItem.RowHeadersVisible = false;
            this.dgridItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridItem.Size = new System.Drawing.Size(785, 473);
            this.dgridItem.TabIndex = 210;
            this.dgridItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItem_CellDoubleClick);
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Image = global::AbleRetailPOS.Properties.Resources.refresh24X24;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(626, 45);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(146, 34);
            this.btnLoad.TabIndex = 211;
            this.btnLoad.Text = "Load Transactions";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblItemID
            // 
            this.lblItemID.AutoSize = true;
            this.lblItemID.Location = new System.Drawing.Point(354, 59);
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.Size = new System.Drawing.Size(0, 13);
            this.lblItemID.TabIndex = 212;
            this.lblItemID.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::AbleRetailPOS.Properties.Resources.print24;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(673, 595);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(122, 33);
            this.btnPrint.TabIndex = 219;
            this.btnPrint.Text = "        Print Report";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.cmb_searby);
            this.groupBox1.Controls.Add(this.lblItemID);
            this.groupBox1.Controls.Add(this.pbItem);
            this.groupBox1.Controls.Add(this.lblCustomer);
            this.groupBox1.Controls.Add(this.txtItem);
            this.groupBox1.Controls.Add(this.lbl);
            this.groupBox1.Controls.Add(this.edateTimePicker);
            this.groupBox1.Controls.Add(this.sdateTimePicker);
            this.groupBox1.Controls.Add(this.edate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(778, 93);
            this.groupBox1.TabIndex = 220;
            this.groupBox1.TabStop = false;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(12, 595);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(122, 33);
            this.btnExportExcel.TabIndex = 265;
            this.btnExportExcel.Text = "       Export";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // ItemRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 640);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgridItem);
            this.MaximizeBox = false;
            this.Name = "ItemRegister";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Register";
            this.Load += new System.EventHandler(this.ItemRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItem)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.DateTimePicker edateTimePicker;
        private System.Windows.Forms.Label edate;
        private System.Windows.Forms.DateTimePicker sdateTimePicker;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.PictureBox pbItem;
        private System.Windows.Forms.ComboBox cmb_searby;
        private System.Windows.Forms.DataGridView dgridItem;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblItemID;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExportExcel;
    }
}