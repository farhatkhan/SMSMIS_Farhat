using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Admin;
using System.IO;
using System.Drawing;
using SmsMis.Models.Console.Client;

namespace SmsMis.Models.Console.Handlers.Admin
{
    public class hdlParty : DbContext
    {
        public hdlParty()

            : base("name=ValencySGIEntities")
        { }

        //public IList<Branch> SelectAll()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList();
        //}

        //public IList<Branch> SelectActiveBranches()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.Where(s=>s.Status == true).ToList();
        //}
        public IList<Party> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Party.Where(s=> s.CompanyCode == companycode).ToList();
        }

        public IList<Party> SelectAll(int companycode, int branchCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Party.Where(s => s.CompanyCode == companycode && s.BranchCode == branchCode).ToList();
        }


        //public void save(Branch branch, string imgFile, string path)
        //{
        //    try
        //    {
        //        using (var context = new SmsMisDB())
        //        {
        //            if (imgFile != string.Empty)
        //            {
        //                branch.LogoPath = string.Concat("../upload/Branches/", branch.CompanyCode, '_', branch.BranchCode, ".png");
        //            }

        //            branch.AddDateTime = DateTime.Now;
        //            var entry = context.Entry(branch);

        //            if (entry != null)
        //            {
        //                if (branch.BranchCode == 0)
        //                {
        //                    branch.BranchCode = Functions.getNextPk("Branch", branch.BranchCode, branch.CompanyCode);

        //                    if (imgFile != string.Empty)
        //                    {
        //                        branch.LogoPath = string.Concat("../upload/Branches/", branch.CompanyCode, '_', branch.BranchCode, ".png");
        //                    }

        //                    if (branch.BranchContactPersonList != null)
        //                    branch.BranchContactPersonList.ToList<BranchContactPerson>().ForEach(i => i.BranchCode = branch.BranchCode);

        //                    entry.State = EntityState.Added;
        //                }
        //                else
        //                {
        //                    entry.State = EntityState.Modified;
        //                }
        //                if (branch.BranchContactPersonList != null)
        //                {
        //                    branch.BranchContactPersonList.ToList<BranchContactPerson>().ForEach(entry1 => context.Entry(entry1).State = EntityState.Added);
        //                }
        //                context.BranchContactPerson.ToList<BranchContactPerson>().Where(i => i.CompanyCode == branch.CompanyCode && i.BranchCode == branch.BranchCode && i.SrNo > 0).ToList<BranchContactPerson>().ForEach(s => context.Entry(s).State = EntityState.Deleted);
        //                context.SaveChanges();

        //                try
        //                {
        //                    char[] split = { ',' };
        //                    string base64Image = imgFile.Split(split)[1];// data:image/jpeg;base64,
        //                    byte[] imageBytes = Convert.FromBase64String(base64Image);
        //                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        //                    ms.Write(imageBytes, 0, imageBytes.Length);
        //                    Image image = Image.FromStream(ms, true);

        //                    image.Save(string.Concat(path,branch.CompanyCode,'_',branch.BranchCode,".png"));
        //                    ms.Close();
        //                    ms.Dispose();
        //                }
        //                catch (Exception ex) { }


        //            }
        //        }
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //    {
        //        //throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //       // throw ex;
        //    }
        //}
        //public void delete(Branch branch)
        //{
        //    try
        //    {
        //        var context = new SmsMisDB();
        //        context.Branch.Attach(branch);
        //        var entry = context.Entry(branch);
        //        if (entry != null)
        //        {
        //            entry.State = EntityState.Deleted;
        //            context.BranchContactPerson.ToList<BranchContactPerson>().Where(i => i.CompanyCode == branch.CompanyCode && i.BranchCode == branch.BranchCode && i.SrNo > 0).ToList<BranchContactPerson>().ForEach(s => context.Entry(s).State = EntityState.Deleted);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //    {
        //        //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
        //        throw ex;
        //    }
        //}
        //public comBranch SelectByID(System.Guid BranchID)
        //{
        //    comBranch branch = new SGIValencyDB().comBranchList.Find(BranchID);
        //    return branch;
        //}
    }
}
