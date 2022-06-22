namespace RestaurantPOS.Reports.PurchaseReports
{
    partial class RptPurchasePayablesJournal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptPurchasePayablesJournal));
            this.DateRangesEnd = new System.Windows.Forms.ComboBox();
            this.DateRangesStart = new System.Windows.Forms.ComboBox();
            this.PurchaseStatuscb = new System.Windows.Forms.ComboBox();
            this.toNum = new System.Windows.Forms.NumericUpDown();
            this.fromNum = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.PEdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.PSdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmployee = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.sdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.pbEmployee = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.toNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // DateRangesEnd
            // 
            this.DateRangesEnd.FormattingEnabled = true;
            this.DateRangesEnd.Items.AddRange(new object[] {
            "Date Range",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.DateRangesEnd.Location = new System.Drawing.Point(550, 18);
            this.DateRangesEnd.Name = "DateRangesEnd";
            this.DateRangesEnd.Size = new System.Drawing.Size(90, 21);
            this.DateRangesEnd.TabIndex = 229;
            this.DateRangesEnd.Text = "Date Range";
            // 
            // DateRangesStart
            // 
            this.DateRangesStart.FormattingEnabled = true;
            this.DateRangesStart.Items.AddRange(new object[] {
            "Date Range",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.DateRangesStart.Location = new System.Drawing.Point(423, 18);
            this.DateRangesStart.Name = "DateRangesStart";
            this.DateRangesStart.Size = new System.Drawing.Size(90, 21);
            this.DateRangesStart.TabIndex = 228;
            this.DateRangesStart.Text = "Date Range";
            // 
            // PurchaseStatuscb
            // 
            this.PurchaseStatuscb.FormattingEnabled = true;
            this.PurchaseStatuscb.Items.AddRange(new object[] {
            "Generals",
            "Sales",
            "Purchases",
            "Disbursements",
            "Receipts",
            "Inventory",
            "All"});
            this.PurchaseStatuscb.Location = new System.Drawing.Point(423, 77);
            this.PurchaseStatuscb.Name = "PurchaseStatuscb";
            this.PurchaseStatuscb.Size = new System.Drawing.Size(216, 21);
            this.PurchaseStatuscb.TabIndex = 227;
            this.PurchaseStatuscb.Text = "Purchases";
            // 
            // toNum
            // 
            this.toNum.AllowDrop = true;
            this.toNum.InterceptArrowKeys = false;
            this.toNum.Location = new System.Drawing.Point(219, 47);
            this.toNum.Name = "toNum";
            this.toNum.Size = new System.Drawing.Size(90, 20);
            this.toNum.TabIndex = 225;
            // 
            // fromNum
            // 
            this.fromNum.Location = new System.Drawing.Point(92, 47);
            this.fromNum.Name = "fromNum";
            this.fromNum.Size = new System.Drawing.Size(90, 20);
            this.fromNum.TabIndex = 224;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(332, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 222;
            this.label11.Text = "Purchase Status :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(518, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 221;
            this.label9.Text = "To :";
            // 
            // PEdateTimePicker
            // 
            this.PEdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PEdateTimePicker.Location = new System.Drawing.Point(550, 45);
            this.PEdateTimePicker.Name = "PEdateTimePicker";
            this.PEdateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.PEdateTimePicker.TabIndex = 220;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(338, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 219;
            this.label10.Text = "Promised Date :";
            // 
            // PSdateTimePicker
            // 
            this.PSdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PSdateTimePicker.Location = new System.Drawing.Point(423, 45);
            this.PSdateTimePicker.Name = "PSdateTimePicker";
            this.PSdateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.PSdateTimePicker.TabIndex = 218;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 215;
            this.label5.Text = "Employees :";
            // 
            // txtEmployee
            // 
            this.txtEmployee.Location = new System.Drawing.Point(92, 75);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(213, 20);
            this.txtEmployee.TabIndex = 214;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 213;
            this.label4.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 212;
            this.label3.Text = "Amount from :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 211;
            this.label2.Text = "To :";
            // 
            // edateTimePicker
            // 
            this.edateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edateTimePicker.Location = new System.Drawing.Point(219, 16);
            this.edateTimePicker.Name = "edateTimePicker";
            this.edateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.edateTimePicker.TabIndex = 210;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 209;
            this.label1.Text = "Date from :";
            // 
            // sdateTimePicker
            // 
            this.sdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdateTimePicker.Location = new System.Drawing.Point(92, 16);
            this.sdateTimePicker.Name = "sdateTimePicker";
            this.sdateTimePicker.Size = new System.Drawing.Size(90, 20);
            this.sdateTimePicker.TabIndex = 208;
            // 
            // pbEmployee
            // 
            this.pbEmployee.Image = ((System.Drawing.Image)(resources.GetObject("pbEmployee.Image")));
            this.pbEmployee.Location = new System.Drawing.Point(309, 77);
            this.pbEmployee.Name = "pbEmployee";
            this.pbEmployee.Size = new System.Drawing.Size(19, 19);
            this.pbEmployee.TabIndex = 230;
            this.pbEmployee.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::RestaurantPOS.Properties.Resources.clear32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(523, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 51);
            this.btnCancel.TabIndex = 223;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplay.Image")));
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(396, 128);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(116, 51);
            this.btnDisplay.TabIndex = 207;
            this.btnDisplay.Text = "Generate";
            this.btnDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // RptPurchasePayablesJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 194);
            this.Controls.Add(this.pbEmployee);
            this.Controls.Add(this.DateRangesEnd);
            this.Controls.Add(this.DateRangesStart);
            this.Controls.Add(this.PurchaseStatuscb);
            this.Controls.Add(this.toNum);
            this.Controls.Add(this.fromNum);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.PEdateTimePicker);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.PSdateTimePicker);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sdateTimePicker);
            this.Controls.Add(this.btnDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RptPurchasePayablesJournal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customiser - Purchases & Payables Journal";
            ((System.ComponentModel.ISupportInitialize)(this.toNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbEmployee;
        private System.Windows.Forms.ComboBox DateRangesEnd;
        private System.Windows.Forms.ComboBox DateRangesStart;
        private System.Windows.Forms.ComboBox PurchaseStatuscb;
        private System.Windows.Forms.NumericUpDown toNum;
        private System.Windows.Forms.NumericUpDown fromNum;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker PEdateTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker PSdateTimePicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmployee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker edateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker sdateTimePicker;
        private System.Windows.Forms.Button btnDisplay;
    }
}