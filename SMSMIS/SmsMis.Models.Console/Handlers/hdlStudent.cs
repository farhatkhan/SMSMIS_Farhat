using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsMis.Models.Console.Handlers.Admin;
using System.Data;
using Microsoft.ApplicationBlocks.Data;


namespace SmsMis.Models.Console.Handlers.Client
{
    public class hdlStudent : DbContext
    {
        public hdlStudent() : base("name=ValencySGIEntities") { }

        public IList<object> SelectAllStudentsByCompanyBranchSession(string strValues)
        {
            string[] strArray = strValues.ToString().Split('/');

            int companyCode = Convert.ToInt32(strArray[0]);
            int BranchCode = Convert.ToInt32(strArray[1]);
            int SessionCode = Convert.ToInt32(strArray[2]);

            SmsMisDB db = new SmsMisDB();

            var otherItems = (from br in db.Student
                              select new
                              {
                                  CompanyCode = br.CompanyCode,
                                  BranchCode = br.BranchCode,
                                  SessionCode = br.SessionCode,
                                  FirstName = br.FirstName,
                                  FullName = br.FullName,
                                  StudentNo = br.StudentNo
                              }).
                              Where(s => s.CompanyCode == companyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode)
                             .OrderBy(opr => opr.StudentNo)
                             .ToArray();
            return otherItems;

            ////return db.Student.Where(s => s.StudentRollNo == null && s.CompanyCode == companyCode
            ////    && s.BranchCode == BranchCode
            ////     && s.SessionCode == SessionCode
            ////     ).ToList();



        }

        public IList<Student> SelectAllStudent(string strValues)
        {
            string[] strArray = strValues.ToString().Split('/');

            int companyCode = Convert.ToInt32(strArray[0]);
            int BranchCode = Convert.ToInt32(strArray[1]);
            int SessionCode = Convert.ToInt32(strArray[2]);
            int ClassCode = Convert.ToInt32(strArray[3]);
            string Gender = strArray[4].ToString();


            SmsMisDB db = new SmsMisDB();
            return db.Student.Where(s => s.StudentRollNo == null && s.CompanyCode == companyCode
                && s.BranchCode == BranchCode
                 && s.SessionCode == SessionCode && s.ClassCode == ClassCode
                 && s.Gender == Gender

                 ).ToList();
        }

        public IList<object> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.Student.ToList();

