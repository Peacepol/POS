namespace KitchenDisplay
{
    partial class KitchenDisplay
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KitchenDisplay));
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabNewOrder = new System.Windows.Forms.TabPage();
            this.flowNewOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.TabProgress = new System.Windows.Forms.TabPage();
            this.flowOnProgress = new System.Windows.Forms.FlowLayoutPanel();
            this.TabReady = new System.Windows.Forms.TabPage();
            this.flowReady = new System.Windows.Forms.FlowLayoutPanel();
            this.TabServed = new System.Windows.Forms.TabPage();
            this.flowServed = new System.Windows.Forms.FlowLayoutPanel();
            this.TabCompleted = new System.Windows.Forms.TabPage();
            this.flowComplete = new System.Windows.Forms.FlowLayoutPanel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TabControl1.SuspendLayout();
            this.TabNewOrder.SuspendLayout();
            this.TabProgress.SuspendLayout();
            this.TabReady.SuspendLayout();
            this.TabServed.SuspendLayout();
            this.TabCompleted.SuspendLayout();
            this.flowComplete.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabNewOrder);
            this.TabControl1.Controls.Add(this.TabProgress);
            this.TabControl1.Controls.Add(this.TabReady);
            this.TabControl1.Controls.Add(this.TabServed);
            this.TabControl1.Controls.Add(this.TabCompleted);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl1.ItemSize = new System.Drawing.Size(150, 50);
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(916, 570);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.TabIndex = 0;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // TabNewOrder
            // 
            this.TabNewOrder.Controls.Add(this.flowNewOrders);
            this.TabNewOrder.Location = new System.Drawing.Point(4, 54);
            this.TabNewOrder.Name = "TabNewOrder";
            this.TabNewOrder.Padding = new System.Windows.Forms.Padding(3);
            this.TabNewOrder.Size = new System.Drawing.Size(908, 512);
            this.TabNewOrder.TabIndex = 0;
            this.TabNewOrder.Text = "New Orders";
            this.TabNewOrder.UseVisualStyleBackColor = true;
            // 
            // flowNewOrders
            // 
            this.flowNewOrders.AutoScroll = true;
            this.flowNewOrders.AutoSize = true;
            this.flowNewOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.flowNewOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNewOrders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowNewOrders.Location = new System.Drawing.Point(3, 3);
            this.flowNewOrders.Name = "flowNewOrders";
            this.flowNewOrders.Size = new System.Drawing.Size(902, 506);
            this.flowNewOrders.TabIndex = 0;
            // 
            // TabProgress
            // 
            this.TabProgress.Controls.Add(this.flowOnProgress);
            this.TabProgress.Location = new System.Drawing.Point(4, 54);
            this.TabProgress.Name = "TabProgress";
            this.TabProgress.Padding = new System.Windows.Forms.Padding(3);
            this.TabProgress.Size = new System.Drawing.Size(908, 512);
            this.TabProgress.TabIndex = 1;
            this.TabProgress.Text = "On Progress";
            this.TabProgress.UseVisualStyleBackColor = true;
            // 
            // flowOnProgress
            // 
            this.flowOnProgress.AutoScroll = true;
            this.flowOnProgress.AutoSize = true;
            this.flowOnProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.flowOnProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowOnProgress.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowOnProgress.Location = new System.Drawing.Point(3, 3);
            this.flowOnProgress.Name = "flowOnProgress";
            this.flowOnProgress.Size = new System.Drawing.Size(902, 506);
            this.flowOnProgress.TabIndex = 1;
            // 
            // TabReady
            // 
            this.TabReady.Controls.Add(this.flowReady);
            this.TabReady.Location = new System.Drawing.Point(4, 54);
            this.TabReady.Name = "TabReady";
            this.TabReady.Size = new System.Drawing.Size(908, 512);
            this.TabReady.TabIndex = 2;
            this.TabReady.Text = "Ready";
            this.TabReady.UseVisualStyleBackColor = true;
            // 
            // flowReady
            // 
            this.flowReady.AutoScroll = true;
            this.flowReady.AutoSize = true;
            this.flowReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.flowReady.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowReady.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowReady.Location = new System.Drawing.Point(0, 0);
            this.flowReady.Name = "flowReady";
            this.flowReady.Size = new System.Drawing.Size(908, 512);
            this.flowReady.TabIndex = 2;
            // 
            // TabServed
            // 
            this.TabServed.Controls.Add(this.flowServed);
            this.TabServed.Location = new System.Drawing.Point(4, 54);
            this.TabServed.Name = "TabServed";
            this.TabServed.Size = new System.Drawing.Size(908, 512);
            this.TabServed.TabIndex = 3;
            this.TabServed.Text = "Served";
            this.TabServed.UseVisualStyleBackColor = true;
            // 
            // flowServed
            // 
            this.flowServed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.flowServed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowServed.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowServed.Location = new System.Drawing.Point(0, 0);
            this.flowServed.Name = "flowServed";
            this.flowServed.Size = new System.Drawing.Size(908, 512);
            this.flowServed.TabIndex = 0;
            // 
            // TabCompleted
            // 
            this.TabCompleted.Controls.Add(this.flowComplete);
            this.TabCompleted.Location = new System.Drawing.Point(4, 54);
            this.TabCompleted.Name = "TabCompleted";
            this.TabCompleted.Size = new System.Drawing.Size(908, 512);
            this.TabCompleted.TabIndex = 4;
            this.TabCompleted.Text = "Completed";
            this.TabCompleted.UseVisualStyleBackColor = true;
            // 
            // flowComplete
            // 
            this.flowComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.flowComplete.Controls.Add(this.dateTimePicker1);
            this.flowComplete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowComplete.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowComplete.Location = new System.Drawing.Point(0, 0);
            this.flowComplete.Name = "flowComplete";
            this.flowComplete.Size = new System.Drawing.Size(908, 512);
            this.flowComplete.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // KitchenDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(916, 570);
            this.Controls.Add(this.TabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KitchenDisplay";
            this.Text = "Able Retail POS - [Kitchen Display]";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.KitchenDisplay_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabNewOrder.ResumeLayout(false);
            this.TabNewOrder.PerformLayout();
            this.TabProgress.ResumeLayout(false);
            this.TabProgress.PerformLayout();
            this.TabReady.ResumeLayout(false);
            this.TabReady.PerformLayout();
            this.TabServed.ResumeLayout(false);
            this.TabCompleted.ResumeLayout(false);
            this.flowComplete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage TabNewOrder;
        private System.Windows.Forms.TabPage TabProgress;
        private System.Windows.Forms.TabPage TabReady;
        private System.Windows.Forms.TabPage TabServed;
        private System.Windows.Forms.FlowLayoutPanel flowNewOrders;
        private System.Windows.Forms.FlowLayoutPanel flowOnProgress;
        private System.Windows.Forms.FlowLayoutPanel flowReady;
        private System.Windows.Forms.FlowLayoutPanel flowServed;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage TabCompleted;
        private System.Windows.Forms.FlowLayoutPanel flowComplete;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}

