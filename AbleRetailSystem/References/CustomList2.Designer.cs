namespace RestaurantPOS
{
    partial class CustomList2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomList2));
            this.label2 = new System.Windows.Forms.Label();
            this.txtSupListName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSupListID = new System.Windows.Forms.Label();
            this.lvSup = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabList = new System.Windows.Forms.TabControl();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.lvItem = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblItemListID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtItemListName = new System.Windows.Forms.TextBox();
            this.tabCustomers = new System.Windows.Forms.TabPage();
            this.lvCus = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCusListID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCusListName = new System.Windows.Forms.TextBox();
            this.tabSuppliers = new System.Windows.Forms.TabPage();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblItem = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabList.SuspendLayout();
            this.tabItems.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabCustomers.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabSuppliers.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Name";
            // 
            // txtSupListName
            // 
            this.txtSupListName.BackColor = System.Drawing.Color.White;
            this.txtSupListName.Location = new System.Drawing.Point(46, 17);
            this.txtSupListName.MaxLength = 50;
            this.txtSupListName.Name = "txtSupListName";
            this.txtSupListName.Size = new System.Drawing.Size(228, 20);
            this.txtSupListName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblSupListID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSupListName);
            this.groupBox1.Location = new System.Drawing.Point(5, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(285, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Record ID:";
            // 
            // lblSupListID
            // 
            this.lblSupListID.AutoSize = true;
            this.lblSupListID.Location = new System.Drawing.Point(369, 18);
            this.lblSupListID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSupListID.Name = "lblSupListID";
            this.lblSupListID.Size = new System.Drawing.Size(0, 13);
            this.lblSupListID.TabIndex = 40;
            // 
            // lvSup
            // 
            this.lvSup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7});
            this.lvSup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lvSup.FullRowSelect = true;
            this.lvSup.GridLines = true;
            this.lvSup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSup.HideSelection = false;
            this.lvSup.Location = new System.Drawing.Point(5, 80);
            this.lvSup.Name = "lvSup";
            this.lvSup.Size = new System.Drawing.Size(405, 278);
            this.lvSup.TabIndex = 1;
            this.lvSup.UseCompatibleStateImageBehavior = false;
            this.lvSup.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView1_ColumnWidthchanging);
            this.lvSup.Click += new System.EventHandler(this.lvSup_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "List Name";
            this.columnHeader1.Width = 380;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ID";
            this.columnHeader2.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Record Type";
            this.columnHeader7.Width = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(121, 413);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 32);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "    Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(351, 412);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 32);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "     Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(282, 412);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 33);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "       Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Record_Click);
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.tabItems);
            this.tabList.Controls.Add(this.tabCustomers);
            this.tabList.Controls.Add(this.tabSuppliers);
            this.tabList.Location = new System.Drawing.Point(6, 10);
            this.tabList.Margin = new System.Windows.Forms.Padding(2);
            this.tabList.Name = "tabList";
            this.tabList.SelectedIndex = 0;
            this.tabList.Size = new System.Drawing.Size(422, 392);
            this.tabList.TabIndex = 6;
            this.tabList.Click += new System.EventHandler(this.tabList_Click);
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.lblItem);
            this.tabItems.Controls.Add(this.lvItem);
            this.tabItems.Controls.Add(this.groupBox3);
            this.tabItems.Location = new System.Drawing.Point(4, 22);
            this.tabItems.Margin = new System.Windows.Forms.Padding(2);
            this.tabItems.Name = "tabItems";
            this.tabItems.Padding = new System.Windows.Forms.Padding(2);
            this.tabItems.Size = new System.Drawing.Size(414, 366);
            this.tabItems.TabIndex = 0;
            this.tabItems.Text = "Items";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // lvItem
            // 
            this.lvItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader9});
            this.lvItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lvItem.FullRowSelect = true;
            this.lvItem.GridLines = true;
            this.lvItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvItem.HideSelection = false;
            this.lvItem.Location = new System.Drawing.Point(5, 85);
            this.lvItem.Name = "lvItem";
            this.lvItem.Size = new System.Drawing.Size(406, 273);
            this.lvItem.TabIndex = 3;
            this.lvItem.UseCompatibleStateImageBehavior = false;
            this.lvItem.Click += new System.EventHandler(this.lvItem_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "List Name";
            this.columnHeader5.Width = 380;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ID";
            this.columnHeader6.Width = 0;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Record Type";
            this.columnHeader9.Width = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lblItemListID);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtItemListName);
            this.groupBox3.Location = new System.Drawing.Point(5, 30);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 49);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(285, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Record ID:";
            // 
            // lblItemListID
            // 
            this.lblItemListID.AutoSize = true;
            this.lblItemListID.Location = new System.Drawing.Point(346, 20);
            this.lblItemListID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblItemListID.Name = "lblItemListID";
            this.lblItemListID.Size = new System.Drawing.Size(0, 13);
            this.lblItemListID.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Name";
            // 
            // txtItemListName
            // 
            this.txtItemListName.BackColor = System.Drawing.Color.White;
            this.txtItemListName.Location = new System.Drawing.Point(46, 17);
            this.txtItemListName.MaxLength = 50;
            this.txtItemListName.Name = "txtItemListName";
            this.txtItemListName.Size = new System.Drawing.Size(228, 20);
            this.txtItemListName.TabIndex = 1;
            // 
            // tabCustomers
            // 
            this.tabCustomers.Controls.Add(this.lblCustomer);
            this.tabCustomers.Controls.Add(this.lvCus);
            this.tabCustomers.Controls.Add(this.groupBox2);
            this.tabCustomers.Location = new System.Drawing.Point(4, 22);
            this.tabCustomers.Margin = new System.Windows.Forms.Padding(2);
            this.tabCustomers.Name = "tabCustomers";
            this.tabCustomers.Padding = new System.Windows.Forms.Padding(2);
            this.tabCustomers.Size = new System.Drawing.Size(414, 366);
            this.tabCustomers.TabIndex = 1;
            this.tabCustomers.Text = "Customers";
            this.tabCustomers.UseVisualStyleBackColor = true;
            // 
            // lvCus
            // 
            this.lvCus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader8});
            this.lvCus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lvCus.FullRowSelect = true;
            this.lvCus.GridLines = true;
            this.lvCus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCus.HideSelection = false;
            this.lvCus.Location = new System.Drawing.Point(5, 80);
            this.lvCus.Name = "lvCus";
            this.lvCus.Size = new System.Drawing.Size(406, 278);
            this.lvCus.TabIndex = 3;
            this.lvCus.UseCompatibleStateImageBehavior = false;
            this.lvCus.Click += new System.EventHandler(this.lvCus_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "List Name";
            this.columnHeader3.Width = 380;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ID";
            this.columnHeader4.Width = 0;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Record Type";
            this.columnHeader8.Width = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblCusListID);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCusListName);
            this.groupBox2.Location = new System.Drawing.Point(5, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(285, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Record ID:";
            // 
            // lblCusListID
            // 
            this.lblCusListID.AutoSize = true;
            this.lblCusListID.Location = new System.Drawing.Point(372, 19);
            this.lblCusListID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCusListID.Name = "lblCusListID";
            this.lblCusListID.Size = new System.Drawing.Size(0, 13);
            this.lblCusListID.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Name";
            // 
            // txtCusListName
            // 
            this.txtCusListName.BackColor = System.Drawing.Color.White;
            this.txtCusListName.Location = new System.Drawing.Point(46, 17);
            this.txtCusListName.MaxLength = 50;
            this.txtCusListName.Name = "txtCusListName";
            this.txtCusListName.Size = new System.Drawing.Size(228, 20);
            this.txtCusListName.TabIndex = 1;
            // 
            // tabSuppliers
            // 
            this.tabSuppliers.Controls.Add(this.lblSupplier);
            this.tabSuppliers.Controls.Add(this.lvSup);
            this.tabSuppliers.Controls.Add(this.groupBox1);
            this.tabSuppliers.Location = new System.Drawing.Point(4, 22);
            this.tabSuppliers.Margin = new System.Windows.Forms.Padding(2);
            this.tabSuppliers.Name = "tabSuppliers";
            this.tabSuppliers.Size = new System.Drawing.Size(414, 366);
            this.tabSuppliers.TabIndex = 2;
            this.tabSuppliers.Text = "Suppliers";
            this.tabSuppliers.UseVisualStyleBackColor = true;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNew.Location = new System.Drawing.Point(33, 413);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(82, 32);
            this.btnAddNew.TabIndex = 7;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(207, 413);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(69, 32);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "        Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItem.Location = new System.Drawing.Point(6, 11);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(0, 13);
            this.lblItem.TabIndex = 4;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(10, 10);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(0, 13);
            this.lblCustomer.TabIndex = 4;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.Location = new System.Drawing.Point(6, 12);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(0, 13);
            this.lblSupplier.TabIndex = 2;
            // 
            // CustomList2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(438, 456);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.tabList);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(454, 495);
            this.MinimumSize = new System.Drawing.Size(454, 495);
            this.Name = "CustomList2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom List 2";
            this.Load += new System.EventHandler(this.CustomList2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabList.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            this.tabItems.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabCustomers.ResumeLayout(false);
            this.tabCustomers.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabSuppliers.ResumeLayout(false);
            this.tabSuppliers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSupListName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvSup;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabControl tabList;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.TabPage tabCustomers;
        private System.Windows.Forms.TabPage tabSuppliers;
        private System.Windows.Forms.ListView lvItem;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtItemListName;
        private System.Windows.Forms.ListView lvCus;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCusListName;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Label lblSupListID;
        private System.Windows.Forms.Label lblItemListID;
        private System.Windows.Forms.Label lblCusListID;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblSupplier;
    }
}