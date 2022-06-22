namespace ChangeCompanyNameRetail
{
    partial class frmChangeCompanyName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeCompanyName));
            this.txtCompName = new System.Windows.Forms.TextBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.dgvCompInfo = new System.Windows.Forms.DataGridView();
            this.CoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.btnFix = new System.Windows.Forms.Button();
            this.odgBrowseConfig = new System.Windows.Forms.OpenFileDialog();
            this.sdgConfigFile = new System.Windows.Forms.SaveFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.rTxtResults = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(557, 27);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.ReadOnly = true;
            this.txtCompName.Size = new System.Drawing.Size(366, 20);
            this.txtCompName.TabIndex = 0;
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(655, 53);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.ReadOnly = true;
            this.txtDbName.Size = new System.Drawing.Size(268, 20);
            this.txtDbName.TabIndex = 3;
            // 
            // dgvCompInfo
            // 
            this.dgvCompInfo.AllowUserToAddRows = false;
            this.dgvCompInfo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvCompInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCompInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CoName,
            this.SerialNo,
            this.RegCode,
            this.DBName,
            this.ServerAddress});
            this.dgvCompInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCompInfo.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvCompInfo.Location = new System.Drawing.Point(22, 38);
            this.dgvCompInfo.MultiSelect = false;
            this.dgvCompInfo.Name = "dgvCompInfo";
            this.dgvCompInfo.ReadOnly = true;
            this.dgvCompInfo.RowHeadersVisible = false;
            this.dgvCompInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCompInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompInfo.Size = new System.Drawing.Size(506, 147);
            this.dgvCompInfo.TabIndex = 5;
            this.dgvCompInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompInfo_CellClick);
            // 
            // CoName
            // 
            this.CoName.HeaderText = "Company Name";
            this.CoName.Name = "CoName";
            this.CoName.ReadOnly = true;
            // 
            // SerialNo
            // 
            this.SerialNo.HeaderText = "Serial Number";
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            // 
            // RegCode
            // 
            this.RegCode.HeaderText = "Registration No.";
            this.RegCode.Name = "RegCode";
            this.RegCode.ReadOnly = true;
            // 
            // DBName
            // 
            this.DBName.HeaderText = "Database Name";
            this.DBName.Name = "DBName";
            this.DBName.ReadOnly = true;
            // 
            // ServerAddress
            // 
            this.ServerAddress.HeaderText = "Server Name";
            this.ServerAddress.Name = "ServerAddress";
            this.ServerAddress.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(554, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Company Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(554, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database Name:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(22, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(58, 23);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtConfigFile
            // 
            this.txtConfigFile.Location = new System.Drawing.Point(86, 12);
            this.txtConfigFile.Name = "txtConfigFile";
            this.txtConfigFile.ReadOnly = true;
            this.txtConfigFile.Size = new System.Drawing.Size(442, 20);
            this.txtConfigFile.TabIndex = 14;
            // 
            // btnFix
            // 
            this.btnFix.Enabled = false;
            this.btnFix.Location = new System.Drawing.Point(839, 162);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(84, 23);
            this.btnFix.TabIndex = 15;
            this.btnFix.Text = "Fix";
            this.btnFix.UseVisualStyleBackColor = true;
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // odgBrowseConfig
            // 
            this.odgBrowseConfig.FileName = "odgBrowseConfig";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(554, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Enter Duplicate Invoice Number:";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Enabled = false;
            this.txtInvoiceNo.Location = new System.Drawing.Point(557, 164);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(268, 20);
            this.txtInvoiceNo.TabIndex = 16;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(655, 79);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.ReadOnly = true;
            this.txtServerName.Size = new System.Drawing.Size(268, 20);
            this.txtServerName.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(554, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Server Name:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(839, 105);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(84, 23);
            this.btnConnect.TabIndex = 18;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // rTxtResults
            // 
            this.rTxtResults.Location = new System.Drawing.Point(22, 215);
            this.rTxtResults.Name = "rTxtResults";
            this.rTxtResults.ReadOnly = true;
            this.rTxtResults.Size = new System.Drawing.Size(901, 387);
            this.rTxtResults.TabIndex = 19;
            this.rTxtResults.Text = "";
            // 
            // frmChangeCompanyName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 627);
            this.Controls.Add(this.rTxtResults);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtInvoiceNo);
            this.Controls.Add(this.btnFix);
            this.Controls.Add(this.txtConfigFile);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCompInfo);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.txtDbName);
            this.Controls.Add(this.txtCompName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmChangeCompanyName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fix Duplicate Starting AR Invoice Number";
            this.Load += new System.EventHandler(this.frmCoProEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCompName;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.DataGridView dgvCompInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.Button btnFix;
        private System.Windows.Forms.OpenFileDialog odgBrowseConfig;
        private System.Windows.Forms.SaveFileDialog sdgConfigFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.RichTextBox rTxtResults;
    }
}

