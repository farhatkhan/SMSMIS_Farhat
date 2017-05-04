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
    public class hdlSubjectPracticleFee : DbContext
    {
        public hdlSubjectPracticleFee() : base("name=ValencySGIEntities") { }
        public IList<SubjectPracticleFee> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.SubjectPracticleFee.ToList();
        }
        public IList<SubjectPracticleFee> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.SubjectPracticleFee.Where(s => s.CompanyCode == CompanyCode).ToList();
        }
        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(IList<SubjectPracticleFee> SubjectPracticleFee, string userId, int CompanyCode, int BranchCode, int SessionCode, int ClassCode, int CourseCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {

                    if (SubjectPracticleFee != null)
                    {
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.AddDateTime = DateTime.Now);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.AddByUserId = userId);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.CompanyCode = CompanyCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.BranchCode = BranchCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.SessionCode = SessionCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.ClassCode = ClassCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.CourseCode = CourseCode);
                        //if(SubjectPracticleFee)
                        //Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                        foreach (var row in SubjectPracticleFee)
                        {
                            context.SubjectPracticleFee.Add(row);
                        }
                        //entry.State = EntityState.Added;
                        context.SubjectPracticleFee.ToList<SubjectPracticleFee>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode && s.ClassCode == ClassCode && s.CourseCode == CourseCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.SubjectPracticleFee.ToList<SubjectPracticleFee>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode && s.ClassCode == ClassCode && s.CourseCode == CourseCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

                    context.SaveChanges();

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
        public void delete(IList<SubjectPracticleFee> SubjectPracticleFee)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (SubjectPracticleFee != null)
                    {
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.AddDateTime = DateTime.Now);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.AddByUserId = SubjectPracticleFee[0].AddByUserId);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.CompanyCode = SubjectPracticleFee[0].CompanyCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.BranchCode = SubjectPracticleFee[0].BranchCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.SessionCode = SubjectPracticleFee[0].SessionCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.ClassCode = SubjectPracticleFee[0].ClassCode);
                        SubjectPracticleFee.ToList<SubjectPracticleFee>().ForEach(i => i.CourseCode = SubjectPracticleFee[0].CourseCode);
                        foreach (var row in SubjectPracticleFee)
                        {
                            context.SubjectPracticleFee.Add(row);
                        }
                        context.SubjectPracticleFee.ToList<SubjectPracticleFee>().Where(s => s.CompanyCode == SubjectPracticleFee[0].CompanyCode && s.BranchCode == SubjectPracticleFee[0].BranchCode && s.SessionCode == SubjectPracticleFee[0].SessionCode && s.ClassCode == SubjectPracticleFee[0].ClassCode && s.CourseCode == SubjectPracticleFee[0].CourseCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.SubjectPracticleFee.ToList<SubjectPracticleFee>().Where(s => s.CompanyCode == SubjectPracticleFee[0].CompanyCode && s.BranchCode == SubjectPracticleFee[0].BranchCode && s.SessionCode == SubjectPracticleFee[0].SessionCode && s.ClassCode == SubjectPracticleFee[0].ClassCode && s.CourseCode == SubjectPracticleFee[0].CourseCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
