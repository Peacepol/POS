using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbleRetailPOS.Utilities
{
    public partial class SessionManager : Form
    {
        public bool CanEdit = false;
        public SessionManager()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
                FormRights.TryGetValue("Edit", out outx);
                CanEdit = outx;
            }
        }

        private void SessionManager_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = CanEdit; 
            LoadOpenSession();
        }

        void LoadOpenSession()
        {
            dgSesison.Rows.Clear();
            string sqlSes = @"Select SessionID,UserID,user_fullname,TerminalName,SessionStatus,SessionStart 
                                FROM Sessions s 
                                LEFT JOIN Users u ON u.user_id = s.UserID 
                                INNER JOIN Terminal t ON t.TerminalID = s.SessionKey 
                                WHERE SessionStatus = 'Open'";
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sqlSes);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dgSesison.Rows.Add();
                    dgSesison.Rows[i].Cells["Selected"].Value = "false";
                    dgSesison.Rows[i].Cells["SessionID"].Value = dr["SessionID"].ToString();
                    dgSesison.Rows[i].Cells["Terminal"].Value = dr["TerminalName"].ToString();
                    dgSesison.Rows[i].Cells["User"].Value = dr["UserID"].ToString()== "0"?"No user":dr["user_fullname"].ToString();
                    dgSesison.Rows[i].Cells["SessionStatus"].Value = dr["SessionStatus"].ToString();
                    dgSesison.Rows[i].Cells["StartDate"].Value = dr["SessionStart"].ToString();
                }
            }
            else
            {
                MessageBox.Show("No Open Session to Manage", "Information");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int x = 0;
            if (MessageBox.Show("Are you sure to clear users from the session?", "Information",
                 MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sqlUpdateSession = "Update Sessions SET UserID = @UserID WHERE SessionID = @SessionID";
                foreach (DataGridViewRow dgvr in dgSesison.Rows)
                {
                    if (bool.Parse(dgvr.Cells["Selected"].Value.ToString()))
                    {
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@UserID", 0);
                        param.Add("@SessionID", dgvr.Cells["SessionID"].Value.ToString());
                        x+=CommonClass.runSql(sqlUpdateSession, CommonClass.RunSqlInsertMode.QUERY, param);
                    }
                }
                if(x> 0)
                {
                    MessageBox.Show(x + " Cleared Users ", "Information");
                    LoadOpenSession();
                }
            }
        }
    }
}
