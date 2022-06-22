using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPOS
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

    public class RuleCriteriaPoints{
        public string CriteriaName; //"item", "supplier"...
        public string CriteriaValue; //itemid, supplierid...
    }
    public class RuleCriteria 
    {
        public string CriteriaName; //"item", "supplier"...
        public string CriteriaValue; //itemid, supplierid...
    }

    public class ActivateResult
    {
        public string Status;
        public string Message;
        public string Company;
        public bool IsActivated;
    }

    public class ApiElements
    {
        public string apihost;
        public int apiport;
        public string apiprotocol;
        public string apimethod;
    }

    public class Account
    {
        public string AccountID;
        public string ParentAccountID;
        public string IsInactive;
        public string AccountName;
        public string AccountNumber;
        public string TaxCode;
        public string CurrencyID;
        public string CurrencyExchangeAccountID;
        public string AccountClassificationID;
        public string SubAccountClassificationID;
        public string AccountLevel;
        public string AccountTypeID;
        public string LastChequeNumber;
        public string IsReconciled;
        public string LastReconciledDate;
        public string StatementBalance;
        public string IsCreditBalance;
        public string OpeningAccountBalance;
        public string CurrentAccountBalance;
        public string PreLastYearActivity;
        public string LastYearOpeningBalance;
        public string ThisYearOpeningBalance;
        public string PostThisYearActivity;
        public string AccountDescription;
        public string IsTotal;
        public string CashFlowClassificationID;
        public string BSBCode;
        public string BankAccountNumber;
        public string BankAccountName;
        public string CompanyTradingName;
        public string CreateBankFiles;
        public string BankCode;
        public string DirectEntryUserID;
        public string IsSelfBalancing;
        public string StatementParticulars;
        public string StatementCode;
        public string StatementReference;
        public string AccountantLinkCode;
        public string IndustryClassification;
        public string TypeOfBusiness;
        public string IsHeader;
    }

    public class EmailFields
    {
        public string smtphost;
        public Int32 smtpport;
        public string usermail;
        public string userpassword;
    }

}
