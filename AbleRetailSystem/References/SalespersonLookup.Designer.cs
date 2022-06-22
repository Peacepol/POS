namespace RestaurantPOS
{
    partial class SalespersonLookup
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
            this.dgriduser = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgriduser)).BeginInit();
            this.SuspendLayout();
            // 
            // dgriduser
            // 
            this.dgriduser.AllowUserToAddRows = false;
            this.dgriduser.AllowUserToDeleteRows = false;
            this.dgriduser.AllowUserToOrderColumns = true;
            this.dgriduser.AllowUserToResizeColumns = false;
            this.dgriduser.AllowUserToResizeRows = false;
            this.dgriduser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgriduser.BackgroundColor = System.Drawing.Color.White;
            this.dgriduser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgriduser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgriduser.Location = new System.Drawing.Point(0, 0);
            this.dgriduser.Name = "dgriduser";
            this.dgriduser.Size = new System.Drawing.Size(384, 361);
            this.dgriduser.TabIndex = 0;
            this.dgriduser.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgriduser_CellContentClick);
            // 
            // SalespersonLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.dgriduser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(275, 350);
            this.Name = "SalespersonLookup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salesperson Lookup";
            this.Load += new System.EventHandler(this.selectShippingMethodList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgriduser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgriduser;
    }
}