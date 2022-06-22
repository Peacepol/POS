namespace AbleRetailPOS.Sales
{
    partial class EmailStatement
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
            this.txtEmailAddr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.rTxtMessage = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStatementDt = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dgEmailList = new System.Windows.Forms.DataGridView();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgEmailList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEmailAddr
            // 
            this.txtEmailAddr.Location = new System.Drawing.Point(106, 45);
            this.txtEmailAddr.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmailAddr.Name = "txtEmailAddr";
            this.txtEmailAddr.Size = new System.Drawing.Size(362, 20);
            this.txtEmailAddr.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Email Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Subject:";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(106, 69);
            this.txtSubject.Margin = new System.Windows.Forms.Padding(2);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(362, 20);
            this.txtSubject.TabIndex = 2;
            // 
            // rTxtMessage
            // 
            this.rTxtMessage.Location = new System.Drawing.Point(106, 115);
            this.rTxtMessage.Margin = new System.Windows.Forms.Padding(2);
            this.rTxtMessage.Name = "rTxtMessage";
            this.rTxtMessage.Size = new System.Drawing.Size(362, 87);
            this.rTxtMessage.TabIndex = 4;
            this.rTxtMessage.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Message:";
            // 
            // dtpStatementDt
            // 
            this.dtpStatementDt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStatementDt.Location = new System.Drawing.Point(106, 20);
            this.dtpStatementDt.Margin = new System.Windows.Forms.Padding(2);
            this.dtpStatementDt.Name = "dtpStatementDt";
            this.dtpStatementDt.Size = new System.Drawing.Size(117, 20);
            this.dtpStatementDt.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Statement Date:";
            // 
            // dgEmailList
            // 
            this.dgEmailList.AllowUserToAddRows = false;
            this.dgEmailList.AllowUserToDeleteRows = false;
            this.dgEmailList.AllowUserToResizeColumns = false;
            this.dgEmailList.AllowUserToResizeRows = false;
            this.dgEmailList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgEmailList.BackgroundColor = System.Drawing.Color.White;
            this.dgEmailList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEmailList.Location = new System.Drawing.Point(21, 222);
            this.dgEmailList.Margin = new System.Windows.Forms.Padding(2);
            this.dgEmailList.MultiSelect = false;
            this.dgEmailList.Name = "dgEmailList";
            this.dgEmailList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgEmailList.RowHeadersVisible = false;
            this.dgEmailList.RowTemplate.Height = 24;
            this.dgEmailList.Size = new System.Drawing.Size(447, 189);
            this.dgEmailList.TabIndex = 8;
            this.dgEmailList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEmailList_CellContentClick);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend.Location = new System.Drawing.Point(392, 426);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(76, 26);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(21, 427);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 26);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EmailStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 463);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.dgEmailList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpStatementDt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rTxtMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmailAddr);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EmailStatement";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Statement";
            this.Load += new System.EventHandler(this.EmailStatement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgEmailList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmailAddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.RichTextBox rTxtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStatementDt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgEmailList;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;
    }
}