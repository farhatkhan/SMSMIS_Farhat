using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlProject : DbContext
    {
        public hdlProject() : base("name=ValencySGIEntities") { }
        public IList<Project> SelectAllCompanyProjects(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Project.Where(s => s.CompanyCode == CompanyCode).ToList();
        }
        
        public IList<Project> SelectAllBranchProjects(int CompanyCode, int BranchCode)
        {
            SmsMisDB db = new SmsMisDB();
            var projectCodes = db.ProjectBranch.Where(x => x.CompanyCode == CompanyCode && x.BranchCode == BranchCode).Select(x => x.ProjectCode).ToList();
            var result = db.Project.Where(x => projectCodes.Contains(x.ProjectCode)).ToList();
            return result;
        }

        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(Project Project, string userId, int CompanyCode)
        {
            try
            {

                using (var context = new SmsMisDB())
                {
                    var mainentry = context.Entry(Project);
                    if (mainentry != null)
                    {
                        Project.AddDateTime = DateTime.Now;
                        Project.AddByUserId = userId;
                        Project.CompanyCode = CompanyCode;
                        if (Project.ProjectCode == 0)
                        {
                            Project.ProjectCode = Functions.getNextPk("Project", "ProjectCode", string.Concat(" Where CompanyCode =", Project.CompanyCode));
                            mainentry.State = EntityState.Added;
                            context.Project.Add(Project);
                        }
                        else
                        {
                            if (Project.ProjectBranch != null && Project.ProjectBranch.Count > 0)
                            {
                                Project.ProjectBranch.ToList().ForEach(i => { i.CompanyCode = Project.CompanyCode; });
                                //Project.ProjectBranch.ToList().ForEach(i => { i.BranchCode = Project.BranchCode; });
                                Project.ProjectBranch.ToList().ForEach(i => { i.ProjectCode = Project.ProjectCode; });
                                //Project.ProjectBranch.ToList().ForEach(i => { i.VoucherCode = Project.VoucherCode; });

                            }
                            mainentry.State = EntityState.Modified;
                        }
                        //if(Project)
                        //Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                        //foreach (var row in Project)
                        if (Project.ProjectBranch != null && Project.ProjectBranch.Count > 0)
                            Project.ProjectBranch.ToList<ProjectBranch>().ForEach(entry => context.Entry(entry).State = EntityState.Added);
                        context.ProjectBranch.ToList().Where(i => i.ProjectCode == Project.ProjectCode && i.CompanyCode == Project.CompanyCode).ToList<ProjectBranch>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
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
        public void delete(IList<Project> Project)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (Project != null)
                    {
                        Project.ToList<Project>().ForEach(i => i.AddDateTime = DateTime.Now);
                        Project.ToList<Project>().ForEach(i => i.AddByUserId = Project[0].AddByUserId);
                        Project.ToList<Project>().ForEach(i => i.CompanyCode = Project[0].CompanyCode);

                        foreach (var row in Project)
                        {
                            context.Project.Add(row);
                        }
                        context.Project.ToList<Project>().Where(s => s.CompanyCode == Project[0].CompanyCode ).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.Project.ToList<Project>().Where(s => s.CompanyCode == Project[0].CompanyCode ).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
