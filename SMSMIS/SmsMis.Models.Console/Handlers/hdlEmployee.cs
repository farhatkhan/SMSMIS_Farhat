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
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;


namespace SmsMis.Models.Console.Handlers.Client
{
    public class hdlEmployee : DbContext
    {
        public hdlEmployee() : base("name=ValencySGIEntities") { }

        public IList<Employee> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Employee.ToList();
        }
        public IList<Object> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var otherItems = (from p in db.Employee
                              //from e in db.COABranch.Where(e => e.CompanyCode == p.CompanyCode ).DefaultIfEmpty()
                              select new
                              {
                                  CompanyCode = p.CompanyCode,
                                  EmployeeCode = p.EmployeeCode,
                                  EmployeeName = p.EmployeeName
                              }).Where(a => a.CompanyCode == CompanyCode).ToArray();
            return otherItems;
            //return db.Employee.ToList<Employee>().Where(s => s.CompanyCode == CompanyCode).ToList();
        }
        public IList<Employee> SelectAll(int CompanyCode, int EmployeeCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Employee.ToList<Employee>().Where(s => s.CompanyCode == CompanyCode && s.EmployeeCode == EmployeeCode).ToList();
        }

        public IList<KinshipDiscount> SelectAllKinshipDiscount()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.KinshipDiscount.ToList();
        }

        public void save(Employee Employee, string imgEmployeePhoneFile, string imgSignature, string path, string signaturePath)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Employee.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Employee);

                    if (entry != null)
                    {
                        if (!string.IsNullOrEmpty(imgEmployeePhoneFile))
                            Employee.EmployeePhotoPath = "../upload/Employees/";
                        if (!string.IsNullOrEmpty(imgSignature))
                            Employee.EmployeeSignaturePath = "../upload/Employees/Signatures/";

                        //Employee.EmployeeSignaturePath = signaturePath;

                        if (Employee.EmployeeCode == 0)
                        {
                            Employee.EmployeeCode = Functions.getNextPk("Employee", "EmployeeCode", string.Concat("WHERE CompanyCode =", Employee.CompanyCode));
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            entry.State = EntityState.Modified;
                        }

                        context.SaveChanges();

                        try
                        {
                            char[] split = { ',' };
                            string base64Image = imgEmployeePhoneFile.Split(split)[1];// data:image/jpeg;base64,
                            byte[] imageBytes = Convert.FromBase64String(base64Image);
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            Image image = Image.FromStream(ms, true);


                            image.Save(path + Employee.CompanyCode + "_" + Employee.EmployeeCode + ".png");
                        }
                        catch (Exception ex) { }
                        try
                        {
                            char[] split = { ',' };
                            string base64Image = imgSignature.Split(split)[1];// data:image/jpeg;base64,
                            byte[] imageBytes = Convert.FromBase64String(base64Image);
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
                            Image image = Image.FromStream(ms, true);


                            image.Save(signaturePath + Employee.CompanyCode + "_" + Employee.EmployeeCode + ".png");
                        }
                        catch (Exception ex) { }
                    }

                    //List<object> parameters = new List<object>();

                    //string strQuery = string.Format("Exec SpSaveVoucher {0},{1},{2},{3},{4},{5},{6}", Employee.CompanyCode, Employee.BranchCode, Employee.AddByUserId, Employee.Amount, Employee.EmployeeCode, (Employee.VoucherCode == null ? 0 : Employee.VoucherCode), (Employee.VoucherNo == null ? 0 : Employee.VoucherNo));
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
        public void delete(Employee Employee)
        {
            try
            {
                var context = new SmsMisDB();
                context.Employee.Attach(Employee);
                var entry = context.Entry(Employee);
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
