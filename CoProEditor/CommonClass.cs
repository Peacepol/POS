using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoProEditor
{
    public class DeserializeTypes
    {
        public string company_name;
        public string registration_number;
        public string serial_number;
        public string database_name;
        public string server_name;
        public string db_user;
        public string db_pass;
    }
    class CommonClass
    {
        public static string Encrypt(string pStr)
        {
            if (pStr != null)
            {
                int shft = 5;
                string encrypted = pStr.Select(ch => ((int)ch) << shft).Aggregate("", (current, val) => current + (char)(val * 2));
                encrypted = Convert.ToBase64String(Encoding.UTF8.GetBytes(encrypted));

                return encrypted;
            }
            else
            {
                return "";
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
    }
}
