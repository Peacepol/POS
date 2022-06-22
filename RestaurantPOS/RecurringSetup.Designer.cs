namespace AbleRetailPOS
{
    partial class RecurringSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecurringSetup));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTransactionName = new System.Windows.Forms.TextBox();
            this.gboxSchedule = new System.Windows.Forms.GroupBox();
            this.dtpEdate = new System.Windows.Forms.DateTimePicker();
            this.txtNoOfTimes = new System.Windows.Forms.TextBox();
            this.dtpStartOn = new System.Windows.Forms.DateTimePicker();
            this.rdoNumberOfTimes = new System.Windows.Forms.RadioButton();
            this.rdoUntilDate = new System.Windows.Forms.RadioButton();
            this.rdoIndefinitely = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFrequency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gboxAlerts = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDaysInAdvance = new System.Windows.Forms.TextBox();
            this.pbNotifyUser = new System.Windows.Forms.PictureBox();
            this.pbAccount = new System.Windows.Forms.PictureBox();
            this.txtNotifyUser = new System.Windows.Forms.TextBox();
            this.rdoDueAndNotify = new System.Windows.Forms.RadioButton();
            this.cmbRecTrans = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRemindUser = new System.Windows.Forms.TextBox();
            this.rdoRemind = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gboxSchedule.SuspendLayout();
            this.gboxAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotifyUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction Name:";
            // 
            // txtTransactionName
            // 
            this.txtTransactionName.Location = new System.Drawing.Point(220, 20);
            this.txtTransactionName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTransactionName.Name = "txtTransactionName";
            this.txtTransactionName.ReadOnly = true;
            this.txtTransactionName.Size = new System.Drawing.Size(149, 20);
            this.txtTransactionName.TabIndex = 1;
            // 
            // gboxSchedule
            // 
            this.gboxSchedule.Controls.Add(this.dtpEdate);
            this.gboxSchedule.Controls.Add(this.txtNoOfTimes);
            this.gboxSchedule.Controls.Add(this.dtpStartOn);
            this.gboxSchedule.Controls.Add(this.rdoNumberOfTimes);
            this.gboxSchedule.Controls.Add(this.rdoUntilDate);
            this.gboxSchedule.Controls.Add(this.rdoIndefinitely);
            this.gboxSchedule.Controls.Add(this.label3);
            this.gboxSchedule.Controls.Add(this.cmbFrequency);
            this.gboxSchedule.Controls.Add(this.label2);
            this.gboxSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxSchedule.Location = new System.Drawing.Point(26, 61);
            this.gboxSchedule.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gboxSchedule.Name = "gboxSchedule";
            this.gboxSchedule.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gboxSchedule.Size = new System.Drawing.Size(500, 154);
            this.gboxSchedule.TabIndex = 3;
            this.gboxSchedule.TabStop = false;
            this.gboxSchedule.Text = "Frequency Parameters";
            // 
            // dtpEdate
            // 
            this.dtpEdate.Enabled = false;
            this.dtpEdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEdate.Location = new System.Drawing.Point(254, 97);
            this.dtpEdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpEdate.Name = "dtpEdate";
            this.dtpEdate.Size = new System.Drawing.Size(89, 19);
            this.dtpEdate.TabIndex = 10;
            // 
            // txtNoOfTimes
            // 
            this.txtNoOfTimes.Enabled = false;
            this.txtNoOfTimes.Location = new System.Drawing.Point(254, 122);
            this.txtNoOfTimes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNoOfTimes.Name = "txtNoOfTimes";
            this.txtNoOfTimes.Size = new System.Drawing.Size(56, 19);
            this.txtNoOfTimes.TabIndex = 9;
            // 
            // dtpStartOn
            // 
            this.dtpStartOn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartOn.Location = new System.Drawing.Point(314, 28);
            this.dtpStartOn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpStartOn.Name = "dtpStartOn";
            this.dtpStartOn.Size = new System.Drawing.Size(104, 19);
            this.dtpStartOn.TabIndex = 8;
            // 
            // rdoNumberOfTimes
            // 
            this.rdoNumberOfTimes.AutoSize = true;
            this.rdoNumberOfTimes.Location = new System.Drawing.Point(110, 122);
            this.rdoNumberOfTimes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdoNumberOfTimes.Name = "rdoNumberOfTimes";
            this.rdoNumberOfTimes.Size = new System.Drawing.Size(129, 17);
            this.rdoNumberOfTimes.TabIndex = 7;
            this.rdoNumberOfTimes.TabStop = true;
            this.rdoNumberOfTimes.Text = "Perform this # of times";
            this.rdoNumberOfTimes.UseVisualStyleBackColor = true;
            this.rdoNumberOfTimes.CheckedChanged += new System.EventHandler(this.rdoNumberOfTimes_CheckedChanged);
            // 
            // rdoUntilDate
            // 
            this.rdoUntilDate.AutoSize = true;
            this.rdoUntilDate.Location = new System.Drawing.Point(110, 97);
            this.rdoUntilDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdoUntilDate.Name = "rdoUntilDate";
            this.rdoUntilDate.Size = new System.Drawing.Size(132, 17);
            this.rdoUntilDate.TabIndex = 6;
            this.rdoUntilDate.TabStop = true;
            this.rdoUntilDate.Text = "Continue until this date";
            this.rdoUntilDate.UseVisualStyleBackColor = true;
            this.rdoUntilDate.CheckedChanged += new System.EventHandler(this.rdoUntilDate_CheckedChanged);
            // 
            // rdoIndefinitely
            // 
            this.rdoIndefinitely.AutoSize = true;
            this.rdoIndefinitely.Location = new System.Drawing.Point(110, 72);
            this.rdoIndefinitely.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdoIndefinitely.Name = "rdoIndefinitely";
            this.rdoIndefinitely.Size = new System.Drawing.Size(119, 17);
            this.rdoIndefinitely.TabIndex = 5;
            this.rdoIndefinitely.TabStop = true;
            this.rdoIndefinitely.Text = "Continue indefinitely";
            this.rdoIndefinitely.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Starting On:";
            // 
            // cmbFrequency
            // 
            this.cmbFrequency.FormattingEnabled = true;
            this.cmbFrequency.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly",
            "Quarterly",
            "Every 6 Months"});
            this.cmbFrequency.Location = new System.Drawing.Point(110, 30);
            this.cmbFrequency.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.Size = new System.Drawing.Size(116, 21);
            this.cmbFrequency.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Frequency:";
            // 
            // gboxAlerts
            // 
            this.gboxAlerts.Controls.Add(this.label5);
            this.gboxAlerts.Controls.Add(this.txtDaysInAdvance);
            this.gboxAlerts.Controls.Add(this.pbNotifyUser);
            this.gboxAlerts.Controls.Add(this.pbAccount);
            this.gboxAlerts.Controls.Add(this.txtNotifyUser);
            this.gboxAlerts.Controls.Add(this.rdoDueAndNotify);
            this.gboxAlerts.Controls.Add(this.cmbRecTrans);
            this.gboxAlerts.Controls.Add(this.label4);
            this.gboxAlerts.Controls.Add(this.txtRemindUser);
            this.gboxAlerts.Controls.Add(this.rdoRemind);
            this.gboxAlerts.Location = new System.Drawing.Point(26, 235);
            this.gboxAlerts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gboxAlerts.Name = "gboxAlerts";
            this.gboxAlerts.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gboxAlerts.Size = new System.Drawing.Size(500, 126);
            this.gboxAlerts.TabIndex = 4;
            this.gboxAlerts.TabStop = false;
            this.gboxAlerts.Text = "Alerts";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 56);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 198;
            this.label5.Text = "No. of days";
            // 
            // txtDaysInAdvance
            // 
            this.txtDaysInAdvance.Enabled = false;
            this.txtDaysInAdvance.Location = new System.Drawing.Point(418, 54);
            this.txtDaysInAdvance.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDaysInAdvance.Name = "txtDaysInAdvance";
            this.txtDaysInAdvance.Size = new System.Drawing.Size(56, 20);
            this.txtDaysInAdvance.TabIndex = 197;
            // 
            // pbNotifyUser
            // 
            this.pbNotifyUser.Image = ((System.Drawing.Image)(resources.GetObject("pbNotifyUser.Image")));
            this.pbNotifyUser.Location = new System.Drawing.Point(455, 85);
            this.pbNotifyUser.Name = "pbNotifyUser";
            this.pbNotifyUser.Size = new System.Drawing.Size(18, 19);
            this.pbNotifyUser.TabIndex = 196;
            this.pbNotifyUser.TabStop = false;
            this.pbNotifyUser.Click += new System.EventHandler(this.pbNotifyUser_Click);
            // 
            // pbAccount
            // 
            this.pbAccount.Image = ((System.Drawing.Image)(resources.GetObject("pbAccount.Image")));
            this.pbAccount.Location = new System.Drawing.Point(202, 32);
            this.pbAccount.Name = "pbAccount";
            this.pbAccount.Size = new System.Drawing.Size(18, 19);
            this.pbAccount.TabIndex = 195;
            this.pbAccount.TabStop = false;
            this.pbAccount.Click += new System.EventHandler(this.pbAccount_Click);
            // 
            // txtNotifyUser
            // 
            this.txtNotifyUser.Enabled = false;
            this.txtNotifyUser.Location = new System.Drawing.Point(345, 85);
            this.txtNotifyUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNotifyUser.Name = "txtNotifyUser";
            this.txtNotifyUser.Size = new System.Drawing.Size(106, 20);
            this.txtNotifyUser.TabIndex = 6;
            // 
            // rdoDueAndNotify
            // 
            this.rdoDueAndNotify.AutoSize = true;
            this.rdoDueAndNotify.Location = new System.Drawing.Point(30, 85);
            this.rdoDueAndNotify.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdoDueAndNotify.Name = "rdoDueAndNotify";
            this.rdoDueAndNotify.Size = new System.Drawing.Size(296, 17);
            this.rdoDueAndNotify.TabIndex = 5;
            this.rdoDueAndNotify.TabStop = true;
            this.rdoDueAndNotify.Text = " Automatically record this transaction when due and notify";
            this.rdoDueAndNotify.UseVisualStyleBackColor = true;
            this.rdoDueAndNotify.CheckedChanged += new System.EventHandler(this.rdoDueAndNotify_CheckedChanged);
            // 
            // cmbRecTrans
            // 
            this.cmbRecTrans.Enabled = false;
            this.cmbRecTrans.FormattingEnabled = true;
            this.cmbRecTrans.Items.AddRange(new object[] {
            "never",
            "on its due date",
            "#days in advance"});
            this.cmbRecTrans.Location = new System.Drawing.Point(364, 29);
            this.cmbRecTrans.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbRecTrans.Name = "cmbRecTrans";
            this.cmbRecTrans.Size = new System.Drawing.Size(110, 21);
            this.cmbRecTrans.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "To record this transaction";
            // 
            // txtRemindUser
            // 
            this.txtRemindUser.Enabled = false;
            this.txtRemindUser.Location = new System.Drawing.Point(92, 31);
            this.txtRemindUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRemindUser.Name = "txtRemindUser";
            this.txtRemindUser.Size = new System.Drawing.Size(106, 20);
            this.txtRemindUser.TabIndex = 2;
            // 
            // rdoRemind
            // 
            this.rdoRemind.AutoSize = true;
            this.rdoRemind.Location = new System.Drawing.Point(30, 32);
            this.rdoRemind.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rdoRemind.Name = "rdoRemind";
            this.rdoRemind.Size = new System.Drawing.Size(61, 17);
            this.rdoRemind.TabIndex = 0;
            this.rdoRemind.TabStop = true;
            this.rdoRemind.Text = "Remind";
            this.rdoRemind.UseVisualStyleBackColor = true;
            this.rdoRemind.CheckedChanged += new System.EventHandler(this.rdoRemind_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(461, 379);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(392, 379);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 26);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // RecurringSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 422);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gboxAlerts);
            this.Controls.Add(this.gboxSchedule);
            this.Controls.Add(this.txtTransactionName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RecurringSetup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Recurring Transaction";
            this.Load += new System.EventHandler(this.RecurringSetup_Load);
            this.gboxSchedule.ResumeLayout(false);
            this.gboxSchedule.PerformLayout();
            this.gboxAlerts.ResumeLayout(false);
            this.gboxAlerts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotifyUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAccount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTransactionName;
        private System.Windows.Forms.GroupBox gboxSchedule;
        private System.Windows.Forms.GroupBox gboxAlerts;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpStartOn;
        private System.Windows.Forms.RadioButton rdoNumberOfTimes;
        private System.Windows.Forms.RadioButton rdoUntilDate;
        private System.Windows.Forms.RadioButton rdoIndefinitely;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFrequency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNotifyUser;
        private System.Windows.Forms.RadioButton rdoDueAndNotify;
        private System.Windows.Forms.ComboBox cmbRecTrans;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRemindUser;
        private System.Windows.Forms.RadioButton rdoRemind;
        private System.Windows.Forms.PictureBox pbNotifyUser;
        private System.Windows.Forms.PictureBox pbAccount;
        private System.Windows.Forms.TextBox txtNoOfTimes;
        private System.Windows.Forms.DateTimePicker dtpEdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDaysInAdvance;
    }
}