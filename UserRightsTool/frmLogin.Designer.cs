namespace UserRightsTool
{
    partial class frmLogin
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
            this.grpLoginInfo = new System.Windows.Forms.GroupBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.cmbAuthentication = new System.Windows.Forms.ComboBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.btnCncel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpLoginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLoginInfo
            // 
            this.grpLoginInfo.Controls.Add(this.label59);
            this.grpLoginInfo.Controls.Add(this.label58);
            this.grpLoginInfo.Controls.Add(this.txtPassword);
            this.grpLoginInfo.Controls.Add(this.txtUsername);
            this.grpLoginInfo.Controls.Add(this.label50);
            this.grpLoginInfo.Controls.Add(this.cmbAuthentication);
            this.grpLoginInfo.Controls.Add(this.txtServerName);
            this.grpLoginInfo.Controls.Add(this.label46);
            this.grpLoginInfo.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLoginInfo.Location = new System.Drawing.Point(26, 26);
            this.grpLoginInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpLoginInfo.Name = "grpLoginInfo";
            this.grpLoginInfo.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpLoginInfo.Size = new System.Drawing.Size(390, 182);
            this.grpLoginInfo.TabIndex = 38;
            this.grpLoginInfo.TabStop = false;
            this.grpLoginInfo.Text = "Log-in information";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(50, 128);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(75, 17);
            this.label59.TabIndex = 45;
            this.label59.Text = "Password:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(74, 103);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(43, 17);
            this.label58.TabIndex = 44;
            this.label58.Text = "User:";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(148, 126);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(212, 24);
            this.txtPassword.TabIndex = 41;
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(148, 100);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(212, 24);
            this.txtUsername.TabIndex = 40;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(28, 73);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(108, 17);
            this.label50.TabIndex = 43;
            this.label50.Text = "Authentication:";
            // 
            // cmbAuthentication
            // 
            this.cmbAuthentication.FormattingEnabled = true;
            this.cmbAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "Username / Password"});
            this.cmbAuthentication.Location = new System.Drawing.Point(148, 71);
            this.cmbAuthentication.Name = "cmbAuthentication";
            this.cmbAuthentication.Size = new System.Drawing.Size(212, 25);
            this.cmbAuthentication.TabIndex = 39;
            this.cmbAuthentication.SelectedIndexChanged += new System.EventHandler(this.cmbAuthentication_SelectedIndexChanged);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(148, 43);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(212, 24);
            this.txtServerName.TabIndex = 38;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(34, 46);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(97, 17);
            this.label46.TabIndex = 42;
            this.label46.Text = "Server Name:";
            // 
            // btnCncel
            // 
            this.btnCncel.Location = new System.Drawing.Point(358, 226);
            this.btnCncel.Name = "btnCncel";
            this.btnCncel.Size = new System.Drawing.Size(58, 28);
            this.btnCncel.TabIndex = 39;
            this.btnCncel.Text = "Close";
            this.btnCncel.UseVisualStyleBackColor = true;
            this.btnCncel.Click += new System.EventHandler(this.btnCncel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(288, 226);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(58, 28);
            this.btnOk.TabIndex = 40;
            this.btnOk.Text = "Log-in";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 271);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCncel);
            this.Controls.Add(this.grpLoginInfo);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Log-in";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.grpLoginInfo.ResumeLayout(false);
            this.grpLoginInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLoginInfo;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox cmbAuthentication;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button btnCncel;
        private System.Windows.Forms.Button btnOk;
    }
}

