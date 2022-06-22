using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace AbleRetailPOS.Reports
{
    public class ReportParams
    {
        public Byte PrtOpt;
        public string ReportName;
        public List<DataTable> Rec = new List<DataTable>();
        public  string RptTitle = "";
        public string Params = "";
        public string PVals = "";
        public string SubRpt = "";
        public DataTable tblSubRpt = null;
        public bool TotS = false;
        public string Hidden = "";
        public string fname = "";
        public string HideSec = "";
        public string PapSize = "";

        public SortedList<string, DataTable> children = new SortedList<string, DataTable>();
    }
}
