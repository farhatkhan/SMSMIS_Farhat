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
    public class hdlBranchBuilding : DbContext
    {
        public hdlBranchBuilding()

            : base("name=ValencySGIEntities")
        { }

        public IList<BranchBuilding> SelectAll(string strValues)
        {

            string[] strArray = strValues.ToString().Split('/');

            int CompanyCode = Convert.ToInt32(strArray[0]);
            int BranchCode = Convert.ToInt32(strArray[1]);

            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.BranchBuilding.Where(s=>s.CompanyCode==CompanyCode && s.BranchCode == BranchCode).ToList();
        }

        public void save(BranchBuilding BranchBuilding)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    BranchBuilding.AddDateTime = DateTime.Now;
                    var entry = context.Entry(BranchBuilding);

                    if (entry != null)
                    {
                        if (BranchBuilding.BuildingCode == 0)
                        {
                            BranchBuilding.BuildingCode = Functions.getNextPk("BranchBuilding", BranchBuilding.BuildingCode, BranchBuilding.CompanyCode, BranchBuilding.BranchCode);
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
                throw ex;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        public void delete(BranchBuilding BranchBuilding)
        {
            try
            {
                var context = new SmsMisDB();
                context.BranchBuilding.Attach(BranchBuilding);
                var entry = context.Entry(BranchBuilding);
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
