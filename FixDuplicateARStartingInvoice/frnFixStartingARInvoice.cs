using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ChangeCompanyNameRetail
{
    public partial class frmChangeCompanyName : Form
    {
        private bool addnew = false;
        private bool justdeleted = false;
        private DataGridViewRow SelCoRow = null;
        List <DeserializeTypes> compinfo = null;
        public frmChangeCompanyName()
        {
            InitializeComponent();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = odgBrowseConfig.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvCompInfo.Rows.Clear();
                txtConfigFile.Text = odgBrowseConfig.FileName;

                System.IO.StreamReader file = null;
                try
                {
                    file = new System.IO.StreamReader(txtConfigFile.Text);
                    string configjson = file.ReadToEnd();
                    configjson = configjson.Replace("\t", "");

                    compinfo = JsonConvert.DeserializeObject<List<DeserializeTypes>>(configjson);

                    foreach (DeserializeTypes compinfoleaf in compinfo)
                    {
                        string[] gridrow =
                        {
                            CommonClass.Decrypt(compinfoleaf.company_name),
                            CommonClass.Decrypt(compinfoleaf.serial_number),
                            CommonClass.Decrypt(compinfoleaf.registration_number),
                            compinfoleaf.database_name,
                            compinfoleaf.server_name
                        };

                        dgvCompInfo.Rows.Add(gridrow);
                    }

                    if (this.dgvCompInfo.Rows.Count > 0)
                    {
                        this.dgvCompInfo.FirstDisplayedScrollingRowIndex = 0;
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }
            }
        }

        private void dgvCompInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelCoRow = dgvCompInfo.CurrentRow;
            txtCompName.Text = SelCoRow.Cells["CoName"].Value.ToString();
                txtDbName.Text = SelCoRow.Cells["DBName"].Value.ToString();
            txtServerName.Text = SelCoRow.Cells["ServerAddress"].Value.ToString();
           
            this.Refresh();
           
            addnew = false;
            justdeleted = false;
        }


   

      
       

        private void frmCoProEditor_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                CommonClass.ConStr = "Data Source = " + txtServerName.Text + "; Initial Catalog = " + txtDbName.Text + "; MultipleActiveResultSets = true; User ID = ableacctg; Password = 5!37e5CCt9";
                con = new SqlConnection(CommonClass.ConStr);                
                con.Open();
                MessageBox.Show("Connection Successful.");
                this.txtInvoiceNo.Enabled = true;
                this.btnFix.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                this.txtInvoiceNo.Enabled = false;
                this.btnFix.Enabled = false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            if(txtInvoiceNo.Text == "")
            {
                return;
            }
            try
            {
                DataTable tbSI = new DataTable();
                string qry = "SELECT * from Sales where SalesType = 'SINVOICE' and SalesNumber = '" + txtInvoiceNo.Text + "' order by SalesID";
                CommonClass.runSql(ref tbSI, qry);
                string OldSalesNumber = "";
                string NewSalesNumber = "";
                int OldSalesNo = 0;
                int NewSalesNo = 0;
                string SalesID;
                DataTable tbCustomer = new DataTable();
                qry = "SELECT DISTINCT CustomerID from Sales where SalesType = 'SINVOICE' and SalesNumber = '" + txtInvoiceNo.Text + "'";
                CommonClass.runSql(ref tbCustomer, qry);
                if (tbSI.Rows.Count > 0)
                {
                   qry = "Update Sales set TotalPaid = 0 where SalesNumber = '" + txtInvoiceNo.Text + "'";
                   if (CommonClass.runSql(qry,CommonClass.RunSqlInsertMode.QUERY) > 0)
                   {
                        rTxtResults.Text = "Set TotalPaid of all Invoices with SalesNumber " + txtInvoiceNo.Text + Environment.NewLine;
                        for (int i = 0; i < tbSI.Rows.Count; i++)
                        {
                            DataRow rw = tbSI.Rows[i];
                            SalesID = rw["SalesID"].ToString();
                            float tpaid = 0;
                            float grandtotal = 0;
                            float tdue = 0;
                            grandtotal = float.Parse(rw["GrandTotal"].ToString());

                            if (i > 0)
                            {
                                NewSalesNo = OldSalesNo + 1;
                                NewSalesNumber = "SI-" + NewSalesNo.ToString();
                                qry = "Update Sales set SalesNumber = '" + NewSalesNumber + "' where SalesID = " + SalesID;
                                if(CommonClass.runSql(qry, CommonClass.RunSqlInsertMode.QUERY) > 0)
                                {
                                    rTxtResults.Text += "Set SalesNumber " + NewSalesNumber + " to SalesID " + SalesID +  Environment.NewLine;
                                }


                            }

                            if (i == 0)
                            {
                                string[] lNo = rw["SalesNumber"].ToString().Split('-');
                                lNo[1] = lNo[1].TrimStart('0');
                                if (lNo[1] != "")
                                {
                                    OldSalesNo = Convert.ToInt16(lNo[1]);
                                }
                            }
                            else
                            {
                                OldSalesNo = NewSalesNo;
                            }
                            //UPDATE TOTAL PAID
                            qry = "SELECT EntityID, sum(Amount) as totalpaid from PaymentLines where EntityID = " + SalesID + " group by EntityID";
                            DataTable ltb = new DataTable();
                            CommonClass.runSql(ref ltb, qry);
                            if(ltb.Rows.Count > 0)
                            {
                                
                                tpaid = float.Parse(ltb.Rows[0]["totalpaid"].ToString());
                               
                            }
                            else
                            {
                                tpaid = 0;
                                
                            }
                            tdue = grandtotal - tpaid;
                            qry = "Update Sales set TotalPaid = " + tpaid.ToString() + ", TotalDue = " + tdue.ToString() + " where SalesID = " + SalesID;
                            if(CommonClass.runSql(qry, CommonClass.RunSqlInsertMode.QUERY) > 0)
                            {
                                rTxtResults.Text += "Set TotalPaid = " + tpaid.ToString() + ", TotalDue = " + tdue.ToString() + " of SalesID = " + SalesID + Environment.NewLine;

                            }

                        }
                   }
                   

                }
                if(tbCustomer.Rows.Count > 0)
                {
                    foreach(DataRow rwC in tbCustomer.Rows)
                    {

                        float TotalInvoices = 0;
                        //GET Invoices
                        qry = "SELECT CustomerID, sum(GrandTotal) as invoice from Sales where SalesType in ('SINVOICE','INVOICE') and CustomerID = " + rwC["CustomerID"].ToString() + " group by CustomerID";
                        DataTable ltbInvoice = new DataTable();
                        CommonClass.runSql(ref ltbInvoice, qry);
                        if(ltbInvoice.Rows.Count > 0)
                        {
                            TotalInvoices = float.Parse(ltbInvoice.Rows[0]["invoice"].ToString());
                        }
                        float TotalPayments = 0;
                        //GET PAYMENTS
                        qry = "SELECT ProfileID, sum(TotalAmount) as payment from Payment where ProfileID = " + rwC["CustomerID"].ToString() + " group by ProfileID";
                        DataTable ltbPayment = new DataTable();
                        CommonClass.runSql(ref ltbPayment, qry);
                        if (ltbPayment.Rows.Count > 0)
                        {
                            TotalPayments = float.Parse(ltbPayment.Rows[0]["payment"].ToString());
                        }
                        float Customerbalance = TotalInvoices - TotalPayments;
                        qry = "Update Profile set CurrentBalance = " + Customerbalance.ToString() + " where ID = " + rwC["CustomerID"].ToString();
                        if(CommonClass.runSql(qry, CommonClass.RunSqlInsertMode.QUERY) > 0)
                        {
                            rTxtResults.Text += "Set CurrentBalance = " + Customerbalance.ToString() + " of CustomerID = " + rwC["CustomerID"].ToString() + Environment.NewLine;

                        }

                    }
                }
                rTxtResults.Text += "COMPLETED....";


            }
            catch (SqlException ex)
            {

            }
            finally
            {

            }
        }
    }
}
