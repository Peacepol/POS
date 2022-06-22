using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitchenDisplay
{
    public partial class KitchenDisplay : Form
    {
        private CoProForm CoPro = null;

        public KitchenDisplay()
        {
            InitializeComponent();
        }

        private void KitchenDisplay_Load(object sender, EventArgs e)
        {
            Hide();
            CoPro = new CoProForm(this);
            CoPro.ShowDialog();
            LoadNewOrders();
            LoadOnProgress();
            timer1.Start(); 
        }
        void LoadNewOrders()
        {
            flowNewOrders.Controls.Clear();
            string newOrdersql = @"SELECT s.TableNumber,sm.ShippingMethod,s.SalesID,s.TransactionDate FROM Sales s LEFT JOIN ShippingMethods sm on s.ShippingMethodID = sm.ShippingID WHERE OrderStatus = 'New'";//arrange by time
            DataTable dtNewOrder = new DataTable();

            CommonClass.runSql(ref dtNewOrder, newOrdersql);
            if (dtNewOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtNewOrder.Rows.Count; i++)
                {
                    //Panel Per Order
                    int PanelHeight = 50;
                    DataRow dr = dtNewOrder.Rows[i];
                    Panel newPanel = new Panel();
                    newPanel.Size = new Size(400, PanelHeight);//(width,Height)
                    newPanel.BackColor = Color.Bisque;
                   //flowNewOrders.Controls.Add(newPanel);

                    Label TableNum = new Label();
                    TableNum.Size = new Size(400, 20);
                    PanelHeight += 50;
                    TableNum.Text = "Table Number: " + dr["TableNumber"].ToString();
                    newPanel.Controls.Add(TableNum);

                    Label TimeOrder = new Label();
                    TimeOrder.Top = 25;
                    TimeOrder.Left = 5;
                    TimeOrder.Size = new Size(100, 20);
                    TimeOrder.Text = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToLocalTime().ToShortTimeString();
                    newPanel.Controls.Add(TimeOrder);

                    Label ShippingStatus = new Label();
                    ShippingStatus.Top = 25;
                    ShippingStatus.Left = 340;
                    ShippingStatus.Size = new Size(100, 20);
                    ShippingStatus.Text = dr["ShippingMethod"].ToString();
                    newPanel.Controls.Add(ShippingStatus);

                    //DataGridView in Panel per Order
                    int dgHeight = 50;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    DataGridView dgv = new DataGridView();
                    dgv.Top += 50;
                    var col0 = new DataGridViewTextBoxColumn();
                    var col1 = new DataGridViewTextBoxColumn();
                 //   var col2 = new DataGridViewButtonColumn();
                    dgv.RowHeadersVisible = false;
                    dgv.ColumnHeadersVisible = false;
                    dgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
                    dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(202)))), ((int)(((byte)(93)))));
                   // col2.DefaultCellStyle = dataGridViewCellStyle;
                    dgv.Columns.AddRange(new DataGridViewColumn[] { col0,col1});
                    col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                  //  col2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    col0.Visible = false;
                    dgv.RowTemplate.Height = 50;// heigth per row
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                   // dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_CellContentClick);
                    dgv.ReadOnly = true;
                    // dgv.Size = new Size(400, dgHeight);
                   

                    //Order ITems
                    string salesLinesql = @"SELECT Description,ShipQty,SalesLineID FROM SalesLines WHERE KitchenStatus= 'New' AND SalesID = '" + dr["SalesID"].ToString() +"'" ;
                    DataTable dtsl = new DataTable();
                    CommonClass.runSql(ref dtsl, salesLinesql);
                    if (dtsl.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtsl.Rows.Count; x++)
                        {
                            DataRow dsr = dtsl.Rows[x];
                            string[] RowArray;
                            RowArray = new string[3];
                            RowArray[0] = dsr["SalesLineID"].ToString();
                            RowArray[1] = dsr["ShipQty"].ToString() + " " + dsr["Description"].ToString();
                            //RowArray[2] = "In Progress";
                            dgv.Rows.Add(RowArray);
                            PanelHeight += 50;
                            dgHeight += 50;
                        }
                        flowNewOrders.Controls.Add(newPanel);
                    }
                   
                    newPanel.Size = new Size(400, PanelHeight+10);
                    dgv.Size = new Size(400, dgHeight-50);
                    newPanel.Controls.Add(dgv);//Display Grid
                    dgv.ClearSelection();   
                    Button btnInProg = new Button();
                    btnInProg.Size = new Size(400, 50);
                    btnInProg.Top = dgHeight + 5;
                    btnInProg.Text = "In Progress";
                    btnInProg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(202)))), ((int)(((byte)(93)))));
                    btnInProg.Click += new EventHandler(btnInProg_Click);
                    btnInProg.Name = dr["SalesID"].ToString();
                    btnInProg.Tag = dr["SalesID"].ToString();
                    newPanel.Controls.Add(btnInProg);
                }
            }
        }
        void LoadOnProgress()
        {
            flowOnProgress.Controls.Clear();
            string newOrdersql = @"SELECT s.TableNumber,sm.ShippingMethod,s.SalesID,s.TransactionDate FROM Sales s 
                LEFT JOIN ShippingMethods sm on s.ShippingMethodID = sm.ShippingID WHERE OrderStatus = 'InProgress'";//arrange by time
            DataTable dtNewOrder = new DataTable();


            CommonClass.runSql(ref dtNewOrder, newOrdersql);
            if (dtNewOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtNewOrder.Rows.Count; i++)
                {
                    //Panel Per Order
                    int PanelHeight = 50;
                    DataRow dr = dtNewOrder.Rows[i];
                    Panel newPanel = new Panel();
                    newPanel.Size = new Size(400, PanelHeight);//(width,Height)
                    newPanel.BackColor = Color.DarkTurquoise;
                   

                    Label TableNum = new Label();
                    TableNum.Size = new Size(400, 20);
                    PanelHeight += 50;
                    TableNum.Text = "Table Number: " + dr["TableNumber"].ToString();
                    newPanel.Controls.Add(TableNum);

                    Label TimeOrder = new Label();
                    TimeOrder.Top = 25;
                    TimeOrder.Left = 5;
                    TimeOrder.Size = new Size(100, 20);
                    TimeOrder.Text = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToLocalTime().ToShortTimeString();
                    newPanel.Controls.Add(TimeOrder);

                    Label ShippingStatus = new Label();
                    ShippingStatus.Top = 25;
                    ShippingStatus.Left = 340;
                    ShippingStatus.Size = new Size(100, 20);
                    ShippingStatus.Text = dr["ShippingMethod"].ToString();
                    newPanel.Controls.Add(ShippingStatus);

                    //DataGridView in Panel per Order
                    int dgHeight = 50;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    DataGridView dgv = new DataGridView();
                    dgv.Top += 50;
                    var col0 = new DataGridViewTextBoxColumn();
                    var col1 = new DataGridViewTextBoxColumn();
                    var col2 = new DataGridViewButtonColumn();
                    dgv.RowHeadersVisible = false;
                    dgv.ColumnHeadersVisible = false;
                    dgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
                    dataGridViewCellStyle.BackColor = Color.LightCoral;
                    col2.DefaultCellStyle = dataGridViewCellStyle;
                    dgv.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2 });
                    col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                    col2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    col0.Visible = false;
                    dgv.RowTemplate.Height = 50;// heigth per row
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_CellContentClick);
                    // dgv.Size = new Size(400, dgHeight);

                    //Order ITems
                    string salesLinesql = @"SELECT Description,ShipQty,SalesLineID FROM SalesLines WHERE KitchenStatus= 'InProgress' AND SalesID = '" + dr["SalesID"].ToString() + "'";
                    DataTable dtsl = new DataTable();
                    CommonClass.runSql(ref dtsl, salesLinesql);
                    if (dtsl.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtsl.Rows.Count; x++)
                        {
                            DataRow dsr = dtsl.Rows[x];
                            string[] RowArray;
                            RowArray = new string[3];
                            RowArray[0] = dsr["SalesLineID"].ToString();
                            RowArray[1] = dsr["ShipQty"].ToString() + " " + dsr["Description"].ToString();
                            RowArray[2] = "Ready";
                            dgv.Rows.Add(RowArray);
                            PanelHeight += 50;
                            dgHeight += 50;
                        }
                        flowOnProgress.Controls.Add(newPanel);
                       
                    }
                    else
                    {
                      
                    }
                   
                    newPanel.Size = new Size(400, PanelHeight - 30);
                    dgv.Size = new Size(400, dgHeight - 50);
                    newPanel.Controls.Add(dgv);//Display Grid
                    dgv.ClearSelection();
                }
            }
        }
        void LoadReady()
        {
            flowReady.Controls.Clear();
            string newOrdersql = @"SELECT s.TableNumber,sm.ShippingMethod,s.SalesID,s.TransactionDate FROM Sales s 
                LEFT JOIN ShippingMethods sm on s.ShippingMethodID = sm.ShippingID WHERE OrderStatus in ('InProgress', 'Ready')";//arrange by time
            DataTable dtNewOrder = new DataTable();

            CommonClass.runSql(ref dtNewOrder, newOrdersql);
            if (dtNewOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtNewOrder.Rows.Count; i++)
                {
                    //Panel Per Order
                    int PanelHeight = 50;
                    DataRow dr = dtNewOrder.Rows[i];
                    Panel newPanel = new Panel();
                    newPanel.Size = new Size(400, PanelHeight);//(width,Height)
                    newPanel.BackColor = Color.LightCoral;
                   // flowReady.Controls.Add(newPanel);

                    Label TableNum = new Label();
                    TableNum.Size = new Size(400, 20);
                    PanelHeight += 50;
                    TableNum.Text = "Table Number: " + dr["TableNumber"].ToString();
                    newPanel.Controls.Add(TableNum);

                    Label TimeOrder = new Label();
                    TimeOrder.Top = 25;
                    TimeOrder.Left = 5;
                    TimeOrder.Size = new Size(100, 20);
                    TimeOrder.Text = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToLocalTime().ToShortTimeString();
                    newPanel.Controls.Add(TimeOrder);

                    Label ShippingStatus = new Label();
                    ShippingStatus.Top = 25;
                    ShippingStatus.Left = 340;
                    ShippingStatus.Size = new Size(100, 20);
                    ShippingStatus.Text = dr["ShippingMethod"].ToString();
                    newPanel.Controls.Add(ShippingStatus);

                    //DataGridView in Panel per Order
                    int dgHeight = 50;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    DataGridView dgvReady = new DataGridView();
                    dgvReady.Top += 50;
                    var col0 = new DataGridViewTextBoxColumn();
                    var col1 = new DataGridViewTextBoxColumn();
                    var col2 = new DataGridViewButtonColumn();
                    dgvReady.RowHeadersVisible = false;
                    dgvReady.ColumnHeadersVisible = false;
                    dgvReady.ScrollBars = System.Windows.Forms.ScrollBars.None;
                    dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(202)))), ((int)(((byte)(93)))));
                    col2.DefaultCellStyle = dataGridViewCellStyle;
                    dgvReady.Columns.AddRange(new DataGridViewColumn[] { col0, col1, col2 });
                    col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                    col2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    col0.Visible = false;
                    dgvReady.RowTemplate.Height = 50;// heigth per row
                    dgvReady.AllowUserToAddRows = false;
                    dgvReady.AllowUserToDeleteRows = false;
                    dgvReady.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvReady_CellContentClick);
                    // dgv.Size = new Size(400, dgHeight);
                    
                    //Order ITems
                    string salesLinesql = @"SELECT Description,ShipQty,SalesLineID FROM SalesLines WHERE KitchenStatus= 'Ready' AND SalesID = '" + dr["SalesID"].ToString() + "'";
                    DataTable dtsl = new DataTable();
                    CommonClass.runSql(ref dtsl, salesLinesql);
                    if (dtsl.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtsl.Rows.Count; x++)
                        {
                            DataRow dsr = dtsl.Rows[x];
                            string[] RowArray;
                            RowArray = new string[3];
                            RowArray[0] = dsr["SalesLineID"].ToString();
                            RowArray[1] = dsr["ShipQty"].ToString() + " " + dsr["Description"].ToString();
                            RowArray[2] = "Served";
                            dgvReady.Rows.Add(RowArray);
                            PanelHeight += 50;
                            dgHeight += 50;
                        }
                        flowReady.Controls.Add(newPanel);
                    }
                    else
                    {
                    }
                   
                    newPanel.Size = new Size(400, PanelHeight - 30);
                    dgvReady.Size = new Size(400, dgHeight - 50);
                    newPanel.Controls.Add(dgvReady);//Display Grid
                    dgvReady.ClearSelection();
                }
            }
        }
        void LoadServed()
        {
            flowServed.Controls.Clear();
            string newOrdersql = @"SELECT s.TableNumber,sm.ShippingMethod,s.SalesID,s.TransactionDate FROM Sales s 
                LEFT JOIN ShippingMethods sm on s.ShippingMethodID = sm.ShippingID WHERE OrderStatus in ('Served', 'Ready','InProgress')";//arrange by time
            DataTable dtNewOrder = new DataTable();

            CommonClass.runSql(ref dtNewOrder, newOrdersql);
            if (dtNewOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtNewOrder.Rows.Count; i++)
                {
                    //Panel Per Order
                    int PanelHeight = 50;
                    DataRow dr = dtNewOrder.Rows[i];
                    Panel newPanel = new Panel();
                    newPanel.Size = new Size(400, PanelHeight);//(width,Height)
                    newPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(202)))), ((int)(((byte)(93)))));

                    Label TableNum = new Label();
                    TableNum.Size = new Size(400, 20);
                    PanelHeight += 50;
                    TableNum.Text = "Table Number: " + dr["TableNumber"].ToString();
                    newPanel.Controls.Add(TableNum);

                    Label TimeOrder = new Label();
                    TimeOrder.Top = 25;
                    TimeOrder.Left = 5;
                    TimeOrder.Size = new Size(100, 20);
                    TimeOrder.Text = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToLocalTime().ToShortTimeString();
                    newPanel.Controls.Add(TimeOrder);

                    Label ShippingStatus = new Label();
                    ShippingStatus.Top = 25;
                    ShippingStatus.Left = 340;
                    ShippingStatus.Size = new Size(100, 20);
                    ShippingStatus.Text = dr["ShippingMethod"].ToString();
                    newPanel.Controls.Add(ShippingStatus);

                    //DataGridView in Panel per Order
                    int dgHeight = 50;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    DataGridView dgvReady = new DataGridView();
                    dgvReady.Top += 50;
                    var col0 = new DataGridViewTextBoxColumn();
                    var col1 = new DataGridViewTextBoxColumn();
                  //  var col2 = new DataGridViewButtonColumn();
                    dgvReady.RowHeadersVisible = false;
                    dgvReady.ColumnHeadersVisible = false;
                    dgvReady.ScrollBars = System.Windows.Forms.ScrollBars.None;
                    dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(202)))), ((int)(((byte)(93)))));
                  //  col2.DefaultCellStyle = dataGridViewCellStyle;
                    dgvReady.Columns.AddRange(new DataGridViewColumn[] { col0, col1});
                    col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                   // col2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    col0.Visible = false;
                    dgvReady.RowTemplate.Height = 50;// heigth per row
                    dgvReady.AllowUserToAddRows = false;
                    dgvReady.AllowUserToDeleteRows = false;
                    dgvReady.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvReady_CellContentClick);
                    // dgv.Size = new Size(400, dgHeight);

                    //Order ITems
                    string salesLinesql = @"SELECT Description,ShipQty,SalesLineID FROM SalesLines WHERE KitchenStatus= 'Served' AND SalesID = '" + dr["SalesID"].ToString() + "'";
                    DataTable dtsl = new DataTable();
                    CommonClass.runSql(ref dtsl, salesLinesql);
                    if (dtsl.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtsl.Rows.Count; x++)
                        {
                            DataRow dsr = dtsl.Rows[x];
                            string[] RowArray;
                            RowArray = new string[3];
                            RowArray[0] = dsr["SalesLineID"].ToString();
                            RowArray[1] = dsr["ShipQty"].ToString() + " " + dsr["Description"].ToString();
                           // RowArray[2] = "Served";
                            dgvReady.Rows.Add(RowArray);
                            PanelHeight += 50;
                            dgHeight += 50;
                        }
                        flowServed.Controls.Add(newPanel);
                    }
                    else
                    {

                    }
                   
                    dgvReady.Size = new Size(400, dgHeight - 50);
                    newPanel.Controls.Add(dgvReady);//Display Grid
                    dgvReady.ClearSelection();
                    if (!CheckAllItemKitchenStatus(dr["SalesID"].ToString()))
                    {
                        newPanel.Size = new Size(400, PanelHeight - 30);
                    }
                    else
                    {
                        
                        newPanel.Size = new Size(400, PanelHeight + 10);
                        Button btnComplete = new Button();
                        btnComplete.Size = new Size(400, 50);
                        btnComplete.Top = dgHeight + 5;
                        btnComplete.Text = "Order Completed";
                        btnComplete.BackColor = Color.SandyBrown;
                        btnComplete.Click += new EventHandler(btnComplete_Click);
                        // btnInProg.Name = dr["SalesID"].ToString();
                        btnComplete.Tag = dr["SalesID"].ToString();
                       
                        newPanel.Controls.Add(btnComplete);

                    }
                }
            }
        }

        void LoadComplete()
        {
            flowComplete.Controls.Clear();
            string newOrdersql = @"SELECT s.TableNumber,sm.ShippingMethod,s.SalesID,s.TransactionDate FROM Sales s 
                LEFT JOIN ShippingMethods sm on s.ShippingMethodID = sm.ShippingID WHERE OrderStatus ='Completed'";//arrange by time
            DataTable dtNewOrder = new DataTable();

            CommonClass.runSql(ref dtNewOrder, newOrdersql);
            if (dtNewOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtNewOrder.Rows.Count; i++)
                {
                    //Panel Per Order
                    int PanelHeight = 50;
                    DataRow dr = dtNewOrder.Rows[i];
                    Panel newPanel = new Panel();
                    newPanel.Size = new Size(400, PanelHeight);//(width,Height)
                    newPanel.BackColor =Color.SandyBrown;

                    Label TableNum = new Label();
                    TableNum.Size = new Size(400, 20);
                    PanelHeight += 50;
                    TableNum.Text = "Table Number: " + dr["TableNumber"].ToString();
                    newPanel.Controls.Add(TableNum);

                    Label TimeOrder = new Label();
                    TimeOrder.Top = 25;
                    TimeOrder.Left = 5;
                    TimeOrder.Size = new Size(100, 20);
                    TimeOrder.Text = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToLocalTime().ToShortTimeString();
                    newPanel.Controls.Add(TimeOrder);

                    Label ShippingStatus = new Label();
                    ShippingStatus.Top = 25;
                    ShippingStatus.Left = 340;
                    ShippingStatus.Size = new Size(100, 20);
                    ShippingStatus.Text = dr["ShippingMethod"].ToString();
                    newPanel.Controls.Add(ShippingStatus);

                    //DataGridView in Panel per Order
                    int dgHeight = 50;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    DataGridView dgvReady = new DataGridView();
                    dgvReady.Top += 50;
                    var col0 = new DataGridViewTextBoxColumn();
                    var col1 = new DataGridViewTextBoxColumn();
                    //  var col2 = new DataGridViewButtonColumn();
                    dgvReady.RowHeadersVisible = false;
                    dgvReady.ColumnHeadersVisible = false;
                    dgvReady.ScrollBars = System.Windows.Forms.ScrollBars.None;
                    dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(202)))), ((int)(((byte)(93)))));
                    //  col2.DefaultCellStyle = dataGridViewCellStyle;
                    dgvReady.Columns.AddRange(new DataGridViewColumn[] { col0, col1 });
                    col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                    // col2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    col0.Visible = false;
                    dgvReady.RowTemplate.Height = 50;// heigth per row
                    dgvReady.AllowUserToAddRows = false;
                    dgvReady.AllowUserToDeleteRows = false;
                    dgvReady.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvReady_CellContentClick);
                    // dgv.Size = new Size(400, dgHeight);

                    //Order ITems
                    string salesLinesql = @"SELECT Description,ShipQty,SalesLineID FROM SalesLines WHERE KitchenStatus= 'Served' AND SalesID = '" + dr["SalesID"].ToString() + "'";
                    DataTable dtsl = new DataTable();
                    CommonClass.runSql(ref dtsl, salesLinesql);
                    if (dtsl.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtsl.Rows.Count; x++)
                        {
                            DataRow dsr = dtsl.Rows[x];
                            string[] RowArray;
                            RowArray = new string[3];
                            RowArray[0] = dsr["SalesLineID"].ToString();
                            RowArray[1] = dsr["ShipQty"].ToString() + " " + dsr["Description"].ToString();
                            // RowArray[2] = "Served";
                            dgvReady.Rows.Add(RowArray);
                            PanelHeight += 50;
                            dgHeight += 50;
                        }
                        flowComplete.Controls.Add(newPanel);
                    }
                    else
                    {

                    }

                    dgvReady.Size = new Size(400, dgHeight - 50);
                    newPanel.Controls.Add(dgvReady);//Display Grid
                    dgvReady.ClearSelection();
                    newPanel.Size = new Size(400, PanelHeight - 30);
                }
            }
        }
        private void btnInProg_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.
            MessageBox.Show("Update sales Line to in progress " + ((Button)sender).Tag);
            ChangeOrderStatus("InProgress", ""+((Button)sender).Tag);
            string sqlUpdateSL = @"Update SalesLines Set KitchenStatus = 'InProgress' WHERE SalesID = '" + ((Button)sender).Tag + "'";
            CommonClass.runSql(sqlUpdateSL);
            LoadNewOrders();
        }
        private void btnComplete_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.
            //Check if all item is Ready
            if (CheckAllItemKitchenStatus(""+((Button)sender).Tag))
            {
                ChangeOrderStatus("Completed", "" + ((Button)sender).Tag);
             
                string sqlUpdateSL = @"Update SalesLines Set KitchenStatus = 'Served' WHERE SalesID = '" + ((Button)sender).Tag + "'";
                CommonClass.runSql(sqlUpdateSL);
                LoadServed();
            }
            else
            {
                MessageBox.Show("Order is not complete yet. \n All Orders must be served.");
            }
           
        }
        bool CheckAllItemKitchenStatus(string SalesID)
        {
            string salesLinesql = @"SELECT Description,ShipQty,SalesLineID FROM SalesLines WHERE KitchenStatus in ('New','InProgress','Ready') AND SalesID = '" + SalesID + "'";
            DataTable dtsl = new DataTable();
            CommonClass.runSql(ref dtsl, salesLinesql);
            if (dtsl.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                string sqlUpdateSL = @"Update SalesLines Set KitchenStatus = 'Ready' WHERE SalesLineID = '" + ((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                CommonClass.runSql(sqlUpdateSL);
                LoadOnProgress();
            }
        }
        private void dgvReady_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string sqlUpdateSL = @"Update SalesLines Set KitchenStatus = 'Served' WHERE SalesLineID = '" + ((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                CommonClass.runSql(sqlUpdateSL);
                LoadReady();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    LoadNewOrders();
                    break;
                case 1:
                    LoadOnProgress();
                    break;
                case 2:
                    LoadReady();
                    break;
                case 3:
                    LoadServed();
                    break;
                case 4:
                    LoadComplete();
                    break;
            }
        }
        void ChangeOrderStatus(string newStatus,string salesID)
        {
            string sqlUpdate = @"Update Sales Set OrderStatus = '"+ newStatus + "' WHERE SalesID = '" + salesID + "'";
            CommonClass.runSql(sqlUpdate);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    LoadNewOrders();
                    break;
                case 1:
                    LoadOnProgress();
                    break;
                case 2:
                    LoadReady();
                    break;
                case 3:
                    LoadServed();
                    break;
                case 4:
                    LoadComplete();
                    break;
            }
        }
    }
}
