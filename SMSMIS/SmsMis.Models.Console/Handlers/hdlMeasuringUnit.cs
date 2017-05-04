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
    public class hdlMeasuringUnit : DbContext
    {
        public hdlMeasuringUnit() : base("name=ValencySGIEntities") { }
        public IList<MeasuringUnit> SelectAll(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.MeasuringUnit.Where(a => a.CompanyCode == companyCode).ToList();
        }
        public IList<MeasuringUnit> SelectAllApproved(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.MeasuringUnit.Where(a => a.CompanyCode == companyCode && a.Status == true).ToList();
        }
        
        public void save(MeasuringUnit MeasuringUnit, string userId, int CompanyCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(MeasuringUnit);
                    if (entry != null)
                    {
                        MeasuringUnit.AddDateTime = DateTime.Now;
                        MeasuringUnit.AddByUserId = userId;

                        if (MeasuringUnit.UnitCode == 0)
                        {
                            MeasuringUnit.UnitCode = Functions.getNextPk("MeasuringUnit", "MeasuringUnit.UnitCode", " WHERE MeasuringUnit.CompanyCode = " + MeasuringUnit.CompanyCode);

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
        public void delete(MeasuringUnit MeasuringUnit)
        {
            try
            {
                var context = new SmsMisDB();
                //context.MeasuringUnit.Attach(MeasuringUnit);
                var entry = context.Entry(MeasuringUnit);
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
