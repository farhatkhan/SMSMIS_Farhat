using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMis.Models.Console.ViewModel
{
    public partial class VoucherTypeVM
    {
        public int VoucherTypeRowNumber { get; set; }
        public int VoucherCode { get; set; }
        public string VoucherName { get; set; }
        public string AccountCode { get; set; }
        public string AccountTitle { get; set; }
        public string Category { get; set; }
        public string Frequency { get; set; }
        public string TransactionType { get; set; }

        public int CompanyCode { get; set; }
        public int BranchCode { get; set; }
        public string CurrencyName { get; set; }
        public int CurrencyCode { get; set; }
        public int LocalCurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string LocalCurrencySymbol { get; set; }

    }

    //,TransactionType, ,CompanyCode,BranchCode

}
