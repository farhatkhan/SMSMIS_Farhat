using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlFeeTerm: DbContext
    {
        public hdlFeeTerm() : base("name=ValencySGIEntities") { }
        public IList<FeeTerm> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeTerm.ToList();
        }
        public IList<FeeTerm> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeTerm.Where(s=>s.CompanyCode == companycode && s.Status == true).ToList();
        }
        //public FeeTerm SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(FeeTerm FeeTerm, string userId)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(FeeTerm);
                    if (entry != null)
                    {
                        FeeTerm.AddDateTime = DateTime.Now;
                        FeeTerm.AddByUserId = userId;
                        if (FeeTerm.FeeTermCode == 0)
                        {
                            FeeTerm.FeeTermCode = Functions.getNextPk("FeeTerm", FeeTerm.FeeTermCode, FeeTerm.CompanyCode);
                            entry.State = EntityState.Added;
                        }
                        else entry.State = EntityState.Modified;

                        context.SaveChanges();
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }
        public void delete(FeeTerm FeeTerm)
        {
            try
            {
                var context = new SmsMisDB();
                context.FeeTerm.Attach(FeeTerm);
                var entry = context.Entry(FeeTerm);
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
