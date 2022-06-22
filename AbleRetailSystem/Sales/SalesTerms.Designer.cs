namespace RestaurantPOS.Sales
{
    partial class SalesTerms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesTerms));
            this.lblBalanceNote = new System.Windows.Forms.Label();
            this.lblDiscountNote = new System.Windows.Forms.Label();
            this.txtVolumeDiscount = new System.Windows.Forms.NumericUpDown();
            this.txtLatePaymentCharge = new System.Windows.Forms.NumericUpDown();
            this.txtEarlyPayment = new System.Windows.Forms.NumericUpDown();
            this.txtDiscount = new System.Windows.Forms.NumericUpDown();
            this.txtBalance = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.cboTerms = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btn_Record = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtVolumeDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLatePaymentCharge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEarlyPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBalanceNote
            // 
            this.lblBalanceNote.AutoSize = true;
            this.lblBalanceNote.Location = new System.Drawing.Point(225, 64);
            this.lblBalanceNote.Name = "lblBalanceNote";
            this.lblBalanceNote.Size = new System.Drawing.Size(22, 13);
            this.lblBalanceNote.TabIndex = 168;
            this.lblBalanceNote.Text = "     ";
            // 
            // lblDiscountNote
            // 
            this.lblDiscountNote.AutoSize = true;
            this.lblDiscountNote.Location = new System.Drawing.Point(225, 98);
            this.lblDiscountNote.Name = "lblDiscountNote";
            this.lblDiscountNote.Size = new System.Drawing.Size(22, 13);
            this.lblDiscountNote.TabIndex = 167;
            this.lblDiscountNote.Text = "     ";
            // 
            // txtVolumeDiscount
            // 
            this.txtVolumeDiscount.DecimalPlaces = 2;
            this.txtVolumeDiscount.Location = new System.Drawing.Point(153, 126);
            this.txtVolumeDiscount.Name = "txtVolumeDiscount";
            this.txtVolumeDiscount.Size = new System.Drawing.Size(65, 20);
            this.txtVolumeDiscount.TabIndex = 158;
            this.txtVolumeDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVolumeDiscount.ValueChanged += new System.EventHandler(this.txtVolumeDiscount_ValueChanged);
            // 
            // txtLatePaymentCharge
            // 
            this.txtLatePaymentCharge.DecimalPlaces = 2;
            this.txtLatePaymentCharge.Location = new System.Drawing.Point(175, 192);
            this.txtLatePaymentCharge.Name = "txtLatePaymentCharge";
            this.txtLatePaymentCharge.Size = new System.Drawing.Size(65, 20);
            this.txtLatePaymentCharge.TabIndex = 160;
            this.txtLatePaymentCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLatePaymentCharge.ValueChanged += new System.EventHandler(this.txtLatePaymentCharge_ValueChanged);
            // 
            // txtEarlyPayment
            // 
            this.txtEarlyPayment.DecimalPlaces = 2;
            this.txtEarlyPayment.Location = new System.Drawing.Point(175, 166);
            this.txtEarlyPayment.Name = "txtEarlyPayment";
            this.txtEarlyPayment.Size = new System.Drawing.Size(65, 20);
            this.txtEarlyPayment.TabIndex = 159;
            this.txtEarlyPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEarlyPayment.ValueChanged += new System.EventHandler(this.txtEarlyPayment_ValueChanged);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(153, 92);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(65, 20);
            this.txtDiscount.TabIndex = 157;
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiscount.ValueChanged += new System.EventHandler(this.txtDiscount_ValueChanged);
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new System.Drawing.Point(153, 61);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(65, 20);
            this.txtBalance.TabIndex = 156;
            this.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBalance.ValueChanged += new System.EventHandler(this.txtBalance_ValueChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.SteelBlue;
            this.label32.Location = new System.Drawing.Point(39, 194);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(120, 13);
            this.label32.TabIndex = 166;
            this.label32.Text = "Late Payment Charge %";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.SteelBlue;
            this.label28.Location = new System.Drawing.Point(39, 168);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(130, 13);
            this.label28.TabIndex = 165;
            this.label28.Text = "Early Payment Discount %";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDiscount.Location = new System.Drawing.Point(43, 98);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(76, 13);
            this.lblDiscount.TabIndex = 164;
            this.lblDiscount.Text = "Discount Days";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblBalance.Location = new System.Drawing.Point(43, 66);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(96, 13);
            this.lblBalance.TabIndex = 163;
            this.lblBalance.Text = "Balance Due Days";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.SteelBlue;
            this.label30.Location = new System.Drawing.Point(43, 128);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(98, 13);
            this.label30.TabIndex = 162;
            this.label30.Text = "Volume Discount %";
            // 
            // cboTerms
            // 
            this.cboTerms.FormattingEnabled = true;
            this.cboTerms.Location = new System.Drawing.Point(153, 24);
            this.cboTerms.Name = "cboTerms";
            this.cboTerms.Size = new System.Drawing.Size(194, 21);
            this.cboTerms.TabIndex = 154;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(45, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(102, 13);
            this.label21.TabIndex = 153;
            this.label21.Text = "Payment Is Due On:";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(366, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 34);
            this.btnCancel.TabIndex = 218;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_Record
            // 
            this.btn_Record.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_Record.ForeColor = System.Drawing.Color.Black;
            this.btn_Record.Image = ((System.Drawing.Image)(resources.GetObject("btn_Record.Image")));
            this.btn_Record.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Record.Location = new System.Drawing.Point(297, 240);
            this.btn_Record.Name = "btn_Record";
            this.btn_Record.Size = new System.Drawing.Size(63, 34);
            this.btn_Record.TabIndex = 217;
            this.btn_Record.Text = "OK";
            this.btn_Record.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Record.UseVisualStyleBackColor = true;
            this.btn_Record.Click += new System.EventHandler(this.btn_Record_Click);
            // 
            // SalesTerms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 335);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btn_Record);
            this.Controls.Add(this.lblBalanceNote);
            this.Controls.Add(this.lblDiscountNote);
            this.Controls.Add(this.txtVolumeDiscount);
            this.Controls.Add(this.txtLatePaymentCharge);
            this.Controls.Add(this.txtEarlyPayment);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cboTerms);
            this.Controls.Add(this.label21);
            this.MinimizeBox = false;
            this.Name = "SalesTerms";
            this.ShowIcon = false;
            this.Text = "SalesTerms";
            this.Load += new System.EventHandler(this.SalesTerms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtVolumeDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLatePaymentCharge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEarlyPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBalanceNote;
        private System.Windows.Forms.Label lblDiscountNote;
        private System.Windows.Forms.NumericUpDown txtVolumeDiscount;
        private System.Windows.Forms.NumericUpDown txtLatePaymentCharge;
        private System.Windows.Forms.NumericUpDown txtEarlyPayment;
        private System.Windows.Forms.NumericUpDown txtDiscount;
        private System.Windows.Forms.NumericUpDown txtBalance;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cboTerms;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btn_Record;
    }
}