using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlCOALevels : DbContext
    {
        public hdlCOALevels() : base("name=ValencySGIEntities") { }
        public IList<COALevels> SelectCOALevels(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.COALevels.ToList().Where(s => s.CompanyCode == CompanyCode).ToList();
        }
    }
}
