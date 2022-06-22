namespace RestaurantPOS.Inventory
{
    partial class Categories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Categories));
            this.txtCatCode = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.txtIncomeCode = new System.Windows.Forms.TextBox();
            this.txtCOSCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInventoryCode = new System.Windows.Forms.TextBox();
            this.gbCodes = new System.Windows.Forms.GroupBox();
            this.gbDetail = new System.Windows.Forms.GroupBox();
            this.lblCatID = new System.Windows.Forms.Label();
            this.pbCat = new System.Windows.Forms.PictureBox();
            this.txtItemType = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMainID = new System.Windows.Forms.Label();
            this.txtMainCatID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbSub = new System.Windows.Forms.RadioButton();
            this.rbMain = new System.Windows.Forms.RadioButton();
            this.label44 = new System.Windows.Forms.Label();
            this.lblItemID = new System.Windows.Forms.Label();
            this.treeCategory = new System.Windows.Forms.TreeView();
            this.gbCodes.SuspendLayout();
            this.gbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCat)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCatCode
            // 
            this.txtCatCode.Location = new System.Drawing.Point(105, 74);
            this.txtCatCode.Name = "txtCatCode";
            this.txtCatCode.Size = new System.Drawing.Size(190, 20);
            this.txtCatCode.TabIndex = 3;
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(105, 100);
            this.Description.Multiline = true;
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(190, 43);
            this.Description.TabIndex = 4;
            // 
            // txtIncomeCode
            // 
            this.txtIncomeCode.Location = new System.Drawing.Point(131, 19);
            this.txtIncomeCode.Name = "txtIncomeCode";
            this.txtIncomeCode.Size = new System.Drawing.Size(147, 20);
            this.txtIncomeCode.TabIndex = 0;
            // 
            // txtCOSCode
            // 
            this.txtCOSCode.Location = new System.Drawing.Point(131, 45);
            this.txtCOSCode.Name = "txtCOSCode";
            this.txtCOSCode.Size = new System.Drawing.Size(147, 20);
            this.txtCOSCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Category Code :";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNew.Location = new System.Drawing.Point(332, 444);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(97, 42);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(235, 444);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(91, 42);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "        Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(531, 444);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(91, 42);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "    Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(628, 444);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 42);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "      Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(435, 444);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 42);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "        Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Description :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Income GL Code :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "COS GL Code:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Inventory GL Code :";
            // 
            // txtInventoryCode
            // 
            this.txtInventoryCode.Location = new System.Drawing.Point(131, 71);
            this.txtInventoryCode.Name = "txtInventoryCode";
            this.txtInventoryCode.Size = new System.Drawing.Size(147, 20);
            this.txtInventoryCode.TabIndex = 2;
            // 
            // gbCodes
            // 
            this.gbCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCodes.Controls.Add(this.txtIncomeCode);
            this.gbCodes.Controls.Add(this.label5);
            this.gbCodes.Controls.Add(this.txtCOSCode);
            this.gbCodes.Controls.Add(this.txtInventoryCode);
            this.gbCodes.Controls.Add(this.label3);
            this.gbCodes.Controls.Add(this.label4);
            this.gbCodes.Location = new System.Drawing.Point(393, 226);
            this.gbCodes.Name = "gbCodes";
            this.gbCodes.Size = new System.Drawing.Size(318, 116);
            this.gbCodes.TabIndex = 19;
            this.gbCodes.TabStop = false;
            this.gbCodes.Text = "GL Codes :";
            // 
            // gbDetail
            // 
            this.gbDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetail.Controls.Add(this.lblCatID);
            this.gbDetail.Controls.Add(this.pbCat);
            this.gbDetail.Controls.Add(this.txtItemType);
            this.gbDetail.Controls.Add(this.label7);
            this.gbDetail.Controls.Add(this.lblMainID);
            this.gbDetail.Controls.Add(this.txtMainCatID);
            this.gbDetail.Controls.Add(this.label6);
            this.gbDetail.Controls.Add(this.rbSub);
            this.gbDetail.Controls.Add(this.rbMain);
            this.gbDetail.Controls.Add(this.label44);
            this.gbDetail.Controls.Add(this.lblItemID);
            this.gbDetail.Controls.Add(this.txtCatCode);
            this.gbDetail.Controls.Add(this.Description);
            this.gbDetail.Controls.Add(this.label2);
            this.gbDetail.Controls.Add(this.label1);
            this.gbDetail.Location = new System.Drawing.Point(393, 27);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(326, 193);
            this.gbDetail.TabIndex = 20;
            this.gbDetail.TabStop = false;
            this.gbDetail.Text = "Category Detail :";
            // 
            // lblCatID
            // 
            this.lblCatID.AutoSize = true;
            this.lblCatID.Location = new System.Drawing.Point(305, 77);
            this.lblCatID.Name = "lblCatID";
            this.lblCatID.Size = new System.Drawing.Size(0, 13);
            this.lblCatID.TabIndex = 141;
            this.lblCatID.Visible = false;
            // 
            // pbCat
            // 
            this.pbCat.Image = ((System.Drawing.Image)(resources.GetObject("pbCat.Image")));
            this.pbCat.Location = new System.Drawing.Point(205, 48);
            this.pbCat.Name = "pbCat";
            this.pbCat.Size = new System.Drawing.Size(19, 19);
            this.pbCat.TabIndex = 140;
            this.pbCat.TabStop = false;
            this.pbCat.Click += new System.EventHandler(this.pbCat_Click);
            // 
            // txtItemType
            // 
            this.txtItemType.Location = new System.Drawing.Point(105, 149);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Size = new System.Drawing.Size(190, 20);
            this.txtItemType.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 43;
            this.label7.Text = "Item Type :";
            // 
            // lblMainID
            // 
            this.lblMainID.AutoSize = true;
            this.lblMainID.Location = new System.Drawing.Point(260, 48);
            this.lblMainID.Name = "lblMainID";
            this.lblMainID.Size = new System.Drawing.Size(18, 13);
            this.lblMainID.TabIndex = 42;
            this.lblMainID.Text = "ID";
            this.lblMainID.Visible = false;
            // 
            // txtMainCatID
            // 
            this.txtMainCatID.Location = new System.Drawing.Point(105, 48);
            this.txtMainCatID.Name = "txtMainCatID";
            this.txtMainCatID.Size = new System.Drawing.Size(94, 20);
            this.txtMainCatID.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Main Category :";
            // 
            // rbSub
            // 
            this.rbSub.AutoSize = true;
            this.rbSub.Location = new System.Drawing.Point(159, 20);
            this.rbSub.Name = "rbSub";
            this.rbSub.Size = new System.Drawing.Size(89, 17);
            this.rbSub.TabIndex = 1;
            this.rbSub.TabStop = true;
            this.rbSub.Text = "Sub Category";
            this.rbSub.UseVisualStyleBackColor = true;
            this.rbSub.CheckedChanged += new System.EventHandler(this.rbSub_CheckedChanged);
            // 
            // rbMain
            // 
            this.rbMain.AutoSize = true;
            this.rbMain.Location = new System.Drawing.Point(105, 20);
            this.rbMain.Name = "rbMain";
            this.rbMain.Size = new System.Drawing.Size(48, 17);
            this.rbMain.TabIndex = 0;
            this.rbMain.TabStop = true;
            this.rbMain.Text = "Main";
            this.rbMain.UseVisualStyleBackColor = true;
            this.rbMain.CheckedChanged += new System.EventHandler(this.rbMain_CheckedChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(47, 21);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(55, 13);
            this.label44.TabIndex = 36;
            this.label44.Text = "Category :";
            // 
            // lblItemID
            // 
            this.lblItemID.AutoSize = true;
            this.lblItemID.Location = new System.Drawing.Point(138, 21);
            this.lblItemID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.Size = new System.Drawing.Size(0, 13);
            this.lblItemID.TabIndex = 37;
            // 
            // treeCategory
            // 
            this.treeCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCategory.Location = new System.Drawing.Point(12, 12);
            this.treeCategory.Name = "treeCategory";
            this.treeCategory.Size = new System.Drawing.Size(347, 426);
            this.treeCategory.TabIndex = 21;
            this.treeCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCategory_AfterSelect);
            this.treeCategory.DoubleClick += new System.EventHandler(this.treeCategory_DoubleClick);
            // 
            // Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 498);
            this.Controls.Add(this.treeCategory);
            this.Controls.Add(this.gbDetail);
            this.Controls.Add(this.gbCodes);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Name = "Categories";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Categories";
            this.Load += new System.EventHandler(this.Categories_Load);
            this.gbCodes.ResumeLayout(false);
            this.gbCodes.PerformLayout();
            this.gbDetail.ResumeLayout(false);
            this.gbDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtCatCode;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.TextBox txtIncomeCode;
        private System.Windows.Forms.TextBox txtCOSCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInventoryCode;
        private System.Windows.Forms.GroupBox gbCodes;
        private System.Windows.Forms.GroupBox gbDetail;
        private System.Windows.Forms.RadioButton rbSub;
        private System.Windows.Forms.RadioButton rbMain;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblItemID;
        private System.Windows.Forms.TextBox txtMainCatID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TreeView treeCategory;
        private System.Windows.Forms.Label lblMainID;
        private System.Windows.Forms.TextBox txtItemType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbCat;
        private System.Windows.Forms.Label lblCatID;
    }
}