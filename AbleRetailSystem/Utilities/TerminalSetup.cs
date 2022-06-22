using RestaurantPOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Utilities
{
    public partial class TerminalSetup : Form
    {
        Dictionary<string, object> param = new Dictionary<string, object>();
        DataTable Duplicatedt = new DataTable();
        DataTable dt = new DataTable();
        int TerminalID;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";
        public TerminalSetup()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Add", out outx);
                if (outx == true)
                {
                    CanAdd = true;
                }
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
                outx = false;
                FormRights.TryGetValue("Delete", out outx);
                if (outx == true)
                {
                    CanDelete = true;
                }
            }
            string outy = "";
            CommonClass.AppFormCode.TryGetValue(this.Text, out outy);
            if (outy != null && outy != "")
            {
                thisFormCode = outy;
            }
            else
            {
                thisFormCode = this.Text;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTerminalName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TerminalID == 0)
            {
                if (!IsDuplicate(txtTerminalID.Text))
                {
                    SaveTerminal();
                    btnRefresh.PerformClick();
                }
                else
                {
                    MessageBox.Show("Record Already exists.");
                    btnRefresh.PerformClick();
                }
            }
            else
            {
                UpdatePoint(TerminalID);
                btnRefresh.PerformClick();
            }
        }
        public void UpdatePoint(int pID)
        {
            string sqlUpdate = @"UPDATE Terminal SET TerminalName = @TerminalName WHERE TerminalID = " + pID + "";
            Dictionary<string, object> updateparam = new Dictionary<string, object>();

            updateparam.Add("@TerminalName", txtTerminalName.Text);

            string titles = "Update Terminal Record for Terminal " + TerminalID;
            DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                CommonClass.runSql(sqlUpdate, CommonClass.RunSqlInsertMode.QUERY, updateparam);
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Terminal ID " + TerminalID, TerminalID.ToString());

                MessageBox.Show("Record has been updated.", "INFORMATION");
                btnRefresh.PerformClick();
                LoadTerminal();
            }
        }
        private bool IsDuplicate(string tID)
        {
            string selectSql = "SELECT * FROM Terminal WHERE TerminalID ='" + tID + "'";
            CommonClass.runSql(ref Duplicatedt, selectSql);
            if (Duplicatedt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveTerminal()
        {
            string InsertSql = @"INSERT INTO Terminal (TerminalName) VALUES(@TerminalName)";
                                                       
            param.Add("@TerminalName", txtTerminalName.Text);
            TerminalID = CommonClass.runSql(InsertSql, CommonClass.RunSqlInsertMode.SCALAR, param);
            if (TerminalID > 0)
            {
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New Terminal Name " + txtTerminalName.Text + " with Terminal ID " + TerminalID, TerminalID.ToString());

                string titles = "Information";
                MessageBox.Show("Terminal Record has been created.", titles);
            }
        }
        private void TerminalSetup_Load(object sender, EventArgs e)
        {
            LoadTerminal();            
            formCheck();
            if (dt.Rows.Count >= CommonClass.MaxTerminalAllowed)
            {
                btnAdd.Enabled = false;
            }
        }
        public void LoadTerminal()
        {
            string sql = @"SELECT * FROM Terminal";
            dt = new DataTable();
            CommonClass.runSql(ref dt, sql);
            if (dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dt = dv.ToTable();
                dgTerminal.DataSource = dt;
                //DataRow rw = dt.NewRow();
                //dt.Rows.Add(rw);
                //  dgReport.Columns["PartNumber"].Visible = false;
                FormatGrid();
                foreach (DataGridViewColumn column in dgTerminal.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            else
            {
                MessageBox.Show("No Terminal Found!.", "Terminal Information");
            }
        }
        private void FormatGrid()
        {
            this.dgTerminal.Columns[0].HeaderText = "Terminal ID";
            this.dgTerminal.Columns[1].HeaderText = "Terminal Name";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtTerminalName.Enabled = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            dgTerminal.Enabled = true;
            btnEdit.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (dt.Rows.Count >= CommonClass.MaxTerminalAllowed)
            {
                MessageBox.Show("You have already reached the maximum allowed terminals. Please contact your vendor if you require additional terminals.");
            }
            else
            {
                TerminalID = 0;
                txtTerminalName.Enabled = true;
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTerminal();           
            formCheck();
            if (dt.Rows.Count >= CommonClass.MaxTerminalAllowed)
            {
                btnAdd.Enabled = false;
            }
        }
        public void formCheck()
        {
            txtTerminalName.Text = "";
            txtTerminalName.Enabled = false;

            if (dgTerminal.Rows.Count == 0)
            {
                btnEdit.Enabled = false;
                
            }
            else
            {
                btnEdit.Enabled = CanEdit;
              
            }
           
            btnSave.Enabled = false;
            btnAdd.Enabled = CanAdd;
            dgTerminal.ClearSelection();

        }

        private void dgTerminal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadSelectedTerminal();
            
        }

        private void LoadSelectedTerminal()
        {
            //dgTerminal.ClearSelection();

            if (dgTerminal.Rows.Count > 0)
            {
                txtTerminalName.Text = dgTerminal.CurrentRow.Cells[1].Value.ToString();
                string terminalID = dgTerminal.CurrentRow.Cells[0].Value.ToString();
                TerminalID = terminalID != "" ? Convert.ToInt32(terminalID) : 0;
            }

        }
    }
}
