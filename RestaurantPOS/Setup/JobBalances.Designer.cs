namespace AbleRetailPOS
{
    partial class JobBalances
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobBalances));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgridBalance = new System.Windows.Forms.DataGridView();
            this.btnRecord = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJobs = new System.Windows.Forms.TextBox();
            this.pbJob = new System.Windows.Forms.PictureBox();
            this.lblJobID = new System.Windows.Forms.Label();
            this.accttype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acctno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acctname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acctid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgridBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJob)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridBalance
            // 
            this.dgridBalance.AllowUserToAddRows = false;
            this.dgridBalance.AllowUserToDeleteRows = false;
            this.dgridBalance.AllowUserToOrderColumns = true;
            this.dgridBalance.AllowUserToResizeColumns = false;
            this.dgridBalance.AllowUserToResizeRows = false;
            this.dgridBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgridBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridBalance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.accttype,
            this.acctno,
            this.acctname,
            this.balance,
            this.acctid,
            this.ACID});
            this.dgridBalance.Location = new System.Drawing.Point(13, 65);
            this.dgridBalance.Name = "dgridBalance";
            this.dgridBalance.RowHeadersVisible = false;
            this.dgridBalance.Size = new System.Drawing.Size(585, 412);
            this.dgridBalance.TabIndex = 1;
            this.dgridBalance.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridBalance_CellEndEdit);
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecord.Location = new System.Drawing.Point(498, 493);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(98, 41);
            this.btnRecord.TabIndex = 2;
            this.btnRecord.Text = "Record";
            this.btnRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 172;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 173;
            this.label2.Text = "Job:";
            // 
            // txtJobs
            // 
            this.txtJobs.Location = new System.Drawing.Point(58, 26);
            this.txtJobs.Name = "txtJobs";
            this.txtJobs.ReadOnly = true;
            this.txtJobs.Size = new System.Drawing.Size(457, 20);
            this.txtJobs.TabIndex = 0;
            // 
            // pbJob
            // 
            this.pbJob.Image = ((System.Drawing.Image)(resources.GetObject("pbJob.Image")));
            this.pbJob.Location = new System.Drawing.Point(521, 26);
            this.pbJob.Name = "pbJob";
            this.pbJob.Size = new System.Drawing.Size(19, 19);
            this.pbJob.TabIndex = 180;
            this.pbJob.TabStop = false;
            this.pbJob.Click += new System.EventHandler(this.pbJob_Click);
            // 
            // lblJobID
            // 
            this.lblJobID.AutoSize = true;
            this.lblJobID.Location = new System.Drawing.Point(58, 9);
            this.lblJobID.Name = "lblJobID";
            this.lblJobID.Size = new System.Drawing.Size(0, 13);
            this.lblJobID.TabIndex = 181;
            // 
            // accttype
            // 
            this.accttype.Frozen = true;
            this.accttype.HeaderText = "";
            this.accttype.Name = "accttype";
            // 
            // acctno
            // 
            this.acctno.FillWeight = 90F;
            this.acctno.Frozen = true;
            this.acctno.HeaderText = "Profile ID Number";
            this.acctno.Name = "acctno";
            this.acctno.Width = 150;
            // 
            // acctname
            // 
            this.acctname.HeaderText = "Job Name";
            this.acctname.Name = "acctname";
            this.acctname.Width = 220;
            // 
            // balance
            // 
            this.balance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.balance.DefaultCellStyle = dataGridViewCellStyle1;
            this.balance.HeaderText = "Opening Balance";
            this.balance.Name = "balance";
            // 
            // acctid
            // 
            this.acctid.HeaderText = "Account ID";
            this.acctid.Name = "acctid";
            this.acctid.Visible = false;
            this.acctid.Width = 5;
            // 
            // ACID
            // 
            this.ACID.HeaderText = "ACID";
            this.ACID.Name = "ACID";
            this.ACID.Visible = false;
            this.ACID.Width = 5;
            // 
            // JobBalances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 562);
            this.Controls.Add(this.lblJobID);
            this.Controls.Add(this.pbJob);
            this.Controls.Add(this.txtJobs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.dgridBalance);
            this.MaximizeBox = false;
            this.Name = "JobBalances";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Job Balances";
            this.Load += new System.EventHandler(this.JobBalances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJob)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridBalance;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJobs;
        private System.Windows.Forms.PictureBox pbJob;
        private System.Windows.Forms.Label lblJobID;
        private System.Windows.Forms.DataGridViewTextBoxColumn accttype;
        private System.Windows.Forms.DataGridViewTextBoxColumn acctno;
        private System.Windows.Forms.DataGridViewTextBoxColumn acctname;
        private System.Windows.Forms.DataGridViewTextBoxColumn balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn acctid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACID;
    }
}