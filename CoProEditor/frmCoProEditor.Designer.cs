﻿namespace CoProEditor
{
    partial class frmCoProEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCoProEditor));
            this.txtCompName = new System.Windows.Forms.TextBox();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.txtRegNo = new System.Windows.Forms.TextBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.dgvCompInfo = new System.Windows.Forms.DataGridView();
            this.CoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.odgBrowseConfig = new System.Windows.Forms.OpenFileDialog();
            this.sdgConfigFile = new System.Windows.Forms.SaveFileDialog();
            this.lblAddNewEntry = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCompName
            // 
            this.txtCompName.Location = new System.Drawing.Point(655, 70);
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Size = new System.Drawing.Size(169, 20);
            this.txtCompName.TabIndex = 0;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(655, 109);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(169, 20);
            this.txtSerialNo.TabIndex = 1;
            // 
            // txtRegNo
            // 
            this.txtRegNo.Location = new System.Drawing.Point(655, 146);
            this.txtRegNo.Name = "txtRegNo";
            this.txtRegNo.Size = new System.Drawing.Size(169, 20);
            this.txtRegNo.TabIndex = 2;
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(655, 184);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(169, 20);
            this.txtDbName.TabIndex = 3;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(655, 224);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(169, 20);
            this.txtServerName.TabIndex = 4;
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
            this.dgvCompInfo.Location = new System.Drawing.Point(22, 64);
            this.dgvCompInfo.MultiSelect = false;
            this.dgvCompInfo.Name = "dgvCompInfo";
            this.dgvCompInfo.ReadOnly = true;
            this.dgvCompInfo.RowHeadersVisible = false;
            this.dgvCompInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCompInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompInfo.Size = new System.Drawing.Size(506, 299);
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
            this.label1.Location = new System.Drawing.Point(554, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Company Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(554, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Serial Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(554, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Registration No:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(554, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(554, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Server Name:";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(766, 22);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(58, 23);
            this.btnAddNew.TabIndex = 11;
            this.btnAddNew.Text = "Add new";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(745, 347);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(79, 23);
            this.btnSaveAs.TabIndex = 12;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(22, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(58, 23);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtConfigFile
            // 
            this.txtConfigFile.Location = new System.Drawing.Point(86, 26);
            this.txtConfigFile.Name = "txtConfigFile";
            this.txtConfigFile.ReadOnly = true;
            this.txtConfigFile.Size = new System.Drawing.Size(442, 20);
            this.txtConfigFile.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(655, 347);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // odgBrowseConfig
            // 
            this.odgBrowseConfig.FileName = "odgBrowseConfig";
            // 
            // lblAddNewEntry
            // 
            this.lblAddNewEntry.AutoSize = true;
            this.lblAddNewEntry.ForeColor = System.Drawing.Color.Crimson;
            this.lblAddNewEntry.Location = new System.Drawing.Point(654, 49);
            this.lblAddNewEntry.Name = "lblAddNewEntry";
            this.lblAddNewEntry.Size = new System.Drawing.Size(75, 13);
            this.lblAddNewEntry.TabIndex = 16;
            this.lblAddNewEntry.Text = "Add new entry";
            this.lblAddNewEntry.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(702, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(58, 23);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmCoProEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 386);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblAddNewEntry);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfigFile);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCompInfo);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.txtDbName);
            this.Controls.Add(this.txtRegNo);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.txtCompName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCoProEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoPro Editor";
            this.Load += new System.EventHandler(this.frmCoProEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCompName;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.TextBox txtRegNo;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.DataGridView dgvCompInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog odgBrowseConfig;
        private System.Windows.Forms.SaveFileDialog sdgConfigFile;
        private System.Windows.Forms.Label lblAddNewEntry;
        private System.Windows.Forms.Button btnDelete;
    }
}

