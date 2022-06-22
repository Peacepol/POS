namespace RestaurantPOS.References
{
    partial class OpeningFund
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOpeningFund = new System.Windows.Forms.NumericUpDown();
            this.btnSaveOpening = new System.Windows.Forms.Button();
            this.cmbTerminal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOpenSession = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningFund)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your Opening Fund :";
            // 
            // txtOpeningFund
            // 
            this.txtOpeningFund.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningFund.Location = new System.Drawing.Point(71, 72);
            this.txtOpeningFund.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtOpeningFund.Name = "txtOpeningFund";
            this.txtOpeningFund.Size = new System.Drawing.Size(188, 26);
            this.txtOpeningFund.TabIndex = 1;
            // 
            // btnSaveOpening
            // 
            this.btnSaveOpening.Location = new System.Drawing.Point(71, 235);
            this.btnSaveOpening.Name = "btnSaveOpening";
            this.btnSaveOpening.Size = new System.Drawing.Size(188, 70);
            this.btnSaveOpening.TabIndex = 2;
            this.btnSaveOpening.Text = "Save";
            this.btnSaveOpening.UseVisualStyleBackColor = true;
            this.btnSaveOpening.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbTerminal
            // 
            this.cmbTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTerminal.FormattingEnabled = true;
            this.cmbTerminal.Location = new System.Drawing.Point(71, 148);
            this.cmbTerminal.Name = "cmbTerminal";
            this.cmbTerminal.Size = new System.Drawing.Size(183, 28);
            this.cmbTerminal.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Terminal :";
            // 
            // lblOpenSession
            // 
            this.lblOpenSession.AutoSize = true;
            this.lblOpenSession.Location = new System.Drawing.Point(30, 79);
            this.lblOpenSession.Name = "lblOpenSession";
            this.lblOpenSession.Size = new System.Drawing.Size(0, 13);
            this.lblOpenSession.TabIndex = 5;
            // 
            // OpeningFund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 327);
            this.Controls.Add(this.lblOpenSession);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTerminal);
            this.Controls.Add(this.btnSaveOpening);
            this.Controls.Add(this.txtOpeningFund);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpeningFund";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opening Fund";
            this.Load += new System.EventHandler(this.OpeningFund_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningFund)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtOpeningFund;
        private System.Windows.Forms.Button btnSaveOpening;
        private System.Windows.Forms.ComboBox cmbTerminal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOpenSession;
    }
}