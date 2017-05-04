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
    public class hdlSection : DbContext
    {
        public hdlSection()

            : base("name=ValencySGIEntities")
        { }

        //public IList<Section> SelectAll()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Section.ToList();
        //}
        //public IList<Section> SelectAllActive()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Section.Where(s => s.Status == true).ToList();
        //}

        public void save(Section Section)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Section.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Section);

                    if (entry != null)
                    {
                        if (Section.SectionCode == 0)
                        {
                            Section.SectionCode = Functions.getNextPk("Section", Section.SectionCode, Section.CompanyCode);
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
        public void delete(Section Section)
        {
            try
            {
                var context = new SmsMisDB();
                context.Section.Attach(Section);
                var entry = context.Entry(Section);
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
