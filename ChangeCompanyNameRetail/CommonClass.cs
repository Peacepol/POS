using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeCompanyNameRetail
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
        public static string[] decodeActivationKey2(string pActivationKey, bool activation = true)
        {
            string[] activationelements = base64Decode(pActivationKey).Split(',');
            if (activationelements.Count() != 4)
            {
                string[] notvalid = { "Not Valid" };
                return notvalid;
            }

            string compnamestr = activationelements[0];
            string regnostr = activationelements[1];
            string serialnostr = activationelements[2];
            string maxuserstr = activationelements[3];

            string compnamedecodedstr = "";
            string regnodecodedstr = "";
            string serialnodecodedstr = "";
            string maxuserdecodedstr = "";

            for (short iterator = 0; iterator < compnamestr.Length; iterator += 2)
            {
                string hexacode = compnamestr.ElementAt(iterator).ToString() + compnamestr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                compnamedecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            for (short iterator = 0; iterator < regnostr.Length; iterator += 2)
            {
                string hexacode = regnostr.ElementAt(iterator).ToString() + regnostr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                regnodecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            for (short iterator = 0; iterator < serialnostr.Length; iterator += 2)
            {
                string hexacode = serialnostr.ElementAt(iterator).ToString() + serialnostr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                serialnodecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            for (short iterator = 0; iterator < maxuserstr.Length; iterator += 2)
            {
                string hexacode = maxuserstr.ElementAt(iterator).ToString() + maxuserstr.ElementAt(iterator + 1).ToString();
                string decimalcode = short.Parse(hexacode, System.Globalization.NumberStyles.HexNumber).ToString();
                maxuserdecodedstr += char.ConvertFromUtf32(Int32.Parse(decimalcode));
            }

            string[] activationkeygroup = { compnamedecodedstr, regnodecodedstr, serialnodecodedstr, maxuserdecodedstr };

            return activationkeygroup;
        }
        public static string base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
