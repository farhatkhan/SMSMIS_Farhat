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
    public class hdlCourse : DbContext
    {
        public hdlCourse()

            : base("name=ValencySGIEntities")
        { }

        //public IList<Course> SelectAll()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Course.ToList();
        //}
        //public IList<Course> SelectAllActive()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Course.Where(s => s.Status == true).ToList();
        //}

        public void save(Course Course)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Course.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Course);

                    if (entry != null)
                    {
                        if (Course.CourseCode == 0)
                        {
                            Course.CourseCode = Functions.getNextPk("Course", Course.CourseCode, Course.CompanyCode);
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
        public void delete(Course Course)
        {
            try
            {
                var context = new SmsMisDB();
                context.Course.Attach(Course);
                var entry = context.Entry(Course);
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
