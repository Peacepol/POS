using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitchenDisplay
{
    class CommonClass
    {
        //User Variables
        public static string UserName;
        public static string UserID;
        public static bool isSalesperson;
        public static bool isSupervisor;
        public static bool isAdministrator;
        public static bool isTechnician;

        //Company Variables
        public static string CompName;
        public static string CompAddress;
        public static int CurFY;
        public static string CompSalesTaxNo;
        public static int MaxTerminalAllowed;
        public static string CompLogoPath = "";


        public static string ConStr;

        private static string s_LoggedInCompany;
        private static string s_LoggedInSerialNo;
        private static string s_LoggedInRegNo;
        private static string s_LoggedInDbName;
        private static string s_LoggedInServerName;
        private static string s_DbUser;
        private static string s_DbPass;
        private static bool s_IsLocked;

        public static string LoggedInCompany
        {
            get { return s_LoggedInCompany; }
            set { s_LoggedInCompany = value; }
        }

        public static string LoggedInSerialNo
        {
            get { return s_LoggedInSerialNo; }
            set { s_LoggedInSerialNo = value; }
        }

        public static string LoggedInRegNo
        {
            get { return s_LoggedInRegNo; }
            set { s_LoggedInRegNo = value; }
        }
        public enum RunSqlInsertMode
        {
            QUERY = 0,
            SCALAR
        }
        public static string LoggedInDbName
        {
            get { return s_LoggedInDbName; }
            set { s_LoggedInDbName = value; }
        }

        public static string LoggedInServerName
        {
            get { return s_LoggedInServerName; }
            set { s_LoggedInServerName = value; }
        }

        public static string DbUser
        {
            get { return s_DbUser; }
            set { s_DbUser = value; }
        }

        public static string DbPass
        {
            get { return s_DbPass; }
            set { s_DbPass = value; }
        }
        public static bool IsLocked
        {
            get { return s_IsLocked; }
            set { s_IsLocked = value; }
        }


        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                //Convert to text
                var hashedInputStringBuilder = new System.Text.StringBuilder(64);
                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }

        public static bool AppLogin(string pServerName,
                                   string pDbName,
                                   string pUname,
                                   string pPwd,
                                   string pDbUname,
                                   string pDbPwd)
        {
            SqlConnection con_ = null;
            try
            {
                string salesperon;
                string supervisor;
                string administrator;
                string technician;
                con_ = new SqlConnection(ConStr);
                String Sql = "SELECT * FROM Users WHERE user_name = '" + pUname + "' AND user_pwd = '" + SHA512(pPwd) + "'";

                SqlCommand cmd_ = new SqlCommand(Sql, con_);
                con_.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_;
                DataTable ltb = new DataTable();
                da.Fill(ltb);
                if (ltb.Rows.Count == 1)
                {
                    UserName = ltb.Rows[0]["user_name"].ToString();
                    UserID = ltb.Rows[0]["user_id"].ToString();
                    salesperon = ltb.Rows[0]["IsSalesperson"].ToString();
                    supervisor = ltb.Rows[0]["IsSupervisor"].ToString();
                    administrator = ltb.Rows[0]["IsAdministrator"].ToString();
                    technician = ltb.Rows[0]["IsTechnician"].ToString();
                    isSalesperson = bool.Parse(salesperon);
                    isSupervisor = bool.Parse(supervisor);
                    isAdministrator = bool.Parse(administrator);
                    isTechnician = bool.Parse(technician);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to connect to the selected server or database doest not exist");
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (con_ != null)
                    con_.Close();
            }
        }
        public static string Decrypt(string pStr)
        {
            if (pStr != null)
            {
                int shft = 5;
                byte[] ub64str = Convert.FromBase64String(pStr);
                string decrypted = Encoding.UTF8.GetString(ub64str).Select(ch => ((int)ch) >> shft).Aggregate("", (current, val) => current + (char)(val / 2));

                return decrypted;
            }
            else
            {
                return "";
            }
        }
        public static void InitCompanyFile()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConStr);
                connection.Open();
                string sql = "SELECT TOP 1 * FROM DataFileInformation";
                SqlCommand cmd_ = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd_;
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    CompName = dt.Rows[0]["CompanyName"].ToString();
                    CurFY = DateTime.Now.Year;

                    CompAddress = dt.Rows[0]["Address"].ToString();
                    CompSalesTaxNo = dt.Rows[0]["SalesTaxNumber"].ToString();
                    MaxTerminalAllowed = int.Parse(dt.Rows[0]["MaxTerminal"].ToString());
                    CompLogoPath = Application.StartupPath + "\\" + (dt.Rows[0]["CompanyLogo"] != null ? dt.Rows[0]["CompanyLogo"].ToString() : "");
                } 
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        public static int runSql(ref DataTable dtOutput, string sql, Dictionary<string, object> valueParams = null)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConStr);
                SqlCommand cmd = new SqlCommand(sql, con);
                if (valueParams != null)
                {
                    foreach (KeyValuePair<string, object> param in valueParams)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dtOutput.Clear();
                da.Fill(dtOutput);
                return dtOutput.Rows.Count;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public static int runSql(string sql, RunSqlInsertMode mode = RunSqlInsertMode.QUERY, Dictionary<string, object> valueParams = null)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConStr);
                con.Open();
                if (mode == RunSqlInsertMode.SCALAR)
                {
                    sql += "; SELECT SCOPE_IDENTITY()";
                }
                SqlCommand cmd = new SqlCommand(sql, con);
                if (valueParams != null)
                {
                    foreach (KeyValuePair<string, object> param in valueParams)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                int returnvalue = 0;
                switch (mode)
                {
                    case RunSqlInsertMode.QUERY:
                        returnvalue = cmd.ExecuteNonQuery();
                        break;
                    case RunSqlInsertMode.SCALAR:
                        returnvalue = Convert.ToInt32(cmd.ExecuteScalar());
                        break;
                }
                return returnvalue;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
    }
}
