namespace AbleRetailPOS.Reports.PurchaseReports
{
    partial class OpenBillsOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenBillsOrder));
            this.PurchaseStatuscb = new System.Windows.Forms.ComboBox();
            this.pbCustomer = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtShipVia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.sdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.toNum = new System.Windows.Forms.NumericUpDown();
            this.fromNum = new System.Windows.Forms.NumericUpDown();
            this.DateRangesEnd = new System.Windows.Forms.ComboBox();
            this.DateRangesStart = new System.Windows.Forms.ComboBox();
            this.PEdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.PSdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.pbEmployee = new System.Windows.Forms.PictureBox();
            this.txtEmployee = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // PurchaseStatuscb
            // 
            this.PurchaseStatuscb.FormattingEnabled = true;
            this.PurchaseStatuscb.Items.AddRange(new object[] {
            "All Bills",
            "Orders",
            "Both"});
            this.PurchaseStatuscb.Location = new System.Drawing.Point(421, 80);
            this.PurchaseStatuscb.Name = "PurchaseStatuscb";
            this.PurchaseStatuscb.Size = new System.Drawing.Size(216, 21);
            this.PurchaseStatuscb.TabIndex = 253;
            this.PurchaseStatuscb.Text = "Both";
            // 
            // pbCustomer
            // 
            this.pbCustomer.Image = ((System.Drawing.Image)(resources.GetObject("pbCustomer.Image")));
            this.pbCustomer.Location = new System.Drawing.Point(309, 105);
            this.pbCustomer.Name = "pbCustomer";
            this.pbCustomer.Size = new System.Drawing.Size(19, 19);
            this.pbCustomer.TabIndex = 252;
            this.pbCustomer.TabStop = false;
            this.pbCustomer.Click += new System.EventHandler(this.pbCustomer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::AbleRetailPOS.Properties.Resources.clear32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(521, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 51);
            this.btnCancel.TabIndex = 249;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(330, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 248;
            this.label11.Text = "Purchase Status :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(516, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 247;
            this.label9.Text = "To :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(336, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 245;
            this.label10.Text = "Promised Date :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 243;
            this.label8.Text = "Ship via :";
            // 
            // txtShipVia
            // 
            this.txtShipVia.Location = new System.Drawing.Point(90, 104);
            this.txtShipVia.Name = "txtShipVia";
            this.txtShipVia.Size = new System.Drawing.Size(213, 20);
            this.txtShipVia.TabIndex = 242;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 237;
            this.label5.Text = "Employees :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 235;
            this.label4.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 234;
            this.label3.Text = "Amount from :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 233;
            this.label2.Text = "To :";
            // 
            // edateTimePicker
            // 
            this.edateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edateTimePicker.Location = new System.Drawing.Point(217, 19);
            this.edateTimePicker.Name = "edateTimePicker";
            this.edateTimePicker.Size = new System.Drawing.Size(86, 20);
            this.edateTimePicker.TabIndex = 232;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 231;
            this.label1.Text = "Date from :";
            // 
            // sdateTimePicker
            // 
            this.sdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.sdateTimePicker.Location = new System.Drawing.Point(90, 19);
            this.sdateTimePicker.Name = "sdateTimePicker";
            this.sdateTimePicker.Size = new System.Drawing.Size(86, 20);
            this.sdateTimePicker.TabIndex = 230;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Image = global::AbleRetailPOS.Properties.Resources.refresh32;
            this.btnDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplay.Location = new System.Drawing.Point(394, 131);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(116, 51);
            this.btnDisplay.TabIndex = 229;
            this.btnDisplay.Text = "Generate";
            this.btnDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // toNum
            // 
            this.toNum.Location = new System.Drawing.Point(214, 51);
            this.toNum.Name = "toNum";
            this.toNum.Size = new System.Drawing.Size(89, 20);
            this.toNum.TabIndex = 255;
            // 
            // fromNum
            // 
            this.fromNum.Location = new System.Drawing.Point(90, 50);
            this.fromNum.Name = "fromNum";
            this.fromNum.Size = new System.Drawing.Size(89, 20);
            this.fromNum.TabIndex = 254;
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
            this.DateRangesEnd.Location = new System.Drawing.Point(548, 18);
            this.DateRangesEnd.Name = "DateRangesEnd";
            this.DateRangesEnd.Size = new System.Drawing.Size(89, 21);
            this.DateRangesEnd.TabIndex = 259;
            this.DateRangesEnd.Text = "Date Range";
            this.DateRangesEnd.SelectedIndexChanged += new System.EventHandler(this.DateRangesEnd_SelectedIndexChanged);
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
            this.DateRangesStart.Location = new System.Drawing.Point(421, 21);
            this.DateRangesStart.Name = "DateRangesStart";
            this.DateRangesStart.Size = new System.Drawing.Size(89, 21);
            this.DateRangesStart.TabIndex = 258;
            this.DateRangesStart.Text = "Date Range";
            this.DateRangesStart.SelectedIndexChanged += new System.EventHandler(this.DateRangesStart_SelectedIndexChanged);
            // 
            // PEdateTimePicker
            // 
            this.PEdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PEdateTimePicker.Location = new System.Drawing.Point(548, 48);
            this.PEdateTimePicker.Name = "PEdateTimePicker";
            this.PEdateTimePicker.Size = new System.Drawing.Size(89, 20);
            this.PEdateTimePicker.TabIndex = 257;
            // 
            // PSdateTimePicker
            // 
            this.PSdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PSdateTimePicker.Location = new System.Drawing.Point(421, 48);
            this.PSdateTimePicker.Name = "PSdateTimePicker";
            this.PSdateTimePicker.Size = new System.Drawing.Size(89, 20);
            this.PSdateTimePicker.TabIndex = 256;
            // 
            // pbEmployee
            // 
            this.pbEmployee.Image = ((System.Drawing.Image)(resources.GetObject("pbEmployee.Image")));
            this.pbEmployee.Location = new System.Drawing.Point(309, 80);
            this.pbEmployee.Name = "pbEmployee";
            this.pbEmployee.Size = new System.Drawing.Size(19, 19);
            this.pbEmployee.TabIndex = 261;
            this.pbEmployee.TabStop = false;
            this.pbEmployee.Click += new System.EventHandler(this.pbEmployee_Click);
            // 
            // txtEmployee
            // 
            this.txtEmployee.Location = new System.Drawing.Point(90, 78);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(213, 20);
            this.txtEmployee.TabIndex = 260;
            // 
            // OpenBillsOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 194);
            this.Controls.Add(this.pbEmployee);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.DateRangesEnd);
            this.Controls.Add(this.DateRangesStart);
            this.Controls.Add(this.PEdateTimePicker);
            this.Controls.Add(this.PSdateTimePicker);
            this.Controls.Add(this.toNum);
            this.Controls.Add(this.fromNum);
            this.Controls.Add(this.PurchaseStatuscb);
            this.Controls.Add(this.pbCustomer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtShipVia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sdateTimePicker);
            this.Controls.Add(this.btnDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpenBillsOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Customiser - Open Bills";
            this.Load += new System.EventHandler(this.OpenBillsOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PurchaseStatuscb;
        private System.Windows.Forms.PictureBox pbCustomer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtShipVia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker edateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker sdateTimePicker;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.NumericUpDown toNum;
        private System.Windows.Forms.NumericUpDown fromNum;
        private System.Windows.Forms.ComboBox DateRangesEnd;
        private System.Windows.Forms.ComboBox DateRangesStart;
        private System.Windows.Forms.DateTimePicker PEdateTimePicker;
        private System.Windows.Forms.DateTimePicker PSdateTimePicker;
        private System.Windows.Forms.PictureBox pbEmployee;
        private System.Windows.Forms.TextBox txtEmployee;
    }
}