            var otherItems = (from br in db.Student
                              select new
                              {
                                  CompanyCode = br.CompanyCode,
                                  BranchCode = br.BranchCode,
                                  SessionCode = br.SessionCode,
                                  FirstName = br.FirstName,
                                  FullName = br.FullName,
                                  StudentNo = br.StudentNo
                              })
                             .OrderBy(opr => opr.StudentNo)
                             .ToArray();
            return otherItems;
        }
        public IList<object> SelectAll(int CompanyCode, int BranchCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.Student.ToList();

            var otherItems = (from br in db.Student
                              select new
                              {
                                  CompanyCode = br.CompanyCode,
                                  BranchCode = br.BranchCode,
                                  SessionCode = br.SessionCode,
                                  FirstName = br.FirstName,
                                  FullName = br.FullName,
                                  StudentNo = br.StudentNo
                              }).Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode)
                             .OrderBy(opr => opr.StudentNo)
                             .ToArray();
            return otherItems;
        }
        public IList<object> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.Student.ToList();

            var otherItems = (from br in db.Student
                              select new
                              {
                                  CompanyCode = br.CompanyCode,
                                  BranchCode = br.BranchCode,
                                  SessionCode = br.SessionCode,
                                  FirstName = br.FirstName,
                                  FullName = br.FullName,
                                  StudentNo = br.StudentNo
                              }).Where(s => s.CompanyCode == CompanyCode)
                             .OrderBy(opr => opr.StudentNo)
                             .ToArray();
            return otherItems;
        }

        public IList<Student> SelectAll(int companyCode, int BranchCode, int SessionCode, int StudentNo)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Student.ToList<Student>().Where(opr => opr.BranchCode == BranchCode && opr.CompanyCode == companyCode && opr.SessionCode == SessionCode && opr.StudentNo == StudentNo).ToList();
        }

        public IList<KinshipDiscount> SelectAllKinshipDiscount()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.KinshipDiscount.ToList();
        }

        public void save(Student Student, string imgFile, string path)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Student.AddDateTime = DateTime.Now;
                    bool isNew = true;
                    var entry = context.Entry(Student);

                    if (entry != null)
                    {
                        if (!string.IsNullOrEmpty(imgFile))
                            Student.LogoPath = "../upload/students/";

                        if (Student.StudentNo == 0)
                        {
                            Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                            entry.State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            isNew = false;
                            if (Student.StudentApplicationCheckList != null && Student.StudentApplicationCheckList.Count > 0)
                                Student.StudentApplicationCheckList.ToList().ForEach(i => { i.StudentNo = Student.StudentNo; });

                            if (Student.StudentMarketingReference != null && Student.StudentMarketingReference.Count > 0)
                                Student.StudentMarketingReference.ToList().ForEach(i => { i.StudentNo = Student.StudentNo; });

                            if (Student.StudentLastClassSubject != null && Student.StudentLastClassSubject.Count > 0)
                                Student.StudentLastClassSubject.ToList().ForEach(i => { i.StudentNo = Student.StudentNo; });

                            if (Student.StudentKinship != null && Student.StudentKinship.Count > 0)
                                Student.StudentKinship.ToList().ForEach(i => { i.StudentNo = Student.StudentNo; });

                            if (Student.StudentSubjectList != null && Student.StudentSubjectList.Count > 0)
                                Student.StudentSubjectList.ToList().ForEach(i => { i.StudentNo = Student.StudentNo; });

                            if (Student.StudentMarketingReference != null && Student.StudentMarketingReference.Count > 0)
                                Student.StudentMarketingReference.ToList<StudentMarketingReference>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Added);

                            if (Student.StudentLastClassSubject != null && Student.StudentLastClassSubject.Count > 0)
                                Student.StudentLastClassSubject.ToList<StudentLastClassSubject>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Added);

                            if (Student.StudentKinship != null && Student.StudentKinship.Count > 0)
                                Student.StudentKinship.ToList<StudentKinship>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Added);

                            if (Student.StudentApplicationCheckList != null && Student.StudentApplicationCheckList.Count > 0)
                                Student.StudentApplicationCheckList.ToList<StudentApplicationCheckList>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Added);

                            if (Student.StudentSubjectList != null && Student.StudentSubjectList.Count > 0)
                                Student.StudentSubjectList.ToList<StudentSubject>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Added);

                            entry.State = System.Data.Entity.EntityState.Modified;
                        }

                        context.StudentApplicationCheckList.ToList().Where(i => i.CompanyCode == Student.CompanyCode && i.BranchCode == Student.BranchCode && i.SessionCode == Student.SessionCode && i.StudentNo == Student.StudentNo).ToList<StudentApplicationCheckList>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                        context.StudentMarketingReference.ToList().Where(i => i.CompanyCode == Student.CompanyCode && i.BranchCode == Student.BranchCode && i.SessionCode == Student.SessionCode && i.StudentNo == Student.StudentNo).ToList<StudentMarketingReference>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                        context.StudentLastClassSubject.ToList().Where(i => i.CompanyCode == Student.CompanyCode && i.BranchCode == Student.BranchCode && i.SessionCode == Student.SessionCode && i.StudentNo == Student.StudentNo).ToList<StudentLastClassSubject>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                        context.StudentKinship.ToList().Where(i => i.CompanyCode == Student.CompanyCode && i.BranchCode == Student.BranchCode && i.SessionCode == Student.SessionCode && i.StudentNo == Student.StudentNo).ToList<StudentKinship>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                        context.StudentSubject.ToList().Where(i => i.CompanyCode == Student.CompanyCode && i.BranchCode == Student.BranchCode && i.SessionCode == Student.SessionCode && i.StudentNo == Student.StudentNo).ToList<StudentSubject>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);

                        context.SaveChanges();

                        int studentNo = Student.StudentNo;

                        try
                        {
                            if (isNew)
                            {
                                Prefrences p = context.Prefrences.Where(a => a.CompanyCode == Student.CompanyCode && a.BranchCode == Student.BranchCode).FirstOrDefault();
                                if (p != null && p.StudentBillingCycle > 0)
                                {
                                    StudentBillingCycle sbc = new StudentBillingCycle();
                                    sbc.BiilingCycle = p.StudentBillingCycle;
                                    sbc.BranchCode = Student.BranchCode;
                                    sbc.CompanyCode = Student.CompanyCode;
                                    sbc.SessionCode = Student.SessionCode;
                                    sbc.StudentNo = Student.StudentNo;
                                    new SmsMis.Models.Console.Handlers.Fee.hdlStudentBillingCycle().save(sbc, Student.AddByUserId, true);
                                }
                            }
                        }
                        catch (Exception) { }

                        try
                        {
                            char[] split = { ',' };
                            string base64Image = imgFile.Split(split)[1];// data:image/jpeg;base64,
                            byte[] imageBytes = Convert.FromBase64String(base64Image);
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            Image image = Image.FromStream(ms, true);


                            image.Save(path + Student.CompanyCode + "_" + Student.BranchCode + "_" + Student.SessionCode + "_" + Student.StudentNo + ".png");
                        }
                        catch (Exception ex) { }
                    }

                    //List<object> parameters = new List<object>();

                    //string strQuery = string.Format("Exec SpSaveVoucher {0},{1},{2},{3},{4},{5},{6}", Student.CompanyCode, Student.BranchCode, Student.AddByUserId, Student.Amount, Student.StudentNo, (Student.VoucherCode == null ? 0 : Student.VoucherCode), (Student.VoucherNo == null ? 0 : Student.VoucherNo));
                    //context.Database.ExecuteSqlCommand(strQuery);

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
        public void delete(Student student)
        {
            try
            {
                var sgi = new SmsMisDB();

                if (student.StudentApplicationCheckList != null && student.StudentApplicationCheckList.Count > 0)
                    student.StudentApplicationCheckList.ToList().ForEach(i => { i.StudentNo = student.StudentNo; });

                if (student.StudentMarketingReference != null && student.StudentMarketingReference.Count > 0)
                    student.StudentMarketingReference.ToList().ForEach(i => { i.StudentNo = student.StudentNo; });

                if (student.StudentLastClassSubject != null && student.StudentLastClassSubject.Count > 0)
                    student.StudentLastClassSubject.ToList().ForEach(i => { i.StudentNo = student.StudentNo; });

                if (student.StudentKinship != null && student.StudentKinship.Count > 0)
                    student.StudentKinship.ToList().ForEach(i => { i.StudentNo = student.StudentNo; });

                if (student.StudentSubjectList != null && student.StudentSubjectList.Count > 0)
                    student.StudentSubjectList.ToList().ForEach(i => { i.StudentNo = student.StudentNo; });

                sgi.Student.Attach(student);
                var entry = sgi.Entry(student);
                if (entry != null)
                {
                    entry.State = System.Data.Entity.EntityState.Deleted;

                    sgi.StudentApplicationCheckList.ToList().Where(i => i.CompanyCode == student.CompanyCode && i.BranchCode == student.BranchCode && i.SessionCode == student.SessionCode && i.StudentNo == student.StudentNo).ToList<StudentApplicationCheckList>().ForEach(s => sgi.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                    sgi.StudentMarketingReference.ToList().Where(i => i.CompanyCode == student.CompanyCode && i.BranchCode == student.BranchCode && i.SessionCode == student.SessionCode && i.StudentNo == student.StudentNo).ToList<StudentMarketingReference>().ForEach(s => sgi.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                    sgi.StudentLastClassSubject.ToList().Where(i => i.CompanyCode == student.CompanyCode && i.BranchCode == student.BranchCode && i.SessionCode == student.SessionCode && i.StudentNo == student.StudentNo).ToList<StudentLastClassSubject>().ForEach(s => sgi.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                    sgi.StudentKinship.ToList().Where(i => i.CompanyCode == student.CompanyCode && i.BranchCode == student.BranchCode && i.SessionCode == student.SessionCode && i.StudentNo == student.StudentNo).ToList<StudentKinship>().ForEach(s => sgi.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                    sgi.StudentSubject.ToList().Where(i => i.CompanyCode == student.CompanyCode && i.BranchCode == student.BranchCode && i.SessionCode == student.SessionCode && i.StudentNo == student.StudentNo).ToList<StudentSubject>().ForEach(s => sgi.Entry(s).State = System.Data.Entity.EntityState.Deleted);

                    sgi.SaveChanges();

                   StudentBillingCycle sbc = new SmsMis.Models.Console.Handlers.Fee.hdlStudentBillingCycle().SelectAll(student.CompanyCode,student.BranchCode,student.SessionCode,student.StudentNo);
                   if (sbc != null)
                       new SmsMis.Models.Console.Handlers.Fee.hdlStudentBillingCycle().delete(sbc);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //throw Valency.Models.Console.Common.ExceptionTranslater.translate(ex);
            }
            catch (Exception ex)
            {
                //throw Valency.Models.Console.Common.ExceptionTranslater.translate(ex);
            }
        }


        #region Student Admission

        public void saveStudentAdmission(string strStudentNo, StudentAdmission stdAdmission, List<StudentAdmissionSubject> lstAdmissionSubjects)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    stdAdmission.AddDateTime = DateTime.Now;
                    var entry = context.Entry(stdAdmission);
                    if (entry != null)
                    {
                        entry.State = System.Data.Entity.EntityState.Added;
                        if (lstAdmissionSubjects != null && lstAdmissionSubjects.Count > 0)
                            lstAdmissionSubjects.ToList().ForEach(i => { i.StudentRollNo = stdAdmission.StudentRollNo; });

                        if (lstAdmissionSubjects != null && lstAdmissionSubjects.Count > 0)
                            lstAdmissionSubjects.ToList<StudentAdmissionSubject>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Added);

                        context.StudentAdmissionSubject.ToList().Where(i => i.CompanyCode == stdAdmission.CompanyCode && i.StudentRollNo == stdAdmission.StudentRollNo && i.ClassCode == stdAdmission.ClassCode && i.CourseCode == stdAdmission.CourseCode).ToList<StudentAdmissionSubject>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                        context.StudentAdmission.ToList().Where(i => i.CompanyCode == stdAdmission.CompanyCode && i.BranchCode == stdAdmission.BranchCode && i.SessionCode == stdAdmission.SessionCode && i.StudentRollNo == stdAdmission.StudentRollNo).ToList<StudentAdmission>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                        context.SaveChanges();
                    }
                    string strQuery = string.Format("Exec SpSaveStudentOther {0},{1},{2},{3},{4}", stdAdmission.CompanyCode, stdAdmission.BranchCode, stdAdmission.SessionCode, strStudentNo, stdAdmission.StudentRollNo);
                    context.Database.ExecuteSqlCommand(strQuery);
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

        #endregion

        #region Class with Snap
        private string SQL_CONN_STRING = GlobalConstants.connectionString;
        private SqlConnection GetConnection(string tstrConnectionString)
        {
            SqlConnection dbconnection = new SqlConnection(tstrConnectionString);
            try
            {
                dbconnection.Open();
                return dbconnection;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ExceptionTranslater(ex);
            }
        }

        public DataTable SelectClassWithSnap(string strValues)
        {
            SmsMisDB db = new SmsMisDB();

            string[] strArray = strValues.ToString().Split('/');

            int companyCode = Convert.ToInt32(strArray[0]);
            int BranchCode = strArray[1].ToString() == "undefined" ? 0 : Convert.ToInt32(strArray[1]);
            int SessionCode = strArray[2].ToString() == "undefined" || strArray[2].ToString() == "null" ? 0 : Convert.ToInt32(strArray[2]);
            int ClassCode = strArray[3].ToString() == "undefined" || strArray[3].ToString() == "null" ? 0 : Convert.ToInt32(strArray[3]);
            string Gender = strArray[4].ToString();
            int CourseCode = strArray[5].ToString() == "undefined" || strArray[5].ToString() == "null" ? 0 : Convert.ToInt32(strArray[5].ToString());
            int SubjectCode = strArray[6].ToString() == "undefined" || strArray[6].ToString() == "null" ? 0 : Convert.ToInt32(strArray[6].ToString());
            int SectionCode = strArray[7].ToString() == "undefined" || strArray[7].ToString() == "null" ? 0 : Convert.ToInt32(strArray[7].ToString());

            // SqlConnection that will be used to execute the sql commands
            SqlConnection connection = null;
            try
            {
                string qry = "Select distinct st.StudentNo,st.FullName,sn.SessionName,cl.ClassName,st.Gender,co.CompanyName,br.BranchName from Student st with(nolock) inner join Company co with(nolock) on st.CompanyCode = co.CompanyCode   inner join Branch br with(nolock) on br.CompanyCode = st.CompanyCode and br.BranchCode = st.BranchCode    inner join [Session] sn with(nolock) on sn.CompanyCode = st.CompanyCode and sn.SessionCode = st.SessionCode   inner join [Class] cl with(nolock) on cl.ClassCode = st.ClassCode and cl.CompanyCode = st.CompanyCode  inner join StudentAdmissionSubject sas with(nolock) on sas.CompanyCode = st.CompanyCode and sas.StudentRollNo = st.StudentRollNo inner join StudentAdmission adm with(nolock) on adm.StudentRollNo = st.StudentRollNo And adm.CompanyCode = st.CompanyCode and adm.BranchCode = st.BranchCode and adm.SessionCode = st.SessionCode Where  st.CompanyCode = @CompanyCode and   1 = Case When isnull(@BranchCode,0) > 0 And st.BranchCode = @BranchCode then 1 When isnull(@BranchCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SessionCode,0) > 0 And st.SessionCode = @SessionCode then 1 When isnull(@SessionCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@ClassCode,0) > 0 And st.ClassCode = @ClassCode then 1 When isnull(@ClassCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SubjectCode,0) > 0 And sas.SubjectCode = @SubjectCode then 1 When isnull(@SubjectCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@CourseCode,0) > 0 And sas.CourseCode = @CourseCode then 1 When isnull(@CourseCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@Gender,'') <> '' And st.Gender = @Gender then 1 When isnull(@Gender,'') = '' then 1 Else 0 End  And 1 = Case When isnull(@ClassSection,0) > 0 And adm.SectionCode = @ClassSection then 1 When isnull(@ClassSection,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SubjectSection,0) > 0 And sas.SectionCode = @SubjectSection then 1 When isnull(@SubjectSection,0) = 0 then 1 Else 0 End  And st.StudentRollNo is not null";
                connection = GetConnection(SQL_CONN_STRING);

                SqlParameter[] paramList = new SqlParameter[]{ new SqlParameter("@companyCode",companyCode),
                new SqlParameter("@BranchCode",BranchCode),
                new SqlParameter("@SessionCode",SessionCode),
                new SqlParameter("@ClassCode",ClassCode),
                new SqlParameter("@Gender",Gender),
                new SqlParameter("@CourseCode",CourseCode),
                new SqlParameter("@SubjectCode",SubjectCode),
                new SqlParameter("@ClassSection",SectionCode),
                new SqlParameter("@SubjectSection","0")
                
                };


                DataTable dt = SqlHelper.ExecuteDataTable(connection, CommandType.Text, qry, paramList);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ExceptionTranslater(ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            //"Select distinct st.StudentNo,st.FullName,sn.SessionName,cl.ClassName,st.Gender,co.CompanyName,br.BranchName from Student st with(nolock) inner join Company co with(nolock) on st.CompanyCode = co.CompanyCode   inner join Branch br with(nolock) on br.CompanyCode = st.CompanyCode and br.BranchCode = st.BranchCode    inner join [Session] sn with(nolock) on sn.CompanyCode = st.CompanyCode and sn.SessionCode = st.SessionCode   inner join [Class] cl with(nolock) on cl.ClassCode = st.ClassCode and cl.CompanyCode = st.CompanyCode  inner join StudentAdmissionSubject sas with(nolock) on sas.CompanyCode = st.CompanyCode and sas.StudentRollNo = st.StudentRollNo inner join StudentAdmission adm with(nolock) on adm.StudentRollNo = st.StudentRollNo And adm.CompanyCode = st.CompanyCode and adm.BranchCode = st.BranchCode and adm.SessionCode = st.SessionCode Where  st.CompanyCode = @CompanyCode and   1 = Case When isnull(@BranchCode,0) > 0 And st.BranchCode = @BranchCode then 1 When isnull(@BranchCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SessionCode,0) > 0 And st.SessionCode = @SessionCode then 1 When isnull(@SessionCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@ClassCode,0) > 0 And st.ClassCode = @ClassCode then 1 When isnull(@ClassCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SubjectCode,0) > 0 And sas.SubjectCode = @SubjectCode then 1 When isnull(@SubjectCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@CourseCode,0) > 0 And sas.CourseCode = @CourseCode then 1 When isnull(@CourseCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@Gender,'') <> '' And st.Gender = @Gender then 1 When isnull(@Gender,'') = '' then 1 Else 0 End  And 1 = Case When isnull(@ClassSection,0) > 0 And adm.SectionCode = @ClassSection then 1 When isnull(@ClassSection,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SubjectSection,0) > 0 And sas.SectionCode = @SubjectSection then 1 When isnull(@SubjectSection,0) = 0 then 1 Else 0 End  And st.StudentRollNo is not null";


            //return db.Student.Where(s => s.CompanyCode == companyCode && s.BranchCode == BranchCode
            //     && s.SessionCode == SessionCode 
            //     && s.ClassCode == ClassCode
            //     && s.Gender == Gender
            //     ).ToList();
        }

        #endregion





    }
}
