namespace AbleRetailPOS
{
    partial class GiftCertificateLookup
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
            this.lstGiftCertificate = new System.Windows.Forms.ListView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ItemID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GCNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GCAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsUsed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstGiftCertificate
            // 
            this.lstGiftCertificate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemID,
            this.GCNumber,
            this.GCAmount,
            this.StartDate,
            this.EndDate,
            this.IsUsed});
            this.lstGiftCertificate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstGiftCertificate.FullRowSelect = true;
            this.lstGiftCertificate.GridLines = true;
            this.lstGiftCertificate.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstGiftCertificate.Location = new System.Drawing.Point(12, 38);
            this.lstGiftCertificate.Name = "lstGiftCertificate";
            this.lstGiftCertificate.Size = new System.Drawing.Size(528, 460);
            this.lstGiftCertificate.TabIndex = 168;
            this.lstGiftCertificate.UseCompatibleStateImageBehavior = false;
            this.lstGiftCertificate.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView1_ColumnWidthchanging);
            this.lstGiftCertificate.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(118, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(247, 20);
            this.txtSearch.TabIndex = 169;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 170;
            this.label1.Text = "GC Number :";
            // 
            // ItemID
            // 
            this.ItemID.Text = "Item ID";
            // 
            // GCNumber
            // 
            this.GCNumber.Text = "GC Number";
            this.GCNumber.Width = 100;
            // 
            // GCAmount
            // 
            this.GCAmount.Text = "GC Amount";
            this.GCAmount.Width = 100;
            // 
            // StartDate
            // 
            this.StartDate.Text = "Start Date";
            this.StartDate.Width = 100;
            // 
            // EndDate
            // 
            this.EndDate.Text = "End Date";
            this.EndDate.Width = 100;
            // 
            // IsUsed
            // 
            this.IsUsed.Text = "Used";
            // 
            // GiftCertificateLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 507);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lstGiftCertificate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(621, 636);
            this.MinimizeBox = false;
            this.Name = "GiftCertificateLookup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gift Certificate Lookup";
            this.Load += new System.EventHandler(this.selectTaxCodeList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lstGiftCertificate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader ItemID;
        private System.Windows.Forms.ColumnHeader GCNumber;
        private System.Windows.Forms.ColumnHeader GCAmount;
        private System.Windows.Forms.ColumnHeader StartDate;
        private System.Windows.Forms.ColumnHeader EndDate;
        private System.Windows.Forms.ColumnHeader IsUsed;
    }
}