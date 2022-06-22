namespace AbleRetailPOS
{
    partial class ReportsNavigation
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
            this.btnOK = new System.Windows.Forms.Button();
            this.tabCustomerSupplierList = new System.Windows.Forms.TabPage();
            this.listCustomerSupplierReports = new System.Windows.Forms.ListBox();
            this.tabJob = new System.Windows.Forms.TabPage();
            this.listJobReports = new System.Windows.Forms.ListBox();
            this.tabTax = new System.Windows.Forms.TabPage();
            this.listTaxReport = new System.Windows.Forms.ListBox();
            this.tabPurchases = new System.Windows.Forms.TabPage();
            this.listPurchasesReports = new System.Windows.Forms.ListBox();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.listInventoryReports = new System.Windows.Forms.ListBox();
            this.tabSales = new System.Windows.Forms.TabPage();
            this.listSalesReports = new System.Windows.Forms.ListBox();
            this.ReportsContainer = new System.Windows.Forms.TabControl();
            this.tabSessionReports = new System.Windows.Forms.TabPage();
            this.listSummaryReport = new System.Windows.Forms.ListBox();
            this.tabPromotionDiscountReports = new System.Windows.Forms.TabPage();
            this.listPromotionAndDiscount = new System.Windows.Forms.ListBox();
            this.tabCustomerSupplierList.SuspendLayout();
            this.tabJob.SuspendLayout();
            this.tabTax.SuspendLayout();
            this.tabPurchases.SuspendLayout();
            this.tabInventory.SuspendLayout();
            this.tabSales.SuspendLayout();
            this.ReportsContainer.SuspendLayout();
            this.tabSessionReports.SuspendLayout();
            this.tabPromotionDiscountReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(443, 491);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 41);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabCustomerSupplierList
            // 
            this.tabCustomerSupplierList.Controls.Add(this.listCustomerSupplierReports);
            this.tabCustomerSupplierList.Location = new System.Drawing.Point(4, 22);
            this.tabCustomerSupplierList.Name = "tabCustomerSupplierList";
            this.tabCustomerSupplierList.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomerSupplierList.Size = new System.Drawing.Size(509, 447);
            this.tabCustomerSupplierList.TabIndex = 7;
            this.tabCustomerSupplierList.Text = "Customer & Supplier";
            this.tabCustomerSupplierList.UseVisualStyleBackColor = true;
            // 
            // listCustomerSupplierReports
            // 
            this.listCustomerSupplierReports.FormattingEnabled = true;
            this.listCustomerSupplierReports.Items.AddRange(new object[] {
            "Customer & Supplier Reports",
            "          Customer & Supplier List",
            "          Loyalty Member"});
            this.listCustomerSupplierReports.Location = new System.Drawing.Point(7, 7);
            this.listCustomerSupplierReports.Name = "listCustomerSupplierReports";
            this.listCustomerSupplierReports.Size = new System.Drawing.Size(496, 420);
            this.listCustomerSupplierReports.TabIndex = 0;
            this.listCustomerSupplierReports.DoubleClick += new System.EventHandler(this.listCustomerSupplierReports_DoubleClick);
            // 
            // tabJob
            // 
            this.tabJob.Controls.Add(this.listJobReports);
            this.tabJob.Location = new System.Drawing.Point(4, 22);
            this.tabJob.Name = "tabJob";
            this.tabJob.Padding = new System.Windows.Forms.Padding(3);
            this.tabJob.Size = new System.Drawing.Size(509, 447);
            this.tabJob.TabIndex = 6;
            this.tabJob.Text = "Job";
            this.tabJob.UseVisualStyleBackColor = true;
            // 
            // listJobReports
            // 
            this.listJobReports.FormattingEnabled = true;
            this.listJobReports.Items.AddRange(new object[] {
            "Job Reports",
            "          Job List",
            "          Job Activity",
            "          Job Transactions",
            "          Job Profit And Loss"});
            this.listJobReports.Location = new System.Drawing.Point(7, 7);
            this.listJobReports.Name = "listJobReports";
            this.listJobReports.Size = new System.Drawing.Size(496, 420);
            this.listJobReports.TabIndex = 0;
            this.listJobReports.DoubleClick += new System.EventHandler(this.listJobReports_DoubleClick);
            // 
            // tabTax
            // 
            this.tabTax.Controls.Add(this.listTaxReport);
            this.tabTax.Location = new System.Drawing.Point(4, 22);
            this.tabTax.Name = "tabTax";
            this.tabTax.Padding = new System.Windows.Forms.Padding(3);
            this.tabTax.Size = new System.Drawing.Size(509, 447);
            this.tabTax.TabIndex = 4;
            this.tabTax.Text = "Tax";
            this.tabTax.UseVisualStyleBackColor = true;
            // 
            // listTaxReport
            // 
            this.listTaxReport.FormattingEnabled = true;
            this.listTaxReport.Items.AddRange(new object[] {
            "Tax Code Reports",
            "          Tax Transaction Summarry",
            "          Tax Transaction Details"});
            this.listTaxReport.Location = new System.Drawing.Point(7, 7);
            this.listTaxReport.Name = "listTaxReport";
            this.listTaxReport.Size = new System.Drawing.Size(496, 420);
            this.listTaxReport.TabIndex = 0;
            this.listTaxReport.DoubleClick += new System.EventHandler(this.listTaxReport_DoubleClick);
            // 
            // tabPurchases
            // 
            this.tabPurchases.Controls.Add(this.listPurchasesReports);
            this.tabPurchases.Location = new System.Drawing.Point(4, 22);
            this.tabPurchases.Name = "tabPurchases";
            this.tabPurchases.Padding = new System.Windows.Forms.Padding(3);
            this.tabPurchases.Size = new System.Drawing.Size(509, 447);
            this.tabPurchases.TabIndex = 2;
            this.tabPurchases.Text = "Purchases";
            this.tabPurchases.UseVisualStyleBackColor = true;
            // 
            // listPurchasesReports
            // 
            this.listPurchasesReports.FormattingEnabled = true;
            this.listPurchasesReports.Items.AddRange(new object[] {
            "Supplier",
            "          Purchase Summary",
            "          Purchase Details",
            "          Analyse Purchase",
            "          Analyse Purchase(Date Range Comparison)",
            "",
            "Purchase Register",
            "          All Purchases",
            "          Received Orders Summary",
            "          Received Orders Detail",
            "       ",
            "Items",
            "          Supplier Item Summary",
            "          Supplier Item Details"});
            this.listPurchasesReports.Location = new System.Drawing.Point(7, 7);
            this.listPurchasesReports.Name = "listPurchasesReports";
            this.listPurchasesReports.Size = new System.Drawing.Size(496, 433);
            this.listPurchasesReports.TabIndex = 0;
            this.listPurchasesReports.DoubleClick += new System.EventHandler(this.listPurchasesReports_DoubleClick);
            // 
            // tabInventory
            // 
            this.tabInventory.Controls.Add(this.listInventoryReports);
            this.tabInventory.Location = new System.Drawing.Point(4, 22);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventory.Size = new System.Drawing.Size(509, 447);
            this.tabInventory.TabIndex = 1;
            this.tabInventory.Text = "Inventory";
            this.tabInventory.UseVisualStyleBackColor = true;
            // 
            // listInventoryReports
            // 
            this.listInventoryReports.FormattingEnabled = true;
            this.listInventoryReports.Items.AddRange(new object[] {
            "Items",
            "          Item List Summary",
            "          Item List Details",
            "          Analyse Inventory Summary",
            "          Analyse Inventory Details",
            "          AutoBuild Items",
            "          Inventory Count Sheet",
            "          Item Register Summary",
            "          Item Transactions",
            "          Best Selling Items",
            "          Category List",
            "          Sales Ingredients",
            "",
            "Pricing",
            "          Price List",
            "          Price Analysis",
            "",
            "Sales Analysis",
            "          Item Sales Analysis",
            "          Category Sales",
            "           "});
            this.listInventoryReports.Location = new System.Drawing.Point(7, 7);
            this.listInventoryReports.Name = "listInventoryReports";
            this.listInventoryReports.Size = new System.Drawing.Size(496, 420);
            this.listInventoryReports.TabIndex = 0;
            this.listInventoryReports.DoubleClick += new System.EventHandler(this.listInventoryReports_DoubleClick);
            // 
            // tabSales
            // 
            this.tabSales.Controls.Add(this.listSalesReports);
            this.tabSales.Location = new System.Drawing.Point(4, 22);
            this.tabSales.Name = "tabSales";
            this.tabSales.Padding = new System.Windows.Forms.Padding(3);
            this.tabSales.Size = new System.Drawing.Size(509, 447);
            this.tabSales.TabIndex = 0;
            this.tabSales.Text = "Sales";
            this.tabSales.UseVisualStyleBackColor = true;
            // 
            // listSalesReports
            // 
            this.listSalesReports.FormattingEnabled = true;
            this.listSalesReports.Items.AddRange(new object[] {
            "Customer",
            "          Sales Summary",
            "          Sales Details",
            "          Sales Item Summary ",
            "          Sales Item Details",
            "          Analyse Sales",
            "          Analyse Sales(Date Range Comparison)",
            "          Customer Payments(Closed Invoice)",
            "          Customer Ledger",
            "",
            "Sales Register",
            "          All Sales ",
            "          Closed Invoice ",
            "          Open Invoices",
            "          Orders ",
            "          Quotes ",
            "          Lay-By",
            "",
            "Recievables",
            "          Age Receivable Summary",
            "          Age Receivable Details",
            "          Summary With Tax ",
            " ",
            "Other Sales Reports",
            "          Customer Statement",
            "          Invoice Transaction",
            "",
            "Transaction Journals ",
            "          Sales Receivable Journal",
            "          Print Receipt"});
            this.listSalesReports.Location = new System.Drawing.Point(6, 0);
            this.listSalesReports.Name = "listSalesReports";
            this.listSalesReports.Size = new System.Drawing.Size(497, 433);
            this.listSalesReports.TabIndex = 0;
            this.listSalesReports.DoubleClick += new System.EventHandler(this.listSalesReports_DoubleClick);
            // 
            // ReportsContainer
            // 
            this.ReportsContainer.Controls.Add(this.tabSales);
            this.ReportsContainer.Controls.Add(this.tabInventory);
            this.ReportsContainer.Controls.Add(this.tabPurchases);
            this.ReportsContainer.Controls.Add(this.tabTax);
            this.ReportsContainer.Controls.Add(this.tabJob);
            this.ReportsContainer.Controls.Add(this.tabCustomerSupplierList);
            this.ReportsContainer.Controls.Add(this.tabSessionReports);
            this.ReportsContainer.Controls.Add(this.tabPromotionDiscountReports);
            this.ReportsContainer.Location = new System.Drawing.Point(12, 12);
            this.ReportsContainer.Name = "ReportsContainer";
            this.ReportsContainer.SelectedIndex = 0;
            this.ReportsContainer.Size = new System.Drawing.Size(517, 473);
            this.ReportsContainer.TabIndex = 1;
            this.ReportsContainer.SelectedIndexChanged += new System.EventHandler(this.ReportsContainer_SelectedIndexChanged);
            // 
            // tabSessionReports
            // 
            this.tabSessionReports.Controls.Add(this.listSummaryReport);
            this.tabSessionReports.Location = new System.Drawing.Point(4, 22);
            this.tabSessionReports.Name = "tabSessionReports";
            this.tabSessionReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabSessionReports.Size = new System.Drawing.Size(509, 447);
            this.tabSessionReports.TabIndex = 8;
            this.tabSessionReports.Text = "POS Session";
            this.tabSessionReports.UseVisualStyleBackColor = true;
            // 
            // listSummaryReport
            // 
            this.listSummaryReport.FormattingEnabled = true;
            this.listSummaryReport.Items.AddRange(new object[] {
            "POS Session Reports",
            "          Summary & Detail Reports",
            "          Tender Summary",
            "          Tender Detail"});
            this.listSummaryReport.Location = new System.Drawing.Point(6, 7);
            this.listSummaryReport.Name = "listSummaryReport";
            this.listSummaryReport.Size = new System.Drawing.Size(497, 420);
            this.listSummaryReport.TabIndex = 0;
            this.listSummaryReport.DoubleClick += new System.EventHandler(this.listSummaryReport_DoubleClick);
            // 
            // tabPromotionDiscountReports
            // 
            this.tabPromotionDiscountReports.Controls.Add(this.listPromotionAndDiscount);
            this.tabPromotionDiscountReports.Location = new System.Drawing.Point(4, 22);
            this.tabPromotionDiscountReports.Name = "tabPromotionDiscountReports";
            this.tabPromotionDiscountReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabPromotionDiscountReports.Size = new System.Drawing.Size(509, 447);
            this.tabPromotionDiscountReports.TabIndex = 9;
            this.tabPromotionDiscountReports.Text = "Promotions & Discount";
            this.tabPromotionDiscountReports.UseVisualStyleBackColor = true;
            // 
            // listPromotionAndDiscount
            // 
            this.listPromotionAndDiscount.FormattingEnabled = true;
            this.listPromotionAndDiscount.Items.AddRange(new object[] {
            "Promotion & Discount Reports",
            "          Sales By Promo",
            "          Discount Report",
            "          Free Product Report"});
            this.listPromotionAndDiscount.Location = new System.Drawing.Point(6, 6);
            this.listPromotionAndDiscount.Name = "listPromotionAndDiscount";
            this.listPromotionAndDiscount.Size = new System.Drawing.Size(497, 433);
            this.listPromotionAndDiscount.TabIndex = 0;
            this.listPromotionAndDiscount.DoubleClick += new System.EventHandler(this.listPromotionAndDiscount_DoubleClick);
            // 
            // ReportsNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 544);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ReportsContainer);
            this.MaximizeBox = false;
            this.Name = "ReportsNavigation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports Navigation Pane";
            this.Load += new System.EventHandler(this.ReportsNavigation_Load);
            this.tabCustomerSupplierList.ResumeLayout(false);
            this.tabJob.ResumeLayout(false);
            this.tabTax.ResumeLayout(false);
            this.tabPurchases.ResumeLayout(false);
            this.tabInventory.ResumeLayout(false);
            this.tabSales.ResumeLayout(false);
            this.ReportsContainer.ResumeLayout(false);
            this.tabSessionReports.ResumeLayout(false);
            this.tabPromotionDiscountReports.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabPage tabCustomerSupplierList;
        private System.Windows.Forms.ListBox listCustomerSupplierReports;
        private System.Windows.Forms.TabPage tabJob;
        private System.Windows.Forms.ListBox listJobReports;
        private System.Windows.Forms.TabPage tabTax;
        private System.Windows.Forms.ListBox listTaxReport;
        private System.Windows.Forms.TabPage tabPurchases;
        private System.Windows.Forms.ListBox listPurchasesReports;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.ListBox listInventoryReports;
        private System.Windows.Forms.TabPage tabSales;
        private System.Windows.Forms.ListBox listSalesReports;
        private System.Windows.Forms.TabControl ReportsContainer;
        private System.Windows.Forms.TabPage tabSessionReports;
        private System.Windows.Forms.ListBox listSummaryReport;
        private System.Windows.Forms.TabPage tabPromotionDiscountReports;
        private System.Windows.Forms.ListBox listPromotionAndDiscount;
    }
}