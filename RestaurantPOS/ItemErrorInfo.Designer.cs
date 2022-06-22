namespace AbleRetailPOS
{
    partial class ItemErrorInfo
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
            this.dgridError = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgridError)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridError
            // 
            this.dgridError.AllowUserToAddRows = false;
            this.dgridError.AllowUserToDeleteRows = false;
            this.dgridError.AllowUserToResizeColumns = false;
            this.dgridError.AllowUserToResizeRows = false;
            this.dgridError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgridError.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridError.BackgroundColor = System.Drawing.Color.White;
            this.dgridError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridError.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridError.Location = new System.Drawing.Point(13, 12);
            this.dgridError.Name = "dgridError";
            this.dgridError.RowHeadersVisible = false;
            this.dgridError.Size = new System.Drawing.Size(462, 322);
            this.dgridError.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(407, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ItemErrorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 380);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgridError);
            this.Name = "ItemErrorInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Items Error Information";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemErrorInfo_FormClosing);
            this.Load += new System.EventHandler(this.ItemErrorInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridError;
        private System.Windows.Forms.Button btnOK;
    }
}