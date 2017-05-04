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
    public class hdlSubject : DbContext
    {
        public hdlSubject()

            : base("name=ValencySGIEntities")
        { }

        //public IList<Subject> SelectAll()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Subject.ToList();
        //}
        //public IList<Subject> SelectAllActive()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Subject.Where(s => s.Status == true).ToList();
        //}

        public void save(Subject Subject)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Subject.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Subject);

                    if (entry != null)
                    {
                        if (Subject.SubjectCode == 0)
                        {
                            Subject.SubjectCode = Functions.getNextPk("Subject", Subject.SubjectCode, 0);
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
        public void delete(Subject Subject)
        {
            try
            {
                var context = new SmsMisDB();
                context.Subject.Attach(Subject);
                var entry = context.Entry(Subject);
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
