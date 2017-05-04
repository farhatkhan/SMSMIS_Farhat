using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsMis.Models.Console.Common;

namespace SmsMis.Models.Console.Common
{
    public static class Functions
    {
        private static string SQL_CONN_STRING = GlobalConstants.connectionString;
        //private Functions() { } //no instances
        public static string EncryptString(string inputString)
        {
            return new aliasBit.Encryption.RijndaelEnhanced("sGIVAlEncy_KeY").Encrypt(inputString);
        }
        public static string DecryptString(string inputString)
        {
            return new aliasBit.Encryption.RijndaelEnhanced("sGIVAlEncy_KeY").Decrypt(inputString);
        }

        public static int getNextPk(string strDbObject, int iCurrentPK, int iCompanyCode, int iBranchCode, int iSessionCode)
        {
            int primaryKey = 0;
            if (iCurrentPK > 0) primaryKey = iCurrentPK;
            else
            {
                using (SqlConnection conn = GetConnection(SQL_CONN_STRING))
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            if (strDbObject == "Student")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(StudentNo) is null then 1 else MAX(StudentNo)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1} And branchCode = {2} And SessionCode = {3}", strDbObject, iCompanyCode, iBranchCode, iSessionCode));

                            trans.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            trans.Rollback();
                            throw new ExceptionTranslater(ex);
                        }
                        finally
                        {
                            if (conn != null && conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                    }
                }
            }

            return primaryKey;
        }

        public static int getNextPk(string strDbObject, int iCurrentPK, int iCompanyCode, int iBranchCode)
        {
            int primaryKey = 0;
            if (iCurrentPK > 0) primaryKey = iCurrentPK;
            else
            {
                using (SqlConnection conn = GetConnection(SQL_CONN_STRING))
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            if (strDbObject == "House")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(HouseCode) is null then 1 else MAX(HouseCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1} And branchCode = {2}", strDbObject, iCompanyCode, iBranchCode));
                            if (strDbObject == "BranchBuilding")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(BuildingCode) is null then 1 else MAX(BuildingCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1} And branchCode = {2}", strDbObject, iCompanyCode, iBranchCode));
                            trans.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            trans.Rollback();
                            throw new ExceptionTranslater(ex);
                        }
                        finally
                        {
                            if (conn != null && conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                    }
                }
            }

            return primaryKey;
        }

        public static int getNextPk(string strDbObject, int iCurrentPK, int iCompanyCode)
        {
            int primaryKey = 0;
            //if (iCurrentPK > 0) primaryKey = iCurrentPK;
            //else
            {
                using (SqlConnection conn = GetConnection(SQL_CONN_STRING))
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            //string PKColumn = Enum.Parse(typeof(ObjectType), strDbObject).ToString();
                            if (strDbObject == "Branch")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(BranchCode) is null then 1 else MAX(BranchCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Session")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(SessionCode) is null then 1 else MAX(SessionCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Subject")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(SubjectCode) is null then 1 else MAX(SubjectCode)+1 end FROM {0} WITH(NOLOCK)", strDbObject));
                            else if (strDbObject == "Class")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(ClassCode) is null then 1 else MAX(ClassCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Course")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(CourseCode) is null then 1 else MAX(CourseCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Section")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(SectionCode) is null then 1 else MAX(SectionCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Religion")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(ReligionCode) is null then 1 else MAX(ReligionCode)+1 end FROM {0} WITH(NOLOCK)", strDbObject));
                            else if (strDbObject == "SeatType")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(SeatTypeCode) is null then 1 else MAX(SeatTypeCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "MarketingReference")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(MarketingReferenceCode) is null then 1 else MAX(MarketingReferenceCode)+1 end FROM {0} WITH(NOLOCK)", strDbObject));
                            else if (strDbObject == "FeeParticular")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(ParticularCode) is null then 1 else MAX(ParticularCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Category")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(CategoryCode) is null then 1 else MAX(CategoryCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Type")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(TypeCode) is null then 1 else MAX(TypeCode)+1 end FROM {0} WITH(NOLOCK)", strDbObject));

                            else if (strDbObject == "DocType")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(DocCode) is null then 1 else MAX(DocCode)+1 end FROM {0} WITH(NOLOCK)", strDbObject));
                            else if (strDbObject == "Grade")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(GradeCode) is null then 1 else MAX(GradeCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Department")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(DepartmentCode) is null then 1 else MAX(DepartmentCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Designation")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(DesignationCode) is null then 1 else MAX(DesignationCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Branch")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(BranchCode) is null then 1 else MAX(BranchCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));
                            else if (strDbObject == "Session")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(SessionCode) is null then 1 else MAX(SessionCode)+1 end FROM {0} WITH(NOLOCK) Where companyCode = {1}", strDbObject, iCompanyCode));

                            else if (strDbObject == "Nationality")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(NationalityCode) is null then 1 else MAX(NationalityCode)+1 end FROM {0} WITH(NOLOCK)", strDbObject));
                            else if (strDbObject == "InstrumentType")
                                primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Format("SELECT case when MAX(InstrumentCode) is null then 1 else MAX(InstrumentCode)+1 end FROM {0} WITH(NOLOCK)", strDbObject));
                            
                            trans.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            trans.Rollback();
                            throw new ExceptionTranslater(ex);
                        }
                        finally
                        {
                            if (conn != null && conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                    }
                }
            }

            return primaryKey;
        }

        public static int getNextPk(string strDbObject, string strField, string strWhere)
        {
            int primaryKey = 0;
            //if (iCurrentPK > 0) primaryKey = iCurrentPK;
            
            
                using (SqlConnection conn = GetConnection(SQL_CONN_STRING))
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, string.Concat("SELECT case when MAX(", strField, ") is null then 1 else MAX(", strField, ")+1 end FROM ", strDbObject, " WITH(NOLOCK) ", strWhere));
                            trans.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            trans.Rollback();
                            throw new ExceptionTranslater(ex);
                        }
                        finally
                        {
                            if (conn != null && conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                    }
                }

            return primaryKey;
        }

        private static SqlConnection GetConnection(string tstrConnectionString)
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

        public enum ObjectType
        {
            Branch = 1
        }
    }
}
