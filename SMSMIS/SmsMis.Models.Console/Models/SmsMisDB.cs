using System.Data.Entity.Migrations;
using SmsMis.Models.Console.Models;
using SmsMis.Models.Console.Models.SmsMis.Models.Console.Admin;

namespace SmsMis.Models.Console.Handlers.Admin
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using SmsMis.Models.Console.Handlers.Admin;
    using SmsMis.Models.Console.Admin;
    using SmsMis.Models.Console.Client;

    public class SmsMisDB : DbContext//TrackerContext
    {
        public SmsMisDB(): base("name=SmsMisEntities")
        {
            Database.SetInitializer<SmsMisDB>(new CreateDatabaseIfNotExists<SmsMisDB>());
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<BranchSession> BranchSession { get; set; }
        public virtual DbSet<BranchContactPerson> BranchContactPerson { get; set; }
        public virtual DbSet<admUser> admUsers { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Religion> Religion { get; set; }
        public virtual DbSet<SeatType> SeatType { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<ClassCourseSubject> ClassCourseSubject { get; set; }
        public virtual DbSet<MarketingReference> MarketingReference { get; set; }
        
        public virtual DbSet<FeeParticularRecurringType> FeeParticularRecurringType { get; set; }

        public virtual DbSet<FeeParticular> FeeParticular { get; set; }

        public virtual DbSet<User> UserList { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<DesignationBranch> DesignationBranch { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentBranch> DepartmentBranch { get; set; }
        public virtual DbSet<BranchBuilding> BranchBuilding { get; set; }
        public virtual DbSet<DocType> DocType { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<GradeBranch> GradeBranch { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryBranch> CategoryBranch { get; set; }
        public virtual DbSet<SmsMis.Models.Console.Admin.Type> Type { get; set; }
        public virtual DbSet<TypeBranch> TypeBranch { get; set; }
        public virtual DbSet<BranchType> BranchType { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<comUsers> comUserList { get; set; }
        public virtual DbSet<cltUsers> cltUsers { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentMarketingReference> StudentMarketingReference { get; set; }
        public virtual DbSet<StudentLastClassSubject> StudentLastClassSubject { get; set; }
        public virtual DbSet<KinshipDiscount> KinshipDiscount { get; set; }
        public virtual DbSet<StudentKinship> StudentKinship { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<FeePeriod> FeePeriod { get; set; }
        public virtual DbSet<FeeTerm> FeeTerm { get; set; }
        public virtual DbSet<ChartOfAccounts> ChartOfAccounts { get; set; }
        public virtual DbSet<FeeBillPaymentTerms> FeeBillPaymentTerms { get; set; }
        public virtual DbSet<FeeParticularRate> FeeParticularRate { get; set; }
        public virtual DbSet<StudentBillingCycle> StudentBillingCycle { get; set; }
        public virtual DbSet<StudentFeeTermScholarship> StudentFeeTermScholarship { get; set; }
        public virtual DbSet<StudentFixedScholarship> StudentFixedScholarship { get; set; }
        public virtual DbSet<VoucherTypeBranch> VoucherTypeBranch { get; set; }
        public virtual DbSet<VoucherType> VoucherType { get; set; }
        public virtual DbSet<InstrumentSerial> InstrumentSerial { get; set; }
        public virtual DbSet<InstrumentType> InstrumentType { get; set; }
        public virtual DbSet<COALevels> COALevels { get; set; }
        public virtual DbSet<VoucherDetail> VoucherDetail { get; set; }
        public virtual DbSet<VoucherMaster> VoucherMaster { get; set; }
        public virtual DbSet<COABranch> COABranch { get; set; }
        public virtual DbSet<StudentOptionalFeeParticular> StudentOptionalFeeParticular { get; set; }
        public virtual DbSet<SubjectPracticleFee> SubjectPracticleFee { get; set; }
        public virtual DbSet<FeeBillDetail> FeeBillDetails { get; set; }
        public virtual DbSet<FeeBillDetailMonthly> FeeBillDetailMonthlies { get; set; }
        public virtual DbSet<FeeBillMaster> FeeBillMasters { get; set; }
        public virtual DbSet<StudentSubject> StudentSubject { get; set; }

        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Prefrences> Prefrences { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<StudentApplicationCheckList> StudentApplicationCheckList { get; set; }
        public virtual DbSet<StudentAdmission> StudentAdmission { get; set; }
        public virtual DbSet<StudentAdmissionSubject> StudentAdmissionSubject { get; set; }
        public virtual DbSet<FiscalYear> FiscalYear { get; set; }
        #region saif
        public virtual DbSet<InstrumentSerialDetail> InstrumentSerialDetail { get; set; }
        public virtual DbSet<InstrumentSerialMaster> InstrumentSerialMaster { get; set; }
        public virtual DbSet<StudentFixedDiscount> StudentFixedDiscount { get; set; }
        public virtual DbSet<StudentFixedDiscountType> StudentFixedDiscountTypes { get; set; }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectBranch> ProjectBranch { get; set; }
        public virtual DbSet<CostCenter> CostCenter { get; set; }
        public virtual DbSet<CostCenterBranch> CostCenterBranch { get; set; }
        public virtual DbSet<AnalysisType> AnalysisType { get; set; }
        public virtual DbSet<AnalysisTypeBranch> AnalysisTypeBranch { get; set; }

        public virtual DbSet<FeeReceipt> FeeReceipt { get; set; }
        
        public virtual DbSet<ExamType> ExamType { get; set; }
        public virtual DbSet<GradingCriteria> GradingCriteria { get; set; }
        public virtual DbSet<SubjectTeacher> SubjectTeacher { get; set; }
        public virtual DbSet<SubjectPapers> SubjectPapers { get; set; }
        public virtual DbSet<Party> Party { get; set; }
        public virtual DbSet<COAGroup> COAGroup { get; set; }

        public virtual DbSet<CSMaster> CSMaster { get; set; }
        public virtual DbSet<CSDetail> CSDetail { get; set; }
        public virtual DbSet<CSDataAs> CSDataAs { get; set; }
        public virtual DbSet<CSFontStyle> CSFontStyle { get; set; }
        public virtual DbSet<CSObjectBorder> CSObjectBorder { get; set; }
        public virtual DbSet<CSRowAction> CSRowAction { get; set; }

        public virtual DbSet<ItemBrand> ItemBrand { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<ItemGrade> ItemGrade { get; set; }
        public virtual DbSet<ItemModel> ItemModel { get; set; }
        public virtual DbSet<MeasuringUnit> MeasuringUnit { get; set; }

        #endregion
        public string performLogin(User admin, string ip)
        {
            //string encPassword = admin.Password);
            var dbLogin = from lg in UserList
                          where //lg.CompanyUserLoginId == admin.CompanyUserLoginId && 
                          lg.Password == admin.Password
                          select lg.UserId;
            if (dbLogin != null && dbLogin.Any())
            {
                string adminID = dbLogin.Single();

                #region LOG LOGIN ACTIVITY
                //SmsMisDB dbSGILog = new SmsMisDB();
                //dbSGILog.Database.Connection.Open();
                //dbSGILog.Database.Connection.ChangeDatabase("ValencySGILog");

                //loginAcitivtylog x = new loginAcitivtylog();
                //x.adminID = adminID;
                //x.LoginTypeId = 1;
                //x.IP = ip;
                //x.ActivityDateTime = DateTime.Now;
                //dbSGILog.loginAcitivtylogs.Attach(x);
                //var entry = dbSGILog.Entry(x);
                //if (entry != null)
                //{
                //    entry.State = EntityState.Added;
                //    dbSGILog.SaveChanges();
                //    dbSGILog.Database.Connection.Close();
                //}
                #endregion

                return adminID;
            }
            else return "-1";
        }

        public void performLogOut(int adminID, string ip)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SmsMisDB,Configuration>());
            //modelBuilder.Entity<VoucherDetail>()
            //        .HasRequired<VoucherMaster>(s => s.VoucherMaster) // Student entity requires Standard 
            //        .WithMany(s => s.VoucherDetail);
            //modelBuilder.Entity<admUser>()
            //    .Property(e => e.adminLogin)
            //    .IsUnicode(false);

            //modelBuilder.Entity<admUser>()
            //    .Property(e => e.adminPassword)
            //    .IsUnicode(false);
            //modelBuilder.Entity<comBranch>();
        }        
    }   
}
