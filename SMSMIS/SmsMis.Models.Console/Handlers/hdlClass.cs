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
    public class hdlClass : DbContext
    {
        public hdlClass()

            : base("name=ValencySGIEntities")
        { }

        public IList<Class> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Classes.ToList();
        }
        public IList<Class> SelectAllActive(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Classes.Where(s => s.Status == true && s.CompanyCode == CompanyCode).ToList();
        }

        public void save(Class Class)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Class.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Class);

                    if (entry != null)
                    {
                        if (Class.ClassCode == 0)
                        {
                            Class.ClassCode = Functions.getNextPk("Class", Class.ClassCode, Class.CompanyCode);
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
        public void delete(Class Class)
        {
            try
            {
                var context = new SmsMisDB();
                context.Classes.Attach(Class);
                var entry = context.Entry(Class);
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
