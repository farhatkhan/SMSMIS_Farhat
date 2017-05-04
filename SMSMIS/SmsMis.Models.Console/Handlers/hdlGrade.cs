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
    public class hdlGrade : DbContext
    {
        public hdlGrade()

            : base("name=ValencySGIEntities")
        { }

        public IList<Grade> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Grade.Where(a => a.CompanyCode == companycode).ToList();
        }

        public void save(Grade Grade,bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Grade.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Grade);

                    if (entry != null)
                    {
                        if (isNew)
                        {
                            Grade.GradeCode = Functions.getNextPk("Grade", Grade.GradeCode, Grade.CompanyCode);
                            if (Grade.GradeBranch != null && Grade.GradeBranch.Count > 0)
                                Grade.GradeBranch.ToList().ForEach(i => { i.GradeCode = Grade.GradeCode; });
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            if (Grade.GradeBranch != null && Grade.GradeBranch.Count > 0)
                                Grade.GradeBranch.ToList().ForEach(i => { i.GradeCode = Grade.GradeCode; });

                            entry.State = EntityState.Modified;
                        }

                        

                        if (Grade.GradeBranch != null && Grade.GradeBranch.Count > 0)
                            Grade.GradeBranch.ToList<GradeBranch>().ForEach(x => context.Entry(x).State = EntityState.Added);

                        context.GradeBranch.ToList().Where(i => i.GradeCode == Grade.GradeCode && i.CompanyCode == Grade.CompanyCode).ToList<GradeBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);

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
        public void delete(Grade Grade)
        {
            try
            {
                var context = new SmsMisDB();
                context.Grade.Attach(Grade);
                var entry = context.Entry(Grade);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.GradeBranch.ToList().Where(i => i.GradeCode == Grade.GradeCode && i.CompanyCode == Grade.CompanyCode).ToList<GradeBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);
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
