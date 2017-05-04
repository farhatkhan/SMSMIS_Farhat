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
    public class hdlReligion : DbContext
    {
        public hdlReligion()

            : base("name=ValencySGIEntities")
        { }

        public IList<Religion> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Religion.ToList();
        }

        public void save(Religion Religion)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Religion.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Religion);

                    if (entry != null)
                    {
                        if (Religion.ReligionCode == 0)
                        {
                            Religion.ReligionCode = Functions.getNextPk("Religion", Religion.ReligionCode, 0);
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
        public void delete(Religion Religion)
        {
            try
            {
                var context = new SmsMisDB();
                context.Religion.Attach(Religion);
                var entry = context.Entry(Religion);
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
