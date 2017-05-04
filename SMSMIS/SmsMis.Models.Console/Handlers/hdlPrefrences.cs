using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlPrefrences : DbContext
    {
        public hdlPrefrences() : base("name=ValencySGIEntities") { }
        public IList<Prefrences> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Prefrences.Where(a=>a.CompanyCode == CompanyCode).ToList();
        }
    }
}



