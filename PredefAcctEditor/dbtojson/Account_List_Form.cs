using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;

namespace PredefAcctEditor
{
    public partial class Account_List_Form : Form
    {
        string connetionString = "Data Source=.\\SQLEXPRESS; Initial Catalog=ABLE_TEMPLATE_DB; Integrated Security=true; MultipleActiveResultSets=true";
        SqlConnection db_connection = null;
        SqlDataAdapter data_Adapter = new SqlDataAdapter();
        DataTable db_data = new DataTable();
        BindingSource bind_source = new BindingSource();
        String query = null;

        public object Display_Data_dgView { get; private set; }

        public Account_List_Form()
        {
            InitializeComponent();
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            string[] IndustryClass_list = new string[] { "Services", "Retail", "Manufacturing", "Agriculture", "Other" , "All"};
            BindingList<string> IC_bindingList = new BindingList<string>(IndustryClass_list);
            IndustryClass_box.DataSource = IC_bindingList;
        }

        private void Convert_to_Json_Button(object sender, EventArgs e)
        {
            DataTable data_Table = new DataTable();

            foreach (DataGridViewColumn col in accountListDataGridView.Columns)
            {
                data_Table.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in accountListDataGridView.Rows)
            {
                DataRow dRow = data_Table.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    switch (cell.ColumnIndex)
                    {
                        case 0:
                        case 1:
                        case 10:
                        case 17:
                        case 18:
                            if (cell.Value != null
                                && cell.Value.ToString() != "")
                                dRow[cell.ColumnIndex] = Int32.Parse(cell.Value.ToString().Trim());
                            break;
                        default:
                            if (cell.Value != null)
                                dRow[cell.ColumnIndex] = cell.Value.ToString().Trim();
                            break;
                    }
                }

                data_Table.Rows.Add(dRow);
            }
            string JSONString = JsonConvert.SerializeObject(data_Table, Formatting.Indented);
            Display_text.Text = JSONString;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndustryClass_box.Text == "Services")
            {
                string[] ServicesClass_list = new string[] { "Accounting Firm",
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
                                                             "Travel Agency" };
                BindingList<string> services_bindingList = new BindingList<string>(ServicesClass_list);
                type0fBusiness_box.DataSource = services_bindingList;
            }
            else if (IndustryClass_box.Text == "Retail")
            {
                string[] RetailClass_list = new string[] { "Chemist",
                                                           "Computer Dealer",
                                                           "Clothing - Retail",
                                                           "Hardware Store",
                                                           "Home Appliance Dealer",
                                                           "Motor Vehicle Dealership",
                                                           "Liquor Store",
                                                           "Restaurant" };
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
            else if(IndustryClass_box.Text == "Other")
            {
                string[] OtherClass_list = new string[] { "Partnership",
                                                          "Retail Business",
                                                          "Service Business",
                                                          "Sole Proprietorship",
                                                          "Wholesale Business" };
                BindingList<string> other_bindingList = new BindingList<string>(OtherClass_list);
                type0fBusiness_box.DataSource = other_bindingList;
            }
            else
            {
                string[] OtherClass_list = new string[] { "All" };
                BindingList<string> other_bindingList = new BindingList<string>(OtherClass_list);
                type0fBusiness_box.DataSource = other_bindingList;
           }
        }

        private void type0fBusiness_box_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            query_selection();
            db_data.Clear();
            try
            {
                db_connection = new SqlConnection(connetionString);
                SqlCommand cmd = new SqlCommand(query, db_connection);
                db_connection.Open();//open
                SqlDataAdapter data_Adapter = new SqlDataAdapter();
                data_Adapter.SelectCommand = cmd;
                data_Adapter.Fill(db_data);
                bind_source.DataSource = db_data;
                accountListDataGridView.DataSource = bind_source;
                data_Adapter.Update(db_data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (db_connection != null)
                    db_connection.Close();//close
            }
        }

