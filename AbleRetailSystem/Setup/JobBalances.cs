using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace RestaurantPOS
{
    public partial class JobBalances : Form
    {
        private static Decimal TAsset = 0;
        private static Decimal TLiability = 0;
        private static Decimal TEquity = 0;
        private static Decimal TIncome = 0;
        private static Decimal TExpense = 0;
        private static Decimal TOutBal = 0;
       
        private bool CanEdit = false;
      

        public JobBalances()
        {
            InitializeComponent();
            Dictionary<string, Boolean> FormRights;
            CommonClass.UserAccess.TryGetValue(this.Text, out FormRights);
            Boolean outx = false;
            if (FormRights != null && FormRights.Count > 0)
            {
              
                outx = false;
                FormRights.TryGetValue("Edit", out outx);
                if (outx == true)
                {
                    CanEdit = true;
                }
               
            }

        }

        private void JobBalances_Load(object sender, EventArgs e)
        {
          //  pbJob_Click(sender, e);  
        }


        private void dgridBalance_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           // MessageBox.Show("END EDIT");
            decimal lbal = 0;
            bool converted = Decimal.TryParse(this.dgridBalance.CurrentRow.Cells[3].Value.ToString(), NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.CurrentCulture.NumberFormat, out lbal);
            this.dgridBalance.CurrentRow.Cells[3].Value = (Math.Round(lbal, 2).ToString("C"));
            calcBalance();
        }

        private void calcBalance()
        {
            TAsset = 0;
            TLiability = 0;
            TEquity = 0;
            TIncome = 0;
            TExpense = 0;
            decimal lbal = 0;
            DataGridViewCellStyle HeaderStyle = new DataGridViewCellStyle();
            HeaderStyle.Font = new Font(this.dgridBalance.Font, FontStyle.Bold);
            foreach (DataGridViewRow row in this.dgridBalance.Rows)
            {
                if (row.Cells[5].Value.ToString() != "")
                {
                    bool converted = Decimal.TryParse(row.Cells[3].Value.ToString(),NumberStyles.AllowCurrencySymbol|NumberStyles.AllowDecimalPoint |NumberStyles.AllowThousands,CultureInfo.CurrentCulture.NumberFormat,out lbal);
                    switch (row.Cells[5].Value.ToString())
                    {
                        case "A":
                            TAsset += lbal;
                            break;
                        case "L":
                            TLiability += lbal;
                            break;
                        case "EQ":
                            TEquity += lbal;
                            break;
                        case "I":
                            TIncome += lbal;
                            break;
                        case "OI":
                            TIncome += lbal;
                            break;
                        case "EXP":
                            TExpense += lbal;
                            break;
                        case "OE":
                            TExpense += lbal;
                            break;
                    }
                }
                else
                {
                    row.DefaultCellStyle = HeaderStyle;
                }
            }

            TOutBal = TAsset - (TLiability + TEquity + TIncome - TExpense);
           
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                decimal lbal = 0;
                using (SqlConnection sqlConsa = new SqlConnection(CommonClass.ConStr))
                {
                    sqlConsa.Open();
                    string sql = "";
                    string lAcctID = "";
                    string lJobID = "";

                    foreach (DataGridViewRow row in dgridBalance.Rows)
                    {
                        if (row.Cells[5].Value.ToString() != "")
                        {
                            bool converted = Decimal.TryParse(row.Cells[3].Value.ToString(), NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.CurrentCulture.NumberFormat, out lbal);
                            lAcctID = row.Cells[4].Value.ToString();
                         
                            if (CheckIfExists(lblJobID.Text, lAcctID))
                            {
                                sql = "UPDATE JobOpeningBalance SET OpeningJobBalance = " + lbal.ToString() + " WHERE JobID = " + lblJobID.Text;

                            }
                            else
                            {
                                sql = "INSERT INTO JobOpeningBalance(JobID,AccountID,OpeningJobBalance) VALUES(" + lblJobID.Text + "," + lAcctID + "," + lbal.ToString() + ")";
                            }
                            SqlCommand CmdUpdate = new SqlCommand(sql, sqlConsa);
                            CmdUpdate.CommandType = CommandType.Text;
                            CmdUpdate.ExecuteNonQuery();
                        }
                    }
                    sqlConsa.Close();
                }
                MessageBox.Show("Job Opening Balances Successfully Saved.");
              //CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Created Build Inventory Transaction  No. " + AdjNumber, AdjNumber);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

        private void pbJob_Click(object sender, EventArgs e)
        {
            SelectJobs DlgJob = new SelectJobs();

            if (DlgJob.ShowDialog() == DialogResult.OK)
            {
                string[] Job = DlgJob.GetJob;
               
               this.lblJobID.Text = Job[0];
                this.txtJobs.Text =  Job[1] + " - " + Job[2];
                LoadJobBalances();

            }

        }

        private void LoadJobBalances()
        {
            btnRecord.Enabled = CanEdit;

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = "";

                sql = @"SELECT p.ID, p.ProfileIDNumber,j.JobID, j.JobCode, j.JobName, j.JobDescription, ISNULL(o.OpeningJobBalance,0) as OpeningJobBalance 
						FROM Profile p
						INNER JOIN Jobs j ON p.ID = j.CustomerID
						LEFT JOIN JobOpeningBalance o ON o.JobID = j.JobID";

                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                string lType = "";
                this.dgridBalance.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    string stropenjobbalance = row["OpeningJobBalance"].ToString();

                    if (lType != row["JobDescription"].ToString())
                    {
                        string[] toAddRow = new string[7];
                        toAddRow[0] = row["JobDescription"].ToString();
                        lType = row["JobDescription"].ToString();
                        toAddRow[5] = "";
                        this.dgridBalance.Rows.Add(toAddRow);

                        decimal lbal = stropenjobbalance != "" ? Convert.ToDecimal(stropenjobbalance) : 0;
                        toAddRow[0] = "";
                        toAddRow[1] = row["ProfileIDNumber"].ToString();
                        toAddRow[2] = row["JobName"].ToString();
                        toAddRow[3] = (Math.Round(lbal, 2).ToString("C"));

                        toAddRow[4] = row["ID"].ToString();
                        toAddRow[5] = row["JobID"].ToString();
                        toAddRow[6] = this.lblJobID.Text; ;


                        this.dgridBalance.Rows.Add(toAddRow);
                    }
                    else
                    {
                        string[] toAddRow = new string[7];
                        decimal lbal = stropenjobbalance != "" ? Convert.ToDecimal(row["OpeningJobBalance"].ToString()) : 0;
                        lType = row["JobDescription"].ToString();
                        toAddRow[0] = "";
                        toAddRow[1] = row["ProfileIDNumber"].ToString();
                        toAddRow[2] = row["JobName"].ToString();
                        toAddRow[3] = (Math.Round(lbal, 2).ToString("C"));

                        toAddRow[4] = row["ID"].ToString();
                        toAddRow[5] = row["JobID"].ToString();
                        toAddRow[6] = this.lblJobID.Text;

                        this.dgridBalance.Rows.Add(toAddRow);
                    }
                }
                this.dgridBalance.Columns["balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            foreach (DataGridViewColumn column in dgridBalance.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private static bool CheckIfExists(string pJobID, string pAccountID)
        {
            

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(CommonClass.ConStr);
                connection.Open();
                string sql = "";

                sql = @"SELECT * from JobOpeningBalance where AccountID = " + pAccountID + " and JobID = " + pJobID;

                SqlCommand cmd_ = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    return true;
                }else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }



    }
}


