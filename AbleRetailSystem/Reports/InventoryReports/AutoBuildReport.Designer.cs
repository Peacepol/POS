namespace RestaurantPOS.Reports.InventoryReports
{
    partial class AutoBuildReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoBuildReport));
            this.label3 = new System.Windows.Forms.Label();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.txtList3 = new System.Windows.Forms.TextBox();
            this.txtList2 = new System.Windows.Forms.TextBox();
            this.txtList1 = new System.Windows.Forms.TextBox();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pbList3 = new System.Windows.Forms.PictureBox();
            this.pbList2 = new System.Windows.Forms.PictureBox();
            this.pbList1 = new System.Windows.Forms.PictureBox();
            this.pbSupplier = new System.Windows.Forms.PictureBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbList3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSupplier)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 226;
            this.label3.Text = "Category :";
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCategory.Location = new System.Drawing.Point(92, 171);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(224, 215);
            this.treeCategory.TabIndex = 225;
            this.treeCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 144);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 224;
            this.label2.Text = "Sort By :";
            // 
            // cmbSort
            // 
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            "Part Number",
            "Item Number"});
            this.cmbSort.Location = new System.Drawing.Point(92, 144);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(223, 21);
            this.cmbSort.TabIndex = 223;
            // 
            // txtList3
            // 
            this.txtList3.Location = new System.Drawing.Point(92, 80);
            this.txtList3.Margin = new System.Windows.Forms.Padding(2);
            this.txtList3.Name = "txtList3";
            this.txtList3.Size = new System.Drawing.Size(224, 20);
            this.txtList3.TabIndex = 219;
            this.txtList3.TextChanged += new System.EventHandler(this.txtList3_TextChanged);
            // 
            // txtList2
            // 
            this.txtList2.Location = new System.Drawing.Point(92, 53);
            this.txtList2.Margin = new System.Windows.Forms.Padding(2);
            this.txtList2.Name = "txtList2";
            this.txtList2.Size = new System.Drawing.Size(224, 20);
            this.txtList2.TabIndex = 218;
            this.txtList2.TextChanged += new System.EventHandler(this.txtList2_TextChanged);
            // 
            // txtList1
            // 
            this.txtList1.Location = new System.Drawing.Point(92, 26);
            this.txtList1.Margin = new System.Windows.Forms.Padding(2);
            this.txtList1.Name = "txtList1";
            this.txtList1.Size = new System.Drawing.Size(224, 20);
            this.txtList1.TabIndex = 217;
            this.txtList1.TextChanged += new System.EventHandler(this.txtList1_TextChanged);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(92, 104);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(223, 20);
            this.txtSupplier.TabIndex = 215;
            this.txtSupplier.TextChanged += new System.EventHandler(this.txtSupplier_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 214;
            this.label5.Text = "Supplier :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 83);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 213;
            this.label12.Text = "Custom List 3 :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 56);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 212;
            this.label11.Text = "Custom List 2 :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 29);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 211;
            this.label10.Text = "Custom List 1 :";
            // 
            // pbList3
            // 
            this.pbList3.Image = ((System.Drawing.Image)(resources.GetObject("pbList3.Image")));
            this.pbList3.Location = new System.Drawing.Point(325, 80);
            this.pbList3.Name = "pbList3";
            this.pbList3.Size = new System.Drawing.Size(19, 19);
            this.pbList3.TabIndex = 222;
            this.pbList3.TabStop = false;
            this.pbList3.Click += new System.EventHandler(this.pbList3_Click);
            // 
            // pbList2
            // 
            this.pbList2.Image = ((System.Drawing.Image)(resources.GetObject("pbList2.Image")));
            this.pbList2.Location = new System.Drawing.Point(325, 52);
            this.pbList2.Name = "pbList2";
            this.pbList2.Size = new System.Drawing.Size(19, 19);
            this.pbList2.TabIndex = 221;
            this.pbList2.TabStop = false;
            this.pbList2.Click += new System.EventHandler(this.pbList2_Click);
            // 
            // pbList1
            // 
            this.pbList1.Image = ((System.Drawing.Image)(resources.GetObject("pbList1.Image")));
            this.pbList1.Location = new System.Drawing.Point(325, 27);
            this.pbList1.Name = "pbList1";
            this.pbList1.Size = new System.Drawing.Size(19, 19);
            this.pbList1.TabIndex = 220;
            this.pbList1.TabStop = false;
            this.pbList1.Click += new System.EventHandler(this.pbList1_Click);
            // 
            // pbSupplier
            // 
            this.pbSupplier.Image = ((System.Drawing.Image)(resources.GetObject("pbSupplier.Image")));
            this.pbSupplier.Location = new System.Drawing.Point(325, 105);
            this.pbSupplier.Name = "pbSupplier";
            this.pbSupplier.Size = new System.Drawing.Size(19, 19);
            this.pbSupplier.TabIndex = 216;
            this.pbSupplier.TabStop = false;
            this.pbSupplier.Click += new System.EventHandler(this.pbSupplier_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerate.Image")));
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(222, 392);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(93, 40);
            this.btnGenerate.TabIndex = 210;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // AutoBuildReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 437);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.pbList3);
            this.Controls.Add(this.pbList2);
            this.Controls.Add(this.pbList1);
            this.Controls.Add(this.txtList3);
            this.Controls.Add(this.txtList2);
            this.Controls.Add(this.txtList1);
            this.Controls.Add(this.pbSupplier);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AutoBuildReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customizer - Auto Build Items";
            this.Load += new System.EventHandler(this.AutoBuildReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbList3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSupplier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.PictureBox pbList3;
        private System.Windows.Forms.PictureBox pbList2;
        private System.Windows.Forms.PictureBox pbList1;
        private System.Windows.Forms.TextBox txtList3;
        private System.Windows.Forms.TextBox txtList2;
        private System.Windows.Forms.TextBox txtList1;
        private System.Windows.Forms.PictureBox pbSupplier;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGenerate;
    }
}