        public void query_selection()
        {
            if (type0fBusiness_box.Text == "Accounting Firm")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Accounting Firm'";
            }
            else if (type0fBusiness_box.Text == "Advertising Agency")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Advertising Agency'";
            }
            else if (type0fBusiness_box.Text == "Assoc & Clubs")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Assoc & Club'";
            }
            else if (type0fBusiness_box.Text == "Computer consultant")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Computer Consultant'";
            }
            else if(type0fBusiness_box.Text == "Construction Comp / building contractor")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Construction Company/Building Contractor'";
            }
            else if (type0fBusiness_box.Text == "Consulting Firm")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Consulting Firm'";
            }
            else if (type0fBusiness_box.Text == "Educational Institution")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Educational Institution'";
            }
            else if (type0fBusiness_box.Text == "Electrical Contractor")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Electrical Contractor'";
            }
            else if (type0fBusiness_box.Text == "Financial Services")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Financial Services'";
            }
            else if (type0fBusiness_box.Text == "Insurance Agency")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Insurance Agency'";
            }
            else if (type0fBusiness_box.Text == "Motel/Hotel")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Motel/Hotel'";
            }
            else if (type0fBusiness_box.Text == "Non-Profit Organization")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Non-Profit Organisation'";
            }
            else if (type0fBusiness_box.Text == "Property Management")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Property Management'";
            }     
            else if (type0fBusiness_box.Text == "Religious Organization")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Religious Organisation'";
            }
            else if (type0fBusiness_box.Text == "Travel Agency")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Travel Agency'";
            }
            else if (type0fBusiness_box.Text == "Chemist")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Chemist'";
            }
            else if (type0fBusiness_box.Text == "Computer Dealer")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Computer Dealer'";
            }
            else if (type0fBusiness_box.Text == "Clothing - Retail")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Clothing - Retail'";
            }
            else if (type0fBusiness_box.Text == "Hardware Store")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Hardware Store'";
            }
            else if (type0fBusiness_box.Text == "Home Appliance Dealer")
            {
                query = "SELECT * FROM  PredefinedAccounts WHERE TypeofBusiness = 'Home Appliance Dealer'";
            }
            else if (type0fBusiness_box.Text == "Motor Vehicle Dealership")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Motor Vehicle Dealership'";
            }
            else if (type0fBusiness_box.Text == "Liquor Store")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Liquor Store'";
            }
            else if (type0fBusiness_box.Text == "Restaurant")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Restaurant'";
            }
            else if (type0fBusiness_box.Text == "Manufacturing")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Manufacturing'";
            }
            else if (type0fBusiness_box.Text == "Agricultural Business")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Agricultural Business'";
            }
            else if (type0fBusiness_box.Text == "Farm")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Farm'";
            }
            else if (type0fBusiness_box.Text == "Partnership")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Partnership'";

            }
            else if (type0fBusiness_box.Text == "Retail Business")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Retail Business'"; ;

            }
            else if (type0fBusiness_box.Text == "Service Business")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Service Business'";

            }
            else if (type0fBusiness_box.Text == "Sole Proprietorship")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Sole Proprietorship'";

            }
            else if (type0fBusiness_box.Text == "Wholesale Business")
            {
                query = "SELECT * FROM PredefinedAccounts WHERE TypeofBusiness = 'Wholesale Business'";
            }
            else
            {
                query = "select * from PredefinedAccounts";
            }        
        }

        private void Save_Click(object sender, EventArgs e)
        {
           // saveFileJSON.ShowDialog();
            if(saveFileJSON.ShowDialog() == DialogResult.OK)
            {
                using (Stream save = File.Open(saveFileJSON.FileName, FileMode.CreateNew))
                using (StreamWriter toText = new StreamWriter(save))
                {
                    toText.Write(Display_text.Text);
                }
            }
        }

        private void Account_List_Form_Load(object sender, EventArgs e)
        {

        }
    }
}

