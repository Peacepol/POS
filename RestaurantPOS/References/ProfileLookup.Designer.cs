namespace AbleRetailPOS
{
    partial class ProfileLookup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileLookup));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.rdoLoyal = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rdoState = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoPhone = new System.Windows.Forms.RadioButton();
            this.rdoCountry = new System.Windows.Forms.RadioButton();
            this.rdoPostCode = new System.Windows.Forms.RadioButton();
            this.rdoEmail = new System.Windows.Forms.RadioButton();
            this.rdoCity = new System.Windows.Forms.RadioButton();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.rdoName = new System.Windows.Forms.RadioButton();
            this.rdoID = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProfileIDNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProfileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Phone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CurrentBalance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Street = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.City = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Postcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Country = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkActive);
            this.groupBox1.Controls.Add(this.rdoLoyal);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.rdoState);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoPhone);
            this.groupBox1.Controls.Add(this.rdoCountry);
            this.groupBox1.Controls.Add(this.rdoPostCode);
            this.groupBox1.Controls.Add(this.rdoEmail);
            this.groupBox1.Controls.Add(this.rdoCity);
            this.groupBox1.Controls.Add(this.txtsearch);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.rdoName);
            this.groupBox1.Controls.Add(this.rdoID);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(648, 9);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(78, 17);
            this.chkActive.TabIndex = 198;
            this.chkActive.Text = "Active only";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // rdoLoyal
            // 
            this.rdoLoyal.AutoSize = true;
            this.rdoLoyal.Location = new System.Drawing.Point(476, 54);
            this.rdoLoyal.Name = "rdoLoyal";
            this.rdoLoyal.Size = new System.Drawing.Size(103, 17);
            this.rdoLoyal.TabIndex = 197;
            this.rdoLoyal.Text = "Member Number";
            this.rdoLoyal.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(79, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 63);
            this.button2.TabIndex = 196;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(36, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 58);
            this.button1.TabIndex = 195;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // rdoState
            // 
            this.rdoState.AutoSize = true;
            this.rdoState.Location = new System.Drawing.Point(273, 54);
            this.rdoState.Name = "rdoState";
            this.rdoState.Size = new System.Drawing.Size(50, 17);
            this.rdoState.TabIndex = 5;
            this.rdoState.Text = "State";
            this.rdoState.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(175, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Search By ";
            // 
            // rdoPhone
            // 
            this.rdoPhone.AutoSize = true;
            this.rdoPhone.Location = new System.Drawing.Point(415, 33);
            this.rdoPhone.Name = "rdoPhone";
            this.rdoPhone.Size = new System.Drawing.Size(96, 17);
            this.rdoPhone.TabIndex = 2;
            this.rdoPhone.Text = "Phone Number";
            this.rdoPhone.UseVisualStyleBackColor = true;
            // 
            // rdoCountry
            // 
            this.rdoCountry.AutoSize = true;
            this.rdoCountry.Location = new System.Drawing.Point(409, 54);
            this.rdoCountry.Name = "rdoCountry";
            this.rdoCountry.Size = new System.Drawing.Size(61, 17);
            this.rdoCountry.TabIndex = 7;
            this.rdoCountry.Text = "Country";
            this.rdoCountry.UseVisualStyleBackColor = true;
            // 
            // rdoPostCode
            // 
            this.rdoPostCode.AutoSize = true;
            this.rdoPostCode.Location = new System.Drawing.Point(329, 54);
            this.rdoPostCode.Name = "rdoPostCode";
            this.rdoPostCode.Size = new System.Drawing.Size(74, 17);
            this.rdoPostCode.TabIndex = 6;
            this.rdoPostCode.Text = "Post Code";
            this.rdoPostCode.UseVisualStyleBackColor = true;
            // 
            // rdoEmail
            // 
            this.rdoEmail.AutoSize = true;
            this.rdoEmail.Location = new System.Drawing.Point(517, 33);
            this.rdoEmail.Name = "rdoEmail";
            this.rdoEmail.Size = new System.Drawing.Size(50, 17);
            this.rdoEmail.TabIndex = 3;
            this.rdoEmail.Text = "Email";
            this.rdoEmail.UseVisualStyleBackColor = true;
            // 
            // rdoCity
            // 
            this.rdoCity.AutoSize = true;
            this.rdoCity.Location = new System.Drawing.Point(573, 33);
            this.rdoCity.Name = "rdoCity";
            this.rdoCity.Size = new System.Drawing.Size(42, 17);
            this.rdoCity.TabIndex = 4;
            this.rdoCity.Text = "City";
            this.rdoCity.UseVisualStyleBackColor = true;
            // 
            // txtsearch
            // 
            this.txtsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(219, 77);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(405, 21);
            this.txtsearch.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label14.Location = new System.Drawing.Point(175, 80);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 56;
            this.label14.Text = "Search";
            // 
            // rdoName
            // 
            this.rdoName.AutoSize = true;
            this.rdoName.Checked = true;
            this.rdoName.Location = new System.Drawing.Point(240, 33);
            this.rdoName.Name = "rdoName";
            this.rdoName.Size = new System.Drawing.Size(53, 17);
            this.rdoName.TabIndex = 0;
            this.rdoName.TabStop = true;
            this.rdoName.Text = "Name";
            this.rdoName.UseVisualStyleBackColor = true;
            // 
            // rdoID
            // 
            this.rdoID.AutoSize = true;
            this.rdoID.Location = new System.Drawing.Point(299, 33);
            this.rdoID.Name = "rdoID";
            this.rdoID.Size = new System.Drawing.Size(108, 17);
            this.rdoID.TabIndex = 1;
            this.rdoID.Text = "Profile ID Number";
            this.rdoID.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(648, 61);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(83, 45);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.ProfileIDNumber,
            this.ProfileName,
            this.Phone,
            this.Email,
            this.CurrentBalance,
            this.Street,
            this.City,
            this.State,
            this.Postcode,
            this.Country});
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(12, 141);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(760, 500);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 0;
            // 
            // ProfileIDNumber
            // 
            this.ProfileIDNumber.Text = "Number";
            this.ProfileIDNumber.Width = 150;
            // 
            // ProfileName
            // 
            this.ProfileName.Text = "Name";
            this.ProfileName.Width = 200;
            // 
            // Phone
            // 
            this.Phone.Text = "Phone";
            this.Phone.Width = 150;
            // 
            // Email
            // 
            this.Email.Text = "Email";
            this.Email.Width = 150;
            // 
            // CurrentBalance
            // 
            this.CurrentBalance.Text = "Balance";
            this.CurrentBalance.Width = 150;
            // 
            // Street
            // 
            this.Street.Width = 0;
            // 
            // City
            // 
            this.City.Width = 0;
            // 
            // State
            // 
            this.State.Width = 0;
            // 
            // Postcode
            // 
            this.Postcode.Width = 0;
            // 
            // ProfileLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ProfileLookup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profile Lookup";
            this.Load += new System.EventHandler(this.ProfileLookup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoPostCode;
        private System.Windows.Forms.RadioButton rdoEmail;
        private System.Windows.Forms.RadioButton rdoCity;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rdoName;
        private System.Windows.Forms.RadioButton rdoID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoPhone;
        private System.Windows.Forms.RadioButton rdoCountry;
        private System.Windows.Forms.RadioButton rdoState;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ProfileName;
        private System.Windows.Forms.ColumnHeader Phone;
        private System.Windows.Forms.ColumnHeader Email;
        private System.Windows.Forms.ColumnHeader CurrentBalance;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColumnHeader ProfileIDNumber;
        private System.Windows.Forms.ColumnHeader Street;
        private System.Windows.Forms.ColumnHeader City;
        private System.Windows.Forms.ColumnHeader State;
        private System.Windows.Forms.ColumnHeader Postcode;
        private System.Windows.Forms.ColumnHeader Country;
        private System.Windows.Forms.RadioButton rdoLoyal;
        private System.Windows.Forms.CheckBox chkActive;
    }
}