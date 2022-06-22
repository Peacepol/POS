namespace UserRightsTool
{
    partial class frmMain
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
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.lblDb = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCncel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDbName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.Location = new System.Drawing.Point(170, 28);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(222, 24);
            this.cmbDatabase.TabIndex = 0;
            this.cmbDatabase.SelectedIndexChanged += new System.EventHandler(this.cmbDatabase_SelectedIndexChanged);
            // 
            // lblDb
            // 
            this.lblDb.AutoSize = true;
            this.lblDb.Font = new System.Drawing.Font("Arial Black", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDb.Location = new System.Drawing.Point(31, 25);
            this.lblDb.Name = "lblDb";
            this.lblDb.Size = new System.Drawing.Size(102, 24);
            this.lblDb.TabIndex = 1;
            this.lblDb.Text = "Database:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(218, 164);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 34);
            this.btnOk.TabIndex = 42;
            this.btnOk.Text = "Apply";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCncel
            // 
            this.btnCncel.Location = new System.Drawing.Point(314, 164);
            this.btnCncel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCncel.Name = "btnCncel";
            this.btnCncel.Size = new System.Drawing.Size(78, 34);
            this.btnCncel.TabIndex = 41;
            this.btnCncel.Text = "Close";
            this.btnCncel.UseVisualStyleBackColor = true;
            this.btnCncel.Click += new System.EventHandler(this.btnCncel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 24);
            this.label1.TabIndex = 43;
            this.label1.Text = "Apply user restrictions to DB";
            // 
            // lblDbName
            // 
            this.lblDbName.AutoSize = true;
            this.lblDbName.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDbName.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblDbName.Location = new System.Drawing.Point(31, 123);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(91, 24);
            this.lblDbName.TabIndex = 44;
            this.lblDbName.Text = "DBNAME";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 211);
            this.Controls.Add(this.lblDbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCncel);
            this.Controls.Add(this.lblDb);
            this.Controls.Add(this.cmbDatabase);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Apply user rights to DB";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDatabase;
        private System.Windows.Forms.Label lblDb;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCncel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDbName;
    }
}