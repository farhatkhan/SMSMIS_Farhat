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
    public class hdlCategory : DbContext
    {
        public hdlCategory()

            : base("name=ValencySGIEntities")
        { }

        public IList<Category> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Category.Where(a => a.CompanyCode == companycode).ToList();
        }

        public void save(Category Category, bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Category.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Category);

                    if (entry != null)
                    {
                        if (isNew)
                        {
                            Category.CategoryCode = Functions.getNextPk("Category", Category.CategoryCode, Category.CompanyCode);
                            if (Category.CategoryBranch != null && Category.CategoryBranch.Count > 0)
                                Category.CategoryBranch.ToList().ForEach(i => { i.CategoryCode = Category.CategoryCode; });
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            if (Category.CategoryBranch != null && Category.CategoryBranch.Count > 0)
                                Category.CategoryBranch.ToList().ForEach(i => { i.CategoryCode = Category.CategoryCode; });

                            entry.State = EntityState.Modified;
                        }



                        if (Category.CategoryBranch != null && Category.CategoryBranch.Count > 0)
                            Category.CategoryBranch.ToList<CategoryBranch>().ForEach(x => context.Entry(x).State = EntityState.Added);

                        context.CategoryBranch.ToList().Where(i => i.CategoryCode == Category.CategoryCode && i.CompanyCode == Category.CompanyCode).ToList<CategoryBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);

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
        public void delete(Category Category)
        {
            try
            {
                var context = new SmsMisDB();
                context.Category.Attach(Category);
                var entry = context.Entry(Category);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.CategoryBranch.ToList().Where(i => i.CategoryCode == Category.CategoryCode && i.CompanyCode == Category.CompanyCode).ToList<CategoryBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);
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
