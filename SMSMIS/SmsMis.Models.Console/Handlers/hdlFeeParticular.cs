using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Admin;
using SmsMis.Models.Console.Client;

namespace SmsMis.Models.Console.Handlers.Admin
{
    public class hdlFeeParticular : DbContext
    {
        public hdlFeeParticular()

            : base("name=ValencySGIEntities")
        { }

        public IList<FeeParticular> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeParticular.ToList();
        }
        public IList<FeeParticular> SelectAllActiveOptional()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeParticular.Where(s => s.Status == true && s.Optional == true).ToList();
        }
        public IList<FeeParticular> SelectAllActive()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeParticular.Where(s => s.Status == true).ToList();
        }
        public IList<FeeParticular> SelectAllActive(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeParticular.Where(s => s.Status == true && s.Optional == true && s.CompanyCode == CompanyCode).ToList();
        }
        public IList<FeeParticular> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeParticular.Where(s => s.CompanyCode == CompanyCode).ToList();
        }

        public void save(FeeParticular FeeParticular)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    FeeParticular.AddDateTime = DateTime.Now;
                    var entry = context.Entry(FeeParticular);

                    if (entry != null)
                    {
                        if (FeeParticular.ParticularCode == 0)
                        {
                            FeeParticular.ParticularCode = Functions.getNextPk("FeeParticular", FeeParticular.ParticularCode, FeeParticular.CompanyCode);
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
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void delete(FeeParticular FeeParticular)
        {
            try
            {
                var context = new SmsMisDB();
                context.FeeParticular.Attach(FeeParticular);
                var entry = context.Entry(FeeParticular);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
