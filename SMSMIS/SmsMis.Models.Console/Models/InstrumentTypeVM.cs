using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMis.Models.Console.Models
{
    public class InstrumentTypeVM
    {
        public int InstrumentTypeRowNumber { get; set; }        
        public string AccountCode { get; set; }
        public int InstrumentTypeCode { get; set; }
        public string InstrumentName { get; set; }

        public int CompanyCode { get; set; }
        public int BranchCode { get; set; }
    }
}
