namespace RestaurantPOS.Sales
{
    partial class SalesNP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesNP));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQuickSale = new System.Windows.Forms.Button();
            this.btnTransactionLookup = new System.Windows.Forms.Button();
            this.btnTransaction = new System.Windows.Forms.Button();
            this.receivePayment_btn = new System.Windows.Forms.Button();
            this.salesReg_btn = new System.Windows.Forms.Button();
            this.entersales_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQuickSale);
            this.groupBox1.Controls.Add(this.btnTransactionLookup);
            this.groupBox1.Controls.Add(this.btnTransaction);
            this.groupBox1.Controls.Add(this.receivePayment_btn);
            this.groupBox1.Controls.Add(this.salesReg_btn);
            this.groupBox1.Controls.Add(this.entersales_btn);
            this.groupBox1.Location = new System.Drawing.Point(8, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(765, 405);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnQuickSale
            // 
            this.btnQuickSale.BackgroundImage = global::RestaurantPOS.Properties.Resources.QuickSales;
            this.btnQuickSale.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuickSale.Location = new System.Drawing.Point(510, 35);
            this.btnQuickSale.Name = "btnQuickSale";
            this.btnQuickSale.Size = new System.Drawing.Size(155, 148);
            this.btnQuickSale.TabIndex = 17;
            this.btnQuickSale.UseVisualStyleBackColor = true;
            this.btnQuickSale.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTransactionLookup
            // 
            this.btnTransactionLookup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTransactionLookup.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransactionLookup.Image = global::RestaurantPOS.Properties.Resources.search32;
            this.btnTransactionLookup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTransactionLookup.Location = new System.Drawing.Point(58, 290);
            this.btnTransactionLookup.Name = "btnTransactionLookup";
            this.btnTransactionLookup.Size = new System.Drawing.Size(97, 69);
            this.btnTransactionLookup.TabIndex = 16;
            this.btnTransactionLookup.Text = "TRANSACTIONS LOOKUP";
            this.btnTransactionLookup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTransactionLookup.UseVisualStyleBackColor = true;
            this.btnTransactionLookup.Click += new System.EventHandler(this.btnTransactionLookup_Click);
            // 
            // btnTransaction
            // 
            this.btnTransaction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTransaction.BackgroundImage")));
            this.btnTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTransaction.Location = new System.Drawing.Point(189, 211);
            this.btnTransaction.Name = "btnTransaction";
            this.btnTransaction.Size = new System.Drawing.Size(160, 148);
            this.btnTransaction.TabIndex = 4;
            this.btnTransaction.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTransaction.UseVisualStyleBackColor = true;
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // receivePayment_btn
            // 
            this.receivePayment_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("receivePayment_btn.BackgroundImage")));
            this.receivePayment_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.receivePayment_btn.Image = ((System.Drawing.Image)(resources.GetObject("receivePayment_btn.Image")));
            this.receivePayment_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.receivePayment_btn.Location = new System.Drawing.Point(402, 211);
            this.receivePayment_btn.Name = "receivePayment_btn";
            this.receivePayment_btn.Size = new System.Drawing.Size(160, 148);
            this.receivePayment_btn.TabIndex = 3;
            this.receivePayment_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.receivePayment_btn.UseVisualStyleBackColor = true;
            this.receivePayment_btn.Click += new System.EventHandler(this.receivePayment_btn_Click);
            // 
            // salesReg_btn
            // 
            this.salesReg_btn.BackgroundImage = global::RestaurantPOS.Properties.Resources.Sales_Register;
            this.salesReg_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.salesReg_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.salesReg_btn.Location = new System.Drawing.Point(90, 35);
            this.salesReg_btn.Name = "salesReg_btn";
            this.salesReg_btn.Size = new System.Drawing.Size(160, 148);
            this.salesReg_btn.TabIndex = 2;
            this.salesReg_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.salesReg_btn.UseVisualStyleBackColor = true;
            this.salesReg_btn.Click += new System.EventHandler(this.salesReg_btn_Click);
            // 
            // entersales_btn
            // 
            this.entersales_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("entersales_btn.BackgroundImage")));
            this.entersales_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.entersales_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.entersales_btn.Location = new System.Drawing.Point(303, 35);
            this.entersales_btn.Name = "entersales_btn";
            this.entersales_btn.Size = new System.Drawing.Size(160, 148);
            this.entersales_btn.TabIndex = 1;
            this.entersales_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.entersales_btn.UseVisualStyleBackColor = true;
            this.entersales_btn.Click += new System.EventHandler(this.entersales_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(341, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sales";
            // 
            // SalesNP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SalesNP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Navigation";
            this.Load += new System.EventHandler(this.SalesNP_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button receivePayment_btn;
        private System.Windows.Forms.Button salesReg_btn;
        private System.Windows.Forms.Button entersales_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTransaction;
        private System.Windows.Forms.Button btnTransactionLookup;
        private System.Windows.Forms.Button btnQuickSale;
    }
}