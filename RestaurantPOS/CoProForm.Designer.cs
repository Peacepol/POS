namespace AbleRetailPOS
{
    partial class CoProForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoProForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgridCo = new System.Windows.Forms.DataGridView();
            this.CompName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegistrationNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatabaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dbuser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dbpass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNewCompany = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridCo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgridCo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(644, 283);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Company File to Login";
            // 
            // dgridCo
            // 
            this.dgridCo.AllowUserToAddRows = false;
            this.dgridCo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgridCo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgridCo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridCo.BackgroundColor = System.Drawing.Color.White;
            this.dgridCo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridCo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompName,
            this.SerialNo,
            this.RegistrationNo,
            this.DatabaseName,
            this.ServerName,
            this.dbuser,
            this.dbpass});
            this.dgridCo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgridCo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridCo.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgridCo.Location = new System.Drawing.Point(3, 16);
            this.dgridCo.Name = "dgridCo";
            this.dgridCo.RowHeadersVisible = false;
            this.dgridCo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridCo.Size = new System.Drawing.Size(638, 264);
            this.dgridCo.TabIndex = 0;
            this.dgridCo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridCo_CellDoubleClick);
            // 
            // CompName
            // 
            this.CompName.HeaderText = "Company Name";
            this.CompName.Name = "CompName";
            // 
            // SerialNo
            // 
            this.SerialNo.HeaderText = "Serial Number";
            this.SerialNo.Name = "SerialNo";
            // 
            // RegistrationNo
            // 
            this.RegistrationNo.HeaderText = "Registration Number";
            this.RegistrationNo.Name = "RegistrationNo";
            // 
            // DatabaseName
            // 
            this.DatabaseName.HeaderText = "Database Name";
            this.DatabaseName.Name = "DatabaseName";
            // 
            // ServerName
            // 
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            // 
            // dbuser
            // 
            this.dbuser.HeaderText = "dbuser";
            this.dbuser.Name = "dbuser";
            this.dbuser.Visible = false;
            // 
            // dbpass
            // 
            this.dbpass.HeaderText = "dbpass";
            this.dbpass.Name = "dbpass";
            this.dbpass.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "No company data file yet? Click the button below to create a new company data.";
            // 
            // btnNewCompany
            // 
            this.btnNewCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnNewCompany.Image")));
            this.btnNewCompany.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewCompany.Location = new System.Drawing.Point(18, 331);
            this.btnNewCompany.Name = "btnNewCompany";
            this.btnNewCompany.Size = new System.Drawing.Size(202, 58);
            this.btnNewCompany.TabIndex = 0;
            this.btnNewCompany.Text = "Create  a New Company";
            this.btnNewCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewCompany.UseVisualStyleBackColor = true;
            this.btnNewCompany.Click += new System.EventHandler(this.btnNewCompany_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(554, 330);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(99, 58);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // CoProForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(668, 400);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnNewCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CoProForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Company Files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CoProForm_FormClosing);
            this.Load += new System.EventHandler(this.CoProForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgridCo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgridCo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegistrationNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatabaseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dbuser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dbpass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewCompany;
        private System.Windows.Forms.Button btnLogin;
    }
}