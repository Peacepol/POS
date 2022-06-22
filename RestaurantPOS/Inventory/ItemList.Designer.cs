namespace AbleRetailPOS.Inventory
{
    partial class ItemList
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
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgridItems = new System.Windows.Forms.DataGridView();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.txtCustomList3 = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtCustomList2 = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtCustomList1 = new System.Windows.Forms.TextBox();
            this.txtSuppPartNum = new System.Windows.Forms.TextBox();
            this.txtItemNum = new System.Windows.Forms.TextBox();
            this.txtPartNum = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.treeCategory = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.Image = global::AbleRetailPOS.Properties.Resources.Add24;
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNew.Location = new System.Drawing.Point(609, 117);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(106, 36);
            this.btnAddNew.TabIndex = 0;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(817, 117);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 36);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::AbleRetailPOS.Properties.Resources.Delete24;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(730, 117);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(76, 36);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgridItems
            // 
            this.dgridItems.AllowUserToAddRows = false;
            this.dgridItems.AllowUserToDeleteRows = false;
            this.dgridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridItems.BackgroundColor = System.Drawing.Color.White;
            this.dgridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgridItems.Location = new System.Drawing.Point(12, 160);
            this.dgridItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgridItems.MultiSelect = false;
            this.dgridItems.Name = "dgridItems";
            this.dgridItems.RowHeadersVisible = false;
            this.dgridItems.RowTemplate.Height = 24;
            this.dgridItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridItems.Size = new System.Drawing.Size(881, 379);
            this.dgridItems.TabIndex = 2;
            this.dgridItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellClick);
            this.dgridItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridItems_CellDoubleClick);
            this.dgridItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgridItems_CellFormatting);
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(710, 7);
            this.txtBrand.Margin = new System.Windows.Forms.Padding(2);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(181, 20);
            this.txtBrand.TabIndex = 38;
            this.txtBrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrand_KeyDown);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(99, 136);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(181, 20);
            this.txtSupplier.TabIndex = 36;
            this.txtSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplier_KeyDown);
            // 
            // txtCustomList3
            // 
            this.txtCustomList3.Location = new System.Drawing.Point(710, 85);
            this.txtCustomList3.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomList3.Name = "txtCustomList3";
            this.txtCustomList3.Size = new System.Drawing.Size(181, 20);
            this.txtCustomList3.TabIndex = 35;
            this.txtCustomList3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomList3_KeyDown);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(99, 88);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(181, 42);
            this.txtDesc.TabIndex = 33;
            this.txtDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDesc_KeyDown);
            // 
            // txtCustomList2
            // 
            this.txtCustomList2.Location = new System.Drawing.Point(710, 58);
            this.txtCustomList2.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomList2.Name = "txtCustomList2";
            this.txtCustomList2.Size = new System.Drawing.Size(181, 20);
            this.txtCustomList2.TabIndex = 32;
            this.txtCustomList2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomList2_KeyDown);
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(99, 58);
            this.txtItemName.Margin = new System.Windows.Forms.Padding(2);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(181, 20);
            this.txtItemName.TabIndex = 31;
            this.txtItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyDown);
            // 
            // txtCustomList1
            // 
            this.txtCustomList1.Location = new System.Drawing.Point(710, 31);
            this.txtCustomList1.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomList1.Name = "txtCustomList1";
            this.txtCustomList1.Size = new System.Drawing.Size(181, 20);
            this.txtCustomList1.TabIndex = 28;
            this.txtCustomList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomList1_KeyDown);
            // 
            // txtSuppPartNum
            // 
            this.txtSuppPartNum.Location = new System.Drawing.Point(411, 6);
            this.txtSuppPartNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtSuppPartNum.Name = "txtSuppPartNum";
            this.txtSuppPartNum.Size = new System.Drawing.Size(181, 20);
            this.txtSuppPartNum.TabIndex = 27;
            this.txtSuppPartNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSuppPartNum_KeyDown);
            // 
            // txtItemNum
            // 
            this.txtItemNum.Location = new System.Drawing.Point(99, 33);
            this.txtItemNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtItemNum.Name = "txtItemNum";
            this.txtItemNum.Size = new System.Drawing.Size(181, 20);
            this.txtItemNum.TabIndex = 37;
            this.txtItemNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemNum_KeyDown);
            // 
            // txtPartNum
            // 
            this.txtPartNum.Location = new System.Drawing.Point(99, 7);
            this.txtPartNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtPartNum.Name = "txtPartNum";
            this.txtPartNum.Size = new System.Drawing.Size(181, 20);
            this.txtPartNum.TabIndex = 26;
            this.txtPartNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNum_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(630, 88);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Custom List 3 :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(665, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Brand :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(630, 61);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Custom List 2 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 136);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Supplier :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 61);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Item Name :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(630, 34);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Custom List 1 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Description :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(287, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Supplier\'s Part Number :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 36);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Item Number :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Part No/Barcode:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 209;
            this.label3.Text = "Category :";
            // 
            // treeCategory
            // 
            this.treeCategory.CheckBoxes = true;
            this.treeCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeCategory.Location = new System.Drawing.Point(411, 36);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(182, 113);
            this.treeCategory.TabIndex = 208;
            this.treeCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterCheck);
            // 
            // ItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 550);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.treeCategory);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.txtCustomList3);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtCustomList2);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.txtCustomList1);
            this.Controls.Add(this.txtSuppPartNum);
            this.Controls.Add(this.txtItemNum);
            this.Controls.Add(this.txtPartNum);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgridItems);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAddNew);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "ItemList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item List";
            this.Load += new System.EventHandler(this.ItemList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgridItems;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.TextBox txtCustomList3;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtCustomList2;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtCustomList1;
        private System.Windows.Forms.TextBox txtSuppPartNum;
        private System.Windows.Forms.TextBox txtItemNum;
        private System.Windows.Forms.TextBox txtPartNum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeCategory;
    }
}