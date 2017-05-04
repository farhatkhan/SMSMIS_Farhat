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
    public class hdlItemModel : DbContext
    {
        public hdlItemModel() : base("name=ValencySGIEntities") { }
        public IList<ItemModel> SelectAll(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemModel.Where(a => a.CompanyCode == companyCode).ToList();
        }
        public IList<ItemModel> SelectAllApproved(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemModel.Where(a => a.CompanyCode == companyCode && a.Status == true).ToList();
        }

        public void save(ItemModel ItemModel, string userId, int CompanyCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(ItemModel);
                    if (entry != null)
                    {
                        ItemModel.AddDateTime = DateTime.Now;
                        ItemModel.AddByUserId = userId;

                        if (ItemModel.ModelCode == 0)
                        {
                            ItemModel.ModelCode = Functions.getNextPk("ItemModel", "ItemModel.ModelCode", " WHERE ItemModel.CompanyCode = " + ItemModel.CompanyCode);

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
        public void delete(ItemModel ItemModel)
        {
            try
            {
                var context = new SmsMisDB();
                //context.ItemModel.Attach(ItemModel);
                var entry = context.Entry(ItemModel);
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
