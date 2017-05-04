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
    public class hdlItemBrand : DbContext
    {
        public hdlItemBrand() : base("name=ValencySGIEntities") { }
        public IList<ItemBrand> SelectAll(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemBrand.Where(a => a.CompanyCode == companyCode).ToList();
        }
        public IList<ItemBrand> SelectAllApproved(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemBrand.Where(a => a.CompanyCode == companyCode && a.Status == true).ToList();
        }

        public void save(ItemBrand ItemBrand, string userId, int CompanyCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(ItemBrand);
                    if (entry != null)
                    {
                        ItemBrand.AddDateTime = DateTime.Now;
                        ItemBrand.AddByUserId = userId;

                        if (ItemBrand.BrandCode == 0)
                        {
                            ItemBrand.BrandCode = Functions.getNextPk("ItemBrand", "ItemBrand.BrandCode", " WHERE ItemBrand.CompanyCode = " + ItemBrand.CompanyCode);

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
        public void delete(ItemBrand ItemBrand)
        {
            try
            {
                var context = new SmsMisDB();
                //context.ItemBrand.Attach(ItemBrand);
                var entry = context.Entry(ItemBrand);
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
