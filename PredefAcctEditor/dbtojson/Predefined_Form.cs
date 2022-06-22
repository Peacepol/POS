using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using System.Data.OleDb;


namespace PredefAcctEditor
{
    public partial class Predefined_Form : Form
    {
        string connetionString = "Data Source=.\\SQLEXPRESS; Initial Catalog=ABLE_TEMPLATE_DB; Integrated Security=true; MultipleActiveResultSets=true";
        SqlConnection db_connection = null;
        SqlDataAdapter data_Adapter = new SqlDataAdapter();
        DataTable db_data = new DataTable();
        BindingSource bind_source = new BindingSource();
        public Predefined_Form()
        {
            InitializeComponent();
            string[] IndustryClass_list = new string[] { "Services",
                                                         "Retail",
                                                         "Manufacturing",
                                                         "Agriculture",
                                                         "Other" };
            BindingList<string> IC_bindingList = new BindingList<string>(IndustryClass_list);
            IndustryClass_box.DataSource = IC_bindingList;
        }

        private void Predefined_Form_Load(object sender, EventArgs e)
        {
            try
            {
                db_connection = new SqlConnection(connetionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM PredefinedAccounts", db_connection);
                db_connection.Open();//open
                data_Adapter.SelectCommand = cmd;
              //  data_Adapter.Fill(db_data);
                bind_source.DataSource = db_data;
                Display_Data_dgView.DataSource = bind_source;          
            }
            finally
            {
                if (db_connection != null)
                    db_connection.Close();//close
            }
        }

        private void reload_Button(object sender, EventArgs e)
        {
            try
            {
                db_connection = new SqlConnection(connetionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM PredefinedAccounts", db_connection);
                db_connection.Open();//open
                SqlDataAdapter data_Adapter = new SqlDataAdapter();
                data_Adapter.SelectCommand = cmd;
                data_Adapter.Fill(db_data);
                bind_source.DataSource = db_data;
                Display_Data_dgView.DataSource = bind_source;
                data_Adapter.Update(db_data);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (db_connection != null)
                    db_connection.Close();//close
            }
        }

        private void convert_Button(object sender, EventArgs e)
        {
            //toJSON
            DataTable data_Table = new DataTable();
            foreach (DataGridViewColumn col in Display_Data_dgView.Columns)
            {
                data_Table.Columns.Add(col.Name);
            }
            foreach (DataGridViewRow row in Display_Data_dgView.Rows)
            {
                DataRow dRow = data_Table.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                data_Table.Rows.Add(dRow);
            }
            string JSONString = JsonConvert.SerializeObject(data_Table, Formatting.Indented);
            richTextBox1.Text = JSONString;
        }

        private void Add_Button(object sender, EventArgs e)
        {
            AccountClassificationIDChecker();
            db_data.Rows.Add(Int32.Parse(text_AccountID.Text),
                            (text_ParentAcctID.Text == "") ? 0 : Int32.Parse(text_ParentAcctID.Text),
                             text_Inactive.Text,
                             textAcctName.Text,
                             text_AcctNum.Text,
                             (text_TaxCode.Text == "") ? "N-T" : text_TaxCode.Text,
                             (text_CurrID.Text == "") ? 0 : Int32.Parse(text_CurrID.Text),
                             (text_CurExchangeID.Text == "") ? 0 : Int32.Parse(text_CurExchangeID.Text),
                             text_AcctClassID.Text,
                             text_SubAcctClassID.Text,
                             (text_AcctLvl.Text == "") ? 0 : Int32.Parse(text_AcctLvl.Text),
                             text_AcctTypeID.Text,
                             text_LastCheqNum.Text,
                             text_isReconciled.Text,
                             datePicker_LastReconciledDate.Value.Date,
                             (text_StatementBalance.Text == "") ? 0 : float.Parse(text_StatementBalance.Text),
                             text_isCreditBalance.Text,
                             (text_OpeningAccountBalance.Text == "") ? 0 : float.Parse(text_OpeningAccountBalance.Text),
                             (text_CurrentAcctBalance.Text == "") ? 0 : float.Parse(text_CurrentAcctBalance.Text),
                             (text_PreLastYearActivity.Text == "") ? 0 : float.Parse(text_PreLastYearActivity.Text),
                             (text_LastYearOpeningBal.Text == "") ? 0 : float.Parse(text_LastYearOpeningBal.Text),
                             (text_ThisYearOpenBal.Text == "") ? 0 : float.Parse(text_ThisYearOpenBal.Text),
                             (text_PostThisYearActivity.Text == "") ? 0 : float.Parse(text_PostThisYearActivity.Text),
                             text_AccountDescrip.Text,
                             text_isTotal.Text,
                             text_CashFlowClassificationID.Text,
                             text_BsBCode.Text,
                             text_BankAccountNumber.Text,
                             text_BankAccountName.Text,
                             text_CompanyTradingName.Text,
                             text_CreateBankFiles.Text,
                             textBankCode.Text,
                             textDirectEntryUserID.Text,
                             text_isSelfBalancing.Text,
                             text_StatementParticulars.Text,
                             text_StatementCode.Text,
                             text_StatementReference.Text,
                             (text_AccoutantLinkCode.Text == "") ? 0 : Int32.Parse(text_AccoutantLinkCode.Text),
                             IndustryClass_box.Text,
                             type0fBusiness_box.Text,
                             text_isHeader.Text);
            Display_Data_dgView.DataSource = db_data; 
        }

        int indexrow;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexrow = e.RowIndex;
            DataGridViewRow row = Display_Data_dgView.Rows[indexrow];
                text_AccountID.Text = row.Cells[0].Value.ToString();
                text_ParentAcctID.Text = row.Cells[1].Value.ToString();
                text_Inactive.Text = row.Cells[2].Value.ToString();
                textAcctName.Text = row.Cells[3].Value.ToString();
                text_AcctNum.Text = row.Cells[4].Value.ToString();
                text_TaxCode.Text = row.Cells[5].Value.ToString();
                text_CurrID.Text = row.Cells[6].Value.ToString();
                text_CurExchangeID.Text = row.Cells[7].Value.ToString();
                text_AcctClassID.Text = row.Cells[8].Value.ToString();
                text_SubAcctClassID.Text = row.Cells[9].Value.ToString();
                text_AcctLvl.Text = row.Cells[10].Value.ToString();
                text_AcctTypeID.Text = row.Cells[11].Value.ToString();
                text_LastCheqNum.Text = row.Cells[12].Value.ToString();
                text_isReconciled.Text = row.Cells[13].Value.ToString();
                datePicker_LastReconciledDate.Text = row.Cells[14].Value.ToString();
                text_StatementBalance.Text = row.Cells[15].Value.ToString();
                text_isCreditBalance.Text = row.Cells[16].Value.ToString();
                text_OpeningAccountBalance.Text = row.Cells[17].Value.ToString();
                text_CurrentAcctBalance.Text = row.Cells[18].Value.ToString();
                text_PreLastYearActivity.Text = row.Cells[19].Value.ToString();
                text_LastYearOpeningBal.Text = row.Cells[20].Value.ToString();
                text_ThisYearOpenBal.Text = row.Cells[21].Value.ToString();
                text_PostThisYearActivity.Text = row.Cells[22].Value.ToString();
                text_AccountDescrip.Text = row.Cells[23].Value.ToString();
                text_isTotal.Text = row.Cells[24].Value.ToString();
                text_CashFlowClassificationID.Text = row.Cells[25].Value.ToString();
                text_BsBCode.Text = row.Cells[26].Value.ToString();
                text_BankAccountNumber.Text = row.Cells[27].Value.ToString();
                text_BankAccountName.Text = row.Cells[28].Value.ToString();
                text_CompanyTradingName.Text = row.Cells[29].Value.ToString();
                text_CreateBankFiles.Text = row.Cells[30].Value.ToString();
                textBankCode.Text = row.Cells[31].Value.ToString();
                textDirectEntryUserID.Text = row.Cells[32].Value.ToString();
                text_isSelfBalancing.Text = row.Cells[33].Value.ToString();
                text_StatementParticulars.Text = row.Cells[34].Value.ToString();
                text_StatementCode.Text = row.Cells[35].Value.ToString();
                text_StatementReference.Text = row.Cells[36].Value.ToString();
                text_AccoutantLinkCode.Text = row.Cells[37].Value.ToString();
                IndustryClass_box.Text = row.Cells[38].Value.ToString();
                type0fBusiness_box.Text = row.Cells[39].Value.ToString();
                text_isHeader.Text = row.Cells[40].Value.ToString();
        }
        private void Update_Button(object sender, EventArgs e)
        {
            AccountClassificationIDChecker();
            DataGridViewRow newdata = Display_Data_dgView.Rows[indexrow];
            newdata.Cells[0].Value = (text_AccountID.Text == "") ? 0 : Int32.Parse(text_AccountID.Text);
            newdata.Cells[1].Value = (text_ParentAcctID.Text == "") ? 0 : Int32.Parse(text_ParentAcctID.Text);
            newdata.Cells[2].Value = text_Inactive.Text;
            newdata.Cells[3].Value = textAcctName.Text;
            newdata.Cells[4].Value = text_AcctNum.Text;
            newdata.Cells[5].Value = text_TaxCode.Text;
            newdata.Cells[6].Value = (text_CurrID.Text == "") ? 0 : Int32.Parse(text_CurrID.Text);
            newdata.Cells[7].Value = (text_CurExchangeID.Text == "") ? 0 : Int32.Parse(text_CurExchangeID.Text);
            newdata.Cells[8].Value = text_AcctClassID.Text;
            newdata.Cells[9].Value = text_SubAcctClassID.Text;
            newdata.Cells[10].Value = (text_AcctLvl.Text == "") ? 0 : Int32.Parse(text_AcctLvl.Text);
            newdata.Cells[11].Value = text_AcctTypeID.Text;
            newdata.Cells[12].Value = text_LastCheqNum.Text;
            newdata.Cells[13].Value = text_isReconciled.Text;
            newdata.Cells[14].Value = datePicker_LastReconciledDate.Value.Date;
            newdata.Cells[15].Value = (text_StatementBalance.Text == "") ? 0 : float.Parse(text_StatementBalance.Text);
            newdata.Cells[16].Value = text_isCreditBalance.Text;
            newdata.Cells[17].Value = (text_OpeningAccountBalance.Text == "") ? 0 : float.Parse(text_OpeningAccountBalance.Text);
            newdata.Cells[18].Value = (text_CurrentAcctBalance.Text == "") ? 0 : float.Parse(text_CurrentAcctBalance.Text);
            newdata.Cells[19].Value = (text_PreLastYearActivity.Text == "") ? 0 : float.Parse(text_PreLastYearActivity.Text);
            newdata.Cells[20].Value = (text_LastYearOpeningBal.Text == "") ? 0 : float.Parse(text_LastYearOpeningBal.Text);
            newdata.Cells[21].Value = (text_ThisYearOpenBal.Text == "") ? 0 : float.Parse(text_ThisYearOpenBal.Text);
            newdata.Cells[22].Value = (text_PostThisYearActivity.Text == "") ? 0 : float.Parse(text_PostThisYearActivity.Text);
            newdata.Cells[23].Value = text_AccountDescrip.Text;
            newdata.Cells[24].Value = text_isTotal.Text;
            newdata.Cells[25].Value = text_CashFlowClassificationID.Text;
            newdata.Cells[26].Value = text_BsBCode.Text;
            newdata.Cells[27].Value = text_BankAccountName.Text;
            newdata.Cells[28].Value = text_BankAccountName.Text;
            newdata.Cells[29].Value = text_CompanyTradingName.Text;
            newdata.Cells[30].Value = text_CreateBankFiles.Text;
            newdata.Cells[31].Value = textBankCode.Text;
            newdata.Cells[32].Value = textDirectEntryUserID.Text;
            newdata.Cells[33].Value = text_isSelfBalancing.Text;
            newdata.Cells[34].Value = text_StatementParticulars.Text;
            newdata.Cells[35].Value = text_StatementCode.Text;
            newdata.Cells[36].Value = text_StatementReference.Text;
            newdata.Cells[37].Value = (text_AccoutantLinkCode.Text == "") ? 0 : Int32.Parse(text_AccoutantLinkCode.Text);
            newdata.Cells[38].Value = IndustryClass_box.Text;
            newdata.Cells[39].Value = type0fBusiness_box.Text;
            newdata.Cells[40].Value = text_isHeader.Text;
        }

        private void Delete_Button(object sender, EventArgs e)
        {
            int rowindex = Display_Data_dgView.CurrentCell.RowIndex;
            Display_Data_dgView.Rows.RemoveAt(rowindex);
        }

        private void Open_File_Button(object sender, EventArgs e)
        {
            DataTable data_Table = new DataTable();
            OpenFiletoCSV.ShowDialog();
            string filename = OpenFiletoCSV.FileName;
            string readfile = File.ReadAllText(filename);
            string File_extension = Path.GetExtension(filename);
            //check file
            if (File_extension == ".csv")
            {
                string[] row_text = System.IO.File.ReadAllLines(filename);
                string[] data_col = null;
                int x = 0;
                foreach (string text_line in row_text)
                {
                    data_col = text_line.Split(',');
                    if (x == 0)
                    {
                        for (int i = 0; i <= data_col.Count() - 1; i++)
                        {
                            data_Table.Columns.Add(data_col[i]);
                        }
                        x++;
                    }
                    else
                    {
                        data_Table.Rows.Add(data_col);
                    }
                }
                Display_Data_dgView.DataSource = data_Table;
                this.Controls.Add(Display_Data_dgView);
            }
            else
            {
                MessageBox.Show("Not a.csv file");
            }
        }

        private void IndustryClassBox_onChange(object sender, EventArgs e)
        {
            if (IndustryClass_box.Text == "Services")
            {
                string[] ServicesClass_list = new string[] {"Accounting Firm",
                                                            "Advertising Agency",
                                                            "Assoc & Clubs",
                                                            "Computer consultant",
                                                            "Construction Comp / building contractor",
                                                            "Consulting Firm",
                                                            "Educational Institution",
                                                            "Electrical Contractor",
                                                            "Financial Services",
                                                            "Insurance Agency",
                                                            "Motel/Hotel",
                                                            "Non-Profit Organization",
                                                            "Property Management",
                                                            "Religious Organization",
                                                            "Travel Agency"};

                BindingList<string> services_bindingList = new BindingList<string>(ServicesClass_list);
                type0fBusiness_box.DataSource = services_bindingList;
            }
            else if (IndustryClass_box.Text == "Retail")
            {
                string[] RetailClass_list = new string[] { "Chemist", "Computer Dealer", "Clothing - Retail",
                                                               "Hardware Store", "Home Appliance Dealer",
                                                               "Motor Vehicle Dealership",
                                                               "Liquor Store", "Restaurant" };
                BindingList<string> retail_bindingList = new BindingList<string>(RetailClass_list);
                type0fBusiness_box.DataSource = retail_bindingList;
            }
            else if (IndustryClass_box.Text == "Manufacturing")
            {
                string[] ManufacturingClass_list = new string[] { "Manufacturing" };
                BindingList<string> manufacture_bindingList = new BindingList<string>(ManufacturingClass_list);
                type0fBusiness_box.DataSource = manufacture_bindingList;
            }
            else if (IndustryClass_box.Text == "Agriculture")
            {
                string[] AgriClass_list = new string[] { "Agricultural Business", "Farm" };
                BindingList<string> agri_bindingList = new BindingList<string>(AgriClass_list);
                type0fBusiness_box.DataSource = agri_bindingList;
            }
            else
            {
                string[] OtherClass_list = new string[] { "Partnership", "Retail Business", " Service Business", "Sole Proprietorship", "Wholesale Business" };
                BindingList<string> other_bindingList = new BindingList<string>(OtherClass_list);
                type0fBusiness_box.DataSource = other_bindingList;
            }
        }

        private void Show_Account_List_Form(object sender, EventArgs e)
        {
            Account_List_Form form2 = new Account_List_Form();
            form2.Show();
        }

        private void text_AcctNum_TextChanged(object sender, EventArgs e)
        {
            if(text_AcctNum.Text.Length!=6)
            {
                label9.Text = "not ok";
            }
            else
            {
                AccountClassificationIDChecker();
                label9.Text = "ok";
            }
        }

        public void AccountClassificationIDChecker()
        {
            if (text_AcctNum.Text.Substring(0, 1) == "1")//level 0 (1-0000)
            {
                text_AcctClassID.Text = "A";    
               //level 1 (1-1000/1-2000/1-3000)
                if (text_AcctNum.Text.Substring(0, 3) == "1-1")
                {
                    text_SubAcctClassID.Text = "A";
                    text_AcctLvl.Text = "1";
                    //level 2 (1-2 (2-9) ##/1-3 (1-9) ## == other 0Asset)(1-1### ==bank)(1-1200== AR)
                    if (text_AcctNum.Text.Substring(0, 5) == "1-110")
                    {
                        text_SubAcctClassID.Text = "A";
                        text_AcctLvl.Text = "2";
                    }
                   //Asset Level 3 :: (1-1110/1-1120/1-1130/1-1140/1-1180/1-1190)
                    else if (text_AcctNum.Text.Substring(0, 5) == "1-111"
                        || text_AcctNum.Text.Substring(0, 5) == "1-112"
                        || text_AcctNum.Text.Substring(0, 5) == "1-113" 
                        || text_AcctNum.Text.Substring(0, 5) == "1-114" 
                        || text_AcctNum.Text.Substring(0, 5) == "1-118" 
                        || text_AcctNum.Text.Substring(0, 5) == "1-119")
                    {
                            text_SubAcctClassID.Text = "B";
                            text_AcctLvl.Text = "3";
                    }
                    else if (text_AcctNum.Text.Substring(0, 5) == "1-100")
                    {
                        text_SubAcctClassID.Text = "A";
                        text_AcctLvl.Text = "1";
                    }
                    else if (text_AcctNum.Text.Substring(0, 4) == "1-12")
                    {
                        text_SubAcctClassID.Text = "AR";
                        text_AcctLvl.Text = "2";
                    }
                    else
                    {
                        text_SubAcctClassID.Text = "OAn";
                        text_AcctLvl.Text = "2";
                    }
                }//1-2000/1-3000
                else if (text_AcctNum.Text.Substring(0,4)=="1-20" || text_AcctNum.Text.Substring(0, 4) == "1-30")
                {
                    text_SubAcctClassID.Text = "A";
                    text_AcctLvl.Text = "1";
                }
                else if (text_AcctNum.Text.Substring(0, 4) == "1-00" )
                {
                    text_SubAcctClassID.Text = "A";
                    text_AcctLvl.Text = "0";
                }
                else
                {
                    text_SubAcctClassID.Text = "OA";
                    text_AcctLvl.Text = "2";
                }
            }
            // Liabilities
            else if (text_AcctNum.Text.Substring(0, 1) == "2")
            {
                text_AcctClassID.Text = "L";
                // [0]=2,[1]='-'; LEVEL 0
                if (text_AcctNum.Text.Substring(0, 3) == "2-0")
                {
                    text_SubAcctClassID.Text = "L";
                    text_AcctLvl.Text = "0";
                } //LEVEL 1
                else if (text_AcctNum.Text.Substring(0,4) == "2-10" || text_AcctNum.Text.Substring(0, 3) == "2-20")
                {
                    text_SubAcctClassID.Text = "L";
                    text_AcctLvl.Text = "1";
                }//LEVEL 2
                else if (text_AcctNum.Text.Substring(0, 5) == "2-110")
                {
                    text_SubAcctClassID.Text = "L";
                    text_AcctLvl.Text = "2";
                }
                else if (text_AcctNum.Text.Substring(0, 4) == "2-11")
                {
                    text_SubAcctClassID.Text = "CC";
                    text_AcctLvl.Text = "3";
                }
                else if (text_AcctNum.Text.Substring(0, 5) == "2-120")
                {
                    text_SubAcctClassID.Text = "AP";
                    text_AcctLvl.Text = "2";
                }
                else if (text_AcctNum.Text.Substring(0, 5) == "2-121")
                {
                    text_SubAcctClassID.Text = "OL";
                    text_AcctLvl.Text = "2";
                }
                else if (text_AcctNum.Text.Substring(0, 5) == "2-130")
                {
                    text_SubAcctClassID.Text = "L";
                    text_AcctLvl.Text = "2";
                }
                else
                {
                    text_SubAcctClassID.Text = "OL";
                }
            }
            else if (text_AcctNum.Text.Substring(0, 1) == "3")
            {
                text_AcctClassID.Text = "EQ";
                text_SubAcctClassID.Text = "EQ";
            }
            else if (text_AcctNum.Text.Substring(0, 1) == "4")
            {
                text_AcctClassID.Text = "I";
                text_SubAcctClassID.Text = "I";
            }
            else if (text_AcctNum.Text.Substring(0, 1) == "5")
            {
                text_AcctClassID.Text = "COS";
                text_SubAcctClassID.Text = "COS";
            }
            else if (text_AcctNum.Text.Substring(0, 1) == "6")
            {
                text_AcctClassID.Text = "EXP";
                text_SubAcctClassID.Text = "EXP";
            }
            else if (text_AcctNum.Text.Substring(0, 1) == "7")
            {
                text_AcctClassID.Text = "OI";
                text_SubAcctClassID.Text = "OI";
            }
            else
            {
                text_AcctClassID.Text = "OE";
                text_SubAcctClassID.Text = "OE";
            }
        }
    }  
}
