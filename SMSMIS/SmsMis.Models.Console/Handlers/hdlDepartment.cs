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
    public class hdlDepartment : DbContext
    {
        public hdlDepartment()

            : base("name=ValencySGIEntities")
        { }

        public IList<Department> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Department.Where(a => a.CompanyCode == companycode).ToList();
        }

        public void save(Department Department, bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Department.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Department);

                    if (entry != null)
                    {
                        if (isNew)
                        {
                            Department.DepartmentCode = Functions.getNextPk("Department", Department.DepartmentCode, Department.CompanyCode);
                            if (Department.DepartmentBranch != null && Department.DepartmentBranch.Count > 0)
                                Department.DepartmentBranch.ToList().ForEach(i => { i.DepartmentCode = Department.DepartmentCode; });
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            if (Department.DepartmentBranch != null && Department.DepartmentBranch.Count > 0)
                                Department.DepartmentBranch.ToList().ForEach(i => { i.DepartmentCode = Department.DepartmentCode; });

                            entry.State = EntityState.Modified;
                        }



                        if (Department.DepartmentBranch != null && Department.DepartmentBranch.Count > 0)
                            Department.DepartmentBranch.ToList<DepartmentBranch>().ForEach(x => context.Entry(x).State = EntityState.Added);

                        context.DepartmentBranch.ToList().Where(i => i.DepartmentCode == Department.DepartmentCode && i.CompanyCode == Department.CompanyCode).ToList<DepartmentBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);

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
        public void delete(Department Department)
        {
            try
            {
                var context = new SmsMisDB();
                context.Department.Attach(Department);
                var entry = context.Entry(Department);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.DepartmentBranch.ToList().Where(i => i.DepartmentCode == Department.DepartmentCode && i.CompanyCode == Department.CompanyCode).ToList<DepartmentBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);
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
