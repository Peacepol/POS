namespace RestaurantPOS
{
    partial class UserMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMaintenance));
            this.tabUserAccess = new System.Windows.Forms.TabControl();
            this.tabUserProfile = new System.Windows.Forms.TabPage();
            this.isAdmin = new System.Windows.Forms.CheckBox();
            this.isTechnician = new System.Windows.Forms.CheckBox();
            this.isSupervisor = new System.Windows.Forms.CheckBox();
            this.IsSalesPerson = new System.Windows.Forms.CheckBox();
            this.lbUserRole = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.txtVerifyPassword = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.btnOkUserProfile = new System.Windows.Forms.Button();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserProfileSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvUserListProfile = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblUser1 = new System.Windows.Forms.Label();
            this.chkbAllView = new System.Windows.Forms.CheckBox();
            this.chkbAllDelete = new System.Windows.Forms.CheckBox();
            this.chkbAllEdit = new System.Windows.Forms.CheckBox();
            this.chkbAllAdd = new System.Windows.Forms.CheckBox();
            this.btnRefreshAccess = new System.Windows.Forms.Button();
            this.btnEditAccess = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUserAccess = new System.Windows.Forms.DataGridView();
            this.dgvUserList = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblUser2 = new System.Windows.Forms.Label();
            this.SelectAllViewSpecialRights = new System.Windows.Forms.CheckBox();
            this.SelectAllEditSpecialRights = new System.Windows.Forms.CheckBox();
            this.btnItemRefresh = new System.Windows.Forms.Button();
            this.btnItemEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.dgSpecialAccessRight = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearchUser = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dgListOfUsers = new System.Windows.Forms.DataGridView();
            this.ReportsViewingRights = new System.Windows.Forms.TabPage();
            this.lblUser3 = new System.Windows.Forms.Label();
            this.SelectAllViewRights = new System.Windows.Forms.CheckBox();
            this.btnRefreshRights = new System.Windows.Forms.Button();
            this.btnEditRights = new System.Windows.Forms.Button();
            this.btnSaveRights = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtUserProfile = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.dgReportRights = new System.Windows.Forms.DataGridView();
            this.dgUserProfileRights = new System.Windows.Forms.DataGridView();
            this.tabUserAccess.SuspendLayout();
            this.tabUserProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserListProfile)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpecialAccessRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgListOfUsers)).BeginInit();
            this.ReportsViewingRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportRights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserProfileRights)).BeginInit();
            this.SuspendLayout();
            // 
            // tabUserAccess
            // 
            this.tabUserAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabUserAccess.Controls.Add(this.tabUserProfile);
            this.tabUserAccess.Controls.Add(this.tabPage2);
            this.tabUserAccess.Controls.Add(this.tabPage1);
            this.tabUserAccess.Controls.Add(this.ReportsViewingRights);
            this.tabUserAccess.Location = new System.Drawing.Point(12, 12);
            this.tabUserAccess.Name = "tabUserAccess";
            this.tabUserAccess.SelectedIndex = 0;
            this.tabUserAccess.Size = new System.Drawing.Size(1120, 524);
            this.tabUserAccess.TabIndex = 0;
            // 
            // tabUserProfile
            // 
            this.tabUserProfile.Controls.Add(this.isAdmin);
            this.tabUserProfile.Controls.Add(this.isTechnician);
            this.tabUserProfile.Controls.Add(this.isSupervisor);
            this.tabUserProfile.Controls.Add(this.IsSalesPerson);
            this.tabUserProfile.Controls.Add(this.lbUserRole);
            this.tabUserProfile.Controls.Add(this.btnRefresh);
            this.tabUserProfile.Controls.Add(this.btnEdit);
            this.tabUserProfile.Controls.Add(this.btnDeleteUser);
            this.tabUserProfile.Controls.Add(this.btnAddUser);
            this.tabUserProfile.Controls.Add(this.txtVerifyPassword);
            this.tabUserProfile.Controls.Add(this.label12);
            this.tabUserProfile.Controls.Add(this.txtPassword);
            this.tabUserProfile.Controls.Add(this.label11);
            this.tabUserProfile.Controls.Add(this.cbActive);
            this.tabUserProfile.Controls.Add(this.btnOkUserProfile);
            this.tabUserProfile.Controls.Add(this.txtDepartment);
            this.tabUserProfile.Controls.Add(this.txtFullName);
            this.tabUserProfile.Controls.Add(this.txtUserName);
            this.tabUserProfile.Controls.Add(this.txtUserID);
            this.tabUserProfile.Controls.Add(this.label10);
            this.tabUserProfile.Controls.Add(this.label9);
            this.tabUserProfile.Controls.Add(this.label8);
            this.tabUserProfile.Controls.Add(this.label7);
            this.tabUserProfile.Controls.Add(this.label6);
            this.tabUserProfile.Controls.Add(this.label4);
            this.tabUserProfile.Controls.Add(this.txtUserProfileSearch);
            this.tabUserProfile.Controls.Add(this.label5);
            this.tabUserProfile.Controls.Add(this.dgvUserListProfile);
            this.tabUserProfile.Location = new System.Drawing.Point(4, 22);
            this.tabUserProfile.Name = "tabUserProfile";
            this.tabUserProfile.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserProfile.Size = new System.Drawing.Size(1112, 498);
            this.tabUserProfile.TabIndex = 0;
            this.tabUserProfile.Text = "User Profile";
            this.tabUserProfile.UseVisualStyleBackColor = true;
            // 
            // isAdmin
            // 
            this.isAdmin.AutoSize = true;
            this.isAdmin.Location = new System.Drawing.Point(777, 360);
            this.isAdmin.Name = "isAdmin";
            this.isAdmin.Size = new System.Drawing.Size(55, 17);
            this.isAdmin.TabIndex = 38;
            this.isAdmin.Text = "Admin";
            this.isAdmin.UseVisualStyleBackColor = true;
            // 
            // isTechnician
            // 
            this.isTechnician.AutoSize = true;
            this.isTechnician.Location = new System.Drawing.Point(692, 360);
            this.isTechnician.Name = "isTechnician";
            this.isTechnician.Size = new System.Drawing.Size(79, 17);
            this.isTechnician.TabIndex = 37;
            this.isTechnician.Text = "Technician";
            this.isTechnician.UseVisualStyleBackColor = true;
            // 
            // isSupervisor
            // 
            this.isSupervisor.AutoSize = true;
            this.isSupervisor.Location = new System.Drawing.Point(610, 360);
            this.isSupervisor.Name = "isSupervisor";
            this.isSupervisor.Size = new System.Drawing.Size(76, 17);
            this.isSupervisor.TabIndex = 36;
            this.isSupervisor.Text = "Supervisor";
            this.isSupervisor.UseVisualStyleBackColor = true;
            // 
            // IsSalesPerson
            // 
            this.IsSalesPerson.AutoSize = true;
            this.IsSalesPerson.Location = new System.Drawing.Point(516, 360);
            this.IsSalesPerson.Name = "IsSalesPerson";
            this.IsSalesPerson.Size = new System.Drawing.Size(88, 17);
            this.IsSalesPerson.TabIndex = 35;
            this.IsSalesPerson.Text = "Sales Person";
            this.IsSalesPerson.UseVisualStyleBackColor = true;
            // 
            // lbUserRole
            // 
            this.lbUserRole.AutoSize = true;
            this.lbUserRole.Location = new System.Drawing.Point(461, 328);
            this.lbUserRole.Name = "lbUserRole";
            this.lbUserRole.Size = new System.Drawing.Size(57, 13);
            this.lbUserRole.TabIndex = 34;
            this.lbUserRole.Text = "User Role:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::RestaurantPOS.Properties.Resources.refresh24X24;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(483, 440);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(92, 35);
            this.btnRefresh.TabIndex = 32;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(679, 440);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(92, 35);
            this.btnEdit.TabIndex = 31;
            this.btnEdit.Text = "Edit User";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteUser.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteUser.Image")));
            this.btnDeleteUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteUser.Location = new System.Drawing.Point(777, 440);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(105, 35);
            this.btnDeleteUser.TabIndex = 10;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUser.Image")));
            this.btnAddUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddUser.Location = new System.Drawing.Point(581, 440);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(92, 35);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // txtVerifyPassword
            // 
            this.txtVerifyPassword.Location = new System.Drawing.Point(544, 183);
            this.txtVerifyPassword.Name = "txtVerifyPassword";
            this.txtVerifyPassword.Size = new System.Drawing.Size(260, 20);
            this.txtVerifyPassword.TabIndex = 5;
            this.txtVerifyPassword.UseSystemPasswordChar = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(452, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Verify Password: ";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(544, 147);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(260, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(452, 150);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Password: ";
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Location = new System.Drawing.Point(544, 293);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(56, 17);
            this.cbActive.TabIndex = 8;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // btnOkUserProfile
            // 
            this.btnOkUserProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkUserProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOkUserProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnOkUserProfile.Image")));
            this.btnOkUserProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOkUserProfile.Location = new System.Drawing.Point(888, 440);
            this.btnOkUserProfile.Name = "btnOkUserProfile";
            this.btnOkUserProfile.Size = new System.Drawing.Size(82, 35);
            this.btnOkUserProfile.TabIndex = 11;
            this.btnOkUserProfile.Text = "Save";
            this.btnOkUserProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOkUserProfile.UseVisualStyleBackColor = true;
            this.btnOkUserProfile.Click += new System.EventHandler(this.btnOkUserProfile_Click);
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(544, 257);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(260, 20);
            this.txtDepartment.TabIndex = 7;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(544, 220);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(260, 20);
            this.txtFullName.TabIndex = 6;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(544, 114);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(260, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // txtUserID
            // 
            this.txtUserID.Enabled = false;
            this.txtUserID.Location = new System.Drawing.Point(544, 78);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(260, 20);
            this.txtUserID.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(452, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "User Name: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(452, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "User ID: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(452, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Department: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(452, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Full Name: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(452, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "User Profile";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "User Search:";
            // 
            // txtUserProfileSearch
            // 
            this.txtUserProfileSearch.Location = new System.Drawing.Point(248, 15);
            this.txtUserProfileSearch.Name = "txtUserProfileSearch";
            this.txtUserProfileSearch.Size = new System.Drawing.Size(125, 20);
            this.txtUserProfileSearch.TabIndex = 1;
            this.txtUserProfileSearch.TextChanged += new System.EventHandler(this.txtUserProfileSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "List of Users";
            // 
            // dgvUserListProfile
            // 
            this.dgvUserListProfile.AllowUserToAddRows = false;
            this.dgvUserListProfile.AllowUserToDeleteRows = false;
            this.dgvUserListProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvUserListProfile.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvUserListProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserListProfile.Location = new System.Drawing.Point(31, 50);
            this.dgvUserListProfile.MultiSelect = false;
            this.dgvUserListProfile.Name = "dgvUserListProfile";
            this.dgvUserListProfile.ReadOnly = true;
            this.dgvUserListProfile.RowHeadersVisible = false;
            this.dgvUserListProfile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserListProfile.Size = new System.Drawing.Size(342, 425);
            this.dgvUserListProfile.TabIndex = 13;
            this.dgvUserListProfile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserListProfile_CellClick);
            this.dgvUserListProfile.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserListProfile_CellEnter);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblUser1);
            this.tabPage2.Controls.Add(this.chkbAllView);
            this.tabPage2.Controls.Add(this.chkbAllDelete);
            this.tabPage2.Controls.Add(this.chkbAllEdit);
            this.tabPage2.Controls.Add(this.chkbAllAdd);
            this.tabPage2.Controls.Add(this.btnRefreshAccess);
            this.tabPage2.Controls.Add(this.btnEditAccess);
            this.tabPage2.Controls.Add(this.btnOk);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtUserSearch);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgvUserAccess);
            this.tabPage2.Controls.Add(this.dgvUserList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1112, 498);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User Access";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // lblUser1
            // 
            this.lblUser1.AutoSize = true;
            this.lblUser1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser1.Location = new System.Drawing.Point(505, 18);
            this.lblUser1.Name = "lblUser1";
            this.lblUser1.Size = new System.Drawing.Size(0, 13);
            this.lblUser1.TabIndex = 46;
            // 
            // chkbAllView
            // 
            this.chkbAllView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbAllView.AutoSize = true;
            this.chkbAllView.Checked = true;
            this.chkbAllView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbAllView.Enabled = false;
            this.chkbAllView.Location = new System.Drawing.Point(686, 26);
            this.chkbAllView.Name = "chkbAllView";
            this.chkbAllView.Size = new System.Drawing.Size(96, 17);
            this.chkbAllView.TabIndex = 45;
            this.chkbAllView.Text = "Select All View";
            this.chkbAllView.UseVisualStyleBackColor = true;
            this.chkbAllView.CheckedChanged += new System.EventHandler(this.chkbAllView_CheckedChanged);
            // 
            // chkbAllDelete
            // 
            this.chkbAllDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbAllDelete.AutoSize = true;
            this.chkbAllDelete.Checked = true;
            this.chkbAllDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbAllDelete.Enabled = false;
            this.chkbAllDelete.Location = new System.Drawing.Point(983, 26);
            this.chkbAllDelete.Name = "chkbAllDelete";
            this.chkbAllDelete.Size = new System.Drawing.Size(104, 17);
            this.chkbAllDelete.TabIndex = 44;
            this.chkbAllDelete.Text = "Select All Delete";
            this.chkbAllDelete.UseVisualStyleBackColor = true;
            this.chkbAllDelete.CheckedChanged += new System.EventHandler(this.chkbAllDelete_CheckedChanged);
            // 
            // chkbAllEdit
            // 
            this.chkbAllEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbAllEdit.AutoSize = true;
            this.chkbAllEdit.Checked = true;
            this.chkbAllEdit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbAllEdit.Enabled = false;
            this.chkbAllEdit.Location = new System.Drawing.Point(886, 26);
            this.chkbAllEdit.Name = "chkbAllEdit";
            this.chkbAllEdit.Size = new System.Drawing.Size(91, 17);
            this.chkbAllEdit.TabIndex = 43;
            this.chkbAllEdit.Text = "Select All Edit";
            this.chkbAllEdit.UseVisualStyleBackColor = true;
            this.chkbAllEdit.CheckedChanged += new System.EventHandler(this.chkbAllEdit_CheckedChanged);
            // 
            // chkbAllAdd
            // 
            this.chkbAllAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbAllAdd.AutoSize = true;
            this.chkbAllAdd.Checked = true;
            this.chkbAllAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbAllAdd.Enabled = false;
            this.chkbAllAdd.Location = new System.Drawing.Point(788, 26);
            this.chkbAllAdd.Name = "chkbAllAdd";
            this.chkbAllAdd.Size = new System.Drawing.Size(92, 17);
            this.chkbAllAdd.TabIndex = 42;
            this.chkbAllAdd.Text = "Select All Add";
            this.chkbAllAdd.UseVisualStyleBackColor = true;
            this.chkbAllAdd.CheckedChanged += new System.EventHandler(this.chkbAllAdd_CheckedChanged);
            // 
            // btnRefreshAccess
            // 
            this.btnRefreshAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshAccess.Image = global::RestaurantPOS.Properties.Resources.refresh24X24;
            this.btnRefreshAccess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshAccess.Location = new System.Drawing.Point(806, 453);
            this.btnRefreshAccess.Name = "btnRefreshAccess";
            this.btnRefreshAccess.Size = new System.Drawing.Size(92, 35);
            this.btnRefreshAccess.TabIndex = 33;
            this.btnRefreshAccess.Text = "Refresh";
            this.btnRefreshAccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshAccess.UseVisualStyleBackColor = true;
            this.btnRefreshAccess.Click += new System.EventHandler(this.btnRefreshAccess_Click);
            // 
            // btnEditAccess
            // 
            this.btnEditAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAccess.Image = global::RestaurantPOS.Properties.Resources.edit24;
            this.btnEditAccess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditAccess.Location = new System.Drawing.Point(904, 453);
            this.btnEditAccess.Name = "btnEditAccess";
            this.btnEditAccess.Size = new System.Drawing.Size(106, 35);
            this.btnEditAccess.TabIndex = 32;
            this.btnEditAccess.Text = "Edit Access";
            this.btnEditAccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditAccess.UseVisualStyleBackColor = true;
            this.btnEditAccess.Click += new System.EventHandler(this.btnEditAccess_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(1016, 453);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(74, 35);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "Save";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "User Search:";
            // 
            // txtUserSearch
            // 
            this.txtUserSearch.Location = new System.Drawing.Point(248, 15);
            this.txtUserSearch.Name = "txtUserSearch";
            this.txtUserSearch.Size = new System.Drawing.Size(125, 20);
            this.txtUserSearch.TabIndex = 1;
            this.txtUserSearch.TextChanged += new System.EventHandler(this.txtUserSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(413, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "User Access - ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "List of Users";
            // 
            // dgvUserAccess
            // 
            this.dgvUserAccess.AllowUserToAddRows = false;
            this.dgvUserAccess.AllowUserToDeleteRows = false;
            this.dgvUserAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUserAccess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUserAccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserAccess.Location = new System.Drawing.Point(416, 49);
            this.dgvUserAccess.Name = "dgvUserAccess";
            this.dgvUserAccess.RowHeadersVisible = false;
            this.dgvUserAccess.Size = new System.Drawing.Size(671, 392);
            this.dgvUserAccess.TabIndex = 8;
            this.dgvUserAccess.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserAccess_CellContentClick_1);
            // 
            // dgvUserList
            // 
            this.dgvUserList.AllowUserToAddRows = false;
            this.dgvUserList.AllowUserToDeleteRows = false;
            this.dgvUserList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvUserList.Location = new System.Drawing.Point(31, 50);
            this.dgvUserList.MultiSelect = false;
            this.dgvUserList.Name = "dgvUserList";
            this.dgvUserList.RowHeadersVisible = false;
            this.dgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserList.Size = new System.Drawing.Size(342, 425);
            this.dgvUserList.TabIndex = 7;
            this.dgvUserList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserList_CellClick);
            this.dgvUserList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserList_CellEnter);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblUser2);
            this.tabPage1.Controls.Add(this.SelectAllViewSpecialRights);
            this.tabPage1.Controls.Add(this.SelectAllEditSpecialRights);
            this.tabPage1.Controls.Add(this.btnItemRefresh);
            this.tabPage1.Controls.Add(this.btnItemEdit);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.dgSpecialAccessRight);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtSearchUser);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.dgListOfUsers);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1112, 498);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "User Special Rights Access";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblUser2
            // 
            this.lblUser2.AutoSize = true;
            this.lblUser2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser2.Location = new System.Drawing.Point(590, 18);
            this.lblUser2.Name = "lblUser2";
            this.lblUser2.Size = new System.Drawing.Size(0, 13);
            this.lblUser2.TabIndex = 48;
            // 
            // SelectAllViewSpecialRights
            // 
            this.SelectAllViewSpecialRights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectAllViewSpecialRights.AutoSize = true;
            this.SelectAllViewSpecialRights.Checked = true;
            this.SelectAllViewSpecialRights.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectAllViewSpecialRights.Enabled = false;
            this.SelectAllViewSpecialRights.Location = new System.Drawing.Point(891, 26);
            this.SelectAllViewSpecialRights.Name = "SelectAllViewSpecialRights";
            this.SelectAllViewSpecialRights.Size = new System.Drawing.Size(96, 17);
            this.SelectAllViewSpecialRights.TabIndex = 47;
            this.SelectAllViewSpecialRights.Text = "Select All View";
            this.SelectAllViewSpecialRights.UseVisualStyleBackColor = true;
            this.SelectAllViewSpecialRights.CheckedChanged += new System.EventHandler(this.SelectAllViewSpecialRights_CheckedChanged);
            // 
            // SelectAllEditSpecialRights
            // 
            this.SelectAllEditSpecialRights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectAllEditSpecialRights.AutoSize = true;
            this.SelectAllEditSpecialRights.Checked = true;
            this.SelectAllEditSpecialRights.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectAllEditSpecialRights.Enabled = false;
            this.SelectAllEditSpecialRights.Location = new System.Drawing.Point(993, 26);
            this.SelectAllEditSpecialRights.Name = "SelectAllEditSpecialRights";
            this.SelectAllEditSpecialRights.Size = new System.Drawing.Size(91, 17);
            this.SelectAllEditSpecialRights.TabIndex = 46;
            this.SelectAllEditSpecialRights.Text = "Select All Edit";
            this.SelectAllEditSpecialRights.UseVisualStyleBackColor = true;
            this.SelectAllEditSpecialRights.CheckedChanged += new System.EventHandler(this.SelectAllEditSpecialRights_CheckedChanged);
            // 
            // btnItemRefresh
            // 
            this.btnItemRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemRefresh.Image = global::RestaurantPOS.Properties.Resources.refresh24X24;
            this.btnItemRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItemRefresh.Location = new System.Drawing.Point(801, 453);
            this.btnItemRefresh.Name = "btnItemRefresh";
            this.btnItemRefresh.Size = new System.Drawing.Size(92, 35);
            this.btnItemRefresh.TabIndex = 36;
            this.btnItemRefresh.Text = "Refresh";
            this.btnItemRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnItemRefresh.UseVisualStyleBackColor = true;
            this.btnItemRefresh.Click += new System.EventHandler(this.btnItemRefresh_Click);
            // 
            // btnItemEdit
            // 
            this.btnItemEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemEdit.Image = global::RestaurantPOS.Properties.Resources.edit24;
            this.btnItemEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItemEdit.Location = new System.Drawing.Point(899, 453);
            this.btnItemEdit.Name = "btnItemEdit";
            this.btnItemEdit.Size = new System.Drawing.Size(106, 35);
            this.btnItemEdit.TabIndex = 35;
            this.btnItemEdit.Text = "Edit Access";
            this.btnItemEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnItemEdit.UseVisualStyleBackColor = true;
            this.btnItemEdit.Click += new System.EventHandler(this.btnItemEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1011, 453);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 35);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(413, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(176, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "User Special Rights Access - ";
            // 
            // dgSpecialAccessRight
            // 
            this.dgSpecialAccessRight.AllowUserToAddRows = false;
            this.dgSpecialAccessRight.AllowUserToDeleteRows = false;
            this.dgSpecialAccessRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSpecialAccessRight.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSpecialAccessRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSpecialAccessRight.Location = new System.Drawing.Point(416, 49);
            this.dgSpecialAccessRight.Name = "dgSpecialAccessRight";
            this.dgSpecialAccessRight.RowHeadersVisible = false;
            this.dgSpecialAccessRight.Size = new System.Drawing.Size(671, 392);
            this.dgSpecialAccessRight.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(173, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "User Search:";
            // 
            // txtSearchUser
            // 
            this.txtSearchUser.Location = new System.Drawing.Point(248, 15);
            this.txtSearchUser.Name = "txtSearchUser";
            this.txtSearchUser.Size = new System.Drawing.Size(125, 20);
            this.txtSearchUser.TabIndex = 13;
            this.txtSearchUser.TextChanged += new System.EventHandler(this.txtSearchUser_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "List of Users";
            // 
            // dgListOfUsers
            // 
            this.dgListOfUsers.AllowUserToAddRows = false;
            this.dgListOfUsers.AllowUserToDeleteRows = false;
            this.dgListOfUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgListOfUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgListOfUsers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgListOfUsers.Location = new System.Drawing.Point(31, 50);
            this.dgListOfUsers.MultiSelect = false;
            this.dgListOfUsers.Name = "dgListOfUsers";
            this.dgListOfUsers.RowHeadersVisible = false;
            this.dgListOfUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgListOfUsers.Size = new System.Drawing.Size(342, 425);
            this.dgListOfUsers.TabIndex = 8;
            this.dgListOfUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListOfUsers_CellClick);
            this.dgListOfUsers.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListOfUsers_CellEnter);
            // 
            // ReportsViewingRights
            // 
            this.ReportsViewingRights.Controls.Add(this.lblUser3);
            this.ReportsViewingRights.Controls.Add(this.SelectAllViewRights);
            this.ReportsViewingRights.Controls.Add(this.btnRefreshRights);
            this.ReportsViewingRights.Controls.Add(this.btnEditRights);
            this.ReportsViewingRights.Controls.Add(this.btnSaveRights);
            this.ReportsViewingRights.Controls.Add(this.label16);
            this.ReportsViewingRights.Controls.Add(this.txtUserProfile);
            this.ReportsViewingRights.Controls.Add(this.label17);
            this.ReportsViewingRights.Controls.Add(this.label18);
            this.ReportsViewingRights.Controls.Add(this.dgReportRights);
            this.ReportsViewingRights.Controls.Add(this.dgUserProfileRights);
            this.ReportsViewingRights.Location = new System.Drawing.Point(4, 22);
            this.ReportsViewingRights.Name = "ReportsViewingRights";
            this.ReportsViewingRights.Padding = new System.Windows.Forms.Padding(3);
            this.ReportsViewingRights.Size = new System.Drawing.Size(1112, 498);
            this.ReportsViewingRights.TabIndex = 3;
            this.ReportsViewingRights.Text = "Report Viewing Rights";
            this.ReportsViewingRights.UseVisualStyleBackColor = true;
            // 
            // lblUser3
            // 
            this.lblUser3.AutoSize = true;
            this.lblUser3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser3.Location = new System.Drawing.Point(520, 16);
            this.lblUser3.Name = "lblUser3";
            this.lblUser3.Size = new System.Drawing.Size(0, 13);
            this.lblUser3.TabIndex = 54;
            // 
            // SelectAllViewRights
            // 
            this.SelectAllViewRights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectAllViewRights.AutoSize = true;
            this.SelectAllViewRights.Checked = true;
            this.SelectAllViewRights.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectAllViewRights.Enabled = false;
            this.SelectAllViewRights.Location = new System.Drawing.Point(1013, 26);
            this.SelectAllViewRights.Name = "SelectAllViewRights";
            this.SelectAllViewRights.Size = new System.Drawing.Size(70, 17);
            this.SelectAllViewRights.TabIndex = 53;
            this.SelectAllViewRights.Text = "Select All";
            this.SelectAllViewRights.UseVisualStyleBackColor = true;
            this.SelectAllViewRights.CheckedChanged += new System.EventHandler(this.SelectAllViewRights_CheckedChanged);
            // 
            // btnRefreshRights
            // 
            this.btnRefreshRights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshRights.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshRights.Image = global::RestaurantPOS.Properties.Resources.refresh24X24;
            this.btnRefreshRights.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshRights.Location = new System.Drawing.Point(803, 451);
            this.btnRefreshRights.Name = "btnRefreshRights";
            this.btnRefreshRights.Size = new System.Drawing.Size(92, 35);
            this.btnRefreshRights.TabIndex = 42;
            this.btnRefreshRights.Text = "Refresh";
            this.btnRefreshRights.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefreshRights.UseVisualStyleBackColor = true;
            this.btnRefreshRights.Click += new System.EventHandler(this.btnRefreshRights_Click);
            // 
            // btnEditRights
            // 
            this.btnEditRights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditRights.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRights.Image = global::RestaurantPOS.Properties.Resources.edit24;
            this.btnEditRights.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditRights.Location = new System.Drawing.Point(901, 451);
            this.btnEditRights.Name = "btnEditRights";
            this.btnEditRights.Size = new System.Drawing.Size(106, 35);
            this.btnEditRights.TabIndex = 41;
            this.btnEditRights.Text = "Edit Access";
            this.btnEditRights.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditRights.UseVisualStyleBackColor = true;
            this.btnEditRights.Click += new System.EventHandler(this.btnEditRights_Click);
            // 
            // btnSaveRights
            // 
            this.btnSaveRights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRights.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRights.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRights.Image")));
            this.btnSaveRights.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveRights.Location = new System.Drawing.Point(1013, 451);
            this.btnSaveRights.Name = "btnSaveRights";
            this.btnSaveRights.Size = new System.Drawing.Size(74, 35);
            this.btnSaveRights.TabIndex = 40;
            this.btnSaveRights.Text = "Save";
            this.btnSaveRights.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveRights.UseVisualStyleBackColor = true;
            this.btnSaveRights.Click += new System.EventHandler(this.btnSaveRights_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(173, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 13);
            this.label16.TabIndex = 39;
            this.label16.Text = "User Search:";
            // 
            // txtUserProfile
            // 
            this.txtUserProfile.Location = new System.Drawing.Point(248, 15);
            this.txtUserProfile.Name = "txtUserProfile";
            this.txtUserProfile.Size = new System.Drawing.Size(125, 20);
            this.txtUserProfile.TabIndex = 34;
            this.txtUserProfile.TextChanged += new System.EventHandler(this.txtUserProfile_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(413, 18);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 13);
            this.label17.TabIndex = 38;
            this.label17.Text = "Report Access -";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(28, 18);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 13);
            this.label18.TabIndex = 37;
            this.label18.Text = "List of Users";
            // 
            // dgReportRights
            // 
            this.dgReportRights.AllowUserToAddRows = false;
            this.dgReportRights.AllowUserToDeleteRows = false;
            this.dgReportRights.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgReportRights.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgReportRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReportRights.Location = new System.Drawing.Point(416, 49);
            this.dgReportRights.Name = "dgReportRights";
            this.dgReportRights.RowHeadersVisible = false;
            this.dgReportRights.Size = new System.Drawing.Size(671, 392);
            this.dgReportRights.TabIndex = 36;
            // 
            // dgUserProfileRights
            // 
            this.dgUserProfileRights.AllowUserToAddRows = false;
            this.dgUserProfileRights.AllowUserToDeleteRows = false;
            this.dgUserProfileRights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgUserProfileRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUserProfileRights.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgUserProfileRights.Location = new System.Drawing.Point(31, 50);
            this.dgUserProfileRights.MultiSelect = false;
            this.dgUserProfileRights.Name = "dgUserProfileRights";
            this.dgUserProfileRights.RowHeadersVisible = false;
            this.dgUserProfileRights.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUserProfileRights.Size = new System.Drawing.Size(342, 425);
            this.dgUserProfileRights.TabIndex = 35;
            this.dgUserProfileRights.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUserProfileRights_CellClick);
            this.dgUserProfileRights.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUserProfileRights_CellEnter);
            // 
            // UserMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 534);
            this.Controls.Add(this.tabUserAccess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UserMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Maintenance";
            this.Load += new System.EventHandler(this.UserMaintenance_Load);
            this.tabUserAccess.ResumeLayout(false);
            this.tabUserProfile.ResumeLayout(false);
            this.tabUserProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserListProfile)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpecialAccessRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgListOfUsers)).EndInit();
            this.ReportsViewingRights.ResumeLayout(false);
            this.ReportsViewingRights.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportRights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserProfileRights)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabUserAccess;
        private System.Windows.Forms.TabPage tabUserProfile;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvUserAccess;
        private System.Windows.Forms.DataGridView dgvUserList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserProfileSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvUserListProfile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Button btnOkUserProfile;
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.TextBox txtVerifyPassword;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnEditAccess;
        private System.Windows.Forms.Button btnRefreshAccess;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lbUserRole;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dgSpecialAccessRight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchUser;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dgListOfUsers;
        private System.Windows.Forms.Button btnItemRefresh;
        private System.Windows.Forms.Button btnItemEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox isAdmin;
        private System.Windows.Forms.CheckBox isTechnician;
        private System.Windows.Forms.CheckBox isSupervisor;
        private System.Windows.Forms.CheckBox IsSalesPerson;
        private System.Windows.Forms.TabPage ReportsViewingRights;
        private System.Windows.Forms.Button btnRefreshRights;
        private System.Windows.Forms.Button btnEditRights;
        private System.Windows.Forms.Button btnSaveRights;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtUserProfile;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView dgReportRights;
        private System.Windows.Forms.DataGridView dgUserProfileRights;
        private System.Windows.Forms.CheckBox chkbAllView;
        private System.Windows.Forms.CheckBox chkbAllDelete;
        private System.Windows.Forms.CheckBox chkbAllEdit;
        private System.Windows.Forms.CheckBox chkbAllAdd;
        private System.Windows.Forms.CheckBox SelectAllViewRights;
        private System.Windows.Forms.CheckBox SelectAllViewSpecialRights;
        private System.Windows.Forms.CheckBox SelectAllEditSpecialRights;
        private System.Windows.Forms.Label lblUser1;
        private System.Windows.Forms.Label lblUser2;
        private System.Windows.Forms.Label lblUser3;
    }
}