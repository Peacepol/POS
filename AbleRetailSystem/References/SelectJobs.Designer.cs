namespace RestaurantPOS
{
    partial class SelectJobs
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.JobID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.JobCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.JobName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FinishDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.JobSearch_tb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JobID,
            this.JobCode,
            this.JobName,
            this.StartDate,
            this.FinishDate});
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(16, 49);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(844, 376);
            this.listView1.TabIndex = 168;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView1_ColumnWidthchanging);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // JobID
            // 
            this.JobID.Text = "JobID";
            this.JobID.Width = 100;
            // 
            // JobCode
            // 
            this.JobCode.Text = "Job Code";
            this.JobCode.Width = 100;
            // 
            // JobName
            // 
            this.JobName.Text = "Job Name";
            this.JobName.Width = 200;
            // 
            // StartDate
            // 
            this.StartDate.Text = "Start Date";
            this.StartDate.Width = 100;
            // 
            // FinishDate
            // 
            this.FinishDate.Text = "Finish Date";
            this.FinishDate.Width = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 169;
            this.label1.Text = "Look for :";
            // 
            // JobSearch_tb
            // 
            this.JobSearch_tb.Location = new System.Drawing.Point(161, 14);
            this.JobSearch_tb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.JobSearch_tb.Name = "JobSearch_tb";
            this.JobSearch_tb.Size = new System.Drawing.Size(452, 22);
            this.JobSearch_tb.TabIndex = 170;
            this.JobSearch_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.JobSearch_tb_KeyPress);
            // 
            // SelectJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(875, 433);
            this.Controls.Add(this.JobSearch_tb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(893, 480);
            this.MinimumSize = new System.Drawing.Size(893, 480);
            this.Name = "SelectJobs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Jobs";
            this.Load += new System.EventHandler(this.SelectJobs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader JobID;
        private System.Windows.Forms.ColumnHeader JobCode;
        private System.Windows.Forms.ColumnHeader JobName;
        private System.Windows.Forms.ColumnHeader StartDate;
        private System.Windows.Forms.ColumnHeader FinishDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox JobSearch_tb;
    }
}