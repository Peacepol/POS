namespace RestaurantPOS
{
    partial class APBalances
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APBalances));
            this.dgridSuppliers = new System.Windows.Forms.DataGridView();
            this.ProfileID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfileNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddPurchase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgridSuppliers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridSuppliers
            // 
            this.dgridSuppliers.AllowUserToAddRows = false;
            this.dgridSuppliers.AllowUserToDeleteRows = false;
            this.dgridSuppliers.AllowUserToOrderColumns = true;
            this.dgridSuppliers.AllowUserToResizeColumns = false;
            this.dgridSuppliers.AllowUserToResizeRows = false;
            this.dgridSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgridSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridSuppliers.BackgroundColor = System.Drawing.Color.White;
            this.dgridSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridSuppliers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProfileID,
            this.ProfileNum,
            this.ProfileName,
            this.CurrentBalance});
            this.dgridSuppliers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridSuppliers.GridColor = System.Drawing.Color.White;
            this.dgridSuppliers.Location = new System.Drawing.Point(9, 10);
            this.dgridSuppliers.Margin = new System.Windows.Forms.Padding(2);
            this.dgridSuppliers.Name = "dgridSuppliers";
            this.dgridSuppliers.RowHeadersVisible = false;
            this.dgridSuppliers.RowTemplate.Height = 24;
            this.dgridSuppliers.Size = new System.Drawing.Size(644, 367);
            this.dgridSuppliers.TabIndex = 0;
            // 
            // ProfileID
            // 
            this.ProfileID.HeaderText = "ID";
            this.ProfileID.Name = "ProfileID";
            this.ProfileID.ReadOnly = true;
            // 
            // ProfileNum
            // 
            this.ProfileNum.HeaderText = "Customer Number";
            this.ProfileNum.Name = "ProfileNum";
            this.ProfileNum.ReadOnly = true;
            // 
            // ProfileName
            // 
            this.ProfileName.HeaderText = "Name";
            this.ProfileName.Name = "ProfileName";
            this.ProfileName.ReadOnly = true;
            // 
            // CurrentBalance
            // 
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = "0";
            this.CurrentBalance.DefaultCellStyle = dataGridViewCellStyle1;
            this.CurrentBalance.HeaderText = "Current Balance";
            this.CurrentBalance.Name = "CurrentBalance";
            this.CurrentBalance.ReadOnly = true;
            // 
            // btnAddPurchase
            // 
            this.btnAddPurchase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddPurchase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPurchase.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPurchase.Image")));
            this.btnAddPurchase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddPurchase.Location = new System.Drawing.Point(511, 383);
            this.btnAddPurchase.Name = "btnAddPurchase";
            this.btnAddPurchase.Size = new System.Drawing.Size(141, 32);
            this.btnAddPurchase.TabIndex = 5;
            this.btnAddPurchase.Text = "ADD PURCHASE";
            this.btnAddPurchase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddPurchase.UseVisualStyleBackColor = true;
            this.btnAddPurchase.Click += new System.EventHandler(this.btnAddSale_Click);
            // 
            // APBalances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 431);
            this.Controls.Add(this.btnAddPurchase);
            this.Controls.Add(this.dgridSuppliers);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "APBalances";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accounts Payable Starting Balances";
            this.Load += new System.EventHandler(this.APBalances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridSuppliers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridSuppliers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentBalance;
        private System.Windows.Forms.Button btnAddPurchase;
    }
}