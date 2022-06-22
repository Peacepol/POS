using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Setup
{
    public partial class PointsExchangeRate : Form
    {
        DataTable Duplicatedt = new DataTable();
        DataTable dt = new DataTable();
        int PointsID;
        private bool CanAdd = false;
        private bool CanEdit = false;
        private bool CanDelete = false;
        private string thisFormCode = "";

        public PointsExchangeRate()
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
        public void formCheck()
        {
            txtPoints.Value = 0;
            txtCustomer.Text = "";
            txtAmount.Value = 0;
            CustomerID.Text = "";
            PointsID = 0;

            txtPoints.Enabled = false;
            txtCustomer.Enabled = false;
            txtPoints.Enabled = false;
            pbCustomer.Enabled = false;
            if (dgPoints.Rows.Count == 0)
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                
            }
            else
            {
                btnDelete.Enabled = CanDelete;
                btnEdit.Enabled = CanEdit;
              
            }
            dgPoints.Enabled = true;
          
            btnSave.Enabled = false;
            btnAddNew.Enabled = CanAdd;
            dgPoints.ClearSelection();

        }

        private void PointsExchangeRate_Load(object sender, EventArgs e)
        {
            LoadPoints();
            formCheck();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtPoints.Enabled = true;
            txtCustomer.Enabled = true;
            txtPoints.Enabled = true;
            pbCustomer.Enabled = true;

            btnAddNew.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;

        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {
            ShowCustomers();
        }
        public void ShowCustomers()
        {
            ProfileLookup ProfileDlg = new ProfileLookup("Customer");
            if (ProfileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] lProfile = ProfileDlg.GetProfile;
                CustomerID.Text = lProfile[0];
                this.txtCustomer.Text = lProfile[2];

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            formCheck();
        }


        public void SavePoints()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            string InsertSql = @"INSERT INTO PointsExchangeRate ( CustomerID,
                                                       PointsValue,
                                                       AmountValue) 
                                              VALUES ( @CustomerID,
                                                       @PointsValue,
                                                       @AmountValue) 
                                                       ";
            param.Add("@CustomerID", CustomerID.Text);
            param.Add("@PointsValue", txtPoints.Value);
            param.Add("@AmountValue", txtAmount.Value);
            PointsID = CommonClass.runSql(InsertSql, CommonClass.RunSqlInsertMode.SCALAR, param);
            if (PointsID > 0)
            {

                string titles = "Information";
                DialogResult createNew = MessageBox.Show("Points Record has been created. Would you like to enter a new point?", titles, MessageBoxButtons.YesNo);
                if (createNew == DialogResult.Yes)
                {   //clear for new datas
                    btnRefresh.PerformClick();
                }
                else if (createNew == DialogResult.No)
                {
                    CommonClass.PointExhangeRate.Close();
                }
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added Promotions/Points Setup for Customer ID " + CustomerID.Text, CustomerID.Text);
            }
        }
        private bool IsDuplicate(string cID)
        {
            string selectSql = "SELECT * FROM PointsExchangeRate WHERE CustomerID ='" + cID + "'";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string code = "";
            //code = dgAccumulationPoints.CurrentRow.Cells[3].Value.ToString();
            if (PointsID == 0)
            {
                if (!IsDuplicate(CustomerID.Text))
                {
                    SavePoints();
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
                UpdatePoint(PointsID);
                btnRefresh.PerformClick();
            }
        }
        public void UpdatePoint(int pID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            string ptValue = dgPoints.CurrentRow.Cells[1].Value.ToString();
            //txtPoints.Value = ptValue != "" ? Convert.ToDecimal(ptValue) : 0;
            string amtValue = dgPoints.CurrentRow.Cells[2].Value.ToString();
            //txtAmount.Value = ptValue != "" ? Convert.ToDecimal(amtValue) : 0;

           // float PointValue = float.Parse(ptValue);

            string sqlUpdate = @"UPDATE PointsExchangeRate SET PointsValue = @PointsValue, AmountValue = @AmountValue WHERE ID = " + pID + "";

            param.Add("@PointsValue", txtPoints.Value);
            param.Add("@AmountValue", txtAmount.Value);

            // param.Add("@RuleCriteria", "");
            // param.Add("@RuleCriteriaID", "");

            string titles = "Update Points Record for " + txtCustomer.Text;
            DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                CommonClass.runSql(sqlUpdate, CommonClass.RunSqlInsertMode.QUERY, param);
                CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated Promotions/Points Setup for Customer ID " + CustomerID.Text, CustomerID.Text);
                MessageBox.Show("Record has been updated.", "INFORMATION");
                btnRefresh.PerformClick();
                LoadPoints();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtPoints.Enabled = true;
            txtAmount.Enabled = true;

            btnAddNew.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
        }
        public void LoadPoints()
        {
            dt.Rows.Clear();
            dgPoints.Rows.Clear();
            string selectSql = @"SELECT e.*, p.Name FROM PointsExchangeRate 
                                    e INNER JOIN Profile p ON e.CustomerID = p.ID";
            CommonClass.runSql(ref dt, selectSql);
            DataGridViewRow DRow;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgPoints.Rows.Add();
                DRow = dgPoints.Rows[i];
                DRow.Cells["ID"].Value = dt.Rows[i]["ID"].ToString();
                CustomerID.Text = dt.Rows[i]["CustomerID"].ToString();
                DRow.Cells["CustomerName"].Value = dt.Rows[i]["Name"].ToString();
                DRow.Cells["Points"].Value = dt.Rows[i]["PointsValue"].ToString();
                DRow.Cells["Amount"].Value = dt.Rows[i]["AmountValue"].ToString();
            }

        }

        private void dgPoints_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadPoint();
            dgPoints.Enabled = false;
        }
        private void LoadPoint()
        {
            dgPoints.ClearSelection();
           
            if (dgPoints.Rows.Count > 0)
            {
                txtCustomer.Text = dgPoints.CurrentRow.Cells[1].Value.ToString();
                string ptValue = dgPoints.CurrentRow.Cells[2].Value.ToString();
                txtPoints.Value = ptValue != "" ? Convert.ToDecimal(ptValue) : 0;
                string amtValue = dgPoints.CurrentRow.Cells[3].Value.ToString();
                txtAmount.Value = amtValue != "" ? Convert.ToDecimal(amtValue) : 0;

                string pointID = dgPoints.CurrentRow.Cells[0].Value.ToString();
                PointsID = pointID != "" ? Convert.ToInt32(pointID) : 0;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeletePoint(PointsID);
        }
        private void DeletePoint(int pID)
        {
            string sqlDelete = @"DELETE FROM PointsExchangeRate WHERE ID = " + pID;

            string titles = "Delete Point Record for " + txtCustomer.Text;
            DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                CommonClass.runSql(sqlDelete);
                MessageBox.Show("Record has been Deleted.", "INFORMATION");
                btnRefresh.PerformClick();
                LoadPoints();
            }
        }
    }
}
