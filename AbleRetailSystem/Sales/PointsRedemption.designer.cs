namespace RestaurantPOS
{
    partial class PointsRedemption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PointsRedemption));
            this.cmbRedeemType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalPointsAvailable = new System.Windows.Forms.TextBox();
            this.btnRedeem = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.customerText = new System.Windows.Forms.TextBox();
            this.pbAccount = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEquivAmt = new System.Windows.Forms.TextBox();
            this.txtUsePts = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUsePts = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRedeemType
            // 
            this.cmbRedeemType.FormattingEnabled = true;
            this.cmbRedeemType.Items.AddRange(new object[] {
            "Gift Certificate",
            "Item",
            "Price Discount"});
            this.cmbRedeemType.Location = new System.Drawing.Point(125, 18);
            this.cmbRedeemType.Name = "cmbRedeemType";
            this.cmbRedeemType.Size = new System.Drawing.Size(121, 21);
            this.cmbRedeemType.TabIndex = 0;
            this.cmbRedeemType.SelectedIndexChanged += new System.EventHandler(this.cmbRedeemType_SelectedIndexChanged);
            this.cmbRedeemType.TextChanged += new System.EventHandler(this.cmbRedeemType_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Redemption Type:";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(12, 87);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(632, 218);
            this.dgvItems.TabIndex = 2;
            this.dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Points Available: ";
            // 
            // txtTotalPointsAvailable
            // 
            this.txtTotalPointsAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPointsAvailable.Location = new System.Drawing.Point(148, 335);
            this.txtTotalPointsAvailable.Name = "txtTotalPointsAvailable";
            this.txtTotalPointsAvailable.ReadOnly = true;
            this.txtTotalPointsAvailable.Size = new System.Drawing.Size(100, 22);
            this.txtTotalPointsAvailable.TabIndex = 5;
            // 
            // btnRedeem
            // 
            this.btnRedeem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedeem.Location = new System.Drawing.Point(548, 325);
            this.btnRedeem.Name = "btnRedeem";
            this.btnRedeem.Size = new System.Drawing.Size(97, 41);
            this.btnRedeem.TabIndex = 7;
            this.btnRedeem.Text = "Redeem";
            this.btnRedeem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRedeem.UseVisualStyleBackColor = true;
            this.btnRedeem.Click += new System.EventHandler(this.btnRedeem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(56, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 181;
            this.label4.Text = "Customer:";
            // 
            // customerText
            // 
            this.customerText.Location = new System.Drawing.Point(125, 45);
            this.customerText.Name = "customerText";
            this.customerText.Size = new System.Drawing.Size(218, 20);
            this.customerText.TabIndex = 182;
            // 
            // pbAccount
            // 
            this.pbAccount.BackColor = System.Drawing.SystemColors.Control;
            this.pbAccount.Image = ((System.Drawing.Image)(resources.GetObject("pbAccount.Image")));
            this.pbAccount.Location = new System.Drawing.Point(349, 46);
            this.pbAccount.Name = "pbAccount";
            this.pbAccount.Size = new System.Drawing.Size(19, 19);
            this.pbAccount.TabIndex = 183;
            this.pbAccount.TabStop = false;
            this.pbAccount.Click += new System.EventHandler(this.pbAccount_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::RestaurantPOS.Properties.Resources.clear32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(445, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 41);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(393, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 184;
            this.label3.Text = "Equivalent Amount:";
            this.label3.Visible = false;
            // 
            // txtEquivAmt
            // 
            this.txtEquivAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEquivAmt.Location = new System.Drawing.Point(511, 16);
            this.txtEquivAmt.Name = "txtEquivAmt";
            this.txtEquivAmt.ReadOnly = true;
            this.txtEquivAmt.Size = new System.Drawing.Size(100, 22);
            this.txtEquivAmt.TabIndex = 185;
            this.txtEquivAmt.Visible = false;
            // 
            // txtUsePts
            // 
            this.txtUsePts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsePts.Location = new System.Drawing.Point(511, 43);
            this.txtUsePts.Name = "txtUsePts";
            this.txtUsePts.Size = new System.Drawing.Size(92, 22);
            this.txtUsePts.TabIndex = 187;
            this.txtUsePts.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(436, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 186;
            this.label5.Text = "Use Points:";
            this.label5.Visible = false;
            // 
            // btnUsePts
            // 
            this.btnUsePts.Location = new System.Drawing.Point(609, 42);
            this.btnUsePts.Name = "btnUsePts";
            this.btnUsePts.Size = new System.Drawing.Size(34, 23);
            this.btnUsePts.TabIndex = 188;
            this.btnUsePts.Text = "Use";
            this.btnUsePts.UseVisualStyleBackColor = true;
            this.btnUsePts.Visible = false;
            this.btnUsePts.Click += new System.EventHandler(this.btnUsePts_Click);
            // 
            // PointsRedemption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 380);
            this.Controls.Add(this.btnUsePts);
            this.Controls.Add(this.txtUsePts);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEquivAmt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbAccount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customerText);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRedeem);
            this.Controls.Add(this.txtTotalPointsAvailable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRedeemType);
            this.Name = "PointsRedemption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Redeem Points";
            this.Load += new System.EventHandler(this.PointsRedemption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRedeemType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalPointsAvailable;
        private System.Windows.Forms.Button btnRedeem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbAccount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox customerText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEquivAmt;
        private System.Windows.Forms.TextBox txtUsePts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUsePts;
    }
}