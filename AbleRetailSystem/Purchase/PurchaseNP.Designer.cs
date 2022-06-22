namespace RestaurantPOS.Purchase
{
    partial class PurchaseNP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseNP));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReplenish = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTransactionLookup = new System.Windows.Forms.Button();
            this.btnTransaction = new System.Windows.Forms.Button();
            this.btn_PurchaseEnter = new System.Windows.Forms.Button();
            this.btn_PurchaseReg = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReplenish);
            this.groupBox1.Controls.Add(this.btnTransactionLookup);
            this.groupBox1.Controls.Add(this.btnTransaction);
            this.groupBox1.Controls.Add(this.btn_PurchaseEnter);
            this.groupBox1.Controls.Add(this.btn_PurchaseReg);
            this.groupBox1.Location = new System.Drawing.Point(8, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(708, 405);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnReplenish
            // 
            this.btnReplenish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReplenish.Image = global::RestaurantPOS.Properties.Resources.StockReplenishment;
            this.btnReplenish.Location = new System.Drawing.Point(384, 210);
            this.btnReplenish.Name = "btnReplenish";
            this.btnReplenish.Size = new System.Drawing.Size(151, 148);
            this.btnReplenish.TabIndex = 18;
            this.btnReplenish.UseVisualStyleBackColor = true;
            this.btnReplenish.Click += new System.EventHandler(this.btnReplenish_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(309, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Purchase";
            // 
            // btnTransactionLookup
            // 
            this.btnTransactionLookup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTransactionLookup.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransactionLookup.Image = global::RestaurantPOS.Properties.Resources.search32;
            this.btnTransactionLookup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTransactionLookup.Location = new System.Drawing.Point(43, 290);
            this.btnTransactionLookup.Name = "btnTransactionLookup";
            this.btnTransactionLookup.Size = new System.Drawing.Size(97, 69);
            this.btnTransactionLookup.TabIndex = 17;
            this.btnTransactionLookup.Text = "TRANSACTIONS LOOKUP";
            this.btnTransactionLookup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTransactionLookup.UseVisualStyleBackColor = true;
            this.btnTransactionLookup.Click += new System.EventHandler(this.btnTransactionLookup_Click);
            // 
            // btnTransaction
            // 
            this.btnTransaction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTransaction.BackgroundImage")));
            this.btnTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTransaction.Location = new System.Drawing.Point(171, 211);
            this.btnTransaction.Name = "btnTransaction";
            this.btnTransaction.Size = new System.Drawing.Size(151, 148);
            this.btnTransaction.TabIndex = 7;
            this.btnTransaction.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTransaction.UseVisualStyleBackColor = true;
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // btn_PurchaseEnter
            // 
            this.btn_PurchaseEnter.Image = ((System.Drawing.Image)(resources.GetObject("btn_PurchaseEnter.Image")));
            this.btn_PurchaseEnter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_PurchaseEnter.Location = new System.Drawing.Point(384, 29);
            this.btn_PurchaseEnter.Name = "btn_PurchaseEnter";
            this.btn_PurchaseEnter.Size = new System.Drawing.Size(151, 148);
            this.btn_PurchaseEnter.TabIndex = 5;
            this.btn_PurchaseEnter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_PurchaseEnter.UseVisualStyleBackColor = true;
            this.btn_PurchaseEnter.Click += new System.EventHandler(this.btn_PurchaseEnter_Click);
            // 
            // btn_PurchaseReg
            // 
            this.btn_PurchaseReg.Image = ((System.Drawing.Image)(resources.GetObject("btn_PurchaseReg.Image")));
            this.btn_PurchaseReg.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_PurchaseReg.Location = new System.Drawing.Point(171, 29);
            this.btn_PurchaseReg.Name = "btn_PurchaseReg";
            this.btn_PurchaseReg.Size = new System.Drawing.Size(151, 148);
            this.btn_PurchaseReg.TabIndex = 4;
            this.btn_PurchaseReg.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_PurchaseReg.UseVisualStyleBackColor = true;
            this.btn_PurchaseReg.Click += new System.EventHandler(this.btn_PurchaseReg_Click);
            // 
            // PurchaseNP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(724, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PurchaseNP";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Navigation Pane";
            this.Load += new System.EventHandler(this.PurchaseNP_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_PurchaseReg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_PurchaseEnter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTransaction;
        private System.Windows.Forms.Button btnTransactionLookup;
        private System.Windows.Forms.Button btnReplenish;
    }
}