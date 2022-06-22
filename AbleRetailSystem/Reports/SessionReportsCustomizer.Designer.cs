namespace RestaurantPOS.Reports
{
    partial class SessionReportsCustomizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionReportsCustomizer));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoadSession = new System.Windows.Forms.Button();
            this.dtmTxTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmTxFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgSession = new System.Windows.Forms.DataGridView();
            this.chkSession = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SessionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SessionStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SessionEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbSessionType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSession)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadSession);
            this.groupBox1.Controls.Add(this.dtmTxTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtmTxFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 80);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transaction Date";
            // 
            // btnLoadSession
            // 
            this.btnLoadSession.Location = new System.Drawing.Point(314, 18);
            this.btnLoadSession.Name = "btnLoadSession";
            this.btnLoadSession.Size = new System.Drawing.Size(125, 46);
            this.btnLoadSession.TabIndex = 7;
            this.btnLoadSession.Text = "Load Session";
            this.btnLoadSession.UseVisualStyleBackColor = true;
            this.btnLoadSession.Click += new System.EventHandler(this.btnLoadSession_Click);
            // 
            // dtmTxTo
            // 
            this.dtmTxTo.Location = new System.Drawing.Point(72, 45);
            this.dtmTxTo.Name = "dtmTxTo";
            this.dtmTxTo.Size = new System.Drawing.Size(200, 20);
            this.dtmTxTo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "To: ";
            // 
            // dtmTxFrom
            // 
            this.dtmTxFrom.Location = new System.Drawing.Point(72, 19);
            this.dtmTxFrom.Name = "dtmTxFrom";
            this.dtmTxFrom.Size = new System.Drawing.Size(200, 20);
            this.dtmTxFrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "From: ";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerate.Image")));
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(364, 395);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(93, 40);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dgSession
            // 
            this.dgSession.AllowUserToAddRows = false;
            this.dgSession.AllowUserToDeleteRows = false;
            this.dgSession.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSession.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkSession,
            this.SessionID,
            this.Terminal,
            this.SessionStart,
            this.SessionEnd});
            this.dgSession.Location = new System.Drawing.Point(14, 125);
            this.dgSession.Name = "dgSession";
            this.dgSession.RowHeadersVisible = false;
            this.dgSession.Size = new System.Drawing.Size(444, 264);
            this.dgSession.TabIndex = 9;
            this.dgSession.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSession_CellContentClick);
            // 
            // chkSession
            // 
            this.chkSession.HeaderText = "";
            this.chkSession.Name = "chkSession";
            this.chkSession.Width = 30;
            // 
            // SessionID
            // 
            this.SessionID.HeaderText = "SessionID";
            this.SessionID.Name = "SessionID";
            // 
            // Terminal
            // 
            this.Terminal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Terminal.HeaderText = "Terminal";
            this.Terminal.Name = "Terminal";
            // 
            // SessionStart
            // 
            this.SessionStart.HeaderText = "Start Date";
            this.SessionStart.Name = "SessionStart";
            // 
            // SessionEnd
            // 
            this.SessionEnd.HeaderText = "End Date";
            this.SessionEnd.Name = "SessionEnd";
            // 
            // cmbSessionType
            // 
            this.cmbSessionType.FormattingEnabled = true;
            this.cmbSessionType.Items.AddRange(new object[] {
            "POS Session Summary Report",
            "List of Transactions By Payment Method",
            "List of Transaction By Entry Date",
            "List of A/R Payments"});
            this.cmbSessionType.Location = new System.Drawing.Point(94, 98);
            this.cmbSessionType.Name = "cmbSessionType";
            this.cmbSessionType.Size = new System.Drawing.Size(363, 21);
            this.cmbSessionType.TabIndex = 7;
            this.cmbSessionType.SelectedIndexChanged += new System.EventHandler(this.cmbSessionType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Report Type :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // SessionReportsCustomizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 447);
            this.Controls.Add(this.cmbSessionType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgSession);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SessionReportsCustomizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Session Reports - Customizer";
            this.Load += new System.EventHandler(this.SessionReportsCustomizer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSession)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtmTxTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtmTxFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dgSession;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSessionType;
        private System.Windows.Forms.Button btnLoadSession;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSession;
        private System.Windows.Forms.DataGridViewTextBoxColumn SessionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn SessionStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn SessionEnd;
    }
}