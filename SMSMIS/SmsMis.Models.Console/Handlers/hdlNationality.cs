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
    public class hdlNationality : DbContext
    {
        public hdlNationality()

            : base("name=ValencySGIEntities")
        { }

        public IList<Nationality> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Nationality.ToList();
        }

        public void save(Nationality Nationality)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Nationality.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Nationality);

                    if (entry != null)
                    {
                        if (Nationality.NationalityCode == 0)
                        {
                            Nationality.NationalityCode = Functions.getNextPk("Nationality", Nationality.NationalityCode, 0);
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
        public void delete(Nationality Nationality)
        {
            try
            {
                var context = new SmsMisDB();
                context.Nationality.Attach(Nationality);
                var entry = context.Entry(Nationality);
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
