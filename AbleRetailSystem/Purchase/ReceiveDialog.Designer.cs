namespace RestaurantPOS.Purchase
{
    partial class ReceiveDialog
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
            this.txtReference = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dtReceivedDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnReceive = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSupInvoice = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(135, 99);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(158, 20);
            this.txtReference.TabIndex = 234;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(43, 69);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 233;
            this.label14.Text = "Supplier Invoice:";
            // 
            // dtReceivedDate
            // 
            this.dtReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtReceivedDate.Location = new System.Drawing.Point(135, 40);
            this.dtReceivedDate.Name = "dtReceivedDate";
            this.dtReceivedDate.Size = new System.Drawing.Size(158, 20);
            this.dtReceivedDate.TabIndex = 232;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 231;
            this.label4.Text = "Received Date :";
            // 
            // btnReceive
            // 
            this.btnReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReceive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnReceive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceive.Location = new System.Drawing.Point(188, 157);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(105, 34);
            this.btnReceive.TabIndex = 235;
            this.btnReceive.Text = "RECEIVE";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 236;
            this.label1.Text = "Reference:";
            // 
            // txtSupInvoice
            // 
            this.txtSupInvoice.Location = new System.Drawing.Point(135, 69);
            this.txtSupInvoice.Name = "txtSupInvoice";
            this.txtSupInvoice.Size = new System.Drawing.Size(158, 20);
            this.txtSupInvoice.TabIndex = 237;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Red;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(24, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 34);
            this.btnCancel.TabIndex = 238;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ReceiveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 222);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSupInvoice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dtReceivedDate);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReceiveDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Receive Items";
            this.Load += new System.EventHandler(this.ReceiveDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtReceivedDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSupInvoice;
        private System.Windows.Forms.Button btnCancel;
    }
}