using Microsoft.ApplicationBlocks.Data;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using SmsMis.Models.Console.Common;
using System.IO;
using System.Drawing;

namespace SmsMis.Models.Console
{
    public class CompanyHandler
    {
        private string SQL_CONN_STRING = GlobalConstants.connectionString;
        private const string SQL_DELETE_Company = "DELETE FROM Company WHERE CompanyCode = @CompanyCode";
        private const string SQL_SELECT_Company = "SELECT * FROM Company  WITH(NOLOCK) WHERE CompanyCode = @CompanyCode";
        private const string SQL_SELECT_ALL_Company = "SELECT * FROM Company";
        private const string SQL_SELECT_ALL_Company_Active = "SELECT * FROM Company Where isnull(status,0) = 1";
        private const string SQL_INSERT_Company = "INSERT INTO Company (CompanyCode,CompanyName, ShortName, Salogan, ContactPerson, Address, Phone1, Phone2, Phone3, Phone4, Fax1, Fax2, URL, Eamil1, Email2, STRNo, NTN, LogoPath, Status, AddByUserId, AddDateTime) VALUES(@CompanyCode,@CompanyName, @ShortName, @Salogan, @ContactPerson, @Address, @Phone1, @Phone2, @Phone3, @Phone4, @Fax1, @Fax2, @URL, @Eamil1, @Email2, @STRNo, @NTN, @LogoPath, @Status, @AddByUserId, GETDATE()) SELECT @@IDENTITY";
        private const string SQL_UPDATE_Company = "UPDATE Company SET CompanyName = @CompanyName, ShortName = @ShortName, Salogan = @Salogan, ContactPerson = @ContactPerson, Address = @Address, Phone1 = @Phone1, Phone2 = @Phone2, Phone3 = @Phone3, Phone4 = @Phone4, Fax1 = @Fax1, Fax2 = @Fax2, URL = @URL, Eamil1 = @Eamil1, Email2 = @Email2, STRNo = @STRNo, NTN = @NTN, LogoPath = @LogoPath, Status = @Status, AddByUserId = @AddByUserId, AddDateTime = @AddDateTime WHERE CompanyCode = @CompanyCode if @@ROWCOUNT = 0 Begin INSERT INTO Company (CompanyCode,CompanyName, ShortName, Salogan, ContactPerson, Address, Phone1, Phone2, Phone3, Phone4, Fax1, Fax2, URL, Eamil1, Email2, STRNo, NTN, LogoPath, Status, AddByUserId, AddDateTime) VALUES(@CompanyCode,@CompanyName, @ShortName, @Salogan, @ContactPerson, @Address, @Phone1, @Phone2, @Phone3, @Phone4, @Fax1, @Fax2, @URL, @Eamil1, @Email2, @STRNo, @NTN, @LogoPath, @Status, @AddByUserId, GETDATE()) SELECT @@IDENTITY End";
        private const string SQL_SELECT_PRIMARYKEY = "SELECT case when MAX(COMPANYCODE) is null then 1 else MAX(COMPANYCODE)+1 end FROM Company WITH(NOLOCK)";
        private const string PARAM_CompanyCode = "@CompanyCode";
        private const string PARAM_CompanyName = "@CompanyName";
        private const string PARAM_ShortName = "@ShortName";
        private const string PARAM_Salogan = "@Salogan";
        private const string PARAM_ContactPerson = "@ContactPerson";
        private const string PARAM_Address = "@Address";
        private const string PARAM_Phone1 = "@Phone1";
        private const string PARAM_Phone2 = "@Phone2";
        private const string PARAM_Phone3 = "@Phone3";
        private const string PARAM_Phone4 = "@Phone4";
        private const string PARAM_Fax1 = "@Fax1";
        private const string PARAM_Fax2 = "@Fax2";
        private const string PARAM_URL = "@URL";
        private const string PARAM_Eamil1 = "@Eamil1";
        private const string PARAM_Email2 = "@Email2";
        private const string PARAM_STRNo = "@STRNo";
        private const string PARAM_NTN = "@NTN";
        private const string PARAM_LogoPath = "@LogoPath";
        private const string PARAM_Status = "@Status";
        private const string PARAM_AddByUserId = "@AddByUserId";
        private const string PARAM_AddDateTime = "@AddDateTime";
        public void Save(Company objCompany, string imgFile, string path)
        {
            //Get the connection 
            SqlConnection con = GetConnection(SQL_CONN_STRING);

            try
            {
                if (objCompany != null)
                {
                    //if (!string.IsNullOrEmpty(imgFile))
                    //    Student.LogoPath = "../upload/students/";
                    int iCompanyCode = 0;

                    //Get the parameter list needed by the given object
                    SqlParameter[] lParamArray = GetParameters(objCompany);
                    SetParameters(lParamArray, objCompany, ref iCompanyCode, imgFile);
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, SQL_UPDATE_Company, lParamArray);

                    try
                    {
                        char[] split = { ',' };
                        string base64Image = imgFile.Split(split)[1];// data:image/jpeg;base64,
                        byte[] imageBytes = Convert.FromBase64String(base64Image);
                        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                        ms.Write(imageBytes, 0, imageBytes.Length);
                        Image image = Image.FromStream(ms, true);

                        image.Save(path + iCompanyCode + ".png");
                        ms.Close();
                        ms.Dispose();
                    }
                    catch (Exception ex) { }

                }
            }
            catch (Exception ex)
            {
                throw new ExceptionTranslater(ex);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public Company Select(string strCompanyCode)
        {
            Company m = new Company();
            return m;

            // SqlConnection that will be used to execute the sql commands
            //SqlConnection connection = null;
            //SqlParameter[] aParms = new SqlParameter[] { new SqlParameter(PARAM_CompanyCode, strCompanyCode) };
            //try
            //{
            //    connection = GetConnection(SQL_CONN_STRING);
            //    DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, SQL_SELECT_Company, aParms);
            //    // read the contents of data table and return the results:
            //    if (dtCompany.Rows.Count > 0)
            //    {
            //        return new Company(
            //            int.Parse(dtCompany.Rows[0]["CompanyCode"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["CompanyName"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["ShortName"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Salogan"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["ContactPerson"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Address"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Phone1"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Phone2"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Phone3"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Phone4"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Fax1"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Fax2"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["URL"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Eamil1"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["Email2"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["STRNo"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["NTN"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["LogoPath"].ToString()),
            //            bool.Parse(dtCompany.Rows[0]["Status"].ToString()),
            //            Convert.ToString(dtCompany.Rows[0]["AddByUserId"].ToString()),
            //            System.DateTime.Parse(dtCompany.Rows[0]["AddDateTime"].ToString()));
            //    }
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    throw new ExceptionTranslater(ex);
            //}
            //finally
            //{
            //    if (connection != null && connection.State == ConnectionState.Open)
            //        connection.Close();
            //}
        }
        public DataTable SelectAll()
        {
            // SqlConnection that will be used to execute the sql commands
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(SQL_CONN_STRING);
                DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, SQL_SELECT_ALL_Company);
                if (dtCompany.Rows.Count > 0)
                {
                    return dtCompany;
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
        }

        //public DataTable SelectAllActiveCompany()
        //{
        //    // SqlConnection that will be used to execute the sql commands
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = GetConnection(SQL_CONN_STRING);
        //        DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, SQL_SELECT_ALL_Company_Active);
        //        if (dtCompany.Rows.Count > 0)
        //        {
        //            return dtCompany;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ExceptionTranslater(ex);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open)
        //            connection.Close();
        //    }
        //}
        public int Delete(int iCompanyId)
        {
            SqlConnection connection = null;
            SqlParameter[] aParms = new SqlParameter[] { new SqlParameter(PARAM_CompanyCode, iCompanyId) };
            try
            {
                connection = GetConnection(SQL_CONN_STRING);
                if (connection == null)
                    return -2;
                int i = SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_DELETE_Company, aParms);
                return i;
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
        }
        /// <summary>
        /// Update a object given the object primary key
        /// </summary>
        /// <param name="tstrobjectId"></param>
        /// <returns></returns>
        public int Update(Company objCompany)
        {
            int iCompanyCode = 0;
            SqlParameter[] aParms = GetParameters(objCompany);
            SetParameters(aParms, objCompany, ref iCompanyCode, "");
            using (SqlConnection conn = GetConnection(SQL_CONN_STRING))
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int i = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SQL_UPDATE_Company, aParms);
                        trans.Commit();
                        return i;
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
        private SqlParameter[] GetParameters(Company objCompany)
        {
            SqlParameter[] objParamArray = SqlHelperParameterCache.GetCachedParameterSet(SQL_CONN_STRING, SQL_INSERT_Company);
            if (objParamArray == null)
            {
                //Represents a parameter to a System.Data.SqlClient.SqlCommand, 
                //and optionally, its mapping to System.Data.DataSet columns. 
                objParamArray = new SqlParameter[]
            {
                new SqlParameter(PARAM_CompanyCode, objCompany.CompanyCode),
                new SqlParameter(PARAM_CompanyName, objCompany.CompanyName),
                new SqlParameter(PARAM_ShortName, objCompany.ShortName),
                new SqlParameter(PARAM_Salogan, objCompany.Salogan),
                new SqlParameter(PARAM_ContactPerson, objCompany.ContactPerson),
                new SqlParameter(PARAM_Address, objCompany.Address),
                new SqlParameter(PARAM_Phone1, objCompany.Phone1),
                new SqlParameter(PARAM_Phone2, objCompany.Phone2),
                new SqlParameter(PARAM_Phone3, objCompany.Phone3),
                new SqlParameter(PARAM_Phone4, objCompany.Phone4),
                new SqlParameter(PARAM_Fax1, objCompany.Fax1),
                new SqlParameter(PARAM_Fax2, objCompany.Fax2),
                new SqlParameter(PARAM_URL, objCompany.URL),
                new SqlParameter(PARAM_Eamil1, objCompany.Eamil1),
                new SqlParameter(PARAM_Email2, objCompany.Email2),
                new SqlParameter(PARAM_STRNo, objCompany.STRNo),
                new SqlParameter(PARAM_NTN, objCompany.NTN),
                new SqlParameter(PARAM_LogoPath, objCompany.LogoPath),
                new SqlParameter(PARAM_Status, objCompany.Status),
                new SqlParameter(PARAM_AddByUserId, objCompany.AddByUserId),
                new SqlParameter(PARAM_AddDateTime, objCompany.AddDateTime),
            };
                SqlHelperParameterCache.CacheParameterSet(SQL_CONN_STRING, SQL_INSERT_Company, objParamArray);
            }
            return objParamArray;
        }
        private void SetParameters(SqlParameter[] CompanyParms, Company objCompany, ref int icompanyCode, string imgFile)
        {
            CompanyParms[0].Value = icompanyCode = getCompanyPk(objCompany.CompanyCode);
            CompanyParms[1].Value = objCompany.CompanyName;
            CompanyParms[2].Value = objCompany.ShortName;
            CompanyParms[3].Value = objCompany.Salogan;
            if (objCompany.ContactPerson == null) CompanyParms[4].Value = DBNull.Value; else CompanyParms[4].Value = objCompany.ContactPerson;
            CompanyParms[5].Value = objCompany.Address;
            CompanyParms[6].Value = objCompany.Phone1;
            if (objCompany.Phone2 == null) CompanyParms[7].Value = DBNull.Value; else CompanyParms[7].Value = objCompany.Phone2;
            if (objCompany.Phone3 == null) CompanyParms[8].Value = DBNull.Value; else CompanyParms[8].Value = objCompany.Phone3;
            if (objCompany.Phone4 == null) CompanyParms[9].Value = DBNull.Value; else CompanyParms[9].Value = objCompany.Phone4;
            if (objCompany.Fax1 == null) CompanyParms[10].Value = DBNull.Value; else CompanyParms[10].Value = objCompany.Fax1;
            if (objCompany.Fax2 == null) CompanyParms[11].Value = DBNull.Value; else CompanyParms[11].Value = objCompany.Fax2;
            if (objCompany.URL == null) CompanyParms[12].Value = DBNull.Value; else CompanyParms[12].Value = objCompany.URL;
            if (objCompany.Eamil1 == null) CompanyParms[13].Value = DBNull.Value; else CompanyParms[13].Value = objCompany.Eamil1;
            if (objCompany.Email2 == null) CompanyParms[14].Value = DBNull.Value; else CompanyParms[14].Value = objCompany.Email2;
            if (objCompany.STRNo == null) CompanyParms[15].Value = DBNull.Value; else CompanyParms[15].Value = objCompany.STRNo;
            if (objCompany.NTN == null) CompanyParms[16].Value = DBNull.Value; else CompanyParms[16].Value = objCompany.NTN;
            if (objCompany.LogoPath == null)
                CompanyParms[17].Value = string.Empty;
            else
                CompanyParms[17].Value = string.Concat("../upload/companies/", CompanyParms[0].Value, ".png");
            CompanyParms[18].Value = objCompany.Status;
            CompanyParms[19].Value = objCompany.AddByUserId;
            CompanyParms[20].Value = DateTime.Now;
        }

        private int getCompanyPk(int iCompanyCode)
        {
            int primaryKey = 0;
            if (iCompanyCode > 0) primaryKey = iCompanyCode;
            else
            {
                using (SqlConnection conn = GetConnection(SQL_CONN_STRING))
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            primaryKey = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, SQL_SELECT_PRIMARYKEY);
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
    }
}