using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Admin;
using SmsMis.Models.Console.Admin;

namespace SmsMis.Models.Console.Handlers.Admin
{
    public class hdlFeeParticularRecurringType : DbContext
    {
        public hdlFeeParticularRecurringType()

            : base("name=ValencySGIEntities")
        { }

        public IList<FeeParticularRecurringType> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeParticularRecurringType.ToList();
        }
    }
}
