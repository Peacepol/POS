using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.References
{
    public partial class OpeningFund : Form
    {
        DataTable dtsession;
        public DateTime sDate;
        public bool showsessions;
        public OpeningFund(bool opensessions = false)//if true show open sessionss
        {
            InitializeComponent();
            showsessions = opensessions;
        }
        public decimal GetOpeningFund
        {
            get { return txtOpeningFund.Value; }
        }
        public string GetTerminal
        {
            get { return cmbTerminal.SelectedValue.ToString(); }
        }
        public DateTime GetsDate
        {
            get { return sDate; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlSession = @"Select SessionKey,SessionID,SessionStart, UserID From Sessions Where SessionStatus = 'Open' AND UserID = '0' and SessionKey = '" + cmbTerminal.SelectedValue.ToString() + "'";
            dtsession = new DataTable();
            CommonClass.runSql(ref dtsession, sqlSession);

            string sqlTerminal = @"Select TerminalID,TerminalName FROM Terminal";
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sqlTerminal);
            if (dtsession.Rows.Count > 0)
            {
                DialogResult beginSession = MessageBox.Show("Terminal has a session. \n Would you like to continue?", "Session Warning!", MessageBoxButtons.YesNo);
                if (beginSession == DialogResult.Yes)
                {
                    if (dtsession.Rows[0]["SessionKey"].ToString() == cmbTerminal.SelectedValue.ToString())
                    {
                        CommonClass.SessionID = int.Parse(dtsession.Rows[0]["SessionID"].ToString());
                        sDate = Convert.ToDateTime(dtsession.Rows[0]["SessionStart"].ToString());
                        DialogResult = DialogResult.OK;
                        CommonClass.TerminalName = cmbTerminal.Text;
                        CommonClass.SessionDate = sDate;
                    }
                }
                else
                {

                }
            }
            else
            {
                CommonClass.OpeningFund = float.Parse(txtOpeningFund.Value.ToString());
                CommonClass.TerminalName = cmbTerminal.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void OpeningFund_Load(object sender, EventArgs e)
        {
            LoadTerminal();
            if (showsessions)
            {
                txtOpeningFund.Visible = false;
                label1.Visible = false;
                this.Text = "Terminals";
                label2.Text = "Select Open session to continue.";
            }

        }
        void LoadTerminal()
        {
            string sqlTerminal = @"Select TerminalID,TerminalName FROM Terminal";
            DataTable dt = new DataTable();
            CommonClass.runSql(ref dt, sqlTerminal);

            bool check = false;
            Dictionary<string, object> dTerminal = new Dictionary<string, object>();
            foreach (DataRow dr in dt.Rows)
            {
                string sqlSession = @"Select SessionKey,SessionID,SessionStart, UserID From Sessions Where SessionStatus = 'Open' AND SessionKey = '" + dr["TerminalID"].ToString() + "'";
                DataTable dtses = new DataTable();
                CommonClass.runSql(ref dtses, sqlSession);
                if (dtses.Rows.Count > 0)
                {
                    if (showsessions)//
                    {
                        if (dtses.Rows[0]["UserID"].ToString() == "0")
                        {
                            dTerminal.Add(dr["TerminalName"].ToString(), dr["TerminalID"].ToString());
                        }
                    }
                }
                else
                {
                    if (!showsessions)//
                    {
                        dTerminal.Add(dr["TerminalName"].ToString(), dr["TerminalID"].ToString());
                    }
                }
            }

            if (dTerminal.Count > 0)
            {
                cmbTerminal.ValueMember = "Value";
                cmbTerminal.DisplayMember = "Key";
                cmbTerminal.DataSource = new BindingSource(dTerminal, null);
            }

        }

    }
}
