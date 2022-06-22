namespace CodeGenerator
{
    partial class frmCodeGenerator
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
            this.tabGrouping = new System.Windows.Forms.TabControl();
            this.tabActivate = new System.Windows.Forms.TabPage();
            this.rtxtActivationKey = new System.Windows.Forms.RichTextBox();
            this.btnGenerateActivation = new System.Windows.Forms.Button();
            this.txtRegNo = new System.Windows.Forms.TextBox();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.btnGenerateSerialReg = new System.Windows.Forms.Button();
            this.txtCompName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabReactivate = new System.Windows.Forms.TabPage();
            this.rtxtActKeyReact = new System.Windows.Forms.RichTextBox();
            this.btnGenerateReact = new System.Windows.Forms.Button();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.txtCompNameReact = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabGrouping.SuspendLayout();
            this.tabActivate.SuspendLayout();
            this.tabReactivate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabGrouping
            // 
            this.tabGrouping.Controls.Add(this.tabActivate);
            this.tabGrouping.Controls.Add(this.tabReactivate);
            this.tabGrouping.Location = new System.Drawing.Point(12, 12);
            this.tabGrouping.Name = "tabGrouping";
            this.tabGrouping.SelectedIndex = 0;
            this.tabGrouping.Size = new System.Drawing.Size(461, 341);
            this.tabGrouping.TabIndex = 0;
            // 
            // tabActivate
            // 
            this.tabActivate.Controls.Add(this.rtxtActivationKey);
            this.tabActivate.Controls.Add(this.btnGenerateActivation);
            this.tabActivate.Controls.Add(this.txtRegNo);
            this.tabActivate.Controls.Add(this.txtSerialNo);
            this.tabActivate.Controls.Add(this.btnGenerateSerialReg);
            this.tabActivate.Controls.Add(this.txtCompName);
            this.tabActivate.Controls.Add(this.label4);
            this.tabActivate.Controls.Add(this.label3);
            this.tabActivate.Controls.Add(this.label2);
            this.tabActivate.Controls.Add(this.label1);
            this.tabActivate.Location = new System.Drawing.Point(4, 22);
            this.tabActivate.Name = "tabActivate";
            this.tabActivate.Padding = new System.Windows.Forms.Padding(3);
            this.tabActivate.Size = new System.Drawing.Size(453, 315);
            this.tabActivate.TabIndex = 0;
            this.tabActivate.Text = "Activate";
            this.tabActivate.UseVisualStyleBackColor = true;
            // 
            // rtxtActivationKey
            // 
            this.rtxtActivationKey.Location = new System.Drawing.Point(157, 224);
            this.rtxtActivationKey.Name = "rtxtActivationKey";
            this.rtxtActivationKey.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxtActivationKey.Size = new System.Drawing.Size(236, 60);
            this.rtxtActivationKey.TabIndex = 16;
            this.rtxtActivationKey.Text = "";
            // 
            // btnGenerateActivation
            // 
            this.btnGenerateActivation.Location = new System.Drawing.Point(157, 195);
            this.btnGenerateActivation.Name = "btnGenerateActivation";
            this.btnGenerateActivation.Size = new System.Drawing.Size(63, 23);
            this.btnGenerateActivation.TabIndex = 15;
            this.btnGenerateActivation.Text = "generate";
            this.btnGenerateActivation.UseVisualStyleBackColor = true;
            this.btnGenerateActivation.Click += new System.EventHandler(this.btnGenerateActivation_Click);
            // 
            // txtRegNo
            // 
            this.txtRegNo.Location = new System.Drawing.Point(157, 140);
            this.txtRegNo.Name = "txtRegNo";
            this.txtRegNo.Size = new System.Drawing.Size(236, 20);
            this.txtRegNo.TabIndex = 14;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(157, 106);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(236, 20);
            this.txtSerialNo.TabIndex = 12;
            // 
            // btnGenerateSerialReg
            // 
            this.btnGenerateSerialReg.Location = new System.Drawing.Point(157, 77);
            this.btnGenerateSerialReg.Name = "btnGenerateSerialReg";
            this.btnGenerateSerialReg.Size = new System.Drawing.Size(63, 23);
            this.btnGenerateSerialReg.TabIndex = 10;
            this.btnGenerateSerialReg.Text = "generate";
            this.btnGenerateSerialReg.UseVisualStyleBackColor = true;
            this.btnGenerateSerialReg.Click += new System.EventHandler(this.btnGenerateSerialReg_Click);
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(157, 30);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Size = new System.Drawing.Size(236, 20);
            this.txtCompName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Activation Key:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Registration Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Serial Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Company Name:";
            // 
            // tabReactivate
            // 
            this.tabReactivate.Controls.Add(this.rtxtActKeyReact);
            this.tabReactivate.Controls.Add(this.btnGenerateReact);
            this.tabReactivate.Controls.Add(this.txtInvoiceNumber);
            this.tabReactivate.Controls.Add(this.txtCompNameReact);
            this.tabReactivate.Controls.Add(this.label5);
            this.tabReactivate.Controls.Add(this.label7);
            this.tabReactivate.Controls.Add(this.label8);
            this.tabReactivate.Location = new System.Drawing.Point(4, 22);
            this.tabReactivate.Name = "tabReactivate";
            this.tabReactivate.Padding = new System.Windows.Forms.Padding(3);
            this.tabReactivate.Size = new System.Drawing.Size(453, 315);
            this.tabReactivate.TabIndex = 1;
            this.tabReactivate.Text = "Reactivate";
            this.tabReactivate.UseVisualStyleBackColor = true;
            // 
            // rtxtActKeyReact
            // 
            this.rtxtActKeyReact.Location = new System.Drawing.Point(157, 151);
            this.rtxtActKeyReact.Name = "rtxtActKeyReact";
            this.rtxtActKeyReact.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxtActKeyReact.Size = new System.Drawing.Size(236, 60);
            this.rtxtActKeyReact.TabIndex = 26;
            this.rtxtActKeyReact.Text = "";
            // 
            // btnGenerateReact
            // 
            this.btnGenerateReact.Location = new System.Drawing.Point(157, 122);
            this.btnGenerateReact.Name = "btnGenerateReact";
            this.btnGenerateReact.Size = new System.Drawing.Size(63, 23);
            this.btnGenerateReact.TabIndex = 25;
            this.btnGenerateReact.Text = "generate";
            this.btnGenerateReact.UseVisualStyleBackColor = true;
            this.btnGenerateReact.Click += new System.EventHandler(this.btnGenerateReact_Click);
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(157, 67);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(236, 20);
            this.txtInvoiceNumber.TabIndex = 22;
            // 
            // txtCompNameReact
            // 
            this.txtCompNameReact.Location = new System.Drawing.Point(157, 30);
            this.txtCompNameReact.Name = "txtCompNameReact";
            this.txtCompNameReact.Size = new System.Drawing.Size(236, 20);
            this.txtCompNameReact.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Activation Key:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Invoice Number:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Company Name:";
            // 
            // frmCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 365);
            this.Controls.Add(this.tabGrouping);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCodeGenerator";
            this.Text = "Serial, Registration, Activation generator";
            this.Load += new System.EventHandler(this.frmCodeGenerator_Load);
            this.tabGrouping.ResumeLayout(false);
            this.tabActivate.ResumeLayout(false);
            this.tabActivate.PerformLayout();
            this.tabReactivate.ResumeLayout(false);
            this.tabReactivate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabGrouping;
        private System.Windows.Forms.TabPage tabActivate;
        private System.Windows.Forms.RichTextBox rtxtActivationKey;
        private System.Windows.Forms.Button btnGenerateActivation;
        private System.Windows.Forms.TextBox txtRegNo;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.Button btnGenerateSerialReg;
        private System.Windows.Forms.TextBox txtCompName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabReactivate;
        private System.Windows.Forms.RichTextBox rtxtActKeyReact;
        private System.Windows.Forms.Button btnGenerateReact;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.TextBox txtCompNameReact;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

