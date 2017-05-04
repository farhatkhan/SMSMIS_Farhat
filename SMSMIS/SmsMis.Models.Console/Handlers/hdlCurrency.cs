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
    public class hdlCurrency : DbContext
    {
        public hdlCurrency() : base("name=ValencySGIEntities") { }
        public IList<Currency> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Currency.ToList();
        }
        public IList<Currency> SelectAllActive()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Currency.Where(s=> s.Status==true).ToList();
        }

        //public Currency SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(Currency Currency, string userId)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(Currency);
                    if (entry != null)
                    {
                        Currency.AddDateTime = DateTime.Now;
                        Currency.AddByUserId = userId;

                        if (Currency.CurrencyCode == 0)
                        {
                            Currency.CurrencyCode = Functions.getNextPk("Currency", "CurrencyCode", string.Empty);
                            entry.State = EntityState.Added;
                        }
                        else entry.State = EntityState.Modified;

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
        public void delete(Currency Currency)
        {
            try
            {
                var context = new SmsMisDB();
                context.Currency.Attach(Currency);
                var entry = context.Entry(Currency);
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



