namespace PredefAcctEditor
{
    partial class Account_List_Form
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
            this.Convert_btn = new System.Windows.Forms.Button();
            this.Display_text = new System.Windows.Forms.RichTextBox();
            this.accountListDataGridView = new System.Windows.Forms.DataGridView();
            this.IndustryClass_box = new System.Windows.Forms.ComboBox();
            this.type0fBusiness_box = new System.Windows.Forms.ComboBox();
            this.Industry_Classification = new System.Windows.Forms.Label();
            this.Type_of_Business = new System.Windows.Forms.Label();
            this.saveFileJSON = new System.Windows.Forms.SaveFileDialog();
            this.Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.accountListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Convert_btn
            // 
            this.Convert_btn.Location = new System.Drawing.Point(603, 47);
            this.Convert_btn.Name = "Convert_btn";
            this.Convert_btn.Size = new System.Drawing.Size(75, 42);
            this.Convert_btn.TabIndex = 1;
            this.Convert_btn.Text = "Convert to Json";
            this.Convert_btn.UseVisualStyleBackColor = true;
            this.Convert_btn.Click += new System.EventHandler(this.Convert_to_Json_Button);
            // 
            // Display_text
            // 
            this.Display_text.Location = new System.Drawing.Point(698, 34);
            this.Display_text.Name = "Display_text";
            this.Display_text.Size = new System.Drawing.Size(307, 304);
            this.Display_text.TabIndex = 2;
            this.Display_text.Text = "";
            // 
            // accountListDataGridView
            // 
            this.accountListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accountListDataGridView.Location = new System.Drawing.Point(12, 47);
            this.accountListDataGridView.Name = "accountListDataGridView";
            this.accountListDataGridView.Size = new System.Drawing.Size(585, 291);
            this.accountListDataGridView.TabIndex = 3;
            // 
            // IndustryClass_box
            // 
            this.IndustryClass_box.FormattingEnabled = true;
            this.IndustryClass_box.Location = new System.Drawing.Point(149, 20);
            this.IndustryClass_box.Name = "IndustryClass_box";
            this.IndustryClass_box.Size = new System.Drawing.Size(121, 21);
            this.IndustryClass_box.TabIndex = 4;
            this.IndustryClass_box.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // type0fBusiness_box
            // 
            this.type0fBusiness_box.FormattingEnabled = true;
            this.type0fBusiness_box.Location = new System.Drawing.Point(430, 20);
            this.type0fBusiness_box.Name = "type0fBusiness_box";
            this.type0fBusiness_box.Size = new System.Drawing.Size(121, 21);
            this.type0fBusiness_box.TabIndex = 5;
            this.type0fBusiness_box.SelectedIndexChanged += new System.EventHandler(this.type0fBusiness_box_SelectedIndexChanged);
            // 
            // Industry_Classification
            // 
            this.Industry_Classification.AutoSize = true;
            this.Industry_Classification.Location = new System.Drawing.Point(35, 23);
            this.Industry_Classification.Name = "Industry_Classification";
            this.Industry_Classification.Size = new System.Drawing.Size(108, 13);
            this.Industry_Classification.TabIndex = 72;
            this.Industry_Classification.Text = "Industry Classification";
            // 
            // Type_of_Business
            // 
            this.Type_of_Business.AutoSize = true;
            this.Type_of_Business.Location = new System.Drawing.Point(334, 23);
            this.Type_of_Business.Name = "Type_of_Business";
            this.Type_of_Business.Size = new System.Drawing.Size(90, 13);
            this.Type_of_Business.TabIndex = 74;
            this.Type_of_Business.Text = "Type Of Business";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(603, 118);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 42);
            this.Save.TabIndex = 75;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Account_List_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 357);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Type_of_Business);
            this.Controls.Add(this.Industry_Classification);
            this.Controls.Add(this.type0fBusiness_box);
            this.Controls.Add(this.IndustryClass_box);
            this.Controls.Add(this.accountListDataGridView);
            this.Controls.Add(this.Display_text);
            this.Controls.Add(this.Convert_btn);
            this.MaximizeBox = false;
            this.Name = "Account_List_Form";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Account_List_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accountListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Convert_btn;
        private System.Windows.Forms.RichTextBox Display_text;
        private System.Windows.Forms.DataGridView accountListDataGridView;
        private System.Windows.Forms.ComboBox IndustryClass_box;
        private System.Windows.Forms.ComboBox type0fBusiness_box;
        private System.Windows.Forms.Label Industry_Classification;
        private System.Windows.Forms.Label Type_of_Business;
        private System.Windows.Forms.SaveFileDialog saveFileJSON;
        private System.Windows.Forms.Button Save;
    }
}