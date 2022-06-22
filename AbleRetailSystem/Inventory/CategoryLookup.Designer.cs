namespace RestaurantPOS.Inventory
{
    partial class CategoryLookup
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
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeCategory
            // 
            this.treeCategory.Location = new System.Drawing.Point(21, 12);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(234, 463);
            this.treeCategory.TabIndex = 22;
            this.treeCategory.DoubleClick += new System.EventHandler(this.treeCategory_DoubleClick);
            // 
            // CategoryLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 487);
            this.Controls.Add(this.treeCategory);
            this.Name = "CategoryLookup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Category Lookup";
            this.Load += new System.EventHandler(this.CategoryLookup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeCategory;
    }
}