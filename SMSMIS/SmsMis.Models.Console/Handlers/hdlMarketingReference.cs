using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Admin;

namespace SmsMis.Models.Console.Handlers.Admin
{
    public class hdlMarketingReference : DbContext
    {
        public hdlMarketingReference()

            : base("name=ValencySGIEntities")
        { }

        public IList<MarketingReference> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.MarketingReference.ToList();
        }

        public void save(MarketingReference MarketingReference)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    MarketingReference.AddDateTime = DateTime.Now;
                    var entry = context.Entry(MarketingReference);

                    if (entry != null)
                    {
                        if (MarketingReference.MarketingReferenceCode == 0)
                        {
                            MarketingReference.MarketingReferenceCode = Functions.getNextPk("MarketingReference", MarketingReference.MarketingReferenceCode, 0);
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            entry.State = EntityState.Modified;
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //throw ex;
            }
            catch (Exception ex)
            {
               // throw ex;
            }
        }
        public void delete(MarketingReference MarketingReference)
        {
            try
            {
                var context = new SmsMisDB();
                context.MarketingReference.Attach(MarketingReference);
                var entry = context.Entry(MarketingReference);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
                throw ex;
            }
        }
    }
}
