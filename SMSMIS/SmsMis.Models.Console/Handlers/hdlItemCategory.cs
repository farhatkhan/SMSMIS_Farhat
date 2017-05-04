using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlItemCategory : DbContext
    {
        public hdlItemCategory() : base("name=ValencySGIEntities") { }
        public IList<ItemCategory> SelectAll(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemCategory.Where(a => a.CompanyCode == companyCode).ToList();
        }
        public IList<ItemCategory> SelectAllApproved(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemCategory.Where(a => a.CompanyCode == companyCode && a.Status == true).ToList();
        }

        public void save(ItemCategory ItemCategory, string userId, int CompanyCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(ItemCategory);
                    if (entry != null)
                    {
                        ItemCategory.AddDateTime = DateTime.Now;
                        ItemCategory.AddByUserId = userId;

                        if (ItemCategory.CategoryCode == 0)
                        {
                            ItemCategory.CategoryCode = Functions.getNextPk("ItemCategory", "ItemCategory.CategoryCode", " WHERE ItemCategory.CompanyCode = " + ItemCategory.CompanyCode);

                            entry.State = EntityState.Added;
                        }
                        else entry.State = EntityState.Modified;

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
        public void delete(ItemCategory ItemCategory)
        {
            try
            {
                var context = new SmsMisDB();
                //context.ItemCategory.Attach(ItemCategory);
                var entry = context.Entry(ItemCategory);
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
