namespace RestaurantPOS.References
{
    partial class ReferralLookup
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
            this.dgridreferral = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgridreferral)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridreferral
            // 
            this.dgridreferral.AllowUserToAddRows = false;
            this.dgridreferral.AllowUserToDeleteRows = false;
            this.dgridreferral.AllowUserToOrderColumns = true;
            this.dgridreferral.AllowUserToResizeColumns = false;
            this.dgridreferral.AllowUserToResizeRows = false;
            this.dgridreferral.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridreferral.BackgroundColor = System.Drawing.Color.White;
            this.dgridreferral.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridreferral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgridreferral.Location = new System.Drawing.Point(0, 0);
            this.dgridreferral.Name = "dgridreferral";
            this.dgridreferral.Size = new System.Drawing.Size(384, 361);
            this.dgridreferral.TabIndex = 1;
            // 
            // ReferralLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.dgridreferral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReferralLookup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReferralLookup";
            this.Load += new System.EventHandler(this.ReferralLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridreferral)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridreferral;
    }
}