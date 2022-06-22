using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AbleRetailPOS.Sales
{
    class StatementMailer
    {
        private string mFromAddress;
        private string mSubject;
        private string mBody;
        private Dictionary<string, List<string>> mAttachments;
        private string mServerAddr = "127.0.0.1";
        private Int32 mServerPort = 25;
        private string mSMTPUser;
        private string mSMTPPassword;
        private string mProfileIDs;

        public string From
        {
            get { return mFromAddress; }
            set { mFromAddress = value; }
        }

        public string Subject
        {
            get { return mSubject; }
            set { mSubject = value; }
        }

        public string Body
        {
            get { return mBody; }
            set { mBody = value; }
        }

        public string Profiles
        {
            get { return mProfileIDs; }
            set { mProfileIDs = value; }
        }

        public StatementMailer(string pServerAddr, Int32 pPort, string pUser, string pPassword)
        {
            mServerAddr = pServerAddr;
            mServerPort = pPort;
            mSMTPUser = pUser;
            mSMTPPassword = pPassword;
            mAttachments = new Dictionary<string, List<string>>();
        }

        public bool MailAttachments(DateTime pStatementDate)
        {
            SqlConnection con = null;
            try
            {
                string lCustomersSql = @"SELECT GrandTotal, 
                                            p.Name,
                                            p.ProfileIDNumber,
                                            SalesNumber,
                                            TransactionDate,
                                            TotalPaid, 
                                            Memo,
                                            Street,
                                            City,
                                            State, 
                                            Postcode, 
                                            Country, 
                                            p.ABN,
                                            c.email
                                        FROM Sales s
                                        INNER JOIN Profile p ON s.CustomerID = p.ID
                                        INNER JOIN Contacts c ON p.ID = c.ProfileID
                                        WHERE InvoiceStatus = 'Open' AND c.Location = p.LocationID
                                        AND TransactionDate <= @trandate";
                if (mProfileIDs != "")
                {
                    lCustomersSql += " AND p.ID IN (" + mProfileIDs + ")";
                }

                con = new SqlConnection(CommonClass.ConStr);
                SqlCommand cmd = new SqlCommand(lCustomersSql, con);
                cmd.Parameters.AddWithValue("@trandate", pStatementDate);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable TbRep = new DataTable();
                da.Fill(TbRep);
                if (TbRep.Rows.Count > 0)
                {
                    foreach (DataRow pTbRepLeaf in TbRep.Rows)
                    {
                        DataTable lCustTable = pTbRepLeaf.Table.Clone();
                        lCustTable.ImportRow(pTbRepLeaf);
                        string lPDFPath = "./" + pTbRepLeaf["Name"].ToString() + pTbRepLeaf["ProfileIDNumber"].ToString() + ".pdf";
                        PrintDataTableToPDF(ref lCustTable, lPDFPath);
                        List<string> lCustAttachments = new List<string>();
                        lCustAttachments.Add(lPDFPath);
                        mAttachments.Add(pTbRepLeaf["email"].ToString(), lCustAttachments);
                        SendEmail(pTbRepLeaf["email"].ToString());
                    }
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private bool SendEmail(string pToEmailAddr)
        {
            System.Net.Mail.SmtpClient lMailClient = new System.Net.Mail.SmtpClient(mServerAddr, mServerPort);
            System.Net.Mail.MailMessage lMailMsg = new System.Net.Mail.MailMessage();
            lMailMsg.From = new System.Net.Mail.MailAddress(mFromAddress);
            lMailMsg.Subject = mSubject.Trim();
            lMailMsg.Body = mBody.Trim();
            lMailMsg.IsBodyHtml = false;
            lMailMsg.To.Add(pToEmailAddr);

            lMailClient.Credentials = new System.Net.NetworkCredential(mSMTPUser, mSMTPPassword);
            foreach (String pAttachments in mAttachments[pToEmailAddr])
            {
                lMailMsg.Attachments.Add(new System.Net.Mail.Attachment(pAttachments));
            }

            lMailClient.Send(lMailMsg);
            return true;
        }

        private void PrintDataTableToPDF(ref DataTable pDataTable, string pPDFPath)
        {
            Reports.ReportParams custstatmntparams = new Reports.ReportParams();
            custstatmntparams.PrtOpt = 2; //save to disk
            custstatmntparams.Rec.Add(pDataTable);

            custstatmntparams.ReportName = "SalesStatements.rpt";
            custstatmntparams.RptTitle = "Customer Statements";
            custstatmntparams.fname = pPDFPath;
            custstatmntparams.Params = "compname";
            custstatmntparams.PVals = CommonClass.CompName.Trim();

            CommonClass.ShowReport(custstatmntparams);
        }
    }
}
