namespace AbleRetailPOS.Utilities
{
    partial class FormLookup
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
            this.dgvFormList = new System.Windows.Forms.DataGridView();
            this.FormCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFormSearh = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFormList
            // 
            this.dgvFormList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FormCode,
            this.FormName});
            this.dgvFormList.Location = new System.Drawing.Point(11, 38);
            this.dgvFormList.Name = "dgvFormList";
            this.dgvFormList.RowHeadersVisible = false;
            this.dgvFormList.Size = new System.Drawing.Size(401, 400);
            this.dgvFormList.TabIndex = 1;
            this.dgvFormList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormList_CellDoubleClick);
            // 
            // FormCode
            // 
            this.FormCode.HeaderText = "Form Code";
            this.FormCode.Name = "FormCode";
            this.FormCode.ReadOnly = true;
            this.FormCode.Width = 150;
            // 
            // FormName
            // 
            this.FormName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FormName.HeaderText = "Form Name";
            this.FormName.Name = "FormName";
            this.FormName.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Form Search:";
            // 
            // txtFormSearh
            // 
            this.txtFormSearh.Location = new System.Drawing.Point(287, 12);
            this.txtFormSearh.Name = "txtFormSearh";
            this.txtFormSearh.Size = new System.Drawing.Size(125, 20);
            this.txtFormSearh.TabIndex = 19;
            this.txtFormSearh.TextChanged += new System.EventHandler(this.txtFormSearh_TextChanged);
            // 
            // FormLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFormSearh);
            this.Controls.Add(this.dgvFormList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormLookup";
            this.Text = "Form Lookup";
            this.Load += new System.EventHandler(this.FormLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFormList;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFormSearh;
    }
}