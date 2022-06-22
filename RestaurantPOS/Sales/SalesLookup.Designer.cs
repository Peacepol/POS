namespace AbleRetailPOS
{
    partial class SalesLookup
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
            this.lvSales = new System.Windows.Forms.ListView();
            this.SalesNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Customer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InvoiceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SalesType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InvoiceStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvSales
            // 
            this.lvSales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SalesNumber,
            this.Customer,
            this.InvoiceType,
            this.SalesType,
            this.InvoiceStatus});
            this.lvSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lvSales.FullRowSelect = true;
            this.lvSales.GridLines = true;
            this.lvSales.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSales.Location = new System.Drawing.Point(12, 13);
            this.lvSales.Name = "lvSales";
            this.lvSales.Size = new System.Drawing.Size(528, 485);
            this.lvSales.TabIndex = 168;
            this.lvSales.UseCompatibleStateImageBehavior = false;
            this.lvSales.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvSales_ColumnWidthchanging);
            this.lvSales.DoubleClick += new System.EventHandler(this.lvSales_DoubleClick);
            // 
            // SalesNumber
            // 
            this.SalesNumber.Text = "Sales Number";
            this.SalesNumber.Width = 100;
            // 
            // Customer
            // 
            this.Customer.Text = "Customer";
            this.Customer.Width = 200;
            // 
            // InvoiceType
            // 
            this.InvoiceType.Text = "Invoice Type";
            this.InvoiceType.Width = 100;
            // 
            // SalesType
            // 
            this.SalesType.Text = "Sales Type";
            this.SalesType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SalesType.Width = 0;
            // 
            // InvoiceStatus
            // 
            this.InvoiceStatus.Text = "Invoice Status";
            this.InvoiceStatus.Width = 0;
            // 
            // SalesLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 507);
            this.Controls.Add(this.lvSales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(622, 638);
            this.MinimizeBox = false;
            this.Name = "SalesLookup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Lookup";
            this.Load += new System.EventHandler(this.selectSalesLookupList_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvSales;
        private System.Windows.Forms.ColumnHeader SalesNumber;
        private System.Windows.Forms.ColumnHeader Customer;
        private System.Windows.Forms.ColumnHeader SalesType;
        private System.Windows.Forms.ColumnHeader InvoiceStatus;
        internal System.Windows.Forms.ColumnHeader InvoiceType;
    }
}