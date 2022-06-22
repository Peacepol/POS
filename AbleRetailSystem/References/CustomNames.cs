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
using System.Configuration;
using System.Security.Cryptography;


namespace RestaurantPOS.References
{
    public partial class CustomNames : Form
    {
        private string thisFormCode = "";
        SqlCommand cmd;
        private string lblID = "";
        string lRecTypeItem = "";
        string lRecTypeCustomer = "";
        string lRecTypeSupplier = "";
        Dictionary<string, object> param = new Dictionary<string, object>();

        public CustomNames()
        {
            InitializeComponent();
        }

        private void CustomNames_Load(object sender, EventArgs e)
        {
            LoadItemsCustomName();
            LoadCustomerCustomName();
            LoadSuppliersCustomName();
        }

        private void LoadItemsCustomName()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomNames where RecordType = 'Items'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    lRecTypeItem = dt.Rows[0]["RecordType"].ToString();
                    this.txtItemL1.Text = dt.Rows[0]["CList1Name"].ToString();
                    this.txtItemL2.Text = dt.Rows[0]["CList2Name"].ToString();
                    this.txtItemL3.Text = dt.Rows[0]["CList3Name"].ToString();

                    this.txtItemF1.Text = dt.Rows[0]["CField1Name"].ToString();
                    this.txtItemF2.Text = dt.Rows[0]["CField2Name"].ToString();
                    this.txtItemF3.Text = dt.Rows[0]["CField3Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void LoadCustomerCustomName()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomNames where RecordType = 'Customers'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lRecTypeCustomer = dt.Rows[0]["RecordType"].ToString();
                    this.txtCustL1.Text = dt.Rows[0]["CList1Name"].ToString();
                    this.txtCustL2.Text = dt.Rows[0]["CList2Name"].ToString();
                    this.txtCustL3.Text = dt.Rows[0]["CList3Name"].ToString();

                    this.txtCustF1.Text = dt.Rows[0]["CField1Name"].ToString();
                    this.txtCustF2.Text = dt.Rows[0]["CField2Name"].ToString();
                    this.txtCustF3.Text = dt.Rows[0]["CField3Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void LoadSuppliersCustomName()
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomNames where RecordType = 'Suppliers'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lRecTypeSupplier = dt.Rows[0]["RecordType"].ToString();
                    this.txtSupL1.Text = dt.Rows[0]["CList1Name"].ToString();
                    this.txtSupL2.Text = dt.Rows[0]["CList2Name"].ToString();
                    this.txtSupL3.Text = dt.Rows[0]["CList3Name"].ToString();

                    this.txtSupF1.Text = dt.Rows[0]["CField1Name"].ToString();
                    this.txtSupF2.Text = dt.Rows[0]["CField2Name"].ToString();
                    this.txtSupF3.Text = dt.Rows[0]["CField3Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string listName1 = "";
            string listName2 = "";
            string listName3 = "";
            string fieldName1 = "";
            string fieldName2 = "";
            string fieldName3 = "";

            switch (this.TabForm.SelectedIndex)
            {
                case 0:
                    if (txtItemL1.Text != "" || txtItemL2.Text != "" || txtItemL3.Text != "") {
                        listName2 = this.txtItemL2.Text;
                        listName3 = this.txtItemL3.Text;
                        fieldName1 = this.txtItemF1.Text;
                        fieldName2 = this.txtItemF2.Text;
                        fieldName3 = this.txtItemF3.Text;
                        listName1 = this.txtItemL1.Text;
                    }
                    if (lRecTypeItem.ToString() == "")
                    {
                        if (!IsDuplicate(listName1, listName2, listName3, lRecTypeItem))
                        {
                            lRecTypeItem = "Items";   
                           New_(listName1, listName2, listName3, fieldName1, fieldName2, fieldName3, lRecTypeItem);
                        }
                        else
                        {
                            MessageBox.Show("Record Already exists.");
                        }
                    }
                    else
                    {
                        UpdateList(listName1, listName2, listName3, fieldName1, fieldName2, fieldName3, lRecTypeItem);
                    }

                    lblID = ItemlistNameID1.Text;

                    break;
                case 1:
                    if (txtCustF1.Text != "" || txtCustF2.Text != "" || txtCustF3.Text != "")
                    {
                        listName1 = this.txtCustL1.Text;
                        listName2 = this.txtCustL2.Text;
                        listName3 = this.txtCustL3.Text;
                        fieldName1 = this.txtCustF1.Text;
                        fieldName2 = this.txtCustF2.Text;
                        fieldName3 = this.txtCustF3.Text;
                    }
                    if (lRecTypeCustomer.ToString() == "")
                    {
                        if (!IsDuplicate(listName1, listName2, listName3, lRecTypeCustomer))
                        {
                            lRecTypeCustomer = "Customers";
                            New_(listName1, listName2, listName3, fieldName1, fieldName2, fieldName3, lRecTypeCustomer);
                        }
                        else
                        {
                            MessageBox.Show("Record Already exists.");
                        }
                    }
                    else
                    {
                        UpdateList(listName1, listName2, listName3, fieldName1, fieldName2, fieldName3, lRecTypeCustomer);
                    }
                    lblID = CustListNameID1.Text;

                    break;
                case 2:
                    if (txtSupF1.Text != "" || txtSupF2.Text != "" || txtSupF3.Text != "")
                    {
                        listName1 = this.txtSupL1.Text;
                        listName2 = this.txtSupL2.Text;
                        listName3 = this.txtSupL3.Text;
                        fieldName1 = this.txtSupF1.Text;
                        fieldName2 = this.txtSupF2.Text;
                        fieldName3 = this.txtSupF3.Text;
                    }
                    if (lRecTypeSupplier.ToString() == "")
                    {
                        if (!IsDuplicate(listName1, listName2, listName3, lRecTypeSupplier))
                        {
                            lRecTypeSupplier = "Suppliers";
                            New_(listName1, listName2, listName3, fieldName1, fieldName2, fieldName3, lRecTypeSupplier);
                        }
                        else
                        {
                            MessageBox.Show("Record Already exists.");
                        }
                    }
                    else
                    {
                        UpdateList(listName1, listName2, listName3, fieldName1, fieldName2, fieldName3, lRecTypeSupplier);
                    }
                    lblID = SupListNameID1.Text;

                    break;
            }
            
        }
        void UpdateList( string plistName1, string plistName2, string plistName3, string pfieldName1, string pfieldName2, string pfieldName3, string pRecType)
        {

                string titles = "Update Custom List Record for " + pRecType;
                DialogResult dialogResult = MessageBox.Show("Do you wish to continue? (yes/no)", titles, MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {

                Dictionary<string, object> param = new Dictionary<string, object>();
                string updatesql = "Update CustomNames SET CList1Name = @List1Name, CList2Name = @List2Name, CList3Name = @List3Name, CField1Name = @FieldName1, CField2Name = @FieldName2, CField3Name = @FieldName3 where RecordType  = @RecordType";
                    //Sales Data
                    param.Add("@RecordType", pRecType);
                    param.Add("@List1Name", plistName1);
                    param.Add("@List2Name", plistName2);
                    param.Add("@List3Name", plistName3);
                    param.Add("@FieldName1", pfieldName1);
                    param.Add("@FieldName2", pfieldName2);
                    param.Add("@FieldName3", pfieldName2);
                
                int rowsaffected = CommonClass.runSql(updatesql, CommonClass.RunSqlInsertMode.QUERY, param);

                    if (rowsaffected != 0)
                    {
                        CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Updated " + pRecType + " Custom List Record " + pRecType);
                        MessageBox.Show("Record has been updated.", "INFORMATION");
                        switch (this.TabForm.SelectedIndex)
                        {
                            case 0:
                                //LoadList1Items(plistID);
                                break;
                            case 1:
                                //LoadList1Customers(plistID);
                                break;
                            case 2:
                                //LoadList1Suppliers(plistID);
                                break;
                        }
                    }
                }
        }
        private bool IsDuplicate(string pListName1, string pListName2, string pListName3, string pRecordType)
        {
            SqlConnection con_ = null;
            try
            {
                con_ = new SqlConnection(CommonClass.ConStr);
                string selectSql = "SELECT * FROM CustomNames WHERE CList1Name ='" + pListName1 + "' and CList2Name = '"+ pListName2 + "'and CList3Name = '" + pListName3 + "' and RecordType = '" + pRecordType + "'";
                SqlCommand cmd_ = new SqlCommand(selectSql, con_);
                con_.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
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
                if (con_ != null)
                    con_.Close();
            }
        }

        void New_(string pListName1, string pListName2, string pListName3, string pFieldName1, string pFieldName2, string pFieldName3, string pRecordType)
        {
            Dictionary<string, object> paramCustomNames = new Dictionary<string, object>();

            string sqlInsert = "INSERT INTO CustomNames (RecordType, CList1Name, CList2Name, CList3Name, CField1Name, CField2Name, CField3Name) VALUES (@RecordType, @List1Name, @List2Name, @List3Name, @FieldName1, @FieldName2, @FieldName3 )";
            paramCustomNames.Add("@RecordType", pRecordType);
            paramCustomNames.Add("@List1Name", pListName1);
            paramCustomNames.Add("@List2Name", pListName2);
            paramCustomNames.Add("@List3Name", pListName3);
            paramCustomNames.Add("@FieldName1", pFieldName1);
            paramCustomNames.Add("@FieldName2", pFieldName2);
            paramCustomNames.Add("@FieldName3", pFieldName3);

                int rowsaffected = CommonClass.runSql(sqlInsert, CommonClass.RunSqlInsertMode.QUERY, paramCustomNames);

                if (rowsaffected != 0)
                {
                    CommonClass.SaveSystemLogs(CommonClass.UserID, thisFormCode, "Added New " + pRecordType + " List  Record " + pListName1, pListName2, pListName3);
                    string titles = "INFORMATION";
                    MessageBox.Show(""+ pRecordType + " List 1Record has been created.", titles);
                }
        }     
    }
}
