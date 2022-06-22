namespace RestaurantPOS
{
    partial class ARBalances
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ARBalances));
            this.dgridCustomers = new System.Windows.Forms.DataGridView();
            this.ProfileID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfileNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddSale = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgridCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridCustomers
            // 
            this.dgridCustomers.AllowUserToAddRows = false;
            this.dgridCustomers.AllowUserToDeleteRows = false;
            this.dgridCustomers.AllowUserToOrderColumns = true;
            this.dgridCustomers.AllowUserToResizeColumns = false;
            this.dgridCustomers.AllowUserToResizeRows = false;
            this.dgridCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgridCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgridCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProfileID,
            this.ProfileNum,
            this.ProfileName,
            this.CurrentBalance});
            this.dgridCustomers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridCustomers.GridColor = System.Drawing.Color.White;
            this.dgridCustomers.Location = new System.Drawing.Point(9, 10);
            this.dgridCustomers.Margin = new System.Windows.Forms.Padding(2);
            this.dgridCustomers.Name = "dgridCustomers";
            this.dgridCustomers.RowHeadersVisible = false;
            this.dgridCustomers.RowTemplate.Height = 24;
            this.dgridCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridCustomers.Size = new System.Drawing.Size(644, 367);
            this.dgridCustomers.TabIndex = 0;
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
            // btnAddSale
            // 
            this.btnAddSale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSale.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSale.Image")));
            this.btnAddSale.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddSale.Location = new System.Drawing.Point(549, 383);
            this.btnAddSale.Name = "btnAddSale";
            this.btnAddSale.Size = new System.Drawing.Size(103, 32);
            this.btnAddSale.TabIndex = 5;
            this.btnAddSale.Text = "ADD SALE";
            this.btnAddSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddSale.UseVisualStyleBackColor = true;
            this.btnAddSale.Click += new System.EventHandler(this.btnAddSale_Click);
            // 
            // ARBalances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 431);
            this.Controls.Add(this.btnAddSale);
            this.Controls.Add(this.dgridCustomers);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ARBalances";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accounts Receivable Starting Balances";
            this.Load += new System.EventHandler(this.ARBalances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridCustomers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentBalance;
        private System.Windows.Forms.Button btnAddSale;
    }
}